<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BORD0102POP.aspx.cs" Inherits="SPC.WebUI.Pages.BORD.Popup.BORD0102POP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .devChkbox {
            margin-top: -13px;
        }

        .devRdo {
            margin-top: -5px;
        }
    </style>
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
            txtCOMMENT.SetHeight(height-150)
        }

        // 조회
        function fn_OnSearchClick() {

        }
        
        // 입력
        function fn_OnNewClick() {
        }

        function fn_OnSaveClick() {
            var Params = lblNUMBER.GetText() + "|" + txtCOMMENT.GetText() + "|" + txtIMAGESEQ.GetText() + "|";

            pnlContent.PerformCallback(Params);
            parent.parent.fn_OnSearchClick();
            parent.fn_Close();
            fn_Close();
        }
        // 답글입력
        function fn_OnInsertClick() {
            var Params = lblNUMBER.GetText() + "|" + txtCOMMENT.GetText() + "|" + txtIMAGESEQ.GetText() + "|";

            pnlContent.PerformCallback(Params);
            parent.parent.fn_OnSearchClick();
            parent.fn_Close();
            fn_Close();
        }

        // 취소
        function fn_OnCancelClick() {
            fn_Close();
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
            //parent.fn_OnSearchClick();
            close();
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
        </div>
        <div class="content">
            <dx:ASPxCallbackPanel ID="pnlContent" runat="server" ClientInstanceName="pnlContent" Enabled="true" Visible="true" Width="100%" OnCallback="pnlContent_Callback">
                <ClientSideEvents EndCallback="fn_OnEndCallback" />
                <Paddings PaddingBottom="10px"></Paddings>
                <PanelCollection>
                    <dx:PanelContent>
                        <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                            
                                           
                            <div class="form-group">
                                <dx:ASPxLabel ID="lblchkCOMPNM" CssClass="col-sm-2 control-label" runat="server"></dx:ASPxLabel>
                                <div class="col-sm-10">
                                    <dx:ASPxCheckBoxList ID="chkCOMP" RepeatDirection="Horizontal" RepeatLayout="Flow" ClientInstanceName="chkCOMP" runat="server" CssClass="devChkbox">
                                        <Border BorderStyle="None" />                  
                                    </dx:ASPxCheckBoxList>
                                </div>
                            </div>

                            <div class="form-group">
                              <label class="col-sm-1 control-label">작성자</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtTITLE" ClientInstanceName="txtTITLE" runat="server" Width="100%" class="form-control input-sm" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </div>
                            </div>

                                <div class="form-group">
                                    <label class="col-sm-1 control-label">답글내용</label>
                                    <div class="col-sm-11 control-label">
                                        <dx:ASPxMemo ID="txtCOMMENT" ClientInstanceName="txtCOMMENT" runat="server" Width="100%" class="form-control input-sm" Height="270px">
                                        </dx:ASPxMemo>
                                    </div>
                                </div>   


                               <div class="form-group">
                                <dx:ASPxLabel ID="lblNUMBER" ClientInstanceName="lblNUMBER" CssClass="col-sm-10 control-label" runat="server" Font-Size="Large" Text="" ClientVisible="false"></dx:ASPxLabel>
                               </div>   

                                <div class="form-group">
                                    <label class="col-sm-1 control-label"></label>
                                    <div class="col-sm-3 control-label">
                                        <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false" />
                                        <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpen('게시판파일첨부', 'I', 'F'); return false;">
                                            <i class="i i-file-plus "></i>
                                            <span class="text">파일첨부</span>
                                        </button>
                                    </div>                        
                                </div>
                            </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
            </div>
        <div class="paging"></div>                  
    </div>
</asp:Content>