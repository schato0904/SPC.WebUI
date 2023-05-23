(function (CTF, window, undefined) {

    CTF.UcBanMulti = function (clientInstanceName, strHidBANCD, strTargetCtrls, strDdlBAN, strJsonParameter ) {
        // ClientInstanceName : 스크립트를 처리할 객체 오브젝트 명으로 사용
        this.ClientInstanceName = clientInstanceName;

        // 이하 private 멤버
        // 선택값 저장 텍스트 박스
        var objHidText = ASPxClientTextBox.Cast(strHidBANCD);
        // 콤보박스
        var objDdl = ASPxClientComboBox.Cast(strDdlBAN);
        // 콤보박스 조회 파라미터
        var jsonParameter = strJsonParameter != '' ? JSON.parse(strJsonParameter) : {};

        // 콤보박스 EndCallback 이벤트 핸들러
        var EndCallback = function (s, e) {
            if (typeof s.cpJsonParameter != 'undefined') {
                jsonParameter = JSON.parse(s.cpJsonParameter);
            }
        };

        var fn_OnValueChanged = function (s, e) {
            var val = s.GetValue();
            
            if (Trim(val) == Trim(objHidText.GetValue())) return;

            objHidText.SetValue(val);
            PerFormCallbackTarget();
        };
        
        // PerFormCallbak Event Target Control
        var PerFormCallbackTarget = function () {
            var jsonPart = { 'F_BANCD': objHidText.GetValue() };
            if ('' != strTargetCtrls) {
                var tCtrls = strTargetCtrls.split(';');
                if (tCtrls.length > 0) {
                    $.each(tCtrls, function (key, tCtrl) {
                        var Ctrl = ASPxClientControl.Cast(tCtrl);
                        // 대상 컨트롤이 dev 컨트롤이면 파라미터를 문자열로 변환 후 Callback 호출
                        if (ASPxClientUtils.IsExists(Ctrl) && Ctrl.isASPxClientControl) {
                            Ctrl.PerformCallback(JSON.stringify(jsonPart));
                        // PerformCallback을 구현한 사용자 컨트롤이면, json파라미터를 사용하여 Callback 호출
                        } else if (typeof window[tCtrl] != 'undefined' && typeof window[tCtrl].PerformCallback == 'function') {
                            window[tCtrl].PerformCallback(jsonPart);
                        } else {
                            alert('대상 컨트롤이 존재하지 않습니다.');
                        }
                    });
                }
            }
        };

        // 이하 public 멤버
        this.PerformCallback = function (jsonPart) {
            // 넘겨받은 파라미터가 기존 조회한 파라미터와 다르면, 기존 파라미터와 새 파라미터를 병합하여 Callback 호출
            var isChanged = false;
            var callbackParameter = JSON.stringify({});
            for (var key in jsonPart) {
                if (typeof jsonParameter[key] != 'undefined' && jsonParameter[key] != jsonPart[key]) {
                    jsonParameter[key] = jsonPart[key];
                    isChanged = true;
                }
            }
            if (isChanged) {
                callbackParameter = JSON.stringify(jsonParameter);
                objDdl.PerformCallback(callbackParameter);
            }
        };

        // Tree용 Event
        this.fn_OnSetupBanTree = function (oParam) {
            var oParams = oParam.split('|');

            fn_SetTextValue(strHidBANCD, oParams[0]);
            fn_SetSelectedItem(strDdlBAN, oParams[0]);

            PerFormCallbackTarget();
        }

        this.fn_Validateban = function () {
            if (objHidText.GetValue() == "" || objHidText.GetValue() == null) {
                alert("반을 선택하세요!!"); return false;
            } else {
                return true;
            }
        }

        this.GetValue = function () {
            return objHidText.GetValue();
        };

        this.SetValue = function (value) {
            fn_SetTextValue(strHidBANCD, value);
            fn_SetValue(strDdlBAN, value);

            PerFormCallbackTarget();
        };

        this.Focus = function () {
            objDdl.Focus();
        };
        
        // 이하 이벤트 핸들러 연결
        objDdl.EndCallback.AddHandler(EndCallback);
        objDdl.ValueChanged.AddHandler(fn_OnValueChanged);
    };

    window["CTF"] = CTF;

})(window["CTF"] || {}, window);

