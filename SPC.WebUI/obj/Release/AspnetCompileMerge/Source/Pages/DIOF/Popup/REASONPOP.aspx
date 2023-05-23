<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="REASONPOP.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.Popup.REASONPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_Focus('schF_REASON');
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
        }

        // 저장
        function fn_OnSaveClick() {
            if (!fn_OnValidate()) return false;

            if ('<%=TYPE%>' == 'U') {
                parent.fn_DoSave(fn_GetCastValue('schF_REASON'));
            } else if ('<%=TYPE%>' == 'D') {
                parent.fn_DoDelete(fn_GetCastValue('schF_REASON'));
            }

            parent.fn_devPopupClose();
        }

        // Validate
        function fn_OnValidate(s, e) {
            if (fn_GetCastValue('schF_REASON') == '') {
                alert('사유를 입력하세요!!');
                fn_Focus('schF_REASON');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <script type="text/javascript">
        // 닫기
        function fn_Close() {
            parent.fn_OnChangeMode('U');
            parent.fn_devPopupClose();
        }
    </script>
    <div class="container">
        <div class="search">
            <div style="width:100%;text-align:left;font-weight:bold;color:red;">수정(또는 삭제)하려는 사유를 입력하신 후 저장을 누르세요</div>
        </div>
        <div class="content">
            <table class="contentTable">
                <colgroup>
                    <col style="width:60px;" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">사유</td>
                    <td class="tdInput">
                        <dx:ASPxMemo ID="schF_REASON" ClientInstanceName="schF_REASON" runat="server" Width="100%" Height="70px" class="form-control input-sm" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>