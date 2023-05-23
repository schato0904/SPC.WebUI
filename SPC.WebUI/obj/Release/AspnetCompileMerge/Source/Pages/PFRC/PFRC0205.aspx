<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PFRC0205.aspx.cs" Inherits="SPC.WebUI.Pages.PFRC.PFRC0205" %>
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

        function fn_OnCustomButtonClick(s, e) {
            if (e.buttonID != 'btnActivate') return;
            devGrid.GetRowValues(e.visibleIndex, 'F_IDX;F_ISACTIVATE', fn_OnCommit);
        }

        function fn_OnCommit(values) {
            var msg = '선택한 현장측정프로그램의 사용을 {0} 하겠습니까?';
            if (values[1] == false) {
                msg = msg.replace('{0}', '승인');
            } else {
                msg = msg.replace('{0}', '취소');
            }

            if (!confirm(msg))
                return false;
            else
                devGrid.PerformCallback(values.join('|'));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_IDX;F_COMPCD;F_MAC" EnableViewState="false" EnableRowsCache="false"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCustomCallback="devGrid_CustomCallback" OnCustomButtonInitialize="devGrid_CustomButtonInitialize">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" CustomButtonClick="fn_OnCustomButtonClick" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_IDX" Caption="순번" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="200px" />
                    <dx:GridViewDataColumn FieldName="F_MAC" Caption="MAC주소" />
                    <dx:GridViewDataColumn FieldName="F_REQUSER" Caption="신청인명" />
                    <dx:GridViewDataDateColumn FieldName="F_REQDT" Caption="신청일시" Width="180px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" />
                    <dx:GridViewDataColumn FieldName="F_RESUSER" Caption="최종승인(중단)자" Width="150px" />
                    <dx:GridViewDataDateColumn FieldName="F_RESDT" Caption="최종승인(중단)일시" Width="180px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" />
                    <dx:GridViewDataColumn FieldName="F_ISACTIVATE" Caption="승인상태" Width="80px" />
                    <dx:GridViewCommandColumn Caption="상태변경" Width="80px"  >
                        <CustomButtons>                            
                            <dx:GridViewCommandColumnCustomButton ID="btnActivate" Text="승인" Visibility="BrowsableRow" Styles-Native="True" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn> 
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>