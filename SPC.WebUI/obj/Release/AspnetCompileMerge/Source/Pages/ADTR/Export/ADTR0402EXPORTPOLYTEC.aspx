<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcExportMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0402EXPORTPOLYTEC.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.Export.ADTR0402EXPORTPOLYTEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxGridView ID="devGridForXls" ClientInstanceName="devGridForXls" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;F_WORKTIME;F_WORKCD;F_ITEMCD;F_SERIALNO;F_NUMBER;F_MEASNO" EnableViewState="false" EnableRowsCache="false">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="200px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO"    Caption="시료군"  Width="70px"  />
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번"  Width="50px"  />
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="60px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NGOKCHKTEXT" Caption="판정"  Width="50px"  />
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No."  Width="100px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자"  Width="60px"  />
                    <dx:GridViewDataColumn FieldName="F_PCNM" Caption="PC명"   Width="100px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MOLDNO" Caption="작지번호"   Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TAGAKNO" Caption="제품QR"   Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
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
                        </Columns>
                    </dx:GridViewBandColumn>
                        
                    <%--여기서부터 히든 필드--%>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEASNO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_NGOKCHK" Visible="false" />
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGridForXls"></dx:ASPxGridViewExporter>
</asp:Content>
