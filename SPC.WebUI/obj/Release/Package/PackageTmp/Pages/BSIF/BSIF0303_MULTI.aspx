<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0303_MULTI.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0303_MULTI" %>
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
            else {
                if (!gridIsValid)
                    alert('입력값을 확인해보세요');
                else
                    devGrid.UpdateEdit();
            }
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

            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }

            devGrid.UpdateEdit();
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {
            window.open('./Export/BSIF0303EXPORT.aspx?pBANCD=' + fn_GetCastValue('hidBANCD') + '&pITEMCD=' + fn_GetCastValue('hidUCITEMCD') + '&pWORKCD=' + fn_GetCastValue('hidWORKCD') + '&pINSPCD=' + fn_GetCastValue('hidINSPECTION'));
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

                    devGrid.PerformCallback();
                }
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            gridValids = [];
            gridValidIdx = 0;
            gridIsValid = true;

            if (parseInt(e.visibleIndex, 10) >= 0) {
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_INSPCD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_MEAINSPCD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_INSPDETAIL", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_WORKCD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_WORKNM", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_STANDARD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_SIRYO", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidateMin("F_SIRYO", s, e, 1);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_GETDATA", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_DEFECTS_N", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidateMin("F_DEFECTS_N", s, e, 1);
            } else {
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_ITEMCD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_ITEMNM", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_INSPCD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_MEAINSPCD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_INSPDETAIL", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_WORKCD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_WORKNM", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_STANDARD", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_SIRYO", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidateMin("F_SIRYO", s, e, 1);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_GETDATA", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_DEFECTS_N", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidateMin("F_DEFECTS_N", s, e, 1);
            }

            gridValids.forEach(function (gridValid) {
                if (!gridValid) {
                    gridIsValid = false;
                    return false;
                }
            });
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            editRowIndex = parseInt(e.visibleIndex, 10);

            if (editRowIndex >= 0) {
                fn_OnControlEnableBox(s.GetEditor('F_DISPLAYNO'), null);
                fn_OnControlEnableBox(s.GetEditor('F_SAMPLENO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlRemoveAttr(s.GetEditor('F_ITEMCD'), null, 'ondblclick');
                fn_OnControlDisableBox(s.GetEditor('F_WORKCD'), null);
                fn_OnControlRemoveAttr(s.GetEditor('F_WORKCD'), null, 'ondblclick');
                fn_OnControlEnableComboBox(s.GetEditor('F_INSPCD'), false);
                // 수정모드인 경우 수작업여부 판단
                if (devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_GETTYPE') == '0') {
                    fn_OnControlEnableBox(s.GetEditor('F_PORT'), null);
                    fn_OnControlEnableBox(s.GetEditor('F_CHANNEL'), null);
                    fn_OnControlEnableComboBox(s.GetEditor('F_PORT'), true);
                } else {
                    fn_OnControlDisableBox(s.GetEditor('F_PORT'), null);
                    fn_OnControlDisableBox(s.GetEditor('F_CHANNEL'), null);
                    fn_OnControlEnableComboBox(s.GetEditor('F_PORT'), false);
                }
            } else {
                fn_OnControlEnableComboBox(s.GetEditor('F_INSPCD'), true);
                fn_OnControlDisableBox(s.GetEditor('F_DISPLAYNO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_SAMPLENO'), null);
                fn_OnControlEnableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlAddAttr(s.GetEditor('F_ITEMCD'), null, 'ondblclick', "fn_OnPopupItemSearch('T')");
                fn_OnControlEnableBox(s.GetEditor('F_WORKCD'), null);
                fn_OnControlAddAttr(s.GetEditor('F_WORKCD'), null, 'ondblclick', "fn_OnPopupWorkSearchForm()");
            }

            fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_MODELNM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_SERIALNO'), null);
            fn_OnControlDisableBox(s.GetEditor('F_INSPDETAIL'), null);
            fn_OnControlDisableBox(s.GetEditor('F_BANCD'), null);
            fn_OnControlDisableBox(s.GetEditor('F_LINECD'), null);
            fn_OnControlDisableBox(s.GetEditor('F_WORKNM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_MEASURE'), null);
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
        }

        // 품목코드 입력시 명칭조회
        function fn_OnITEMCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                devGrid.GetEditor('F_ITEMNM').SetValue('');
                devGrid.GetEditor('F_ITEMNM').SetText('');
                devGrid.GetEditor('F_ITEMNM').GetMainElement().title = '';
                devGrid.GetEditor('F_MODELNM').SetValue('');
                devGrid.GetEditor('F_MODELNM').SetText('');
                devGrid.GetEditor('F_MODELNM').GetMainElement().title = '';
                return;
            } else
                devCallback.PerformCallback('ITEM|' + s.GetValue());
        }

        // 검색된 품목 세팅
        function fn_OnSettingItem(CODE, TEXT, MODEL) {
            devGrid.StartEditRow(editRowIndex);
            devGrid.GetEditor('F_ITEMCD').SetText(CODE);
            devGrid.GetEditor('F_ITEMNM').SetText(TEXT);
            devGrid.GetEditor('F_MODELNM').SetText(MODEL);
        }

        // 항목코드 입력시 명칭조회
        function fn_OnMEAINSPCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                devGrid.GetEditor('F_INSPDETAIL').SetValue('');
                devGrid.GetEditor('F_INSPDETAIL').SetText('');
                devGrid.GetEditor('F_INSPDETAIL').GetMainElement().title = '';
                return;
            } else
                devCallback.PerformCallback('MEAINSP|' + s.GetValue());
        }

        // 항목코드조회
        function fn_OnPopupMeainspSearchForm() {
            fn_OnPopupMeainspSearchBatch(devGrid.GetEditor('F_MEAINSPCD').GetText(), devGrid.GetEditor('F_INSPDETAIL').GetText());
        }

        // 검색된 검사항목 세팅
        function fn_OnSettingMeainsp(CODE, TEXT) {
            devGrid.StartEditRow(editRowIndex);
            devGrid.GetEditor('F_MEAINSPCD').SetText(CODE);
            devGrid.GetEditor('F_INSPDETAIL').SetText(TEXT);
        }

        // 공정코드 조회전 품목코드 입력여부 확인
        function fn_OnPopupWorkSearchForm() {
            if (devGrid.GetEditor('F_ITEMCD').GetText() == '' || devGrid.GetEditor('F_ITEMCD').GetText() == '') {
                alert('품목을 입력하세요!!');
                devGrid.GetEditor('F_ITEMCD').Focus();
                return false;
            }

            fn_OnPopupWorkSearchBatch(devGrid.GetEditor('F_ITEMCD').GetText(), devGrid.GetEditor('F_WORKCD').GetText());
        }

        // 공정코드 입력시 명칭조회
        function fn_OnWORKCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                devGrid.GetEditor('F_WORKNM').SetValue('');
                devGrid.GetEditor('F_WORKNM').SetText('');
                devGrid.GetEditor('F_WORKNM').GetMainElement().title = '';
                return;
            } else
                devCallback.PerformCallback('WORK|' + s.GetValue());
        }

        // 검색된 공정(반/라인 포함) 세팅
        function fn_OnSettingWork(CODE, TEXT, BANCD, LINECD) {
            devGrid.StartEditRow(editRowIndex);
            devGrid.GetEditor('F_WORKCD').SetText(CODE);
            devGrid.GetEditor('F_WORKNM').SetText(TEXT);
            devGrid.GetEditor('F_BANCD').SetText(BANCD);
            devGrid.GetEditor('F_LINECD').SetText(LINECD);
        }

        // 품목, 검사항목, 공정 콜백처리
        function fn_OndevCallback(s, e) {
            var IDCD = s.cpIDCD;
            var IDNM = s.cpIDNM;

            if (IDCD != '' && IDNM != '') {
                var MDNM = s.cpMODELNM;
                var CODE = s.cpCODE;
                var TEXT = s.cpTEXT;
                var MODEL = s.cpMODEL

                var txtIDCD = devGrid.GetEditor('F_' + IDCD);
                var txtIDNM = devGrid.GetEditor('F_' + IDNM);
                var txtMODEL = MDNM == '' ? '' : devGrid.GetEditor('F_' + MDNM);

                if (CODE != '' && TEXT != '') {
                    txtIDCD.SetValue(CODE);
                    txtIDCD.SetText(CODE);
                    txtIDNM.SetValue(TEXT);
                    txtIDNM.SetText(TEXT);
                    txtIDNM.GetMainElement().title = TEXT;
                    if (txtMODEL != '') {
                        txtMODEL.SetValue(MODEL);
                        txtMODEL.SetText(MODEL);
                        txtMODEL.GetMainElement().title = MODEL;
                    }
                } else {
                    txtIDCD.SetValue('');
                    txtIDCD.SetText('');
                    txtIDNM.SetValue('');
                    txtIDNM.SetText('');
                    txtIDNM.GetMainElement().title = '';
                    if (txtMODEL != '') {
                        txtMODEL.SetValue('');
                        txtMODEL.SetText('');
                        txtMODEL.GetMainElement().title = '';
                    }
                }

                if (IDCD == 'WORKCD' && IDNM == 'WORKNM') {
                    devGrid.GetEditor('F_BANCD').SetValue(s.cpBANCD);
                    devGrid.GetEditor('F_BANCD').SetText(s.cpBANCD);
                    devGrid.GetEditor('F_LINECD').SetValue(s.cpLINECD);
                    devGrid.GetEditor('F_LINECD').SetText(s.cpLINECD);
                }
            }
        }

        // 수작업 CheckChanged
        function fn_OnGETTYPECheckedChanged(s, e) {
            if (!s.GetChecked()) {
                fn_OnControlEnableBox(devGrid.GetEditor('F_PORT'), null);
                fn_OnControlEnableBox(devGrid.GetEditor('F_CHANNEL'), null);
                fn_OnControlEnableComboBox(devGrid.GetEditor('F_PORT'), true);
            } else {
                fn_OnControlDisableBox(devGrid.GetEditor('F_PORT'), null);
                fn_OnControlDisableBox(devGrid.GetEditor('F_CHANNEL'), null);
                fn_OnControlEnableComboBox(devGrid.GetEditor('F_PORT'), false);
            }
        }

        // 계측기 선택 시 측정기기명을 입력한다
        function fn_OnAIRCKSelectedIndexChanged(s, e) {
            devGrid.GetEditor('F_MEASURE').SetText(s.GetText());
        }

        // 규격공차계산(상,하한)
        function fn_OnStandardCal(s, e) {
        }

        // 규격공차(상한공차 Claer)
        function fn_OnMaxCalClear(s, e) {
        }

        // 규격공차(하한공차 Claer)
        function fn_OnMinCalClear(s, e) {
        }

        // UCLR계산
        function fn_OnUCLRCal() {
            var txtSTANDARD = devGrid.GetEditor('F_STANDARD');
            var txtMAX = devGrid.GetEditor('F_MAX');
            var txtMIN = devGrid.GetEditor('F_MIN');
            var txtUCLX = devGrid.GetEditor('F_UCLX');
            var txtLCLX = devGrid.GetEditor('F_LCLX');
            var txtUCLR = devGrid.GetEditor('F_UCLR');

            if (txtUCLX.GetText() == '' || txtLCLX.GetText() == '') {
                txtUCLR.SetText('');
                return false;
            }

            var UCLXVal = txtUCLX.GetText();
            var LCLXVal = txtLCLX.GetText();

            var nUCLXFixLen = fn_GetDecimalPoint(UCLXVal);
            var nLCLXFixLen = fn_GetDecimalPoint(LCLXVal);

            var nFixedLen = nUCLXFixLen > nLCLXFixLen ? nUCLXFixLen : nLCLXFixLen;

            var fUCLX = parseFloat(parseFloat(UCLXVal).toFixed(nFixedLen));
            var fLCLX = parseFloat(parseFloat(LCLXVal).toFixed(nFixedLen));

            txtUCLR.SetText(Math.abs(fUCLX - fLCLX).toFixed(nFixedLen));
        }

        // 검사분류가 치수검사인 경우 규격은 Float 형만 입력가능
        function fn_OnValidateStandard(s, e) {
            if (devGrid.GetEditor('F_INSPCD').GetValue() == 'AAC501') {
                fn_ValidateOnlyFloat(s, e);
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">

                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlWORK" />
                    </div>               
                    
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>     
                    

                    <label class="col-sm-1 control-label">구분</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Inspection ID="ucInspection" runat="server" />
                    </div>

                </div>
                <div class="form-group">

                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Work ID="ucWork" runat="server" />
                    </div>
                    
                    <label class="col-sm-1 control-label">항목</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="ddlINSPITEM" ClientInstanceName="ddlINSPITEM" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                        </dx:ASPxComboBox>
                    </div>

                    
                    <label class="col-sm-1 control-label">측정여부</label>
                        <div class="col-sm-2">
                            <dx:ASPxComboBox ID="ddlMEASYESNO" ClientInstanceName="ddlMEASYESNO" runat="server" Width="50%"
                                IncrementalFilteringMode="None" CssClass="NoXButton">
                                <Items>
                                    <dx:ListEditItem Text="전체" Value="" Selected="true"/>
                                    <dx:ListEditItem Text="사용" Value="0" />
                                    <dx:ListEditItem Text="제외" Value="1" />
                                </Items>
                                <ClientSideEvents Init="fn_OnControlDisable" />
                            </dx:ASPxComboBox>
                            
                        </div>

                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hdnCurrPage" ClientInstanceName="hdnCurrPage" runat="server" ClientVisible="false" Text="0" />
            <dx:ASPxTextBox ID="hdnPageSize" ClientInstanceName="hdnPageSize" runat="server" ClientVisible="false" Text="0" />    
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_INSPCD;F_SERIALNO;F_WORKCD" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnCellEditorInitialize="devGrid_CellEditorInitialize" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnBatchUpdate="devGrid_BatchUpdate">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="False" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" BatchEditEndEditing="fn_OnBatchEditEndEditing" />
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px" FixedStyle="Left">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewBandColumn Caption="품목" FixedStyle="Left">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="코드" Width="120px">
                                <CellStyle HorizontalAlign="Left" />
                                <PropertiesTextEdit>
                                    <ClientSideEvents LostFocus="fn_OnITEMCDLostFocus" />
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="명칭" Width="150px" ReadOnly="true">
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MODELNM" Caption="기종명" Width="100px" ReadOnly="true" FixedStyle="Left">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_SERIALNO" Caption="일련<br />번호" Width="50px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_INSPCD" Caption="검사분류" Width="80px" FixedStyle="Left">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewBandColumn Caption="검사항목" FixedStyle="Left">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_MEAINSPCD" Caption="코드" Width="60px">
                                <PropertiesTextEdit>
                                    <ClientSideEvents LostFocus="fn_OnMEAINSPCDLostFocus" />
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_INSPDETAIL" Caption="명칭" Width="100px" ReadOnly="true">
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataTextColumn FieldName="F_DISPLAYNO" Caption="검사<br />순서" Width="50px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_BANCD" Caption="반" Width="40px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LINECD" Caption="라인" Width="40px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewBandColumn Caption="공정" FixedStyle="Left">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_WORKCD" Caption="코드" Width="70px">
                                <CellStyle HorizontalAlign="Left" />
                                <PropertiesTextEdit>
                                    <ClientSideEvents LostFocus="fn_OnWORKCDLostFocus" />
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_WORKNM" Caption="명칭" Width="150px" ReadOnly="true">
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="검사기준">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="규격" Width="70px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol">
                                    <ClientSideEvents ValueChanged="fn_OnStandardCal" KeyPress="fn_OnValidateStandard" />
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="상한규격" Width="70px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol">
                                    <ClientSideEvents ValueChanged="fn_OnMaxCalClear" KeyPress="fn_ValidateOnlyFloat" />
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="하한규격" Width="70px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol">
                                    <ClientSideEvents ValueChanged="fn_OnMinCalClear" KeyPress="fn_ValidateOnlyFloat" />
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn FieldName="F_ZERO" Caption="Zero" Width="50px">
                                <PropertiesCheckEdit ValueType="System.String" ValueChecked="1" ValueUnchecked="0" />
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataTextColumn FieldName="F_ZIG" Caption="보정치" Width="50px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol"></PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="품질목표">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_UCLX" Caption="UCLX" Width="70px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol">
                                    <ClientSideEvents ValueChanged="fn_OnUCLRCal" KeyPress="fn_ValidateOnlyFloat" />
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_LCLX" Caption="LCLX" Width="70px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol">
                                    <ClientSideEvents ValueChanged="fn_OnUCLRCal" KeyPress="fn_ValidateOnlyFloat" />
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_UCLR" Caption="UCLR" Width="70px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol">
                                    <ClientSideEvents KeyPress="fn_ValidateOnlyFloat" />
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_QCYCLECD" Caption="QC<br />검사주기" Width="70px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_JCYCLECD" Caption="현장<br />검사주기" Width="70px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_HCOUNT" Caption="산정<br />군수" ToolTip="UCL,LCL 산정군수" Width="40px">
                        <PropertiesSpinEdit NumberType="Integer" MinValue="0" MaxValue="99999"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_SIRYO" Caption="시료수" Width="50px">
                        <PropertiesSpinEdit NumberType="Integer" MinValue="0" MaxValue="99999"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_RANK" Caption="품질<br />수준" Width="50px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MEASURE" Caption="측정기기" Width="70px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_MEASYESNO" Caption="측정<br />제외" Width="50px">
                        <PropertiesCheckEdit ValueType="System.String" ValueChecked="1" ValueUnchecked="0" />
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_IMPORT" Caption="중요<br />항목" Width="50px">
                        <PropertiesCheckEdit ValueType="System.String" ValueChecked="1" ValueUnchecked="0" />
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_SINGLECHK" Caption="편측" Width="50px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_UNIT" Caption="공차<br />기호" Width="50px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_RESULTGUBUN" Caption="성적서<br />출력" Width="50px">
                        <PropertiesCheckEdit ValueType="System.String" ValueChecked="1" ValueUnchecked="0" />
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_SAMPLENO" Caption="성적서<br />출력<br />순서" Width="50px">
                        <PropertiesSpinEdit NumberType="Integer" MinValue="0" MaxValue="99999"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_SAMPLECHK" Caption="초중종<br />관리" Width="50px">
                        <PropertiesCheckEdit ValueType="System.String" ValueChecked="1" ValueUnchecked="0" />
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_ACCEPT_SEQ" Caption="관리한계<br />기준" Width="70px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewBandColumn Caption="측정기준">
                        <Columns>
                            <dx:GridViewDataComboBoxColumn FieldName="F_AIRCK" Caption="계측기" Width="120px">
                                <PropertiesComboBox ClientSideEvents-SelectedIndexChanged="fn_OnAIRCKSelectedIndexChanged" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataCheckColumn FieldName="F_GETTYPE" Caption="수작업" Width="50px">
                                <PropertiesCheckEdit ValueType="System.String" ValueChecked="1" ValueUnchecked="0"
                                    ClientSideEvents-CheckedChanged="fn_OnGETTYPECheckedChanged" />
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="F_PORT" Caption="측정포트" Width="70px">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="F_CHANNEL" Caption="측정채널" Width="70px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol"></PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="F_GETDATA" Caption="측정방법" Width="70px">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="F_LOADTF" Caption="설비구분" Width="70px">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="F_DEFECTS_N" Caption="측정횟수" Width="70px">
                                <PropertiesTextEdit MaskSettings-IncludeLiterals="DecimalSymbol"></PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MEASCD1" Caption="측정<br />매핑코드" Width="100px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OndevCallback" />
            </dx:ASPxCallback>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="100" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
