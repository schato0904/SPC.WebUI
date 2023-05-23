<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0102POP.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.Popup.ADTR0102POP" %>
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

        function fn_devGridRowDbClick(s, e) {
            var visibleIndex = e.visibleIndex
            if (visibleIndex < 0) return;

            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(e.visibleIndex));

            var oParams = "";

            oParams += parent.fn_GetCastText('hidUCFROMDT') + '|';
            oParams += parent.fn_GetCastText('hidUCTODT') + '|';
            oParams += rowKeys + '|';
            oParams += '<%=keyFields[0]%>|<%=keyFields[1]%>'

            parent.parent.fn_OnDeleteTab('ANLS0101', parent.parent.fn_OnGetTabObject('ANLS0101'));
            parent.parent.parent.doCreateTab('MM03|MM0301|ANLS|ANLS0101|품질종합현황|RP|1', oParams);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False"  Width="100%"
                SettingsBehavior-AllowSelectSingleRowOnly="true" SettingsBehavior-AllowSelectByRowClick="true" SettingsBehavior-AllowSort="true"
                KeyFieldName="F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN;F_WORKDATE;F_WORKTIME" EnableViewState="False" EnableRowsCache="False"
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
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridRowDbClick" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKNM" Caption="공정명" Width="200px" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKDATE" Caption="검사일자" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKTIME" Caption="검사시간" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKMAN" Caption="작업자" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_INSPDETAIL" Caption="검사항목" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="규격" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="상한" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="하한" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MEASURE" Caption="측정값" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ERR" Caption="판정" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="F_WORKCD"  Visible="false" />                    
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Visible="false" />                    
                    <dx:GridViewDataTextColumn FieldName="F_SERIALNO" Visible="false" />                    
                    <dx:GridViewDataTextColumn FieldName="F_MEAINSPCD" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_SIRYO" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_FREEPOINT" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
