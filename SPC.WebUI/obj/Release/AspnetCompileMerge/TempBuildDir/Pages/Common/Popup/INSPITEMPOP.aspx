<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="INSPITEMPOP.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Popup.INSPITEMPOP" %>

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
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            var parentParams = ''
            <%if (!gsVENDOR) {%>
            var parentCompCD = parent.fn_OnGetCompCD();
            parentParams += (parentParams == '') ? parentCompCD : '|' + parentCompCD;
            var parentFactCD = parent.fn_OnGetFactCD();
            parentParams += (parentParams == '') ? parentFactCD : '|' + parentFactCD;
            <%}%>

            devGrid.PerformCallback(parentParams);
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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // RowDblClick
        function fn_OnRowDblClick(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex)).split('|');
                if (hidOpenType.GetText() == 'FORM1') {          // 일반 Form 에서 호출한 경우
                    //devGrid.GetRowValues(e.visibleIndex, 'F_MEAINSPCD;F_INSPDETAIL;F_INSPCD;F_INSPNM;F_STANDARD;F_MAX;F_MIN;F_UCLX;F_LCLX;F_UCLR;F_SERIALNO;F_SIRYO;F_FREEPOINT', fn_OnGetRowValues1);
                    fn_OnGetRowValues1(rowKeys);
                }
                else if (hidOpenType.GetText() == 'FORM2') {          // 일반 Form 에서 호출한 경우
                    //devGrid.GetRowValues(e.visibleIndex, 'F_MEAINSPCD;F_INSPDETAIL;F_INSPCD;F_INSPNM;F_STANDARD;F_MAX;F_MIN;F_UCLX;F_LCLX;F_UCLR;F_SERIALNO;F_SIRYO;F_FREEPOINT', fn_OnGetRowValues2);
                    fn_OnGetRowValues2(rowKeys);
                }
                else {
                    //devGrid.GetRowValues(e.visibleIndex, 'F_MEAINSPCD;F_INSPDETAIL;F_INSPCD;F_INSPNM;F_STANDARD;F_MAX;F_MIN;F_UCLX;F_LCLX;F_UCLR;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_WORKCD;F_WORKNM', fn_OnGetRowValues);
                    fn_OnGetRowValues(rowKeys);
                }
            }
        }

        // OnRowValues
        function fn_OnGetRowValues(rowValues) {
            parent.fn_OnSetQCD34Values(rowValues);
            parent.fn_devPopupClose();
        }

        // OnRowValues
        function fn_OnGetRowValues1(rowValues) {
            parent.fn_OnSetQCD34_1Values(rowValues);
            parent.fn_devPopupClose();
        }

        // OnRowValues
        function fn_OnGetRowValues2(rowValues) {
            parent.fn_OnSetQCD34_2Values(rowValues);
            parent.fn_devPopupClose();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidOpenType" ClientInstanceName="hidOpenType" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-2 control-label">품목코드</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">공정코드</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtWORKCD" ClientInstanceName="txtWORKCD" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MEAINSPCD;F_INSPDETAIL;F_INSPCD;F_INSPNM;F_STANDARD;F_MAX;F_MIN;F_UCLX;F_LCLX;F_UCLR;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_WORKCD;F_WORKNM;F_ITEMCD" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="구분" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="구분" Width="70px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_DISPLAYNO" Caption="검사순서" Width="70px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="검사항목" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="검사규격" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SIRYO" Caption="시료수" Width="60px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_UCLX" Caption="UCLX" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LCLX" Caption="LCLX" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_UCLR" Caption="UCLR" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Caption="일련번호" Width="80px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Width="80px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_FREEPOINT" Caption="규격소수점자리수" Visible="false"></dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
