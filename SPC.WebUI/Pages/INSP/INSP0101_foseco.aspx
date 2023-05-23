<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0101_foseco.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.INSP0101_foseco" %>
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

            //if (ASPxClientControl.Cast("hidUCITEMCD").GetText() == "") {
            //    alert("품목을 입력하세요!!");
            //    ASPxClientControl.Cast("txtUCITEMCD").Focus();
            //    return false;
            //}

            //if (ASPxClientControl.Cast("hidUCWORKPOPCD").GetText() == "") {
            //    alert("공정을 입력하세요!!");
            //    ASPxClientControl.Cast("txtUCWORKPOPCD").Focus();
            //    return false;
            //}

            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnInspectionClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('발행할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }

            devGrid.UpdateEdit();

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

                    devGrid.PerformCallback();
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

        
        // 출하용 그리드를 위한 키값 전달
        function fn_OnGetSelectedKeys() {
            return devGrid.GetSelectedKeysOnPage();
        }

        // 검사성적서 오픈
        function fn_OnCustomButtonClick(s, e) {

            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var oParams = rowKeys.split('|');
            
            //pPage = rootURL + 'Pages/INSP/Popup/INSP0101REPORT_foseco.aspx' +
            //            '?TITLE=검사성적서' +
            //            '&pPARAMS=' + rowKeys;
            //fn_OnPopupOpen(pPage, '820', '0');

            if (oParams[10] == "미발행" || oParams[10] == null) {
                alert("성적서 발행 후 출력해 주세요.!!")
            } else {
                if (oParams[10] == "<%=gsUSERID%>") {
                    pPage = rootURL + 'Pages/INSP/Popup/INSP0101REPORT_foseco.aspx' +
                        '?TITLE=검사성적서' +
                        '&pPARAMS=' + rowKeys;
                    fn_OnPopupOpen(pPage, '820', '0');
                } else {
                    alert("본인이 발행한 성적서만 출력 할 수 있습니다.!!")
                }
            }
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
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" TodayFromDiff="90" />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD;F_WORKDATE;F_WORKTIME;F_ITEMCD;F_WORKCD;F_TSERIALNO;F_LOTNO;F_REPORT;F_NUMBER;F_ISSUER;F_REPORTDATE" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" 
                OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" 
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                OnBatchUpdate="devGrid_BatchUpdate"
                >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" CustomButtonClick="fn_OnCustomButtonClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn Caption="검사성적서" Width="80px" >
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton Text="출력" ID="btnShipment" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="측정일자" Width="80px" EditFormSettings-Visible="False">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_PARTNERNM" Caption="거래처명" Width="200px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="100px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="110px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="150px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="100px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO" Caption="시료군" Width="70px" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="False" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="LOT번호" Width="100px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
                    <%--<dx:GridViewDataColumn FieldName="F_FIRSTITEMNM" Caption="초품구분"  Width="80px" />--%>
                    <%--<dx:GridViewBandColumn Name="F_COUNT" Caption="수량">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_GOODQTY" Caption="OK" Width="40px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="F_REJQTY" Caption="NG" Width="40px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="F_SHARTCNT" Caption="관리이탈" Width="70px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
                        </Columns>
                    </dx:GridViewBandColumn>--%>
                    <%--<dx:GridViewDataColumn FieldName="F_MODELNM" Caption="모델명" Width="80px" />--%>
                    <dx:GridViewBandColumn Caption="수량">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_QTY" Caption="생산수" Width="60px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="F_INSPQTY" Caption="검사수" Width="60px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="F_BADQTY" Caption="불량수" Width="60px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="F_ISSUER" Caption="발행인" Width="80px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
                    <dx:GridViewDataColumn FieldName="F_REPORTDATE" Caption="발행일자" Width="150px" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="False" />
                    <%--<dx:GridViewDataColumn FieldName="F_TAGAKNO" Caption="타각번호" Width="140px" />--%>

                    <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MODELCD" Caption="모델코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_EXTCD" Caption="모델코드" Visible="false" />

                    <dx:GridViewDataColumn FieldName="F_REPORT" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>