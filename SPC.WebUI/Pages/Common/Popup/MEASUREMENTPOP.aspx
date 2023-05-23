<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="MEASUREMENTPOP.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Popup.MEASUREMENTPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            var keys = '<%=sParams%>'.split('|');

            fn_SetTextValue('hidUCFROMDT', keys[0]);
            fn_SetDate('txtFROMDT', convertDateString(keys[0]));

            fn_SetTextValue('hidUCTODT', keys[1]);
            fn_SetDate('txtTODT', convertDateString(keys[1]));

            fn_SetTextValue('hidUCITEMCD', keys[2]);
            fn_SetTextValue('txtUCITEMCD', keys[2]);
            fn_SetTextValue('txtUCITEMNM', keys[3]);

            fn_SetTextValue('hidUCWORKPOPCD', keys[4]);
            fn_SetTextValue('txtUCWORKPOPCD', keys[4]);
            fn_SetTextValue('txtUCWORKNM', keys[5]);

            fn_SetTextValue('hidUCINSPITEMCD', keys[6]);
            fn_SetTextValue('txtUCINSPITEMCD', keys[6]);
            fn_SetTextValue('txtUCINSPITEMNM', keys[7]);

            fn_SetTextValue('txtSTANDARD', keys[8]);
            fn_SetTextValue('txtMAX', keys[9]);
            fn_SetTextValue('txtMIN', keys[10]);

            fn_SetTextValue('txtSERIALNO', keys[11]);

            devGrid.PerformCallback();
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
            window.open('../Export/MEASUREMENTEXPORT.aspx?pSTDT=' + fn_GetCastValue('hidUCFROMDT') + '&pEDDT=' + fn_GetCastValue('hidUCTODT') + '&pITEMCD=' + fn_GetCastValue('hidUCITEMCD') + '&pWORKCD=' + fn_GetCastValue('hidUCWORKPOPCD') + '&pSERIALNO=' + fn_GetCastValue('txtSERIALNO'));
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

        // Validate
        function fn_OnValidate(s, e) {
        }

        // Set QCD34
        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);

            txtSERIALNO.SetText(resultValues[10]);
            txtSERIALNO.SetValue(resultValues[10]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <div class="col-sm-8 control-label">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">기간</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:Date ID="ucDate" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">품목</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:Item ID="ucItem" runat="server" usedModel="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">공정</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">항목</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목;hidUCWORKPOPCD|공정" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <label class="col-sm-6 control-label">검사규격</label>
                            <div class="col-sm-6 control-label">
                                <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-6 control-label">상한규격</label>
                            <div class="col-sm-6 control-label">
                                <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-6 control-label">하한규격</label>
                            <div class="col-sm-6 control-label">
                                <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;F_WORKTIME;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번" Width="50px" />
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값" Width="150px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="LOTNO" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_NGOKCHK" Caption="NGOKCHK" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" BlockSize="10" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
