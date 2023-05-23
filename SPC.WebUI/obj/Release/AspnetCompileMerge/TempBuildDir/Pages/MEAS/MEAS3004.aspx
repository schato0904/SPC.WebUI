<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS3004.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS3004" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = "";
        var key = "";
        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(200);
            devGrid2.SetHeight(height-250);
        }

        // 조회
        function fn_OnSearchClick() {
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
            if (srcF_MS01MID.GetText() == "")
                return;

            if (!fn_OnValidate()) { return; }

            devCallback.PerformCallback('UPDATE');
        }

        // 취소
        function fn_OnCancelClick() {
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            devCallback.PerformCallback('DELETE');
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
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
        }

        function fn_CallbackComplete(s, e) {
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            //alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            return ASPxClientEdit.ValidateEditorsInContainerById('cbpContent');
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
        }

        function devGrid_RowDblClick(s, e) {
        }

        function fn_shcPopupSearch() {
            fn_OnPopupMEAS3001POPSearch('ShcSetItem', shcF_EQUIPNO.GetText());
        }

        function ShcSetItem(resultvalue) {
            shcF_EQUIPNO.SetText(resultvalue.F_EQUIPNO);
            shcF_EQUIPNM.SetText(resultvalue.F_EQUIPNM);
        }

        function fn_devGridOnRowDblClick(s, e) {
            key = s.GetRowKey(e.visibleIndex)
            devGrid2.PerformCallback(key);
        }

        function fn_OnEquipnoLostFocus(s, e) {
            var F_EQUIPNO = ASPxClientControl.Cast("shcF_EQUIPNO");
            var F_EQUIPNM = ASPxClientControl.Cast("shcF_EQUIPNM");

            var EQUIPCallback = ASPxClientControl.Cast("EQUIPCallback");
            if (!shcF_EQUIPNO.GetText() || shcF_EQUIPNO.GetText() == '') {
                fn_SetTextValue(F_EQUIPNM, '');
                return;
            } else
                EQUIPCallback.PerformCallback();
        }

        function fn_OnEQUIPEndCallback(s, e) {
            var F_EQUIPNO = ASPxClientControl.Cast("shcF_EQUIPNO");
            var F_EQUIPNM = ASPxClientControl.Cast("shcF_EQUIPNM");
            var EQUIPNO = s.cpEQUIPNO;
            var EQUIPNM = s.cpEQUIPNM;

            if (EQUIPNO != '' && EQUIPNM != '') {
                fn_SetTextValue(F_EQUIPNO, EQUIPNO);
                fn_SetTextValue(F_EQUIPNM, EQUIPNM);
            } else {
                fn_SetTextValue(F_EQUIPNO, '');
                fn_SetTextValue(F_EQUIPNM, '');
            }
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxCallback runat="server" ID="EQUIPCallback" ClientInstanceName="EQUIPCallback" OnCallback="EQUIPCallback_Callback">
                <ClientSideEvents EndCallback="fn_OnEQUIPEndCallback" CallbackError="fn_OnCallbackError" />
            </dx:ASPxCallback>
            <table class="InputTable" style="margin-bottom: 10px;">
                <colgroup>
                    <col style="width:9%" />
                    <col style="width:16%" />
                    <col style="width:9%" />
                    <col style="width:16%" />
                    <col style="width:9%" />
                    <col style="width:16%" />
                    <col style="width:9%" />
                    <col style="width:16%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>발생일</label>
                    </td>
                    <td class="tdContentR">
                        <ucCTF:Date ID="ucDate" runat="server" />
                    </td>
                    <td class="tdContentR"></td>
                    <td class="tdContentR"></td>
                    <td class="tdContentR"></td>
                    <td class="tdContentR"></td>
                    <td class="tdContentR"></td>
                    <td class="tdContent"></td>
                </tr>
                <tr>
                    <td class="tdTitle">사용팀</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox ID="srcF_TEAMCD" ClientInstanceName="srcF_TEAMCD" runat="server" ValueType="System.String" Width="100%">
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">
                        <label>관리번호</label>
                    </td>
                    <td class="tdContent">
                        <div class="control-label" style="float: left; width: 34%; padding-top: 0;">
                            <dx:ASPxTextBox ID="shcF_EQUIPNO" ClientInstanceName="shcF_EQUIPNO" runat="server" Width="100%">
                                <ClientSideEvents LostFocus="fn_OnEquipnoLostFocus" />
                            </dx:ASPxTextBox>
                        </div>
                        <div class="control-label" style="float: left; width: 54%; margin-left: 2px; padding-top: 0;">
                            <dx:ASPxTextBox ID="shcF_EQUIPNM" ClientInstanceName="shcF_EQUIPNM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                        </div>
                        <div class="control-label" style="float: left; width: 10%; text-align: left; padding-left: 2px; padding-top: 0;">
                            <button class="btn btn-default btn-xs" data-toggle="button" title="관리번호조회" onclick="fn_shcPopupSearch(); return false;">
                                <i class="i i-popup text"></i>
                                <i class="i i-popup text-active text-danger"></i>
                            </button>
                        </div>
                    </td>
                    <td class="tdTitle">
                        <label>사용구분</label>
                    </td>
                    <td class="tdContent">
                        <dx:ASPxComboBox ID="shcF_STATUSCD" ClientInstanceName="shcF_STATUSCD" runat="server" ValueType="System.String" Width="100%"></dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">사용자</td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_USER" ClientInstanceName="srcF_USER" runat="server" Width="100%"></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <section class="panel panel-default" >
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridOnRowDblClick" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="200px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_PRODNO" Caption="제조번호"  Width="120px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_STAND" Caption="규격" Width="120px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_UNQNO" Caption="고유번호" Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_MAKERNM" Caption="도입회사" Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_TEAMNM" Caption="사용팀" Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="사용구분"  Width="90px" />
                        <dx:GridViewDataColumn FieldName="F_CNT" Caption="발생건수" Width="100px" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataColumn FieldName="F_USER" Caption="사용자" Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_MS01MID" Visible="false" />
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
            </section>
        </div>
        <div class="content">
            <section class="panel panel-default" >
                <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MS01D4ID" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid2_CustomCallback">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"/>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_ONDT" Caption="발생일자"  Width="90px" />
                        <dx:GridViewDataColumn FieldName="F_EQUIPNO" Caption="관리번호"  Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_EQUIPNM" Caption="측정기명"  Width="200px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_CUSTNM" Caption="협력사"  Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="사용구분"  Width="90px" />
                        <dx:GridViewDataColumn FieldName="F_TEAMNM" Caption="사용팀"  Width="90px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_USER" Caption="사용자"  Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_CONTENTS" Caption="비고"  Width="100%" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_MS01D4ID" Visible="false" />
                    </Columns>
                </dx:ASPxGridView>
            </section>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
