<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD0202.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD0202" %>

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
                <td class="tdTitle">품목</td>
                <td class="tdContent">
                    <ucCTF:Item1 ID="ucItem1" runat="server" usedModel="true" useWERD="true" />
                </td>
                <td class="tdTitle">
                    <label>공정코드</label>
                </td>
                <td class="tdContent">
                    <ucCTF:WorkPOP1 ID="ucWorkPOP1" runat="server" useWERD="true" validateFields="hidUCITEMCD1|품목"  />
                </td>                
            </tr>
         </table>
        <div class="form-group"></div>

        <%-- 하단 그리드 --%>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;F_ITEMCD;F_WORKCD;F_DAYPRODUCTNO;F_GUBUN;F_PLANNO" EnableViewState="false" EnableRowsCache="false" OnCustomSummaryCalculate ="devGrid_CustomSummaryCalculate"
                OnCustomCallback="devGrid_CustomCallback"
                SettingsDataSecurity-AllowEdit="false">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" ShowFooter="true" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false"/>
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_DAYPRODUCTNO" Caption="작업일보NO" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="작업일자" Width="80px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="120px" CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" Width="200px" CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>                    
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Width="100px"  CellStyle-HorizontalAlign="Center"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="150px"  CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No." Width="130px" CellStyle-HorizontalAlign="Left">
                     <FooterTemplate>
                            <span>합계</span>
                        </FooterTemplate>
                    </dx:GridViewDataColumn>                   
                    <dx:GridViewDataTextColumn FieldName="F_PRODUCTQTY" Caption="생산수량" Width="70px"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_INSPQTY" Caption="검사수량" Width="70px"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_NGQTY" Caption="부적합수량" Width="75px"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_NGRATE" Caption="부적합률(%)" Width="90px"  CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PPM" Caption="PPM" Width="90px"  CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="검사자" Width="100px" CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_GUBUN" Caption="작업시간(F)" Width="80px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_PLANNO" Caption="작지번호" Width="100px" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                </Columns>
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="F_DAYPRODUCTNO" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_WORKDATE" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_ITEMCD" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_ITEMNM" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_WORKCD" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_LOTNO" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_PRODUCTQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_INSPQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_NGQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_NGRATE" SummaryType="Custom" DisplayFormat="{0:##0.##}" />
                    <dx:ASPxSummaryItem FieldName="F_PPM" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_USERNM" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_GUBUN" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_PLANNMO" Visible="false" />
                    
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
