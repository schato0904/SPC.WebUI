<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.login" %>
<input type="hidden" id="hidCompNMKR" value="사이버테크프랜드" />
<input type="hidden" id="hidCompNMUS" value="CyberTechFriend" />
<input type="hidden" id="hidCompNMCN" value="CyberTechFriend" />    
<section id="content" class="m-t-lg wrapper-md animated fadeInUp">
    <div class="container aside-xl">
        <span name="compNM" class="navbar-brand block">사이버테크프랜드</span>
        <section class="m-b-lg">
            <header class="wrapper text-center">
                <strong id="productNM">Statistical Process Control</strong>
            </header>
            <div class="list-group">
                <div class="list-group-item">
                    <div style="float:right;" class="inline">
                        <asp:DropDownList ID="ddlLangClsCd" runat="server" CssClass="form-control input-sm" Width="85px">
                            <asp:ListItem Text="한국어" Value="ko-KR" />
                            <asp:ListItem Text="中國語" Value="zh-CN" />
                            <asp:ListItem Text="English" Value="en-US" />
                        </asp:DropDownList>
                    </div>
                    <div style="clear:both;"></div>
                </div>
                <div class="list-group-item">
                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control no-border" placeholder="id" />
                </div>
                <div class="list-group-item">
                    <asp:TextBox ID="txtUserPW" runat="server" CssClass="form-control no-border" placeholder="password" TextMode="Password" />
                </div>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Sign in" CssClass="btn btn-lg btn-primary btn-block" OnClientClick="return fnDoLogin();" />
            <div class="line line-dashed"></div>
            <p class="text-muted text-left"><asp:CheckBox id="chkSaveUserID" runat="server" style="vertical-align:middle;" />&nbsp;<label id="saveID" for="<%=chkSaveUserID.ClientID%>"></label></p>
        </section>
    </div>
</section>
<!-- footer -->
<footer id="footer">
    <div class="text-center padder">
        <p>
            <small>&copy; 2014 CYBERTECHFRIEND CO.,LTD. ALL RIGHTS RESERVED.</small>
        </p>
    </div>
</footer>
<!-- / footer -->