<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucStopwatch.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucStopwatch" %>
<script type="text/javascript">
    var duration<%=ClientInstanceName%> = 1;
    var timer<%=ClientInstanceName%> = null;

    $(document).ready(function () {
        if('<%=ShowRemaintime%>' == 'false')
            $("#span<%=ClientInstanceName%>").hide();
        
        duration = parseInt(fn_GetCastValue('<%=ddlDIFF.ClientInstanceName%>'), 10);

        <%=ClientInstanceName%>StopwatchUpdate();
    });

    function <%=ClientInstanceName%>StopwatchUpdate() {
        if (duration <= 0) {
            duration = parseInt(fn_GetCastValue('<%=ddlDIFF.ClientInstanceName%>'), 10);
            <%=ClientInstanceName%>StopwatchCallback();
        }

        duration = duration - 1000;

        $("#span<%=ClientInstanceName%>").html('남은시간 : ' + msToTime(duration));

        timer<%=ClientInstanceName%> = setTimeout(<%=ClientInstanceName%>StopwatchUpdate, 1000);
    }

    function <%=ClientInstanceName%>StopwatchCallback() {
        <%=CallbackEvent%>();
    }

    function NullCallback() {

    }

    function <%=ClientInstanceName%>SelectedIndexChanged() {
        duration = parseInt(fn_GetCastValue('<%=ddlDIFF.ClientInstanceName%>'), 10);
    }

    function <%=ClientInstanceName%>Init() {
        duration = parseInt(fn_GetCastValue('<%=ddlDIFF.ClientInstanceName%>'), 10);
    }

    function <%=ClientInstanceName%>Value() {
        return fn_GetCastValue('<%=ddlDIFF.ClientInstanceName%>');
    }
</script>
<div style="width: 65px; float: left;">
    <dx:ASPxComboBox ID="ddlDIFF" runat="server" Width="60px"
        IncrementalFilteringMode="None" CssClass="NoXButton"
        OnInit="ddlDIFF_Init" OnDataBinding="ddlDIFF_DataBinding">
    </dx:ASPxComboBox>
</div>
<div id="span<%=ClientInstanceName%>" style="width: 125px; float: left; text-align: left;"></div>
