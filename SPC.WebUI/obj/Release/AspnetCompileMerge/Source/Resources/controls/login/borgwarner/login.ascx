<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.borgwarner.login" %>
<style type="text/css">
    .warp {
        width: 913px;
        height: 442px;
        margin-top: 200px;
        background: url(Resources/controls/login/borgwarner/Image/login_CS_bg.png) repeat-x 0 0;
        margin-left: auto;
        margin-right: auto;
        margin-bottom: 0;
    }
    .box {
        width: 300px;
        position: relative;
        top: 156px;
        left: 710px;
    }

    .box table td{
        padding:0px 0px 0px 0px;
    }

    .footer {
        width: 625px;
        margin: 0 auto;
        display:none;
    }
    
    .auto-style {
    }

    .box table span{
        font-size:15px;
        color:white;
    }
</style>
<input type="hidden" id="hidCompNMKR" value="<%=LoginInfo.F_COMPNMKR%>" />
<input type="hidden" id="hidCompNMUS" value="<%=LoginInfo.F_COMPNMUS%>" />
<input type="hidden" id="hidCompNMCN" value="<%=LoginInfo.F_COMPNMCN%>" />
<div class="warp" >
    <div class="box" >
        <table border="0" style="border-spacing:0" >
            <tr>
                <td>
                    <span>아이디</span>
                </td>
            </tr>
            <tr>
                <td class="auto-style" colspan="2">
                    <asp:TextBox ID="txtUserID" runat="server" placeholder="ID" TabIndex="1" Width="180px" Height="35" />
                </td>                
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <span>비밀번호</span>
                </td>
            </tr>
            <tr>                
                <td class="auto-style"  colspan="2">
                    <asp:TextBox ID="txtUserPW" runat="server"  placeholder="Password" TextMode="Password" TabIndex="2" Width="180px" Height="35" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align:top">
                    <asp:CheckBox ID="chkSaveUserID" runat="server" Style="vertical-align: middle;" /><span style="font-size:10px"> 아이디저장</span>
                </td>
                <td style="width:72px; text-align:right;">
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Resources/controls/login/borgwarner/Image/butt_login.png" OnClientClick="return fnDoLogin();" TabIndex="3" style="margin-left: 6px" />
                </td>
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