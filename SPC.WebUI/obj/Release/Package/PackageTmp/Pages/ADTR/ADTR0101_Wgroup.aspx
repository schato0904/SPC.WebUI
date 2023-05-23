<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0101_Wgroup.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0101_Wgroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var data = null;
        var offsetx;
        var offsety;
        var points = '';
        var imgOriginW, imgOriginH, imgActW, imgActH, imgNewW, imgNewH, imgRatioW, imgRatioH, pnlW, pnlH;

        $(document).ready(function () {
            fn_OnSearchClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $('.content').height(height);
            if (points != '')
                fn_AddPc();
        }

        // 조회
        function fn_OnSearchClick() {
            ASPxClientCallback.Cast('pnlSearch').PerformCallback();
        }

        function fn_Timer() {
            txtFROMDT.SetValue(new Date());
            fn_DateCheck(txtFROMDT, "");
            txtTODT.SetValue(new Date());
            fn_DateCheck(hidUCTODT, "");
            <%=ucStopwatch.ClientInstanceName%>Init();
            ASPxClientCallback.Cast('pnlSearch').PerformCallback();
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
            fn_OnSearchClick();
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

        // Image Resize To
        function fn_ImgResizeTo() {
            var img = $('#imgMonitoring');
            var pnl = $('.container');

            imgActW = parseInt($(img).width(), 10);
            imgActH = parseInt($(img).height(), 10);
            pnlW = parseInt($(pnl).width(), 10);
            pnlH = parseInt($(pnl).height(), 10);

            // 이미지가 큰경우
            if (imgActW > pnlW && imgActH > pnlH) {
                // 가로비율이 더 큰 경우
                if (Math.round(pnlW * 100 / imgActW) > Math.round(pnlH * 100 / imgActH)) {
                    imgNewW = pnlW;
                    imgNewH = Math.round((imgNewW * imgActH) / imgActW);
                } else {
                    imgNewH = pnlH;
                    imgNewW = Math.round((imgNewH * imgActW) / imgActH);
                }
            } else if (imgActW > pnlW) {
                imgNewW = pnlW;
                imgNewH = Math.round((imgNewW * imgActH) / imgActW);
            } else if (imgActH > pnlH) {
                imgNewH = pnlH;
                imgNewW = Math.round((imgActW * imgNewH) / imgActH);
            } else {
                imgNewW = imgActW;
                imgNewH = imgActH;
            }

            $('#imgMonitoring').css('height', imgNewH).css('width', imgNewW);
        }

        function fn_AddPc() {
            data = eval(points);

            $(".content").html('<img id="imgMonitoring" src="<%=ImageURL%>" style="border:1px solid #9f9f9f;" />');
            $('#imgMonitoring').load(function () {
                imgOriginW = parseInt($(this).width(), 10);
                imgOriginH = parseInt($(this).height(), 10);

                var bLoop = true;

                while (bLoop) {
                    fn_ImgResizeTo();

                    if (imgNewW <= pnlW && imgNewH <= pnlH)
                        bLoop = false;
                }

                imgRatioW = (imgNewW / imgOriginW);
                imgRatioH = (imgNewH / imgOriginH);

                var offset = $("#imgMonitoring").position();

                offsetx = offset.left;
                offsety = offset.top;

                $(data).each(function () {
                    var imgHalfSize = 6;
                    var positionX = (this.F_POSITION.split(',')[0] == '' ? 0 : parseInt(this.F_POSITION.split(',')[0], 10));//(left)
                    var positionY = (this.F_POSITION.split(',')[1] == '' ? 0 : parseInt(this.F_POSITION.split(',')[1], 10));//(top)

                    if (positionX > 0 && positionY > 0) {
                        positionX = parseInt(positionX * imgRatioW, 10) - imgHalfSize + offsetx;
                        positionY = parseInt(positionY * imgRatioH, 10) - imgHalfSize + offsety;

                        var stageTop = parseInt(positionY, 10) + 12;
                        var stageLeft = parseInt(positionX, 10) + 14;

                        if (stageTop > 220) {
                            stageTop = parseInt(stageTop, 10) - 80;
                        }

                        if (stageLeft > 440) {
                            stageLeft = parseInt(stageLeft, 10) - 133;
                        }

                        var param = { "WGROUPNM": this.F_WGROUPNM, "PCNO": this.F_PCNO, "ID": "div_" + this.F_PCNO };
                        var stageImageDesc = "<div style='text-align:center;'>" + this.F_WGROUPNM + "</div>"
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
                        var stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[1])%>';
                        // 관리양호
                        //if (this.F_CNT > 0) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[2])%>';
                        // 관리이탈
                        //if (this.F_OVER > 0) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[0])%>';
                        // SPEC이탈
                        //if (this.F_NG > 0) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[3])%>';

                        // 관리양호
                        if (this.F_LASTJUDGE == 0) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[2])%>';
                        // 관리이탈
                        if (this.F_LASTJUDGE == 2) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[0])%>';
                        // SPEC이탈
                        if (this.F_LASTJUDGE == 1) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[3])%>';

                        if (this.F_CNT == 0) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[1])%>';

                        var newimg = $("<img src='" + stageImage + "' id='img_" + this.F_PCNO + "' />");
                        $(newimg).bind('mousedown', function (e) { imgMouseOver(param); imgClick(param); });
                        $(newimg).bind('mouseover', function (e) { imgMouseOver(param); });
                        $(newimg).bind('mouseout', function (e) { imgMouseOut(param); });
                        var newdiv = $("<div id='div_" + this.F_PCNO + "' style='position:absolute;top:" + positionY + "px;left:" + positionX + "px;z-index:1; visibility:visible;'></div>");
                        $(newdiv).append($(newimg));
                        $(".content").append($(newdiv));
                        $(".content").append(stageImageDesc);
                    }
                });
            });
        }

        function imgClick(param) {
            var CompCD = "<%=gsCOMPCD%>"
                , FactCD = "<%=gsFACTCD%>"
                , FromDt = hidUCFROMDT.GetText()
                , Todt = hidUCTODT.GetText()
                , PCNO = param.PCNO;
            var key = CompCD + '|' + FactCD + '|' + FromDt + '|' + Todt + '|' + PCNO;

            fn_OnPopupMonitoringWGroup(key);
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
                        <ucCTF:Stopwatch ID="ucStopwatch" runat="server" ClientInstanceName="STATUSDIFF" ItemList="M|10;M|20;M|30;M|40;M|50;M|60" ShowRemaintime="true" CallbackEvent="fn_Timer" />
                    </div>
                </div>
            </div>
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
        </div>
        <div class="content">
            <img id="imgMonitoring" src="<%=ImageURL%>" style="border:1px solid #9f9f9f;" />
        </div>
        <div class="paging"></div>
    </div>
    <dx:ASPxCallback ID="pnlSearch" ClientInstanceName="pnlSearch" runat="server" OnCallback="pnlSearch_Callback">
        <ClientSideEvents EndCallback="pnlSearch_EndCallback" CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>
