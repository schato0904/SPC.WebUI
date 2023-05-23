<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0203_FND.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0203_FND" %>
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
            devGrid.SetHeight(height - 145);
            devGrid2.SetHeight(100);
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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_devGridRowClick(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {                
                var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
                
                devGrid2.PerformCallback(rowKeys);
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
                    <label class="col-sm-1 control-label">합격여부</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlJUDGE" ClientInstanceName="ddlJUDGE" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" />
                    </div>  
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>                 
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                    <label class="col-sm-1 control-label">Lot No.</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="txtLOTNO" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <table style="width:100%;">
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%" 
                            KeyFieldName="F_COMPCD;F_FACTCD;F_ITEMCD;F_WORKCD;F_SERIALNO;F_TSERIALNO;F_WORKDATE;F_WORKTIME" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                            OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" >
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowClick="fn_devGridRowClick" />
                            <Columns>                    
                                <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                                <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                                <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="120px" />
                                <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명"   Width="150px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKCD"  Caption="공정코드" Width="120px"/>
                                <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="170px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="작지번호"  Width="120px" >
                                    <CellStyle HorizontalAlign="Left" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKNO" Caption="LOTNO"  Width="140px" >
                                    <CellStyle HorizontalAlign="Left" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MOLDNO" Caption="통번호"  Width="100px" >
                                    <CellStyle HorizontalAlign="Left" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="120px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_JUDGE" Caption="판정" Width="80px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="검사기준">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="F_STANDARD"    Caption="규격"  Width="70" >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MAX"    Caption="상한규격"  Width="70"  >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MIN"    Caption="하한규격"  Width="70"  >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_UNIT"    Caption="단위"  Width="70" />
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No."  Width="100px"  />
                                <dx:GridViewBandColumn Caption="이상/조치내용">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="F_CAUSECD"  Caption="공정이상코드" Width="85px" >
                                            <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CAUSECD" >
                                            </PropertiesTextEdit>                            
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="F_CONTENTS" Caption="공정이상내용" Width="120px"   >
                                            <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CONTENTS" />
                                            <CellStyle HorizontalAlign="Left" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="F_MANAGECD" Caption="조치코드" Width="85px" >
                                            <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="F_RETURNCO" Caption="조치내용" Width="120px"   >
                                            <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_RETURNCO" />
                                            <CellStyle HorizontalAlign="Left" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고"  Width="200px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_SERIALNO"   Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_TSERIALNO"  Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_COMPCD"     Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_FACTCD"     Visible="false" />
                            </Columns>                
                        </dx:ASPxGridView>
                        <dx:ASPxGridView ID="devGrid3" ClientInstanceName="devGrid3" runat="server" AutoGenerateColumns="false" Width="100%" 
                            KeyFieldName="F_COMPCD;F_FACTCD;F_ITEMCD;F_WORKCD;F_SERIALNO;F_TSERIALNO;F_WORKDATE;F_WORKTIME" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" Visible="false"
                            OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" >
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <Columns>                    
                                <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                                <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                                <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="120px" />
                                <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명"   Width="150px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKCD"  Caption="공정코드" Width="120px"/>
                                <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="170px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="120px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_JUDGE" Caption="판정" Width="80px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="검사기준">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="F_STANDARD"    Caption="규격"  Width="70" >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MAX"    Caption="상한규격"  Width="70"  >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MIN"    Caption="하한규격"  Width="70"  >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_UNIT"    Caption="단위"  Width="70" />
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No."  Width="100px"  />
                                <dx:GridViewBandColumn Caption="이상/조치내용">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="F_CAUSECD"  Caption="공정이상코드" Width="85px" >
                                            <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CAUSECD" >
                                            </PropertiesTextEdit>                            
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="F_CONTENTS" Caption="공정이상내용" Width="120px"   >
                                            <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CONTENTS" />
                                            <CellStyle HorizontalAlign="Left" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="F_MANAGECD" Caption="조치코드" Width="85px" >
                                            <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="F_RETURNCO" Caption="조치내용" Width="120px"   >
                                            <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_RETURNCO" />
                                            <CellStyle HorizontalAlign="Left" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고"  Width="200px"  >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_SERIALNO"   Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_TSERIALNO"  Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_COMPCD"     Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_FACTCD"     Visible="false" />
                            </Columns>                
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid3" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td style="height: 100px; padding-top: 10px;">
                        <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="ROWNUM" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid2_CustomCallback" >
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Hidden" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                            <Columns>
                                <dx:GridViewBandColumn Caption="검사기준">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="F_STANDARD"    Caption="규격"  Width="70px" >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MAX"    Caption="상한규격"  Width="70px"  >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MIN"    Caption="하한규격"  Width="70px"  >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_UNIT"    Caption="단위"  Width="70px" />
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="관리한계">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="F_UCLX"    Caption="UCL"  Width="70px" >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_LCLX"    Caption="LCL"  Width="70px" >
                                            <CellStyle HorizontalAlign="Right" />
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataColumn FieldName="F_XBAR" Caption="XBar"  Width="80px"  >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_SERIALNO"  Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_TSERIALNO"  Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_WORKCD"  Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_COMPCD"  Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_FACTCD"  Visible="false" />
                            </Columns>                
                        </dx:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
