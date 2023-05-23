<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0208.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0208" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
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
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else
                devGrid.UpdateEdit();

        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.CancelEdit();
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
            fn_OnBatchValidate("F_EQUIPTP", s, e);
            fn_OnBatchValidate("F_EQUIPNM", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            fn_OnControlDisableBox(s.GetEditor('F_EQUIPCD'), null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">구분</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="ddlEQUIPTP" ClientInstanceName="ddlEQUIPTP" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">설비명</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="txtEQUIPNM" ClientInstanceName="txtEQUIPNM" runat="server" Width="100%" />
                    </div>
                    <label class="col-sm-1 control-label">사용</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxRadioButtonList ID="rdoSTATUS" ClientInstanceName="rdoSTATUS" runat="server"
                            RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" />
                                <dx:ListEditItem Text="사용" Value="1" />
                                <dx:ListEditItem Text="중단" Value="0" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD;F_EQUIPCD" EnableViewState="false" EnableRowsCache="false"
                OnInit="devGrid_Init" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnBatchUpdate="devGrid_BatchUpdate">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_EQUIPTP" Caption="설비구분" Width="100px" />
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPCD" Caption="설비코드" Width="100px" />
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="설비명">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ETC" Caption="비고">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용여부" Width="80px"></dx:GridViewDataCheckColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>