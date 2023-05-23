<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="report.ascx.cs" Inherits="SPC.WebUI.Resources.report.form.report" %>
<dx:ASPxDocumentViewer ID="devDocument" ClientInstanceName="devDocument" runat="server" Width="100%" Height="0px">
    <ClientSideEvents Init="fn_OnDevDocumentInit" />
</dx:ASPxDocumentViewer>