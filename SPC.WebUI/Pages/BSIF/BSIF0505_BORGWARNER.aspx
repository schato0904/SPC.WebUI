<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0505_BORGWARNER.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0505_BORGWARNER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            pnlChart.PerformCallback();
        });

        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
        });

        // 조회
        function fn_OnSearchClick() {
            var selectedlocation = cbLOCATION.GetValue();
            if (selectedlocation == '0') {
                alert('조회 할 설비를 선택하세요.');
                return false;
            }
            pnlChart.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
            var selectedlocation = cbLOCATION.GetValue();
            if (selectedlocation == '0') {
                alert('변경 할 설비를 선택하세요.');
                return false;
            }
            else {
                devCallback.PerformCallback();
            }
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

        // Grid End Callback fn_OnGridEndCallback
        function fn_OnGrid1EndCallback(s, e) {
            fn_doSetGridEventAction('false');
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_OndevCallback(s, e) {
            pnlChart.PerformCallback();
            alert('저장 되었습니다.');
        }

        function fn_OnLocationChanged(s, e) {

            var selectedlocation = cbLOCATION.GetValue();

            hidLOCATION.SetText(selectedlocation);

            pnlChart.PerformCallback();
        }
        function endcallback(s, e) {
            var selectedlocation = cbLOCATION.GetValue();

            if (selectedlocation == 0) {
                cbM11.SetEnabled(false);
                cbM21.SetEnabled(false);
                cbM31.SetEnabled(false);
                cbM32.SetEnabled(false);
                cbM33.SetEnabled(false);
                cbM41.SetEnabled(false);
                cbM42.SetEnabled(false);
                cbM43.SetEnabled(false);
                cbM51.SetEnabled(false);
                cbM52.SetEnabled(false);
                cbM53.SetEnabled(false);
                cbM61.SetEnabled(false);

                fn_OnControlDisableBox(cbM11);
                fn_OnControlDisableBox(cbM21);
                fn_OnControlDisableBox(cbM31);
                fn_OnControlDisableBox(cbM32);
                fn_OnControlDisableBox(cbM33);
                fn_OnControlDisableBox(cbM41);
                fn_OnControlDisableBox(cbM42);
                fn_OnControlDisableBox(cbM43);
                fn_OnControlDisableBox(cbM51);
                fn_OnControlDisableBox(cbM52);
                fn_OnControlDisableBox(cbM53);
                fn_OnControlDisableBox(cbM61);
            }
            else {
                cbM11.SetEnabled(true);
                cbM21.SetEnabled(true);
                cbM41.SetEnabled(true);
                cbM51.SetEnabled(true);
                cbM61.SetEnabled(true);

                if (selectedlocation == 1) {
                    cbM31.SetEnabled(false);
                    cbM32.SetEnabled(false);
                    cbM33.SetEnabled(false);
                    cbM42.SetEnabled(false);
                    cbM43.SetEnabled(false);
                    cbM52.SetEnabled(false);
                    cbM53.SetEnabled(false);

                    fn_OnControlDisableBox(cbM31);
                    fn_OnControlDisableBox(cbM32);
                    fn_OnControlDisableBox(cbM33);
                    fn_OnControlDisableBox(cbM42);
                    fn_OnControlDisableBox(cbM43);
                    fn_OnControlDisableBox(cbM52);
                    fn_OnControlDisableBox(cbM53);
                }
                else if (selectedlocation == 2 || selectedlocation == 3) {
                    cbM32.SetEnabled(false);
                    cbM33.SetEnabled(false);
                    cbM52.SetEnabled(false);
                    cbM53.SetEnabled(false);
                    fn_OnControlDisableBox(cbM32);
                    fn_OnControlDisableBox(cbM33);
                    fn_OnControlDisableBox(cbM52);
                    fn_OnControlDisableBox(cbM53);

                    cbM41.SetEnabled(true);
                    cbM42.SetEnabled(true);
                    cbM43.SetEnabled(true);
                }
                else if (selectedlocation == 4) {
                    cbM32.SetEnabled(false);
                    cbM33.SetEnabled(false);
                    fn_OnControlDisableBox(cbM32);
                    fn_OnControlDisableBox(cbM33);

                    cbM31.SetEnabled(true);
                    cbM42.SetEnabled(true);
                    cbM43.SetEnabled(true);
                    cbM52.SetEnabled(true);
                    cbM53.SetEnabled(true);
                }
            }
        }

    </script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">

        <div>
            <table style="width: 70%;">
                <colgroup>
                    <col style="width: 33%" />
                    <col style="width: 33%" />
                    <col style="width: 33%" />
                </colgroup>
                <tr>
                    <td colspan="3">
                        <div class="search">
                            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">설비</label>
                                    <div class="col-sm-2 control-label">
                                        <dx:ASPxTextBox ID="hidLOCATION" ClientInstanceName="hidLOCATION" runat="server" Text="0" ClientVisible="false">
                                        </dx:ASPxTextBox>
                                        <dx:ASPxComboBox ID="cbLOCATION" ClientInstanceName="cbLOCATION" runat="server">
                                            <ClientSideEvents ValueChanged="fn_OnLocationChanged" />
                                            <Items>
                                                <dx:ListEditItem Text="선택하세요" Value="0" Selected="true" />
                                                <dx:ListEditItem Text="CASE1" Value="1" />
                                                <dx:ListEditItem Text="EOL1" Value="2" />
                                                <dx:ListEditItem Text="EOL2" Value="3" />
                                                <dx:ListEditItem Text="EOL3" Value="4" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>

        <dx:ASPxCallbackPanel ID="pnlChart" runat="server" ClientInstanceName="pnlChart" Enabled="true" Visible="true" Width="100%" OnCallback="cbM_Callback">
            <ClientSideEvents EndCallback="endcallback" />
            <PanelCollection>
                <dx:PanelContent>
                    <div>
                        <table style="width: 70%;">
                            <colgroup>
                                <col style="width: 33%" />
                                <col style="width: 33%" />
                                <col style="width: 33%" />
                            </colgroup>
                            <tr>
                                <td style="border: 1px solid #9F9F9F; background-color: white; height: 200px; padding: 50px;">
                                    <div class="form-group">
                                        <table>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">1번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM11" ClientInstanceName="cbM11" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;"></label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM12" ClientInstanceName="cbM12" runat="server" ClientVisible="false">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;"></label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM13" ClientInstanceName="cbM13" runat="server" ClientVisible="false">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td style="border: 1px solid #9F9F9F; background-color: white; height: 200px; padding: 50px;">
                                    <div class="form-group">
                                        <table>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">1번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM21" ClientInstanceName="cbM21" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;"></label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM22" ClientInstanceName="cbM22" runat="server" ClientVisible="false">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;"></label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM23" ClientInstanceName="cbM23" runat="server" ClientVisible="false">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td style="border: 1px solid #9F9F9F; background-color: white; height: 200px; padding: 50px;">
                                    <div class="form-group">
                                        <table>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">1번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM31" ClientInstanceName="cbM31" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">2번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM32" ClientInstanceName="cbM32" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">3번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM33" ClientInstanceName="cbM33" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #9F9F9F; background-color: white; height: 200px; padding: 50px;">
                                    <div class="form-group">
                                        <table>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">1번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM41" ClientInstanceName="cbM41" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">2번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM42" ClientInstanceName="cbM42" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">3번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM43" ClientInstanceName="cbM43" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td style="border: 1px solid #9F9F9F; background-color: white; height: 200px; padding: 50px;">
                                    <div class="form-group">
                                        <table>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">1번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM51" ClientInstanceName="cbM51" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">2번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM52" ClientInstanceName="cbM52" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">3번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM53" ClientInstanceName="cbM53" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td style="border: 1px solid #9F9F9F; background-color: white; height: 200px; padding: 50px;">
                                    <div class="form-group">
                                        <table>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;">1번 항목</label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM61" ClientInstanceName="cbM61" runat="server"
                                                        IncrementalFilteringMode="None" CssClass="NoXButton"
                                                        TextField="F_MEAINSPNM" ValueField="F_MEAINSPCD" OnCallback="cbM_Callback">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;"></label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM62" ClientInstanceName="cbM62" runat="server" ClientVisible="false">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin: 5px;">
                                                    <label class="control-label" style="margin: 5px;"></label>
                                                </td>
                                                <td style="margin: 5px;">
                                                    <dx:ASPxComboBox ID="cbM" ClientInstanceName="cbM63" runat="server" ClientVisible="false">
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                            <ClientSideEvents EndCallback="fn_OndevCallback" />
                        </dx:ASPxCallback>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>
</asp:Content>
