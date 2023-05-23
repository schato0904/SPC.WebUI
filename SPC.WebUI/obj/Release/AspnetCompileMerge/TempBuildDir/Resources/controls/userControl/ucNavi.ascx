<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNavi.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucNavi" %>
<header class="header bg-gradient b-b b-light content-navi">
    <p><i class="fa fa-map-marker"></i> <span id="spanNavi" class="font-bold"></span></p>
    <div style="float:right;">
        <ul class="nav navbar-nav navbar-right m-n">
            <li id="R" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick(); return false;">
                    <i class="fa fa-search"></i>
                    <span class="text">조회</span>
                </button>
            </li>
            <li id="C" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-primary" onclick="fn_OnNewClick(); return false;">
                    <i class="fa fa-pencil"></i>
                    <span class="text">입력</span>
                </button>
            </li>
            <li id="U" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-info" onclick="fn_OnModifyClick(); return false;">
                    <i class="fa fa-edit"></i>
                    <span class="text">수정</span>
                </button>
            </li>
            <li id="W" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-warning" onclick="fn_OnCancelClick(); return false;">
                    <i class="fa fa-undo"></i>
                    <span class="text">취소</span>
                </button>
            </li>
            <li id="S" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnSaveClick(); return false;">
                    <i class="fa fa-save"></i>
                    <span class="text">저장</span>
                </button>
            </li>
            <li id="D" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnDeleteClick(); return false;">
                    <i class="fa fa-scissors"></i>
                    <span class="text">삭제</span>
                </button>
            </li>
            <li id="P" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnPrintClick(); return false;">
                    <i class="fa fa-print"></i>
                    <span class="text">인쇄</span>
                </button>
            </li>
            <li id="E" class="topbutton" style="padding-top: 9px;  padding-right: 5px;display: none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnExcelClick(); return false;">
                    <i class="i i-file-excel"></i>
                    <span class="text">엑셀</span>
                </button>
            </li>
            <li class="topbutton SPMT0101" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnShipmentClick(); return false;">
                    <i class="fa fa-sign-out"></i>
                    <span class="text">출하</span>
                </button>
            </li>
            <li class="topbutton INSP0101" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnInspectionClick(); return false;">
                    <i class="fa fa-sign-out"></i>
                    <span class="text">검사성적서 생성</span>
                </button>
            </li>

             <li class="topbutton INSP0101_CHUNIL" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnInspectionClick(); return false;">
                    <i class="fa fa-sign-out"></i>
                    <span class="text">검사성적서 생성</span>
                </button>
            </li>

            <li class="topbutton INSP0101_foseco" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnInspectionClick(); return false;">
                    <i class="fa fa-sign-out"></i>
                    <span class="text">발행</span>
                </button>
            </li>

            <li class="topbutton WSTA0101 WSTA0102 WSTA0103" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_alldata(); return false;">
                    <i class="fa fa-sign-out"></i>
                    <span class="text">Data 조회</span>
                </button>
            </li>
            <li id="bMoveANLS0101" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnPopupQulityResponse(); return false;">
                    <i class="i i-cancel"></i>
                    <span class="text">품질이상제기</span>
                </button>
            </li>
            <li class="topbutton BSIF0303 BSIF0303NR BSIF0303FND" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnPopupQCD34Copy(); return false;">
                    <i class="fa fa-copy"></i>
                    <span class="text">검사기준복사</span>
                </button>
            </li>
             <li class="topbutton BSIF0303 BSIF0303NR BSIF0303FND" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnPopupQCD34Copy2(); return false;">
                    <i class="fa fa-copy"></i>
                    <span class="text">검사기준다중복사</span>
                </button>
            </li>
            <li class="topbutton BSIF0303 BSIF0303NR BSIF0303FND" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-primary" onclick="fn_GoToQCD34Multi('<%=sProgramID%>'); return false;">
                    <i class="fa fa-copy"></i>
                    <span class="text">다중관리</span>
                </button>
            </li>
            <li class="topbutton MNTR0901 MNTR0906 ADTR0101 ADTR0101_Image ADTR0101_Work ADTR0101FND_Work BSIF0303_MULTI ADTR0104FND ADTR0105_FND FITM0201 ADTR0101_Wgroup" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-danger" onclick="fn_FullWindow(); return false;">
                    <i class="i i-popup"></i>
                    <span class="text">전체창</span>
                </button>
            </li>
            <li id="bPopupClose" class="topbutton" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-primary" onclick="fn_Close(); return false;">
                    <i class="i i-cancel"></i>
                    <span class="text">닫기</span>
                </button>
            </li>
        </ul>
    </div>
    <div id="divNotice_Navi" class="divNotice" style="float:right;margin-right:50px;padding-top:15px;">
        <dx:ASPxLabel ID="txtHeaderNotice" runat="server" OnInit="txtHeaderNotice_Init" Text="" Font-Size="Small" Font-Bold="true"></dx:ASPxLabel>
    </div>
</header>
<script type="text/javascript">
    // "R" - 조회, "C" - 입력, "U" - 수정, "W" - 취소, "S" - 저장, "D" - 삭제, "P" - 인쇄, "E" - 엑셀
    $(document).ready(function () {
        var sToolbars = '<%=sToolbars%>';
        var sAuthority = '<%=sAuthority%>';
        var sAuthToolbars = 'CUWSD';
        var ClientInstanceID = 'ucNavi';
        var HeaderNotice = '<%=this.txtHeaderNotice.UniqueID%>';

        if (ClientInstanceID != '') { // && typeof(window[ClientInstanceID]) == 'undefined') {
            window[ClientInstanceID] = new (function (ClientInstanceID) {
                var objThis = this;
                var label = ASPxClientLabel.Cast(HeaderNotice);
                var timer;
                this.SetNotice = function (txt, color, enabledBlink) {
                    label.SetText(txt);
                    if (Trim(color) != '') {
                        label.GetMainElement().style.color = color;
                    }
                    if (enabledBlink)
                        this.Blink(Trim(txt) != '' ? label.GetMainElement() : null);
                    else this.Blink(null);
                };
                this.SetNoticeOnce = function (txt, color, enabledBlink, hideTime) {
                    $('#divNotice_Navi').hide();
                    if (typeof (timer) != 'undefined' && timer != null) {
                        clearInterval(timer);
                    };
                    if (typeof hideTime == 'undefined' || hideTime == null || hideTime == '' || isNaN(hideTime)) {
                        hideTime = 3000;
                    } else {
                        hideTime = parseInt(hideTime, 10);
                    }
                    color = color || 'navy';
                    enabledBlink = enabledBlink || false;

                    txt = Trim(txt) != '' ? '* ' + txt : txt;

                    label.SetText(txt);
                    $('#divNotice_Navi').slideDown();
                    if (Trim(color) != '') {
                        label.GetMainElement().style.color = color;
                    }
                    if (enabledBlink)
                        this.Blink(Trim(txt) != '' ? label.GetMainElement() : null);
                    else this.Blink(null);

                    if (hideTime > 0) {
                        timer = setTimeout(function () {
                            $('#divNotice_Navi').slideUp();
                            //label.SetText(null);
                            objThis.Blink(null);
                        }, hideTime);
                    }
                };
                this.Blink = function (obj) {
                    var elem = $(obj);
                    if (typeof (timer) != 'undefined' && timer != null) {
                        clearInterval(timer);
                    };
                    if (typeof (obj) != 'undefined' && obj != null) {
                        timer = setInterval(function () {
                            if (elem.css('visibility') == 'hidden') {
                                elem.css('visibility', 'visible');
                            } else {
                                elem.css('visibility', 'hidden');
                            }
                        }, 1000);
                    }
                };
            })(ClientInstanceID);
        }

        $(".topbutton").each(function (index) {
            if (sToolbars.indexOf($(this).attr("id")) != -1) {
                if (sAuthToolbars.indexOf($(this).attr("id")) == -1 || (sAuthToolbars.indexOf($(this).attr("id")) != -1 && sAuthority == '1')) {
                    $(this).css("display", "block");
                }
            }
        });

        // 품질이상 - 품질이상제기(모업체인 경우 등록 기능을 사용하지 않는다)
        if ('<%=Page.gsVENDOR%>' == 'False' && '<%=sProgramID%>' == 'IPCM0101') {
            $("#C").css("display", "none");
        }

        // 품질종합현황 - 품질이상제기
        if ('<%=sProgramID%>' == 'ANLS0101' || '<%=sProgramID%>' == 'TISP01POP' || '<%=sProgramID%>' == 'ANLS0101_NEW') {
            $("#bMoveANLS0101").css("display", "block");
        }

        // 페이지 별 특수 기능버튼 활성화
        if ('<%=sPopup%>' != 'true') {
            var sProgramID = '<%=sProgramID%>';
            $("." + sProgramID).css("display", "block");
        }

        // 팝업으로 연 경우 닫기버튼 활성화
        if ('<%=sPopup%>' == 'true') {
            $("#bPopupClose").css("display", "block");
        }

        var sNavigation = '<%=sNavigation%>';
        $("#spanNavi").html(sNavigation);
    });

    function fn_Close() {
        if ('<%=sFrame%>' == 'false') {
            self.opener = self;
            window.close();
        }  else
            parent.fn_devPopupClose();
    }

    function fn_FullWindow() {
        var url = location.href + '&bPopup=true&bFrame=false';
        OpenNewFullWindow(url);
    }
</script>
