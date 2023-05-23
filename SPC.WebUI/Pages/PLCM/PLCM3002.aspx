<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM3002.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM3002" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            devGrid.PerformCallback();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height - 20);
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
            fn_grid(s, e);
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_grid(s, e) {
            var arrData = new Array();

            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr[id$=DXDataRow0]>td:gt(0)').each(function (idx) {
                arrData[idx] = { "CODE": $(this).text() };
            });

            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr:gt(0)').each(function (idx) {
                arrData[idx] = { "NAME": $(this).text() };
            });

            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr').each(function (idx) {
                $(this).find('td').each(function (idx) {
                    $(this).on("dblclick", function () {
                        var Date;
                        idx--;

                        if (idx < 10)
                            Date = '0' + idx;
                        else
                            Date = idx;

                        if (idx > 0) {
                            fn_OnCustomButtonClick(devGrid.GetSelectedKeysOnPage(), hidUCFROMDT.GetText() + '-' + Date);
                        }
                    });
                });
            });
        }

        function fn_OnCustomButtonClick(_MACH, _DATE) {
            var rootURL = '/' + window.location.href.replace('http://', '').split('/')[1] + '/';
            var KEYS = _MACH + '|' + _DATE;
            var pPage = rootURL + 'Pages/PLCM/Popup/PLCM3002POP.aspx' +
                  '?TITLE=설비작업현황' +
                  '&CRUD=' +
                  '&KEYFIELDS=' + KEYS;
            fn_OnPopupOpen(pPage, '800', '600', fn_OnSearchClick());
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="InputTable" style="width: 100%;">
                <colgroup>
                    <col style="width: 10%" />
                    <col style="width: 15%" />
                    <col style="width: 10%" />
                    <col style="width: 15%" />
                    <col style="width: 10%" />
                    <col style="width: 15%" />
                    <col style="width: 10%" />
                    <col style="width: 15%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">작업일자</td>
                    <td class="tdContentR">
                        <ucCTF:Date runat="server" ID="ucDate" SingleDate="true" MonthOnly="true" />
                    </td>
                    <td class="tdContentL" colspan="6"></td>
                </tr>
            </table>
        </div>
        <div style="clear: both; height: 2px;"></div>
        <p style="height: 10px;">※ 더블클릭 시 일자별 현황을 조회 할 수 있습니다.</p>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%"
                KeyFieldName="F_MACHCD" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback"
                OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
