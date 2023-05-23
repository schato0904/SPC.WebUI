<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0502.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0502" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var bSearch = false;

        $(document).ready(function () {
            devGridResp.SetHeight(150);
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            devGridResp.SetHeight(150);
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            if (!fn_OnValidate()) return false;
            devGrid.PerformCallback();
            devGridResp.PerformCallback();
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

            bSearch = true;

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            var sMONTH = fn_GetCastValue('hidUCFROMDT') + '-01';
            if (!isValidDate(sMONTH)) {
                alert('날짜형식이 올바르지 않습니다!!');
                fn_Focus('txtFROMDT');
                return false;
            }

            return true;
        }

        // 이상조치 상세내역
        function fn_RespRowDblClick(s, e) {
            fn_OnPopupNgReasonView(devGridResp.GetRowKey(e.visibleIndex));
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
                    <td class="tdLabel">조회월</td>
                    <td class="tdInput">
                        <ucCTF:Date runat="server" id="ucDate" SingleDate="true" MonthOnly="true" />
                    </td>
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
                            IncrementalFilteringMode="None" CssClass="NoXButton" ValueField="F_MACHIDX" TextField="F_MACHNM" DropDownStyle="DropDownList"
                            OnCallback="schF_MACHIDX_Callback" NullText="반을 선택하세요">
                            <Columns>
                                <dx:ListBoxColumn FieldName="F_MACHIDX" Visible="false" />
                                <dx:ListBoxColumn FieldName="F_MACHCD" Caption="설비코드" />
                                <dx:ListBoxColumn FieldName="F_MACHNM" Caption="설비명" />
                            </Columns>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="txtManager" ClientInstanceName="txtManager" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_INSPIDX;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="100px" FixedStyle="Left" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="100px" FixedStyle="Left" />
                    <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="80px" FixedStyle="Left" />
                    <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="150px" FixedStyle="Left">
                        <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPNO" Caption="점검부위" Width="70px" FixedStyle="Left" />
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="점검항목" Width="150px" FixedStyle="Left">
                        <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CYCLENM" Caption="점검주기" Width="70px" FixedStyle="Left" />
                    <dx:GridViewDataColumn FieldName="F_DAY1" Caption="1" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY2" Caption="2" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY3" Caption="3" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY4" Caption="4" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY5" Caption="5" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY6" Caption="6" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY7" Caption="7" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY8" Caption="8" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY9" Caption="9" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY10" Caption="10" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY11" Caption="11" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY12" Caption="12" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY13" Caption="13" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY14" Caption="14" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY15" Caption="15" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY16" Caption="16" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY17" Caption="17" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY18" Caption="18" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY19" Caption="19" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY20" Caption="20" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY21" Caption="21" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY22" Caption="22" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY23" Caption="23" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY24" Caption="24" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY25" Caption="25" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY26" Caption="26" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY27" Caption="27" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY28" Caption="28" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY29" Caption="29" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY30" Caption="30" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_DAY31" Caption="31" Width="70px" />

                    <dx:GridViewDataColumn FieldName="F_INSPIDX" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_CYCLECD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_CHASU" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging">
            <div class="divPadding"></div>
            <dx:ASPxGridView ID="devGridResp" ClientInstanceName="devGridResp" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_REMEDYIDX" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGridResp_CustomCallback" OnHtmlDataCellPrepared="devGridResp_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    RowDblClick="fn_RespRowDblClick" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridResp" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ROWNUM" Caption="No" Width="40px" />
                    <dx:GridViewDataColumn FieldName="F_OCCURDT" Caption="점검일" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="150px">
                        <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NGTYPENM" Caption="이상유형" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_NGREMK" Caption="이상내역" Width="250px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="등록자" Width="100px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RESPTYPENM" Caption="조치유형" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_RESPREMK" Caption="조치내역" Width="250px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RESPUSER" Caption="조치자" Width="100px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RESPDT" Caption="완료일" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="진행상태" Width="70px" />

                    
                    <dx:GridViewDataColumn FieldName="F_REMEDYIDX" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>