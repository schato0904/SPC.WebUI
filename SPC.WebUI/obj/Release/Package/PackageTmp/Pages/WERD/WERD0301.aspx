<%@ Page Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD3001.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD3001" %>
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
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));

            devGrid1.SetHeight(height - 78);
            devGrid2.SetHeight(height - 73); //187
            
        }

        // 조회
        function fn_OnSearchClick() {
            
            //var rdoGbn = $("input[name='<%=rdoGbn.UniqueID%>']").attr("value");
            var rdoGbn = ASPxClientComboBox.Cast("rdoGbn").GetValue();
            if (rdoGbn == "0")
                devGrid1.PerformCallback();
            else
                devGrid2.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
        }

        // 취소
        function fn_OnCancelClick() {
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
            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    fn_AdjustSize();

                    // 페이저 Callback
                    if (s.cpPagerTarget == '0')
                        ucPager1.fn_pagerPerformCallback(s.cpResultMsg);
                    else
                        ucPager2.fn_pagerPerformCallback(s.cpResultMsg);
                }
                else {
                    alert(s.cpResultMsg);
                    //fn_doSetGridEventAction('false');
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
        }

        function fn_CallbackComplete(s, e) {
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            //alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            //return ASPxClientEdit.ValidateEditorsInContainerById('cbpContent');
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
        }

        function devGrid_RowDblClick(s, e) {

        }

        // 요청사 조회 클릭
        function fn_OpenCustPop() {
            //fn_OnPopupCustSearch("SetCust", srcF_REQCUSTID.GetText());
        }

        // 팝업에서 결과 돌려줄때 사용되는 콜백함수
        // 결과값은 json형태로 파라미터로 전달받음
        function SetCust(returnValue) {
            //if (Trim(returnValue.F_CUSTID) != "") {
            //    srcF_REQCUSTID.SetText(returnValue.F_CUSTID);
            //}

            //if (Trim(returnValue.F_CUSTNM) != "") {
            //    srcF_REQCUSTNM.SetText(returnValue.F_CUSTNM);
            //}
        }

        function fn_CodeText_LostFocus(s, cdType, cdField, nmField) {
            ////var cdField = "";
            ////var nmField = "";
            //var dbCdField = "";
            //var dbNmField = "";
            //ASPxClientTextBox.Cast(nmField).SetText("");
            //switch (cdType) {
            //    case "DTMNTYPE":
            //        dbCdField = "F_TYPECD";
            //        dbNmField = "F_TYPENMFULL";
            //        fn_GetF_TYPENM(s.GetText(), _DOCGRPCD, AfterGetData);
            //        break;
            //    case "DEPT":
            //        dbCdField = "F_DEPTCD";
            //        dbNmField = "F_DEPTNM";
            //        fn_GetF_DEPTNM(s.GetText(), AfterGetData);
            //        break;
            //    case "USER":
            //        dbCdField = "F_USERID";
            //        dbNmField = "F_USERNM";
            //        fn_GetF_USERNM(s.GetText(), AfterGetData);
            //        break;
            //    case "CUST":
            //        dbCdField = "F_CUSTID";
            //        dbNmField = "F_CUSTNM";
            //        fn_GetF_CUSTNM(s.GetText(), AfterGetData);
            //        break;
            //}

            //function AfterGetData(returnJSON) {
            //var cdControl = s;
            //var nmControl = ASPxClientTextBox.Cast(nmField)
            //nmControl.SetText("");
            //if (Trim(returnJSON[dbNmField]) != "") {
            //    cdControl.SetText(returnJSON[dbCdField]);
            //    nmControl.SetText(returnJSON[dbNmField]);
            //}
            //}
        }

        function fn_rboGbnChange(s, e) {
            if (s.GetValue() == "0") {
                $("#prinDiv1").css("display", "");
                $("#prinDiv2").css("display", "none");
                $("#pagingDiv1").css("display", "");
                $("#pagingDiv2").css("display", "none");
                devGrid1.PerformCallback("clear;gbn=" + s.GetValue());
            }
            else {
                $("#prinDiv1").css("display", "none");
                $("#prinDiv2").css("display", "");
                $("#pagingDiv1").css("display", "none");
                $("#pagingDiv2").css("display", "");
                devGrid2.PerformCallback("clear;gbn=" + s.GetValue());
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" />
    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
    <div class="container">
        <table class="InputTable">
            <colgroup>
                <col style="width:10%" />
                <col style="width:23%" />
                <col style="width:10%" />
                <col style="width:23%" />
                <col style="width:10%" />
                <col style="width:24%" />
            </colgroup>
            <tr>
                <td class="tdTitle">
                    <label>해당연월</label>
                </td>
                <td class="tdContent">
                   <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" MaxDate="334"  MonthOnly="true" MaxMonth="12"  />
                </td>
                 <td class="tdTitle">
                    <label>조회조건</label>
                </td>
                <td class="tdContent">
                    <dx:ASPxRadioButtonList ID="rdoGbn" ClientInstanceName="rdoGbn" runat="server"
                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None" CssClass="devRdo">
                        <ClientSideEvents ValueChanged="fn_rboGbnChange" />   
                        <Items>
                            <dx:ListEditItem Selected="true" Text="공정별집계" Value="0" />
                            <dx:ListEditItem Text="부적합수량집계" Value="1" />
                        </Items>                     
                    </dx:ASPxRadioButtonList>
                </td>
                <td class="tdTitle">
                    <label>공정코드</label>
                </td>
                <td class="tdContentR">
                    <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                </td>
            </tr>
         </table>
        <div class="form-group"></div>
        <div class="content" id="prinDiv1">
            <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="False"  Width="100%"
                KeyFieldName="F_WORKCD" EnableViewState="False" EnableRowsCache="False"
                OnCustomCallback="devGrid_CustomCallback" OnCustomSummaryCalculate="devGrid_CustomSummaryCalculate">
                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" ShowFooter="true"/>
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"/>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명"  Width="20%">
                        <CellStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <span>합계</span>
                        </FooterTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PRODUCTQTY" Caption="생산수량"  Width="13%">
                        <CellStyle HorizontalAlign="Right"  /> 
                         <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_INSPQTY" Caption="검사수량"  Width="13%">
                        <CellStyle HorizontalAlign="Right" />
                         <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_NGQTY" Caption="부적합수량"  Width="13%">
                        <CellStyle HorizontalAlign="Right" />
                         <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_NGRATE" Caption="부적합률(%)"  Width="13%">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PPM" Caption="PPM"  Width="13%">
                        <CellStyle HorizontalAlign="Right" />
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LOSSAMT" Caption="손실금액(원)" Width="13%">
                        <CellStyle HorizontalAlign="Right" />
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                </Columns>
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="F_WORKCD" SummaryType="Custom" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_WORKNM" DisplayFormat="합계" />
                    <dx:ASPxSummaryItem FieldName="F_PRODUCTQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_INSPQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_NGQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_NGRATE" SummaryType="Custom" DisplayFormat="{0:##0.##}" />
                    <dx:ASPxSummaryItem FieldName="F_PPM" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_LOSSAMT" SummaryType="Custom" DisplayFormat="{0:n0}" />
                </TotalSummary>
            </dx:ASPxGridView>
        </div>
        <div class="content" id="prinDiv2" style="display:none;">
            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="False"  Width="100%"
                KeyFieldName="F_WORKCD" EnableViewState="False" EnableRowsCache="False"
                OnCustomCallback="devGrid_CustomCallback" OnCustomSummaryCalculate="devGrid_CustomSummaryCalculate">
                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>                    
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" ShowFooter="true" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"/>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명"  Width="10%">
                        <CellStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <span>합계</span>
                        </FooterTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PRODUCTQTY" Caption="생산수량"  Width="9%">
                        <CellStyle HorizontalAlign="Right" />
                         <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_INSPQTY" Caption="검사수량"  Width="9%">
                        <CellStyle HorizontalAlign="Right" />
                         <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_NGQTY" Caption="부적합수량"  Width="9%">
                        <CellStyle HorizontalAlign="Right" />
                         <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_NGRATE" Caption="부적합률(%)"  Width="9%">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PPM" Caption="PPM"  Width="9%">
                        <CellStyle HorizontalAlign="Right" />
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LOSSAMT" Caption="손실금액(원)" Width="9%">
                        <CellStyle HorizontalAlign="Right" />
                        <PropertiesTextEdit DisplayFormatString="n0" />
                    </dx:GridViewDataTextColumn>
                   

                     <dx:GridViewBandColumn Caption="부적합유형">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_NGTYPENM" Caption="내용" Width="10%">
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_NGTYPECNT" Caption="수량" Width="7%">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit DisplayFormatString="n0" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="부적합원인">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_NGCAUSENM" Caption="내용" Width="10%">
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_NGCAUSECNT" Caption="수량" Width="7%">
                                <CellStyle HorizontalAlign="Right" />
                                <PropertiesTextEdit DisplayFormatString="n0" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="F_WORKCD" SummaryType="Custom" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="F_PRODUCTQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_INSPQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_NGQTY" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_NGRATE" SummaryType="Custom" DisplayFormat="{0:##0.##}" />
                    <dx:ASPxSummaryItem FieldName="F_PPM" SummaryType="Custom" DisplayFormat="{0:n0}" />
                    <dx:ASPxSummaryItem FieldName="F_LOSSAMT" SummaryType="Custom" DisplayFormat="{0:n0}" />
                </TotalSummary>
            </dx:ASPxGridView>
        </div>
        <div class="paging" id="pagingDiv1">
            <ucCTF:PagerMulti ID="ucPager1" runat="server" PageSize="50" targetCtrls="devGrid1" />            
        </div>
        <div class="paging" id="pagingDiv2" style="display:none;">
            <ucCTF:PagerMulti ID="ucPager2" runat="server" PageSize="50" targetCtrls="devGrid2" />
        </div>
    </div>
</asp:Content>

