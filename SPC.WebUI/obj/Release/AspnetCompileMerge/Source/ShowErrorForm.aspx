<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowErrorForm.aspx.cs" Inherits="SPC.WebUI.ShowErrorForm" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SPC Error</title>
    <link type="text/css" rel="stylesheet" href="~/Resources/jquery/jquery-ui-1.10.4.custom.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/icon.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/app.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/spc.css" />
    <!--[if lt IE 9]>
    <script src="./Resources/scripts/ie/html5shiv.js"></script>
    <script src="./Resources/scripts/ie/respond.min.js"></script>
    <script src="./Resources/scripts/ie/excanvas.js"></script>
    <![endif]-->
    <script type="text/javascript" src="./Resources/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="./Resources/jquery/jquery-ui-1.10.4.custom.min.js"></script>
    <script type="text/javascript">
        function CloseTab(id) {
            parent.fn_OnDeleteTab(id, parent.fn_OnGetTabObject(id));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <section id="content">
            <div class="row m-n">
                <div class="col-sm-4 col-sm-offset-4">
                    <div class="text-center m-b-lg">
                        <h1 class="h text-white animated fadeInDownBig"><%=m_sErrorCode%></h1>
                    </div>
                    <div class="list-group bg-info auto m-b-sm m-b-lg">
                        <a href="#" class="list-group-item">
                            <i class="fa fa-fw fa-file-text-o icon-muted"></i>Error Message<br /><%=m_sErrorMsg%>
                        </a>
                        <a href="#" class="list-group-item">
                            <i class="fa fa-fw fa-question icon-muted"></i>Error Page : <%=m_sErrorPage%>
                        </a>
                        <a href="#" class="list-group-item">
                            <span class="badge bg-info lt">02-587-5927</span>
                            <i class="fa fa-fw fa-phone icon-muted"></i>Call us
                        </a>
                        <%if (!String.IsNullOrEmpty(m_sErrorTab)) {%>
                        <a href="javascript:CloseTab('<%=m_sErrorTab%>')" class="list-group-item">
                            <i class="i fa-fw i-cancel icon-muted"></i>탭닫기
                        </a>
                        <%}%>
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
