<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0504_ANDON.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0504_ANDON" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
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
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

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
                    fn_doSetGridEventAction('true');
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";

                    fn_OnSearchClick();
                }
            }


        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }


        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            var STATUS = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_STATUS');
            var TIME = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_CDATETIME');

            fn_OnControlDisableBox(s.GetEditor('F_CDATETIME'), null);

            if (STATUS == "1") {
                fn_OnControlEnableComboBox(s.GetEditor('F_STATUS'), true);
            } else {
                fn_OnControlEnableComboBox(s.GetEditor('F_STATUS'), false);
                if ((TIME == "" || TIME == null))
                    fn_OnControlEnableComboBox(s.GetEditor('F_STATUS'), true);
            }
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
            //var subColumn = s.GetColumnByField("F_LINECD");
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
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlWORK"  Width="100%" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-1">
                        <ucCTF:Work ID="ucWork" runat="server"  Width="100%" />
                    </div>
                    <label class="col-sm-1 control-label">안돈코드</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="ddlANDON" ClientInstanceName="ddlANDON" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_TDATETIME;F_BANCD;F_WORKCD;F_ANDONCD;F_STATUS;F_TYPE" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" 
                OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" 
                OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                OnDataBinding="devGrid_DataBinding"
                OnDataBound="devGrid_DataBound"
                OnBatchUpdate="devGrid_BatchUpdate"
                >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="EndlessPaging" PageSize="50" />
                <ClientSideEvents   
                    Init="fn_OnGridInit" 
                    EndCallback="fn_OnGridEndCallback" 
                    CallbackError="fn_OnCallbackError" 
                    BatchEditStartEditing="fn_OnBatchEditStartEditing" 
                    BatchEditEndEditing="fn_OnBatchEditEndEditing"
                    />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_TDATETIME"  Caption="발생일시" Width="180px" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="100px" >
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" ></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="설비명(공정명)" Width="200px" >
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" ></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ANDONNM" Caption="안돈상태명" Width="100px" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSUSER" Caption="작업자" Width="100px" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_STATUS" Name="F_STATUS" Caption="상태" Width="100px">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataColumn FieldName="F_CDATETIME" Caption="해제시간" Width="180px" >
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CODE01NM" Caption="발생원인" Width="100px" >
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" ></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CODE02" Caption="조치내용" Width="100%" >
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" ></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TYPE" Caption="F_TYPE" Visible="false" ></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CODE01" Caption="F_CODE01" Visible="false" ></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_BANCD" Caption="F_BANCD" Visible="false" ></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="F_WORKCD" Visible="false" ></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ANDONCD" Caption="F_ANDONCD" Visible="false" ></dx:GridViewDataColumn>
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>

    </div>
</asp:Content>
