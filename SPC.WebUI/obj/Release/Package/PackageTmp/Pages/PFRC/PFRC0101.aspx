<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PFRC0101.aspx.cs" Inherits="SPC.WebUI.Pages.PFRC.PFRC0101" %>
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

        $(document).ready(function () {
            fn_doSetGridEventAction('true');
            fn_RendorTotalCount();

            _selectedRowKeyValue = devTree.GetFocusedNodeKey();
            hidTreeFocusedKey.SetText(_selectedRowKeyValue);

            var oParams = 'select;' + _selectedRowKeyValue;
            devGrid.PerformCallback(oParams);
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".content").offset().top;
            var height = Math.max(0, $(document).height() - _hMargin - top);
            var btnHeight = parseInt($("#btnCacheRefresh").height(), 10);
            devTree.SetHeight(height - btnHeight - 10);
            devGrid.SetHeight(height);
        }

        function fn_OnNodeDblClick(s, e) {
            fn_doSetGridEventAction('true');

            _selectedRowKeyValue = devTree.GetFocusedNodeKey();
            hidTreeFocusedKey.SetText(_selectedRowKeyValue);

            var oParams = 'select;' + _selectedRowKeyValue;
            devGrid.PerformCallback(oParams);
        }

        // 입력
        function fn_OnNewClick() {
            if (_selectedRowKeyValue == '') { alert('좌측 트리에서 입력할 코드그룹를 선택하세요!!'); return false; }

            fn_doSetGridEventAction('true');

            var oParams = 'new;' + _selectedRowKeyValue;
            devGrid.PerformCallback(oParams);
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();
            
            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            // 수정은 1 Row씩만 가능
            if (selectedKeys.length > 1) {
                alert('수정은 한개의 데이타만 가능합니다.');
                return false;
            }

            fn_doSetGridEventAction('true');

            $("#hidRowKeys").val(selectedKeys);
            
            var selectedRowIndex = fn_GetSelectedRowIndex(devGrid, selectedKeys);

            var oParams = 'edit;' + _selectedRowKeyValue + '|' + selectedKeys + '|' + selectedRowIndex;
            devGrid.PerformCallback(oParams);
        }

        // 저장
        function fn_OnSaveClick() {
            if (false == fn_OnValidate()) return false;

            fn_doSetGridEventAction('true');

            selectedKeys = _selectedRowKeyValue;

            var oParams = 'save;' + selectedKeys;
            devGrid.PerformCallback(oParams);
        }

        // 취소
        function fn_OnCancelClick() {
            if (_selectedRowKeyValue != "") {
                fn_doSetGridEventAction('true');

                selectedKeys = _selectedRowKeyValue.toString().split('|');
                if (selectedKeys.length >= 2) {
                    selectedKeys = selectedKeys[0];
                }

                var oParams = 'cancel;' + selectedKeys;
                devGrid.PerformCallback(oParams);
            }
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 이상 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 데이타는 복원할 수 없습니다.')) { return false; }

            fn_doSetGridEventAction('true');

            selectedKeys = devGrid.GetSelectedKeysOnPage();

            var oParams = 'delete;' + _selectedRowKeyValue + '|' + selectedKeys;
            
            devGrid.PerformCallback(oParams);
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        // Grid End Callback
        function fn_devGridOnEndCallback(s, e) {
            fn_doSetGridEventAction('false');
            
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }

            fn_RendorTotalCount();
        }

        function fn_OnValidate() {
            if (!devGrid.IsNewRowEditing()) {
                // 수정모드인경우
                if (txtCOMMNMKR.GetText() == '') {
                    alert('코드명(한글)는 필수입력사항입니다!!');
                    txtCOMMNMKR.Focus();
                    return false;
                }
            } else {
                // 신규모드인 경우
                if (txtCOMMCD.GetText() == '') {
                    alert('코드는 필수입력사항입니다!!');
                    txtCOMMCD.Focus();
                    return false;
                }
                if (txtCOMMNMKR.GetText() == '') {
                    alert('코드명(한글)는 필수입력사항입니다!!');
                    txtCOMMNMKR.Focus();
                    return false;
                }
            }
        }

        // 캐쉬갱신하기
        function fn_OnRefreshCommonCodeCache() {
            if (!confirm("Application Cache를 갱신합니다.\r캐쉬 갱신시 사이트 속도가 느려질 수 있습니다.\r계속 진행하려면 확인을 누르세요"))
                return;
            else
                devCallback.PerformCallback();
        }

        // Tree End Callback
        function fn_OnEndCallback() { }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div class="left">
                <section id="sectionTree" class="panel panel-default">
                    <dx:ASPxTextBox ID="hidTreeFocusedKey" ClientInstanceName="hidTreeFocusedKey" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
                    <dx:ASPxTreeList ID="devTree" ClientInstanceName="devTree" runat="server" AutoGenerateColumns="false" Width="100%"
                        KeyFieldName="F_COMMCD" ParentFieldName="F_GROUPCD">
                        <Columns>
                            <dx:TreeListDataColumn FieldName="F_COMMNM" Caption="코드명" Width="100%" VisibleIndex="0" />
                            <dx:TreeListDataColumn FieldName="F_STATUS" Caption="상태" Width="50" VisibleIndex="1" />
                        </Columns>
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" ShowColumnHeaders="true" />
                        <SettingsBehavior ExpandCollapseAction="Button" AllowSort="false" AllowFocusedNode="True" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" NodeDblClick="fn_OnNodeDblClick" />
                    </dx:ASPxTreeList>
                    <button id="btnCacheRefresh" class="btn btn-sm btn-danger" style="width: 100%;" onclick="fn_OnRefreshCommonCodeCache(); return false;">
                        <i class="fa fa-refresh"></i>
                        <span class="text">캐쉬갱신하기</span>
                    </button>
                </section>
            </div>
            <div class="right">
                <section class="panel panel-default" style="height: 100%;">
                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                        KeyFieldName="F_COMMCD" EnableViewState="false" EnableRowsCache="false"
                        OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                        OnInitNewRow="devGrid_InitNewRow" OnHtmlEditFormCreated="devGrid_HtmlEditFormCreated"
                        OnRowInserting="devGrid_RowInserting" OnRowUpdating="devGrid_RowUpdating">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
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
                            <dx:GridViewDataColumn FieldName="F_GROUPCD" Caption="그룹코드" Width="100px"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_COMMCD" Caption="코드" Width="100px"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_SORTNO" Caption="출력<br/>순서" Width="50px"></dx:GridViewDataColumn>
                            <dx:GridViewBandColumn Name="F_COMMNM" Caption="코드명">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="F_COMMNMKR" Caption="코드명(한글)" Width="100%">
                                        <cellstyle horizontalalign="Left"></cellstyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_COMMNMUS" Caption="코드명(영문)" Width="150px">
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_COMMNMCN" Caption="코드명(중문)" Width="150px">
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용<br/>여부" Width="40px"></dx:GridViewDataCheckColumn>
                        </Columns>
                        <Templates>
                            <EditForm>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">그룹코드</label>
                                        <div class="col-sm-2">
                                            <dx:ASPxTextBox ID="txtGROUPCD" ClientInstanceName="txtGROUPCD" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_GROUPCD]") %>' />
                                        </div>
                                        <label class="col-sm-2 control-label">코드</label>
                                        <div class="col-sm-2">
                                            <dx:ASPxTextBox ID="txtCOMMCD" ClientInstanceName="txtCOMMCD" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_COMMCD]") %>' />
                                        </div>
                                        <label class="col-sm-2 control-label">사용여부</label>
                                        <div class="col-sm-2">
                                            <dx:ASPxCheckBox ID="chkSTATUS" ClientInstanceName="chkSTATUS" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_STATUS]") %>' />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">코드명(한글)</label>
                                        <div class="col-sm-10">
                                            <dx:ASPxTextBox ID="txtCOMMNMKR" ClientInstanceName="txtCOMMNMKR" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_COMMNMKR]") %>' />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">코드명(영문)</label>
                                        <div class="col-sm-10">
                                            <dx:ASPxTextBox ID="txtCOMMNMUS" ClientInstanceName="txtCOMMNMUS" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_COMMNMUS]") %>' />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">코드명(중문)</label>
                                        <div class="col-sm-10">
                                            <dx:ASPxTextBox ID="txtCOMMNMCN" ClientInstanceName="txtCOMMNMCN" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_COMMNMCN]") %>' />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-1 control-label"></label>
                                        <label class="col-sm-1 control-label">기타1</label>
                                        <div class="col-sm-1">
                                            <dx:ASPxTextBox ID="txtPARAM01" ClientInstanceName="txtPARAM01" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_PARAM01]") %>' />
                                        </div>
                                        <label class="col-sm-1 control-label">기타2</label>
                                        <div class="col-sm-1">
                                            <dx:ASPxTextBox ID="txtPARAM02" ClientInstanceName="txtPARAM02" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_PARAM02]") %>' />
                                        </div>
                                        <label class="col-sm-1 control-label">기타3</label>
                                        <div class="col-sm-1">
                                            <dx:ASPxTextBox ID="txtPARAM03" ClientInstanceName="txtPARAM03" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_PARAM03]") %>' />
                                        </div>
                                        <label class="col-sm-1 control-label">기타4</label>
                                        <div class="col-sm-1">
                                            <dx:ASPxTextBox ID="txtPARAM04" ClientInstanceName="txtPARAM04" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_PARAM04]") %>' />
                                        </div>
                                        <label class="col-sm-1 control-label">기타5</label>
                                        <div class="col-sm-1">
                                            <dx:ASPxTextBox ID="txtPARAM05" ClientInstanceName="txtPARAM05" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_PARAM05]") %>' />
                                        </div>
                                        <label class="col-sm-1 control-label"></label>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">비고1</label>
                                        <div class="col-sm-2">
                                            <dx:ASPxTextBox ID="txtREMARK1" ClientInstanceName="txtREMARK1" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_REMARK1]") %>' />
                                        </div>
                                        <label class="col-sm-2 control-label">비고2</label>
                                        <div class="col-sm-2">
                                            <dx:ASPxTextBox ID="txtREMARK2" ClientInstanceName="txtREMARK2" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_REMARK2]") %>' />
                                        </div>
                                        <label class="col-sm-2 control-label">출력순번</label>
                                        <div class="col-sm-1">
                                            <dx:ASPxSpinEdit ID="txtSORTNO" ClientInstanceName="txtSORTNO" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_SORTNO]") %>' Number="0" MinValue="0" MaxValue="99999" />
                                            <label class="col-sm-1 control-label"></label>
                                        </div>
                                    </div>
                                </div>
                            </EditForm>
                        </Templates>
                        <ClientSideEvents EndCallback="fn_devGridOnEndCallback" />
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                        <SettingsBehavior AllowSort="false" />
                        <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                        <SettingsPager Mode="ShowAllRecords" />
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                    <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
                </section>
            </div>
        </div>
    </div>

    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback" />
</asp:Content>
