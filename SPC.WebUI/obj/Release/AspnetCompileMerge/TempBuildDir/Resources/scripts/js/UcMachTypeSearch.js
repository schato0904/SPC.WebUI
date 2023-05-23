(function (CTF, window, undefined) {

    CTF.UcMachTypeSearch = function (clientInstanceName, strDevGrid, strBtnSearch, strSelectedValues, _keyFieldNames, fnCallback) {

        this.ClientInstanceName = clientInstanceName;

        var objDevGrid = ASPxClientGridView.Cast(strDevGrid);
        var objBtnSearch = ASPxClientButton.Cast(strBtnSearch);
        var objSelectedValues = ASPxClientTextBox.Cast(strSelectedValues);
        var keyFieldNames = _keyFieldNames;
        var selectedValues = {};
        
        // 그리드 더블클릭 이벤트 핸들러
        var fn_OnDevGridRowDblClick = function (s, e) {            
            //selectedValues = objDevGrid.GetRowKey(e.visibleIndex);
            SetGridValues(e.visibleIndex);
            fnCallback(s, e);
        };

        // 선택한 그리드 행의 값 설정
        var SetGridValues = function (idx) {
            if (idx < 0) return null;
            var names = keyFieldNames.split(';');
            var values = objDevGrid.GetRowKey(idx).split('|');
            for (var i = 0 ; i < names.length ; ++i) {
                selectedValues[names[i]] = values[i];
            }
            objSelectedValues.SetText(JSON.stringify(selectedValues));
        };

        // 선택한 그리드 값 조회
        this.GetGridValues = function (fieldName) {
            if (typeof (fieldName) == 'undefined' || fieldName == null || fieldName == '') {
                return selectedValues;
            } else {
                return typeof (selectedValues[fieldName]) != 'undefined' ? selectedValues[fieldName] : '';
            }
        };

        // 조회버튼 클릭 이벤트 핸들러
        var fn_OnBtnSearchClick = function (s, e) {
            objDevGrid.PerformCallback('');
        };

        // 그리드 더블클릭 이벤트
        objDevGrid.RowDblClick.AddHandler(fn_OnDevGridRowDblClick);
        // 조회 버튼 클릭 이벤트
        objBtnSearch.Click.AddHandler(fn_OnBtnSearchClick);
        
        // 동적으로 높이값을 계산한다
        this.fn_AdjustSize = function () {
            var devGrid = objDevGrid;
            var height = $(document.documentElement).height() - _hMargin - $(devGrid.GetMainElement()).offset().top;
            devGrid.SetHeight(height);
        };

        // 그리드 오브젝트 반환
        this.GetGridObect = function () {
            return objDevGrid;
        };

        // 컨트롤 초기화
        function fn_InitControl() {
            this.fn_AdjustSize();
        }

        this.GetSelectedValues = function () {
            return selectedValues;
        };

        //// 컨트롤 초기화 실행
        //fn_InitControl();
    };

    window["CTF"] = CTF;

})(window["CTF"] || {}, window);

