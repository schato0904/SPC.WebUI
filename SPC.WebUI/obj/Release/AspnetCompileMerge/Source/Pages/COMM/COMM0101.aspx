<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="COMM0101.aspx.cs" Inherits="SPC.WebUI.Pages.COMM.COMM0101" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            <%if (!Convert.ToBoolean(gsUSEBOARD)) {%>
            $(".board-group").hide();
            <%}%>
            fn_OnSearchClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            <%if (!Convert.ToBoolean(gsUSEBOARD)) {%>
            height = parseInt(height / 2, 10) - 50;
            <%} else {%>
            height = parseInt(height / 3, 10) - 50;
            <%}%>

            devGrid1.SetHeight(height);

            <%if (!gsVENDOR) {%>
            devGrid3.SetHeight(height);
            <%if (true == Convert.ToBoolean(gsUSEBOARD)) {%>
            devGrid6.SetHeight(height);
            OptionDisplay1();
            <%}%>
            <%} else {%>
            devGrid2.SetHeight(height);
            <%if (true == Convert.ToBoolean(gsUSEBOARD)) {%>
            devGrid4.SetHeight(height);
            devGrid5.SetHeight(height);
            OptionDisplay2();
            <%}%>
            <%}%>
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid1.PerformCallback();

            <%if (!gsVENDOR) {%>
            devGrid3.PerformCallback();
            <%if (true == Convert.ToBoolean(gsUSEBOARD)) {%>
            devGrid6.PerformCallback();
            <%}%>
            <%} else {%>
            devGrid2.PerformCallback();
            <%if (true == Convert.ToBoolean(gsUSEBOARD)) {%>
            devGrid4.PerformCallback();
            devGrid5.PerformCallback();
            <%}%>;
            <%}%>
        }

        function OptionDisplay1() {
            document.getElementById("mainoption1").style.display = "none"; //공지사항, 사내공지사항 라벨 안보이기
        }

        function OptionDisplay2() {
            document.getElementById("mainoption2").style.display = "none"; //공지사항 라벨 안보이기
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
            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    //alert(s.cpResultMsg);

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

        // Grid RowDblClick
        function fn_OnRowDblClick1(s, e) {
            parent.parent.doCreateTab('MM05|MM0501|IPCM|IPCM0102|품질이상대책|R|1');
        }

        // Grid RowDblClick
        function fn_OnRowDblClick2(s, e) {
            parent.parent.doCreateTab('MM05|MM0501|IPCM|IPCM0103|품질이상확인|R|1');
        }

        function fn_RowDblClick(s, e) {
            parent.fn_OnDeleteTab('BORD0101', parent.fn_OnGetTabObject('BORD0101'));
            parent.parent.doCreateTab('MM99|MM9902|BORD|BORD0101|게시판|RC|1', null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="padding: 0px 10px 0px 10px;">
                    <label class="col-sm-12 bg-danger font-bold" style="height: 30px; padding-top: 5px;">이상통보</label>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_WORKDATE;F_WORKTIME;F_PCNM" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid1_CustomCallback">
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
                                <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="작업일자" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="시간" Width="90px" />
                                <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="80px">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="80px">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정" Width="160px">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_USERNM" Caption="작업자" Width="100px">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CONTENTS" Caption="이상통보내용">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
                <div class="form-group" style="padding: 0px 10px 0px 10px;">
                    <label class="col-sm-12 bg-success font-bold" style="height: 30px; padding-top: 5px;"><%if (!gsVENDOR)
                                                                                                            {%>품질이상 미확인 목록<%}
                                                                                                            else
                                                                                                            {%>품질이상 미대책 목록<%}%></label>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%" Visible="false"
                            KeyFieldName="F_COMPCD;F_FACTCD;F_INDXNO;F_RQUSID" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid2_CustomCallback" OnHtmlDataCellPrepared="devGrid2_HtmlDataCellPrepared">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick1" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_TRNM" Caption="부적합유형" Width="150px">
                                    <CellStyle HorizontalAlign="left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RQDATE" Caption="이상제기일" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RQUSNM" Caption="제기자" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RQRCDT" Caption="회신요청일" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CTRLNO" Caption="발행번호" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="160px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="200px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_UNSTTP" Caption="부적합유형코드" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_DEPARTCD" Caption="대책부서코드" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_MEASGD" Caption="대책등급" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_RQUSID" Caption="제기자" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RQCPCD" Caption="제기업체코드" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RQFTCD" Caption="제기공장코드" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_BVENDOR" Caption="협력업체여부" Visible="false"></dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridView ID="devGrid3" ClientInstanceName="devGrid3" runat="server" AutoGenerateColumns="false" Width="100%" Visible="false"
                            KeyFieldName="F_COMPCD;F_FACTCD;F_INDXNO;F_ITEMCD;F_RQUSID" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid3_CustomCallback" OnHtmlDataCellPrepared="devGrid3_HtmlDataCellPrepared">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick2" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_TRNM" Caption="부적합유형" Width="120px">
                                    <CellStyle HorizontalAlign="left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RQDATE" Caption="이상제기일" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RQUSNM" Caption="제기자" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RQRCDT" Caption="회신요청일" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RSDATE" Caption="회신일" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RSUSNM" Caption="회신자" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CFDATE" Caption="확인일" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CFUSNM" Caption="확인자" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CTRLNO" Caption="발행번호" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_UNSTTP" Caption="부적합유형코드" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_DEPARTCD" Caption="대책부서코드" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_MEASGD" Caption="대책등급" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_RQUSID" Caption="제기자" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_PROGRESS" Caption="진행단계" Visible="false" />
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>

                <div id="mainoption1" class="form-group board-group" style="padding: 0px 10px 0px 10px;">
                    <%--<label class="col-sm-6 bg-info font-bold" style="height: 30px; padding-top: 5px;">공지사항 및 요청사항</label>--%>
                    <label class="col-sm-12 bg-warning font-bold" style="height: 30px; padding-top: 5px;">사내공지사항</label>
                </div>

                <div id="mainoption2" class="form-group board-group" style="padding: 0px 10px 0px 10px;">
                    <label class="col-sm-12 bg-info font-bold" style="height: 30px; padding-top: 5px;">공지사항</label>
                </div>

                <div class="form-group board-group">
                    <div class="col-sm-6" style="display:none">
                        <dx:ASPxGridView ID="devGrid4" ClientInstanceName="devGrid4" runat="server" AutoGenerateColumns="false" Width="100%" Visible="false"
                            KeyFieldName="F_NUMBER;F_COMPNMKR;F_GBNNM;F_TITLE;F_COMPCD;F_GBN" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid4_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_RowDblClick" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="번호" Width="50px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_GBNNM" Caption="구분" Width="70px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_TITLE" Caption="제목" Width="100%">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_COMPNMKR" Caption="업체" Width="100px" ></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_USERNM" Caption="게시자" Width="80px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSDT" Caption="게시일" Width="100px"></dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                    <div class="col-sm-12">
                        <dx:ASPxGridView ID="devGrid5" ClientInstanceName="devGrid5" runat="server" AutoGenerateColumns="false" Width="100%" Visible="false"
                            KeyFieldName="F_NUMBER;F_COMPNMKR;F_GBNNM;F_TITLE;F_COMPCD;F_GBN" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid5_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_RowDblClick" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="번호" Width="50px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_GBNNM" Caption="구분" Width="70px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_TITLE" Caption="제목" Width="100%">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_COMPNMKR" Caption="업체" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_USERNM" Caption="게시자" Width="80px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSDT" Caption="게시일" Width="100px"></dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>

                    <div class="col-sm-12">
                        <dx:ASPxGridView ID="devGrid6" ClientInstanceName="devGrid6" runat="server" AutoGenerateColumns="false" Width="100%" Visible="false"
                            KeyFieldName="F_NUMBER;F_COMPNMKR;F_GBNNM;F_TITLE;F_COMPCD;F_GBN" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid6_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_RowDblClick" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="번호" Width="70px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_GBNNM" Caption="구분" Width="100px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_TITLE" Caption="제목" Width="100%" >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_COMPNMKR" Caption="업체" Width="120px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_USERNM" Caption="게시자" Width="120px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSDT" Caption="게시일" Width="120px"></dx:GridViewDataColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
