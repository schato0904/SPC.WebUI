<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MNTR0901.aspx.cs" Inherits="SPC.WebUI.Pages.MNTR.MNTR0901" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .dxgvInlineEditRow,
        .dxgvDataRow {
              height:40px;
         }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

            fn_RendorTotalCount();
            fn_OnSearchClick();

            if ('<%=sPopup%>' == 'true') {
                $(".bg-white").addClass("bg-black");
                $(".bg-white").removeClass("bg-white").css("color", "white");
            }
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
            <%=ucStopwatch.ClientInstanceName%>Init();
            devGrid.PerformCallback(ddlSTATUSDIFF.GetValue());
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
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">표시간격</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlSTATUSDIFF" ClientInstanceName="ddlSTATUSDIFF" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <Items>
                                <dx:ListEditItem Text="30분" Value="M|30" Selected="true" />
                                <dx:ListEditItem Text="1시간" Value="H|1" />
                                <dx:ListEditItem Text="2시간" Value="H|2" />
                                <dx:ListEditItem Text="4시간" Value="H|4" />
                                <dx:ListEditItem Text="1일" Value="D|1" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">갱신간격</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Stopwatch ID="ucStopwatch" runat="server" ClientInstanceName="REPLACEDIFF" ItemList="M|10;M|20;M|30" ShowRemaintime="true" CallbackEvent="fn_OnSearchClick" />
                    </div>
                    <div class="col-sm-5 control-label">
                        <img src="../../Resources/images/bullet-green.png" alt="OK" />
                        정상
                        <img src="../../Resources/images/bullet-blue.png" alt="OK" />
                        OC
                        <img src="../../Resources/images/bullet-red.png" alt="OK" />
                        NG
                        <img src="../../Resources/images/bullet-grey.png" alt="OK" />
                        미가동
                        <img src="../../Resources/images/bullet-black.png" alt="OK" />
                        미사용&nbsp;&nbsp;&nbsp;
                        <ucCTF:Clock ID="ucClock" runat="server" />
                    </div>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxCheckBox ID="chkNotUsedShow" runat="server" Text="미사용" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="협력사" Width="180px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewBandColumn Name="F_INSP03" Caption="자주검사">
                        <Columns>
                            <dx:GridViewBandColumn Name="F_INSP0301" Caption="공정검사">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="F_STATUS1" Caption="상태" Width="40px" />
                                    <dx:GridViewDataColumn FieldName="F_ALLCNT1" Caption="검사수" Width="70px">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_OCCNT1" Caption="OC(수)" Width="70px">
                                        <CellStyle HorizontalAlign="Right" ForeColor="Blue"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_NGCNT1" Caption="NG(수)" Width="70px">
                                        <CellStyle HorizontalAlign="Right" ForeColor="Red"></CellStyle>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewBandColumn Name="F_INSP0302" Caption="3차원">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="F_STATUS2" Caption="상태" Width="40px" />
                                    <dx:GridViewDataColumn FieldName="F_ALLCNT2" Caption="검사수" Width="70px">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_OCCNT2" Caption="OC(수)" Width="70px">
                                        <CellStyle HorizontalAlign="Right" ForeColor="Blue"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_NGCNT2" Caption="NG(수)" Width="70px">
                                        <CellStyle HorizontalAlign="Right" ForeColor="Red"></CellStyle>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewBandColumn Name="F_INSP0303" Caption="치형">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="F_STATUS3" Caption="상태" Width="40px" />
                                    <dx:GridViewDataColumn FieldName="F_ALLCNT3" Caption="검사수" Width="70px">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_OCCNT3" Caption="OC(수)" Width="70px">
                                        <CellStyle HorizontalAlign="Right" ForeColor="Blue"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_NGCNT3" Caption="NG(수)" Width="70px">
                                        <CellStyle HorizontalAlign="Right" ForeColor="Red"></CellStyle>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Name="F_INSP08" Caption="전수">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_STATUS4" Caption="상태" Width="40px" />
                            <dx:GridViewDataColumn FieldName="F_ALLCNT4" Caption="검사수" Width="70px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_OCCNT4" Caption="OC(수)" Width="70px">
                                <CellStyle HorizontalAlign="Right" ForeColor="Blue"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_NGCNT4" Caption="NG(수)" Width="70px">
                                <CellStyle HorizontalAlign="Right" ForeColor="Red"></CellStyle>
                            </dx:GridViewDataColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
