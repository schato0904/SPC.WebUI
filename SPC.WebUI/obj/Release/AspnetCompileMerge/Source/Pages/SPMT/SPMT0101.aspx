<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SPMT0101.aspx.cs" Inherits="SPC.WebUI.Pages.SPMT.SPMT0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
            // 시작일, 종료일 기본값
            //var date = new Date();
            //F_WORKSD.SetDate(addDays(date, - 1));
            //F_WORKED.SetDate(date);
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

            if (ASPxClientControl.Cast("hidUCITEMCD").GetText() == "") {
                alert("품목을 입력하세요!!");
                ASPxClientControl.Cast("txtUCITEMCD").Focus();
                return false;
            }

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

        // 출하
        function fn_OnShipmentClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 무조건 선택
            if (selectedKeys.length <= 0) {
                alert('출하 할 데이타를 선택하세요!!');
                return false;
            }

            // 최대 선택 5개까지
            if (selectedKeys.length > 5) {
                alert('출하데이타는 최대 5개까지 선택할 수 있습니다!!');
                return false;
            }

            fn_OnPopupShipment();
        }

        // 출하용 그리드를 위한 키값 전달
        function fn_OnGetSelectedKeys() {
            return devGrid.GetSelectedKeysOnPage();
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" TodayFromDate="true" />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Item ID="ucItem" runat="server" targetCtrls="ddlWORK" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE;ddlWORK" />
                    </div>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" targetCtrls="ddlWORK" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Work ID="ucWork" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;F_BANCD;F_LINECD;F_ITEMCD;F_WORKCD;F_TSERIALNO" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
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
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="측정일자" Width="80px">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="120px" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="180px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="130px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px" />
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO" Caption="시료군" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="LOT번호" Width="140px" />
                    <dx:GridViewBandColumn Name="F_COUNT" Caption="수량">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_GOODQTY" Caption="OK" Width="40px" />
                            <dx:GridViewDataColumn FieldName="F_REJQTY" Caption="NG" Width="40px" />
                            <dx:GridViewDataColumn FieldName="F_SHARTCNT" Caption="관리이탈" Width="70px" />
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="F_MODELNM" Caption="모델명" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_QC" Caption="검사원" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_TAGAKNO" Caption="타각번호" Width="140px" />

                    <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MODELCD" Caption="모델코드" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
