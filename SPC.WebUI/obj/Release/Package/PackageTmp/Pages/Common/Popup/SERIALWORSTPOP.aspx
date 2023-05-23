<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="SERIALWORSTPOP.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Popup.SERIALWORSTPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            var keys = '<%=sParams%>'.split('|');

            fn_SetTextValue('hidUCFROMDT', keys[0]);
            fn_SetDate('txtFROMDT', convertDateString(keys[0]));

            fn_SetTextValue('hidUCTODT', keys[1]);
            fn_SetDate('txtTODT', convertDateString(keys[1]));

            fn_SetTextValue('hidBANCD', keys[2]);

            fn_SetTextValue('hidUCWORKPOPCD', keys[3]);
            fn_SetTextValue('txtUCWORKPOPCD', keys[3]);
            fn_SetTextValue('txtUCWORKNM', keys[4]);

            fn_SetTextValue('hidMODELCD', keys[5]);
            rdoGBN.fn_SetTextValue = keys[6];
            rdoUNIT.fn_SetTextValue = keys[7];

            fn_SetTextValue('hidUCITEMCD', keys[8]);
            fn_SetTextValue('txtUCITEMCD', keys[8]);
            fn_SetTextValue('txtUCITEMNM', keys[9]);

            devGrid.PerformCallback();
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="display:<%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">분석기간</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate"   />
                    </div>
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">모델</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:ModelDDL ID="ucModelDDL" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">구분</label>
                        <div class="col-sm-1">
                            <dx:ASPxRadioButtonList ID="rdoGBN" ClientInstanceName="rdoGBN" runat="server"
                                RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                <Items>
                                    <dx:ListEditItem Value="1" Text="Worst" Selected="true" />
                                    <dx:ListEditItem Value="2" Text="Best"/>
                                </Items>
                            </dx:ASPxRadioButtonList>
                        </div>
                    <label class="col-sm-1 control-label">단위</label>
                        <div class="col-sm-2">
                            <dx:ASPxRadioButtonList ID="rdoUNIT" ClientInstanceName="rdoUNIT" runat="server"
                                RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                <Items>
                                    <dx:ListEditItem Value="1" Text="Percent(%)" Selected="true" />
                                    <dx:ListEditItem Value="2" Text="ppm"/>
                                </Items>
                            </dx:ASPxRadioButtonList>
                        </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;F_WORKTIME;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEM" Caption="품목코드" Width="110px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="140px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="160px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="160px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TOTQTY" Caption="전체시료" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_REJQTY" Caption="부적합시료" Width="80px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_GOODQTY" Caption="적합시료" Width="80px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_OKRATE" Caption="접합률" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_REJRATE" Caption="부적합률" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_REJPPM" Caption="부적합PPM" Width="90px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" BlockSize="10" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
