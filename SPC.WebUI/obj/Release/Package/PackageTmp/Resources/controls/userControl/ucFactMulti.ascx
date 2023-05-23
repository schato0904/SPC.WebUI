<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFactMulti.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucFactMulti" %>
<dx:ASPxTextBox ID="hidFACTCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlFACT" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_FACTNM" ValueField="F_FACTCD" OnDataBound="ddlFACT_DataBound" OnCallback="ddlFACT_Callback">
    <ClientSideEvents />
</dx:ASPxComboBox>
<script type="text/javascript">
    (function () {
        CTF.loadConstructor("CTF.UcFactMulti"
            , function (constructor) { // 생성자 로드시 콜백 함수
                //function (clientInstanceName, strHidFACTCD, strTargetCtrls, strDdlFACT ) {
                var clientInstanceName = '<%= ClientInstanceName %>';
                var strHidFACTCD = '<%= hidFACTCD.ClientInstanceName %>';
                var strTargetCtrls = '<%= targetCtrls %>';
                var strDdlFACT = '<%= ddlFACT.ClientInstanceName %>';
                var factParam = '<%= factParam %>';
                var instance = new constructor(clientInstanceName, strHidFACTCD, strTargetCtrls, strDdlFACT, factParam);
                window[clientInstanceName] = instance;
            }
            , function (errmsg) { // 오류 처리 함수
                alert(errmsg);
            });
    })();
</script>
