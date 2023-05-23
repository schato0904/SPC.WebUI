<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERDBS1001.aspx.cs" Inherits="SPC.WebUI.Pages.WERDBS.WERDBS1001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" ID="ucDate" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox runat="server" ID="workCombo" ClientInstanceName="workCombo" TextField="F_WORKNM" ValueField="F_WORKCD"
                            IncrementalFilteringMode="None" CssClass="NoXButton" OnCallback="workCombo_Callback">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">초중종구분</label>
                    <div class="col-sm-1">
                        <dx:ASPxComboBox ID="ddlFIRSTITEM" ClientInstanceName="ddlFIRSTITEM" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="3" Selected="true" />
                                <dx:ListEditItem Text="초품" Value="0" />
                                <dx:ListEditItem Text="중품" Value="1" />
                                <dx:ListEditItem Text="종품" Value="2" />

                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox runat="server" ID="itemCombo" ClientInstanceName="itemCombo" TextField="F_ITEMCD" ValueField="F_ITEMCD"
                            IncrementalFilteringMode="None" CssClass="NoXButton" OnCallback="itemCombo_Callback">
                        </dx:ASPxComboBox>
                    </div>

                    <label class="col-sm-2 control-label">작업자</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox runat="server" ID="userCombo" ClientInstanceName="userCombo" TextField="F_USERID" ValueField="F_USERID"
                            IncrementalFilteringMode="None" CssClass="NoXButton" OnCallback="userCombo_Callback">
                        </dx:ASPxComboBox>
                    </div>

                    <label class="col-sm-1 control-label">구분</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox runat="server" ID="gubunCombo" ClientInstanceName="gubunCombo" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="2" Selected="true" />
                                <dx:ListEditItem Text="주간" Value="0" />
                                <dx:ListEditItem Text="야간" Value="1" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Disabled" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자" Width="9%" />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="9%" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="8%">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="8%">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="10%">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="14%">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_GUBUN" Caption="구분" Width="5%" />
                    <dx:GridViewDataColumn FieldName="F_FIRSTITEM" Caption="초품구분" Width="5%" />
                    <dx:GridViewDataColumn FieldName="F_TOTQTY" Caption="생산수량" Width="8%">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSOQTY" Caption="합격수량" Width="8%">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NGQTY" Caption="불량수량" Width="8%">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_USERID" Caption="작업자" Width="8%" />
                    <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
