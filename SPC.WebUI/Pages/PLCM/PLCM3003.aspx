<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM3003.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM3003" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            //devGrid.PerformCallback();
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
            if (schF_MACHCD.GetValue() == "" || schF_MACHCD.GetValue() == null) {
                alert("설비를 선택하세요.");
                return false;
            }
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

        function fn_onDateChanged(s, e) {
            if (schF_MACHCD.GetValue() != "" && schF_MACHCD.GetValue() != null)
                schF_RECIPID.PerformCallback();
        }

        function fn_schF_MACHCD_ValueChanged(s, e) {
            if (schF_MACHCD.GetValue() != "" && schF_MACHCD.GetValue() != null)
                schF_RECIPID.PerformCallback();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="InputTable" style="width: 100%;">
                <colgroup>
                    <col style="width: 10%" />
                    <col style="width: 15%" />
                    <col style="width: 10%" />
                    <col style="width: 15%" />
                    <col style="width: 10%" />
                    <col style="width: 15%" />
                    <col style="width: 10%" />
                    <col style="width: 15%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">작업일자</td>
                    <td class="tdContent">
                        <ucCTF:Date runat="server" ID="ucDate" SingleDate="true" MonthOnly="true" Changed="fn_onDateChanged" />
                    </td>
                    <td class="tdTitle">설비</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox runat="server" ID="schF_MACHCD" ClientInstanceName="schF_MACHCD" IncrementalFilteringMode="None" ValueType="System.String" Width="100%">
                            <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            <ClientSideEvents ValueChanged="fn_schF_MACHCD_ValueChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">레시피ID</td>
                    <td class="tdContentR">
                        <dx:ASPxComboBox runat="server" ID="schF_RECIPID" ClientInstanceName="schF_RECIPID" IncrementalFilteringMode="None" ValueType="System.String" OnCallback="schF_RECIPID_Callback">
                            <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdContentL" colspan="2" />
                </tr>
            </table>
        </div>
        <div style="clear: both; height: 2px;"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%"
                KeyFieldName="" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback"
                OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
