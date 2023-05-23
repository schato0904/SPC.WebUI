<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.chunileng.login" %>
<style type="text/css">
    .warp {
        width: 724px;
        height: 393px;
        margin-top: 200px;
        background: url(Resources/controls/login/chunileng/Image/bg.jpg) repeat-x 0 0;
        margin-left: auto;
        margin-right: auto;
        margin-bottom: 0;
    }
    .box {
        width: 250px;
        position: relative;
        top: 216px;
        left: 89px;
    }

    .box table td{
        padding:0px 0px 0px 0px;
    }

    .footer {
        width: 625px;
        margin: 0 auto;
        display:none;
    }
    .auto-style1 {
        width: 268435456px;
    }
    .auto-style2 {
        width: 138px;
    }
    .auto-style3 {
        width: 138px;
        height: 23px;
    }
</style>
<input type="hidden" id="hidCompNMKR" value="<%=LoginInfo.F_COMPNMKR%>" />
<input type="hidden" id="hidCompNMUS" value="<%=LoginInfo.F_COMPNMUS%>" />
<input type="hidden" id="hidCompNMCN" value="<%=LoginInfo.F_COMPNMCN%>" />
<div class="warp" >
    <div class="box" >
        <table border="0" style="border-spacing:0" >
            <tr>
                <td class="auto-style2">
                    <asp:TextBox ID="txtUserID" runat="server" placeholder="ID" TabIndex="1" Width="130px" />
                </td>
                <td style="width:72px; text-align:right;" rowspan="2" >
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Resources/controls/login/chunileng/Image/butt.gif" OnClientClick="return fnDoLogin();" TabIndex="3" style="margin-left: 6px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:TextBox ID="txtUserPW" runat="server"  placeholder="Password" TextMode="Password" TabIndex="2" Width="130px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; vertical-align:top">
                    <asp:CheckBox ID="chkSaveUserID" runat="server" Style="vertical-align: middle;" />아이디저장</td>
                <td class="auto-style1"></td>
            </tr>
        </table>
    </div>
</div>
<!-- footer -->
<div class="footer">
    <div class="text-center padder">
        <p>
            <small><%=LoginInfo.F_COMPCOPY%></small>
        </p>
    </div>
</div>
<!-- / footer -->