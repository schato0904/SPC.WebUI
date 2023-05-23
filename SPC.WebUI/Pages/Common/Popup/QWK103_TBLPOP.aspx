<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="QWK103_TBLPOP.aspx.cs" Inherits="SPC.WebUI.Pages.IPCM.QWK103_TBLPOP" %>
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
                <label class="col-sm-1 control-label">작성자</label>
                <div class="col-sm-2" >
                    <dx:ASPxTextBox ID="txt_mademan" ClientInstanceName="txt_mademan"  runat="server"  Width="100px" 
                        class="form-control input-sm" >
                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                    </dx:ASPxTextBox>
                </div>
                <label class="col-sm-1 control-label">회신일자</label>
                <div class="col-sm-2" >
                    <dx:ASPxTextBox ID="txt_replydate" ClientInstanceName="txt_replydate" runat="server" Width="100%" 
                        class="form-control input-sm" >
                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                    </dx:ASPxTextBox>
                </div> 
                <label class="col-sm-1 control-label">파일보기</label>
                <div class="col-sm-2" >
                    <dx:ASPxTextBox ID="txtIMAGESEQ2" ClientInstanceName="txtIMAGESEQ2" ClientVisible="false" runat="server" Width="100%" class="form-control input-sm"/>
                    <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpenReadOnly('품질이상대책', 'H', 'F'); return false;" >
                        <i class="i i-file-plus "></i>
                        <span class="text">파일보기</span>
                    </button>
                </div>
            </div>

            <div class="form-group">&nbsp;</div>

            <div class="form-group">                                
                <label class="col-sm-1 control-label">조치</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_action" ClientInstanceName="txt_action" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm">                                            
                    </dx:ASPxMemo>
                </div>
            </div>
            <div class="form-group">                                
                <label class="col-sm-1 control-label">원인</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_cause" ClientInstanceName="txt_cause" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm" >                                          
                    </dx:ASPxMemo>
                </div>
            </div>
            <div class="form-group">                                
                <label class="col-sm-1 control-label">잠정대책</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_dechek1" ClientInstanceName="txt_dechek1" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm" >                                           
                    </dx:ASPxMemo>
                </div>
            </div>
            <div class="form-group">                                
                <label class="col-sm-1 control-label">근본대책</label>
                <div class="col-sm-11">
                    <dx:ASPxMemo ID="txt_dechek2" ClientInstanceName="txt_dechek2" runat="server" Width="100%" Height="60px" 
                        class="form-control input-sm" >                                              
                    </dx:ASPxMemo>
                </div>
            </div>
        </div>
        <div class="paging"></div>
</asp:Content>
