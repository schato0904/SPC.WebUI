<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="SPMT0101POP.aspx.cs" Inherits="SPC.WebUI.Pages.SPMT.Popup.SPMT0101POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var bSave = false;

        $(document).ready(function () {
            txtKEY.SetText(parent.fn_OnGetSelectedKeys());
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
            if (true == bSave) {
                alert('저장중입니다. 잠시만 기다려주세요');
                return false;
            }

            if (false == fn_OnValidate()) return false;
            
            devCallback.PerformCallback();
            bSave = true;
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

            bSave = false;
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            if (ASPxClientControl.Cast('hidCOMPCD').GetText() == '<%=gsCOMPCD%>') {
                alert('납품업체를 선택하세요!!');
                return false;
            }
            hidCOMPCODE.SetText(ASPxClientControl.Cast('ddlCOMP').GetValue());
            hidCOMPNM.SetText(ASPxClientControl.Cast('ddlCOMP').GetText());
            if (txtEONO.GetText() == '') {
                alert('E/O를 입력하세요!!');
                txtEONO.Focus();
                return false;
            }
            if (txtMATERLOTNO.GetText() == '') {
                alert('소재 LOT 번호를 입력하세요!!');
                txtMATERLOTNO.Focus();
                return false;
            }
            if (txtDCNT.GetText() == '') {
                alert('납품수량을 입력하세요!!');
                txtDCNT.Focus();
                return false;
            }
            if (txtDIRECTOR.GetText() == '') {
                alert('품질보증 책임자를 입력하세요!!');
                txtDIRECTOR.Focus();
                return false;
            }
        }

        // 출하정보 콜백처리
        function fn_OndevCallback(s, e) {
            var resultCode = s.cpResultCode;
            var resultMsg = s.cpResultMsg;

            alert(resultMsg);

            if(resultCode=='1')
                parent.fn_devPopupClose();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <dx:ASPxTextBox ID="txtKEY" ClientInstanceName="txtKEY" runat="server" ClientVisible="false" />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2 control-label">납품업체</label>
                    <div class="col-sm-4">
                        <dx:ASPxTextBox ID="hidCOMPCODE" ClientInstanceName="hidCOMPCODE" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="hidCOMPNM" ClientInstanceName="hidCOMPNM" runat="server" ClientVisible="false" />
                        <ucCTF:Comp ID="ucComp" runat="server" masterChk="1" nullText="선택하세요" />
                    </div>
                    <label class="col-sm-2 control-label">E/O</label>
                    <div class="col-sm-4">
                        <dx:ASPxTextBox ID="txtEONO" ClientInstanceName="txtEONO" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">소재LOT번호</label>
                    <div class="col-sm-4">
                        <dx:ASPxTextBox ID="txtMATERLOTNO" ClientInstanceName="txtMATERLOTNO" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                    <label class="col-sm-2 control-label">가공LOT번호</label>
                    <div class="col-sm-4">
                        <dx:ASPxTextBox ID="txtWORKLOTNO" ClientInstanceName="txtWORKLOTNO" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">납품수량</label>
                    <div class="col-sm-4">
                        <dx:ASPxTextBox ID="txtDCNT" ClientInstanceName="txtDCNT" runat="server" Width="100%" class="form-control input-sm" />
                    </div>
                    <label class="col-sm-2 control-label">책임자</label>
                    <div class="col-sm-4">
                        <dx:ASPxTextBox ID="txtDIRECTOR" ClientInstanceName="txtDIRECTOR" runat="server" Width="100%" class="form-control input-sm" />
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
