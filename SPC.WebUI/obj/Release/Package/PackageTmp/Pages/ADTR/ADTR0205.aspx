<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0205.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0205" %>
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

            if (hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) {
                alert("품목을 선택하세요!!");
                return false;
            }

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

            fn_RendorTotalCount();
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

        }

        function fn_devGridRowDbClick(s, e) {
            var visibleIndex = e.visibleIndex
            if (visibleIndex < 0) return;

            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(e.visibleIndex));

            var oParams = "";

            oParams += fn_GetCastText('hidUCFROMDT') + '|'
            oParams += fn_GetCastText('hidUCTODT') + '|'
            oParams += rowKeys;

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
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>                 
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_WORKDATE;F_GUBUN;F_WORKCD;F_ITEMCD" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridRowDbClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="일자"  Width="120px"  >
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정"  Width="150px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>                    
                    <dx:GridViewDataColumn FieldName="F_GUBUN" Caption="구분"  Width="80px"  >
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG00" Caption="총수량"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG01" Caption="미성형/소재불량"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG02" Caption="10공정 찍힘"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG03" Caption="20공정 찍힘"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG04" Caption="내경찍힘"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG05" Caption="스크래치"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG06" Caption="치수"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG07" Caption="주름(조도불량)"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG08" Caption="1차 손상"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG09" Caption="1차 셋팅"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG10" Caption="1차 치수"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG11" Caption="1차 원소재찍힘,손상"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG12" Caption="1차 미가공"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG13" Caption="1차 Tap불량"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG14" Caption="1차 Tool파손"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG15" Caption="1차 위상"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG16" Caption="2차 찍힘/SC/손상"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG17" Caption="2차 위상"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG18" Caption="2차 치수"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG19" Caption="2차 M3, M4Tap불량"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG20" Caption="2차 코팅, 외주불량"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG21" Caption="Scroll 치수"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG22" Caption="Scroll 외관"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NG23" Caption="기타"  Width="100px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
