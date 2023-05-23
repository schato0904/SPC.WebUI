<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="IPCM0101.aspx.cs" Inherits="SPC.WebUI.Pages.IPCM.IPCM0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            // 모니터링에서 넘어온 경우 조회시작
            var oSetParam = "<%=oSetParam%>";
            if (oSetParam != '') {
                devGrid.AddNewRow();                
            }

            fn_RendorTotalCount();
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

            // 수정시 본인이 제기한 것만 수정가능
            var EditRowIndex = fn_GetCastValue('hidEditRowIndex');
            if (EditRowIndex != 'false') {
                var rowKey = devGrid.GetRowKey(EditRowIndex).split('|');
                if (rowKey[3] != '<%=gsUSERID%>') {
                    alert('품질이상 제기자 본인만 수정할 수 있습니다');
                    return false;
                }
            }
            if (false == fn_OnValidate()) return false;

            devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            fn_SetTextValue('hidEditRowIndex', 'false');

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

            fn_doSetGridEventAction('false');

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
            fn_SetTextValue('hidEditRowIndex', 'false');

            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            } else {
                var oSetParam = "<%=oSetParam%>";
                if (oSetParam != '') {
                    var oSetParams = oSetParam.split('|');//F_ITEMCD|F_ITEMNM|F_WORKCD|F_WORKNM|F_MEAINSPCD|F_INSPDETAIL|F_SERIALNO|F_SIRYO|F_FREEPOINT|F_STANDARD|F_MAX|F_MIN
                    //품목
                    fn_SetTextValue('txtITEMCD', oSetParams[2]);
                    fn_SetTextValue('txtITEMNM', oSetParams[3]);
                    //공정
                    fn_SetTextValue('txtWORKCD', oSetParams[4]);
                    fn_SetTextValue('txtWORKNM', oSetParams[5]);
                }
            }

            // EditForm 크기를 조정한다
            if ($(".divEditForm").length) {
                var editFormWidth = $("#cphBody_devGrid").width() - 34 - scrollbarWidth();
                editFormWidth = editFormWidth > parseInt(hidGridColumnsWidth.GetText(), 10) ? parseInt(hidGridColumnsWidth.GetText(), 10) - 34 : editFormWidth;
                $(".divEditForm").width(editFormWidth);
            }

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate() {
            <%if (gsVENDOR) {%>
            if (!txtCTRLNO.GetValue() || txtCTRLNO.GetValue() == '') {
                alert('관리번호를 입력하세요!!');
                txtCTRLNO.Focus();
                return false;
            }
            <%}%>
            if (!txtRQDATE.GetValue() || txtRQDATE.GetValue() == '') {
                alert('이상제기일을 입력하세요!!');
                txtRQDATE.Focus();
                return false;
            }
            if (!txtITEMCD.GetValue() || txtITEMCD.GetValue() == '') {
                alert('품목코드를 선택하세요!!');
                txtITEMCD.Focus();
                return false;
            }
            if (!txtWORKCD.GetValue() || txtWORKCD.GetValue() == '') {
                alert('공정코드를 선택하세요!!');
                txtWORKCD.Focus();
                return false;
            }
            if (!ddlUNSTTP.GetValue() || ddlUNSTTP.GetValue() == '') {
                alert('부적합유형을 선택하세요!!');
                ddlUNSTTP.Focus();
                return false;
            }
            if (!txtRQTXT5.GetValue() || txtRQTXT5.GetValue() == '') {
                alert('의뢰사항을 입력하세요!!');
                txtRQTXT5.Focus();
                return false;
            }
        }

        // Grid RowDblClick
        function fn_OnRowDblClick(s, e) {
            fn_SetTextValue('hidEditRowIndex', e.visibleIndex);
            devGrid.StartEditRow(e.visibleIndex);
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
        // 검색된 품목 세팅
        function fn_OnSettingItem(CODE, TEXT, MODEL) {
            txtITEMCD.SetText(CODE);
            txtITEMNM.SetText(TEXT);
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
            if (s.cpResultCode == '99') {
                alert(s.cpResultMsg);
            } else if (s.cpResultCode == 'save') {
                alert(s.cpResultMsg);
                //parent.fn_devPopupClose();
            } else {
                var IDCD = s.cpIDCD;
                var IDNM = s.cpIDNM;
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
            }
        }

        function fn_OnUNSTTPValueChanged(s, e) {
            var val = s.GetValue();
            hidUNSTTP.SetValue(val);
        }

        function fn_OnDEPARTCDalueChanged(s, e) {
            var val = s.GetValue();
            hidDEPARTCD.SetValue(val);
        }

        // 검사항목검색창 오픈
        function fn_OnPopupInspectionItem() {
            var ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtITEMCD')) ? '' : ASPxClientControl.Cast('txtITEMCD').GetText();
            var WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtWORKCD')) ? '' : ASPxClientControl.Cast('txtWORKCD').GetText();

            var pPage = '<%=Page.ResolveUrl("~/Pages/Common/Popup/INSPITEMPOP.aspx")%>' +
                '?TITLE=검사항목조회' +
                '&CRUD=R' +
                '&ITEMCD=' + ITEMCD + '' +
                '&WORKCD=' + WORKCD;
            fn_OnPopupOpen(pPage, 800, 500);
        }

        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목            
            //F_MEAINSPCD; F_INSPDETAIL; F_INSPCD; F_INSPNM; F_STANDARD; F_MAX; F_MIN; F_UCLX; F_LCLX; F_UCLR; F_SERIALNO; F_SIRYO; F_FREEPOINT
            txtMEAINSPCD.SetValue(resultValues[0]);
            txtINSPDETAIL.SetValue(resultValues[1]);
            txtSTANDARD.SetValue(resultValues[4]);
            txtMAX.SetValue(resultValues[5]);
            txtMIN.SetValue(resultValues[6]);
            txtSERIALNO.SetValue(resultValues[10])
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <div class="col-sm-3 control-label" style="display: <%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">업체</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 control-label" style="display: <%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">공장</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:Fact ID="ucFact" runat="server" />
                            </div>
                        </div>
                    </div>
                    <label class="col-sm-1 control-label">검색일</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidEditRowIndex" ClientInstanceName="hidEditRowIndex" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD;F_INDXNO;F_RQUSID" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnInitNewRow="devGrid_InitNewRow" OnHtmlEditFormCreated="devGrid_HtmlEditFormCreated"
                OnRowInserting="devGrid_RowInserting" OnRowUpdating="devGrid_RowUpdating" OnRowDeleting="devGrid_RowDeleting">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Visible" />
                <SettingsBehavior AllowSort="false"  />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>                
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px" FixedStyle="Left">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TRNM" Caption="부적합유형" Width="150px">
                        <CellStyle HorizontalAlign="left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RQDATE" Caption="이상제기일" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RQUSNM" Caption="제기자" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RQRCDT" Caption="회신요청일" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CTRLNO" Caption="발행번호" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="160px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="200px"></dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="F_UNSTTP" Caption="부적합유형코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_DEPARTCD" Caption="대책부서코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEASGD" Caption="대책등급" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_RQUSID" Caption="제기자" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RQCPCD" Caption="제기업체코드" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RQFTCD" Caption="제기공장코드" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_BVENDOR" Caption="협력업체여부" Visible="false"></dx:GridViewDataColumn>
                </Columns>
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>                
                    <EditForm>
                        <div class="form-horizontal divEditForm">
                            <div class="form-group">
                                <label class="col-sm-1 control-label">관리번호</label>
                                <div class="col-sm-1">
                                    <dx:ASPxTextBox ID="txtCTRLNO" ClientInstanceName="txtCTRLNO" runat="server" Width="100%"
                                        class="form-control input-sm" Text='<%# Bind("[F_CTRLNO]") %>'>
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">이상제기일</label>
                                <div class="col-sm-2">
                                    <dx:ASPxDateEdit ID="txtRQDATE" ClientInstanceName="txtRQDATE" runat="server" Width="100%" Theme="MetropolisBlue"
                                        DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd"
                                        class="form-control input-sm" Text='<%# Bind("[F_RQDATE]") %>'>
                                    </dx:ASPxDateEdit>
                                </div>
                                <label class="col-sm-1 control-label">회신요구일</label>
                                <div class="col-sm-2">
                                    <dx:ASPxDateEdit ID="txtRQRCDT" ClientInstanceName="txtRQRCDT" runat="server" Width="100%" Theme="MetropolisBlue"
                                        DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd"
                                        class="form-control input-sm" Text='<%# Bind("[F_RQRCDT]") %>'>
                                    </dx:ASPxDateEdit>
                                </div>
                                <label class="col-sm-1 control-label">발생기간</label>
                                <div class="col-sm-3">
                                    <div style="float: left; width: 50%; padding-right: 3px;">
                                        <dx:ASPxDateEdit ID="txtOCSTDT" ClientInstanceName="txtOCSTDT" runat="server" Width="100%" Theme="MetropolisBlue"
                                            DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd"
                                            class="form-control input-sm" Text='<%# Bind("[F_OCSTDT]") %>'>
                                        </dx:ASPxDateEdit>
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <dx:ASPxDateEdit ID="txtOCEDDT" ClientInstanceName="txtOCEDDT" runat="server" Width="100%" Theme="MetropolisBlue"
                                            DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd"
                                            class="form-control input-sm" Text='<%# Bind("[F_OCEDDT]") %>'>
                                        </dx:ASPxDateEdit>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">품목코드</label>
                                <div class="col-sm-4">
                                    <div style="float: left; width: 40%; padding-right: 3px;">
                                        <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%"
                                            class="form-control input-sm" Text='<%# Bind("[F_ITEMCD]") %>'
                                            OnInit="txtITEMCD_Init">
                                            <ClientSideEvents LostFocus="fn_OnITEMCDLostFocus" />
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div style="float: left; width: 60%;">
                                        <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%"
                                            class="form-control input-sm" Text='<%# Bind("[F_ITEMNM]") %>'>
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                                <label class="col-sm-1 control-label">공정코드</label>
                                <div class="col-sm-3">
                                    <div style="float: left; width: 40%; padding-right: 3px;">
                                        <dx:ASPxTextBox ID="txtBANCD" ClientInstanceName="txtBANCD" runat="server" ClientVisible="false" Text='<%# Bind("[F_BANCD]") %>' />
                                        <dx:ASPxTextBox ID="txtLINECD" ClientInstanceName="txtLINECD" runat="server" ClientVisible="false" Text='<%# Bind("[F_LINECD]") %>' />
                                        <dx:ASPxTextBox ID="txtWORKCD" ClientInstanceName="txtWORKCD" runat="server" Width="100%"
                                            class="form-control input-sm" Text='<%# Bind("[F_WORKCD]") %>'
                                            OnInit="txtWORKCD_Init">
                                            <ClientSideEvents LostFocus="fn_OnWORKCDLostFocus" />
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div style="float: left; width: 60%;">
                                        <dx:ASPxTextBox ID="txtWORKNM" ClientInstanceName="txtWORKNM" runat="server" Width="100%"
                                            class="form-control input-sm" Text='<%# Bind("[F_WORKNM]") %>'>
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                                <label class="col-sm-1 control-label">기종명</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtMODELCD" ClientInstanceName="txtMODELCD" runat="server" ClientVisible="false" Text='<%# Bind("[F_MODELCD]") %>' />
                                    <dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtMODELNM" runat="server" Width="100%"
                                        class="form-control input-sm" Text='<%# Bind("[F_MODELNM]") %>'>
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">Lot No.</label>
                                <div class="col-sm-1">
                                    <dx:ASPxTextBox ID="txtLOTNO" ClientInstanceName="txtLOTNO" runat="server" Width="100%"
                                        class="form-control input-sm" Text='<%# Bind("[F_LOTNO]") %>'>
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">부적합유형</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="hidUNSTTP" ClientInstanceName="hidUNSTTP" runat="server" ClientVisible="false" Text='<%# Bind("[F_UNSTTP]") %>' />
                                    <dx:ASPxComboBox ID="ddlUNSTTP" ClientInstanceName="ddlUNSTTP" runat="server" Width="100%"
                                        IncrementalFilteringMode="None" CssClass="NoXButton" OnDataBound="ddlComboBox_DataBound">
                                        <ClientSideEvents Init="fn_OnControlDisable" ValueChanged="fn_OnUNSTTPValueChanged" />
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="col-sm-1 control-label">대책부서</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="hidDEPARTCD" ClientInstanceName="hidDEPARTCD" runat="server" ClientVisible="false" Text='<%# Bind("[F_DEPARTCD]") %>' />
                                    <dx:ASPxComboBox ID="ddlDEPARTCD" ClientInstanceName="ddlDEPARTCD" runat="server" Width="100%"
                                        IncrementalFilteringMode="None" CssClass="NoXButton" OnDataBound="ddlComboBox_DataBound">
                                        <ClientSideEvents Init="fn_OnControlDisable" ValueChanged="fn_OnDEPARTCDalueChanged" />
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="col-sm-1 control-label">대책요청</label>
                                <div class="col-sm-1 control-label">
                                    <dx:ASPxComboBox ID="ddlMEASGD" runat="server" ClientInstanceName="ddlMEASGD" Width="100%"
                                        IncrementalFilteringMode="None" CssClass="NoXButton" OnDataBound="ddlComboBox_DataBound">
                                        <Items>
                                            <dx:ListEditItem Text="A급" Value="A" />
                                            <dx:ListEditItem Text="B급" Value="B" />
                                            <dx:ListEditItem Text="C급" Value="C" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="col-sm-1 control-label">파일첨부</label>
                                <div class="col-sm-1">
                                    <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false" Text='<%# Bind("[F_RQFILE]") %>' />
                                    <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpen('품질이상제기', 'F', 'T'); return false;">
                                        <i class="i i-file-plus "></i>
                                        <span class="text">파일첨부</span>
                                    </button>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">주제</label>
                                <div class="col-sm-11">
                                    <dx:ASPxTextBox ID="txtRQTXT1" ClientInstanceName="txtRQTXT1" runat="server" Width="100%" MaxLength="400"
                                        class="form-control input-sm" Text='<%# Bind("[F_RQTXT1]") %>'>
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">부적합사항</label>
                                <div class="col-sm-5">
                                    <dx:ASPxMemo ID="txtRQTXT2" ClientInstanceName="txtRQTXT2" runat="server" Width="100%" Height="60px" MaxLength="400"
                                        class="form-control input-sm" Text='<%# Bind("[F_RQTXT2]") %>'>
                                    </dx:ASPxMemo>
                                </div>
                                <label class="col-sm-1 control-label">의뢰사항</label>
                                <div class="col-sm-5">
                                    <dx:ASPxMemo ID="txtRQTXT5" ClientInstanceName="txtRQTXT5" runat="server" Width="100%" Height="60px" MaxLength="400"
                                        class="form-control input-sm" Text='<%# Bind("[F_RQTXT5]") %>'>
                                    </dx:ASPxMemo>
                                </div>
                            </div>
                            <div class="form-group" style="display: <%if (gsVENDOR){%>display<%} else { %>none<%} %>">
                                <label class="col-sm-1 control-label">발견동기</label>
                                <div class="col-sm-5">
                                    <dx:ASPxMemo ID="txtRQTXT3" ClientInstanceName="txtRQTXT3" runat="server" Width="100%" Height="60px" MaxLength="400"
                                        class="form-control input-sm" Text='<%# Bind("[F_RQTXT3]") %>'>
                                    </dx:ASPxMemo>
                                </div>
                                <label class="col-sm-1 control-label">잠정조치</label>
                                <div class="col-sm-5">
                                    <dx:ASPxMemo ID="txtRQTXT4" ClientInstanceName="txtRQTXT4" runat="server" Width="100%" Height="60px" MaxLength="400"
                                        class="form-control input-sm" Text='<%# Bind("[F_RQTXT4]") %>'>
                                    </dx:ASPxMemo>
                                </div>
                            </div>
                        </div>
                    </EditForm>
                </Templates>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OndevCallback" />
            </dx:ASPxCallback>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
