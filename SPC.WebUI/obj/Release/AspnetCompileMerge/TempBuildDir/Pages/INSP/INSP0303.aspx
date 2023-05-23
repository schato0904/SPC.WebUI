<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0303.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.INSP0303" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
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
            devGrid.PerformCallback(fn_GetCastSelectedItemValue('srcF_REPORTTP'));
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

        function fn_OnRowDblClick(s, e) {
            // F_QWK13IMID;F_REPORTTP;F_ITEMCD;F_WORKCD;F_WORKDATE;F_LOTNO;F_MOLDNO
            var devGrid = ASPxClientGridView.Cast(s);
            var rowKey = devGrid.GetRowKey(e.visibleIndex).split('|');
            var data = '{';
            data += '"F_QWK13IMID":"' + rowKey[0] + '"';
            data += ',"F_REPORTTP":"' + rowKey[1] + '"';
            data += ',"F_ITEMCD":"' + rowKey[2] + '"';
            data += ',"F_WORKCD":"' + rowKey[3] + '"';
            data += ',"F_WORKDATE":"' + rowKey[4] + '"';
            data += '}';
            fn_OnPopupINSP0303(data);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>              
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                    <label class="col-sm-1 control-label">성적서분류</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="schF_REPORTTP" ClientInstanceName="schF_REPORTTP" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" NullText="선택하세요" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_QWK13IMID;F_REPORTTP;F_ITEMCD;F_WORKCD;F_WORKDATE;F_LOTNO;F_MOLDNO" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_QWK13IMID" Caption="성적서번호"  Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_REPORTTP" Caption="성적서분류"  Width="150px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_REPORTDATE" Caption="생성일"  Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드"  Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="200px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="300px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No."  Width="100px"  />
                    <dx:GridViewDataColumn FieldName="F_MOLDNO" Caption="작지번호"  Width="120px"  />
                    <dx:GridViewDataColumn FieldName="F_CONFIRM" Caption="등록상태"  Width="80px"  />
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>