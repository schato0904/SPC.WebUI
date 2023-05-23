﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM3004.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM3004" %>

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
            if (schF_MASTERID.GetValue() == null || schF_MASTERID.GetValue() == '') {
                alert('마스터ID를 선택하세요.');
                return false;
            }
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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_schF_FROMYMD_ValueChanged(s, e) {
            schF_MASTERID.PerformCallback();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="InputTable" style="width: 100%;">
                <colgroup>
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                    <col style="width: 8%" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdTitle">작업일자</td>
                    <td class="tdContent">
                        <ucCTF:Date runat="server" ID="ucDate" Changed="fn_schF_FROMYMD_ValueChanged" />
                    </td>
                    <td class="tdTitle">마스터ID</td>
                    <td class="tdContentR">
                        <dx:ASPxComboBox runat="server" ID="schF_MASTERID" ClientInstanceName="schF_MASTERID" IncrementalFilteringMode="None" ValueType="System.String"  OnCallback="schF_MASTERID_Callback">
                            <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdContentL" colspan="2"></td>
                </tr>
            </table>
        </div>
        <div style="clear: both; height: 2px;"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback"
                OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ROWNUM" Caption="NO" Width="35px" />
                    <dx:GridViewDataColumn FieldName="HEADER01" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER02" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER03" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER04" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER05" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER06" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER07" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER08" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER10" Width="160px" />
                    <dx:GridViewDataColumn FieldName="HEADER12" Width="160px" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
