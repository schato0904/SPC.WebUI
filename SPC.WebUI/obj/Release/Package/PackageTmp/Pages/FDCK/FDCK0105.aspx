<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FDCK0105.aspx.cs" Inherits="SPC.WebUI.Pages.FDCK.FDCK0105" %>
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
            /*
            var idList = 'srcF_MACHCD';
            fn_OnlyAlphaNumeric(idList);
            */
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

            ucMachTypeSearch.fn_AdjustSize();
        }

        // 조회
        function fn_OnSearchClick() {
            //if (fn_GetCastValue('txtucMachineCD') == "")
            if (fn_GetCastValue('hidF_MACHCD') == "")
            {
                alert("설비를 선택 후 조회 하세요.");
                return;
            }

            fn_doSetGridEventAction('true');

            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            //if (fn_GetCastValue('txtucMachineCD') == "") {
            if (fn_GetCastValue('hidF_MACHCD') == "") {
                alert("설비를 선택 후 입력 하세요.");
                return;
            }
            fn_doSetGridEventAction('true');
            devGrid.PerformCallback();
            ClearPage();
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            //devGrid.PerformCallback('SAVE');

            if (fn_OnValidate(null, null) == false) return;

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
            alert('점검기준 삭제 정책 수립 후 기능 오픈합니다.');
            return false;

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
            if (fn_GetCastValue('txtINSPCD') == "") {
                errMsg += "[점검항목]을 선택해 주세요.\n";
            }
            if (fn_GetCastValue('txtINSPGBN') == "") {
                errMsg += "[점검분류]를 선택해 주세요.\n";
            }
            if (fn_GetCastValue('txtINSPORDER') == "") {
                errMsg += "[점검순서]를 입력해 주세요.\n";
            }
            if (fn_GetCastValue('txtSTANDARD') == "") {
                errMsg += "[규격]을 입력해 주세요.\n";
            }
            if (fn_GetCastValue('txtCYCLE') == "") {
                errMsg += "[점검주기]를 선택해 주세요.\n";
            }
            
            if (errMsg != "") {
                alert(errMsg);
                return false;
            } else {
                return true;
            }            
        }

        // 입력부 초기화
        function ClearPage() {

            txtINSPCD.SetValue(''); //점검항목
            txtINSPNM.SetValue(''); //점검항목명
            ddlINSPCD.SetValue('');   //점검분류
            txtINSPGBN.SetValue('');  //점검분류
            txtINSPORDER.SetValue(''); //점검순서
            txtSTANDARD.SetValue(''); //규격
            txtINSPREMARK.SetValue(''); //점검내용
            txtINSPSTAND.SetValue('');  //점검기준
            txtMAX.SetValue('');   //상한
            txtMIN.SetValue('');   //하한
            txtUNIT.SetValue('');  //단위
            ddlUNIT.SetValue('');
            ddlCYCLE.SetValue(''); //점검주기
            txtCYCLE.SetValue(''); //점검주기
            rdoUSEYN.SetValue('1'); //사용여부
            txtWorkers.SetValue('');
            srcWORKERS_TEAM.SetValue('');
            srcWORKERS_USER.SetValue('');
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
                + '&compcd=' + "<%=GetCompCD()%>";
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
            var f_insptype = pkey.split('|')[3];
            var f_inspserial = pkey.split('|')[4];
            var data = {
                'F_COMPCD': f_compcd,
                'F_FACTCD': f_factcd,
                'F_MACHCD': f_machcd,
                'F_INSPTYPECD': f_insptype,
                'F_INSPSERIAL': f_inspserial,
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
            txtINSPCD.SetValue(GetItem(pagedata, 'F_INSPCD')); //점검항목
            txtINSPNM.SetValue(GetItem(pagedata, 'F_INSPNM')); //점검항목명
            ddlINSPCD.SetValue(GetItem(pagedata, 'F_INSPKINDCD'));   //점검분류
            txtINSPGBN.SetValue(GetItem(pagedata, 'F_INSPKINDCD'));  //점검분류
            txtINSPORDER.SetValue(GetItem(pagedata, 'F_INSPORDER')); //점검순서
            txtSTANDARD.SetValue(GetItem(pagedata, 'F_STANDARD')); //규격
            txtINSPREMARK.SetValue(GetItem(pagedata, 'F_INSPREMARK')); //점검내용
            txtINSPSTAND.SetValue(GetItem(pagedata, 'F_INSPSTAND'));  //점검기준
            txtMAX.SetValue(GetItem(pagedata, 'F_MAX'));   //상한
            txtMIN.SetValue(GetItem(pagedata, 'F_MIN'));   //하한
            txtUNIT.SetValue(GetItem(pagedata, 'F_UNIT'));  //단위
            ddlUNIT.SetValue(GetItem(pagedata, 'F_UNIT'));  //단위
            ddlCYCLE.SetValue(GetItem(pagedata, 'F_CYCLECD')); //점검주기
            txtCYCLE.SetValue(GetItem(pagedata, 'F_CYCLECD')); //점검주기
            rdoUSEYN.SetValue(GetItem(pagedata, 'F_USEYN')); //사용여부
            txtIMAGESEQ_SetValue(GetItem(pagedata, 'F_IMAGESEQ'));
            hidSerialno.SetValue(GetItem(pagedata, 'F_INSPSERIAL'));
            srcWORKERS_TEAM.SetValue(GetItem(pagedata, ''));
            srcWORKERS_USER.SetValue(GetItem(pagedata, 'F_WORKERS'));
            txtWorkers.SetValue(GetItem(pagedata, 'F_WORKERSNM'));

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
                        //fn_devCallback_PerformCallback(data);
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
                    case "INSPCD":
                        txtINSPCD.SetText(result.INSPCD);
                        txtINSPNM.SetText(result.INSPNM);
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
            fn_AttachFileOpenParam('설비점검항목사진등록', 'E', 'F', "<%=GetCompCD()%>"); 
        }
        
        // 사용자 선택 버튼 클릭
        function fn_OnBtnWorkersClick(s, e) {

            var teamTextboxId = "srcWORKERS_TEAM";
            var userTextboxId = "srcWORKERS_USER";

            fn_OpenFDCKUSERPOP("담당자선택", teamTextboxId, userTextboxId, "fn_AfterFDCKUSERPOP_Workers");
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


        // 점검항목코드 입력시 명칭조회
        function fn_OnINSPCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                txtINSPNM.SetValue('');
                txtINSPNM.SetText('');
                txtINSPNM.GetMainElement().title = '';
                return;
            } else
                //devCallback.PerformCallback('MEAINSP|' + s.GetValue());
            {
                var data = {
                    'INSPCD': s.GetValue(),
                    'ACTION': 'INSPCD'
                };
                fn_devCallback_PerformCallback(data);
            }
        }
        // 검색된 점검항목 세팅
        function fn_OnSettingInsp(CODE, TEXT) {
            txtINSPCD.SetText(CODE);
            txtINSPNM.SetText(TEXT);
        }

        function fn_idxchang(s, e) {
            txtINSPGBN.SetValue(s.GetValue());
        }
        function fn_idxchang2(s, e) {
            txtCYCLE.SetValue(s.GetValue());
        }
        function fn_idxchang3(s, e) {
            txtUNIT.SetValue(s.GetValue());
        }

        // 좌측에서 설비 더블클릭하여 선택시
        function fn_OnLeftSelectItem(s, e) {
            //alert('left item select');
            var v = ucMachTypeSearch.GetSelectedValues();
            //alert(v);
            
            hidF_MACHCD.SetText(v.F_MACHCD);
            hidF_MACHNM.SetText(v.F_MACHNM);
            hidF_INSPTYPECD.SetText(v.F_INSPTYPECD);
            hidF_INSPTYPENM.SetText(v.F_INSPTYPENM);
            fn_DisplayKeyField(v);

            // 선택한 값으로 조회
            fn_OnSearchClick();
        }

        // 선택한 설비, 점검타입 명칭 설정
        function fn_DisplayKeyField(v) {
            var txtMACHCDNM = ASPxClientTextBox.Cast('txtMACHCDNM');
            var txtINSPTYPECDNM = ASPxClientTextBox.Cast('txtINSPTYPECDNM');
            txtMACHCDNM.SetText("[" + v.F_MACHCD + "] " + v.F_MACHNM);
            txtINSPTYPECDNM.SetText("[" + v.F_INSPTYPECD + "] " + v.F_INSPTYPENM);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divLeft" style="float:left;width:30%;padding-right:5px;">
            <ucCTF:MachTypeSearch id="ucMachTypeSearch" runat="server" ClientInstanceName="ucMachTypeSearch" OnSelectItem="fn_OnLeftSelectItem" />
            <%--<ucCTF:DDLMachType id="ucMachTypeSearch" runat="server" ClientInstanceName="ucMachTypeSearch" OnSelectItem="fn_OnLeftSelectItem" />--%>
        </div>
        <div id="divRight" style="float:left;width:70%;">
        <div class="content">
            <table border="1" class="contentTable">
                <colgroup>
                    <col style="width:11.1%" />
                    <col style="width:22.2%" />
                    <col style="width:11.1%" />
                    <col style="width:22.2%" />
                    <col style="width:33.4%" />
                </colgroup>
                <tr>
                    <td class="tdLabel">
                        <label>설비</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="txtMACHCDNM" ClientInstanceName="txtMACHCDNM" runat="server" Width="100%" 
                            class="form-control input-sm" ClientEnabled="false">
                            <DisabledStyle BackColor="#CCCCCC" ForeColor="#444444"></DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="hidF_MACHCD" ClientInstanceName="hidF_MACHCD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="hidF_MACHNM" ClientInstanceName="hidF_MACHNM" runat="server" ClientVisible="false" />
                    </td>
                    <td class="tdLabel">
                        <label>점검타입</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="txtINSPTYPECDNM" ClientInstanceName="txtINSPTYPECDNM" runat="server" Width="100%" 
                            class="form-control input-sm" ClientEnabled="false">
                            <DisabledStyle BackColor="#CCCCCC" ForeColor="#444444"></DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="hidF_INSPTYPECD" ClientInstanceName="hidF_INSPTYPECD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="hidF_INSPTYPENM" ClientInstanceName="hidF_INSPTYPENM" runat="server" ClientVisible="false" />
                    </td>
                    <td class="tdInput">&nbsp;</td>
                    <%--<td class="tdLabel required">
                        <label>설비번호</label>
                    </td>
                    <td class="tdInput">
                        <ucCTF:Machine ID="ucMachine" runat="server" />
                    </td>--%>
                </tr>
            </table>
        </div>
        <div class="divLine" style="height:20px; text-align:center;"><label style="color:black;">점검기준</label></div> 
        <div class="content">
            <table border="1" class="contentTable">
                <colgroup>
                    <col style="width:11.1%" />
                    <col style="width:22.2%" />
                    <col style="width:11.1%" />
                    <col style="width:22.2%" />
                    <col style="width:33.4%" />
                </colgroup>
                <tr>
                    <td class="tdLabel required">
                        <label>점검항목</label>
                    </td>
                    <td class="tdInput">
                        <div style="float: left; width: 40%; padding-right: 3px;">
                        <dx:ASPxTextBox ID="txtINSPCD" ClientInstanceName="txtINSPCD" runat="server" Width="100%" class="form-control input-sm"
                            OnInit="txtINSPCD_Init">
                            <ClientSideEvents LostFocus="fn_OnINSPCDLostFocus" />
                        </dx:ASPxTextBox>
                       </div>
                        <div style="float: left; width: 60%;">
                        <dx:ASPxTextBox ID="txtINSPNM" ClientInstanceName="txtINSPNM" runat="server" Width="100%" class="form-control input-sm">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                        </div>
                    </td>
                    <td class="tdLabel required">
                        <label>점검분류</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="ddlINSPCD" ClientInstanceName="ddlINSPCD" runat="server" Width="100%"
                        OnDataBound="ddlComboBox_DataBound"
                        IncrementalFilteringMode="None" CssClass="NoXButton">
                        <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_idxchang" />
                    </dx:ASPxComboBox>
                    <dx:ASPxTextBox ID="txtINSPGBN" ClientInstanceName="txtINSPGBN" runat="server" Width="100%" class="form-control input-sm" ClientVisible="false" />
                    </td>
                    <td class="tdLabel">
                        <label>설비사진</label>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel required">
                        <label>점검순서</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxSpinEdit ID="txtINSPORDER" ClientInstanceName="txtINSPORDER" runat="server" Width="100%" AllowMouseWheel="false" SpinButtons-ClientVisible="false"></dx:ASPxSpinEdit>
                    </td>
                    <td class="tdLabel required">
                        <label>규격</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" class="form-control input-sm" />
                    </td>
                    <td class="tdInput" rowspan="6">
                        <dx:ASPxImage ID="srcImage" ClientInstanceName="srcImage" runat="server"></dx:ASPxImage>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>점검내용</label>
                    </td>
                    <td class="tdInput" colspan ="3">
                        <dx:ASPxTextBox ID="txtINSPREMARK" ClientInstanceName="txtINSPREMARK" runat="server" Width="100%" class="form-control input-sm" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>점검기준</label>
                    </td>
                    <td class="tdInput" colspan="3">
                        <dx:ASPxTextBox ID="txtINSPSTAND" ClientInstanceName="txtINSPSTAND" runat="server" Width="100%" class="form-control input-sm" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>상한</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxSpinEdit ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%" AllowMouseWheel="false" SpinButtons-ClientVisible="false"></dx:ASPxSpinEdit>
                    </td>
                    <td class="tdLabel">
                        <label>하한</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxSpinEdit ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%" AllowMouseWheel="false" SpinButtons-ClientVisible="false"></dx:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>단위</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="ddlUNIT" ClientInstanceName="ddlUNIT" runat="server" Width="100%"
                                OnDataBound="ddlComboBox_DataBound"
                                IncrementalFilteringMode="None" CssClass="NoXButton">
                                <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_idxchang3" />
                            </dx:ASPxComboBox>
                        <dx:ASPxTextBox ID="txtUNIT" ClientInstanceName="txtUNIT" runat="server" Width="100%" class="form-control input-sm" ClientVisible="false" />
                    </td>
                    <td class="tdLabel required">
                        <label>점검주기</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="ddlCYCLE" ClientInstanceName="ddlCYCLE" runat="server" Width="100%"
                        OnDataBound="ddlComboBox_DataBound"
                        IncrementalFilteringMode="None" CssClass="NoXButton">
                        <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_idxchang2" />
                    </dx:ASPxComboBox>
                        <dx:ASPxTextBox ID="txtCYCLE" ClientInstanceName="txtCYCLE" runat="server" Width="100%" class="form-control input-sm" ClientVisible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>사용여부</label>
                    </td>
                    <td class="tdInput" colspan="3">
                        <dx:ASPxRadioButtonList ID="rdoUSEYN" ClientInstanceName="rdoUSEYN" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                            <Items>
                                <dx:ListEditItem Text="사용" Value="1" Selected="true" />
                                <dx:ListEditItem Text="사용 안함" Value="0" Selected="false" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">
                        <label>점검기준담당자</label>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxButton ID="btnWorker" ClientInstanceName="btnWorker" runat="server" Text="담당자등록" AutoPostBack="false">
                            <ClientSideEvents Click="fn_OnBtnWorkersClick" />
                        </dx:ASPxButton>
                    </td>
                    <td class="tdInput" colspan="2">
                        <dx:ASPxTextBox ID="txtWorkers" ClientInstanceName="txtWorkers" runat="server" Width="100%" ClientEnabled="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="srcWORKERS_TEAM" ClientInstanceName="srcWORKERS_TEAM" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="srcWORKERS_USER" ClientInstanceName="srcWORKERS_USER" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxButton ID="btnImageFile" ClientInstanceName="btnImageFile" runat="server" Text="첨부파일" AutoPostBack="false">
                            <ClientSideEvents Click="fn_OnBtnImageClick" />
                        </dx:ASPxButton>
                        <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divLine"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD;F_MACHCD;F_INSPTYPECD;F_INSPSERIAL" EnableViewState="false" EnableRowsCache="false"
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
                    <dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MACHCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_INSPTYPECD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_INSPSERIAL" Visible="false" />

                    <dx:GridViewDataColumn FieldName="NO" Caption="No." Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="항목" Width="60px" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_INSPKINDNM" Caption="분류" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_INSPORDER" Caption="점검순서" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_CYCLENM" Caption="점검주기" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_UNITNM" Caption="단위" Width="60px" />
                    
                    
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            <div class="paging"></div>
        </div>

        </div>
        <div style="clear:both;"></div>
        <div id="divHiddenLayer" style="display:none;">
            <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" ClientVisible="false" Text="NEW"></dx:ASPxTextBox>
            <dx:ASPxTextBox ID="hidSerialno" ClientInstanceName="hidSerialno" runat="server" ClientVisible="false"></dx:ASPxTextBox>
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
