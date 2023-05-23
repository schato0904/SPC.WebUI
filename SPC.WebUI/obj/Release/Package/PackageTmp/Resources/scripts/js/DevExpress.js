var selectedKeys;
var dxGrid;
var compParam = '';
var factParam = '';
var editRowIndex = 0;
var gridValids = [];
var gridValidIdx = 0;
var gridIsValid = true;

function fn_OnControlToLabel(s, e) {
    var inputElement = s.GetInputElement();
    inputElement.disabled = true;
    inputElement.readonly = true;
    inputElement.style.backgroundColor = '#ffffff';
    inputElement.style.borderColor = '#ffffff';
    var mainElement = s.GetMainElement();
    mainElement.style.backgroundColor = '#ffffff';
    mainElement.style.borderColor = '#ffffff';
}

function fn_OnControlDisable(s, e) {
    var inputElement = s.GetInputElement();
    inputElement.disabled = true;
    inputElement.readonly = true;
}

function fn_OnControlEnable(s, e) {
    var inputElement = s.GetInputElement();
    inputElement.disabled = false;
    inputElement.readonly = false;
}

function fn_OnControlDisableBox(s, e) {
    var inputElement = s.GetInputElement();
    inputElement.disabled = true;
    inputElement.readonly = true;
    inputElement.style.backgroundColor = '#EEEEEE';
    var mainElement = s.GetMainElement();
    mainElement.style.backgroundColor = '#EEEEEE';
}

function fn_OnControlEnableBox(s, e) {
    var inputElement = s.GetInputElement();
    inputElement.disabled = false;
    inputElement.readonly = false;
    inputElement.style.backgroundColor = '#ffffff';
    var mainElement = s.GetMainElement();
    mainElement.style.backgroundColor = '#ffffff';
}

function fn_OnControlRemoveAttr(s, e, attr) {
    var inputElement = s.GetInputElement();
    inputElement.removeAttribute(attr);
    var mainElement = s.GetMainElement();
    mainElement.removeAttribute(attr);
}

function fn_OnControlAddAttr(s, e, attr, evt) {
    var inputElement = s.GetInputElement();
    inputElement.setAttribute(attr, evt);
    var mainElement = s.GetMainElement();
    mainElement.setAttribute(attr, evt);
}

function fn_OnControlEnableComboBox(s, e) {
    s.SetEnabled(e);
}

function fn_OnControlEnableDateEdit(s, e) {
    s.GetCalendar().SetEnabled(e);
}

// 페이저가 없는페이지에서 오류 나지 않게 기본형
function fn_pagerPerformCallback(oParams) {
}

function fn_OnDevDocumentInit(s) {
    var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
    var createFrameElement = s.viewer.printHelper.createFrameElement;
    s.viewer.printHelper.createFrameElement = function (name) {
        var frameElement = createFrameElement.call(this, name);
        if (isChrome) {
            frameElement.addEventListener("load", function (e) {
                if (frameElement.contentDocument && frameElement.contentDocument.contentType !== "text/html")
                    frameElement.contentWindow.print();
            });
        }
        return frameElement;
    }

    fn_AdjustSize();
}



/* ================================================================================================
TITLE   : 그리드 이벤트 처리용 함수
AUTHOR  : RYU WON KYU
DATE    : 2014.10.01
---------------------------------------------------------------------------------------------------
DESC    : 그리드용 버튼 이벤트 기본형 함수(각 페이지에서 재정의 해서 사용해야함
================================================================================================ */
// 조회
function fn_OnSearchClick() { }

// 입력
function fn_OnNewClick() { }

// 수정
function fn_OnModifyClick() { }

// 취소
function fn_OnCancelClick() { }

// 저장
function fn_OnSaveClick() { }

// 삭제
function fn_OnDeleteClick() { }

// 인쇄
function fn_OnPrintClick() { }

// 엑셀
function fn_OnExcelClick() { }

// 초기화
function fn_OnRefresh() { }

// 확인
function fn_OnAcceptClick() { }

// 전체갯수
// 그리드 카운트를 위한 함수
var fn_RendorTotalCounts = {};
function fn_RendorTotalCount() {
    for (funcID in fn_RendorTotalCounts) {
        fn_RendorTotalCounts[funcID]();
    }
}
/* ================================================================================================
TITLE   : DevExpress Control Dynamic Height
AUTHOR  : RYU WON KYU
DATE    : 2014.10.01
---------------------------------------------------------------------------------------------------
DESC    : DevExpress Control 높이를 창에 맞게 리사이징한다.
================================================================================================ */
// 기본마진(버튼 영역, 상/하단 padding)
var _hMargin = 18;

// DevExpress Control Init
function fn_OnGridInit(s, e) {
    fn_AdjustSize();
}

// DevExpress Control EndCallback
function fn_OnGridEndCallback(s, e) {
    fn_OnEndCallback(s, e);
    //fn_AdjustSize();
}

$(window).bind({
    load: function () {
        fn_AdjustSize();
        fn_OnPopupAbjust(ASPxClientControl.Cast('devPopup'));
        // 호환성보기체크
        if (isCsLogin != '1' && isTrident()) {
            alert('현재 브라우저가 호환성보기로 실행중입니다.\r사이트 사용을 위해서는 호환성 보기를 꺼주세요!!');
        }
    },
    resize: function () {
        fn_AdjustSize();
        fn_OnPopupAbjust(ASPxClientControl.Cast('devPopup'));
    }
});

/* ================================================================================================
TITLE   : DevExpress Grid Get RowIndex
AUTHOR  : RYU WON KYU
DATE    : 2014.10.02
---------------------------------------------------------------------------------------------------
DESC    : DevExpress Grid 의 선택된 RowIndex를 구한다
================================================================================================ */
// 선택된 Row Key Value
var _selectedRowKeyValue = '';

function fn_GetSelectedRowIndex(_devGrid, selectedRowKeyValue) {
    //_selectedRowKeyValue = selectedRowKeyValue;
    var rowCount = _devGrid.GetVisibleRowsOnPage();
    var rowIndex = -1;
    for (var i = 0; i < rowCount; i++) {
        if (selectedRowKeyValue == _devGrid.GetRowKey(i)) {
            rowIndex = i;
            break;
        }
    }
    
    return rowIndex;
}

function fn_GetSelectedFieldValues(selectedValues) {
    if (selectedValues.length == 0) return;
    return selectedValues;
}


/* ================================================================================================
TITLE   : DevExpress Validate
AUTHOR  : RYU WON KYU
DATE    : 2014.10.02
---------------------------------------------------------------------------------------------------
DESC    : DevExpress 의 Validate 모음
================================================================================================ */
var _IsValidate = false;

/* ================================================================================================
TITLE   : DevExpress Common Function
AUTHOR  : RYU WON KYU
DATE    : 2014.10.02
---------------------------------------------------------------------------------------------------
DESC    : DevExpress 의 일반 함수 모음
================================================================================================ */
var objCtrl = '';

function fn_GetCtrl(name) {
    return ASPxClientControl.GetControlCollection().GetByName(name);
}

function fn_GetText(name) {
    return ASPxClientControl.GetControlCollection().GetByName(name).GetText();
}

function fn_Focus(name) {
    return ASPxClientControl.GetControlCollection().GetByName(name).Focus();
}

function fn_doSetGridEventAction(value) {
    hidGridAction.SetText(value);
}

function fn_OnBatchValidateDisable(ControlID, s, e) {
    var ColumnIndex = s.GetColumnByField(ControlID).index;
    e.validationInfo[ColumnIndex].isValid = true;
}

function fn_OnBatchValidate(ControlID, s, e) {
    var ColumnIndex = s.GetColumnByField(ControlID).index;

    if (e.validationInfo[ColumnIndex].value == null) {
        e.validationInfo[ColumnIndex].isValid = false;
        e.validationInfo[ColumnIndex].errorText = "필수입력";
        return false;
    }

    return true;
}

function fn_OnBatchValidateMin(ControlID, s, e, minValue) {
    var ColumnIndex = s.GetColumnByField(ControlID).index;

    if (parseInt(e.validationInfo[ColumnIndex].value, 10) < minValue) {
        e.validationInfo[ColumnIndex].isValid = false;
        e.validationInfo[ColumnIndex].errorText = minValue + "이상으로 입력";
        return false;
    }

    return true;
}

function fn_SetTextValue(ControlID, val) {
    var ctrl = ASPxClientControl.Cast(ControlID);
    if (ASPxClientUtils.IsExists(ctrl)) {
        if (typeof(ASPxClientComboBox) != 'undefined' && ctrl instanceof ASPxClientComboBox) {
            // 지정값이 공백일 경우 첫번째 값 선택하도록 처리
            if (val == '') {
                if (ctrl.GetItemCount() > 0) {
                    ctrl.SetSelectedIndex(0);
                }
                else {
                    ctrl.SetValue(val);
                }
            }
            else {
                ctrl.SetValue(val);
            }
        } else if (typeof (ASPxClientDateEdit) != 'undefined' && ctrl instanceof ASPxClientDateEdit) {
            // 날짜 필드가 아니거나, 날짜값이 유효한 경우
            if (val instanceof Date) {
                ctrl.SetDate(val);
                ctrl.SetText(val.toString('yyyy-MM-dd'));
            }
            else if (typeof (val) == 'undefined' || val == null || Trim(val) == '') {
                ctrl.SetDate(null);
            }
            else if (!isNaN(Date.parse(String(val)))) {
                if (typeof (ctrl.SetText) == "function") {
                    //ctrl.SetText(val);
                    ctrl.SetText(Date.parse(String(val)).toString('yyyy-MM-dd'));
                }
                //ctrl.SetDate(val);
                ctrl.SetDate(Date.parse(String(val)));
            }
        } else if (typeof (ctrl.SetChecked) == "function") {
            // 체크박스일 경우
            if (String(val).toLowerCase() == "true")
                ctrl.SetChecked(true);
            else 
                ctrl.SetChecked(false);
        } else {
            if (typeof (ctrl.SetText) == "function") {
                ctrl.SetText(val);
            }
            if (typeof (ctrl.SetValue) == "function") {
                ctrl.SetValue(val);
            }
        }
    }
    else if (typeof (window[ControlID]) != 'undefined' && typeof(window[ControlID].SetValue) == 'function' ) {
        window[ControlID].SetValue(val);
    }
}

function fn_SetValue(ControlID, val) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        ASPxClientControl.Cast(ControlID).SetValue(val);
    }
}

function fn_SetSelectedItem(ControlID, val) {
    if (typeof (val) == 'undefined' || val == null || val == '') {
        var ctrl = ASPxClientComboBox.Cast(ControlID)
        ctrl.SetSelectedIndex(0);
    }
    else if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        //var ctrl = ASPxClientControl.Cast(ControlID);
        var ctrl = ASPxClientComboBox.Cast(ControlID)
        ctrl.SetSelectedIndex(ctrl.FindItemByValue(val).index);
    }
}

function fn_SetDate(ControlID, val) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        ASPxClientControl.Cast(ControlID).SetDate(val);
    }
}

function fn_SetNullText(Control, val) {
    var devInputCtrl = Control.GetInputElement();
    if (devInputCtrl.value == val || devInputCtrl.value == '' || devInputCtrl.value == null) {
        devInputCtrl.value = val;
        devInputCtrl.style.color = 'gray';
    } else {
        devInputCtrl.style.color = 'black';
    }
}

function fn_RemoveNullText(Control, val) {
    var devInputCtrl = Control.GetInputElement();
    if (devInputCtrl.value != null && devInputCtrl.value != '' && devInputCtrl.value == val)
            devInputCtrl.value = null;
    devInputCtrl.style.color = 'black';
}

function fn_GetCastText(ControlID) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return ASPxClientControl.Cast(ControlID).GetText() == null ? '' : ASPxClientControl.Cast(ControlID).GetText();
    } else {
        return null;
    }
}

function fn_GetCastValue(ControlID) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return ASPxClientControl.Cast(ControlID).GetValue() == null ? '' : ASPxClientControl.Cast(ControlID).GetValue();
    } else {
        return null;
    }
}

function fn_GetCastSelectedItemText(ControlID) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return ASPxClientControl.Cast(ControlID).GetSelectedItem() == null ? '' : ASPxClientControl.Cast(ControlID).GetSelectedItem().text == null ? '' : ASPxClientControl.Cast(ControlID).GetSelectedItem().text;
    } else {
        return null;
    }
}

function fn_GetCastSelectedItemValue(ControlID) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return ASPxClientControl.Cast(ControlID).GetSelectedItem() == null ? '' : ASPxClientControl.Cast(ControlID).GetSelectedItem().value == null ? '' : ASPxClientControl.Cast(ControlID).GetSelectedItem().value;
    } else {
        return null;
    }
}

function fn_GetCastSelectedItemsValue(ControlID, Separator) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        if (ASPxClientControl.Cast(ControlID).GetSelectedItems() == null) {
            return '';
        } else if (ASPxClientControl.Cast(ControlID).GetSelectedItems().length == 0) {
            return ''
        } else {
            var texts = '';
            var selectItems = ASPxClientControl.Cast(ControlID).GetSelectedItems();
            for (var i = 0; i < selectItems.length; i++) {
                if (i > 0) texts += Separator;
                texts += selectItems[i].value;
            }

            return texts;
        }
    } else {
        return null;
    }
}

function fn_GetCastDate(ControlID) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return ASPxClientControl.Cast(ControlID).GetDate();
    } else {
        return null;
    }
}

function fn_SetChecked(ControlID, bChecked) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return ASPxClientControl.Cast(ControlID).SetChecked(bChecked);
    }
}

function fn_GetCastChecked(ControlID) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return ASPxClientControl.Cast(ControlID).GetChecked();
    } else {
        return null;
    }
}

// RowKeys Null to Empty
function fn_OnRowKeysNullValueToEmpty(rowKeys) {
    var resultKeys = [];
    $.each(rowKeys.split('|'), function (idx, rowKey) {
        if (rowKey.toLowerCase().indexOf('~xtra') >= 0)
            resultKeys[idx] = '';
        else
            resultKeys[idx] = rowKey;
    });

    return resultKeys.join('|');
}

// RowKeys Null to Empty
function fn_OnRowKeysNullValueToEmptyWithEncode(rowKeys) {
    var resultKeys = [];
    $.each(rowKeys.split('|'), function (idx, rowKey) {
        if (rowKey.toLowerCase().indexOf('~xtra') >= 0)
            resultKeys[idx] = '';
        else
            resultKeys[idx] = encodeURIComponent(rowKey);
    });

    return resultKeys.join('|');
}

/* ================================================================================================
TITLE   : DevExpress Popup Control
AUTHOR  : RYU WON KYU
DATE    : 2014.10.22
---------------------------------------------------------------------------------------------------
DESC    : DevExpress 의 Popup Contorl 함수 모음
================================================================================================ */
// Popup Control
var isVisibled = false;

function fn_OnPopupOpen(pPage, pWidth, pHeight) {
    var devPopupCtrl = ASPxClientControl.Cast('devPopup');
    var popupCtrlWidth = ASPxClientControl.Cast('popupCtrlWidth');
    var popupCtrlHeight = ASPxClientControl.Cast('popupCtrlHeight');
    var popupWidth = 0;
    var popupHeight = 0;

    if (parseInt(pWidth, 10) <= 0 && parseInt(pHeight, 10) <= 0) {
        popupWidth = $(document).width() - 20;
        popupHeight = $(document).height() - 20;
    } else if (parseInt(pWidth, 10) <= 0) {
        popupCtrlHeight.SetText(pHeight);
        popupWidth = $(document).width() - 20;
        popupHeight = parseInt(pHeight, 10);
    } else if (parseInt(pHeight, 10) <= 0) {
        popupCtrlWidth.SetText(pWidth);
        popupWidth = parseInt(pWidth, 10);
        popupHeight = $(document).height() - 20;
    } else {
        popupCtrlWidth.SetText(pWidth);
        popupCtrlHeight.SetText(pHeight);
        popupWidth = parseInt(pWidth, 10);
        popupHeight = parseInt(pHeight, 10);
    }

    devPopupCtrl.SetSize(popupWidth, popupHeight);

    if (!isVisibled) {
        devPopupCtrl.SetContentUrl(pPage);
        devPopupCtrl.Show();
    } else {
        devPopupCtrl.Hide();
    }
}

function fn_OnPopupAbjust(s) {
    var isPopup = window.parent.location.href.toLowerCase().indexOf('tabframe.aspx') < 0;
    
    if (!isPopup) {
        var popupCtrlWidth = ASPxClientControl.Cast('popupCtrlWidth');
        var popupCtrlHeight = ASPxClientControl.Cast('popupCtrlHeight');
        var popupWidth = 0;
        var popupHeight = 0;

        if (parseInt(popupCtrlWidth.GetText(), 10) <= 0 && parseInt(popupCtrlHeight.GetText(), 10) <= 0) {
            popupWidth = $(document).width() - 20;
            popupHeight = $(document).height() - 20;
            s.SetSize(popupWidth, popupHeight);
        } else if (parseInt(popupCtrlWidth.GetText(), 10) <= 0) {
            popupWidth = $(document).width() - 20;
            popupHeight = parseInt(popupCtrlHeight.GetText(), 10);
            s.SetSize(popupWidth, popupHeight);
        } else if (parseInt(popupCtrlHeight.GetText(), 10) <= 0) {
            popupWidth = parseInt(popupCtrlWidth.GetText(), 10);
            popupHeight = $(document).height() - 20;
            s.SetSize(popupWidth, popupHeight);
        }
    }
}

function fn_OnPopupInit(s, e) {
    fn_OnPopupAbjust(s);
}

function fn_OnPopupEndCallback(s, e) {
    fn_OnPopupAbjust(s);
}

/* ================================================================================================
TITLE   : DevExpress AspxEditor Control Regex
AUTHOR  : RYU WON KYU
DATE    : 2014.10.29
---------------------------------------------------------------------------------------------------
DESC    : DevExpress 의 AspxEditor Contorl Regex 함수 모음
================================================================================================ */
function fn_RemoveHangul(s, e) {
    var devInputCtrl = s.GetInputElement();
    devInputCtrl.value = devInputCtrl.value.replace(/[\ㄱ-ㅎㅏ-ㅣ가-힣]/g, '');
}

function fn_ValidateOnlyNumber(s, e) {
    var theEvent = e.htmlEvent || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9\b\t]|\-/;

    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault)
            theEvent.preventDefault();
    }
}

function fn_ValidateOnlyFloat(s, e) {
    var theEvent = e.htmlEvent || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9\b\t]|\-|\./;

    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault)
            theEvent.preventDefault();
    }
}

function fn_ValidateOnlyNumberAbs(s, e) {
    var theEvent = e.htmlEvent || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9\b\t]/;

    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault)
            theEvent.preventDefault();
    }
}

function fn_ValidateOnlyFloatAbs(s, e) {
    var theEvent = e.htmlEvent || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9\b\t]|\./;

    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault)
            theEvent.preventDefault();
    }
}

function isNumeric(num, opt) {
    // 좌우 trim(공백제거)을 해준다.
    num = String(num).replace(/^\s+|\s+$/g, "");
 
    if(typeof opt == "undefined" || opt == "1"){
        // 모든 10진수 (부호 선택, 자릿수구분기호 선택, 소수점 선택)
        var regex = /^[+\-]?(([1-9][0-9]{0,2}(,[0-9]{3})*)|[0-9]+){1}(\.[0-9]+)?$/g;
    }else if(opt == "2"){
        // 부호 미사용, 자릿수구분기호 선택, 소수점 선택
        var regex = /^(([1-9][0-9]{0,2}(,[0-9]{3})*)|[0-9]+){1}(\.[0-9]+)?$/g;
    }else if(opt == "3"){
        // 부호 미사용, 자릿수구분기호 미사용, 소수점 선택
        var regex = /^[0-9]+(\.[0-9]+)?$/g;
    }else{
        // only 숫자만(부호 미사용, 자릿수구분기호 미사용, 소수점 미사용)
        var regex = /^[0-9]$/g;
    }
 
    if( regex.test(num) ){
        num = num.replace(/,/g, "");
        return isNaN(num) ? false : true;
    }else{ return false;  }
}

function fn_isNumeric(ControlID) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return isNumeric(ASPxClientControl.Cast(ControlID).GetText());
    } else {
        return false;
    }
}

function fn_isNumber(ControlID) {
    if (ASPxClientUtils.IsExists(ASPxClientControl.Cast(ControlID))) {
        return isNumeric(ASPxClientControl.Cast(ControlID).GetText(), 0);
    } else {
        return false;
    }
}

/* ================================================================================================
TITLE   : undefined도 null과 동일하게 처리
AUTHOR  : KIM S
DATE    : 2014.11.17
---------------------------------------------------------------------------------------------------
DESC    : obj - 대상 값, obj가 undefined 또는 null 일때 대체 값
================================================================================================ */
function IsNull(obj, alt) {
    return typeof (obj) == 'undefined' || obj == null ? alt : obj;
}

// 그리드뷰 높이 조절 : 화면 크기에 맞게 자동 조절, 최소높이 100px 유지
// 사용 예 : DevExpress ASPxGridView 클라이언트 이벤트 Init에 설정
// <dx:ASPxGridView ID="grdList1" ...... <ClientSideEvents RowDblClick="grdView_RowDblClick" Init="fnASPxGridView_Init" />
function fnASPxGridView_ReHeight_ByContainer(s, e) {
    var grdView = ASPxClientControl.Cast(s);
    grdView.SetHeight(100);
    var container = grdView.GetMainElement().parentNode;
    var containerHeight = $(container).height();
    var targetHeight = containerHeight + $(container).offset().top - $(grdView.GetMainElement()).offset().top;
    var minHeight = 100;
    if (targetHeight < minHeight) targetHeight = minHeight;
    grdView.SetHeight(targetHeight);
}

function fnASPxGridView_ReHeight(s, e, paddingBottom) {
    //var g = ASPxClientGridView.Cast(s);
    fnASPxGridView_ReHeightValue(s, $(s.GetMainElement()).offset().top, 200, paddingBottom);
}

function fnASPxGridView_ReHeightValue(s, offsetTop, minHeight, paddingBottom) {
    //var g = ASPxClientGridView.Cast(s);
    paddingBottom = parseInt(Trim(paddingBottom), 10);
    paddingBottom = isNaN(paddingBottom) ? 0 : paddingBottom;
    var targetHeight = $(document.documentElement).height() - offsetTop - _hMargin - paddingBottom;
    s.SetHeight(targetHeight < minHeight ? minHeight : targetHeight);
}