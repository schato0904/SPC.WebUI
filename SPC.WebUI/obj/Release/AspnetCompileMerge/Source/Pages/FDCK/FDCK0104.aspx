<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FDCK0104.aspx.cs" Inherits="SPC.WebUI.Pages.FDCK.FDCK0104" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .content {
            width: 100%;
            height: 100%;
            display: table;
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

                .contentTable {
            width: 100%;
            border-color: darkgray;
        }
            .contentTable > tbody > tr > .tdLabel {
                /*background-color: #CFEFFF;*/
                background-color: #DCDCDC;
                color: dimgray;
                text-align: center;
                padding-top: 3px;
                padding-bottom: 3px;
            }
            .contentTable > tbody > tr > .tdLabel > label {
                color: #444444;
                font-weight:bold;
            }
            .contentTable > tbody > tr > .tdInput {
                background-color: white;
                padding-left: 3px;
                padding-right: 3px;
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
            devGrid2.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            hidGridAction2.SetText("true");
            devGrid.PerformCallback();
                        
            // 조회버튼 클릭시 그리드2 초기화 시켜주기 위해 콜백
            hidMachCD.SetText('');
            devGrid2.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            if (hidMachCD.GetText() == '') {
                alert("좌측의 설비를 선택 후 입력 해주세요.");
                return;
            }
            devGrid2.AddNewRow();
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
            
            fn_RendorTotalCount();
        }
        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_INSPTYPECD", s, e);
        }

        // BatchEditStartEditing
        function fn_OnRowClick(s, e) {

            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(e.visibleIndex));
            hidMachCD.SetText(rowKeys);
            /*
            if (parseInt(e.visibleIndex, 10) >= 0) {                
                devGrid.GetRowValues(e.visibleIndex, "F_MACHCD", OnRowValues)

                // 행 클릭 이후 처리
                function OnRowValues(MACHCD) {
                    var UserID = devGrid.GetRowKey(e.visibleIndex);
                    fn_OnCancelClick();
                    hidMachCD.SetText(MACHCD);
                    hidGridAction2.SetText("true");
                    devGrid2.PerformCallback(JSON.stringify(Param));
                }
            }
            */
            devGrid2.PerformCallback(JSON.stringify(Param));
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing2(s, e) {
            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    var INSPTYPECD = s.batchEditApi.GetCellValue(e.visibleIndex, 'F_INSPTYPECD');
            //    ddlLINEEdit.SetValue(LINECD)
            //    fn_OnControlDisableBox(s.GetEditor('F_USERID'), null);
            //    fn_OnControlDisableBox(s.GetEditor('F_USERNM'), null);
            //    fn_OnControlEnableComboBox(s.GetEditor('F_BANCD'), false);
            //    fn_OnControlDisableBox(s.GetEditor('F_INSPTYPENM'), null);
            //} else {
            //    fn_OnControlDisableBox(s.GetEditor('F_USERID'), null);
            //    fn_OnControlDisableBox(s.GetEditor('F_USERNM'), null);
            //    fn_OnControlEnableComboBox(s.GetEditor('F_BANCD'), true);
            //    fn_OnControlEnableBox(s.GetEditor('F_INSPTYPENM'), null);
            //}
        }

        // Edit Form 의 점검타입 선택값이 바뀌는 경우
        function fn_OnBANCDEditorSelectedIndexChanged(s, e) {
            devGrid2.GetEditor("F_INSPTYPENM").SetText(s.GetText());
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search" style="width:100%;">
                <table border="1" class="contentTable" >
                <colgroup>
                    <col style="width:7%" />
                    <col style="width:18%"/>
                    <col style="width:7%" />
                    <col style="width:18%"/>
                    <col style="width:7%" />
                    <col style="width:18%"/>
                    <col style="width:7%" />
                    <col style="width:18%"/>
                </colgroup>
                 <tr>
                    <td class="tdLabel">
                        <label>반</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="schF_BANCD" ClientInstanceName="schF_BANCD" targetCtrls="schF_LINECD" />
                    </td>          
                    <td class="tdLabel">                        
                        <label>라인</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:LineMulti runat="server" ID="schF_LINECD" ClientInstanceName="schF_LINECD" />
                    </td>
                     <td class="tdLabel">
                        <label>설비구분</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" ID="schF_MACHKIND" ClientInstanceName="schF_MACHKIND" F_CODEGROUP="40" />
                    </td>          
                    <td class="tdLabel">
                        <label>설비명</label>
                    </td>
                     <td class="tdInput">
                        <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" Width="100%" MaxLength="50"></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="height:10px;"></div>
        <div class="content">
            <div class="left">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="F_MACHCD" EnableViewState="False" EnableRowsCache="False" OnInitNewRow="devGrid_InitNewRow"
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
                            <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반" Visible="false">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="120px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인" Visible="false">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="120px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="120px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MACHNM" Caption="설비명" Width="260px">
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            </div>
            <div class="right">
                <section class="panel panel-default" style="height: 100%;">
                    <dx:ASPxTextBox ID="hidGridAction2" ClientInstanceName="hidGridAction2" runat="server" ClientVisible="false" />
                    <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="False"  Width="100%"
                        KeyFieldName="F_INSPTYPECD" EnableViewState="False" EnableRowsCache="False"
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
                            BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing2"/>
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
                            <dx:GridViewDataComboBoxColumn FieldName="F_INSPTYPECD" Name="F_INSPTYPECD" Caption="점검타입코드" Width="100px">
                                <Settings AllowSort="True" />
                                <PropertiesComboBox>
                                    <ClientSideEvents SelectedIndexChanged="fn_OnBANCDEditorSelectedIndexChanged" />
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="F_INSPTYPENM" Caption="점검타입명" Width="160px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_REMARK" Caption="비고" Width="180px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn FieldName="F_SORTNO" Caption="순번" Width="60px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataCheckColumn FieldName="F_USEYN" Caption="사용" Width="60px" />
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            </div>
            <dx:ASPxTextBox ID="hidMachCD" ClientInstanceName="hidMachCD" runat="server" ClientVisible="false" />
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxGridViewExporter ID="devGridExporter2" runat="server" GridViewID="devGrid2" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>

