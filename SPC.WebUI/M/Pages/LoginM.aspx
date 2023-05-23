<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginM.aspx.cs" Inherits="SPC.WebUI.M.Pages.LoginM" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>SPC :: 통계적공정능력관리</title>
    <link rel="stylesheet" type="text/css" href="../../Resources/jquery/jquery-ui-1.10.4.custom.min.css" />
    <link rel="stylesheet" type="text/css" href="../../Resources/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="../../Resources/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="../../Resources/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../../Resources/css/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../Resources/css/font.css" />
    <link rel="stylesheet" type="text/css" href="../../Resources/css/app.css" />
    <script type="text/javascript" src="../../Resources/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../Resources/jquery/jquery-ui-1.10.4.custom.min.js"></script>
    <script type="text/javascript" src="../../Resources/scripts/message/Message.js"></script>
    <script type="text/javascript" src="../../Resources/scripts/js/SPCCommon.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 선택된 언어셋 세팅
            fnSetLanguage('<%=InitLangClsCd%>');

            // 사용자 언어셋 세팅
            $("#hidUserLN").val('<%=InitLangClsCd%>');

            // 기본언어셋으로 저장문구 세팅
            fnSetLangText();

            // Radio List Add Event
            $("input[name='rdoType']").bind("click", function () {
                $("#hidUserTP").val($(this).val());
            });

            $("#txtUserID").focus();
        });

        function fnDoValidate() {
            var obj;

            // ID
            obj = $("#txtUserID");
            if (obj.val() == "") {
                alert(fnGetMessage("LOGIN_ID_EMPTY"));
                obj.focus();
                return false;
            }

            // PASSWORD
            obj = $("#txtUserPW");
            if (obj.val() == "") {
                alert(fnGetMessage("LOGIN_PASSWORD_EMPTY"));
                obj.focus();
                return false;
            }

            return true;
        }

        function fnDoLogin() {
            if (!fnDoValidate()) {
                return false;
            } else {
                $("#hidUserID").val($("#txtUserID").val());
                $("#hidUserPW").val($("#txtUserPW").val());

                callbackControl.PerformCallback();
                return false;
            }
        }

        function fnSetLangText() {
            $("span[name='partNM']").text(fnGetMessage("PARTNER_NAME"));
            $("#productNM").text(fnGetMessage("TITLE_NAME"));
            $("#saveID").text(fnGetMessage("LOGIN_ID_SAVE"));

            document.title = 'SPC :: ' + fnGetMessage("TITLE_NAME");

            if (cpLanguage == 'ko-KR') {
                $("span[name='compNM']").text($("#hidCompNMKR").val())
            } else if (cpLanguage == 'en-US') {
                $("span[name='compNM']").text($("#hidCompNMUS").val())
            } else if (cpLanguage == 'zh-CN') {
                $("span[name='compNM']").text($("#hidCompNMCN").val())
            }
        }

        function fnCallback(s, e) {
            switch (s.cpResultCode) {
                case "1":
                    //OpenMain(s.cpResultMsg);
                    window.location.href = s.cpResultMsg;
                    break;
                case "0":
                    alert(s.cpResultMsg);
                    break;
                case "9":
                    alert(s.cpResultMsg);
                    break;
            }
        }
    </script>
    <style type="text/css" >
        body{margin:0;padding:0}
        html{overflow:hidden}
        html,body{width:100%;height:100%;}
        .NoXButton .dxeEditAreaSys::-ms-clear {
            display: none;
        }
        .dxeDisabled,
        .dxeDisabled td.dxe
        {
            color: #acacac !important;
            cursor: default;
        }

        /* Font */
        body,input,textarea,select,button,table, td {
	        font-family: 'Malgun Gothic', '맑은고딕', 'Dotum', sans-serif;
	        font-size: 13px;
	        line-height: 1.5em;
	        text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidUserTP" runat="server" Value="10" />
        <section id="content" class="m-t-lg wrapper-md animated fadeInUp">
            <div class="container aside-xl">
                <span name="compNM" class="navbar-brand block">사이버테크프랜드</span>
                <section class="m-b-lg">
                    <header class="wrapper text-center">
                        <strong id="productNM">Statistical Process Control</strong>
                    </header>
                    <div class="list-group">
                        <div class="list-group-item">
                            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control no-border" placeholder="id" />
                        </div>
                        <div class="list-group-item">
                            <asp:TextBox ID="txtUserPW" runat="server" CssClass="form-control no-border" placeholder="password" TextMode="Password" />
                        </div>
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Sign in" CssClass="btn btn-lg btn-primary btn-block" OnClientClick="return fnDoLogin();" />
                    <div class="line line-dashed"></div>
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
        <!-- CallBack 처리를 위한 객체 -->
        <dx:ASPxCallback ID="callbackControl" ClientInstanceName="callbackControl" runat="server" OnCallback="Login">
            <ClientSideEvents CallbackComplete="function(s, e) {fnCallback(s, e);}" />
        </dx:ASPxCallback>
    </form>
</body>
</html>