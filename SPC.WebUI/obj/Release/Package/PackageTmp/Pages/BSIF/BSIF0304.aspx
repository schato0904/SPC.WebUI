<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0304.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0304" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .GridCss {
            display:none
        }

    </style>
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
            if (txtUCITEMCD.GetText() == '' || txtUCITEMNM.GetText() == '' ) {
                alert('품목정보를 입력하세요!!');
                txtUCITEMCD.Focus();
                return false;
            }
            fn_doSetGridEventAction('true');

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
            //fn_OnBatchValidate("F_MEAINSPCD", s, e);
            //fn_OnBatchValidate("F_INSPDETAIL", s, e);
            
            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {                
                //var editor = s.GetEditor('F_MEAINSPCD');
                fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_INSPCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_SERIALNO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_INSPDETAIL'), null);
                fn_OnControlDisableBox(s.GetEditor('F_STANDARD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MAX'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MIN'), null);
            } else {
                //var editor = s.GetEditor('F_MEAINSPCD');
                //fn_OnControlEnableBox(editor, null);
            }
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
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlWORK" />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" targetCtrls="ddlWORK" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Work ID="ucWork" runat="server" />
                    </div>
                </div>
            </div>
            <section class="panel panel-default">
                <table class="table table-striped m-b-none">
                    <thead>
                        <tr>
                            <th colspan="8">공정이상설정</th>                    
                        </tr>
                        <tr>
                            <th class="col-sm-1 control-label">1</th>
                            <th class="col-sm-2 control-label">관리이탈</th>
                            <th class="col-sm-1 control-label">2</th>
                            <th class="col-sm-2 control-label">런(RUN)</th>
                            <th class="col-sm-1 control-label">3</th>
                            <th class="col-sm-2 control-label">경향</th>
                            <th class="col-sm-1 control-label">4</th>
                            <th class="col-sm-2 control-label">상하상하</th>
                        </tr>
                        <tr>
                            <th class="col-sm-1 control-label">5</th>
                            <th class="col-sm-2 control-label">영역경고</th>
                            <th class="col-sm-1 control-label">6</th>
                            <th class="col-sm-2 control-label">영역예고</th>
                            <th class="col-sm-1 control-label">7</th>
                            <th class="col-sm-2 control-label">중심선근처의점</th>
                            <th class="col-sm-1 control-label">8</th>
                            <th class="col-sm-2 control-label">상포가심한점</th>
                        </tr>
                    </thead>
                </table>
            </section>
        </div>
        
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_INSPCD;F_SERIALNO" EnableViewState="false" EnableRowsCache="false"
                OnBatchUpdate="devGrid_BatchUpdate" OnCustomCallback="devGrid_CustomCallback"
                OnInitNewRow="devGrid_InitNewRow" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_INSPCD" Caption="검사구분" Width="1px" />  
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px" />                                      
                    <dx:GridViewDataTextColumn FieldName="F_WORKNM" Caption="공정명" Width="200px" />
                    <dx:GridViewDataTextColumn FieldName="F_SERIALNO" Caption="일련번호" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목명" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>                    
                    <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="검사기준" Width="100px" >
                        <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="상한규격" Width="100px" >
                        <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="하한규격" Width="100px" >
                        <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewBandColumn Caption="공정이상설정">
                        <Columns>
                            <dx:GridViewDataCheckColumn FieldName="F_SPECBREAK" Caption="1" Width="60px" />
                            <dx:GridViewDataCheckColumn FieldName="F_RUNWORK1" Caption="2" Width="60px" />
                            <dx:GridViewDataCheckColumn FieldName="F_RUNWORK2" Caption="3" Width="60px" />
                            <dx:GridViewDataCheckColumn FieldName="F_RUNWORK3" Caption="4" Width="60px" />
                            <dx:GridViewDataCheckColumn FieldName="F_SLANTWORK1" Caption="5" Width="60px" />
                            <dx:GridViewDataCheckColumn FieldName="F_SLANTWORK2" Caption="6" Width="60px" />
                            <dx:GridViewDataCheckColumn FieldName="F_SLANTWORK3" Caption="7" Width="60px" />
                            <dx:GridViewDataCheckColumn FieldName="F_SLANTWORK4" Caption="8" Width="60px" />
                        </Columns>
                    </dx:GridViewBandColumn>                    
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
