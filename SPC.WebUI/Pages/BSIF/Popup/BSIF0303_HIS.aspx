<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0303_HIS.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.Popup.BSIF0303_HIS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            devGrid.PerformCallback('select');
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid.PerformCallback('select');
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경사유를 입력 해 주세요!!');
            else
                devGrid.UpdateEdit();
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
            } else if (s.cpResultParam == 'select') {
                devGrid.AddNewRow();
                s.cpResultParam = "";
            }
            else if (s.cpResultParam == 'save') {
                s.cpResultParam = "";
                parent.fn_hisPopClose();
                
            }
        }


        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_REMARK", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {                
                fn_OnControlDisableBox(s.GetEditor('F_REMARK'), null);

            } else {
                fn_OnControlEnableBox(s.GetEditor('F_REMARK'), null);
            }
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
            
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-2 control-label">품번</label>
                    <div id="divITEMCD" class="col-sm-4"><%=oSetParam.Split('|')[0]%></div>
                    <label class="col-sm-2 control-label">품명</label>
                    <div id="divITEMNM" class="col-sm-4"><%=oSetParam.Split('|')[1] %></div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">공정코드</label>
                    <div id="divWORKCD" class="col-sm-4"><%=oSetParam.Split('|')[2] %></div>
                    <label class="col-sm-2 control-label">공정명</label>
                    <div id="divWORKNM" class="col-sm-4"><%=oSetParam.Split('|')[3] %></div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">검사항목</label>
                    <div id="divINSPDETAIL" class="col-sm-10"><%=oSetParam.Split('|')[5] %></div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="form-group">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_WORKDATE;F_WORKTIME" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback"
                    OnBatchUpdate="devGrid_BatchUpdate" 
                    OnInitNewRow="devGrid_InitNewRow"
                    >
                    <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                    <SettingsBehavior AllowSort="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError"
                        BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" BatchEditEndEditing="fn_OnBatchEditEndEditing" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>                        
                        <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="수정일자" Width="80px" EditFormSettings-Visible="False" >
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="시간" Width="60px" EditFormSettings-Visible="False" >
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_REMARK" Caption="변경사유" Width="100%" >
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKER" Caption="수정자" Width="70px" EditFormSettings-Visible="False" >
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Visible="false" ></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INSPCD" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" ></dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
            </div>
        </div>
    </div>
</asp:Content>