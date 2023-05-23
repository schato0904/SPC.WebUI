<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM1003.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM1003" %>
<%-- 작업지시서 조회 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {
            SetClear();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            fnASPxGridView_ReHeight(devGrid);
        }

        // 조회
        function fn_OnSearchClick() {
            if (schF_MACHCD.GetValue() == null) {
                alert("설비를 선택 후 조회 하세요.");
                return false;
            }
            devGrid.PerformCallback('GET');
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
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else {
                if (!gridIsValid)
                    alert('입력값을 확인해보세요');
                else
                    devGrid.UpdateEdit();
            }
        }

        // 취소
        function fn_OnCancelClick() {
        }

        // 삭제
        function fn_OnDeleteClick() {
        }

        // 인쇄
        function fn_OnPrintClick() {
        }

        // 엑셀
        function fn_OnExcelClick() {
            if (devGrid.GetVisibleRowsOnPage() == 0) {
                ucNavi.SetNoticeOnce('조회된 내용이 없습니다.', 'red');
                return false;
            }
            btnExport.DoClick();
        }
        // 컨트롤값 클리어
        function SetClear() {
        }
        // 기본값 설정
        function SetDefault() {
            
        }

        // 이벤트 핸들러
        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            if (s.cpResultCode != '0') {
                ucNavi.SetNoticeOnce(s.cpResultMsg);
            }
            else {
                alert(s.cpResultMsg);
            }
            s.cpResultCode = "";
            s.cpResultMsg = "";
        }

        // ASPxCallback 사용 : 우측항목 조회, 저장, 삭제
        function devCallback_CallbackComplete(s, e) {
            try {
                if (e.result != '') {
                    var result = JSON.parse(e.result);
                    if (typeof result.msg != 'undefined' && Trim(result.msg) != '') ucNavi.SetNoticeOnce(result.msg);
                    if (typeof result.error != 'undefined' && Trim(result.error) != '') {
                        alert(result.error);
                        return false;
                    }
                    switch (result.action) {
                        case "VIEW":
                            if (typeof result.data == 'string') {
                                result.data = JSON.parse(decodeURIComponent(result.data||'{}'));
                            }
                            fn_OnValidate();
                            break;
                        default:
                            break;
                    }
                }
            } catch (err) {
                alert(err);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }
        // Validate
        function fn_OnValidate(s, e) {
            var result = true;
            result = ASPxClientEdit.ValidateEditorsInContainerById('divBody');
            return result;
        }

        function devGrid_RowDblClick(s, e) {
            //var key = s.GetRowKey(e.visibleIndex);
            //ViewList(key);
        }

        // 검사항목 검색 팝업창 열기
        function fn_OnPopupMeainspSearchBatch(MEAINSPCD, INSPDETAIL) {
            var rootURL = '/' + window.location.href.replace('http://', '').split('/')[1] + '/';
            pPage = rootURL + 'Pages/Common/Popup/PHIL_MEAINSPPOP.aspx' +
                '?TITLE=검사항목조회' +
                '&CRUD=R' +
                '&MEAINSPCD=' + MEAINSPCD +
                '&INSPDETAIL=' + INSPDETAIL;
            fn_OnPopupOpen(pPage, '800', '500');
        }

        // 항목코드조회
        function fn_OnPopupMeainspSearchForm() {
            fn_OnPopupMeainspSearchBatch(devGrid.GetEditor('F_MEAINSPCD').GetText(), devGrid.GetEditor('F_INSPDETAIL').GetText());
        }

        // 검색된 검사항목 세팅
        function fn_OnSettingMeainsp(MACHCD, MACHNM, MEAINSPCD, INSPDETAIL) {
            devGrid.StartEditRow(editRowIndex);
            devGrid.GetEditor('F_MACHCD').SetText(MACHCD);
            devGrid.GetEditor('F_MACHNM').SetText(MACHNM);
            devGrid.GetEditor('F_MEAINSPCD').SetText(MEAINSPCD);
            devGrid.GetEditor('F_INSPDETAIL').SetText(INSPDETAIL);
        }
        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            editRowIndex = parseInt(e.visibleIndex, 10);

            if (editRowIndex >= 0) {
                fn_OnControlDisableBox(s.GetEditor('F_MACHCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MACHNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MEAINSPCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_INSPDETAIL'), null);
                fn_OnControlDisableBox(s.GetEditor('F_RECIPID'), null);
                fn_OnControlDisableBox(s.GetEditor('F_STEP'), null);
                fn_OnControlDisableBox(s.GetEditor('F_SERIALNO'), null);
                fn_OnControlRemoveAttr(s.GetEditor('F_MEAINSPCD'), null, 'ondblclick');
            } else {
                fn_OnControlDisableBox(s.GetEditor('F_SERIALNO'), null);
                fn_OnControlEnableBox(s.GetEditor('F_MACHCD'), null);
                fn_OnControlEnableBox(s.GetEditor('F_MACHNM'), null);
                fn_OnControlEnableBox(s.GetEditor('F_MEAINSPCD'), null);
                fn_OnControlEnableBox(s.GetEditor('F_INSPDETAIL'), null);
                fn_OnControlEnableBox(s.GetEditor('F_RECIPID'), null);
                fn_OnControlEnableBox(s.GetEditor('F_STEP'), null);
                fn_OnControlAddAttr(s.GetEditor('F_MEAINSPCD'), null, 'ondblclick', "fn_OnPopupMeainspSearchForm()");
            }
            //fn_OnControlDisableBox(s.GetEditor('F_MEASURE'), null);
        }

        // 검사기준복사 팝업창 열기
        function fn_OnPopupPHIL34Copy() {
            pPage = rootURL + 'Pages/Common/Popup/COPYPHIL34POP.aspx' +
                '?TITLE=검사기준복사' +
                '&CRUD=S';
            fn_OnPopupOpen(pPage, '800', '500');
        }


        function fn_schF_MACHCD_ValueChanged(s, e) {
            if (s.GetValue() != null) {
                schF_RECIPID.PerformCallback();
                schF_STEP.PerformCallback();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divTop" class="form-horizontal">
            <div id="divMiddle" style="float:left;margin-bottom:2px;width:100%;">
                <table class="InputTable" style="width:100%;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">설비</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxComboBox runat="server" ID="schF_MACHCD" ClientInstanceName="schF_MACHCD" IncrementalFilteringMode="None" ValueType="System.String" >
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                    <ClientSideEvents ValueChanged="fn_schF_MACHCD_ValueChanged" />
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">레시피</td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_RECIPID" ClientInstanceName="schF_RECIPID" IncrementalFilteringMode="None" ValueType="System.String" OnCallback="schF_RECIPID_Callback" Width="100%" >
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">STEP</td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_STEP" ClientInstanceName="schF_STEP" IncrementalFilteringMode="None" ValueType="System.String"  OnCallback="schF_STEP_Callback" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">기준복사</td>
                        <td class="tdContent">
                            <button class="btn btn-sm btn-success" onclick="fn_OnPopupPHIL34Copy(); return false;">
                                <i class="fa fa-copy"></i>
                                <span class="text">검사기준복사</span>
                            </button>
                        </td>
                    </tr>
                </table>
            </div>
         </div>
        <div style="clear:both;height:2px;"></div>
        <div id="divBody">
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MACHCD;F_RECIPID;F_SERIALNO;F_STEP" EnableViewState="false" EnableRowsCache="false" OnCellEditorInitialize="devGrid_CellEditorInitialize"
                    OnCustomCallback="devGrid_CustomCallback" OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                    OnDataBinding="devGrid_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                    <SettingsBehavior AllowSort="false" AllowDragDrop="False" ColumnResizeMode="Control" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                    <ClientSideEvents Init="fn_OnGridInit"   EndCallback="fn_OnGridEndCallback"   CallbackError="fn_OnCallbackError"  RowDblClick="devGrid_RowDblClick" BatchEditStartEditing="fn_OnBatchEditStartEditing"/>
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount"  runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="40px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_MACHCD" Caption="설비코드" Width="60px"/>
                        <dx:GridViewDataTextColumn FieldName="F_MACHNM" Caption="설비명" Width="120px"/>
                        <dx:GridViewDataTextColumn FieldName="F_MEAINSPCD" Caption="항목코드" Width="80px" />
                        <dx:GridViewDataTextColumn FieldName="F_INSPDETAIL" Caption="항목명칭" Width="150px" CellStyle-HorizontalAlign="Left"/>
                        <dx:GridViewDataTextColumn FieldName="F_SERIALNO" Caption="일련번호" Width="70px"/>
                        <dx:GridViewDataTextColumn FieldName="F_RECIPID" Caption="레시피" Width="120px" />
                        <dx:GridViewDataComboBoxColumn FieldName="F_STEP" Caption="STEP" Width="120px" CellStyle-HorizontalAlign="Left">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="F_DISPLAYNO" Caption="순번" Width="60px" />
                        <dx:GridViewDataTextColumn FieldName="F_RESULTSTAND" Caption="표시규격" Width="80px" />
                        <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="기준값" Width="70px" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="MIN" Width="70px"  CellStyle-HorizontalAlign="Right"/>
                        <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="MAX" Width="70px" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataTextColumn FieldName="F_UCLX" Caption="UCLX" Width="70px" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataTextColumn FieldName="F_LCLX" Caption="LCLX" Width="70px" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataTextColumn FieldName="F_UCLR" Caption="관리공차" Width="70px" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataComboBoxColumn FieldName="F_RANK" Caption="품질수준" Width="80px">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="F_UNIT" Caption="단위" Width="70px">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="F_ZIG" Caption="보정치" Width="60px" />
                        <dx:GridViewDataTextColumn FieldName="F_ZERO" Caption="제로세팅" Width="70px" />
                        <dx:GridViewDataTextColumn FieldName="F_MEASCD1" Caption="측정매핑코드" Width="100px" />
                    </Columns>
                </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
        </div>
        <div style="clear:both;"></div>
    </div>
    <div id="divHidden" style="display:none;">
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
        <dx:ASPxTextBox runat="server" ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>
    </div>
</asp:Content>