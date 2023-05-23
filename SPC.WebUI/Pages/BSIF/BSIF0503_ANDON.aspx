<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0503_ANDON.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0503_ANDON" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .search {
            width: 100%;
            display: table-row;
        }
        .content {
            width: 100%;
            height: 100%;
            display: table-row;
        }
        .left {
            width: 300px;
            height: 100%;
            display: table-cell;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            height: 100%;
            display: table-cell;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var Param = null;
        $(document).ready(function () {
            devGrid.PerformCallback();
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height();
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
            devGrid2.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            devGrid2.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid2.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid2.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
            if (!devGrid2.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else
                devGrid2.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid2.UnselectAllRowsOnPage();
            devGrid2.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid2.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid2.DeleteRowByKey(selectedKeys[i]);
            }

            devGrid2.UpdateEdit();
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

        function fn_OnEndCallback2(s, e) {
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
        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_ANDONCD", s, e);
            fn_OnBatchValidate("F_BANCD", s, e);
            fn_OnBatchValidate("F_JUYAKIND", s, e);
        }

        // BatchEditStartEditing
        function fn_OnRowClick(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var UserID = devGrid.GetRowKey(e.visibleIndex);
                fn_OnCancelClick();
                hidUserID.SetText(UserID);
                hidGbn.SetText("");
                devGrid2.PerformCallback();
            }
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing2(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                fn_OnControlEnableComboBox(s.GetEditor('F_BANCD'), false);
                //fn_OnControlEnableComboBox(s.GetEditor('F_JUYAKIND'), false);
                fn_OnControlEnableComboBox(s.GetEditor('F_ANDONCD'), false);
            } else {
                fn_OnControlEnableComboBox(s.GetEditor('F_BANCD'), true);
                //fn_OnControlEnableComboBox(s.GetEditor('F_JUYAKIND'), true);
                fn_OnControlEnableComboBox(s.GetEditor('F_ANDONCD'), true);
            }
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
            //var subColumn = s.GetColumnByField("F_JUYAKIND");
            //if (!e.rowValues.hasOwnProperty(subColumn.index))
            //    return;
            //var cellInfo = e.rowValues[subColumn.index];
            //if (ddlLINEEdit.GetSelectedIndex() > -1 || cellInfo.text != ddlLINEEdit.GetText()) {
            //    cellInfo.value = ddlLINEEdit.GetValue();
            //    cellInfo.text = ddlLINEEdit.GetText();
            //    ddlLINEEdit.SetValue(null);
            //}
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <%--<div class="search"></div>--%>
        <div class="content">
            <div class="left">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" 
                        KeyFieldName="F_USERID" EnableViewState="False" EnableRowsCache="False" OnDataBinding="devGrid_DataBinding"
                        OnCustomCallback="devGrid_CustomCallback">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowClick="fn_OnRowClick" />
                        <Templates>
                            <StatusBar>
                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                            </StatusBar>
                        </Templates>
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_USERID" Caption="ID" Width="150px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="F_USERNM" Caption="사용자명" Width="150px">
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            </div>
            <div class="right">
                <section class="panel panel-default" style="height: 100%;">
                    <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="False"  Width="100%"
                        KeyFieldName="F_BANCD;F_JUYAKIND;F_ANDONCD" EnableViewState="False" EnableRowsCache="False"
                        OnDataBinding="devGrid2_DataBinding" 
                        OnDataBound="devGrid2_DataBound" 
                        OnInitNewRow="devGrid2_InitNewRow"
                        OnBatchUpdate="devGrid2_BatchUpdate" 
                        OnCustomCallback="devGrid2_CustomCallback" 
                        OnCellEditorInitialize="devGrid2_CellEditorInitialize"
                    >
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                        <SettingsBehavior AllowSort="false" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback2" CallbackError="fn_OnCallbackError"
                            BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing2" BatchEditEndEditing="fn_OnBatchEditEndEditing" />
                        <Templates>
                            <StatusBar>
                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
                            </StatusBar>
                        </Templates>
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                        ClientSideEvents-CheckedChanged="function(s, e) { devGrid2.SelectAllRowsOnPage(s.GetChecked()); }" />
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>                            
                            <dx:GridViewDataComboBoxColumn FieldName="F_BANCD" Name="F_BANCD" Caption="반코드" Width="100px">
                                <Settings AllowSort="True" />
                            </dx:GridViewDataComboBoxColumn>                            
                            <dx:GridViewDataComboBoxColumn FieldName="F_JUYAKIND" Name="F_JUYAKIND" Caption="주야구분" Width="100px">
                                <Settings AllowSort="True" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="F_ANDONCD" Name="F_ANDONCD" Caption="안돈코드" Width="100px">
                                <Settings AllowSort="True" />
                            </dx:GridViewDataComboBoxColumn>                            
                            <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용" Width="60px" />
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            </div>
            <dx:ASPxTextBox ID="hidUserID" ClientInstanceName="hidUserID" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidGbn" ClientInstanceName="hidGbn" runat="server" ClientVisible="false" />            
            
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>

