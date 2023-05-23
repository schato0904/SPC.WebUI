<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDDLSYCOD01.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucDDLSYCOD01" %>
<dx:ASPxTextBox ID="hidSYCOD01CD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlSYCOD01" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_CODENM" ValueField="F_CODE"
    OnDataBound="ddlSYCOD01_DataBound"
    OnCallback="ddlSYCOD01_Callback"
    OnInit="ddlSYCOD01_Init">
    <ClientSideEvents Init="fn_OnControlDisable" />
</dx:ASPxComboBox>
<script type="text/javascript">
    (function () {
        CTF.loadConstructor("CTF.UcSYCOD01"
            , function (constructor) { // 생성자 로드시 콜백 함수
                //function (clientInstanceName, strHidFACTCD, strTargetCtrls, strDdlFACT ) {
                var clientInstanceName = '<%= ClientInstanceName %>';
                var strHidSYCOD01CD = '<%= hidSYCOD01CD.ClientInstanceName %>';
                var strTargetCtrls = '<%= targetCtrls %>';
                var strDdlSYCOD01 = '<%= ddlSYCOD01.ClientInstanceName %>';
                var jsonParameter = '<%= this.JsonParameter %>';
                var instance = new constructor(clientInstanceName, strHidSYCOD01CD, strTargetCtrls, strDdlSYCOD01, jsonParameter);
                window[clientInstanceName] = instance;
            }
            , function (errmsg) { // 오류 처리 함수
                alert(errmsg);
            });
    })();
</script>