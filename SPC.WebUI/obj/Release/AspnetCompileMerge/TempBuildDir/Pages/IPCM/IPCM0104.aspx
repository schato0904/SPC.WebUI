<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="IPCM0104.aspx.cs" Inherits="SPC.WebUI.Pages.IPCM.IPCM0104" %>
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
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
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

        function fn_OnRowDblClick(s, e) {
            fn_OnPopupIPCM0104(devGrid.GetRowKey(e.visibleIndex));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                
                <div class="form-group" style="display: <%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일</label>
                    <label class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlDATETP" runat="server" ClientInstanceName="ddlDATETP" Width="100%">
                            <Items>
                                <dx:ListEditItem Text="제기일" Value="RQ" Selected="true" />
                                <dx:ListEditItem Text="회신일" Value="RS" />
                                <dx:ListEditItem Text="확인일" Value="CF" />
                            </Items>
                        </dx:ASPxComboBox>
                    </label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Date runat="server" ID="ucDate" />
                    </div>
                    <label class="col-sm-1 control-label">진행단계</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlPROGRESS" runat="server" ClientInstanceName="ddlPROGRESS" Width="100%">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true" />
                                <dx:ListEditItem Text="제기" Value="1" />
                                <dx:ListEditItem Text="회신" Value="2" />
                                <dx:ListEditItem Text="확인" Value="3" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">개선여부</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlIMPROVE" runat="server" ClientInstanceName="ddlIMPROVE" Width="100%">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true" />
                                <dx:ListEditItem Text="개선" Value="1" />
                                <dx:ListEditItem Text="미개선" Value="0" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">완료여부</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlCOMPLETE" runat="server" ClientInstanceName="ddlCOMPLETE" Width="100%">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true" />
                                <dx:ListEditItem Text="완료" Value="1" />
                                <dx:ListEditItem Text="미완료" Value="0" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD;F_INDXNO;F_ITEMCD;F_RQUSID" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Visible" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_PROGRESS" Caption="진행상태" Width="70px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_BIMPROVE" Caption="개선여부" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TRNM" Caption="부적합유형" Width="120px">
                        <CellStyle HorizontalAlign="left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RQDATE" Caption="이상제기일" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RQUSNM" Caption="제기자" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RQRCDT" Caption="회신요청일" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RSDATE" Caption="회신일" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RSUSNM" Caption="회신자" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CFDATE" Caption="확인일" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CFUSNM" Caption="확인자" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CTRLNO" Caption="발행번호" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="180px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px"></dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="F_UNSTTP" Caption="부적합유형코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_DEPARTCD" Caption="대책부서코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEASGD" Caption="대책등급" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_RQUSID" Caption="제기자" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
