<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0101FND_Work.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0101FND_Work" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var data = null;
        var offsetx;
        var offsety;
        var points = '';
        var imgOldW, imgOldH, imgNewW, imgNewH, imgRatioW, imgRatioH, pnlW, pnlH;

        $(document).ready(function () {
            devGrid.SetHeight(140);
            var offset = $("#imgMonitoring").offset();

            offsetx = offset.left
            offsety = offset.top

            fn_OnSearchClick();

            $(document).tooltip({
                items: "[data-toggle]",
                content: function () {
                    return $(this).prop('title');
                }
            });
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $('.content').height(height-30);
            if (points != '')
                fn_AddPc();
        }

        // 조회
        function fn_OnSearchClick() {
            txtFROMDT.SetValue(new Date());
            fn_DateCheck(txtFROMDT, "");
            txtTODT.SetValue(new Date());
            fn_DateCheck(hidUCTODT, "");
            <%=ucStopwatch.ClientInstanceName%>Init();
            ASPxClientCallback.Cast('pnlSearch').PerformCallback();

            //var ref = "http://14.35.235.229/SPC.WebUI/Pages/ADTR/ADTR0104FND.aspx?pParam=MM02|MM0201|ADTR|ADTR0104FND|실시간측정모니터링|R|1&oSetParam=&bPopup=true&bFrame=false";
            //var url = location.href + '&bPopup=true&bFrame=false';
            //OpenNewFullWindow(ref);
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
        function pnlSearch_EndCallback(s, e) {

            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            } else if (s.cpTable != '') {
                points = s.cpTable;
                fn_AddPc();
            }
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

        // Image Resize To
        function fn_ImgResizeTo() {
            var img = $('#imgMonitoring');
            var pnl = $('.content');

            imgOldW = parseInt($(img).width(), 10);
            imgOldH = parseInt($(img).height(), 10);
            pnlW = parseInt($(pnl).width(), 10);
            pnlH = parseInt($(pnl).height(), 10);

            if (pnlW > imgOldW || pnlH > imgOldH) {
                if (pnlW > pnlH) {
                    imgNewW = (pnlW > imgOldW) ? pnlW : imgOldW;
                    imgNewH = Math.round((imgNewW * imgOldH) / imgOldW);
                } else {
                    imgNewH = (pnlH > imgOldH) ? pnlH : imgOldH;
                    imgNewW = Math.round((imgOldW * imgNewH) / imgOldH);
                }

            } else {
                imgNewW = imgOldW;
                imgNewH = imgOldH;
            }

            if (imgNewW / pnlW > imgNewH / pnlH) {
                imgNewW = parseInt($(pnl).width());
                imgNewH = Math.round((imgNewW * imgOldH) / imgOldW);
            } else {
                imgNewH = parseInt($(pnl).height());
                imgNewW = Math.round((imgOldW * imgNewH) / imgOldH);
            }


            imgRatioW = (imgNewW / imgOldW) - 1;
            imgRatioH = (imgNewH / imgOldH) - 1;

            $('#imgMonitoring').css('height', imgNewH).css('width', imgNewW);
        }

        function fn_AddPc() {
            data = eval(points);

            $(".content").html('<img id="imgMonitoring" src="<%=ImageURL%>" style="border:1px solid #9f9f9f;" />');
            //imgMonitoring.style.marginLeft = (imgMonitoring.style.marginRight + imgMonitoring.style.marginleft) / 2;
            $('#imgMonitoring').css('marginLeft', ((parseInt($(".container").css('width'))) - parseInt($('#imgMonitoring').css('width'))) / 2)
            
            var offset = $("#imgMonitoring").offset();

            offsetx = offset.left

            fn_ImgResizeTo();

            $(data).each(function () {
                var positionX = this.F_POSITION.split(',')[0] == '0' ? (offsetx) : parseInt(this.F_POSITION.split(',')[0], 10) + offsetx;
                var positionY = this.F_POSITION.split(',')[1] == '0' ? (offsety) : parseInt(this.F_POSITION.split(',')[1], 10) + offsety;

                positionX = parseInt(positionX, 10) + (parseInt(positionX, 10) * imgRatioW) - (offsetx * imgRatioW);
                positionY = parseInt(positionY, 10) + (parseInt(positionY, 10) * imgRatioH) - (offsety * imgRatioH);

                var stageTop = parseInt(positionY, 10) + 12;
                var stageLeft = parseInt(positionX, 10) + 14;

                if (stageTop > 220) {
                    stageTop = parseInt(stageTop, 10) - 80;
                }

                if (stageLeft > 440) {
                    stageLeft = parseInt(stageLeft, 10) - 133;
                }

                var param = { "PCNM": this.F_PCNM, "PCNO": this.F_PCNO, "ID": "div_" + this.F_PCNO };
                var stageImageDesc = "<div style='text-align:center;'>" + this.F_PCNM + "</div>"
                                    + "<div id='tip_" + this.F_PCNO + "'>"
                                    + "&nbsp;&nbsp;검사수 : " + this.F_CNT + " 건<br>"
                                    + "&nbsp;&nbsp;불량수 : " + this.F_NG + " 건<br>"
                                    + "관리이탈 : " + this.F_OVER + " 건"
                                    + "</div>"
                stageImageDesc = "<table border='1' width='120' height='64' cellspacing='0' cellpadding='0' style='background-color:white;'>"
                                + " <tr>"
                                    + "<td width='100%' height='100%' valign='middle' align='left'>" + stageImageDesc + "</td>"
                                + " </tr>"
                                + "</table>";
                stageImageDesc = "<div id='desc_" + this.F_PCNO + "' style='position:absolute; z-index:99999; "
                                + "top:" + stageTop + "px; left:" + stageLeft + "px; width:200px; height:64px; visibility:hidden; '> "
                                + stageImageDesc
                                + "</div>";

                // 미사용
                var stageImage = "<%=iconURL%>/gray.gif";
                // 관리양호
                if (this.F_CNT > 0) {
                    stageImage = "<%=iconURL%>/green.gif";
                }
                // 관리이탈
                if (this.F_LASTOVER > 0) {
                    stageImage = "<%=iconURL%>/blue.gif";
                }
                // SPEC이탈
                if (this.F_LASTNG > 0) {
                    stageImage = "<%=iconURL%>/red.gif";
                }

                var newimg = $("<img src='" + stageImage + "' id='img_" + this.F_PCNO + "' />");
                $(newimg).bind('mousedown', function (e) { imgMouseOver(param); imgClick(param); });
                $(newimg).bind('mouseover', function (e) { imgMouseOver(param); });
                $(newimg).bind('mouseout', function (e) { imgMouseOut(param); });
                var newdiv = $("<div id='div_" + this.F_PCNO + "' style='position:absolute;top:" + positionY + "px;left:" + positionX + "px;z-index:1; visibility:visible;'></div>");
                $(newdiv).append($(newimg));
                $(".content").append($(newdiv))
                $(".content").append(stageImageDesc)
            });
        }

        function imgClick(param) {
            var CompCD = "<%=gsCOMPCD%>"
                , FactCD = "<%=gsFACTCD%>"
                , FromDt = hidUCFROMDT.GetText()
                , Todt = hidUCTODT.GetText()
                , PCNO = param.PCNO;
            var key = CompCD + '|' + FactCD + '|' + FromDt + '|' + Todt + '|' + PCNO;

            //fn_OnPopupMonitoringWork(key);
            fn_OnSearchMeasureClick(Todt + '|' + PCNO);
        }

        function imgMouseOver(param) {
            document.getElementById('desc_' + param.PCNO).style.visibility = "visible";
        }

        function imgMouseOut(param) {
            document.getElementById('desc_' + param.PCNO).style.visibility = "hidden";
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // 측정모니터링
        function fn_OnSearchMeasureClick(sWORKCD) {
            devGrid.PerformCallback(sWORKCD);
        }

        // 상세내역보기
        function fn_OnPopupList(TIMEZONE, visibleIndex) {
            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(visibleIndex));
            fn_OnPopupADTR0104('01|생산|01|성형라인|' + rowKeys + '|' + fn_GetCastText('hidUCFROMDT') + '|' + TIMEZONE);
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
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false"  TodayFromDate="true" />
                    </div>
                    <label class="col-sm-1 control-label">갱신주기</label>
                    <div class="col-sm-3">
                        <ucCTF:Stopwatch ID="ucStopwatch" runat="server" ClientInstanceName="STATUSDIFF" ItemList="M|10;M|20;M|30;M|40;M|50;M|60" ShowRemaintime="true" CallbackEvent="fn_OnSearchClick" />
                    </div>
                </div>
            </div>
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
        </div>
        <div class="content">
            <img id="imgMonitoring" src="<%=ImageURL%>" style="border:1px solid #9f9f9f;" />
        </div>
        <div class="paging" style="padding:10px 0px 10px 0px;">
            <dx:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="ASPxTextBox2" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKCD;F_WORKNM;F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Visible="false" />
                    <dx:GridViewBandColumn Caption="검사예약, 측정 및 무작업정보">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE01" Caption="08" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE02" Caption="09" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE03" Caption="10" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE04" Caption="11" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE05" Caption="12" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE06" Caption="13" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE07" Caption="14" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE08" Caption="15" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE09" Caption="16" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE10" Caption="17" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE11" Caption="18" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE12" Caption="19" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE13" Caption="20" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE14" Caption="21" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE15" Caption="22" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE16" Caption="23" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE17" Caption="24" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE18" Caption="01" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE19" Caption="02" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE20" Caption="03" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE21" Caption="04" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE22" Caption="05" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE23" Caption="06" Width="35px" />
                            <dx:GridViewDataColumn FieldName="F_TIMEZONE24" Caption="07" Width="35px" />
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="F_TIMEZONE" Caption="타임존" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
    <dx:ASPxCallback ID="pnlSearch" ClientInstanceName="pnlSearch" runat="server" OnCallback="pnlSearch_Callback">
        <ClientSideEvents EndCallback="pnlSearch_EndCallback" CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>
