<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BORD0102.aspx.cs" Inherits="SPC.WebUI.Pages.BORD.BORD0102" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .devChkbox {
            margin-top:-13px;
        }

        .devRdo {
            margin-top:-5px;
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
        }

        // 조회
        function fn_OnSearchClick() {

        }

        // 입력
        function fn_OnNewClick() {
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
            
            var Params = rdoGbn.GetValue() + "|" + txtTITLE.GetText() + "|" + txtCONTENTS.GetText() + "|" + txtIMAGESEQ.GetText()+"|";

            <%if(!gsVENDOR){%>
                var selectItems = chkCOMP.GetSelectedItems();
                for (var i = 0; i < selectItems.length; i++) {
                    Params += selectItems[i].value + ";";
                }
                Params += "|";
            <%}%>
            
            
            if (fn_OnValidate())
                pnlContent.PerformCallback(Params);
        }

        // 취소
        function fn_OnCancelClick() {
            txtTITLE.SetText('');
            txtCONTENTS.SetText('');
        }

        // Validate
        function fn_OnValidate() {
            if (rdoGbn.GetValue() == "1" && chkCOMP.GetSelectedItems().length == 0) {
                alert("업체를 선택해 주세요!!"); return false;
            } else if (txtTITLE.GetText() == "") {
                alert("제목을 입력해 주세요!!"); return false;
            }
            else if (txtCONTENTS.GetText() == "") {
                alert("내용을 입력해 주세요!!"); return false;
            } else {
                return true;
            }
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
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
            parent.fn_OnSearchClick();
            fn_Close();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_rboGbnChange(s, e) {
            <%if (!gsVENDOR) {%>
                if (s.GetValue() == "0") {
                    document.getElementById("compDIV").style.display = "none";
                } else {
                    document.getElementById("compDIV").style.display = "";
                }
            <%}%>
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
        </div>
        <div class="content">
            <dx:ASPxCallbackPanel ID="pnlContent" runat="server" ClientInstanceName="pnlContent" Enabled="true" Visible="true" Width="100%" OnCallback="pnlContent_Callback" >
                <ClientSideEvents EndCallback="fn_OnEndCallback" />
                <Paddings PaddingBottom="10px"></Paddings>
                <PanelCollection>
                    <dx:PanelContent>
                        <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                            
                            <div class="form-group">
                                <label class="col-sm-1 control-label">구분</label>
                                <div class="col-sm-6">
                                    <dx:ASPxRadioButtonList ID="rdoGbn" ClientInstanceName="rdoGbn" runat="server"
                                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None" CssClass="devRdo" >
                                        <ClientSideEvents ValueChanged="fn_rboGbnChange" />
                                    </dx:ASPxRadioButtonList>
                                </div>
                                
                            </div>
                            <div class="form-group" style="display:none" id="compDIV">
                                <label class="col-sm-1 control-label">업체</label>
                                <div class="col-sm-11" >                                    
                                    <dx:ASPxCheckBoxList ID="chkCOMP"  RepeatDirection="Horizontal" RepeatLayout="Flow" ClientInstanceName="chkCOMP" runat="server" CssClass="devChkbox" >
                                        <Border BorderStyle="None" />                                        
                                    </dx:ASPxCheckBoxList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">제목</label>
                                <div class="col-sm-11 control-label">
                                    <dx:ASPxTextBox ID="txtTITLE" ClientInstanceName="txtTITLE" runat="server" Width="100%" class="form-control input-sm">
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">내용</label>
                                <div class="col-sm-11 control-label">
                                    <dx:ASPxMemo ID="txtCONTENTS" ClientInstanceName="txtCONTENTS" runat="server" Width="100%" class="form-control input-sm" Height="380px">
                                    </dx:ASPxMemo>
                                </div>
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
        <div class="paging">            
        </div>        
    </div>
</asp:Content>
