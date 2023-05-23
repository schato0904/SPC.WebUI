<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInspectionBox.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucInspectionBox" %>
<script type="text/javascript">
    function fn_OnSetInspectionItem(resultValues) {
        // 구분
        hidUCINSPECTIONCD.SetValue(resultValues[2]);
        txtUCINSPECTIONNM.SetText(resultValues[3]);
        txtUCINSPECTIONNM.SetValue(resultValues[3]);
    }
</script>
<dx:ASPxTextBox ID="hidINSPECTIONCD" ClientInstanceName="hidUCINSPECTIONCD" runat="server" ClientVisible="false" />
<dx:ASPxTextBox ID="txtINSPECTIONNM" ClientInstanceName="txtUCINSPECTIONNM" runat="server" Width="100%">
    <ClientSideEvents Init="fn_OnControlDisableBox" />
</dx:ASPxTextBox>
