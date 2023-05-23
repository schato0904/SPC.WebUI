(function (CTF, window, undefined) {

    CTF.UcSYCOD01 = function (clientInstanceName, strHidSYCOD01CD, strTargetCtrls, strDdlSYCOD01, strJsonParameter) {

        this.ClientInstanceName = clientInstanceName;

        var objHidText = ASPxClientTextBox.Cast(strHidSYCOD01CD);
        var objDdl = ASPxClientComboBox.Cast(strDdlSYCOD01);
        // 콤보박스 조회 파라미터
        var jsonParameter = strJsonParameter != '' ? JSON.parse(strJsonParameter) : {};

        // 콤보박스 EndCallback 이벤트 핸들러
        var EndCallback = function (s, e) {
            if (typeof s.cpJsonParameter != 'undefined') {
                jsonParameter = JSON.parse(s.cpJsonParameter);
            }
        };
        objDdl.EndCallback.AddHandler(EndCallback);

        var fn_OnValueChanged = function (s, e) {
            var val = s.GetValue();
            if (Trim(val) == Trim(objHidText.GetValue())) return;
            objHidText.SetValue(val);
            PerFormCallbackTarget();
        };
        
        objDdl.ValueChanged.AddHandler(fn_OnValueChanged);

        // PerFormCallbak Event Target Control
        var PerFormCallbackTarget = function () {
            var jsonPart = { 'F_CODE': objHidText.GetValue() };
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
                objDdlBAN.PerformCallback(callbackParameter);
            }
        };

        this.GetValue = function () {
            return objHidText.GetValue();
        };

        this.SetValue = function (value) {
            fn_SetTextValue(strHidSYCOD01CD, value);
            fn_SetValue(strDdlSYCOD01, value);

            PerFormCallbackTarget();
        };

        this.SetEnabled = function (value) {
            objDdl.SetEnabled(value);
        };

        this.Focus = function () {
            objDdl.Focus();
        };
    };

    window["CTF"] = CTF;

})(window["CTF"] || {}, window);

