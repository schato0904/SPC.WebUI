<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="report.ascx.cs" Inherits="SPC.WebUI.Resources.report.head.report" %>
<div class="table-responsive">
    <table class="table table-bordered b-t b-light">
        <tbody>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">기종</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtMODELNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">품명</td>
                <td>
                    <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">공정명</td>
                <td style="width:140px;">
                    <dx:ASPxTextBox ID="txtUSAGE" ClientInstanceName="txtUSAGE" runat="server" Width="100%" Text="수입검사" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">품번</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">EO NO</td>
                <td>
                    <dx:ASPxTextBox ID="txtEONO" ClientInstanceName="txtEONO" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">입고수량</td>
                <td style="width:140px;">
                    <dx:ASPxTextBox ID="txtQUANTITY" ClientInstanceName="txtQUANTITY" runat="server" Width="100%" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">업체명</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtCOMPNM" ClientInstanceName="txtCOMPNM" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">LOT NO</td>
                <td>
                    <dx:ASPxTextBox ID="txtLOTNO2" ClientInstanceName="txtLOTNO2" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">입고일자</td>
                <td style="width:140px;">
                    <dx:ASPxDateEdit ID="txtDATE" ClientInstanceName="txtDATE" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
                        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%">
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">서식명</td>
                <td colspan="5">
                    <dx:ASPxTextBox ID="txtFORMAT" ClientInstanceName="txtFORMAT" runat="server" Width="100%" />
                </td>
            </tr>
        </tbody>
    </table>
</div>