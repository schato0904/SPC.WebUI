<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0306.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0306" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
            //devGrid.PerformCallback();
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
            if (hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) {
                alert("품목을 선택하세요.");
                return false;
            }

            if (hidUCWORKPOPCD.GetValue() == "" || hidUCWORKPOPCD.GetValue() == null) {
                alert('공정을 선택하세요.');
                return false;
            }

            fn_doSetGridEventAction('true');
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else {
                if (!confirm('※ ' + schF_MACHINE.GetText() + '(으)로 매핑정보를 저장 하시겠습니까?')) { return false; }
                else
                    devGrid.UpdateEdit();
            }
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
            btnExport.DoClick();
        }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else if (s.cpResultCode == '2') {
                    fn_doSetGridEventAction('true');
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                    devGrid.PerformCallback();
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
            //fn_OnBatchValidate("F_MEAINSPCD", s, e);
            //fn_OnBatchValidate("F_INSPDETAIL", s, e);
            //fn_OnBatchValidate("F_STANDARD", s, e);
            //fn_OnBatchValidate("F_MAX", s, e);
            //fn_OnBatchValidate("F_MIN", s, e);
            //fn_OnBatchValidate("F_ROW", s, e);
            //fn_OnBatchValidate("F_COL", s, e);

            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                //                var editor = s.GetEditor('F_INSPDETAIL');
                fn_OnControlDisableBox(s.GetEditor('F_INSPDETAIL'), null);
                fn_OnControlDisableBox(s.GetEditor('F_STANDARD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MAX'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MIN'), null);
                fn_OnControlDisableBox(s.GetEditor('F_BANCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_LINECD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_SERIALNO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MEAINSPCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MACHINE'), null);
                //var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));

            } else {
                fn_OnControlDisableBox(s.GetEditor('F_INSPDETAIL'), null);
                fn_OnControlDisableBox(s.GetEditor('F_STANDARD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MAX'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MIN'), null);
                fn_OnControlDisableBox(s.GetEditor('F_BANCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_LINECD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_SERIALNO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MEAINSPCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MACHINE'), null);

            }
        }
    </script>
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">

        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">

                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-4">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>

                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>

                    <%--<label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Work ID="ucWork" runat="server" />
                    </div>--%>

                    <label class="col-sm-1 control-label">측정기</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="schF_MACHINE" ClientInstanceName="schF_MACHINE" runat="server">
                            <Items>
                                <dx:ListEditItem Value="Dukin" Text="Dukin" Selected="true" />
                                <dx:ListEditItem Value="Carl Zeiss" Text="Carl Zeiss" />
                                <dx:ListEditItem Value="Hexagon" Text="Hexagon" />
                            </Items>
                        </dx:ASPxComboBox>

                    </div>

                </div>
            </div>
        </div>


        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />

            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false"
                KeyFieldName="F_INSPDETAIL;F_BANCD;F_LINECD;F_WORKCD;F_ITEMCD;F_SERIALNO" EnableViewState="false" EnableRowsCache="false"
                OnBatchUpdate="devGrid_BatchUpdate" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize"
                OnAutoFilterCellEditorInitialize="devGrid_AutoFilterCellEditorInitialize" Width="100%">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" ShowFilterRow="false" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count ID="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>


                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목명" Width="25%">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataTextColumn FieldName="F_MACHINE" Caption="측정기" Width="15%">
                        <PropertiesTextEdit Style-HorizontalAlign="Center"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="규격" Width="12%">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesTextEdit Style-HorizontalAlign="Right"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="상한값" Width="12%">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesTextEdit Style-HorizontalAlign="Right"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="하한값" Width="12%">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesTextEdit Style-HorizontalAlign="Right"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="F_ROW" Caption="검사항목행" Width="12%">
                        <PropertiesTextEdit Style-HorizontalAlign="Center"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="F_COL" Caption="데이터열" Width="12%">
                        <PropertiesTextEdit Style-HorizontalAlign="Center"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="F_BANCD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <%--<dx:GridViewDataColumn FieldName="F_INSPCD" Visible="false" />--%>
                </Columns>

            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
