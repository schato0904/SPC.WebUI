<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SYST0103.aspx.cs" Inherits="SPC.WebUI.Pages.SYST.SYST0103" %>
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

        }

        // 인쇄
        function fn_OnPrintClick() {
        }

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

                    if (0 < s.cpResultcount) {
                        var Param = s.cpResultreInert.split('^');

                            for (var j = 1; j <= s.cpResultcount ; j++) {
                                var Val = Param[j - 1].split('|');
                                    
                                devGrid.AddNewRow();

                                devGrid.SetEditValue('F_USERNM', Val[0]);
                                devGrid.SetEditValue('F_GRADECD', Val[1]);
                                devGrid.SetEditValue('F_DEPARTCD', Val[2]);
                                devGrid.SetEditValue('F_GROUPCD', Val[3]);
                                devGrid.SetEditValue('F_USERID', Val[4]);
                                devGrid.SetEditValue('F_USERPW', Val[5]);
                                devGrid.SetEditValue('F_EMAIL', Val[6]);
                                devGrid.SetEditValue('F_FACTCD', Val[7]); 
                            }
                    }

                    s.cpResultreInert = "";
                    s.cpResultcount = "";
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
            fn_OnBatchValidate("F_USERNM", s, e);
            fn_OnBatchValidate("F_GRADECD", s, e);
            fn_OnBatchValidate("F_DEPARTCD", s, e);
            fn_OnBatchValidate("F_GROUPCD", s, e);
            fn_OnBatchValidate("F_USERID", s, e);
            fn_OnBatchValidate("F_USERPW", s, e);
            fn_OnBatchValidate("F_FACTCD", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            editRowIndex = parseInt(e.visibleIndex, 10);

            if (editRowIndex >= 0) {
                fn_OnControlDisableBox(s.GetEditor('F_USERID'), null);
            } else {
                fn_OnControlEnableBox(s.GetEditor('F_USERID'), null);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">사업장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">아이디</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="txtUSERID" ClientInstanceName="txtUSERID" runat="server" Width="100%" />
                    </div>
                    <label class="col-sm-1 control-label">성명</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxTextBox ID="txtUSERNM" ClientInstanceName="txtUSERNM" runat="server" Width="100%" />
                    </div>
                    <label class="col-sm-1 control-label">사용</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxRadioButtonList ID="rdoSTATUS" ClientInstanceName="rdoSTATUS" runat="server"
                            RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None" ValueField="CODE" TextField="TEXT">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" />
                                <dx:ListEditItem Text="사용함" Value="1" />
                                <dx:ListEditItem Text="사용안함" Value="0" />
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
                KeyFieldName="F_USERID" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnCellEditorInitialize="devGrid_CellEditorInitialize" OnBatchUpdate="devGrid_BatchUpdate"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="F_USERNM" Caption="성명" Width="200px">
                        <Settings AllowSort="True" />
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_GRADECD" Caption="직위" Width="100px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_DEPARTCD" Caption="부서" Width="120px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_GROUPCD" Caption="작업그룹" Width="90px">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="F_USERID" Caption="아이디" Width="100px">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_USERPW" Caption="비밀번호" Width="100px" PropertiesTextEdit-Password="true">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EMAIL" Caption="이메일" Width="150px">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MOBILENO" Caption="모바일" Width="100px">
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_FACTCD" Caption="사업장" Width="100px">
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용여부" Width="70px"></dx:GridViewDataCheckColumn>
                    <dx:GridViewDataColumn FieldName="F_GRADENM" Caption="직위명" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_DEPARTNM" Caption="부서명" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_GROUPNM" Caption="작업그룹명" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_FACTNM" Caption="사업장명" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
