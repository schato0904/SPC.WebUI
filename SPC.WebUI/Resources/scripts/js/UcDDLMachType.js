(function (CTF, window, undefined) {

    CTF.UcDDLMachType = function (clientInstanceName, devGridLookupClientInstanceName, strSelectedValues, _keyFieldNames, fnCallback) {

        this.ClientInstanceName = clientInstanceName;

        var objDevGridLookup = ASPxClientGridLookup.Cast(devGridLookupClientInstanceName);
        var objGrid = objDevGridLookup.GetGridView();
        var objSelectedValues = ASPxClientTextBox.Cast(strSelectedValues);
        var keyFieldNames = _keyFieldNames;
        var selectedValues = {};
        
        // 선택 값 변경 처리 핸들러
        var fn_OnDevGridLookupChanged = function (s, e) {
            SetGridValues(objGrid.GetFocusedRowIndex());
            fnCallback(s, e);
        };

        // 선택한 그리드 행의 값 설정
        var SetGridValues = function (idx) {
            if (idx < 0) return null;
            var names = keyFieldNames.split(';');
            var values = objGrid.GetRowKey(idx).split('|');
            for (var i = 0 ; i < names.length ; ++i) {
                selectedValues[names[i]] = values[i];
            }
            objSelectedValues.SetText(JSON.stringify(selectedValues));
        };

        // 선택한 그리드 값 조회
        this.GetValues = function (fieldName) {
            if (typeof (fieldName) == 'undefined' || fieldName == null || fieldName == '') {
                return selectedValues;
            } else {
                return typeof (selectedValues[fieldName]) != 'undefined' ? selectedValues[fieldName] : '';
            }
        };

        // 선택값 변경 이벤트
        objDevGridLookup.ValueChanged.AddHandler(fn_OnDevGridLookupChanged);

        // 그리드 오브젝트 반환
        this.GetGridObect = function () {
            return objGrid;
        };

        // GridLookup 오브젝트 반환
        this.GetGridLookupObject = function () {
            return objDevGridLookup;
        };

        // 컨트롤 초기화
        function fn_InitControl() {
        }

        this.GetSelectedValues = function () {
            return selectedValues;
        };

        // 컨트롤 초기화 실행
        fn_InitControl();
    };

    window["CTF"] = CTF;

})(window["CTF"] || {}, window);

