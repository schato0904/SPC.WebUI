<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TISP0301.aspx.cs" Inherits="SPC.WebUI.Pages.TISP.TISP0301" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
     <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .search {
            width: 100%;
        }
        .content {
            width: 100%;
            height: 100%;
        }
       .left {
            width: 1500px;
            height: 100%;
            display: table-cell;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            width:50%;
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
            devGrid2.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            <%if (!gsVENDOR){%>
                if (hidCOMPCD.GetValue() == "" || hidCOMPCD.GetValue() == null) {
                    alert("업체를 선택하세요!!");
                    return false;
                }
                if (hidFACTCD.GetValue() == "" || hidFACTCD.GetValue() == null) {
                    alert("공장을 선택하세요!!");
                    return false;
                }
           <%}%>

            if (hidUCITEMCD.GetValue() == "" && hidUCWORKPOPCD.GetValue() == "" && hidUCITEMCD.GetValue() == null && hidUCWORKPOPCD.GetValue() == null) {
                alert("품목이나 공정을 선택하세요!!");
                return false;
            }
            devGrid.PerformCallback();
            devGrid2.PerformCallback('||');
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

        // BatchEditStartEditing
        function fn_OnRowClick(s, e) {
            if (devGrid.GetRowKey(e.visibleIndex).split('|')[3].replace(/\s+$/, "") != "AAC501") {
                alert("외관인 항목은 상세내역을 조회 할 수 없습니다");
                devGrid2.PerformCallback("|||");
            }
            else {
                devGrid2.PerformCallback(devGrid.GetRowKey(e.visibleIndex));
            }

        }

        function OnCellOver(cell, col, row, rowcnt,s ) {
            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr').each(function (idx) {
                $(this).find('td').each(function (idx) {
                    this.style.backgroundColor = '#FFFFFF';
                    this.style.color = '#000000'
                });
            });

            if ($(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr[id$=DXDataRow' + row + ']>td').length == 7) {
                $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr[id$=DXDataRow' + row + ']>td').each(function (idx) {
                    if (idx == 5 || idx == 4 || idx == 3) {
                        this.style.backgroundColor = '#A0A0A0';
                        this.style.color = '#FFFFFF'
                    }
                });
            } else if ($(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr[id$=DXDataRow' + row + ']>td').length == 4) {
                $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr[id$=DXDataRow' + row + ']>td').each(function (idx) {
                    if (idx == 2 || idx == 1 || idx == 0) {
                        this.style.backgroundColor = '#A0A0A0';
                        this.style.color = '#FFFFFF'
                    }
                });
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="display:<%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" SingleDate="true"  />
                    </div>
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" />
                    </div>                   
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" machGubun="3" />
                    </div>                 
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" machGubun="3"  />
                    </div>                    
                </div>
            </div>
        </div>
        <div class="content">
            <div class="left" >
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_WORKCD;F_SERIALNO;F_INSPCD" EnableViewState="false" EnableRowsCache="false"
                    OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible"  HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"  RowClick="fn_OnRowClick" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="180px"  >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>                    
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명"    Width="180px"  >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="100px"  >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>                    
                        <dx:GridViewDataColumn FieldName="F_CNT" Caption="생산수"  Width="80px"  >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_NGCNT" Caption="부적합수"  Width="80px"  >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataColumn>                
                            
                        <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_INSPCD" Visible="false" />
                    </Columns>                
                </dx:ASPxGridView>
            </div>
            <div class="right">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxTextBox ID="hidGridAction2" ClientInstanceName="hidGridAction2" runat="server" ClientVisible="false" />
                    <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="False"  Width="100%" 
                        KeyFieldName="F_ROWNUM" EnableViewState="False" EnableRowsCache="False"
                        OnCustomCallback="devGrid2_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared" >
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                        <SettingsBehavior AllowSort="false" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" />
                        <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
                        </StatusBar>
                    </Templates>
                        <Columns>                            
                           <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="90px"  />
                            <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목"  Width="70px"  >
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>       
                            <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>       
                            <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>       
                            <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>       
                            <dx:GridViewDataColumn FieldName="F_SD" Caption="편차"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>    
                            
                            
                            <dx:GridViewDataColumn FieldName="F_ROWNUM" Visible="false"/>   
                        </Columns>
                    </dx:ASPxGridView>
                </section>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxGridViewExporter ID="devGridExporter2" runat="server" GridViewID="devGrid2" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            </div>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
