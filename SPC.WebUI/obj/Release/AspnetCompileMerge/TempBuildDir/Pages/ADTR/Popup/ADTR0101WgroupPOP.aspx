<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0101WgroupPOP.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.Popup.ADTR0101WgroupPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height();
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height - 20);
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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_OnRowDblClick(s, e) {
            var visibleIndex = e.visibleIndex
            
            if (visibleIndex < 0) return;

            var rowKey = devGrid.GetRowKey(e.visibleIndex)
            //var rowKey = devGrid.GetRowValues(e.visibleIndex, 'F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN', fn_OnGetRowValues);
            var oParams = "";
            
            oParams += parent.fn_GetCastText('hidUCFROMDT') + '|'
            oParams += parent.fn_GetCastText('hidUCTODT') + '|'
            oParams += rowKey; //F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN
            oParams += '|||' + fn_GetCastText(  'hidCOMPCD') + '|' + fn_GetCastText('hidFACTCD');
            
            parent.parent.fn_OnDeleteTab('ANLS0101', parent.parent.fn_OnGetTabObject('ANLS0101'));
            parent.parent.parent.doCreateTab('MM03|MM0301|ANLS|ANLS0101|품질종합현황|RP|1', oParams);
        }

        function fn_OnGetRowValues(val) {
            alert(val)
        }

        function fn_Adtr0103() {
            //alert(fn_GetCastText('hidWorkcd'))
            var oParams = '<%=gsCOMPCD%>' + '|'
            oParams += '<%=gsFACTCD%>' + '|'
            oParams += parent.fn_GetCastText('hidUCFROMDT') + '|'
            oParams += parent.fn_GetCastText('hidUCTODT') + '|'
            oParams += fn_GetCastText('hidWorkcd')

            parent.parent.fn_OnDeleteTab('ADTR0103', parent.parent.fn_OnGetTabObject('ADTR0103'));
            parent.parent.parent.doCreateTab('MM02|MM0201|ADTR|ADTR0103|공정능력모니터링|RE|1', oParams);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidWorkcd" ClientInstanceName="hidWorkcd" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False"  Width="100%"
                SettingsBehavior-AllowSelectSingleRowOnly="true" SettingsBehavior-AllowSelectByRowClick="true" SettingsBehavior-AllowSort="true"
                KeyFieldName="F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN" EnableViewState="False" EnableRowsCache="False"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_WORKDATE" Caption="검사일자" Width="80" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKMAN" Caption="작업자" Width="70" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px" />
                    <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="품목명" MinWidth="200">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="100">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="규격" Width="70" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="상한" Width="70" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="하한" Width="70">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_UCLX" Caption="UCL" Width="70">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LCLX" Caption="LCL" Width="70">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MEASURE" Caption="측정값" Width="70">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_ERR" Caption="판정"  Width="70px"  >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_CONTENTS" Caption="원인" Width="120" />
                    <dx:GridViewDataTextColumn FieldName="F_RETURNCO" Caption="조치" Width="120" />

                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_SIRYO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_FREEPOINT" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging" style="text-align:center;margin-top:8px;">
            <a href="#" onclick="fn_Adtr0103()" class="btn btn-default btn-sm">공정능력모니터링</a>
        </div>
        <%--<div class="paging">
        </div>--%>
    </div>
</asp:Content>
