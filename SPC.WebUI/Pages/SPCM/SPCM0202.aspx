<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SPCM0202.aspx.cs" Inherits="SPC.WebUI.Pages.SPCM.SPCM0202" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var itlst = [];

        $(document).ready(function () {
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            var treeheight = Math.max(0, parseInt($(document).height() - _hMargin - top - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height - 330);
            devTree.SetHeight(treeheight);
            devGrid2.SetHeight(270);
            hidGridNew.SetText("NEW");
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            devGrid2.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            if (hidGridNew.GetText() == "NEW") devGrid.AddNewRow();
            else {
                hidIns.SetText("INS");
                fn_OnCancelClick();
            }
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
            hidGridParentKey.SetValue('');
            devGrid.PerformCallback();
            hidGridNew.SetText("NEW");
            itlst = [];
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
                    itlst = [];
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
                    itlst = [];
                    devGrid2.PerformCallback();
                }
            }
            if (hidIns.GetText() == "INS") {
                devGrid.AddNewRow();
                hidIns.SetText("");
            }
            fn_RendorTotalCount();

        }

        function fn_OnEndCallback2(s, e) {
            fn_doSetGridEventAction('false');
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
            fn_OnBatchValidate("F_IPQTY", s, e);
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
            //fn_OnControlDisableBox(s.GetEditor('F_SUM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_IPILBONO'), null);
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

        // Grid2 RowDblClick
        function fn_OnRowDblClick(s, e) {
            fn_doSetGridEventAction('true');
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid2.GetRowKey(e.visibleIndex));
            hidGridParentKey.SetValue(rowKeys);
            devGrid.PerformCallback();
            hidGridNew.SetText("");
        }

        // Grid_item RowDblClick
        function fn_OnItemRowDblClick(values) {
            var rowKey = values.split('|');
            fn_OnPopupSPItemIns(rowKey[0], rowKey[1]);

            /*
            if (hidGridNew.GetText() == "NEW") {
                devGrid.AddNewRow();
            }
            else {
                hidIns.SetText("INS");
                fn_OnCancelClick();
            }
            //for (var i = 0; i < devGrid.GetVisibleRowsOnPage();i++)
            devGrid.GetEditor('F_ITCD').SetText(rowKey[0]);
            devGrid.GetEditor('F_ITNM').SetText(rowKey[1]);
            */
        }

        function fn_gridInsItem(rowKeys) {
            var rowKey = rowKeys.split('|');

            for (var i = 0; i <= itlst.length; i++) {
                if (itlst[i] == rowKey[0]) {
                    alert("이미 등록된 품목 입니다.");
                    return;
                }
            }

            devGrid.AddNewRow();
            devGrid.GetEditor('F_ITCD').SetText(rowKey[0]);
            devGrid.GetEditor('F_ITNM').SetText(rowKey[1]);
            devGrid.GetEditor('F_IPQTY').SetText(rowKey[2]);
            devGrid.GetEditor('F_PRICE').SetText(rowKey[3]);
            devGrid.GetEditor('F_CSCD').SetValue(rowKey[4]);

            itlst[itlst.length+1]=rowKey[0];
            
        }

        function fn_typechange(s, e) {
            devgrid_item.PerformCallback();
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
                                <label class="col-sm-1 control-label">입고일자</label>
                                <div class="col-sm-2 control-label">
                                    <ucCTF:Date ID="ucDate" runat="server" SingleDate="true" />
                                </div>
                                <label class="col-sm-2 control-label">창고</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxComboBox ID="ddlStcd" ClientInstanceName="ddlStcd" runat="server" Width="100%"
                                        IncrementalFilteringMode="None" CssClass="NoXButton">
                                    </dx:ASPxComboBox>
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
                                KeyFieldName="F_ITCD" EnableViewState="false" EnableRowsCache="false" OnDataBound="devGrid_DataBound"  OnCellEditorInitialize="devGrid_CellEditorInitialize"
                                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                <SettingsBehavior AllowSort="false" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback1" CallbackError="fn_OnCallbackError" BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                                <Templates>
                                    <StatusBar>
                                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                                    </StatusBar>
                                </Templates>
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="F_IPILBONO" Caption="입고번호" Width="100px"  CellStyle-HorizontalAlign="Center" />
                                    <dx:GridViewDataTextColumn FieldName="F_ITCD" Caption="품목코드" Width="90px"  CellStyle-HorizontalAlign="Center" />
                                    <dx:GridViewDataColumn FieldName="F_ITNM" Caption="품목명" Width="220px" CellStyle-HorizontalAlign="Left" />
                                    <dx:GridViewDataColumn FieldName="F_STAND" Caption="규격" Width="90px" CellStyle-HorizontalAlign="Center" />
                                    <dx:GridViewDataTextColumn FieldName="F_IPQTY" Caption="입고수량" Width="80px" CellStyle-HorizontalAlign="Right">
                                        <PropertiesTextEdit NullText="Number" ConvertEmptyStringToNull="True">
                                            <ClientSideEvents KeyUp="fn_OnMeasureKeyUp" KeyPress="fn_OnMeasureKeyPress"/>
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_PRICE" Caption="단가" Width="80px" CellStyle-HorizontalAlign="Right">
                                        <PropertiesTextEdit NullText="Number" ConvertEmptyStringToNull="True">
                                            <ClientSideEvents KeyUp="fn_OnMeasureKeyUp" KeyPress="fn_OnMeasureKeyPress"/>
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn FieldName="F_SUM" Caption="금액" Width="90px" CellStyle-HorizontalAlign="Right" />
                                    <dx:GridViewDataComboBoxColumn FieldName="F_CSCD" Name="F_CSCD" Caption="거래처" Width="150px" PropertiesComboBox-ConvertEmptyStringToNull="false" Settings-AllowSort="True" />
                                    <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고" CellStyle-HorizontalAlign="Center" />
                                </Columns>
                            </dx:ASPxGridView>
                            
                        </div>
                    <div style="height:10px;"></div>
                    <div class="search">
                        <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                            <div class="form-group">
                                <label class="col-sm-1 control-label">입고일자</label>
                                <div class="col-sm-4 control-label">
                                    <ucCTF:Date1 ID="ucDate1" runat="server" />
                                </div>
                                <div class="col-sm-5 control-label"></div>
                                <div class="col-sm-1 control-label">
                                    <button class="btn btn-sm btn-success" style="width: 100%; padding: 3px 0px;" onclick="fn_OnSearchClick(); return false;" >
                                    <i class="i i-file-plus "></i>
                                    <span class="text">조회</span>
                                </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="content">
                        <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_IPILBONO;F_ITCD" EnableViewState="false" EnableRowsCache="false" 
                            OnCustomCallback="devGrid2_CustomCallback" 
                            OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"  
                            >
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible"/>
                            <SettingsBehavior AllowSort="false"  />
                            <SettingsPager Mode="ShowAllRecords" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_STNM"       Caption="창고"          Width="100px"/>
                                <dx:GridViewDataColumn FieldName="F_IPDT"       Caption="입고일자"      Width="100px"       CellStyle-HorizontalAlign="Center" />
                                <dx:GridViewDataColumn FieldName="F_IPILBONO"   Caption="입고번호"      Width="100px"       CellStyle-HorizontalAlign="Center" />
                                <dx:GridViewDataColumn FieldName="F_ITCD"       Caption="품목코드"      Width="90px"        CellStyle-HorizontalAlign="Center" />
                                <dx:GridViewDataColumn FieldName="F_ITNM"       Caption="품목명"      Width="220px"       CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataColumn FieldName="F_STAND"      Caption="규격"          Width="90px"        CellStyle-HorizontalAlign="Center" />
                                <dx:GridViewDataTextColumn FieldName="F_IPQTY"  Caption="입고수량"      Width="80px"        CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatInEditMode="true" PropertiesTextEdit-DisplayFormatString="n0" PropertiesTextEdit-MaskSettings-Mask="n0"  />
                                <dx:GridViewDataColumn FieldName="F_PRICE"      Caption="단가"          Width="80px"        CellStyle-HorizontalAlign="Right" />
                                <dx:GridViewDataColumn FieldName="F_SUM"        Caption="금액"          Width="90px"        CellStyle-HorizontalAlign="Right" />
                                <dx:GridViewDataColumn FieldName="F_CSNM"       Caption="거래처"        Width="100px"       CellStyle-HorizontalAlign="Right" />
                                <dx:GridViewDataColumn FieldName="F_REMARK"     Caption="비고"                              CellStyle-HorizontalAlign="Center" />
                            </Columns>                
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid2" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
                    </div>
                    <div class="paging"></div>
                </div>
            </td>
         </tr>
     </table>
   
</asp:Content>
