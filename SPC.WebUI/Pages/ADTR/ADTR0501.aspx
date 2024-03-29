﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0501.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0501" %>
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" SingleDate="true" />
                    </div>
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" />
                    </div>
                </div>                
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKCD;F_LINECD;F_MEAINSPCD" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"   />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="150px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="150px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="180px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>                                        
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CNT"  Caption="입력수" Width="50px" />
                    <dx:GridViewDataColumn FieldName="T01"  Caption="8" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T02"  Caption="9" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T03"  Caption="10" Width="40px"  />
                    <dx:GridViewDataColumn FieldName="T04"  Caption="11" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T05"  Caption="12" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T06"  Caption="13" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T07"  Caption="14" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T08"  Caption="15" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T09"  Caption="16" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T10"  Caption="17" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T11"  Caption="18" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T12"  Caption="19" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T13"  Caption="20" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T14"  Caption="21" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T15"  Caption="22" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T16"  Caption="23" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T17"  Caption="24" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T18"  Caption="1" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T19"  Caption="2" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T20"  Caption="3" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T21"  Caption="4" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T22"  Caption="5" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T23"  Caption="6" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T24"  Caption="7" Width="40px" />
                        
                    <%--여기서부터 히든 필드--%>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Visible="false" />
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKCD;F_LINECD;F_MEAINSPCD" EnableViewState="false" EnableRowsCache="false" Visible="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"   />
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="150px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="150px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="180px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>                                        
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CNT"  Caption="입력수" Width="50px" />
                    <dx:GridViewDataColumn FieldName="T01"  Caption="8" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T02"  Caption="9" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T03"  Caption="10" Width="40px"  />
                    <dx:GridViewDataColumn FieldName="T04"  Caption="11" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T05"  Caption="12" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T06"  Caption="13" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T07"  Caption="14" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T08"  Caption="15" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T09"  Caption="16" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T10"  Caption="17" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T11"  Caption="18" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T12"  Caption="19" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T13"  Caption="20" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T14"  Caption="21" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T15"  Caption="22" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T16"  Caption="23" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T17"  Caption="24" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T18"  Caption="1" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T19"  Caption="2" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T20"  Caption="3" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T21"  Caption="4" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T22"  Caption="5" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T23"  Caption="6" Width="40px" />
                    <dx:GridViewDataColumn FieldName="T24"  Caption="7" Width="40px" />
                        
                    <%--여기서부터 히든 필드--%>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Visible="false" />
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid2" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="100" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
