<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="403.aspx.cs" Inherits="SPC.WebUI.Pages.ERROR._403" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="~/Resources/jquery/jquery-ui-1.10.4.custom.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/icon.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/app.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/unresponsive.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/spc.css" />
    <!--[if lt IE 9]>
    <script src="<%#Page.ResolveUrl("~/Resources/scripts/ie/html5shiv.js")%>"></script>
    <script src="<%#Page.ResolveUrl("~/Resources/scripts/ie/respond.min.js")%>"></script>
    <script src="<%#Page.ResolveUrl("~/Resources/scripts/ie/excanvas.js")%>"></script>
    <![endif]-->
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/jquery/jquery-1.10.2.min.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/jquery/jquery-ui-1.10.4.custom.min.js")%>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <section id="content">
            <div class="row m-n">
                <div class="col-sm-4 col-sm-offset-4">
                    <div class="text-center m-b-lg">
                        <h1 class="h text-white animated fadeInDownBig">403</h1>
                    </div>
                    <div class="list-group bg-info auto m-b-sm m-b-lg font-bold padder padder-v">
                        현재 페이지의 사용권한이 없습니다<br />
                        사용을 위해서는 시스템관리자에게 사용권한을 신청하세요
                    </div>
                </div>
            </div>
        </section>
        <!-- footer -->
        <footer id="footer">
            <div class="text-center padder clearfix">
                <p>
                    <small></small>
                </p>
            </div>
        </footer>
    </form>
</body>
</html>
