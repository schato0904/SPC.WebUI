<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0401.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0401" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var sINSPKINDCD = '';
        var sMEASURE = '';
        var inspVisibleIndex = -1;
        var changedVisibleIndex = [];
        var ngClasses = {};
        var ngArray = [];
        var sOldMeasure = null;
        var sOldJudge = null;
        var chartWidth = 0;
        var chartHeight = 0;
        var chartResized = false;
        var viewVisibleIndex = -1;

        $(document).ready(function () {
            // 입력상자 Enter Key Event
            $('#inputTable input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_DoSaveNgReason();
                    return false;
                }
            });

            // 점검일에서 엔터키 입력 시 점검항목 목록 조회
            $('#inspSearchTable input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_OnDateChanged();
                    return false;
                }
            });

            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);

            top = $(".container_r").offset().top;
            searchHeight = $(".search_r").height() > 0 ? $(".search_r").height() + 6 : 0;
            pagingHeight = $(".paging_r").height() > 0 ? $(".paging_r").height() - 20 : 0;
            height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGridInsp.SetHeight(height);

            $('#machImage').dialog('option', 'width', $(document).width() - 20);
            $('#machImage').dialog('option', 'height', $(document).height() - 20);
            $('#divMachImage').width($(document).width() - 40).height($(document).height() - 80);

            chartWidth = parseInt($(".paging_r").width() - 310, 10);
            chartHeight = 180;

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart, chartResized, chartWidth, chartHeight);
        });

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            fn_OnInputSelectClear();
            devGrid.PerformCallback();
            devGridInsp.PerformCallback();
        }

        // 점검항목조회
        function fn_OnSubSearchClick() {
            fn_doSetGridEventAction('true');
            devGridInsp.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() { }

        // 수정
        function fn_OnModifyClick() { }

        // 저장
        function fn_OnSaveClick() {
            if (!isSelectMach()) { alert('설비점검항목기준을 등록/삭제할 설비를 선택하세요'); return false; }
            if (!devGridInsp.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else {
                if (!gridIsValid)
                    alert('입력값을 확인해보세요');
                else {
                    //var rowKeys = [];

                    //changedVisibleIndex.forEach(function (index) {
                    //    if (devGridInsp.batchEditApi.GetCellValue(index, 'F_JUDGE') == 'AAG702')
                    //        rowKeys.push(devGridInsp.GetRowKey(index));
                    //});

                    //if (rowKeys.length > 0) {
                    //    fn_OnPopupNgReason(rowKeys.join('$'));
                    //} else {
                    //    fn_DoSave();
                    //}
                    fn_DoSave();
                }
            }
        }

        // 이상발생 등록 후 저장
        function fn_DoSave() {
            devGridInsp.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            devGridInsp.UnselectAllRowsOnPage();
            devGridInsp.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            sINSPUSER = devGridInsp.GetRowKey(inspVisibleIndex).split('|')[3];

            if (sINSPUSER == '' || sINSPUSER == '<%=gsUSERID%>') {
                devGridInsp.GetEditor("F_MEASURE").SetValue(null);
                devGridInsp.GetEditor("F_JUDGE").SetValue(null);
            } else {
                alert('점검전이거나 본인이 등록한 점검기록만 삭제할 수 있습니다.');
            }
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }

            fn_RendorTotalCount();
        }

        // 점검 Grid End Callback
        function fn_OnGridInspEndCallback(s, e) {
            if (s.cpResultCode != '') {
                if (s.cpResultCode == '9') {
                    ngArray = [];
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            gridValids = [];
            gridValidIdx = 0;
            gridIsValid = true;

            if (sINSPKINDCD == 'AAG601') {
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_MEASURE", s, e);
            }
            gridValids[gridValidIdx++] = fn_OnBatchValidate("F_JUDGE", s, e);

            gridValids.forEach(function (gridValid) {
                if (!gridValid) {
                    gridIsValid = false;
                }
            });

            if ((devGridInsp.GetEditor("F_MEASURE").GetValue() == '' || devGridInsp.GetEditor("F_MEASURE").GetValue() == null)
                && (devGridInsp.GetEditor("F_JUDGE").GetValue() == '' || devGridInsp.GetEditor("F_JUDGE").GetValue() == null)) {
                fn_OnBatchValidateDisable("F_MEASURE", s, e)
                fn_OnBatchValidateDisable("F_JUDGE", s, e)
                gridIsValid = true;

                if (changedVisibleIndex.indexOf(e.visibleIndex) >= 0)
                    changedVisibleIndex.pop(e.visibleIndex);
            } else if (devGridInsp.GetEditor("F_MEASURE").GetValue() != '' && devGridInsp.GetEditor("F_MEASURE").GetValue() != null
                && devGridInsp.GetEditor("F_JUDGE").GetValue() != '' && devGridInsp.GetEditor("F_JUDGE").GetValue() != null) {
                if (changedVisibleIndex.indexOf(e.visibleIndex) >= 0)
                    changedVisibleIndex.pop(e.visibleIndex);
                changedVisibleIndex.push(e.visibleIndex);
            }

            return gridIsValid;
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            // 관리자 확인된 경우 수정 금지
            if (fn_GetCastValue('srcF_CONFIRM') != '') {
                e.cancel = true;
            } else {
                inspVisibleIndex = e.visibleIndex;
                var rowKeys = devGridInsp.GetRowKey(inspVisibleIndex).split('|');
                sINSPUSER = rowKeys[3];

                if (sINSPUSER == '' || sINSPUSER == '<%=gsUSERID%>') {
                    sOldMeasure = rowKeys[6];
                    sOldJudge = rowKeys[7];

                    sINSPKINDCD = rowKeys[2];

                    if (sINSPKINDCD == 'AAG601') {
                        fn_OnControlEnableBox(s.GetEditor('F_MEASURE'), null);
                    } else {
                        fn_OnControlDisableBox(s.GetEditor('F_MEASURE'), null);
                    }
                } else {
                    e.cancel = true;
                }
            }
        }

        // Grid Row DblClick
        function fn_OnRowDblClick(s, e) {
            ngArray = [];
            fn_doSetGridEventAction('false');
            fn_SetDate('txtFROMDT', new Date());
            fn_SetTextValue('hidUCFROMDT', fn_GetCastText('txtFROMDT'));
            fn_OnInputSelectData(devGrid.GetRowKey(e.visibleIndex).split('|'));
            fn_devCallback_PerformCallback();
            devGridInsp.PerformCallback();
            viewVisibleIndex = -1;
            fn_OnChartDoCallback(devChart, chartWidth, chartHeight);
        }

        // 입력폼 초기화
        function fn_OnInputSelectClear() {
            fn_SetTextValue('srcF_MACHIDX', '');
            fn_SetTextValue('srcF_MACHCD', '');
            fn_SetTextValue('srcF_MACHNM', '');
            fn_SetTextValue('srcF_BANCD', '');
            fn_SetTextValue('srcF_BANNM', '');
            fn_SetTextValue('srcF_LINECD', '');
            fn_SetTextValue('srcF_LINENM', '');
            fn_SetTextValue('srcF_IMAGENO', '');
            fn_SetDevImage('');
        }

        // 입력폼 조회값입력
        function fn_OnInputSelectData(rowKey) {
            fn_SetTextValue('srcF_MACHIDX', rowKey[0]);
            fn_SetTextValue('srcF_MACHCD', rowKey[1]);
            fn_SetTextValue('srcF_MACHNM', rowKey[2]);
            fn_SetTextValue('srcF_BANCD', rowKey[3]);
            fn_SetTextValue('srcF_BANNM', rowKey[4]);
            fn_SetTextValue('srcF_LINECD', rowKey[5]);
            fn_SetTextValue('srcF_LINENM', rowKey[6]);
            fn_SetTextValue('srcF_IMAGENO', rowKey[7]);
            fn_SetDevImage('');
        }

        // 설비선택여부
        function isSelectMach() {
            return fn_GetCastValue('srcF_MACHIDX') != '' && fn_GetCastValue('srcF_MACHCD') != '' && fn_GetCastValue('srcF_MACHNM') != '';
        }

        // 등록된 이미지 로드
        function fn_SetDevImage(sIMAGENO) {
        fn_SetTextValue('txtIMAGESEQ', sIMAGENO);
        var objImage = $('#srcImage');
        var objDiv = $('#divImage');
        $(objDiv).width($(objDiv).parent().width());
        $(objDiv).height($(objDiv).parent().height());
        var imageUrl = sIMAGENO == '' ? '' : rootURL + 'API/Common/Download.ashx'
            + '?attfileno=' + sIMAGENO
            + '&attfileseq=1'
            + '&data_gbn=E'
            + '&compcd=<%=gsCOMPCD%>';

            $(objImage).attr('src', imageUrl);
            testImage(imageUrl, objImage);
        }

        // 점검결과 KeyUpEvent
        function fn_OnMeasureKeyUp(s, e) {
            sINSPKINDCD = devGridInsp.GetRowKey(inspVisibleIndex).split('|')[2];
            if (sINSPKINDCD == 'AAG601') {
                fn_RemoveHangul(s, e);

                sMEASURE = s.GetText();
                if (sMEASURE != '')
                    devGridInsp.GetRowValues(inspVisibleIndex, 'F_MAX;F_MIN', fn_OnAutoJudge);
            }
        }

        // 점검결과 KeyPressEvent
        function fn_OnMeasureKeyPress(s, e) {
            sINSPKINDCD = devGridInsp.GetRowKey(inspVisibleIndex).split('|')[2];
            if (sINSPKINDCD == 'AAG601') {
                fn_ValidateOnlyFloat(s, e);
            }
        }

        // 검검결과 LostFocusEvent
        function fn_OnMeasureLostFocus(s, e) {
            sINSPKINDCD = devGridInsp.GetRowKey(inspVisibleIndex).split('|')[2];
            if (sINSPKINDCD == 'AAG601') {
                sMEASURE = s.GetText();
                if (sMEASURE != '')
                    devGridInsp.GetRowValues(inspVisibleIndex, 'F_MAX;F_MIN', fn_OnAutoJudge);
            }
        }

        // 결과값 입력 시
        function fn_OnAutoJudge(vals) {
            var sJudge = 'AAG702';
            var sMax = vals[0];
            var sMin = vals[1];
            if (sMax != '' && vals[1] != '') {
                if (parseFloat(sMEASURE) <= parseFloat(sMax) && parseFloat(sMEASURE) >= parseFloat(sMin))
                    sJudge = 'AAG701';
            } else if (sMax != '') {
                if (parseFloat(sMEASURE) <= parseFloat(sMax))
                    sJudge = 'AAG701';
            } else if (sMin != '') {
                if (parseFloat(sMEASURE) >= parseFloat(sMin))
                    sJudge = 'AAG701';
            }

            devGridInsp.GetEditor("F_JUDGE").SetValue(sJudge);
        }

        // 판정선택 변경 시
        function fn_OnJudgeSelectedIndexChanged(s, e) {
            var rowKeys = devGridInsp.GetRowKey(inspVisibleIndex).split('|');
            sINSPKINDCD = rowKeys[2];
            if (sINSPKINDCD != 'AAG601') {
                if (s.GetText() == '선택하세요')
                    devGridInsp.GetEditor("F_MEASURE").SetText('');
                else
                    devGridInsp.GetEditor("F_MEASURE").SetText(s.GetText());
            }

            if (s.GetText() == 'NG') {
                if (sOldJudge != 'AAG802') {
                    fn_SetTextValue('txtNUMBER', rowKeys[1]);
                    fn_SetTextValue('txtINSPIDX', rowKeys[0]);
                    fn_SetTextValue('txtINSPNM', rowKeys[4]);

                    fn_SetValue('ddlSTATUS', null);
                    fn_SetValue('srcF_NGTYPE', null);
                    fn_SetTextValue('txtNGREMK', '');
                    fn_SetValue('srcF_RESPTYPE', null);
                    fn_SetDate('txtRESPDT', new Date());
                    fn_SetTextValue('txtRESPUSER', '<%=gsUSERNM%>');
                    fn_SetTextValue('txtRESPREMK', '');


                    $("#ngContent").dialog("open");
                    event.preventDefault();
                }
            } else if (s.GetText() == 'OK') {
                var removeIdx = -1;
                ngArray.forEach(function (v, i, a) {
                    if (v.INSPIDX == rowKeys[0] && v.NUMBER == rowKeys[1])
                        removeIdx = i;
                });
                
                if (removeIdx >= 0) {
                    ngArray.splice(removeIdx, 1);
                }
            }
        }

        // 점검일 변경 시
        function fn_OnDateChanged(s, e) {
            if (!isSelectMach()) { alert('설비점검항목기준을 등록/삭제할 설비를 선택하세요'); return false; }
            fn_doSetGridEventAction('false');
            fn_devCallback_PerformCallback();
            devGridInsp.PerformCallback();
            viewVisibleIndex = -1;
            fn_OnChartDoCallback(devChart, chartWidth, chartHeight);
            fn_SetDevImage('');
        }

        // 진행상태 변경 시
        function fn_StatusSelectedIndexChanged(s, e) {
            if (s.GetValue() == 'AAG902') {
                $('.tdRESP').addClass('required');
            } else {
                $('.tdRESP').removeClass('required');
            }
        }

        // 이상발생 사유 저장
        function fn_DoSaveNgReason() {
            var sINSPIDX = fn_GetCastText('txtINSPIDX');
            var sNUMBER = fn_GetCastText('txtNUMBER');
            var sSTATUS = fn_GetCastSelectedItemValue('ddlSTATUS');
            var sNGTYPE = fn_GetCastValue('srcF_NGTYPE');
            var sNGREMK = fn_GetCastText('txtNGREMK');
            var sRESPTYPE = fn_GetCastValue('srcF_RESPTYPE');
            var sRESPDT = fn_GetCastText('txtRESPDT');
            var sRESPUSER = fn_GetCastText('txtRESPUSER');
            var sRESPREMK = fn_GetCastText('txtRESPREMK');

            if (sSTATUS == '' || sSTATUS == null) {
                alert('진행상태를 선택하세요!!');
                ddlSTATUS.Focus();
                return false;
            }

            if (sNGTYPE == '' || sNGTYPE == null) {
                alert('이상유형을 선택하세요!!');
                srcF_NGTYPE.Focus();
                return false;
            }

            if (sNGREMK == '' || sNGREMK == null) {
                alert('이상내역을 입력하세요!!');
                fn_Focus('txtNGREMK');
                return false;
            }

            if (sSTATUS == 'AAG902') {
                if (sRESPTYPE == '' || sRESPTYPE == null) {
                    alert('조치유형을 선택하세요!!');
                    srcF_RESPTYPE.Focus();
                    return false;
                }
                if (sRESPDT == '' || sRESPDT == null) {
                    alert('조치일을 입력하세요!!');
                    fn_Focus('txtRESPDT');
                    return false;
                }
                if (!isValidDate(sRESPDT)) {
                    alert('날짜형식이 올바르지 않습니다!!');
                    fn_Focus('txtRESPDT');
                    return false;
                }
                if (sRESPUSER == '' || sRESPUSER == null) {
                    alert('조치자를 입력하세요!!');
                    fn_Focus('txtRESPUSER');
                    return false;
                }
                if (sRESPREMK == '' || sRESPREMK == null) {
                    alert('조치내역을 입력하세요!!');
                    fn_Focus('txtRESPREMK');
                    return false;
                }
            }

            ngClasses = {
                INSPIDX: sINSPIDX,
                NUMBER: sNUMBER,
                STATUS: sSTATUS,
                NGTYPE: sNGTYPE,
                NGREMK: sNGREMK,
                RESPTYPE: sRESPTYPE,
                RESPDT: sRESPDT,
                RESPUSER: sRESPUSER,
                RESPREMK: sRESPREMK
            }

            ngArray.push(ngClasses);
            fn_SetTextValue('txtRESPVALUES', '');
            fn_SetTextValue('txtRESPVALUES', encodeURIComponent(JSON.stringify(ngArray)));
            $('#ngContent').dialog('close');
        }

        // 이상발생 사유 취소
        function fn_DoCancelNgReason() {
            if (confirm('이상발생 사유 미등록 시 점검결과가 초기화됩니다.\r미등록을 원하면 확인을 사유등록을 진행하려면 취소를 누르세요')) {
                devGridInsp.batchEditApi.SetCellValue(inspVisibleIndex, "F_MEASURE", sOldMeasure);
                devGridInsp.batchEditApi.SetCellValue(inspVisibleIndex, "F_JUDGE", sOldJudge);
                $('#ngContent').dialog('close');
            }
        }

        // 설비이미지 보기
        function fn_SetMachDevImage() {
            var sIMAGENO = fn_GetCastText('srcF_IMAGENO');
            var objImage = $('#srcMachImage');
            var imageUrl = sIMAGENO == '' ? '' : rootURL + 'API/Common/Download.ashx'
                + '?attfileno=' + sIMAGENO
                + '&attfileseq=1'
                + '&data_gbn=E'
                + '&compcd=<%=gsCOMPCD%>';
            $(objImage).attr('src', imageUrl);
            testImage(imageUrl, objImage);

            if ($(objImage).attr('src') != '')
                $("#machImage").dialog("open");
        }

        // 항목 이미지 및 차트 오픈
        function fn_OnCustomButtonClick(s, e) {
            viewVisibleIndex = e.visibleIndex;
            devGridInsp.GetRowValues(viewVisibleIndex, "F_IMAGESEQ", fn_SetInspImage);
            fn_OnChartDoCallback(devChart, chartWidth, chartHeight);
        }

        function fn_SetInspImage(val) {
            var sIMAGENO = val;
            var objImage = $('#srcImage');
            var objDiv = $('#divImage');
            $(objDiv).width($(objDiv).parent().width());
            $(objDiv).height($(objDiv).parent().height());
            var imageUrl = sIMAGENO == '' ? '' : rootURL + 'API/Common/Download.ashx'
                + '?attfileno=' + sIMAGENO
                + '&attfileseq=1'
                + '&data_gbn=E'
                + '&compcd=<%=gsCOMPCD%>';

            $(objImage).attr('src', imageUrl);
            testImage(imageUrl, objImage);
        }

        // 차트 이벤트
        function fn_OnChartResized(chartObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnChartDoCallback(chartObj, _width, _height);
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height) {
            var oParams = '';

            if (viewVisibleIndex >= 0) {
                var vals = devGridInsp.GetRowKey(viewVisibleIndex).split('|');
                oParams = chartWidth + '|' + chartHeight + '|' + vals[0] + '|' + encodeURIComponent(vals[4]);
            } else {
                oParams = _width + '|' + _height + '||';
            }

            chartObj.PerformCallback(oParams);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

        // CallbackPanel Event Handler 시작
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                devCallback.PerformCallback();
            }
        }

        // 콜백 성공시 처리 : 성공시 PKEY에 JSON 구조로 키값을 받는다.
        function fn_devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            var isOK = typeof (result.ISOK) != 'undefined' ? result.ISOK : null;
            var msg = typeof (result.MSG) != 'undefined' ? result.MSG : null;
            if (isOK) {
                var pkey = typeof (result.PKEY) != 'undefined' ? result.PKEY : null;
                var data = {};
                if (typeof (result.PAGEDATA) != 'undefined') {
                    data = result.PAGEDATA;
                }

                fn_OnInputConfirmData(data);
            } else {
                fn_OnInputConfirmData(null);
                if (msg != 'NO DATA')
                    alert(msg);
            }
        }

        // 콜백 종료시 처리
        function fn_devCallback_EndCallback(s, e) {
            // 대기중인 작업이 있을경우, 콜백 실행
            if (devCallback_parameter) {
                devCallback.PerformCallback(JSON.stringify(devCallback_parameter));
                devCallback_parameter = null;
            }
        }
        // CallbackPanel Event Handler   끝

        // 입력폼 조회값입력
        function fn_OnInputConfirmData(pagedata) {
            if (pagedata != null) {
                fn_SetTextValue('srcF_CONFIRM', GetJsonValueByKey(pagedata, 'F_CONFIRM', ''));
            } else {
                fn_SetTextValue('srcF_CONFIRM', '');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <table class="layerTable">
        <colgroup>
            <col style="width:480px;" />
            <col />
        </colgroup>
        <tr>
            <td>
                <div class="container">
                    <div class="search">
                        <table class="contentTable">
                            <colgroup>
                                <col style="width:60px;" />
                                <col style="width:180px;" />
                                <col style="width:60px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td class="tdLabel">반</td>
                                <td class="tdInput">
                                    <ucCTF:BanMulti runat="server" id="schF_BANCD" ClientInstanceName="schF_BANCD" targetCtrls="schF_LINECD" />
                                </td>
                                <td class="tdLabel">라인</td>
                                <td class="tdInput">
                                    <ucCTF:LineMulti runat="server" ID="schF_LINECD" ClientInstanceName="schF_LINECD" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel">설비명</td>
                                <td class="tdInput" colspan="3">
                                    <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" Width="100%" MaxLength="50" />
                                </td>
                            </tr>
                        </table>
                        <div style="width:100%;text-align:left;font-weight:bold;color:red;">설비일상점검을 등록하려면 아래에서 해당 설비를 더블클릭하세요</div>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content">
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_MACHIDX;F_MACHCD;F_MACHNM;F_BANCD;F_BANNM;F_LINECD;F_LINENM;F_IMAGENO" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                                RowDblClick="fn_OnRowDblClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="80px" />
                                <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="100%">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_MACHIDX" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_BANCD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_LINECD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_IMAGENO" Visible="false" />
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                    <div class="paging"></div>
                </div>
            </td>
            <td>
                <div class="container_r">
                    <div class="search_r">
                        <dx:ASPxTextBox ID="srcF_MACHIDX" ClientInstanceName="srcF_MACHIDX" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_IMAGENO" ClientInstanceName="srcF_IMAGENO" runat="server" ClientVisible="false" />
                        <table class="contentTable">
                            <colgroup>
                                <col style="width:70px;" />
                                <col style="width:130px;" />
                                <col style="width:70px;" />
                                <col style="width:130px;" />
                                <col style="width:70px;" />
                                <col style="width:130px;" />
                                <col style="width:70px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td class="tdLabel">반</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_BANCD" ClientInstanceName="srcF_BANCD" runat="server" ClientVisible="false" />
                                    <dx:ASPxTextBox ID="srcF_BANNM" ClientInstanceName="srcF_BANNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel">라인</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_LINECD" ClientInstanceName="srcF_LINECD" runat="server" ClientVisible="false" />
                                    <dx:ASPxTextBox ID="srcF_LINENM" ClientInstanceName="srcF_LINENM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel">설비코드</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_MACHCD" ClientInstanceName="srcF_MACHCD" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel">설비명</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_MACHNM" ClientInstanceName="srcF_MACHNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="divPadding"></div>
                        <table style="width:100%">
                            <colgroup>
                                <col />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td>
                                    <table id="inspSearchTable" border="1" class="contentTable">
                                        <colgroup>
                                            <col style="width:70px;" />
                                            <col style="width:130px;" />
                                            <col style="width:70px;" />
                                            <col style="width:100px;" />
                                            <col />
                                        </colgroup>
                                            <tr>
                                                <td class="tdLabel">점검일</td>
                                                <td class="tdInput">
                                                    <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" TodayFromDate="true" SingleDate="true" Changed="fn_OnDateChanged" />
                                                </td>
                                                <td class="tdInput">
                                                    <button class="btn btn-sm btn-success" onclick="fn_OnDateChanged(); return false;">
                                                        <i class="fa fa-search"></i>
                                                        <span class="text">조회</span>
                                                    </button>
                                                </td>
                                                <td class="tdLabel">관리자확인</td>
                                                <td class="tdInput">
                                                    <dx:ASPxTextBox ID="srcF_CONFIRM" ClientInstanceName="srcF_CONFIRM" runat="server" Width="100%">
                                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width: auto;margin-right: 0px;margin-left: auto;">
                                        <tr>
                                            <td style="padding-left:5px;">
                                                <button class="btn btn-sm btn-primary" onclick="fn_SetMachDevImage(); return false;">
                                                    <i class="fa fa-picture-o"></i>
                                                    <span class="text">설비사진</span>
                                                </button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content_r">
                        <dx:ASPxGridView ID="devGridInsp" ClientInstanceName="devGridInsp" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_INSPIDX;F_NUMBER;F_INSPKINDCD;F_INSPUSER;F_INSPNM;F_MEASIDX;F_MEASURE;F_JUDGE" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGridInsp_CustomCallback" OnCellEditorInitialize="devGridInsp_CellEditorInitialize"
                            OnCustomColumnDisplayText="devGridInsp_CustomColumnDisplayText" OnBatchUpdate="devGridInsp_BatchUpdate">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="Click" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridInspEndCallback" CallbackError="fn_OnCallbackError"
                                BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" CustomButtonClick="fn_OnCustomButtonClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridInsp" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewCommandColumn Width="60px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton Text="보기" />
                                    </CustomButtons>
                                    <HeaderTemplate>
                                        <i class="fa fa-bar-chart-o" title="트랜드"></i>
                                        <i class="fa fa-picture-o" title="항목사진"></i>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPNO" Caption="점검<br />부위" Width="50px">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="점검항목" Width="140px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPREMARK" Caption="점검내용" Width="180px">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPWAY" Caption="점검방법" Width="240px">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_VIEWSTAND" Caption="표시규격" Width="200px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CYCLENM" Caption="점검<br />주기" Width="50px">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPKINDNM" Caption="판정구분" Width="70px">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="F_MEASURE" Caption="점검결과" Width="70px">
                                    <PropertiesTextEdit NullText="-, ., 숫자만가능" ConvertEmptyStringToNull="True">
                                        <ClientSideEvents KeyUp="fn_OnMeasureKeyUp" KeyPress="fn_OnMeasureKeyPress" LostFocus="fn_OnMeasureLostFocus" />
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="F_JUDGE" Caption="판정" Width="50px">
                                    <PropertiesComboBox ConvertEmptyStringToNull="True" NullText="선택">
                                        <ClientSideEvents SelectedIndexChanged="fn_OnJudgeSelectedIndexChanged" />
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>

                                <dx:GridViewDataColumn FieldName="F_INSPIDX" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_INSPKINDCD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_CYCLECD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_NUMBER" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_CHASU" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_MAX" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_MIN" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_MEASIDX" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_INSPUSER" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_IMAGESEQ" Visible="false" />
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                    <div class="paging_r">
                        <div class="divPadding"></div>
                        <table border="0" style="width:100%;height:180px;">
                            <colgroup>
                                <col style="width:300px;" />
                                <col style="width:10px;" />
                                <col style="height:180px;" />
                            </colgroup>
                            <tr>
                                <td style="border:1px solid #808080;">
                                    <div id="divImage" style="position:relative;"><img id="srcImage" src="" class="centerImage resizeImageRatio" /></div>
                                </td>
                                <td></td>
                                <td style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart" ClientInstanceName="devChart" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="10px"
                                        OnCustomCallback="devChart_CustomCallback" CrosshairEnabled="True" Width="300px">
                                        <ClientSideEvents EndCallback="function(s, e) { chartResized = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                        </table>
                        <div class="divPadding"></div>
                        <div class="divPadding"></div>
                        <div class="divPadding"></div>
                        <div class="divPadding"></div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <dx:ASPxTextBox ID="txtRESPVALUES" ClientInstanceName="txtRESPVALUES" runat="server" ClientVisible="false" />
    <div id="ngContent">
        <table id="inputTable" class="contentTable">
            <colgroup>
                <col style="width:80px;" />
                <col style="width:120px;" />
                <col style="width:80px;" />
                <col style="width:120px;" />
                <col style="width:80px;" />
                <col style="width:120px;" />
            </colgroup>
            <tr>
                <td class="tdLabel">점검항목</td>
                <td class="tdInput">
                    <dx:ASPxTextBox ID="txtINSPIDX" ClientInstanceName="txtINSPIDX" runat="server" ClientVisible="false" />
                    <dx:ASPxTextBox ID="txtINSPNM" ClientInstanceName="txtINSPNM" runat="server" Width="100%">
                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                    </dx:ASPxTextBox>
                </td>
                <td class="tdLabel">차수</td>
                <td class="tdInput">
                    <dx:ASPxTextBox ID="txtNUMBER" ClientInstanceName="txtNUMBER" runat="server" Width="100%">
                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                    </dx:ASPxTextBox>
                </td>
                <td class="tdLabel required">진행상태</td>
                <td class="tdInput" colspan="3">
                    <dx:ASPxComboBox ID="ddlSTATUS" ClientInstanceName="ddlSTATUS" runat="server" Width="100%"
                        IncrementalFilteringMode="None" CssClass="NoXButton">
                        <ClientSideEvents SelectedIndexChanged="fn_StatusSelectedIndexChanged" />
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel required">이상유형</td>
                <td class="tdInput tdContentR">
                    <ucCTF:SYCOD01 runat="server" id="srcF_NGTYPE" ClientInstanceName="srcF_NGTYPE" F_CODEGROUP="41" nullText="선택하세요" />
                </td>
                <td class="tdInput tdContentR"></td>
                <td class="tdInput tdContentR"></td>
                <td class="tdInput tdContentR"></td>
                <td class="tdInput"></td>
            </tr>
            <tr>
                <td class="tdLabel required">이상내역</td>
                <td class="tdInput" colspan="5">
                    <dx:ASPxMemo ID="txtNGREMK" ClientInstanceName="txtNGREMK" runat="server" Width="100%" Height="50px" />
                </td>
            </tr>
            <tr>
                <td class="tdLabel tdRESP">조치유형</td>
                <td class="tdInput">
                    <ucCTF:SYCOD01 runat="server" id="srcF_RESPTYPE" ClientInstanceName="srcF_RESPTYPE" F_CODEGROUP="43" nullText="선택하세요" />
                </td>
                <td class="tdLabel tdRESP">조치일</td>
                <td class="tdInput">
                    <dx:ASPxDateEdit ID="txtRESPDT" ClientInstanceName="txtRESPDT" runat="server" Width="100%" Theme="MetropolisBlue"
                        DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd" class="form-control input-sm">
                        <CalendarProperties ShowClearButton="False" ShowTodayButton="False" ShowWeekNumbers="False">
                            <ControlStyle Wrap="True" />
                            <MonthGridPaddings Padding="0px" />
                            <DayStyle Font-Size="11px">
                                <Paddings Padding="1px" />
                            </DayStyle>
                            <FastNavProperties Enabled="false" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td class="tdLabel tdRESP">조치자</td>
                <td class="tdInput" colspan="5">
                    <dx:ASPxTextBox ID="txtRESPUSER" ClientInstanceName="txtRESPUSER" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="tdLabel tdRESP">조치내역</td>
                <td class="tdInput" colspan="5">
                    <dx:ASPxMemo ID="txtRESPREMK" ClientInstanceName="txtRESPREMK" runat="server" Width="100%" Height="100px" />
                </td>
            </tr>
        </table>
        <table style="margin-left: auto; margin-right: auto;">
            <tr>
                <td class="text-center" style="padding-top:10px;">
                    <button class="btn btn-sm btn-primary" onclick="fn_DoSaveNgReason(); return false;">
                        <i class="fa fa-picture-o"></i>
                        <span class="text">확인</span>
                    </button>
                </td>
                <td style="width:10px;"></td>
                <td class="text-center" style="padding-top:10px;">
                    <button class="btn btn-sm btn-warning" onclick="fn_DoCancelNgReason(); return false;">
                    <i class="fa fa-undo"></i>
                    <span class="text">취소</span>
                </button>
                </td>
            </tr>
        </table>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
    <script type="text/javascript">
        // 이상발생 레이아웃 Dialog
        $("#ngContent").dialog({
            autoOpen: false,
            width: 640,
            height: 390,
            modal: true,
            closeOnEscape: false,
            draggable: false,
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            },
            title: "설비일상점검 이상발생 원인/조치등록",
            classes: {
                "ui-dialog": "highlight",
                "ui-dialog-titlebar": "highlight"
            }
        });
    </script>
    <div id="machImage">
        <div id="divMachImage" style="position:relative;"><img id="srcMachImage" src="" class="centerImage resizeImageRatio" /></div>
    </div>
    <script type="text/javascript">
        // 이상발생 레이아웃 Dialog
        $("#machImage").dialog({
            autoOpen: false,
            modal: true,
            closeOnEscape: false,
            draggable: false,
            title: "설비사진",
            classes: {
                "ui-dialog": "highlight",
                "ui-dialog-titlebar": "highlight"
            },
            open: function (event, ui) {
                $('#machImage').css('overflow', 'hidden'); //this line does the actual hiding
            }
        });
    </script>
</asp:Content>