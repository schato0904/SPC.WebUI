<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0201.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0201" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var Index = 0;
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
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            } 
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            //fn_OnBatchValidate("F_BANCD", s, e);
            //fn_OnBatchValidate("F_BANNM", s, e);            
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            var visibleIndex = e.visibleIndex;
            var F_CAUSECD = s.GetEditor('F_CAUSECD').GetInputElement();
            var F_MANAGECD = s.GetEditor('F_MANAGECD').GetInputElement();
            F_CAUSECD.ondblclick = function () { fn_CausePOP(visibleIndex, 'F_CAUSECD', '31'); }
            F_MANAGECD.ondblclick = function () { fn_CausePOP(visibleIndex, 'F_MANAGECD', '32'); }

            if (parseInt(visibleIndex, 10) >= 0) {
                fn_OnControlDisableBox(s.GetEditor('F_CONTENTS'), null);
                fn_OnControlDisableBox(s.GetEditor('F_RETURNCO'), null);
            } else {
                fn_OnControlEnableBox(s.GetEditor('F_CONTENTS'), null);
                fn_OnControlEnableBox(s.GetEditor('F_RETURNCO'), null);
            }
        }

        function fn_CausePOP(idx, col, gbn) {
            fn_OnPopupCauseSearch(idx, devGrid.batchEditApi.GetCellValue(idx, col), gbn)
        }
        
        function fn_OnSetRetuenValue(idx, col, code, codenm) {
            if (col == "31") {
                devGrid.batchEditApi.SetCellValue(idx, "F_CAUSECD", code);
                devGrid.batchEditApi.SetCellValue(idx, "F_CONTENTS", codenm);
            } else {
                devGrid.batchEditApi.SetCellValue(idx, "F_MANAGECD", code);
                devGrid.batchEditApi.SetCellValue(idx, "F_RETURNCO", codenm);
            }
            
        }

        function fn_OnRowDblClick(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var errQty = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_ERRQTY')
                var ovrQty = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_OVRQTY')

                if (errQty + ovrQty < 1) return false;

                var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(e.visibleIndex));
                var FromDt = txtFROMDT.GetText();
                var ToDt = txtTODT.GetText();

                //fn_OnPopupLineMonitoring("<%=GetCompCD()%>" + '|' + "<%=GetFactCD()%>" + '|' + rowKeys + '|' + FromDt + '|' + ToDt);
            }
        }

        function fn_OpenANLS0101(s, e) {
            var visibleIndex = e.visibleIndex
            if (visibleIndex < 0) return;

            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(e.visibleIndex));

            if (rowKeys.substr(rowKeys.length - 13, 6) == 'AAC501') {
                var oParams = "";

                oParams += fn_GetCastText('hidUCFROMDT') + '|'
                oParams += fn_GetCastText('hidUCTODT') + '|'
                oParams += rowKeys;

                parent.fn_OnDeleteTab('ANLS0101', parent.fn_OnGetTabObject('ANLS0101'));
                parent.parent.doCreateTab('MM03|MM0301|ANLS|ANLS0101|품질종합현황|RP|1', oParams);
            } else {
                //alert(rowKeys.substr(rowKeys.length - 13, 6));
                alert('성능검사인 경우만 SPC 분석이 가능합니다!!');
            }

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Date runat="server" id="ucDate" />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>    
                                 
                </div>             
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Line ID="ucLine" runat="server"/>
                    </div>                
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Work ID="ucWork" runat="server" /> 
                    </div>   

                    <label class="col-sm-1 control-label">
                        <dx:ASPxCheckBox ID="chkImport" runat="server" >                            
                        </dx:ASPxCheckBox>
                        중요항목
                    </label>
                    
                </div>
            </div>
        </div>
        <div class="content">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN;F_WORKDATE;F_WORKTIME;F_PCNM;F_MEASNO;F_INSPCD;F_TSERIALNO" 
                    EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize"
                    OnHtmlRowPrepared="devGrid_HtmlRowPrepared" OnBatchUpdate="devGrid_BatchUpdate" >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  HorizontalScrollBarMode="Visible" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick"  />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"  CustomButtonClick="fn_OpenANLS0101"
                        BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                    <Columns>           
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                    ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn Caption="SPC분석" Width="80px"  >
                        <CustomButtons>                            
                            <dx:GridViewCommandColumnCustomButton ID="보기" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>             
                        <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="작업일자" Width="80px" >
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="작업시간" Width="70px" >
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>                        
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="120px"  >
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명"   Width="200px"  >
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명"  Width="200px" >
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="120px"   >
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewBandColumn Caption="이상/조치내용">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_CAUSECD"  Caption="공정원인코드" Width="85px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CAUSECD" >
                                    </PropertiesTextEdit>                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CONTENTS" Caption="공정원인내용" Width="150px"   >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CONTENTS" />
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_MANAGECD" Caption="조치코드" Width="85px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_RETURNCO" Caption="조치내용" Width="150px"   >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_RETURNCO" />
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="검사기준">
                            <Columns>                                
                                <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Width="70px"   >
                                    <CellStyle HorizontalAlign="Right" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격" Width="70px"   >
                                    <CellStyle HorizontalAlign="Right" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격" Width="70px"   >
                                    <CellStyle HorizontalAlign="Right" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_UNIT" Caption="단위" Width="60px"   >
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값" Width="70px"   >
                            <CellStyle HorizontalAlign="Right" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>    
                        <dx:GridViewDataColumn FieldName="F_ERR" Caption="판정"  Width="70px"  >
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="F_RETURNMAN" Caption="회신자" Width="80px"   >
                            <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자" Width="70px"   >
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>                    
                        <dx:GridViewDataTextColumn FieldName="F_REMARK" Caption="기타사항" Width="200px"   >
                            <CellStyle HorizontalAlign="Left" />
                            <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                        </dx:GridViewDataTextColumn>                        

                        <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false"   />
                        <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Visible="false"   />
                        <dx:GridViewDataColumn FieldName="F_SIRYO" Visible="false"   />
                        <dx:GridViewDataColumn FieldName="F_FREEPOINT" Visible="false"   />
                        <dx:GridViewDataColumn FieldName="F_WORKCD"  Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_MEASNO"  Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_PCNM"  Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_INSPCD"  Visible="false" />

                        <dx:GridViewDataColumn FieldName="F_RETURNCHECK"  Width="0px"  />
                    </Columns>                
                </dx:ASPxGridView>
            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN;F_WORKDATE;F_WORKTIME;F_PCNM;F_MEASNO;F_INSPCD" 
                    EnableViewState="false" EnableRowsCache="false" Visible="false"
                    OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize"
                    OnHtmlRowPrepared="devGrid_HtmlRowPrepared" OnBatchUpdate="devGrid_BatchUpdate" >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  HorizontalScrollBarMode="Visible" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick"  />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Columns>           
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                    ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn Caption="SPC분석" Width="80px"  >
                        <CustomButtons>                            
                            <dx:GridViewCommandColumnCustomButton ID="GridViewCommandColumnCustomButton1" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>             
                        <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="작업일자" Width="80px" >
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="작업시간" Width="70px" >
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>                        
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="120px"  >
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명"   Width="200px"  >
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명"  Width="200px" >
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="120px"   >
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewBandColumn Caption="이상/조치내용">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_CAUSECD"  Caption="공정원인코드" Width="85px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CAUSECD" >
                                    </PropertiesTextEdit>                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CONTENTS" Caption="공정원인내용" Width="150px"   >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CONTENTS" />
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_MANAGECD" Caption="조치코드" Width="85px" >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" />                            
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_RETURNCO" Caption="조치내용" Width="150px"   >
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_RETURNCO" />
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="검사기준">
                            <Columns>                                
                                <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Width="70px"   >
                                    <CellStyle HorizontalAlign="Right" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격" Width="70px"   >
                                    <CellStyle HorizontalAlign="Right" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격" Width="70px"   >
                                    <CellStyle HorizontalAlign="Right" />
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_UNIT" Caption="단위" Width="60px"   >
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값" Width="70px"   >
                            <CellStyle HorizontalAlign="Right" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>    
                        <dx:GridViewDataTextColumn FieldName="F_RETURNMAN" Caption="회신자" Width="80px"   >
                            <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자" Width="70px"   >
                            <CellStyle HorizontalAlign="Left" />
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>                    
                        <dx:GridViewDataTextColumn FieldName="F_REMARK" Caption="기타사항" Width="200px"   >
                            <CellStyle HorizontalAlign="Left" />
                            <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                        </dx:GridViewDataTextColumn>                        

                        <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false"   />
                        <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Visible="false"   />
                        <dx:GridViewDataColumn FieldName="F_SIRYO" Visible="false"   />
                        <dx:GridViewDataColumn FieldName="F_FREEPOINT" Visible="false"   />
                        <dx:GridViewDataColumn FieldName="F_WORKCD"  Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_MEASNO"  Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_PCNM"  Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_INSPCD"  Visible="false" />

                        <dx:GridViewDataColumn FieldName="F_RETURNCHECK"  Width="0px"  />
                    </Columns>                
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid2" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
