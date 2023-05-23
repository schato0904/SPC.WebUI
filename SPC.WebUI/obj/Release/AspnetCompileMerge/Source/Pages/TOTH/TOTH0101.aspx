<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TOTH0101.aspx.cs" Inherits="SPC.WebUI.Pages.TOTH.TOTH0101" %>
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
            devGrid1.SetHeight(height);
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
            //selectedKeys = devGrid.GetSelectedKeysOnPage();

            //// 1 Row 반드시 선택
            //if (selectedKeys.length <= 0) {
            //    alert('수정할 데이타를 선택하세요!!');
            //    return false;
            //}

            //for (var i = 0; i < selectedKeys.length ; i++) {
            //    devGrid.StartEditRowByKey(selectedKeys[i]);
            //}
        }

        // 저장
        function fn_OnSaveClick() {
            //if (!devGrid.batchEditApi.HasChanges())
            //    alert('변경된 사항이 없습니다');
            //else
            //    devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            //devGrid.UnselectAllRowsOnPage();
            //devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            //selectedKeys = devGrid.GetSelectedKeysOnPage();

            //// 1 Row 반드시 선택
            //if (selectedKeys.length <= 0) {
            //    alert('삭제할 데이타를 선택하세요!!');
            //    return false;
            //}

            //if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            //for (var i = 0; i < selectedKeys.length ; i++) {
            //    devGrid.DeleteRowByKey(selectedKeys[i]);
            //}
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
            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            //fn_OnBatchValidate("F_BANCD", s, e);
            //fn_OnBatchValidate("F_BANNM", s, e);

            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    var editor = s.GetEditor('F_BANCD');
            //    fn_OnControlDisableBox(editor, null);
            //} else {
            //    var editor = s.GetEditor('F_BANCD');
            //    fn_OnControlEnableBox(editor, null);
            //}
        }

        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            txtSERIALNO.SetText(resultValues[10]);
            //alert(txtSERIALNO.GetText());
        }

        function devGrid_CellDoubleClick(grid, idx, fieldNm, imgname) {
            // 성적서 파일 더블 클릭시 이미지 조회
            if (fieldNm == "F_FILENM") {
                var img = imgname;
                var comp = '<%=this.GetCompCD()%>';
                window.open("<%=this.ResolveUrl("~/")%>API/Common/Gathering/ImageView.ashx?code=" + comp + "&name=" + img, "_blank", "");
            }
            // 성적서 파일명 외 더블 클릭시 디테일 내역 조회
            else {
                devGrid1.PerformCallback(grid.GetRowKey(idx));
            }
        }

        function devGrid1_RowDblClick(s, e) {
            var visibleIndex = e.visibleIndex
            if (visibleIndex < 0) return;

            var grid1Keys = s.GetRowKey(e.visibleIndex).split('|');
            // 키값 순서 : F_WOKRNM;F_WOKRCD;F_MEAINSPCD;F_MEASURE;F_DISPLAYNO;F_WORKDATE;F_ITEMCD;F_SERIALNO;F_INSPDETAIL;F_STANDARD;F_MIN;F_MAX

            var oParams = "";

            oParams += fn_GetCastText('hidUCFROMDT') + '|'
            oParams += fn_GetCastText('hidUCTODT') + '|'
            oParams += grid1Keys[6] + '|';
            oParams += grid1Keys[14] + '|';
            oParams += grid1Keys[1] + '|';
            oParams += grid1Keys[0] + '|';
            oParams += grid1Keys[2] + '|';
            oParams += grid1Keys[8] + '|';
            oParams += grid1Keys[7] + '|';
            oParams += grid1Keys[12] + '|';
            oParams += grid1Keys[13] + '|';
            oParams += grid1Keys[9] + '|';
            oParams += grid1Keys[11] + '|';
            oParams += grid1Keys[10] + '|';
            //oParams += devGrid.GetRowKey(e.visibleIndex); //F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN

            parent.fn_OnDeleteTab('ANLS0101', parent.fn_OnGetTabObject('ANLS0101'));
            parent.parent.doCreateTab('MM03|MM0301|ANLS|ANLS0101|품질종합현황|RP|1', oParams);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검사일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" machGubun="5" />
                    </div> 
                    <div class="col-sm-4">
                    </div>
                </div>                
            </div>
        </div>
        <div class="content">
            <div class="col-sm-7" style="padding-left:0px;padding-right:5px;">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_WORKDATE;F_WORKTIME;F_ITEMCD;F_ITEMNM;F_WORKNM;F_WORKCD;F_FILENM;F_NAMEGB" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible"  HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자" />
                        <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시각" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="120" />
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" Width="200" />
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" />
                        <dx:GridViewDataColumn FieldName="F_FILENM" Caption="성적서파일명" Width="220" />
                        <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_NAMEGB" Caption="" Visible="false" />
                    </Columns>                
                </dx:ASPxGridView>
            </div>
            <div class="col-sm-5" style="padding-left:5px;padding-right:0px;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_WORKNM;F_WORKCD;F_MEAINSPCD;F_MEASURE;F_DISPLAYNO;F_WORKDATE;F_ITEMCD;F_SERIALNO;F_INSPDETAIL;F_STANDARD;F_MIN;F_MAX;F_SIRYO;F_FREEPOINT;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid1_CustomCallback" OnHtmlRowPrepared="devGrid1_HtmlRowPrepared">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible"  HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="devGrid1_RowDblClick" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid1" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_SERIALNO" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_SIRYO" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FREEPOINT" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_DISPLAYNO" Caption="순서" />
                        <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" />
                        <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정치" />
                        <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="측정기준" />
                        <dx:GridViewDataColumn FieldName="F_MAX" Caption="규격상한" />
                        <dx:GridViewDataColumn FieldName="F_MIN" Caption="규격하한" />
                    </Columns>                
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxGridViewExporter ID="devGridExporter2" runat="server" GridViewID="devGrid1" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            </div>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" Visible="false" />
        </div>
    </div>
</asp:Content>
