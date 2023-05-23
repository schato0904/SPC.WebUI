<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucModel1.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucModel1" %>
<dx:ASPxTextBox ID="hidMODELCD1" ClientInstanceName="hidUCMODELCD1" runat="server" ClientVisible="false" />
<dx:ASPxTextBox ID="txtMODELNM1" ClientInstanceName="txtUCMODELNM1" runat="server" Width="100%">
    <ClientSideEvents Init="fn_OnControlDisableBox" />
</dx:ASPxTextBox>
