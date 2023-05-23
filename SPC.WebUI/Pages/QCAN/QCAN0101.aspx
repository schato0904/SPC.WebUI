<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="QCAN0101.aspx.cs" Inherits="SPC.WebUI.Pages.QCAN.QCAN0101" %>

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

            <%if (!gsVENDOR)
              {%>
            if (hidCOMPCD.GetValue() == "" || hidCOMPCD.GetValue() == null) {
                alert("업체를 선택하세요!!");
                return false;
            }
            if (hidFACTCD.GetValue() == "" || hidFACTCD.GetValue() == null) {
                alert("공장을 선택하세요!!");
                return false;
            }
            <%}%>

            //if ((hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) && (hidUCWORKPOPCD.GetValue() == "" || hidUCWORKPOPCD.GetValue() == null)) {
            //    alert("품목이나 공정을 선택하세요!!");
            //    return false;
            //}

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
            fn_OnBatchValidate("F_BANCD", s, e);
            fn_OnBatchValidate("F_BANNM", s, e);

            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlDisableBox(editor, null);
            } else {
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlEnableBox(editor, null);
            }
        }

        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            txtSERIALNO.SetText(resultValues[10]);
            //alert(txtSERIALNO.GetText());
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="display: <%if (!gsVENDOR)
                                                          {%>display<%}
                                                          else
                                                          { %>none<%} %>">
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
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" ID="ucDate" MaxMonth="1" />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">검사항목</label>
                    <div class="col-sm-3">
                        <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목" />
                        <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" Width="100%" class="form-control input-sm" />
                    </div>
                    <label class="col-sm-1 control-label">관리한계</label>
                    <div class="col-sm-4">
                        <dx:ASPxRadioButtonList ID="rdoHIPISNG" ClientInstanceName="rdoHIPISNG" runat="server"
                            RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                            <Items>
                                <dx:ListEditItem Value="2" Text="관리용" Selected="true" />
                                <dx:ListEditItem Value="1" Text="계산식[NG포함]" />
                                <dx:ListEditItem Value="3" Text="계산식[NG제외]" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="100px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="80px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewBandColumn Caption="규격">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="USL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="LSL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="CL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>

                    <dx:GridViewBandColumn Caption="관리한계">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_UCLX" Caption="UCL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_LCLX" Caption="LCL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_UCLR" Caption="R" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="측정 DATA">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_SIRYO" Caption="시료수" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MAXVALUE" Caption="최대치" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MINVALUE" Caption="최소치" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_XBAR" Caption="X 평균" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false"  />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_XRANGE" Caption="R 범위" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_SD" Caption="표준편차" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false"  />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_3SPLUS" Caption="+3S" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false"  />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_3SMINUS" Caption="-3S" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false"  />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>

                    <dx:GridViewBandColumn Caption="공정능력">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_CP" Caption="CP" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" >
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_CPK" Caption="CPK" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false"/>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>

                    <dx:GridViewBandColumn Caption="공정분석">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_SIGMAL" Caption="6시그마(장)" Width="85px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" >
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_SIGMAS" Caption="6시그마(단)" Width="85px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_RERATE" Caption="예상불합격률" Width="95px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" DisplayFormatString="n2"/>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_PPM" Caption="예상PPM" Width="85px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false" Visible="false"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="100px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="80px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewBandColumn Caption="규격">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="USL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CAUSECD">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="LSL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" ClientInstanceName="F_CONTENTS" />
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="CL" Width="80px">
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>

                    <dx:GridViewBandColumn Caption="관리한계">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_UCLX" Caption="UCL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_LCLX" Caption="LCL" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_UCLR" Caption="R" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>

                    <dx:GridViewDataColumn FieldName="f_lotcnt" Caption="LOT번호" Width="70px" />

                    <dx:GridViewBandColumn Caption="측정 DATA">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_SIRYO" Caption="시료수" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MAXVALUE" Caption="최대치" Width="80px">
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MINVALUE" Caption="최소치" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_XBAR" Caption="X 평균" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_XRANGE" Caption="R 범위" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_SD" Caption="표준편차" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_3SPLUS" Caption="+3S" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_3SMINUS" Caption="-3S" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>

                    <dx:GridViewBandColumn Caption="공정능력">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_CP" Caption="CP" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_CPK" Caption="CPK" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>

                    <dx:GridViewBandColumn Caption="공정분석">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_SIGMAL" Caption="6시그마(장)" Width="85px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_SIGMAS" Caption="6시그마(단)" Width="85px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_RERATE" Caption="예상불합격률" Width="95px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_PPM" Caption="예상PPM" Width="85px">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
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
