<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0205BON.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0205BON" %>
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
            fn_OnBatchValidate("F_ITEMCD", s, e);
            fn_OnBatchValidate("F_ITEMNM", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var editor = s.GetEditor('F_ITEMCD');
                fn_OnControlDisableBox(editor, null);
            } else {
                var editor = s.GetEditor('F_ITEMCD');
                fn_OnControlEnableBox(editor, null);
            }
        }

        function fn_OnCustomButtonClick(s, e) {
            if (devGrid.GetRowKey(e.visibleIndex) == 'undefined' || devGrid.GetRowKey(e.visibleIndex) == null) {
                alert("품목코드를 저장 후 등록해주세요.");
                return;
            }

            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));

            if (e.buttonID == 'btnAddLine')
                fn_OnPopupQCD011Add(encodeURIComponent(rowKeys));
            else if (e.buttonID == 'btnAddImage')
                fn_OnPopupQCD014Add(encodeURIComponent(rowKeys));

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="False" EnableRowsCache="False"
                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize"
                OnAutoFilterCellEditorInitialize="devGrid_AutoFilterCellEditorInitialize" OnDataBound="devGrid_DataBound">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" ShowFilterRow="true" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowPager"  PageSize="50"/>
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" >
                <BatchEditSettings EditMode="Row" StartEditAction="DblClick"></BatchEditSettings>
                </SettingsEditing>
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
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품목코드" Width="120px">
                        <PropertiesTextEdit MaxLength="40" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                        <Settings AutoFilterCondition="Contains"  />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_CODENO" Caption="구품번" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_MODELCD" Name="F_MODELCD" Caption="기종코드" Width="150px" >
                        <PropertiesComboBox ConvertEmptyStringToNull="false" >
                        </PropertiesComboBox>
                        <Settings AllowSort="True" />
                        <%--<PropertiesComboBox>
                            <ClientSideEvents Init="fn_OnControlDisable"  />
                        </PropertiesComboBox>--%>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용" Width="60px" />
                    <dx:GridViewCommandColumn Caption="라인등록" Width="100px"  >
                        <CustomButtons>                            
                            <dx:GridViewCommandColumnCustomButton Text="등록" ID="btnAddLine" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn Caption="도면등록" Width="100px"  >
                        <CustomButtons>                            
                            <dx:GridViewCommandColumnCustomButton Text="등록" ID="btnAddImage" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_INSPGUBN" Caption="수입검사" Width="60px" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
