<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="QWK101_TBLPOP.aspx.cs" Inherits="SPC.WebUI.Pages.IPCM.QWK101_TBLPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">

            <div class="form-group">
                <label class="col-sm-1 control-label">발행번호</label>
                <div class="col-sm-2" >
                    <dx:ASPxTextBox ID="txt_publicno" ClientInstanceName="txt_publicno"  runat="server"  Width="100px" 
                        class="form-control input-sm" >
                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                    </dx:ASPxTextBox>
                </div>
                <label class="col-sm-1 control-label">의뢰작성자</label>
                <div class="col-sm-2" >
                    <dx:ASPxTextBox ID="txt_mademan" ClientInstanceName="txt_mademan"  runat="server"  Width="100px" 
                        class="form-control input-sm" >
                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                    </dx:ASPxTextBox>
                </div>
                <label class="col-sm-1 control-label">작성일자</label>
                <div class="col-sm-2" >
                    <dx:ASPxTextBox ID="txt_workdate" ClientInstanceName="txt_workdate" runat="server" Width="100%" 
                        class="form-control input-sm" >
                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                    </dx:ASPxTextBox>
                </div> 
                <label class="col-sm-1 control-label">파일보기</label>
                <div class="col-sm-2" >
                    <dx:ASPxTextBox ID="txtIMAGESEQ2" ClientInstanceName="txtIMAGESEQ2" ClientVisible="false" runat="server" Width="100%" class="form-control input-sm"/>
                    <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpenReadOnly('품질이상제기', 'F', 'F'); return false;" >
                        <i class="i i-file-plus "></i>
                        <span class="text">파일보기</span>
                    </button>
                </div>
            </div>

            <div class="form-group">&nbsp;</div>

            <div class="form-group">                                
                <label class="col-sm-1 control-label">주제</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_title" ClientInstanceName="txt_title" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm">                                                
                    </dx:ASPxMemo>
                </div>
            </div>
            <div class="form-group">                                
                <label class="col-sm-1 control-label">부적합사항</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_error" ClientInstanceName="txt_error" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm" >                                                
                    </dx:ASPxMemo>
                </div>
            </div>
            <div class="form-group">                                
                <label class="col-sm-1 control-label">발견동기</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_detect" ClientInstanceName="txt_detect" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm" >                                              
                    </dx:ASPxMemo>
                </div>
            </div>
            <div class="form-group">                                
                <label class="col-sm-1 control-label">잠정조치</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_temp" ClientInstanceName="txt_temp" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm" >                                              
                    </dx:ASPxMemo>
                </div>
            </div>
            <div class="form-group">                                
                <label class="col-sm-1 control-label">의뢰사항</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_subject" ClientInstanceName="txt_subject" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm" >                                    
                    </dx:ASPxMemo>
                </div>
            </div>

        </div>
        <div class="paging"></div>
</asp:Content>
