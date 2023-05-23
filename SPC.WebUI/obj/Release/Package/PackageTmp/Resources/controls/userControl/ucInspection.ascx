<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInspection.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucInspection" %>
<script type="text/javascript">
    function fn_OnINSPECTIONValueChanged(s, e) {
        hidINSPECTION.SetValue(s.GetValue());
    }
</script>
<dx:ASPxTextBox ID="hidINSPECTION" ClientInstanceName="hidINSPECTION" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlINSPECTION" ClientInstanceName="ddlINSPECTION" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    OnDataBound="ddlINSPECTION_DataBound">
    <ClientSideEvents ValueChanged="fn_OnINSPECTIONValueChanged" Init="fn_OnControlDisable" />
</dx:ASPxComboBox>