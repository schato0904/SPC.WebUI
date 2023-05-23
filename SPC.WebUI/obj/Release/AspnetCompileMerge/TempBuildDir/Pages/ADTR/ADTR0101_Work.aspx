<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0101_Work.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0101_Work" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var data = null;
        var offsetx;
        var offsety;
        var points = '';
        var imgOldW, imgOldH, imgNewW, imgNewH, imgRatioW, imgRatioH, pnlW, pnlH;

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

            imgRatioW = (imgNewW / imgOldW);
            imgRatioH = (imgNewH / imgOldH);

            $('#imgMonitoring').css('height', imgNewH).css('width', imgNewW);
        }

        function fn_AddPc() {
            data = eval(points);

            $(".content").html('<img id="imgMonitoring" src="<%=ImageURL%>" style="border:1px solid #9f9f9f;" />');

            fn_ImgResizeTo();

            var marginLeft = ((parseInt($(".container").css('width'))) - parseInt($('#imgMonitoring').css('width'))) / 2;
            $('#imgMonitoring').css('marginLeft', marginLeft);

            var offset = $("#imgMonitoring").position();

            offsetx = offset.left;
            offsety = offset.top;

            $(data).each(function () {
                var imgHalfSize = 11;
                var positionX = (this.F_POSITION.split(',')[0] == '' ? 0 : parseInt(this.F_POSITION.split(',')[0], 10)) + imgHalfSize;//(left)
                var positionY = (this.F_POSITION.split(',')[1] == '' ? 0 : parseInt(this.F_POSITION.split(',')[1], 10)) + imgHalfSize;//(top)
                
                if (positionX > 0 && positionY > 0) {
                    positionX = (parseInt(positionX * imgRatioW, 10)) - imgHalfSize + offsetx + marginLeft;
                    positionY = (parseInt(positionY * imgRatioH, 10)) - imgHalfSize + offsety;
                    
                    var stageTop = parseInt(positionY, 10) + 12;
                    var stageLeft = parseInt(positionX, 10) + 14;

                    if (stageTop > 220) {
                        stageTop = parseInt(stageTop, 10) - 80;
                    }

                    if (stageLeft > 440) {
                        stageLeft = parseInt(stageLeft, 10) - 133;
                    }

                    var param = { "PCNM": this.F_WORKNM, "F_WORKCD": this.F_WORKCD, "ID": "div_" + this.F_WORKCD };
                    var stageImageDesc = "<div style='text-align:center;'>" + this.F_WORKNM + "</div>"
                                        + "<div id='tip_" + this.F_WORKCD + "'>"
                                        + "&nbsp;&nbsp;검사수 : " + this.F_CNT + " 건<br>"
                                        + "&nbsp;&nbsp;불량수 : " + this.F_NG + " 건<br>"
                                        + "관리이탈 : " + this.F_OVER + " 건"
                                        + "</div>"
                    stageImageDesc = "<table border='1' width='120' height='64' cellspacing='0' cellpadding='0' style='background-color:white;'>"
                                    + " <tr>"
                                        + "<td width='100%' height='100%' valign='middle' align='left'>" + stageImageDesc + "</td>"
                                    + " </tr>"
                                    + "</table>";
                    stageImageDesc = "<div id='desc_" + this.F_WORKCD + "' style='position:absolute; z-index:99999; "
                                    + "top:" + stageTop + "px; left:" + stageLeft + "px; width:200px; height:64px; visibility:hidden; '> "
                                    + stageImageDesc
                                    + "</div>";

                    // 미사용
                    var stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[1])%>';
                    // 관리양호
                    if (this.F_CNT > 0) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[2])%>';
                    // 관리이탈
                    if (this.F_OVER > 0) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[0])%>';
                    // SPEC이탈
                    if (this.F_NG > 0) stageImage = '<%=String.Format("{0}{1}", iconURL, arrBullet[3])%>';

                    var newimg = $("<img src='" + stageImage + "' id='img_" + this.F_WORKCD + "' />");
                    $(newimg).bind('mousedown', function (e) { imgMouseOver(param); imgClick(param); });
                    $(newimg).bind('mouseover', function (e) { imgMouseOver(param); });
                    $(newimg).bind('mouseout', function (e) { imgMouseOut(param); });
                    var newdiv = $("<div id='div_" + this.F_WORKCD + "' style='position:absolute;top:" + positionY + "px;left:" + positionX + "px;z-index:1; visibility:visible;'></div>");
                    $(newdiv).append($(newimg));
                    $(".content").append($(newdiv));
                    $(".content").append(stageImageDesc);
                }
            });
        }

        function imgClick(param) {
            var CompCD = "<%=gsCOMPCD%>"
                , FactCD = "<%=gsFACTCD%>"
                , FromDt = hidUCFROMDT.GetText()
                , Todt = hidUCTODT.GetText()
                , F_WORKCD = param.F_WORKCD;
            var key = CompCD + '|' + FactCD + '|' + FromDt + '|' + Todt + '|' + F_WORKCD;

            fn_OnPopupMonitoringWork(key);
        }

        function imgMouseOver(param) {
            document.getElementById('desc_' + param.F_WORKCD).style.visibility = "visible";
        }

        function imgMouseOut(param) {
            document.getElementById('desc_' + param.F_WORKCD).style.visibility = "hidden";
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
        <div class="paging"></div>
    </div>
    <dx:ASPxCallback ID="pnlSearch" ClientInstanceName="pnlSearch" runat="server" OnCallback="pnlSearch_Callback">
        <ClientSideEvents EndCallback="pnlSearch_EndCallback" CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>
