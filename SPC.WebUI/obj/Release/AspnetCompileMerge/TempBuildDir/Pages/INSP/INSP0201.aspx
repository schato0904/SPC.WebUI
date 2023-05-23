<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0201.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.INSP0201" %>
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
            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // 검사성적서 오픈
        function fn_OnCustomButtonClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var oParams = rowKeys.split('|');

            fn_OnPopupInspectionReportView(oParams[0], oParams[1], oParams[2]);
        }

        // 고객사 선택 변경 시
        function fn_OnSelectedIndexChanged(s, e) {
            fn_SetTextValue('hidCUSTOMCD', s.GetValue());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">기간</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Date ID="ucDate" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">고객사</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="ddlCustomer" ClientInstanceName="ddlCustomer" runat="server" Width="200px"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents SelectedIndexChanged="fn_OnSelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">LOT</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="txtLOTNO2" ClientInstanceName="txtLOTNO2" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                    <label class="col-sm-1 control-label">E/0</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxTextBox ID="txtEONO" ClientInstanceName="txtEONO" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidCUSTOMCD" ClientInstanceName="hidCUSTOMCD" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_GROUPCD;F_CUSTOMCD;F_REPORT" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" CustomButtonClick="fn_OnCustomButtonClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataDateColumn FieldName="F_INSDT" Caption="등록일시" Width="150px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" />
                    <dx:GridViewDataTextColumn FieldName="F_CUSTOMNM" Caption="납품업체" CellStyle-HorizontalAlign="Left" Width="170px" />
                    <dx:GridViewDataTextColumn FieldName="F_REPORTNM" Caption="성적서양식명" CellStyle-HorizontalAlign="Left" Width="170px" />
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품번" Width="160px" />
                    <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="품명" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataTextColumn FieldName="F_LOTNO2" Caption="LOT번호" Width="150px" />
                    <dx:GridViewDataTextColumn FieldName="F_EONO" Caption="E/0" Width="180px" />
                    <dx:GridViewDataTextColumn FieldName="F_QUANTITY" Caption="납품수(시료수)" Width="100px" />
                    <dx:GridViewCommandColumn Caption="검사성적서" Width="90px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton Text="출력" ID="btnShipment" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="F_GROUPCD" Caption="그룹코드" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_CUSTOMCD" Caption="납품업체코드" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_REPORT" Caption="리포트" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>