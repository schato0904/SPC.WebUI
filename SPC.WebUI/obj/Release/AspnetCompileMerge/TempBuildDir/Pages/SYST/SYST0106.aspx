<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SYST0106.aspx.cs" Inherits="SPC.WebUI.Pages.SYST.SYST0106" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .search {
            width: 100%;
            display: table-row;
        }
        .content {
            width: 100%;
            height: 100%;
            display: table-row;
        }
        .left {
            width: 300px;
            height: 100%;
            display: table-cell;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            height: 100%;
            display: table-cell;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var Param = null;
        $(document).ready(function () {
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height();
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
            devGrid2.SetHeight(height-40);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            hidGridAction2.SetText("true");
            hidGbn.SetText("");
            devGrid.PerformCallback();
                        
            // 조회버튼 클릭시 그리드2 초기화 시켜주기 위해 콜백
            devGrid2.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            devGrid2.AddNewRow();
            devGrid2.GetEditor("F_USERID").SetText(hidUserID.GetText());
            devGrid2.GetEditor("F_USERNM").SetText(hidUserNm.GetText());
            
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid2.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid2.StartEditRowByKey(selectedKeys[i]);
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
            selectedKeys = devGrid2.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid2.DeleteRowByKey(selectedKeys[i]);
            }
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
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }

            fn_RendorTotalCount();
        }

        function fn_OnEndCallback2(s, e) {
            fn_doSetGridEventAction('false');
            hidGridAction2.SetText("false");
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
            fn_OnBatchValidate("F_USERID", s, e);
            fn_OnBatchValidate("F_BANCD", s, e);
            fn_OnBatchValidate("F_LINECD", s, e);
        }

        // BatchEditStartEditing
        function fn_OnRowClick(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {                
                devGrid.GetRowValues(e.visibleIndex, "F_USERNM", OnRowValues)

                // 행 클릭 이후 처리
                function OnRowValues(UserNm) {
                    var UserID = devGrid.GetRowKey(e.visibleIndex);
                    fn_OnCancelClick();
                    hidUserID.SetText(UserID);
                    hidUserNm.SetText(UserNm);
                    hidGbn.SetText("");
                    hidGridAction2.SetText("true");
                    devGrid2.PerformCallback(JSON.stringify(Param));
                }
            }
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing2(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var LINECD = s.batchEditApi.GetCellValue(e.visibleIndex, 'F_LINECD');
                ddlLINEEdit.SetValue(LINECD)
                fn_OnControlDisableBox(s.GetEditor('F_USERID'), null);
                fn_OnControlDisableBox(s.GetEditor('F_USERNM'), null);
                fn_OnControlEnableComboBox(s.GetEditor('F_BANCD'), false);
                fn_OnControlDisableBox(s.GetEditor('F_BANNM'), null);
                fn_OnControlEnableComboBox(ddlLINEEdit, false);
                fn_OnControlDisableBox(s.GetEditor('F_LINENM'), null);
            } else {
                fn_OnControlDisableBox(s.GetEditor('F_USERID'), null);
                fn_OnControlDisableBox(s.GetEditor('F_USERNM'), null);
                fn_OnControlEnableComboBox(s.GetEditor('F_BANCD'), true);
                fn_OnControlEnableBox(s.GetEditor('F_BANNM'), null);
                fn_OnControlEnableComboBox(ddlLINEEdit, true);
                fn_OnControlEnableBox(s.GetEditor('F_LINENM'), null);
            }
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
            var subColumn = s.GetColumnByField("F_LINECD");
            if (!e.rowValues.hasOwnProperty(subColumn.index))
                return;
            var cellInfo = e.rowValues[subColumn.index];
            if (ddlLINEEdit.GetSelectedIndex() > -1 || cellInfo.text != ddlLINEEdit.GetText()) {
                cellInfo.value = ddlLINEEdit.GetValue();
                cellInfo.text = ddlLINEEdit.GetText();
                ddlLINEEdit.SetValue(null);
            }
        }

        // Edit Form 의 반 선택값이 바뀌는 경우
        function fn_OnBANCDEditorSelectedIndexChanged(s, e) {
            ddlLINEEdit.PerformCallback(s.GetValue() + '|');
            devGrid2.GetEditor("F_BANNM").SetText(s.GetText());
        }

        function fn_OnLINECDEditorSelectedIndexChanged(s, e) {
            devGrid2.GetEditor("F_LINENM").SetText(s.GetText());
        }

        function fn_AddBanLine() {
            fn_OnCancelClick();
            hidGbn.SetText("edit");
            devGrid2.PerformCallback();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <%--<div class="search"></div>--%>
        <div class="content">
            <div class="left">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="F_USERID" EnableViewState="False" EnableRowsCache="False"
                        OnCustomCallback="devGrid_CustomCallback">
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
                            <dx:GridViewDataColumn FieldName="F_USERID" Caption="ID" Width="150px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="F_USERNM" Caption="사용자명" Width="150px">
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataSpinEditColumn FieldName="F_CNT" Caption="라인수" Name="F_MONREFRTM" Width="80px">
                                <PropertiesSpinEdit NumberType="Integer" MinValue="0" MaxValue="99999" SpinButtons-ShowIncrementButtons="false">
                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                </PropertiesSpinEdit>
                            </dx:GridViewDataSpinEditColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            </div>
            <div class="right">
                <section class="panel panel-default" style="height: 100%;">
                    <div class="form-horizontal bg-white fa-border"  >
                        <div class="form-group">
                            <label class="col-sm-1 control-label">반</label>
                            <div class="col-sm-2 control-label">
                                <ucCTF:Ban ID="ucBan" runat="server" />
                            </div>
                            <div class="col-sm-2 control-label">
                                <a href="#" onclick="fn_AddBanLine()" class="btn btn-default btn-sm">해당반 전체</a>
                            </div>                    
                        </div>                        
                    </div>
                    <dx:ASPxTextBox ID="hidGridAction2" ClientInstanceName="hidGridAction2" runat="server" ClientVisible="false" />
                    <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="False"  Width="100%"
                        KeyFieldName="F_USERID;F_BANCD;F_LINECD" EnableViewState="False" EnableRowsCache="False"
                        OnBatchUpdate="devGrid_BatchUpdate" OnCustomCallback="devGrid2_CustomCallback" OnCellEditorInitialize="devGrid_CellEditorInitialize">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                        <SettingsBehavior AllowSort="false" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback2" CallbackError="fn_OnCallbackError"
                            BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing2" BatchEditEndEditing="fn_OnBatchEditEndEditing" />
                        <Templates>
                            <StatusBar>
                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
                            </StatusBar>
                        </Templates>
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                        ClientSideEvents-CheckedChanged="function(s, e) { devGrid2.SelectAllRowsOnPage(s.GetChecked()); }" />
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="F_USERID" Caption="ID" Width="150px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="F_USERNM" Caption="사용자명" Width="150px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="F_BANCD" Name="F_BANCD" Caption="반코드" Width="100px">
                                <Settings AllowSort="True" />
                                <PropertiesComboBox>
                                    <ClientSideEvents SelectedIndexChanged="fn_OnBANCDEditorSelectedIndexChanged" />
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="F_BANNM" Name="F_BANNM" Caption="반명" Width="100px" ReadOnly="true">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="F_LINECD" Name="F_LINECD" Caption="라인코드" Width="100px">
                                <EditItemTemplate>
                                    <dx:ASPxComboBox ID="ddlLINEEdit" ClientInstanceName="ddlLINEEdit" runat="server" Width="100%"
                                        OnCallback="ddlLINEEdit_Callback" OnDataBound="ddlLINEEdit_DataBound"
                                        EnableCallbackMode="true" IncrementalFilteringMode="None" CssClass="NoXButton">
                                        <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_OnLINECDEditorSelectedIndexChanged" />
                                    </dx:ASPxComboBox>
                                </EditItemTemplate>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="100px" ReadOnly="true">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_GBN" Caption="" Width="1px"  />
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            </div>
            <dx:ASPxTextBox ID="hidUserID" ClientInstanceName="hidUserID" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidUserNm" ClientInstanceName="hidUserNm" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidGbn" ClientInstanceName="hidGbn" runat="server" ClientVisible="false" />            
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxGridViewExporter ID="devGridExporter2" runat="server" GridViewID="devGrid2" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>

