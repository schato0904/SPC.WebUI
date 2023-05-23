<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM3111.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM3111" %>
<%-- 출하지시서 등록 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        /*동적 생성 테이블*/
        ._cTab {
            color:black;
            width:100%;
            border:1px solid #9F9F9F;
        }
        /*테이블 헤더 tr 스타일*/
        ._cTrH {
            background-color: #DCDCDC;
            color: black;
        }
        /*테이블 데이터 tr 스타일*/
        ._cTrD {
            height:40px;
        }
        /*테이블 헤더 td 스타일*/
        ._cTdH {
            /*font-size: 0.9em;*/
            text-align:center;
            padding-left:5px;
            padding-right:5px;
            padding-top:3px;
            padding-bottom:3px;
        }
        /*테이블 데이터 td 스타일*/
        ._cTdD {
            /*font-size: 0.9em;*/
            text-align:center;
            background-color:white;
            padding-left:5px;
            padding-right:5px;
            padding-top:3px;
            padding-bottom:3px;
        }
        /*검사성적 컬럼 너비*/
        .wCHK {
            width: 80px;
        }
        .wWORKNO {
            width: 180px;
        }
        .wREMAINS {
            width: 120px;
        }
        .wOUTCOUNT {
            width: 120px;
        }
        ._cTBox {
            width: 100%;
            text-align:right;
        }
        ._gridNODATA {
            height: 110px;
            text-align: center;
            background-color: white;
        }
    </style>
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

        // 입력
        function fn_OnNewClick() {
            SetNew();
            devGrid1.PerformCallback('NEW');
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
                'action': action
            };
            hidGridData.SetText(encodeURIComponent(JSON.stringify(GetGridValue())));
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(params)));
        }

        // 취소
        function fn_OnCancelClick() {
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (GetPageMode() != "EDIT" || !HasPkey()) {
                //ucNavi.SetNoticeOnce('필요한 항목이 모두 입력되지 않았습니다.', 'red');
                alert('삭제할 데이터가 없습니다.');
                return false;
            }

            if (!confirm('삭제하시겠습니까?')) {
                return false;
            }

            var params = {
                'action': 'DELETE'
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(params)));
        }

        // 인쇄
        function fn_OnPrintClick() {
            var F_OUTORDERNO = Trim(srcF_OUTORDERNO.GetText());
            var TITLE = '부품식별표';
            if (F_OUTORDERNO == '') {
                ucNavi.SetNoticeOnce('인쇄할 내용이 없습니다.', 'red');
                return false;
            }

            fn_OnPopRptCATM3101(TITLE, F_OUTORDERNO, '');
        }

        // 부품식별표 팝업창 열기
        function fn_OnPopRptCATM3101(TITLE, F_OUTORDERNO, parentCallbackName) {
            var PopParam = {
                'F_OUTORDERNO': F_OUTORDERNO
            };
            pPage = './Popup/PopRptCATM3101.aspx' +
                '?TITLE=' + encodeURIComponent(TITLE) +
                '&parentCallback=' + parentCallbackName +
                '&PopParam=' + encodeURIComponent(JSON.stringify(PopParam));
            //pPage = rootURL + 'Pages/Common/Popup/INSPREPORTPOP.aspx?TITLE=test&KEYFIELDS=14|LB%20RA506101|41940|04|1';
            fn_OnPopupOpen(pPage, '1000', '0');
        }

        // 엑셀
        function fn_OnExcelClick() {
            //var devGrid1 = ASPxClientGridView.Cast('devGrid1');
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
            return Trim(srcF_OUTORDERNO.GetText()) != '' && Trim(srcF_OUTORDERNO.GetText()) != '0';
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
            //    srcF_ITEMCD.SetValue('');
            //    srcF_ITEMNM.SetValue('');
            //}
            //var today = new Date().toISOString().slice(0, 10);
            srcF_OUTORDERNO.SetValue('');
            srcF_OUTYMD.SetValue(new Date());
            srcF_CUSTCD.SetValue('');
            srcF_ORDERNO.SetValue('');
            //srcF_ITEMCD.SetValue('');
            srcF_OUTCOUNT.SetValue(0);
            srcF_MEMO.SetValue('');
            hidGridData.SetValue('');
        }
        // 기본값 설정
        function SetDefault() {
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
            srcF_OUTYMD.SetEnabled(enable);
            srcF_CUSTCD.SetEnabled(enable);
            srcF_ORDERNO.SetEnabled(enable);
            //srcF_ITEMCD.SetEnabled(enable);
            //srcF_OUTCOUNT.SetEnabled(enable);
            srcF_MEMO.SetEnabled(enable);
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
                //'F_COMPCD': pkey[0],
                //'F_FACTCD': pkey[1],
                'F_OUTORDERNO': pkey[0]
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 선택항목 조회(좌측 키로 우측 목록 조회)
        function ViewList(pkey) {
            // 콜백으로 우측목록 grid 조회
            pkey = pkey.split('|');
            srcF_ITEMCD.SetText(pkey[0]);
            srcF_ITEMNM.SetText(pkey[1]);
            devGrid1.PerformCallback('NEW');
            SetNew();
        }
        // 컨트롤 값 설정
        function SetValue(data) {
            if (typeof data.F_OUTORDERNO != 'undefined') srcF_OUTORDERNO.SetValue(data.F_OUTORDERNO);
            if (typeof data.F_OUTYMD != 'undefined') srcF_OUTYMD.SetValue(str2date(data.F_OUTYMD));
            if (typeof data.F_CUSTCD != 'undefined') srcF_CUSTCD.SetValue(data.F_CUSTCD);
            if (typeof data.F_ORDERNO != 'undefined') srcF_ORDERNO.SetValue(data.F_ORDERNO);
            if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            if (typeof data.F_ITEMNM != 'undefined') srcF_ITEMNM.SetValue(data.F_ITEMNM);
            if (typeof data.F_OUTCOUNT != 'undefined') srcF_OUTCOUNT.SetValue(data.F_OUTCOUNT);
            if (typeof data.F_MEMO != 'undefined') srcF_MEMO.SetValue(data.F_MEMO);
        }

        function SetGridValue(dataList) {
            var html = '';
            var htmlfooter = '';
            if (!Array.isArray(dataList)) return false;
            html = '<table id="_cTab1" class="_cTab" border="1"><thead><tr class="_cTrH"><th class="_cTdH wCHK"><input type="checkbox" id="chkAll" onchange="chkAll_OnChange(this);" /></th><th class="_cTdH wWORKNO">작업지시번호</th><th class="_cTdH wREMAINS">재고</th><th class="_cTdH wOUTCOUNT">출하수량</th></tr></thead><tbody>';
            htmlfooter = '</tbody></table>';
            for (var i = 0; i < dataList.length; i++) {
                html += '<tr class="_cTrD" id="_cTab1_Tr_' + i + '"><td class="_cTdD wCHK"><input type="checkbox" id="_cTab1_chk_' + i + '" onchange="ReCal_OUTCOUNT();" ' + (parseInt(dataList[i].F_COUNT, 10) > 0 ? 'checked="checked"' : '') + ' /></td><td class="_cTdD wWORKNO" id="_cTab1_F_WORKNO_' + i + '">' + dataList[i].F_WORKNO + '</td><td class="_cTdD wREMAINS" id="_cTab1_F_REMAINS_' + i + '">' + dataList[i].F_REMAINS + '</td><td class="_cTdD wOUTCOUNT"><input type="text" id="_cTab1_F_COUNT_' + i + '" value="' + dataList[i].F_COUNT + '" onblur="F_COUNT_OnBlur(this);" /></td></tr>';
            }
            html += htmlfooter;

            $('#divOUTCOUNT').html(html);
        }

        function chkAll_OnChange(chk) {
            var isChecked = $(chk).prop('checked');
            $('#_cTab1>tbody>tr>td>input:checkbox').prop('checked', isChecked);
            ReCal_OUTCOUNT();
        }

        function ReCal_OUTCOUNT() {
            var result = 0;
            var rowcnt = $('#_cTab1>tbody>tr').length;
            for (var i = 0; i < rowcnt; i++) {
                if (!$('#_cTab1_chk_' + i).is(':checked')) continue;
                result += parseInt($('#_cTab1_F_COUNT_' + i).val(), 10);
            }
            srcF_OUTCOUNT.SetValue(result);
        }

        function F_COUNT_OnBlur(txt) {
            var t = Trim($(txt).val());
            if (t == '' || isNaN(t)) $(txt).val('0');
            else $(txt).val(parseInt(t, 10));
            ReCal_OUTCOUNT();
        }

        function GetGridValue() {
            var result = [];
            var dic = {};
            var rowcnt = $('#_cTab1>tbody>tr').length;
            var CHK = false;
            var F_WORKNO = '', F_COUNT = '';
            for (var i = 0; i < rowcnt; i++) {
                CHK = $('#_cTab1_chk_' + i).is(':checked');
                if (!CHK) continue;
                F_WORKNO = $('#_cTab1_F_WORKNO_' + i).text();
                F_COUNT = $('#_cTab1_F_COUNT_' + i).val();
                dic = {
                    'F_WORKNO': F_WORKNO,
                    'F_COUNT': F_COUNT
                };
                result.push(dic);
            }

            return result;
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
                    var griddata = {};
                    if (typeof s.cpGridData != 'undefined' && typeof s.cpGridData == 'string' && s.cpGridData != '') {
                        griddata = JSON.parse(decodeURIComponent(s.cpGridData || '{}'));
                        SetGridValue(griddata);
                        ReCal_OUTCOUNT();
                    }
                    s.cpGridData = '';
                    break;
            }
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

                            if (typeof result.griddata == 'string') {
                                result.griddata = JSON.parse(decodeURIComponent(result.griddata || '{}'));
                            }
                            SetGridValue(result.griddata);

                            SetPageMode('EDIT');
                            fn_OnValidate();
                            break;
                        case "SAVE":
                            result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            SetValue(result.data);

                            if (typeof result.griddata == 'string') {
                                result.griddata = JSON.parse(decodeURIComponent(result.griddata || '{}'));
                            }
                            SetGridValue(result.griddata);

                            SetPageMode('EDIT');
                            devGrid.PerformCallback('GET');
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('저장되었습니다.');
                            break;
                        case "UPDATE":
                            result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            SetValue(result.data);

                            if (typeof result.griddata == 'string') {
                                result.griddata = JSON.parse(decodeURIComponent(result.griddata || '{}'));
                            }
                            SetGridValue(result.griddata);

                            SetPageMode('EDIT');
                            devGrid.PerformCallback('GET');
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('저장되었습니다.');
                            break;
                        case "DELETE":
                            //SetInit();
                            SetNew();
                            devGrid.PerformCallback('GET');
                            devGrid1.PerformCallback('NEW');
                            ucNavi.SetNoticeOnce('삭제되었습니다.');
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
            result = result && (srcF_OUTCOUNT.GetNumber() > 0);
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
        <div id="divLeft" style="width:29%;float:left;">
            <div class="blockTitle">
                <span>[출하대상 품목 목록]</span>
            </div>
            <div id="divLeftTop" style="width:100%;">
                <table class="InputTable" style="margin-bottom: 5px;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 23%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">품번/명</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox runat="server" ID="schF_SEARCHTEXT" ClientInstanceName="schF_SEARCHTEXT" ClientEnabled="true" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLeftBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
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
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount"  runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="80px"/>
                        <%--<dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />--%>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="120px"/>
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" />
                        <dx:GridViewDataColumn FieldName="F_REMAINS" Caption="현재고" Width="80px"/>
                        <%--<dx:GridViewDataColumn FieldName="F_NEEDCOUNT" Caption="적정재고" Width="80px"/>--%>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
        <div id="divSpace" style="width:1%;float:left;">&nbsp;</div>
        <div id="divRight" style="width:70%;float:left;">
            <div class="blockTitle">
                <span>[출하지시서 정보]</span>
            </div>
            <div id="divRightTop" style="margin-bottom:10px;">
                <table class="InputTable" style="margin-bottom: 5px;width:33%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">품명</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ITEMNM" ClientInstanceName="srcF_ITEMNM" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                                <dx:ASPxTextBox runat="server" ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" ClientEnabled="false" Width="100%" ClientVisible="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divMiddle" style="margin-bottom:10px;">
                <table class="InputTable" style="margin-bottom: 5px;width:100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 22%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">출하일자</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_OUTYMD" ClientInstanceName="srcF_OUTYMD" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                        <td class="tdTitle">출하수량</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxSpinEdit runat="server" ID="srcF_OUTCOUNT" ClientInstanceName="srcF_OUTCOUNT" ClientEnabled="false" Width="100%" NumberType="Integer" Number="0" MinValue="1" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" DisplayFormatString="{0:#,0}">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RegularExpression ErrorText="출하수량이 0입니다." ValidationExpression="[1-9]+[0-9]*" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdContent" colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdTitle">출하지시번호</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_OUTORDERNO" ClientInstanceName="srcF_OUTORDERNO" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdContent" colspan="4" rowspan="4" style="vertical-align:top;">
                            <div id="divOUTCOUNT" style="width:100%;height:100%;overflow-y:scroll;">
                                <table class="_cTab" border="1">
                                    <thead>
                                        <tr class="_cTrH">
                                            <th class="_cTdH wCHK"><input type="checkbox" id="chkAll" /></th>
                                            <th class="_cTdH wWORKNO">작업지시번호</th>
                                            <th class="_cTdH wREMAINS">재고</th>
                                            <th class="_cTdH wOUTCOUNT">출하수량</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td colspan="6" class="_gridNODATA">조회한 데이터가 없습니다.</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">발주번호</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ORDERNO" ClientInstanceName="srcF_ORDERNO" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수항목입니다.">
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">출하처</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_CUSTCD" ClientInstanceName="srcF_CUSTCD" IncrementalFilteringMode="None" ValueType="System.String" Width="100%" >
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수항목입니다.">
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">비고</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MEMO" ClientInstanceName="srcF_MEMO" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="blockTitle">
                <span>[출하 목록]</span>
            </div>
            <div id="divMiddle1" style="float:left;margin-bottom:2px;width:33%;">
                <table class="InputTable" style="width:100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">출하지시일</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_OUTYMD" ClientInstanceName="schF_OUTYMD" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" >
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>    
            <div id="divRightBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_OUTORDERNO" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid1_CustomCallback"
                    OnDataBinding="devGrid1_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
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
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount"  runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="60px" />
                        <%--<dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />--%>
                        <dx:GridViewDataColumn FieldName="F_OUTYMD" Caption="출하지시일" Width="120px"/>
                        <dx:GridViewDataColumn FieldName="F_ORDERNO" Caption="발주번호" Width="120px"/>
                        <dx:GridViewDataColumn FieldName="F_CUSTNM" Caption="출하처" Width="120px"/>                                                
                        <%--<dx:GridViewDataColumn FieldName="F_WORKNO" Caption="작업지시번호" Width="120px"/>--%>
                        <dx:GridViewDataColumn FieldName="F_OUTORDERNO" Caption="출하지시번호" Width="120px"/>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="120px"/>
                        <dx:GridViewDataSpinEditColumn FieldName="F_OUTCOUNT" Caption="출하수량" Width="120px" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>
                        <dx:GridViewDataColumn FieldName="F_MEMO" Caption="비고" CellStyle-HorizontalAlign="Left" />
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid1" OnRenderBrick="devGrid1Exporter_RenderBrick"></dx:ASPxGridViewExporter>
            </div>
        </div>
        <div style="clear:both;"></div>
    </div>
    <div id="divHidden" style="display:none;">
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
        <dx:ASPxTextBox runat="server" ID="hidGridData" ClientInstanceName="hidGridData" ClientVisible="false">
        </dx:ASPxTextBox>
        <%--<dx:ASPxButton ID="btnResourceTemp" runat="server" Image-IconID="save_save_32x32" ClientVisible="false" ></dx:ASPxButton>--%>
        <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>
    </div>
</asp:Content>