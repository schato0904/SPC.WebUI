<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WKSD0101.aspx.cs" Inherits="SPC.WebUI.Pages.WKSD.WKSD0101" %>
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

            //fn_doSetGridEventAction('true');

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
            if (ddlDOCCD.GetValue() == null) {
                alert('관리구분을 입력하세요!!');
                ddlDOCCD.Focus();
                return false;
            }
            if (txtITEMCD.GetText() == '' || txtITEMCD.GetText() == '') {
                alert('품목코드를 입력하세요!!');
                txtITEMCD.Focus();
                return false;
            }
            if (txtWORKCD.GetText() == '' || txtWORKNM.GetText() == '') {
                alert('공정코드를 입력하세요!!');
                txtWORKCD.Focus();
                return false;
            } 
            if (txtIMAGESEQ.GetText() == '' ) {
                alert('작업표준서를 등록하세요');
                return false;
            }
        }

        // Grid RowDblClick
        function fn_OnRowDblClick(s, e) {
            if (!fn_DocCheck()) return false;;
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
            var REQUEST = s.cpREQUEST

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

            if (REQUEST == "WORK") {
                txtBANCD.SetValue(s.cpBANCD);
                txtBANCD.SetText(s.cpBANCD);
                txtLINECD.SetValue(s.cpLINECD);
                txtLINECD.SetText(s.cpLINECD);
            }
        }

        function fn_DocCheck() {            
            var returnVal = true;
            try {
                if (ddlDOCCD.GetValue() == null || txtITEMCD.GetText() == '' || txtWORKCD.GetText() == '') {
                    return true;
                } else {
                    if (txtIMAGESEQ.GetText() == '') { 
                        alert("작업표준서가 등록되지 않았습니다");
                        returnVal = false;
                    }
                }
            } catch (e) {
                returnVal = true;
            }
            
            return returnVal;
        }

        function fn_OnPopupWork_() {

            //fn_OnPopupWorkSearch_WKSD();
            fn_OnPopupWorkSearch('FORM');
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
                        <ucCTF:Item ID="ucItem" runat="server" searchOption="T" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />

            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_WORKCD;F_DOCCD" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnHtmlEditFormCreated="devGrid_HtmlEditFormCreated"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                OnRowInserting="devGrid_RowInserting" OnRowUpdating="devGrid_RowUpdating" OnRowDeleting="devGrid_RowDeleting">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
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
                    <dx:GridViewDataColumn FieldName="F_MAKEDATE" Caption="개정일자" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_DOCCD" Caption="관리구분" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="200px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DATA_ORIGIN_NAME" Caption="파일명" Width="200px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_DESC" Caption="비고" Width="100%" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                        
                    <%--여기서부터 히든 필드--%>
                    <dx:GridViewDataColumn FieldName="F_DOCNO" Caption="문서번호" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_REVCD" Caption="" Visible="false"></dx:GridViewDataColumn>
                </Columns>
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                    <EditForm>
                        <div class="form-horizontal divEditForm">
                            <div class="form-group">
                                <label class="col-sm-1 control-label">관리구분</label>
                                <div class="col-sm-2">
                                    <dx:ASPxComboBox ID="ddlDOCCD" ClientInstanceName="ddlDOCCD" runat="server" Width="100%"
                                        OnDataBound="ddlComboBox_DataBound"
                                        IncrementalFilteringMode="None" CssClass="NoXButton">
                                        <ClientSideEvents Init="fn_OnControlDisable" />
                                    </dx:ASPxComboBox>
                                </div>
                                <label class="col-sm-1 control-label">등록일자</label>
                                <div class="col-sm-2">
                                    <dx:ASPxDateEdit ID="txtMAKEDATE" ClientInstanceName="txtMAKEDATE" runat="server" Width="100%" Theme="MetropolisBlue"
                                        DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd" 
                                        class="form-control input-sm" Text='<%# Bind("[F_MAKEDATE]") %>'>
                                    </dx:ASPxDateEdit>
                                </div>
                                <label class="col-sm-1 control-label">작업표준서</label>
                                <div class="col-sm-2" >
                                    <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" ClientVisible="false" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_DOCNO]") %>' />
                                    <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpen('작업표준서관리', 'C', 'F'); return false;" >
                                        <i class="i i-file-plus "></i>
                                        <span class="text">작업표준서관리</span>
                                    </button>
                                </div>
                            </div>
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
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">개정사유</label>
                                    <div class="col-sm-8">
                                        <dx:ASPxMemo ID="txtDESC" ClientInstanceName="txtDESC" runat="server" Width="100%" Height="80px" 
                                            class="form-control input-sm" Text='<%# Bind("[F_DESC]") %>' >                                                
                                        </dx:ASPxMemo>
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
