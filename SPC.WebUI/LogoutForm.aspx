<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogoutForm.aspx.cs" Inherits="SPC.WebUI.LogoutForm" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Resources/jquery/jquery-1.10.2.min.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            parent.location.href = 'LoginForm.aspx';
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
