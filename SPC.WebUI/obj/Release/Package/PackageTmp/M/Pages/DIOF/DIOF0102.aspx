<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DIOF0102.aspx.cs" Inherits="SPC.WebUI.M.Pages.DIOF.DIOF0102" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../jquery/jquery.mobile-1.4.5.min.css" />
    <link rel="stylesheet" href="../../jquery/swiper.min.css" />
    <link rel="stylesheet" href="../../css/mobileStyle.css" />

    <script src="../../jquery/jquery-1.12.4.min.js"></script>
    <script src="../../jquery/jquery.mobile-1.4.5.min.js"></script>
    <script src="../../jquery/jquery.dataTables.min.js"></script>
    <script src="../../script/mobileScript.js"></script>
    <script src="../../jquery/swiper.min.js"></script>

    <script type="text/javascript">
        var oHeight;
        var objid;
        var slideobjNum;
        var slideobjNum_POP;
        var jsonObj;
        var oParams;
        var swiper;
        var tabChk = false;
        $(document).ready(function () {
            oParams = getUrlParams();
            $("#txtDate").val(oParams.DATE);
            //$("#txtDate").val("2017-12-03");

            oHeight = $("body").height();
            $(".swiper-wrapper").height(oHeight - $("#header").height())

            $(document).keydown(function (e) {

                if (e.keyCode == "9") {
                    if (jsonObj[slideobjNum].F_NUMBER == jsonObj[slideobjNum].F_CHASU) {
                        if ($("#txtMEASURE_" + slideobjNum).val() != "") {
                            jsonObj[slideobjNum].F_INPUTMEASURE = $("#txtMEASURE_" + slideobjNum).val();
                            if (parseInt($("#txtMAX_" + slideobjNum).val()) < parseInt($("#txtMEASURE_" + slideobjNum).val()) || parseInt($("#txtMIN_" + slideobjNum).val()) > parseInt($("#txtMEASURE_" + slideobjNum).val())) {
                                $("#popupDialog").popup("open");
                                return false;
                            }
                            else {
                                swiper.slideNext(300, null);
                                return false;
                            }
                        }
                    }
                }
            });

            //설비명 조회
            $.ajax({
                type: "POST",
                url: "../../../API/Common/Websvc/mobileSvc.asmx/MACH21_LST",
                data: "{'MACHIDX' : '" + oParams.MACHIDX + "' }",
                //data: "{'MACHIDX' : '01' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonData) {
                    var json = eval("(" + jsonData.d + ")");
                    var ItemList = "";
                    $(json).each(function (i) {
                        $("#txtMach").val(this.F_MACHNM);
                    });

                },
                error: function () {
                    alert("Error : MACH21_LST");
                }
            });

            //이상유형 조회
            $.ajax({
                type: "POST",
                url: "../../../API/Common/Websvc/mobileSvc.asmx/SYCOD01_LST",
                data: "{'CODEGROUP' : '41' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonData) {
                    var json = eval("(" + jsonData.d + ")");
                    var ItemList = "";
                    $(json).each(function (i) {
                        if (i == 0) {
                            ItemList = '<option value="' + this.F_CODE + '" selected="selected">' + this.F_CODENM + '</option>'
                        } else {
                            ItemList = '<option value="' + this.F_CODE + '" >' + this.F_CODENM + '</option>'
                        }

                        $("#txtNGTYPE").append(ItemList);
                    });

                    $("#txtNGTYPE").selectmenu();
                    $("#txtNGTYPE").selectmenu('refresh', true);
                },
                error: function () {
                    alert("Error : SYCOD01_LST");
                }
            });

            //점검항목조회
            $.ajax({
                type: "POST",
                url: "../../../API/Common/Websvc/mobileSvc.asmx/MACH23_MACH26_LST",
                data: "{'MACHIDX' : '" + oParams.MACHIDX + "', 'DATE' : '" + oParams.DATE + "'}",
                //data: "{'MACHIDX' : '01', 'DATE' : '2017-11-06'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonData) {
                    var json = eval("(" + jsonData.d + ")");
                    fn_Createobj(json);
                },
                error: function () {
                    alert("Error : MACH23_MACH26_LST");
                }
            });

            //저장
            $("#btnSave").click(function () {
                var cnt = 0;
                $(jsonObj).each(function () {
                    if (isEmpty(this.F_INPUTMEASURE)) {
                        alert("측정값을 모두 입력해 주세요.");
                        cnt++;
                        return false;
                    }
                });

                if (cnt == 0) {
                    //F_MEASYMD
                    $(jsonObj).each(function () {
                        this.F_MEASYMD = $("#txtDate").val();
                        this.F_BANCD = oParams.BANCD;
                        this.F_LINECD = oParams.LINECD;
                        //this.F_BANCD = "01";
                        //this.F_LINECD = "01";
                        this.F_STATUS = "AAG901";
                    });

                    $.ajax({
                        type: "POST",
                        url: "../../../API/Common/Websvc/mobileSvc.asmx/MACH23_INS",
                        data: "{'JSON' : '" + JSON.stringify(jsonObj) + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultMsg) {
                            var json = eval("(" + resultMsg.d + ")");
                            alert(json)

                            window.location.href = "DIOF0103.aspx?DATE=" + oParams.DATE + "&MACHIDX=" + oParams.MACHIDX;
                        },
                        error: function () {
                            alert("Error : MACH23_INS");
                        }
                    });
                }
            });

            /*이상발생 팝업창 버튼 이벤트*/
            $("#btnpopSave").click(function () {
                if ($("#txtNGTYPE").val() == "" || $("#txtNGREMARK").val() == "" || $("#txtNGREMARKCON").val() == "") {
                    alert("항목을 모두 입력해 주세요");
                    return;
                } else {
                    jsonObj[slideobjNum_POP].F_INPUTMEASURE = $("#txtMEASURE_" + slideobjNum_POP).val();
                    jsonObj[slideobjNum_POP].F_NGTYPE = $("#txtNGTYPE").val();
                    jsonObj[slideobjNum_POP].F_NGREMK = $("#txtNGREMARK").val();
                    $("#popupDialog").popup("close")
                }
            });

            $("#btnpopCancel").click(function () {
                $("#btnNG_" + slideobjNum_POP).button("disable");
                $("#btnOK_" + slideobjNum_POP).button("disable");
                $("#txtMEASURE_" + slideobjNum_POP).val("")

                jsonObj[slideobjNum_POP].F_INPUTMEASURE = "";
                jsonObj[slideobjNum_POP].F_NGTYPE = "";
                jsonObj[slideobjNum_POP].F_NGREMK = "";

                $("#popupDialog").popup("close")
            });


            $("#txtNGREMARK").textinput("option", "autogrow", false);
        });

        /* slide div 및 점검항목 table생성*/
        function fn_Createobj(data) {
            jsonObj = data;
            var tableElement = "";

            for (var i = 0; i < data.length ; i++) {
                var val = "";
                tableElement += "<div class='swiper-slide'>";
                tableElement += "<div data-role='fieldcontain' style='width:100%' >";
                tableElement += "<table><tbody>";
                tableElement += "<tr><td><label class='td-title'>점검부위</label></td><td><label class='td-content'>" + data[i].F_INSPNO + "</label></td></tr>";
                tableElement += "<tr><td><label class='td-title'>점검항목</label></td><td><label class='td-content'>" + data[i].F_INSPNM + "</label></td></tr>";
                tableElement += "<tr><td><label class='td-title'>점검내용</label></td><td><label class='td-content'>" + data[i].F_INSPREMARK + "</label></td></tr>";
                tableElement += "<tr><td><label class='td-title'>점검방법</label></td><td><label class='td-content'>" + data[i].F_INSPWAY + "</label></td></tr>";
                tableElement += "<tr><td><label class='td-title'>점검규격</label></td><td><label class='td-content'>" + data[i].F_VIEWSTAND + "</label></td></tr>";

                var chasu = data[i].F_CHASU
                for (var j = 0; j < chasu; j++) {
                    switch (data[i].F_CYCLECD) {
                        case "AAG401": if (data[i].F_CHASU == 1) val = "1일"; else val = data[i].F_NUMBER + "차";
                            break;
                        case "AAG407": if (data[i].F_NUMBER == 1) val = "주간"; else val = "야간";
                            break;
                        default:
                            val = data[i].F_CYCLENM;
                            break;
                    }

                    if (data[i].F_INSPKINDCD == "AAG601") {
                        tableElement += "<tr>" +
                                            "<td class='td-title'>" + val + "</td>" +
                                            "<td>" +
                                                "<div class='ui-grid-a'>" +
                                                "<div class='ui-block-a' >" +
                                                    "<input data-mini='true'  type='number' id='txtMEASURE_" + i + "' onfocus='fn_inPutMeasurefocus(this)' onblur='fn_inPutMeasure(this)' />" +
                                                "</div>" +
                                                "<div class='ui-block-b' >" +
                                                    "<a id='lblOK_" + i + "'' >" +
                                                        "<input type='button' id='btnOK_" + i + "' data-mini='true' value='OK' disabled='disabled' />" +
                                                    "</a>" +
                                                    "<a style='display:none' id='lblNG_" + i + "' for='btnNG' onclick='fn_btnClick(this)' >" +
                                                        "<input type='button'  id='btnNG_" + i + "' data-mini='true' value='NG' disabled='disabled' />" +
                                                    "</a>" +
                                                "</div>" +
                                            "</td>" +
                                        "</tr>" +
                                        "<input type='hidden' class='txt-max' value='" + data[i].F_MAX + "' id='txtMAX_" + i + "' />" +
                                        "<input type='hidden' class='txt-min' value='" + data[i].F_MIN + "' id='txtMIN_" + i + "' />";
                    }
                    else {
                        tableElement += "<tr>" +
                                            "<td>" +
                                                "<label class='td-title'>" + val + "</label>" +
                                            "</td>" +
                                            "<td>" +
                                                "<div class='ui-grid-a'>" +
                                                    "<div class='ui-block-a'>" +
                                                        "<input type='hidden' id='txtMEASURE_" + i + "' />" +
                                                        "<label id='lblOK_" + i + "' for='btnOK' >" +
                                                            "<input type='button' id='btnOK_" + i + "' data-mini='true' value='OK' disabled='disabled' />" +
                                                        "</label>" +
                                                    "</div>" +
                                                    "<div class='ui-grid-b'>" +
                                                        "<label id='lblNG_" + i + "' for='btnNG' >" +
                                                            "<input type='button' id='btnNG_" + i + "' data-mini='true' value='NG' disabled='disabled' />" +
                                                        "</label>" +
                                                    "</div>" +
                                                "</div>" +
                                            "</td>" +
                                        "</tr>";
                    }

                    i++;
                }

                //차수만큼 반복 증가 하는 i값을 맞춤
                //if (data.length - 1 != i) {
                //    i--;
                //}
                i--;

                tableElement += "</tbody></table>";
                tableElement += "</div>";
                tableElement += "</div>";
            }


            $(".swiper-wrapper").append(tableElement);

            swiper = new Swiper('.swiper-container', {
                pagination: {
                    el: '.swiper-pagination',
                    type: 'progressbar',
                },
                navigation: {
                    nextEl: '.swiper-button-next',
                    prevEl: '.swiper-button-prev',
                }
            });

            $('.swiper-slide').page();
            //$(".swiper-wrapper").height(oHeight - $("#header").height()-58)

            // 외관검사 측정버튼 클릭시
            $('.swiper-slide table div label').click(function () {
                var objid = $(this).attr('id');
                slideobjNum = objid.substring(objid.indexOf("_") + 1);

                if (objid.substring(3, objid.indexOf("_")) == "OK") {
                    jsonObj[slideobjNum].F_NGTYPE = "";
                    jsonObj[slideobjNum].F_NGREMK = "";
                    jsonObj[slideobjNum].F_INPUTMEASURE = "OK";
                    jsonObj[slideobjNum].F_INPUTJUDGE = "AAG701";

                    $("#btnNG_" + slideobjNum).button("disable");
                    $("#btnOK_" + slideobjNum).button("enable");
                    $("#txtMEASURE_" + slideobjNum).val("OK")

                } else {
                    $("#txtNGTYPE").val(jsonObj[slideobjNum].F_NGTYPE).selectmenu('refresh', true);
                    $("#txtNGREMARK").val(jsonObj[slideobjNum].F_NGREMK);
                    $("#btnNG_" + slideobjNum).button("enable");
                    $("#btnOK_" + slideobjNum).button("disable");
                    $("#txtMEASURE_" + slideobjNum).val("NG")
                    jsonObj[slideobjNum].F_INPUTMEASURE = "NG";
                    jsonObj[slideobjNum].F_INPUTJUDGE = "AAG702";
                    jsonObj[slideobjNum].F_NGTYPE = "";
                    jsonObj[slideobjNum].F_NGREMK = "";

                    //$("#popupDialog p:first-child").text('점검항목 : ' + jsonObj[slideobjNum].F_INSPNM);
                    //$("#popupDialog p:last-child").text('차수 : ' + jsonObj[slideobjNum].F_NUMBER);
                    slideobjNum_POP = slideobjNum;
                    $("#popupDialog").popup("open");
                }

                //alert(jsonObj[slideobjNum].F_INPUTMEASURE)
            });
        }

        function fn_inPutMeasurefocus(obj) {
            objid = $(obj).attr("id");
            slideobjNum = objid.substring(objid.indexOf("_") + 1);
        }

        // 치수검사 Data입력
        function fn_inPutMeasure(obj) {
            //var objid = $(obj).attr("id");
            //slideobjNum = objid.substring(objid.indexOf("_") + 1);

            if ($("#txtMEASURE_" + slideobjNum).val() != "") {
                jsonObj[slideobjNum].F_INPUTMEASURE = $("#txtMEASURE_" + slideobjNum).val();
                //alert(jsonObj[slideobjNum].F_INPUTMEASURE)
                if (parseInt($("#txtMAX_" + slideobjNum).val()) < parseInt($("#" + objid).val()) || parseInt($("#txtMIN_" + slideobjNum).val()) > parseInt($("#" + objid).val())) {
                    //NG
                    $("#txtNGTYPE").val(jsonObj[slideobjNum].F_NGTYPE).selectmenu('refresh', true);
                    $("#txtNGREMARK").val(jsonObj[slideobjNum].F_NGREMK);

                    $("#btnOK_" + slideobjNum).button("disable");
                    $("#btnNG_" + slideobjNum).button("enable");

                    $("#lblOK_" + slideobjNum).css("display", "none");
                    $("#lblNG_" + slideobjNum).css("display", "");

                    slideobjNum_POP = slideobjNum;
                    jsonObj[slideobjNum].F_INPUTJUDGE = "AAG702";
                } else {
                    //OK                    
                    jsonObj[slideobjNum].F_INPUTJUDGE = "AAG701";

                    $("#btnOK_" + slideobjNum).button("enable");
                    $("#btnNG_" + slideobjNum).button("disable");

                    $("#lblOK_" + slideobjNum).css("display", "");
                    $("#lblNG_" + slideobjNum).css("display", "none");
                }
                jsonObj[slideobjNum].F_NGTYPE = "";
                jsonObj[slideobjNum].F_NGREMK = "";

            } else {
                //아무것도 입력 없을시 초기화
                jsonObj[slideobjNum].F_NGTYPE = "";
                jsonObj[slideobjNum].F_NGREMK = "";
                jsonObj[slideobjNum].F_INPUTMEASURE = "";
                jsonObj[slideobjNum].F_INPUTJUDGE = "";

                $("#btnOK_" + slideobjNum).button("disable");
                $("#btnNG_" + slideobjNum).button("disable");

                $("#lblOK_" + slideobjNum).css("display", "");
                $("#lblNG_" + slideobjNum).css("display", "none");
            }
        }

        function fn_btnClick(obj) {
            objid = $(obj).attr('id');
            slideobjNum = objid.substring(objid.indexOf("_") + 1);

            if ($("#txtMEASURE_" + slideobjNum).val() != "") {
                if (parseInt($("#txtMAX_" + slideobjNum).val()) < parseInt($("#txtMEASURE_" + slideobjNum).val()) || parseInt($("#txtMIN_" + slideobjNum).val()) > parseInt($("#txtMEASURE_" + slideobjNum).val())) {
                    $("#txtNGTYPE").val(jsonObj[slideobjNum].F_NGTYPE).selectmenu('refresh', true);
                    $("#txtNGREMARK").val(jsonObj[slideobjNum].F_NGREMK);
                    //$("#btnNG_" + objnum).button("enable");
                    //$("#btnOK_" + objnum).button("disable");
                    $("#btnNG_" + slideobjNum).val("NG");
                    $("#InspNm").text('점검항목 : ' + jsonObj[slideobjNum].F_INSPNM);
                    $("#InspCnt").text('차수 : ' + jsonObj[slideobjNum].F_NUMBER);
                    $("#popupDialog").popup("open");
                    slideobjNum_POP = slideobjNum;
                }
            } else {
                $("#btnNG_" + slideobjNum).button("disable");
            }
        }

    </script>
</head>
<body>
    <div data-role="page">
        <div id="content" data-role="content">
            <div id="header" data-role="fieldcontain">
                <table style="width: 100%">
                    <tbody>
                        <tr>
                            <td style="width: 70px;">
                                <label for="textinput">점검일자</label>
                            </td>
                            <td>
                                <input data-mini="true" id="txtDate" type="text" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label for="teamLst">설비</label>
                            </td>
                            <td>
                                <input data-mini="true" id="txtMach" type="text" readonly="readonly" />
                            </td>
                            <td>
                                <input data-mini="true" type="button" id="btnSave" value="저장" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id="container" class="swiper-container">
                <div class="swiper-wrapper">
                </div>
                <!-- Add Pagination -->
                <div class="swiper-pagination"></div>
                <!-- Add Arrows -->
                <div class="swiper-button-next"></div>
                <div class="swiper-button-prev"></div>
            </div>
        </div>
    

    <div id="popupDialog" data-transition="slidedown" style="max-width: 500px;" data-role="popup" data-overlay-theme="b" data-dismissible="false">
        <div data-role="header" data-theme="a" >            
            <h1>이상발생등록</h1>    
        </div>        
        <table style="padding-left:10px; padding-right:10px">
            <tbody>
                <tr>
                    <td colspan="2">
                        <span id="InspNm">점검항목</span>
                    </td>                    
                </tr>
                <tr>
                    <td colspan="2">
                        <span id="InspCnt">차수</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>이상유형</span>
                    </td>
                    <td>
                        <select data-mini="true" name="txtNGTYPE" id="txtNGTYPE"></select>
                    </td>
                </tr>

                <tr>
                    <td>
                        <span>이상내역</span>
                    </td>
                    <td>
                        <textarea  name="txtNGREMARK" id="txtNGREMARK"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <table>
            <colgroup><col width="25%"/><col width="25%"/><col width="25%"/><col width="25%"/></colgroup>
            <tr>
                <td></td>
                <td><input data-mini="true" type="button" id="btnpopSave" value="저장" /></td>
                <td><input data-mini="true" type="button" id="btnpopCancel" value="취소" /></td>
                <td></td>
            </tr>
        </table>
    </div>
    </div>
</body>
</html>
