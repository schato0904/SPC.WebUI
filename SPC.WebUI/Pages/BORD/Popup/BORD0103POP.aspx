<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BORD0103POP.aspx.cs" Inherits="SPC.WebUI.Pages.BORD.Popup.BORD0103POP" %>

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
            txtRECOMMENT.SetHeight(height - 100)
        }

        // 취소
        function fn_OnCancelClick() {
            fn_Close();
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

        // 입력
        function fn_OnNewClick() {
        }

        // 조회
        function fn_OnSearchClick() {
        }

        //화면옵션
        function OptionDisplay() {
            document.getElementById("btnU").style.display = "none";
            document.getElementById("btnD").style.display = "none";
        }

        // 답글입력
        function fn_OnInsertClick() {
        }

        //댓글입력
        function fn_CommentInsClick() {
        }

        // 수정
        function fn_OnModifyClick() {
            <%if (keyFields[4] != gsCOMPCD)
              {%>
            alert("등록자만 수정 할 수 있습니다!!"); return;
            <%}%>
            var pPage = './BORD0105POP.aspx?TITLE=답글수정&CRUD=S&KEYFIELDS=' + lblNUMBER.GetText() + "|" + txtRECOMMENT.GetText() + "|" + txtIMAGESEQ.GetText() + "|";
            fn_OnPopupOpen(pPage, 0, 0);
        }

        // 삭제
        function fn_OnDeleteClick() {

            if (!confirm('삭제하시겠습니까?')) { return false; }

            var Params = "delete" + "|" + txtRECOMMENT.GetText() + "|" + txtIMAGESEQ.GetText() + "|";
            pnlContent.PerformCallback(Params);
        }

        // 저장
        function fn_OnSaveClick() {
        }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            if (s.cpResultCode == "delete") {
                alert("삭제가 완료되었습니다.");
                parent.fn_OnSearchClick();
                fn_Close();
            } else if (s.cpResultCode == "delete2") {
                alert("삭제 권한이 없습니다.");
                fn_Close();
            } else {
                parent.fn_OnSearchClick();
                close();
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
        </div>
        <div class="content">
            <dx:ASPxCallbackPanel ID="pnlContent" runat="server" ClientInstanceName="pnlContent" Enabled="true" Visible="true" Width="100%" OnCallback="pnlContent_Callback">
                <ClientSideEvents EndCallback="fn_OnEndCallback" />
                <Paddings PaddingBottom="10px"></Paddings>
                <PanelCollection>
                    <dx:PanelContent>
                        <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                            
           
                            <div class="form-group"></div>

                           
                           <div id="replycontent">
                            <div class="form-group">
                                    <dx:ASPxLabel ID="lblNUMBER" ClientInstanceName="lblNUMBER" CssClass="col-sm-10 control-label" runat="server" Font-Size="Large" Text="" ClientVisible="false"></dx:ASPxLabel>
                            </div>   
                                <div class="form-group">
                                  <label class="col-sm-2 control-label">답글 작성자</label>
                                    <div class="col-sm-3 control-label">
                                        <dx:ASPxTextBox ID="txtREUSER" ClientInstanceName="txtREUSER" runat="server" Width="100%" class="form-control input-sm" ReadOnly="true">
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">답글내용</label>
                                    <div class="col-sm-10 control-label">
                                        <dx:ASPxMemo ID="txtRECOMMENT" ClientInstanceName="txtRECOMMENT" runat="server" Width="100%" class="form-control input-sm" Height="180px" ReadOnly="true">
                                        </dx:ASPxMemo>
                                    </div>
                                </div>   
                                <div class="form-group">
                                    <label class="col-sm-2 control-label"></label>
                                    <div class="col-sm-3 control-label">
                                        <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false" />
                                        <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpenReadOnly('게시판파일첨부', 'I', 'F',''); return false;">
                                            <i class="i i-file-plus "></i>
                                            <span class="text">파일보기</span>
                                        </button>
                                    </div>                        
                                </div>
                             </div>
                        </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        <div class="paging"></div>                  
    </div>
</div>
</asp:Content>