<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRK0204.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.LTRK0204" %>
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
            devTree.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            if (!fn_OnValidate()) return false;

            devTree.PerformCallback();
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

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            if (fn_GetCastText('srcF_PRODUCTNO') == '') {
                alert('Lot Tracking을 진행할 생산번호를 입력하세요!!');
                fn_Focus('srcF_PRODUCTNO');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="contentTable">
                <colgroup>
                    <col style="width:80px;" />
                    <col style="width:200px;" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">생산번호</td>
                    <td class="tdInput tdContentR">
                        <dx:ASPxTextBox ID="srcF_PRODUCTNO" ClientInstanceName="F_PRODUCTNO" runat="server" Width="100%" />
                    </td>
                    <td class="tdInput tdContentL"></td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxTreeList ID="devTree" ClientInstanceName="devTree" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_CURRENTCD" ParentFieldName="F_PARENTCD" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devTree_CustomCallback">
                <Styles>
                    <AlternatingNode Enabled="True" />
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" GridLines="Both" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AutoExpandAllNodes="true" AllowDragDrop="False" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:TreeListDataColumn FieldName="F_ITEMCD" Caption="품번" Width="150px" />
                    <dx:TreeListDataColumn FieldName="F_ITEMNM" Caption="품명" Width="300px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:TreeListDataColumn>
                    <dx:TreeListDataColumn FieldName="F_GROUPCD" Caption="품목그룹" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_GUBNCD" Caption="품목구분코드" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_GUBNNM" Caption="품목구분" Width="100px" />
                    <dx:TreeListDataColumn FieldName="F_ORDERNO" Caption="입고(작지)번호" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:TreeListDataColumn>
                    <dx:TreeListDataColumn FieldName="F_LOT" Caption="투입(생산)LOT" Width="110px" />
                    <dx:TreeListDataColumn FieldName="F_DATE" Caption="투입(생산)일" Width="150px" />
                    <dx:TreeListSpinEditColumn FieldName="F_CNT" Caption="투입(생산)수량" Width="110px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:TreeListSpinEditColumn>
                    <dx:TreeListDataColumn FieldName="F_WORKMAN" Caption="작업자" Width="110px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:TreeListDataColumn>
                    <dx:TreeListDataColumn FieldName="F_UNITNM" Caption="단위" Width="80px" />
                    <dx:TreeListDataColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_WORKNM" Caption="공정명" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:TreeListDataColumn>
                    <dx:TreeListDataColumn FieldName="F_EQUIPCD" Caption="설비코드" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_EQUIPNM" Caption="설비명" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:TreeListDataColumn>
                </Columns>
            </dx:ASPxTreeList>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>