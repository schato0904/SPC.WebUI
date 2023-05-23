<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0202.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0202" %>
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
            devGrid.SetWidth(parseInt($(".search").width() - 5, 10));
            devGrid.SetHeight(190);
            devGridWork.SetHeight(135)
            chartWidth = parseInt($(".search").width() - 5, 10);
            chartHeight1 = parseInt((height - 210), 10);
            chartHeight2 = height - 335;

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);
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

            if (hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) {
                alert("품목을 선택하세요!!");
                return false;
            }

            if (false == fn_WorkCheck())
                return false;
            
            devGrid.PerformCallback();
        }


        function fn_WorkCheck() {
            selectedKeys = devGridWork.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('우측 검사항목을 선택하세요!!');
                return false;
            }

            if (selectedKeys.length > 11) {
                alert('공정은 최대 10개까지 선택 가능합니다!!');
                return false;
            }

            var Serialno = selectedKeys.join('');

            txtCNT.SetValue(selectedKeys.length);
            txtSERIALNO.SetValue(Serialno)
        }
        // 입력
        function fn_OnNewClick() {
            devGrid.AddNewRow();
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
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else
                devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }
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
                fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table style="width:100%" border="0">
                <tr>
                    <td style="width:60%">
                        <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                
                            <div class="form-group">
                                <label class="col-sm-2 control-label">검색일자</label>
                                <div class="col-sm-4">
                                    <ucCTF:Date runat="server" id="ucDate"  />
                                </div>                    
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">품목</label>
                                <div class="col-sm-8">
                                    <ucCTF:Item ID="ucItem" runat="server" />
                                </div>                                     
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">공정</label>
                                <div class="col-sm-8 control-label">
                                    <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" CallBackInsp="fn_CallBack()" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">선택</label>
                                <div class="col-sm-8 control-label">
                                    <dx:ASPxRadioButtonList ID="rdoGBN" runat="server" RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None" >
                                        <Items>
                                            <dx:ListEditItem Value="1" Text="Cp" />
                                            <dx:ListEditItem Value="2" Text="Cpk" />
                                        </Items>                                                                
                                    </dx:ASPxRadioButtonList>
                                </div>
                            </div>
                        </div>    
                    </td>
                    <td style="padding-top:9px;">
                        <section class="panel panel-default" style="height: 100%;">                    
                            <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" />
                            <dx:ASPxTextBox ID="txtCNT" ClientInstanceName="txtCNT" runat="server" ClientVisible="false" />
                            <dx:ASPxGridView ID="devGridWork" ClientInstanceName="devGridWork" runat="server" AutoGenerateColumns="false"  Width="100%"
                            KeyFieldName="F_SERIALNO" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGridWork_CustomCallback" >
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                                <SettingsBehavior AllowSort="false" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                                        <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                                ClientSideEvents-CheckedChanged="function(s, e) { devGridWork.SelectAllRowsOnPage(s.GetChecked()); }" />
                                        </HeaderTemplate>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="100px" >
                                        <CellStyle HorizontalAlign="Left" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="검사규격" Width="80px">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격" Width="80px">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격" Width="80px">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </section>
                    </td>
                </tr>
            </table>
            
        </div>
        <div class="content">
            <table class="tblContents" style="width: 100%;">
                <tr>
                    <td id="tdLeft" style="padding-right: 5px;">
                        <table class="tblContents" style="width: 100%;">
                            <tr>
                                <td style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                                        OnCustomCallback="devChart1_CustomCallback">
                                        <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized1 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 200px; padding-top: 10px;">
                                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                                    <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%"
                                        KeyFieldName="F_SERIALNO" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid_CustomCallback">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"  />
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
