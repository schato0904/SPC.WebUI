<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FDCK0101.aspx.cs" Inherits="SPC.WebUI.Pages.FDCK.FDCK0101" %>
<%-- 설비 등록 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .divLine {
            width: 100%;
            height: 3px;
            /*background-color: dimgray;*/
            background-color: #6495ED;
            margin-top: 2px;
            margin-bottom: 2px;
        }
        .contentTable {
            width: 100%;
            border-color: darkgray;
        }
            .contentTable > tbody > tr > .tdLabel {
                /*background-color: #CFEFFF;*/
                background-color: #DCDCDC;
                color: dimgray;
                text-align: center;
                padding-top: 3px;
                padding-bottom: 3px;
            }
            .contentTable > tbody > tr > .tdLabel > label {
                color: #444444;
                font-weight:bold;
            }
            .contentTable > tbody > tr > .tdInput {
                background-color: white;
                padding-left: 3px;
                padding-right: 3px;
            }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
            var idList = 'srcF_MACHCD';
            fn_OnlyAlphaNumeric(idList);

            //$('.required > label').each(function (i, e) {
            //    $(e).parent().add("<span>*</span>").css("color", "red");
            //});
            $('td.tdLabel.required').append("<span style='color:red'>*</span>");
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            //var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            var height = $(document.documentElement).height() - _hMargin - $(devGrid.GetMainElement()).offset().top;
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            ClearPage();
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (fn_OnValidate(null, null) == false) {
                return false;
            }
            //devGrid.PerformCallback('SAVE');
            var data = {
                'PAGEMODE': hidPageMode.GetText(),
                'ACTION' : 'SAVE'
            };
            fn_devCallback_PerformCallback(data);
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (fn_OnValidateDelete() == false) {
                alert('삭제할 수 없습니다.');
                return false;
            }

            var data = {
                'PAGEMODE': hidPageMode.GetText(),
                'ACTION': 'DELETE'
            };
            fn_devCallback_PerformCallback(data);
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            var errMsg = "";
            if (fn_GetCastValue('srcF_COMPCD') == "") {
                errMsg += "[사업장]을 선택해 주세요.\n";
            }
            if (fn_GetCastValue('srcF_FACTCD') == "") {
                errMsg += "[공장]을 선택해 주세요.\n";
            }
            if (fn_GetCastValue('srcF_MACHCD') == "") {
                errMsg += "[설비코드]를 입력해 주세요.\n";
            }
            if (fn_GetCastValue('srcF_MACHNM') == "") {
                errMsg += "[설비명]을 입력해 주세요.\n";
            }

            if (errMsg != "") {
                alert(errMsg);
                return false;
            } else {
                return true;
            }
        }

        // Validate
        function fn_OnValidateDelete() {
            var isEnable = false;
            isEnable = !(fn_GetCastValue('srcF_COMPCD') == "" || fn_GetCastValue('srcF_FACTCD') == "" || fn_GetCastValue('srcF_MACHCD') == "");

            return isEnable;
        }

        // 입력부 초기화
        function ClearPage() {
            //srcF_COMPCD.SetValue('');
            //srcF_FACTCD.SetValue('');
            srcF_MACHCD.SetValue('');
            srcF_MACHNM.SetValue('');
            srcF_BANCD.SetValue('');
            srcF_LINECD.SetValue('');
            srcF_MACHKIND.SetValue('');
            srcF_SORTNO.SetValue('');
            srcF_USEYN.SetValue('1');
            srcF_MAKER.SetValue('');
            srcF_INDATE.SetValue('');
            srcF_SELLER.SetValue('');
            srcF_PRICE.SetValue('');
            srcF_SPEC.SetValue('');
            srcF_SUBPART.SetValue('');
            srcF_REMARK.SetValue('');
            srcWORKERS_TEAM.SetValue('');
            srcWORKERS_USER.SetValue('');
            srcADMINS_TEAM.SetValue('');
            srcADMINS_USER.SetValue('');
            txtWorkers.SetValue('');
            txtAdmins.SetValue('');
            txtIMAGESEQ_SetValue('');

            hidPageMode.SetText('NEW');
        }

        function txtIMAGESEQ_SetValue(no) {            
            //string attfileno = context.Request.Params["attfileno"];
            //string attfileseq = context.Request.Params["attfileseq"];
            //string data_gbn = context.Request.Params["data_gbn"];
            //string compcd = context.Request.Params["compcd"];
            
            txtIMAGESEQ.SetValue(no);
            var srcImage = ASPxClientImage.Cast("srcImage");
            var width = $(srcImage.GetMainElement()).parent().width();
            var height = $(srcImage.GetMainElement()).parent().height();
            srcImage.SetWidth(width);
            srcImage.SetHeight(height);
            var imageUrl = no == '' ? '' : rootURL + 'API/Common/Download.ashx'
                + '?attfileno=' + no
                + '&attfileseq=1'
                + '&data_gbn=E'
                + '&compcd=' + srcF_COMPCD.GetText();
            srcImage.SetImageUrl(imageUrl);
        }

        function txtIMAGESEQ_Init(s, e) {
            //s.SetText('');
        }

        // ------ 이하 Callback 이벤트 핸들러          
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                devCallback.PerformCallback(JSON.stringify(parameter));
            }
        }

        // 그리드 더블 클릭시 선택항목 상세 조회
        function fn_devGrid_RowDblClick(s, e) {
            var devGrid = ASPxClientGridView.Cast('devGrid');
            var pkey = devGrid.GetRowKey(e.visibleIndex);
            GetDataAndSetPage(pkey);
        }

        // 지정한 키값을 가져오도록 
        function GetDataAndSetPage(pkey) {
            var f_compcd = pkey.split('|')[0];
            var f_factcd = pkey.split('|')[1];
            var f_machcd = pkey.split('|')[2];
            var data = {
                'F_COMPCD': f_compcd,
                'F_FACTCD': f_factcd,
                'F_MACHCD': f_machcd,
                'ACTION' : 'GET'
            };
            fn_devCallback_PerformCallback(data);
        }

        // json에서 값 가져오기(null 처리 포함)
        function GetItem(json, key, isnullvalue) {
            isnullvalue = typeof (isnullvalue) != 'undefined' ? isnullvalue : '';
            return (typeof (json[key]) != 'undefined') ? json[key] : isnullvalue;
        }

        // 전달된 페이지 데이터로 페이지를 채운다.
        function SetPageValue(pagedata) {
            var f_compcd = GetItem(pagedata, 'F_COMPCD', '');
            var f_factcd = GetItem(pagedata, 'F_FACTCD', '');
            var f_machcd = GetItem(pagedata, 'F_MACHCD', '');
            if (f_compcd == '' || f_factcd == '' || f_machcd == '') {
                alert('조회 내용이 없습니다.');
                return;
            }
            srcF_COMPCD.SetValue(f_compcd);
            srcF_FACTCD.SetValue(f_factcd);
            srcF_MACHCD.SetValue(f_machcd);
            srcF_MACHNM.SetValue(GetItem(pagedata, 'F_MACHNM'));
            srcF_BANCD.SetValue(GetItem(pagedata, 'F_BANCD'));
            srcF_LINECD.SetValue(GetItem(pagedata, 'F_LINECD'));
            srcF_MACHKIND.SetValue(GetItem(pagedata, 'F_MACHKIND'));
            srcF_SORTNO.SetValue(GetItem(pagedata, 'F_SORTNO'));            
            srcF_USEYN.SetValue(GetItem(pagedata, 'F_USEYN'));
            srcF_MAKER.SetValue(GetItem(pagedata, 'F_MAKER'));
            srcF_INDATE.SetValue(GetItem(pagedata, 'F_INDATE'));
            srcF_SELLER.SetValue(GetItem(pagedata, 'F_SELLER'));
            srcF_PRICE.SetValue(GetItem(pagedata, 'F_PRICE'));
            srcF_SPEC.SetValue(GetItem(pagedata, 'F_SPEC'));
            srcF_SUBPART.SetValue(GetItem(pagedata, 'F_SUBPART'));
            srcF_REMARK.SetValue(GetItem(pagedata, 'F_REMARK'));
            srcWORKERS_TEAM.SetValue(GetItem(pagedata, ''));
            srcWORKERS_USER.SetValue(GetItem(pagedata, 'F_WORKERS'));
            srcADMINS_TEAM.SetValue(GetItem(pagedata, ''));
            srcADMINS_USER.SetValue(GetItem(pagedata, 'F_ADMINS'));
            txtWorkers.SetValue(GetItem(pagedata, 'F_WORKERSNM'));
            txtAdmins.SetValue(GetItem(pagedata, 'F_ADMINSNM'));
            txtIMAGESEQ_SetValue(GetItem(pagedata, 'F_IMAGENO'));

            hidPageMode.SetText('EDIT');
        }

        // 콜백 성공시 처리 : 성공시 PKEY에 JSON 구조로 키값을 받는다.
        function fn_devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            var parameter = JSON.parse(e.parameter);
            var isOK = typeof (result.ISOK) != 'undefined' ? result.ISOK : null;
            var msg = typeof (result.MSG) != 'undefined' ? result.MSG : null;
            var pkey = typeof (result.PKEY) != 'undefined' ? result.PKEY : null;
            var data = {};
            if (typeof (result.PAGEDATA) != 'undefined') {
                data = result.PAGEDATA;
            } else {
                for (var key in pkey) { data[key] = pkey[key]; }
            }

            var action = typeof (parameter.ACTION) != 'undefined' ? parameter.ACTION : null;
            if (isOK) {
                switch (action) {
                    case "SAVE":
                        alert(msg);
                        data.ACTION = 'GET';
                        fn_devCallback_PerformCallback(data);
                        fn_OnSearchClick();
                        break;
                    case "GET":
                        SetPageValue(data);
                        break;
                    case "DELETE":
                        alert(msg);
                        ClearPage();
                        fn_OnSearchClick();
                        break;
                }
            } else {
                alert(msg);
            }
        }

        // 콜백 종료시 처리
        function fn_devCallback_EndCallback(s, e) {
            // 대기중인 작업이 있을경우, 콜백 실행
            if (devCallback_parameter) {
                devCallback.PerformCallback(JSON.stringify(devCallback_parameter));
                devCallback_parameter = null;
            }
        }

        // 콜백 오류시 처리
        function fn_devCallback_CallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }
        // ------ 여기까지 Callback 이벤트 핸들러

        // 설비사진 파일첨부 버튼 클릭
        function fn_OnBtnImageClick(s, e) {
            fn_fileUpload();
            return false;
        }
        // 파일 업로드 팝업 호출
        function fn_fileUpload() {
            fn_AttachFileOpenParam('설비사진등록', 'E', 'F', ASPxClientTextBox.Cast("srcF_COMPCD").GetText()); 
        }
        
        // 사용자 선택 버튼 클릭
        function fn_OnBtnWorkersClick(s, e) {

            var teamTextboxId = "srcWORKERS_TEAM";
            var userTextboxId = "srcWORKERS_USER";

            fn_OpenFDCKUSERPOP("담당자선택", teamTextboxId, userTextboxId, "fn_AfterFDCKUSERPOP_Workers");
            return false;
        }

        // 사용자 선택 버튼 클릭
        function fn_OnBtnAdminsClick(s, e) {

            var teamTextboxId = "srcADMINS_TEAM";
            var userTextboxId = "srcADMINS_USER";

            fn_OpenFDCKUSERPOP("관리자선택", teamTextboxId, userTextboxId, "fn_AfterFDCKUSERPOP_Admins");
            return false;
        }

        // 사용자 선택 버튼 클릭
        function fn_OpenFDCKUSERPOP(popup_title, teamTextboxId, userTextboxId, callbackfn) {

            var txtTEAM = ASPxClientTextBox.Cast(teamTextboxId);
            var txtUSER = ASPxClientTextBox.Cast(userTextboxId);

            fn_OnPopupFDCKUSERPOP(popup_title, txtTEAM.GetText() + "|" + txtUSER.GetText(), callbackfn);
            return false;
        }

        // 담당자 선택 팝업결과 처리
        function fn_AfterFDCKUSERPOP_Workers(result) {
            fn_AfterFDCKUSERPOP(result, "txtWorkers", "srcWORKERS_TEAM", "srcWORKERS_USER");
        }

        // 담당자 선택 팝업결과 처리
        function fn_AfterFDCKUSERPOP_Admins(result) {
            fn_AfterFDCKUSERPOP(result, "txtAdmins", "srcADMINS_TEAM", "srcADMINS_USER");
        }

        // 팝업결과 처리
        function fn_AfterFDCKUSERPOP(result, viewBoxId, teamBoxId, userBoxId) {
            //alert(result);
            var txtUsers = ASPxClientTextBox.Cast(viewBoxId);
            var txtUsersCD_TEAM = ASPxClientTextBox.Cast(teamBoxId);
            var txtUsersCD_USER = ASPxClientTextBox.Cast(userBoxId);

            if (result != '') {
                txtUsers.SetText(result.split("|")[0]);
                txtUsersCD_TEAM.SetText(result.split("|")[1]);
                txtUsersCD_USER.SetText(result.split("|")[2]);
            }
            else {
                txtUsers.SetText("");
                txtUsersCD_TEAM.SetText("");
                txtUsersCD_USER.SetText("");
            }
        }

        // 영문, 숫자만 허용. 'id1;id2;id3' 형태로 clientinstancename을 파라미터로 전달
        function fn_OnlyAlphaNumeric(idList) {
            var ids = idList.split(';');
            var c = null;
            for (var i = 0 ; i < ids.length; i++) {
                c = ASPxClientTextBox.Cast(ids[i]);
                if (ASPxClientUtils.IsExists(c)) {
                    c.KeyUp.AddHandler(fn_CustomTextbox_KeyUp_Alpha_Numeric);
                    c.TextChanged.AddHandler(fn_CustomTextbox_TextChanged_Alpha_Numeric);
                }
            }
        }

        // 영, 숫자만 허용되는 keyup 이벤트 핸들러
        function fn_CustomTextbox_KeyUp_Alpha_Numeric(s, e) {
            var keycode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            // 좌,우 Cursor, Backspace, Delete, Tab 키는 패스
            if (keycode == 8 || keycode == 9 || keycode == 37 || keycode == 39 || keycode == 46) return;
            // 숫자, 영대문자, 영소문자, _(underscore) 문자가 아닌경우, 텍스트값 변경 및 키입력 이벤트는 취소 처리
            if (keycode < 48 || (keycode > 57 && keycode < 65) || (keycode > 90 && keycode < 95) || keycode == 96 || keycode > 122) {
                fn_CustomTextbox_TextChanged_Alpha_Numeric(s, null);
                e.htmlEvent.returnValue = false;
                return false;
            }
        }

        // 영, 숫자만 허용되는 텍스트변경 이벤트 핸들러. 정규식 사용하여 숫자, 영대소문자, _ 외에는 ''으로 치환 처리
        function fn_CustomTextbox_TextChanged_Alpha_Numeric(s, e) {
            s.SetText(s.GetText().replace(/[^0-9A-Za-z_]/gi, ''));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">        
        <div class="content">
            <table border="1" class="contentTable">
                <colgroup>
                    <col style="width:11.1%" />
                    <col style="width:22.2%" />
                    <col style="width:11.1%" />
                    <col style="width:22.2%" />
                    <col style="width:11.1%" />
                    <col style="width:22.3%" />
                </colgroup>
                <tr>
                    <td class="tdLabel required">
                        <label>업체</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_COMPNM" ClientInstanceName="srcF_COMPNM" runat="server" ClientEnabled="false" Width="100%"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" runat="server" ClientEnabled="false" ClientVisible="false"></dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel required">
                        <label>공장</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:FactMulti runat="server" id="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" IsRequired="true" targetCtrls="srcF_BANCD;srcF_LINECD;srcF_MACHKIND" />
                    </td>
                    <td class="tdLabel" colspan="2">
                        <label>설비사진</label>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel required">
                        <label>설비코드</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MACHCD" ClientInstanceName="srcF_MACHCD" runat="server" Width="100%" MaxLength="5"></dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel required">
                        <label>설비명</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MACHNM" ClientInstanceName="srcF_MACHNM" runat="server" Width="100%" MaxLength="50"></dx:ASPxTextBox>
                    </td>
                    <td class="tdInput" colspan="2" rowspan="8">
                        <dx:ASPxImage ID="srcImage" ClientInstanceName="srcImage" runat="server"></dx:ASPxImage>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>반</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="srcF_BANCD" ClientInstanceName="srcF_BANCD" nullText="" targetCtrls="srcF_LINECD" />
                    </td>
                    <td class="tdLabel">
                        <label>라인</label>
                    </td>
                    <td class="tdInput">         
                        <ucCTF:LineMulti runat="server" id="srcF_LINECD" ClientInstanceName="srcF_LINECD" nullText="" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>설비구분</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" id="srcF_MACHKIND" ClientInstanceName="srcF_MACHKIND" F_CODEGROUP="40" nullText="" />
                    </td>
                    <td class="tdLabel">
                        <label></label>
                    </td>
                    <td class="tdInput">
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>정렬순서</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxSpinEdit ID="srcF_SORTNO" ClientInstanceName="srcF_SORTNO" runat="server" Width="100%" AllowMouseWheel="false" SpinButtons-ClientVisible="false"></dx:ASPxSpinEdit>
                    </td>
                    <td class="tdLabel">
                        <label>사용여부</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxRadioButtonList ID="srcF_USEYN" ClientInstanceName="srcF_USEYN" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                            <Items>
                                <dx:ListEditItem Text="사용" Value="1" Selected="true" />
                                <dx:ListEditItem Text="사용 안함" Value="0" Selected="false" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>제조사</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MAKER" ClientInstanceName="srcF_MAKER" runat="server" Width="100%" MaxLength="80"></dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">
                        <label>도입일자</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_INDATE" ClientInstanceName="srcF_INDATE" runat="server" Width="100%" MaxLength="10" MaskSettings-Mask="9999.99.99"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>구입처</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_SELLER" ClientInstanceName="srcF_SELLER" runat="server" Width="100%" MaxLength="80"></dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">
                        <label>구입가격</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_PRICE" ClientInstanceName="srcF_PRICE" runat="server" Width="100%" MaxLength="10"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>사양</label>
                    </td>
                    <td class="tdInput" colspan="3">
                        <dx:ASPxTextBox ID="srcF_SPEC" ClientInstanceName="srcF_SPEC" runat="server" Width="100%" MaxLength="200"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>주요부속품</label>
                    </td>
                    <td class="tdInput" colspan="3">
                        <dx:ASPxTextBox ID="srcF_SUBPART" ClientInstanceName="srcF_SUBPART" runat="server" Width="100%" MaxLength="200"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>비고</label>
                    </td>
                    <td class="tdInput" colspan="3">
                        <dx:ASPxTextBox ID="srcF_REMARK" ClientInstanceName="srcF_REMARK" runat="server" Width="100%" MaxLength="4000"></dx:ASPxTextBox>
                    </td>
                    <td class="tdInput" colspan="2">
                        <dx:ASPxButton ID="btnImageFile" ClientInstanceName="btnImageFile" runat="server" Text="설비사진파일등록" AutoPostBack="false">
                            <ClientSideEvents Click="fn_OnBtnImageClick" />
                        </dx:ASPxButton>
                        <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>담당자</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxButton ID="btnWorker" ClientInstanceName="btnWorker" runat="server" Text="담당자등록" AutoPostBack="false">
                            <ClientSideEvents Click="fn_OnBtnWorkersClick" />
                        </dx:ASPxButton>
                    </td>
                    <td class="tdInput" colspan="4">
                        <dx:ASPxTextBox ID="txtWorkers" ClientInstanceName="txtWorkers" runat="server" Width="100%" ClientEnabled="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="srcWORKERS_TEAM" ClientInstanceName="srcWORKERS_TEAM" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="srcWORKERS_USER" ClientInstanceName="srcWORKERS_USER" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>관리자</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxButton ID="btnAdmin" ClientInstanceName="btnAdmin" runat="server" Text="관리자등록" AutoPostBack="false">
                            <ClientSideEvents Click="fn_OnBtnAdminsClick" />
                        </dx:ASPxButton>
                    </td>
                    <td class="tdInput" colspan="4">
                        <dx:ASPxTextBox ID="txtAdmins" ClientInstanceName="txtAdmins" runat="server" Width="100%" ClientEnabled="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="srcADMINS_TEAM" ClientInstanceName="srcADMINS_TEAM" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="srcADMINS_USER" ClientInstanceName="srcADMINS_USER" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divLine"></div>
        <div class="search">
            <table border="1" class="contentTable">
                <colgroup>
                    <col style="width:11.1%" />
                    <col style="width:22.2%" />
                    <col style="width:11.1%" />
                    <col style="width:22.2%" />
                    <col style="width:11.1%" />
                    <col style="width:22.3%" />
                </colgroup>
                <tr>
                    <td class="tdLabel">
                        <label>공장</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:FactMulti runat="server" id="schF_FACTCD" ClientInstanceName="schF_FACTCD" nullText="전체" targetCtrls="schF_BANCD;schF_LINECD;schF_MACHKIND" />
                    </td> 
                    <td class="tdLabel">
                        <label>반</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="schF_BANCD" ClientInstanceName="schF_BANCD" targetCtrls="schF_LINECD" />
                    </td>          
                    <td class="tdLabel">                        
                        <label>라인</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:LineMulti runat="server" ID="schF_LINECD" ClientInstanceName="schF_LINECD" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>설비구분</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" ID="schF_MACHKIND" ClientInstanceName="schF_MACHKIND" F_CODEGROUP="40" />
                    </td>          
                    <td class="tdLabel">
                        <label>설비명</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" Width="100%" MaxLength="50"></dx:ASPxTextBox>
                    </td>
                    <td class="tdInput" colspan="2">
                        <dx:ASPxButton ID="btnSearch" ClientInstanceName="btnSearch" runat="server" Text="조회" AutoPostBack="false">
                            <ClientSideEvents Click="fn_OnSearchClick" />
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnExcel" ClientInstanceName="btnExcel" runat="server" Text="엑셀" AutoPostBack="false">
                            <ClientSideEvents Click="fn_OnExcelClick" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD;F_MACHCD" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings HorizontalScrollBarMode="Hidden" VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    RowDblClick="fn_devGrid_RowDblClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="NO" Caption="No." Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_FACTNM" Caption="공장명" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_MACHKINDNM" Caption="설비구분" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_SORTNO" Caption="순번" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_USEYN" Caption="사용" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_WORKERSNM" Caption="담당자" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_ADMINSNM" Caption="관리자" Width="60px" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            <div class="paging"></div>
        </div>
        <div id="divHiddenLayer" style="display:none;">
            <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" ClientVisible="false" Text="NEW"></dx:ASPxTextBox>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
                OnCallback="devCallback_Callback"
                >
                <ClientSideEvents 
                    CallbackComplete="fn_devCallback_CallbackComplete" 
                    EndCallback="fn_devCallback_EndCallback" 
                    CallbackError="fn_devCallback_CallbackError" />
            </dx:ASPxCallback>
        </div>
    </div>
</asp:Content>
