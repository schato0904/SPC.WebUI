<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0303POP.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.Popup.BSIF0303POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var originMaxVal = '';
        var originMinVal = '';

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight, 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
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
            if (!devGrid.IsNewRowEditing() && !devGrid.IsEditing()) {
                alert('신규등록 되거나 수정된 데이타가 없습니다');
                return false;
            }

            if (false == fn_OnValidate()) return false;

            // 상,하한 규격입력된 경우
            // UCLX, LCLX 입력안된 경우
            // 상,하한 규격으로 입력저장한다
            var bCalUCLR = false;
            if (txtMAX.GetText() != '' && txtUCLX.GetText() == '') {
                txtUCLX.SetText(txtMAX.GetText());
                bCalUCLR = true;
            }

            if (txtMIN.GetText() != '' && txtLCLX.GetText() == '') {
                txtLCLX.SetText(txtMIN.GetText());
                bCalUCLR = true;
            }

            // UCLX 또는 LCLX 값이 변경되는 경우
            // UCLR 을 다시 산정한다
            fn_OnUCLRCal();

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

            // 수정모드인 경우
            if (devGrid.IsNewRowEditing() || devGrid.IsEditing()) {
                fn_GETTYPECheckedChanged(chkGETTYPE, null);
                originMaxVal = txtMAX.GetText();
                originMinVal = txtMIN.GetText();
            }
            
            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else if (s.cpResultCode == '2') {
                    fn_doSetGridEventAction('true');
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                    //devGrid.PerformCallback();
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

        // Validate
        function fn_OnValidate() {
            if (!ddlINSPCD.GetValue() || ddlINSPCD.GetValue() == '') {
                alert('검사분류를 선택하세요!!');
                ddlINSPCD.Focus();
                return false;
            }
            if (txtMEAINSPCD.GetText() == '' || txtINSPDETAIL.GetText() == '') {
                alert('검사항목을 입력하세요!!');
                txtMEAINSPCD.Focus();
                return false;
            }
            if (txtWORKCD.GetText() == '' || txtWORKNM.GetText() == '') {
                alert('공정을 입력하세요!!');
                txtWORKCD.Focus();
                return false;
            }
            if (txtSTANDARD.GetText() == '') {
                alert('검사기준규격을 입력하세요!!');
                txtSTANDARD.Focus();
                return false;
            }
            if (txtSIRYO.GetText() == '') {
                alert('시료수를 입력하세요!!');
                txtSIRYO.Focus();
                return false;
            } else if (parseInt(txtSIRYO.GetText(), 10) < 1) {
                alert('시료수는 1 이상으로 입력하세요!!');
                txtSIRYO.SetText('1');
                txtSIRYO.Focus();
                return false;
            }
            if (ddlGETDATA.GetValue() == '') {
                alert('측정방법을 선택하세요!!');
                ddlGETDATA.Focus();
                return false;
            }
            if (txtDEFECTS_N.GetText() == '') {
                alert('측정횟수를 입력하세요!!');
                txtDEFECTS_N.Focus();
                return false;
            } else if (parseInt(txtDEFECTS_N.GetText(), 10) < 1) {
                alert('측정횟수는 1 이상으로 입력하세요!!');
                txtDEFECTS_N.SetText('1');
                txtDEFECTS_N.Focus();
                return false;
            }
        }

        // Grid RowDblClick
        function fn_OnRowDblClick(s, e) {
            devGrid.StartEditRow(e.visibleIndex);
        }

        // 수작업 CheckChanged
        function fn_GETTYPECheckedChanged(s, e) {
            if (!s.GetChecked()) {
                //fn_OnControlEnableBox(ddlAIRCK, e);
                fn_OnControlEnableBox(ddlPORT, e);
                fn_OnControlEnableBox(txtCHANNEL, e);
                //fn_OnControlEnableBox(ddlGETDATA, e);

                //fn_OnControlEnableComboBox(ddlAIRCK, true);
                fn_OnControlEnableComboBox(ddlPORT, true);
                //fn_OnControlEnableComboBox(ddlGETDATA, true);
            } else {
                //fn_OnControlDisableBox(ddlAIRCK, e);
                fn_OnControlDisableBox(ddlPORT, e);
                fn_OnControlDisableBox(txtCHANNEL, e);
                //fn_OnControlDisableBox(ddlGETDATA, e);

                //fn_OnControlEnableComboBox(ddlAIRCK, false);
                fn_OnControlEnableComboBox(ddlPORT, false);
                //fn_OnControlEnableComboBox(ddlGETDATA, false);
            }
        }

        // 규격공차계산(상,하한)
        function fn_OnStandardCal(s, e) {
            originMaxVal = txtMAX.GetText();
            originMinVal = txtMIN.GetText();

            if (txtSTANDARD.GetText() != '' && txtMAXCAR.GetText() != '') {
                var StandardVal = txtSTANDARD.GetText();
                var ToleranceVal = txtMAXCAR.GetText();

                var nStandardFixLen = fn_GetDecimalPoint(StandardVal);
                var nToleranceFixLen = fn_GetDecimalPoint(ToleranceVal);

                var nFixedLen = nStandardFixLen > nToleranceFixLen ? nStandardFixLen : nToleranceFixLen;

                var fStandard = parseFloat(parseFloat(StandardVal).toFixed(nFixedLen));
                var fTolerance = parseFloat(parseFloat(ToleranceVal).toFixed(nFixedLen));

                //if (parseFloat('0').toFixed(nFixedLen) > fTolerance)
                //    fTolerance = parseFloat('0').toFixed(nFixedLen) - fTolerance;

                txtMAX.SetText((fStandard + fTolerance).toFixed(nFixedLen));
            } else {
                txtMAX.SetText(originMaxVal);
            }

            if (txtSTANDARD.GetText() != '' && txtMINCAR.GetText() != '') {
                var StandardVal = txtSTANDARD.GetText();
                var ToleranceVal = txtMINCAR.GetText();

                var nStandardFixLen = fn_GetDecimalPoint(StandardVal);
                var nToleranceFixLen = fn_GetDecimalPoint(ToleranceVal);

                var nFixedLen = nStandardFixLen > nToleranceFixLen ? nStandardFixLen : nToleranceFixLen;

                var fStandard = parseFloat(parseFloat(StandardVal).toFixed(nFixedLen));
                var fTolerance = parseFloat(parseFloat(ToleranceVal).toFixed(nFixedLen));

                //if (parseFloat('0').toFixed(nFixedLen) > fTolerance)
                //    fTolerance = parseFloat('0').toFixed(nFixedLen) - fTolerance;

                txtMIN.SetText((fStandard + fTolerance).toFixed(nFixedLen));
            } else {
                txtMIN.SetText(originMinVal);
            }

            // UCLR계산
            fn_OnUCLRCal();
        }

        // 규격공차계산(상한)
        function fn_OnMaxCal(s, e) {

            if (txtSTANDARD.GetText() != '' && txtMAXCAR.GetText() != '') {
                var StandardVal = txtSTANDARD.GetText();
                var ToleranceVal = txtMAXCAR.GetText();

                var nStandardFixLen = fn_GetDecimalPoint(StandardVal);
                var nToleranceFixLen = fn_GetDecimalPoint(ToleranceVal);

                var nFixedLen = nStandardFixLen > nToleranceFixLen ? nStandardFixLen : nToleranceFixLen;

                var fStandard = parseFloat(parseFloat(txtSTANDARD.GetText()).toFixed(nFixedLen));
                var fTolerance = parseFloat(parseFloat(txtMAXCAR.GetText()).toFixed(nFixedLen));

                //if (parseFloat('0').toFixed(nFixedLen) > fTolerance)
                //    fTolerance = parseFloat('0').toFixed(nFixedLen) - parseFloat('0').toFixed(nFixedLen);

                txtMAX.SetText((fStandard + fTolerance).toFixed(nFixedLen));
            } else {
                txtMAX.SetText(originMaxVal);
            }

            // UCLR계산
            //fn_OnUCLRCal();
        }

        // 규격공차계산(하한)
        function fn_OnMinCal(s, e) {

            if (txtSTANDARD.GetText() != '' && txtMINCAR.GetText() != '') {
                var StandardVal = txtSTANDARD.GetText();
                var ToleranceVal = txtMINCAR.GetText();

                var nStandardFixLen = fn_GetDecimalPoint(StandardVal);
                var nToleranceFixLen = fn_GetDecimalPoint(ToleranceVal);

                var nFixedLen = nStandardFixLen > nToleranceFixLen ? nStandardFixLen : nToleranceFixLen;

                var fStandard = parseFloat(parseFloat(StandardVal).toFixed(nFixedLen));
                var fTolerance = parseFloat(parseFloat(ToleranceVal).toFixed(nFixedLen));

                //if (parseFloat('0').toFixed(nFixedLen) > fTolerance)
                //    fTolerance = parseFloat('0').toFixed(nFixedLen) - parseFloat('0').toFixed(nFixedLen);

                txtMIN.SetText((fStandard + fTolerance).toFixed(nFixedLen));
            } else {
                txtMIN.SetText(originMinVal);
            }

            // UCLR계산
            //fn_OnUCLRCal();
        }

        // 규격공차(상한공차 Claer)
        function fn_OnMaxCalClear(s, e) {
            //if (s.GetText() != '') {
            //    txtUCLX.SetText(s.GetText());

            //    // UCLR계산
            //    fn_OnUCLRCal();
            //}

            txtMAXCAR.SetText('');
        }

        // 규격공차(하한공차 Claer)
        function fn_OnMinCalClear(s, e) {
            //if (s.GetText() != '') {
            //    txtLCLX.SetText(s.GetText());

            //    // UCLR계산
            //    fn_OnUCLRCal();
            //}

            txtMINCAR.SetText('');
        }

        // UCLR계산
        function fn_OnUCLRCal() {
            //if (txtMAX.GetText() != '')
            //    txtUCLX.SetText(txtMAX.GetText());

            //if (txtMIN.GetText() != '')
            //    txtLCLX.SetText(txtMIN.GetText());

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

        // 계측기 선택 시 측정기기명을 입력한다
        function fn_OnAIRCKSelectedIndexChanged(s, e) {
            txtMEASURE.SetText(s.GetText());
        }

        // 검사항목코드 입력시 명칭조회
        function fn_OnMEAINSPCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                txtINSPDETAIL.SetValue('');
                txtINSPDETAIL.SetText('');
                txtINSPDETAIL.GetMainElement().title = '';
                return;
            } else
                devCallback.PerformCallback('MEAINSP|' + s.GetValue());
        }

        // 검색된 검사항목 세팅
        function fn_OnSettingMeainsp(CODE, TEXT) {
            txtMEAINSPCD.SetText(CODE);
            txtINSPDETAIL.SetText(TEXT);
        }

        // 공정코드 입력시 명칭조회
        function fn_OnWORKCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                txtWORKNM.SetValue('');
                txtWORKNM.SetText('');
                txtWORKNM.GetMainElement().title = '';
                return;
            } else
                devCallback.PerformCallback('WORK|' + s.GetValue());
        }

        // 검색된 공정(반/라인 포함) 세팅
        function fn_OnSettingWork(CODE, TEXT, BANCD, LINECD) {
            txtWORKCD.SetText(CODE);
            txtWORKNM.SetText(TEXT);
            txtBANCD.SetText(BANCD);
            txtLINECD.SetText(LINECD);
        }

        // 검사항목, 공정 콜백처리
        function fn_OndevCallback(s, e) {
            var IDCD = s.cpIDCD;
            var IDNM = s.cpIDNM;

            if (IDCD != '' && IDNM != '') {
                var CODE = s.cpCODE;
                var TEXT = s.cpTEXT;

                var txtIDCD = ASPxClientControl.Cast("txt" + IDCD);
                var txtIDNM = ASPxClientControl.Cast("txt" + IDNM);

                if (CODE != '' && TEXT != '') {
                    txtIDCD.SetValue(CODE);
                    txtIDCD.SetText(CODE);
                    txtIDNM.SetValue(TEXT);
                    txtIDNM.SetText(TEXT);
                    txtIDNM.GetMainElement().title = TEXT;
                } else {
                    txtIDCD.SetValue('');
                    txtIDCD.SetText('');
                    txtIDNM.SetValue('');
                    txtIDNM.SetText('');
                    txtIDNM.GetMainElement().title = '';
                }

                if (IDCD == 'WORKCD' && IDNM == 'WORKNM') {
                    fn_SetTextValue(ASPxClientControl.Cast("txtBANCD"), s.cpBANCD);
                    fn_SetTextValue(ASPxClientControl.Cast("txtLINECD"), s.cpLINECD);
                }
            }
        }

        // 검사분류가 치수검사인 경우 규격은 Float 형만 입력가능
        function fn_OnValidateStandard(s, e) {
            if (!ddlINSPCD.GetValue() || ddlINSPCD.GetValue() == '') {
                alert('검사분류를 선택하세요!!');
                ddlINSPCD.Focus();
                return false;
            }

            if (ddlINSPCD.GetValue() == 'AAC501') {
                fn_ValidateOnlyFloat(s, e);
            }

            return true;
        }

        function fn_OnControlDisable_Inspcd(s, e) {
            if (txtSERIALNO.GetValue() != null) {
                var inputElement = s.GetInputElement();
                inputElement.disabled = true;
                inputElement.readonly = true;
                inputElement.style.backgroundColor = '#cccccc';
                var mainElement = s.GetMainElement();
                mainElement.style.backgroundColor = '#cccccc';
                s.SetEnabled(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <section class="panel panel-default" style="height: 100%;">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_INSPCD;F_SERIALNO;F_WORKCD" EnableViewState="false" EnableRowsCache="false"
                    OnInitNewRow="devGrid_InitNewRow" OnHtmlEditFormCreated="devGrid_HtmlEditFormCreated"
                    OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                    OnRowInserting="devGrid_RowInserting" OnRowUpdating="devGrid_RowUpdating" OnRowDeleting="devGrid_RowDeleting">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                        <Footer CssClass="m-l-none m-r-none m-t-none m-b-none wrapper-xxs"></Footer>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" ShowFooter="true" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px" FixedStyle="Left">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                    ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="140px" FixedStyle="Left">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="검사분류" Width="70px"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="150px">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_DISPLAYNO" Caption="검사<br />순서" Width="50px"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewBandColumn Name="F_INSPECTION_STANDARDS" Caption="검사기준">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Width="50px">
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한값" Width="50px">
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한값" Width="50px">
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_ZERO" Caption="Zero" Width="40px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_ZIG" Caption="보정치" Width="70px"></dx:GridViewDataColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Name="F_INSPECTION_METHOD" Caption="검사방법">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_SIRYO" Caption="시료수" Width="50px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RANKNM" Caption="수준" Width="40px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정기기" Width="130px">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MEASYESNO" Caption="측정제외" Width="70px"></dx:GridViewDataColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataColumn FieldName="F_SERIALNO" Caption="일련<br />번호" Width="50px"></dx:GridViewDataColumn>
                        <%--여기서부터 히든 필드--%>
                        <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_MODELNM" Caption="모델명" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="검사항목코드" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_DATAUNIT" Caption="" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="검사분류코드" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_QCYCLECD" Caption="QC검사주기" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_JCYCLECD" Caption="현장검사주기" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_RANK" Caption="품질수준" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_IMPORT" Caption="중요항목" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_SINGLECHK" Caption="상,하한편측" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_UNIT" Caption="공차기호" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_AIRCK" Caption="계측기" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_GETTYPE" Caption="수작업여부" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_PORT" Caption="측정포트" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_GETDATA" Caption="측정방법" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_LOADTF" Caption="설비구분" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_ACCEPT_SEQ" Caption="관리한계기준" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_RESULTGUBUN" Caption="성적서출력유무" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_MEASCD1" Caption="측정매핑코드" Visible="false"></dx:GridViewDataColumn>
                    </Columns>
                    <Templates>
                        <EditForm>
                            <div class="form-horizontal divEditForm">
                                <div class="form-group">
                                    <p class="bg-info text-center"><span class="font-bold">검사기준 기본정보</span></p>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">품목코드</label>
                                    <div class="col-sm-2">
                                        <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_ITEMCD]") %>'>
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                    <label class="col-sm-1 control-label">품목명</label>
                                    <div class="col-sm-2">
                                        <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_ITEMNM]") %>'>
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                    <label class="col-sm-1 control-label">기종명</label>
                                    <div class="col-sm-2">
                                        <dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtMODELNM" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_MODELNM]") %>'>
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                    <label class="col-sm-1 control-label">일련번호</label>
                                    <div class="col-sm-2">
                                        <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_SERIALNO]") %>'>
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">검사분류</label>
                                    <div class="col-sm-2">
                                        <dx:ASPxComboBox ID="ddlINSPCD" ClientInstanceName="ddlINSPCD" runat="server" Width="100%"
                                            OnDataBound="ddlComboBox_DataBound"
                                            IncrementalFilteringMode="None" CssClass="NoXButton">
                                            <ClientSideEvents Init="fn_OnControlDisable_Inspcd" />
                                        </dx:ASPxComboBox>
                                    </div>
                                    <label class="col-sm-1 control-label">검사항목</label>
                                    <div class="col-sm-2">
                                        <div style="float: left; width: 40%; padding-right: 3px;">
                                            <dx:ASPxTextBox ID="txtMEAINSPCD" ClientInstanceName="txtMEAINSPCD" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_MEAINSPCD]") %>'
                                                OnInit="txtMEAINSPCD_Init">
                                                <ClientSideEvents LostFocus="fn_OnMEAINSPCDLostFocus" />
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div style="float: left; width: 60%;">
                                            <dx:ASPxTextBox ID="txtINSPDETAIL" ClientInstanceName="txtINSPDETAIL" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_INSPDETAIL]") %>'>
                                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </div>
                                    </div>
                                    <label class="col-sm-1 control-label">검사순서</label>
                                    <div class="col-sm-1">
                                        <dx:ASPxTextBox ID="txtDISPLAYNO" ClientInstanceName="txtDISPLAYNO" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_DISPLAYNO]") %>' MaxLength="4">
                                            <ClientSideEvents KeyPress="fn_ValidateOnlyNumber" />
                                        </dx:ASPxTextBox>
                                    </div>
                                    <label class="col-sm-1 control-label">공정</label>
                                    <div class="col-sm-3">
                                        <div style="float: left; width: 40%; padding-right: 3px;">
                                            <dx:ASPxTextBox ID="txtBANCD" ClientInstanceName="txtBANCD" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_BANCD]") %>' ClientVisible="false" />
                                            <dx:ASPxTextBox ID="txtLINECD" ClientInstanceName="txtLINECD" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_LINECD]") %>' ClientVisible="false" />
                                            <dx:ASPxTextBox ID="txtWORKCD" ClientInstanceName="txtWORKCD" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_WORKCD]") %>'
                                                OnInit="txtWORKCD_Init">
                                                <ClientSideEvents LostFocus="fn_OnWORKCDLostFocus" />
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div style="float: left; width: 60%;">
                                            <dx:ASPxTextBox ID="txtWORKNM" ClientInstanceName="txtWORKNM" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_WORKNM]") %>'>
                                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <p class="bg-info text-center"><span class="font-bold">검사기준</span></p>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">규격</label>
                                            <div class="col-sm-8">
                                                <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_STANDARD]") %>' HorizontalAlign="Right">
                                                    <MaskSettings IncludeLiterals="DecimalSymbol" />
                                                    <ClientSideEvents ValueChanged="fn_OnStandardCal" KeyPress="fn_OnValidateStandard" />
                                                </dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">상한값</label>
                                            <div class="col-sm-8">
                                                <div style="float: left; width: 50%; padding-right: 3px;">
                                                    <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_MAX]") %>' HorizontalAlign="Right" NullText="상한규격">
                                                        <MaskSettings IncludeLiterals="DecimalSymbol" />
                                                        <ClientSideEvents ValueChanged="fn_OnMaxCalClear" KeyPress="fn_ValidateOnlyFloat" />
                                                    </dx:ASPxTextBox>
                                                </div>
                                                <div style="float: left; width: 50%;">
                                                    <dx:ASPxTextBox ID="txtMAXCAR" ClientInstanceName="txtMAXCAR" runat="server" Width="100%" class="form-control input-sm" HorizontalAlign="Right" NullText="상한공차">
                                                        <MaskSettings IncludeLiterals="DecimalSymbol" />
                                                        <ClientSideEvents ValueChanged="fn_OnMaxCal" KeyPress="fn_ValidateOnlyFloatAbs" />
                                                    </dx:ASPxTextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">하한값</label>
                                            <div class="col-sm-8">
                                                <div style="float: left; width: 50%; padding-right: 3px;">
                                                    <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_MIN]") %>' HorizontalAlign="Right" NullText="하한규격">
                                                        <MaskSettings IncludeLiterals="DecimalSymbol" />
                                                        <ClientSideEvents ValueChanged="fn_OnMinCalClear" KeyPress="fn_ValidateOnlyFloat" />
                                                    </dx:ASPxTextBox>
                                                </div>
                                                <div style="float: left; width: 50%;">
                                                    <dx:ASPxTextBox ID="txtMINCAR" ClientInstanceName="txtMINCAR" runat="server" Width="100%" class="form-control input-sm" HorizontalAlign="Right" NullText="하한공차">
                                                        <MaskSettings IncludeLiterals="DecimalSymbol" />
                                                        <ClientSideEvents ValueChanged="fn_OnMinCal" KeyPress="fn_ValidateOnlyFloat" />
                                                    </dx:ASPxTextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <dx:ASPxCheckBox ID="chkZERO" ClientInstanceName="chkZERO" runat="server" Text="Zero Setting" />
                                            </div>
                                            <label class="col-sm-3 control-label">보정치</label>
                                            <div class="col-sm-3">
                                                <dx:ASPxTextBox ID="txtZIG" ClientInstanceName="txtZIG" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_ZIG]") %>' HorizontalAlign="Right">
                                                    <masksettings includeliterals="DecimalSymbol" />
                                                </dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display: none;">
                                            <div class="col-sm-12">
                                                <dx:ASPxRadioButtonList ID="rdoDATAUNIT" ClientInstanceName="rdoDATAUNIT" runat="server" ClientVisible="false"
                                                    OnDataBound="rdoButtonList_DataBound"
                                                    RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                                    <Items>
                                                        <dx:ListEditItem Value="0" Text="x1" />
                                                        <dx:ListEditItem Value="1" Text="/1000" />
                                                        <dx:ListEditItem Value="2" Text="x1000" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </div>
                                        </div>
                                        <p class="bg-info text-center line"><span class="font-bold"></span></p>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">QC검사주기</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxComboBox ID="ddlQCYCLECD" ClientInstanceName="ddlQCYCLECD" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">현장검사주기</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxComboBox ID="ddlJCYCLECD" ClientInstanceName="ddlJCYCLECD" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">UCL,LCL산정군수</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxSpinEdit ID="txtHCOUNT" ClientInstanceName="txtHCOUNT" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_HCOUNT]") %>'
                                                    NumberType="Integer" MinValue="0" MaxValue="99999" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <p class="bg-info text-center"><span class="font-bold">검사항목</span></p>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">시료수</label>
                                            <div class="col-sm-8">
                                                <dx:ASPxSpinEdit ID="txtSIRYO" ClientInstanceName="txtSIRYO" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_SIRYO]") %>'
                                                    NumberType="Integer" MinValue="0" MaxValue="99999" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">품질수준</label>
                                            <div class="col-sm-8">
                                                <dx:ASPxComboBox ID="ddlRANK" ClientInstanceName="ddlRANK" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group none">
                                            <label class="col-sm-4 control-label">측정기기</label>
                                            <div class="col-sm-8">
                                                <dx:ASPxTextBox ID="txtMEASURE" ClientInstanceName="txtMEASURE" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_MEASURE]") %>' />
                                            </div>
                                        </div>
                                        <p class="bg-info text-center line"><span class="font-bold"></span></p>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <dx:ASPxCheckBox ID="chkMEASYESNO" ClientInstanceName="chkMEASYESNO" runat="server" Text="측정제외" />
                                            </div>
                                            <div class="col-sm-6">
                                                <dx:ASPxCheckBox ID="chkIMPORT" ClientInstanceName="chkIMPORT" runat="server" Text="중요항목" />
                                            </div>
                                        </div>
                                        <p class="bg-info text-center line"><span class="font-bold"></span></p>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">편측</label>
                                            <div class="col-sm-7">
                                                <dx:ASPxComboBox ID="ddlSINGLECHK" ClientInstanceName="ddlSINGLECHK" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">공차기호</label>
                                            <div class="col-sm-7">
                                                <dx:ASPxComboBox ID="ddlUNIT" ClientInstanceName="ddlUNIT" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group none">
                                            <label class="col-sm-5 control-label">설비규격공차</label>
                                            <div class="col-sm-7">
                                                <dx:ASPxComboBox ID="ddlMACHCD" ClientInstanceName="ddlMACHCD" runat="server" Width="100%"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">도면이미지</label>
                                            <div class="col-sm-7">
                                                <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" Text='<%# Bind("[F_IMAGESEQ]") %>' ClientVisible="false" />
                                                <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpen('검사성적서관리', 'E', 'F'); return false;">
                                                    <i class="i i-file-plus "></i>
                                                    <span class="text">도면이미지관리</span>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">성적서출력</label>
                                            <div class="col-sm-7">
                                                <dx:ASPxRadioButtonList ID="rdoRESULTGUBUN" ClientInstanceName="rdoRESULTGUBUN" runat="server"
                                                    OnDataBound="rdoButtonList_DataBound"
                                                    RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                                    <Items>
                                                        <dx:ListEditItem Value="1" Text="유" />
                                                        <dx:ListEditItem Value="0" Text="무" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label">성적서출력순서</label>
                                            <div class="col-sm-7">
                                                <dx:ASPxSpinEdit ID="txtSAMPLENO" ClientInstanceName="txtSAMPLENO" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_SAMPLENO]") %>'
                                                    NumberType="Integer" MinValue="0" MaxValue="99999" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <p class="bg-info text-center"><span class="font-bold">품질목표</span></p>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">UCLX</label>
                                            <div class="col-sm-3">
                                                <dx:ASPxTextBox ID="txtUCLX" ClientInstanceName="txtUCLX" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_UCLX]") %>' HorizontalAlign="Right">
                                                    <masksettings includeliterals="DecimalSymbol" />
                                                    <ClientSideEvents ValueChanged="fn_OnUCLRCal" KeyPress="fn_ValidateOnlyFloatAbs" />
                                                </dx:ASPxTextBox>
                                            </div>
                                            <label class="col-sm-3 control-label">전송상한</label>
                                            <div class="col-sm-3">
                                                <dx:ASPxTextBox ID="txtTMAX" ClientInstanceName="txtTMAX" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_TMAX]") %>' HorizontalAlign="Right">
                                                    <masksettings includeliterals="DecimalSymbol" />
                                                    <ClientSideEvents KeyPress="fn_ValidateOnlyFloatAbs" />
                                                </dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">LCLX</label>
                                            <div class="col-sm-3">
                                                <dx:ASPxTextBox ID="txtLCLX" ClientInstanceName="txtLCLX" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_LCLX]") %>' HorizontalAlign="Right">
                                                    <masksettings includeliterals="DecimalSymbol" />
                                                    <ClientSideEvents ValueChanged="fn_OnUCLRCal" KeyPress="fn_ValidateOnlyFloat" />
                                                </dx:ASPxTextBox>
                                            </div>
                                            <label class="col-sm-3 control-label">전송하한</label>
                                            <div class="col-sm-3">
                                                <dx:ASPxTextBox ID="txtTMIN" ClientInstanceName="txtTMIN" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_TMIN]") %>' HorizontalAlign="Right">
                                                    <masksettings includeliterals="DecimalSymbol" />
                                                    <ClientSideEvents KeyPress="fn_ValidateOnlyFloat" />
                                                </dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">UCLR</label>
                                            <div class="col-sm-3">
                                                <dx:ASPxTextBox ID="txtUCLR" ClientInstanceName="txtUCLR" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_UCLR]") %>' HorizontalAlign="Right">
                                                    <masksettings includeliterals="DecimalSymbol" />
                                                    <ClientSideEvents KeyPress="fn_ValidateOnlyFloat" />
                                                </dx:ASPxTextBox>
                                            </div>
                                            <label class="col-sm-6 control-label"></label>
                                        </div>
                                        <p class="bg-info text-center"><span class="font-bold">관리한계기준</span></p>
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <dx:ASPxRadioButtonList ID="rdoACCEPT" ClientInstanceName="rdoACCEPT" runat="server"
                                                    OnDataBound="rdoButtonList_DataBound"
                                                    RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                                </dx:ASPxRadioButtonList>
                                            </div>
                                        </div>
                                        <p class="bg-info text-center"><span class="font-bold">전송관리</span></p>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">전송유무</label>
                                            <div class="col-sm-3">
                                                <dx:ASPxRadioButtonList ID="rdoHIPISNG" ClientInstanceName="rdoHIPISNG" runat="server"
                                                    OnDataBound="rdoButtonList_DataBound"
                                                    RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                                    <Items>
                                                        <dx:ListEditItem Value="1" Text="유" />
                                                        <dx:ListEditItem Value="0" Text="무" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </div>
                                            <label class="col-sm-3 control-label">검사코드</label>
                                            <div class="col-sm-3">
                                                <dx:ASPxTextBox ID="txtRESULTSTAND" ClientInstanceName="txtRESULTSTAND" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_RESULTSTAND]") %>' />
                                            </div>
                                        </div>
                                        <p class="bg-info text-center"><span class="font-bold">초중종관리</span></p>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">사용여부</label>
                                            <div class="col-sm-9">
                                                <dx:ASPxRadioButtonList ID="rdoSAMPLECHK" ClientInstanceName="rdoSAMPLECHK" runat="server"
                                                    OnDataBound="rdoButtonList_DataBound"
                                                    RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                                    <Items>
                                                        <dx:ListEditItem Value="1" Text="사용" />
                                                        <dx:ListEditItem Value="0" Text="미사용" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <p class="bg-info text-center"><span class="font-bold">측정기준</span></p>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">계측기</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxComboBox ID="ddlAIRCK" ClientInstanceName="ddlAIRCK" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_OnAIRCKSelectedIndexChanged" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label"></label>
                                            <div class="col-sm-6 text-right">
                                                <dx:ASPxCheckBox ID="chkGETTYPE" ClientInstanceName="chkGETTYPE" runat="server" Text="수작업">
                                                    <ClientSideEvents CheckedChanged="fn_GETTYPECheckedChanged" />
                                                </dx:ASPxCheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">측정포트</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxComboBox ID="ddlPORT" ClientInstanceName="ddlPORT" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">측정채널</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxSpinEdit ID="txtCHANNEL" ClientInstanceName="txtCHANNEL" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_CHANNEL]") %>'
                                                    NumberType="Integer" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">측정방법</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxComboBox ID="ddlGETDATA" ClientInstanceName="ddlGETDATA" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">설비구분</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxComboBox ID="ddlLOADTF" ClientInstanceName="ddlLOADTF" runat="server" Width="100%"
                                                    OnDataBound="ddlComboBox_DataBound"
                                                    IncrementalFilteringMode="None" CssClass="NoXButton">
                                                    <ClientSideEvents Init="fn_OnControlDisable" />
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">측정횟수</label>
                                            <div class="col-sm-6">
                                                <dx:ASPxSpinEdit ID="txtDEFECTS_N" ClientInstanceName="txtDEFECTS_N" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_DEFECTS_N]") %>'
                                                    NumberType="Integer" MinValue="0" MaxValue="99999" />
                                            </div>
                                        </div>
                                        <p class="bg-info text-center"><span class="font-bold">측정매핑코드</span></p>
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <dx:ASPxTextBox ID="txtMEASCD1" ClientInstanceName="txtMEASCD1" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_MEASCD1]") %>' />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </EditForm>
                    </Templates>
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="F_ITEMCD" ShowInColumn="F_ITEMCD" DisplayFormat="Total {0} Items" SummaryType="Count" />
                    </TotalSummary>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                    <ClientSideEvents EndCallback="fn_OndevCallback" />
                </dx:ASPxCallback>
            </section>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
