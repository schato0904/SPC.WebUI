<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="QCAN0102.aspx.cs" Inherits="SPC.WebUI.Pages.QCAN.QCAN0102" %>
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
            devGrid.SetHeight(height);
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

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_BANCD", s, e);
            fn_OnBatchValidate("F_BANNM", s, e);
            
            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {                
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlDisableBox(editor, null);                
            } else {
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlEnableBox(editor, null);
            }
        }

        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            txtSERIALNO.SetText(resultValues[10]);
            //alert(txtSERIALNO.GetText());
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
                        <ucCTF:Fact ID="ucFact" runat="server" targetCtrls="ddlBAN" nullText="선택" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>
<%--                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" targetCtrls="ddlWORK" />
                    </div>  --%>                  
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">&nbsp;</label>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" targetCtrls="ddlWORK" />
                    </div>                
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Work ID="ucWork" runat="server" />
                    </div>                        
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드"  Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명"   Width="250px"   >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CNT" Caption="검사항목수"  Width="120px"  />
                    <dx:GridViewDataColumn FieldName="F_GBNM"  Caption="구분"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewBandColumn Caption="공정능력">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_DATA1"  Caption="0.67이하" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false">
                                    </PropertiesTextEdit>                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA2" Caption="1.0" Width="70px"   >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false"/>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA3" Caption="1.33" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA4" Caption="1.67" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA5" Caption="2.0" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA6" Caption="2.0초과" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false" Visible="false"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드"  Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명"   Width="250px"   >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CNT" Caption="검사항목수"  Width="120px"  />
                    <dx:GridViewDataColumn FieldName="F_GBNM"  Caption="구분"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewBandColumn Caption="공정능력">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_DATA1"  Caption="0.67이하" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false">
                                    </PropertiesTextEdit>                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA2" Caption="1.0" Width="70px"   >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false"/>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA3" Caption="1.33" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA4" Caption="1.67" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA5" Caption="2.0" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_DATA6" Caption="2.0초과" Width="70px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid2" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
