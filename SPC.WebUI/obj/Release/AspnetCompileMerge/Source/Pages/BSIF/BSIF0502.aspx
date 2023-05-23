<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0502.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0502" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .WORKGRID {
            display:none;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var isUpdate = false;
        var data = null;
        var isMove = false;
        var offsetx;
        var offsety;
        var xobj = null;

        $(document).ready(function () {
            $("#imgMonitoring").bind('mousemove', function () { mouseMove(); });

            var offset = $("#imgMonitoring").offset();

            offsetx = offset.left
            offsety = offset.top

            fn_OnSearchClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            //var top = $(".container").offset().top;
            //var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            //var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            //var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            //devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            isUpdate = false;
            //var params = { "command": "init" };
            //ASPxClientCallbackPanel.Cast('pnlSearch').PerformCallback(JSON.stringify(params));
            
            ASPxClientCallbackPanel.Cast('pnlSearch').PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

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
            if(!isUpdate){
                alert("수정된 내역이 없습니다.")
                return;
            }
            //var params = { "command": "" };
            //ASPxClientCallbackPanel.Cast('pnlSearch').PerformCallback(JSON.stringify(params));
            
            ASPxClientCallbackPanel.Cast('pnlSearch').PerformCallback(JSON.stringify(data));
            
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
                fn_AddPc(s.cpTable)
            }
        }

        function fn_AddPc(pcTable) {
            data = eval(pcTable);

            $(".content").html('<img id="imgMonitoring" src="<%=ImageURL%>" />');
            $("#imgMonitoring").bind('mousemove', function () { mouseMove(); });

            $(data).each(function () {
                var positionX = this.F_POSITION.split(',')[0] == '0' ? (offsetx) : parseInt(this.F_POSITION.split(',')[0],10) + offsetx
                   ,positionY = this.F_POSITION.split(',')[1] == '0' ? (offsety) : parseInt(this.F_POSITION.split(',')[1],10) + offsety;

                var param = { "XVal": positionX, "YVal": positionY, "PCNO": this.F_PCNO, "ID": "div_" + this.F_PCNO };
                
                var newimg = $("<img src='<%=iconURL%>' id='img_" + this.F_PCNO + "' title='[No. " + this.F_PCNO + "] " + this.F_PCNM + "' />");
                $(newimg).bind('mousedown', function (e) { imgClick(param) });
                var newdiv = $("<div id='div_" + this.F_PCNO + "' style='position:absolute;top:" + positionY + "px;left:" + positionX + "px;z-index:1; visibility:visible;'></div>");
                $(newdiv).append($(newimg));
                $(".content").append($(newdiv))
            });
        }

        function mouseMove() {
            //var x = event.clientX - offsetx
            //var y = event.clientY - offsety
            var x = event.clientX;
            var y = event.clientY - 11;

            if (isMove) {
                moveTo(xobj, x, y);                
            }

            //$("#x").html(x);
            //$("#y").html(y);
        }

        function moveTo(obj, xL, yL) {
            document.getElementById(obj).style.left = xL + 'px';
            document.getElementById(obj).style.top = yL + 'px';            
        }

        function imgClick(param) {
            
            xobj = param.ID

            if (isMove == false) {
                isMove = true;
                return;
            }
            isMove = false;
            var x = event.clientX - offsetx;
            var y = event.clientY - 11 - offsety;
            Data_insert(x, y, param.PCNO);
        }

        function Data_insert(valx, valy, pcno) {
            isUpdate = true;
            
            $(data).each(function () {
                if(this.F_PCNO == pcno){
                    this.F_POSITION = valx + ',' + valy;
                    this.F_GBN = "y";
                }
            });
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <%--<div class="search">
        </div>--%>
        
        <div class="content">
            <img id="imgMonitoring" src="<%=ImageURL%>" />
        </div>
        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
        <dx:ASPxCallbackPanel ID="pnlSearch" runat="server" ClientInstanceName="pnlSearch" Enabled="true" Visible="true" Width="100%" OnCallback="pnlSearch_Callback" ClientSideEvents-EndCallback="pnlSearch_EndCallback" ClientSideEvents-CallbackError="fn_OnCallbackError"  >
        </dx:ASPxCallbackPanel>
        <div class="paging"></div>
        <%--<p id="x"></p>
        <p id="y"></p>--%>
    </div>
</asp:Content>
