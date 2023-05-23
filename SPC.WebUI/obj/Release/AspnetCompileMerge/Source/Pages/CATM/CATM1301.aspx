<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM1301.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM1301" %>
<%-- 생산실적 등록 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css" id="cssPLAN1016">
        /*동적 생성 테이블*/
        ._cTab {
            color:black;
            width:100%;
            /*border:1px solid #9F9F9F;*/
            table-layout:fixed;
            border-collapse:collapse;
        }
            ._cTab > tbody > tr > td {
                border: 1px solid #9F9F9F;
            }
        /* 헤더 최상단 컬럼 너비 설정용*/
        ._colTrH {
            height:0px;
            padding:0;
            border-style:none;
            border-width:0;
            border-color:transparent;
        }
        ._colTdH {
            height:0px;
            padding:0;
            margin:0;
            border-style:none;
            border-width:0;
            border-color:transparent;
        }
        /*테이블 헤더 tr 스타일*/
        ._cTrH {
            background-color: #DCDCDC;
            color: black;
            border:1px solid #9F9F9F;
        }
        /*테이블 데이터 tr 스타일*/
        ._cTrD {
            height:40px;
            border:1px solid #9F9F9F;
        }
        /*테이블 헤더 td 스타일*/
        ._cTdH {
            /*font-size: 0.9em;*/
            text-align:center;
            padding-left:5px;
            padding-right:5px;
            padding-top:3px;
            padding-bottom:3px;
            border:1px solid #9F9F9F;
        }

        ._bTop0 {
            border-top:0 none transparent;
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

        /* 각 컬럼 너비 설정 */
        /* 각 컬럼별 너비는 하단의 hidden필드에 지정하여 사용한다. */        
        .wNo { width: 10%; }
        .wF_ERRORNM { width: 60%; }
        .wF_ERRORCOUNT { width: 30%; }
            .wF_ERRORCOUNT input[type="text"] {
                width: 100%;
            }
        .wF_LOSSNM { width: 60%; }
        .wF_LOSSHOUR { width: 30%; }

        /*그리드 텍스트박스*/
        ._cTBox {
            width: 100%;
            text-align:right;
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
            text-align:center;
            background-color:white;
            padding-left:2px;
            padding-right:2px;
            padding-top:2px;
            padding-bottom:2px;
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

        ._Click{
            cursor: pointer;
        }

        a._Click{
            color: RoyalBlue;
        }

        ._lineBlue, ._lineBlue>a {
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
            fnASPxGridView_ReHeight(devGrid);
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid.PerformCallback('GET');
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
                'action': action,
                'F_WORKNO': Trim(srcF_WORKNO.GetText())
            };
            hidGridData1.SetText(encodeURIComponent(JSON.stringify(getVALUES1())));
            hidGridData2.SetText(encodeURIComponent(JSON.stringify(getVALUES2())));
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
            return Trim(srcF_WORKNO.GetText()) != '' ;
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
            //srcF_WORKNO_SEQ.SetValue('0');
            srcF_MACHNM.SetValue('');
            srcF_MELTNM.SetValue('');
            //srcF_MACHCD.SetSelectedIndex(0);
            //srcF_MELTCD.SetSelectedIndex(0);
            srcF_ITEMCD.SetValue('');
            srcF_PLANCOUNT.SetValue('');
            srcF_PRODCOUNT.SetValue('');
            srcF_ERRCOUNT.SetValue('');
            srcF_LOSSHOUR.SetValue('');

            ClearDivGrid1();
            ClearDivGrid2();
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
            schF_PLANYMD.SetValue(new Date());
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
            srcF_PRODCOUNT.SetEnabled(enable);
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

        // 컨트롤 값 설정
        function SetValue(data) {
            if (typeof data.F_WORKNO != 'undefined') srcF_WORKNO.SetValue(data.F_WORKNO);
            if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            if (typeof data.F_MACHNM != 'undefined') srcF_MACHNM.SetValue(data.F_MACHNM);
            if (typeof data.F_MELTNM != 'undefined') srcF_MELTNM.SetValue(data.F_MELTNM);
            if (typeof data.F_PLANCOUNT != 'undefined') srcF_PLANCOUNT.SetValue(data.F_PLANCOUNT);
            if (typeof data.F_PRODCOUNT != 'undefined') srcF_PRODCOUNT.SetValue(data.F_PRODCOUNT);
            if (typeof data.F_ERRCOUNT != 'undefined') srcF_ERRCOUNT.SetValue(data.F_ERRCOUNT);
            if (typeof data.F_LOSSHOUR != 'undefined') srcF_LOSSHOUR.SetValue(data.F_LOSSHOUR);
            if (typeof data.F_PLANYMD != 'undefined') srcF_PLANYMD.SetValue(data.F_PLANYMD);
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
                //case devGrid1:
                //    break;
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
                                result.data = JSON.parse(decodeURIComponent(result.data||'{}'));
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
    </script>
    <script type="text/javascript">
        // --------------- 그리드 처리함수 목록 -----------------
        // 그리드 데이터 추출
        function getVALUES1() {
            var rowcnt = $('#_cTab1>tbody>tr').length;
            var data = [];
            var rowdata = {};

            var F_ERRORCD = ''
            , F_ERRORCOUNT = 0
            , jsondata = {};

            for (var i = 0; i < rowcnt; i++) {
                jsondata = JSON.parse($('#_JSONDATA1_' + i).val() || '{}');
                F_ERRORCD = jsondata.F_ERRORCD;
                F_ERRORCOUNT = parseInt($('#_F_ERRORCOUNT_' + i).val(), 10);

                rowdata = {
                    'F_ERRORCD': F_ERRORCD,
                    'F_ERRORCOUNT': F_ERRORCOUNT
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

        // 그리드 데이터 추출
        function getVALUES2() {
            var rowcnt = $('#_cTab2>tbody>tr').length;
            var data = [];
            var rowdata = {};

            var F_LOSSCD = ''
            , F_LOSSHOUR = 0
            , jsondata = {}
            , jsondetail = [];

            for (var i = 0; i < rowcnt; i++) {
                //jsondata = JSON.parse($('#_JSONDATA2_' + i).val() || '{}');
                //F_LOSSCD = jsondata.F_LOSSCD;
                //F_LOSSHOUR = parseInt($('#_F_LOSSHOUR_' + i).val(), 10);
                jsondetail = JSON.parse($('#_JSONDATADETAIL_' + i).val() || '[]');

                //rowdata = {
                //    'F_LOSSCD': F_LOSSCD,
                //    'F_LOSSHOUR': F_LOSSHOUR,
                //    'JSONDETAIL': jsondetail
                //};
                while (rowdata = jsondetail.shift()) {
                    data.push(rowdata);
                }
            }
            return data;
        }

        // 필수항목 공백 체크
        function isEmptyVALUES() {
            var rowcnt = 0;
            rowcnt = $('#_cTab1>tbody>tr').length;
            var result = false;

            return result;
        }
        // 그리드 초기화
        function ClearDivGrid1() {
            $('#divGrid1').html('<table id="_cTab1" class="_cTab" border="0" style="width:100%;"><thead><tr class="_cTrH"><th class="_cTdH wNo">No</th><th class="_cTdH wF_ERRORNM">불량유형</th><th class="_cTdH wF_ERRORCOUNT">불량수량</th></tr></thead><tbody><tr class="_cTrD"><td class="_gridNODATA" colspan="3">조회한 데이터가 없습니다.</td></tr></tbody></table>');
        }
        // 그리드 초기화
        function ClearDivGrid2() {
            $('#divGrid2').html('<table id="_cTab1" class="_cTab" border="0" style="width:100%;"><thead><tr class="_cTrH"><th class="_cTdH wNo">No</th><th class="_cTdH wF_LOSSNM">유실유형</th><th class="_cTdH wF_LOSSHOUR">유실시간</th></tr></thead><tbody><tr class="_cTrD"><td class="_gridNODATA" colspan="3">조회한 데이터가 없습니다.</td></tr></tbody></table>');
        }
    </script>
    <script type="text/javascript">
        function DrawGridHtml(d) {
            if (typeof d == 'undefined' || d == null || ((typeof d.html1 == 'undefined' || d.html1 == null) && (typeof d.html2 == 'undefined' || d.html2 == null))) {
                loadingPanel.Hide();
                return false;
            }
            if (d.html1 == '') {
                ClearDivGrid1();
            } else {
                var html1 = decodeURIComponent(d.html1);
                $('#divGrid1').html(html1);
            }

            if (d.html2 == '') {
                ClearDivGrid2();
            } else {
                var html2 = decodeURIComponent(d.html2);
                $('#divGrid2').html(html2);
            }

            loadingPanel.Hide();
            return false;            
        }

        // 유실유형 row클릭
        function trLOSSCD_OnClick(rowIdx) {
            // 1. 변수 설정
            var jsondata = JSON.parse($('#_JSONDATA2_' + rowIdx).val() || '{}');
            var jsondatadetail = JSON.parse($('#_JSONDATADETAIL_' + rowIdx).val() || '[]');
            var F_LOSSNM = jsondata.F_LOSSNM;
            var F_LOSSCD = jsondata.F_LOSSCD;
            var trdata = {};
            var trhtml = '';
            var idx = 0;
            // 2. 헤드 세팅
            popsrcF_LOSSNM.SetText(F_LOSSNM);
            popsrcF_LOSSCD.SetText(F_LOSSCD);
            // 3. 그리드 세팅 FROM jsonDataDetail
            //   3.1 row 갯수만큼 루프하며 tr생성하여 tbody에 append
            //   3.2 tr 생성시 tr에 id 배정 및 from, to 시간 분 select태그에 id 배정
            for (var i = 0; i < jsondatadetail.length; i++) {
                trdata = jsondatadetail[i];
                trhtml += getHtmlPopGridTr(i, trdata);
            }
            $('#_cTabPop>tbody').html(trhtml);
            // 4. 유실유형 시간 상세내역 팝업 오픈
            popLOSS.Show();
        }

        // 팝업 그리드 Tr 문자열 반환
        function getHtmlPopGridTr(rIdx, trdata) {
            trdata = trdata || {};
            var i = rIdx;
            var trhtml = '';
            var fHH = trdata.F_FROMDT ? trdata.F_FROMDT.substr(11, 2) : '01';
            var fMM = trdata.F_FROMDT ? trdata.F_FROMDT.substr(14, 2) : '00';
            var tHH = trdata.F_TODT ? trdata.F_TODT.substr(11, 2) : '01';
            var tMM = trdata.F_TODT ? trdata.F_TODT.substr(14, 2) : '00';
            var trId = '__TR_' + i;
            var fHHId = '__FHH_' + i;
            var fMMId = '__FMM_' + i;
            var tHHId = '__THH_' + i;
            var tMMId = '__TMM_' + i;

            trhtml += "<tr id='" + trId + "' class='_cTrD'>";
            trhtml += "<td class='_cTdD wNo _lineblue _Click' onclick='poptdDel_Click(" + i + ");'>삭제</td>";
            trhtml += "<td class='_cTdD wNo'>" + String(i + 1) + "</td>";
            trhtml += "<td class='_cTdD wF_LOSSFROM'>" + getHtmlHourCombo(fHHId, fHH) + "시 " + getHtmlMinuteCombo(fMMId, fMM) + "분 </td>";
            trhtml += "<td class='_cTdD wF_LOSSTO'>" + getHtmlHourCombo(tHHId, tHH) + "시 " + getHtmlMinuteCombo(tMMId, tMM) + "분 </td>";
            trhtml += "</tr>";
            return trhtml;
        }

        // 시간 콤보박스 html 반환
        function getHtmlHourCombo(id, HH) {
            var html = '';
            var v = '';
            html = "<select id='" + id + "' class='_tempHH'>";
            for (var i = 1; i < 25; i++) {
                v = ("0" + String(i)).substr(-2);
                html += "<option value='" + v + "'" + ( HH == v ? " selected='selected'" : "" ) + ">" + v + "</option>";
            }
            html += "</select>";
            return html;
        }

        // 분 콤보박스 html 반환
        function getHtmlMinuteCombo(id, MM) {
            var html = '';
            var v = '';
            html = "<select id='" + id + "' class='_tempMM'>";
            for (var i = 0; i < 6; i++) {
                v = ("0" + String(i * 10)).substr(-2);
                html += "<option value='" + v + "'" + (MM == v ? " selected='selected'" : "") + ">" + v + "</option>";
            }
            html += "</select>";
            return html;
        }

        // 팝업 내 그리드 데이터 수집
        function getDataPopGrid() {
            var data = [];
            var trdata = {};
            var ymd = Trim(srcF_PLANYMD.GetText());
            var ymda = ymd.split(/-/gi);
            var F_LOSSCD = Trim(popsrcF_LOSSCD.GetText());
            var F_FROMDT, F_TODT;            
            var fh, fm, th, tm;
            var F_LOSSHOUR = 0;
            var trs = $('#_cTabPop>tbody>tr');
            for (var i = 0; i < trs.length; i++) {
                fh = $(trs[i]).find('select._tempHH:first option:selected').val();
                fm = $(trs[i]).find('select._tempMM:first option:selected').val();
                th = $(trs[i]).find('select._tempHH:last option:selected').val();
                tm = $(trs[i]).find('select._tempMM:last option:selected').val();
                
                F_FROMDT = new Date(ymda[0], ymda[1] * 1 - 1, ymda[2], fh * 1, fm * 1, 0);
                F_TODT = new Date(ymda[0], ymda[1] * 1 - 1, ymda[2], th * 1, tm * 1, 0);

                F_LOSSHOUR = Math.round((F_TODT - F_FROMDT) / 3600) / 1000;

                F_FROMDT = ymd + ' ' + fh + ':' + fm + ':00'
                F_TODT = ymd + ' ' + th + ':' + tm + ':00'
                trdata = {
                    'F_LOSSCD': F_LOSSCD,
                    'F_FROMDT': F_FROMDT,
                    'F_TODT': F_TODT,
                    'F_LOSSHOUR': String(F_LOSSHOUR)
                };
                data.push(trdata);
            }
            return data;
        }

        // 팝업 [확인] 클릭
        function popLOSS_Save_Click(btn) {
            // 경고 문구 및 올바르지 않은 시간 데이터 행 무시 처리
            // 팝업 그리드 내 데이터 수집하여 jsonDataDetail 데이터 갱신
            // 시간 집계하여 메인화면 그리드내 유실유형 시간 갱신
            //if (!confirm('정확하지 않은 시간 행은 제외처리됩니다.')) {
            //    return false;
            //}
            var data = getDataPopGrid();
            var F_LOSSCD = data[0].F_LOSSCD;
            var F_LOSSHOUR = 0;
            for (var i = 0; i < data.length; i++) {
                F_LOSSHOUR += parseFloat(data[i].F_LOSSHOUR);
            }

            var jsondata = {};
            var trs = $('#_cTab2>tbody>tr');
            var rIdx = 0;
            for (var i = 0; i < trs.length; i++) {
                rIdx = parseInt($(trs[i]).attr('id').split('_').pop(), 10);
                jsondata = JSON.parse($('#_JSONDATA2_' + rIdx).val() || '{}');
                if (jsondata.F_LOSSCD == F_LOSSCD) {
                    $('#_JSONDATADETAIL_' + rIdx).val(JSON.stringify(data));
                    $(trs[i]).find('.wF_LOSSHOUR').text(String(F_LOSSHOUR));
                    break;
                }
            }

            popLOSS.Hide();
        }

        // 팝업 [추가] 클릭
        function popLOSS_Add_Click(btn) {
            // 팝업 그리드 행 추가
            var tr = $('#_cTabPop>tbody>tr:last');
            var idx = (tr.attr('id') ? parseInt(tr.attr('id').split('_').pop(), 10) : -1) + 1;
            var html = getHtmlPopGridTr(idx);
            $('#_cTabPop>tbody').append(html);
            setTimeout(function () {
                $('#popdivGrid').animate({ scrollTop: $('#_cTabPop>tbody>tr:last').position().top }, 100);
            }, 10);            
        }

        // 행 삭제 클릭
        function poptdDel_Click(idx) {
            var id = '__TR_' + String(idx);
            $('#' + id).remove();
        }

        function popLOSS_OnShown(s, e) {
        }

        function IsNumber(evt) {
            evt = evt || window.event;
            var cc = evt.which || evt.keyCode;
            if (cc > 31 && (cc < 48 || cc > 57)) return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divLeft" style="width:39%;float:left;">
            <div class="blockTitle">
                <span>[생산실적]</span>
            </div>
            <div id="divLeftTop" style="width:100%;">
                <table class="InputTable" style="margin-bottom: 5px;width:50%;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 23%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">작업지시일</td>
                        <td class="tdContent">
                            <dx:ASPxDateEdit runat="server" ID="schF_PLANYMD" ClientInstanceName="schF_PLANYMD" UseMaskBehavior="true" 
                                EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                Width="100%" AllowNull="true" >
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLeftBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_COMPCD;F_FACTCD;F_WORKNO" EnableViewState="false" EnableRowsCache="false"
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
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="40px"/>
                        <dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_WORKNO" Caption="작업지시번호" Width="20%" />
                        <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="20%" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="생산품번" Width="20%" />
                        <dx:GridViewDataSpinEditColumn FieldName="F_PLANCOUNT" Caption="지시수량" Width="20%" PropertiesSpinEdit-DisplayFormatString="{0:#,0}" />
                        <dx:GridViewDataSpinEditColumn FieldName="F_PRODCOUNT" Caption="생산수량" Width="20%" PropertiesSpinEdit-DisplayFormatString="{0:#,0}" />
                        <%--<dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="상태" Width="80px" />--%>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
        <div id="divSpace" style="width:1%;float:left;">&nbsp;</div>
        <div id="divRight" style="width:60%;float:left;">
            <div class="blockTitle">
                <span>[작업지시/생산실적 정보]</span>
            </div>
            <div id="divRightTop" style="margin-bottom:10px;">
                <table class="InputTable" style="margin-bottom: 5px;width:100%;">
                    <colgroup>
                        <col style="width: 20%" />
                        <col style="width: 30%" />
                        <col style="width: 20%" />
                        <col style="width: 30%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">작업지시번호</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_WORKNO" ClientInstanceName="srcF_WORKNO" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수 항목입니다.">
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                                <dx:ASPxTextBox runat="server" ID="srcF_PLANYMD" ClientInstanceName="srcF_PLANYMD" ClientEnabled="false" Width="100%" ClientVisible="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">지시수량</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_PLANCOUNT" ClientInstanceName="srcF_PLANCOUNT" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">설비명</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MACHNM" ClientInstanceName="srcF_MACHNM" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">생산수량</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_PRODCOUNT" ClientInstanceName="srcF_PRODCOUNT" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수 항목입니다.">
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_PRODCOUNT" ClientInstanceName="srcF_PRODCOUNT" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="1" MaxValue="10000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" DisplayFormatString="{0:#,0}">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">생산품번</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">불량수량</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ERRCOUNT" ClientInstanceName="srcF_ERRCOUNT" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">보온로</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MELTNM" ClientInstanceName="srcF_MELTNM" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">유실시간</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_LOSSHOUR" ClientInstanceName="srcF_LOSSHOUR" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divRightBody1" style="float:left;width:49%;">
                <div class="blockTitle">
                    <span>[불량유형별 수량]</span>
                </div>
                <div id="divGrid1" style="width:100%;">
                </div>
            </div>
            <div style="float:left;width:1%;">&nbsp;</div>
            <div id="divRightBody2" style="float:left;width:50%;">
                <div class="blockTitle">
                    <span>[유실유형별 시간]</span>
                </div>
                <div id="divGrid2" style="width:100%;">
                </div>
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
        <dx:ASPxTextBox runat="server" ID="hidGridData1" ClientInstanceName="hidGridData1" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="hidGridData2" ClientInstanceName="hidGridData2" ClientVisible="false">
        </dx:ASPxTextBox>
        <%--<dx:ASPxButton ID="btnResourceTemp" runat="server" Image-IconID="save_save_32x32" ClientVisible="false" ></dx:ASPxButton>--%>
        <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>
    </div>
    <div class="divPopup">
        <dx:ASPxPopupControl ID="popLOSS" ClientInstanceName="popLOSS" runat="server" 
            CloseAction="OuterMouseClick" 
            Modal="true" 
            AutoUpdatePosition="true" 
            HeaderText="유실유형별 유실시간" 
            ShowHeader="false"
            Height="250" Width="500" 
            ContentStyle-Paddings-PaddingLeft="0"
            ContentStyle-Paddings-PaddingRight="0"
            PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
            ClientSideEvents-Shown="popLOSS_OnShown">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <header 
                        class="header bg-gradient b-b b-light content-navi container-non-responsive" style="min-width:500px;color:#788288;"
                        >
                        <p><i class="fa fa-map-marker"></i> <span class="font-bold">유실유형별 시간(From ~ To)</span></p>
                        <div style="float:right;">
                            <ul class="nav">
                                <li class="topbutton" style="padding-top: 9px;display:inline-block;">
                                    <button id="popLOSS_btnAdd" class="btn btn-sm btn-primary" 
                                        onclick="popLOSS_Add_Click(this); return false;" >
                                        <i class="fa fa-pencil"></i>
                                        <span class="text">추가</span>
                                    </button>
                                </li>
                                <li class="topbutton" style="padding-top: 9px; display:inline-block;">
                                    <button id="popLOSS_btnSave" class="btn btn-sm btn-success" 
                                        onclick="popLOSS_Save_Click(this); return false;" >
                                        <i class="fa fa-save"></i>
                                        <span class="text">확인</span>
                                    </button>
                                </li>
                                <li class="topbutton" style="padding-top: 9px; display:inline-block;">
                                    <button id="popLOSS_btnCancel" class="btn btn-sm btn-warning" 
                                        onclick="popLOSS.Hide(); return false;" >
                                        <i class="fa fa-undo"></i>
                                        <span class="text">취소</span>
                                    </button>
                                </li>
                            </ul>
                        </div>
                    </header>
                    <div class="container" style="background-color: #f2f4f8;height:180px;">
                        <table class="InputTable" style="margin-bottom: 5px;margin-top:10px; width: 100%;">
                            <colgroup>
                                <col style="width: 20%" />
                                <col style="width: 80%" />
                            </colgroup>
                            <tr>
                                <td class="tdTitle">유실유형</td>
                                <td class="tdContent">
                                    <div style="width:100%;float:left;">
                                        <dx:ASPxTextBox runat="server" ID="popsrcF_LOSSNM" ClientInstanceName="popsrcF_LOSSNM" ClientEnabled="false" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxTextBox runat="server" ID="popsrcF_LOSSCD" ClientInstanceName="popsrcF_LOSSCD" ClientEnabled="false" Width="100%" ClientVisible="false">
                                            <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div id="popdivGrid" style="height:127px;overflow-y:scroll;">
                            <table id="_cTabPop" class="_cTab" border="0" style="width:100%;">
                                <thead>
                                    <tr class="_cTrH">
                                        <th class="_cTdH wNo">&nbsp;</th>
                                        <th class="_cTdH wNo">No</th>
                                        <th class="_cTdH wF_LOSSFROM">from</th>
                                        <th class="_cTdH wF_LOSSTO">to</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
</asp:Content>