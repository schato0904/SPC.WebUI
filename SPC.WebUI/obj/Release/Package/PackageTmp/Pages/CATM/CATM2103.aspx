<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM2103.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM2103" %>

<%-- 실사조정 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css" id="cssPLAN1016">
        /*동적 생성 테이블*/
        ._cTab {
            color: black;
            width: 100%;
            /*border:1px solid #9F9F9F;*/
            table-layout: fixed;
            border-collapse: collapse;
        }

            ._cTab > tbody > tr > td {
                border: 1px solid #9F9F9F;
            }
        /* 헤더 최상단 컬럼 너비 설정용*/
        ._colTrH {
            height: 0px;
            padding: 0;
            border-style: none;
            border-width: 0;
            border-color: transparent;
        }

        ._colTdH {
            height: 0px;
            padding: 0;
            margin: 0;
            border-style: none;
            border-width: 0;
            border-color: transparent;
        }
        /*테이블 헤더 tr 스타일*/
        ._cTrH {
            background-color: #DCDCDC;
            color: black;
            border: 1px solid #9F9F9F;
        }
        /*테이블 데이터 tr 스타일*/
        ._cTrD {
            height: 40px;
            border: 1px solid #9F9F9F;
        }
        /*테이블 헤더 td 스타일*/
        ._cTdH {
            /*font-size: 0.9em;*/
            text-align: center;
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 3px;
            padding-bottom: 3px;
            border: 1px solid #9F9F9F;
        }

        ._bTop0 {
            border-top: 0 none transparent;
        }

        /*테이블 데이터 td 스타일*/
        ._cTdD {
            /*font-size: 0.9em;*/
            text-align: center;
            background-color: white;
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 3px;
            padding-bottom: 3px;
        }

        /* 각 컬럼 너비 설정 */
        /* 각 컬럼별 너비는 하단의 hidden필드에 지정하여 사용한다. */
        .wNo {
            width: 5%;
        }

        .wF_ITEMCD {
            width: 10%;
        }

        .wF_ITEMNM {
            width: 10%;
        }

        .wF_REMAINS {
            width: 10%;
        }

        .wF_REALCNT {
            width: 10%;
        }

            .wF_REALCNT > input[type="text"] {
                width: 100%;
            }

        .wF_FIXCNT {
            width: 10%;
        }

            .wF_FIXCNT > input[type="text"] {
                width: 100%;
            }
        /*.wF_NEEDCOUNT { width: 10%; }*/
        .wF_UNIT {
            width: 10%;
        }

        .wF_MEMO {
            width: 30%;
        }

            .wF_MEMO > input[type="text"] {
                width: 100%;
            }

        /*그리드 텍스트박스*/
        ._cTBox {
            width: 100%;
            text-align: right;
        }
        /*그리드 최초 데이터 없음 영역*/
        ._gridNODATA {
            height: 200px;
            text-align: center;
            background-color: white;
        }
        /*그리드 색상 변경 영역 셀스타일*/
        ._cCell {
            /*font-size: 0.9em;*/
            text-align: center;
            background-color: white;
            padding-left: 2px;
            padding-right: 2px;
            padding-top: 2px;
            padding-bottom: 2px;
        }
        /*그리드 읽기전용 셀스타일*/
        ._RCell {
            background-color: #EEEEEE;
        }
        /*그리드 비활성화 셀스타일*/
        ._DisabledRow {
            background-color: #EEEEEE;
        }
        /*그리드 NG셀스타일*/
        ._NGRow {
            background-color: #FFCCCC;
        }

        ._Click {
            cursor: pointer;
        }

        a._Click {
            color: RoyalBlue;
        }

        ._lineBlue, ._lineBlue > a {
            color: RoyalBlue;
            cursor: pointer;
            text-decoration-line: underline;
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
            //fnASPxGridView_ReHeight(devGrid);
        }

        // 조회
        function fn_OnSearchClick() {
            //devGrid.PerformCallback('GET');
            //devGrid1.PerformCallback('GET');
            var params = {
                'action': 'VIEW'
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(params)));
        }

        // 입력
        function fn_OnNewClick() {
            //SetNew();
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
            var md = GetPageMode();

            if (!HasPkey()) {
                ucNavi.SetNoticeOnce('저장할 내용이 없습니다.', 'red');
                return false;
            }

            if (!fn_OnValidate()) {
                ucNavi.SetNoticeOnce('필요한 항목이 모두 입력되지 않았습니다.', 'red');
                return false;
            }

            if (!confirm('저장하시겠습니까?')) {
                return false;
            }

            var action = 'SAVE'; // (md == 'NEW' ? 'SAVE' : 'UPDATE');

            var params = {
                'action': action
            };
            hidGridData.SetText(encodeURIComponent(JSON.stringify(getVALUES1())));
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(params)));
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
            return Trim(schF_INYMD.GetText()) != '';
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

            ClearDivGrid();
        }
        // 기본값 설정
        function SetDefault() {
            schF_INYMD.SetValue(new Date());
        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        function SetPageMode(mode) {
            // 페이지 모드에 따라 설정
            switch (mode) {
                case "NEW":
                    //SetPageEnable(Trim(srcF_ITEMCD.GetText()) != '');
                    SetPageEnable(true);
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
        }

        // 페이지모드 조회
        function GetPageMode() {
            return hidPageMode.GetText();
        }

        // 선택항목 조회(좌측 키로 우측 조회)
        function ViewSelectedItem(pkey) {
            //// 콜백으로 우측영역값 json으로 조회
            //// 우측영역 세팅
            //pkey = pkey.split('|');
            //var param = {
            //    'action': 'VIEW',
            //    'F_COMPCD': pkey[0],
            //    'F_FACTCD': pkey[1],
            //    'F_WORKNO': pkey[2]
            //};
            //devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 컨트롤 값 설정
        function SetValue(data) {
            //if (typeof data.F_WORKNO != 'undefined') srcF_WORKNO.SetValue(data.F_WORKNO);
            //if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            //if (typeof data.F_MACHNM != 'undefined') srcF_MACHNM.SetValue(data.F_MACHNM);
            //if (typeof data.F_MELTNM != 'undefined') srcF_MELTNM.SetValue(data.F_MELTNM);
            //if (typeof data.F_PLANCOUNT != 'undefined') srcF_PLANCOUNT.SetValue(data.F_PLANCOUNT);
            //if (typeof data.F_PRODCOUNT != 'undefined') srcF_PRODCOUNT.SetValue(data.F_PRODCOUNT);
            //if (typeof data.F_ERRCOUNT != 'undefined') srcF_ERRCOUNT.SetValue(data.F_ERRCOUNT);
            //if (typeof data.F_LOSSHOUR != 'undefined') srcF_LOSSHOUR.SetValue(data.F_LOSSHOUR);
            //if (typeof data.F_INYMD != 'undefined') srcF_INYMD.SetValue(data.F_INYMD);
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

            //// 그리드별 처리
            //switch (s) {
            //    case devGrid:
            //        break;
            //    //case devGrid1:
            //    //    break;
            //}
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
                            DrawGridHtml(result.gridhtml);
                            break;
                        case "SAVE":
                            result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            SetValue(result.data);
                            SetPageMode('EDIT');
                            DrawGridHtml(result.gridhtml);
                            //devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('저장되었습니다.');
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
            ViewSelectedItem(key);
        }

        function schF_INYMD_DateChanged(s, e) {
            fn_OnSearchClick();
        }
    </script>
    <script type="text/javascript">
        // --------------- 그리드 처리함수 목록 -----------------
        // 그리드 데이터 추출
        function getVALUES1() {
            var rowcnt = $('#_cTab1>tbody>tr').length;
            var data = [];
            var rowdata = {};

            var F_ITEMCD = ''
            , F_FIXCNT = 0
            , F_MEMO = ''
            , jsondata = {};

            for (var i = 0; i < rowcnt; i++) {
                jsondata = JSON.parse($('#_JSONDATA_' + i).val() || '{}');
                F_ITEMCD = jsondata.F_ITEMCD;
                F_FIXCNT = parseInt($('#_F_FIXCNT_' + i).val(), 10);
                F_MEMO = parseInt($('#_F_MEMO_' + i).val(), 10);

                rowdata = {
                    'F_ITEMCD': F_ITEMCD,
                    'F_FIXCNT': F_FIXCNT,
                    'F_MEMO': F_MEMO
                };

                data.push(rowdata);
            }
            //alert(data);
            return data;
            //result = {
            //    'data1': data1
            //};

            //return result;
        }

        // 필수항목 공백 체크
        function isEmptyVALUES() {
            var rowcnt = 0;
            rowcnt = $('#_cTab1>tbody>tr').length;
            var result = false;

            return result;
        }
        // 그리드 초기화
        function ClearDivGrid() {
            $('#divGrid').html('<table id="_cTab1" class="_cTab" border="0" style="width:100%;"><thead><tr class="_cTrH"><th class="_cTdH wNo" >No</th><th class="_cTdH wF_ITEMCD" >품목코드</th><th class="_cTdH wF_ITEMNM" >품목명</th><th class="_cTdH wF_REMAINS" >현재고(조정일)</th><th class="_cTdH wF_REALCNT" >실사수량</th><th class="_cTdH wF_FIXCNT" >조정수량</th><th class="_cTdH wF_UNIT" >단위</th><th class="_cTdH wF_MEMO" >비고</th></tr></thead><tbody><tr class="_cTrD"><td class="_gridNODATA" colspan="8">조회한 데이터가 없습니다.</td></tr></tbody></table>');
        }
    </script>
    <script type="text/javascript">
        function DrawGridHtml(d) {
            if (typeof d == 'undefined' || d == null || typeof d.html1 == 'undefined' || d.html1 == null) {
                loadingPanel.Hide();
                return false;
            }
            if (d.html1 == '') {
                ClearDivGrid();
            } else {
                var html1 = decodeURIComponent(d.html1);
                $('#divGrid').html(html1);
            }

            //if (d.html2 == '') {
            //    ClearDivGrid2();
            //} else {
            //    var html2 = decodeURIComponent(d.html2);
            //    $('#divGrid2').html(html2);
            //}

            loadingPanel.Hide();
            return false;
        }

        function F_REALCNT_OnBlur(i) {
            var jsondata = JSON.parse($('#_JSONDATA_' + i).val() || '{}');
            var F_REMAINS = parseInt(jsondata.F_REMAINS, 10);
            var F_REALCNT = parseInt($('#_F_REALCNT_' + i).val(), 10);
            var F_FIXCNT = F_REALCNT - F_REMAINS;
            $('#_F_FIXCNT_' + i).val(F_FIXCNT);
        }

        function F_FIXCNT_OnBlur(i) {
            var jsondata = JSON.parse($('#_JSONDATA_' + i).val() || '{}');
            var F_REMAINS = parseInt(jsondata.F_REMAINS, 10);
            var F_FIXCNT = parseInt($('#_F_FIXCNT_' + i).val(), 10);
            var F_REALCNT = F_FIXCNT + F_REMAINS;
            $('#_F_REALCNT_' + i).val(F_REALCNT);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container" style=" ">
        <div id="divLeft" style="width: 100%; float: left;">
            <%--<div class="blockTitle">
                <span>[금형목록]</span>
            </div>--%>
            <div id="divLeftTop" style="width: 100%;">
                <table class="InputTable" style="margin-bottom: 5px; width: 33%;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 23%" />
                        <%--<col style="width: 67%" />--%>
                    </colgroup>
                    <tr>
                        <td class="tdTitle">조정일자</td>
                        <td class="tdContent">
                            <dx:ASPxDateEdit runat="server" ID="schF_INYMD" ClientInstanceName="schF_INYMD" UseMaskBehavior="true"
                                EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                Width="100%" AllowNull="true">
                                <ClientSideEvents DateChanged="schF_INYMD_DateChanged" />
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLeftBody" style="overflow:scroll; width: 100%; height:700px;">
                <div id="divGrid" style=" width: 100%; ">
                </div>
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
        <dx:ASPxTextBox runat="server" ID="hidGridData" ClientInstanceName="hidGridData" ClientVisible="false">
        </dx:ASPxTextBox>
        <%--<dx:ASPxButton ID="btnResourceTemp" runat="server" Image-IconID="save_save_32x32" ClientVisible="false" ></dx:ASPxButton>--%>
        <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>
    </div>
</asp:Content>
