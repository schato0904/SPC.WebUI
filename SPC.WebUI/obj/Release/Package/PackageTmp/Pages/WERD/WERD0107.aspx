<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD0107.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD0107" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .WORKGRID {
            display: none;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_OnSearchClick();
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
            //fn_OnBatchValidate("F_FACTCD", s, e);
            fn_OnBatchValidate("F_PLANYEAR", s, e);
            fn_OnBatchValidate("F_RATIOYEAR", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {

        }


        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {    //수정
                //fn_OnControlEnableComboBox(s.GetEditor('F_FACTCD'), false);
                fn_OnControlDisibleBox(s.GetEditor('F_PLANYEAR'), null);
                //fn_OnControlDisibleBox(s.GetEditor('F_FACTNM'), null);
            } else {
                //fn_OnControlEnableComboBox(s.GetEditor('F_FACTCD'), true);
                //fn_OnControlDisibleBox(s.GetEditor('F_FACTNM'), null);
                fn_OnControlEnableBox(s.GetEditor('F_PLANYEAR'), null);
            }

        }

        function fn_OnControlDisibleBox(s, e) {
            var inputElement = s.GetInputElement();
            inputElement.disabled = true;
            inputElement.readonly = true;
            inputElement.style.backgroundColor = '#cccccc';
            var mainElement = s.GetMainElement();
            mainElement.style.backgroundColor = '#cccccc';
        }

        function fn_OnControlEnableBox(s, e) {
            var inputElement = s.GetInputElement();
            inputElement.disabled = false;
            inputElement.readonly = false;
            inputElement.style.backgroundColor = '#ffffff';
            var mainElement = s.GetMainElement();
            mainElement.style.backgroundColor = '#ffffff';
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <%--<table class="InputTable bg-white" style="margin-bottom:5px;">
                <colgroup>
                    <col style="width:10%" />
                    <col style="width:23%" />
                    <col style="width:10%" />
                    <col style="width:23%" />
                    <col style="width:10%" />
                    <col style="width:24%" />
                </colgroup>
                <tr>                    
                    <td class="tdTitle">공장</td>
                    <td class="tdContentR">
                        <dx:ASPxComboBox ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" runat="server" ValueType="System.String" OnDataBound="ddlComboBox_DataBound" Width="100%" />
                    </td>
                    <td class="tdContentR"></td>
                    <td class="tdContentR"></td>
                    <td class="tdContentR"></td>
                    <td class="tdContent" style="text-align:right;">(단위 : %)</td>
                </tr>
            </table>--%>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" Width="100%"
                KeyFieldName="F_PLANYEAR" EnableViewState="False" EnableRowsCache="False"
                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize" OnDataBound="devGrid_DataBound">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
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
                    <dx:GridViewDataColumn FieldName="F_PLANYEAR" Name="F_PLANYEAR" Caption="년도" Width="120px"  />
                    <%--<dx:GridViewDataComboBoxColumn FieldName="F_FACTCD" Name="F_FACTCD" Caption="공장코드" Width="100px">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataComboBoxColumn>--%>
                    <%--<dx:GridViewDataColumn FieldName="F_FACTNM" Name="F_FACTNM" Caption="공장명" Width="120px"  ReadOnly="true">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="F_RATIOYEAR" Name="F_RATIOYEAR" Caption="년목표" Width="120px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M01" Name="F_M01" Caption="1월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M02" Name="F_M02" Caption="2월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M03" Name="F_M03" Caption="3월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M04" Name="F_M04" Caption="4월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M05" Name="F_M05" Caption="5월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M06" Name="F_M06" Caption="6월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M07" Name="F_M07" Caption="7월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M08" Name="F_M08" Caption="8월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M09" Name="F_M09" Caption="9월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M10" Name="F_M10" Caption="10월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M11" Name="F_M11" Caption="11월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_M12" Name="F_M12" Caption="12월" Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
