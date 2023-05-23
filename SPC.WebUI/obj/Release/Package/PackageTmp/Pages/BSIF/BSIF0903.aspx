<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0903.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0903" %>
<%-- 공정별 품목 등록 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .search {
            width: 100%;
            display: table-row;
        }
        .content {
            width: 100%;
            height: 100%;
            display: table-row;
        }
        .left {
            width: 300px;
            height: 100%;
            display: table-cell;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            height: 100%;
            display: table-cell;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = '';

        $(document).ready(function () {
            document.onkeypress = CancelEnterKey;

            fn_doSetGridEventAction('true');
            var oParams = 'select;' + _selectedRowKeyValue;
        });

        function CancelEnterKey() {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var devGrid1 = ASPxClientGridView.Cast('devGrid1');
            devGrid1.SetHeight($(document.documentElement).height() - _hMargin - $(devGrid2.GetMainElement()).offset().top-300);

            $('#divTopLeftGrid').height($('#divTopLeft').height() - $('#divTopLeftTitle').height() - $('#divTopLeftSearch').height());
            $('#divTopRightGrid').height($('#divTopRight').height() - $('#divTopRightTitle').height() - $('#divTopRightSearch').height() - 43);

            fnASPxGridView_ReHeight_ByContainer(devGrid);
            fnASPxGridView_ReHeight_ByContainer(devGrid2);
        }

        function fnASPxGridView_ReHeight_ByContainer(s, e) {
            var grdView = ASPxClientGridView.Cast(s);
            grdView.SetHeight(100);
            var container = grdView.GetMainElement().parentNode;
            var containerHeight = $(container).height();
            var targetHeight = containerHeight + $(container).offset().top - $(grdView.GetMainElement()).offset().top;
            var minHeight = 100;
            if (targetHeight < minHeight) targetHeight = minHeight;
            grdView.SetHeight(targetHeight);
        }

        // 조회
        function fn_OnSearchClick() {
            //if (false == fn_OnValidate()) return false;
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            //fn_SetPageMode("NEW");
            //fn_InputArea_Init();
            //schF_PARTNERCD.SetText('');
            schF_PARTNERNM.SetText('');
            schF_PARTGBNCD.SetValue('');
            schF_ITEMCD.SetText('');
            schF_ITEMNM.SetText('');
            devGrid.PerformCallback('clear');
            devGrid1.PerformCallback('clear');
            devGrid2.PerformCallback('clear');
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
            //if (false == fn_OnValidate()) return false;
            //devCallback.PerformCallback('SAVE');

            var rowCount = devGrid1.GetVisibleRowsOnPage();            

            devGrid1.PerformCallback('SAVE|');
        }

        // 저장, 삭제 후 처리
        function devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            // 정상일 경우 그리드 재 조회
            if (result.CODE == "1") {
            }

            alert(result.MESSAGE);
        }

        // 오류시
        function devCallback_CallbackError(s, e) {
            alert(e);
        }

        // 취소
        function fn_OnCancelClick() {

        }

        // 삭제
        function fn_OnDeleteClick() {
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // Grid End Callback
        function fn_devGridOnEndCallback(s, e) {
            fn_doSetGridEventAction('false');

            if (Trim(s.cpResultCode) != '') {
                alert(s.cpResultMsg);

                if (s.cpResultCode == '1' && Trim(s.cpResultEtc) == 'AfterSave') {
                    devGrid1.PerformCallback('select');
                    devGrid2.PerformCallback('select');
                    s.cpResultEtc = "";
                }

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
            else {
            }
        }

        // Validation 체크
        function fn_OnValidate() {
            //return ASPxClientEdit.ValidateEditorsInContainerById('searchCondition');
        }

        // Tree End Callback
        function fn_OnEndCallback() { }

        // 그리드 더블클릭(공정목록)
        function fn_devGridOnRowDblClick(s, e) {
            // 1. 우상단 그리드 콜백 : 우상단 그리드에 공정별 품목 목록 조회
            var devGrid = ASPxClientGridView.Cast('devGrid');
            var PROCCD = devGrid.GetRowKey(e.visibleIndex);
            schF_PARTNERCD.SetText(PROCCD);
            devGrid1.PerformCallback('select|' + PROCCD);
            devGrid2.PerformCallback('select|' + PROCCD);
        }


        // 우상단 그리드 이벤트
        function fn_devGrid1OnEndCallback(s, e) {
        }
        function fn_devGrid1OnRowDblClick(s, e) {
            // 1. 우상단 그리드 콜백 : 우상단 그리드에서 선택된 항목 제거
            // 2. 하단 그리드 콜백 : 우상단 그리드에서 선택된 항목 하단에 추가
            //fn_OnDownClick();
        }

        // 중단 버튼 클릭
        function fn_OnUpClick() {


            // 1. 하단 그리드 콜백 : 하단 그리드에서 선택된 항목 제거
            // 2. 우상단 그리드 콜백 : 하단 그리드에서 선택된 항목 우상단에 추가
            selectedKeys = devGrid2.GetSelectedKeysOnPage();

            //alert(selectedKeys);
            devGrid2.PerformCallback('delete|' + selectedKeys);
            devGrid1.PerformCallback('insert|' + selectedKeys);
        }
        function fn_OnDownClick() {
            // 1. 우상단 그리드 콜백 : 우상단 그리드에서 선택된 항목 제거
            // 2. 하단 그리드 콜백 : 우상단 그리드에서 선택된 항목 하단에 추가
            selectedKeys = devGrid1.GetSelectedKeysOnPage();

            //alert(selectedKeys);
            devGrid1.PerformCallback('delete|' + selectedKeys);
            devGrid2.PerformCallback('select');
        }

        // 하단 그리드 이벤트
        function fn_devGrid2OnEndCallback(s, e) {
        }
        function fn_devGrid2OnRowDblClick(s, e) {
            // 1. 하단 그리드 콜백 : 하단 그리드에서 선택된 항목 제거
            // 2. 우상단 그리드 콜백 : 하단 그리드에서 선택된 항목 우상단에 추가
            //devGrid1.PerformCallback('insert');
            //fn_OnUpClick();
        }

        // 하단 그리드 조회 클릭
        function fn_OnItemSearch() {
            if (schF_PARTNERCD.GetText() == '') {
                alert('상단의 공정 조회를 먼저 해 주세요.');
                return false;
            }
            devGrid2.PerformCallback('select');
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            fn_OnControlDisableBox(s.GetEditor('F_PARTNER'), null);
            fn_OnControlDisableBox(s.GetEditor('F_PARTNERNM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
            fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);
        }

        function fn_BatchEditConfirmShowing(s, e) {
            //alert();
            e.cancel = false;
            //popup.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <%-- 상단 블럭 --%>
        <div id="divTop" style="height:300px;" class="form-horizontal">
            <%-- 좌상단 블럭 --%>
            <div id="divTopLeft" style="height:100%;width:40%;display:inline-block;padding-right:5px;">
                <%-- 블럭 타이틀 --%>
                <div id="divTopLeftTitle">
                    <div class="form-group padder">
                        <label class="col-sm-12 h5 font-bold bg-primary" style="padding:5px;">■ 거래처목록</label>
                    </div>
                </div>
                <%-- 좌상단 조회조건 --%>
                <div id="divTopLeftSearch" >
                  <table class="InputTable">
                    <colgroup>
                        <col style="width:20%" />
                        <col style="width:60%" />
                        <col style="width:20%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">
                            <label>거래처명</label>
                        </td>
                        <td class="tdContent">
                           <dx:ASPxTextBox ID="schF_PARTNERCD" ClientInstanceName="schF_PARTNERCD" runat="server" Width="100%" ClientVisible="false" ></dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="schF_PARTNERNM" ClientInstanceName="schF_PARTNERNM" runat="server" Width="100%"></dx:ASPxTextBox>
                        </td>
                        <td class="tdContent">
                            <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick();return false;" style="padding-top:3px;padding-bottom:3px;">
                                <i class="fa fa-search"></i>
                                <span class="text">조회</span>
                            </button>
                        </td>
                    </tr>
                 </table>
                <div class="form-group"></div>
                </div>
                <%-- 좌상단 그리드 --%>
                <div id="divTopLeftGrid" style="height:100%;">
                    <section class="panel panel-default" style="height: 98%;">
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_PARTNER" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback" 
                            OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText"
                            SettingsDataSecurity-AllowEdit="false" 
                            >
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Columns>
                                <dx:GridViewDataColumn Caption="No" Width="60px" UnboundType="Integer">
                                    <DataItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_PARTNER" Caption="거래처코드" Width="150px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_PARTNERNM" Caption="거래처명" Width="100%" CellStyle-HorizontalAlign="Left" ></dx:GridViewDataColumn>
                            </Columns>
                            <ClientSideEvents EndCallback="fn_devGridOnEndCallback" RowDblClick="fn_devGridOnRowDblClick"  />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  />
                            <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                    </section>
                </div>
            </div><!--
            <%-- 우상단 블럭 --%>
            --><div id="divTopRight" style="height:100%;width:60%;display:inline-block;padding-left:5px;">
                <%-- 블럭 타이틀 --%>
                <div id="divTopRightTitle">
                    <div class="form-group padder">
                        <label class="col-sm-12 h5 font-bold bg-primary" style="padding:5px;">
                            ■ 품목리스트
                        </label>
                    </div>
                </div>
                <table class="InputTable">
                    <tr>            
                        <td class="tdTitle">
                            <label>품번</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_ITEMCD" ClientInstanceName="schF_ITEMCD" runat="server" Width="100%" >
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">
                            <label>품명</label>
                        </td>
                        <td class="tdContentR">
                            <dx:ASPxTextBox ID="schF_ITEMNM" ClientInstanceName="schF_ITEMNM" runat="server" Width="100%" ></dx:ASPxTextBox>
                        </td>
                        <td class="tdContent" style="text-align:right; border-left-width:0px;">
                            <button class="btn btn-sm btn-success" onclick="fn_OnItemSearch();return false;" style="padding-top:3px;padding-bottom:3px;">
                                <i class="fa fa-search"></i>
                                <span class="text">조회</span>
                            </button>
                        </td>
                      </tr>
                </table>
                <div class="form-group"></div>
                <%-- 우상단 그리드 --%>
                <div id="divTopRightGrid" style="height:100%;">
                    
                        <section class="panel panel-default" style="height: 100%;">
                            <dx:ASPxTextBox ID="hidGridAction2" ClientInstanceName="hidGridAction2" runat="server" ClientVisible="false" Text="false" />
                            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="F_ITEMCD" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGrid2_CustomCallback"
                                SettingsDataSecurity-AllowEdit="false" 
                                >
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px" >
                                        <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid2.SelectAllRowsOnPage(s.GetChecked()); }" />
                                        </HeaderTemplate>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataColumn Caption="No" Width="40px" UnboundType="Integer">
                                        <DataItemTemplate>
                                            <%# Container.ItemIndex + 1 %>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="200px"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" Width="100%" CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>
                                </Columns>
                                <ClientSideEvents EndCallback="fn_devGrid2OnEndCallback" RowDblClick="fn_devGrid2OnRowDblClick"  />
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  />
                                <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" AllowDragDrop="false" />
                                <SettingsPager Mode="ShowAllRecords" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                        </section>
                    </div>
                
            </div>
        </div>
        <%-- 중단 버튼부 --%>
        <div class="form-group" style="height:5px;"></div>
        <table class="InputTable" style="margin-bottom:10px; background-color:transparent; border-left-width:0px; border-top-width:0px;width:100%">
            <tr style="width:100%">
                <td class="tdContent"  style="border-left-width:0px;border-bottom-width:0px;text-align:center">
                    <button class="btn btn-sm btn-success" data-toggle="button" title="▼추가" onclick="fn_OnUpClick(); return false;">
                        <i class="glyphicon glyphicon-chevron-down">추가</i>
                    </button>

                    <button class="btn btn-sm btn-success" data-toggle="button" title="▲제외" onclick="fn_OnDownClick(); return false;">
                        <i class="glyphicon glyphicon-chevron-up">제외</i>
                    </button>                    
                </td>
            </tr>
        </table>            
        <div class="form-group"></div>

        <%-- 하단 블럭 --%>
        <div id="divBottom" style="width:100%" >
           <%-- <div id="divBottomTitle" style="width:100%">
                <div class="form-group padder">
                    <label class="col-sm-12 h5 font-bold bg-primary" style="padding:5px; width:100%">
                        ■ 공정별 품목/단가
                    </label>
                </div>
            </div>--%>
            
            <%-- 하단 그리드 --%>
            <div id="divBottomGrid">
            <section class="panel panel-default" style="height: 100%;">
                <dx:ASPxTextBox ID="hidGridAction1" ClientInstanceName="hidGridAction1" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid1_CustomCallback"
                    SettingsDataSecurity-AllowEdit="false" >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                    ClientSideEvents-CheckedChanged="function(s, e) { devGrid1.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <%-- <dx:GridViewDataColumn Caption="No" Width="60px" UnboundType="Integer" ReadOnly="true" CellStyle-BackColor="#a0a0a0" >
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>--%>
                        <dx:GridViewDataColumn FieldName="F_PARTNER" Caption="거래처코드" Width="100px" ReadOnly="true" />
                        <dx:GridViewDataColumn FieldName="F_PARTNERNM" Caption="거래처명" Width="200px" ReadOnly="true" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px" ReadOnly="true" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" Width="250px" CellStyle-HorizontalAlign="Left" ReadOnly="true" />
                    </Columns>
                    <ClientSideEvents EndCallback="fn_devGridOnEndCallback" BatchEditStartEditing="fn_OnBatchEditStartEditing" BatchEditConfirmShowing="fn_BatchEditConfirmShowing" />
                    <SettingsText ConfirmOnLosingBatchChanges="저장 하시겠습니까?" />
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden"  />
                    <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" AllowDragDrop="false" />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                    <SettingsPager Mode="ShowAllRecords" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid1"></dx:ASPxGridViewExporter>
            </section>
            </div>
        </div>
    </div>

    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback" ClientSideEvents-CallbackError="devCallback_CallbackError" ClientSideEvents-CallbackComplete="devCallback_CallbackComplete" />
</asp:Content>