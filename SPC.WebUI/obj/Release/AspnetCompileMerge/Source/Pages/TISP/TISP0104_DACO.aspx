<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TISP0104_DACO.aspx.cs" Inherits="SPC.WebUI.Pages.TISP.TISP0104_DACO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartHeight2 = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        var chartResized3 = false;

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height);
            //devGrid.SetWidth(parseInt($(".search").width() - 5, 10));
            //devGrid.SetHeight(190);
            devGrid1.SetHeight(150);
            devGridWork.SetHeight(150);
            chartWidth = parseInt($(".search").width(), 10);
            chartHeight1 = parseInt((height - 320), 10);
            chartHeight2 = height - 335;

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);
            fn_OnChartResized(devChart2, chartResized2, chartWidth, chartHeight1);
        });

        // 차트 이벤트
        function fn_OnChartResized(chartObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnChartDoCallback(chartObj, _width, _height);
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height) {
            var oParams = _width + '|' + _height;
            chartObj.PerformCallback(oParams);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            if (txtITEMCD.GetValue() == "" || txtITEMCD.GetValue() == null) {
                alert("품목을 선택하세요.");
                return false;
            }

            if (devGrid1.GetSelectedKeysOnPage() == '' || devGrid1.GetSelectedKeysOnPage() == null) {
                alert("공정을 선택하세요.")
                return false;
            }

            if (devGridWork.GetSelectedKeysOnPage() == '' || devGridWork.GetSelectedKeysOnPage() == null) {
                alert("검사 항목을 선택하세요.")
                return false;
            }

            fn_OnSearch();
        }



        // 입력
        function fn_OnNewClick() {
            //devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid1.UnselectAllRowsOnPage();
            devGrid1.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                //devGrid.DeleteRowByKey(selectedKeys[i]);
            }
        }

        // 인쇄
        function fn_OnPrintClick() {
            var KEYS = '';
            // 업체
            KEYS += '<%#gsCOMPCD%>|';
            KEYS += encodeURIComponent('<%#gsCOMPNM%>') + '|';

            // 날짜
            KEYS += fn_GetCastText('hidUCFROMDT') + ' ~ ' + fn_GetCastText('hidUCTODT') + '|';
            KEYS += fn_GetCastText('hidUCTODT') + '|';
            // 품목
            KEYS += fn_GetCastText('txtITEMCD') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtITEMNM')) + '|';
            KEYS += encodeURIComponent('model') + '|';
            // 공정
            KEYS += encodeURIComponent((devGrid1.GetSelectedKeysOnPage()[0].split('|'))[0]) + '|';
            KEYS += encodeURIComponent((devGrid1.GetSelectedKeysOnPage()[0].split('|'))[1]) + '|';
            // 항목
            KEYS += encodeURIComponent((devGridWork.GetSelectedKeysOnPage()[0].split('|'))[0]) + '|';
            KEYS += encodeURIComponent('R') + '|';
            // 관리식 or 계산식
            KEYS += encodeURIComponent(fn_GetCastSelectedItemText('chk_calc')) + '|';
            // 시료수
            KEYS += encodeURIComponent((devGridWork.GetSelectedKeysOnPage()[0].split('|'))[1]) + '|';
            // 검사규격
            KEYS += fn_GetCastText('txtSTANDARD') + '|';
            KEYS += fn_GetCastText('txtMAX') + '|';
            KEYS += fn_GetCastText('txtMIN') + '|';
            // 관리규격
            KEYS += fn_GetCastText('txtUCLR') + '|';
            KEYS += fn_GetCastText('txtUCLX') + '|';
            KEYS += fn_GetCastText('txtLCLX') + '|';
            // 항목순번
            KEYS += encodeURIComponent('') + '|';
            //// 규격이탈제외
            KEYS += !chk_reject.GetChecked() ? "0" : "1";

            fn_OnPopupTISP0104Report_DACO(KEYS);
        }

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
            } else {
                fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
                fn_OnChartDoCallback(devChart2, chartWidth, chartHeight2);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {

        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {

        }

        function fn_CallBack() {
            devGridWork.PerformCallback();

        }

        function fn_OnSettingItem(CODE, TYPE) {
            ASPxClientControl.Cast('txtITEMCD').SetText(CODE);
            var parentParams = ''
            if (TYPE == 'S') {
            }
        }

        function fn_OnSettingItem(CODE, TEXT, MODEL) {
            txtITEMCD.SetText(CODE);
            txtITEMNM.SetText(TEXT);
            devGrid1.PerformCallback(CODE);
        }

        function fn_OnPopupUCItemSearch(CODE) {
            var Option = ''
            var BANCD = IsNull(ASPxClientControl.Cast('hidBANCD'), '') == '' ? '' : ASPxClientControl.Cast('hidBANCD').GetText();
            var pPage = '<%=Page.ResolveUrl("~/Pages/Common/Popup/ITEMPOP.aspx")%>' +
            '?TITLE=품목조회' +
            '&CRUD=R' +
            '&TYPE=FORM' +
            '&OPTION=' + Option +
            '&BANCD=' + BANCD + '' +
            '&ITEMCD=' + txtITEMCD.GetText() +
            '&MACHGUBUN=';
            fn_OnPopupOpen(pPage, 800, 500);
        }

        function fn_OnRowClick(s, e) {
            selectedKeys = devGrid1.GetSelectedKeysOnPage();
            if (selectedKeys[0] != null) {
                selectedKeys1 = (selectedKeys[0].split('|'))[0];
                devGridWork.PerformCallback(selectedKeys1);
                txtMEAINSPCD.SetText('');
            }
        }

        function fn_OnRowClick2() {
            selectedKeys = devGridWork.GetSelectedKeysOnPage();
            if (selectedKeys[0] != null) {
                txtSTANDARD.SetText((selectedKeys[0].split('|'))[2]);
                txtMAX.SetText((selectedKeys[0].split('|'))[3]);
                txtMIN.SetText((selectedKeys[0].split('|'))[4]);
                txtUCLR.SetText((selectedKeys[0].split('|'))[5]);
                txtUCLX.SetText((selectedKeys[0].split('|'))[6]);
                txtLCLX.SetText((selectedKeys[0].split('|'))[7]);
                txtMEAINSPCD.SetText(selectedKeys);
            }
        }

        function fn_OnValueChanged() {

            alert(hidSTANDARD.GetText());
        }

        function fn_OnSearch(s, e) {
            selectedKeys = devGrid1.GetSelectedKeysOnPage();
            hidGrid.PerformCallback(selectedKeys);
        }

        function fn_OnLostFocus(s, e) {
            if (!s.GetText() || s.GetText() == '') {
                txtITEMNM.SetText('');
                txtITEMNM.SetValue('');
            }

        }

        function fn_OnPopupTISP0104Report_DACO(key) {
            pPage = rootURL + 'Pages/TISP/Popup/TISP0104REPORT_DACO.aspx' +
                '?TITLE=' + encodeURIComponent(encodeURIComponent('X-Rs관리도')) +
                '&KEYFIELDS=' + encodeURIComponent(key);
            fn_OnPopupOpen(pPage, '830', '0');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="form-horizontal bg-white fa-border" style="width: 100%; border: 1px solid #9F9F9F; height: 135px;">
                <tr>
                    <td style="width: 25%;">
                        <div class="form-group" style="padding-top: 20px;">
                            <label class="col-sm-2 control-label">검색일자</label>
                            <div class="col-sm-9">
                                <ucCTF:Date runat="server" ID="ucDate" />
                            </div>
                        </div>

                        <div class="form-group" style="padding-top: 20px;">
                            <label class="col-sm-2 control-label">품목</label>
                            <div class="col-sm-9" style="float: left;">
                                <div class="control-label" style="float: left; padding-right: 5px;">
                                    <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server"
                                        OnInit="txtITEMCD_Init">
                                        <ClientSideEvents LostFocus="fn_OnLostFocus" />
                                    </dx:ASPxTextBox>
                                </div>
                                <div class="control-label" style="float: left; padding-right: 5px;">
                                    <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <div class="control-label" style="float: left;">
                                    <button class="btn btn-default btn-xs" data-toggle="button" title="품목조회" onclick="fn_OnPopupUCItemSearch(); return false;">
                                        <i class="i i-popup text"></i>
                                        <i class="i i-popup text-active text-danger"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="padding-top: 10px; padding-left: 100px; float: left;">
                            <dx:ASPxRadioButtonList ID="chk_calc" ClientInstanceName="chk_calc" runat="server"
                                RepeatDirection="Horizontal" Border-BorderStyle="None">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="관리용" />
                                    <dx:ListEditItem Value="1" Text="계산식" />
                                </Items>
                            </dx:ASPxRadioButtonList>
                        </div>
                    </td>

                    <td style="width: 10%; flex-align: center;">                        
                        <div style="padding-top: 10px;">
                            <section class="panel panel-default" style="height: 100%;">
                                <dx:ASPxTextBox ID="txtWORKNO" ClientInstanceName="txtWORKNO" runat="server" ClientVisible="false" />
                                <dx:ASPxTextBox ID="txtWORKCNT" ClientInstanceName="txtWORKCNT" runat="server" ClientVisible="false" />

                                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />

                                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                                    KeyFieldName="F_WORKCD;F_WORKNM;" EnableViewState="false" EnableRowsCache="false"
                                    OnCustomCallback="devGrid1_CustomCallback">
                                    <Styles>
                                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                        <EditForm CssClass="bg-default"></EditForm>
                                    </Styles>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" ShowColumnHeaders="false" />
                                    <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" SelectionChanged="fn_OnRowClick" />
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" Width="15%">
                                            <HeaderCaptionTemplate></HeaderCaptionTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="85%">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_WORKCD" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </section>
                        </div>
                    </td>

                    <td style="width: 1%;"></td>

                    <td style="width: 10%;">
                        <div style="padding-top: 10px;">
                            <section class="panel panel-default" style="height: 100%; vertical-align: middle;">
                                <dx:ASPxTextBox ID="txtMEAINSPCD" ClientInstanceName="txtMEAINSPCD" runat="server" ClientVisible="false" />
                                <dx:ASPxTextBox ID="txtCNT" ClientInstanceName="txtCNT" runat="server" ClientVisible="false" />
                                <dx:ASPxGridView ID="devGridWork" ClientInstanceName="devGridWork" runat="server" AutoGenerateColumns="false" Width="100%"
                                    KeyFieldName="F_MEAINSPCD;F_SIRYO;F_STANDARD;F_MAX;F_MIN;F_UCLR;F_UCLX;F_LCLX;" EnableViewState="false" EnableRowsCache="false"
                                    OnCustomCallback="devGridWork_CustomCallback">
                                    <Styles>
                                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                        <EditForm CssClass="bg-default"></EditForm>
                                    </Styles>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" ShowColumnHeaders="false" />
                                    <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" SelectionChanged="fn_OnRowClick2" />
                                    <Columns>

                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" Width="15%">
                                            <HeaderCaptionTemplate></HeaderCaptionTemplate>
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Width="85%">
                                            <CellStyle HorizontalAlign="Left"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MEAINSPCD" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn FieldName="F_SIRYO" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn FieldName="F_STANDARD" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn FieldName="F_MAX" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn FieldName="F_MIN" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn FieldName="F_UCLR" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn FieldName="F_UCLX" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn FieldName="F_LCLX" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>

                                    </Columns>
                                </dx:ASPxGridView>
                            </section>
                        </div>
                    </td>
                    <td style="width: 30%; padding-right: 20px;">
                        <div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    <input type="button" style="visibility: hidden;" /></label>
                                <label class="col-sm-3 control-label">규격</label>
                                <label class="col-sm-3 control-label">상한규격</label>
                                <label class="col-sm-3 control-label">하한규격</label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">검사규격</label>
                                <div class="col-sm-3 control-label">
                                    <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <div class="col-sm-3 control-label">
                                    <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <div class="col-sm-3 control-label">
                                    <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">관리용</label>
                                <div class="col-sm-3 control-label">
                                    <dx:ASPxTextBox ID="txtUCLR" ClientInstanceName="txtUCLR" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <div class="col-sm-3 control-label">
                                    <dx:ASPxTextBox ID="txtUCLX" ClientInstanceName="txtUCLX" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <div class="col-sm-3 control-label">
                                    <dx:ASPxTextBox ID="txtLCLX" ClientInstanceName="txtLCLX" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-8 control-label" style="padding-left: 170px;">
                                    <dx:ASPxCheckBox ID="chk_reject" ClientInstanceName="chk_reject" runat="server" Text="규격이탈제외" />
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>

        <div class="content" style="padding-top: 5px;">
            <table class="tblContents" style="width: 100%;">
                <tr>
                    <td id="tdLeft">
                        <table class="tblContents" style="width: 100%;">
                            <tr style="padding-bottom: 10px; padding-top: 10px;">
                                <td style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                        ToolTipEnabled="True" ViewStateMode="Disabled" EnableViewState="False"
                                        OnCustomCallback="devChart1_CustomCallback" CrosshairEnabled="True" Width="300px">
                                        <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>

                            <tr style="padding-top: 0px;">
                                <td style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False"
                                        OnCustomCallback="devChart2_CustomCallback" CrosshairEnabled="True" Width="300px" Height="200px">
                                        <clientsideevents endcallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized2 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>

                            <tr style="visibility: hidden">
                                <dx:ASPxGridView ID="hidGrid" ClientInstanceName="hidGrid" runat="server" AutoGenerateColumns="true" ClientVisible="false"
                                    KeyFieldName="" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="hidGrid_CustomCallback">
                                    <Styles>
                                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                        <EditForm CssClass="bg-default"></EditForm>
                                    </Styles>
                                    <Settings VerticalScrollBarMode="Hidden" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                                    <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                </dx:ASPxGridView>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>


