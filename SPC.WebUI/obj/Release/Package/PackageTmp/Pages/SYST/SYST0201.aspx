<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SYST0201.aspx.cs" Inherits="SPC.WebUI.Pages.SYST.SYST0201" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
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
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

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

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_FileDownload(gbn) {
            var dd = Date.now();
            
            if (gbn == 'P') {
                if (!confirm('현장측정 프로그램을 다운로드 하시겠습니까?'))
                    return;
                else
                    window.open('../../API/Common/FileDownload.ashx?attfolder=SPC&attfilename=SPCSetup.EXE&dd=' +dd);
            } else {
                if (!confirm('설치 매뉴얼을 다운로드 하시겠습니까?'))
                    return;
                else
                    window.open('../../API/Common/FileDownload.ashx?attfolder=SPC&attfilename=SPCManual.pdf&dd='+dd);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div class="hidden-nav-xs padder m-t-sm m-b-sm font-bold text-black">File Download</div>
                <ul class="nav">
                    <li>
                        <a href="javascript:fn_FileDownload('P');">
                            <i class="i i-sm i-link text-info-dk"></i>
                            <span>현장측정프로그램 다운로드</span>
                        </a>
                    </li>
                    <li>
                        <a href="javascript:fn_FileDownload('M');">
                            <i class="i i-sm i-link text-danger-dk"></i>
                            <span>설치 매뉴얼 다운로드</span>
                        </a>
                    </li>
                </ul>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
