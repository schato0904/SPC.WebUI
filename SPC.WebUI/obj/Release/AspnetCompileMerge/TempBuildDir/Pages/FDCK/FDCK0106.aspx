<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FDCK0106.aspx.cs" Inherits="SPC.WebUI.Pages.FDCK.FDCK0106" %>

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
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {

            if (fn_GetCastValue('txtucMachineCD') == "") {
                alert("설비를 선택 후 조회 하세요.");
                return;
            }

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
            
            fn_SetTextValue('hiducMachineCD', '');
            fn_SetTextValue('hiducMachTypeCD', '');
            fn_SetTextValue('txtucMachineCD', '');
            fn_SetTextValue('txtucMachineNM', '');
            fn_SetTextValue('txtucMachtypeNM', '');
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
                    <td class="tdLabel required">
                        <label>설비번호</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:Machine ID="ucMachine" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height:10px;"></div>
        <div class="content">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False"  Width="100%"
                        KeyFieldName="F_LOGDT" EnableViewState="False" EnableRowsCache="False" OnCustomCallback="devGrid_CustomCallback">
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
                            <dx:GridViewDataColumn FieldName="F_LOGDT" Caption="일시" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_LOGTYPENM" Caption="구분" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="항목" Width="60px" Visible="false" />
                            <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_INSPKINDNM" Caption="분류" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_INSPORDER" Caption="검사순서" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_CYCLENM" Caption="검사주기" Width="60px" />
                            <dx:GridViewDataColumn FieldName="F_UNITNM" Caption="단위" Width="60px" />
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>

