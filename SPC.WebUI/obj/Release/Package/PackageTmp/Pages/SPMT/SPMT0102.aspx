<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SPMT0102.aspx.cs" Inherits="SPC.WebUI.Pages.SPMT.SPMT0102" %>
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

        // 검사성적서 오픈
        function fn_OnCustomButtonClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var oParams = rowKeys.split('|');

            if (e.buttonID == 'btnShipment')
                fn_OnPopupInspectionReport(oParams[0]);

            if (e.buttonID == 'btnDelete') {
                if (oParams[1].toLowerCase() =='false' && oParams[2].toLowerCase() == 'true') {
                    if (confirm('해당 출하데이타를 삭제하시겠습니까?\r삭제를 진행하려면 확인을 누르세요')) {
                        devGrid.PerformCallback(oParams[0]);
                    }
                }
                else if(oParams[2].toLowerCase() == 'false')
                    alert('이미 삭제처리된 출하입니다.');
                else if (oParams[1].toLowerCase() == 'true')
                    alert('출하된 데이타는 삭제처리를 할 수 없습니다!!');
            }
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
                    <label class="col-sm-1 control-label">소재LOT</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxTextBox ID="txtMATERLOTNO" ClientInstanceName="txtMATERLOTNO" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                    <label class="col-sm-1 control-label">가공LOT</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxTextBox ID="txtWORKLOTNO" ClientInstanceName="txtWORKLOTNO" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                    <label class="col-sm-1 control-label">E/0</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxTextBox ID="txtEONO" ClientInstanceName="txtEONO" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxCheckBox ID="chkSTATUS" ClientInstanceName="chkSTATUS" runat="server" Text="삭제제외" class="form-control input-sm" Checked="true" />
                    </div>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlSHIPMENT" ClientInstanceName="ddlSHIPMENT" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <Items>
                                <dx:ListEditItem Text="출하상태" Value="" />
                                <dx:ListEditItem Text="출하완료" Value="1" />
                                <dx:ListEditItem Text="출하대기" Value="0" />
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
                KeyFieldName="F_GROUPCD;F_SHIPMENT;F_STATUS" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
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
                    <dx:GridViewDataTextColumn FieldName="F_GROUPCD" Caption="그룹코드" Width="140px" />
                    <dx:GridViewDataTextColumn FieldName="F_SHIPCOMPNM" Caption="납품업체" Width="100px" />
                    <dx:GridViewDataTextColumn FieldName="F_MATERLOTNO" Caption="소재LOT번호" Width="150px" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKLOTNO" Caption="가공LOT번호" Width="150px" />
                    <dx:GridViewDataTextColumn FieldName="F_EONO" Caption="E/0" Width="150px" />
                    <dx:GridViewDataTextColumn FieldName="F_DCNT" Caption="납품수량" Width="150px" />
                    <dx:GridViewDataTextColumn FieldName="F_DIRECTOR" Caption="품질보증책임자" Width="100px" />
                    <dx:GridViewDataTextColumn FieldName="F_STATUS" Caption="사용여부" Width="60px" />
                    <dx:GridViewDataTextColumn FieldName="F_SHIPMENT" Caption="출하상태" Width="60px" />
                    <dx:GridViewCommandColumn Caption="검사성적서" Width="80px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton Text="출력" ID="btnShipment" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn Caption="삭제" Width="40px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton Text="삭제" ID="btnDelete" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="F_GROUPCD" Caption="고유코드" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
