<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0901NEW.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.Popup.BSIF0901NEW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
            F_WORKTXT.Focus();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height - 50);
            F_WORKTXT.SetHeight(height - 30);
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid.PerformCallback('SEARCH|');
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (fn_OnValidate() == false) return false;

            devGrid.PerformCallback('SAVE|' + devGrid.GetSelectedKeysOnPage());
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

                    if (s.cpResultCode == '1') {
                        fn_OnSearchClick();
                        parent.fn_OnSearchClick();
                        parent.fn_devPopupClose();
                    }

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
            if (fn_GetCastText('F_WORKTXT') == '') {
                alert('업무지시내용을 입력하세요');
                F_WORKTXT.Focus();
                return false;
            }

            selectedKeys = devGrid.GetSelectedKeysOnPage();
            if (selectedKeys.length <= 0) {
                alert('업무지시 할 작업지시서를 선택하세요');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-4">
                        <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                            <div class="form-group">
                                <label class="col-sm-12 control-label" style="text-align:left;">업무지시내용</label>
                                <div class="col-sm-12 control-label">
                                    <dx:ASPxMemo ID="F_WORKTXT" ClientInstanceName="F_WORKTXT" runat="server" Width="100%" Height="200px" class="form-control input-sm" MaxLength="4000"></dx:ASPxMemo>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-8">
                        <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">작업일</label>
                                <div class="col-sm-5 control-label">
                                    <ucCTF:Date ID="ucDate" runat="server" TodayFromDate="true" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">품목</label>
                                <div class="col-sm-5">
                                    <ucCTF:Item ID="ucItem" runat="server" />
                                </div>
                                <label class="col-sm-1 control-label">공정</label>
                                <div class="col-sm-4 control-label">
                                    <ucCTF:Work ID="ucWork" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="form-horizontal">
                            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="PLANNO" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGrid_CustomCallback">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                <SettingsBehavior AllowSort="false" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
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
                                    <dx:GridViewDataColumn FieldName="PLANNO" Caption="작지번호" Width="110px"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ITEMCD" Caption="품목코드" Width="110px"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ITEMNM" Caption="품목명">
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Width="70px"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="100px"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="LOTNO" Caption="LOT NO" Width="150px"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PLANQTY" Caption="수량" Width="60px">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>