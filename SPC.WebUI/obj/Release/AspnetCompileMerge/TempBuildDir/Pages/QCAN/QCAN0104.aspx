<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="QCAN0104.aspx.cs" Inherits="SPC.WebUI.Pages.QCAN.QCAN0104" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            
        }
        .search {
            width: 100%;
            
        }
        .content {
            width: 100%;
            height: 100%;
            display: table;
        }
        .left {
            width: 250px;
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
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
            devGridWork.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');         

            if (hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) {
                alert("품목을 선택하세요!!");
                return false;
            }

            if (hidUCINSPITEMCD.GetValue() == "" || hidUCINSPITEMCD.GetValue() == null) {
                alert("검사항목을 선택하세요!!");
                return false;
            }

            if (false == fn_WorkCheck())
                return false;

            devGrid.PerformCallback();
        }


        function fn_WorkCheck() {
            selectedKeys = devGridWork.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('좌측 공정을 선택하세요!!');
                return false;
            }

            if (selectedKeys.length > 50) {
                alert('공정은 최대 50개까지 선택 가능합니다!!');
                return false;
            }

            var WorkCD = selectedKeys.join('');

            txtCNT.SetValue(selectedKeys.length);
            txtWORKCD.SetValue(WorkCD)
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

        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {

        }

        var timerWorkCallback = null;

        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            
            //timerWorkCallback = setTimeout(fn_OnCallbackWorkGrid, 100);
        }

        function fn_OnCallbackWorkGrid() {
            if (fn_GetCastValue('hidUCINSPITEMCD') == null || fn_GetCastValue('hidUCINSPITEMCD') == '') {
                timerWorkCallback = setTimeout(fn_OnCallbackWorkGrid, 100);
            } else {
                alert('');
                clearTimeout(timerWorkCallback);
                timerWorkCallback = null;

                devGridWork.PerformCallback();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>                 
                    <label class="col-sm-1 control-label">검사항목</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목" targetCtrls="devGridWork" />
                        <dx:ASPxTextBox ID="txtMEAINSPCD" ClientInstanceName="txtMEAINSPCD" runat="server" ClientVisible="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="left">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxTextBox ID="txtWORKCD" ClientInstanceName="txtWORKCD" runat="server" ClientVisible="false" />
                    <dx:ASPxTextBox ID="txtCNT" ClientInstanceName="txtCNT" runat="server" ClientVisible="false" />
                    <dx:ASPxGridView ID="devGridWork" ClientInstanceName="devGridWork" runat="server" AutoGenerateColumns="false" 
                    KeyFieldName="F_WORKCD" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGridWork_CustomCallback" >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                    <SettingsBehavior AllowSort="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridWork" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                    ClientSideEvents-CheckedChanged="function(s, e) { devGridWork.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="F_WORKCD" Caption="공정코드" Width="100px" />
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px">
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataColumn>                        
                    </Columns>
                </dx:ASPxGridView>
                </section>
            </div>
            <div class="right">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_SERIALNO" EnableViewState="false" EnableRowsCache="false"
                    OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto"  />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명"  Width="120px"  >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명"  Width="160px"  >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>                            
                        <dx:GridViewBandColumn Caption="검사기준">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격"  Width="80px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격"  Width="80px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격"  Width="80px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="관리한계">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_UCLX" Caption="UCL"  Width="80px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_LCLX" Caption="LCL"  Width="80px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="측정Data">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_SIRYO" Caption="시료수"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAXVALUE" Caption="최대치"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MINVALUE" Caption="최소치"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>                        
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="공정능력">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_CP" Caption="Cp"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CPK" Caption="Cpk"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="공정분석">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_SD" Caption="표준편차"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>                        
                                <dx:GridViewDataColumn FieldName="F_3SPLUS" Caption="+3S"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>                        
                                <dx:GridViewDataColumn FieldName="F_3SMINUS" Caption="-3S"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>                        
                                <dx:GridViewDataColumn FieldName="F_SIGMAL" Caption="6시그마(장)"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_SIGMAS" Caption="6시그마(단)"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_OKRATE" Caption="수율(%)"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RERATE" Caption="예상불합격률"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_PPM" Caption="예상PPM"  Width="100px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                    </Columns>                
                </dx:ASPxGridView>
            </div>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
