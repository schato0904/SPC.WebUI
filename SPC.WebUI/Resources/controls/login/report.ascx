<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="report.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.report" %>
<dx:ASPxDocumentViewer ID="devDocument" ClientInstanceName="devDocument" runat="server" Width="100%" Height="0px"
    ReportTypeName="SPC.WebUI.Pages.Common.Report.InspectionReport">
    <ClientSideEvents Init="fn_OnDevDocumentInit" />
</dx:ASPxDocumentViewer>