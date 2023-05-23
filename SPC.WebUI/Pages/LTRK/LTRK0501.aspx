<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRK0501.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.LTRK0501" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_SetTextValue('hidUCFROMDT', '<%=sYearFirstDate%>');
            fn_SetDate('txtFROMDT', convertDateString('<%=sYearFirstDate%>'));
            fn_OnSearchClick();
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

        function fn_OnClose(key) {
            if (!confirm(key + ' 의 마감을 진행하겠습니까?\r계속진행하려면 확인을 누르세요!'))
                return;
            else
                devGrid.PerformCallback(key + '|1');
        }

        function fn_OnCancel(key) {
            if (!confirm(key + ' 의 마감을 취소하겠습니까?\r계속진행하려면 확인을 누르세요!'))
                return;
            else
                devGrid.PerformCallback(key + '|0');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="contentTable">
                <colgroup>
                    <col style="width:80px;" />
                    <col style="width:80px;" />
                    <col style="width:20px;" />
                    <col style="width:80px;" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">월</td>
                    <td class="tdInput tdContentR">
                        <ucCTF:Date runat="server" id="ucDate" MonthOnly="true" SingleDate="true" />
                    </td>
                    <td class="tdInput tdContentLR text-center">~</td>
                    <td class="tdInput tdContentLR">
                        <dx:ASPxTextBox ID="srcF_EMONTH" ClientInstanceName="srcF_EMONTH" runat="server" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlDisableBox" GotFocus="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdInput tdContentL"></td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_YYYYMM" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_YYYYMM" Caption="마감월" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_ISREALCLOSE" Caption="마감구분" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_ISCLOSE" Caption="마감가능" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_ISCANCEL" Caption="취소가능" Visible="false" />
                    <dx:GridViewDataDateColumn FieldName="F_UPDDT" Caption="마감실행일시" Width="200px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" />
                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="작업자" Width="150px" />
                    <dx:GridViewDataTextColumn Caption="마감진행" UnboundType="String" Width="120px">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnClose" runat="server" Text="마감진행" OnInit="btnClose_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="마감취소" UnboundType="String" Width="120px">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnCancel" runat="server" Text="마감취소" OnInit="btnCancel_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="이력보기" UnboundType="String" Width="120px">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnLink" runat="server" Text="이력보기" OnInit="btnLink_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>