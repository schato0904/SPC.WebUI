<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0501POP.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.Popup.DIOF0501POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            devGridResp.SetHeight(150);
            fn_RendorTotalCount();
            fn_OnSearchClick();

            if (fn_GetCastValue('txtManager') == '1') {
                $('#_btnConfirm').show();
                $('#_btnCancel').hide();

                $('#trManager').hide();
            } else {
                $('#_btnConfirm').hide();
                $('#_btnCancel').hide();
            
                $('#trManager').show();
            }
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            devGridResp.SetHeight(150);
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            var data = {
                'ACTION': 'GET'
            };

            fn_devCallback_PerformCallback(data);
        }

        // 입력
        function fn_OnNewClick() {

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

        // 이상조치 상세내역
        function fn_RespRowDblClick(s, e) {
            fn_OnPopupNgReasonView(devGridResp.GetRowKey(e.visibleIndex));
        }

        function fn_OnConfirm(bConfirm) {
            var msg = bConfirm == '1' ?
                fn_GetCastValue('srcF_DATE') + ' 점검시트를 관리자 확인처리하시겠습니까?' :
                fn_GetCastValue('srcF_DATE') + ' 점검시트 관리자 확인을 취소처리하시겠습니까?';
            if (!confirm(msg + '\r계속 진행하려면 확인을 누르세요!!'))
                return false;
            else {
                var data = {
                    'ACTION': 'SAVE',
                    'CONFIRM': bConfirm
                };

                fn_devCallback_PerformCallback(data);
            }
        }

        // CallbackPanel Event Handler 시작
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                devCallback.PerformCallback(JSON.stringify(parameter));
            }
        }

        // 콜백 성공시 처리 : 성공시 PKEY에 JSON 구조로 키값을 받는다.
        function fn_devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            var parameter = JSON.parse(e.parameter);
            var isOK = typeof (result.ISOK) != 'undefined' ? result.ISOK : null;
            var msg = typeof (result.MSG) != 'undefined' ? result.MSG : null;
            if (isOK) {
                var pkey = typeof (result.PKEY) != 'undefined' ? result.PKEY : null;
                var data = {};
                if (typeof (result.PAGEDATA) != 'undefined') {
                    data = result.PAGEDATA;
                } else {
                    for (var key in pkey) { data[key] = pkey[key]; }
                }
                var action = typeof (parameter.ACTION) != 'undefined' ? parameter.ACTION : null;

                switch (action) {
                    case "SAVE":
                        alert(msg);
                        data.ACTION = 'GET';
                        fn_devCallback_PerformCallback(data);
                        parent.fn_devCallback_PerformCallback(true);
                        break;
                    case "GET":
                        fn_OnInputData(data);

                        if (fn_GetCastValue('srcF_CNFMYN').toLowerCase() == 'true') {
                            $('#_btnConfirm').hide();
                            $('#_btnCancel').show();
                        } else {
                            $('#_btnConfirm').show();
                            $('#_btnCancel').hide();
                        }
                        break;
                }
            } else {
                if (msg != 'NO DATA')
                    alert(msg);
            }
        }

        // 콜백 종료시 처리
        function fn_devCallback_EndCallback(s, e) {
            // 대기중인 작업이 있을경우, 콜백 실행
            if (devCallback_parameter) {
                devCallback.PerformCallback(JSON.stringify(devCallback_parameter));
                devCallback_parameter = null;
            }
        }
        // CallbackPanel Event Handler   끝

        // 입력폼 조회값입력
        function fn_OnInputData(pagedata) {
            fn_SetTextValue('srcF_DAYIDX', GetJsonValueByKey(pagedata, 'F_DAYIDX', ''));
            fn_SetTextValue('srcF_CNFMYN', GetJsonValueByKey(pagedata, 'F_CNFMYN', ''));
            fn_SetTextValue('srcF_COMFIRM', GetJsonValueByKey(pagedata, 'F_CONFIRM', ''));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxTextBox ID="txtManager" ClientInstanceName="txtManager" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="srcF_MACHIDX" ClientInstanceName="srcF_MACHIDX" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="srcF_DAYIDX" ClientInstanceName="srcF_DAYIDX" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="srcF_CNFMYN" ClientInstanceName="srcF_CNFMYN" runat="server" ClientVisible="false" />
            <table class="contentTable">
                <colgroup>
                    <col style="width:70px;" />
                    <col style="width:100px;" />
                    <col style="width:70px;" />
                    <col style="width:200px;" />
                    <col style="width:70px;" />
                    <col style="width:100px;" />
                    <col style="width:90px;" />
                    <col />
                    <col style="width:70px;" />
                </colgroup>
                <tr id="trManager">
                    <td class="tdLabel" colspan="9">해당 설비에 관리자로 등록된 경우만 확인 처리를 할 수 있습니다</td>
                </tr>
                <tr>
                    <td class="tdLabel">설비코드</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MACHCD" ClientInstanceName="srcF_MACHCD" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">설비명</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MACHNM" ClientInstanceName="srcF_MACHNM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">점검일자</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_DATE" ClientInstanceName="srcF_DATE" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">관리자확인</td>
                    <td class="tdInput tdContentR">
                        <dx:ASPxTextBox ID="srcF_COMFIRM" ClientInstanceName="srcF_COMFIRM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdInput text-right">
                        <button id="_btnConfirm" class="btn btn-sm btn-info" onclick="fn_OnConfirm('1'); return false;">
                            <i class="fa fa-check-circle-o"></i>
                            <span class="text">확인</span>
                        </button>
                        <button id="_btnCancel" class="btn btn-sm btn-warning" onclick="fn_OnConfirm('0'); return false;">
                            <i class="i i-cancel"></i>
                            <span class="text">취소</span>
                        </button>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_INSPIDX;F_NUMBER;F_INSPKINDCD;F_INSPUSER;F_INSPNM;F_MEASIDX;F_MEASURE;F_JUDGE" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_INSPNO" Caption="점검<br />부위" Width="50px" />
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="점검항목" Width="140px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPREMARK" Caption="점검내용" Width="180px">
                        <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPWAY" Caption="점검방법" Width="240px">
                        <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_VIEWSTAND" Caption="표시규격" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CYCLENM" Caption="점검<br />주기" Width="50px" />
                    <dx:GridViewDataColumn FieldName="F_INSPKINDNM" Caption="판정구분" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="점검결과" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_JUDGE" Caption="판정" Width="50px" />

                    <dx:GridViewDataColumn FieldName="F_INSPIDX" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_INSPKINDCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_CYCLECD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_CHASU" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEASIDX" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_INSPUSER" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_IMAGESEQ" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging">
            <div class="divPadding"></div>
            <dx:ASPxGridView ID="devGridResp" ClientInstanceName="devGridResp" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_REMEDYIDX" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGridResp_CustomCallback" OnHtmlDataCellPrepared="devGridResp_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    RowDblClick="fn_RespRowDblClick" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridResp" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ROWNUM" Caption="No" Width="40px" />
                    <dx:GridViewDataColumn FieldName="F_OCCURDT" Caption="점검일" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NGTYPENM" Caption="이상유형" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_NGREMK" Caption="이상내역" Width="250px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="등록자" Width="100px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RESPTYPENM" Caption="조치유형" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_RESPREMK" Caption="조치내역" Width="250px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RESPUSER" Caption="조치자" Width="100px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_RESPDT" Caption="완료일" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="진행상태" Width="70px" />

                    
                    <dx:GridViewDataColumn FieldName="F_REMEDYIDX" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>