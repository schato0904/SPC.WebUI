<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0305.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0305" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var procMode = 'search';
        var inputMode = 'new';

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
            fn_OnCancelClick();
            //if (false == fn_OnValidate()) return false;

            // 변경검사기준 TextBox Enable & Disable
            fn_OnToggleInputBox(true);

            // 변경검사기준 기본값 세팅
            //fn_OnSetNewUCL();

            procMode = 'input';
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (procMode != 'input') {
                alert('입력을 누른 후 값을 입력후 저장을 누르세요!!');
                return false;
            }

            if (false == fn_OnValidate()) return false;

            procMode = 'save';

            devCallback.PerformCallback();
        }

        // 취소
        function fn_OnCancelClick() {
            procMode = 'search';
            // 품목
            fn_SetTextValue('hidUCITEMCD', '');
            fn_SetTextValue('txtUCITEMCD', '');
            fn_SetTextValue('txtUCITEMNM', '');

            // 공정
            fn_SetTextValue('hidUCWORKPOPCD', '');
            fn_SetTextValue('txtUCWORKPOPCD', '');
            fn_SetTextValue('txtUCWORKNM', '');

            // 검사항목
            fn_SetTextValue('hidUCINSPITEMCD', '');
            fn_SetTextValue('txtUCINSPITEMCD', '');
            fn_SetTextValue('txtUCINSPITEMNM', '');

            // 검사규격
            fn_SetTextValue('txtSTANDARD', '');
            fn_SetTextValue('txtMAX', '');
            fn_SetTextValue('txtMIN', '');

            // 관리규격
            fn_SetTextValue('txtUCLR', '');
            fn_SetTextValue('txtUCLX', '');
            fn_SetTextValue('txtLCLX', '');

            // 관리규격
            fn_SetTextValue('txtUCLRNEW', '');
            fn_SetTextValue('txtUCLXNEW', '');
            fn_SetTextValue('txtLCLXNEW', '');

            // 변경일
            fn_SetTextValue('txtSTDT', null);

            // 변경사유
            fn_SetTextValue('txtCONTENTS', '');

            // 변경검사기준 TextBox Enable & Disable
            fn_OnToggleInputBox(false);
        }

        // 삭제
        function fn_OnDeleteClick() {

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

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            if (fn_GetCastText('txtUCITEMCD') == '') {
                alert('품목정보를 입력하세요!!');
                txtUCITEMCD.Focus();
                return false;
            }

            if (fn_GetCastText('txtUCWORKPOPCD') == '') {
                alert('공정정보를 입력하세요!!');
                txtUCWORKPOPCD.Focus();
                return false;
            }

            if (fn_GetCastText('txtUCINSPITEMCD') == '') {
                alert('항목정보를 입력하세요!!');
                txtUCINSPITEMCD.Focus();
                return false;
            }

            if (procMode == 'input') {
                if (fn_GetCastText('txtSTANDARD') == '') {
                    alert('검사규격을 입력하세요!!');
                    txtSTANDARD.Focus();
                    return false;
                }

                if (fn_GetCastText('txtMAX') == '') {
                    alert('상한규격 입력하세요!!');
                    txtMAX.Focus();
                    return false;
                }

                if (fn_GetCastText('txtMIN') == '') {
                    alert('하한규격을 입력하세요!!');
                    txtMIN.Focus();
                    return false;
                }

                if (fn_GetCastText('txtUCLX') == '') {
                    alert('상한목표를 입력하세요!!');
                    txtUCLX.Focus();
                    return false;
                }

                if (fn_GetCastText('txtLCLX') == '') {
                    alert('하한목표를 입력하세요!!');
                    txtLCLX.Focus();
                    return false;
                }

                if (fn_GetCastText('txtUCLR') == '') {
                    alert('범위를 입력하세요!!');
                    txtUCLR.Focus();
                    return false;
                }

                if (fn_GetCastText('txtUCLXNEW') == '') {
                    alert('상한목표를 입력하세요!!');
                    txtUCLXNEW.Focus();
                    return false;
                }

                if (fn_GetCastText('txtLCLXNEW') == '') {
                    alert('하한목표를 입력하세요!!');
                    txtLCLXNEW.Focus();
                    return false;
                }

                if (fn_GetCastText('txtUCLRNEW') == '') {
                    alert('범위를 입력하세요!!');
                    txtUCLRNEW.Focus();
                    return false;
                }

                if (fn_GetCastText('txtSTDT') == '') {
                    alert('시작일자를 입력하세요!!');
                    txtSTDT.Focus();
                    return false;
                }

                if (fn_GetCastText('txtCONTENTS') == '') {
                    alert('변경사유를 입력하세요!!');
                    txtCONTENTS.Focus();
                    return false;
                }

                // 기존값과 비교
                if (fn_GetCastText('txtUCLX') == fn_GetCastText('txtUCLXNEW') &&
                    fn_GetCastText('txtLCLX') == fn_GetCastText('txtLCLXNEW') &&
                    fn_GetCastText('txtUCLR') == fn_GetCastText('txtUCLRNEW')) {
                    alert('변경된 관리한계 값이 없습니다');
                    return false;
                }
            }
        }

        function fn_OnSetQCD34Values(resultValues) {
            // 검사유형
            fn_SetTextValue('hidINSPCD', resultValues[2]);

            // 일련번호
            fn_SetTextValue('hidSERIALNO', resultValues[10]);

            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);

            // 검사규격
            fn_SetTextValue('txtSTANDARD', resultValues[4]);
            fn_SetTextValue('txtMAX', resultValues[5]);
            fn_SetTextValue('txtMIN', resultValues[6]);

            // 관리규격
            fn_SetTextValue('txtUCLR', resultValues[9]);
            fn_SetTextValue('txtUCLX', resultValues[7]);
            fn_SetTextValue('txtLCLX', resultValues[8]);

            // 공정(11, 12)
            //fn_SetTextValue('hidUCWORKPOPCD', resultValues[11]);
            //fn_SetTextValue('txtUCWORKPOPCD', resultValues[11]);
            //fn_SetTextValue('txtUCWORKNM', resultValues[12]);

            fn_OnSetNewUCL();
        }

        // 변경검사기준 TextBox Enable & Disable
        function fn_OnToggleInputBox(bMode) {
            if (false == bMode) {   // Disable
                fn_OnControlDisableBox(txtUCLXNEW);
                fn_OnControlDisableBox(txtLCLXNEW);
                fn_OnControlDisableBox(txtUCLRNEW);
                fn_OnControlDisableBox(txtSTDT);
                fn_OnControlDisableBox(txtCONTENTS);
            } else {                // Enable
                fn_OnControlEnableBox(txtUCLXNEW);
                fn_OnControlEnableBox(txtLCLXNEW);
                fn_OnControlEnableBox(txtUCLRNEW);
                fn_OnControlEnableBox(txtSTDT);
                fn_OnControlEnableBox(txtCONTENTS);
            }
        }

        // 변경검사기준 기본값 세팅
        function fn_OnSetNewUCL() {
            fn_SetTextValue('txtUCLRNEW', fn_GetCastText('txtUCLR'));
            fn_SetTextValue('txtUCLXNEW', fn_GetCastText('txtUCLX'));
            fn_SetTextValue('txtLCLXNEW', fn_GetCastText('txtLCLX'));
            fn_SetTextValue('txtSTDT', null);
            fn_SetTextValue('txtCONTENTS', '');
        }

        // 콜백
        function fn_OndevCallback(s, e) {
            alert(s.cpErrMsg);

            // 작업이 성공한 경우 입력창 Clear 후 조회한다.
            if (s.cpErrCode == '1') {
                fn_OnCancelClick();
            } else {
                procMode = 'input';
            }
        }

        // UCLR계산
        function fn_OnUCLRCal() {
            if (txtUCLXNEW.GetText() == '' || txtLCLXNEW.GetText() == '') {
                txtUCLRNEW.SetText('');
                return false;
            }

            var UCLXVal = txtUCLXNEW.GetText();
            var LCLXVal = txtLCLXNEW.GetText();

            var nUCLXFixLen = fn_GetDecimalPoint(UCLXVal);
            var nLCLXFixLen = fn_GetDecimalPoint(LCLXVal);

            var nFixedLen = nUCLXFixLen > nLCLXFixLen ? nUCLXFixLen : nLCLXFixLen;

            var fUCLX = parseFloat(parseFloat(UCLXVal).toFixed(nFixedLen));
            var fLCLX = parseFloat(parseFloat(LCLXVal).toFixed(nFixedLen));

            txtUCLRNEW.SetText(Math.abs(fUCLX - fLCLX).toFixed(nFixedLen));
        }

        // RowDblClick
        function fn_OnRowDblClick(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var rowColumns = 'F_MEAINSPCD;F_INSPDETAIL;F_INSPCD;F_INSPNM;' +
                    'F_STANDARD;F_MAX;F_MIN;F_UCLX;F_LCLX;F_UCLR;' +
                    'F_SERIALNO;F_WORKCD;F_WORKNM;' +
                    'F_STANDARDNEW;F_MAXNEW;F_MINNEW;F_UCLXNEW;F_LCLXNEW;F_UCLRNEW;' +
                    'F_ITEMCD;F_ITEMNM;F_FROMDATE;F_CONTENTS';
                devGrid.GetRowValues(e.visibleIndex, rowColumns, fn_OnGetRowValues);
            }
        }

        // OnRowValues
        function fn_OnGetRowValues(rowValues) {

            // 검사항목
            fn_SetTextValue('hidUCITEMCD', rowValues[19]);
            fn_SetTextValue('txtUCITEMCD', rowValues[19]);
            fn_SetTextValue('txtUCITEMNM', rowValues[20]);

            fn_OnSetQCD34Values(rowValues);

            // 검사규격
            fn_SetTextValue('txtSTANDARDNEW', rowValues[13]);
            fn_SetTextValue('txtMAXNEW', rowValues[14]);
            fn_SetTextValue('txtMINNEW', rowValues[15]);

            // 관리규격
            fn_SetTextValue('txtUCLRNEW', rowValues[18]);
            fn_SetTextValue('txtUCLXNEW', rowValues[16]);
            fn_SetTextValue('txtLCLXNEW', rowValues[17]);

            // 검사시작일
            fn_SetDate('txtSTDT', convertDateString(rowValues[21]));

            // 변경사유
            fn_SetTextValue('txtCONTENTS', rowValues[22]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidINSPCD" ClientInstanceName="hidINSPCD" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidSERIALNO" ClientInstanceName="hidSERIALNO" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">검사항목</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목" />
                    </div>
                </div>
            </div>
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="padding-left: 15px; padding-right: 15px;">
                    <label class="col-sm-12 control-label bg-info" style="text-align: left;">현재검사기준</label>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검사규격</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">상한규격</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">하한규격</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">상한목표</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtUCLX" ClientInstanceName="txtUCLX" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">하한목표</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtLCLX" ClientInstanceName="txtLCLX" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">범위</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtUCLR" ClientInstanceName="txtUCLR" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="padding-left: 15px; padding-right: 15px;">
                    <label class="col-sm-12 control-label bg-warning" style="text-align: left;">변경검사기준</label>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">상한목표</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtUCLXNEW" ClientInstanceName="txtUCLXNEW" runat="server" Width="100%">
                            <MaskSettings IncludeLiterals="DecimalSymbol" />
                            <ClientSideEvents Init="fn_OnControlDisableBox" ValueChanged="fn_OnUCLRCal" KeyPress="fn_ValidateOnlyFloatAbs" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">하한목표</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtLCLXNEW" ClientInstanceName="txtLCLXNEW" runat="server" Width="100%">
                            <MaskSettings IncludeLiterals="DecimalSymbol" />
                            <ClientSideEvents Init="fn_OnControlDisableBox" ValueChanged="fn_OnUCLRCal" KeyPress="fn_ValidateOnlyFloat" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">범위</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtUCLRNEW" ClientInstanceName="txtUCLRNEW" runat="server" Width="100%">
                            <MaskSettings IncludeLiterals="DecimalSymbol" />
                            <ClientSideEvents Init="fn_OnControlDisableBox" KeyPress="fn_ValidateOnlyFloat" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">시작일자</label>
                    <div class="col-sm-2">
                        <dx:ASPxDateEdit ID="txtSTDT" ClientInstanceName="txtSTDT" runat="server" Width="100%" NullText="">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">변경사유</label>
                    <div class="col-sm-11">
                        <dx:ASPxTextBox ID="txtCONTENTS" ClientInstanceName="txtCONTENTS" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MODIFYNO" EnableViewState="false" EnableRowsCache="false" PreviewFieldName="F_CONTENTS"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Visible" ShowPreview="True" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowClick="fn_OnRowDblClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="120px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="120px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewBandColumn Name="F_DATEBETWEEN" Caption="적용기간">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_FROMDATE" Caption="시작일" Width="100px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_TODATE" Caption="종료일" Width="100px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Name="F_DATEBETWEEN" Caption="검사기준 및 목표">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_STANDARDNEW" Caption="규격" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_MAXNEW" Caption="상한값" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_MINNEW" Caption="하한값" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_UCLXNEW" Caption="UCLX" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_LCLXNEW" Caption="LCLX" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_UCLRNEW" Caption="UCLR" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="F_INSUSER" Caption="수정자" Width="150px"></dx:GridViewDataColumn>
                    <dx:GridViewDataDateColumn FieldName="F_INSDT" Caption="수정일시" Width="150px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="검사항목코드" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="검사유형" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="검사유형명" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Caption="일련번호" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한값" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한값" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_UCLX" Caption="UCLX" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LCLX" Caption="LCLX" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_UCLR" Caption="UCLR" Visible="false"></dx:GridViewDataColumn>
                </Columns>
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
