<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FDCK0110.aspx.cs" Inherits="SPC.WebUI.Pages.FDCK.FDCK0110" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .content {
            width: 100%;
            height: 100%;
            display: table;
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
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var Param = null;
        $(document).ready(function () {
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height();
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            //var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            var height = $(document.documentElement).height() - _hMargin - $(devGrid.GetMainElement()).offset().top;
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {

            //if (fn_GetCastValue('txtucMachineCD') == "") {
            //    alert("설비를 선택 후 조회 하세요.");
            //    return;
            //}

            fn_doSetGridEventAction('true');
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
        }

        // 취소
        function fn_OnCancelClick() {

            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();

        }

        // 삭제
        function fn_OnDeleteClick() {

        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            fn_doSetGridEventAction('false');
            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="content">
            <table border="1" class="contentTable">
                <colgroup>
                    <col style="width:11.1%" />
                    <col style="width:88.9%" />
                </colgroup>
                <tr>
                    <td class="tdLabel">
                        <label>조회월</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:Date ID="ucDate" runat="server" MonthOnly="true" SingleDate="true" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height:10px;"></div>
        <div class="content">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False"  Width="100%"
                        KeyFieldName="NO" EnableViewState="False" EnableRowsCache="False" OnCustomCallback="devGrid_CustomCallback" 
                        OnDataBound="devGrid_DataBound" OnDataBinding="devGrid_DataBinding">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                        <SettingsBehavior AllowSort="false"/>
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                        <Templates>
                            <StatusBar>
                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                            </StatusBar>
                        </Templates>
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="No." Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_INSPTYPENM" Caption="점검타입" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD01" Caption="1일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD02" Caption="2일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD03" Caption="3일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD04" Caption="4일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD05" Caption="5일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD06" Caption="6일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD07" Caption="7일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD08" Caption="8일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD09" Caption="9일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD10" Caption="10일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD11" Caption="11일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD12" Caption="12일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD13" Caption="13일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD14" Caption="14일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD15" Caption="15일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD16" Caption="16일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD17" Caption="17일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD18" Caption="18일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD19" Caption="19일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD20" Caption="20일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD21" Caption="21일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD22" Caption="22일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD23" Caption="23일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD24" Caption="24일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD25" Caption="25일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD26" Caption="26일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD27" Caption="27일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD28" Caption="28일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD29" Caption="29일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD30" Caption="30일" Width="30px" />
                            <dx:GridViewDataColumn FieldName="F_FIELD31" Caption="31일" Width="30px" />
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>