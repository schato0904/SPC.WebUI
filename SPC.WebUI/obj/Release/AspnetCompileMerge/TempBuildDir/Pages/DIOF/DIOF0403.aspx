<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0403.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0403" %>
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

            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            fn_OnChangeMode('C');
            fn_OnInputClear();
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {
            fn_OnChangeMode('U');
        }

        // 저장
        function fn_OnSaveClick() {
            if (fn_GetCastValue('hidPageMode') != 'C' && fn_GetCastValue('hidPageMode') != 'U') { alert('입력, 수정모드에서만 저장할 수 있습니다'); return false; }
            if (!fn_OnValidate()) return false;

            var data = {
                'PAGEMODE': fn_GetCastValue('hidPageMode'),
                'ACTION': 'SAVE'
            };

            fn_devCallback_PerformCallback(data);
        }

        // 취소
        function fn_OnCancelClick() {
            fn_OnInputClear();
            fn_OnChangeMode('R');
        }

        // 삭제
        function fn_OnDeleteClick() {

        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

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
            var sSTATUS = srcF_STATUS.GetValue();
            var sRESPTYPE = fn_GetCastValue('srcF_RESPTYPE');
            var sRESPDT = fn_GetCastText('srcF_RESPDT');
            var sRESPUSER = fn_GetCastText('srcF_RESPUSER');
            var sRESPREMK = fn_GetCastText('srcF_RESPREMK');

            if (sSTATUS == '' || sSTATUS == null) {
                alert('진행상태를 선택하세요!!');
                srcF_STATUS.Focus();
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
                    fn_Focus('srcF_RESPDT');
                    return false;
                }
                if (!isValidDate(sRESPDT)) {
                    alert('날짜형식이 올바르지 않습니다!!');
                    fn_Focus('srcF_RESPDT');
                    return false;
                }
                if (sRESPUSER == '' || sRESPUSER == null) {
                    alert('조치자를 입력하세요!!');
                    fn_Focus('srcF_RESPUSER');
                    return false;
                }
                if (sRESPREMK == '' || sRESPREMK == null) {
                    alert('조치내역을 입력하세요!!');
                    fn_Focus('srcF_RESPREMK');
                    return false;
                }
            }

            return true;
        }

        // 그리드 더블 클릭시 선택항목 상세 조회
        function fn_OnRowDblClick(s, e) {
            var data = {
                'F_REMEDYIDX': devGrid.GetRowKey(e.visibleIndex),
                'ACTION': 'GET'
            };
            fn_devCallback_PerformCallback(data);
        }

        // 입력폼 모드변경
        function fn_OnChangeMode(t) {
            fn_SetTextValue('hidPageMode', t);

            if (t == 'R') {
                $('#inputTable').find('*').prop('disabled', true);
                fn_SetTextValue(srcF_MODE, '조회모드(기존 정보를 수정하려면 목록에서 수정할 정보를 선택하세요)');
                fn_OnControlEnableComboBox(srcF_STATUS, false);
                fn_OnControlEnableComboBox(srcF_RESPTYPE, false);
                fn_OnControlEnableDateEdit(srcF_RESPDT, false);
            } else if (t == 'C') {
                $('#inputTable').find('*').prop('disabled', false);
                fn_SetTextValue(srcF_MODE, '신규입력모드(입력한 정보를 저장하려면[저장] 버튼을, 입력을 취소하려면 [취소] 버튼을 누르세요)');
            } else if (t == 'U') {
                $('#inputTable').find('*').prop('disabled', false);
                fn_SetTextValue(srcF_MODE, '수정입력모드(입력한 정보를 저장하려면[저장] 버튼을, 입력을 취소하려면 [취소] 버튼을 누르세요)');
                fn_OnControlEnableComboBox(srcF_STATUS, true);
                fn_OnControlEnableComboBox(srcF_RESPTYPE, true);
                fn_OnControlEnableDateEdit(srcF_RESPDT, true);
            }
        }

        // 입력폼 초기화
        function fn_OnInputClear() {
            fn_SetTextValue('srcF_REMEDYIDX', '');
            fn_SetTextValue('srcF_MACHIDX', '');
            fn_SetTextValue('txtIMAGESEQ', '');
            fn_SetTextValue('srcF_BANCD', '');
            fn_SetTextValue('srcF_BANNM', '');
            fn_SetTextValue('srcF_LINECD', '');
            fn_SetTextValue('srcF_LINENM', '');
            fn_SetTextValue('srcF_OCCURDT', '');
            fn_SetTextValue('srcF_MACHCD', '');
            fn_SetTextValue('srcF_MACHNM', '');
            fn_SetTextValue('srcF_INSUSER', '');
            fn_SetTextValue('srcF_INSUSERNM', '');
            fn_SetTextValue('srcF_INSPIDX', '');
            fn_SetTextValue('srcF_INSPNM', '');
            fn_SetTextValue('srcF_NUMBER', '');
            srcF_STATUS.SetValue(null); 
            fn_SetTextValue('srcF_NGTYPE', '');
            fn_SetTextValue('srcF_NGREMK', '');
            srcF_RESPTYPE.SetValue(null);
            fn_SetDate('srcF_RESPDT', new Date());
            fn_SetTextValue('srcF_RESPUSER', '');
            fn_SetTextValue('srcF_RESPREMK', '');
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
                    case "SAVE":
                        alert(msg);
                        data.ACTION = 'GET';
                        fn_devCallback_PerformCallback(data);
                        fn_OnSearchClick();
                        break;
                    case "GET":
                        fn_OnInputData(data);
                        fn_OnModifyClick();
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

        // 진행상태 변경 시
        function fn_StatusSelectedIndexChanged(s, e) {
            if (s.GetValue() == 'AAG902') {
                $('.tdRESP').addClass('required');
            } else {
                $('.tdRESP').removeClass('required');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" ClientVisible="false" Text="R" /><!--C:신규,R:조회,U:수정-->
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
                    <td class="tdLabel">현재상태</td>
                    <td class="tdInput" colspan="5">
                        <dx:ASPxTextBox ID="srcF_MODE" ClientInstanceName="srcF_MODE" runat="server" Width="100%" Font-Bold="true" ForeColor="Black" Text="조회모드">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
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
                    <td class="tdLabel required">진행상태</td>
                    <td class="tdInput">
                        <ucCTF:CommonCodeDDLMulti ID="srcF_STATUS" ClientInstanceName="srcF_STATUS" runat="server" targetParams="AA|AAG9" targetFuncs="fn_StatusSelectedIndexChanged" />
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
                        <dx:ASPxTextBox ID="srcF_RESPUSER" ClientInstanceName="srcF_RESPUSER" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel tdRESP">조치내역</td>
                    <td class="tdInput" colspan="5">
                        <dx:ASPxMemo ID="srcF_RESPREMK" ClientInstanceName="srcF_RESPREMK" runat="server" Width="100%" Height="50px" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">첨부파일</td>
                    <td class="tdInput" colspan="5">
                        <button class="btn btn-sm btn-success" onclick="fn_AttachFileOpen('설비이상조치관리 첨부파일등록', 'E', 'F'); return false;">
                            <i class="fa fa-upload"></i>
                            <span class="text">첨부파일등록</span>
                        </button>
                    </td>
                </tr>
            </table>
            <div class="divLine"></div>
            <table class="contentTable">
                <colgroup>
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">기간</td>
                    <td class="tdInput" colspan="3">
                        <ucCTF:Date runat="server" id="ucDate" />
                    </td>
                    <td class="tdLabel">진행상태</td>
                    <td class="tdInput tdContentR">
                        <ucCTF:CommonCodeDDLMulti ID="schF_STATUS" ClientInstanceName="schF_STATUS" runat="server" targetParams="AA|AAG9" />
                    </td>
                    <td class="tdInput" colspan="4"></td>
                </tr>
                <tr>
                    <td class="tdLabel">반</td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="schF_BANCD" ClientInstanceName="schF_BANCD" targetCtrls="schF_LINECD" />
                    </td>
                    <td class="tdLabel">라인</td>
                    <td class="tdInput">
                        <ucCTF:LineMulti runat="server" ID="schF_LINECD" ClientInstanceName="schF_LINECD" targetCtrls="schF_MACHIDX" />
                    </td>
                    <td class="tdLabel">설비분류</td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" ID="schF_MACHKIND" ClientInstanceName="schF_MACHKIND" F_CODEGROUP="40" targetCtrls="schF_MACHIDX" />
                    </td>
                    <td class="tdLabel">설비</td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="schF_MACHIDX" ClientInstanceName="schF_MACHIDX" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" ValueField="F_MACHIDX" DropDownStyle="DropDownList"
                            OnCallback="schF_MACHIDX_Callback" NullText="반을 선택하세요">
                            <Columns>
                                <dx:ListBoxColumn FieldName="F_MACHIDX" Visible="false" />
                                <dx:ListBoxColumn FieldName="F_MACHCD" Caption="설비코드" />
                                <dx:ListBoxColumn FieldName="F_MACHNM" Caption="설비명" />
                            </Columns>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdLabel">설비명</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" Width="100%" MaxLength="50" />
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_REMEDYIDX" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
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
                    <dx:GridViewDataColumn FieldName="F_ROWNUM" Caption="No" Width="40px" />
                    <dx:GridViewDataColumn FieldName="F_OCCURDT" Caption="점검일" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NGTYPENM" Caption="이상유형" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_NGREMK" Caption="이상내역" Width="250px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RESPTYPENM" Caption="조치유형" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_RESPREMK" Caption="조치내역" Width="250px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="진행상태" Width="70px" />

                    
                    <dx:GridViewDataColumn FieldName="F_REMEDYIDX" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MACHIDX" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_BANCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_INSPIDX" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
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