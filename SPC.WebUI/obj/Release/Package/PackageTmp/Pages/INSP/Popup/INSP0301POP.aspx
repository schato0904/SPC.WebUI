<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0301POP.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.Popup.INSP0301POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var sortNo = -1;
        var visibleIdx = -1;
        var isSortChanged = false;

        $(document).ready(function () {
            // 입력상자 Enter Key Event
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

            fn_OnClearData('A');
            fn_OnSetControlVisibility('A', fn_GetCastValue('srcF_REPORTTP'));
            fn_OnSearchClick('devGrid1');
            fn_OnClearData('B');
            fn_OnSetControlVisibility('B', fn_GetCastValue('srcF_REPORTTP'));
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container_l").offset().top;
            var searchHeight = $(".search_l").height() > 0 ? $(".search_l").height() + 5 : 0;
            var pagingHeight = $(".paging_l").height() > 0 ? $(".paging_l").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid1.SetHeight(height - 10);
            devGridSub1.SetHeight(150);

            top = $(".container_r").offset().top;
            searchHeight = $(".search_r").height() > 0 ? $(".search_r").height() + 5 : 0;
            pagingHeight = $(".paging_r").height() > 0 ? $(".paging_r").height() - 20 : 0;
            height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid2.SetHeight(height - 10);
            devGridSub2.SetHeight(150);
        }

        // 조회
        function fn_OnSearchClick(gridID) {
            var devGrid = ASPxClientGridView.Cast(gridID);
            devGrid.PerformCallback(fn_GetCastValue('srcF_DIVISIONIDX'));
        }

        // 입력
        function fn_OnNewClick(t) {
            if (t == 'B' && fn_GetCastValue('srcF_DIVISIONIDX') == '') {
                alert('등록할 타입항목을 선택하세요!!');
                return false;
            }

            fn_OnCancelClick(t);
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick(t) {
            if (!fn_OnValidate(t)) return false;
            if (!confirm('저장하시겠습니까?\r계속 진행하려면 확인을 누르세요')) return false;
            var data = fn_OnCreateJsonData(t);
            //console.log(data);
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }

        // 취소
        function fn_OnCancelClick(t) {
            fn_OnClearData(t);
            fn_OnSetControlVisibility(t, fn_GetCastValue('srcF_REPORTTP'));

            if (t == 'A') {
                devGrid1.SetFocusedRowIndex(-1);
                devGridSub1.PerformCallback('-1');
            } else if (t == 'B') {
                devGrid2.SetFocusedRowIndex(-1);
                devGridSub2.PerformCallback(fn_GetCastValue('srcF_DIVISIONIDX') + '|-1');
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

            if (true == isSortChanged) {
                isSortChanged = false;
                s.SetFocusedRowIndex(visibleIdx);
            } else {
                visibleIdx = -1;
                s.SetFocusedRowIndex(visibleIdx);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
            fn_OnCloseLoadingPanel();
        }

        // Validate
        function fn_OnValidate(t) {
            if (t == 'A') {
                if (fn_GetCastText('txtF_DIVISIONNM') == '') {
                    alert('항목명을 입력하세요');
                    fn_Focus('txtF_DIVISIONNM');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('rdoF_ASSORTMENT') == '') {
                    alert('항목구분을 선택하세요');
                    fn_Focus('rdoF_ASSORTMENT');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('rdoF_STATUS') == '') {
                    alert('사용여부를 선택하세요');
                    fn_Focus('rdoF_STATUS');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('rdoF_REVSTATUS') == '') {
                    alert('개정여부를 선택하세요');
                    fn_Focus('rdoF_REVSTATUS');
                    return false;
                }
                if (fn_GetCastDate('txtF_REVSTDT') == '') {
                    alert('개정시작일을 입력하세요');
                    fn_Focus('txtF_REVSTDT');
                    return false;
                }
            } else if (t == 'B') {
                if (t == 'B' && fn_GetCastValue('srcF_DIVISIONIDX') == '') {
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
                if (fn_GetCastSelectedItemValue('rdoF_INSPSTATUS') == '') {
                    alert('사용여부를 선택하세요');
                    fn_Focus('rdoF_INSPSTATUS');
                    return false;
                }
                if (fn_GetCastSelectedItemValue('rdoF_INSPREVSTATUS') == '') {
                    alert('개정여부를 선택하세요');
                    fn_Focus('rdoF_INSPREVSTATUS');
                    return false;
                }
                if (fn_GetCastDate('txtF_INSPREVSTDT') == '') {
                    alert('개정시작일을 입력하세요');
                    fn_Focus('txtF_INSPREVSTDT');
                    return false;
                }
            }

            return true;
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
                var type = typeof (parameter.TYPE) != 'undefined' ? parameter.TYPE : null;
                switch (action) {
                    case "I":
                    case "U":
                        alert(msg);
                        fn_OnClearData(type);
                        fn_OnSetControlVisibility(type, '');
                        if(type == 'A')
                            fn_OnSearchClick(devGrid1);
                        else if(type == 'B')
                            fn_OnSearchClick(devGrid2);
                        break;
                    case "G":
                        fn_OnFillData(type, data);
                        break;
                    case "S":
                        if (type == 'A')
                            fn_OnSearchClick(devGrid1);
                        else if (type == 'B')
                            fn_OnSearchClick(devGrid2);
                        break;
                }
            } else {
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

        function fn_OnSetControlVisibility(t, val) {
            //if (t == 'A') {
            //    fn_OnControlDisableBox(txtF_METHOD, null);
            //    fn_OnControlDisableBox(txtF_EQUIPMENT, null);

            //    switch (val) {
            //        case 'AAI101':
            //        case 'AAI103':
            //            fn_OnControlEnableBox(txtF_METHOD, null);
            //            fn_OnControlEnableBox(txtF_EQUIPMENT, null);
            //            break;
            //    }
            //} else if (t == 'B') {
            //    fn_OnControlDisableBox(txtF_INSPNM, null);
            //    fn_OnControlDisableBox(txtF_STANDARD, null);
            //    fn_OnControlDisableBox(txtF_TERM, null);
            //    fn_OnControlDisableBox(txtF_INSPNO, null); 
            //    fn_OnControlEnableComboBox(rdoF_TRANSPARENT, false);
            //    fn_OnControlEnableComboBox(rdoF_INSPSTATUS, false);
            //    fn_OnControlEnableComboBox(rdoF_INSPREVSTATUS, false); 
            //    fn_OnControlDisableBox(txtF_INSPREVSTDT, null);
            //    fn_OnControlDisableBox(chkF_ISEXCEPT, null);

            //    if (fn_GetCastText('srcF_DIVISIONIDX') != '') {
            //        switch (val) {
            //            case 'AAI101':
            //                fn_OnControlEnableBox(txtF_STANDARD, null);
            //                break;
            //            case 'AAI102':
            //                fn_OnControlEnableBox(txtF_INSPNM, null);
            //                fn_OnControlEnableBox(txtF_STANDARD, null);
            //                fn_OnControlEnableBox(txtF_TERM, null);
            //                break;
            //            case 'AAI103':
            //                fn_OnControlEnableBox(txtF_INSPNM, null);
            //                fn_OnControlEnableBox(txtF_STANDARD, null);
            //                break;
            //        }

            //        if (fn_GetCastSelectedItemValue('rdoF_TRANSPARENT') == '1') {
            //            fn_OnControlDisableBox(txtF_INSPNM, null);
            //        }

            //        fn_OnControlEnableBox(txtF_INSPNO, null);
            //        fn_OnControlEnableComboBox(rdoF_TRANSPARENT, true);
            //        fn_OnControlEnableComboBox(rdoF_INSPSTATUS, true);
            //        fn_OnControlEnableComboBox(rdoF_INSPREVSTATUS, true);
            //        fn_OnControlEnableBox(txtF_INSPREVSTDT, null);
            //        fn_OnControlEnableBox(chkF_ISEXCEPT, null);
            //    }
            //}
        }

        function fn_OnClearData(t) {
            if (t == 'A') {
                fn_OnControlEnableComboBox(rdoF_ASSORTMENT, true);
                fn_SetValue('rdoF_ASSORTMENT', '');
                fn_SetTextValue('txtF_DIVISIONIDX', '0');
                fn_SetTextValue('txtF_DIVISIONNM', '');
                fn_SetTextValue('txtF_METHOD', '');
                fn_SetTextValue('txtF_EQUIPMENT', '');
                fn_SetValue('rdoF_STATUS', '1');
                fn_SetValue('rdoF_REVSTATUS', '1'); 
                fn_SetTextValue('txtF_DIVISIONSORT', '0');
                fn_SetTextValue('txtF_DIVISIONREV', '1.0');
                fn_SetDate('txtF_REVSTDT', new Date());
            } else if (t == 'B') {
                fn_SetTextValue('txtF_INSPIDX', '0');
                fn_SetTextValue('txtF_INSPNM', '');
                fn_SetTextValue('txtF_STANDARD', '');
                fn_SetTextValue('txtF_TERM', '');
                try { fn_SetTextValue('txtF_INSPNO', ''); } catch (e) { }
                fn_SetValue('rdoF_TRANSPARENT', '0');
                fn_SetValue('rdoF_INSPSTATUS', '1');
                fn_SetValue('rdoF_INSPREVSTATUS', '1');
                fn_SetTextValue('txtF_INSPREV', '1.0');
                fn_SetDate('txtF_INSPREVSTDT', new Date());
                fn_SetChecked('chkF_ISEXCEPT', false);
            }
        }

        function fn_OnFillData(t, pagedata) {
            if (t == 'A') {
                var divisionIDX = GetJsonValueByKey(pagedata, 'F_DIVISIONIDX', '');

                if (divisionIDX == '') {
                    alert('조회된 데이터가 없거나 일시적 장애입니다');
                    return false;
                }
                
                fn_OnControlEnableComboBox(rdoF_ASSORTMENT, false);
                fn_SetValue('rdoF_ASSORTMENT', GetJsonValueByKey(pagedata, 'F_ASSORTMENT', ''));
                fn_SetTextValue('txtF_DIVISIONIDX', divisionIDX);
                fn_SetTextValue('txtF_DIVISIONNM', GetJsonValueByKey(pagedata, 'F_DIVISIONNM', ''));
                fn_SetTextValue('txtF_METHOD', GetJsonValueByKey(pagedata, 'F_METHOD', ''));
                fn_SetTextValue('txtF_EQUIPMENT', GetJsonValueByKey(pagedata, 'F_EQUIPMENT', ''));
                fn_SetValue('rdoF_STATUS', GetJsonValueByKey(pagedata, 'F_STATUS', ''));
                fn_SetValue('rdoF_REVSTATUS', '0');
                fn_SetTextValue('txtF_DIVISIONSORT', GetJsonValueByKey(pagedata, 'F_DIVISIONSORT', ''));
                fn_SetTextValue('txtF_DIVISIONREV', GetJsonValueByKey(pagedata, 'F_DIVISIONREV', ''));
                fn_SetDate('txtF_REVSTDT', convertDateString(GetJsonValueByKey(pagedata, 'F_REVSTDT', '')));
            } else if (t == 'B') {
                var inspIDX = GetJsonValueByKey(pagedata, 'F_INSPIDX', '');

                if (inspIDX == '') {
                    alert('조회된 데이터가 없거나 일시적 장애입니다');
                    return false;
                }

                fn_SetTextValue('txtF_INSPIDX', inspIDX);
                fn_SetTextValue('txtF_INSPNM', GetJsonValueByKey(pagedata, 'F_INSPNM', ''));
                fn_SetTextValue('txtF_STANDARD', GetJsonValueByKey(pagedata, 'F_STANDARD', ''));
                fn_SetTextValue('txtF_TERM', GetJsonValueByKey(pagedata, 'F_TERM', ''));
                fn_SetValue('rdoF_TRANSPARENT', GetJsonValueByKey(pagedata, 'F_TRANSPARENT', ''));
                fn_SetValue('rdoF_INSPSTATUS', GetJsonValueByKey(pagedata, 'F_STATUS', ''));
                fn_SetValue('rdoF_INSPREVSTATUS', '0');
                fn_SetTextValue('txtF_INSPNO', GetJsonValueByKey(pagedata, 'F_INSPNO', ''));
                fn_SetTextValue('txtF_INSPREV', GetJsonValueByKey(pagedata, 'F_INSPREV', ''));
                fn_SetDate('txtF_INSPREVSTDT', convertDateString(GetJsonValueByKey(pagedata, 'F_REVSTDT', '')));
                fn_SetChecked('chkF_ISEXCEPT', GetJsonValueByKey(pagedata, 'F_ISEXCEPT', '') == '1');

                fn_OnSetControlVisibility('B', fn_GetCastValue('srcF_REPORTTP'));
            }
        }

        function fn_OnCreateJsonData(t) {
            var mode = '';
            if(t == 'A') {
                mode = fn_GetCastValue('txtF_DIVISIONIDX') == '0' ? 'I' : 'U';
            } else if(t == 'B') {
                mode = fn_GetCastValue('txtF_INSPIDX') == '0' ? 'I' : 'U';
            }
            var data = '{';

            data += '"ACTION":"' + mode + '"';
            data += ',"TYPE":"' + t + '"';
            data += ',"F_TYPECD":"' + fn_GetCastValue('srcF_TYPECD') + '"';
            if (t == 'A') {
                data += ',"F_ASSORTMENT":"' + fn_GetCastSelectedItemValue('rdoF_ASSORTMENT') + '"';
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('txtF_DIVISIONIDX') + '"';
                data += ',"F_DIVISIONNM":"' + jsonEscape(fn_GetCastValue('txtF_DIVISIONNM')) + '"';
                data += ',"F_METHOD":"' + jsonEscape(fn_GetCastValue('txtF_METHOD')) + '"';
                data += ',"F_EQUIPMENT":"' + jsonEscape(fn_GetCastValue('txtF_EQUIPMENT')) + '"';
                data += ',"F_DIVISIONSORT":"' + fn_GetCastValue('txtF_DIVISIONSORT') + '"';
                data += ',"F_REVSTATUS":"' + fn_GetCastSelectedItemValue('rdoF_REVSTATUS') + '"';
                data += ',"F_DIVISIONREV":"' + fn_GetCastValue('txtF_DIVISIONREV') + '"';
                data += ',"F_REVSTDT":"' + formatDateString(fn_GetCastDate('txtF_REVSTDT'), '-') + '"';
                data += ',"F_STATUS":"' + fn_GetCastSelectedItemValue('rdoF_STATUS') + '"';
            } else if (t == 'B') {
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('srcF_DIVISIONIDX') + '"';
                data += ',"F_INSPIDX":"' + fn_GetCastValue('txtF_INSPIDX') + '"';
                data += ',"F_INSPNO":"' + fn_GetCastValue('txtF_INSPNO') + '"';
                data += ',"F_INSPNM":"' + jsonEscape(fn_GetCastValue('txtF_INSPNM')) + '"';
                data += ',"F_STANDARD":"' + jsonEscape(fn_GetCastValue('txtF_STANDARD')) + '"';
                data += ',"F_TERM":"' + fn_GetCastValue('txtF_TERM') + '"';
                data += ',"F_TRANSPARENT":"' + fn_GetCastSelectedItemValue('rdoF_TRANSPARENT') + '"';
                var checked = !fn_GetCastChecked('chkF_ISEXCEPT') ? 'false' : 'true';
                data += ',"F_ISEXCEPT":"' + checked + '"';
                data += ',"F_INSPREVSTATUS":"' + fn_GetCastSelectedItemValue('rdoF_INSPREVSTATUS') + '"';
                data += ',"F_INSPREV":"' + fn_GetCastValue('txtF_INSPREV') + '"';
                data += ',"F_REVSTDT":"' + formatDateString(fn_GetCastDate('txtF_INSPREVSTDT'), '-') + '"';
                data += ',"F_STATUS":"' + fn_GetCastSelectedItemValue('rdoF_STATUS') + '"';
            }
            data += '}';

            return data;
        }

        function fn_OnRowDblClick(t, s, e) {
            var rowKeys;
            
            var data = '{';
            data += '"ACTION":"G"';
            data += ',"TYPE":"' + t + '"';
            data += ',"F_TYPECD":"' + fn_GetCastValue('srcF_TYPECD') + '"';
            if (t == 'A') {
                rowKeys = devGrid1.GetRowKey(e.visibleIndex);
                data += ',"F_DIVISIONIDX":"' + rowKeys + '"';
            } else {
                rowKeys = devGrid2.GetRowKey(e.visibleIndex);
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('srcF_DIVISIONIDX') + '"';
                data += ',"F_INSPIDX":"' + rowKeys + '"';
            }
            
            data += '}';
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
        }

        function fn_OnChangeSort(t, d, c, n, i) {
            var data = '{';
            data += '"ACTION":"S"';
            data += ',"TYPE":"' + t + '"';
            data += ',"F_TYPECD":"' + fn_GetCastValue('srcF_TYPECD') + '"';

            if (t == 'A') {
                data += ',"F_DIVISIONIDX":"' + c + '"';
                data += ',"F_DIVISIONSORT":"' + n + '"';
                data += ',"F_DIRECTION":"' + d + '"';
            } else if (t == 'B') {
                data += ',"F_DIVISIONIDX":"' + fn_GetCastValue('srcF_DIVISIONIDX') + '"';
                data += ',"F_INSPIDX":"' + c + '"';
                data += ',"F_INSPNO":"' + n + '"';
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

        function fn_OnAddInspection(c, n, i) {
            fn_OnClearData('A');
            fn_SetTextValue('srcF_DIVISIONIDX', c);
            fn_SetTextValue('srcF_DIVISIONNM', n);
            fn_OnClearData('B');
            fn_OnSetControlVisibility('B', fn_GetCastValue('srcF_REPORTTP'));
            fn_OnSearchClick('devGrid2');
            devGrid1.SetFocusedRowIndex(i);
        }

        function fn_OnViewHistory(t, c, i) {
            var devGrid;
            if (t == 'A') {
                devGrid1.SetFocusedRowIndex(i);
                devGridSub1.PerformCallback(c);
            } else if (t == 'B') {
                devGrid2.SetFocusedRowIndex(i);
                devGridSub2.PerformCallback(fn_GetCastValue('srcF_DIVISIONIDX') + '|' + c);
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

        function fn_OnSetFocusRowIndex(t) {
            if (t == 'A') {
                if (n > 0 || n < devGrid1.GetVisibleRowsOnPage() - 1)
                    devGrid1.SetFocusedRowIndex(d == '+' ? ++i : --i);
            } else if (t == 'B') {
                if (n > 0 || n < devGrid2.GetVisibleRowsOnPage() - 1)
                    devGrid2.SetFocusedRowIndex(d == '+' ? ++i : --i);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
    <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
    <table class="layerTable">
        <colgroup>
            <col style="width:49%" />
            <col style="width:2%" />
            <col style="width:49%" />
        </colgroup>
        <tbody>
            <tr>
                <td>
                    <div class="container_l">
                        <div class="search_l">
                            <table class="layerTable">
                                <colgroup>
                                    <col style="width:50%" />
                                    <col style="width:50%" />
                                </colgroup>
                                <tbody>
                                    <tr>
                                        <td style="font-weight:bold;">[검사성적서 타입항목 관리]</td>
                                        <td>
                                            <ul class="nav navbar-nav navbar-right m-n">
                                                <li class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                    <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick('devGrid1'); return false;">
                                                        <i class="fa fa-search"></i>
                                                        <span class="text">조회</span>
                                                    </button>
                                                </li>
                                                <li class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                    <button class="btn btn-sm btn-primary" onclick="fn_OnNewClick('A'); return false;">
                                                        <i class="fa fa-pencil"></i>
                                                        <span class="text">입력</span>
                                                    </button>
                                                </li>
                                                <li class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                    <button class="btn btn-sm btn-warning" onclick="fn_OnCancelClick('A'); return false;">
                                                        <i class="fa fa-undo"></i>
                                                        <span class="text">취소</span>
                                                    </button>
                                                </li>
                                                <li class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                    <button class="btn btn-sm btn-success" onclick="fn_OnSaveClick('A'); return false;">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">저장</span>
                                                    </button>
                                                </li>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="divPadding"></div>
                            <dx:ASPxTextBox ID="txtF_DIVISIONIDX" ClientInstanceName="txtF_DIVISIONIDX" runat="server" ClientVisible="false" />
                            <table id="inputTableA" class="contentTable">
                                <colgroup>
                                    <col style="width:10%" />
                                    <col style="width:23%" />
                                    <col style="width:10%" />
                                    <col style="width:23%" />
                                    <col style="width:10%" />
                                    <col style="width:24%" />
                                </colgroup>
                                <tbody>
                                    <tr>
                                        <td class="tdLabel">구분</td>
                                        <td class="tdInput">
                                            <dx:ASPxTextBox ID="srcF_TYPECD" ClientInstanceName="srcF_TYPECD" runat="server" ClientVisible="false" />
                                            <dx:ASPxTextBox ID="srcF_REPORTTP" ClientInstanceName="srcF_REPORTTP" runat="server" ClientVisible="false" />
                                            <dx:ASPxTextBox ID="srcF_REPORTTPNM" ClientInstanceName="srcF_REPORTTPNM" runat="server" Width="100%">
                                                <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="tdLabel">성적서명</td>
                                        <td class="tdInput" colspan="3">
                                            <dx:ASPxTextBox ID="srcF_TYPENM" ClientInstanceName="srcF_TYPENM" runat="server" Width="100%">
                                                <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel">항목구분</td>
                                        <td class="tdInput" colspan="5">
                                            <dx:ASPxRadioButtonList ID="rdoF_ASSORTMENT" ClientInstanceName="rdoF_ASSORTMENT" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel">항목명</td>
                                        <td class="tdInput">
                                            <dx:ASPxMemo ID="txtF_DIVISIONNM" ClientInstanceName="txtF_DIVISIONNM" runat="server" Width="100%" Height="70px" />
                                        </td>
                                        <td class="tdLabel">검사방식</td>
                                        <td class="tdInput">
                                            <dx:ASPxMemo ID="txtF_METHOD" ClientInstanceName="txtF_METHOD" runat="server" Width="100%" Height="70px" />
                                        </td>
                                        <td class="tdLabel">측정기</td>
                                        <td class="tdInput">
                                            <dx:ASPxMemo ID="txtF_EQUIPMENT" ClientInstanceName="txtF_EQUIPMENT" runat="server" Width="100%" Height="70px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel">사용여부</td>
                                        <td class="tdInput">
                                            <dx:ASPxRadioButtonList ID="rdoF_STATUS" ClientInstanceName="rdoF_STATUS" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                                                <Items>
                                                    <dx:ListEditItem Text="사용" Value="1" />
                                                    <dx:ListEditItem Text="중단" Value="0" />
                                                </Items>
                                            </dx:ASPxRadioButtonList>
                                        </td>
                                        <td class="tdLabel">개정여부</td>
                                        <td class="tdInput">
                                            <dx:ASPxRadioButtonList ID="rdoF_REVSTATUS" ClientInstanceName="rdoF_REVSTATUS" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                                                <Items>
                                                    <dx:ListEditItem Text="사용" Value="1" />
                                                    <dx:ListEditItem Text="중단" Value="0" />
                                                </Items>
                                            </dx:ASPxRadioButtonList>
                                        </td>
                                        <td class="tdLabel">시작일</td>
                                        <td class="tdInput">
                                            <dx:ASPxTextBox ID="txtF_DIVISIONSORT" ClientInstanceName="txtF_DIVISIONSORT" runat="server" ClientVisible="false" />
                                            <dx:ASPxTextBox ID="txtF_DIVISIONREV" ClientInstanceName="txtF_DIVISIONREV" runat="server" ClientVisible="false" />
                                            <dx:ASPxDateEdit ID="txtF_REVSTDT" ClientInstanceName="txtF_REVSTDT" runat="server" Width="100%"
                                                 UseMaskBehavior="true" EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White">
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="divPadding"></div>
                        </div>
                        <div class="content_l">
                            <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="F_DIVISIONIDX" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGrid1_CustomCallback" OnCustomColumnDisplayText="devGrid1_CustomColumnDisplayText">
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
						                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid1" />
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
                                    <dx:GridViewDataColumn FieldName="F_STATUS" Caption="사용<br/>여부" Width="40px" />
                                    <dx:GridViewDataTextColumn Caption="개정<br/>이력" UnboundType="String" Width="40px">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="btnHistoryA" runat="server" Text="이력" OnInit="btnHistoryA_Init" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
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
                        </div>
                        <div class="paging_l">
                            <div class="divPadding"></div>
                            <table class="layerTable">
                                <tbody>
                                    <tr>
                                        <td style="font-weight:bold;">[개정정보]</td>
                                    </tr>
                                </tbody>
                            </table>
                            <dx:ASPxGridView ID="devGridSub1" ClientInstanceName="devGridSub1" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="F_DIVISIONIDX;F_DIVISIONREV" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGridSub1_CustomCallback">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				                <Templates>
					                <StatusBar>
						                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridSub1" />
					                </StatusBar>
				                </Templates>
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="F_DIVISIONNM" Caption="항목명">
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_DIVISIONREV" Caption="개정번호" Width="70px" />
                                    <dx:GridViewDataColumn FieldName="F_REVSTDT" Caption="시작일" Width="80px" />
                                    <dx:GridViewDataColumn FieldName="F_REVEDDT" Caption="종료일" Width="80px" />
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </td>
                <td></td>
                <td>
                    <div class="container_r">
                        <div class="search_r">
                            <table class="layerTable">
                                <colgroup>
                                    <col style="width:50%" />
                                    <col style="width:50%" />
                                </colgroup>
                                <tbody>
                                    <tr>
                                        <td style="font-weight:bold;">[검사성적서 타입항목별 기준 관리]</td>
                                        <td>
                                            <ul class="nav navbar-nav navbar-right m-n">
                                                <li class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                    <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick('devGrid2'); return false;">
                                                        <i class="fa fa-search"></i>
                                                        <span class="text">조회</span>
                                                    </button>
                                                </li>
                                                <li class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                    <button class="btn btn-sm btn-primary" onclick="fn_OnNewClick('B'); return false;">
                                                        <i class="fa fa-pencil"></i>
                                                        <span class="text">입력</span>
                                                    </button>
                                                </li>
                                                <li class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                    <button class="btn btn-sm btn-warning" onclick="fn_OnCancelClick('B'); return false;">
                                                        <i class="fa fa-undo"></i>
                                                        <span class="text">취소</span>
                                                    </button>
                                                </li>
                                                <li class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                    <button class="btn btn-sm btn-success" onclick="fn_OnSaveClick('B'); return false;">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">저장</span>
                                                    </button>
                                                </li>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="divPadding"></div>
                            <dx:ASPxTextBox ID="txtF_INSPIDX" ClientInstanceName="txtF_INSPIDX" runat="server" ClientVisible="false" />
                            <table id="inputTableB" class="contentTable">
                                <colgroup>
                                    <col style="width:10%" />
                                    <col style="width:56%" />
                                    <col style="width:10%" />
                                    <col style="width:24%" />
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
                                        <td class="tdLabel">사용여부</td>
                                        <td class="tdInput">
                                            <dx:ASPxRadioButtonList ID="rdoF_INSPSTATUS" ClientInstanceName="rdoF_INSPSTATUS" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                                                <Items>
                                                    <dx:ListEditItem Text="사용" Value="1" />
                                                    <dx:ListEditItem Text="중단" Value="0" />
                                                </Items>
                                            </dx:ASPxRadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel" rowspan="3">판정기준</td>
                                        <td class="tdInput" rowspan="3">
                                            <dx:ASPxMemo ID="txtF_STANDARD" ClientInstanceName="txtF_STANDARD" runat="server" Width="100%" Height="85px" />
                                        </td>
                                        <td class="tdLabel">개정여부</td>
                                        <td class="tdInput">
                                            <dx:ASPxRadioButtonList ID="rdoF_INSPREVSTATUS" ClientInstanceName="rdoF_INSPREVSTATUS" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                                                <Items>
                                                    <dx:ListEditItem Text="사용" Value="1" />
                                                    <dx:ListEditItem Text="중단" Value="0" />
                                                </Items>
                                            </dx:ASPxRadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabel">시작일</td>
                                        <td class="tdInput">
                                            <dx:ASPxTextBox ID="txtF_INSPNO" ClientInstanceName="txtF_INSPNO" runat="server" ClientVisible="false" />
                                            <dx:ASPxTextBox ID="txtF_INSPREV" ClientInstanceName="txtF_INSPREV" runat="server" ClientVisible="false" />
                                            <dx:ASPxDateEdit ID="txtF_INSPREVSTDT" ClientInstanceName="txtF_INSPREVSTDT" runat="server" Width="100%"
                                                 UseMaskBehavior="true" EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White">
                                            </dx:ASPxDateEdit>
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
                            <div class="divPadding"></div>
                        </div>
                        <div class="content_r">
                            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="F_INSPIDX" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGrid2_CustomCallback" OnCustomColumnDisplayText="devGrid2_CustomColumnDisplayText">
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
						                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
					                </StatusBar>
				                </Templates>
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명">
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_INSPNO" Caption="표시<br/>순번" Width="40px">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_STATUS" Caption="사용<br/>여부" Width="40px" />
                                    <dx:GridViewDataTextColumn Caption="개정<br/>이력" UnboundType="String" Width="40px">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="btnHistoryB" runat="server" Text="이력" OnInit="btnHistoryB_Init" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="순서<br/>변경" UnboundType="String" Width="50px">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="btnSortDownB" runat="server" Text="△" OnInit="btnSortDownB_Init" />
                                            <dx:ASPxHyperLink ID="btnSortUpB" runat="server" Text="▽" OnInit="btnSortUpB_Init" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                        <div class="paging_r">
                            <div class="divPadding"></div>
                            <table class="layerTable">
                                <tbody>
                                    <tr>
                                        <td style="font-weight:bold;">[개정정보]</td>
                                    </tr>
                                </tbody>
                            </table>
                            <dx:ASPxGridView ID="devGridSub2" ClientInstanceName="devGridSub2" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="F_DIVISIONIDX;F_DIVISIONREV" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGridSub2_CustomCallback">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				                <Templates>
					                <StatusBar>
						                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridSub2" />
					                </StatusBar>
				                </Templates>
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명">
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_INSPREV" Caption="개정번호" Width="70px" />
                                    <dx:GridViewDataColumn FieldName="F_REVSTDT" Caption="시작일" Width="80px" />
                                    <dx:GridViewDataColumn FieldName="F_REVEDDT" Caption="종료일" Width="80px" />
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>