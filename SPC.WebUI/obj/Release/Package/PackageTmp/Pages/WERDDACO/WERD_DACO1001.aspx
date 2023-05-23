<%@ Page Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD_DACO1001.aspx.cs" Inherits="SPC.WebUI.Pages.WERDDACO.WERD_DACO1001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });


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

        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

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
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                fn_OnControlDisableBox(s.GetEditor('F_WORKDATE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_BANNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_LINENM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_COMPANYNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_TYPE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_CODE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_INSUSER'), null);
                fn_OnControlDisableBox(s.GetEditor('F_INSDT'), null);

            } else {
                fn_OnControlDisableBox(s.GetEditor('F_WORKDATE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_BANNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_LINENM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_COMPANYNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);
                fn_OnControlDisableBox(s.GetEditor('F_TYPE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_CODE'), null);
                fn_OnControlDisableBox(s.GetEditor('F_INSUSER'), null);
                fn_OnControlDisableBox(s.GetEditor('F_INSDT'), null);

            }
        }

        function fn_OnLINEValueChanged(s, e) {
            var val = s.GetValue();
            hidCOMP.SetValue(val);
            LINEPerFormCallbackTarget();
        }

        function fn_OnLINEEndCallback(s, e) {
            isLINEEndCallback = parent.parent.isTreeLINESetup;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" ID="ucDate" DateTimeOnly="false" />
                    </div>

                    <label class="col-sm-1 control-label">업체명</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="hidCOMP" ClientInstanceName="hidCOMP" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlCOMP" ClientInstanceName="ddlCOMP" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton"
                            TextField="F_LINENM" ValueField="F_LINECD" OnDataBound="ddlLINE_DataBound" OnCallback="ddlLINE_Callback">
                            <ClientSideEvents ValueChanged="fn_OnLINEValueChanged" Init="fn_OnControlDisable" EndCallback="fn_OnLINEEndCallback" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-1 control-label">구분</label>
                    <div class="col-sm-1">
                        <dx:ASPxComboBox ID="TYPE" ClientInstanceName="TYPE" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <Items>
                                <dx:ListEditItem Selected="true" Text="전체" Value="0" />
                                <dx:ListEditItem Text="외주불량" Value="1" />
                                <dx:ListEditItem Text="작업불량" Value="2" />
                            </Items>
                            <ClientSideEvents Init="fn_OnControlDisable" />

                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">&nbsp;</label>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;F_BANCD;F_LINECD;F_COMPANYCD;F_ITEMCD;F_CODE;F_ERRORCD;F_INSUSER;F_INSDT" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback"
                OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                OnHtmlRowPrepared="devGrid_HtmlRowPrepared" OnBatchUpdate="devGrid_BatchUpdate">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="일자" Width="12.5%" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="6.25%" />
                    <dx:GridViewDataColumn FieldName="F_BANCD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="6.25%" />
                    <dx:GridViewDataColumn FieldName="F_LINECD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_COMPANYNM" Caption="업체명" Width="12.5%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_COMPANYCD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="12.5%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_TYPE" Caption="불량구분" Width="6.25%" />
                    <dx:GridViewDataColumn FieldName="F_CODE" Caption="불량유형" Width="12.5%" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataColumn FieldName="F_ERRORCD" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Border-BorderWidth="0px" />
                    <dx:GridViewDataColumn FieldName="F_NGCOUNT" Caption="불량수량" Width="6.25%" />
                    <dx:GridViewDataColumn FieldName="F_NGTIME" Caption="수리시간(초)" Width="6.25%" CellStyle-HorizontalAlign="Right" />
                    <dx:GridViewDataColumn FieldName="F_INSUSER" Caption="등록자" Width="6.25%" />
                    <dx:GridViewDataColumn FieldName="F_INSDT" Caption="등록일자" Width="12.5%" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
