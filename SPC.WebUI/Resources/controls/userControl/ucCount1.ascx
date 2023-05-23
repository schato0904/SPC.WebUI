<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCount1.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucCount1" %>
<div id="divCountText<%=targetCtrl%>" class="col-sm-12 text-right font-bold" style="height:17px;"></div>
<script type="text/javascript">
    function fn_RendorTotalCount1() {
        if ('<%=targetCtrl%>' != '') {
            if (ASPxClientUtils.IsExists('<%=targetCtrl%>')) {
                $('#divCountText<%=targetCtrl%>').text('Total Item Count : ' + ASPxClientGridView.Cast('<%=targetCtrl%>').GetVisibleRowsOnPage());
            }
        }
    }

    $(document).ready(function () {
        fn_RendorTotalCount1();
    });
</script>