<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMachTypeSearch.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucMachTypeSearch" %>
<%@ Register TagPrefix="ucCTF" TagName="BanMulti" Src="~/Resources/controls/userControl/ucBanMulti.ascx" %>
<%@ Register TagPrefix="ucCTF" TagName="LineMulti" Src="~/Resources/controls/userControl/ucLineMulti.ascx" %>
<%@ Register TagPrefix="ucCTF" TagName="Count" Src="~/Resources/controls/userControl/ucCount.ascx" %>
<%--<style type="text/css">
    .divLine {
        width: 100%;
        height: 3px;
        /*background-color: dimgray;*/
        background-color: #6495ED;
        margin-top: 2px;
        margin-bottom: 2px;
    }
    .contentTable {
        width: 100%;
        border-color: darkgray;
    }
        .contentTable > tbody > tr > .tdLabel {
            /*background-color: #CFEFFF;*/
            background-color: #DCDCDC;
            color: dimgray;
            text-align: center;
            padding-top: 3px;
            padding-bottom: 3px;
        }
        .contentTable > tbody > tr > .tdLabel > label {
            color: #444444;
            font-weight:bold;
        }
        .contentTable > tbody > tr > .tdInput {
            background-color: white;
            padding-left: 3px;
            padding-right: 3px;
        }
</style>--%>
<dx:ASPxTextBox ID="hidSelectedValues" runat="server" ClientVisible="false" />
        <div class="content">
            <table border="1" class="contentTable">
                <colgroup>
                    <col style="width:15%" />
                    <col style="width:35%" />
                    <col style="width:15%" />
                    <col style="width:35%" />
                </colgroup>
                <tr>
                    <td class="tdLabel">
                        <label>반</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="schF_BANCD" />
                    </td>
                    <td class="tdLabel">
                        <label>라인</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:LineMulti runat="server" ID="schF_LINECD" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>설비명</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="schF_MACHNM" runat="server" Width="100%" MaxLength="50"></dx:ASPxTextBox>
                    </td>
                    <td class="tdInput" colspan="2">
                        <dx:ASPxButton ID="btnSearch" ClientInstanceName="btnSearch" runat="server" Text="조회" AutoPostBack="false">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divLine"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD;F_MACHCD;F_MACHNM;F_INSPTYPECD;F_INSPTYPENM" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback"
                OnDataBinding="devGrid_DataBinding" >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings HorizontalScrollBarMode="Hidden" VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" EnableRowHotTrack="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents 
                    RowDblClick="function(s,e) { }" />
                <Templates>
                    <StatusBar>
                        <%--<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />--%>
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="NO" Caption="No." Width="40px" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_INSPTYPENM" Caption="점검타입" Width="80px" />
                </Columns>
            </dx:ASPxGridView>
            <%--<dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>--%>
            <div class="paging"></div>
        </div>
<script type="text/javascript">
    (function () {
        CTF.loadConstructor("CTF.UcMachTypeSearch"
            , function (constructor) { // 생성자 로드시 콜백 함수
                //function (clientInstanceName, strHidFACTCD, strTargetCtrls, strDdlFACT ) {
                var clientInstanceName = '<%= this.ClientInstanceName %>';
                var fnCallback = (function () { return <%= this.OnSelectItem %>; })();                
                var strDevGridId = '<%= devGrid.ClientInstanceName %>';
                var strBtnSearchId = '<%= btnSearch.ClientInstanceName %>';
                var strSelectedValues = '<%= hidSelectedValues.ClientInstanceName %>';                
                var keyfieldnames = '<%= devGrid.KeyFieldName %>';
                var instance = new constructor(clientInstanceName, strDevGridId, strBtnSearchId, strSelectedValues, keyfieldnames, fnCallback);
                window[clientInstanceName] = instance;
            }
            , function (errmsg) { // 오류 처리 함수
                alert(errmsg);
            });
    })();
</script>
