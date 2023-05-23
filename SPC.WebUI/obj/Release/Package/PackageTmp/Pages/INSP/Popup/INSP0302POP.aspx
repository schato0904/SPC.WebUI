<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0302POP.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.Popup.INSP0302POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid1_efnew_';

        $(document).ready(function () {
            fn_OnSearchClick();
            fn_RendorTotalCount();
            fn_OnSetControlVisibility('');
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(window).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid1.SetHeight(height);
            devGrid2.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            var data = '{';
            data += '"ACTION":"GET_NM"';
            data += ',"F_ITEMCD":"' + fn_GetCastValue('hidITEMCD') + '"';
            data += ',"F_WORKCD":"' + fn_GetCastValue('hidWORKCD') + '"';
            data += '}';
            data = $.parseJSON(data);
            fn_devCallback_PerformCallback(data);
            devGrid1.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            var data = '';
            if (!confirm('저장하시겠습니까?\r계속 진행하려면 확인을 누르세요')) return false;
            data = fn_OnCreateJsonData();
            data = $.parseJSON(data);
            //console.log(data);
            fn_devCallback_PerformCallback(data);
        }

        // 취소
        function fn_OnCancelClick() {

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
        function fn_OnValidate(s, e) {
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
                        alert('검사성적서가 생성되었습니다\r이 창을 닫습니다');
                        fn_Close();
                        break;
                    case "GET_NM":
                        fn_SetTextValue('srcF_ITEMNM', GetJsonValueByKey(data, 'F_ITEMNM', ''));
                        fn_SetTextValue('srcF_WORKNM', GetJsonValueByKey(data, 'F_WORKNM', ''));
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

        function fn_OnSelectedIndexChanged(s, e) {
            fn_OnSetControlVisibility(s.GetValue());
        }

        function fn_OnSetControlVisibility(val) {
            fn_OnControlDisableBox(srcF_KSINFO, null);
            fn_OnControlDisableBox(srcF_TITLE, null);
            fn_OnControlDisableBox(srcF_WRITER, null);
            fn_OnControlDisableBox(srcF_REVIEWER, null);
            fn_OnControlDisableBox(srcF_APPROVER, null);
            fn_OnControlDisableBox(srcF_LOTCNT, null);
            fn_OnControlDisableBox(srcF_UNIT, null);
            fn_OnControlEnableComboBox(srcF_JUDGE, false);
            fn_OnControlDisableBox(srcF_MATERIAL, null);
            fn_OnControlDisableBox(srcF_INCNT, null);
            fn_OnControlDisableBox(srcF_INUNIT, null);
            fn_OnControlDisableBox(srcF_INDATE, null);
            fn_OnControlDisableBox(srcF_INITEMTITLE, null);
            fn_OnControlDisableBox(srcF_INCUST, null);
            fn_OnControlDisableBox(srcF_INBASENO, null);
            fn_OnControlDisableBox(srcF_ESTDATE, null);
            fn_OnControlDisableBox(srcF_INJCUST, null);
            fn_OnControlDisableBox(srcF_INJDATE, null);
            fn_OnControlDisableBox(srcF_THERMICOUT, null);
            fn_OnControlDisableBox(srcF_THERMICUNIT, null);
            fn_OnControlDisableBox(srcF_COVEROUT, null);
            fn_OnControlDisableBox(srcF_COVERUNIT, null);
            fn_OnControlDisableBox(srcF_INTYPE, null);
            fn_OnControlDisableBox(srcF_INMATERIAL, null);
            fn_OnControlDisableBox(srcF_DRAWNO, null);
            fn_OnControlDisableBox(srcF_INITEMNM, null);
            fn_OnControlDisableBox(srcF_INTEMP, null);
            fn_OnControlDisableBox(srcF_CYCEL, null);
            fn_OnControlDisableBox(srcF_CYCELUNIT, null);
            fn_OnControlDisableBox(srcF_MACHINE, null);
            fn_OnControlDisableBox(srcF_DISTRIBUTE, null);
            fn_OnControlDisableBox(srcF_THERMICCNT, null);
            fn_OnControlDisableBox(srcF_COATING, null);
            fn_OnControlDisableBox(srcF_COVERING, null);
            fn_OnControlDisableBox(srcF_RAWLOTNO, null);
            fn_OnControlDisableBox(srcF_INJMACHINE, null);
            fn_OnControlDisableBox(srcF_WIRELOTNO, null);
            fn_OnControlDisableBox(srcF_MAKEDATE, null);
            fn_OnControlEnableComboBox(srcF_DAYNIGHT, false);

            switch (val) {
                case 'AAI101':  // 제품검사(가스용A)
                case 'AAI102':  // 제품검사(가스용B)
                case 'AAI103':  // 제품검사(수도용A)
                case 'AAI104':  // 제품검사(수도용B)
                    fn_OnControlEnableBox(srcF_WRITER, null);
                    fn_OnControlEnableBox(srcF_REVIEWER, null);
                    fn_OnControlEnableBox(srcF_APPROVER, null);
                    fn_OnControlEnableBox(srcF_KSINFO, null);
                    fn_OnControlEnableBox(srcF_TITLE, null);
                    fn_OnControlEnableBox(srcF_LOTCNT, null);
                    fn_OnControlEnableBox(srcF_UNIT, null);
                    fn_OnControlEnableBox(srcF_INITEMNM, null);
                    fn_OnControlEnableComboBox(srcF_JUDGE, true);
                    break;
                case 'AAI111':  // 인수검사(원재료-가스)
                case 'AAI112':  // 인수검사(원재료-수도)
                case 'AAI113':  // 인수검사(고무)
                    fn_OnControlEnableBox(srcF_WRITER, null);
                    fn_OnControlEnableBox(srcF_REVIEWER, null);
                    fn_OnControlEnableBox(srcF_APPROVER, null);
                    fn_OnControlEnableBox(srcF_MATERIAL, null);
                    fn_OnControlEnableBox(srcF_INCNT, null);
                    fn_OnControlEnableBox(srcF_INUNIT, null);
                    fn_OnControlEnableBox(srcF_INDATE, null);
                    fn_OnControlEnableBox(srcF_INMATERIAL, null);
                    fn_OnControlEnableComboBox(srcF_JUDGE, true);
                    break;
                case 'AAI114':  // 인수검사(부품)
                    fn_OnControlEnableBox(srcF_WRITER, null);
                    fn_OnControlEnableBox(srcF_REVIEWER, null);
                    fn_OnControlEnableBox(srcF_APPROVER, null);
                    fn_OnControlEnableBox(srcF_MATERIAL, null);
                    fn_OnControlEnableComboBox(srcF_JUDGE, true);
                    fn_OnControlEnableBox(srcF_INCNT, null);
                    fn_OnControlEnableBox(srcF_INUNIT, null);
                    fn_OnControlEnableBox(srcF_INDATE, null);
                    fn_OnControlEnableBox(srcF_INITEMTITLE, null);
                    fn_OnControlEnableBox(srcF_INBASENO, null);
                    fn_OnControlEnableBox(srcF_ESTDATE, null);
                    fn_OnControlEnableBox(srcF_INTYPE, null);
                    fn_OnControlEnableBox(srcF_INMATERIAL, null);
                    fn_OnControlEnableBox(srcF_DRAWNO, null);
                    fn_OnControlEnableBox(srcF_INITEMNM, null);
                    fn_OnControlEnableBox(srcF_INTEMP, null);
                    fn_OnControlEnableBox(srcF_CYCEL, null);
                    fn_OnControlEnableBox(srcF_CYCELUNIT, null);
                    fn_OnControlEnableBox(srcF_MACHINE, null);
                    fn_OnControlEnableBox(srcF_DISTRIBUTE, null);
                    fn_OnControlEnableBox(srcF_INJCUST, null);
                    fn_OnControlEnableBox(srcF_INJDATE, null);
                    fn_OnControlEnableComboBox(srcF_JUDGE, true);
                    break;
                case 'AAI115':  // 인수검사(열선)
                    fn_OnControlEnableBox(srcF_WRITER, null);
                    fn_OnControlEnableBox(srcF_REVIEWER, null);
                    fn_OnControlEnableBox(srcF_APPROVER, null);
                    fn_OnControlEnableBox(srcF_INDATE, null);
                    fn_OnControlEnableBox(srcF_INITEMTITLE, null);
                    fn_OnControlEnableBox(srcF_INBASENO, null);
                    fn_OnControlEnableBox(srcF_ESTDATE, null);
                    fn_OnControlEnableBox(srcF_INITEMNM, null);
                    fn_OnControlEnableBox(srcF_INTYPE, null);
                    fn_OnControlEnableBox(srcF_DRAWNO, null);
                    fn_OnControlEnableBox(srcF_THERMICOUT, null);
                    fn_OnControlEnableBox(srcF_THERMICUNIT, null);
                    fn_OnControlEnableBox(srcF_COVEROUT, null);
                    fn_OnControlEnableBox(srcF_COVERUNIT, null);
                    fn_OnControlEnableBox(srcF_THERMICCNT, null);
                    fn_OnControlEnableBox(srcF_COATING, null);
                    fn_OnControlEnableBox(srcF_COVERING, null);
                    fn_OnControlEnableComboBox(srcF_JUDGE, true);
                    break;
                case 'AAI121':  // 중간검사(스피곳가스)
                case 'AAI122':  // 중간검사(스피곳수도)
                    fn_OnControlEnableBox(srcF_WRITER, null);
                    fn_OnControlEnableBox(srcF_REVIEWER, null);
                    fn_OnControlEnableBox(srcF_APPROVER, null);
                    fn_OnControlEnableBox(srcF_LOTCNT, null);
                    fn_OnControlEnableBox(srcF_UNIT, null);
                    fn_OnControlEnableBox(srcF_MATERIAL, null);
                    fn_OnControlEnableBox(srcF_INITEMNM, null);
                    fn_OnControlEnableBox(srcF_INTYPE, null);
                    fn_OnControlEnableBox(srcF_DRAWNO, null);
                    fn_OnControlEnableBox(srcF_RAWLOTNO, null);
                    fn_OnControlEnableBox(srcF_INJMACHINE, null);
                    fn_OnControlEnableBox(srcF_INJDATE, null);
                    fn_OnControlEnableComboBox(srcF_JUDGE, true);
                    break;
                case 'AAI123':  // 중간검사(EF)
                    fn_OnControlEnableBox(srcF_INITEMNM, null);
                    fn_OnControlEnableBox(srcF_INTYPE, null);
                    fn_OnControlEnableBox(srcF_INJMACHINE, null);
                    fn_OnControlEnableBox(srcF_WIRELOTNO, null);
                    fn_OnControlEnableBox(srcF_MAKEDATE, null);
                    fn_OnControlEnableBox(srcF_RAWLOTNO, null);
                    fn_OnControlEnableComboBox(srcF_DAYNIGHT, true);
                    fn_OnControlEnableComboBox(srcF_JUDGE, true);
                    break;
            }

            fn_SetTextValue('hidSAMPLEDATA', '');
            devGrid2.PerformCallback('');

            var w = $('#inputTable').width();

            if (fn_GetCastSelectedItemValue('srcF_REPORTTP') == 'AAI123') {
                //console.log(w);
                $('#tdHeadL').css('width', (w - 5) / 2 + 'px');
                $('#tdHeadS').css('width', '5px');
                $('#tdHeadR').css('width', (w - 5) / 2 + 'px');
                $('#tdContentL').css('width', (w - 5) / 2 + 'px');
                $('#tdContentS').css('width', '5px');
                $('#tdContentR').css('width', (w - 5) / 2 + 'px');
                devGrid1.SetWidth((w - 5) / 2);
                $('#tdHeadS').show();
                $('#tdHeadR').show();
                $('#tdContentS').show();
                $('#tdContentR').show();
                $('#btnLink').attr('disabled', false);
            } else {
                devGrid1.SetWidth(w);
                $('#tdHeadS').hide();
                $('#tdHeadR').hide();
                $('#tdContentS').hide();
                $('#tdContentR').hide();
                $('#btnLink').attr('disabled', true);
            }
        }

        function fn_OnCreateJsonData() {
            var jsonValue = '';
            var data = '{';
            data += '"ACTION":"NEW"';
            data += ',"F_QWK13IMID":"0"';
            data += ',"F_REPORTTP":"' + fn_GetCastSelectedItemValue('srcF_REPORTTP') + '"';
            data += ',"F_WORKDATE":"' + fn_GetCastValue('srcF_WORKDATE') + '"';
            data += ',"F_ITEMCD":"' + fn_GetCastValue('srcF_ITEMCD') + '"';
            data += ',"F_WORKCD":"' + fn_GetCastValue('srcF_WORKCD') + '"';
            data += ',"F_LOTNO":"' + fn_GetCastValue('srcF_LOTNO') + '"';
            data += ',"F_MOLDNO":"' + fn_GetCastValue('srcF_MOLDNO') + '"';
            data += ',"F_WORKMAN":"' + fn_GetCastValue('srcF_WORKMAN') + '"';
            data += ',"F_KSINFO":"' + fn_GetCastValue('srcF_KSINFO') + '"';
            jsonValue = fn_GetCastValue('srcF_TITLE');
            jsonValue = jsonValue == '' ? jsonValue : jsonValue.replace(/"/g, '\\"').replace(/\n/g, "\\n");
            data += ',"F_TITLE":"' + jsonValue + '"';
            data += ',"F_WRITER":"' + fn_GetCastValue('srcF_WRITER') + '"';
            data += ',"F_REVIEWER":"' + fn_GetCastValue('srcF_REVIEWER') + '"';
            data += ',"F_APPROVER":"' + fn_GetCastValue('srcF_APPROVER') + '"';
            data += ',"F_LOTCNT":"' + fn_GetCastValue('srcF_LOTCNT') + '"';
            data += ',"F_UNIT":"' + fn_GetCastValue('srcF_UNIT') + '"';
            data += ',"F_JUDGE":"' + fn_GetCastSelectedItemValue('srcF_JUDGE') + '"';
            data += ',"F_MATERIAL":"' + fn_GetCastValue('srcF_MATERIAL') + '"';
            data += ',"F_INCNT":"' + fn_GetCastValue('srcF_INCNT') + '"';
            data += ',"F_INUNIT":"' + fn_GetCastValue('srcF_INUNIT') + '"';
            data += ',"F_INDATE":"' + fn_GetCastValue('srcF_INDATE') + '"';
            data += ',"F_INITEMTITLE":"' + fn_GetCastValue('srcF_INITEMTITLE') + '"';
            data += ',"F_INCUST":"' + fn_GetCastValue('srcF_INCUST') + '"';
            data += ',"F_INBASENO":"' + fn_GetCastValue('srcF_INBASENO') + '"';
            data += ',"F_ESTDATE":"' + fn_GetCastValue('srcF_ESTDATE') + '"';
            data += ',"F_INJCUST":"' + fn_GetCastValue('srcF_INJCUST') + '"';
            data += ',"F_INJDATE":"' + fn_GetCastValue('srcF_INJDATE') + '"';
            data += ',"F_THERMICOUT":"' + fn_GetCastValue('srcF_THERMICOUT') + '"';
            data += ',"F_THERMICUNIT":"' + fn_GetCastValue('srcF_THERMICUNIT') + '"';
            data += ',"F_COVEROUT":"' + fn_GetCastValue('srcF_COVEROUT') + '"';
            data += ',"F_COVERUNIT":"' + fn_GetCastValue('srcF_COVERUNIT') + '"';
            data += ',"F_INTYPE":"' + fn_GetCastValue('srcF_INTYPE') + '"';
            data += ',"F_INMATERIAL":"' + fn_GetCastValue('srcF_INMATERIAL') + '"';
            data += ',"F_DRAWNO":"' + fn_GetCastValue('srcF_DRAWNO') + '"';
            data += ',"F_INITEMNM":"' + fn_GetCastValue('srcF_INITEMNM') + '"';
            data += ',"F_INTEMP":"' + fn_GetCastValue('srcF_INTEMP') + '"';
            data += ',"F_CYCEL":"' + fn_GetCastValue('srcF_CYCEL') + '"';
            data += ',"F_CYCELUNIT":"' + fn_GetCastValue('srcF_CYCELUNIT') + '"';
            data += ',"F_MACHINE":"' + fn_GetCastValue('srcF_MACHINE') + '"';
            data += ',"F_DISTRIBUTE":"' + fn_GetCastValue('srcF_DISTRIBUTE') + '"';
            data += ',"F_THERMICCNT":"' + fn_GetCastValue('srcF_THERMICCNT') + '"';
            data += ',"F_COATING":"' + fn_GetCastValue('srcF_COATING') + '"';
            data += ',"F_COVERING":"' + fn_GetCastValue('srcF_COVERING') + '"';
            data += ',"F_RAWLOTNO":"' + fn_GetCastValue('srcF_RAWLOTNO') + '"';
            data += ',"F_INJMACHINE":"' + fn_GetCastValue('srcF_INJMACHINE') + '"';
            data += ',"F_WIRELOTNO":"' + fn_GetCastValue('srcF_WIRELOTNO') + '"';
            data += ',"F_MAKEDATE":"' + fn_GetCastValue('srcF_MAKEDATE') + '"';
            data += ',"F_DAYNIGHT":"' + fn_GetCastSelectedItemValue('srcF_DAYNIGHT') + '"';
            data += ',"F_CONFIRM":"0"';
            data += ',"F_TSERIALNO":"' + fn_GetCastValue('hidTSERIALNO') + '"';
            if (fn_GetCastSelectedItemValue('srcF_REPORTTP') == 'AAI123')
                data += ',"F_EFITEMS":' + fn_GetCastValue('hidSAMPLEDATA');
            else
                data += ',"F_EFITEMS":""';
            data += '}';

            return data;
        }

        function fn_OnLinkClick() {
            var data = '{';
            data += '"F_ITEMCD":"' + fn_GetCastValue('srcF_ITEMCD') + '"';
            data += ',"F_WORKCD":"' + fn_GetCastValue('srcF_WORKCD') + '"';
            data += ',"F_MOLDNO":"' + fn_GetCastValue('srcF_MOLDNO') + '"';
            data += '}';
            fn_OnPopupINSP0302SUB(data);
        }

        function fn_OnSetSampleData(data) {
            fn_SetTextValue('hidSAMPLEDATA', data);
            devGrid2.PerformCallback(data);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidITEMCD" ClientInstanceName="hidITEMCD" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidWORKCD" ClientInstanceName="hidWORKCD" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidWORKDATE" ClientInstanceName="hidWORKDATE" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidTSERIALNO" ClientInstanceName="hidTSERIALNO" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidSAMPLEDATA" ClientInstanceName="hidSAMPLEDATA" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
    <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
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
                    <col style="width:100px;" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">품번/품명</td>
                    <td class="tdInput" colspan="5">
                        <table class="layerTable">
                            <colgroup>
                                <col style="width:200px;" />
                                <col style="width:5px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" runat="server" Width="100%" HorizontalAlign="Center">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td></td>
                                <td>
                                    <dx:ASPxTextBox ID="srcF_ITEMNM" ClientInstanceName="srcF_ITEMNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                    <td class="tdLabel">공정코드/명</td>
                    <td class="tdInput" colspan="3">
                        <table class="layerTable">
                            <colgroup>
                                <col style="width:100px;" />
                                <col style="width:5px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="srcF_WORKCD" ClientInstanceName="srcF_WORKCD" runat="server" Width="100%" HorizontalAlign="Center">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td></td>
                                <td>
                                    <dx:ASPxTextBox ID="srcF_WORKNM" ClientInstanceName="srcF_WORKNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">성적서구분</td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="srcF_REPORTTP" ClientInstanceName="srcF_REPORTTP" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" NullText="선택하세요">
                            <ClientSideEvents SelectedIndexChanged="fn_OnSelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdLabel">검사일</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_WORKDATE" ClientInstanceName="srcF_WORKDATE" runat="server" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">Lot No.</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_LOTNO" ClientInstanceName="srcF_LOTNO" runat="server" Width="100%" />
                    </td>
                    <td class="tdLabel">작지번호</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MOLDNO" ClientInstanceName="srcF_MOLDNO" runat="server" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">검사원</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_WORKMAN" ClientInstanceName="srcF_WORKMAN" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">KS 명칭</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_KSINFO" ClientInstanceName="srcF_KSINFO" runat="server" Width="100%" /></td>
                    <td class="tdLabel" rowspan="2">종류 및 호칭</td>
                    <td class="tdInput" rowspan="2"><dx:ASPxMemo ID="srcF_TITLE" ClientInstanceName="srcF_TITLE" runat="server" Width="100%" Height="56px" /></td>
                    <td class="tdLabel">작성자</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_WRITER" ClientInstanceName="srcF_WRITER" runat="server" Width="100%" /></td>
                    <td class="tdLabel">검토자</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_REVIEWER" ClientInstanceName="srcF_REVIEWER" runat="server" Width="100%" /></td>
                    <td class="tdLabel">승인자</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_APPROVER" ClientInstanceName="srcF_APPROVER" runat="server" Width="100%" /></td>
                </tr>
                <tr>
                    <td class="tdLabel">로트크기/단위</td>
                    <td class="tdInput">
                        <table class="layerTable">
                            <colgroup>
                                <col />
                                <col style="width:5px;" />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxSpinEdit ID="srcF_LOTCNT" ClientInstanceName="srcF_LOTCNT" runat="server" Width="100%" HorizontalAlign="Right"
                                        NumberType="Integer" MinValue="0" MaxValue="99999999" SpinButtons-ShowIncrementButtons="false" DisplayFormatString="n0" />
                                </td>
                                <td></td>
                                <td><dx:ASPxTextBox ID="srcF_UNIT" ClientInstanceName="srcF_UNIT" runat="server" Width="100%" Text="EA" /></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdLabel">종합판정</td>
                    <td class="tdInput">
                        <dx:ASPxRadioButtonList ID="srcF_JUDGE" ClientInstanceName="srcF_JUDGE" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                            <Items>
                                <dx:ListEditItem Text="합격" Value="1" />
                                <dx:ListEditItem Text="불합격" Value="0" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                    <td class="tdLabel">인수검사품명</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INITEMTITLE" ClientInstanceName="srcF_INITEMTITLE" runat="server" Width="100%" /></td>
                    <td class="tdLabel">납품처</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INCUST" ClientInstanceName="srcF_INCUST" runat="server" Width="100%" /></td>
                </tr>
                <tr>
                    <td class="tdLabel">원료명</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_MATERIAL" ClientInstanceName="srcF_MATERIAL" runat="server" Width="100%" /></td>
                    <td class="tdLabel">입고수량/단위</td>
                    <td class="tdInput">
                        <table class="layerTable">
                            <colgroup>
                                <col />
                                <col style="width:5px;" />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxSpinEdit ID="srcF_INCNT" ClientInstanceName="srcF_INCNT" runat="server" Width="100%" HorizontalAlign="Right"
                                        NumberType="Integer" MinValue="0" MaxValue="99999999" SpinButtons-ShowIncrementButtons="false" DisplayFormatString="n0" />
                                </td>
                                <td></td>
                                <td><dx:ASPxTextBox ID="srcF_INUNIT" ClientInstanceName="srcF_INUNIT" runat="server" Width="100%" Text="EA" /></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdLabel">입고일</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INDATE" ClientInstanceName="srcF_INDATE" runat="server" Width="100%" MaskSettings-Mask="yyyy-MM-dd" ValidationSettings-Display="None" /></td>
                    <td class="tdLabel">형식</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INTYPE" ClientInstanceName="srcF_INTYPE" runat="server" Width="100%" /></td>
                    <td class="tdLabel">재질</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INMATERIAL" ClientInstanceName="srcF_INMATERIAL" runat="server" Width="100%" /></td>
                </tr>
                <tr>
                    <td class="tdLabel">기준서 NO</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INBASENO" ClientInstanceName="srcF_INBASENO" runat="server" Width="100%" /></td>
                    <td class="tdLabel">제정일자</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_ESTDATE" ClientInstanceName="srcF_ESTDATE" runat="server" Width="100%" MaskSettings-Mask="yyyy-MM-dd" ValidationSettings-Display="None" /></td>
                    <td class="tdLabel">사출업체</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INJCUST" ClientInstanceName="srcF_INJCUST" runat="server" Width="100%" /></td>
                    <td class="tdLabel">사출일자</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INJDATE" ClientInstanceName="srcF_INJDATE" runat="server" Width="100%" MaskSettings-Mask="yyyy-MM-dd" ValidationSettings-Display="None" /></td>
                    <td class="tdLabel">도번</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_DRAWNO" ClientInstanceName="srcF_DRAWNO" runat="server" Width="100%" /></td>
                </tr>
                <tr>
                    <td class="tdLabel">제품명</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INITEMNM" ClientInstanceName="srcF_INITEMNM" runat="server" Width="100%" /></td>
                    <td class="tdLabel">사출온도</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INTEMP" ClientInstanceName="srcF_INTEMP" runat="server" Width="100%" /></td>
                    <td class="tdLabel">사이클시간</td>
                    <td class="tdInput">
                        <table class="layerTable">
                            <colgroup>
                                <col />
                                <col style="width:5px;" />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxSpinEdit ID="srcF_CYCEL" ClientInstanceName="srcF_CYCEL" runat="server" Width="100%" HorizontalAlign="Right"
                                        NumberType="Float" MinValue="0" MaxValue="99999999" SpinButtons-ShowIncrementButtons="false" />
                                </td>
                                <td></td>
                                <td><dx:ASPxTextBox ID="srcF_CYCELUNIT" ClientInstanceName="srcF_CYCELUNIT" runat="server" Width="100%" Text="SEC" /></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdLabel">사출설비명</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_MACHINE" ClientInstanceName="srcF_MACHINE" runat="server" Width="100%" /></td>
                    <td class="tdLabel">업체배포</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_DISTRIBUTE" ClientInstanceName="srcF_DISTRIBUTE" runat="server" Width="100%" /></td>
                </tr>
                <tr>
                    <td class="tdLabel">열선외경</td>
                    <td class="tdInput">
                        <table class="layerTable">
                            <colgroup>
                                <col />
                                <col style="width:5px;" />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxSpinEdit ID="srcF_THERMICOUT" ClientInstanceName="srcF_THERMICOUT" runat="server" Width="100%" HorizontalAlign="Right"
                                        NumberType="Float" MinValue="0" MaxValue="99999999" SpinButtons-ShowIncrementButtons="false" />
                                </td>
                                <td></td>
                                <td><dx:ASPxTextBox ID="srcF_THERMICUNIT" ClientInstanceName="srcF_THERMICUNIT" runat="server" Width="100%" Text="Φ" /></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdLabel">피복외경</td>
                    <td class="tdInput">
                        <table class="layerTable">
                            <colgroup>
                                <col />
                                <col style="width:5px;" />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxSpinEdit ID="srcF_COVEROUT" ClientInstanceName="srcF_COVEROUT" runat="server" Width="100%" HorizontalAlign="Right"
                                        NumberType="Float" MinValue="0" MaxValue="99999999" SpinButtons-ShowIncrementButtons="false" />
                                </td>
                                <td></td>
                                <td><dx:ASPxTextBox ID="srcF_COVERUNIT" ClientInstanceName="srcF_COVERUNIT" runat="server" Width="100%" Text="Φ" /></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdLabel">열선수량</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_THERMICCNT" ClientInstanceName="srcF_THERMICCNT" runat="server" Width="100%" /></td>
                    <td class="tdLabel">코팅원재료</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_COATING" ClientInstanceName="srcF_COATING" runat="server" Width="100%" /></td>
                    <td class="tdLabel">피복원재료</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_COVERING" ClientInstanceName="srcF_COVERING" runat="server" Width="100%" /></td>
                </tr>
                <tr>
                    <td class="tdLabel">원재료LOT</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_RAWLOTNO" ClientInstanceName="srcF_RAWLOTNO" runat="server" Width="100%" /></td>
                    <td class="tdLabel">사출기</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_INJMACHINE" ClientInstanceName="srcF_INJMACHINE" runat="server" Width="100%" /></td>
                    <td class="tdLabel">전열선LOT</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_WIRELOTNO" ClientInstanceName="srcF_WIRELOTNO" runat="server" Width="100%" /></td>
                    <td class="tdLabel">생산일</td>
                    <td class="tdInput"><dx:ASPxTextBox ID="srcF_MAKEDATE" ClientInstanceName="srcF_MAKEDATE" runat="server" Width="100%" MaskSettings-Mask="yyyy-MM-dd" ValidationSettings-Display="None" /></td>
                    <td class="tdLabel">주/야간</td>
                    <td class="tdInput">
                        <dx:ASPxRadioButtonList ID="srcF_DAYNIGHT" ClientInstanceName="srcF_DAYNIGHT" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                            <Items>
                                <dx:ListEditItem Text="주간" Value="D" />
                                <dx:ListEditItem Text="야간" Value="N" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
            <table class="layerTable">
                <tr>
                    <td id="tdHeadL" style="width:50%;"><div class="blockTitle"><span>[선택한 측정정보 목록]</span></div></td>
                    <td id="tdHeadS" style="width:5px;">&nbsp;</td>
                    <td id="tdHeadR" style="width:50%;">
                        <table class="layerTable">
                            <colgroup>
                                <col />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td><div class="blockTitle"><span>[선택한 중간검사(EF) 샘플검사 목록]</span></div></td>
                                <td class="text-right" style="padding-bottom:3px;">
                                    <button id="btnLink" class="btn btn-xs btn-success" onclick="fn_OnLinkClick(); return false;">
                                        <i class="fa fa-save"></i>
                                        <span class="text">샘플검사연동</span>
                                    </button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <table class="layerTable">
                <tr>
                    <td id="tdContentL" style="width:50%;">
                        <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_SERIALNO;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid1_CustomCallback" OnCustomColumnDisplayText="devGrid1_CustomColumnDisplayText">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				            <Templates>
					            <StatusBar>
						            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid1" />
					            </StatusBar>
				            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_TSERIALNO"    Caption="측정군"  Width="70px"  />
                                <dx:GridViewDataColumn FieldName="F_WORKTIME"    Caption="검사시간"  Width="70px"  />
                                <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="항목코드"  Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="항목명" Width="150px" >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번"  Width="50px"  />
                                <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격"  Width="80px" >
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한"  Width="80px" >
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한"  Width="80px" >
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="80px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_NGOKCHK" Caption="판정" Width="80px" />
                            </Columns>
                        </dx:ASPxGridView>
                    </td>
                    <td id="tdContentS" style="width:5px;">&nbsp;</td>
                    <td id="tdContentR" style="width:50%;">
                        <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_WORKDATE;F_TSERIALNO;F_SERIALNO;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid2_CustomCallback" OnCustomColumnDisplayText="devGrid2_CustomColumnDisplayText">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				            <Templates>
					            <StatusBar>
						            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
					            </StatusBar>
				            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="200px">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                                <dx:GridViewDataColumn FieldName="F_TSERIALNO"    Caption="측정군"  Width="70px"  />
                                <dx:GridViewDataColumn FieldName="F_WORKTIME"    Caption="검사시간"  Width="70px"  />
                                <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="항목코드"  Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="항목명" Width="150px" >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번"  Width="50px"  />
                                <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격"  Width="80px" >
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한"  Width="80px" >
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한"  Width="80px" >
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="80px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_NGOKCHK" Caption="판정" Width="80px" />
                            </Columns>
                        </dx:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging"></div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>