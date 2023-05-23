<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="COMPPOP.aspx.cs" Inherits="SPC.WebUI.Pages.COMM.Popup.COMPPOP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_OnSearchClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(370);
        }

        // 조회
        function fn_OnSearchClick() {


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
        function fn_OnExcelClick() { }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            //fn_doSetGridEventAction('false');

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
            // e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // RowDblClick
        function fn_OnRowDblClick(s, e) {



            var selectedKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var rowKey = selectedKeys.split('|');
            parent.fn_OnSettingComp(rowKey);


            parent.fn_devPopupClose();



        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="COMPSearch" >
            <table class="InputTable">
                <colgroup>
                    <col style="width: 20%" />
                    <col style="width: 60%" />
                    <col style="width: 20%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>업체명</label>
                    </td>
                    <td class="tdContentR">
                        <dx:ASPxTextBox ID="COMPCD" ClientInstanceName="COMPCD" runat="server" Width="100%" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="COMPNM" ClientInstanceName="COMPNM" runat="server" Width="100%"></dx:ASPxTextBox>
                    </td>
                    <td class="tdContent" style="text-align:right">
                        <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick();return false;" style="padding-top: 3px; padding-bottom: 3px;">
                            <i class="fa fa-search"></i>
                            <span class="text">조회</span>
                        </button>
                    </td>
                </tr>
            </table>
        </div>


        <div style="padding-top:10px" >
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPNM;F_REGION;F_TELNO;F_ADDRESS;F_BIGO;F_CSCD;" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>

                    <dx:GridViewDataColumn FieldName="F_NO" Caption="NO" Width="15%" />
                    <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="40%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_REGION" Caption="지역" Width="45%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_TELNO" Width="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_ADDRESS" Width="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_BIGO" Width="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_CSCD" Width="0px" CellStyle-Border-BorderWidth="0px" />
                  <%--  <dx:GridViewDataColumn FieldName="F_INSTAL" Width="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_MANAGE" Width="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_COUNDT" Width="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_REMARK" Width="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_REASON" Width="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_COUNREMARK" Width="0px" CellStyle-Border-BorderWidth="0px" />--%>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
