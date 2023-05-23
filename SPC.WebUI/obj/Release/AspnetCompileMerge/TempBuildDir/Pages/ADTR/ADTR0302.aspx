<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0302.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0302" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var Index = 0;
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
            //fn_OnBatchValidate("F_BANCD", s, e);
            //fn_OnBatchValidate("F_BANNM", s, e);            
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {            
            if (parseInt(e.visibleIndex, 10) >= 0) {
                fn_OnControlEnableBox(s.GetEditor('F_CONTENTS'), null);
                fn_OnControlEnableBox(s.GetEditor('F_RETURNCO'), null);
            } else {
                fn_OnControlDisableBox(s.GetEditor('F_CONTENTS'), null);
                fn_OnControlDisableBox(s.GetEditor('F_RETURNCO'), null);
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
                    <div class="col-sm-4 control-label">
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="true"  />
                    </div>                                        
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false"  Width="100%"
                    KeyFieldName="F_NOWORKNO" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize"
                    OnHtmlRowPrepared="devGrid_HtmlRowPrepared" >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  HorizontalScrollBarMode="Visible" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                    <Columns>                        
                        <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="90px" />
                        <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="90px" />                     
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_FROMDATE" Caption="시작일자" Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_TODATE" Caption="종료일자" Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="120px" />
                        <dx:GridViewDataColumn FieldName="F_CODENM" Caption="무작업사유" Width="150px" >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_CONTENTS" Caption="상세내용" Width="300px" >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn FieldName="F_NOWORKNO"  Visible="false" />                        
                    </Columns>                
                </dx:ASPxGridView>
             <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false"  Width="100%"
                    KeyFieldName="F_NOWORKNO" EnableViewState="false" EnableRowsCache="false" Visible="false"
                    OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize"
                    OnHtmlRowPrepared="devGrid_HtmlRowPrepared" >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  HorizontalScrollBarMode="Visible" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Columns>                        
                        <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="90px" />
                        <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="90px" />                     
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_FROMDATE" Caption="시작일자" Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_TODATE" Caption="종료일자" Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="120px" />
                        <dx:GridViewDataColumn FieldName="F_CODENM" Caption="무작업사유" Width="150px" >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_CONTENTS" Caption="상세내용" Width="300px" >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn FieldName="F_NOWORKNO"  Visible="false" />                        
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
