﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD0109_DACO.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD0109_DACO" %>

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
                }
                else if (s.cpResultCode == '2') {
                    fn_doSetGridEventAction('true');
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                    devGrid.PerformCallback();
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }

            devGrid.SetWidth(hidGridColumnsWidth.GetText());
            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_OnCustomButtonClick(s, e) {
            if (devGrid.GetRowKey(e.visibleIndex) == 'undefined' || devGrid.GetRowKey(e.visibleIndex) == null) {
                alert("품목코드를 저장 후 등록해주세요.");
                return;
            }

            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));

            if (e.buttonID == 'btnAddComp')
                fn_OnPopupQCD0122Add(encodeURIComponent(rowKeys));
        }

        function fn_OnPopupQCD0122Add(key) {
            pPage = rootURL + 'Pages/WERD/Popup/WERD0109_DACO_POP.aspx' +
                '?TITLE=품목별 거래처 등록' +
                '&CRUD=CDWS' +
                '&KEYFIELDS=' + key;
            fn_OnPopupOpen(pPage, '770', '400');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false"
                KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="EndlessPaging" PageSize="50" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" CustomButtonClick="fn_OnCustomButtonClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count ID="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품목코드" Width="160px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewCommandColumn Caption="거래처등록" Width="100px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton Text="등록" ID="btnAddComp" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
