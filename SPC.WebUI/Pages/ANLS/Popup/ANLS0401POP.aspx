<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0401POP.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.Popup.ANLS0401POP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            //devGrid.PerformCallback();
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

        // OnRowValues
        function fn_OnGetRowValues(rowValues) {
            parent.fn_GetInspcdItem(rowValues);
            parent.fn_devPopupClose();
        }

        function fn_Select() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('검사항목을 선택하세요!!');
                return false;
            }
            if (selectedKeys.length > 13) {
                alert('검사항목은 12개까지 선택하실 수 있습니다!!');
                return false;
            }

            fn_OnGetRowValues(selectedKeys)
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
                        <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" ReadOnly="true"></dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">
                        <a href="#" onclick="fn_Select()" class="btn btn-default btn-sm" style="background-color:peachpuff;" >선택</a>
                    </label>
                    <dx:ASPxTextBox ID="hidKEYVALUE" ClientInstanceName="hidKEYVALUE" runat="server" Width="100%" ClientVisible="false"></dx:ASPxTextBox>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false"  Width="100%"
                KeyFieldName="F_ITEMCD;F_SERIALNO;F_INSPDETAIL;F_SIRYO;F_WORKCD;F_WORKNM" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback"
                >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px" Name="F_CHK" >
                        <HeaderTemplate >
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" ClientInstanceName="rowChk" runat="server" ToolTip="Select/Unselect all rows on the page" 
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>                    
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
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
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
