<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBanMulti.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucBanMulti" %>
<dx:ASPxTextBox ID="hidBANCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlBAN" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_BANNM" ValueField="F_BANCD" OnDataBound="ddlBAN_DataBound" OnCallback="ddlBAN_Callback">
    <ClientSideEvents Init="fn_OnControlDisable"/>
</dx:ASPxComboBox>
<script type="text/javascript">
    $(document).ready(function () { <%if (false==bAutoFillByTree) {%>if (!isPopup) parent.parent.isBanUserControl[parent.fn_GetIFrameID()] = true;<%}%> });
    (function () {
        CTF.loadConstructor("CTF.UcBanMulti"
            , function (constructor) { // 생성자 로드시 콜백 함수
                //function (clientInstanceName, strHidBANCD, strTargetCtrls, strDdlBAN ) {
                var clientInstanceName = '<%= ClientInstanceName %>';
                var strHidBANCD = '<%= hidBANCD.ClientInstanceName %>';
                var strTargetCtrls = '<%= targetCtrls %>';
                var strDdlBAN = '<%= ddlBAN.ClientInstanceName %>';
                var jsonParameter = '<%= this.JsonParameter %>';
                var instance = new constructor(clientInstanceName, strHidBANCD, strTargetCtrls, strDdlBAN, jsonParameter);
                window[clientInstanceName] = instance;
            }
            , function (errmsg) { // 오류 처리 함수
                alert(errmsg);
            });
    })();
</script>