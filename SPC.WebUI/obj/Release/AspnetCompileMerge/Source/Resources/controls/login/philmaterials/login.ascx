<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.philmaterials.login" %>
<style type="text/css">
    .warp {
        width: 950px;
        height: 450px;
        margin-top: 200px;
        background: url(Resources/controls/login/philmaterials/Image/bg.png) repeat-x 0 0;
        margin-left: auto;
        margin-right: auto;
        margin-bottom: 0;
    }

    .box {
        width: 300px;
        position: relative;
        top: 300px;
        left: 40px;
        float: left;
    }


        .box table td {
            padding: 0px 0px 0px 0px;
        }

    .footer {
        width: 625px;
        margin: 0 auto;
        display: none;
    }

    .auto-style {
    }

    .box table span{
        font-size: 15px;
        color: black;
    }

</style>
<input type="hidden" id="hidCompNMKR" value="<%=LoginInfo.F_COMPNMKR%>" />
<input type="hidden" id="hidCompNMUS" value="<%=LoginInfo.F_COMPNMUS%>" />
<input type="hidden" id="hidCompNMCN" value="<%=LoginInfo.F_COMPNMCN%>" />
<div class="warp">
    <div class="box">
        <div style="width: 64%; float: left;">
            <table border="0">
                <tr>
                    <td>
                        <span>ID</span>
                    </td>
                    <td class="auto-style" colspan="2">
                        <asp:TextBox ID="txtUserID" runat="server" placeholder="ID" TabIndex="1" Width="180px" Height="25" />
                    </td>
                </tr>
                <tr style="height: 5px;">
                </tr>
                <tr>
                    <td>
                        <span>PW </span>
                    </td>
                    <td class="auto-style" colspan="2">
                        <asp:TextBox ID="txtUserPW" runat="server" placeholder="Password" TextMode="Password" TabIndex="2" Width="180px" Height="25" />
                    </td>
                </tr>
                <tr style="height: 10px;">
                </tr>
                <tr>
                    <td style="text-align: left; vertical-align: top" colspan="2">
                        <asp:CheckBox ID="chkSaveUserID" runat="server" Style="vertical-align: middle;" /><span style="font-size: 10px"> 아이디저장</span>
                    </td>

                </tr>
            </table>
        </div>
        <div style="width: 34%; float: right; height: 75px;">
            <table border="0">
                <tr>
                    <td style="width: 72px; text-align: right;">
                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Resources/controls/login/philmaterials/Image/butt_login.png" OnClientClick="return fnDoLogin();" TabIndex="3" Style="margin-left: 6px" Height="60px" />
                    </td>
                </tr>
            </table>
        </div>
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
