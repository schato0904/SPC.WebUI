<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="SPC.WebUI.LoginForm" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>SPC :: 통계적공정능력관리</title>
    <link rel="stylesheet" type="text/css" href="~/Resources/jquery/jquery-ui-1.10.4.custom.min.css" />
    <%if (true == bUseBootStrap) {%>
    <link rel="stylesheet" type="text/css" href="~/Resources/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/icon.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/app.css" />
    <!--[if lt IE 9]>
    <script src="Resources/scripts/ie/html5shiv.js"></script>
    <script src="Resources/scripts/ie/respond.min.js"></script>
    <script src="Resources/scripts/ie/excanvas.js"></script>
    <![endif]-->
    <%}%>
    <script type="text/javascript" src="Resources/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Resources/jquery/jquery-ui-1.10.4.custom.min.js"></script>
    <script type="text/javascript" src="Resources/scripts/message/Message.js"></script>
    <script type="text/javascript" src="Resources/scripts/js/SPCCommon.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            // Login Page Iframe 여부 확인
            var isInIFrame = (window.location != window.parent.location);
            if (isInIFrame == true) {
                top.window.location.href = 'LoginForm.aspx';
                return;
            }

            <%
            //string u = Request.ServerVariables["HTTP_USER_AGENT"];
            //Regex b = new Regex("(android|bb\\d+|meego).+mobile|avantgo|bada\\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase);
            //Regex v = new Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\-(n|u)|c55\\/|capi|ccwa|cdm\\-|cell|chtm|cldc|cmd\\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\\-|_)|g1 u|g560|gene|gf\\-5|g\\-mo|go(\\.w|od)|gr(ad|un)|haie|hcit|hd\\-(m|p|t)|hei\\-|hi(pt|ta)|hp( i|ip)|hs\\-c|ht(c(\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\-(20|go|ma)|i230|iac( |\\-|\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\\/)|klon|kpt |kwc\\-|kyo(c|k)|le(no|xi)|lg( g|\\/(k|l|u)|50|54|\\-[a-w])|libw|lynx|m1\\-w|m3ga|m50\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\-2|po(ck|rt|se)|prox|psio|pt\\-g|qa\\-a|qc(07|12|21|32|60|\\-[2-7]|i\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\-|oo|p\\-)|sdk\\/|se(c(\\-|0|1)|47|mc|nd|ri)|sgh\\-|shar|sie(\\-|m)|sk\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\-|v\\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\-|tdg\\-|tel(i|m)|tim\\-|t\\-mo|to(pl|sh)|ts(70|m\\-|m3|m5)|tx\\-9|up(\\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\\-|your|zeto|zte\\-", RegexOptions.IgnoreCase);
            //if( b.IsMatch(u) || v.IsMatch((u.Substring(0, 4)))){
            //    Response.Redirect("http://www.naver.com/");
            //}
            %>
            
            // 호환성보기체크
            if (true == isTrident()) {
                alert('현재 브라우저가 호환성보기로 실행중입니다.\r사이트 사용을 위해서는 호환성 보기를 꺼주세요!!');
            }

            // 선택된 언어셋 세팅
            fnSetLanguage('<%=InitLangClsCd%>');

            // 사용자 언어셋 세팅
            $("#hidUserLN").val('<%=InitLangClsCd%>');

            // 기본언어셋으로 저장문구 세팅
            fnSetLangText();

            // 언어선택 DropDownList Client OnChange Event handler
            $("#loginControl_ddlLangClsCd").change(function () {
                $("#hidUserLN").val($(this).val());
                fnSetLanguage($(this).val());
                fnSetLangText();
            });

            // Radio List Add Event
            $("input[name='rdoType']").bind("click", function () {
                $("#hidUserTP").val($(this).val());
            });

            // Save User ID
            if ($("#loginControl_chkSaveUserID").is(":checked") == true || $("#loginControl_txtUserID").val().length > 0) {
                $("#loginControl_txtUserPW").focus();
            } else {
                $("#loginControl_txtUserID").focus();
            }
        });

        function fnDoValidate() {
            var obj;

            // ID
            obj = $("#loginControl_txtUserID");
            if (obj.val() == "") {
                alert(fnGetMessage("LOGIN_ID_EMPTY"));
                obj.focus();
                return false;
            }

            // PASSWORD
            obj = $("#loginControl_txtUserPW");
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
                $("#hidUserID").val($("#loginControl_txtUserID").val());
                $("#hidUserPW").val($("#loginControl_txtUserPW").val());

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
                $("span[name='compNM']").text('<%=compnmkr%>')
            } else if (cpLanguage == 'en-US') {
                $("span[name='compNM']").text('<%=compnmus%>')
            } else if (cpLanguage == 'zh-CN') {
                $("span[name='compNM']").text('<%=compnmcn%>')
            }
        }

        function fnCallback(s, e) {
            switch (s.cpResultCode) {
                case "1":
                    OpenMain(s.cpResultMsg);
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
        <asp:PlaceHolder ID="pHolder" runat="server" />
        <!-- CallBack 처리를 위한 객체 -->
        <dx:ASPxCallback ID="callbackControl" runat="server" OnCallback="Login">
            <ClientSideEvents CallbackComplete="function(s, e) {fnCallback(s, e);}" />
        </dx:ASPxCallback>
    </form>
</body>
</html>