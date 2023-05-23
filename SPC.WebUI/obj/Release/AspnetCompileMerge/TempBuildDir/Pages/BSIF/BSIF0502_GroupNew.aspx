<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0502_GroupNew.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0502_GroupNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var isUpdate = false;
        var posdata = [];
        var isMove = false;
        var xobj = null;
        var offset = null;
        var offset_x = null;
        var offset_y = null;
        var icon_green = '<%=this.iconURL%>';
            var icon_red = '<%=this.iconURL%>'.replace('green', 'red');

        $(document).ready(function () {
            fn_OnSearchClick();
            fn_RendorTotalCount();

            offset = $('#imgPanel').offset();
            offset_y = offset.top;
            offset_x = offset.left;
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            devGrid.SetHeight(200);
            var top = $(".container").offset().top;
            var searchHeight = $(".content").height() > 0 ? $(".content").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $('#imgPanel').height(height);
        }

        // 조회
        function fn_OnSearchClick() {
            isUpdate = false;
            fn_doSetGridEventAction('true');
            devGrid.PerformCallback();
            fn_LoadImage();
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (!isUpdate) {
                alert("수정된 내역이 없습니다.")
                return;
            }
            devCallback.PerformCallback(JSON.stringify(posdata));
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
            } else {
                fn_CreateBullet();
            }

            devGrid.SetFocusedRowIndex(-1);
            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // 뷸렛생성
        function fn_CreateBullet() {
            var rowCount = devGrid.GetVisibleRowsOnPage();

            posdata = [];

            for (var i = 0; i < rowCount; i++) {
                var rowKeys = devGrid.GetRowKey(i).split('|');
                var newdata = {};
                newdata.MACHIDX = rowKeys[0];
                newdata.MACHNM = rowKeys[1];
                newdata.POSX = rowKeys[2];
                newdata.POSY = rowKeys[3];
                newdata.STATUS = rowKeys[4];
                posdata.push(newdata);
            }

            $('#imgPanel div').remove();

            var newpnl = $('<div id="divLayout" style="width:' + $('#imgLayout').width() + 'px;height:' + $('#imgLayout').height() + 'px;"></div>');
            newpnl.css("background-image", "url(" + $('#imgLayout').attr('src') + ")");

            $(posdata).each(function () {
                var pos_x = this.POSX == '' ? 0 : parseInt(this.POSX, 10);
                var pos_y = this.POSY == '' ? 0 : parseInt(this.POSY, 10);
                var param = { "XVal": pos_x, "YVal": pos_y, "MACHIDX": this.MACHIDX, "MACHNM": this.MACHNM, "ID": "div_" + this.MACHIDX };
                var newdiv = $("<div id='div_" + this.MACHIDX + "' style='z-index:" + this.MACHIDX + ";position:relative;'></div>");
                newdiv.css('width', '<%=iconSIZE%>');
                newdiv.css('height', '<%=iconSIZE%>');
                newdiv.css('cursor', 'pointer');
                newdiv.css('background-image', 'url(' + icon_green + ')');
                newdiv.attr('title', this.MACHNM);
                $(newdiv).draggable({
                    containment: '#divLayout',
                    snap: '#divLayout'
                }).on("dragstart", function (event, ui) {
                    $(this).css('background-image', 'url(' + icon_red + ')');
                    dragStart(param, parseInt(ui.offset.top, 10) - parseInt(offset_y, 10), parseInt(ui.offset.left, 10) - parseInt(offset_x, 10));
                }).on("dragstop", function (event, ui) {
                    dragStop(param, parseInt(ui.offset.top, 10) - parseInt(offset_y, 10), parseInt(ui.offset.left, 10) - parseInt(offset_x, 10), true);
                });
                newpnl.append(newdiv);
            });

            $('#imgPanel').append(newpnl);

            $(posdata).each(function () {
                var pos_x = this.POSX == '' ? 0 : parseInt(this.POSX, 10);
                var pos_y = this.POSY == '' ? 0 : parseInt(this.POSY, 10);
                $('#div_' + this.MACHIDX).offset({ top: parseInt(pos_y, 10) + parseInt(offset_y, 10), left: parseInt(pos_x, 10) + parseInt(offset_x, 10) });

                var param = { "XVal": pos_x, "YVal": pos_y, "MACHIDX": this.MACHIDX, "MACHNM": this.MACHNM, "ID": "div_" + this.MACHIDX };
                $('#div_' + this.MACHIDX).bind('click', function () {
                    dragStop(param, parseInt(pos_y, 10), parseInt(pos_x, 10), false);
                    $('#imgPanel div').each(function () {
                        if ($(this).attr('id') != 'divLayout')
                            $(this).css('background-image', 'url(' + icon_green + ')');
                    });
                    $(this).css('background-image', 'url(' + icon_red + ')');
                    devGrid.SetFocusedRowIndex(parseInt(param.NO, 10) - 1);
                });
            });
        }

        function dragStart(param, y, x) {
            $('#imgPanel div').each(function () {
                if ($(this).attr('id') != 'divLayout')
                    $(this).css('background-image', 'url(' + icon_green + ')');
            });
            $('#div_' + param.MACHIDX).css('background-image', 'url(' + icon_red + ')');
            $('#div_' + param.MACHIDX).css('cursor', 'move');

            $('#tdMach').html(param.MACHNM);
            fn_SetTextValue('hidMACHIDX', param.MACHIDX);
            fn_SetTextValue('txtXPOS', x + parseInt($('#imgPanel').scrollTop(), 10));
            fn_SetTextValue('txtYPOS', y + parseInt($('#imgPanel').scrollLeft(), 10));
            devGrid.SetFocusedRowIndex(parseInt(param.NO, 10) - 1);
        }

        function dragStop(param, y, x, _isUpdate) {
            $('#div_' + param.MACHIDX).css('cursor', 'pointer');

            isUpdate = _isUpdate;
            Data_insert(x, y, param.MACHIDX);
            $('#tdMach').html(param.MACHNM);
            fn_SetTextValue('hidMACHIDX', param.MACHIDX);
            fn_SetTextValue('txtXPOS', x + parseInt($('#imgPanel').scrollTop(), 10));
            fn_SetTextValue('txtYPOS', y + parseInt($('#imgPanel').scrollLeft(), 10));
        }

        function Data_insert(valx, valy, machIDX) {
            $(posdata).each(function () {
                if (this.MACHIDX == machIDX) {
                    this.POSY = valy + parseInt($('#imgPanel').scrollTop());
                    this.POSX = valx + parseInt($('#imgPanel').scrollLeft());
                }
            });
        }

        // 도면 업로드 완료 후
        function fn_LoadImage() {
            var layoutImg = $('#imgLayout');
            layoutImg.attr('src', '<%=s_ImageIDX%>');
        }

        // CallbackPanel Event Handler 시작
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                devCallback.PerformCallback(JSON.stringify(parameter));
            }
        }

        // 콜백 성공시 처리 : 성공시 PKEY에 JSON 구조로 키값을 받는다.
        function fn_devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            var isOK = typeof (result.ISOK) != 'undefined' ? result.ISOK : null;
            var msg = typeof (result.MSG) != 'undefined' ? result.MSG : null;
            if (isOK) {
                alert(msg);
                fn_OnSearchClick();
            } else {
                alert(msg);
            }
        }

        // 콜백 종료시 처리
        function fn_devCallback_EndCallback(s, e) {
            // 대기중인 작업이 있을경우, 콜백 실행
            if (devCallback_parameter) {
                devCallback.PerformCallback(JSON.stringify(devCallback_parameter));
                devCallback_parameter = null;
            }
        }

        // X,Y축 수동조절
        function fn_OnPOSTextChanged(s, e) {
            var _machIDX = fn_GetCastText('hidMACHIDX');
            var _posX = fn_GetCastText('txtXPOS');
            var _posY = fn_GetCastText('txtYPOS');

            $('#div_' + _machIDX).offset({ top: parseInt(_posY, 10) + parseInt(offset_y, 10), left: parseInt(_posX, 10) + parseInt(offset_x, 10) });

            Data_insert(_posX, _posY, _machIDX, true);
        }

        // 그리드 선택 시
        function fn_OnRowClick(s, e) {
            $('#imgPanel div').each(function () {
                if ($(this).attr('id') != 'divLayout')
                    $(this).css('background-image', 'url(' + icon_green + ')');
            });

            var rowKeys = devGrid.GetRowKey(e.visibleIndex).split('|');
            $('#div_' + rowKeys[0]).css('background-image', 'url(/SPC.WebUI/Resources/icons/14x14_red.png)');

            $('#tdMach').html(rowKeys[1]);
            fn_SetTextValue('hidMACHIDX', rowKeys[0]);
            fn_SetTextValue('txtXPOS', rowKeys[2]);
            fn_SetTextValue('txtYPOS', rowKeys[3]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div style="width:0px;height:0px;overflow:hidden;"><img id="imgLayout" src="" /></div>
    <div class="container">
        <div class="search">
            <div id="imgPanel" style="overflow:scroll;width:100%;background-color:#e0e0e0;"></div>
        </div>
        <div class="content">
            <div class="divLine"></div>
            <dx:ASPxTextBox ID="hidMACHIDX" ClientInstanceName="hidMACHIDX" runat="server" ClientVisible="false" />
            <table class="contentTable">
                <colgroup>
                    <col style="width:8%;" />
                    <col style="width:17%;" />
                    <col />
                    <col style="width:80px;" />
                    <col style="width:180px;" />
                    <col style="width:50px;" />
                    <col style="width:80px;" />
                    <col style="width:50px;" />
                    <col style="width:80px;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">생산팀</td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="schF_BANCD" ClientInstanceName="schF_BANCD" />
                    </td>
                    <td></td>
                    <td class="tdLabel">선택설비</td>
                    <td id="tdMach" class="tdInput">
                    </td>
                    <td class="tdLabel">X축</td>
                    <td id="tdXpos" class="tdInput">
                        <dx:ASPxTextBox ID="txtXPOS" ClientInstanceName="txtXPOS" runat="server" Width="100%">
                            <ClientSideEvents KeyPress="fn_ValidateOnlyNumber" TextChanged="fn_OnPOSTextChanged" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">Y축</td>
                    <td id="tdYpos" class="tdInput">
                        <dx:ASPxTextBox ID="txtYPOS" ClientInstanceName="txtYPOS" runat="server" Width="100%">
                            <ClientSideEvents KeyPress="fn_ValidateOnlyNumber" TextChanged="fn_OnPOSTextChanged" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="MACHIDX;MACHNM;POSX;POSY;STATUS" EnableViewState="false" EnableRowsCache="false"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowFocusedRow="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    RowClick="fn_OnRowClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="MACHIDX" Caption="No." Width="70px" />
                    <dx:GridViewDataColumn FieldName="MACHNM" Caption="공정그룹명" Width="100%">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewBandColumn Caption="설비위치">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="POSX" Caption="X" Width="100px" />
                            <dx:GridViewDataColumn FieldName="POSY" Caption="Y" Width="100px" />
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="STATUS" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>