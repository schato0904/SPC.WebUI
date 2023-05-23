<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNgCauseRow.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucNgCauseRow" %>
<table style="width:100%;border-collapse:collapse;border:1px solid darkgrey;height:30px;background-color:#c9c9c9;margin-top:-1px;">
    <tr style="height:100%;">
        <td style="width:60%;border:1px solid darkgrey;text-align:center;height:100%;">
            <dx:ASPxTextBox ID="txtF_NGCAUSENM" runat="server" Width="100%" HorizontalAlign="Center" ClientSideEvents-Init="fn_OnControlDisable" Border-BorderStyle="None" Height="100%"></dx:ASPxTextBox>
            <dx:ASPxTextBox ID="txtF_NGCAUSECD" runat="server" ClientVisible="false"></dx:ASPxTextBox>
        </td>
        <td style="width:40%;border:1px solid darkgrey;text-align:center;height:100%;">
            <dx:ASPxSpinEdit ID="txtF_CNT" runat="server" Width="100%" HorizontalAlign="Center" Height="100%" OnInit="txtF_CNT_Init" Border-BorderStyle="None" AllowNull="false" MinValue="0" MaxValue="99999999">
                <ClientSideEvents GotFocus="function(s,e){s.SelectAll();}" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
</table>