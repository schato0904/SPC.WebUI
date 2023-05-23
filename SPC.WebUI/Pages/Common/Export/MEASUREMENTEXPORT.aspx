<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcExportMasterPage.Master" AutoEventWireup="true" CodeBehind="MEASUREMENTEXPORT.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Export.MEASUREMENTEXPORT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxGridView ID="devGridForXls" ClientInstanceName="devGridForXls" runat="server" AutoGenerateColumns="false" Width="100%"
        KeyFieldName="F_WORKDATE;F_WORKTIME;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
        OnHtmlRowPrepared="devGridForXls_HtmlRowPrepared">
        <Styles>
            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
            <EditForm CssClass="bg-default"></EditForm>
        </Styles>
        <Columns>
            <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자" Width="100px" />
            <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="100px" />
            <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번" Width="50px" />
            <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값" Width="150px">
                <CellStyle HorizontalAlign="Right"></CellStyle>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="LOTNO" Width="150px" />
            <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자" Width="150px" />
            <dx:GridViewDataColumn FieldName="F_NGOKCHK" Caption="NGOKCHK" Visible="false" />
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGridForXls"></dx:ASPxGridViewExporter>
</asp:Content>
