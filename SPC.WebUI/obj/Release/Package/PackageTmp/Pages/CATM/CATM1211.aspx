<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM1211.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM1211" %>

<%-- 작업지시서등록(2019.11.11) --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;




        

        $(document).ready(function () {
            //fn_InitRequired('required', 'divPopContent1');
            //srcF_JUDGECD.SetEnabled(false);
            // 검사성적 영역 높이 설정
            //var h = $(document).height() - $('#divRight').offset().top - 18;
            //$('#divRight').height(h);

            //fn_InitRequired('required', 'divRightTop');
            //var isFirst = Trim(srcF_PJ10MID.GetText()) != '' && Trim(srcF_PJ10MID.GetText()) != '0';
            setTimeout(SetNew, 100);
            fn_OnSearchClick();

            //if (isFirst) {
            //    ViewSelectedItem(Trim(srcF_PJ10MID.GetText()));
            //}
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            fnASPxGridView_ReHeight(devGrid);
            fnASPxGridView_ReHeight(devGrid1);
        }

        // 조회
        function fn_OnSearchClick() {
            //fn_ObjInit();
            devGrid.PerformCallback('GET');
            devGrid1.PerformCallback('GET');
            //if (Trim(srcF_PJ10MID.GetText()) != '') ViewSelectedItem(srcF_PJ10MID.GetText());
        }

        // 완료
        function fn_OnCompleteClick() {

            devCallback2.PerformCallback();

            fn_OnSearchClick();

            //devGrid2.PerformCallback();

            

            //devGrid.PerformCallback('GET');
            //devGrid1.PerformCallback('GET');
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
            var md = GetPageMode();

            if (!fn_OnValidate()) {
                ucNavi.SetNoticeOnce('필요한 항목이 모두 입력되지 않았습니다.', 'red');
                return false;
            }

            if (!confirm('저장하시겠습니까?')) {
                return false;
            }

            var action = (md == 'NEW' ? 'SAVE' : 'UPDATE');

            var params = {
                'action': action,
                'F_WORKNO': Trim(srcF_WORKNO.GetText()),
                'F_PLANYMD': Trim(srcF_PLANYMD.GetText())
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(params)));
        }

        // 취소
        function fn_OnCancelClick() {
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (GetPageMode() != "EDIT" || !HasPkey()) {
                //ucNavi.SetNoticeOnce('필요한 항목이 모두 입력되지 않았습니다.', 'red');
                alert('작업취소 대상 데이터가 없습니다.');
                return false;
            }

            if (!confirm('작업지시를 취소하시겠습니까?')) {
                return false;
            }

            var params = {
                'action': 'DELETE'
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(params)));
        }

        // 인쇄
        function fn_OnPrintClick() {
            var F_WORKNO = Trim(srcF_WORKNO.GetText());
            var TITLE = '작업지시서';
            if (F_WORKNO == '') {
                ucNavi.SetNoticeOnce('인쇄할 내용이 없습니다.', 'red');
                return false;
            }

            fn_OnPopRptCATM1211(TITLE, F_WORKNO, '');
        }

        // 부품식별표 팝업창 열기
        function fn_OnPopRptCATM1211(TITLE, F_WORKNO, parentCallbackName) {
            var PopParam = {
                'F_WORKNO': F_WORKNO
            };
            pPage = './Popup/PopRptCATM1201.aspx' +
                '?TITLE=' + encodeURIComponent(TITLE) +
                '&parentCallback=' + parentCallbackName +
                '&PopParam=' + encodeURIComponent(JSON.stringify(PopParam));
            //pPage = rootURL + 'Pages/Common/Popup/INSPREPORTPOP.aspx?TITLE=test&KEYFIELDS=14|LB%20RA506101|41940|04|1';
            fn_OnPopupOpen(pPage, '1000', '0');
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
            return Trim(srcF_WORKNO.GetText()) != '';
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
            // 컨트롤값 제거
            //if (!isFirst) {
            //    srcF_MACHCD.SetValue('');
            //    srcF_MACHNM.SetValue('');
            //}
            //var today = new Date().toISOString().slice(0, 10);
            srcF_WORKNO.SetValue('');
            srcF_WORKNO_SEQ.SetValue(1);
            //srcF_MACHCD.SetValue('');
            //srcF_MELTCD.SetValue('');
            srcF_MACHCD.SetSelectedIndex(0);
            srcF_MELTCD.SetSelectedIndex(0);
            srcF_LOTNO.SetValue('');
            srcF_PLANCOUNT.SetValue('');
            srcF_PLANYMD.SetValue(new Date());
            //srcF_PLANYMD.SetValue(new Date().Add(0, 0, 1));


            //srcF_PLANYMD.SetEnabled(false);

            
            

            
                
            srcF_MEMO.SetValue('');
            srcF_PLANWORKER1.SetSelectedIndex(0);
            srcF_PLANWORKER2.SetSelectedIndex(0);
            srcF_PLANWORKER3.SetSelectedIndex(0);
        }
        // 기본값 설정
        function SetDefault() {
            //// 기본값 설정할 것만 지정하여 SetValue 호출
            //var data = {
            //    'F_COMPCD': _compCD, // '04',
            //    'F_FACTCD': _factCD, // '01',
            //    // 'F_CUSTID': '41940',
            //    'F_APPRDT1': fn_GetDateString(new Date()),
            //    'F_APPRDT2': fn_GetDateString(new Date()),
            //    'F_APPRDT3': fn_GetDateString(new Date())
            //};
            //srcF_COMPCD.SetText(_compCD);
            //srcF_FACTCD.SetText(_factCD);
            //SetValue(data);
            //schF_PLANYMD.SetValue(new Date().Add(0, 0, 1));
            schF_PLANYMD.SetValue(new Date());
        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        function SetPageMode(mode) {
            // 페이지 모드에 따라 설정
            switch (mode) {
                case "NEW":
                    SetPageEnable(Trim(srcF_ITEMCD.GetText()) != '');
                    //SetPageEnable(true);
                    break;
                case "EDIT":
                    SetPageEnable2(true);
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
            srcF_WORKNO_SEQ.SetEnabled(enable);
            srcF_MACHCD.SetEnabled(enable);
            srcF_MELTCD.SetEnabled(enable);
            srcF_LOTNO.SetEnabled(enable);
            srcF_PLANCOUNT.SetEnabled(enable);
            srcF_PLANYMD.SetEnabled(enable);
            srcF_MEMO.SetEnabled(enable);
            srcF_PLANWORKER1.SetEnabled(enable);
            srcF_PLANWORKER2.SetEnabled(enable);
            srcF_PLANWORKER3.SetEnabled(enable);
        }

        function SetPageEnable2(enable) {
            srcF_WORKNO_SEQ.SetEnabled(enable);
            srcF_MACHCD.SetEnabled(enable);
            srcF_MELTCD.SetEnabled(enable);
            srcF_LOTNO.SetEnabled(enable);
            srcF_PLANCOUNT.SetEnabled(enable);
            srcF_PLANYMD.SetEnabled(false);
            srcF_MEMO.SetEnabled(enable);
            srcF_PLANWORKER1.SetEnabled(enable);
            srcF_PLANWORKER2.SetEnabled(enable);
            srcF_PLANWORKER3.SetEnabled(enable);
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
                'F_WORKNO': pkey[2]
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 선택항목 조회(좌측 키로 우측 목록 조회)
        function ViewList(pkey) {
            // 콜백으로 우측목록 grid 조회
            pkey = pkey.split('|');
            srcF_ITEMCD.SetText(pkey[0]);
            srcF_MOLDNO.SetText(pkey[1]);
            srcF_MOLDNTH.SetText(pkey[2]);
            srcF_CAVITY.SetText(pkey[3]);
            devGrid1.PerformCallback('GET');
            SetNew();
        }
        // 컨트롤 값 설정
        function SetValue(data) {
            if (typeof data.F_WORKNO != 'undefined') srcF_WORKNO.SetValue(data.F_WORKNO);
            if (typeof data.F_WORKNO_SEQ != 'undefined') srcF_WORKNO_SEQ.SetValue(data.F_WORKNO_SEQ);
            if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            if (typeof data.F_MOLDNO != 'undefined') srcF_MOLDNO.SetValue(data.F_MOLDNO);
            if (typeof data.F_MOLDNTH != 'undefined') srcF_MOLDNTH.SetValue(data.F_MOLDNTH);
            if (typeof data.F_CAVITY != 'undefined') srcF_CAVITY.SetValue(data.F_CAVITY);
            if (typeof data.F_MACHCD != 'undefined') srcF_MACHCD.SetValue(data.F_MACHCD);
            if (typeof data.F_MELTCD != 'undefined') srcF_MELTCD.SetValue(data.F_MELTCD);
            if (typeof data.F_LOTNO != 'undefined') srcF_LOTNO.SetValue(data.F_LOTNO);
            if (typeof data.F_PLANCOUNT != 'undefined') srcF_PLANCOUNT.SetValue(data.F_PLANCOUNT);
            if (typeof data.F_PLANYMD != 'undefined') {
                srcF_PLANYMD.SetValue(convertDateString(data.F_PLANYMD));
                schF_PLANYMD.SetValue(convertDateString(data.F_PLANYMD));
            }
            if (typeof data.F_MEMO != 'undefined') srcF_MEMO.SetValue(data.F_MEMO);
            if (typeof data.F_PLANWORKER1 != 'undefined') {
                if (Trim(data.F_PLANWORKER1) == '') srcF_PLANWORKER1.SetSelectedIndex(0);
                else srcF_PLANWORKER1.SetValue(Trim(data.F_PLANWORKER1));
            }
            if (typeof data.F_PLANWORKER2 != 'undefined') {
                if (Trim(data.F_PLANWORKER2) == '') srcF_PLANWORKER2.SetSelectedIndex(0);
                else srcF_PLANWORKER2.SetValue(Trim(data.F_PLANWORKER2));
            }
            if (typeof data.F_PLANWORKER3 != 'undefined') {
                if (Trim(data.F_PLANWORKER3) == '') srcF_PLANWORKER3.SetSelectedIndex(0);
                else srcF_PLANWORKER3.SetValue(Trim(data.F_PLANWORKER3));
            }
        }

        function SetButtonEnable(enable) {
            //var v = enable ? 'inline-block' : 'none';
            //$('#btnSubmit').css('display', v);
            //$('#btnSubmitCancel').css('display', v);
        }
    </script>
    <script type="text/javascript">
        // 이벤트 핸들러
        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            // 리턴값 처리
            var result = '';
            var data = '';
            if (Trim(s.cpResult) != '') {
                try {
                    result = JSON.parse(s.cpResult);
                    if (result.error != '') {
                        alert(result.error);
                    } else {
                        if (result.msg != '') {
                            ucNavi.SetNoticeOnce(result.msg);
                        }
                        if (result.data != '') {
                            data = result.data;
                        }
                    }
                } catch (err) {
                    alert(err);
                }
            }

            // 그리드별 처리
            switch (s) {
                case devGrid:
                    break;
                case devGrid1:
                    break;
            }
        }

        function fn_OnEndCallback2(s, e) {


            alert(s.cpResultMsg);
            
        }

        // ASPxCallback 사용 : 우측항목 조회, 저장, 삭제
        function devCallback_CallbackComplete(s, e) {
            try {
                if (e.result != '') {
                    var result = JSON.parse(e.result);
                    if (typeof result.msg != 'undefined' && Trim(result.msg) != '') ucNavi.SetNoticeOnce(result.msg);
                    if (typeof result.error != 'undefined' && Trim(result.error) != '') {
                        alert(result.error);
                        return false;
                    }
                    switch (result.action) {
                        case "VIEW":
                            if (typeof result.data == 'string') {
                                result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            }
                            SetValue(result.data);
                            SetPageMode('EDIT');
                            fn_OnValidate();
                            break;
                        case "SAVE":
                            result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            SetValue(result.data);
                            SetPageMode('EDIT');
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('저장되었습니다.');
                            break;
                        case "UPDATE":
                            result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            SetValue(result.data);
                            SetPageMode('EDIT');
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('저장되었습니다.');
                            break;
                        case "DELETE":
                            //SetInit();
                            SetNew();
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('작업지시가 취소되었습니다.');
                            break;
                        default:
                            break;
                    }
                }
            } catch (err) {
                alert(err);
            }
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
            var key = s.GetRowKey(e.visibleIndex);
            ViewList(key);
        }

        function devGrid1_RowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            ViewSelectedItem(key);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divLeft" style="width: 29%; float: left;">
            <div class="blockTitle">
                <span>[금형목록]</span>
            </div>
            <div id="divLeftTop" style="width: 100%;">
                <table class="InputTable" style="margin-bottom: 5px;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 23%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">품번/금형번호</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox runat="server" ID="schF_SEARCHTEXT" ClientInstanceName="schF_SEARCHTEXT" ClientEnabled="true" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLeftBody" style="width: 100%;">
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_MOLDNO;F_MOLDNTH;F_CAVITY" EnableViewState="false" EnableRowsCache="false"
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
                        CallbackError="fn_OnCallbackError"
                        RowDblClick="devGrid_RowDblClick" />
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="40px" />
                        <%--<dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />--%>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_MOLDNO" Caption="금형번호" Width="100px" />
                        <%--<dx:GridViewDataColumn FieldName="F_MOLDNTH" Caption="차수" Width="100px" />--%>
                        <dx:GridViewDataColumn FieldName="F_CAVITY" Visible="false" />
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
        <div id="divSpace" style="width: 1%; float: left;">&nbsp;</div>
        <div id="divRight" style="width: 70%; float: left;">
            <div class="blockTitle">
                <span>[작업지시 정보]</span>
            </div>
            <div id="divRightTop" style="margin-bottom: 10px;">
                <table class="InputTable" style="margin-bottom: 5px; width: 100%;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">생산품번</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">금형번호</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MOLDNO" ClientInstanceName="srcF_MOLDNO" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">금형번호차수</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MOLDNTH" ClientInstanceName="srcF_MOLDNTH" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">Cavity</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_CAVITY" ClientInstanceName="srcF_CAVITY" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divMiddle" style="margin-bottom: 10px;">
                <table class="InputTable" style="margin-bottom: 5px; width: 100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 22%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">작업지시번호</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_WORKNO" ClientInstanceName="srcF_WORKNO" ClientEnabled="false" Width="100%" Text="">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">작업지시일</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_PLANYMD" ClientInstanceName="srcF_PLANYMD" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                    Width="100%" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                        <td class="tdTitle">작업순서</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_WORKNO_SEQ" ClientInstanceName="srcF_WORKNO_SEQ" ClientEnabled="false" Width="100%" Text="0">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수 항목입니다.">
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_WORKNO_SEQ" ClientInstanceName="srcF_WORKNO_SEQ" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="1" MaxValue="10000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">주조기</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_MACHCD" ClientInstanceName="srcF_MACHCD" IncrementalFilteringMode="None" ValueType="System.String">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">보온로</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_MELTCD" ClientInstanceName="srcF_MELTCD" IncrementalFilteringMode="None" ValueType="System.String">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">Lot No.</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_LOTNO" ClientInstanceName="srcF_LOTNO" ClientEnabled="false" Width="100%" MaxLength="20">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">작업자1</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_PLANWORKER1" ClientInstanceName="srcF_PLANWORKER1" IncrementalFilteringMode="None" ValueType="System.String">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수 항목입니다.">
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">작업자2</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_PLANWORKER2" ClientInstanceName="srcF_PLANWORKER2" IncrementalFilteringMode="None" ValueType="System.String">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">작업자3</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_PLANWORKER3" ClientInstanceName="srcF_PLANWORKER3" IncrementalFilteringMode="None" ValueType="System.String">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">지시수량</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_PLANCOUNT" ClientInstanceName="srcF_PLANCOUNT" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수 항목입니다.">
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_PLANCOUNT" ClientInstanceName="srcF_PLANCOUNT" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="0" MaxValue="10000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" DisplayFormatString="{0:#,0}">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">비고</td>
                        <td class="tdContent" colspan="3">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MEMO" ClientInstanceName="srcF_MEMO" ClientEnabled="false" Width="100%" MaxLength="200">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="blockTitle">
                <span>[작업지시 목록]</span>
            </div>
            <div id="divMiddle1" style="float: left; margin-bottom: 2px; width: 33%;">
                <table class="InputTable" style="width: 100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">작업지시일</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_PLANYMD" ClientInstanceName="schF_PLANYMD" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                    Width="100%" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divMiddle2" style="float: left; margin-bottom: 2px; width: 100px; padding-top: 5px; padding-left: 10px;">

                <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick(); return false;" style="width: 80px;">
                    <span class="text">조회</span>
                </button>
            </div>
            <div id="divMiddle3" style="float: left; margin-bottom: 2px; width: 100px; padding-top: 5px; padding-left: 10px;">
                <button class="btn btn-sm btn-success" onclick="fn_OnCompleteClick(); return false;" style="width: 80px;">
                    <span class="text">작업완료</span>
                </button>
            </div>
            <div style="clear: both;"></div>
            <div id="divRightBody" style="width: 100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_COMPCD;F_FACTCD;F_WORKNO" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid1_CustomCallback"
                    OnDataBinding="devGrid1_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn"  AllowSelectSingleRowOnly="true" AllowDragDrop="false" AllowSelectByRowClick="true"/>  
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />
                    <%--<SettingsEditing Mode="Batch">
                        <BatchEditSettings AllowValidationOnEndEdit="true" EditMode="Row" StartEditAction="DblClick" />
                    </SettingsEditing>--%>
                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError"
                        RowDblClick="devGrid1_RowDblClick"/>
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="50px" />
                        <dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />
                        <dx:GridViewDataTextColumn FieldName="F_PLANYMD" Caption="지시일" Width="90px" />
                        <dx:GridViewDataTextColumn FieldName="F_WORKNO_SEQ" Caption="작업순서" Width="70px" />
                        <dx:GridViewDataTextColumn FieldName="F_WORKNO" Caption="작업지시번호" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_MACHNM" Caption="설비명" Width="90px" />
                        <dx:GridViewDataTextColumn FieldName="F_LOTNO" Caption="Lot No." Width="90px" />
                        <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="생산품번" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_MOLDNO" Caption="금형번호" Width="90px" />
                        <dx:GridViewDataTextColumn FieldName="F_MOLDNTH" Caption="금형차수" Width="70px" />
                        <dx:GridViewDataTextColumn FieldName="F_CAVITY" Caption="Cavity" Width="70px" />
                        <dx:GridViewDataSpinEditColumn FieldName="F_PLANCOUNT" Caption="지시수량" Width="90px" PropertiesSpinEdit-DisplayFormatString="{0:#,0}" />
                        <dx:GridViewDataTextColumn FieldName="F_MELTNM" Caption="보온로" Width="90px" />
                        <dx:GridViewDataTextColumn FieldName="F_STATUSNM" Caption="상태" Width="90px" />
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
        <%--<dx:ASPxTextBox runat="server" ID="srcF_STARTYN" ClientInstanceName="srcF_STARTYN" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcApprF_PJ11D1ID" ClientInstanceName="srcApprF_PJ11D1ID" ClientVisible="false">
        </dx:ASPxTextBox>--%>
        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>
        <%--<dx:ASPxTextBox runat="server" ID="hidGridData" ClientInstanceName="hidGridData" ClientVisible="false">
        </dx:ASPxTextBox>--%>
        <%--<dx:ASPxButton ID="btnResourceTemp" runat="server" Image-IconID="save_save_32x32" ClientVisible="false" ></dx:ASPxButton>--%>
        <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>
        <dx:ASPxCallback ID="devCallback2" runat="server" ClientInstanceName="devCallback2" OnCallback="devCallback2_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback2" />
        </dx:ASPxCallback>
    </div>
</asp:Content>
