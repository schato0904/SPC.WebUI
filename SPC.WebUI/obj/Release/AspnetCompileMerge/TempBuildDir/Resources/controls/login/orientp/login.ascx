<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.orientp.login" %>
<style type="text/css">
    .warp {
        width: 758px;
        height: 400px;
        background: url(Resources/controls/login/orientp/Image/loginbg.jpg) repeat-x 0 0;
    }
    .box {
        width: 250px;
        position: relative;
        top: 282px;
        left: 500px;
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
        <table border="0" style="border-spacing:0;padding:0px;">
            <tr>
                <td style="width:130px; ">
                    <asp:TextBox ID="txtUserID" runat="server" placeholder="ID" TabIndex="1" Height="20px" />
                </td>
                <td style="width:65px; text-align:right;" rowspan="2" >
                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Resources/controls/login/orientp/Image/butt_login.gif" OnClientClick="return fnDoLogin();" TabIndex="3" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtUserPW" runat="server"  placeholder="Password" TextMode="Password" TabIndex="2" Height="20px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; vertical-align:top; color:white;">
                    <asp:CheckBox ID="chkSaveUserID" runat="server" Style="vertical-align: middle;" Text="아이디저장" TextAlign="Left" />&nbsp;</td>
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
<script type="text/javascript">
    $(function () {
        $('.warp').css({
            'position': 'absolute',
            'left': '50%',
            'top': '50%',
            'margin-left': -$('.warp').outerWidth() / 2,
            'margin-top': -$('.warp').outerHeight() / 2
        });
    });
    $('.footer').css({
        'position': 'absolute',
        'left': '50%',
        'top': '50%',
        'margin-left': -$('.footer').outerWidth() / 2,
        'margin-top': (-$('.footer').outerHeight() + $('.warp').height()) / 2
    });
</script>