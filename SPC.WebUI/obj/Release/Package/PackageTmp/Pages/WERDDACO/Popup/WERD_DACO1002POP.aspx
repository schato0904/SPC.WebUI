<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD_DACO1002POP.aspx.cs" Inherits="SPC.WebUI.Pages.WERDDACO.Popup.WERD_DACO1002POP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });


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
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else
                devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {

        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {            
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
            fn_OnBatchValidate("F_BANCD", s, e);
            fn_OnBatchValidate("F_BANNM", s, e);

            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlDisableBox(editor, null);
            } else {
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlEnableBox(editor, null);
            }
        }

        function fn_OnLINEValueChanged(s, e) {
            var val = s.GetValue();
            hidCOMP.SetValue(val);
            LINEPerFormCallbackTarget();
        }
        function fn_OnLINEEndCallback(s, e) {
            isLINEEndCallback = parent.parent.isTreeLINESetup;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="control-label" style="float: left; width: 10%;">
                        <dx:ASPxTextBox ID="txtFROMDT" ClientInstanceName="txtFROMDT" runat="server" Text="" Width="95%" ClientEnabled="false" />
                    </div>
                    <div class="control-label" style="float: left; width: 10%;">
                        <dx:ASPxTextBox ID="txtTODT" ClientInstanceName="txtTODT" runat="server" Text="" Width="99%" ClientEnabled="false" />
                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-1 control-label">업체명</label>
                    <div class="col-sm-2">
                        <dx:ASPxTextBox ID="hidCOMP" ClientInstanceName="hidCOMP" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlCOMP" ClientInstanceName="ddlCOMP" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton"
                            TextField="F_LINENM" ValueField="F_LINECD" OnDataBound="ddlLINE_DataBound" OnCallback="ddlLINE_Callback" ClientEnabled="false">
                            <ClientSideEvents ValueChanged="fn_OnLINEValueChanged" Init="fn_OnControlDisable" EndCallback="fn_OnLINEEndCallback" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-1 control-label">품목명</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Text="" ClientEnabled="false" />
                    </div>
                </div>
            </div>
        </div>

        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="일자" Width="12%" CellStyle-HorizontalAlign="Center" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="12%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="12%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="12%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_COMPANYNM" Caption="업체명" Width="12%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_TYPE" Caption="불량구분" Width="10%" CellStyle-HorizontalAlign="Center" />
                    <dx:GridViewDataColumn FieldName="F_ERRORNM" Caption="불량유형" Width="10%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_NGCOUNT" Caption="불량수량" Width="10%" CellStyle-HorizontalAlign="Right" />
                    <dx:GridViewDataColumn FieldName="F_NGTIME" Caption="수리시간(초)" Width="10%" CellStyle-HorizontalAlign="Right" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>            
        </div>
    </div>
</asp:Content>
