<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.singye.login" %>
<style type="text/css">
    .warp {
        width: 766px;
        height: 473px;
        margin: 0 auto;
        margin-top: 200px;
        background: url(Resources/controls/login/singye/images/login_bg.jpg) repeat-x 0 0;
    }
    .box {
        width: 250px;
        position: relative;
        top: 350px;
        left: 440px;
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
                <td style="width:38px" class="icon" ></td>
                <td style="width:140px">
                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control"  TabIndex="1" Height="21px" /></td>
                <td style="width:72px; padding-left:5px;" rowspan="2" >
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="images/butt_login.png" OnClientClick="return fnDoLogin();" TabIndex="3" /></td>
            </tr>
            <tr>
                <td class="icon"></td>
                <td>
                    <asp:TextBox ID="txtUserPW" runat="server" CssClass="form-control "  TextMode="Password" TabIndex="2" Height="21px"/></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; ">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkSaveUserID" runat="server" Style="vertical-align: middle;" /><label id="saveID" for="<%=chkSaveUserID.ClientID%>"></label></td>
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