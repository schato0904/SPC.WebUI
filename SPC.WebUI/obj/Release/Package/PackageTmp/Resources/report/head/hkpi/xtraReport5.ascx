<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xtraReport5.ascx.cs" Inherits="SPC.WebUI.Resources.report.head.hkpi.xtraReport5" %>
<div class="table-responsive">
    <table class="table table-bordered b-t b-light">
        <tbody>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">CAR</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtMODELNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:80px;text-align:right;font-weight:bold;">Supplier</td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtCOMPNM" ClientInstanceName="txtCOMPNM" runat="server" Width="100%" NullText="인쇄 시 출력할 회사명" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">Part No</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:110px;text-align:right;font-weight:bold;">Part Name</td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">Incoming<br />Date</td>
                <td style="width:160px;">
                    <dx:ASPxDateEdit ID="txtDATE" ClientInstanceName="txtDATE" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
                        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%">
                    </dx:ASPxDateEdit>
                </td>
                <td style="width:80px;text-align:right;font-weight:bold;">Incoming<br />Place</td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtPLACE" ClientInstanceName="txtPLACE" runat="server" Width="100%" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">Po No</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtPONO" ClientInstanceName="txtPONO" runat="server" Width="100%" />
                </td>
                <td style="width:80px;text-align:right;font-weight:bold;">Lot No</td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtLOTNO2" ClientInstanceName="txtLOTNO2" runat="server" Width="100%" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">Incoming<br />quantity</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtQUANTITY" ClientInstanceName="txtQUANTITY" runat="server" Width="100%" />
                </td>
                <td style="width:80px;text-align:right;font-weight:bold;">Incoming<br />Type</td>
                <td colspan="3">
                    <dx:ASPxCheckBoxList ID="chkTYPE" ClientInstanceName="chkTYPE" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Paddings-Padding="0px">
                        <Border BorderStyle="None" />
                        <Items>
                            <dx:ListEditItem Text="PALLET" Value="0" />
                            <dx:ListEditItem Text="PP BOX" Value="1" />
                            <dx:ListEditItem Text="CARTOON" Value="2" />
                        </Items>
                    </dx:ASPxCheckBoxList>
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