<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SPCM0201.aspx.cs" Inherits="SPC.WebUI.Pages.SPCM.SPCM0201" %>
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
            devGrid.SetHeight(height-10);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            if (hidGridCount.TEXT == "Y") {
                devGrid.AddNewRow();
            }
            else {
                alert("기초재고등록은 한 번만 할 수 있습니다.");
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

            if (devGrid.GetVisibleRowsOnPage() == 0) {

                hidGridCount.TEXT = "Y";
            }
            else {
                hidGridCount.TEXT = "N";
            }

            if (devGrid.batchEditApi.GetCellValue(1, 'F_ENDYN') == 'Y') {
                document.getElementById('btn_end').style.display = 'none';
                document.getElementById('txt_end').style.display = '';
                
            }
            else {
                document.getElementById('btn_end').style.display = '';
                document.getElementById('txt_end').style.display = 'none';
            }
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
            
            //fn_OnControlAddAttr(s.GetEditor('F_ITCD'), null, 'ondblclick', "fn_OnPopupSPItemSearch('FORM','')");
            //fn_OnControlDisableBox(s.GetEditor('F_ITTYPENM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_ITNM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_STAND'), null);
            fn_OnControlDisableBox(s.GetEditor('F_STNM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_ITTYPENM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_ENDYN'), null);

            if (devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_ENDYN') == 'Y'){
                fn_OnControlDisableBox(s.GetEditor('F_PRICE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_CSCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_REMARK'), null);
                fn_OnControlDisableBox(s.GetEditor('F_NQTY'), null);
                fn_OnControlDisableBox(s.GetEditor('F_SUM'), null);
            }
            else {
                fn_OnControlEnableBox(s.GetEditor('F_PRICE'), null);
                fn_OnControlEnableBox(s.GetEditor('F_CSCD'), null);
                fn_OnControlEnableBox(s.GetEditor('F_REMARK'), null);
                fn_OnControlEnableBox(s.GetEditor('F_NQTY'), null);
                fn_OnControlEnableBox(s.GetEditor('F_SUM'), null);
            }
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

        function fn_stchange(s, e) {
            devGrid.PerformCallback();
        }

        // 검사항목, 공정 콜백처리
        function fn_OndevCallback(s, e) {
            alert(s.cpResultMsg);
            devGrid.PerformCallback();
        }

        function fn_end(s, e) {
            
            if (!confirm('선택한 창고의 기초재고를 마감하시겠습니까?\r마감후 더이상 등록 할 수 없습니다.')) { return false; }
            devCallback.PerformCallback();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">입고일자</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Date ID="ucDate" runat="server" SingleDate="true" />
                    </div>
                    <label class="col-sm-1 control-label">창고</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="ddlStcd" ClientInstanceName="ddlStcd" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents SelectedIndexChanged="fn_stchange" />
                        </dx:ASPxComboBox>
                    </div>
                    <div class="col-sm-3 control-label">
                        <div id="txt_end" style="display:none;">※ 기초재고등록이 마감 되었습니다.</div>
                    </div>
                    <div class="col-sm-1 control-label">
                    </div>
                    <div class="col-sm-1 control-label">
                    <button id="btn_end" class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px; display:none" onclick="fn_end(); return false;">
                        <i class="i i-file-plus "></i>
                        <span class="text">재고마감</span>
                    </button>
                    </div>
                </div>
            </div>
            <div>※ 기초재고 마감 시 등록 할 수 없습니다.</div>
        </div>
        
        <div class="content">
            <dx:ASPxTextBox ID="hidGridCount" ClientInstanceName="hidGridCount" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false"  Width="100%"
                KeyFieldName="F_STCD;F_ITCD" EnableViewState="false" EnableRowsCache="false" OnDataBound="devGrid_DataBound"  OnCellEditorInitialize="devGrid_CellEditorInitialize"
                OnBatchUpdate="devGrid_BatchUpdate" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
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
                    <dx:GridViewDataTextColumn FieldName="F_COMPCD" Caption="공장코드" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_STCD" Caption="창고코드" Width="1px"  CellStyle-HorizontalAlign="Center" Visible="false"/>
                    <dx:GridViewDataTextColumn FieldName="F_STNM" Caption="창고명" Width="80px"  CellStyle-HorizontalAlign="Center" />
                    <dx:GridViewDataTextColumn FieldName="F_ITTYPENM" Caption="품목분류" Width="90px"  CellStyle-HorizontalAlign="Center" />
                    <dx:GridViewDataTextColumn FieldName="F_ITCD" Caption="품목코드" Width="100px"  CellStyle-HorizontalAlign="Center" />
                    <dx:GridViewDataColumn FieldName="F_ITNM" Caption="품목명" Width="250px" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_STAND" Caption="규격" Width="90px" CellStyle-HorizontalAlign="Center" />
                    <dx:GridViewDataTextColumn FieldName="F_NQTY" Caption="기초재고수량" Width="100px" CellStyle-HorizontalAlign="Right" >
                        <PropertiesTextEdit NullText="Number" ConvertEmptyStringToNull="True">
                            <ClientSideEvents KeyUp="fn_OnMeasureKeyUp" KeyPress="fn_OnMeasureKeyPress"/>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PRICE" Caption="단가" Width="90px" CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit NullText="Number" ConvertEmptyStringToNull="True">
                            <ClientSideEvents KeyUp="fn_OnMeasureKeyUp" KeyPress="fn_OnMeasureKeyPress"/>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_SUM" Caption="금액" Width="90px" CellStyle-HorizontalAlign="Right" >
                        <PropertiesTextEdit NullText="Number" ConvertEmptyStringToNull="True">
                            <ClientSideEvents KeyUp="fn_OnMeasureKeyUp" KeyPress="fn_OnMeasureKeyPress"/>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_CSCD" Name="F_CSCD" Caption="거래처" Width="120px" PropertiesComboBox-ConvertEmptyStringToNull="false" Settings-AllowSort="True" />
                    <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고" CellStyle-HorizontalAlign="Center" />
                    <dx:GridViewDataColumn FieldName="F_ENDYN"  Caption="마감여부" CellStyle-HorizontalAlign="Center" Width="80px" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OndevCallback" />
            </dx:ASPxCallback>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
