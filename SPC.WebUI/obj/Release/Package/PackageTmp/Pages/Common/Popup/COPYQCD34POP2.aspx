<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="COPYQCD34POP2.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Popup.COPYQCD34POP2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height - 80);
            devGrid2.SetHeight(height - 110);
        }

        // 조회
        function fn_OnSearchClick() {
            var parentParams = ''
            <%if (!gsVENDOR)
              {%>
            //var parentCompCD = parent.fn_OnGetCompCD();
            //parentParams += (parentParams == '') ? parentCompCD : '|' + parentCompCD;
            //var parentFactCD = parent.fn_OnGetFactCD();
            //parentParams += (parentParams == '') ? parentFactCD : '|' + parentFactCD;
            <%}%>

        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {

            var selectedKeys = '';
            var rowCount = devGrid.GetVisibleRowsOnPage();

            for (i = 0; i < rowCount; i++) {
                if (devGrid.batchEditApi.GetCellValue(i, 'F_COPYYN') == 1) {
                    selectedKeys = selectedKeys + devGrid.batchEditApi.GetCellValue(i, 'F_WORKCD') + '|';
                }
                else {
                }
            }

            txtWORKCDT.SetText(selectedKeys);

            selectedKeys2 = devGrid2.GetSelectedKeysOnPage();

            //alert(selectedKeys2);

            if (selectedKeys == '' || selectedKeys == null) {
                alert('복사 될 공정을 선택하세요.');
                return false;
            }

            if (selectedKeys2 == '' || selectedKeys2 == null) {
                alert('복사 할 검사항목을 클릭 하세요.');
                return false;
            }

            //if (false == fn_OnValidate()) return false;

            if (!confirm('검사기준 복사를 시작합니다.\r계속해서 진행하려면 확인을 누르세요!!')) return false;
                        
            txtDISPLAYNO.SetText(selectedKeys2);            

            devGrid.CancelEdit();            
            devCallback.PerformCallback('Copy');
            devGrid.PerformCallback();
            devGrid2.PerformCallback();
            //devGrid.PerformCallback();

            



        }

        // 취소
        function fn_OnCancelClick() {

        }

        // 삭제
        function fn_OnDeleteClick() {

        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

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
        function fn_OnValidate() {
            if (txtITEMCDS.GetText() == '') {
                alert('복사할(Source) 품목을 입력하세요!!');
                txtITEMCDS.Focus();
                return false;
            }
            if (txtWORKCDS.GetText() == '') {
                alert('복사할(Source) 공정을 입력하세요!!');
                txtWORKCDS.Focus();
                return false;
            }
            if (txtITEMCDT.GetText() == '') {
                alert('복사될(Target) 품목을 입력하세요!!');
                txtITEMCDT.Focus();
                return false;
            }
            if (txtWORKCDT.GetText() == '') {
                alert('복사될(Target) 공정을 입력하세요!!');
                txtWORKCDT.Focus();
                return false;
            }

            if (txtITEMCDS.GetText() == txtITEMCDT.GetText() && txtWORKCDS.GetText() == txtWORKCDT.GetText()) {
                alert('Source와 Target의 품목, 공정이 같습니다!!');
                txtITEMCDT.Focus();
                return false;
            }
        }

        // 검색된 품목 세팅
        function fn_OnSettingItem(CODE, TYPE) {
            ASPxClientControl.Cast('txtITEMCD' + TYPE).SetText(CODE);
            var parentParams = ''
            if (TYPE == 'T')
                devGrid.PerformCallback(parentParams);
        }

        // 검색된 품목 세팅
        function fn_OnSettingWork(CODE, TYPE) {
            ASPxClientControl.Cast('txtWORKCD' + TYPE).SetText(CODE);

            if (TYPE == 'S')
                devGrid2.PerformCallback();
        }

        // 검사기준복사 콜백처리
        function fn_OndevCallback(s, e) {

            var procType = s.cpProcType;
            var resultCode = s.cpResultCode;
            var resultMsg = s.cpResultMsg;

            if (procType == 'Exists' && resultCode == '0') {
                if (!confirm(resultMsg))
                    return false;
                else
                    devCallback.PerformCallback('Copy');
            } else {
                alert(resultMsg)

                if (resultCode == '1') {
                    //parent.fn_devPopupClose();
                }
            }
        }

        function fn_OnRowClick(s, e) {
            var parentParams = ''

            //selectedKeys3 = devGrid2.GetSelectedKeysOnPage();

            selectedKeys3 = devGrid2.GetRowKey(e.visibleIndex)

            //txtINSPCD.SetText(selectedKeys3);

            parentParams = selectedKeys3;
            devGrid.PerformCallback(parentParams);
        }

        function fn_OnBatchEditStartEditing(s, e) {

            if (devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_INSPCD') == 'O') {
                s.GetEditor('F_COPYYN').SetEnabled(false);
                s.GetEditor('F_WORKCD').SetEnabled(false);
                s.GetEditor('F_WORKNM').SetEnabled(false);
                s.GetEditor('F_INSPCD').SetEnabled(false);
                return false;
            } else {
                s.GetEditor('F_COPYYN').SetEnabled(true);
                s.GetEditor('F_WORKCD').SetEnabled(false);
                s.GetEditor('F_WORKNM').SetEnabled(false);
                s.GetEditor('F_INSPCD').SetEnabled(false);
                return false;
            }
        }

        function fn_AllCheckedChange(checked) {
            var rowCount = devGrid.GetVisibleRowsOnPage();

            var tf = ASPxClientControl.Cast('selectAllCheckBox').GetChecked();

            if (tf == false) {
                for (i = 0; i < rowCount; i++) {
                    devGrid.batchEditApi.SetCellValue(i, 'F_COPYYN', 0);
                }
            } else {
                for (i = 0; i < rowCount; i++) {
                    if (devGrid.batchEditApi.GetCellValue(i, 'F_INSPCD') == 'O') {
                    }
                    else {
                        devGrid.batchEditApi.SetCellValue(i, 'F_COPYYN', 1);
                    }
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-6">
                        <p class="bg-info text-center"><span class="font-bold">Source</span></p>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">품목코드</label>
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtITEMCDS" ClientInstanceName="txtITEMCDS" runat="server" Width="100%" class="form-control input-sm"
                                    OnInit="txtITEMCDS_Init">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">공정코드</label>
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtWORKCDS" ClientInstanceName="txtWORKCDS" runat="server" Width="100%" class="form-control input-sm"
                                    OnInit="txtWORKCDS_Init">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtDISPLAYNO" ClientInstanceName="txtDISPLAYNO" runat="server" Width="100%" class="form-control input-sm" ClientVisible="false">
                                </dx:ASPxTextBox>
                                <dx:ASPxTextBox ID="txtINSPCD" ClientInstanceName="txtINSPCD" runat="server" Width="100%" class="form-control input-sm" ClientVisible="false">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="content">
                            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%" ViewStateMode="Disabled"
                                EnableViewState="true" OnCustomCallback="devGrid2_CustomCallback" KeyFieldName="F_MEAINSPCD;F_DISPLAYNO">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Hidden" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowClick="fn_OnRowClick" />
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="100px" FixedStyle="Left" PropertiesEditType="TextBox">
                                        <CellStyle HorizontalAlign="Left" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_DISPLAYNO" Caption="검사순서" Width="60px" FixedStyle="Left" PropertiesEditType="TextBox"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_UNIT" Caption="단위" Width="50px" PropertiesEditType="TextBox"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Width="60px" PropertiesEditType="TextBox">
                                        <CellStyle HorizontalAlign="Right" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="항목코드" Width="60px" PropertiesEditType="TextBox" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="항목코드" Width="60px" PropertiesEditType="TextBox" Visible="false"></dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <p class="bg-warning text-center"><span class="font-bold">Target</span></p>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">품목코드</label>
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtITEMCDT" ClientInstanceName="txtITEMCDT" runat="server" Width="100%" class="form-control input-sm"
                                    OnInit="txtITEMCDT_Init">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtWORKCDT" ClientInstanceName="txtWORKCDT" runat="server" Width="100%" class="form-control input-sm"
                                    OnInit="txtWORKCDT_Init" ClientVisible="false">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="content">
                            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="F_INSPCD; F_WORKCD" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGrid_CustomCallback">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Hidden" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                                <SettingsBehavior AllowSort="false" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="Click" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                                <Columns>
                                    <dx:GridViewDataCheckColumn FieldName="F_COPYYN" Width="35px">
                                        <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="selectAllCheckBox" ClientInstanceName="selectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                                ClientSideEvents-CheckedChanged="function(s, e) { fn_AllCheckedChange(s.GetChecked()); }"
                                                AutoPostBack="false" />
                                        </HeaderTemplate>
                                    </dx:GridViewDataCheckColumn>
                                    <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Width="60px" Visible="false" />
                                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="60px" Visible="false" />
                                    <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Width="60px" Visible="false" />
                                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="120px" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="F_WORKCD" Caption="공정코드" Width="80px" />
                                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="120px">
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="항목 유무" Width="100px" />
                                    <%--여기서부터 히든 필드--%>
                                    <dx:GridViewDataColumn FieldName="F_COMPCD" Caption="" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_FACTCD" Caption="" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_WORKCD1" Caption="" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_WORKCD2" Caption="" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_MACHGUBUN" Caption="" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_STATUS" Caption="" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_SORTNO" Caption="" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_PCNM" Caption="" Visible="false"></dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </div>
                <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                    <ClientSideEvents EndCallback="fn_OndevCallback" />
                </dx:ASPxCallback>
            </div>
            <div class="paging"></div>

        </div>

    </div>
</asp:Content>
