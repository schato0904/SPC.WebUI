<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MNTR0103.aspx.cs" Inherits="SPC.WebUI.Pages.MNTR.MNTR0103" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .left {
            height: 100%;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            width: 550px;
            height: 100%;
            vertical-align: top;
        }
    </style>
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
            devGridWorst.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            devCallbackPanel.PerformCallback();
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
        }

        //  CallbackPanel Callback
        function fn_OnCallbackPanelEndCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
        }

        // Grid Callback Error
        function fn_OnCallbackPanelCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" ID="ucDate" SingleDate="true" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxCallbackPanel ID="devCallbackPanel" ClientInstanceName="devCallbackPanel" runat="server"
                OnCallback="devCallbackPanel_Callback">
                <ClientSideEvents EndCallback="fn_OnCallbackPanelEndCallback" CallbackError="fn_OnCallbackPanelCallbackError" />
                <PanelCollection>
                    <dx:PanelContent>
                        <table style="width: 100%;">
                            <tr>
                                <td class="left">
                                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                                    <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD;F_BANCD" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
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
                                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                                            </StatusBar>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="150px">
                                                <CellStyle HorizontalAlign="Left"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="150px">
                                                <CellStyle HorizontalAlign="Left"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_TOTCNT" Caption="전체시료수" Width="120px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_GOODCNT" Caption="적합시료수" Width="120px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_REJCNT" Caption="부적합시료수" Width="120px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="F_GOODRATE" Caption="적합률" Width="70px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                                <td class="right">
                                    <dx:ASPxGridView ID="devGridWorst" ClientInstanceName="devGridWorst" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD;F_ITEMCD;F_MEAINSPCD" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomColumnDisplayText="devGridWorst_CustomColumnDisplayText">
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
                                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridWorst" />
                                            </StatusBar>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewBandColumn Name="F_BAND" Caption="업체별 WORST 3">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="150px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                        <Settings AllowSort="True" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="항목명" Width="120px">
                                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="F_REJRATE" Caption="부적합률" Width="70px">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
            
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxGridViewExporter ID="devGridExporter2" runat="server" GridViewID="devGridWorst" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
