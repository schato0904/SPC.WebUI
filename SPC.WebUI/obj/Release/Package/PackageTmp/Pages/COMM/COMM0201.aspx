<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="COMM0201.aspx.cs" Inherits="SPC.WebUI.Pages.COMM.COMM0201" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script>
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
        }

        // 조회
        function fn_OnSearchClick() {

        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (!fn_OnValidate()) return;

            callbackControl.PerformCallback();
        }

        // 취소
        function fn_OnCancelClick() {

        }

        // 삭제
        function fn_OnDeleteClick() {

        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {

            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                if (s.cpResultCode == '1') {
                    parent.parent.doLogout();
                }

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isGoodCache') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/REDIRECTERR.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            if (fn_GetCastValue('txtCurrPass') == '') {
                alert('현재비밀번호를 입력하세요!!');
                txtCurrPass.Focus();
                return false;
            }

            if (fn_GetCastValue('txtChngPass') == '') {
                alert('변경할 비밀번호를 입력하세요!!');
                txtChngPass.Focus();
                return false;
            }

            var checkPasswordRule = validatePassword(fn_GetCastValue('txtChngPass'), {
                //length: [8, 15],
                //alpha: 2,
                //lower: 0,
                //upper: 0,
                //numeric: 1,
                badWords: ["+", ",", "&", "%", "~", "-", "$", "'", "\"", "|", "\\", "<", ">"]
            });

            if (!checkPasswordRule) {
                alert('입력한 비밀번호가 규칙에 맞지 않습니다!!\r\n' + valPasswordErrMsg);
                txtChngPass.Focus();
                return false;
            }

            if (fn_GetCastValue('txtCurrPass') == fn_GetCastValue('txtChngPass')) {
                alert('현재 비밀번호와 변경할 비밀번호가 같습니다');
                fn_SetTextValue('txtChngPass', '');
                txtChngPass.Focus();
                return false;
            }

            if (fn_GetCastValue('txtCnfmPass') == '') {
                alert('비밀번호 확인을 입력하세요!!');
                txtCnfmPass.Focus();
                return false;
            }

            if (fn_GetCastValue('txtChngPass') != fn_GetCastValue('txtCnfmPass')) {
                alert('변경할 비밀번호와 비밀번호 확인이 다릅니다');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <table class="table b-t table-bordered bg-white center-block" style="border:2px solid #DDDDDD;margin-bottom:0px;width:400px;">
                <tr>
                    <td class="text-right font-bold" style="width:140px;padding-right:5px;">현재비밀번호</td>
                    <td style="width:260px;padding:5px;">
                        <dx:ASPxTextBox ID="txtCurrPass" ClientInstanceName="txtCurrPass" runat="server" Password="true" MaxLength="20" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td class="text-right font-bold" style="width:140px;padding-right:5px;">변경할비밀번호</td>
                    <td style="width:260px;padding:5px;">
                        <dx:ASPxTextBox ID="txtChngPass" ClientInstanceName="txtChngPass" runat="server" Password="true" MaxLength="20" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td class="text-right font-bold" style="width:140px;padding-right:5px;">비밀번호확인</td>
                    <td style="width:260px;padding:5px;">
                        <dx:ASPxTextBox ID="txtCnfmPass" ClientInstanceName="txtCnfmPass" runat="server" Password="true" MaxLength="20" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td class="text-left font-bold" colspan="2" style="padding:5px;">
                        - 비밀번호에 포함되지 않아야 하는 문자: +,&%~-$'"|\<>
                        <%--<br />- 비밀번호는 8자 이상 15자 이하여야 합니다.
                        <br />- 비밀번호에는 2개 이상의 영문자가 포함되어야 합니다.
                        <br />- 비밀번호에는 1개 이상의 대문자가 포함되어야 합니다.<br />
                        <br />- 비밀번호에는 1개 이상의 소문자가 포함되어야 합니다.<br />
                        <br />- 비밀번호에는 1개 이상의 숫자가 포함되어야 합니다.--%>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging"></div>
    </div>
    <!-- CallBack 처리를 위한 객체 -->
    <dx:ASPxCallback ID="callbackControl" ClientInstanceName="callbackControl" runat="server" OnCallback="callbackControl_Callback">
        <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>