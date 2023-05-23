<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucModel.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucModel" %>
<dx:ASPxTextBox ID="hidMODELCD" ClientInstanceName="hidUCMODELCD" runat="server" ClientVisible="false" />
<dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtUCMODELNM" runat="server" Width="100%">
    <ClientSideEvents Init="fn_OnControlDisableBox" />
</dx:ASPxTextBox>
