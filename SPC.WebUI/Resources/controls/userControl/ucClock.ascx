<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucClock.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucClock" %>
<script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/jquery/moment/moment-with-locales.min.js")%>"></script>
<span id="divClock" style="background-color: transparent;"></span>
<script type="text/javascript">
    $(document).ready(function () {
        moment.locale('ko');
        timedUpdate();
    });

    function timedUpdate() {
        var now = moment().format('YYYY년 MMMM Do dddd a h:mm:ss');
        $("#divClock").html(now);

        setTimeout(timedUpdate, 1000);
    }
</script>
