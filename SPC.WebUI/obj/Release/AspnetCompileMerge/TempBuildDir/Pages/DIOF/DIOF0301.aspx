<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0301.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0301" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            // 입력상자 Enter Key Event
            $('#inputTable input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_OnSaveClick();
                    return false;
                }
            });

            fn_OnChangeMode('R');
            fn_OnSearchClick();
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
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            fn_OnInputSelectClear();
            fn_OnInputClear();
            devGrid.PerformCallback();
            devGridInsp.PerformCallback();
        }

        // 점검항목조회
        function fn_OnSubSearchClick(bAuto) {
            fn_doSetGridEventAction('true');
            fn_OnChangeMode('C');
            devGridInsp.PerformCallback();
            if (!bAuto) fn_OnInputClear();
        }

        // 입력
        function fn_OnNewClick() {
            fn_OnInputClear();
            fn_OnChangeMode('C');
        }

        // 수정
        function fn_OnModifyClick() {
            fn_OnChangeMode('U');
        }

        // 저장
        function fn_OnSaveClick() {
            if (!isSelectMach()) { alert('설비점검항목기준을 등록/삭제할 설비를 선택하세요'); return false; }
            if (fn_GetCastValue('hidPageMode') != 'C' && fn_GetCastValue('hidPageMode') != 'U') { alert('입력, 수정모드에서만 저장할 수 있습니다'); return false; }
            if (!fn_OnValidate()) return false;
            if (fn_GetCastValue('hidPageMode') == 'C') { fn_DoSave(); }
        }

        // 저장실행
        function fn_DoSave(val) {
            fn_SetTextValue('srcF_MODREASON', val);

            var data = {
                'PAGEMODE': fn_GetCastValue('hidPageMode'),
                'ACTION': 'SAVE'
            };

            fn_devCallback_PerformCallback(data);
        }

        // 취소
        function fn_OnCancelClick() {
            fn_OnInputClear();
            fn_OnChangeMode('C');
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (fn_GetCastValue('hidPageMode') != 'U') { alert('수정모드에서만 삭제할 수 있습니다\r하단 점검항목목록에서 삭제할 정보(double-click)를 선택하세요'); return false; }
            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 정보는 복원할 수 없습니다.')) { return false; }
            else {
                fn_OnChangeMode('D');
                fn_OnPopupHistoryReason(fn_GetCastValue('hidPageMode'));
            }
        }

        // 삭제실행
        function fn_DoDelete(val) {
            fn_SetTextValue('srcF_MODREASON', val);
            var data = {
                'PAGEMODE': fn_GetCastValue('hidPageMode'),
                'ACTION': 'DELETE'
            };

            fn_devCallback_PerformCallback(data);
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

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            if (fn_GetCastValue('srcF_INSPNM') == '') {
                alert('항목명을 입력하세요!!');
                fn_Focus('srcF_INSPNM');
                return false;
            }
            if (fn_GetCastValue('srcF_INSPKINDCD') == '') {
                alert('판정방식을 선택하세요!!');
                srcF_INSPKINDCD.Focus();
                return false;
            }
            if (fn_GetCastValue('srcF_INSPORDER') == '') {
                alert('점검순서를 입력하세요!!');
                fn_Focus('srcF_INSPORDER');
                return false;
            }
            if (fn_GetCastValue('srcF_CYCLECD') == '') {
                alert('점검주기를 선택하세요!!');
                srcF_CYCLECD.Focus();
                return false;
            }
            if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601' && (fn_GetCastValue('srcF_STAND') == '' || fn_GetCastValue('srcF_STAND') == '-, ., 숫자만가능')) {
                alert('점검규격을 입력하세요!!');
                fn_Focus('srcF_STAND');
                return false;
            } else if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601' && fn_GetCastValue('srcF_STAND') != '' && !fn_isNumeric('srcF_STAND')) {
                alert('점검규격은 숫자형만 입력하세요!!');
                fn_Focus('srcF_STAND');
                return false;
            }
            if (fn_GetCastValue('srcF_VIEWSTAND') == '') {
                alert('표시규격을 입력하세요!!');
                fn_Focus('srcF_VIEWSTAND');
                return false;
            }
            if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601' && (fn_GetCastValue('srcF_MAX') == '' || fn_GetCastValue('srcF_MIN') == '')) {
                alert('판정 방식이 수치비교인 경우\r점검상한 또는 검검하한을 입력하세요!!');
                fn_Focus('srcF_MAX');
                return false;
            }
            if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601' && fn_GetCastValue('srcF_MAX') != '' && !fn_isNumeric('srcF_MAX')) {
                alert('점검상한은 숫자형만 입력하세요!!');
                fn_Focus('srcF_MAX');
                return false;
            }if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601' && fn_GetCastValue('srcF_MIN') != '' && !fn_isNumeric('srcF_MIN')) {
                alert('점검하한은 숫자형만 입력하세요!!');
                fn_Focus('srcF_MIN');
                return false;
            }
            if (fn_GetCastValue('srcF_USESTDT') == '') {
                alert('사용시작일을 입력하세요!!');
                fn_Focus('srcF_USESTDT');
                return false;
            }
            if (!isValidDate(fn_GetCastValue('srcF_USESTDT'))) {
                alert('날짜형식이 올바르지 않습니다!!');
                fn_Focus('srcF_USESTDT');
                return false;
            }
            if (fn_GetCastValue('srcF_USESTDT') == '') {
                alert('점검항목 사용시작일을 입력하세요!!');
                fn_Focus('srcF_USESTDT');
                return false;
            }
            if (fn_GetCastValue('srcF_INSPREMARK') == '') {
                alert('점검내용을 입력하세요!!');
                fn_Focus('srcF_INSPREMARK');
                return false;
            }
            if (fn_GetCastValue('srcF_INSPWAY') == '') {
                alert('점검방법을 입력하세요!!');
                fn_Focus('srcF_INSPWAY');
                return false;
            }
            if ((fn_GetCastValue('hidPageMode') == 'U' || fn_GetCastValue('hidPageMode') == 'D') && fn_GetCastValue('srcF_MODREASON') == '') {
                fn_OnPopupHistoryReason(fn_GetCastValue('hidPageMode'));
                return false;
            }

            return true;
        }

        // 입력폼 모드변경
        function fn_OnChangeMode(t) {
            fn_SetTextValue('hidPageMode', t);

            if (t == 'R') {
                $('#inputTable').find('*').prop('disabled', true);
                $('#tdMode').html('조회모드(새로운 정보를 등록 또는 기존정보를 수정하려면 좌측의 설비를 선택하세요)');
            } else if (t == 'C') {
                $('#inputTable').find('*').prop('disabled', false);
                $('#tdMode').html('신규입력모드(입력한 정보를 저장하려면[저장] 버튼을, 입력을 취소하려면 [취소] 버튼을 누르세요)');
            } else if (t == 'U') {
                $('#inputTable').find('*').prop('disabled', false);
                $('#tdMode').html('수정입력모드(입력한 정보를 저장하려면[저장] 버튼을, 입력을 취소하려면 [취소] 버튼을 누르세요)');
            } else if (t == 'D') {
                $('#inputTable').find('*').prop('disabled', false);
                $('#tdMode').html('삭제모드(선택한 정보를 삭제합니다)');
            }
        }

        // 입력폼 초기화
        function fn_OnInputClear() {
            fn_SetTextValue('srcF_INSPIDX', '');
            fn_SetTextValue('srcF_INSPNM', '');
            fn_SetValue('srcF_INSPKINDCD', null);
            fn_SetTextValue('srcF_INSPORDER', parseInt(devGridInsp.GetVisibleRowsOnPage(), 10) + 1);
            fn_SetValue('srcF_CYCLECD', null);
            fn_SetTextValue('srcF_INSPNO', '');
            fn_SetTextValue('srcF_CHASU', '1');
            fn_SetTextValue('srcF_STAND', '');
            fn_SetTextValue('srcF_VIEWSTAND', '');
            fn_SetTextValue('srcF_MAX', '');
            fn_SetTextValue('srcF_MIN', '');
            fn_SetValue('srcF_UNIT', null);
            fn_SetTextValue('srcF_USESTDT', formatDateString(new Date(), ''));
            fn_SetTextValue('srcF_INSPREMARK', '');
            fn_SetTextValue('srcF_INSPWAY', '');
            fn_SetTextValue('srcF_MODREASON', '');
            fn_SetChecked('srcF_USEYN', false);
            fn_SetDevImage(''); // 이미지 초기화

            $('#tdSTAND').removeClass('required');
            $('#tdMAX').removeClass('required');
            $('#tdMIN').removeClass('required');

            fn_RemoveNullText(srcF_STAND, '-, ., 숫자만가능');

            fn_Focus('srcF_INSPNM');
        }

        // 입력폼 조회값입력
        function fn_OnInputData(pagedata) {
            var inspIDX = GetJsonValueByKey(pagedata, 'F_INSPIDX', '');

            if (inspIDX == '') {
                alert('조회된 데이터가 없거나 일시적 장애입니다');
                return false;
            }

            var JsonValue = '';

            fn_SetTextValue('srcF_INSPIDX', inspIDX);
            fn_SetTextValue('srcF_INSPCD', GetJsonValueByKey(pagedata, 'F_INSPCD', ''));
            fn_SetTextValue('srcF_INSPNM', GetJsonValueByKey(pagedata, 'F_INSPNM', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_INSPKINDCD', '');
            JsonValue = JsonValue == '' ? null : JsonValue;
            fn_SetValue('srcF_INSPKINDCD', JsonValue);
            fn_SetTextValue('srcF_INSPORDER', GetJsonValueByKey(pagedata, 'F_INSPORDER', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_CYCLECD', '');
            JsonValue = JsonValue == '' ? null : JsonValue;
            fn_SetValue('srcF_CYCLECD', JsonValue);
            fn_SetTextValue('srcF_INSPNO', GetJsonValueByKey(pagedata, 'F_INSPNO', ''));
            fn_SetTextValue('srcF_CHASU', GetJsonValueByKey(pagedata, 'F_CHASU', ''));
            fn_SetTextValue('srcF_STAND', GetJsonValueByKey(pagedata, 'F_STAND', ''));
            fn_SetTextValue('srcF_VIEWSTAND', GetJsonValueByKey(pagedata, 'F_VIEWSTAND', ''));
            fn_SetTextValue('srcF_MAX', GetJsonValueByKey(pagedata, 'F_MAX', ''));
            fn_SetTextValue('srcF_MIN', GetJsonValueByKey(pagedata, 'F_MIN', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_UNIT', '');
            JsonValue = JsonValue == '' ? null : JsonValue;
            fn_SetValue('srcF_UNIT', JsonValue);
            fn_SetTextValue('srcF_USESTDT', GetJsonValueByKey(pagedata, 'F_USESTDT', ''));
            fn_SetTextValue('srcF_INSPREMARK', GetJsonValueByKey(pagedata, 'F_INSPREMARK', ''));
            fn_SetTextValue('srcF_INSPWAY', GetJsonValueByKey(pagedata, 'F_INSPWAY', ''));
            fn_SetTextValue('srcF_MODREASON', ''); //반드시 입력받기 위해 팝업창에서 등록
            fn_SetTextValue('srcF_USESTDT', GetJsonValueByKey(pagedata, 'F_USESTDT', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_USEYN', '');
            JsonValue = JsonValue == '' ? null : JsonValue;
            fn_SetChecked('srcF_USEYN', JsonValue);
            fn_SetDevImage(GetJsonValueByKey(pagedata, 'F_IMAGESEQ', ''));   // 이미지


            //수치비교인 경우 필수입력값 세팅
            if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601') {
                $('#tdSTAND').addClass('required');
                $('#tdMAX').addClass('required');
                $('#tdMIN').addClass('required');

                fn_SetNullText(srcF_STAND, '-, ., 숫자만가능');
            } else {
                $('#tdSTAND').removeClass('required');
                $('#tdMAX').removeClass('required');
                $('#tdMIN').removeClass('required');

                fn_RemoveNullText(srcF_STAND);
            }
        }

        // Grid Row DblClick
        function fn_OnRowDblClick(s, e) {
            fn_doSetGridEventAction('false');
            fn_OnInputSelectData(devGrid.GetRowKey(e.visibleIndex).split('|'));
            fn_OnChangeMode('C');
            devGridInsp.PerformCallback();
            fn_OnInputClear();
        }

        // 점검항목 Grid Row DblClick
        function fn_OnInspRowDblClick(s, e) {
            fn_OnChangeMode('U');
            var data = {
                'F_INSPIDX': devGridInsp.GetRowKey(e.visibleIndex),
                'ACTION': 'GET'
            };
            fn_devCallback_PerformCallback(data);
        }

        // 점검순서세팅
        function fn_OnSetOrderNo(s, e) {
            fn_SetTextValue('srcF_INSPORDER', parseInt(s.GetVisibleRowsOnPage(), 10) + 1);
        }

        // 입력폼 초기화
        function fn_OnInputSelectClear() {
            fn_SetTextValue('srcF_MACHIDX', '');
            fn_SetTextValue('srcF_MACHCD', '');
            fn_SetTextValue('srcF_MACHNM', '');
            fn_SetTextValue('srcF_BANNM', '');
            fn_SetTextValue('srcF_LINENM', '');
        }

        // 입력폼 조회값입력
        function fn_OnInputSelectData(rowKey) {
            fn_SetTextValue('srcF_MACHIDX', rowKey[0]);
            fn_SetTextValue('srcF_MACHCD', rowKey[1]);
            fn_SetTextValue('srcF_MACHNM', rowKey[2]);
            fn_SetTextValue('srcF_BANNM', rowKey[3]);
            fn_SetTextValue('srcF_LINENM', rowKey[4]);
        }

        // 설비선택여부
        function isSelectMach() {
            return fn_GetCastValue('srcF_MACHIDX') != '' && fn_GetCastValue('srcF_MACHCD') != '' && fn_GetCastValue('srcF_MACHNM') != '';
        }

        // CallbackPanel Event Handler 시작
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                devCallback.PerformCallback(JSON.stringify(parameter));
            }
        }

        // 콜백 성공시 처리 : 성공시 PKEY에 JSON 구조로 키값을 받는다.
        function fn_devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            var parameter = JSON.parse(e.parameter);
            var isOK = typeof (result.ISOK) != 'undefined' ? result.ISOK : null;
            var msg = typeof (result.MSG) != 'undefined' ? result.MSG : null;
            if (isOK) {
                var pkey = typeof (result.PKEY) != 'undefined' ? result.PKEY : null;
                var data = {};
                if (typeof (result.PAGEDATA) != 'undefined') {
                    data = result.PAGEDATA;
                } else {
                    for (var key in pkey) { data[key] = pkey[key]; }
                }
                var action = typeof (parameter.ACTION) != 'undefined' ? parameter.ACTION : null;

                switch (action) {
                    case "SAVE":
                        alert(msg);
                        data.ACTION = 'GET';
                        fn_devCallback_PerformCallback(data);
                        fn_OnSubSearchClick('true');
                        break;
                    case "GET":
                        fn_OnInputData(data);
                        fn_OnModifyClick();
                        break;
                    case "DELETE":
                        alert(msg);
                        fn_OnSubSearchClick();
                        break;
                }
            } else {
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

        // 콜백 오류시 처리
        function fn_devCallback_CallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }
        // CallbackPanel Event Handler   끝

        // 판정방식 변경 시
        function fn_OnINSPKINDCDValueChanged() {
            if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601') {
                $('#tdSTAND').addClass('required');
                $('#tdMAX').addClass('required');
                $('#tdMIN').addClass('required');

                fn_SetNullText(srcF_STAND, '-, ., 숫자만가능');
            } else {
                $('#tdSTAND').removeClass('required');
                $('#tdMAX').removeClass('required');
                $('#tdMIN').removeClass('required');

                fn_RemoveNullText(srcF_STAND, '-, ., 숫자만가능');
            }
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

        // 점검항목사진 업로드 완료 후
        function fn_UploadedComplete(val) {
            fn_SetTextValue('txtIMAGESEQ', val);

            if (val != '') {
                fn_SetDevImage(val);
            }
        }

        // 검사규격 KeyUpEvent
        function fn_OnStandKeyUp(s, e) {
            if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601') {
                fn_RemoveHangul(s, e);
            }
        }

        // 검사규격 KeyPressEvent
        function fn_OnStandKeyPress(s, e) {
            if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601') {
                fn_ValidateOnlyFloat(s, e);
            }
        }

        // 검사규격 GotFocus
        function fn_OnStandGotFocus(s, e) {
            fn_RemoveNullText(s, '-, ., 숫자만가능');
        }

        // 검사규격 LostFocus
        function fn_OnStandLostFocus(s, e) {
            if (fn_GetCastValue('srcF_INSPKINDCD') == 'AAG601') {
                fn_SetNullText(s, '-, ., 숫자만가능');
            } else {
                fn_RemoveNullText(s);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <table class="layerTable">
        <colgroup>
            <col style="width:600px;" />
            <col />
        </colgroup>
        <tr>
            <td>
                <div class="container">
                    <div class="search">
                        <table class="contentTable">
                            <colgroup>
                                <col style="width:60px;" />
                                <col style="width:130px;" />
                                <col style="width:60px;" />
                                <col style="width:130px;" />
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
                                <td class="tdLabel">설비명</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" Width="100%" MaxLength="50" />
                                </td>
                            </tr>
                        </table>
                        <div style="width:100%;text-align:left;font-weight:bold;color:red;">점검항목 기준을 등록/삭제하려면 아래에서 해당 설비를 더블클릭하세요</div>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content">
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_MACHIDX;F_MACHCD;F_MACHNM;F_BANNM;F_LINENM" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
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
                                <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="120px" />
                                <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="120px" />
                                <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="80px" />
                                <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="100%">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_USEYN" Caption="상태" Width="50px" />

                                <dx:GridViewDataColumn FieldName="F_MACHIDX" Visible="false" />
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
                                    <dx:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="srcF_BANNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel">라인</td>
                                <td class="tdInput">
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
                        <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" ClientVisible="false" Text="R" /><!--C:신규,R:조회,U:수정-->
                        <dx:ASPxTextBox ID="srcF_INSPIDX" ClientInstanceName="srcF_INSPIDX" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_INSPCD" ClientInstanceName="srcF_INSPCD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_MODREASON" ClientInstanceName="srcF_MODREASON" runat="server" ClientVisible="false" />
                        <table id="inputTable" class="contentTable">
                            <colgroup>
                                <col style="width:11%" />
                                <col style="width:26%" />
                                <col style="width:11%" />
                                <col style="width:26%" />
                                <col style="width:26%" />
                            </colgroup>
                            <tr>
                                <td class="tdLabel">현재상태</td>
                                <td id="tdMode" class="tdInput" colspan="5" style="color:black;font:bold;">조회모드</td>
                            </tr>
                            <tr>
                                <td class="tdLabel required">항목명</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_INSPNM" ClientInstanceName="srcF_INSPNM" runat="server" Width="100%" MaxLength="100" />
                                </td>
                                <td class="tdLabel required">판정방식</td>
                                <td class="tdInput">
                                    <ucCTF:CommonCodeDDLMulti ID="srcF_INSPKINDCD" ClientInstanceName="srcF_INSPKINDCD" runat="server" targetParams="AA|AAG6" targetFuncs="fn_OnINSPKINDCDValueChanged" />
                                </td>
                                <td class="tdLabel">점검항목이미지</td>
                            </tr>
                            <tr>
                                <td class="tdLabel required">점검순서</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_INSPORDER" ClientInstanceName="srcF_INSPORDER" runat="server" Width="100%" NullText="숫자만가능">
                                        <ClientSideEvents KeyPress="fn_ValidateOnlyNumberAbs" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel required">점검주기</td>
                                <td class="tdInput">
                                    <ucCTF:CommonCodeDDLMulti ID="srcF_CYCLECD" ClientInstanceName="srcF_CYCLECD" runat="server" targetParams="AA|AAG4" />
                                </td>
                                <td class="tdInput" rowspan="7" style="background-color:#e0e0e0;">
                                    <div id="divImage" style="position:relative;"><img id="srcImage" src="" class="centerImage resizeImageRatio" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel">점검부위번호</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_INSPNO" ClientInstanceName="srcF_INSPNO" runat="server" Width="100%" MaxLength="4" />
                                </td>
                                <td id="tdCHASU" class="tdLabel">일점검차수</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_CHASU" ClientInstanceName="srcF_CHASU" runat="server" Width="100%" NullText="숫자만가능">
                                        <ClientSideEvents KeyPress="fn_ValidateOnlyNumberAbs" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdSTAND" class="tdLabel">점검규격</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_STAND" ClientInstanceName="srcF_STAND" runat="server" Width="100%" MaxLength="100">
                                        <ClientSideEvents KeyUp="fn_OnStandKeyUp" KeyPress="fn_OnStandKeyPress" GotFocus="fn_OnStandGotFocus" LostFocus="fn_OnStandLostFocus" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel required">표시규격</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_VIEWSTAND" ClientInstanceName="srcF_VIEWSTAND" runat="server" Width="100%" MaxLength="100" />
                                </td>
                            </tr>
                            <tr>
                                <td id="tdMAX" class="tdLabel">점검상한</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_MAX" ClientInstanceName="srcF_MAX" runat="server" Width="100%" NullText="-, ., 숫자만가능">
                                        <ClientSideEvents KeyUp="fn_RemoveHangul" KeyPress="fn_ValidateOnlyFloat" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td id="tdMIN" class="tdLabel">점검하한</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_MIN" ClientInstanceName="srcF_MIN" runat="server" Width="100%" NullText="-, ., 숫자만가능">
                                        <ClientSideEvents KeyUp="fn_RemoveHangul" KeyPress="fn_ValidateOnlyFloat" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel">단위</td>
                                <td class="tdInput">
                                    <ucCTF:CommonCodeDDLMulti ID="srcF_UNIT" ClientInstanceName="srcF_UNIT" runat="server" targetParams="AA|AAC1" />
                                </td>
                                <td class="tdLabel required">사용시작일</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_USESTDT" ClientInstanceName="srcF_USESTDT" runat="server" Width="100%" MaxLength="10" MaskSettings-Mask="9999-99-99" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel required">점검내용</td>
                                <td class="tdInput" colspan="3">
                                    <dx:ASPxTextBox ID="srcF_INSPREMARK" ClientInstanceName="srcF_INSPREMARK" runat="server" Width="100%" MaxLength="400" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel required">점검방법</td>
                                <td class="tdInput" colspan="3">
                                    <dx:ASPxTextBox ID="srcF_INSPWAY" ClientInstanceName="srcF_INSPWAY" runat="server" Width="100%" MaxLength="400" />
                                </td>
                            </tr>
                            <tr>
                                <td id="tdMODREASON" class="tdLabel">점검제외</td>
                                <td class="tdInput" colspan="3">
                                    <dx:ASPxCheckBox ID="srcF_USEYN" ClientInstanceName="srcF_USEYN" runat="server" Text="일상점검 시 해당항목은 점검에서 제외" />
                                </td>
                                <td class="tdInput" style="text-align:right;">
                                    <button class="btn btn-sm btn-success" onclick="fn_AttachFileOpen('점검항목사진등록', 'E', 'F'); return false;">
                                        <i class="fa fa-upload"></i>
                                        <span class="text">점검항목사진등록</span>
                                    </button>
                                </td>
                            </tr>
                        </table>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content_r">
                        <dx:ASPxGridView ID="devGridInsp" ClientInstanceName="devGridInsp" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_INSPIDX" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGridInsp_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="function(s, e) {fn_OnGridEndCallback(s, e);fn_OnSetOrderNo(s, e);}" CallbackError="fn_OnCallbackError"
                                RowDblClick="fn_OnInspRowDblClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridInsp" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_INSPORDER" Caption="순서" Width="50px" />
                                <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="200px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_VIEWSTAND" Caption="표시규격" Width="220px">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPKINDNM" Caption="판정방식" Width="70px" />
                                <dx:GridViewDataColumn FieldName="F_STAND" Caption="기준" Width="90px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한" Width="90px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한" Width="90px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CYCLENM" Caption="점검주기" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_UNITNM" Caption="단위" Width="50px" />

                                <dx:GridViewDataColumn FieldName="F_INSPIDX" Visible="false" />
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                    <div class="paging_r"></div>
                </div>
            </td>
        </tr>
    </table>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>