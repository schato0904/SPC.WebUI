<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPagerMulti.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucPagerMulti" %>
<script type="text/javascript">
    $(document).ready(function () {
        var ID = '<%=this.ID%>';
        var targetCtrls = '<%= this.targetCtrls %>';
        var hidCurrPage = '<%= hidCurrPage.UniqueID %>';
        var ddlPageSize = '<%= ddlPageSize.UniqueID %>';
        var pagerPanel = '<%=pagerPanel.UniqueID %>';
        if (ID != '') { // && typeof(window[ID]) == 'undefined') {
            window[ID] = new (function (ID) {
                // 멤버변수 세팅
                this.ID = ID;
                this.targetCtrls = targetCtrls;
                this.hidCurrPage = ASPxClientTextBox.Cast(hidCurrPage);
                this.ddlPageSize = ASPxClientComboBox.Cast(ddlPageSize);
                this.pagerPanel = ASPxClientCallbackPanel.Cast(pagerPanel);
                // 필드명 세팅

                // 멤버함수 세팅
                // PerFormCallbak Event Target Control
                this.ItemPerformCallbackTarget = function (CURRPAGE, PAGESIZE) {
                    var oParams = 'PAGESIZE=' + PAGESIZE + ';CURRPAGE=' + CURRPAGE;

                    if ('' != this.targetCtrls) {
                        var Ctrl = ASPxClientControl.Cast(this.targetCtrls);
                        Ctrl.PerformCallback(oParams);
                    }
                }

                this.fn_gotoPage = function (currPage) {
                    this.hidCurrPage.SetValue(currPage);

                    this.ItemPerformCallbackTarget(currPage, this.ddlPageSize.GetValue());
                }

                this.fn_PageSizeSelectedIndexChanged = function (s, e) {
                    this.ItemPerformCallbackTarget("1", s.GetValue());
                }

                this.fn_pagerPerformCallback = function (oParams) {
                    this.pagerPanel.PerformCallback(oParams);
                }
            })(ID);
        }
    });
</script>
<div class="form-horizontal" style="padding-top: 10px;">
    <div class="form-group">
        <dx:ASPxCallbackPanel ID="pagerPanel" runat="server" CssClass="col-sm-11"
            OnCallback="pagerPanel_Callback">
            <PanelCollection>
                <dx:PanelContent>
                    <div class="form-group">
                        <div class="col-sm-2" >
                            <dx:ASPxLabel ID="lblPageInfo" runat="server" />
                        </div>
                        <div class="col-sm-10" >
                            <div id="divPageNavigation" runat="server" style="text-align: center;"></div>
                        </div>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        <dx:ASPxTextBox ID="hidCurrPage" runat="server" ClientVisible="false" />
        <div class="col-sm-1">
            <div style="float: right;">
                <dx:ASPxComboBox ID="ddlPageSize" runat="server" Width="60px"
                    IncrementalFilteringMode="None" CssClass="NoXButton" ToolTip="한 페이지에 출력할 갯수" Caption="갯수">
                    <CaptionStyle Font-Bold="true" />
                    <Items>
                        <dx:ListEditItem Text="50" Value="50" />
                        <dx:ListEditItem Text="100" Value="100" />
                        <dx:ListEditItem Text="150" Value="150" />
                        <dx:ListEditItem Text="200" Value="200" />
                    </Items>
                </dx:ASPxComboBox>
            </div>
        </div>
    </div>
</div>