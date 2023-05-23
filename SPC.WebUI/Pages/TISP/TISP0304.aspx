<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TISP0304.aspx.cs" Inherits="SPC.WebUI.Pages.TISP.TISP0304" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            // 모니터링에서 넘어온 경우 조회시작
            var oSetParam = "<%=oSetParam%>";
            if (oSetParam != '') {
                var oSetParams = oSetParam.split('|');
                //// 업체
                fn_SetTextValue('hidCOMPCD', oSetParams[0]);
                // 공장
                fn_SetTextValue('hidFACTCD', oSetParams[1]);
                // 검색일
                fn_SetTextValue('hidUCFROMDT', oSetParams[2]);
                fn_SetDate('txtFROMDT', convertDateString(oSetParams[2]));
                fn_SetTextValue('hidUCTODT', oSetParams[2]);
                fn_SetDate('txtTODT', convertDateString(oSetParams[2]));

                devGrid.PerformCallback();
            }
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

            <%if (!gsVENDOR){%>
            if (hidCOMPCD.GetValue() == "" || hidCOMPCD.GetValue() == null) {
                alert("업체를 선택하세요!!");
                return false;
            }
            if (hidFACTCD.GetValue() == "" || hidFACTCD.GetValue() == null) {
                alert("공장을 선택하세요!!");
                return false;
            }
           <%}%>

            if ((hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) && (hidUCWORKPOPCD.GetValue() == "" || hidUCWORKPOPCD.GetValue() == null)) {
                alert("품목이나 공정을 선택하세요!!");
                return false;
            }
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

            if (!fn_OnValidate()) return false;

            fn_doSetGridEventAction('true');
            devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            fn_doSetGridEventAction('true');
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

            fn_doSetGridEventAction('true');
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

            // 편측확인
            if (txtMAX.GetText() == '') {
                txtNGOKCHK.SetText("0");
                txtNGOKCHK.SetValue("0");

                // 관리이탈 확인
                if (ldc_lclx > ldc_measure) {
                    txtNGOKCHK.SetText("2");
                    txtNGOKCHK.SetValue("2");
                }

                // 규격이탈 확인
                if (ldc_min > ldc_measure) {
                    txtNGOKCHK.SetText("1");
                    txtNGOKCHK.SetValue("1");
                }
            } else if (txtMIN.GetText() == '') {
                txtNGOKCHK.SetText("0");
                txtNGOKCHK.SetValue("0");

                // 관리이탈 확인
                if (ldc_uclx < ldc_measure) {
                    txtNGOKCHK.SetText("2");
                    txtNGOKCHK.SetValue("2");
                }

                // 규격이탈 확인
                if (ldc_max < ldc_measure) {
                    txtNGOKCHK.SetText("1");
                    txtNGOKCHK.SetValue("1");
                }
            } else {
                txtNGOKCHK.SetText("0");
                txtNGOKCHK.SetValue("0");

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
            } else if (txtCONTENTS.GetText() == '') {
                alert('수정사유를 입력하세요!!');
                txtCONTENTS.Focus();
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="display:<%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false"  SingleDate="true"  />
                    </div>
                    <label class="col-sm-1 control-label">합격여부</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlJUDGE" ClientInstanceName="ddlJUDGE" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">검사구분</label>
                    <div class="col-sm-1">
                        <dx:ASPxComboBox ID="ddlINSPCD" ClientInstanceName="ddlINSPCD" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" machGubun="3" />
                    </div>                 
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" machGubun="3"  />
                    </div>                    
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MEASNO;F_TSERIALNO" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                OnRowUpdating="devGrid_RowUpdating" OnRowDeleting="devGrid_RowDeleting">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="devGrid_RowDbClick" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Width="100px"/>
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="200px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="120px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="검사기준" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격" Width="80px" >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="60px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO"    Caption="시료군"  Width="70px"  />
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번"  Width="50px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자"  Width="60px"  />
                        
                    <%--여기서부터 히든 필드--%>
                    
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEASNO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_NGOKCHK" Visible="false" />
                </Columns>
                <Templates>
                    <EditForm>
                        <div class="form-horizontal divEditForm">
                            <div class="form-group">
                                <label class="col-sm-1 control-label">검사일자</label>
                                <div class="col-sm-2">
                                    <dx:ASPxDateEdit ID="txtWORKDATE" ClientInstanceName="txtWORKDATE" runat="server" Width="100%" Theme="MetropolisBlue"
                                        DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd" 
                                        class="form-control input-sm" Text='<%# Bind("[F_WORKDATE]") %>'>
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxDateEdit>
                                </div>
                                <label class="col-sm-1 control-label">품목코드</label>
                                <div class="col-sm-2">
                                    <div style="float: left; width: 40%; padding-right: 3px;">
                                        <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" 
                                            class="form-control input-sm" Text='<%# Bind("[F_ITEMCD]") %>' >
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
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
                                            class="form-control input-sm" Text='<%# Bind("[F_WORKCD]") %>'>
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
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
                                <label class="col-sm-1 control-label">검사항목</label>
                                <div class="col-sm-2">
                                    <div style="float: left; width: 40%; padding-right: 3px;">
                                        <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" Width="100%" 
                                            class="form-control input-sm" Text='<%# Bind("[F_SERIALNO]") %>' >
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div style="float: left; width: 60%;">
                                        <dx:ASPxTextBox ID="txtINSPDETAIL" ClientInstanceName="txtINSPDETAIL" runat="server" Width="100%" 
                                            class="form-control input-sm" Text='<%# Bind("[F_INSPDETAIL]") %>'>
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                                <label class="col-sm-1 control-label">순번</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtMEASNO" ClientInstanceName="txtMEASNO" runat="server" Text='<%# Bind("[F_MEASNO]") %>' ClientVisible="false" />
                                    <dx:ASPxTextBox ID="txtNUMBER" ClientInstanceName="txtNUMBER" runat="server" Width="100%"
                                        class="form-control input-sm" Text='<%# Bind("[F_NUMBER]") %>' >
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">시료군</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtTSERIALNO" ClientInstanceName="txtTSERIALNO" runat="server" Width="100%"
                                        class="form-control input-sm" Text='<%# Bind("[F_TSERIALNO]") %>' >                                                
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">검사규격</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" HorizontalAlign="Right"
                                        class="form-control input-sm" Text='<%# Bind("[F_STANDARD]") %>' >                                                
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />                                        
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">상한규격</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%" HorizontalAlign="Right"
                                        class="form-control input-sm" Text='<%# Bind("[F_MAX]") %>' >                                                
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">하한규격</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%" HorizontalAlign="Right"
                                        class="form-control input-sm" Text='<%# Bind("[F_MIN]") %>' >                                                
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">UCLX</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtUCLX" ClientInstanceName="txtUCLX" runat="server" Width="100%" HorizontalAlign="Right"
                                        class="form-control input-sm" Text='<%# Bind("[F_UCLX]") %>' >                                                
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">LCLX</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtLCLX" ClientInstanceName="txtLCLX" runat="server" Width="100%" HorizontalAlign="Right"
                                        class="form-control input-sm" Text='<%# Bind("[F_LCLX]") %>' >                                                
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">UCLR</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtUCLR" ClientInstanceName="txtUCLR" runat="server" Width="100%" HorizontalAlign="Right"
                                        class="form-control input-sm" Text='<%# Bind("[F_UCLR]") %>' >  
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />                                              
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">측정Data</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtMEASURE" ClientInstanceName="txtMEASURE" runat="server" Width="100%" HorizontalAlign="Right"
                                        class="form-control input-sm" Text='<%# Bind("[F_MEASURE]") %>' >  
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />                                              
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-1 control-label">수정Data</label>
                                <div class="col-sm-2">
                                    <dx:ASPxTextBox ID="txtMODIFYMEASURE" ClientInstanceName="txtMODIFYMEASURE" runat="server" Width="100%" HorizontalAlign="Right"
                                        class="form-control input-sm" Text='<%# Bind("[F_MODIFYMEASURE]") %>' >
                                        <MaskSettings IncludeLiterals="DecimalSymbol" />
                                        <ClientSideEvents KeyPress="fn_ValidateOnlyFloat" LostFocus="fn_OKNGCHK"/>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtINSPCD" ClientInstanceName="txtINSPCD" runat="server" ClientVisible="false" Text='<%# Bind("[F_INSPCD]") %>' />
                                    <dx:ASPxTextBox ID="txtNGOKCHK" ClientInstanceName="txtNGOKCHK" runat="server" ClientVisible="false" Text='<%# Bind("[F_NGOKCHK]") %>' />
                                </div>                                
                                <div class="col-sm-2">
                                    <dx:ASPxCheckBox ID="chkDELETE" ClientInstanceName="chkDELETE" runat="server" Text="삭제" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label">사유</label>
                                <div class="col-sm-8">
                                    <dx:ASPxMemo ID="txtCONTENTS" ClientInstanceName="txtCONTENTS" runat="server" Width="100%"
                                        class="form-control input-sm" Text='<%# Bind("[F_CONTENTS]") %>' >                                                
                                    </dx:ASPxMemo>
                                </div>                                
                            </div>
                    </EditForm>
                </Templates>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
