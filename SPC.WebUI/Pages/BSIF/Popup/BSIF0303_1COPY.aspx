<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0303_1COPY.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.Popup.BSIF0303_1COPY" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height();
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
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }

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

                    devGrid.PerformCallback();
                }
            }


        }

        // 검사규격과 분석결과를 세팅한다
        function fn_OnSetSearchResult(result) {
            var resultValues1 = result.split('|');
            // 분석결과
            $('#tdResult01').text(resultValues1[0]);
            $('#tdResult02').text(resultValues1[1]);
            $('#tdResult03').text(resultValues1[2]);
            $('#tdResult04').text(resultValues1[3]);
            $('#tdResult05').text(resultValues1[4]);
            $('#tdResult06').text(resultValues1[5]);
            $('#tdResult07').text(resultValues1[6]);
            $('#tdResult08').text(resultValues1[7]);
            $('#tdResult09').text(resultValues1[8]);

        }
        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-2 control-label">품목코드</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox runat="server" ID="txtItemcd" ClientInstanceName="txtItemcd" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">품목명</label>
                    <div class="col-sm-2">
                        <dx:ASPxTextBox runat="server" ID="txtItemnm" ClientInstanceName="txtItemnm" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">검사규격</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox runat="server" ID="txtStandard" ClientInstanceName="txtStandard" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">항목코드</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox runat="server" ID="txtMeainspcd" ClientInstanceName="txtMeainspcd" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">항목명</label>
                    <div class="col-sm-2">
                        <dx:ASPxTextBox runat="server" ID="txtInspdetail" ClientInstanceName="txtInspdetail" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">상한규격</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox runat="server" ID="txtMax" ClientInstanceName="txtMax" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">공정코드</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox runat="server" ID="txtWorkcd" ClientInstanceName="txtWorkcd" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">공정명</label>
                    <div class="col-sm-2">
                        <dx:ASPxTextBox runat="server" ID="txtWorknm" ClientInstanceName="txtWorknm" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">하한규격</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox runat="server" ID="txtMin" ClientInstanceName="txtMin" Width="100%" >
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False"  Width="770"
                KeyFieldName="F_ITEMCD;F_SERIALNO;F_WORKCD;F_MEAINSPCD" EnableViewState="False" EnableRowsCache="False"
                OnCustomCallback="devGrid_CustomCallback" OnBatchUpdate="devGrid_BatchUpdate"  >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
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
                    <dx:GridViewDataTextColumn FieldName="F_WORKCD" Name="F_WORKCD" Caption="공정코드" Width="80px" ReadOnly="true" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKNM" Name="F_WORKNM" Caption="공정명" Width="150px" ReadOnly="true" />
                    <dx:GridViewDataTextColumn FieldName="F_STANDARD" Name="F_STANDARD" Caption="검사규격" Width="80px" ReadOnly="true" />
                    <dx:GridViewDataTextColumn FieldName="F_MAX" Name="F_MAX" Caption="상한규격" Width="80px" ReadOnly="true" />
                    <dx:GridViewDataTextColumn FieldName="F_MIN" Name="F_MIN" Caption="하한규격" Width="80px" ReadOnly="true" />
                    <dx:GridViewDataTextColumn FieldName="F_SIRYO" Name="F_SIRYO" Caption="시료수" Width="60px" ReadOnly="true" />
                    <dx:GridViewDataTextColumn FieldName="F_ZERO" Name="F_ZERO" Caption="ZERO" Width="60px" ReadOnly="true" />
                    <dx:GridViewDataTextColumn FieldName="F_UNIT" Name="F_UNIT" Caption="단위" Width="60px" ReadOnly="true" />


                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_SERIALNO" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_MEAINSPCD" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
