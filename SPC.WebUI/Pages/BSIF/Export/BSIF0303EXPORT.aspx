<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcExportMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0303EXPORT.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.Export.BSIF0303EXPORT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxGridView ID="devGridForXls" ClientInstanceName="devGridForXls" runat="server" AutoGenerateColumns="false" Width="0px"
        KeyFieldName="F_ITEMCD;F_INSPCD;F_SERIALNO" EnableViewState="false" EnableRowsCache="false" ViewStateMode="Disabled"
        OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
        <Styles>
            <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" Cursor="pointer"></Cell>
            <EditForm CssClass="bg-default"></EditForm>
        </Styles>
        <Columns>
            <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="140px"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px">
                <CellStyle HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="검사분류" Width="70px"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Width="70px"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="100px">
                <CellStyle HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Width="70px"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="100px">
                <CellStyle HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Width="70px"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="150px">
                <CellStyle HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_DISPLAYNO" Caption="검사순서" Width="70px"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="항목코드" Width="70px"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px">
                <CellStyle HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_UNIT" Caption="단위" Width="50px"></dx:GridViewDataColumn>
            <dx:GridViewBandColumn Name="F_INSPECTION_STANDARDS" Caption="검사기준">
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Width="60px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한값" Width="60px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한값" Width="60px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ZERO" Caption="Zero" Width="40px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ZIG" Caption="보정치" Width="70px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_DATAUNIT" Caption="구분" Width="60px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_QCYCLENM" Caption="QC검사주기" Width="90px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_JCYCLENM" Caption="현장검사주기" Width="90px"></dx:GridViewDataColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Name="F_INSPECTION_METHOD" Caption="검사방법">
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_SIRYO" Caption="시료수" Width="50px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RANKNM" Caption="수준" Width="40px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정기기" Width="130px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Name="F_QUALITY" Caption="품질목표">
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_UCLX" Caption="UCLX" Width="60px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LCLX" Caption="LCLX" Width="60px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_UCLR" Caption="UCLR" Width="60px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TMAX" Caption="TMAX" Width="60px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TMIN" Caption="TMIN" Width="60px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Name="F_TOOLBAR" Caption="측정기준">
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_PORT" Caption="측정포트" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CHANNEL" Caption="측정채널" Width="70px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_GETDATA" Caption="측정방법" Width="100px"></dx:GridViewDataColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataColumn FieldName="F_SERIALNO" Caption="일련번호" Width="70px"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="F_GETTYPE" Caption="수작업여부" Width="100px"></dx:GridViewDataColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGridForXls"></dx:ASPxGridViewExporter>
</asp:Content>
