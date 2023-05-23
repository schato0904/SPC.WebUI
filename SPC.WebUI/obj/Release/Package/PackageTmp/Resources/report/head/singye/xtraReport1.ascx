<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xtraReport1.ascx.cs" Inherits="SPC.WebUI.Resources.report.head.singye.xtraReport1" %>
<div class="table-responsive">
    <table class="table table-bordered b-t b-light">
        <tbody>
            <tr style="height:20px;">
                <td style="width:70px;text-align:right;font-weight:bold;">작성일자</td>
                <td style="width:160px;">
                    <dx:ASPxDateEdit ID="txtDATE" ClientInstanceName="txtDATE" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
                        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%">
                    </dx:ASPxDateEdit>
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">납품번호</td>
                <td>
                    <dx:ASPxTextBox ID="txtLOTNO2" ClientInstanceName="txtLOTNO2" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">납품수량</td>
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
                <td style="width:70px;text-align:right;font-weight:bold;">책임자</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtREVMANAGER" ClientInstanceName="txtREVMANAGER" runat="server" Width="100%" />
                </td>
                <td style="width:70px;text-align:right;font-weight:bold;">검사자</td>
                <td style="width:160px;">
                    <dx:ASPxTextBox ID="txtREVAPPROVER" ClientInstanceName="txtREVAPPROVER" runat="server" Width="100%" />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="width:70px;text-align:right;font-weight:bold;">구분</td>
                <td colspan="5">
                    <dx:ASPxCheckBoxList ID="chkINSPECTION" ClientInstanceName="chkINSPECTION" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Paddings-Padding="0px">
                        <Border BorderStyle="None" />
                        <Items>
                            <dx:ListEditItem Text="가공" Value="0" />
                            <dx:ListEditItem Text="도금" Value="1" />
                            <dx:ListEditItem Text="PRESS" Value="2" />
                            <dx:ListEditItem Text="사출" Value="3" />
                            <dx:ListEditItem Text="사급" Value="4" />
                        </Items>
                    </dx:ASPxCheckBoxList>
                </td>
            </tr>
        </tbody>
    </table>
</div>