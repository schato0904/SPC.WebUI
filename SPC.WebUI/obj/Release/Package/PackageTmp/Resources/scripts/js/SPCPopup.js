//var rootURL = '/SPC.WebUI/';
var rootURL = '/' + window.location.href.replace('http://', '').split('/')[1] + '/';
var pPage;

// 문제이상제기 팝업창
function QWK101POP(TITLE, REVNO) {
    pPage = rootURL + 'Pages/Common/Popup/QWK101_TBLPOP.aspx' +
        '?TITLE=' + TITLE +
        '&CRUD=' +
        '&FILENO=' + ASPxClientControl.Cast('txtpublicno').GetText();
    fn_OnPopupOpen(pPage, '1200', '600');
}

// 문제이상대책 팝업창
function QWK103POP(TITLE, REVNO) {
    pPage = rootURL + 'Pages/Common/Popup/QWK103_TBLPOP.aspx' +
        '?TITLE=' + TITLE +
        '&CRUD=' +
        '&FILENO=' + ASPxClientControl.Cast('txtpublicno').GetText();
    fn_OnPopupOpen(pPage, '1200', '600');
}

// 검사성적서 관리
function fn_AttachFileOpen(TITLE, GBN, MULTI) {
    // GBN : C - 작업표준서, E - 제품이미지, F - 품질이상제기, G - T/O결과등록 H - 품질이상대책 ,I - 게시판 공지 및 요청사항, J - 게시판 답글
    // MULTI : T - 멀티업로드가능, F - 멀티업로드불가능
    pPage = rootURL + 'API/Common/Popup/Upload.aspx' +
        '?TITLE=' + TITLE +
        '&CRUD=D' +
        '&GBN=' + GBN +
        '&MULTI=' + MULTI +
        '&FILENO=' + ASPxClientControl.Cast('txtIMAGESEQ').GetText();
    fn_OnPopupOpen(pPage, '800', '350');
}

// 검사성적서 관리
function fn_AttachFileOpenParam(TITLE, GBN, MULTI, COMPCD) {
    // GBN : C - 작업표준서, E - 제품이미지, F - 품질이상제기, G - T/O결과등록 H - 품질이상대책 
    // MULTI : T - 멀티업로드가능, F - 멀티업로드불가능
    // COMPCD : 저장대상 업체코드
    pPage = rootURL + 'API/Common/Popup/Upload.aspx' +
        '?TITLE=' + TITLE +
        '&CRUD=D' +
        '&GBN=' + GBN +
        '&MULTI=' + MULTI +
        '&FILENO=' + ASPxClientControl.Cast('txtIMAGESEQ').GetText() +
        '&COMPCD=' + COMPCD;
    fn_OnPopupOpen(pPage, '800', '350');
}

// 검사성적서 관리
function fn_AttachFileOpenReadOnly(TITLE, GBN, MULTI, CTRL) {
    // GBN : C - 작업표준서, E - 제품이미지, F - 품질이상제기, G - T/O결과등록 H - 품질이상대책  삭제버튼, 업로드 없는 파일첨부화면  작성자 : 박병수
    // MULTI : T - 멀티업로드가능, F - 멀티업로드불가능
    // CTRL : 컨트럴명
    CTRL = (CTRL == null || CTRL == '') ? txtIMAGESEQ : CTRL;
    pPage = rootURL + 'API/Common/Popup/Upload2.aspx' +
        '?TITLE=' + TITLE +
        '&CRUD=' +
        '&GBN=' + GBN +
        '&MULTI=' + MULTI +
        '&FILENO=' + ASPxClientControl.Cast(CTRL).GetText();
    fn_OnPopupOpen(pPage, '800', '350');
}

// 검사성적서 파일 업로드 완료
// 2017.02.07 ..jnlee txtIMAGESEQ_SetValue가 있을 경우 txtIMAGESEQ_SetValue를 호출
function fn_UploadedComplete(val) {
    if (typeof txtIMAGESEQ_SetValue === 'function') {
        txtIMAGESEQ_SetValue(val);
    } else {
    ASPxClientControl.Cast('txtIMAGESEQ').SetText(val);
}
}

// 검사성적서 파일 업로드 완료
function fn_UploadedComplete2(val, NoCtrlId, valCnt, CntCtrlId) {

    if (Trim(NoCtrlId) == "") NoCtrlId = "txtIMAGESEQ";

    if (Trim(NoCtrlId) != "" && typeof window[NoCtrlId] != 'undefined' && typeof window[NoCtrlId].SetATTFILENO == 'function') {
        window[NoCtrlId].SetATTFILENO(val);
    } else {

        if (Trim(NoCtrlId) != "" && ASPxClientUtils.IsExists(ASPxClientControl.Cast(NoCtrlId))) {
            ASPxClientControl.Cast(NoCtrlId).SetText(val);
        }

        if (Trim(CntCtrlId) != "" && ASPxClientUtils.IsExists(ASPxClientControl.Cast(CntCtrlId))) {
            ASPxClientControl.Cast(CntCtrlId).SetText(valCnt);
        }
    }
}

// 품목검색창 오픈
function fn_OnPopupItemSearch(TYPE, OPTION) {
    
    if (OPTION == "undefined" || OPTION == null) {
        OPTION = "";
    }
    var _BANCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidBANCD')) ? '' : ASPxClientControl.Cast('hidBANCD').GetText();
    var _ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtITEMCD'))
                    ? TYPE == 'S' ? !ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtITEMCDS')) ? '' : ASPxClientControl.Cast('txtITEMCDS').GetText() : ''
                    : ASPxClientControl.Cast('txtITEMCD').GetText();
    pPage = rootURL + 'Pages/Common/Popup/ITEMPOP.aspx' +
        '?TITLE=품목조회' +
        '&CRUD=R' +
        '&OPTION=' + OPTION +
        '&TYPE=' + TYPE +
        '&BANCD=' + _BANCD +
        '&ITEMCD=' + encodeURIComponent(_ITEMCD);
    fn_OnPopupOpen(pPage, '800', '500');
}

// 품목검색창 오픈(검사기준등록)
function fn_OnPopupItemSearchINS() {
    pPage = rootURL + 'Pages/Common/Popup/ITEMPOP.aspx' +
        '?TITLE=품목조회' +
        '&CRUD=R' +
        '&TYPE=FORM' +
        '&BANCD=' +
        '&ITEMCD=' +
        '&MACHGUBUN=';
    fn_OnPopupOpen(pPage, '800', '500');
}

// 검사항목 검색 팝업창 열기
function fn_OnPopupMeainspSearch() {
    pPage = rootURL + 'Pages/Common/Popup/MEAINSPPOP.aspx' +
        '?TITLE=검사항목조회' +
        '&CRUD=R' +
        '&MEAINSPCD=' + ASPxClientControl.Cast('txtMEAINSPCD').GetText() +
        '&INSPDETAIL=' + ASPxClientControl.Cast('txtINSPDETAIL').GetText();
    fn_OnPopupOpen(pPage, '800', '500');
}

// 검사항목 검색 팝업창 열기
function fn_OnPopupMeainspSearchBatch(MEAINSPCD, INSPDETAIL) {
    pPage = rootURL + 'Pages/Common/Popup/MEAINSPPOP.aspx' +
        '?TITLE=검사항목조회' +
        '&CRUD=R' +
        '&MEAINSPCD=' + MEAINSPCD +
        '&INSPDETAIL=' + INSPDETAIL;
    fn_OnPopupOpen(pPage, '800', '500');
}

// 공정 검색 팝업창 열기
function fn_OnPopupWorkSearch(TYPE) {
    var _ITEMCD = '';
    if (TYPE == 'S') {
        _ITEMCD = ASPxClientControl.Cast('txtITEMCDS').GetText();
    } else if (TYPE == 'T') {
        _ITEMCD = '';//ASPxClientControl.Cast('txtITEMCDT').GetText();
    } else {
        _ITEMCD = ASPxClientControl.Cast('txtITEMCD').GetText();
    }

    var _WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtWORKCD'))
                    ? TYPE == 'S' ? !ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtWORKCDS')) ? '' : ASPxClientControl.Cast('txtWORKCDS').GetText() : ''
                    : ASPxClientControl.Cast('txtWORKCD').GetText();
    
    pPage = rootURL + 'Pages/Common/Popup/WORKPOP.aspx' +
        '?TITLE=공정조회' +
        '&CRUD=R' +
        '&TYPE=' + TYPE +
        '&ITEMCD=' + encodeURIComponent(_ITEMCD) +
        '&WORKCD=' + _WORKCD;
    fn_OnPopupOpen(pPage, '800', '500');
}
// 공정 검색 팝업창 열기
function fn_OnPopupWorkSearchM(TYPE) {
    var _ITEMCD = '';
        _ITEMCD = ASPxClientControl.Cast('txtITEMCD').GetText();
    var _WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtWORKCD'))
                    ? TYPE == 'S' ? !ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtWORKCDS')) ? '' : ASPxClientControl.Cast('txtWORKCDS').GetText() : ''
                    : ASPxClientControl.Cast('txtWORKCD').GetText();

    pPage = rootURL + 'Pages/Common/Popup/WORKPOP_MULTI.aspx' +
        '?TITLE=공정조회(멀티)' +
        '&CRUD=R' +
        '&TYPE=' + TYPE +
        '&ITEMCD=' + encodeURIComponent(_ITEMCD) +
        '&WORKCD=' + _WORKCD;
    fn_OnPopupOpen(pPage, '800', '500');
}

// 공정 검색 팝업창 열기
function fn_OnPopupWorkSearch_WKSD() {
    pPage = rootURL + 'Pages/Common/Popup/WORKPOP.aspx' +
        '?TITLE=공정조회&CRUD=R&TYPE=FORM&ITEMCD=&WORKCD=';
    fn_OnPopupOpen(pPage, '800', '500');
}

// 공정 검색 팝업창 열기
function fn_OnPopupWorkSearchBatch(ITEMCD, WORKCD) {
    pPage = rootURL + 'Pages/Common/Popup/WORKPOP.aspx' +
        '?TITLE=공정조회' +
        '&CRUD=R' +
        '&TYPE=INS' +
        '&ITEMCD=' + encodeURIComponent(ITEMCD) +
        '&WORKCD=' + WORKCD;
    fn_OnPopupOpen(pPage, '800', '500');
}

// 검사기준복사 팝업창 열기
function fn_OnPopupQCD34Copy() {
    pPage = rootURL + 'Pages/Common/Popup/COPYQCD34POP.aspx' +
        '?TITLE=검사기준복사' +
        '&CRUD=S';
    fn_OnPopupOpen(pPage, '800', '500');
}

// 검사기준다중복사 팝업창 열기
function fn_OnPopupQCD34Copy2(ITEMCD) {
    pPage = rootURL + 'Pages/Common/Popup/COPYQCD34POP2.aspx' +
        '?TITLE=검사기준선택복사' +
        '&CRUD=S' +
        '&txtITEMCDS=' + ITEMCD;
    fn_OnPopupOpen(pPage, '800', '500');
}

// 품목별 라인 팝업창 열기
function fn_OnPopupQCD011Add(key) {
    pPage = rootURL + 'Pages/BSIF/Popup/BSIF0205POP.aspx' +
        '?TITLE=품목별라인등록' +
        '&CRUD=CRUDWS' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '800', '400');
}

// 품목별 도면 팝업창 열기
function fn_OnPopupQCD014Add(key) {
    pPage = rootURL + 'Pages/BSIF/Popup/BSIF0205IMGPOP.aspx' +
        '?TITLE=품목별 도면 및 사진등록' +
        '&CRUD=RDW' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '800', '500');
}

// PC별 라인 팝업창 열기
function fn_OnPopupPCLINEAdd(key) {
    pPage = rootURL + 'Pages/BSIF/Popup/BSIF0501POP.aspx' +
        '?TITLE=PC별라인등록' +
        '&CRUD=CRUDWS' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '800', '400');
}

// PC별 공정 팝업창 열기
function fn_OnPopupWGROUPAdd(key) {
    pPage = rootURL + 'Pages/BSIF/Popup/BSIF0501_WGROUPPOP.aspx' +
        '?TITLE=공정그룹별 공정등록' +
        '&CRUD=CRDWS' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '800', '400');
}

// 출하 팝업창 열기
function fn_OnPopupShipment() {
    pPage = rootURL + 'Pages/SPMT/Popup/SPMT0101POP.aspx' +
        '?TITLE=출하 데이타 작성' +
        '&CRUD=S'
    fn_OnPopupOpen(pPage, '800', '200');
}

// 출하성적서 팝업창 열기
function fn_OnPopupInspectionReport(key) {
    pPage = rootURL + 'Pages/SPMT/Popup/SPMT0102REPORT.aspx' +
        '?TITLE=검사성적서' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '830', '0');
}

// 검사성적서 생성 팝업창 열기
function fn_OnPopupInspection(ITEMCD,LOTNO) {
    pPage = rootURL + 'Pages/INSP/Popup/INSP0101POP.aspx' +
        '?TITLE=검사성적서 생성' +
        '&CRUD=S' +
        '&ITEMCD=' + ITEMCD +
        '&LOTNO=' + LOTNO;
    fn_OnPopupOpen(pPage, '800', '600');
}
// 검사성적서 생성 팝업창 열기
function fn_OnPopupInspection_CHUNIL(ITEMCD, LOTNO, WORKDATE) {
    pPage = rootURL + 'Pages/INSP/Popup/INSP0101POP_CHUNIL.aspx' +
        '?TITLE=검사성적서 생성' +
        '&CRUD=S' +
        '&ITEMCD=' + ITEMCD +
        '&LOTNO=' + LOTNO +
        '&WORKDATE=' + WORKDATE;
    fn_OnPopupOpen(pPage, '800', '400');
}

// 검사성적서 생성 팝업창 열기
function fn_OnPopupInspectionReportView(pGROUPCD, pCUSTOMCD, pREPORTNM) {
    pPage = rootURL + 'Pages/INSP/Popup/INSP0201REPORT.aspx' +
        '?TITLE=검사성적서' +
        '&pGROUPCD=' + pGROUPCD +
        '&pCUSTOMCD=' + pCUSTOMCD +
        '&pREPORTNM=' + pREPORTNM;
    fn_OnPopupOpen(pPage, '820', '0');
}

// 검사성적서 생성 팝업창 열기
function fn_OnPopupInspectionReportView_CHUNIL(pGROUPCD, pCUSTOMCD, pREPORTNM) {
    pPage = rootURL + 'Pages/INSP/Popup/INSP0201REPORT_CHUNIL.aspx' +
        '?TITLE=검사성적서' +
        '&pGROUPCD=' + pGROUPCD +
        '&pCUSTOMCD=' + pCUSTOMCD +
        '&pREPORTNM=' + pREPORTNM;
    fn_OnPopupOpen(pPage, '820', '0');
}

// 라인모니터링 불량팝업 열기
function fn_OnPopupLineMonitoring(key) {
    pPage = rootURL + 'Pages/ADTR/Popup/ADTR0102POP.aspx' +
        '?TITLE=불량현황상세보기' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '0', '500');
}

// 라인모니터링 불량팝업 열기
function fn_OnPopupADTR0402PHE(key) {
    pPage = rootURL + 'Pages/ADTR/Popup/ADTR0402_PHEPOP.aspx' +
        '?TITLE=수분측정Data보기' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '500', '700');
}

// 실시간 라인모니터링 불량팝업 열기
function fn_OnPopupMonitoring(key) {
    pPage = rootURL + 'Pages/ADTR/Popup/ADTR0101POP.aspx' +
        '?TITLE=불량현황상세보기' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '0', '500');
}

// 실시간 라인모니터링 불량팝업 열기(공정)
function fn_OnPopupMonitoringWork(key) {
    pPage = rootURL + 'Pages/ADTR/Popup/ADTR0101WORKPOP.aspx' +
        '?TITLE=불량현황상세보기' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '0', '500');
}

// 실시간 라인모니터링 불량팝업 열기(공정그룹)
function fn_OnPopupMonitoringWGroup(key) {
    pPage = rootURL + 'Pages/ADTR/Popup/ADTR0101WgroupPOP.aspx' +
        '?TITLE=불량현황상세보기' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '0', '520');
}

// 불량이상/조치코드 팝업
function fn_OnPopupCauseSearch(idx, code, gbn, col) {
    
    pPage = rootURL + 'Pages/Common/Popup/CAUSEPOP.aspx' +
        '?TITLE=불량원인/조치조회' +
        '&CRUD=R' +
        '&IDX=' + idx +
        '&CODE=' + code +
        '&GBN=' + gbn;
    fn_OnPopupOpen(pPage, '800', '500');
}

// 측정목록 팝업
function fn_OnPopupMeasurement(key) {

    pPage = rootURL + 'Pages/Common/Popup/MEASUREMENTPOP.aspx' +
        '?TITLE=측정 DATA 목록' +
        '&CRUD=E' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '800', '0');
}

// 측정목록 팝업
function fn_OnPopupTotalMeasurement(key) {

    pPage = rootURL + 'Pages/Common/Popup/TOTALMEASUREMENTPOP.aspx' +
        '?TITLE=전수 측정 DATA 목록' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '800', '0');
}

// 전수 X-Rs 팝업
function fn_OnPopupXRs(key) {
    pPage = rootURL + 'Pages/TISP/Popup/TISPXRSPOP.aspx' +
        '?TITLE=전수 X-Rs DATA 목록' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '800', '0');
}

// 품목별 WORST 팝업
function fn_OnPopup_WorstItem(key) {

    pPage = rootURL + 'Pages/Common/Popup/ITEMWORSTPOP.aspx' +
        '?TITLE=품목별 Worst' +
        '&CRUD=E' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 항목별 WORST 팝업
function fn_OnPopup_WorstSerial(key) {

    pPage = rootURL + 'Pages/Common/Popup/SERIALWORSTPOP.aspx' +
        '?TITLE=품목별 Worst' +
        '&CRUD=E' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 공정능력 WORST 팝업
function fn_OnPopup_WorstWork(key) {

    pPage = rootURL + 'Pages/Common/Popup/WORKWORSTPOP.aspx' +
        '?TITLE=품목별 Worst' +
        '&CRUD=E' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 라인모니터링 불량팝업 열기(전수)
function fn_OnPopupLineMonitoringTISP(key) {
    pPage = rootURL + 'Pages/TISP/Popup/TISP0103POP.aspx' +
        '?TITLE=불량현황상세보기' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '0', '600');
}

// 일반폼을 팝업으로 열기
function fn_OnPopup_GeneralForm(oParam, oSetParam, _width, _height) {
    var oParams = oParam.split('|');
    var F_MODULE1 = oParams[0];
    var F_MODULE2 = oParams[1];
    var F_LINK = oParams[2];
    var F_PGMID = oParams[3];
    var F_PGMNM = oParams[4];
    pPage = '../' + F_LINK + '/' + F_PGMID + '.aspx?pParam=' + oParam + '&oSetParam=' + oSetParam + '&bPopup=true';
    fn_OnPopupOpen(pPage, _width == null ? '0' : _width, _height == null ? '0' : _height);
}

// 라인모니터링 불량팝업 열기
function fn_OnPopupEQMonitoring(key) {
    pPage = rootURL + 'Pages/TISP/Popup/TISP0101POP.aspx' +
        '?TITLE=불량현황상세보기' +
        '&CRUD=' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '1200', '500');
}

// 검사항목선택 팝업
function fn_OnPopupANLS0401(ITEMCD,KEY) {
    pPage = rootURL + 'Pages/ANLS/Popup/ANLS0401POP.aspx' +
        '?TITLE=검사항목선택' +
        '&CRUD=' +
        '&ITEMCD=' + encodeURIComponent(ITEMCD)+
        '&KEYFIELDS=' + KEY;
    fn_OnPopupOpen(pPage, '0', '500');
}

// 관리한계이력조회 오픈
function fn_OnPopupCONTROL(ITEMCD, ITEMNM, INSPCD, INSPNM) {
    pPage = rootURL + 'Pages/Common/Popup/CONTROLPOP.aspx' +
        '?TITLE=관리한계이력조회' +
        '&CRUD=' +
        '&ITEMCD=' + encodeURIComponent(ITEMCD) +
        '&ITEMNM=' + encodeURIComponent(ITEMNM) +
        '&INSPCD=' + INSPCD +
        '&INSPNM=' + encodeURIComponent(INSPNM);
    fn_OnPopupOpen(pPage, '0', '0');
}

// 품질이상제기 등록
function fn_OnPopupIPCM0101(COMPCD, FACTCD, ITEMCD, ITEMNM, WORKCD, WORKNM, INSPCD, INSPNM, STDT, EDDT) {
    pPage = '../IPCM/Popup/IPCM0101POP.aspx' +
        '?TITLE=품질이상제기 등록' +
        '&CRUD=S' +
        '&P_COMPCD=' + COMPCD +
        '&P_FACTCD=' + FACTCD +
        '&P_ITEMCD=' + encodeURIComponent(ITEMCD) +
        '&P_ITEMNM=' + encodeURIComponent(ITEMNM) +
        '&P_WORKCD=' + WORKCD +
        '&P_WORKNM=' + encodeURIComponent(WORKNM) +
        '&P_INSPCD=' + INSPCD +
        '&P_INSPNM=' + encodeURIComponent(INSPNM) +
        '&P_STDT=' + STDT +
        '&P_EDDT=' + EDDT;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 품질이상대책등록
function fn_OnPopupIPCM0102(KEY) {
    pPage = 'Popup/IPCM0102POP.aspx' +
        '?TITLE=품질이상대책등록(상세내용보기)' +
        '&CRUD=S' +
        '&KEYFIELDS=' + KEY
    fn_OnPopupOpen(pPage, '0', '0');
}

// 품질이상제기 및 개선대책 조회 및 선택
function fn_OnPopupIPCM0103(KEY) {
    pPage = 'Popup/IPCM0103POP.aspx' +
        '?TITLE=품질이상제기 완료처리(상세내용보기)' +
        '&CRUD=S' +
        '&KEYFIELDS=' + KEY;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 품질이상제기 및 개선대책 조회 및 선택
function fn_OnPopupIPCM0104(KEY) {
    pPage = 'Popup/IPCM0103POP.aspx' +
        '?TITLE=품질이상제기(상세내용보기)' +
        '&CRUD=' +
        '&KEYFIELDS=' + KEY;
    fn_OnPopupOpen(pPage, '0', '0');
}

// Form Submit For New Win
function fn_OnPopupByPass(sURL, sRSAString, sWinNM) {
    var theURL = sURL;
    var theWinNM = sWinNM == 'spc' ? '' : sWinNM;
    var theForms = window.open('', theWinNM);

    var form_url = theURL;
    theForms.document.writeln("<html>");
    theForms.document.writeln("<head>");
    theForms.document.writeln("<title>");
    theForms.document.writeln("By Pass");
    theForms.document.writeln("</title>");
    theForms.document.writeln("</head>");
    theForms.document.writeln("<body>");
    theForms.document.writeln("<form name=\"sendForm\" method=\"post\" action=\"" + form_url + "\">");
    theForms.document.writeln("<\INPUT TYPE=\"HIDDEN\" NAME=\"pUSIF\" VALUE=\"" + sRSAString + "\">");
    theForms.document.writeln("<\/form>");
    theForms.document.writeln("<\script>document.sendForm.submit();<\/script>");
    theForms.document.writeln("<\/body>");
    theForms.document.writeln("<\/html>");
}

// 주간 공정풂질 추이 팝업창 열기
function fn_OnPopupQulityReport(pYEAR, pWEEK) {
    pPage = rootURL + 'Pages/MNTR/Popup/MNTR0907REPORT.aspx' +
        '?TITLE='+encodeURIComponent('협력사 주간 공정품질 추이') +
        '&pYEAR=' + pYEAR +
        '&pWEEK=' + pWEEK;
    fn_OnPopupOpen(pPage, '830', '0');
}

// 초중종 검사 상세목록 팝업창 열기
function fn_OnPopupFITM0201(KEY) {
    pPage = 'Popup/FITM0201POP.aspx' +
        '?TITLE=초중종 검사 상세목록' +
        '&CRUD=' +
        '&KEYFIELDS=' + KEY
    fn_OnPopupOpen(pPage, '0', '0');
}

// 실시간측정 상세목록 팝업창 열기
function fn_OnPopupADTR0104(KEY) {
    pPage = rootURL + 'Pages/FITM/Popup/FITM0201POP.aspx' +
        '?TITLE=실시간 검사 상세목록' +
        '&CRUD=' +
        '&KEYFIELDS=' + KEY
    fn_OnPopupOpen(pPage, '0', '0');
}

// 품질종합현황 팝업창 열기
function fn_OnPopupANLS0101Report(key) {
    pPage = rootURL + 'Pages/ANLS/Popup/ANLS0101REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('품질종합현황')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// 품질종합현황 팝업창 열기
function fn_OnPopupANLS0101Report_OLD(key) {
    pPage = rootURL + 'Pages/ANLS/Popup/ANLS0101REPORT_OLD.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('품질종합현황')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// Xbar-R 관리도 팝업창 열기
function fn_OnPopupANLS0102Report(key) {
    pPage = rootURL + 'Pages/ANLS/Popup/ANLS0102REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('XBar-R관리도')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// Xbar-R 추이도 팝업창 열기
function fn_OnPopupANLS0103Report(key) {
    pPage = rootURL + 'Pages/ANLS/Popup/ANLS0103REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('XBar-R추이도')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// 공정능력평가 팝업창 열기
function fn_OnPopupANLS0201Report(key) {
    pPage = rootURL + 'Pages/ANLS/Popup/ANLS0201REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('공정능력평가')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// 히스토그램 팝업창 열기
function fn_OnPopupANLS0203Report(key) {
    pPage = rootURL + 'Pages/ANLS/Popup/ANLS0203REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('히스토그램')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// 비교차이분석 팝업창 열기
function fn_OnPopupANLS0204Report(key) {
    pPage = rootURL + 'Pages/ANLS/Popup/ANLS0204REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('비교차이분석')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// 품질종합현황 팝업창 열기(전수)
function fn_OnPopupTISP01POPReport(key) {
    pPage = rootURL + 'Pages/TISP/Popup/TISP01POPREPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('품질종합현황')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// Xbar-R 관리도 팝업창 열기(전수)
function fn_OnPopupTISP0104Report(key) {
    pPage = rootURL + 'Pages/TISP/Popup/TISP0104REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('X-Rs관리도')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// 공정능력평가 팝업창 열기(전수)
function fn_OnPopupTISP0105Report(key) {
    pPage = rootURL + 'Pages/TISP/Popup/TISP0105REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('공정능력평가')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// 이미지 등록 팝업창 열기
function fn_OnPopupBSIF0801(key) {
    pPage = rootURL + 'Pages/BSIF/Popup/BSIF0801FILEPOP.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('이미지 등록')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '250');
}

// 업무지시 등록/수정 팝업창 열기
function fn_OnPopupBSIF0901(key) {
    pPage = rootURL + 'Pages/BSIF/Popup/BSIF0901NEW.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('업무지시')) +
        '&CRUD=RS' +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '0', '0');
}

// 설비일상점검 - 부서, 사용자 선택 
function fn_OnPopupFDCKUSERPOP(POPUP_TITLE, KEY, callbackfn) {
    if (typeof callbackfn == 'undefined') callbackfn = '';
    pPage = rootURL + 'Pages/FDCK/Popup/FDCKUSERPOP.aspx' +
        '?TITLE=' + POPUP_TITLE +
        '&CRUD=' +
        '&KEYFIELDS=' + KEY +
        '&CALLBACKFN=' + callbackfn;
    fn_OnPopupOpen(pPage, '800', '500');
}

// 품질종합현황 팝업창 열기
function fn_OnPopupFITM0205Report(key) {
    pPage = rootURL + 'Pages/FITM/Popup/FITM0205REPORT.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('초중종CheckSheet')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '830', '0');
}

// 설비일상점검 검사항목 검색 팝업창 열기
function fn_OnPopupMachInspSearch() {
    pPage = rootURL + 'Pages/Common/Popup/MACHINSPPOP.aspx' +
        '?TITLE=설비일상점검 검사항목조회' +
        '&CRUD=R' +
        '&INSPCD=' + ASPxClientControl.Cast('txtINSPCD').GetText() +
        '&INSPNM=' + ASPxClientControl.Cast('txtINSPNM').GetText();
    fn_OnPopupOpen(pPage, '800', '500');
}

// 설비일상점검 검사항목 수정,삭제 사유 입력 팝업창 열기
function fn_OnPopupHistoryReason(t) {
    pPage = rootURL + 'Pages/DIOF/Popup/REASONPOP.aspx' +
        '?TITLE=설비일상점검 검사항목 수정/삭제 사유' +
        '&CRUD=S' +
        '&TYPE=' + t;
    fn_OnPopupOpen(pPage, '800', '200');
}

// 설비일상점검 이상발생 원인/조치등록
function fn_OnPopupNgReason(keys) {
    pPage = rootURL + 'Pages/DIOF/Popup/DIOF0401POP.aspx' +
        '?TITLE=설비일상점검 이상발생 원인/조치등록' +
        '&CRUD=S' +
        '&MEASYMD=' + ASPxClientControl.Cast('hidUCFROMDT').GetText() +
        '&MACHIDX=' + ASPxClientControl.Cast('srcF_MACHIDX').GetText() +
        '&MACHCD=' + ASPxClientControl.Cast('srcF_MACHCD').GetText() +
        '&MACHNM=' + ASPxClientControl.Cast('srcF_MACHNM').GetText() +
        '&KEYS=' + keys;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 설비일상점검 이상발생 상세보기
function fn_OnPopupNgReasonView(key) {
    pPage = rootURL + 'Pages/DIOF/Popup/DIOF0403POP.aspx' +
    '?TITLE=설비일상점검 이상발생 내용보기' +
    '&CRUD=' +
    '&KEY=' + key;
    fn_OnPopupOpen(pPage, '800', '430');
}

// 설비일상점검 점검내역 일자별 관리자확인
function fn_OnPopupDailyConfirm(_key, _text, _date) {
    pPage = rootURL + 'Pages/DIOF/Popup/DIOF0501POP.aspx' +
    '?TITLE=설비일상점검 점검내역 일자별 관리자확인' +
    '&CRUD=' +
    '&KEY=' + _key +
    '&CODE=' + _text.split(';')[0] +
    '&TEXT=' + encodeURIComponent(_text.split(';')[1]) +
    '&DATE=' + _date;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 설비일상점검 체크시트 인쇄 팝업창 열기
function fn_OnPopupCheckSheetReport(_month, _idx) {
    pPage = rootURL + 'Pages/DIOF/Popup/DIOF0501REPORT.aspx' +
        '?TITLE=설비일상점검표' +
        '&MONTH=' + _month +
        '&IDX=' + _idx;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 설비예비품등록 품목팝업
function fn_OnPopupSPItemSearch(TYPE, OPTION) {

    pPage = rootURL + 'Pages/SPCM/Popup/SPITEMPOP.aspx' +
        '?TITLE=설비예비품목조회' +
        '&CRUD=R' +
        '&ITEMCD=';
    fn_OnPopupOpen(pPage, '800', '500');
}

// 설비예비품목 등록팝업
function fn_OnPopupSPItemIns(ITCD, ITNM) {

    pPage = rootURL + 'Pages/SPCM/Popup/SPITEMINSPOP.aspx' +
        '?TITLE=설비예비품목등록' +
        '&CRUD=C' +
        '&ITCD=' + ITCD +
        '&ITNM=' + ITNM;
    fn_OnPopupOpen(pPage, '800', '200');
}

// 사용자검색창 오픈
function fn_OnPopupUserSearch(parentCallbackName, _USERID, TYPE) {
    pPage = rootURL + 'Pages/Common/Popup/USERPOP.aspx' +
        '?TITLE=사용자조회' +
        '&CRUD=R' +
        '&parentCallback=' + parentCallbackName +
        '&TYPE=' + TYPE +
        //'&BANCD=' + _BANCD +
        '&USERID=' + _USERID
    ;
    fn_OnPopupOpen(pPage, '800', '500');
}



// 공정불량관리 품목검색창 오픈
function fn_OnPopupItemSearch_WorkError(WORKCD) {
    pPage = rootURL + 'Pages/Common/Popup/ITEMPOP_WERD.aspx' +
        '?TITLE=품목조회' +
        '&CRUD=R' +
        '&OPTION=' +
        '&TYPE=UC1' +
        '&WORKCD=' + WORKCD +
        '&ITEMCD=';
    fn_OnPopupOpen(pPage, '800', '500');
}

// 공정 검색 팝업창 열기
function fn_OnPopupWorkSearchBatch_WorkError() {
    pPage = rootURL + 'Pages/Common/Popup/WORKPOP_WERD.aspx' +
        '?TITLE=공정조회' +
        '&CRUD=R' +
        '&TYPE=FORM1' +
        '&ITEMCD=' +
        '&WORKCD=';
    fn_OnPopupOpen(pPage, '800', '500');
}

// 검사성적서 기준항목관리 팝업차 열기
function fn_OnPopupINSP0301(code, type, name) {
    pPage = rootURL + 'Pages/INSP/Popup/INSP0301POP.aspx' +
        '?TITLE=검사성적서 항목관리' +
        '&CRUD=' +
        '&CODE=' + code +
        '&TYPE=' + type +
        '&NAME=' + encodeURIComponent(name);
    fn_OnPopupOpen(pPage, '0', '0');
}

// 검사성적서 생성팝업창 열기
function fn_OnPopupINSP0302(PopParam) {
    pPage = rootURL + 'Pages/INSP/Popup/INSP0302POP.aspx' +
        '?TITLE=검사성적서 생성' +
        '&CRUD=S' +
        '&PopParam=' + PopParam;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 검사성적서 샘플검사조회(중간검사-EF)팝업창 열기
function fn_OnPopupINSP0302SUB(PopParam) {
    pPage = rootURL + 'Pages/INSP/Popup/INSP0302SUBPOP.aspx' +
        '?TITLE=샘플검사 조회 및 선택' +
        '&CRUD=C' +
        '&PopParam=' + PopParam;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 검사성적서 팝업창 열기
function fn_OnPopupINSP0303(PopParam) {
    pPage = rootURL + 'Pages/INSP/Popup/INSP0303POP.aspx' +
        '?TITLE=검사성적서' +
        '&CRUD=SP' +
        '&PopParam=' + PopParam;
    fn_OnPopupOpen(pPage, '0', '0');
}

// 검사성적서 인쇄 팝업창 열기
function fn_OnPopupINSPREPORT(PopParam, t, isWide) {
    pPage = rootURL + 'Pages/INSP/Popup/INSPREPORTPOP.aspx' +
        '?TITLE=' + encodeURIComponent(t + ' 검사성적서') +
        '&CRUD=' +
        '&PopParam=' + PopParam;
    if (!isWide)
        fn_OnPopupOpen(pPage, '830', '0');
    else
        fn_OnPopupOpen(pPage, '0', '0');
}

function fn_OnPopupCATM1302(KEY) {
    pPage = 'Popup/CATM1302POP.aspx' +
        '?TITLE=부적합상세내용' +
        '&CRUD=' +
        '&KEYFIELDS=' + KEY
    fn_OnPopupOpen(pPage, '800', '600');
}

// 계측기 부속품 등록 팝업창 열기
function fn_OnPopupMEAS1001POPSearch(parentCallbackName, _F_MS01MID, _F_PART_INFO) {
    pPage = rootURL + 'Pages/MEAS/Popup/MEAS1001POP.aspx' +
        '?TITLE=' + encodeURIComponent('계측기부속품등록') +
        '&CRUD=ACD' +
        '&parentCallback=' + parentCallbackName +
        '&F_MS01MID=' + _F_MS01MID +
        '&F_PART_INFO=' + _F_PART_INFO;

    fn_OnPopupOpen(pPage, '800', '520');
}


// 계측기 관리번호 팝업
function fn_OnPopupMEAS3001POPSearch(parentCallbackName, KEY) {
    pPage = rootURL + 'Pages/MEAS/Popup/MEAS3001POP.aspx' +
        '?TITLE=' + encodeURIComponent('관리번호조회') +
        '&CRUD=R' +
        '&KEY=' + KEY +
        '&parentCallback=' + parentCallbackName;

    fn_OnPopupOpen(pPage, '800', '520');
}

// 계측기 > 검교정실적관리 > 의뢰번호 팝업
function fn_OnPopupMEAS4001POPSearch(parentCallbackName) {
    pPage = rootURL + 'Pages/MEAS/Popup/MEAS4001POP.aspx' +
        '?TITLE=' + encodeURIComponent('의뢰번호조회') +
        '&CRUD=R' +
        '&parentCallback=' + parentCallbackName;

    fn_OnPopupOpen(pPage, '950', '520');
}

// 파일업로드
function fn_AttachFileOpenParam2(TITLE, GBN, MULTI, NoCtrlId, CntCtrlId, COMPCD, parentCallback, noticeText) {
    // GBN : A - 출하검사일보등록, I - 게시판 공지 및 요청사항, J - 게시판 답글, K - 자료관리 
    // MULTI : T - 멀티업로드가능, F - 멀티업로드불가능, B - 정기검사
    // COMPCD : 저장대상 업체코드
    if (Trim(NoCtrlId) == "") NoCtrlId = "txtIMAGESEQ";
    if (Trim(CntCtrlId) == "") CntCtrlId = "srcF_ATTFILECNT";

    noticeText = (Trim(noticeText) == "") ? "" : noticeText;
    if (typeof parentCallback != 'function') parentCallback = function () { };

    pPage = rootURL + 'API/Common/Popup/Upload3.aspx' +
        '?TITLE=' + encodeURIComponent(TITLE) +
        '&NOTICE=' + encodeURIComponent(noticeText) +
        '&CRUD=D' +
        '&GBN=' + GBN +
        '&MULTI=' + MULTI +
        //'&FILENO=' + ASPxClientControl.Cast(NoCtrlId).GetText() +
        '&FILENO=' + Trim(window[NoCtrlId].GetValue()) +
        '&FILENOID=' + NoCtrlId +
        '&FILECNTID=' + CntCtrlId +
        '&COMPCD=' + COMPCD;

    fn_OnPopupOpen(pPage, '800', '350', parentCallback);
}

// 파일업로드 후 콜백처리
function fn_AttachFileOpenCallBack(TITLE, GBN, MULTI, NoCtrlId, CntCtrlId, COMPCD, parentCallbackName) {
    // GBN : A - 출하검사일보등록, I - 게시판 공지 및 요청사항, J - 게시판 답글, K - 자료관리 
    // MULTI : T - 멀티업로드가능, F - 멀티업로드불가능, B - 정기검사
    // COMPCD : 저장대상 업체코드
    if (Trim(NoCtrlId) == "") NoCtrlId = "txtIMAGESEQ";
    if (Trim(CntCtrlId) == "") CntCtrlId = "srcF_ATTFILECNT";

    var fileNoCtrl = ASPxClientControl.Cast(NoCtrlId);
    var fileNo = "";

    if (fileNoCtrl != undefined && fileNoCtrl != null) {
        fileNo = Trim(ASPxClientControl.Cast(NoCtrlId).GetText());
    }

    pPage = rootURL + 'API/Common/Popup/Upload.aspx' +
        '?TITLE=' + encodeURIComponent(TITLE) +
        '&CRUD=D' +
        '&GBN=' + GBN +
        '&MULTI=' + MULTI +
        '&FILENO=' + fileNo +
        '&FILENOID=' + NoCtrlId +
        '&FILECNTID=' + CntCtrlId +
        '&COMPCD=' + COMPCD +
        '&parentCallback=' + parentCallbackName;

    fn_OnPopupOpen(pPage, '800', '350');
}


// 계측기이력카드창 열기
function fn_OnPopupInspectionReport(key) {
    pPage = rootURL + 'Pages/MEAS/Popup/MEAS3005REPORT.aspx' +
        '?TITLE=계측기이력카드' +
        '&KEYFIELDS=' + key;
    fn_OnPopupOpen(pPage, '830', '0');
}

// 작업지시등록
function fn_OnPopupLTRK0201POP01(code, date) {
    pPage = rootURL + 'Pages/LTRK/Popup/LTRK0201POP01.aspx' +
        '?TITLE=' + encodeURIComponent('작업지시등록') +
        '&CRUD=W' +
        '&CODE=' + code +
        '&DATE=' + date;

    fn_OnPopupOpen(pPage, '0', '0');
}

// 작업지시등록현황
function fn_OnPopupLTRK0201POP02(code, date) {
    pPage = rootURL + 'Pages/LTRK/Popup/LTRK0201POP02.aspx' +
        '?TITLE=' + encodeURIComponent('작업지시등록현황') +
        '&CRUD=' +
        '&CODE=' + code +
        '&DATE=' + date;

    fn_OnPopupOpen(pPage, '0', '0');
}

// 작업지시등록현황
function fn_OnPopupLTRK0201POP02LOC(code, date) {
    pPage = rootURL + 'Pages/LTRK/Popup/LTRK0201POP02.aspx' +
        '?TITLE=' + encodeURIComponent('작업지시등록현황') +
        '&CRUD=' +
        '&CODE=' + code +
        '&DATE=' + date;

    return pPage;
}

// 자재투입현황
function fn_OnPopupLTRK0202POP01(sORDERGROUP, sORDERDATE, sORDERNO) {
    pPage = rootURL + 'Pages/LTRK/Popup/LTRK0202POP01.aspx' +
        '?TITLE=' + encodeURIComponent('자재투입현황') +
        '&CRUD=' +
        '&pGROUP=' + sORDERGROUP +
        '&pDATE=' + sORDERDATE +
        '&pNO=' + sORDERNO;

    fn_OnPopupOpen(pPage, '0', '0');
}

// 작지별 생산현황
function fn_OnPopupLTRK0202POP02(sORDERGROUP, sORDERDATE, sORDERNO) {
    pPage = rootURL + 'Pages/LTRK/Popup/LTRK0202POP02.aspx' +
        '?TITLE=' + encodeURIComponent('작지별 생산현황') +
        '&CRUD=' +
        '&pGROUP=' + sORDERGROUP +
        '&pDATE=' + sORDERDATE +
        '&pNO=' + sORDERNO;

    fn_OnPopupOpen(pPage, '850', '450');
}

// 수불현황
function fn_OnPopupLTRKINOUTPOP(sITEMCD, sITEMNM, sGROUP, nCNT, sUNIT) {
    pPage = rootURL + 'Pages/LTRK/Popup/LTRKINOUTPOP.aspx' +
        '?TITLE=' + encodeURIComponent('자재수불현황') +
        '&CRUD=RE' +
        '&pITEMCD=' + encodeURIComponent(sITEMCD) +
        '&pITEMNM=' + encodeURIComponent(sITEMNM) +
        '&pGROUP=' + sGROUP +
        '&pCNT=' + nCNT +
        '&pUNIT=' + sUNIT;

    fn_OnPopupOpen(pPage, '0', '0');
}

// 마감이력
function fn_OnPopupLTRK0501POP(sYYYYMM) {
    pPage = rootURL + 'Pages/LTRK/Popup/LTRK0501POP.aspx' +
        '?TITLE=' + encodeURIComponent(sYYYYMM + ' 마감이력') +
        '&CRUD=' +
        '&pYYYYMM=' + sYYYYMM;

    fn_OnPopupOpen(pPage, '850', '450');
}

// 체크시트 관리 리포트 팝업
function fn_OnPopupWERD5002_DACO_POP(key) {
    pPage = rootURL + 'Pages/WERD/Popup/WERD5002_DACO_POP.aspx' +
        '?TITLE=' + encodeURIComponent(encodeURIComponent('공정관리 체크시트')) +
        '&KEYFIELDS=' + encodeURIComponent(key);
    fn_OnPopupOpen(pPage, '1200', '800');
}