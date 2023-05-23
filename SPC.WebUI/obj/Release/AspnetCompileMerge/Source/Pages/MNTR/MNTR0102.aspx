<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MNTR0102.aspx.cs" Inherits="SPC.WebUI.Pages.MNTR.MNTR0102" %>
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
            devGridPrevMonth.SetHeight((height - 20) / 3);
            devGridPrevWeek.SetHeight((height - 20) / 3);
            devGridCurrWeek.SetHeight((height - 20) / 3);
        }

        // 조회
        function fn_OnSearchClick() {
            devCallbackPanel.PerformCallback();
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

        //  CallbackPanel Callback
        function fn_OnCallbackPanelEndCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackPanelCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" ID="ucDate" SingleDate="true" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxCallbackPanel ID="devCallbackPanel" ClientInstanceName="devCallbackPanel" runat="server"
                OnCallback="devCallbackPanel_Callback">
                <ClientSideEvents EndCallback="fn_OnCallbackPanelEndCallback" CallbackError="fn_OnCallbackPanelCallbackError" />
                <PanelCollection>
                    <dx:PanelContent>
                        <table style="width:100%">
                            <tr>
                                <td style="vertical-align: top;">
                                    <dx:ASPxGridView ID="devGridPrevMonth" ClientInstanceName="devGridPrevMonth" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD" EnableViewState="false" EnableRowsCache="false">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                        <Templates>
                                            <StatusBar>
                                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridPrevMonth" />
                                            </StatusBar>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="180px" >
                                                <CellStyle HorizontalAlign="Left" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewBandColumn Name="F_CAPABILITY" Caption="전월 공정별 공정능력">
                                                <Columns>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_A" Caption="A급(우수) 1.67 이상" HeaderStyle-BackColor="#1aae88">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_A" Caption="CP" Width="70px" HeaderStyle-BackColor="#1aae88">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_A" Caption="CPK" Width="70px" HeaderStyle-BackColor="#1aae88">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_B" Caption="B급(양호) 1.33 ~ 1.67" HeaderStyle-BackColor="#1ccacc">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_B" Caption="CP" Width="70px" HeaderStyle-BackColor="#1ccacc">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_B" Caption="CPK" Width="70px" HeaderStyle-BackColor="#1ccacc">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_C" Caption="C급(보통) 1.00 ~ 1.33" HeaderStyle-BackColor="#fcc633">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_C" Caption="CP" Width="70px" HeaderStyle-BackColor="#fcc633">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_C" Caption="CPK" Width="70px" HeaderStyle-BackColor="#fcc633">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_D" Caption="D급(미흡) 1.00 이하" HeaderStyle-BackColor="#e33244">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_D" Caption="CP" Width="70px" HeaderStyle-BackColor="#e33244">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_D" Caption="CPK" Width="70px" HeaderStyle-BackColor="#e33244">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_T" Caption="전체">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_T" Caption="CP" Width="70px">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_T" Caption="CPK" Width="70px">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataColumn FieldName="F_COMPCD" Caption="업체코드" Visible="false"></dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; padding-top: 10px;">
                                    <dx:ASPxGridView ID="devGridPrevWeek" ClientInstanceName="devGridPrevWeek" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD" EnableViewState="false" EnableRowsCache="false">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                        <Templates>
                                            <StatusBar>
                                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridPrevWeek" />
                                            </StatusBar>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="180px" >
                                                <CellStyle HorizontalAlign="Left" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewBandColumn Name="F_CAPABILITY" Caption="전주 공정별 공정능력">
                                                <Columns>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_A" Caption="A급(우수) 1.67 이상" HeaderStyle-BackColor="#1aae88">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_A" Caption="CP" Width="70px" HeaderStyle-BackColor="#1aae88">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_A" Caption="CPK" Width="70px" HeaderStyle-BackColor="#1aae88">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_B" Caption="B급(양호) 1.33 ~ 1.67" HeaderStyle-BackColor="#1ccacc">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_B" Caption="CP" Width="70px" HeaderStyle-BackColor="#1ccacc">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_B" Caption="CPK" Width="70px" HeaderStyle-BackColor="#1ccacc">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_C" Caption="C급(보통) 1.00 ~ 1.33" HeaderStyle-BackColor="#fcc633">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_C" Caption="CP" Width="70px" HeaderStyle-BackColor="#fcc633">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_C" Caption="CPK" Width="70px" HeaderStyle-BackColor="#fcc633">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_D" Caption="D급(미흡) 1.00 이하" HeaderStyle-BackColor="#e33244">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_D" Caption="CP" Width="70px" HeaderStyle-BackColor="#e33244">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_D" Caption="CPK" Width="70px" HeaderStyle-BackColor="#e33244">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_T" Caption="전체">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_T" Caption="CP" Width="70px">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_T" Caption="CPK" Width="70px">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataColumn FieldName="F_COMPCD" Caption="업체코드" Visible="false"></dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; padding-top: 10px;">
                                    <dx:ASPxGridView ID="devGridCurrWeek" ClientInstanceName="devGridCurrWeek" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD" EnableViewState="false" EnableRowsCache="false">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                        <Templates>
                                            <StatusBar>
                                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridCurrWeek" />
                                            </StatusBar>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="180px">
                                                <CellStyle HorizontalAlign="Left" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewBandColumn Name="F_CAPABILITY" Caption="선택주 공정별 공정능력">
                                                <Columns>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_A" Caption="A급(우수) 1.67 이상" HeaderStyle-BackColor="#1aae88">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_A" Caption="CP" Width="70px" HeaderStyle-BackColor="#1aae88">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_A" Caption="CPK" Width="70px" HeaderStyle-BackColor="#1aae88">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_B" Caption="B급(양호) 1.33 ~ 1.67" HeaderStyle-BackColor="#1ccacc">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_B" Caption="CP" Width="70px" HeaderStyle-BackColor="#1ccacc">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_B" Caption="CPK" Width="70px" HeaderStyle-BackColor="#1ccacc">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_C" Caption="C급(보통) 1.00 ~ 1.33" HeaderStyle-BackColor="#fcc633">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_C" Caption="CP" Width="70px" HeaderStyle-BackColor="#fcc633">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_C" Caption="CPK" Width="70px" HeaderStyle-BackColor="#fcc633">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_D" Caption="D급(미흡) 1.00 이하" HeaderStyle-BackColor="#e33244">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_D" Caption="CP" Width="70px" HeaderStyle-BackColor="#e33244">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_D" Caption="CPK" Width="70px" HeaderStyle-BackColor="#e33244">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Name="F_CAPABILITY_T" Caption="전체">
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="F_CP_T" Caption="CP" Width="70px">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="F_CPK_T" Caption="CPK" Width="70px">
                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataColumn FieldName="F_COMPCD" Caption="업체코드" Visible="false"></dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
            
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGridPrevMonth" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxGridViewExporter ID="devGridExporter2" runat="server" GridViewID="devGridPrevWeek" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxGridViewExporter ID="devGridExporter3" runat="server" GridViewID="devGridCurrWeek" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
