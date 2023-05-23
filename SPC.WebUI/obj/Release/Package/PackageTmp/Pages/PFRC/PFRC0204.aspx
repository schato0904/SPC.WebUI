<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PFRC0204.aspx.cs" Inherits="SPC.WebUI.Pages.PFRC.PFRC0204" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devCallback_efnew_';

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
            devCallback.PerformCallback('SELECT');
        }

        // 입력
        function fn_OnNewClick() {
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
            devCallback.PerformCallback('UPDATE');
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
        } 

        // Grid End Callback
        function fn_OnEndCallback(s, e) {

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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
             <div class="form-group">
                <label class="col-sm-2 control-label">반</label>
                <div class="col-sm-2 control-label">
                    <dx:ASPxRadioButtonList ID="rdoBAN" ClientInstanceName="rdoBAN" runat="server"
                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Value="0" Text="명칭" />
                            <dx:ListEditItem Value="1" Text="코드" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </div>
            </div>
             <div class="form-group">
                <label class="col-sm-2 control-label">라인</label>
                <div class="col-sm-2 control-label">
                    <dx:ASPxRadioButtonList ID="rdoLINE" ClientInstanceName="rdoLINE" runat="server"
                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Value="0" Text="명칭" />
                            <dx:ListEditItem Value="1" Text="코드" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </div>
            </div>
             <div class="form-group">
                <label class="col-sm-2 control-label">공정</label>
                <div class="col-sm-2 control-label">
                    <dx:ASPxRadioButtonList ID="rdoWORK" ClientInstanceName="rdoWORK" runat="server"
                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Value="0" Text="명칭" />
                            <dx:ListEditItem Value="1" Text="코드" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </div>
            </div>
             <div class="form-group">
                <label class="col-sm-2 control-label">품목</label>
                <div class="col-sm-2 control-label">
                    <dx:ASPxRadioButtonList ID="rdoITEM" ClientInstanceName="rdoITEM" runat="server"
                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Value="0" Text="명칭" />
                            <dx:ListEditItem Value="1" Text="코드" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </div>
            </div>
            </div>
        </div>
        <div class="content">
            

        </div>
        <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" />
        </dx:ASPxCallback>
        <div class="paging"></div>
    </div>
</asp:Content>
