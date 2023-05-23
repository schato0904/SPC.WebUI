<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS9004.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS9004" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .WORKGRID {
            display: none;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            if (selectedKeys.length > 0) {
                return false;
            }

            if (devGrid.batchEditApi.HasChanges()) {
                return false;
            }

            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();
            selectedKeys2 = devGrid.batchEditApi.HasChanges();

            if (selectedKeys2) { return false; }

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else
                devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            fn_doSetGridEventAction('false');
            if (s.cpResultCode != '' && s.cpResultCode != 'undefined' && s.cpResultCode != null) {
                if (s.cpResultCode == 'pager') {
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_FACTNMKR", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {

            if (parseInt(e.visibleIndex, 10) >= 0) {
                var P_BANCD = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_FACTNMKR');
            } else {
                var P_BANCD = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_FACTNMKR');
            }

        }


        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {    //수정
                var param1 = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_FACTCD');
                devGrid.GetEditor("F_FACTCD").SetValue(param1);
                fn_OnControlEnableComboBox(s.GetEditor('F_FACTNMKR'), false);
                s.GetEditor('F_TEAMCD').GetInputElement().disabled = true;

            } else {
                fn_OnControlEnableComboBox(s.GetEditor('F_FACTNMKR'), true);

                s.GetEditor('F_TEAMCD').GetInputElement().disabled = false;
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="InputTable bg-white" style="margin-bottom:5px;">
                <colgroup>
                    <col style="width:10%"/>
                    <col style="width:23%"/>
                    <col style="width:10%"/>
                    <col style="width:23%"/>
                    <col style="width:10%"/>
                    <col style="width:24%"/>
                </colgroup>
                <tr>
                    <td class="tdTitle">공장구분</td>
                    <td class="tdContentR">
                        <dx:ASPxComboBox ID="FACT_GBN" ClientInstanceName="FACT_GBN" runat="server" ValueType="System.String" Width="100%" />
                    </td>
                    <td class="tdContent" colspan="4">&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False"
                KeyFieldName="F_TEAMCD" EnableViewState="False" EnableRowsCache="False"
                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize">

                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="50px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_FACTNMKR" Name="F_FACTNMKR" Caption="공장구분" Width="130px">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataColumn FieldName="F_TEAMCD" Caption="사용팀코드" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TEAMNM" Caption="사용팀명" Width="250px" CellStyle-HorizontalAlign="LEFT"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SORTNO" Caption="순서" Width="40px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_USEYN" Caption="상태" Width="80px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_FACTCD" Caption="공장코드" Width="0px"></dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
