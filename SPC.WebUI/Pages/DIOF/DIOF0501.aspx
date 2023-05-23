<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0501.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0501" %>
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
            fn_devCallback_PerformCallback(true);
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
        function fn_OnPrintClick() {
            if (!bSearch) { alert('인쇄를 하려면 먼저 조회를 해야합니다'); return; }
            fn_OnPopupCheckSheetReport(fn_GetCastValue('hidUCFROMDT'), fn_GetCastValue('schF_MACHIDX'));
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
            if ((schF_BANCD.GetValue() == '' || schF_BANCD.GetValue() == null)
                && (schF_MACHKIND.GetValue() == '' || schF_MACHKIND.GetValue() == null)) {
                alert('반 또는 설비분류를 선택하세요');
                return false;
            }
            if (fn_GetCastValue('schF_MACHIDX') == '') {
                alert('설비를 선택하세요!!');
                fn_Focus('schF_MACHIDX');
                return false;
            }

            return true;
        }

        // Scrolled
        function fn_OnScrolled(s, e) {
            var div = devGrid.GetScrollHelper().GetHorzScrollableControl();
            ASPxClientUtils.AttachEventToElement(div, 'scroll', function (s, e) { devGrid_Scrolled(); });
        }

        function devGrid_Scrolled() {
            var pos = devGrid.GetHorizontalScrollPosition();
            pos = Math.ceil(pos / 70) * 70;
            if (pos > 0)
                $('#divDailyCheck').css('left', (pos * -1) + 'px');
            else
                $('#divDailyCheck').css('left', '0px');
        }

        // CallbackPanel Event Handler 시작
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                devCallback.PerformCallback();
            }
        }

        // 콜백 성공시 처리 : 성공시 PKEY에 JSON 구조로 키값을 받는다.
        function fn_devCallback_CallbackComplete(s, e) {
            for (var i = 0; i < 31; i++) {
                $('#tdDays' + (i + 1)).css('font-weight', '');
                $('#tdDays' + (i + 1)).css('color', '#788288');
                $('#tdDays' + (i + 1)).html('미확인');
            }

            var result = JSON.parse(e.result);
            var isOK = typeof (result.ISOK) != 'undefined' ? result.ISOK : null;
            var msg = typeof (result.MSG) != 'undefined' ? result.MSG : null;
            if (isOK) {
                var data = {};
                if (typeof (result.PAGEDATA) != 'undefined') {
                    data = result.PAGEDATA;
                }

                $.each(data, function (index, data) {
                    $('#tdDays' + data).css('font-weight', 'bold');
                    $('#tdDays' + data).css('color', '#000000');
                    $('#tdDays' + data).html('확인');
                });
            } else {
                if (msg != 'NO DATA')
                    alert(msg);
            }
        }

        // 콜백 종료시 처리
        function fn_devCallback_EndCallback(s, e) {
            // 대기중인 작업이 있을경우, 콜백 실행
            if (devCallback_parameter) {
                devCallback.PerformCallback();
                devCallback_parameter = null;
            }
        }
        // CallbackPanel Event Handler   끝

        function fn_OnDayClick(iDay) {
            if (!bSearch) { alert('관리자확인을 하려면 먼저 조회를 해야합니다'); return; }
            fn_OnPopupDailyConfirm(fn_GetCastValue('schF_MACHIDX'), fn_GetCastText('schF_MACHIDX'), fn_GetCastValue('hidUCFROMDT') + '-' + ('0' + iDay).slice(-2));
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
            <table class="contentTable" style="border-top:0px none transparent;">
                <colgroup>
                    <col style="width:489px;" />
                    <col />
                </colgroup>
                <tr style="height:60px;">
                    <td class="tdLabel tdContentR" style="border-top:1px solid #9F9F9F;">관리자확인<br />* 관리자확인을 하려면 확인 처리할 일자를 클릭하세요</td>
                    <td class="tdContentR" style="padding:0px;margin:0px;border-bottom:0px none transparent;">
                        <div style="width:100%;height:100%;overflow:hidden;">
                            <div id="divDailyCheck" style="position:relative;left:0px;">
                                <table class="contentTable">
                                    <colgroup>
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                    </colgroup>
                                    <tr style="height:30px;">
                                        <td class="tdLabel" onclick="fn_OnDayClick('1')" style="cursor:pointer;"><%=arrDays[0]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('2')" style="cursor:pointer;"><%=arrDays[1]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('3')" style="cursor:pointer;"><%=arrDays[2]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('4')" style="cursor:pointer;"><%=arrDays[3]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('5')" style="cursor:pointer;"><%=arrDays[4]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('6')" style="cursor:pointer;"><%=arrDays[5]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('7')" style="cursor:pointer;"><%=arrDays[6]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('8')" style="cursor:pointer;"><%=arrDays[7]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('9')" style="cursor:pointer;"><%=arrDays[8]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('10')" style="cursor:pointer;"><%=arrDays[9]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('11')" style="cursor:pointer;"><%=arrDays[10]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('12')" style="cursor:pointer;"><%=arrDays[11]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('13')" style="cursor:pointer;"><%=arrDays[12]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('14')" style="cursor:pointer;"><%=arrDays[13]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('15')" style="cursor:pointer;"><%=arrDays[14]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('16')" style="cursor:pointer;"><%=arrDays[15]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('17')" style="cursor:pointer;"><%=arrDays[16]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('18')" style="cursor:pointer;"><%=arrDays[17]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('19')" style="cursor:pointer;"><%=arrDays[18]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('20')" style="cursor:pointer;"><%=arrDays[19]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('21')" style="cursor:pointer;"><%=arrDays[20]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('22')" style="cursor:pointer;"><%=arrDays[21]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('23')" style="cursor:pointer;"><%=arrDays[22]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('24')" style="cursor:pointer;"><%=arrDays[23]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('25')" style="cursor:pointer;"><%=arrDays[24]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('26')" style="cursor:pointer;"><%=arrDays[25]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('27')" style="cursor:pointer;"><%=arrDays[26]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('28')" style="cursor:pointer;"><%=arrDays[27]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('29')" style="cursor:pointer;"><%=arrDays[28]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('30')" style="cursor:pointer;"><%=arrDays[29]%></td>
                                        <td class="tdLabel" onclick="fn_OnDayClick('31')" style="cursor:pointer;"><%=arrDays[30]%></td>
                                    </tr>
                                    <tr style="height:30px;">
                                        <td id="tdDays1" class="tdInput" onclick="fn_OnDayClick('1')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays2" class="tdInput" onclick="fn_OnDayClick('2')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays3" class="tdInput" onclick="fn_OnDayClick('3')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays4" class="tdInput" onclick="fn_OnDayClick('4')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays5" class="tdInput" onclick="fn_OnDayClick('5')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays6" class="tdInput" onclick="fn_OnDayClick('6')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays7" class="tdInput" onclick="fn_OnDayClick('7')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays8" class="tdInput" onclick="fn_OnDayClick('8')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays9" class="tdInput" onclick="fn_OnDayClick('9')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays10" class="tdInput" onclick="fn_OnDayClick('10')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays11" class="tdInput" onclick="fn_OnDayClick('11')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays12" class="tdInput" onclick="fn_OnDayClick('12')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays13" class="tdInput" onclick="fn_OnDayClick('13')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays14" class="tdInput" onclick="fn_OnDayClick('14')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays15" class="tdInput" onclick="fn_OnDayClick('15')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays16" class="tdInput" onclick="fn_OnDayClick('16')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays17" class="tdInput" onclick="fn_OnDayClick('17')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays18" class="tdInput" onclick="fn_OnDayClick('18')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays19" class="tdInput" onclick="fn_OnDayClick('19')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays20" class="tdInput" onclick="fn_OnDayClick('20')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays21" class="tdInput" onclick="fn_OnDayClick('21')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays22" class="tdInput" onclick="fn_OnDayClick('22')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays23" class="tdInput" onclick="fn_OnDayClick('23')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays24" class="tdInput" onclick="fn_OnDayClick('24')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays25" class="tdInput" onclick="fn_OnDayClick('25')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays26" class="tdInput" onclick="fn_OnDayClick('26')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays27" class="tdInput" onclick="fn_OnDayClick('27')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays28" class="tdInput" onclick="fn_OnDayClick('28')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays29" class="tdInput" onclick="fn_OnDayClick('29')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays30" class="tdInput" onclick="fn_OnDayClick('30')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                        <td id="tdDays31" class="tdInput" onclick="fn_OnDayClick('31')" style="text-align:center;cursor:pointer;">&nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
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
                <ClientSideEvents Init="function(s, e) { fn_OnGridInit(s, e); fn_OnScrolled(s, e); }"
                    EndCallback="function(s, e) { fn_OnGridEndCallback(s, e); fn_OnScrolled(s, e); }"
                    CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_INSPNO" Caption="점검부위" Width="70px" FixedStyle="Left" />
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="점검항목" Width="150px" FixedStyle="Left">
                        <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPREMARK" Caption="점검내용" Width="200px" FixedStyle="Left">
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
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>