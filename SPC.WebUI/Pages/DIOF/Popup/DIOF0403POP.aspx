<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0403POP.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.Popup.DIOF0403POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();

            var data = {
                'F_REMEDYIDX': fn_GetCastValue('srcF_REMEDYIDX'),
                'ACTION': 'GET'
            };
            fn_devCallback_PerformCallback(data);

            fn_OnControlEnableComboBox(srcF_STATUS, false);
            fn_OnControlEnableComboBox(srcF_NGTYPE, false);
            fn_OnControlEnableComboBox(srcF_RESPTYPE, false);
            fn_OnControlEnableDateEdit(srcF_RESPDT, false);
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
        }

        // 조회
        function fn_OnSearchClick() {

        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {

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
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // 입력폼 조회값입력
        function fn_OnInputData(pagedata) {
            var remedyIDX = GetJsonValueByKey(pagedata, 'F_REMEDYIDX', '');

            if (remedyIDX == '') {
                alert('조회된 데이터가 없거나 일시적 장애입니다');
                return false;
            }

            var JsonValue = '';

            fn_SetTextValue('srcF_REMEDYIDX', remedyIDX);
            fn_SetTextValue('srcF_MACHIDX', GetJsonValueByKey(pagedata, 'F_MACHIDX', ''));
            fn_SetTextValue('txtIMAGESEQ', GetJsonValueByKey(pagedata, 'F_ATTATCHSEQ', ''));
            fn_SetTextValue('srcF_BANCD', GetJsonValueByKey(pagedata, 'F_BANCD', ''));
            fn_SetTextValue('srcF_BANNM', GetJsonValueByKey(pagedata, 'F_BANNM', ''));
            fn_SetTextValue('srcF_LINECD', GetJsonValueByKey(pagedata, 'F_LINECD', ''));
            fn_SetTextValue('srcF_LINENM', GetJsonValueByKey(pagedata, 'F_LINENM', ''));
            fn_SetTextValue('srcF_OCCURDT', GetJsonValueByKey(pagedata, 'F_OCCURDT', ''));
            fn_SetTextValue('srcF_MACHCD', GetJsonValueByKey(pagedata, 'F_MACHCD', ''));
            fn_SetTextValue('srcF_MACHNM', GetJsonValueByKey(pagedata, 'F_MACHNM', ''));
            fn_SetTextValue('srcF_INSUSER', GetJsonValueByKey(pagedata, 'F_INSUSER', ''));
            fn_SetTextValue('srcF_INSUSERNM', GetJsonValueByKey(pagedata, 'F_INSUSERNM', ''));
            fn_SetTextValue('srcF_INSPIDX', GetJsonValueByKey(pagedata, 'F_INSPIDX', ''));
            fn_SetTextValue('srcF_INSPNM', GetJsonValueByKey(pagedata, 'F_INSPNM', ''));
            fn_SetTextValue('srcF_NUMBER', GetJsonValueByKey(pagedata, 'F_NUMBER', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_STATUS', '');
            JsonValue = JsonValue == '' ? null : JsonValue;
            srcF_STATUS.SetValue(JsonValue);
            fn_SetTextValue('srcF_NGTYPE', GetJsonValueByKey(pagedata, 'F_NGTYPE', ''));
            fn_SetTextValue('srcF_NGTYPENM', GetJsonValueByKey(pagedata, 'F_NGTYPENM', ''));
            fn_SetTextValue('srcF_NGREMK', GetJsonValueByKey(pagedata, 'F_NGREMK', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_RESPTYPE', '');
            JsonValue = JsonValue == '' ? null : JsonValue;
            srcF_RESPTYPE.SetValue(JsonValue);
            JsonValue = GetJsonValueByKey(pagedata, 'F_RESPDT', '');
            JsonValue = JsonValue == '' ? '' : JsonValue;
            fn_SetDate('srcF_RESPDT', convertDateString(JsonValue));
            JsonValue = GetJsonValueByKey(pagedata, 'F_RESPUSER', '');
            JsonValue = JsonValue == '' ? '<%#gsUSERNM%>' : JsonValue;
            fn_SetTextValue('srcF_RESPUSER', JsonValue);
            fn_SetTextValue('srcF_RESPREMK', GetJsonValueByKey(pagedata, 'F_RESPREMK', ''));
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
                    case "GET":
                        fn_OnInputData(data);
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
        // CallbackPanel Event Handler   끝
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <dx:ASPxTextBox ID="srcF_REMEDYIDX" ClientInstanceName="srcF_REMEDYIDX" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="srcF_MACHIDX" ClientInstanceName="srcF_MACHIDX" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false" />
            <table id="inputTable" class="contentTable">
                <colgroup>
                    <col style="width:10%" />
                    <col style="width:23%" />
                    <col style="width:10%" />
                    <col style="width:23%" />
                    <col style="width:10%" />
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
                    <td class="tdLabel">점검일</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_OCCURDT" ClientInstanceName="srcF_OCCURDT" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
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
                    <td class="tdLabel">등록자</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_INSUSER" ClientInstanceName="srcF_INSUSER" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_INSUSERNM" ClientInstanceName="srcF_INSUSERNM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">항목명</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_INSPIDX" ClientInstanceName="srcF_INSPIDX" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_INSPNM" ClientInstanceName="srcF_INSPNM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">차수</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_NUMBER" ClientInstanceName="srcF_NUMBER" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">진행상태</td>
                    <td class="tdInput">
                        <ucCTF:CommonCodeDDLMulti ID="srcF_STATUS" ClientInstanceName="srcF_STATUS" runat="server" targetParams="AA|AAG9" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">이상유형</td>
                    <td class="tdInput tdContentR">
                        <dx:ASPxTextBox ID="srcF_NGTYPE" ClientInstanceName="srcF_NGTYPE" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_NGTYPENM" ClientInstanceName="srcF_NGTYPENM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdInput tdContentR"></td>
                    <td class="tdInput tdContentR"></td>
                    <td class="tdInput tdContentR"></td>
                    <td class="tdInput"></td>
                </tr>
                <tr>
                    <td class="tdLabel">이상내역</td>
                    <td class="tdInput" colspan="5">
                        <dx:ASPxMemo ID="srcF_NGREMK" ClientInstanceName="srcF_NGREMK" runat="server" Width="100%" Height="50px">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel tdRESP">조치유형</td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" id="srcF_RESPTYPE" ClientInstanceName="srcF_RESPTYPE" F_CODEGROUP="43" nullText="선택하세요" />
                    </td>
                    <td class="tdLabel tdRESP">조치일</td>
                    <td class="tdInput">
                        <dx:ASPxDateEdit ID="srcF_RESPDT" ClientInstanceName="srcF_RESPDT" runat="server" Width="100%" Theme="MetropolisBlue"
                            DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd" class="form-control input-sm">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdLabel tdRESP">조치자</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_RESPUSER" ClientInstanceName="srcF_RESPUSER" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel tdRESP">조치내역</td>
                    <td class="tdInput" colspan="5">
                        <dx:ASPxMemo ID="srcF_RESPREMK" ClientInstanceName="srcF_RESPREMK" runat="server" Width="100%" Height="50px">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">첨부파일</td>
                    <td class="tdInput" colspan="5">
                        <button class="btn btn-sm btn-success" onclick="fn_AttachFileOpenReadOnly('설비이상조치관리 첨부파일보기', 'E', 'F'); return false;">
                            <i class="fa fa-upload"></i>
                            <span class="text">첨부파일보기 및 다운로드</span>
                        </button>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging"></div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>