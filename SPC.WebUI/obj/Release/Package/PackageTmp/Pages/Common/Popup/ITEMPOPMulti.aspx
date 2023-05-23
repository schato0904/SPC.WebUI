<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="ITEMPOPMulti.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Popup.ITEMPOPMulti" %>
<%-- 
    ucItemMulti 에서 호출하는 품목선택 팝업 : ITEMPOP.aspx 일부 변형하여 만듬
     --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_OnSearchClick();
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
            var parentParams = ''
            <%if (!gsVENDOR) {%>
            var parentCompCD = parent.fn_OnGetCompCD();
            parentParams += (parentParams == '') ? parentCompCD : '|' + parentCompCD;
            var parentFactCD = parent.fn_OnGetFactCD();
            parentParams += (parentParams == '') ? parentFactCD : '|' + parentFactCD;
            <%}%>
            devGrid.PerformCallback(parentParams);
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

        // Validate
        function fn_OnValidate(s, e) {
        }

        // RowDblClick
        function fn_OnRowDblClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var rowKey = rowKeys.split('|');
            var fnCallback = hidUCID.GetText() != '' && typeof (parent.window[hidUCID.GetText()]) != 'undefined' && typeof (parent.window[hidUCID.GetText()].fn_OnUCSettingItem) == 'function' ? parent.window[hidUCID.GetText()].fn_OnUCSettingItem : parent.fn_OnUCSettingItem;

            if (hidOpenType.GetText() == 'UC') {                // UserControl 에서 호출한 경우
                //parent.fn_OnUCSettingItem(rowKey[0], rowKey[1], rowKey[2], rowKey[3]);
                if (typeof fnCallback == 'undefined') return false;
                fnCallback.call(parent, rowKey[0], rowKey[1], rowKey[2], rowKey[3]);
            } else if (hidOpenType.GetText() == 'UC1') {        // UserControl 에서 호출한 경우
                parent.fn_OnUCSettingItem1(rowKey[0], rowKey[1], rowKey[2], rowKey[3]);
            } else if (hidOpenType.GetText() == 'UC2') {        // UserControl 에서 호출한 경우
                parent.fn_OnUCSettingItem2(rowKey[0], rowKey[1], rowKey[2], rowKey[3]);
            } else if (hidOpenType.GetText() == 'FORM' || hidOpenType.GetText() == 'INS') {       // 일반 Form 에서 호출한 경우
                parent.fn_OnSettingItem(rowKey[0], rowKey[1], rowKey[3]);
            } else if (hidOpenType.GetText() == 'S') {          // 검사기준복사에서 호출한 경우(Source)
                parent.fn_OnSettingItem(rowKey[0], 'S');
            } else if (hidOpenType.GetText() == 'T') {          // 검사기준복사에서 호출한 경우(Target)
                parent.fn_OnSettingItem(rowKey[0], 'T');
            }
            parent.fn_devPopupClose();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidOpenType" ClientInstanceName="hidOpenType" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidOption" ClientInstanceName="hidOption" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidUCID" ClientInstanceName="hidUCID" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:BanPop ID="ucBan" runat="server" />
                    </div>
                    <label class="col-sm-2 control-label">품목코드</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-3 control-label"></label>
                </div>
            </div>
        </div>
        <dx:ASPxTextBox ID="txtMACHGUBUN" ClientInstanceName="txtMACHGUBUN" runat="server" ClientVisible="false" />
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_ITEMNM;F_MODELCD;F_MODELNM" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Auto" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="100%">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SPEC" Caption="규격" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MODELNM" Caption="기종명" Width="100px"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="100px"></dx:GridViewDataColumn>--%>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
