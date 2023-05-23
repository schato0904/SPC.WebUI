<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0801FILEPOP.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.Popup.BSIF0801FILEPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var bExecute = false;

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            //devGrid.SetHeight(height);
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
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // 도면이미지 Upload
        function fn_OnFileUploadComplete(s, e) {
            if (e.callbackData !== "") {
                if (e.callbackData.substring(0, 5) == 'ERROR') {
                    alert(e.callbackData.substring(6));
                } else {
                    bExecute = true;
                }
            }
        }

        function fn_OncallbackFileDelete(s, e) {
            if (e.result === "ok") {
                bExecute = true;
            } else if (e.result.substring(0, 5) == 'ERROR') {
                alert(e.result.substring(6));
            }
        }

        $(window).unload(function () { if (true == bExecute) parent.fn_OnSearchClick(); });
    </script>
    <style type="text/css">
        .table-bordered > thead > tr > th,
        .table-bordered > tbody > tr > th,
        .table-bordered > tfoot > tr > th,
        .table-bordered > thead > tr > td,
        .table-bordered > tbody > tr > td,
        .table-bordered > tfoot > tr > td {
            border: 1px solid #9F9F9F;
        }
        .table-bordered > thead > tr > th,
        .table-bordered > thead > tr > td {
            border-bottom-width: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="content">
            <table class="table b-t table-bordered bg-white" style="border:1px solid #9F9F9F;margin-bottom:0px;">
                <tbody>
                    <tr>
                        <td class="text-lg text-center font-bold" style="width:150px;background-color:#F0F0F0;">이미지 등록</td>
                        <td>
                            <dx:ASPxUploadControl ID="uploadFILEIMAGE" runat="server" AutoStartUpload="true" UploadMode="Auto" Width="400px"
                                OnFileUploadComplete="uploadFILEIMAGE_FileUploadComplete" BrowseButton-Text="찾기">
                                <ClientSideEvents FileUploadComplete="fn_OnFileUploadComplete" />
                                <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png" />
                            </dx:ASPxUploadControl>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>