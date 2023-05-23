<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BORD0101.aspx.cs" Inherits="SPC.WebUI.Pages.BORD.BORD0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .devGridtitle {
            cursor:pointer;
            
        }
    </style>
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
            devGrid.SetHeight(height-10);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            pPage = 'BORD0102.aspx' +
                   '?TITLE=글작성' +
                   '&CRUD=WS'
            fn_OnPopupOpen(pPage, 800, 600);
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
        function fn_OnExcelClick() { }

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

        function fn_write() {
            pPage = 'BORD0102.aspx' +
                    '?TITLE=글작성' +
                    '&CRUD=WS'
            fn_OnPopupOpen(pPage, 800, 600);
        }

        function fn_RowDblClick(s, e) {
            var crud = "";

            if (Left(devGrid.GetRowKey(e.visibleIndex).split('|')[3].replace(/\s+$/, ""), 2) == "Re") {
                var pPage = './Popup/BORD0103POP.aspx?TITLE=상세내용&CRUD=UD&KEYFIELDS=' + devGrid.GetRowKey(e.visibleIndex);
                fn_OnPopupOpen(pPage, 800, 600);
            }
            else if (devGrid.GetRowKey(e.visibleIndex).split('|')[2].replace(/\s+$/, "") == "공지사항") {
                var key = devGrid.GetRowKey(e.visibleIndex).split('|');
                if (key[4] == "<%=gsCOMPCD %>") {
                crud = "UD";
                }

                var pPage = './Popup/BORD0101POP.aspx?TITLE=공지사항&CRUD='+crud+'&KEYFIELDS=' + devGrid.GetRowKey(e.visibleIndex);
                fn_OnPopupOpen(pPage, 800, 600);
            }
            else {
                //F_NUMBER;F_COMPNMKR;F_GBNNM;F_TITLE;F_COMPCD;F_GBN
                var key = devGrid.GetRowKey(e.visibleIndex).split('|');
                if (key[4] == "<%=gsCOMPCD %>") {
                    crud = "UD";
                } else {
                    crud = "C";
                }

                var pPage = './Popup/BORD0101POP.aspx?TITLE=요청사항&CRUD=' + crud + '&KEYFIELDS=' + devGrid.GetRowKey(e.visibleIndex);
                fn_OnPopupOpen(pPage, 800, 600);
            }
        }

        function Left(str, n) {
            if (n <= 0)
                return "";
            else if (n > String(str).length)
                return str;
            else
                return String(str).substring(0, n);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_NUMBER;F_COMPNMKR;F_GBNNM;F_TITLE;F_COMPCD;F_GBN" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_RowDblClick" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_NUMBER"  Caption="번호" Width="80px" />                        
                    <dx:GridViewDataColumn FieldName="F_GBNNM" Caption="구분"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_TITLE" Caption="제목" Width="100%"   CellStyle-CssClass="devGridtitle"  >
                        <CellStyle HorizontalAlign="Left" />                       
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_COMPNMKR" Caption="업체"  Width="100PX" />
                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="게시자" Width="120px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSDT" Caption="게시일" Width="120px"  />
                    <dx:GridViewDataColumn FieldName="F_REPLY" Visible="false"  />
                    <dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_GBN" Visible="false"  />
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
        
    </div>
</asp:Content>
