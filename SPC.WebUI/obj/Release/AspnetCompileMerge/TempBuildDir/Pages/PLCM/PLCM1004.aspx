<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM1004.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM1004" %>
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
        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            //editRowIndex = parseInt(e.visibleIndex, 10);
            //fn_OnControlDisableBox(s.GetEditor('F_MEASURE'), null);
            //fn_OnControlEnableBox(s.GetEditor('F_MACHCD'), null);
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
                        <td class="tdContentR">
                            <div style="width:100%;float:left;">
                                <dx:ASPxComboBox runat="server" ID="schF_MACHCD" ClientInstanceName="schF_MACHCD" IncrementalFilteringMode="None" ValueType="System.String" >
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdContent" colspan="4"></td>
                        <td class="tdTitle">검색일자</td>
                        <td class="tdContent">
                            
                        </td>
                    </tr>
                </table>
            </div>
         </div>
        <div style="clear:both;height:2px;"></div>
        <div id="divBody">
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_WORKDATE2;F_MACHCD;F_RECIPID;F_SERIALNO" EnableViewState="false" EnableRowsCache="false" 
                    OnCustomCallback="devGrid_CustomCallback" OnBatchUpdate="devGrid_BatchUpdate"
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
                    <ClientSideEvents Init="fn_OnGridInit"   EndCallback="fn_OnGridEndCallback"   CallbackError="fn_OnCallbackError"/>
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount"  runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="40px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_WORKDATE" Caption="변경일시" Visible="false"/>
                        <dx:GridViewDataTextColumn FieldName="F_WORKDATE2" Caption="변경일시" Width="160px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_MACHCD" Caption="설비코드" Width="65px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_MACHNM" Caption="설비명" Width="120px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_RECIPID" Caption="레시피" Width="120px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_SERIALNO" Caption="일련번호" Width="70px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_MIN_OLD" Caption="MIN(이전)" Width="80px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_MAX_OLD" Caption="MAX(이전)" Width="80px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_UCLX_OLD" Caption="UCLX(이전)" Width="80px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_LCLX_OLD" Caption="LCLX(이전)" Width="80px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="MIN" Width="70px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="MAX" Width="70px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_UCLX" Caption="UCLX" Width="70px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_LCLX" Caption="LCLX" Width="70px" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_WORKER" Caption="작업자" Width="100px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_REMARK" Caption="비고" Width="200px" />
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