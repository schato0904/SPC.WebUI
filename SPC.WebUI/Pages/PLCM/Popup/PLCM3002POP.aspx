<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM3002POP.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.Popup.PLCM3002POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height();
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
             <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-2 control-label">일자</label>
                    <div class="col-sm-4">
                        <div class="control-label" style="float: left; width: 100%;">
                            <dx:ASPxTextBox ID="txtWORKDATE" ClientInstanceName="txtWORKDATE" runat="server" Width="50%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                    <label class="col-sm-1 control-label">설비</label>
                    <div class="col-sm-4">
                        <div class="control-label" style="float: left; width: 50%;">
                            <dx:ASPxTextBox ID="txtMACHNM" ClientInstanceName="txtMACHNM" runat="server" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                    <div class="col-sm-1">&nbsp;</div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="" EnableViewState="False" EnableRowsCache="False" Width="100%"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" >
                
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="sTime" Caption="시작시간" Width="80px" CellStyle-HorizontalAlign="Center" >
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="eTime" Caption="종료시간" Width="80px" CellStyle-HorizontalAlign="Center" >
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="tRecipeID" Caption="레시피ID" Width="50%" CellStyle-HorizontalAlign="Left" >
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="tMasterID" Caption="마스터ID" Width="50%" CellStyle-HorizontalAlign="Left" >
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
