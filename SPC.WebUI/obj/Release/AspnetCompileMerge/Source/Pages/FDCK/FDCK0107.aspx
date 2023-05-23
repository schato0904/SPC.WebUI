<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FDCK0107.aspx.cs" Inherits="SPC.WebUI.Pages.FDCK.FDCK0107" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .content {
            width: 100%;
            height: 100%;
            display: table;
        }
                .contentTable {
            width: 100%;
            border-color: darkgray;
        }
            .contentTable > tbody > tr > .tdLabel {
                /*background-color: #CFEFFF;*/
                background-color: #DCDCDC;
                color: dimgray;
                text-align: center;
                padding-top: 3px;
                padding-bottom: 3px;
            }
            .contentTable > tbody > tr > .tdLabel > label {
                color: #444444;
                font-weight:bold;
            }
            .contentTable > tbody > tr > .tdInput {
                background-color: white;
                padding-left: 3px;
                padding-right: 3px;
            }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var Param = null;
        $(document).ready(function () {
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height();
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(80);
            devGrid1.SetHeight(height / 3);
        }

        // 조회
        function fn_OnSearchClick() {

            if (fn_GetCastValue('txtucMachineCD') == "") {
                alert("설비를 선택 후 조회 하세요.");
                return;
            }

            fn_doSetGridEventAction('true');
            devGrid.PerformCallback();
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
        }

        // 취소
        function fn_OnCancelClick() {

            fn_SetTextValue('hiducMachineCD', '');
            fn_SetTextValue('hiducMachTypeCD', '');
            fn_SetTextValue('txtucMachineCD', '');
            fn_SetTextValue('txtucMachineNM', '');
            fn_SetTextValue('txtucMachtypeNM', '');
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();

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

        function fn_OnAdminClick(date, val) {
            if (val == "2") {
                var oParams = 'ins|' + date + '|' + val;
                devCallback.PerformCallback(oParams);
            }
        }

        function fn_OndevCallback(s, e) {
            devGrid.PerformCallback();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="content">
            <table border="1" class="contentTable">
                <colgroup>
                    <col style="width:10%" />
                    <col style="width:10%" />
                    <col style="width:10%" />
                    <col style="width:70%" />
                </colgroup>
                <tr>
                    <td class="tdLabel required">
                        <label>조회월</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:Date runat="server" id="ucDate" MonthOnly="true" MaxMonth="12" SingleDate="true"  />
                    </td>
                    <td class="tdLabel required">
                        <label>설비번호</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:Machine ID="ucMachine" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height:10px;"></div>
        <div class="content">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true"  Width="100%" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                         EnableViewState="False" EnableRowsCache="False" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" >
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" ShowColumnHeaders="false" />
                        <SettingsBehavior AllowSort="false"/>
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                    </dx:ASPxGridView>
                    <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="true"  Width="100%"
                         EnableViewState="False" EnableRowsCache="False" OnCustomCallback="devGrid1_CustomCallback">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" ShowColumnHeaders="false" />
                        <SettingsBehavior AllowSort="false"/>
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                        <Templates>
                            <StatusBar>
                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                            </StatusBar>
                        </Templates>
                    </dx:ASPxGridView>
                </section>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OndevCallback" />
            </dx:ASPxCallback>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>

