<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD0103.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD0103" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .WORKGRID {
            display:none;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var editRowIndex;

        $(document).ready(function () {
            fn_RendorTotalCount();
            document.onkeypress = CancelEnterKey;
        });

        function CancelEnterKey() {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
            devGrid2.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            hidGridAction2.SetText("true");

            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            //devGrid.AddNewRow();
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
            if (!devGrid2.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else
                devGrid2.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid2.UnselectAllRowsOnPage();
            devGrid2.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            //selectedKeys = devGrid.GetSelectedKeysOnPage();

            //// 1 Row 반드시 선택
            //if (selectedKeys.length <= 0) {
            //    alert('삭제할 데이타를 선택하세요!!');
            //    return false;
            //}

            //if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            //for (var i = 0; i < selectedKeys.length ; i++) {
            //    devGrid.DeleteRowByKey(selectedKeys[i]);
            //}

            //devGrid.UpdateEdit();
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
            hidGridAction2.SetText("false");
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
        }

        // Grid End Callback
        function fn_OnEndCallback2(s, e) {
            fn_doSetGridEventAction('false');
            hidGridAction2.SetText("false");
            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                }  else {
                    alert(s.cpResultMsg);

                    
                    s.cpResultCode = "";
                    s.cpResultMsg = "";

                    //hidGridAction2.SetText("true");
                    //devGrid2.PerformCallback();
                }
            }

            var rowCount = devGrid2.GetVisibleRowsOnPage();

            for (i = 0; i < rowCount; i++) {
                if (devGrid2.batchEditApi.GetCellValue(i, 'F_GBN') != "1") {
                    devGrid2.batchEditApi.SetCellValue(i, 'F_GBN', "1");
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
            fn_OnBatchValidate("F_WORKCD", s, e);
            fn_OnBatchValidate("F_ITEMCD", s, e);
            //fn_OnBatchValidate("F_CODE", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            editRowIndex = parseInt(e.visibleIndex, 10);
            if (editRowIndex >= 0) {
                fn_OnControlEnableComboBox(s.GetEditor('F_WORKCD'), false);
                //fn_OnControlEnableComboBox(s.GetEditor('F_CODE'), false);
                fn_OnControlEnableComboBox(s.GetEditor('F_ITEMCD'), false);
                
                fn_OnControlDisableBox(s.GetEditor('F_WORKNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);


            } else {
                fn_OnControlEnableComboBox(s.GetEditor('F_WORKCD'), true);
                fn_OnControlEnableComboBox(s.GetEditor('F_ITEMCD'), true);
                //fn_OnControlEnableComboBox(s.GetEditor('F_CODE'), true);
                fn_OnControlEnableBox(s.GetEditor('F_WORKNM'), null);
                fn_OnControlEnableBox(s.GetEditor('F_ITEMNM'), null);
            }
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
        }

        // 공정코드 조회전 품목코드 입력여부 확인
        function fn_OnPopupItemSearchForm_ItemError() {
            if (devGrid.GetEditor('F_WORKCD').GetText() == '' || devGrid.GetEditor('F_WORKCD').GetText() == '') {
                alert('공정을 입력하세요!!');
                devGrid.GetEditor('F_WORKCD').Focus();
                return false;
            }

            fn_OnPopupItemSearch_WorkError(devGrid.GetEditor('F_WORKCD').GetText());
        }

        // 검색된 품목 세팅
        function fn_OnUCSettingItem1(CODE, TEXT, MODEL) {
            devGrid.StartEditRow(editRowIndex);
            devGrid.GetEditor('F_ITEMCD').SetText(CODE);
            devGrid.GetEditor('F_ITEMNM').SetText(TEXT);
        }

        // 검색된 공정(반/라인 포함) 세팅
        function fn_OnSettingWork1(CODE, TEXT, BANCD, LINECD) {
            devGrid.StartEditRow(editRowIndex);
            devGrid.GetEditor('F_WORKCD').SetText(CODE);
            devGrid.GetEditor('F_WORKNM').SetText(TEXT);
        }

        // BatchEditStartEditing
        function fn_OnRowClick(s, e) {
            hidGridAction2.SetText("true");
            fn_OnCancelClick();
            var rowkey = devGrid.GetRowKey(e.visibleIndex)

            hidITEMCD.SetText(rowkey.split('|')[1]);
            hidWORKCD.SetText(rowkey.split('|')[0]);

            devGrid2.PerformCallback();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content" style="display: table-row;">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <div style="display: table-cell;">
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" Width="100%" 
                    KeyFieldName="F_WORKCD;F_ITEMCD" EnableViewState="False" EnableRowsCache="False"
                    OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize" OnDataBound="devGrid_DataBound" SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="false" >
                
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                    <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowClick="fn_OnRowClick" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="품목명" Width="220px" ReadOnly="true">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_WORKCD" Caption="공정코드" Width="80px">
                            <%--<CellStyle HorizontalAlign="Left" />--%>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_WORKNM" Caption="공정명" Width="220px" ReadOnly="true">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>  
                    </Columns>
                </dx:ASPxGridView>
            </div>
            <div style="width:10px;display: table-cell;">
            </div>
            <div style="display: table-cell;">
                <dx:ASPxTextBox ID="hidGridAction2" ClientInstanceName="hidGridAction2" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="False" Width="100%"  
                    KeyFieldName="F_ITEMCD;F_WORKCD;F_CODE" EnableViewState="False" EnableRowsCache="False"
                    OnBatchUpdate="devGrid2_BatchUpdate" OnCustomCallback="devGrid2_CustomCallback" >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                    <SettingsBehavior AllowSort="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="Click" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback2" CallbackError="fn_OnCallbackError" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataCheckColumn FieldName="F_CHECK" Caption=" " Width="50px" />
                       <dx:GridViewDataTextColumn FieldName="F_CODE" Caption="코드" Width="80px" ReadOnly="true" >
                            <%--<CellStyle HorizontalAlign="Left" />--%>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_CODENM" Caption="명칭" Width="250px" ReadOnly="true" >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn> 
                        <dx:GridViewDataColumn FieldName="F_GBN" Caption="" Width="1px"  />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    </Columns>
                </dx:ASPxGridView>
            </div>
            <dx:ASPxTextBox ID="hidITEMCD" ClientInstanceName="hidITEMCD" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidWORKCD" ClientInstanceName="hidWORKCD" runat="server" ClientVisible="false" />
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
