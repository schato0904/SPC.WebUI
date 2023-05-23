<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xtraReport1.ascx.cs" Inherits="SPC.WebUI.Resources.report.head.hkpi.xtraReport1" %>
<div class="table-responsive">
    <table class="table table-bordered b-t b-light">
        <tbody>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">차종</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtMODELNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">LOT NO</td>
                <td>
                    <dx:ASPxTextBox ID="txtLOTNO2" ClientInstanceName="txtLOTNO2" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">LOT SIZE</td>
                <td style="width:140px;">
                    <dx:ASPxTextBox ID="txtQUANTITY" ClientInstanceName="txtQUANTITY" runat="server" Width="100%" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">품번</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">품명</td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">작성일자</td>
                <td style="width:160px;">
                    <dx:ASPxDateEdit ID="txtDATE" ClientInstanceName="txtDATE" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
                        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%">
                    </dx:ASPxDateEdit>
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">작성자</td>
                <td>
                    <dx:ASPxTextBox ID="txtREVAPPROVER" ClientInstanceName="txtREVAPPROVER" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">최종E.0</td>
                <td style="width:140px;">
                    <dx:ASPxTextBox ID="txtEONO" ClientInstanceName="txtEONO" runat="server" Width="100%" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">개정일</td>
                <td style="width:160px;">
                    <dx:ASPxDateEdit ID="txtREVDT" ClientInstanceName="txtREVDT" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
                        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%">
                    </dx:ASPxDateEdit>
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">개정내역</td>
                <td>
                    <dx:ASPxTextBox ID="txtREVDESC" ClientInstanceName="txtREVDESC" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">작성자</td>
                <td style="width:140px;">
                    <dx:ASPxTextBox ID="txtREVMANAGER" ClientInstanceName="txtREVMANAGER" runat="server" Width="100%" />
                </td>
            </tr>
        </tbody>
    </table>
</div>