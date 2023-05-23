<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM1203.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM1203" %>

<%-- 작업지시서등록 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {

            setTimeout(SetNew, 100);

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            fnASPxGridView_ReHeight(devGrid);
            fnASPxGridView_ReHeight(devGrid1);
        }

        // 조회
        function fn_OnSearchClick() {
            //fn_ObjInit();

            if (hidMACH.GetText() == "") {
                alert('조회 할 주조기를 선택하세요.');
                return false;
            }

            //devGrid.Selection.UnselectAll();

            devGrid.UnselectAllRowsOnPage();
            devGrid.PerformCallback('GET');
            hidPK.SetValue('');
            hidMACH2.SetValue('');
            hidDATE.SetValue('');


            //if (Trim(srcF_PJ10MID.GetText()) != '') ViewSelectedItem(srcF_PJ10MID.GetText());
        }

        // 날짜 조회
        function fn_OnSearchClick1() {



            devGrid1.PerformCallback('GET');
        }

        // 시작 버튼
        function fn_OnStartClick() {

            if (hidPK.GetText() == "") {
                alert('작업지시를 선택하세요.');
                return false;
            }

            devCallback2.PerformCallback('1');
            devGrid.PerformCallback('GET');
            devGrid1.PerformCallback('GET');

        }

        // 중지 버튼
        function fn_OnStopClick() {

            if (hidPK.GetText() == "") {
                alert('작업지시를 선택하세요.');
                return false;
            }

            devCallback2.PerformCallback('2');
            devGrid.PerformCallback('GET');
            devGrid1.PerformCallback('GET');

        }

        // 완료 버튼
        function fn_OnCompleteClick() {

            if (hidPK.GetText() == "") {
                alert('작업지시를 선택하세요.');
                return false;
            }

            devCallback2.PerformCallback('3');
            devGrid.PerformCallback('GET');
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
        // 키가 있는지 확인
        function HasPkey() {
            //return Trim(srcF_WORKNO.GetText()) != '';
        }
        // 초기화
        function SetInit(isFirst) {
            // 초기화
            SetClear(isFirst);
            SetDefault();
            //SetStatusBySubmitCD();
        }
        // 컨트롤값 클리어
        function SetClear(isFirst) {

            //srcF_WORKNO.SetValue('');
            //srcF_WORKNO_SEQ.SetValue(1);
            srcF_MACHCD.SetSelectedIndex(0);
            //srcF_MELTCD.SetSelectedIndex(0);
            //srcF_LOTNO.SetValue('');
            //srcF_PLANCOUNT.SetValue(1);
            //srcF_PLANYMD.SetValue(new Date().Add(0, 0, 1));
            //srcF_MEMO.SetValue('');
        }
        // 기본값 설정
        function SetDefault() {

            schF_PLANYMD.SetValue(new Date().Add(0, 0, 1));
        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        function SetPageMode(mode) {
            // 페이지 모드에 따라 설정
            switch (mode) {
                case "NEW":
                    //SetPageEnable(Trim(srcF_ITEMCD.GetText()) != '');
                    //SetPageEnable(true);
                    break;
                case "EDIT":
                    SetPageEnable(true);
                    //srcF_CODE.SetEnabled(false);
                    break;
                case "READONLY":
                    SetPageEnable(false);
                    break;
                default:
                    alert("PageMode가 설정되지 않았습니다");
                    break;
            }
            //SetPageEnable(mode);
            hidPageMode.SetText(mode);
        }

        // 페이지 활성/비활성화
        function SetPageEnable(enable) {
            //srcF_WORKNO_SEQ.SetEnabled(enable);
            srcF_MACHCD.SetEnabled(enable);
            //srcF_MELTCD.SetEnabled(enable);
            //srcF_LOTNO.SetEnabled(enable);
            //srcF_PLANCOUNT.SetEnabled(enable);
            //srcF_PLANYMD.SetEnabled(enable);
            //srcF_MEMO.SetEnabled(enable);
        }

        // 페이지모드 조회
        function GetPageMode() {
            return hidPageMode.GetText();
        }

        // 선택항목 조회(좌측 키로 우측 조회)
        function ViewSelectedItem() {

            srcF_MACHCD.SetSelectedIndex();
        }

        // 선택항목 조회(좌측 키로 우측 목록 조회)
        function ViewList(pkey) {
            // 콜백으로 우측목록 grid 조회
            pkey = pkey.split('|');
            //srcF_ITEMCD.SetText(pkey[0]);
            devGrid1.PerformCallback('GET');
            SetNew();
        }
        // 컨트롤 값 설정
        function SetValue(data) {
            //if (typeof data.F_WORKNO != 'undefined') srcF_WORKNO.SetValue(data.F_WORKNO);
            //if (typeof data.F_WORKNO_SEQ != 'undefined') srcF_WORKNO_SEQ.SetValue(data.F_WORKNO_SEQ);
            //if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            if (typeof data.F_MACHCD != 'undefined') srcF_MACHCD.SetValue(data.F_MACHCD);
            //if (typeof data.F_MELTCD != 'undefined') srcF_MELTCD.SetValue(data.F_MELTCD);
            //if (typeof data.F_LOTNO != 'undefined') srcF_LOTNO.SetValue(data.F_LOTNO);
            //if (typeof data.F_PLANCOUNT != 'undefined') srcF_PLANCOUNT.SetValue(data.F_PLANCOUNT);
            if (typeof data.F_PLANYMD != 'undefined') {
                //srcF_PLANYMD.SetValue(convertDateString(data.F_PLANYMD));
                schF_PLANYMD.SetValue(convertDateString(data.F_PLANYMD));
            }
            //if (typeof data.F_MEMO != 'undefined') srcF_MEMO.SetValue(data.F_MEMO);
        }


    </script>
    <script type="text/javascript">
        // 이벤트 핸들러
        // Grid End Callback


        function fn_OnEndCallback2(s, e) {

            alert(s.cpResultMsg);

        }

        function fn_OnEndCallback(s, e) {

            

        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            var result = true;
            result = ASPxClientEdit.ValidateEditorsInContainerById('divRight');
            return result;
        }

        function devGrid_RowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex).split('|');
            hidPK.SetValue(key[0]);
            hidMACH2.SetValue(key[1]);
            hidDATE.SetValue(key[2]);

        }

        function devGrid1_RowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            srcF_MACHCD.SetSelectedIndex(key);
            hidMACH.SetValue(key);
        }

        function fn_MACHSELECT(s, e) {
            var key = s.GetValue();

            hidMACH.SetValue(key);

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divLeft" style="width: 29%; float: left;">
            <div class="blockTitle">
                <span>[작업중 목록]</span>
            </div>
            <div id="divMiddle1" style="float: left; margin-bottom: 2px; width: 66%;">
                <table class="InputTable" style="width: 100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">작업지시일</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_PLANYMD" ClientInstanceName="schF_PLANYMD" UseMaskBehavior="true" ClientSideEvents-Init="function(s,e){ s.SetDate(new Date());}"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                    Width="100%" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divMiddle2" style="float: left; margin-bottom: 2px; width: 100px; padding-top: 5px; padding-left: 10px;">
                <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick1(); return false;" style="width: 80px;">
                    <span class="text">조회</span>
                </button>
            </div>
            <div style="clear: both;"></div>
            <div id="divRightBody" style="width: 100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MACHCD" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid1_CustomCallback"
                    OnDataBinding="devGrid1_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />

                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError" />
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>                        
                        <dx:GridViewDataTextColumn FieldName="F_MACHNM" Caption="주조기" Width="25%" />
                        <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="생산품번" Width="25%" />
                        <dx:GridViewDataSpinEditColumn FieldName="F_PLANCOUNT" Caption="지시수량" Width="25%" PropertiesSpinEdit-DisplayFormatString="{0:#,0}" />
                        <dx:GridViewDataTextColumn FieldName="F_STATUSNM" Caption="상태" Width="25%" />
                        <dx:GridViewDataTextColumn FieldName="F_MACHCD" Caption="주조기코드" GroupFooterCellStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    </Columns>
                </dx:ASPxGridView>

            </div>



        </div>
        <div id="divSpace" style="width: 1%; float: left;">&nbsp;</div>
        <div id="divRight" style="width: 70%; float: left;">






            <div class="blockTitle">
                <span>[작업지시 목록]</span>
            </div>
            <div id="divLeftTop" style="width: 100%;">
                <table class="InputTable" style="margin-bottom: 5px;">
                    <colgroup>
                        <col style="width: 16%" />
                        <col style="width: 20%" />
                        <col style="width: 16%" />
                        <col style="width: 16%" />
                        <col style="width: 16%" />
                        <col style="width: 16%" />
                    </colgroup>
                    <tr>

                        <td class="tdTitle">주조기</td>

                        <td class="tdContentR">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_MACHCD" ClientInstanceName="srcF_MACHCD" IncrementalFilteringMode="None" ValueField="F_MACHCD" 
                                    TextField="F_MACHNM" ValueType="System.String" OnCallback="srcF_MACHCD_Callback" ClientSideEvents-ValueChanged="fn_MACHSELECT" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>

                        <td class="tdContentLR">
                            <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick(); return false;" style="width: 80px;">
                                <span class="text">조회</span>
                            </button>
                        </td>

                        <td class="tdContentLR">
                            <button class="btn btn-sm btn-primary" onclick="fn_OnStartClick(); return false;" style="width: 80px;">
                                <span class="text">시작</span>
                            </button>
                        </td>

                        <td class="tdContentLR">
                            <button class="btn btn-sm btn-danger" onclick="fn_OnStopClick(); return false;" style="width: 80px;">
                                <span class="text">중지</span>
                            </button>
                        </td>

                        <td class="tdContentL">
                            <button class="btn btn-sm btn-success" onclick="fn_OnCompleteClick(); return false;" style="width: 80px;">
                                <span class="text">완료</span>
                            </button>
                        </td>

                    </tr>
                </table>
            </div>
            <div id="divLeftBody" style="width: 100%;">


                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_WORKNO;F_MACHCD;F_PLANYMD;" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback"
                    OnDataBinding="devGrid_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />
                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError" RowClick="devGrid_RowDblClick" />
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_WORKNO" Caption="작업지시번호" Width="20%" />
                        <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="주조기" Width="20%" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="20%" />
                        <dx:GridViewDataColumn FieldName="F_PRODCOUNT" Caption="생산수량" Width="20%" />
                        <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="상태" Width="20%" />
                        <dx:GridViewDataColumn FieldName="F_STATUSCD" Caption="상태코드" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <dx:GridViewDataColumn FieldName="F_MACHCD" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <dx:GridViewDataColumn FieldName="F_PLANYMD" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    </Columns>
                </dx:ASPxGridView>
            </div>

        </div>
        <div style="clear: both;"></div>
    </div>
    <div id="divHidden" style="display: none;">
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" CausesValidation="false" />
        <dx:ASPxTextBox runat="server" ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback">
            <ClientSideEvents CallbackError="fn_OnCallbackError" />
        </dx:ASPxCallback>
        <dx:ASPxCallback ID="devCallback2" runat="server" ClientInstanceName="devCallback2" OnCallback="devCallback2_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback2" />
        </dx:ASPxCallback>
        <dx:ASPxTextBox ID="hidMACH" ClientInstanceName="hidMACH" runat="server" Text="" ClientVisible="false" />
        <dx:ASPxTextBox ID="hidPK" ClientInstanceName="hidPK" runat="server" Text="" ClientVisible="false"></dx:ASPxTextBox>
        <dx:ASPxTextBox ID="hidMACH2" ClientInstanceName="hidMACH2" runat="server" Text="" ClientVisible="false"></dx:ASPxTextBox>
        <dx:ASPxTextBox ID="hidDATE" ClientInstanceName="hidDATE" runat="server" Text="" ClientVisible="false"></dx:ASPxTextBox>


    </div>
</asp:Content>
