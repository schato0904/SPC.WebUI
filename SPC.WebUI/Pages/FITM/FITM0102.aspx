<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FITM0102.aspx.cs" Inherits="SPC.WebUI.Pages.FITM.FITM0102" %>

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

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
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

            gridValids = [];
            gridValidIdx = 0;
            gridIsValid = true;

            if (parseInt(e.visibleIndex, 10) >= 0) {
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_FOURNM", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_COLORGBN", s, e);
            } else {
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_FOURNM", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_COLORGBN", s, e);
            }




            gridValids.forEach(function (gridValid) {
                if (!gridValid) {
                    gridIsValid = false;
                    return false;
                }
            });
        }

        // ComboBox Change BackGround Color
        function OnCOLORGBN_Init(s, e) {
            var colors = [];
            colors[0] = '#FFFFFF';
            colors[1] = '#FF0000';
            colors[2] = '#FFA500';
            colors[3] = '#FFFF00';
            colors[4] = '#008000';
            colors[5] = '#0000FF';
            colors[6] = '#2626C2';
            colors[7] = '#8A2BE2';
            colors[8] = '#C0C0C0';

            var item;
            for (i = 0; i < s.GetItemCount() ; i++) {
                if (ASPxClientUtils.chrome)
                    item = s.listBox.GetItemElement(i);
                else
                    item = s.listBox.GetItemElement(i).firstElementChild;

                item.style.backgroundColor = colors[i];
            }
        }

        function fn_OnBatchEditStartEditing(s, e) {

            var clickedvalue = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_FOURNM');

            if (clickedvalue == '품목변경' || clickedvalue == '주야교대' || clickedvalue == '인원교대') {
                s.GetEditor('F_FOURNM').SetEnabled(false);
                s.GetEditor('F_COLORGBN').SetEnabled(false);
                s.GetEditor('F_STATUS').SetEnabled(false);
            }
            else {
                s.GetEditor('F_FOURNM').SetEnabled(true);
                s.GetEditor('F_COLORGBN').SetEnabled(true);
                s.GetEditor('F_STATUS').SetEnabled(true);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false"
                KeyFieldName="F_FOURCD" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow"
                OnCellEditorInitialize="devGrid_CellEditorInitialize" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCommandButtonInitialize="devGrid_CommandButtonInitialize"
                OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
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
                        <ucCTF:Count ID="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " VisibleIndex="0" Width="35px" ButtonType="Button">
                        <HeaderTemplate>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FOURCD" Caption="변경코드" Width="70px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FOURNM" Caption="변경사유" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_COLORGBN" Caption="색" Width="40px">
                        <PropertiesComboBox>
                            <ClientSideEvents Init="OnCOLORGBN_Init" />
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용여부" Width="70px" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
