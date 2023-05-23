<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FITM0203_foseco.aspx.cs" Inherits="SPC.WebUI.Pages.FITM.FITM0203_foseco" %>
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

            //if (!fn_ValidateITEM()) { return;};
            //if (!fn_ValidateWORK()) { return; };
            //if(!fn_ValidateINSPITEM()) { return;};

            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            // 수정은 1 Row씩만 가능
            if (selectedKeys.length > 1) {
                alert('수정은 한개의 데이타만 가능합니다.');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
            //if (!devGrid.IsNewRowEditing() && !devGrid.IsEditing()) {
            //    alert('신규등록 되거나 수정된 데이타가 없습니다');
            //    return false;
            //}

            //if (!fn_OnValidate()) return false;

            //devGrid.UpdateEdit();
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

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 데이타는 복원할 수 없습니다.')) { return false; }

            fn_doSetGridEventAction('true');

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

            // EditForm 크기를 조정한다
            if ($(".divEditForm").length) {
                var editFormWidth = $("#cphBody_devGrid").width() - 34 - scrollbarWidth();
                editFormWidth = editFormWidth > parseInt(hidGridColumnsWidth.GetText(), 10) ? parseInt(hidGridColumnsWidth.GetText(), 10) - 34 : editFormWidth;
                $(".divEditForm").width(editFormWidth);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function devGrid_RowDbClick(s, e) {
            if (!(parseInt(e.visibleIndex, 10) >= 0))
                return;

            devGrid.StartEditRow(e.visibleIndex);
        }

        function fn_OKNGCHK(s, e) {
            // 판정 
            var INSPCD = txtINSPCD.GetText();
            var ldc_measure = parseFloat(s.GetText());
            var ldc_max = parseFloat(txtMAX.GetText());
            var ldc_min = parseFloat(txtMIN.GetText());
            var ldc_uclx = parseFloat(txtUCLX.GetText());
            var ldc_lclx = parseFloat(txtLCLX.GetText());

            // 관리이탈 확인
            if (ldc_uclx < ldc_measure || ldc_lclx > ldc_measure) {
                txtNGOKCHK.SetText("2");
                txtNGOKCHK.SetValue("2");
            }

            // 규격이탈 확인
            if (ldc_max < ldc_measure || ldc_min > ldc_measure) {
                txtNGOKCHK.SetText("1");
                txtNGOKCHK.SetValue("1");
            }

        }

        // Validate
        function fn_OnValidate(s, e) {
            if (chkDELETE.GetValue()) {
                return true;
            }

            if (txtMODIFYMEASURE.GetText() == '') {
                alert('수정Data를 입력하세요!!');
                txtMODIFYMEASURE.Focus();
                return false;
            } else {
                return true;
            }
        }

        // Set QCD34
        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);            

            txtSERIALNO.SetText(resultValues[10]);
            txtSERIALNO.SetValue(resultValues[10]);
        }
        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                fn_OnControlDisableBox(s.GetEditor('F_BANNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_LINENM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKDATE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKTIME'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_TSERIALNO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKMAN'), null);
                //fn_OnControlDisableBox(s.GetEditor('F_LOTNO'), null);
            } else {
                fn_OnControlEnableBox(s.GetEditor('F_BANNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_LINENM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKDATE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKTIME'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_TSERIALNO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_WORKMAN'), null);
                //fn_OnControlDisableBox(s.GetEditor('F_LOTNO'), null);
            }
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
                    <label class="col-sm-1 control-label">초중종구분</label>
                    <div class="col-sm-1">
                        <dx:ASPxComboBox ID="ddlFIRSTITEM" ClientInstanceName="ddlFIRSTITEM" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>                 
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                    <label class="col-sm-1 control-label">Lot No.</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="txtLOTNO" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%" 
                KeyFieldName="F_BANCD;F_LINECD;F_ITEMCD;F_WORKCD;F_TSERIALNO;F_WORKDATE;F_WORKTIME" EnableViewState="false" EnableRowsCache="false"
                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnDataBound="devGrid_DataBound"
                OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditStartEditing="fn_OnBatchEditStartEditing"/>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명"  Width="100px"  />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명"  Width="120px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="120px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No."  Width="100px"  >
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_FOURCD" Caption="품질사유" Width="120px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_FIRSTITEM" Caption="구분" Width="80px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO"    Caption="시료군"  Width="70px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자"  Width="60px"  />
                    <dx:GridViewDataColumn FieldName="F_QTY" Caption="수량"  Width="70px"  >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPQTY" Caption="검사수"  Width="70px"  >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_BADQTY" Caption="불량수"  Width="70px"  >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고"  Width="150px"  >
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
