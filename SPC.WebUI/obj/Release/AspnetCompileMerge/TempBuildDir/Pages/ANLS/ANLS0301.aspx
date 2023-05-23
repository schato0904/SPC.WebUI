<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0301.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0301" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
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

            if (false == fn_OnValidate()) return false;

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

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate() {
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
            if (!txtCHASU.GetValue() || txtCHASU.GetValue() == '') {
                alert('차수를 입력하세요!!');
                txtCHASU.Focus();
                return false;
            }
        }

        // Grid RowDblClick
        function fn_OnRowDblClick(s, e) {
            devGrid.StartEditRow(e.visibleIndex);
        }

        // 품목코드 입력시 명칭조회
        function fn_OnITEMCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                txtITEMNM.SetValue('');
                txtITEMNM.SetText('');
                txtITEMNM.GetMainElement().title = '';                
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
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />

            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;;F_WORKCD;F_SERIALNO;F_CHASU" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnHtmlEditFormCreated="devGrid_HtmlEditFormCreated"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
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
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="150px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>                    
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="200px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_JUDGE" Caption="판정" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="120px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SINGLE_CP" Caption="단독Cp" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SINGLE_CPK" Caption="단독Cpk" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SINGLE_R" Caption="단독Range" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINK_CP" Caption="연동Cp" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINK_CPK" Caption="연동Cpk" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINK_R" Caption="연동Range" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CHASU" Caption="차수" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NGREASON" Caption="불합격내용" Width="200px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고" Width="300px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="F_ITEMCD"     Visible="false"  />
                    <dx:GridViewDataColumn FieldName="F_WORKCD"     Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_SERIALNO"   Visible="false" />                    
                </Columns>
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                    <EditForm>
                        <div class="form-horizontal divEditForm">
                            <div class="form-group">
                                <label class="col-sm-1 control-label">품목코드</label>
                                <div class="col-sm-2">
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
                                <div class="col-sm-2">
                                    <div style="float: left; width: 40%; padding-right: 3px;">
                                        <dx:ASPxTextBox ID="txtBANCD" ClientInstanceName="txtBANCD" runat="server" Width="100%" 
                                            class="form-control input-sm" Text='<%# Bind("[F_BANCD]") %>' ClientVisible="false" />
                                        <dx:ASPxTextBox ID="txtLINECD" ClientInstanceName="txtLINECD" runat="server" Width="100%" 
                                            class="form-control input-sm" Text='<%# Bind("[F_LINECD]") %>' ClientVisible="false" />
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
                                <label class="col-sm-1 control-label">검사항목</label>
                                <div class="col-sm-2">
                                    <div style="float: left; width: 40%; padding-right: 3px;">
                                        <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" Text='<%# Bind("[F_SERIALNO]") %>' />
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
                            </div>
                            <div class="form-group">                                
                                <label class="col-sm-1 control-label">등록일자</label>
                                <div class="col-sm-2">
                                    <dx:ASPxDateEdit ID="txtWORKDATE" ClientInstanceName="txtWORKDATE" runat="server" Width="100%" Theme="MetropolisBlue"
                                        DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd" 
                                        class="form-control input-sm" Text='<%# Bind("[F_WORKDATE]") %>'>
                                    </dx:ASPxDateEdit>
                                </div>
                                <label class="col-sm-1 control-label">파일첨부</label>
                                <div class="col-sm-2" >
                                    <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" ClientVisible="false" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_FILENO]") %>' />
                                    <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpen('T/O결과등록', 'G', 'F'); return false;" >
                                        <i class="i i-file-plus "></i>
                                        <span class="text">파일첨부</span>
                                    </button>
                                </div>
                                <label class="col-sm-1 control-label">규격</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_STANDARD]") %>'>
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">상한</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="80px" 
                                        class="form-control input-sm" Text='<%# Bind("[F_MAX]") %>'>
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">하한</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="80px" 
                                        class="form-control input-sm" Text='<%# Bind("[F_MIN]") %>'>
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">                                
                                <label class="col-sm-1 control-label">단독Cp</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtSINGLECP" ClientInstanceName="txtSINGLECP" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_SINGLE_CP]") %>' >
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">단독Cpk</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtSINGLECPK" ClientInstanceName="txtSINGLECPK" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_SINGLE_CPK]") %>' >
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">단독Range</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtSINGLER" ClientInstanceName="txtSINGLER" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_SINGLE_R]") %>' >
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">연동Cp</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtLINKCP" ClientInstanceName="txtLINKCP" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_LINK_CP]") %>' >
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">연동Cpk</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtLINKCPK" ClientInstanceName="txtLINKCPK" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_LINK_CPK]") %>'>
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">연동Range</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtLINKR" ClientInstanceName="txtLINKR" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_LINK_R]") %>' >
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">                                
                                <label class="col-sm-1 control-label">검사일</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxDateEdit ID="txtINSTDT" ClientInstanceName="txtINSTDT" runat="server" Width="100%" Theme="MetropolisBlue"
                                        DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd" 
                                        class="form-control input-sm" Text='<%# Bind("[F_INSTDT]") %>'>
                                    </dx:ASPxDateEdit>
                                </div>
                                <label class="col-sm-1 control-label">차수T/O</label>
                                <div class="col-sm-1" >
                                    <dx:ASPxTextBox ID="txtCHASU" ClientInstanceName="txtCHASU" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_CHASU]") %>' >
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">판정</label>
                                <div class="col-sm-2" >
                                    <dx:ASPxRadioButtonList ID="rdoJUDGE" ClientInstanceName="rdoJUDGE" runat="server"
                                        OnDataBound="rdoButtonList_DataBound"
                                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                        <Items>
                                            <dx:ListEditItem Value="1" Text="합격" />
                                            <dx:ListEditItem Value="0" Text="불합격" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </div>
                                <label class="col-sm-1 control-label">불합격내용</label>
                                <div class="col-sm-4" >
                                    <dx:ASPxTextBox ID="txtNGREASON" ClientInstanceName="txtNGREASON" runat="server" Width="100%" 
                                        class="form-control input-sm" Text='<%# Bind("[F_NGREASON]") %>' >
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">                                
                                <label class="col-sm-1 control-label">비고</label>
                                <div class="col-sm-11">
                                    <dx:ASPxMemo ID="txtREMARK" ClientInstanceName="txtREMARK" runat="server" Width="100%" Height="80px" 
                                        class="form-control input-sm" Text='<%# Bind("[F_REMARK]") %>' >                                                
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
