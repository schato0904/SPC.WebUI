(function (window, $, undefined) {
    // 모듈별 스크립트 경로
    var jsPathPerModule = {
        "CTF.UcBanMulti" : '/SPC.WebUI/Resources/scripts/js/UcBanMulti.js',
        "CTF.UcLineMulti": '/SPC.WebUI/Resources/scripts/js/UcLineMulti.js',
        "CTF.UcFactMulti": '/SPC.WebUI/Resources/scripts/js/UcFactMulti.js',
        "CTF.UcSYCOD01": '/SPC.WebUI/Resources/scripts/js/UcSYCOD01.js',
        "CTF.UcMachTypeSearch": '/SPC.WebUI/Resources/scripts/js/UcMachTypeSearch.js',
        "CTF.UcDDLMachType": '/SPC.WebUI/Resources/scripts/js/UcDDLMachType.js',
        "CTF.UcCOMMCD": '/SPC.WebUI/Resources/scripts/js/UcDDLCommonCodeMulti.js?1'
    };
    var CTF = CTF || {};

    // 스크립트 동적 로딩 함수
    var loadJavascript = function (url, callback, charset) {
        var head = document.getElementsByTagName('head')[0];
        var script = document.createElement('script');
        script.type = 'text/javascript';
        if (typeof (charset) === 'undefined' || charset === null) {
            script.charset = "euc-kr";
        } else { script.charset = charset; }
        var loaded = false;
        script.onreadystatechange = function () {
            if (this.readyState == 'loaded' || this.readyState == 'complete') {
                if (loaded) {
                    return;
                }
                loaded = true;
                callback();
            }
        }
        script.onload = function () {
            callback();
        }
        script.src = url;
        head.appendChild(script);
    };

    // 지정된 네임스페이스 생성
    var createNS = function (namespace) {
        var nsparts = namespace.split(".");
        var parent = CTF;

        // we want to be able to include or exclude the root namespace so we strip
        // it if it's in the namespace
        if (nsparts[0] === "CTF") {
            nsparts = nsparts.slice(1);
        }

        // loop through the parts and create a nested namespace if necessary
        for (var i = 0; i < nsparts.length; i++) {
            var partname = nsparts[i];
            // check if the current parent already has the namespace declared
            // if it isn't, then create it
            if (typeof parent[partname] === "undefined") {
                parent[partname] = {};
            }
            // get a reference to the deepest element in the hierarchy so far
            parent = parent[partname];
        }
        // the parent is now constructed with empty namespaces and can be used.
        // we return the outermost namespace
        return parent;
    };

    // 지정된 네임스페이스의 스크립트가 로딩이 되어있는지 확인
    var existsNS = function (namespace) {
        var result = false;
        var nsparts = namespace.split(".");        
        var tempObj = window;
        for (var i = 0; i < nsparts.length; ++i) {
            result = false;
            var partname = nsparts[i];
            if (typeof tempObj[partname] === "undefined") {
                break;
            } else {
                tempObj = tempObj[partname];
            }
            result = true;
        }

        result = ( result && typeof(tempObj) === "function" ) ;

        return result;
    };

    // 지정된 네임스페이스의 생성자 가져오기
    var getConstructor = function (namespace) {
        var nsparts = namespace.split(".");
        var tempObj = window;
        for (var i = 0; i < nsparts.length; ++i) {
            var partname = nsparts[i];
            if (typeof tempObj[partname] === "undefined") {
                tempObj = null;
                break;
            } else {
                tempObj = tempObj[partname];
            }
        }
        return typeof (tempObj) === "function" ? tempObj : null;
    };

    CTF.queueList = {};         // loadJavascript 동작상태 관리 ( 'namespace' : 'status'[0:nothing, 1:loading, 2:end] )
    CTF.callbackQueue = {};     // 관리 구조 ( 'namespace' : 'callbackArray' )

    // 생성자 함수 - 없을 경우 동적 로딩
    // namespace : 네임스페이스, constructorFunction : 생성자 함수, callbackFn : 생성자 함수 획득 후 콜백 함수, errorCallbackFn : 오류 콜백 함수
    //CTF.createInstanceIfRequiredLoad = function (namespace, constructorFunction, callbackFn, errorCallbackFn) {
    CTF.loadConstructor = function (namespace, callbackFn, errorCallbackFn) {
        var constructorFunction = null;
        // 네임스페이스 존재시 생성자 획득 후, 생성자 함수를 파라미터로 콜백함수 호출
        if (existsNS(namespace)) {
            constructorFunction = new getConstructor(namespace);
            if (typeof (callbackFn) === "function") {
                callbackFn(constructorFunction);
            }
        } else { // 네임스페이스 미존재시
            // js파일 목록에 없을 경우, 오류콜백함수 호출 또는 오류 메시지 처리
            if (typeof (jsPathPerModule[namespace]) === "undefined") {
                if (typeof (errorCallbackFn) === "function") {
                    errorCallbackFn("Not Support Module");
                } else {
                    alert(namespace + " : Not Support Module");
                }
            } else { // js 파일이 있을 경우, 동적 로딩 완료 후 생성자 함수 획득하고, 콜백함수 호출
                // 로딩상태 초기화
                if (typeof (CTF.queueList[namespace]) === 'undefined') {
                    CTF.queueList[namespace] = '0';
                }
                // 콜백 큐 초기화
                CTF.callbackQueue[namespace] = CTF.callbackQueue[namespace] || [];
                // 큐에 콜백 추가
                CTF.callbackQueue[namespace].push(callbackFn);
                // 로딩상태가 최초일 경우
                if (CTF.queueList[namespace] === '0') {
                    // 로딩상태 진행중으로 변경
                    CTF.queueList[namespace] = '1';
                    // js파일 로드 실행
                    loadJavascript(jsPathPerModule[namespace],
                        // js파일 로드 후 실행할 콜백 함수
                        function () {
                            CTF.queueList[namespace] = '2';
                            if (existsNS(namespace)) {
                                constructorFunction = new getConstructor(namespace);
                                // 콜백 큐 순환하며 처리
                                while (true) {
                                    var cbf = CTF.callbackQueue[namespace].shift();
                                    if (typeof (cbf) === 'undefined' && CTF.callbackQueue[namespace].length == 0) break;
                                    else if (typeof (cbf) === 'function') {
                                        cbf(constructorFunction);
                                    }
                                }
                            } else {
                                CTF.callbackQueue[namespace] = [];
                                if (typeof (jsPathPerModule[namespace]) === "undefined") {
                                    if (typeof (errorCallbackFn) === "function") {
                                        errorCallbackFn("Not Support Module");
                                    } else {
                                        alert(namespace + " : Not Support Module");
                                    }
                                }
                            }
                    });
                }
            }
        }
    };

    window.CTF = CTF;
})(window, $);