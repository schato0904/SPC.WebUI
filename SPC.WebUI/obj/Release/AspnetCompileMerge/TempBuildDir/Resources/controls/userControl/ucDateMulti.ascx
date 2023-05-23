<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDateMulti.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucDateMulti" %>
<div class="control-label" style="float: left; width: 48%;" runat="server" id="Fromdiv">
    <dx:ASPxTextBox ID="hidUCFROMDT" runat="server" ClientVisible="false" />
    <dx:ASPxDateEdit ID="txtFROMDT" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%"
        OnInit="txtDate_Init" >
    </dx:ASPxDateEdit>
</div>
<div style="float: left; width: 2%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 48%;" runat="server" id="Todiv">
    <dx:ASPxTextBox ID="hidUCTODT" runat="server" ClientVisible="false" />
    <dx:ASPxDateEdit ID="txtTODT" runat="server" UseMaskBehavior="true" EditFormat="Custom" 
        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%"
        OnInit="txtDate_Init">
    </dx:ASPxDateEdit>
</div>
<script type="text/javascript">
    (function () {
        CTF.loadConstructor("CTF.UcDateMulti"
            , function (constructor) { // 생성자 로드시 콜백 함수
                //function (clientInstanceName, strHidFACTCD, strTargetCtrls, strDdlFACT ) {
                var clientInstanceName = '<%= this.ClientInstanceName %>';
                var fnCallback = (function () { return <%= this.OnChanged %>; })();
                var _hidUCFROMDTClientInstanceName = '<%= hidUCFROMDT.UniqueID %>';
                var _hidUCTODTClientInstanceName = '<%= hidUCTODT.UniqueID %>';
                var _SingleDate = '<%=SingleDate%>';
                var _txtFROMDTClientInstanceName = '<%=txtFROMDT.UniqueID %>';
                var _txtTODTClientInstanceName = '<%=txtTODT.UniqueID %>';
                var _MonthOnly = '<%=MonthOnly%>';
                var _MaxMonth = '<%=MaxMonth%>';
                var _MaxDate = '<%= MaxDate%>';
                var _strTargetCtrls = '<%= this.targetCtrls %>';
                var _FieldName_Fromdt = '<%= this.FieldName_Fromdt %>';
                var _FieldName_Todt = '<%= this.FieldName_Todt %>';

                var instance = new constructor(clientInstanceName, _strTargetCtrls, _hidUCFROMDTClientInstanceName, _hidUCTODTClientInstanceName
                    , _SingleDate, _txtFROMDTClientInstanceName, _txtTODTClientInstanceName, _MonthOnly
                    , _MaxMonth, _MaxDate, _FieldName_Fromdt, _FieldName_Todt, fnCallback);
                window[clientInstanceName] = instance;
            }
            , function (errmsg) { // 오류 처리 함수
                alert(errmsg);
            });
    })();
</script>