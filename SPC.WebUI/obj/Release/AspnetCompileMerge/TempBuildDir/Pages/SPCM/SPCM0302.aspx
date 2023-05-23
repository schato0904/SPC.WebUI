<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SPCM0302.aspx.cs" Inherits="SPC.WebUI.Pages.SPCM.SPCM0302" %>
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
            devTree.SetHeight(height + searchHeight);
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
            if (!devGrid.batchEditApi.HasChanges()) {
                alert('변경된 사항이 없습니다');
            }
            else {
                devGrid.UpdateEdit();
            }
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
            devGrid.PerformCallback();
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }
        }

        // 인쇄
        function fn_OnPrintClick() {
        }

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

        // Grid End Callback
        function fn_OnEndCallback1(s, e) {
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
            if (hidIns.GetText() == "INS") {
                devGrid.AddNewRow();
                hidIns.SetText("");
            }
            fn_RendorTotalCount();

        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_ITCD", s, e);
            fn_OnBatchValidate("F_ITNM", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            editRowIndex = parseInt(e.visibleIndex, 10);
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var editor = s.GetEditor('F_ITCD');
                fn_OnControlDisableBox(editor, null);
            } else {
                var editor = s.GetEditor('F_ITCD');
                fn_OnControlEnableBox(editor, null);
            }
            
            fn_OnControlAddAttr(s.GetEditor('F_ITCD'), null, 'ondblclick', "fn_OnPopupSPItemSearch('FORM','')");
            fn_OnControlDisableBox(s.GetEditor('F_ITNM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_STAND'), null);
            fn_OnControlDisableBox(s.GetEditor('F_OSQTY'), null);
            fn_OnControlDisableBox(s.GetEditor('F_NQTY'), null);
        }

        // 검색된 품목 세팅
        function fn_OnSettingItem(CODE, TEXT) {
            devGrid.StartEditRow(editRowIndex);
            devGrid.GetEditor('F_ITCD').SetText(CODE);
            devGrid.GetEditor('F_ITNM').SetText(TEXT);
        }

        // KeyUpEvent
        function fn_OnMeasureKeyUp(s, e) {
          fn_RemoveHangul(s, e);
        }

        // KeyPressEvent
        function fn_OnMeasureKeyPress(s, e) {
          fn_ValidateOnlyFloat(s, e);
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_OnUCITEMLostFocus(s, e) {
            if (!s.GetText() || s.GetText() == '') {
                txtUCITEMNM.SetValue('');
                txtUCITEMNM.SetText('');
                txtUCITEMNM.GetMainElement().title = '';
                hidUCITEMCD.SetValue('');
                hidUCITEMCD.SetText('');
                return;

            } else
                ITEMCallback.PerformCallback();
        }

        function fn_OnUCITEMKeyUp(s, e) {
            fn_SetTextValue('hidUCITEMCD', s.GetValue());
        }

        function fn_OnUCITEMEndCallback(s, e) {
            var ITEMCD = s.cpITEMCD;
            var ITEMNM = s.cpITEMNM;

            if (ITEMCD != '' && ITEMNM != '') {
                hidUCITEMCD.SetValue(ITEMCD);
                hidUCITEMCD.SetText(ITEMCD);
                txtUCITEMNM.SetValue(ITEMNM);
                txtUCITEMNM.SetText(ITEMNM);
                txtUCITEMNM.GetMainElement().title = ITEMNM;

            } else {
                hidUCITEMCD.SetValue('');
                hidUCITEMCD.SetText('');
                txtUCITEMNM.SetValue('');
                txtUCITEMNM.SetText('');
                txtUCITEMNM.GetMainElement().title = '';
            }
            isITEMEndCallback = parent.parent.isTreeITEMSetup;
        }

        // 검색된 품목 세팅
        function fn_OnSettingItem(CODE, TEXT) {
            txtUCITEMCD.SetText(CODE);
            txtUCITEMNM.SetText(TEXT);
            hidUCITEMCD.SetValue(CODE);
            hidUCITEMCD.SetText(CODE);
        }
        function fn_OnNodeDblClick(s, e) {
            var fields = 'F_LEVEL;F_ITCD;F_CODENM;';
            if (e.nodeKey.split('|').length > 1) {
                devTree.GetNodeValues(e.nodeKey, fields, OnGetNodeValues);
            }
        }

        function OnGetNodeValues(values) {
            var vals = values.splice(1, values.length).join('|');
            if (values[0] == '2') {
                //fn_OnItemRowDblClick(vals);
                fn_gridInsItem(vals);
            }
        }

        function fn_gridInsItem(rowKeys) {
            var rowKey = rowKeys.split('|');

            txtUCITEMCD.SetText(rowKey[0]);
            txtUCITEMNM.SetText(rowKey[1]);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <table class="layerTable">
    <colgroup>
        <col style="width:350px;" />
        <col />
    </colgroup>
    <tr>
        <td>
            <div class="container">
                <div class="content">
                    <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F; margin-top:9px;">
                    <dx:ASPxTreeList ID="devTree" ClientInstanceName="devTree" runat="server" AutoGenerateColumns="false" Width="100%"
                        KeyFieldName="F_CODE" ParentFieldName="F_GROUPCD"
                        OnCustomCallback="devTree_CustomCallback" OnDataBound="devTree_DataBound">
                        <Columns>
                            <dx:TreeListDataColumn FieldName="F_CODENM" Caption="코드명" VisibleIndex="0" Width="150px" />
                            <dx:TreeListDataColumn FieldName="F_LEVEL" Visible="false" />
                            <dx:TreeListDataColumn FieldName="F_ITCD" Visible="false" />
                        </Columns>
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" ScrollableHeight="0" ShowColumnHeaders="false" />
                        <SettingsBehavior ExpandCollapseAction="NodeDblClick" AllowSort="false" AllowFocusedNode="True" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" NodeDblClick="fn_OnNodeDblClick" />
                    </dx:ASPxTreeList>
                    </div>
               </div>
            </div>
        </td>
        <td>
             <div class="container_r">
                 <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">창고</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="ddlStcd" ClientInstanceName="ddlStcd" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="hidITEMCD" ClientInstanceName="hidUCITEMCD" runat="server" ClientVisible="false" />
                        <div class="control-label" style="float: left; width: 37%;">
                            <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtUCITEMCD" runat="server" Width="100%"
                                OnInit="txtITEMCD_Init">
                                <ClientSideEvents LostFocus="fn_OnUCITEMLostFocus" KeyUp="fn_OnUCITEMKeyUp" />
                            </dx:ASPxTextBox>
                        </div>
                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div class="control-label" style="float: left; width: 52%;">
                            <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtUCITEMNM" runat="server" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </div>
                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div class="control-label" style="float: left; width: 9%;">
                            <button class="btn btn-default btn-xs" data-toggle="button" title="품목조회" onclick="fn_OnPopupUCItemSearch(); return false;">
                                <i class="i i-popup text"></i>
                                <i class="i i-popup text-active text-danger"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            <div class="content">
                <dx:ASPxTextBox ID="hidGridNew" ClientInstanceName="hidGridNew" runat="server" ClientVisible="false" />
                <dx:ASPxTextBox ID="hidIns" ClientInstanceName="hidIns" runat="server" ClientVisible="false" />
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridParentKey" ClientInstanceName="hidGridParentKey" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false"  Width="100%"
                    KeyFieldName="F_STCD;F_ITCD" EnableViewState="false" EnableRowsCache="false" OnDataBound="devGrid_DataBound"  OnCellEditorInitialize="devGrid_CellEditorInitialize"
                    OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                    <SettingsBehavior AllowSort="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback1" CallbackError="fn_OnCallbackError" BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="F_STCD" Caption="창고코드" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataTextColumn FieldName="F_STNM" Caption="창고명" Width="100px"  CellStyle-HorizontalAlign="Center" />
                        <dx:GridViewDataTextColumn FieldName="F_ITCD" Caption="품목코드" Width="100px"  CellStyle-HorizontalAlign="Center" />
                        <dx:GridViewDataColumn FieldName="F_ITNM" Caption="품목명" Width="250px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_STAND" Caption="규격" Width="100px" CellStyle-HorizontalAlign="Center" />
                        <dx:GridViewDataTextColumn FieldName="F_OSQTY" Caption="적정재고" Width="90px" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataTextColumn FieldName="F_NQTY" Caption="현재고" Width="90px" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataTextColumn FieldName="F_BQTY" Caption="부족수량" Width="90px" CellStyle-HorizontalAlign="Right" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고" Width="300px" CellStyle-HorizontalAlign="Center" />
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxCallback ID="ITEMCallback" ClientInstanceName="ITEMCallback" runat="server" OnCallback="ITEMCallback_Callback">
                        <ClientSideEvents EndCallback="fn_OnUCITEMEndCallback" />
                    </dx:ASPxCallback>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            </div>
            </div>
        </td>
     </tr>
 </table>
</asp:Content>
