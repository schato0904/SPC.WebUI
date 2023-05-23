<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS1002.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS1002" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = 'F_EQUIPDIVCD;F_PRODNO;F_STATUSCD;F_MAKERCD;F_EQUIPNM;F_TEAMCD;'
        _fieldList = _fieldList + 'F_USER;F_FIXTYPECD;F_FIXGRPCD;F_FROM_EQUIPNO;F_TO_EQUIPNO;F_GRADECD;'
        _fieldList = _fieldList + 'F_FIXDIVCD;F_MODEL;F_PICNO;'

        $(document).ready(function () {
            fn_setdate();
        });

        function fn_setdate() {
            srcF_LASTFIXDT_FROM.SetValue(new Date(new Date().getFullYear() + '-01-01'));
            srcF_LASTFIXDT_TO.SetValue(new Date(new Date().getFullYear() + '-12-31'));
            srcF_FIXPLANDT_FROM.SetValue(new Date(new Date().getFullYear() + '-01-01'));
            srcF_FIXPLANDT_TO.SetValue(new Date(new Date().getFullYear() + '-12-31'));
        }

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
            fn_setdate();
            fn_ClearFields(_fieldList, "src");
            devGrid.PerformCallback("clear");
            srcF_FIXPLANDT_FROM.SetValue(new Date().First());
            srcF_FIXPLANDT_TO.SetValue(new Date());
        }

        // 삭제
        function fn_OnDeleteClick() {}

        // 인쇄
        function fn_OnPrintClick() {
            btnExport.DoClick();
        }

        // 그리드 더블클릭
        function fn_devGridOnRowDblClick(s, e) {
            //var devGrid = ASPxClientGridView.Cast('devGrid');
            //fn_InputArea_Init(false);
            //// 조회키 셋팅 후 함수 콜
            ////var hidSearchPKEY = ASPxClientTextBox.Cast('hidSearchPKEY');
            ////hidSearchPKEY.SetText(devGrid.GetRowKey(e.visibleIndex).split(/|/)[0]);
            var pkey = devGrid.GetRowKey(e.visibleIndex).split(/\|/)[0];
            Move_MEAS1001(pkey);
            //// Set hidSearchPKEY by e.visibleIndex
            //fn_SetPageData(pkey);
        }

        // 공정 부적합 집계 등록 화면으로 이동
        function Move_MEAS1001(keyid) {
            // 파라미터 포함하여 공정 부적합 집계 등록 화면으로 이동
            var j = {
                'F_MS01MID': keyid
            }
            var oSetParam = encodeURIComponent(JSON.stringify(j));
            parent.parent.doCreateTab('MM11|MM1101|MEAS|MEAS1001|측정기정보관리|RCWSDE|1|Y', oSetParam);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <%-- 하단 조회조건 --%>
        <table class="InputTable">
            <colgroup>
                <col style="width:9%" />
                <col style="width:16%" />
                <col style="width:9%" />
                <col style="width:16%" />
                <col style="width:9%" />
                <col style="width:16%" />
                <col style="width:9%" />
                <col style="width:16%" />
            </colgroup>
            <tr>
                <td class="tdTitle">
                    <label>최종교정일자</label>
                </td>
                <td class="tdContent">
                    <div style="width:50%;float:left;">
                        <dx:ASPxDateEdit runat="server" ID="srcF_LASTFIXDT_FROM" ClientInstanceName="srcF_LASTFIXDT_FROM" UseMaskBehavior="true" 
                            EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                            Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                        </dx:ASPxDateEdit>
                    </div>
                    <div style="width:50%;float:left;">
                        <dx:ASPxDateEdit runat="server" ID="srcF_LASTFIXDT_TO" ClientInstanceName="srcF_LASTFIXDT_TO" UseMaskBehavior="true" 
                            EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                            Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                        </dx:ASPxDateEdit>
                    </div>
                    <div style="clear:both;"></div>
                </td>
                <td class="tdTitle">
                    <label>측정기분류</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_EQUIPDIVCD" ClientInstanceName="srcF_EQUIPDIVCD" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>제조번호</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxTextBox ID="srcF_PRODNO" ClientInstanceName="srcF_PRODNO" runat="server" Width="100%"></dx:ASPxTextBox>
                </td>
                <td class="tdTitle">
                    <label>제조사</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxTextBox ID="srcF_MAKERCD" ClientInstanceName="srcF_MAKERCD" runat="server" Width="100%"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <label>사용구분</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_STATUSCD" ClientInstanceName="srcF_STATUSCD" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>측정기명</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxTextBox ID="srcF_EQUIPNM" ClientInstanceName="srcF_EQUIPNM" runat="server" Width="100%"></dx:ASPxTextBox>
                </td>
                <td class="tdTitle">
                    <label>사용팀</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_TEAMCD" ClientInstanceName="srcF_TEAMCD" Width="100%" >
                        <ClientSideEvents Init="fn_OnControlDisable" />
                    </dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>사용자</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxTextBox ID="srcF_USER" ClientInstanceName="srcF_USER" runat="server" Width="100%"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <label>교정구분</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_FIXTYPECD" ClientInstanceName="srcF_FIXTYPECD" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>교정기관</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_FIXGRPCD" ClientInstanceName="srcF_FIXGRPCD" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>교정예정일</label>
                </td>
                <td class="tdContent">
                    <div style="width:50%;float:left;">
                        <dx:ASPxDateEdit runat="server" ID="srcF_FIXPLANDT_FROM" ClientInstanceName="srcF_FIXPLANDT_FROM" UseMaskBehavior="true" 
                            EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                            Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                        </dx:ASPxDateEdit>
                    </div>
                    <div style="width:50%;float:left;">
                        <dx:ASPxDateEdit runat="server" ID="srcF_FIXPLANDT_TO" ClientInstanceName="srcF_FIXPLANDT_TO" UseMaskBehavior="true" 
                            EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                            Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                        </dx:ASPxDateEdit>
                    </div>
                    <div style="clear:both;"></div>
                </td>
                <td class="tdTitle">
                    <label>교정분야</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_FIXDIVCD" ClientInstanceName="srcF_FIXDIVCD" Width="100%"></dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <label>관리번호</label>
                </td>
                <td class="tdContent">
                    <table style="width:100%">
                        <colgroup>
                            <col style="width:45%" />
                            <col style="width:5%" />
                            <col style="width:45%" />
                        </colgroup>
                        <tr>
                            <td><dx:ASPxTextBox ID="srcF_FROM_EQUIPNO" ClientInstanceName="srcF_FROM_EQUIPNO" runat="server" Width="100%"></dx:ASPxTextBox></td>
                            <td style="text-align:center;">~</td>
                            <td><dx:ASPxTextBox ID="srcF_TO_EQUIPNO" ClientInstanceName="srcF_TO_EQUIPNO" runat="server" Width="100%"></dx:ASPxTextBox></td>
                        </tr>
                    </table>
                </td>
                <td class="tdTitle">
                    <label>측정단위</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxComboBox runat="server" ID="srcF_GRADECD" ClientInstanceName="srcF_GRADECD" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="tdTitle">
                    <label>모델</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxTextBox ID="srcF_MODEL" ClientInstanceName="srcF_MODEL" runat="server" Width="100%"></dx:ASPxTextBox>
                </td>
                <td class="tdTitle">
                    <label>도번</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxTextBox ID="srcF_PICNO" ClientInstanceName="srcF_PICNO" runat="server" Width="100%"></dx:ASPxTextBox>
                </td>
            </tr>
         </table>
        <div class="form-group"></div>
        <%-- 하단 그리드 --%>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback"
                SettingsDataSecurity-AllowEdit="false">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowFocusedRow="true" />
                <SettingsPager Mode="ShowAllRecords"/>
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridOnRowDblClick" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_ROWNUM" Caption="NO" Width="40px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px" CellStyle-HorizontalAlign="Left" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="200px" CellStyle-HorizontalAlign="Left" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PRODNO" Caption="제조번호" Width="100px"  CellStyle-HorizontalAlign="Left" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STAND" Caption="규격" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MODEL" Caption="모델" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LASTFIXDT" Caption="최종교정일" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_TERMMONTH" Caption="교정주기" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXPLANDT" Caption="교정예정일" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STATUSNM" Caption="사용구분" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ABNORMALNM" Caption="이상처리" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXNO" Caption="교정번호" Width="110px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPDIVNM" Caption="측정기분류" Width="100px">
                    </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn FieldName="F_MAKERNM" Caption="제조회사" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_INDT" Caption="도입일자" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FACTNM" Caption="공장" Width="100px">
                    </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn FieldName="F_TEAMNM" Caption="사용팀" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_USER" Caption="사용자" Width="70px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPTYPENM" Caption="측정기구분" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_GRADENM" Caption="측정단위" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_JUDGENM" Caption="판정" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXTYPENM" Caption="교정구분" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXGRPNM" Caption="교정기관" Width="80px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXDIVNM" Caption="교정분야" Width="80px">
                    </dx:GridViewDataTextColumn>
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