<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCount.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucCount" %>
<div id="divCountText<%=targetCtrl%>" class="col-sm-12 text-right font-bold" style="height:17px;"></div>
<script type="text/javascript">
    fn_RendorTotalCounts.<%=targetCtrl%> = function() {
        if ('<%=targetCtrl%>' != '') {
            if (ASPxClientUtils.IsExists('<%=targetCtrl%>')) {
                $('#divCountText<%=targetCtrl%>').text('Total Item Count : ' + ASPxClientGridView.Cast('<%=targetCtrl%>').GetVisibleRowsOnPage());
            }
        }
    }
</script>