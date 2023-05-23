<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0302.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0302" %>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="contentTable">
                <colgroup>
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                    <col style="width:7%;" />
                    <col style="width:13%;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">반</td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="schF_BANCD" ClientInstanceName="schF_BANCD" targetCtrls="schF_LINECD" />
                    </td>
                    <td class="tdLabel">라인</td>
                    <td class="tdInput">
                        <ucCTF:LineMulti runat="server" ID="schF_LINECD" ClientInstanceName="schF_LINECD" targetCtrls="schF_MACHIDX" />
                    </td>
                    <td class="tdLabel">설비분류</td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" ID="schF_MACHKIND" ClientInstanceName="schF_MACHKIND" F_CODEGROUP="40" targetCtrls="schF_MACHIDX" />
                    </td>
                    <td class="tdLabel">설비</td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="schF_MACHIDX" ClientInstanceName="schF_MACHIDX" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" ValueField="F_MACHIDX" DropDownStyle="DropDownList"
                            OnCallback="schF_MACHIDX_Callback" NullText="반을 선택하세요">
                            <Columns>
                                <dx:ListBoxColumn FieldName="F_MACHIDX" Visible="false" />
                                <dx:ListBoxColumn FieldName="F_MACHCD" Caption="설비코드" />
                                <dx:ListBoxColumn FieldName="F_MACHNM" Caption="설비명" />
                            </Columns>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdLabel">설비명</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" Width="100%" MaxLength="50" />
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_INSPIDX" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="EndlessPaging" PageSize="50" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPNO" Caption="점검부위번호" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_INSPORDER" Caption="점검순서" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="220px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPKINDNM" Caption="판정방식" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_INSPREMARK" Caption="점검내용" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPWAY" Caption="점검방법" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_VIEWSTAND" Caption="표시규격" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_STAND" Caption="기준" Width="90px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한" Width="90px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한" Width="90px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CYCLENM" Caption="점검주기" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_CHASU" Caption="일점검차수" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_UNITNM" Caption="단위" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_USEYN" Caption="점검제외" Width="80px" />

                    <dx:GridViewDataColumn FieldName="F_INSPIDX" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>