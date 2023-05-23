<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="COPYQCD34POP.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Popup.COPYQCD34POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
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
            if (false == fn_OnValidate()) return false;
            if (!confirm('검사기준 복사를 시작합니다.\r계속해서 진행하려면 확인을 누르세요!!')) return false;

            devCallback.PerformCallback('Exists');
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
        function fn_OnValidate() {
            if (txtITEMCDS.GetText() == '') {
                alert('복사할(Source) 품목을 입력하세요!!');
                txtITEMCDS.Focus();
                return false;
            }
            if (txtWORKCDS.GetText() == '') {
                alert('복사할(Source) 공정을 입력하세요!!');
                txtWORKCDS.Focus();
                return false;
            }
            if (txtITEMCDT.GetText() == '') {
                alert('복사될(Target) 품목을 입력하세요!!');
                txtITEMCDT.Focus();
                return false;
            }
            if (txtWORKCDT.GetText() == '') {
                alert('복사될(Target) 공정을 입력하세요!!');
                txtWORKCDT.Focus();
                return false;
            }

            if(txtITEMCDS.GetText() == txtITEMCDT.GetText() && txtWORKCDS.GetText() == txtWORKCDT.GetText()){
                alert('Source와 Target의 품목, 공정이 같습니다!!');
                txtITEMCDT.Focus();
                return false;
            }
        }

        // 검색된 품목 세팅
        function fn_OnSettingItem(CODE, TYPE) {
            ASPxClientControl.Cast('txtITEMCD' + TYPE).SetText(CODE);
        }

        // 검색된 품목 세팅
        function fn_OnSettingWork(CODE, TYPE) {
            ASPxClientControl.Cast('txtWORKCD' + TYPE).SetText(CODE);
        }

        // 검사기준복사 콜백처리
        function fn_OndevCallback(s, e) {
            var procType = s.cpProcType;
            var resultCode = s.cpResultCode;
            var resultMsg = s.cpResultMsg;

            if (procType == 'Exists' && resultCode == '0') {
                if (!confirm(resultMsg))
                    return false;
                else
                    devCallback.PerformCallback('Copy');
            } else {
                alert(resultMsg)

                if (resultCode == '1') {
                    //parent.fn_devPopupClose();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-6">
                        <p class="bg-info text-center"><span class="font-bold">Source</span></p>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">품목코드</label>
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtITEMCDS" ClientInstanceName="txtITEMCDS" runat="server" Width="100%" class="form-control input-sm"
                                    OnInit="txtITEMCDS_Init">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">공정코드</label>
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtWORKCDS" ClientInstanceName="txtWORKCDS" runat="server" Width="100%" class="form-control input-sm"
                                    OnInit="txtWORKCDS_Init">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <p class="bg-warning text-center"><span class="font-bold">Target</span></p>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">품목코드</label>
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtITEMCDT" ClientInstanceName="txtITEMCDT" runat="server" Width="100%" class="form-control input-sm"
                                    OnInit="txtITEMCDT_Init">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">공정코드</label>
                            <div class="col-sm-8">
                                <dx:ASPxTextBox ID="txtWORKCDT" ClientInstanceName="txtWORKCDT" runat="server" Width="100%" class="form-control input-sm"
                                    OnInit="txtWORKCDT_Init">
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OndevCallback" />
            </dx:ASPxCallback>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
