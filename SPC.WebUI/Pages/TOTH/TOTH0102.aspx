<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TOTH0102.aspx.cs" Inherits="SPC.WebUI.Pages.TOTH.TOTH0102" %>
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
            //devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            //selectedKeys = devGrid.GetSelectedKeysOnPage();

            //// 1 Row 반드시 선택
            //if (selectedKeys.length <= 0) {
            //    alert('수정할 데이타를 선택하세요!!');
            //    return false;
            //}

            //for (var i = 0; i < selectedKeys.length ; i++) {
            //    devGrid.StartEditRowByKey(selectedKeys[i]);
            //}
        }

        // 저장
        function fn_OnSaveClick() {
            //if (!devGrid.batchEditApi.HasChanges())
            //    alert('변경된 사항이 없습니다');
            //else
            //    devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            //devGrid.UnselectAllRowsOnPage();
            //devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            //selectedKeys = devGrid.GetSelectedKeysOnPage();

            //// 1 Row 반드시 선택
            //if (selectedKeys.length <= 0) {
            //    alert('삭제할 데이타를 선택하세요!!');
            //    return false;
            //}

            //if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            //for (var i = 0; i < selectedKeys.length ; i++) {
            //    devGrid.DeleteRowByKey(selectedKeys[i]);
            //}
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
            //fn_OnBatchValidate("F_BANCD", s, e);
            //fn_OnBatchValidate("F_BANNM", s, e);

            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    var editor = s.GetEditor('F_BANCD');
            //    fn_OnControlDisableBox(editor, null);
            //} else {
            //    var editor = s.GetEditor('F_BANCD');
            //    fn_OnControlEnableBox(editor, null);
            //}
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검사일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" />
                    </div>
                    <div class="col-sm-8">
                    </div>
                </div>                
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATETIME;F_ITEMCD;F_ITEMNM;F_JPGFILENAME;F_PRONAME;F_COLOR" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible"  HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_WORKDATETIME" Caption="검사일시" Width="130" PropertiesTextEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss"/>
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품번" Width="140" />
                    <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="품명" Width="180" />
                    <dx:GridViewDataTextColumn FieldName="F_JPGFILENAME" Caption="파일명" Width="240" />
                    <dx:GridViewDataTextColumn FieldName="F_PRONAME" Caption="공정명" Width="350" />
                    <dx:GridViewDataTextColumn FieldName="F_COLOR" Caption="" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" Visible="false" />
        </div>
    </div>
</asp:Content>
