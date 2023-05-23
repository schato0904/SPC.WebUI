var popupWin = null;

function CurrentDate() {
    var today = new Date();
    var Yy = today.getYear();
    var Mm = parseInt(today.getMonth()) + 1;
    var Dd = today.getDate();

    return Yy + "-" + Mm + "-" + Dd;
}

/*=======================================================================
함수명 : allblur
기  능 : 링크시 생기는 점선라인 없애기
인  수 : 
리턴값 :
=======================================================================*/
function allblur() {
    for (i = 0; i < document.links.length; i++)
        document.links[i].onfocus = document.links[i].blur;
}
/*=======================================================================
함수명 : BlockEnterKey
기  능 : keypress 이벤트시 엔터키이벤트를 막는다.
리턴값 : boolean
=======================================================================*/
function BlockEnterKey() {
    if (event.keyCode == 13) {
        return false;
    } else {
        return true;
    }
}

/*=======================================================================
함수명 : OpenWindowEnterKey
기  능 : keypress 이벤트시 공통팝업창을 띄운다.
리턴값 : boolean
=======================================================================*/
function OpenWindowEnterKey(pGubun, pUrl) {
    /*
		01 : 달력팝업
		02 : 
		03 : 		
	*/
    if (event.keyCode == 13) {
        switch (pGubun) {
            case "01":
                OpenWindow("/SPC.WebUI/common/popup/CalendarForm.aspx?" + pUrl, '달력', 220, 230);
                break;
            case "02":

                break;
            case "03":

                break;
        }
        return false;
    }
    else {
        return true;
    }
}

/*=======================================================================
함수명 : OpenWindowByClick
기  능 : onclick 이벤트시 공통팝업창을 띄운다.
리턴값 : boolean
=======================================================================*/
function OpenWindowByClick(pGubun, pUrl) {
    /*
		01 : 달력팝업
		02 : 
		03 : 
	*/
    switch (pGubun) {
        case "01":
            //OpenWindow("/SPC.WebUI/common/popup/CalendarForm.aspx?"+pUrl, '달력',220,230);
            break;
        case "02":

            break;
        case "03":

            break;
    }

    return false;

}

function ClsChkRdoBd() {
    var f = document.forms[0];
    for (var i = 0; i < f.elements.length; i++) {
        if (f.elements[i].type == 'checkbox' || f.elements[i].type == 'radio') {
            f.elements[i].style.boderWidth = "0";
            f.elements[i].style.borderStyle = "none";
        }
    }
}

/*=======================================================================
	함수명 : CreateImage
	기  능 : 이미지 생성
	인  수 : 
	리턴값 :
=======================================================================*/
function CreateImage(strUrl) {
    var objImg = new Image();
    objImg.src = strUrl;
    return objImg;
}


// 팝업창을 띄운다.(사이즈재조정)
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenWindowResize(pUrl, pWidth, pHeight) {
    window.navigate(pUrl);
    self.resizeTo(pWidth, pHeight);
}

// 팝업창을 띄운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenWindow(pUrl, pTitle, pWidth, pHeight) {
    if (popupWin != null)    //현재 팝업창이 존재할경우 닫는다.
        popupWin.close();

    var left = (screen.width - pWidth) / 2;
    var top = (screen.height - pHeight) / 2;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=no,toolbar=no,menubar=no,location=no,resizable=0';
    var oOPENSTATE = document.getElementById('_OPENSTATE');
    if (oOPENSTATE != null)
        oOPENSTATE.value = "OpenWindow";

    popupWin = window.open(pUrl, pTitle, sFeatures);
    popupWin.focus();
}

// 팝업창을 띄운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
// pWidth	- 팝업창 높이
// pHeight	- 팝업창 높이
function OpenWindow2(pUrl, pTitle, pWidth, pHeight) {
    if (popupWin != null)    //현재 팝업창이 존재할경우 닫는다.
        popupWin.close();

    var left = (screen.width - pWidth) / 2;
    var top = (screen.height - pHeight) / 2;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=no,toolbar=no,menubar=no,location=no,resizable=0,scrollbars=1';
    var oOPENSTATE = document.getElementById('_OPENSTATE');
    if (oOPENSTATE != null)
        oOPENSTATE.value = "OpenWindow";

    popupWin = window.open(pUrl, pTitle, sFeatures);
    popupWin.focus();
}

// 새창을 뛰운다.
// pUrl		- 팝업으로 띄울 웹페이지
// pTitle	- 팝업창 제목
function OpenNewWindow(pUrl, pTitle) {
    var left = screen.width - 10;
    var top = screen.height - 10;
    var sFeatures = 'left=' + left + ', top=' + top + ', width=' + pWidth + ', height=' + pHeight + ',status=yes,toolbar=yes,menubar=yes,location=yes';
    window.open(pUrl, pTitle, sFeatures);
}

function ReplaceChar(IsMode, ReplaceStr) {
    if (IsMode == 0) {
        ReplaceStr = ReplaceStr.replace(/\^/g, "＾");
        ReplaceStr = ReplaceStr.replace(/</g, "＜");
        ReplaceStr = ReplaceStr.replace(/>/g, "＞");
        ReplaceStr = ReplaceStr.replace(/&/g, "＆");
        ReplaceStr = ReplaceStr.replace(/'/g, "＇");
        ReplaceStr = ReplaceStr.replace(/\"/g, "＂");
        ReplaceStr = ReplaceStr.replace(/\|/g, "｜");
        ReplaceStr = ReplaceStr.replace(/@/g, "＠");
        ReplaceStr = ReplaceStr.replace("$", "＄");
        ReplaceStr = ReplaceStr.replace(/%/g, "％");
        ReplaceStr = ReplaceStr.replace(/#/g, "＃");
        ReplaceStr = ReplaceStr.replace(/\+/g, "＋");
        ReplaceStr = ReplaceStr.replace(/;/g, "；");
        ReplaceStr = ReplaceStr.replace(/_/g, "＿");
        ReplaceStr = ReplaceStr.replace("/", "／");
        ReplaceStr = ReplaceStr.replace("/", "／");
        ReplaceStr = ReplaceStr.replace("/", "／");
        ReplaceStr = ReplaceStr.replace("/", "／");
        ReplaceStr = ReplaceStr.replace("/", "／");
        ReplaceStr = ReplaceStr.replace("/", "／");
        ReplaceStr = ReplaceStr.replace("/", "／");
        ReplaceStr = ReplaceStr.replace("/", "／");
    }
    else {
        ReplaceStr = ReplaceStr.replace(/＾/g, "^");
        ReplaceStr = ReplaceStr.replace(/＜/g, "<");
        ReplaceStr = ReplaceStr.replace(/＞/g, ">");
        ReplaceStr = ReplaceStr.replace(/＆/g, "&");
        ReplaceStr = ReplaceStr.replace(/＇/g, "'");
        ReplaceStr = ReplaceStr.replace(/＂/g, "\"");
        ReplaceStr = ReplaceStr.replace(/｜/g, "|");
        ReplaceStr = ReplaceStr.replace(/＠/g, "@");
        ReplaceStr = ReplaceStr.replace(/＄/g, "$");
        ReplaceStr = ReplaceStr.replace(/％/g, "%");
        ReplaceStr = ReplaceStr.replace(/＃/g, "#");
        ReplaceStr = ReplaceStr.replace(/＋/g, "+");
        ReplaceStr = ReplaceStr.replace(/；/g, ";");
        ReplaceStr = ReplaceStr.replace(/＿/g, "_");
        ReplaceStr = ReplaceStr.replace(/／/g, "/");
    }
    return ReplaceStr;
}

/*==========================================================================
'함수명		:	ReplaceCharJS
'설명		:	특수문자 전각처리
'==========================================================================*/
function ReplaceCharJS(ReplaceStr) {
    ReplaceStr = ReplaceStr.replace(/</g, "＜");
    ReplaceStr = ReplaceStr.replace(/>/g, "＞");
    ReplaceStr = ReplaceStr.replace(/'/g, "＇");
    ReplaceStr = ReplaceStr.replace(/&/g, "＆");
    ReplaceStr = ReplaceStr.replace(/\^/g, "＾");
    ReplaceStr = ReplaceStr.replace(/\"/g, "＂");
    //ReplaceStr.replace(/|/g,"｜")
    //StrReplace	 = Replace(StrReplace,"","＂")
    return ReplaceStr;
}
//Pageing Setting
function PF_fnPagingCall(pTotalCount) {
    pCurrentPage = CurrentPage;
    pPageSize = PageSize;
    intPageStep = PageStep;

    //false 이면 문자 true 이면 이미지
    bImgButten = true;
    //-----------------------------------------------------------------------------
    // 입력하지 않으면 defalut 이미지 사용
    //-----------------------------------------------------------------------------
    //sPrevImg = "navi_prev.gif"					'이전 이미지 아이콘 
    //sNextImg = "navi_next.gif"					'다음 이미지 아이콘 
    //sPrevImg2 = "navi_next2.gif"				'처음 이미지 아이콘 
    //sNextImg2 = "navi_next2.gif"				'마지막 이미지 아이콘
    //-----------------------------------------------------------------------------	

    document.all("alblTotalCnt").innerText = pTotalCount;
    document.getElementById("pagenavig").innerHTML = Page_Load(pTotalCount, pCurrentPage, intPageStep, pPageSize);
}
/*==========================================================================
'함수명		:	Ajax에러처리 및 함수분기 (2008-06-11.Leira)
'설명		:	PF_AjaxDataHandling
'==========================================================================*/
function PF_AjaxDataHandling(Res) {
    if (Res.error != null) {
        switch (Res.error.Type) {
            case "System.IO.FileNotFoundException":
                alert("System.IO.FileNotFoundException: " + Res.error.Message);
                break;
            default:
                alert("MyException: " + Res.error.Message);
                break;
        }
    }
    else {
        strAry = Res.value.split("|");
        FnAjaxDataHanding(strAry[0], strAry[1], strAry[2]);
    }
}

function PF_AjaxDataHandling4(Res) {
    if (Res.error != null) {
        switch (Res.error.Type) {
            case "System.IO.FileNotFoundException":
                alert("System.IO.FileNotFoundException: " + Res.error.Message);
                break;
            default:
                alert("MyException: " + Res.error.Message);
                break;
        }
    }
    else {
        strAry = Res.value.split("|");
        FnAjaxDataHanding(strAry[0], strAry[1], strAry[2], strAry[3]);
    }
}

/*==========================================================================
'함수명		:	저장완료함수 (2008-06-11.Leira)
'설명		:	fnSaveDone
'==========================================================================*/
function PF_fnSaveDone(pSaveMode) {
    if (pSaveMode == "I") alert(SAVE_DONE_NEW);     //등록완료
    if (pSaveMode == "U") alert(UPDATE_DONE_NEW);   //수정완료
    if (pSaveMode == "D") alert(DELETE_DONE_NEW);   //삭제완료
    if (pSaveMode == "IE") alert(SAVE_DONE_ERR);    //등록에러
    if (pSaveMode == "UE") alert(UPDATE_DONE_ERR);  //수정에러
    if (pSaveMode == "DE") alert(DELETE_DONE_ERR);  //삭제에러
}

/*==========================================================================
'함수명		:	컨트롤일괄활성/비활성 (2008-06-12.Leira)
'설명		:	PF_CtlEnable
'==========================================================================*/
function PF_CtlEnableForm(pMode) {
    for (ctl in document.form1) {
        if (ctl.substring(0, 4) == "atxt" || ctl.substring(0, 4) == "aChk" || ctl.substring(0, 4) == "aRao") {
            document.getElementById(ctl).readOnly = !pMode;
            document.getElementById(ctl).style.borderStyle = pMode ? "Solid" : "None";
        }
        else if (ctl.substring(0, 4) == "addl") document.getElementById(ctl).disabled = !pMode;
    }
}

/*==========================================================================
'함수명		:	컨트롤일괄활성/비활성 (2008-06-12.Leira)
'설명		:	PF_CtlEnable
'==========================================================================*/
function PF_CtlEnable(ctl, pMode) {
    if (ctl.substring(0, 4) == "atxt" || ctl.substring(0, 4) == "aChk" || ctl.substring(0, 4) == "aRao") {
        document.getElementById(ctl).readOnly = !pMode;
        document.getElementById(ctl).style.borderStyle = pMode ? "Solid" : "None";
    }
    else if (ctl.substring(0, 4) == "addl") document.getElementById(ctl).disabled = !pMode;
}

/*==========================================================================
'함수명		:	메시지박스 (2008-06-12.Leira)
'설명		:	PF_ConfirmDialg
'==========================================================================*/
function PF_ConfirmDialg(sMsg) {
    var IsOk = confirm(sMsg);
    if (IsOk) return true; else return false;
}

/*==========================================================================
'함수명		:	메시지박스 (2008-06-12.Leira)
'설명		:	PF_ConfirmDialg
'==========================================================================*/
function PF_ConfirmDialgSave(pSaveMode) {
    if (pSaveMode == "I") return (PF_ConfirmDialg(REGISTER_QUESTION));
    if (pSaveMode == "U") return (PF_ConfirmDialg(UPDATE_QUESTION));
    if (pSaveMode == "D") return (PF_ConfirmDialg(DELETE_QUESTION));
}

/*==========================================================================
'함수명		:	컨트롤숨김 (2008-06-12.Leira)
'설명		:	PF_CtlVisible
'==========================================================================*/
function PF_CtlHide(ctl, pMode, pWidth) {
    if (pMode) {
        document.getElementById(ctl).width = pWidth
    }
    else {
        document.getElementById(ctl).width = 0
    }
}

/*==========================================================================
'함수명		:	컨트롤숨김 (2008-06-12.Leira)
'설명		:	PF_CtlVisible
'==========================================================================*/
function PF_CtlVisible(ctl, pMode) {
    if (pMode) {
        document.getElementById(ctl).style.display = "";
        document.getElementById(ctl).style.visibility = "visible";
    }
    else {
        document.getElementById(ctl).style.display = "none";
        document.getElementById(ctl).style.visibility = "hidden";
    }
}

/*==========================================================================
'함수명		:	컨트롤대체 (2008-06-12.Leira)
'설명		:	PF_CtlReplace
'==========================================================================*/
function PF_CtlReplace(ctl, pMode) {
    var CtlO, CtlT, value;

    CtlO = ctl;
    CtlT = ctl.replace("addl", "atxt");

    if (!pMode) {
        document.getElementById(CtlT).style.display = "block";
        document.getElementById(CtlT).style.visibility = "visible";
        //document.getElementById(CtlT).value = document.all(CtlO).options(document.all(CtlO).SelectedIndex).text;
        document.getElementById(CtlO).style.display = "none";
        document.getElementById(CtlO).style.visibility = "hidden";

    }
    else {
        document.getElementById(CtlO).style.display = "block";
        document.getElementById(CtlO).style.visibility = "visible";
        document.getElementById(CtlT).style.display = "none";
        document.getElementById(CtlT).style.visibility = "hidden";
    }
}

/*==========================================================================
'함수명		:	컨트롤대체 (2008-10-16.송수환)
'설명		:	PF_CtlReplace(수정)
'==========================================================================*/
function PF_CtlReplace2(CtlO, CtlT, pMode) {
    if (!pMode) {
        document.getElementById(CtlT).style.display = "";
        document.getElementById(CtlT).style.visibility = "visible";
        document.getElementById(CtlO).style.display = "none";
        document.getElementById(CtlO).style.visibility = "hidden";
    }
    else {
        document.getElementById(CtlO).style.display = "";
        document.getElementById(CtlO).style.visibility = "visible";
        document.getElementById(CtlT).style.display = "none";
        document.getElementById(CtlT).style.visibility = "hidden";
    }
}

/*==========================================================================
'함수명		:	드롭다운리스트가져오기 (2008-06-16.Leira)
'설명		:	PF_DropdownText
'==========================================================================*/
function PF_DropdownText(ctl) {
    if (document.all(ctl).selectedIndex > 0) return document.all(ctl).options(document.all(ctl).selectedIndex).text;
    else return "";
}

/*==========================================================================
'함수명		:	날짜범위셋팅 (2008-06-18.Leira)
'설명		:	PF_DropdownText
'==========================================================================*/
function PF_DateRange() {
    var adate = document.getElementById("addlDateStep").value.split('^');
    document.getElementById("atxtStaDt").value = adate[0];
    document.getElementById("atxtEndDt").value = adate[1];
}

/*==========================================================================
'함수명		:	드롭다운리스트에 문자열값 채우기 (2008-06-22.Leira)
'설명		:	PF_DropdownFill :: ex) AjaxFunction에서 담아온 스트링값을 드롭다운 리스트에 갱신
'변수       :   pObj(드롭다운리스트),pStrData(스트링문자열),pValue(초기값), pIndex(초기인덱스)
'==========================================================================*/
function PF_DropdownFill(pObj, pStrData, pValue, pIndex) {
    var stgTV;
    var i = 0;
    document.getElementById(pObj).length = 0;

    if (pStrData.length != 0) {
        var strList = pStrData.split('ː');
        document.getElementById(pObj).length = strList.length - 1;
        for (i = 0; i < strList.length - 1; i++) {
            stgTV = strList[i].split('^');
            document.getElementById(pObj).options[i].value = stgTV[0];
            document.getElementById(pObj).options[i].text = stgTV[1];
        }
    }
    if (pValue != "") document.getElementById(pObj).value = pValue;
    if (pValue != "") document.getElementById(pObj).selectedIndex = pIndex;
}

/*==========================================================================
'함수명		:	드롭다운리스트에 문자열값 채우기 (2008-06-22.Leira)
'설명		:	PF_DropdownFill :: ex) AjaxFunction에서 담아온 스트링값을 드롭다운 리스트에 갱신
'변수       :   pObj(드롭다운리스트),pStrData(스트링문자열),pValue(초기값), pIndex(초기인덱스)
'==========================================================================*/
function PF_DropdownFill(pObj, pStrData, pValue, pIndex) {
    var stgTV;
    var i = 0;
    document.getElementById(pObj).length = 0;

    if (pStrData.length != 0) {
        var strList = pStrData.split('ː');
        document.getElementById(pObj).length = strList.length - 1;
        for (i = 0; i < strList.length - 1; i++) {
            stgTV = strList[i].split('^');
            document.getElementById(pObj).options[i].value = stgTV[0];
            document.getElementById(pObj).options[i].text = stgTV[1];
        }
    }
    if (pValue != "") document.getElementById(pObj).value = pValue;
    if (pValue != "") document.getElementById(pObj).selectedIndex = pIndex;
}

/*==========================================================================
'함수명		:	페이징사용여부 (2008-09-09.Leira)
'설명		:	PF_DropdownFill :: ex) AjaxFunction에서 담아온 스트링값을 드롭다운 리스트에 갱신
'변수       :   pCurrentPage(클릭한인덱스)
'==========================================================================*/
function PF_PagingYn(pCurrentPage) {
    if (pCurrentPage < 0) {
        CurrentPage = 1;
        document.getElementById("pagenavig").style.display = "none"
        document.getElementById("pagenavig").style.visibility = "hidden"
    }
    else {
        CurrentPage = pCurrentPage;
        document.getElementById("pagenavig").style.display = "block"
        document.getElementById("pagenavig").style.visibility = "visible"
    }
    return pCurrentPage
}

function PF_SearchPaging() {
    if (document.getElementById("achkPaging").checked) fnPaging_Click(-1);
    else fnPaging_Click(1);
}

/*==========================================================================
'함수	:	OpenWindowsReportHtml
'설명	:	html 리포팅 뷰어
'==========================================================================*/
function OpenWindowsReportHtml(pUrl, pTitle) {
    var left = screen.width - 10;
    var top = screen.height - 10;
    var sFeatures = 'fullscreen=no, location=no, menubar=yes, status=yes, toolbar=no, titlebar=no, left=o, top=0, width=' + left + ', height=' + top + ', directories=no, resizable=yes';

    window.open(pUrl, pTitle, sFeatures);
}

/*===========================================================================================================================================
'함수명		:	페이지 버튼 및 컨트롤 권한(2008-10-17)
'설명		:	PF_PgCtlBtnAuth
'변수       :   pMode(모드),pInit(1:초기화,2:수정모드),PgCrtAuth(등록권한),PgUdtAuth(수정권한),PgDelAuth(삭제권한),PgPrtAuth(인쇄권한)
'===========================================================================================================================================*/
function PF_PgCtlBtnAuth(pMode, pInit, PgCrtAuth, PgUdtAuth, PgDelAuth, PgPrtAuth) {
    switch (pMode) {
        //추가버튼(매입매출화면등 다수)
        case "ADD":
            return (PgCrtAuth.toUpperCase() == "TRUE") ? true : false;

            break;
            //저장버튼(매입매출화면등 다수)	
        case "SAV":
            return ((PgCrtAuth.toUpperCase() == "TRUE" && pInit == "1") || (PgUdtAuth.toUpperCase() == "TRUE" && pInit == "2")) ? true : false;

            break;
            //수정버튼(매입매출화면등 다수)
        case "UDT":
            return (PgUdtAuth.toUpperCase() == "TRUE" && pInit == "2") ? true : false;

            break;
            //삭제버튼(매입매출화면등 다수)
        case "DEL":
            return (PgDelAuth.toUpperCase() == "TRUE" && pInit == "2") ? true : false;

            break;
        case "STA":
            return (PgUdtAuth.toUpperCase() == "TRUE") ? true : false;

            break;
            //컨트롤(매입매출화면등 다수)
        case "CTL":
            return ((PgCrtAuth.toUpperCase() == "TRUE" && pInit == "1") || (PgUdtAuth.toUpperCase() == "TRUE" && pInit == "2")) ? true : false;

            break;
            //그리드 저장버튼(매입매출화면등 다수)
        case "GRD_SAV":
            return ((PgCrtAuth.toUpperCase() == "TRUE" && pInit == "1") || (PgUdtAuth.toUpperCase() == "TRUE" && pInit == "2")) ? true : false;

            break;
            //그리드 삭제버튼(매입매출화면등 다수)
        case "GRD_DEL":
            return ((PgCrtAuth.toUpperCase() == "TRUE" && pInit == "1") || (PgDelAuth.toUpperCase() == "TRUE" && pInit == "2")) ? true : false;

            break;
            //그리드 컨트롤(매입매출화면등 다수)
        case "GRD_CTL":
            return ((PgCrtAuth.toUpperCase() == "TRUE" && pInit == "1") || (PgUdtAuth.toUpperCase() == "TRUE" && pInit == "2")) ? true : false;

            break;
    }

    return
}

//2014-03-13.SSH.반올림
function MathRound(nValue) {
    return Math.round(nValue);
}