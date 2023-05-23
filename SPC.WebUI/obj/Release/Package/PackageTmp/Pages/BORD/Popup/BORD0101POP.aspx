<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BORD0101POP.aspx.cs" Inherits="SPC.WebUI.Pages.BORD.Popup.BORD0101POP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .devChkbox {
            margin-top: -5px;
        }

        .devRdo {
            margin-top: -5px;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var height
        $(document).ready(function () {
        });
        

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            <%if(strGbn == "0"){%>
            txtCONTENTS.SetHeight(height - 100);
            <%}else{%>
            txtCONTENTS.SetHeight(height - 300);
            <%}%>
        }

        // 조회
        function fn_OnSearchClick() {
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }


        // 삭제
        function fn_OnDeleteClick() {
            if (!confirm('게시글을 삭제하시겠습니까?\r 게시글을 등록한 협력사만 삭제할수 있습니다. ')) { return false; }
            var Params = lblNUMBER.GetText() + "|" + txtTITLE.GetText() + "|" + "delete" + "|" + txtCONTENTS.GetText() + "|" + txtIMAGESEQ.GetText() + "|";

            pnlContent.PerformCallback(Params);            
        }

        function OptionDisplay() {
            document.getElementById("pluscontent").style.display = "none"; //댓글입력칸,등록
            document.getElementById("pluscontent4").style.display = "none";  //댓글내용
            document.getElementById("optioncontent").style.display = "none";  //회사선택체크박스
            //document.getElementById("reply").style.display = "none";
        } //공지사항화면

        function OptionDisplay2() {
            //document.getElementById("reply").style.display = "none";
        }   // 접속회사와 게시글작성회사가 같은경우

        function OptionDisplay3() {
            //document.getElementById("modify").style.display = "none";
            //document.getElementById("delete").style.display = "none";
        } // 접속회사와 게시글작성회사가 다른경우

        function OptionDisplay4() {
            document.getElementById("pluscontent4").style.display = "none";
        } // 요청사항에서 댓글이 없는경우

        // 입력
        function fn_OnNewClick() {
            var pPage = './BORD0102POP.aspx?TITLE=답글입력&CRUD=S&KEYFIELDS=' + lblNUMBER.GetText();
            fn_OnPopupOpen(pPage, 0, 0);
        }

        // 답글입력
        function fn_OnInsertClick() {
            
        }

        //댓글입력
        function fn_CommentInsClick() {
            if (txtCOMMENTINS.GetText() != "") {
                var Params = lblNUMBER.GetText() + "|" + txtCOMMENTINS.GetText() + "|" + "comment" + "|";
            pnlContent.PerformCallback(Params);
            }
        }

        //수정
        function fn_OnModifyClick() {
            var pPage = './BORD0104POP.aspx?TITLE=게시글수정&CRUD=S&KEYFIELDS=' + lblNUMBER.GetText() + "|" + txtTITLE.GetText() + "|" + txtCONTENTS.GetText() + "|" + txtIMAGESEQ.GetText() + "|";
            fn_OnPopupOpen(pPage, 0, 0);
        }

        // 저장
        function fn_OnSaveClick() {
        }

        // 취소
        function fn_OnCancelClick() {
            parent.fn_OnSearchClick();
            fn_Close();
        }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            if (s.cpResultCode == "comment") {
                txtCONTENTS.SetHeight(height - 300);
            } else if (s.cpResultCode == "delete") {
                alert("삭제가 완료되었습니다.");
                parent.fn_OnSearchClick();
                fn_Close();
            } else {
                alert("삭제 권한이 없습니다.");
                fn_Close();
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
            <div class="form-group">
               <div class="form-group">
                    <dx:ASPxLabel ID="lblNUMBER" ClientInstanceName="lblNUMBER" CssClass="col-sm-10 control-label" runat="server" Font-Size="Large" Text="" ClientVisible="false"></dx:ASPxLabel>
               </div>   
            </div> 
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                            
                <div id="optioncontent" style="display: <%if (!gsVENDOR){%>display<%}else{ %>none<%} %>">
                    <div class="form-group">
                        <label class="col-sm-1 control-label">업체</label>                        
                        <div class="col-sm-10">
                            <dx:ASPxCheckBoxList ID="chkCOMP" RepeatDirection="Horizontal" RepeatLayout="Flow" ClientInstanceName="chkCOMP" runat="server" CssClass="devChkbox" ReadOnly="true">
                                <Border BorderStyle="None" />                  
                            </dx:ASPxCheckBoxList>
                        </div>
                    </div>
                </div>
                <dx:ASPxCallbackPanel ID="pnlContent" runat="server" ClientInstanceName="pnlContent" Enabled="true" Visible="true" Width="100%" OnCallback="pnlContent_Callback">
                <ClientSideEvents EndCallback="fn_OnEndCallback" />
                <Paddings PaddingBottom="10px"></Paddings>
                <PanelCollection>
                    <dx:PanelContent>
                            <div class="form-group">
                              <label class="col-sm-1 control-label">제목</label>
                                <div class="col-sm-11 control-label">
                                    <dx:ASPxTextBox ID="txtTITLE" ClientInstanceName="txtTITLE" runat="server" Width="100%" class="form-control input-sm" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-1 control-label">내용</label>
                                <div class="col-sm-11 control-label">
                                    <dx:ASPxMemo ID="txtCONTENTS" ClientInstanceName="txtCONTENTS" runat="server" Width="100%" class="form-control input-sm" ReadOnly="true">
                                    </dx:ASPxMemo>
                                </div>
                            </div>   
                               
                            <div id="pluscontent4">
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">댓글내용</label>
                                    <div class="col-sm-11 control-label">
                                        <dx:ASPxMemo ID="txtCOMMENT" ClientInstanceName="txtCOMMENT" runat="server" Width="100%" class="form-control input-sm" Height="100px" ReadOnly="true">
                                        </dx:ASPxMemo>
                                    </div>
                                </div>   
                            </div> 

                            <div id="pluscontent">
                               <div class="form-group">
                                    <label class="col-sm-1 control-label">댓글입력</label>
                                    <div class="col-sm-10 control-label">
                                        <dx:ASPxMemo ID="txtCOMMENTINS" ClientInstanceName="txtCOMMENTINS" runat="server" Width="100%" class="form-control input-sm" Height="20px">
                                        </dx:ASPxMemo>
                                    </div>
                                   <a href="#" onclick="fn_CommentInsClick()" class="btn btn-default btn-sm">등록</a>
                                </div>   
                            </div>

                                <div class="form-group">
                                    <label class="col-sm-1 control-label"></label>
                                    <div class="col-sm-3 control-label">
                                        <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false" />
                                        <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpenReadOnly('게시판파일첨부', 'I', 'F',''); return false;">
                                            <i class="i i-file-plus "></i>
                                            <span class="text">파일보기</span>
                                        </button>
                                    </div>                        
                                </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
            </div>
        <div class="paging"></div>                  
    </div>
</div>
</asp:Content>