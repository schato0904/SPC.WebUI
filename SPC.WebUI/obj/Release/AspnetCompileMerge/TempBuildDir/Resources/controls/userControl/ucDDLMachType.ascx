<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDDLMachType.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucDDLMachType" %>
<dx:ASPxTextBox ID="hidSelectedValues" runat="server" ClientVisible="false" />
<dx:ASPxGridLookup ID="devGridLookup" runat="server" KeyFieldName="F_COMPCD;F_FACTCD;F_MACHCD;F_MACHNM;F_INSPTYPECD;F_INSPTYPENM"
    SelectionMode="Single" TextFormatString="{4} [{5}]" AllowUserInput="false"
    >
    <GridViewProperties>
        <SettingsBehavior EnableRowHotTrack="true" />
    </GridViewProperties>
    <Columns>
        <dx:GridViewDataColumn FieldName="NO" Caption="No." Width="40px" />
        <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="100px" />
        <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="100px" />
        <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="100px" />
        <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="100px" />
        <dx:GridViewDataColumn FieldName="F_INSPTYPENM" Caption="점검타입" Width="100px" />
    </Columns>    
</dx:ASPxGridLookup>
<script type="text/javascript">
    (function () {
        CTF.loadConstructor("CTF.UcDDLMachType"
            , function (constructor) { // 생성자 로드시 콜백 함수
                //function (clientInstanceName, strHidFACTCD, strTargetCtrls, strDdlFACT ) {
                var clientInstanceName = '<%= this.ClientInstanceName %>';
                var fnCallback = function () { };
                <%= string.IsNullOrEmpty(this.OnSelectItem)?"":string.Format("fnCallback = {0};", this.OnSelectItem) %>
                var devGridLookupClientInstanceName = '<%= devGridLookup.ClientInstanceName %>';
                var hidSelectedValuesClientInstanceName = '<%= hidSelectedValues.ClientInstanceName %>';
                var keyfieldnames = '<%= devGridLookup.KeyFieldName %>';
                var instance = new constructor(clientInstanceName, devGridLookupClientInstanceName, hidSelectedValuesClientInstanceName, keyfieldnames, fnCallback);
                window[clientInstanceName] = instance;
            }
            , function (errmsg) { // 오류 처리 함수
                alert(errmsg);
            });
    })();
</script>