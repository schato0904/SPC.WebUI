var popupWin = null;

//document.oncontextmenu = new Function('return false');      //메뉴 방지
//document.ondragstart = new Function('return false');        // 드래그 방지
//document.onselectstart = new Function('return false');      // 선택 방지

// 지정한 프레임을 이동시킨다.
// pTarget	- 이동할 프레임명
// pHref	- 이동할 Url
function NavigateTo (pTarget, pHref)
{
    if (pHref==null || pHref=="") return ;
	
    if (pTarget == "")
    {
        document.location.href = pHref;
        return ;
    }
    if (window.top.frames.length > 1)
    {
        for (i=0; i<window.top.frames.length; i++)
        {
            if (window.top.frames(i).name == pTarget)
            {
                window.top.frames(i).src = pHref;
                return ;
            }
        }
    }
    for (i=0; i<window.document.frames.length; i++)
    {
        if (window.document.frames(i).name == pTarget)
        {
            window.document.frames(i).document.location.href = pHref;
            return ;
        }
    }
	   
    window.open(pHref, pTarget) ;
}

// 팝업창을 띄운다.(사이즈재조정)
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenWindowResize (pUrl, pWidth, pHeight)
{
    window.location.href = pUrl;	
    self.resizeTo(pWidth,pHeight);
}

// 팝업창을 띄운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenWindow (pUrl, pTitle, pWidth, pHeight)
{
    if(popupWin != null)    //현재 팝업창이 존재할경우 닫는다.
        popupWin.close();

    var left = (screen.width - pWidth) / 2 ;
    var top	= (screen.height - pHeight) / 2 ;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=no,toolbar=no,menubar=no,location=no,resizable=0';
    var oOPENSTATE = document.getElementById('_OPENSTATE') ;
    if(oOPENSTATE!=null)
        oOPENSTATE.value = "OpenWindow" ;
		
    popupWin = window.open(pUrl, pTitle, sFeatures);
    popupWin.focus();
}

// 팝업창을 띄운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이

function OpenWindowX (pUrl, pTitle, pWidth, pHeight)
{
    var left = (screen.width - pWidth) / 2 ;
    var top	= (screen.height - pHeight) / 2 ;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=no,toolbar=no,menubar=no,location=no,resizable=0';
    var oOPENSTATE = document.getElementById('_OPENSTATE') ;
    if(oOPENSTATE!=null)
        oOPENSTATE.value = "OpenWindow" ;
		
    popupWin = window.open(pUrl, pTitle, sFeatures);
    popupWin.focus();
}
// 팝업창을 띄운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenWindow2 (pUrl, pTitle, pWidth, pHeight)
{
    if(popupWin != null)    //현재 팝업창이 존재할경우 닫는다.
        popupWin.close();

    var left = (screen.width - pWidth) / 2 ;
    var top	= (screen.height - pHeight) / 2 ;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=no,toolbar=no,menubar=no,location=no,resizable=0,scrollbars=1';
    var oOPENSTATE = document.getElementById('_OPENSTATE') ;
    if(oOPENSTATE!=null)
        oOPENSTATE.value = "OpenWindow" ;
		
    popupWin = window.open(pUrl, pTitle, sFeatures);
    popupWin.focus();
}

// 새창을 뛰운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
function OpenNewWindow (pUrl, pTitle)
{
    var left = screen.width - 10 ;
    var top	= screen.height - 10 ;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=yes,toolbar=yes,menubar=yes,location=yes' ;
    window.open(pUrl, pTitle, sFeatures);
}

// 팝업창을 띄운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenNoti (pUrl, pTitle, pWidth, pHeight)
{
    var left = (screen.width - pWidth) / 2 ;
    var top	= (screen.height - pHeight) / 2 ;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=yes,toolbar=no,menubar=no,location=no' ;
    window.open(pUrl, pTitle, sFeatures);
}

// 팝업창을 띄운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenMain (pUrl)
{
    window.location.href = pUrl;
    self.moveTo(0, 0);
    self.resizeTo(screen.width, screen.height);
}

// 새창을 뛰운다.(전체창)
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
function OpenNewFullWindow(pUrl) {
    var pWidth = screen.availWidth - 20;
    var pHeight = screen.availHeight;
    var left = 0;
    var top = 0;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizeable=no';
    window.open(pUrl, 'SPC', sFeatures).focus();
}

function EmpReadyPage(pUrl,EmpCd)
{
    window.location.href = pUrl + '&' + EmpCd;
    self.moveTo(0,0);
    self.resizeTo(screen.width,screen.height);
}
/*==========================================================================
'함수	:	OpenWindowsReportHtml
'설명	:	html 리포팅 뷰어
'==========================================================================*/
function OpenWindowsReportHtml(pUrl, pTitle)
{
    var left = screen.width - 10 ;
    var top	= screen.height - 10 ;	
    var sFeatures = 'fullscreen=no, location=no, menubar=yes, status=yes, toolbar=no, titlebar=no, left=o, top=0, width='+left+', height='+top+', directories=no, resizable=yes';	
	
    window.open(pUrl, pTitle, sFeatures);
}
/*==========================================================================
'함수	:	OpenWindowsReportHtml
'설명	:	html 리포팅 뷰어
'==========================================================================*/
function OpenWindowsReportHtmlCurrent(pUrl, pTitle, pWidth, pHeight)
{
    if(popupWin != null)    //현재 팝업창이 존재할경우 닫는다.
        popupWin.close();

    var left = (screen.width - pWidth) / 2 ;
    var top	= (screen.height - pHeight) / 2 ;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=no,toolbar=no,menubar=no,location=no,resizable=0';
    var oOPENSTATE = document.getElementById('_OPENSTATE') ;
    if(oOPENSTATE!=null)
        oOPENSTATE.value = "OpenWindow" ;
		
    popupWin = window.open(pUrl, pTitle, sFeatures);
    popupWin.focus();
}

// 팝업창을 띄우고 부모창을 팝업창으로 Submit시킨다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenWindowNSubmit (pUrl, pTitle, pWidth, pHeight)
{
    if(popupWin != null)    //현재 팝업창이 존재할경우 닫는다.
        popupWin.close();
        
    var left = (screen.width - pWidth) / 2 ;
    var top	= (screen.height - pHeight) / 2 ;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=yes,toolbar=no,menubar=no,location=no' ;
    var oNAVIGATIONSTATE = document.getElementById('_NAVIGATIONSTATE') ;
    if(oNAVIGATIONSTATE!=null)
        oNAVIGATIONSTATE.value = "OpenWindowNSubmit" ;
	
    popupWin = window.open("/aspnet_client/saf_net/blank.htm", pTitle, sFeatures);
    var oAction = document.forms[0].action ;
    var oTarget = document.forms[0].target ;
	
    document.forms[0].action = pUrl ;
    document.forms[0].target = pTitle ;
    document.forms[0].submit() ;
    document.forms[0].action = oAction ;
    document.forms[0].target = oTarget ;
	
    popupWin.focus();
}

// 모달 대화상자를 띄운다.
function ShowModalDialog (pUrl, pWidth, pHeight)
{
    var left = (screen.width - pWidth) / 2 ;
    var top	= (screen.height - pHeight) / 2 ;
    var sFeatures = 'dialogLeft:' + left + ';dialogTop:' + top + ';dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px' ;
    var sReturnValue = window.showModalDialog(pUrl, '', sFeatures) ; 
    return sReturnValue ;
}

// 지정한 프레임을 Reload시킨다. 
// 해당 프레임의 페이지에서는 코드비하인드에서 Page.IsReloaded.. 프로퍼티로 Reload될때의 처리를 할 수 있다.
// (CTF.AF.Web.PageBase를 상속받아야 한다.)
function Reload (pTarget)
{
    if (pTarget == "")
        return false ;

    if(pTarget=="parent")
    {
        var oRELOADEDBY = parent.document.getElementById('_RELOADEDBY') ;
        if(oRELOADEDBY!=null)
        {
            oRELOADEDBY.value = 'differentframe' ;	 
        }
        parent.document.forms[0].submit () ;
    }
				
    if (window.top.frames.length > 1)
    {
        for (i=0; i<window.top.frames.length; i++)
        {
            if (window.top.frames(i).name == pTarget)
            {
                var oRELOADEDBY = window.top.frames(i).document.getElementById('_RELOADEDBY') ;
                if(oRELOADEDBY!=null)
                {
                    oRELOADEDBY.value = 'differentframe' ;	 
                }
                window.top.frames(i).document.forms[0].submit () ;
                return true;
            }
        }
    }
    for (i=0; i<window.document.frames.length; i++)
    {
        if (window.document.frames(i).name == pTarget)
        {
            var oRELOADEDBY = window.document.frames(i).document.getElementById('_RELOADEDBY') ;
            if(oRELOADEDBY!=null)
            {
                oRELOADEDBY.value = 'differentframe' ;	 
            }
            window.document.frames(i).document.forms[0].submit () ;
            return true;
        }
    }
}

// 팝업창을 닫고 부모창을 Reload시킨다.
function Close (pReloadOpener)
{
    if(opener != null)
    {
        var oRELOADEDBY = opener.document.getElementById('_RELOADEDBY') ;
        if(oRELOADEDBY!=null)
        {
            oRELOADEDBY.value = 'popup' ;
            if(pReloadOpener)
            {
                opener.document.forms[0].submit() ;
            }
        }
        self.close() ;
    }
}

// 부모창의 element에 값을 설정한다.
// pId - element의 id
// pValue - element에 설정할 값
function SendValueToOpener (pId, pValue)
{
    if(opener !=null)
    {
        var oInputField = opener.document.getElementById(pId) ;
        if(oInputField != null) // element가 있으면 값을 설정한다.
        {
            oInputField.value = pValue ;
        }
        else // element가 없으면 hidden Field를 생성한다.
        {
            var oForm = opener.document.forms[0] ;
            if(opener.IsXjosRegistered ())
            {
                oForm.setSendValue ("'" + pId + "=" + pValue + "'") ;
            }
        }
    }
}

// 지정한 프레임내의 element에 값을 설정한다.
// pTarget - 프레임명
// pId - element의 id
// pValue - element에 설정할 값
function SendValueToFrame (pTarget, pId, pValue)
{
    if(pTarget=="parent")
    {
        var oInputField = parent.document.getElementById(pId) ;
        if(oInputField != null)
        {
            oInputField.value = pValue ;
        }
        return ;
    }
	
    //전체Frame탐색
    if (window.top.frames.length > 1)
    {
        for (i=0; i<window.top.frames.length; i++)
        {
            if (window.top.frames(i).name == pTarget)
            {
                var oInputField = window.top.frames(i).document.getElementById(pId) ;
                if(oInputField != null)
                {
                    oInputField.value = pValue ;
                }
                return ;
            }
        }
    }
    //iFrame탐색
    for (i=0; i<window.document.frames.length; i++)
    {
        if (window.document.frames(i).name == pTarget)
        {
            var oInputField = window.document.frames(i).document.getElementById(pId) ;
            if(oInputField != null)
            {
                oInputField.value = pValue ;
            }
            return ;
        }
    }
}

// 폼을 지정한 프레임으로 Submit시킨다.
function SubmitForm (pTarget, pAction)
{
    var oNAVIGATIONSTATE = document.getElementById('_NAVIGATIONSTATE') ;
    if(oNAVIGATIONSTATE!=null)
        oNAVIGATIONSTATE.value = "SubmitForm" ;
		
    var oAction = document.forms[0].action ;
    var oTarget = document.forms[0].target ;
    document.forms[0].target = pTarget ;
    document.forms[0].action = pAction ;
	
    if(IsXjosRegistered ())
        document.forms[0].fireSubmit () ; // Xjos를 사용할경우 fireSubmit을 호출해야 validation이 작동
    else
        document.forms[0].submit () ;
		
    document.forms[0].action = oAction ;
    document.forms[0].target = oTarget ;
}

function SelectAll(pName)
{
    var oCheckBoxes = document.getElementsByName(pName) ;
    for(i=0;i < oCheckBoxes.length ; i++)
    {
        oCheckBoxes[i].checked = true ;
    }
}

function UnSelectAll(pName)
{
    var oCheckBoxes = document.getElementsByName(pName) ;
    for(i=0;i < oCheckBoxes.length ; i++)
    {
        oCheckBoxes[i].checked = false ;
    }
}
function setChkRdoBorderNone() {
    var f = document.Form1;
    for (var i = 0; i < f.elements.length; i++) {
        if (f.elements[i].type == 'checkbox' || f.elements[i].type == 'radio') {
            f.elements[i].style.boderWidth = "0";
            f.elements[i].style.borderStyle = "none";
        }
    }
}

// 스크롤바의 크기를 구한다.
function scrollbarWidth() {
    if (!$(".content-panel").hasScrollBar()) {
        return 0;
    } else {
        // Create the measurement node
        var scrollDiv = document.createElement("div");
        scrollDiv.className = "scrollbar-measure";
        document.body.appendChild(scrollDiv);

        // Get the scrollbar width
        var scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth;

        // Delete the DIV 
        document.body.removeChild(scrollDiv);
        return scrollbarWidth;
    }
}

function scrollbarWidthAny() {
    // Create the measurement node
    var scrollDiv = document.createElement("div");
    scrollDiv.className = "scrollbar-measure";
    document.body.appendChild(scrollDiv);

    // Get the scrollbar width
    var scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth;

    // Delete the DIV 
    document.body.removeChild(scrollDiv);
    return scrollbarWidth;
}

(function ($) {
    $.fn.hasScrollBar = function () {
        return this.get(0).scrollWidth > this.width();
    }
})(jQuery);

// 소수점 자릿수를 구한다
function fn_GetDecimalPoint(val) {
    return val.length - val.indexOf('.') - 1;
}

// 고정자릿수로 변환
function fn_SetToFixed(val, digit) {
    return (val=='') ? '' : parseFloat(val).toFixed(digit);
}

// 호환성보기 유무체크
function isTrident() {
    return window.navigator.userAgent.search("MSIE 7") > -1 && window.navigator.userAgent.search("Trident") > -1;
}

// 배열
function fn_ArrayContains(obj, key) {
    return key in obj;
}

function fn_ArrayAdd(obj, key, value) {
    obj[key] = value;
}

function fn_ArrayDelete(obj, key) {
    delete obj[key];
}

// Add Days
function addDays(theDate, days) {
    return new Date(theDate.getTime() + days * 24 * 60 * 60 * 1000);
}

// Add Months
function addMonths(theDate, months) {
    var CurrentDate = new Date(theDate.getTime() + 0 * 24 * 60 * 60 * 1000);
    return new Date(CurrentDate.setMonth(parseInt(CurrentDate.getMonth(), 10) + parseInt(months, 10)))
}

// DataDiff
function daysBetween(first, second) {

    // Copy date parts of the timestamps, discarding the time parts.
    var one = new Date(first.getFullYear(), first.getMonth(), first.getDate());
    var two = new Date(second.getFullYear(), second.getMonth(), second.getDate());

    // Do the math.
    var millisecondsPerDay = 1000 * 60 * 60 * 24;
    var millisBetween = two.getTime() - one.getTime();
    var days = millisBetween / millisecondsPerDay;

    // Round down.
    return Math.floor(days);
}

// DataDiff
function monthBetween(first, second) {
    var strtYear = parseInt(first.substring(0, 4));
    var strtMonth = parseInt(first.substring(5, 7));

    var endYear = parseInt(second.substring(0, 4));
    var endMonth = parseInt(second.substring(5, 7));

    var month = (endYear - strtYear) * 12 + (endMonth - strtMonth) + 1;

    // Round down.
    return Math.floor(month);
}

// Convert MiliSecond To Time
function msToTime(s) {

    function addZ(n) {
        return (n < 10 ? '0' : '') + n;
    }

    var ms = s % 1000;
    s = (s - ms) / 1000;
    var secs = s % 60;
    s = (s - secs) / 60;
    var mins = s % 60;
    var hrs = (s - mins) / 60;

    return addZ(hrs) + ':' + addZ(mins) + ':' + addZ(secs);
}

// Convert DateString(yyyy-MM-dd) Split
function convertDateString(dateString) {
    if (dateString.length == 10) {
        var dateArray = dateString.split('-');
        return new Date(dateArray[0], dateArray[1] - 1, dateArray[2]);
    } else {
        return new Date();
    }
}

// Convert Date To String
function formatDateString(date, separate) {
    var dateString = (date.getFullYear() + separate
        + ('0' + (date.getMonth() + 1)).slice(-2)
        + separate + ('0' + (date.getDate())).slice(-2));

    return dateString;
}

// Check Date
function isValidDate(param) {
    try {
        param = param.replace(/[\{\}\[\]\/?.,;:|\)*~`!^\-_+<>@\#$%&\\\=\(\'\"]/gi, '');
        // 자리수가 맞지않을때
        if (isNaN(param) || param.length != 8) {
            return false;
        }

        var year = Number(param.substring(0, 4));
        var month = Number(param.substring(4, 6));
        var day = Number(param.substring(6, 8));

        var dd = day / 0;


        if (month < 1 || month > 12) {
            return false;
        }

        var maxDaysInMonth = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
        var maxDay = maxDaysInMonth[month - 1];

        // 윤년 체크
        if (month == 2 && (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)) {
            maxDay = 29;
        }

        if (day <= 0 || day > maxDay) {
            return false;
        }
        return true;

    } catch (err) {
        return false;
    }
}

function getDate(now, addYear, addMonth, addDay, token){
    token = token == undefined || token == null ? "-" : token;
    addYear = addYear == null ? 0 : addYear;
    addMonth = addMonth == null ? 0 : addMonth;
    addDay = addDay == null ? 0 : addDay;

    var ry = now.getFullYear();
    var rm = now.getMonth() + 1;
    var rd = now.getDate();
        
    if(addYear != 0){//cal year
        ry = ry + addYear;
    }
        
    if(addMonth != 0){//cal month
        var isRun = true;
        rm = rm + addMonth;
        while(isRun == true){
            if(rm > 12){
                ry++;
                rm = rm - 12;
            }else if(rm < 1){
                ry--;
                rm = 12 + rm;
            }
                        
            if(rm > 0 && rm < 13){
                isRun = false;
            }
        }
    }

    var cal = new Date(ry, rm, 0);
    if (rd > cal.getDate()) {
        rd = cal.getDate();
        addDay = addDay < 0 ? 0 : addDay;
    }
    if(addDay != 0){
        rd = rd + addDay;
        if(rd > cal.getDate() || rd < 1){
            var isRun = true;
            while(isRun == true){
                if(rd > cal.getDate()){
                    rd = rd - cal.getDate();
                    rm++;
                    if(rm > 12){
                        ry++;
                        rm = 1;
                    }
                }
                if(rd < 1){
                    rm--;
                    if(rm < 1){
                        ry--;
                        rm = 12;
                    }
                    cal = new Date(ry, rm, 0);
                    rd = cal.getDate() + rd;
                }

                cal = new Date(ry, rm, 0);
                if(rd <= cal.getDate() && rd >= 1)
                    isRun = false;
            }
        }

        if(rd > cal.getDate() || rd < 1){
            cal = new Date(ry, rm, 0);
        }
    }
                
    if(rm.toString().length < 2)
        rm = '0' + rm;
    if (rd.toString().length < 2)
        rd = '0' + rd;
        
    return ry + token + rm + token + rd;
}

// 검사기준정보 다중 입력/수정 페이지 이동
function fn_GoToQCD34Multi(pageNM) {
    parent.parent.doCreateTab('MM01|MM0103|BSIF|' + pageNM + '_MULTI|검사기준관리(다중관리)|CRUDWSE|1');
}

// json에서 값 가져오기(null 처리 포함)
function GetJsonValueByKey(json, key, isnullvalue) {
    isnullvalue = typeof (isnullvalue) != 'undefined' ? isnullvalue : '';
    return (typeof (json[key]) != 'undefined') ? json[key] : isnullvalue;
}

// 아무것도 안하는 Func
function fn_OnEmpty() { };

// 이미지 존재유무
function testImage(URL, obj) {
    var tester = new Image();
    tester.onerror = function () { imageNotFound(obj); }
    tester.src = URL;
}

function imageNotFound(obj) {
    $(obj).attr('src', '');
}

// null, 공백, undefined 모두 공백처리
function Trim(str) {
    if (typeof (str) === 'undefined' || str === null) return '';
    else return String(str).replace(/(^\s*)|(\s*$)/gi, "");
}

// ajax로 Data조회. 콜백함수를 통해 json객체 반환
// string CATEGORY, string TABLE, string PKEYNM, string PKEYVALUE
function fn_GetTable(CATEGORY, TABLE, PKEYNM, PKEYVALUE, successCallback, failCallback) {
    if (CATEGORY == "" || TABLE == "" || PKEYNM == "" || PKEYVALUE == "") return;
    $.ajax({
        type: "POST",
        url: fn_GetCurrentPageName() + "/GetTable",
        data: '{CATEGORY: "' + CATEGORY + '", TABLE: "' + TABLE + '", PKEYNM: "' + PKEYNM + '", PKEYVALUE: "' + PKEYVALUE + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (xhr, status, error) {
            var err = JSON.parse(xhr.responseText);
            if (typeof (failCallback) === "function") {
                failCallback(err.Message);
            } else {
                alert(err.Message);
            }
        }
    });

    function OnSuccess(response) {
        //alert(response.d);
        if (typeof (successCallback) === "function") {
            successCallback(JSON.parse(response.d));
        }
    }
}

// 현재 페이지명 반환(파일명만)  예) DTMN1001.aspx
function fn_GetCurrentPageName() {
    var r = new RegExp("([^/]+\.aspx)");
    return r.exec(location.href)[0];
}

function fn_DelListFromSource(sourceList, delList, separator) {
    if (Trim(separator) == "") separator = ";";
    sourceList += separator;
    var delItems = delList.split(separator);
    for (var i = 0; i < delItems.length; i++) {
        sourceList = sourceList.replace(delItems[i] + separator, "");
    }
    sourceList = sourceList.replace(new RegExp(separator + "$"), "");
    return sourceList;
}

// 지정된 필드 클리어 처리
function fn_ClearFields(fieldList, prefix, exceptList) {
    var ctrlId = "";
    if (Trim(prefix) == "") prefix = "src";
    if (Trim(exceptList) != "")
        fieldList = fn_DelListFromSource(fieldList, exceptList);
    var fields = fieldList.split(";");
    for (var i = 0 ; i < fields.length ; ++i) {
        ctrlId = prefix + fields[i];
        if (typeof (window[ctrlId]) != 'undefined' && typeof (window[ctrlId].Clear) == 'function') {
            window[ctrlId].Clear();
        }
        else fn_SetTextValue(ctrlId, "");
    }
}

// json 객체로 넘겨진 필드명을 가진 컨트롤에, 값 세팅
function fn_SetFields(fieldJSON, prefix) {
    var ctrlId = "";
    if (Trim(prefix) == "") prefix = "src";
    for (var key in fieldJSON) {
        ctrlId = prefix + key;
        if (typeof (window[ctrlId]) != 'undefined' && typeof (window[ctrlId].fn_SetUCControl) == 'function') {
            window[ctrlId].fn_SetUCControl(fieldJSON);
        } else if (typeof (window[ctrlId]) != 'undefined' && window[ctrlId] instanceof ASPxClientDateEdit) {
            fn_SetTextValue(ctrlId, fieldJSON[key]);
        } else if (typeof (window[ctrlId]) != 'undefined' && typeof (window[ctrlId].SetValue) == 'function') {
            window[ctrlId].SetValue(fieldJSON[key]);
        }
        else fn_SetTextValue(ctrlId, fieldJSON[key]);
    }
}

function fn_GetDocumentHeight() {
    if (fn_HasDocumentScroll()) {
        return Math.max($(document).height(), $('.content-panel>form').height());
    } else {
        return $(document).height();
    }
}

function fn_HasDocumentScroll() {
    return $('.content-panel').css('overflow-y') == 'scroll';
}

// ajax로 로그인 사용자 정보조회. 콜백함수를 통해 json객체 반환
// 파라미터 infoList : ";"으로 구분. 필요 정보필드 전달
// 예) USERID;USERNM
// 필드명 : USERID, USERNM, COMPCD, COMPNM, FACTCD, FACTNM, DEPTCD, DEPTNM
function fn_GetLoginUserInfo(infoList, successCallback) {
    if (infoList == "") return;
    $.ajax({
        type: "POST",
        url: fn_GetCurrentPageName() + "/GetLoginUserInfo",
        data: '{infoList: "' + infoList + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (xhr, status, error) {
            var err = JSON.parse(xhr.responseText);
            alert(err.Message);
        }
    });

    function OnSuccess(response) {
        //alert(response.d);
        if (typeof (successCallback) === "function") {
            successCallback(JSON.parse(response.d));
        }
    }
}

function jsonEscape(str) {
    return str.replace(/\n/g, "\\\\n").replace(/\r/g, "\\\\r").replace(/\t/g, "\\\\t");
}

function jsonUnEscape(str) {
    return str.replace("\\\\n", /\n/g).replace("\\\\r", /\r/g).replace("\\\\t", /\t/g);
}


////////////////////////////////////////////////////////////
// 날짜 처리함수 : 여기부터

// 기능구분을 명확하게 하기 위하여 증감치 부여기능 보류
//// 날짜문자열("yyyy-MM-dd[ HH:mm:ss]")을 Date객체로 변환
//// 뒤 파라미터로 년월일시분초에 증감치 부여 가능 : strdate('2019-09-27', 0, 0, 1) ==> Date객체(2019-09-28) 생성
//function str2date(str, a0, a1, a2, a3, a4, a5) {
//    if (!IsDate(str)) throw '날짜 형식이 올바르지 않습니다.';
//    var d = str.toString().split(/[\s-:]/gi);
//    var nd = new Date(d[0] * 1 + (a0 || 0), d[1] * 1 - 1 + (a1 || 0), d[2] * 1 + (a2 || 0), (d[3] || 0) * 1 + (a3 || 0), (d[4] || 0) * 1 + (a4 || 0), (d[5] || 0) * 1 + (a5 || 0));
//    return nd;
//}

// 날짜문자열("yyyy-MM-dd[ HH:mm:ss]")을 Date객체로 변환
function str2date(str) {
    if (!IsDate(str)) throw '날짜 형식이 올바르지 않습니다.';
    var d = str.toString().split(/[\s-:]/gi);
    var nd = new Date(d[0] * 1, d[1] * 1 - 1, d[2] * 1, (d[3] || 0) * 1, (d[4] || 0) * 1, (d[5] || 0) * 1);
    return nd;
}

// Date이거나, "yyyy-MM-dd[ HH:mm:ss]" 형태 문자열인지 확인
function IsDate(o) { return (o instanceof Date) || ((o instanceof String || typeof (o) == 'string') && /^\d{4}-\d{2}-\d{2}(\s\d{2}:\d{2}(:\d{2})?)?$/.test(o)); }

// ECMA6에 추가된 함수 구현 :  ("5").padStart(3, "0") ==> "005"
if (typeof (String.prototype.padStart) != 'function') {
    String.prototype.padStart = function (n, c) { return (new Array(n + 1).join(c) + this).slice(-1 * n); };
}

// 숫자에 padStart 반영 : (5).padStart(3, "0") ==> "005"
if (typeof (Number.prototype.padStart) != 'function') {
    Number.prototype.padStart = function (n, c) { return String(this).padStart(n, c); };
}

// Date 객체의 toString 함수 재정의 : new Date().toString() ==> "2019-09-27 10:38:25"
Date.prototype.toString = function () {
    var d = this;
    var r = d.getFullYear().padStart(4, '0');
    r += '-' + (d.getMonth() + 1).padStart(2, '0');
    r += '-' + d.getDate().padStart(2, '0');
    r += ' ' + d.getHours().padStart(2, '0');
    r += ':' + d.getMinutes().padStart(2, '0');
    r += ':' + d.getSeconds().padStart(2, '0');
    //r += '.' + d.getMilliseconds().padStart(3,'0');
    return r;
}

// Date 객체 증감 처리 확장 함수
// 파라미터는 년월일시분초
Date.prototype.Add = function (a0, a1, a2, a3, a4, a5) {
    var d = this;
    a0 = d.getFullYear() + (a0 || 0);
    a1 = d.getMonth() + (a1 || 0);
    a2 = d.getDate() + (a2 || 0);
    a3 = d.getHours() + (a3 || 0);
    a4 = d.getMinutes() + (a4 || 0);
    a5 = d.getSeconds() + (a5 || 0);
    return new Date(a0, a1, a2, a3, a4, a5);
}

// 해당월 첫째날
Date.prototype.First = function () {
    var d = this;
    a0 = d.getFullYear();
    a1 = d.getMonth();
    a2 = 1;
    return new Date(a0, a1, a2);
}

// 해당월 마지막날
Date.prototype.Last = function () {
    var d = this;
    a0 = d.getFullYear();
    a1 = d.getMonth() + 1;
    a2 = 0;
    return new Date(a0, a1, a2);
}
// 날짜 처리함수 : 여기까지
////////////////////////////////////////////////////////////