<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PFRC0201.aspx.cs" Inherits="SPC.WebUI.Pages.PFRC.PFRC0201" %>
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
        function fn_OnNewClick(s, e) {
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
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_PGMID", s, e);
            fn_OnBatchValidate("F_PGMNMKR", s, e);
            fn_OnBatchValidate("F_LINK", s, e);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom:10px;border:1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-2 control-label">프로그램ID</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtPGMID" ClientInstanceName="txtPGMID" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">사용여부</label>
                    <div class="col-sm-3">
                        <dx:ASPxRadioButtonList ID="rdoSTATUS" ClientInstanceName="rdoSTATUS" runat="server"
                            RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None" ValueField="CODE" TextField="TEXT">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true"/>
                                <dx:ListEditItem Text="사용함" Value="1" />
                                <dx:ListEditItem Text="사용안함" Value="0" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </div>
                    <label class="col-sm-3 control-label"></label>
                </div>
            </div>
        </div>
        <div class="content">
            <section class="panel panel-default" style="height: 100%;">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_PGMID" EnableViewState="false" EnableRowsCache="false"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCustomCallback="devGrid_CustomCallback"
                    OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                    <SettingsBehavior AllowSort="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditRowValidating="fn_OnValidate" />
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
                        <dx:GridViewDataTextColumn FieldName="F_PGMID" Caption="프로그램ID" Width="150px">
                            <Settings AllowSort="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewBandColumn Name="F_PGMNM" Caption="프로그램명">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_PGMNMKR" Caption="프로그램명(한글)" Width="100%">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                    <Settings AllowSort="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_PGMNMUS" Caption="프로그램명(영문)" Width="150px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_PGMNMCN" Caption="프로그램명(중문)" Width="150px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                    <PropertiesTextEdit ConvertEmptyStringToNull="false"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataTextColumn FieldName="F_LINK" Caption="프로그램경로" Width="100px"></dx:GridViewDataTextColumn>
                        <dx:GridViewBandColumn Name="F_TOOLBAR" Caption="페이지 기능버튼 사용여부">
                            <Columns>
                                <dx:GridViewDataCheckColumn FieldName="F_TOOLBARR" Caption="조회" Width="40px"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="F_TOOLBARC" Caption="입력" Width="40px"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="F_TOOLBARU" Caption="수정" Width="40px"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="F_TOOLBARW" Caption="취소" Width="40px"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="F_TOOLBARS" Caption="저장" Width="40px"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="F_TOOLBARD" Caption="삭제" Width="40px"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="F_TOOLBARP" Caption="인쇄" Width="40px"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="F_TOOLBARE" Caption="엑셀" Width="40px"></dx:GridViewDataCheckColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용<br/>여부" Width="40px"></dx:GridViewDataCheckColumn>
                    </Columns>
                </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            </section>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>