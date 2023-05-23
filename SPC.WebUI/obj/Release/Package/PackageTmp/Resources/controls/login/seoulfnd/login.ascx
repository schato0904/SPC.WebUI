<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.seoulfnd.login" %>
<style type="text/css">
    .warp {
        width: 913px;
        height: 442px;
        margin: 0 auto;
        margin-top: 200px;
        background: url(Resources/controls/login/seoulfnd/Image/loginbg.jpg) repeat-x 0 0;
    }
    .box {
        width: 250px;
        position: relative;
        top: 178px;
        left: 465px;
    }

    .box table td{
        padding:0px 0px 0px 0px;
    }

    .footer {
        width: 625px;
        margin: 0 auto;
        display:none;
    }
</style>
<input type="hidden" id="hidCompNMKR" value="<%=LoginInfo.F_COMPNMKR%>" />
<input type="hidden" id="hidCompNMUS" value="<%=LoginInfo.F_COMPNMUS%>" />
<input type="hidden" id="hidCompNMCN" value="<%=LoginInfo.F_COMPNMCN%>" />
<div class="warp">
    <div class="box">
        <table border="0" style="border-spacing:0" >
            <tr>
                <td style="width:140px; ">
                    <asp:TextBox ID="txtUserID" runat="server" placeholder="ID" TabIndex="1" />
                </td>
                <td style="width:72px; text-align:right;" rowspan="2" >
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Resources/controls/login/seoulfnd/Image/butt_login.gif" OnClientClick="return fnDoLogin();" TabIndex="3" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtUserPW" runat="server"  placeholder="Password" TextMode="Password" TabIndex="2" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; vertical-align:top">
                    <asp:CheckBox ID="chkSaveUserID" runat="server" Style="vertical-align: middle;" Text="&nbsp;ID저장" />&nbsp;</td>
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