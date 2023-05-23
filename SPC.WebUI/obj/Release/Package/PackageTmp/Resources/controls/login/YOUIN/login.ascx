<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.YOUIN.login" %>
<style type="text/css">
    .warp {
        width: 720px;
        height: 370px;
        margin: 0 auto;
        margin-top: 300px;
        background: url(Resources/controls/login/YOUIN/Image/login_bg.jpg) repeat-x 0 0;

    }
    .box {
        width: 285px;
        position: relative;
        top: 223px;
        left: 99px;
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
       
    }
    .auto-style2 {
        width: 117px;
        padding-right: 10px;
    }
    .auto-style3 {
        width: 61px;
    }
</style>
<input type="hidden" id="hidCompNMKR" value="<%=LoginInfo.F_COMPNMKR%>" />
<input type="hidden" id="hidCompNMUS" value="<%=LoginInfo.F_COMPNMUS%>" />
<input type="hidden" id="hidCompNMCN" value="<%=LoginInfo.F_COMPNMCN%>" />
<div class="warp">
    <div class="box">
        <table border="0" style="border-spacing:0" >
            <tr>
                <td class="auto-style2">
                    <asp:TextBox ID="txtUserID" runat="server" placeholder="ID" TabIndex="1" Width="130px" />
                </td>
                <td style="text-align:right;" rowspan="2" class="auto-style3" >
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Resources/controls/login/YOUIN/Image/butt.jpg" OnClientClick="return fnDoLogin();" TabIndex="3" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:TextBox ID="txtUserPW" runat="server"  placeholder="Password" TextMode="Password" TabIndex="2" Width="130px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; vertical-align:top; color:white;">
                    <asp:CheckBox ID="chkSaveUserID" runat="server" Style="vertical-align: middle; "/>아이디 저장</td>
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