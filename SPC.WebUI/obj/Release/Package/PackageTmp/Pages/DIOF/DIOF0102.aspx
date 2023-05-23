<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0102.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0102" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var isWorkerCallback = false;
        var isManagerCallback = false;
        var isWorkerClickCallback = false;
        var isManagerClickCallback = false;

        $(document).ready(function () {
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
            pagingHeight = $(".divPadding").height();
            height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $('.divHalfHeight').height(height / 2);

            devGridWorker.SetHeight((height / 2) - 40);
            devGridManager.SetHeight((height / 2) - 40);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (false == isSelectMach()) {
                alert('담당자 또는 관리자를 등록/삭제할 설비를 선택하세요');
                return;
            }

            devCallback.PerformCallback('P');
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

        // Grid Row DblClick
        function fn_OnRowDblClick(s, e) {
            fn_doSetGridEventAction('false');
            fn_OnInputData(devGrid.GetRowKey(e.visibleIndex).split('|'));
            devGridWorker.UnselectAllRowsOnPage();
            devGridManager.UnselectAllRowsOnPage();
            devCallback.PerformCallback('S');
        }

        // 입력폼 초기화
        function fn_OnInputClear() {
            fn_SetTextValue('srcF_MACHIDX', '');
            fn_SetTextValue('srcF_MACHCD', '');
            fn_SetTextValue('srcF_MACHNM', '');
        }

        // 입력폼 조회값입력
        function fn_OnInputData(rowKey) {
            fn_SetTextValue('srcF_MACHIDX', rowKey[0]);
            fn_SetTextValue('srcF_MACHCD', rowKey[1]);
            fn_SetTextValue('srcF_MACHNM', rowKey[2]);
        }

        // 설비선택여부
        function isSelectMach() {
            return fn_GetCastValue('srcF_MACHIDX') != '' && fn_GetCastValue('srcF_MACHCD') != '' && fn_GetCastValue('srcF_MACHNM') != '';
        }

        // 담당자 선택 변경 이벤트
        function fn_OnWorkerSelectionChanged(s, e) {
            if (false == isSelectMach() && !isWorkerClickCallback) {
                alert('담당자를 등록/삭제할 설비를 선택하세요');
                isWorkerClickCallback = true;
                s.UnselectAllRowsOnPage();
                return;
            }
            
            if (!isWorkerCallback)
                s.GetSelectedFieldValues("F_USERID", GetWorkerSelectedFieldValuesCallback);

            isWorkerCallback = false;
        }

        function GetWorkerSelectedFieldValuesCallback(values) {
            fn_SetTextValue('srcF_WORKER', '');
            fn_SetTextValue('srcF_WORKER', values.join('|'));

            devListWorker.BeginUpdate();
            try {
                devListWorker.ClearItems();
                for (var i = 0; i < values.length; i++) {
                    devListWorker.AddItem(values[i]);
                }
            } finally {
                devListWorker.EndUpdate();
            }
        }

        // 관리자자 선택 변경 이벤트
        function fn_OnManagerSelectionChanged(s, e) {
            if (!isSelectMach() && !isManagerClickCallback) {
                alert('담당자를 등록/삭제할 설비를 선택하세요');
                isManagerClickCallback = true;
                s.UnselectAllRowsOnPage();
                return;
            }

            if (!isManagerCallback) {
                s.GetSelectedFieldValues("F_USERID", GetManagerSelectedFieldValuesCallback);
            }

            isManagerCallback = false;
        }

        function GetManagerSelectedFieldValuesCallback(values) {
            fn_SetTextValue('srcF_MANAGER', '');
            fn_SetTextValue('srcF_MANAGER', values.join('|'));

            devListManager.BeginUpdate();
            try {
                devListManager.ClearItems();
                for (var i = 0; i < values.length; i++) {
                    devListManager.AddItem(values[i]);
                }
            } finally {
                devListManager.EndUpdate();
            }
        }

        // 담당자, 관리자 조회 콜백
        function fn_devCallback_EndCallback(s, e) {
            if (s.cpResultCode == '') {
                isWorkerCallback = s.cpWorker != '';
                isManagerCallback = s.cpManager != '';

                fn_SetTextValue('srcF_WORKER', s.cpWorker);
                fn_SetTextValue('srcF_MANAGER', s.cpManager);

                var values = '';

                values = s.cpWorker.split('|');
                devGridWorker.SelectRowsByKey(values);
                devListWorker.BeginUpdate();
                try {
                    devListWorker.ClearItems();
                    for (var i = 0; i < values.length; i++) {
                        devListWorker.AddItem(values[i]);
                    }
                } finally {
                    devListWorker.EndUpdate();
                }

                values = s.cpManager.split('|');
                devGridManager.SelectRowsByKey(values);
                devListManager.BeginUpdate();
                try {
                    devListManager.ClearItems();
                    for (var i = 0; i < values.length; i++) {
                        devListManager.AddItem(values[i]);
                    }
                } finally {
                    devListManager.EndUpdate();
                }

                
            } else {
                alert(s.cpResultMsg);
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
                        <div style="width:100%;text-align:left;font-weight:bold;color:red;">담당자 또는 관리자를 등록/삭제하려면 아래에서 해당 설비를 더블클릭하세요</div>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content">
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_MACHIDX;F_MACHCD;F_MACHNM" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
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
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
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
                                <col style="width:90px;" />
                                <col style="width:130px;" />
                                <col style="width:130px;" />
                                <col />
                            </colgroup>
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
                            </tr>
                        </table>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content_r">
                        <div class="divHalfHeight">
                            <table class="contentTable" style="height:100%;">
                                <thead>
                                    <tr>
                                        <th style="text-align:left;padding-left:5px;width:150px;">설비점검담당자</th>
                                        <th style="text-align:left;padding-left:5px;">사용자목록</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="padding:5px;">
                                            <dx:ASPxListBox ID="devListWorker" ClientInstanceName="devListWorker" runat="server" Height="100%" Width="100%" />
                                        </td>
                                        <td style="padding:5px;">
                                            <dx:ASPxTextBox ID="srcF_WORKER" ClientInstanceName="srcF_WORKER" runat="server" ClientVisible="false" />
                                            <dx:ASPxGridView ID="devGridWorker" ClientInstanceName="devGridWorker" runat="server" AutoGenerateColumns="false" Width="100%"
                                                KeyFieldName="F_USERID" EnableViewState="false" EnableRowsCache="false">
                                                <Styles>
                                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                                    <EditForm CssClass="bg-default"></EditForm>
                                                </Styles>
                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                                                <SettingsBehavior AllowSort="false" />
                                                <SettingsPager Mode="ShowAllRecords" />
                                                <ClientSideEvents Init="fn_OnGridInit" SelectionChanged="fn_OnWorkerSelectionChanged" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="F_USERID" Caption="사용자ID" Width="150px" />
                                                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="사용자명" Width="100%">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_GROUPNM" Caption="작업그룹" Width="150px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_GRADENM" Caption="직위" Width="150px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_DEPARTNM" Caption="부서" Width="150px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>

                                                    <dx:GridViewDataColumn FieldName="F_USERUSED" Visible="false" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="divPadding"></div>
                        <div class="divHalfHeight">
                            <table class="contentTable" style="height:100%;">
                                <thead>
                                    <tr>
                                        <th style="text-align:left;padding-left:5px;width:150px;">설비점검관리자</th>
                                        <th style="text-align:left;padding-left:5px;">사용자목록</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="padding:5px;">
                                            <dx:ASPxListBox ID="devListManager" ClientInstanceName="devListManager" runat="server" Height="100%" Width="100%" />
                                        </td>
                                        <td style="padding:5px;">
                                            <dx:ASPxTextBox ID="srcF_MANAGER" ClientInstanceName="srcF_MANAGER" runat="server" ClientVisible="false" />
                                            <dx:ASPxGridView ID="devGridManager" ClientInstanceName="devGridManager" runat="server" AutoGenerateColumns="false" Width="100%"
                                                KeyFieldName="F_USERID" EnableViewState="false" EnableRowsCache="false">
                                                <Styles>
                                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                                    <EditForm CssClass="bg-default"></EditForm>
                                                </Styles>
                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                                                <SettingsBehavior AllowSort="false" />
                                                <SettingsPager Mode="ShowAllRecords" />
                                                <ClientSideEvents Init="fn_OnGridInit" SelectionChanged="fn_OnManagerSelectionChanged" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="F_USERID" Caption="사용자ID" Width="150px" />
                                                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="사용자명" Width="100%">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_GROUPNM" Caption="작업그룹" Width="150px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_GRADENM" Caption="직위" Width="150px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_DEPARTNM" Caption="부서" Width="150px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>

                                                    <dx:GridViewDataColumn FieldName="F_USERUSED" Visible="false" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>