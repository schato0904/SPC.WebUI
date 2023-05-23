<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xtraReport2.ascx.cs" Inherits="SPC.WebUI.Resources.report.head.hkpi.xtraReport2" %>
<div class="table-responsive">
    <table class="table table-bordered b-t b-light">
        <tbody>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">P/NO</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtMODELNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">P/NAME</td>
                <td>
                    <dx:ASPxTextBox ID="txtMODELCD" ClientInstanceName="txtMODELCD" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">검사자</td>
                <td style="width:140px;">
                    <dx:ASPxTextBox ID="txtREVMANAGER" ClientInstanceName="txtREVMANAGER" runat="server" Width="100%" />
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">품번</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">품명</td>
                <td>
                    <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%" ReadOnly="true" Enabled="false" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">측정일자</td>
                <td style="width:140px;">
                    <dx:ASPxDateEdit ID="txtDATE" ClientInstanceName="txtDATE" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
                        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%">
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">업체명</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtCOMPNM" ClientInstanceName="txtCOMPNM" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">용도</td>
                <td colspan="3">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxCheckBoxList ID="chkUSAGE" ClientInstanceName="chkUSAGE" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Paddings-Padding="0px">
                                    <Border BorderStyle="None" />
                                    <Items>
                                        <dx:ListEditItem Text="ISIR" Value="0" />
                                        <dx:ListEditItem Text="정기검사" Value="1" />
                                        <dx:ListEditItem Text="일반검사" Value="2" Selected="true" />
                                        <dx:ListEditItem Text="기타" Value="3" />
                                    </Items>
                                </dx:ASPxCheckBoxList>
                            </td>
                            <td>(&nbsp;</td>
                            <td style="width:80px;"><dx:ASPxTextBox ID="txtUSAGE" ClientInstanceName="txtUSAGE" runat="server" Width="80px" /></td>
                            <td>&nbsp;)</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">LOT NO</td>
                <td>
                    <dx:ASPxTextBox ID="txtLOTNO2" ClientInstanceName="txtLOTNO2" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">입고일</td>
                <td style="width:140px;">
                    <dx:ASPxDateEdit ID="txtREVDT" ClientInstanceName="txtREVDT" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
                        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%">
                    </dx:ASPxDateEdit>
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">중량</td>
                <td style="width:140px;">
                    <dx:ASPxTextBox ID="txtREVDESC" ClientInstanceName="txtREVDESC" runat="server" Width="100%" />
                </td>
            </tr>
        </tbody>
    </table>
</div>