<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.ctf.login" %>
<style type="text/css">
    .warp {
        width: 652px;
        height: 357px;
        margin: 0 auto;
        margin-top: 200px;
        background: url(Resources/controls/login/ctf/images/login_bg.jpg) repeat-x 0 0;
    }
    .box {
        width: 250px;
        position: relative;
        top: 190px;
        left: 85px;
    }

    .box table td{
        padding:0px;
    }

    .footer {
        width: 625px;
        margin: 0 auto;
    }
</style>
<input type="hidden" id="hidCompNMKR" value="<%=LoginInfo.F_COMPNMKR%>" />
<input type="hidden" id="hidCompNMUS" value="<%=LoginInfo.F_COMPNMUS%>" />
<input type="hidden" id="hidCompNMCN" value="<%=LoginInfo.F_COMPNMCN%>" />
<div class="warp">
    <div class="box">
        <table border="0" style="border-spacing:0" >
            <tr>
                <td style="width:38px" class="icon" >ID</td>
                <td style="width:140px">
                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control no-border" placeholder="id" TabIndex="1" /></td>
                <td style="width:72px" rowspan="2" >
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="images/login_butt.gif" OnClientClick="return fnDoLogin();" TabIndex="3" /></td>
            </tr>
            <tr>
                <td class="icon">PW</td>
                <td>
                    <asp:TextBox ID="txtUserPW" runat="server" CssClass="form-control no-border" placeholder="password" TextMode="Password" TabIndex="2" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right;">
                    <asp:CheckBox ID="chkSaveUserID" runat="server" Style="vertical-align: middle;" />&nbsp;<label id="saveID" for="<%=chkSaveUserID.ClientID%>"></label></td>
                <td></td>
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