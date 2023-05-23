<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM4101.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM4101" %>

<%-- 금형 조회 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {
            setTimeout(SetNew, 100);
            fn_OnSearchClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {

            fnASPxGridView_ReHeight(devGrid1);
        }

        // 조회
        function fn_OnSearchClick() {

            devGrid1.PerformCallback('GET');
        }

        // 입력
        function fn_OnNewClick() {
            SetNew();
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {

            if (!devGrid1.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else {
                devGrid1.UpdateEdit();                
            }
            fn_OnSearchClick();
            alert('저장 되었습니다');

        }

        // 취소
        function fn_OnCancelClick() {
            devGrid1.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
        }

        // 인쇄
        function fn_OnPrintClick() {
        }

        // 엑셀
        function fn_OnExcelClick() {
            if (devGrid1.GetVisibleRowsOnPage() == 0) {
                ucNavi.SetNoticeOnce('조회된 내용이 없습니다.', 'red');
                return false;
            }
            btnExport.DoClick();
        }
    </script>
    <script type="text/javascript">        // 사용자 정의 함수
        function SetNew(isFirst) {
            SetInit(isFirst);
            SetPageMode('NEW');
            fn_OnValidate();
        }
    
        // 초기화
        function SetInit(isFirst) {
            // 초기화
            SetClear(isFirst);
            SetDefault();
        }
        // 컨트롤값 클리어
        function SetClear(isFirst) {
        }

        // 기본값 설정
        function SetDefault() {
        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        function SetPageMode(mode) {
            // 페이지 모드에 따라 설정
            switch (mode) {
                case "NEW":
                    break;
                case "EDIT":
                    break;
                case "READONLY":
                    break;
                default:
                    alert("PageMode가 설정되지 않았습니다");
                    break;
            }
            hidPageMode.SetText(mode);
        }

        // 페이지 활성/비활성화
        function SetPageEnable(enable) {
        }

        // 페이지모드 조회
        function GetPageMode() {
            return hidPageMode.GetText();
        }

        // 선택항목 조회(좌측 키로 우측 조회)
        function ViewSelectedItem(pkey) {
            // 콜백으로 우측영역값 json으로 조회
            // 우측영역 세팅
            pkey = pkey.split('|');
            var param = {
                'action': 'VIEW',
                'F_COMPCD': pkey[0],
                'F_FACTCD': pkey[1],
                'F_MOLDNO': pkey[2],
                'F_MOLDNTH': pkey[3]
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 컨트롤 값 설정
        function SetValue(data) {
        }

        function SetButtonEnable(enable) {
        }
    </script>
    <script type="text/javascript">

        function fn_OnValidate(s, e) {
            var result = true;
            result = ASPxClientEdit.ValidateEditorsInContainerById('divRight');
            return result;
        }       

        function devGrid1_RowDblClick(s, e) {
        }

        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                fn_OnControlDisableBox(s.GetEditor('No'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MOLDNO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MOLDNTH'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MAKER'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MAKEYMD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_COST'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_CAVITY'), null);
                fn_OnControlDisableBox(s.GetEditor('F_LIMITCOUNT'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MEMO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_PERCENT'), null);
                fn_OnControlEnableBox(s.GetEditor('F_COUNTER'), true);
            } else {
                fn_OnControlDisableBox(s.GetEditor('No'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MOLDNO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MOLDNTH'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MAKER'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MAKEYMD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_COST'), null);
                fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
                fn_OnControlDisableBox(s.GetEditor('F_CAVITY'), null);
                fn_OnControlDisableBox(s.GetEditor('F_LIMITCOUNT'), null);
                fn_OnControlDisableBox(s.GetEditor('F_MEMO'), null);
                fn_OnControlDisableBox(s.GetEditor('F_PERCENT'), null);
                fn_OnControlEnableBox(s.GetEditor('F_COUNTER'), true);
            }
        }

        function fn_OnEndCallback(s, e) {
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divRight" style="width: 100%;">
            <div id="divMiddle" style="float: left; margin-bottom: 2px; width: 100%;">
                <table class="InputTable" style="width: 33%;">
                    <colgroup>
                        <col style="width: 11%" />
                        <col style="width: 22%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">금형번호</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="schF_MOLDNO" ClientInstanceName="schF_MOLDNO" ClientEnabled="true" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="clear: both;"></div>
            <div id="divRightBody" style="width: 100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MOLDNO;F_MOLDNTH;F_COUNTER" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid1_CustomCallback"
                    OnDataBinding="devGrid1_DataBinding"
                    OnHtmlDataCellPrepared="devGrid1_HtmlDataCellPrepared" OnBatchUpdate="devGrid1_BatchUpdate">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="ShowAllRecords"/>
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError"
                        RowDblClick="devGrid1_RowDblClick"
                        BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="50px" />
                        <dx:GridViewDataTextColumn FieldName="F_MOLDNO" Caption="금형번호" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_MOLDNTH" Caption="금형차수" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_MAKER" Caption="제작업체" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_MAKEYMD" Caption="제작일자" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_COST" Caption="제작비용" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품번" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_CAVITY" Caption="Cavity" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_LIMITCOUNT" Caption="금형수명<br/>(보증타발수)" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_COUNTER" Caption="타발수" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_PERCENT" Caption="사용도달률" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_MEMO" Caption="비고" CellStyle-HorizontalAlign="Left" />
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid1" OnRenderBrick="devGrid1Exporter_RenderBrick"></dx:ASPxGridViewExporter>
            </div>
        </div>
        <div style="clear: both;"></div>
    </div>
    <div id="divHidden" style="display: none;">
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
        <dx:ASPxTextBox runat="server" ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>
    </div>
</asp:Content>
