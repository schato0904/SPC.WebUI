<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="SPC.WebUI.Pages.ERROR.Report" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/message/Message.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/SPCCommon.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/DevExpress.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/SPCPopup.js")%>"></script>
    <style type="text/css">
        html, body {
            width: 830px;
            height: 100%;
            padding: 0px;
            margin: 0px;
        }

        #content {
            width: 830px;
            height: 100%;
            bottom: 0;
            padding: 0px 5px 0px 5px;
            overflow: hidden;
        }
    </style>
    <script type="text/javascript">
        function fn_Close() {
            parent.fn_devPopupClose();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <section id="content">
            <div class="row m-n">
                <div class="col-sm-12">
                    <div class="text-center m-b-lg">
                        <h1 class="h text-white animated fadeInDownBig">Missing</h1>
                    </div>
                    <div class="list-group bg-info auto m-b-sm m-b-lg font-bold padder padder-v">
                        <a href="#" class="list-group-item">
                            <i class="fa fa-fw fa-warning icon-muted"></i>
                            리포트 생성 중 조회정보가 분실되었습니다.<br />
                            리포트 생성을 위해 다시 한 번 조회 후 [인쇄] 버튼을 누르세요.
                        </a>
                        <a href="javascript:fn_Close();" class="list-group-item">
                            <i class="fa fa-fw fa-times-circle-o icon-muted"></i> 여기를 눌러 이 창을 닫습니다.
                        </a>
                        <a href="#" class="list-group-item">
                            <span class="badge bg-info lt">02-587-5927</span>
                            <i class="fa fa-fw fa-phone icon-muted"></i>Call us
                        </a>
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
