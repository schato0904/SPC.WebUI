<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0301.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.INSP0301" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var sortNo = -1;
        var visibleIdx = -1;
        var isSortChanged = false;

        $(document).ready(function () {
            // 입력상자 Enter Key Event
            $('#inputTable input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_OnSaveClick();
                    return false;
                }
            });
            $('#inputTableA input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_OnSaveClick('A');
                    return false;
                }
            });
            $('#inputTableB input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_OnSaveClick('B');
                    return false;
                }
            });

            fn_OnSearchClick();
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container_l").offset().top;
            var searchHeight = $(".search_l").height() > 0 ? $(".search_l").height() + 5 : 0;
            var pagingHeight = $(".paging_l").height() > 0 ? $(".paging_l").height() - 20 : 0;
            var height = Math.max(0, parseInt($(window).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid1.SetHeight(height - 38);
            devGrid2.SetHeight(150);

            top = $(".container_r").offset().top;
            searchHeight = $(".search_r").height() > 0 ? $(".search_r").height() + 5 : 0;
            pagingHeight = $(".paging_r").height() > 0 ? $(".paging_r").height() - 20 : 0;
            height = Math.max(0, parseInt($(window).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid3.SetHeight(height + 10);
            devGrid4.SetHeight(height + 10);
        }

        // 조회
        function fn_OnSearchClick(t) {
            if (t == 'R') {
                data = '{';
                data += '"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
                data += '}';
                devGrid2.PerformCallback(data);
            } else if (t == 'A') {
                fn_SetTextValue('srcF_DIVISIONIDX', '');
                fn_SetTextValue('srcF_DIVISIONNM', '');
                fn_OnNewClick(t);
                var data = '{';
                data += '"F_QWK13MID":"' + fn_GetCastValue('srcF_QWK13MID') + '"';
                data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
                data += ',"F_REVNO":"' + fn_GetCastValue('srcF_REVNO') + '"';
                data += '}';
                devGrid3.PerformCallback(data);
            } else if (t == 'B') {
                fn_OnNewClick(t);
                var data = '{';
                data += '"F_QWK13MID":"' + fn_GetCastValue('srcF_QWK13MID') + '"';
                data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
                data += ',"F_REVNO":"' + fn_GetCastValue('srcF_REVNO') + '"';
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('srcF_DIVISIONIDX') + '"';
                data += '}';
                devGrid4.PerformCallback(data);
            } else {
                fn_SetTextValue('srcF_DIVISIONIDX', '');
                fn_SetTextValue('srcF_DIVISIONNM', '');
                fn_SetPageMode("NEW");
                fn_OnNewClick();
                devGrid1.PerformCallback();
                devGrid2.PerformCallback();
                devGrid3.PerformCallback();
                devGrid4.PerformCallback();
            }
        }

        // 입력
        function fn_OnNewClick(t) {
            if (t == 'A') {
                fn_SetClear('A');
                fn_SetClear('B');
            } else if (t == 'B') {
                fn_SetClear('B');
            } else {
                fn_SetTextValue('srcF_DIVISIONIDX', '');
                fn_SetTextValue('srcF_DIVISIONNM', '');
                fn_SetClear();
                fn_SetClear('A');
                fn_SetClear('B');
                devGrid3.PerformCallback();
                devGrid4.PerformCallback();
            }
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick(t) {
            if (fn_GetCastValue('srcF_CONFIRM') == 'true') {
                alert('확정한 검사성적서 양식은 수정할 수 없습니다');
                return false;
            }

            var data = '';
            if (t == 'A') {
                if (!fn_OnValidate('A')) return false;
                if (!confirm('저장하시겠습니까?\r계속 진행하려면 확인을 누르세요')) return false;
                data = fn_OnCreateJsonData('A');
            } else if (t == 'B') {
                if (!fn_OnValidate('B')) return false;
                if (!confirm('저장하시겠습니까?\r계속 진행하려면 확인을 누르세요')) return false;
                data = fn_OnCreateJsonData('B');
            } else {
                if (!fn_OnValidate()) return false;
                if (!confirm('저장하시겠습니까?\r계속 진행하려면 확인을 누르세요')) return false;
                data = fn_OnCreateJsonData();
            }

            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }

        function fn_OnCreateJsonData(t) {
            var data = '{';
            if (t == 'A') {
                data += '"ACTION":"NEW_' + t + '"';
                data += ',"F_QWK13MID":"' + fn_GetCastValue('srcF_QWK13MID') + '"';
                data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
                data += ',"F_REVNO":"' + fn_GetCastValue('srcF_REVNO') + '"';
                data += ',"F_ASSORTMENT":"' + fn_GetCastSelectedItemValue('rdoF_ASSORTMENT') + '"';
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('txtF_DIVISIONIDX') + '"';
                data += ',"F_DIVISIONNM":"' + jsonEscape(fn_GetCastValue('txtF_DIVISIONNM')) + '"';
                data += ',"F_METHOD":"' + jsonEscape(fn_GetCastValue('txtF_METHOD')) + '"';
                data += ',"F_EQUIPMENT":"' + jsonEscape(fn_GetCastValue('txtF_EQUIPMENT')) + '"';
                data += ',"F_DIVISIONSORT":"' + fn_GetCastValue('txtF_DIVISIONSORT') + '"';
                data += ',"F_USESTATUS":"' + fn_GetCastSelectedItemValue('rdoF_USESTATUSA') + '"';
            } else if (t == 'B') {
                data += '"ACTION":"NEW_' + t + '"';
                data += ',"F_QWK13MID":"' + fn_GetCastValue('srcF_QWK13MID') + '"';
                data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
                data += ',"F_REVNO":"' + fn_GetCastValue('srcF_REVNO') + '"';
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('srcF_DIVISIONIDX') + '"';
                data += ',"F_INSPIDX":"' + fn_GetCastValue('txtF_INSPIDX') + '"';
                data += ',"F_INSPNM":"' + jsonEscape(fn_GetCastValue('txtF_INSPNM')) + '"';
                data += ',"F_STANDARD":"' + jsonEscape(fn_GetCastValue('txtF_STANDARD')) + '"';
                data += ',"F_TERM":"' + fn_GetCastValue('txtF_TERM') + '"';
                data += ',"F_TRANSPARENT":"' + fn_GetCastSelectedItemValue('rdoF_TRANSPARENT') + '"';
                data += ',"F_GROUPCNT":"' + fn_GetCastValue('txtF_GROUPCNT') + '"';
                var checked = !fn_GetCastChecked('chkF_ISEXCEPT') ? 'false' : 'true';
                data += ',"F_ISEXCEPT":"' + checked + '"';
                data += ',"F_INSPSORT":"' + fn_GetCastValue('txtF_INSPSORT') + '"';
                data += ',"F_USESTATUS":"' + fn_GetCastSelectedItemValue('rdoF_USESTATUSB') + '"';
            } else {
                data += '"ACTION":"' + fn_GetCastValue('hidPageMode') + '"';
                data += ',"F_QWK13MID":"' + fn_GetCastValue('srcF_QWK13MID') + '"';
                data += ',"F_CONFIRM":"' + fn_GetCastValue('srcF_CONFIRM') + '"';
                data += ',"F_STATUS":"' + fn_GetCastValue('srcF_STATUS') + '"';
                data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
                data += ',"F_REVNO":"' + fn_GetCastValue('srcF_REVNO') + '"';
                data += ',"F_REVDATE":"' + fn_GetCastValue('hidUCFROMDT') + '"';
                data += ',"F_JUDGETP":"' + fn_GetCastSelectedItemValue('srcF_JUDGETP') + '"';
                data += ',"F_TYPENM":"' + fn_GetCastValue('srcF_TYPENM') + '"';
                data += ',"F_ITEMNM":"' + fn_GetCastValue('srcF_ITEMNM') + '"';
                data += ',"F_DOCNUM":"' + fn_GetCastValue('srcF_DOCNUM') + '"';
                data += ',"F_MATERIAL":"' + fn_GetCastValue('srcF_MATERIAL') + '"';
                data += ',"F_SDR":"' + fn_GetCastValue('srcF_SDR') + '"';
                data += ',"F_SHAPE":"' + fn_GetCastValue('srcF_SHAPE') + '"';
                data += ',"F_CONDITION":"' + jsonEscape(fn_GetCastValue('srcF_CONDITION')) + '"';
                data += ',"F_REMARK":"' + jsonEscape(fn_GetCastValue('srcF_REMARK')) + '"';
            }
            data += '}';

            return data;
        }

        // 취소
        function fn_OnCancelClick(t) {
            if (t == 'A') {
                fn_SetClear('A');
                fn_SetClear('B');
            } else if (t == 'B') {
                fn_SetClear('B');
            } else {
                fn_SetTextValue('srcF_DIVISIONIDX', '');
                fn_SetTextValue('srcF_DIVISIONNM', '');
                fn_SetClear();
                fn_SetClear('A');
                fn_SetClear('B');
                devGrid3.PerformCallback();
                devGrid4.PerformCallback();
            }
        }

        // 삭제
        function fn_OnDeleteClick() {

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
            fn_OnCloseLoadingPanel();
        }

        // Validate
        function fn_OnValidate(t) {
            if (t == 'A') {
                if (fn_GetCastSelectedItemValue('rdoF_ASSORTMENT') == '') {
                    alert('항목구분을 선택하세요');
                    fn_Focus('rdoF_ASSORTMENT');
                    return false;
                }
                if (fn_GetCastText('txtF_DIVISIONNM') == '') {
                    alert('항목명을 입력하세요');
                    fn_Focus('txtF_DIVISIONNM');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('rdoF_USESTATUSA') == '') {
                    alert('사용여부를 선택하세요');
                    fn_Focus('rdoF_USESTATUSA');
                    return false;
                }
            } else if (t == 'B') {
                if (fn_GetCastValue('srcF_DIVISIONIDX') == '') {
                    alert('등록할 타입항목을 선택하세요!!');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('rdoF_TRANSPARENT') == '0' && fn_GetCastText('txtF_INSPNM') == '') {
                    alert('항목명을 입력하세요');
                    fn_Focus('txtF_INSPNM');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('rdoF_TRANSPARENT') == '') {
                    alert('부모항목분류명 사용여부를 선택하세요');
                    fn_Focus('rdoF_TRANSPARENT');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('rdoF_USESTATUSB') == '') {
                    alert('사용여부를 선택하세요');
                    fn_Focus('rdoF_USESTATUSB');
                    return false;
                }
            } else {
                if (fn_GetCastSelectedItemValue('srcF_REPORTTP') == '') {
                    alert('성적서구분을 선택하세요');
                    fn_Focus('srcF_REPORTTP');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('srcF_JUDGETP') == '') {
                    alert('판정구분을 선택하세요');
                    fn_Focus('srcF_JUDGETP');
                    return false;
                }
                if (fn_GetCastText('srcF_TYPENM') == '') {
                    alert('성적서명칭을 입력하세요');
                    fn_Focus('srcF_TYPENM');
                    return false;
                }
                if (fn_GetCastText('srcF_ITEMNM') == '') {
                    alert('제품명을 입력하세요');
                    fn_Focus('srcF_ITEMNM');
                    return false;
                }
                if (fn_GetCastText('srcF_DOCNUM') == '') {
                    alert('문서번호를 입력하세요');
                    fn_Focus('srcF_DOCNUM');
                    return false;
                }
            }

            return true;
        }

        // 입력값 초기화
        function fn_SetClear(t) {
            if (t == 'A') {
                fn_OnControlEnableComboBox(rdoF_ASSORTMENT, true);
                fn_SetValue('rdoF_ASSORTMENT', '');
                fn_SetTextValue('txtF_DIVISIONIDX', '0');
                fn_SetTextValue('txtF_DIVISIONNM', '');
                fn_SetTextValue('txtF_METHOD', '');
                fn_SetTextValue('txtF_EQUIPMENT', '');
                fn_SetValue('rdoF_USESTATUSA', '1');
                fn_SetTextValue('txtF_DIVISIONSORT', '0');

                if (fn_GetCastValue('srcF_QWK13MID') == '0'
                    || fn_GetCastValue('srcF_CONFIRM') == 'true') {
                    fn_OnControlEnableComboBox(rdoF_ASSORTMENT, false);
                    fn_OnControlDisableBox(txtF_DIVISIONNM, null);
                    fn_OnControlDisableBox(txtF_METHOD, null);
                    fn_OnControlDisableBox(txtF_EQUIPMENT, null);
                    fn_OnControlEnableComboBox(rdoF_USESTATUSA, false);

                    $('#btnSearchA').attr('disabled', true);
                    $('#btnNewA').attr('disabled', true);
                    $('#btnCancelA').attr('disabled', true);
                    $('#btnDeleteA').attr('disabled', true);
                    $('#btnSaveA').attr('disabled', true);
                } else {
                    fn_OnControlEnableComboBox(rdoF_ASSORTMENT, true);
                    fn_OnControlEnableBox(txtF_DIVISIONNM, null);
                    fn_OnControlEnableBox(txtF_METHOD, null);
                    fn_OnControlEnableBox(txtF_EQUIPMENT, null);
                    fn_OnControlEnableComboBox(rdoF_USESTATUSA, true);

                    $('#btnSearchA').attr('disabled', false);
                    $('#btnNewA').attr('disabled', false);
                    $('#btnCancelA').attr('disabled', false);
                    $('#btnDeleteA').attr('disabled', false);
                    $('#btnSaveA').attr('disabled', false);
                }
            } else if (t == 'B') {
                fn_SetTextValue('txtF_INSPIDX', '0');
                fn_SetTextValue('txtF_INSPNM', '');
                fn_SetTextValue('txtF_STANDARD', '');
                fn_SetTextValue('txtF_TERM', '');
                fn_SetValue('rdoF_TRANSPARENT', '0');
                fn_SetValue('rdoF_USESTATUSB', '1');
                var v = '';
                switch (fn_GetCastSelectedItemValue('srcF_REPORTTP'))
                {
                    case 'AAI101':
                    case 'AAI102':
                    case 'AAI103':
                    case 'AAI104':
                    case 'AAI114':
                    case 'AAI121':
                    case 'AAI122':
                        v = '1';
                        break;
                }
                fn_SetTextValue('txtF_GROUPCNT', v);
                fn_SetChecked('chkF_ISEXCEPT', false);
                fn_SetTextValue('txtF_INSPSORT', '0');

                if (fn_GetCastValue('srcF_QWK13MID') == '0'
                    || fn_GetCastValue('srcF_DIVISIONIDX') == ''
                    || fn_GetCastValue('srcF_CONFIRM') == 'true') {
                    fn_OnControlDisableBox(txtF_INSPNM, null);
                    fn_OnControlDisableBox(txtF_STANDARD, null);
                    fn_OnControlDisableBox(txtF_TERM, null);
                    fn_OnControlEnableComboBox(rdoF_TRANSPARENT, false);
                    fn_OnControlEnableComboBox(rdoF_USESTATUSB, false);
                    fn_OnControlEnableComboBox(chkF_ISEXCEPT, false);
                    fn_OnControlDisableBox(txtF_GROUPCNT, null);

                    $('#btnSearchB').attr('disabled', true);
                    $('#btnNewB').attr('disabled', true);
                    $('#btnCancelB').attr('disabled', true);
                    $('#btnDeleteB').attr('disabled', true);
                    $('#btnSaveB').attr('disabled', true);
                } else {
                    fn_OnControlEnableBox(txtF_INSPNM, null);
                    fn_OnControlEnableBox(txtF_STANDARD, null);
                    fn_OnControlEnableBox(txtF_TERM, null);
                    fn_OnControlEnableComboBox(rdoF_TRANSPARENT, true);
                    fn_OnControlEnableComboBox(rdoF_USESTATUSB, true);
                    fn_OnControlEnableComboBox(chkF_ISEXCEPT, true);
                    fn_OnControlEnableBox(txtF_GROUPCNT, null);

                    $('#btnSearchB').attr('disabled', false);
                    $('#btnNewB').attr('disabled', false);
                    $('#btnCancelB').attr('disabled', false);
                    $('#btnDeleteB').attr('disabled', false);
                    $('#btnSaveB').attr('disabled', false);
                }
            } else {
                fn_SetTextValue('srcF_QWK13MID', '0');
                fn_SetTextValue('srcF_CONFIRM', '0');
                fn_SetTextValue('srcF_STATUS', '0');
                fn_SetValue('srcF_REPORTTP', null);
                fn_OnControlEnableComboBox(srcF_REPORTTP, true);
                fn_SetTextValue('srcF_REVNO', '1');
                fn_SetTextValue('hidUCFROMDT', formatDateString(new Date(), '-'));
                fn_SetDate('txtFROMDT', new Date());
                fn_SetValue('srcF_JUDGETP', null);
                fn_SetTextValue('srcF_TYPENM', '');
                fn_SetTextValue('srcF_ITEMNM', '');
                fn_SetTextValue('srcF_DOCNUM', '');
                fn_SetTextValue('srcF_MATERIAL', '');
                fn_SetTextValue('srcF_SDR', '');
                fn_SetTextValue('srcF_SHAPE', '');
                fn_SetTextValue('srcF_CONDITION', '');
                fn_SetTextValue('srcF_REMARK', '');
                fn_SetPageMode('NEW');

                $('#btnRevision').attr('disabled', true);
                $('#btnConfirm').attr('disabled', true);
            }
        }

        // 페이지 모드 체인지 : NEW, EDIT, REV
        function fn_SetPageMode(mode) {
            switch (mode) {
                case 'NEW':
                    $('#tdStatus').text('신규입력모드');
                    break;
                case 'EDIT':
                    $('#tdStatus').text('수정입력모드');
                    break;
                case 'VIEW':
                    $('#tdStatus').text('이력보기모드');
                    break;
                case 'REV':
                    $('#tdStatus').text('개정모드');
                    break;
            }

            fn_SetTextValue('hidPageMode', mode);
            fn_AdjustSize();
        }

        function fn_OnGrid1RowDblClick(s, e) {
            fn_SetTextValue('srcF_DIVISIONIDX', '');
            fn_SetTextValue('srcF_DIVISIONNM', '');
            //F_COMPCD; F_FACTCD; F_QWK13MID; F_REPORTTP; F_REVNO
            var devGrid = ASPxClientGridView.Cast(s);
            var rowKey = devGrid.GetRowKey(e.visibleIndex).split('|');
            var data = '{';
            data += '"ACTION":"GET"';
            data += ',"F_QWK13MID":"' + rowKey[2] + '"';
            data += ',"F_REPORTTP":"' + rowKey[3] + '"';
            data += ',"F_REVNO":"' + rowKey[4] + '"';
            data += '}';
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }

        function fn_OnGrid2RowDblClick(s, e) {
            fn_SetTextValue('srcF_DIVISIONIDX', '');
            fn_SetTextValue('srcF_DIVISIONNM', '');
            //F_COMPCD; F_FACTCD; F_QWK13MID; F_REPORTTP; F_REVNO
            var devGrid = ASPxClientGridView.Cast(s);
            var rowKey = devGrid.GetRowKey(e.visibleIndex).split('|');
            var data = '{';
            data += '"ACTION":"GET"';
            data += ',"F_QWK13MID":"' + rowKey[2] + '"';
            data += ',"F_REPORTTP":"' + rowKey[3] + '"';
            data += ',"F_REVNO":"' + rowKey[4] + '"';
            data += '}';
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }

        function fn_OnSetControlVisibility(val) {
            //fn_OnControlDisableBox(srcF_MATERIAL, null);
            //fn_OnControlDisableBox(srcF_SDR, null);
            //fn_OnControlDisableBox(srcF_SHAPE, null);
            //fn_OnControlDisableBox(srcF_CONDITION, null);
            //fn_OnControlDisableBox(srcF_REMARK, null);

            //switch (val) {
            //    case 'AAI101':
            //    case 'AAI102':
            //    case 'AAI103':
            //    case 'AAI104':
            //        fn_OnControlEnableBox(srcF_MATERIAL, null);
            //        fn_OnControlEnableBox(srcF_SDR, null);
            //        fn_OnControlEnableBox(srcF_SHAPE, null);
            //        fn_OnControlEnableBox(srcF_CONDITION, null);
            //        fn_OnControlEnableBox(srcF_REMARK, null);
            //        break;
            //    case 'AAI111':
            //        fn_OnControlEnableBox(srcF_REMARK, null);
            //        break;
            //    case 'AAI121':
            //        fn_OnControlEnableBox(srcF_CONDITION, null);
            //        break;
            //}
        }

        function fn_OnSelectedIndexChanged(s, e) {
            fn_OnSetControlVisibility(s.GetValue());
            var data = '{';
            data += '"ACTION":"CHKEXISTS"';
            data += ',"F_REPORTTP":"' + s.GetValue() + '"';
            data += '}';
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }

        // CallbackPanel Event Handler 시작
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                fn_OnOpenLoadingPanel();
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
                    case "NEW":
                    case "EDIT":
                    case "CFM":
                    case "REV":
                        alert(msg);
                        devGrid1.PerformCallback();
                        fn_OnSearchClick('R');
                        data['ACTION'] = 'GET'
                        fn_devCallback_PerformCallback(data);
                        break;
                    case "GET":
                        fn_InputData(data);
                        fn_OnSearchClick('R');
                        fn_OnSearchClick('A');
                        fn_OnSearchClick('B');
                        break;
                    case "CHKEXISTS":
                        alert(msg);
                        fn_OnNewClick();
                        break;
                    case "NEW_A":
                    case "EDIT_A":
                    case "SORT_A":
                        fn_OnSearchClick('A');
                        break;
                    case "GET_A":
                        fn_OnFillData('A', data);
                        break;
                    case "NEW_B":
                    case "EDIT_B":
                    case "SORT_B":
                        fn_OnSearchClick('B');
                        break;
                    case "GET_B":
                        fn_OnFillData('B', data);
                        break;
                }
            } else {
                if (msg != '')
                    alert(msg);
            }

            fn_OnCloseLoadingPanel();
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

        function fn_InputData(pagedata) {
            var F_QWK13MID = GetJsonValueByKey(pagedata, 'F_QWK13MID', '');

            if (F_QWK13MID == '') {
                alert('조회된 데이터가 없거나 일시적 장애입니다');
                return false;
            }

            var JsonValue = '';

            fn_SetTextValue('srcF_QWK13MID', F_QWK13MID);
            fn_SetTextValue('srcF_CONFIRM', GetJsonValueByKey(pagedata, 'F_CONFIRM', ''));
            fn_SetTextValue('srcF_STATUS', GetJsonValueByKey(pagedata, 'F_STATUS', ''));
            fn_SetValue('srcF_REPORTTP', GetJsonValueByKey(pagedata, 'F_REPORTTP', ''));
            fn_OnSetControlVisibility(GetJsonValueByKey(pagedata, 'F_REPORTTP', ''));
            fn_OnControlEnableComboBox(srcF_REPORTTP, false);
            fn_SetTextValue('srcF_REVNO', GetJsonValueByKey(pagedata, 'F_REVNO', ''));
            fn_SetTextValue('hidUCFROMDT', GetJsonValueByKey(pagedata, 'F_REVSTDT', ''));
            fn_SetDate('txtFROMDT', convertDateString(GetJsonValueByKey(pagedata, 'F_REVSTDT', '')));
            fn_SetValue('srcF_JUDGETP', GetJsonValueByKey(pagedata, 'F_JUDGETP', ''));
            fn_SetTextValue('srcF_TYPENM', GetJsonValueByKey(pagedata, 'F_TYPENM', ''));
            fn_SetTextValue('srcF_ITEMNM', GetJsonValueByKey(pagedata, 'F_ITEMNM', ''));
            fn_SetTextValue('srcF_DOCNUM', GetJsonValueByKey(pagedata, 'F_DOCNUM', ''));
            fn_SetTextValue('srcF_MATERIAL', GetJsonValueByKey(pagedata, 'F_MATERIAL', ''));
            fn_SetTextValue('srcF_SDR', GetJsonValueByKey(pagedata, 'F_SDR', ''));
            fn_SetTextValue('srcF_SHAPE', GetJsonValueByKey(pagedata, 'F_SHAPE', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_CONDITION', '');
            JsonValue = JsonValue == '' ? '' : JsonValue.replace(/\\"/g, '"').replace(/\\n/g, "\n");
            fn_SetTextValue('srcF_CONDITION', GetJsonValueByKey(pagedata, 'F_CONDITION', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_REMARK', '');
            JsonValue = JsonValue == '' ? '' : JsonValue.replace(/\\"/g, '"').replace(/\\n/g, "\n");
            fn_SetTextValue('srcF_REMARK', GetJsonValueByKey(pagedata, 'F_REMARK', ''));

            $('#btnRevision').attr('disabled', true);
            $('#btnConfirm').attr('disabled', true);

            if (fn_GetCastValue('srcF_STATUS') == '0' || fn_GetCastValue('srcF_STATUS') == '1') {
                if (fn_GetCastValue('srcF_CONFIRM') == 'false') {
                    fn_SetPageMode('EDIT');
                    $('#btnConfirm').attr('disabled', false);
                } else {
                    fn_SetPageMode('VIEW');

                    $('#btnRevision').attr('disabled', false);
                }
            }

            fn_SetClear('A');
            fn_SetClear('B');
        }

        function fn_OnFillData(t, pagedata) {
            if (t == 'A') {
                var F_DIVISIONIDX = GetJsonValueByKey(pagedata, 'F_DIVISIONIDX', '');

                if (F_DIVISIONIDX == '') {
                    alert('조회된 데이터가 없거나 일시적 장애입니다');
                    return false;
                }

                fn_OnControlEnableComboBox(rdoF_ASSORTMENT, false);
                fn_SetValue('rdoF_ASSORTMENT', GetJsonValueByKey(pagedata, 'F_ASSORTMENT', ''));
                fn_SetTextValue('txtF_DIVISIONIDX', F_DIVISIONIDX);
                fn_SetTextValue('txtF_DIVISIONNM', GetJsonValueByKey(pagedata, 'F_DIVISIONNM', ''));
                fn_SetTextValue('txtF_METHOD', GetJsonValueByKey(pagedata, 'F_METHOD', ''));
                fn_SetTextValue('txtF_EQUIPMENT', GetJsonValueByKey(pagedata, 'F_EQUIPMENT', ''));
                fn_SetValue('rdoF_USESTATUS', GetJsonValueByKey(pagedata, 'F_USESTATUS', ''));
                fn_SetTextValue('txtF_DIVISIONSORT', GetJsonValueByKey(pagedata, 'F_DIVISIONSORT', ''));
            } else if (t == 'B') {
                var F_INSPIDX = GetJsonValueByKey(pagedata, 'F_INSPIDX', '');

                if (F_INSPIDX == '') {
                    alert('조회된 데이터가 없거나 일시적 장애입니다');
                    return false;
                }

                fn_SetTextValue('txtF_INSPIDX', F_INSPIDX);
                if (GetJsonValueByKey(pagedata, 'F_TRANSPARENT', '') == true) {
                    fn_OnControlDisableBox(txtF_INSPNM, null);
                } else {
                    fn_OnControlEnableBox(txtF_INSPNM, null);
                }
                fn_SetTextValue('txtF_INSPNM', GetJsonValueByKey(pagedata, 'F_INSPNM', ''));
                fn_SetTextValue('txtF_STANDARD', GetJsonValueByKey(pagedata, 'F_STANDARD', ''));
                fn_SetTextValue('txtF_TERM', GetJsonValueByKey(pagedata, 'F_TERM', ''));
                fn_SetValue('rdoF_TRANSPARENT', GetJsonValueByKey(pagedata, 'F_TRANSPARENT', ''));
                fn_SetValue('rdoF_USESTATUS', GetJsonValueByKey(pagedata, 'F_USESTATUS', ''));
                fn_SetTextValue('txtF_INSPSORT', GetJsonValueByKey(pagedata, 'F_INSPSORT', ''));
                fn_SetChecked('chkF_ISEXCEPT', GetJsonValueByKey(pagedata, 'F_ISEXCEPT', '') == '1');
                fn_SetTextValue('txtF_GROUPCNT', GetJsonValueByKey(pagedata, 'F_GROUPCNT', ''));
            }
        }

        function fn_OnTransSelectedIndexChanged(s, e) {
            if (s.GetValue() == '1') {
                fn_OnControlDisableBox(txtF_INSPNM, null);
                fn_SetTextValue('txtF_INSPNM', fn_GetCastValue('srcF_DIVISIONNM'));
            } else {
                fn_OnControlEnableBox(txtF_INSPNM, null);
                fn_SetTextValue('txtF_INSPNM', '');
            }
        }

        function fn_OnRevisionClick() {
            if (!confirm('개정하겠습니까?')) return;
            fn_SetTextValue('srcF_DIVISIONIDX', '');
            fn_SetTextValue('srcF_DIVISIONNM', '');
            fn_SetClear('A');
            fn_SetClear('B');
            devGrid3.PerformCallback();
            devGrid4.PerformCallback();
            fn_SetPageMode('REV');
            fn_SetTextValue('srcF_CONFIRM', '0');
            fn_SetTextValue('srcF_STATUS', '0');
            fn_SetTextValue('srcF_REVNO', parseInt(fn_GetCastValue('srcF_REVNO'), 10) + 1);
        }

        function fn_OnConfirmClick() {
            if (!confirm('확정하겠습니까?\r확정한 이후에는 수정할 수 없습니다')) return;
            var data = '{';
            data += '"ACTION":"CFM"';
            data += ',"F_QWK13MID":"' + fn_GetCastValue('srcF_QWK13MID') + '"';
            data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
            data += ',"F_REVNO":"' + fn_GetCastValue('srcF_REVNO') + '"';
            data += '}';
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }

        function fn_OnAddInspection(c, n, i) {
            fn_SetTextValue('srcF_DIVISIONIDX', c);
            fn_SetTextValue('srcF_DIVISIONNM', n);
            fn_OnSearchClick('B');
        }

        function fn_OnChangeSort(t, d, c, n, i) {
            var data = '{';
            data += '"ACTION":"SORT_' + t + '"';
            data += ',"F_QWK13MID":"' + fn_GetCastValue('srcF_QWK13MID') + '"';
            data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
            data += ',"F_REVNO":"' + fn_GetCastValue('srcF_REVNO') + '"';

            if (t == 'A') {
                data += ',"F_DIVISIONIDX":"' + c + '"';
                data += ',"F_DIVISIONSORT":"' + n + '"';
                data += ',"F_DIRECTION":"' + d + '"';
            } else if (t == 'B') {
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('srcF_DIVISIONIDX') + '"';
                data += ',"F_INSPIDX":"' + c + '"';
                data += ',"F_INSPSORT":"' + n + '"';
                data += ',"F_DIRECTION":"' + d + '"';
            }

            isSortChanged = true;
            sortNo = n;

            if (t == 'A') {
                if (n > 0 || n < devGrid1.GetVisibleRowsOnPage() - 1)
                    visibleIdx = d == '+' ? ++i : --i;
            } else if (t == 'B') {
                if (n > 0 || n < devGrid2.GetVisibleRowsOnPage() - 1)
                    visibleIdx = d == '+' ? ++i : --i;
            }

            data += '}';
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }

        function fn_OnRowDblClick(t, s, e) {
            var rowKeys;

            var data = '{';
            data += '"ACTION":"GET_' + t + '"';
            data += ',"F_QWK13MID":"' + fn_GetCastValue('srcF_QWK13MID') + '"';
            data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
            data += ',"F_REVNO":"' + fn_GetCastValue('srcF_REVNO') + '"';

            if (t == 'A') {
                rowKeys = devGrid3.GetRowKey(e.visibleIndex);
                data += ',"F_DIVISIONIDX":"' + rowKeys + '"';
            } else {
                rowKeys = devGrid4.GetRowKey(e.visibleIndex);
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('srcF_DIVISIONIDX') + '"';
                data += ',"F_INSPIDX":"' + rowKeys + '"';
            }

            data += '}';
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
    <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
    <table class="layerTable">
        <colgroup>
            <col style="width:450px" />
            <col style="width:10px;" />
            <col />
        </colgroup>
        <tr>
            <td>
                <div class="container_l">
                    <div class="search_l">
                        <div class="blockTitle"><span>[검사성적서 양식 목록]</span></div>
                    </div>
                    <div class="content_l">
                        <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_COMPCD;F_FACTCD;F_QWK13MID;F_REPORTTP;F_REVNO" EnableViewState="false" EnableRowsCache="false"
                             OnCustomCallback="devGrid1_CustomCallback" OnCustomColumnDisplayText="devGrid1_CustomColumnDisplayText">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                                RowDblClick="fn_OnGrid1RowDblClick" />
				            <Templates>
					            <StatusBar>
						            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid1" />
					            </StatusBar>
				            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_REPORTTP" Caption="성적서구분">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_REVNO" Caption="개정" Width="40px" />
                                <dx:GridViewDataColumn FieldName="F_REVSTDT" Caption="개정일" Width="90px" />
                                <dx:GridViewDataColumn FieldName="F_CONFIRM" Caption="진행상태" Width="70px" />
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                    <div class="paging_l">
                        <div class="divPadding"></div>
                        <div class="blockTitle"><span>[검사성적서 양식 개정이력]</span></div>
                        <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_COMPCD;F_FACTCD;F_QWK13MID;F_REPORTTP;F_REVNO" EnableViewState="false" EnableRowsCache="false"
                             OnCustomCallback="devGrid2_CustomCallback" OnCustomColumnDisplayText="devGrid2_CustomColumnDisplayText">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                                RowDblClick="fn_OnGrid2RowDblClick" />
				            <Templates>
					            <StatusBar>
						            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
					            </StatusBar>
				            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_REPORTTP" Caption="성적서구분">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_REVNO" Caption="개정" Width="40px" />
                                <dx:GridViewDataColumn FieldName="F_REVSTDT" Caption="개정일" Width="90px" />
                                <dx:GridViewDataColumn FieldName="F_STATUS" Caption="상태" Width="70px" />
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </td>
            <td>&nbsp;</td>
            <td>
                <div class="container_r">
                    <div class="search_r">
                        <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" ClientVisible="false" Text="NEW" />
                        <dx:ASPxTextBox ID="srcF_QWK13MID" ClientInstanceName="srcF_QWK13MID" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_CONFIRM" ClientInstanceName="srcF_CONFIRM" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_STATUS" ClientInstanceName="srcF_STATUS" runat="server" ClientVisible="false" />
                        <div class="blockTitle"><span>[검사성적서 양식 등록/수정]</span></div>
                        <table id="inputTable" class="contentTable">
                            <colgroup>
                                <col style="width:100px;" />
                                <col />
                                <col style="width:100px;" />
                                <col />
                                <col style="width:100px;" />
                                <col />
                                <col style="width:100px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td class="tdLabel required">성적서구분</td>
                                <td class="tdInput">
                                    <dx:ASPxComboBox ID="srcF_REPORTTP" ClientInstanceName="srcF_REPORTTP" runat="server" Width="100%"
                                        IncrementalFilteringMode="None" CssClass="NoXButton" NullText="선택하세요">
                                        <ClientSideEvents SelectedIndexChanged="fn_OnSelectedIndexChanged" />
                                    </dx:ASPxComboBox>
                                </td>
                                <td class="tdLabel required">개정번호/일</td>
                                <td class="tdInput">
                                    <table class="layerTable">
                                        <colgroup>
                                            <col style="width:40px;" />
                                            <col style="width:5px;" />
                                            <col />
                                        </colgroup>
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="srcF_REVNO" ClientInstanceName="srcF_REVNO" runat="server" Width="100%">
                                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false"  TodayFromDate="true" SingleDate="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="tdLabel required">판정구분</td>
                                <td class="tdInput">
                                    <dx:ASPxComboBox ID="srcF_JUDGETP" ClientInstanceName="srcF_JUDGETP" runat="server" Width="100%"
                                        IncrementalFilteringMode="None" CssClass="NoXButton" NullText="선택하세요" />
                                </td>
                                <td id="tdStatus" class="bg-warning text-black font-bold text-center"></td>
                                <td class="tdInput text-right">
                                    <button id="btnRevision" class="btn btn-sm btn-success" onclick="fn_OnRevisionClick(); return false;">
                                        <i class="i i-retweet"></i>
                                        <span class="text">개정</span>
                                    </button>
                                    <button id="btnConfirm" class="btn btn-sm btn-primary" onclick="fn_OnConfirmClick(); return false;">
                                        <i class="i i-checkmark2"></i>
                                        <span class="text">확정</span>
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel required">성적서명칭</td>
                                <td class="tdInput" colspan="3"><dx:ASPxTextBox ID="srcF_TYPENM" ClientInstanceName="srcF_TYPENM" runat="server" Width="100%" /></td>
                                <td class="tdLabel required">제품명</td>
                                <td class="tdInput" colspan="3"><dx:ASPxTextBox ID="srcF_ITEMNM" ClientInstanceName="srcF_ITEMNM" runat="server" Width="100%" /></td>
                            </tr>
                            <tr>
                                <td class="tdLabel required">문서번호</td>
                                <td class="tdInput"><dx:ASPxTextBox ID="srcF_DOCNUM" ClientInstanceName="srcF_DOCNUM" runat="server" Width="100%" /></td>
                                <td class="tdLabel">재료</td>
                                <td class="tdInput"><dx:ASPxTextBox ID="srcF_MATERIAL" ClientInstanceName="srcF_MATERIAL" runat="server" Width="100%" /></td>
                                <td class="tdLabel">SDR</td>
                                <td class="tdInput"><dx:ASPxTextBox ID="srcF_SDR" ClientInstanceName="srcF_SDR" runat="server" Width="100%" /></td>
                                <td class="tdLabel">형태</td>
                                <td class="tdInput"><dx:ASPxTextBox ID="srcF_SHAPE" ClientInstanceName="srcF_SHAPE" runat="server" Width="100%" /></td>
                            </tr>
                            <tr>
                                <td class="tdLabel">검사방식<br />및 조건</td>
                                <td class="tdInput" colspan="3"><dx:ASPxMemo ID="srcF_CONDITION" ClientInstanceName="srcF_CONDITION" runat="server" Width="100%" Height="60px" /></td>
                                <td class="tdLabel">특이사항</td>
                                <td class="tdInput" colspan="3"><dx:ASPxMemo ID="srcF_REMARK" ClientInstanceName="srcF_REMARK" runat="server" Width="100%" Height="60px" /></td>
                            </tr>
                        </table>
                        <div class="divPadding"></div>
                        <table class="layerTableNoHeight">
                            <colgroup>
                                <col style="width:50%;" />
                                <col style="width:5px;" />
                                <col style="width:50%;" />
                            </colgroup>
                            <tr>
                                <td>
                                    <table class="layerTableNoHeight">
                                        <colgroup>
                                            <col />
                                            <col />
                                        </colgroup>
                                        <tr>
                                            <td class="font-bold text-black">[검사성적서 타입항목 관리]</td>
                                            <td>
                                                <table class="layerTable">
                                                    <colgroup>
                                                        <col />
                                                        <col style="width:50px;" />
                                                        <col style="width:50px;" />
                                                        <col style="width:50px;" />
                                                        <%--<col style="width:50px;" />--%>
                                                        <col style="width:50px;" />
                                                    </colgroup>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="padding-right:3px;">
                                                            <button id="btnSearchA" class="btn btn-xs btn-success" onclick="fn_OnSearchClick('A'); return false;">
                                                                <i class="fa fa-search"></i>
                                                                <span class="text">조회</span>
                                                            </button>
                                                        </td>
                                                        <td style="padding-right:3px;">
                                                            <button id="btnNewA" class="btn btn-xs btn-primary" onclick="fn_OnNewClick('A'); return false;">
                                                                <i class="fa fa-pencil"></i>
                                                                <span class="text">입력</span>
                                                            </button>
                                                        </td>
                                                        <td style="padding-right:3px;">
                                                            <button id="btnCancelA" class="btn btn-xs btn-warning" onclick="fn_OnCancelClick('A'); return false;">
                                                                <i class="fa fa-undo"></i>
                                                                <span class="text">취소</span>
                                                            </button>
                                                        </td>
                                                        <%--<td style="padding-right:3px;">
                                                            <button id="btnDeleteA" class="btn btn-xs btn-danger" onclick="fn_OnDeleteClick('A'); return false;">
                                                                <i class="fa fa-scissors"></i>
                                                                <span class="text">삭제</span>
                                                            </button>
                                                        </td>--%>
                                                        <td>
                                                            <button id="btnSaveA" class="btn btn-xs btn-success" onclick="fn_OnSaveClick('A'); return false;">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">저장</span>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <table class="layerTableNoHeight">
                                        <colgroup>
                                            <col />
                                            <col />
                                        </colgroup>
                                        <tr>
                                            <td class="font-bold text-black">[검사성적서 타입항목별 기준 관리]</td>
                                            <td>
                                                <table class="layerTable">
                                                    <colgroup>
                                                        <col />
                                                        <col style="width:50px;" />
                                                        <col style="width:50px;" />
                                                        <col style="width:50px;" />
                                                        <%--<col style="width:50px;" />--%>
                                                        <col style="width:50px;" />
                                                    </colgroup>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="padding-right:3px;">
                                                            <button id="btnSearchB" class="btn btn-xs btn-success" onclick="fn_OnSearchClick('B'); return false;">
                                                                <i class="fa fa-search"></i>
                                                                <span class="text">조회</span>
                                                            </button>
                                                        </td>
                                                        <td style="padding-right:3px;">
                                                            <button id="btnNewB" class="btn btn-xs btn-primary" onclick="fn_OnNewClick('B'); return false;">
                                                                <i class="fa fa-pencil"></i>
                                                                <span class="text">입력</span>
                                                            </button>
                                                        </td>
                                                        <td style="padding-right:3px;">
                                                            <button id="btnCancelB" class="btn btn-xs btn-warning" onclick="fn_OnCancelClick('B'); return false;">
                                                                <i class="fa fa-undo"></i>
                                                                <span class="text">취소</span>
                                                            </button>
                                                        </td>
                                                        <%--<td style="padding-right:3px;">
                                                            <button id="btnDeleteB" class="btn btn-xs btn-danger" onclick="fn_OnDeleteClick('B'); return false;">
                                                                <i class="fa fa-scissors"></i>
                                                                <span class="text">삭제</span>
                                                            </button>
                                                        </td>--%>
                                                        <td>
                                                            <button id="btnSaveB" class="btn btn-xs btn-success" onclick="fn_OnSaveClick('B'); return false;">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">저장</span>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height:10px;"></td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtF_DIVISIONIDX" ClientInstanceName="txtF_DIVISIONIDX" runat="server" ClientVisible="false" />
                                    <dx:ASPxTextBox ID="txtF_DIVISIONSORT" ClientInstanceName="txtF_DIVISIONSORT" runat="server" ClientVisible="false" />
                                    <table id="inputTableA" class="contentTable">
                                        <colgroup>
                                            <col style="width:80px;" />
                                            <col />
                                            <col style="width:80px;" />
                                            <col />
                                        </colgroup>
                                        <tbody>
                                            <tr>
                                                <td class="tdLabel required">항목구분</td>
                                                <td class="tdInput" colspan="3">
                                                    <dx:ASPxRadioButtonList ID="rdoF_ASSORTMENT" ClientInstanceName="rdoF_ASSORTMENT" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel required">항목명</td>
                                                <td class="tdInput">
                                                    <dx:ASPxMemo ID="txtF_DIVISIONNM" ClientInstanceName="txtF_DIVISIONNM" runat="server" Width="100%" Height="86px" />
                                                </td>
                                                <td class="tdLabel">검사방식</td>
                                                <td class="tdInput">
                                                    <dx:ASPxMemo ID="txtF_METHOD" ClientInstanceName="txtF_METHOD" runat="server" Width="100%" Height="86px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel">측정기</td>
                                                <td class="tdInput">
                                                    <dx:ASPxMemo ID="txtF_EQUIPMENT" ClientInstanceName="txtF_EQUIPMENT" runat="server" Width="100%" Height="50px" />
                                                </td>
                                                <td class="tdLabel required">사용여부</td>
                                                <td class="tdInput">
                                                    <dx:ASPxRadioButtonList ID="rdoF_USESTATUSA" ClientInstanceName="rdoF_USESTATUSA" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                                                        <Items>
                                                            <dx:ListEditItem Text="사용" Value="1" />
                                                            <dx:ListEditItem Text="중단" Value="0" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td></td>
                                <td>
                                    <dx:ASPxTextBox ID="txtF_INSPIDX" ClientInstanceName="txtF_INSPIDX" runat="server" ClientVisible="false" />
                                    <dx:ASPxTextBox ID="txtF_INSPSORT" ClientInstanceName="txtF_INSPSORT" runat="server" ClientVisible="false" />
                                    <table id="inputTableB" class="contentTable">
                                        <colgroup>
                                            <col style="width:80px;" />
                                            <col />
                                            <col style="width:80px;" />
                                            <col style="width:120px;" />
                                        </colgroup>
                                        <tbody>
                                            <tr>
                                                <td class="tdLabel">타입항목명</td>
                                                <td class="tdInput" colspan="3">
                                                    <dx:ASPxTextBox ID="srcF_DIVISIONIDX" ClientInstanceName="srcF_DIVISIONIDX" runat="server" ClientVisible="false" />
                                                    <dx:ASPxTextBox ID="srcF_DIVISIONNM" ClientInstanceName="srcF_DIVISIONNM" runat="server" Width="100%">
                                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel" rowspan="2">항목명</td>
                                                <td class="tdInput" rowspan="2">
                                                    <dx:ASPxMemo ID="txtF_INSPNM" ClientInstanceName="txtF_INSPNM" runat="server" Width="100%" Height="50px" />
                                                </td>
                                                <td class="tdLabel">부모명</td>
                                                <td class="tdInput">
                                                    <dx:ASPxRadioButtonList ID="rdoF_TRANSPARENT" ClientInstanceName="rdoF_TRANSPARENT" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                                                        <ClientSideEvents SelectedIndexChanged="fn_OnTransSelectedIndexChanged" />
                                                        <Items>
                                                            <dx:ListEditItem Text="사용" Value="1" />
                                                            <dx:ListEditItem Text="중단" Value="0" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel required">그룹갯수</td>
                                                <td class="tdInput">
                                                    <dx:ASPxTextBox ID="txtF_GROUPCNT" ClientInstanceName="txtF_GROUPCNT" runat="server" Width="100%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel" rowspan="2">판정기준</td>
                                                <td class="tdInput" rowspan="2">
                                                    <dx:ASPxMemo ID="txtF_STANDARD" ClientInstanceName="txtF_STANDARD" runat="server" Width="100%" Height="50px" />
                                                </td>
                                                <td class="tdLabel required">사용여부</td>
                                                <td class="tdInput">
                                                    <dx:ASPxRadioButtonList ID="rdoF_USESTATUSB" ClientInstanceName="rdoF_USESTATUSB" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                                                        <Items>
                                                            <dx:ListEditItem Text="사용" Value="1" />
                                                            <dx:ListEditItem Text="중단" Value="0" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdInput" colspan="2">
                                                    <dx:ASPxCheckBox ID="chkF_ISEXCEPT" ClientInstanceName="chkF_ISEXCEPT" runat="server" Text="해당사항없음" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdLabel">조건</td>
                                                <td class="tdInput" colspan="3">
                                                    <dx:ASPxTextBox ID="txtF_TERM" ClientInstanceName="txtF_TERM" runat="server" Width="100%" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content_r">
                        <table class="layerTableNoHeight">
                            <colgroup>
                                <col style="width:50%;" />
                                <col style="width:5px;" />
                                <col style="width:50%;" />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="devGrid3" ClientInstanceName="devGrid3" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_DIVISIONIDX" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid3_CustomCallback" OnCustomColumnDisplayText="devGrid3_CustomColumnDisplayText">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowFocusedRow="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                                            RowDblClick="function(s, e) { fn_OnRowDblClick('A', s, e); }" />
				                        <Templates>
					                        <StatusBar>
						                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid3" />
					                        </StatusBar>
				                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_ASSORTMENT" Caption="항목구분" Width="90px" />
                                            <dx:GridViewDataColumn FieldName="F_DIVISIONNM" Caption="항목명">
                                                <CellStyle HorizontalAlign="Left"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_DIVISIONSORT" Caption="출력<br/>순번" Width="40px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_USESTATUS" Caption="사용<br/>여부" Width="40px" />
                                            <dx:GridViewDataTextColumn Caption="순서<br/>변경" UnboundType="String" Width="50px">
                                                <DataItemTemplate>
                                                    <dx:ASPxHyperLink ID="btnSortDownA" runat="server" Text="△" OnInit="btnSortDownA_Init" />
                                                    <dx:ASPxHyperLink ID="btnSortUpA" runat="server" Text="▽" OnInit="btnSortUpA_Init" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="기준<br/>등록" UnboundType="String" Width="40px">
                                                <DataItemTemplate>
                                                    <dx:ASPxHyperLink ID="btnAddA" runat="server" Text="등록" OnInit="btnAddA_Init" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <dx:ASPxGridView ID="devGrid4" ClientInstanceName="devGrid4" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_INSPIDX" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid4_CustomCallback" OnCustomColumnDisplayText="devGrid4_CustomColumnDisplayText">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowFocusedRow="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                                            RowDblClick="function(s, e) { fn_OnRowDblClick('B', s, e); }" />
				                        <Templates>
					                        <StatusBar>
						                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid4" />
					                        </StatusBar>
				                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명">
                                                <CellStyle HorizontalAlign="Left"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_INSPSORT" Caption="출력<br/>순번" Width="40px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_USESTATUS" Caption="사용<br/>여부" Width="40px" />
                                            <dx:GridViewDataTextColumn Caption="순서<br/>변경" UnboundType="String" Width="50px">
                                                <DataItemTemplate>
                                                    <dx:ASPxHyperLink ID="btnSortDownB" runat="server" Text="△" OnInit="btnSortDownB_Init" />
                                                    <dx:ASPxHyperLink ID="btnSortUpB" runat="server" Text="▽" OnInit="btnSortUpB_Init" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="paging_r"></div>
                </div>
            </td>
        </tr>
    </table>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>