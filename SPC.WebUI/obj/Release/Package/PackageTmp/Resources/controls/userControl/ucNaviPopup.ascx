<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNaviPopup.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucNaviPopup" %>
<header class="header bg-gradient b-b b-light content-navi container-non-responsive">
    <p><i class="fa fa-map-marker"></i> <span class="font-bold"><%=HttpUtility.UrlDecode(Title)%></span></p>
    <div style="float:right;">
        <ul class="nav navbar-nav navbar-right m-n nav-user user">
            <li id="btnR" class="topbutton" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick(); return false;">
                    <i class="fa fa-search"></i>
                    <span class="text">조회</span>
                </button>
            </li>
            <li id="btnA" class="topbutton FDCKUSERPOP" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnAcceptClick(); return false;">
                    <i class="fa fa-search"></i>
                    <span class="text">확인</span>
                </button>
            </li>
            <li id="btnC" class="topbutton" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-primary" onclick="fn_OnNewClick(); return false;">
                    <i class="fa fa-pencil"></i>
                    <span class="text">입력</span>
                </button>
            </li>
            <li id="btnU" class="topbutton" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-info" onclick="fn_OnModifyClick(); return false;">
                    <i class="fa fa-edit"></i>
                    <span class="text">수정</span>
                </button>
            </li>
            <li id="btnW" class="topbutton" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-warning" onclick="fn_OnCancelClick(); return false;">
                    <i class="fa fa-undo"></i>
                    <span class="text">취소</span>
                </button>
            </li>
            <li id="btnS" class="topbutton" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnSaveClick(); return false;">
                    <i class="fa fa-save"></i>
                    <span class="text">저장</span>
                </button>
            </li>
            <li id="btnD" class="topbutton" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-danger" onclick="fn_OnDeleteClick(); return false;">
                    <i class="fa fa-scissors"></i>
                    <span class="text">삭제</span>
                </button>
            </li>
            <li id="btnP" class="topbutton" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnPrintClick(); return false;">
                    <i class="fa fa-print"></i>
                    <span class="text">인쇄</span>
                </button>
            </li>
            <li id="btnE" class="topbutton" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-success" onclick="fn_OnExcelClick(); return false;">
                    <i class="i i-file-excel"></i>
                    <span class="text">엑셀</span>
                </button>
            </li>
            <li class="topbutton MEAINSPPOP" style="padding-top: 9px; padding-right: 5px; display:none;">
                <button class="btn btn-sm btn-primary" onclick="fn_OnRefresh(); return false;">
                    <i class="fa fa-refresh"></i>
                    <span class="text">초기화</span>
                </button>
            </li>
            <%--<li id="btnZ" class="topbutton BSIF0303_1POP" style="padding-top: 9px; padding-right: 5px; display: none;">
                <button class="btn btn-sm btn-primary" onclick="fn_InspCopy(); return false;">
                    <i class="fa fa-copy"></i>
                    <span class="text">동일항목검사기준수정</span> 
                </button>
            </li>--%>
            <li class="topbutton BSIF0303_HIS" style="padding-top: 9px;">
                <button class="btn btn-sm btn-primary" onclick="fn_Close(); return false;">
                    <i class="i i-cancel"></i>
                    <span class="text">닫기</span>
                </button>
            </li>
        </ul>
    </div>
</header>
<script type="text/javascript">
    $(document).ready(function () {
        var CRUD = '<%=btnUsed%>';
        for (var i = 0; i < CRUD.length; i++) {
            $('#btn' + CRUD[i]).css('display', 'block');
        }

        if (location.href.toUpperCase().indexOf('MEAINSPPOP') >= 0) {
            $(".MEAINSPPOP").css("display", "block");
        }

        if (location.href.toUpperCase().indexOf('BSIF0303_HIS') >= 0) {
            $(".BSIF0303_HIS").css("display", "none");
        }

        // 페이지 별 특수 기능버튼 활성화
        var loc = location.href.toUpperCase();
        $.each($('#btnA').attr('class').split(' '), function (idx, val) {
            if (loc.indexOf(val) >= 0) $('.' + val).css('display', 'block');
        });
    });

    function fn_Close() {
        parent.fn_devPopupClose();
    }
</script>