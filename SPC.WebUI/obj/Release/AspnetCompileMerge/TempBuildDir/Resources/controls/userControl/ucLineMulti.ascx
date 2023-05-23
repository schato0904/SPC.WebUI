<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLineMulti.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucLineMulti" %>
<dx:ASPxTextBox ID="hidLINECD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlLINE" runat="server" Width="100%" 
    IncrementalFilteringMode="None" CssClass="NoXButton" DropDownStyle="DropDownList"
    TextField="F_LINENM" ValueField="F_LINECD" OnDataBound="ddlLINE_DataBound" OnCallback="ddlLINE_Callback">
    <ClientSideEvents Init="fn_OnControlDisable"/>
</dx:ASPxComboBox>
<script type="text/javascript">
    $(document).ready(function () { if (!isPopup) parent.parent.isLineUserControl[parent.fn_GetIFrameID()] = true; });
    (function () {
        CTF.loadConstructor("CTF.UcLineMulti"
            , function (constructor) { // 생성자 로드시 콜백 함수
                //function (clientInstanceName, strHidLINECD, strTargetCtrls, strDdlLINE ) {
                var clientInstanceName = '<%= ClientInstanceName %>';
                var strHidLINECD = '<%= hidLINECD.ClientInstanceName %>';
                var strTargetCtrls = '<%= targetCtrls %>';
                var strDdlLINE = '<%= ddlLINE.ClientInstanceName %>';
                var jsonParameter = '<%= this.JsonParameter %>';
                var instance = new constructor(clientInstanceName, strHidLINECD, strTargetCtrls, strDdlLINE, jsonParameter);
                window[clientInstanceName] = instance;
            }
            , function (errmsg) { // 오류 처리 함수
                alert(errmsg);
            });
    })();
</script>