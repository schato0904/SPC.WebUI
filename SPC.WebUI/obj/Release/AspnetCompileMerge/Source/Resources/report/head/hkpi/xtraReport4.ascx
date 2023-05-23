<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xtraReport4.ascx.cs" Inherits="SPC.WebUI.Resources.report.head.hkpi.xtraReport4" %>
<div class="table-responsive">
    <table class="table table-bordered b-t b-light">
        <tbody>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">CAR</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtMODELNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:110px;text-align:right;font-weight:bold;">Inspection</td>
                <td colspan="3">
                    <dx:ASPxCheckBoxList ID="chkINSPECTION" ClientInstanceName="chkINSPECTION" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Paddings-Padding="0px">
                        <Border BorderStyle="None" />
                        <Items>
                            <dx:ListEditItem Text="APPERANCE" Value="0" Selected="true" />
                            <dx:ListEditItem Text="DIM'S" Value="1" Selected="true" />
                            <dx:ListEditItem Text="MATERIAL" Value="2" />
                            <dx:ListEditItem Text="PERPORMANCE" Value="3" />
                        </Items>
                    </dx:ASPxCheckBoxList>
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">PART NO</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:110px;text-align:right;font-weight:bold;">PART NAME</td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">DATE</td>
                <td style="width:160px;">
                    <dx:ASPxDateEdit ID="txtDATE" ClientInstanceName="txtDATE" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
                        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%">
                    </dx:ASPxDateEdit>
                </td>
                <td style="width:110px;text-align:right;font-weight:bold;">USE</td>
                <td colspan="3">
                    <dx:ASPxCheckBoxList ID="chkUSAGE" ClientInstanceName="chkUSAGE" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Paddings-Padding="0px">
                        <Border BorderStyle="None" />
                        <Items>
                            <dx:ListEditItem Text="ISIR" Value="0" />
                            <dx:ListEditItem Text="Regular Inspection" Value="1" />
                            <dx:ListEditItem Text="Ordinary" Value="2" Selected="true" />
                            <dx:ListEditItem Text="Other" Value="3" />
                        </Items>
                    </dx:ASPxCheckBoxList>
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:80px;text-align:right;font-weight:bold;">EO NO</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtEONO" ClientInstanceName="txtEONO" runat="server" Width="100%" />
                </td>
                <td style="width:110px;text-align:right;font-weight:bold;">차수</td>
                <td>
                    <dx:ASPxTextBox ID="txtORDER" ClientInstanceName="txtORDER" runat="server" Width="100%" />
                </td>
                <td style="width:110px;text-align:right;font-weight:bold;">수량</td>
                <td style="width:110px;">
                    <dx:ASPxTextBox ID="txtQUANTITY" ClientInstanceName="txtQUANTITY" runat="server" Width="100%" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">서식명</td>
                <td colspan="5">
                    <dx:ASPxTextBox ID="txtCOMPNM" ClientInstanceName="txtCOMPNM" runat="server" Width="100%" NullText="인쇄 시 출력할 회사명" />
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