(function (CTF, window, undefined) {

    CTF.UcLineMulti = function (clientInstanceName, strHidLINECD, strTargetCtrls, strDdlLINE, strJsonParameter) {

        this.ClientInstanceName = clientInstanceName;

        var objHidText = ASPxClientTextBox.Cast(strHidLINECD);
        var objDdl = ASPxClientComboBox.Cast(strDdlLINE);
        // 콤보박스 조회 파라미터
        var jsonParameter = strJsonParameter != '' ? JSON.parse(strJsonParameter) : {};

        var isLINEEndCallback = false;
        var timerLINE = null;

        var fn_OnValueChanged = function (s, e) {
            var val = s.GetValue();
            if (Trim(val) == Trim(objHidText.GetValue())) return;
            objHidText.SetValue(val);
            PerFormCallbackTarget();
        };

        // PerFormCallbak Event Target Control
        var PerFormCallbackTarget = function () {
            var jsonPart = { 'F_LINECD': objHidText.GetValue() };
            if ('' != strTargetCtrls) {
                var tCtrls = strTargetCtrls.split(';');
                if (tCtrls.length > 0) {
                    $.each(tCtrls, function (key, tCtrl) {
                        var Ctrl = ASPxClientControl.Cast(tCtrl);
                        //Ctrl.PerformCallback();
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
            isLINEEndCallback = false;
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

        var fn_OnLINEEndCallback = function (s, e) {
            if (typeof s.cpJsonParameter != 'undefined') {
                jsonParameter = JSON.parse(s.cpJsonParameter);
                isLINEEndCallback = true;

                PerFormCallbackTarget();
            }
        };

        objDdl.ValueChanged.AddHandler(fn_OnValueChanged);
        objDdl.EndCallback.AddHandler(fn_OnLINEEndCallback);
        
        // Tree용 Event
        var fn_OnSetupLineTree = function (oParam) {
            if (!isLINEEndCallback) {
                timerLINE = setTimeout(function () { fn_OnSetupLineTree(oParam); }, 1000);
            } else {
                clearTimeout(timerLINE);
                timerLINE = null;
                isLINEEndCallback = false;
                parent.parent.isTreeLINESetup = false;
                var oParams = oParam.split('|');

                fn_SetTextValue(strHidLINECD, oParams[2]);
                fn_SetSelectedItem(strDdlLINE, oParams[2]);

                PerFormCallbackTarget();
            }
        };

        this.fn_Validateline = function () {
            if (objHidText.GetValue() == "" || objHidText.GetValue() == null) {
                alert("라인을 선택하세요!!"); return false;
            } else {
                return true;
            }
        };

        this.GetValue = function () {
            return objHidText.GetValue();
        };

        this.SetValue = function (value) {
            fn_SetTextValue(strHidLINECD, value);
            fn_SetValue(strDdlLINE, value);

            PerFormCallbackTarget();
        };

        this.SetIntervalValue = function (value, objParent) {
            if (!isLINEEndCallback) {
                var self = this;
                timerLINE = setTimeout(function () { self.SetIntervalValue(value, objParent); }, 1000);
            } else {
                clearTimeout(timerLINE);
                timerLINE = null;
                isLINEEndCallback = false;
                
                fn_SetTextValue(strHidLINECD, value);
                fn_SetSelectedItem(strDdlLINE, value);

                PerFormCallbackTarget();
            }
        };

        this.Focus = function () {
            objDdl.Focus();
        };
    };

    window["CTF"] = CTF;

})(window["CTF"] || {}, window);