<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucPager" %>
<script type="text/javascript">
    // PerFormCallbak Event Target Control
    function ItemPerformCallbackTarget(CURRPAGE, PAGESIZE) {
        var oParams = 'PAGESIZE=' + PAGESIZE + ';CURRPAGE=' + CURRPAGE;
        
        if ('' != '<%=targetCtrls%>') {
            fn_doSetGridEventAction('false');
            var Ctrl = ASPxClientControl.Cast('<%=targetCtrls%>');
            Ctrl.PerformCallback(oParams);
        }
    }

    function fn_gotoPage(currPage) {
        fn_doSetGridEventAction('false');

        fn_SetTextValue('hidCurrPage', currPage);

        ItemPerformCallbackTarget(currPage, ddlPageSize.GetValue());
    }

    function fn_PageSizeSelectedIndexChanged(s, e) {
        fn_SetTextValue('hidPageSize', s.GetValue());
        ItemPerformCallbackTarget("1", s.GetValue());
    }

    function fn_pagerPerformCallback(oParams) {
        pagerPanel.PerformCallback(oParams);
    }
</script>
<div class="form-horizontal" style="padding-top: 10px;">
    <div class="form-group">
        <dx:ASPxCallbackPanel ID="pagerPanel" ClientInstanceName="pagerPanel" runat="server" CssClass="col-sm-11"
            OnCallback="pagerPanel_Callback">
            <PanelCollection>
                <dx:PanelContent>
                    <div class="form-group">
                        <div class="col-sm-3">
                            <dx:ASPxLabel ID="lblPageInfo" runat="server" />
                        </div>
                        <div class="col-sm-9">
                            <div id="divPageNavigation" runat="server" style="text-align: center;"></div>
                        </div>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        <dx:ASPxTextBox ID="hidCurrPage" ClientInstanceName="hidCurrPage" runat="server" Text="0" ClientVisible="false" />
        <dx:ASPxTextBox ID="hidPageSize" ClientInstanceName="hidPageSize" runat="server" ClientVisible="false" />
        <div class="col-sm-1">
            <div style="float: right;">
                <dx:ASPxComboBox ID="ddlPageSize" ClientInstanceName="ddlPageSize" runat="server" Width="60px"
                    IncrementalFilteringMode="None" CssClass="NoXButton" ToolTip="한 페이지에 출력할 갯수" Caption="갯수">
                    <CaptionStyle Font-Bold="true" />
                    <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_PageSizeSelectedIndexChanged" />
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