<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0303_EXCEL.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0303_EXCEL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        
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
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            //if (!devGrid.IsNewRowEditing() && !devGrid.IsEditing()) {
            //    alert('신규등록 되거나 수정된 데이타가 없습니다');
            //    return false;
            //}

            //if (false == fn_OnValidate()) return false;

            devGrid.PerformCallback('insert');
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
            window.open('./Export/BSIF0303EXPORT.aspx?pBANCD=' + fn_GetCastValue('hidBANCD') + '&pITEMCD=' + fn_GetCastValue('hidUCITEMCD') + '&pWORKCD=' + fn_GetCastValue('hidWORKCD') + '&pINSPCD=' + fn_GetCastValue('hidINSPECTION'));
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

            // EditForm 크기를 조정한다
            if ($(".divEditForm").length) {
                var editFormWidth = $("#cphBody_devGrid").width() - 34 - scrollbarWidth();
                editFormWidth = editFormWidth > parseInt(hidGridColumnsWidth.GetText(), 10) ? hidGridColumnsWidth.GetText() : editFormWidth;
                $(".divEditForm").width(editFormWidth);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            if (txtITEMCD.GetText() == '' || txtITEMNM.GetText() == '') {
                alert('품목을 입력하세요!!');
                txtITEMCD.Focus();
                return false;
            }
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

        function fn_OnRowDblClick(s, e) {
            var pPage = './Popup/BSIF0303_1POP.aspx?TITLE=검사기준관리&CRUD=CRDSZ&KEYFIELDS=' + encodeURIComponent(devGrid.GetRowKey(e.visibleIndex)) +
                '|' + fn_GetCastText('hidWORKCD') +
                '|' + fn_GetCastText('hidINSPECTION') + '|S';
            fn_OnPopupOpen(pPage, 0, 0);
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
            if (txtSTANDARD.GetText() != '' && txtMAXCAR.GetText() != '') {
                var StandardVal = txtSTANDARD.GetText();
                var ToleranceVal = txtMAXCAR.GetText();

                var nStandardFixLen = fn_GetDecimalPoint(StandardVal);
                var nToleranceFixLen = fn_GetDecimalPoint(ToleranceVal);

                var nFixedLen = nStandardFixLen > nToleranceFixLen ? nStandardFixLen : nToleranceFixLen;

                var fStandard = parseFloat(parseFloat(StandardVal).toFixed(nFixedLen));
                var fTolerance = parseFloat(parseFloat(ToleranceVal).toFixed(nFixedLen));

                if (parseFloat('0').toFixed(nFixedLen) > fTolerance)
                    fTolerance = parseFloat('0').toFixed(nFixedLen) - parseFloat('0').toFixed(nFixedLen);

                txtMAX.SetText((fStandard + fTolerance).toFixed(nFixedLen));
            }

            if (txtSTANDARD.GetText() != '' && txtMINCAR.GetText() != '') {
                var StandardVal = txtSTANDARD.GetText();
                var ToleranceVal = txtMINCAR.GetText();

                var nStandardFixLen = fn_GetDecimalPoint(StandardVal);
                var nToleranceFixLen = fn_GetDecimalPoint(ToleranceVal);

                var nFixedLen = nStandardFixLen > nToleranceFixLen ? nStandardFixLen : nToleranceFixLen;

                var fStandard = parseFloat(parseFloat(StandardVal).toFixed(nFixedLen));
                var fTolerance = parseFloat(parseFloat(ToleranceVal).toFixed(nFixedLen));

                if (parseFloat('0').toFixed(nFixedLen) > fTolerance)
                    fTolerance = parseFloat('0').toFixed(nFixedLen) - parseFloat('0').toFixed(nFixedLen);

                txtMAX.SetText((fStandard - fTolerance).toFixed(nFixedLen));
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

                if (parseFloat('0').toFixed(nFixedLen) > fTolerance)
                    fTolerance = parseFloat('0').toFixed(nFixedLen) - parseFloat('0').toFixed(nFixedLen);

                txtMAX.SetText((fStandard + fTolerance).toFixed(nFixedLen));
            }

            // UCLR계산
            fn_OnUCLRCal();
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

                if (parseFloat('0').toFixed(nFixedLen) > fTolerance)
                    fTolerance = parseFloat('0').toFixed(nFixedLen) - parseFloat('0').toFixed(nFixedLen);

                txtMIN.SetText((fStandard - fTolerance).toFixed(nFixedLen));
            }

            // UCLR계산
            fn_OnUCLRCal();
        }

        // 규격공차(상한공차 Claer)
        function fn_OnMaxCalClear(s, e) {
            if (s.GetText() != '') {
                txtUCLX.SetText(s.GetText());

                // UCLR계산
                fn_OnUCLRCal();
            }

            txtMAXCAR.SetText('');
        }

        // 규격공차(하한공차 Claer)
        function fn_OnMinCalClear(s, e) {
            if (s.GetText() != '') {
                txtLCLX.SetText(s.GetText());

                // UCLR계산
                fn_OnUCLRCal();
            }

            txtMINCAR.SetText('');
        }

        // UCLR계산
        function fn_OnUCLRCal() {
            if (txtMAX.GetText() != '')
                txtUCLX.SetText(txtMAX.GetText());

            if (txtMIN.GetText() != '')
                txtLCLX.SetText(txtMIN.GetText());

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

        // 품목코드 입력시 명칭조회
        function fn_OnITEMCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                txtITEMNM.SetValue('');
                txtITEMNM.SetText('');
                txtITEMNM.GetMainElement().title = '';
                txtMODELNM.SetValue('');
                txtMODELNM.SetText('');
                txtMODELNM.GetMainElement().title = '';
                return;
            } else
                devCallback.PerformCallback('ITEM|' + s.GetValue());
        }

        // 검색된 품목 세팅
        function fn_OnSettingItem(CODE, TEXT, MODEL) {
            txtITEMCD.SetText(CODE);
            txtITEMNM.SetText(TEXT);
            txtMODELNM.SetText(MODEL);
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

        // 공정코드 조회전 품목코드 입력여부 확인
        function fn_OnPopupWorkSearchForm() {
            if (txtITEMCD.GetText() == '' || txtITEMNM.GetText() == '') {
                alert('품목을 입력하세요!!');
                txtITEMCD.Focus();
                return false;
            }

            fn_OnPopupWorkSearch('INS');
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

        // 품목, 검사항목, 공정 콜백처리
        function fn_OndevCallback(s, e) {
            var IDCD = s.cpIDCD;
            var IDNM = s.cpIDNM;

            if (IDCD != '' && IDNM != '') {
                var MDNM = s.cpMODELNM;
                var CODE = s.cpCODE;
                var TEXT = s.cpTEXT;
                var MODEL = s.cpMODEL

                var txtIDCD = ASPxClientControl.Cast("txt" + IDCD);
                var txtIDNM = ASPxClientControl.Cast("txt" + IDNM);
                var txtMODEL = MDNM == '' ? '' : ASPxClientControl.Cast("txt" + MDNM);

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
                    fn_SetTextValue(ASPxClientControl.Cast("txtBANCD"), s.cpBANCD);
                    fn_SetTextValue(ASPxClientControl.Cast("txtLINECD"), s.cpLINECD);
                }
            }
        }

        function fn_OnGridForXlsEndCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
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

        function fn_OpenInspection() {

        }

        // Upload
        function fn_OnUploadClick() {
            devUploader.Upload();
        }

        // 업로드 버튼 활성화 & 비활성화
        function fn_OnUploaderTextChanged(s, e) {
            if (s.GetText(0) != '') {
                $("#btnUpload").removeClass("disabled");
            } else {
                $("#btnUpload").addClass("disabled");
            }
        }

        // Upload Start
        function fn_OnFileUploadStart(s, e) {
            var files = s.GetText().replace(/\s|C:\\fakepath\\/g, "").split(",");
        }

        // Upload Complete(개별파일완료시)
        function fn_OnFileUploadComplete(s, e) {

        }

        // Upload Complete(전체완료시)
        function fn_OnFilesUploadComplete(s, e) {
            if (e.callbackData != '') {
                var _callbackData = e.callbackData.split('|');
                //alert(_callbackData[2])
                if (_callbackData[0] == 'Error') {
                    var errMsg = _callbackData[1].split('^');
                    errMsg = errMsg.join('\r');
                    alert('파일업로드중 장애가 발생하였습니다\r에러내용\r' + errMsg);
                } else if (_callbackData[0] == 'Success') {
                    //alert('파일업로드가 완료되었습니다.');
                    //parent.fn_UploadedComplete(_callbackData[1]);
                    devGrid.PerformCallback(_callbackData[2]);
                }
            } else {
                alert('파일업로드중 알수없는 장애가 발생하였습니다\r계속해서 장애가 발생하는 경우 관리자에 문의 바랍니다.');
            }
        }

        // Validate
        function fn_OnValidate(s, e) {

            var row = s.GetRow(e.visibleIndex);
            
            //alert(row.style.backgroundColor);
            //row.className = styleRow.className;
            //row.style.cssText = styleRow.style.cssText;

            //if (rowStyle == 0) row.style.backgroundColor = "Red";

            var standard = s.GetEditor('규격').GetValue();
            var max = s.GetEditor('상한값').GetValue();
            var min = s.GetEditor('하한값').GetValue();
            var inspcd = s.GetEditor('검사분류').GetValue();

            if (inspcd == "치수") {
                if (max < min) {
                    row.style.backgroundColor = "Red";
                } else {
                    row.style.backgroundColor = "";
                }
            } else {
                row.style.backgroundColor = "";
            }
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                fn_OnControlDisableBox(s.GetEditor('일련번호'), null);
                fn_OnControlDisableBox(s.GetEditor('품목코드'), null);
                fn_OnControlDisableBox(s.GetEditor('품목명'), null);
                fn_OnControlEnableBox(s.GetEditor('검사분류'), null);
                fn_OnControlEnableBox(s.GetEditor('검사순서'), null);
                fn_OnControlEnableBox(s.GetEditor('검사항목'), null);
                fn_OnControlEnableBox(s.GetEditor('단위'), null);
                fn_OnControlEnableBox(s.GetEditor('디스플레이규격'), null);
                fn_OnControlEnableBox(s.GetEditor('규격'), null);
                fn_OnControlEnableBox(s.GetEditor('하한값'), null);
                fn_OnControlEnableBox(s.GetEditor('상한값'), null);
                fn_OnControlEnableBox(s.GetEditor('측정기기'), null);
                fn_OnControlEnableBox(s.GetEditor('수준'), null);
                fn_OnControlEnableBox(s.GetEditor('밀시트사용'), null);
                fn_OnControlEnableBox(s.GetEditor('밀시트명'), null);
            } else {
                fn_OnControlEnableBox(s.GetEditor('일련번호'), null);
                fn_OnControlEnableBox(s.GetEditor('품목코드'), null);
                fn_OnControlEnableBox(s.GetEditor('품목명'), null);
                fn_OnControlEnableBox(s.GetEditor('검사분류'), null);
                fn_OnControlEnableBox(s.GetEditor('검사순서'), null);
                fn_OnControlEnableBox(s.GetEditor('검사항목'), null);
                fn_OnControlEnableBox(s.GetEditor('단위'), null);
                fn_OnControlEnableBox(s.GetEditor('디스플레이규격'), null);
                fn_OnControlEnableBox(s.GetEditor('규격'), null);
                fn_OnControlEnableBox(s.GetEditor('하한값'), null);
                fn_OnControlEnableBox(s.GetEditor('상한값'), null);
                fn_OnControlEnableBox(s.GetEditor('측정기기'), null);
                fn_OnControlEnableBox(s.GetEditor('수준'), null);
                fn_OnControlEnableBox(s.GetEditor('밀시트사용'), null);
                fn_OnControlEnableBox(s.GetEditor('밀시트명'), null);
            }
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <div class="col-sm-4">
                        <dx:ASPxUploadControl ID="devUploader" ClientInstanceName="devUploader" runat="server" Width="100%" NullText="파일을 선택하세요"
                            UploadMode="Advanced" ShowProgressPanel="true" CancelButtonHorizontalPosition="Right"
                            OnFileUploadComplete="devUploader_FileUploadComplete" OnFilesUploadComplete="devUploader_FilesUploadComplete">
                            <CancelButton Text="업로드취소" />
                            <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png,.doc,.docx,.pdf,.xls,.xlsx" />
                            <BrowseButton Text="파일찾기" />
                            <ClientSideEvents TextChanged="fn_OnUploaderTextChanged" FileUploadStart="fn_OnFileUploadStart"
                                FileUploadComplete="fn_OnFileUploadComplete" FilesUploadComplete="fn_OnFilesUploadComplete" />
                        </dx:ASPxUploadControl>                    
                    </div>
                    <div class="col-sm-1">
                        <ul class="nav navbar-nav navbar-right m-n nav-user user">
                            <li style="padding-right: 5px;">
                                <button id="btnUpload" class="btn btn-sm btn-warning disabled" onclick="fn_OnUploadClick(); return false;">
                                    <i class="fa fa-upload"></i>
                                    <span class="text">업로드</span>
                                </button>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="SEQ" EnableViewState="false" EnableRowsCache="false" ViewStateMode="Disabled"
                OnInitNewRow="devGrid_InitNewRow" OnHtmlEditFormCreated="devGrid_HtmlEditFormCreated"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                OnRowInserting="devGrid_RowInserting" OnHtmlRowPrepared="devGrid_HtmlRowPrepared" OnCellEditorInitialize="devGrid_CellEditorInitialize">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="일련번호" Caption="일련<br />번호" Width="50px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="품목코드" Caption="품목코드" Width="140px" ></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="품목명" Caption="품목명" Width="200px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="검사분류" Caption="검사분류" Width="100px" >
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Text="치수검사" Value="치수" />
                                <dx:ListEditItem Text="외관검사" Value="외관" />                                
                            </Items>                            
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataColumn FieldName="검사순서" Caption="검사<br />순서" Width="50px" ></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="검사항목" Caption="검사항목" Width="160px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="단위" Caption="단위" Width="50px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="디스플레이규격" Caption="디스플레이<br />규격" Width="80px"></dx:GridViewDataColumn>
                    <dx:GridViewBandColumn Name="F_INSPECTION_STANDARDS" Caption="검사기준">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="규격" Caption="규격" Width="60px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="하한값" Caption="하한값" Width="60px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="상한값" Caption="상한값" Width="60px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="측정기기" Caption="측정기기" Width="120px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="수준" Caption="수준" Width="40px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="밀시트사용" Caption="밀시트<br />사용" Width="60px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="밀시트명" Caption="밀시트명" Width="100px"></dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OndevCallback" />
            </dx:ASPxCallback>
        </div>
    </div>
</asp:Content>
