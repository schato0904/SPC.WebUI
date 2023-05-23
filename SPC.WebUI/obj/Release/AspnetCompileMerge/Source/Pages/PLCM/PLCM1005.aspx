<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM1005.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM1005" %>
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
        }

        // 조회
        function fn_OnSearchClick() {
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
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }

            devGrid.UpdateEdit();
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
            fn_OnBatchValidate("F_STEP", s, e);
            fn_OnBatchValidate("F_STEPNM", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var editor = s.GetEditor('F_STEP');
                fn_OnControlDisableBox(editor, null);
                fn_OnControlEnableComboBox(s.GetEditor('F_MACHCD'), false);
            } else {
                var editor = s.GetEditor('F_STEP');
                fn_OnControlEnableBox(editor, null);
                fn_OnControlEnableComboBox(s.GetEditor('F_MACHCD'), true);
            }
        }

        function fn_OnCustomButtonClick(s, e) {
            if (devGrid.GetRowKey(e.visibleIndex) == 'undefined' || devGrid.GetRowKey(e.visibleIndex) == null) {
                alert("STEP을 저장 후 등록해주세요.");
                return;
            }
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            if (e.buttonID == 'btnAddStep')

            var rootURL = '/' + window.location.href.replace('http://', '').split('/')[1] + '/';
            var pPage = rootURL + 'Pages/PLCM/Popup/PLCM1005POP.aspx' +
                  '?TITLE=스템별검사항목등록' +
                  '&CRUD=CRUDWS' +
                  '&KEYFIELDS=' + encodeURIComponent(rowKeys);
           fn_OnPopupOpen(pPage, '800', '600');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" 
                KeyFieldName="F_MACHCD;F_STEP;F_MACHNM;F_STEPNM" EnableViewState="false" EnableRowsCache="false"
                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize"
                OnAutoFilterCellEditorInitialize="devGrid_AutoFilterCellEditorInitialize">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" ShowFilterRow="true" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" 
                    BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" CustomButtonClick="fn_OnCustomButtonClick" />
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
                    <dx:GridViewDataComboBoxColumn FieldName="F_MACHCD" Caption="설비" Width="150px" CellStyle-HorizontalAlign="Left">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MACHNM" Caption="설비명" Width="200px" Visible="false">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STEP" Caption="STEP" Width="100px" CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit MaxLength="12" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_STEPNM" Caption="STEP명" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataColumn>                    
                    <dx:GridViewCommandColumn Caption="항목등록" Width="100px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton Text="등록" ID="btnAddStep" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
