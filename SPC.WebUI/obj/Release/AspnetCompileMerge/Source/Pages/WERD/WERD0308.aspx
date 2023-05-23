<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD0308.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD0308" %>

<%-- 공정검사 일보 조회 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }

        .search {
            width: 100%;
            display: table-row;
        }

        .content {
            width: 100%;
            height: 100%;
            display: table-row;
        }

        .left {
            width: 300px;
            height: 100%;
            display: table-cell;
            padding-right: 10px;
            vertical-align: top;
        }

        .right {
            height: 100%;
            display: table-cell;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = '';

        $(document).ready(function () {
            fn_doSetGridEventAction('true');
            var oParams = 'select;' + _selectedRowKeyValue;
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".InputTable").height() > 0 ? $(".InputTable").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        function fn_GetDocumentHeight() {
            if (fn_HasDocumentScroll()) {
                return Math.max($(document).height(), $('.content-panel>form').height());
            } else {
                return $(document).height();
            }
        }

        function fn_HasDocumentScroll() {
            return $('.content-panel').css('overflow-y') == 'scroll';
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid.PerformCallback();
        }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
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

        // 오류시
        function fn_OnCallbackError(s, e) {
            alert(e);
        }

        // Grid End Callback
        function fn_OnGridEndCallback(s, e) {
            fn_pagerPerformCallback(s.cpResultMsg);
        }


        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
        }


        // 인쇄
        function fn_OnPrintClick() { }

        // Grid End Callback
        function fn_devGridOnEndCallback(s, e) {
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
            else {
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">

        <%-- 하단 조회조건 --%>
        <table class="InputTable">
            <colgroup>
                <col style="width:10%" />
                <col style="width:23%" />
                <col style="width:10%" />
                <col style="width:23%" />
                <col style="width:10%" />
                <col style="width:24%" />
            </colgroup>
            <tr>
                <td class="tdTitle">
                    <label>작업일자</label>
                </td>
                <td class="tdContent">
                   <ucCTF:Date ID="ucDate" runat="server" />
                </td>
                <td class="tdTitle">
                    <label>작업자</label>
                </td>
                <td class="tdContent">
                    <ucCTF:User runat="server" ID="ucUser" ClientInstanceID="srcF_INSPUSER" />
                </td>
               <%-- <td class="tdTitle">
                    <label>초중종</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox ID="ddlFirstitem" runat="server" ClientInstanceName="ddlFirstitem" ValueField="F_GBN" >
                        <ClientSideEvents Init="fn_OnControlDisable" />
                        <Items>
                            <dx:ListEditItem Text="전체" Value="" />                                
                            <dx:ListEditItem Text="초품" Value="0" />
                            <dx:ListEditItem Text="중품" Value="1" />
                            <dx:ListEditItem Text="종품" Value="2" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>--%>
            </tr>
         </table>
        <div class="form-group"></div>

        <%-- 하단 그리드 --%>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="" EnableViewState="false" EnableRowsCache="false" OnCustomSummaryCalculate="devGrid_CustomSummaryCalculate"
                OnCustomCallback="devGrid_CustomCallback"
                SettingsDataSecurity-AllowEdit="false">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" ShowFooter="true"/>
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="NUMBERS" Caption="No."  Width="60px">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_USERID" Caption="검사자ID" Width="12%"  CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="검사자명" Width="13%"  CellStyle-HorizontalAlign="Left">
                        <FooterTemplate>
                            <span>합계</span>
                        </FooterTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PRODUCTQTY" Caption="생산수량" Width="13%"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_INSPQTY" Caption="검사수량" Width="13%"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_NGQTY" Caption="부적합수량" Width="13%"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_NGRATE" Caption="부적합률(%)" Width="13%"  CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PPM" Caption="PPM" Width="13%"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LOSSAMT" Caption="손실금액(원)" Width="13%"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                </Columns>
                <TotalSummary>

                    <dx:ASPxSummaryItem FieldName="F_USERID" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_PRODUCTQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_INSPQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_NGQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_NGRATE" SummaryType="Custom" DisplayFormat="{0:##0.##}" />
                    <dx:ASPxSummaryItem FieldName="F_PPM" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_LOSSAMT" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    


                </TotalSummary>
            </dx:ASPxGridView>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
    </div>
</asp:Content>
