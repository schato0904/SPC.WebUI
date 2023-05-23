<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDDLCommonCodeMulti.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucDDLCommonCodeMulti" %>
<dx:ASPxTextBox ID="hidCOMMCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlCOMMCD" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    OnDataBound="ddlCOMMCD_DataBound">
</dx:ASPxComboBox>
<script type="text/javascript">
    (function () {
        CTF.loadConstructor("CTF.UcCOMMCD"
            , function (constructor) { // 생성자 로드시 콜백 함수
                //function (clientInstanceName, strHidFACTCD, strTargetCtrls, strDdlFACT ) {
                var clientInstanceName = '<%= ClientInstanceName %>';
                var strHidCOMMCD = '<%= hidCOMMCD.ClientInstanceName %>';
                var strTargetCtrls = '<%= targetCtrls %>';
                var strDdlCOMMCD = '<%= ddlCOMMCD.ClientInstanceName %>';
                var jsonParameter = '<%= this.JsonParameter %>';
                var objTargetFuncs = (function () { return <%=this.targetFuncs%>; })();
                var instance = new constructor(clientInstanceName, strHidCOMMCD, strTargetCtrls, strDdlCOMMCD, jsonParameter, objTargetFuncs);
                window[clientInstanceName] = instance;
            }
            , function (errmsg) { // 오류 처리 함수
                alert(errmsg);
            });
    })();
</script>