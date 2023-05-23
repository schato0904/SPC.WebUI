(function (CTF, window, undefined) {

    CTF.UcFactMulti = function (clientInstanceName, strHidFACTCD, strTargetCtrls, strDdlFACT, _factParam) {

        this.ClientInstanceName = clientInstanceName;

        var objHidText = ASPxClientTextBox.Cast(strHidFACTCD);
        var objDdl = ASPxClientComboBox.Cast(strDdlFACT);
        
        var isFactCallbackEnd = false;
        var timerFACT = null;
        var factParam = _factParam;

        var fn_OnValueChanged = function (s, e) {
            if (s.GetItemCount() > 1) {
                var val = s.GetValue();

                if (Trim(val) == Trim(objHidText.GetValue())) return;

                objHidText.SetValue(val);
                PerFormCallbackTarget();
            }
        };

        var GetParameter = function () {
            return { 'F_FACTCD': objHidText.GetValue() };
        };

        objDdl.ValueChanged.AddHandler(fn_OnValueChanged);

        // PerFormCallbak Event Target Control
        var PerFormCallbackTarget = function () {
            var jsonPart = GetParameter();
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

        // Get FactCD
        this.fn_OnGetFactCD = function () {
            return objHidText.GetValue();
        };

        // Init
        var fn_FACTInit = function (s, e) {
            fn_OnControlDisable(s, e);
        };

        objDdl.Init.AddHandler(fn_FACTInit);

        // EndCallback
        var fn_FACTEndCallback = function (s, e) {
            if (factParam != '') {
                // 업체
                fn_SetTextValue(strHidFACTCD, factParam);
                fn_SetSelectedItem(strDdlFACT, factParam);

                isFactCallbackEnd = true;
            } else {
                ddlFACT.SetSelectedIndex(1);
                fn_SetTextValue(strDdlFACT, objDdl.GetValue());
                PerFormCallbackTarget();
            }
        };

        objDdl.EndCallback.AddHandler(fn_FACTEndCallback);

        this.GetValue = function () {
            return objHidText.GetValue();
        };

        this.SetValue = function (value) {
            fn_SetTextValue(strHidFACTCD, value);
            fn_SetValue(strDdlFACT, value);

            PerFormCallbackTarget();
        };
    };

    window["CTF"] = CTF;

})(window["CTF"] || {}, window);

