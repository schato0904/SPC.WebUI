<%@ Page Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD_DACO3001.aspx.cs" Inherits="SPC.WebUI.Pages.WERDDACO.WERD_DACO3001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
        });

        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
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
        function fn_OnNewClick() { }
        // 수정
        function fn_OnModifyClick() { }
        // 저장
        function fn_OnSaveClick() { }
        // 취소
        function fn_OnCancelClick() { }
        // 삭제
        function fn_OnDeleteClick() { }
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
        function fn_OnLINEValueChanged(s, e) {
            var val = s.GetValue();
            hidCOMP.SetValue(val);
        }
        function fn_OnLINEEndCallback(s, e) {
            isLINEEndCallback = parent.parent.isTreeLINESetup;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" ID="ucDate" DateTimeOnly="false" />
                    </div>
                    <label class="col-sm-1 control-label">협력사명</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="hidCOMP" ClientInstanceName="hidCOMP" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlCOMP" ClientInstanceName="ddlCOMP" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton"
                            TextField="F_LINENM" ValueField="F_LINECD" OnDataBound="ddlLINE_DataBound" OnCallback="ddlCOMP_Callback">
                            <ClientSideEvents ValueChanged="fn_OnLINEValueChanged" Init="fn_OnControlDisable" EndCallback="fn_OnLINEEndCallback" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item runat="server" ID="ucItem" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;F_WORKTIME;F_ITEMCD;F_NGTYPE;F_WORKER" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback">
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
                    <dx:GridViewDataColumn FieldName="F_ROWNUM" Caption="No" Width="5%" />
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="일자" Width="8%" />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="시간" Width="8%" />
                    <dx:GridViewDataColumn FieldName="F_COMPANYNM" Caption="협력사명" CellStyle-HorizontalAlign="Left" Width="12%" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" CellStyle-HorizontalAlign="Left" Width="13%" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" CellStyle-HorizontalAlign="Left" Width="13%" />
                    <dx:GridViewDataColumn FieldName="F_NGTYPE" Caption="불량항목" CellStyle-HorizontalAlign="Left" Width="11%" />
                    <dx:GridViewDataColumn FieldName="F_NGCOUNT" Caption="불량수량" CellStyle-HorizontalAlign="Right" Width="10%" />
                    <dx:GridViewDataColumn FieldName="F_NGTIME" Caption="수리시간(초)" CellStyle-HorizontalAlign="Right" Width="10%" />
                    <dx:GridViewDataColumn FieldName="F_WORKER" Caption="작업자" CellStyle-HorizontalAlign="Left" Width="10%" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
    </div>
</asp:Content>
