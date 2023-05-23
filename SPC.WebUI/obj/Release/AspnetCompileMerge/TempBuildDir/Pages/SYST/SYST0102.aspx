<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SYST0102.aspx.cs" Inherits="SPC.WebUI.Pages.SYST.SYST0102" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totalCount = 0;

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
            var _FACTCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidFACTCD')) ? '' : ASPxClientControl.Cast('hidFACTCD').GetText();
            if (_FACTCD == '') {
                alert('공장을 선택하세요!!');
                return false;
            }

            var _AUTHCD = ddlAuthority.GetValue();
            if (_AUTHCD == '' || _AUTHCD == null) {
                alert('권한을 선택하세요!!');
                return false;
            }

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
        }

        // 전체선택/해제
        function fn_AllCheckedChange(id, checked) {
            var rowCount = devGrid.GetVisibleRowsOnPage();
            
            if (id == 'chkALL') {
                ASPxClientControl.Cast('F_READCheckBox').SetChecked(false);
                ASPxClientControl.Cast('F_LIMITCheckBox').SetChecked(false);
                for (i = 0; i < rowCount; i++) {
                    devGrid.batchEditApi.SetCellValue(i, 'F_ALL', checked);
                    devGrid.batchEditApi.SetCellValue(i, 'F_READ', false);
                    devGrid.batchEditApi.SetCellValue(i, 'F_LIMIT', false);
                }
            } else if (id == 'chkREAD') {
                ASPxClientControl.Cast('F_ALLCheckBox').SetChecked(false);
                ASPxClientControl.Cast('F_LIMITCheckBox').SetChecked(false);
                for (i = 0; i < rowCount; i++) {
                    devGrid.batchEditApi.SetCellValue(i, 'F_ALL', false);
                    devGrid.batchEditApi.SetCellValue(i, 'F_READ', checked);
                    devGrid.batchEditApi.SetCellValue(i, 'F_LIMIT', false);
                }
            } else if (id == 'chkLIMIT') {
                ASPxClientControl.Cast('F_ALLCheckBox').SetChecked(false);
                ASPxClientControl.Cast('F_READCheckBox').SetChecked(false);
                for (i = 0; i < rowCount; i++) {
                    devGrid.batchEditApi.SetCellValue(i, 'F_ALL', false);
                    devGrid.batchEditApi.SetCellValue(i, 'F_READ', false);
                    devGrid.batchEditApi.SetCellValue(i, 'F_LIMIT', checked);
                }
            }
        }

        function fn_OnRowCheckChanged(id) {
            if (id == 'F_ALL') {
                devGrid.SetEditValue('F_READ', false);
                devGrid.SetEditValue('F_LIMIT', false);
            } else if (id == 'F_READ') {
                devGrid.SetEditValue('F_ALL', false);
                devGrid.SetEditValue('F_LIMIT', false);
            } else if (id == 'F_LIMIT') {
                devGrid.SetEditValue('F_ALL', false);
                devGrid.SetEditValue('F_READ', false);
            }
        }

        function fn_OnAuthSelectedIndexChanged(s, e) {
            hidAuthority.SetValue(s.GetValue());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">권한</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="hidAuthority" ClientInstanceName="hidAuthority" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlAuthority" ClientInstanceName="ddlAuthority" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton"
                            OnInit="ddlAuthority_Init" OnDataBound="ddlAuthority_DataBound">
                            <ClientSideEvents SelectedIndexChanged="fn_OnAuthSelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MODULE1;F_MODULE2;F_PGMID" EnableViewState="false" EnableRowsCache="false"
                OnBatchUpdate="devGrid_BatchUpdate" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
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
                    <dx:GridViewDataColumn FieldName="F_MODULE1NM" Caption="대분류명" Width="150px">
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MODULE2NM" Caption="중분류명" Width="150px">
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_PGMID" Caption="페이지ID" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_PGMNM" Caption="페이지명" Width="300px">
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewBandColumn Name="F_AUTHORITY" Caption="사용권한관리">
                        <Columns>
                            <dx:GridViewDataCheckColumn FieldName="F_ALL" Caption="모든권한" Width="100px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="F_ALLCheckBox" ClientInstanceName="F_ALLCheckBox" runat="server"
                                        ToolTip="Select/Unselect all rows on the page" Text="모든권한" Font-Bold="true"
                                        ClientSideEvents-CheckedChanged="function(s, e) { fn_AllCheckedChange('chkALL', s.GetChecked()); }">
                                    </dx:ASPxCheckBox>
                                </HeaderTemplate>
                                <PropertiesCheckEdit>
                                    <ClientSideEvents CheckedChanged="function(s, e) { fn_OnRowCheckChanged('F_ALL'); }" />
                                </PropertiesCheckEdit>
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataCheckColumn FieldName="F_READ" Caption="읽기권한" Width="100px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="F_READCheckBox" ClientInstanceName="F_READCheckBox" runat="server"
                                        ToolTip="Select/Unselect all rows on the page" Text="읽기권한" Font-Bold="true"
                                        ClientSideEvents-CheckedChanged="function(s, e) { fn_AllCheckedChange('chkREAD', s.GetChecked()); }">
                                    </dx:ASPxCheckBox>
                                </HeaderTemplate>
                                <PropertiesCheckEdit>
                                    <ClientSideEvents CheckedChanged="function(s, e) { fn_OnRowCheckChanged('F_READ'); }" />
                                </PropertiesCheckEdit>
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataCheckColumn FieldName="F_LIMIT" Caption="권한없음" Width="100px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="F_LIMITCheckBox" ClientInstanceName="F_LIMITCheckBox" runat="server"
                                        ToolTip="Select/Unselect all rows on the page" Text="권한없음" Font-Bold="true"
                                        ClientSideEvents-CheckedChanged="function(s, e) { fn_AllCheckedChange('chkLIMIT', s.GetChecked()); }">
                                    </dx:ASPxCheckBox>
                                </HeaderTemplate>
                                <PropertiesCheckEdit>
                                    <ClientSideEvents CheckedChanged="function(s, e) { fn_OnRowCheckChanged('F_LIMIT'); }" />
                                </PropertiesCheckEdit>
                            </dx:GridViewDataCheckColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="F_MODULE1" Caption="대분류" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MODULE2" Caption="중분류" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_PGMID" Caption="페이지ID" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
