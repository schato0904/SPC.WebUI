<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS2001.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS2001" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = 'F_EQUIPDIVCD;F_FIXTYPECD;F_FIXDIVCD;F_TEAMCD;F_TERMMMONTH;'

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".InputTable").height() > 0 ? $(".InputTable").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
            $(".content").height(height);
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
            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    fn_pagerPerformCallback(s.cpResultMsg);
                }
                else {
                    alert(s.cpResultMsg);
                    fn_doSetGridEventAction('false');
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
        }

        // 취소
        function fn_OnCancelClick() {
            srcF_YEAR.SetText(new Date().getFullYear());
            fn_ClearFields(_fieldList, "src");
            devGrid.PerformCallback("clear");
        }

        // 삭제
        function fn_OnDeleteClick() { }

        // 인쇄
        function fn_OnPrintClick() {
            btnExport.DoClick();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <%-- 하단 조회조건 --%>
        <table class="InputTable">
            <colgroup>
                <col style="width:6%" />
                <col style="width:10%" />
                <col style="width:6%" />
                <col style="width:10%" />
                <col style="width:6%" />
                <col style="width:10%" />
                <col style="width:6%" />
                <col style="width:10%" />
                <col style="width:6%" />
                <col style="width:10%" />
                <col style="width:6%" />
                <col style="width:10%" />
            </colgroup>
            <tr>
                <td class="tdTitle">
                    <label>해당년도</label>
                </td>
                <td class="tdContent">
                   <dx:ASPxComboBox runat="server" ID="srcF_YEAR" ClientInstanceName="srcF_YEAR" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>측정기분류</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_EQUIPDIVCD" ClientInstanceName="srcF_EQUIPDIVCD" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>교정구분</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_FIXTYPECD" ClientInstanceName="srcF_FIXTYPECD" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>교정분야</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_FIXDIVCD" ClientInstanceName="srcF_FIXDIVCD" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>사용팀</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_TEAMCD" ClientInstanceName="srcF_TEAMCD" Width="100%">
                        <ClientSideEvents Init="fn_OnControlDisable" />
                    </dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>교정주기</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_TERMMMONTH" ClientInstanceName="srcF_TERMMMONTH" Width="100%">
                        <ClientSideEvents Init="fn_OnControlDisable"/>
                    </dx:ASPxComboBox>
                </td>
            </tr>
         </table>
        <div class="form-group"></div>
        <%-- 하단 그리드 --%>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                SettingsDataSecurity-AllowEdit="false">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_ROWNUM" Caption="NO" Width="40px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px" FixedStyle="Left">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="200px" FixedStyle="Left">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PRODNO" Caption="제조번호" Width="100px" FixedStyle="Left" CellStyle-HorizontalAlign="Left">
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STAND" Caption="규격" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_TEAMNM" Caption="사용팀" Width="80px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXGRPNM" Caption="교정기관" Width="120px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_TERMMONTH" Caption="교정주기" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LASTFIXDT" Caption="최종교정일" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewBandColumn Caption="월별 교정 계획">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_JAN" Caption="01월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_FEB" Caption="02월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_MAR" Caption="03월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_APR" Caption="04월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_MAY" Caption="05월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_JUN" Caption="06월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_JUL" Caption="07월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_AUG" Caption="08월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_SEP" Caption="09월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_OCT" Caption="10월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_NOV" Caption="11월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>                                       
                            <dx:GridViewDataTextColumn FieldName="F_DEC" Caption="12월" Width="60px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>                 
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
    </div>
</asp:Content>
