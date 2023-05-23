<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWeek.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucWeek" %>
<script type="text/javascript">
    function fn_OnWKValueChanged1(s, e) {
        var val = s.GetValue();
        hidWeek1.SetValue(val);
        fn_OnWKValueChanged_jsFunction(s, e);
        BanPerFormCallbackTarget();
    }

    function fn_OnWKValueChanged2(s, e) {
        var val = s.GetValue();
        hidWeek2.SetValue(val);
        fn_OnWKValueChanged_jsFunction(s, e);
        BanPerFormCallbackTarget();
    }

    // 주차값 변경시 프로퍼티에 주어진 함수(Changed)가 있을경우, 호출
    function fn_OnWKValueChanged_jsFunction(s, e) {
        <% if (!string.IsNullOrEmpty(this.Changed) && this.Changed.IndexOf(' ') < 0)
           { %>
        if ( typeof(<%=Changed%>) == 'function' ){
            <%=Changed%>(s, e);
        }
        <% } %>
    }

    // PerFormCallbak Event Target Control
    function BanPerFormCallbackTarget() {
        if ('' != '<%=targetCtrls%>') {
            var tCtrls = '<%=targetCtrls%>'.split(';');
            if (tCtrls.length > 0) {
                $.each(tCtrls, function (key, tCtrl) {
                    var Ctrl = ASPxClientControl.Cast(tCtrl);
                    Ctrl.PerformCallback();
                });
            }
        }
    }

    function fn_OnFocusout() {
        var now = new Date();
        var yyyy = spin_year.GetValue();
        if (yyyy < 2014) {
            alert("2014년도 부터 조회가능 합니다.");
            fn_SetTextValue('spin_year',now.getYear()+1900);
            return false;
        }
       ddlWeek1.PerformCallback();
       ddlWeek2.PerformCallback();
    }
</script>
<%--<dx:ASPxTextBox ID="txt_Year" Clien tInstanceName="txt_Year" runat="server" >
    <ClientSideEvents LostFocus="fn_OnFocusout" />
</dx:ASPxTextBox>--%>
<div class="control-label" style="float: left; width: 28%;">
<dx:ASPxSpinEdit ID="spin_year" ClientInstanceName="spin_year" runat="server" MaxLength="4" Width="80px" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false" >
    <ClientSideEvents LostFocus="fn_OnFocusout" />
</dx:ASPxSpinEdit>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div id="Fromdiv" runat="server" class="control-label" style="float: left; width: 34%;">
<dx:ASPxTextBox ID="hidWeek1" ClientInstanceName="hidWeek1" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlWeek1" ClientInstanceName="ddlWeek1" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_WEEKNM" ValueField="F_WEEK" OnDataBound="ddlWeek1_DataBound" OnCallback="ddlWeek1_Callback">
    <ClientSideEvents ValueChanged="fn_OnWKValueChanged1" Init="fn_OnControlDisable" />
</dx:ASPxComboBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div id="Todiv" runat="server" class="control-label" style="float: left; width: 34%;">
<dx:ASPxTextBox ID="hidWeek2" ClientInstanceName="hidWeek2" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlWeek2" ClientInstanceName="ddlWeek2" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_WEEKNM" ValueField="F_WEEK" OnDataBound="ddlWeek2_DataBound" OnCallback="ddlWeek2_Callback">
    <ClientSideEvents ValueChanged="fn_OnWKValueChanged2" Init="fn_OnControlDisable" />
</dx:ASPxComboBox>
</div>