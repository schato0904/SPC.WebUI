<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS1001.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS1001" %>
<%-- 측정기정보관리 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';        
        var _fieldList = "<%=FieldList%>";

        $(document).ready(function () {
            var fileno = srcF_IMGATTFILENO.GetText();
            if (fileno != "") {
                fn_GetOpenImage({ "FileNO": fileno });
            }
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(devGrid.GetMainElement()).offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {            
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            fn_SetPageMode("NEW");
            fn_InputArea_Init();
            fn_OnValidate();
        }

        // 입력항목 초기화
        function fn_InputArea_Init() {
            // 필드 클리어
            fn_ClearFields(_fieldList, "src");
            $("#itemImg").prop('src', '');
            srcF_TEAMCD.PerformCallback();
        }

        // 수정
        function fn_OnModifyClick() {
            
        }

        // 저장
        function fn_OnSaveClick() {
            if (false == fn_OnValidate()) {
                alert('필수항목을 모두 입력해 주십시오.');
                return false;
            }

            if (hidPageMode.GetText() == 'EDIT') {
                if (!confirm('데이터를 수정하시겠습니까?')) {
                    return false;
                }
            }

            devCallback.PerformCallback('SAVE');
        }

        // 취소
        function fn_OnCancelClick() {
            fn_SetPageMode("NEW");
            fn_InputArea_Init();
            fn_OnValidate();
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (hidPageMode.GetText() != 'EDIT') {
                alert('삭제할 데이타를 조회하세요!!');
                return false;
            }

            selectedKey = srcF_MS01MID.GetText();

            // 1 Row 이상 반드시 선택
            if (selectedKey.length <= 0) {
                alert('삭제할 데이타를 조회하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 데이타는 복원할 수 없습니다.')) { return false; }

            devCallback.PerformCallback('DEL');
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        // 저장, 삭제 후 처리
        function devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            // 정상일 경우 그리드 재 조회
            if (result.CODE == "1") {
                switch (result.TYPE) {
                    case "AfterSave":
                        devGrid.PerformCallback(result.TYPE + ";" + result.PKEY);
                        break;
                    case "AfterDelete":
                        // 그리드 조회 후
                        devGrid.PerformCallback(result.TYPE);
                        // 입력화면 클리어
                        fn_OnNewClick();
                        break;                   
                    default: break;
                }
            }

            alert(result.MESSAGE);
        }

        // 오류시
        function devCallback_CallbackError(s, e) {
            alert(e);
        }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);
                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
            else {
                if (s.cpResultEtc == "AfterSave") {
                    fn_SetPageMode("EDIT");
                    s.cpResultEtc = "";
                }
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // 그리드 더블클릭
        function fn_devGridOnRowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            if (key == null || key == undefined || key == "") {
                return;
            }

            cbpContent.PerformCallback(key);
        }

        // cbpContent End Callback
        function cbpContent_EndCallback(s, e) {
            fn_SetPageMode("EDIT");
            fn_OnValidate();            
            fn_GetOpenImage(srcF_IMGATTFILENO.GetText());
        }

        // cbpContent Callback Error
        function cbpContent_CallbackError(s, e) {
            alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            return ASPxClientEdit.ValidateEditorsInContainerById('cbpContent');
        }

        // 화면 로드시 필수 필드 표시
        function cbpContent_Init(s, e) {
            fn_OnValidate();
        }

        // 페이지 모드 체인지 : NEW, EDIT, REV
        function fn_SetPageMode(mode) {
            if (mode == "NEW") {
                srcF_TEAMCD.SetEnabled(true);
                srcF_USER.SetEnabled(true);
                srcF_STATUSCD.SetEnabled(true);
                srcF_ABNORMALCD.SetEnabled(true);
            }
            hidPageMode.SetText(mode);
        }

        // 이미지첨부파일 버튼 클릭
        function fn_OnImgAttfileClick() {
            fn_AttachFileOpenCallBack('측정기이미지등록', 'M', 'F', 'srcF_IMGATTFILENO', 'srcF_IMGATTFILECNT',
                ASPxClientTextBox.Cast('srcF_COMPCD').GetText(), 'fn_GetOpenImage');

        }

        // 첨부파일 버튼 클릭
        function fn_OnAttfileClick() {
            fn_AttachFileOpenParam2('파일등록', 'M', 'T', 'srcF_ATTFILENO', 'srcF_ATTFILECNT', ASPxClientTextBox.Cast('srcF_COMPCD').GetText());
            //fn_AttachFileOpenParam('파일등록', 'M', 'T', ASPxClientTextBox.Cast("srcF_COMPCD").GetText());
        }

        function fn_OnMEAS1001OpenPopup() {
            fn_OnPopupMEAS1001POPSearch('fn_OnMEAS1001OpenPopupCallBack', srcF_MS01MID.GetText(), srcF_PART_INFO.GetText());
        }

        function fn_OnMEAS1001OpenPopupCallBack(result)
        {
            if (result != "") {
                srcF_PART_INFO.SetText(result);
            }
        }

        function fn_GetOpenImage(result) {
            var compCd = srcF_COMPCD.GetText();
            var src = "/SPC.WebUI/API/Common/Download.ashx?attfileno="
                + result + "&attfileseq=1&data_gbn=M&compcd=" + compCd + "&viewyn=y";

            $("#itemImg").prop("src", src);
        }

        function fn_UploadedComplete(val) {
            fn_SetTextValue('srcF_IMGATTFILENO', val);
            fn_GetOpenImage(val);
        }
        function fn_F_EQUIPDIVCD_CHANGED(s, e) {

            //alert(srcF_EQUIPDIVCD.GetValue());
            srcF_EQUIPTYPECD.PerformCallback(srcF_EQUIPDIVCD.GetValue());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback" ClientSideEvents-CallbackError="devCallback_CallbackError" ClientSideEvents-CallbackComplete="devCallback_CallbackComplete" />
            <section id="sectionContent" class="panel panel-default wrapper-sm" style="padding:0;border: 0px none transparent;margin-bottom:0px;">
                <dx:ASPxCallbackPanel ID="cbpContent" ClientInstanceName="cbpContent" runat="server" CssClass="form-horizontal" OnCallback="cbpContent_Callback" >
                    <ClientSideEvents Init="cbpContent_Init" EndCallback="cbpContent_EndCallback" CallbackError="cbpContent_CallbackError" />
                    <PanelCollection>
                        <dx:HtmlEditorRoundPanelContent>
                            <div id="divHidden" style="display:none;">
                                <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" Width="100%" ClientVisible="false" Text="NEW" />
                                <dx:ASPxTextBox ID="srcF_MS01MID" ClientInstanceName="srcF_MS01MID" runat="server" Width="100%" ClientVisible="false"/>
                                <dx:ASPxTextBox ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" runat="server" Width="100%" ClientVisible="false"/>
                                <dx:ASPxTextBox ID="srcF_PART_INFO" ClientInstanceName="srcF_PART_INFO" runat="server" Width="100%" ClientVisible="false"/>
                            </div>
                            <table class="InputTable">
                                <colgroup>
                                    <col style="width:9%" />
                                    <col style="width:16%" />
                                    <col style="width:9%" />
                                    <col style="width:16%" />
                                    <col style="width:9%" />
                                    <col style="width:16%" />
                                    <col style="width:9%" />
                                    <col style="width:16%" />
                                </colgroup>
                                <tr>
                                    <td class="tdTitle">
                                        <label>관리번호</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_EQUIPNO" ClientInstanceName="srcF_EQUIPNO" runat="server" Width="100%">
                                            <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>측정기명</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_EQUIPNM" ClientInstanceName="srcF_EQUIPNM" runat="server" Width="100%">
                                            <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>측정기분류</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_EQUIPDIVCD" ClientInstanceName="srcF_EQUIPDIVCD" Width="100%" ClientSideEvents-SelectedIndexChanged="fn_F_EQUIPDIVCD_CHANGED">
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="tdContent" colspan="2" rowspan="6">
                                        <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    <button class="btn btn-sm btn-success" onclick="fn_OnMEAS1001OpenPopup(); return false;">
                                                       <i class="fa fa-search"></i>
                                                       <span class="text">부속품등록</span>
                                                   </button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height:20px; background:#DCDCDC; border-left: 1px solid #000000;border-top:1px solid #000000;border-right:1px solid #000000;">
                                                    <span style="font-weight:bold;color:#000">&nbsp;[측정기 사진]</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height:140px; border-left: 1px solid #000000;border-right:1px solid #000000;">
                                                    <img id="itemImg" alt="측정기이미지" src="" height="129" width="100%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border-left: 1px solid #000000;border-bottom:1px solid #000000;border-right:1px solid #000000;">
                                                    <dx:ASPxTextBox ID="srcF_IMGATTFILECNT" ClientInstanceName="srcF_IMGATTFILECNT" runat="server" Width="100%" class="form-control input-sm" ClientVisible="false"></dx:ASPxTextBox>
                                                    <dx:ASPxTextBox ID="srcF_IMGATTFILENO" ClientInstanceName="srcF_IMGATTFILENO" ClientVisible="false" runat="server" Width="100%" class="form-control input-sm">
                                                    </dx:ASPxTextBox>
                                                    <span style="width:100%">
                                                        <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_OnImgAttfileClick(); return false; " >
                                                            <i class="i i-file-plus "></i>
                                                            <span class="text">파일첨부</span>
                                                        </button>
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdTitle">
                                        <label>모델</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_MODEL" ClientInstanceName="srcF_MODEL" runat="server" Width="100%"></dx:ASPxTextBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>제조번호</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_PRODNO" ClientInstanceName="srcF_PRODNO" runat="server" Width="100%">
                                            <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>측정기구분</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_EQUIPTYPECD" ClientInstanceName="srcF_EQUIPTYPECD" Width="100%" OnCallback="srcF_EQUIPDIVCD_Callback">
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdTitle">
                                        <label>가격</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxSpinEdit ID="srcF_PRICE" ClientInstanceName="srcF_PRICE" runat="server" Width="100%" DisplayFormatString="#,0" SpinButtons-ClientVisible="false">
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td class="tdTitle">
                                        <label>제조회사</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_MAKERCD" ClientInstanceName="srcF_MAKERCD" runat="server" Width="100%"></dx:ASPxTextBox>
                                    </td>
                                    <td class="tdTitle" rowspan="3">
                                        <label>점검개소(항목)</label>
                                    </td>
                                    <td class="tdContent" rowspan="3">
                                        <dx:ASPxMemo ID="srcF_MEMO" ClientInstanceName="srcF_MEMO" runat="server" Width="100%" Height="100%"
                                        class="form-control input-sm" >                                                
                                    </dx:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdTitle">
                                        <label>도입일자</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxDateEdit runat="server" ID="srcF_INDT" ClientInstanceName="srcF_INDT" Width="100%">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td class="tdTitle">
                                        <label>도면번호</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_PICNO" ClientInstanceName="srcF_PICNO" runat="server" Width="100%"></dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdTitle">
                                        <label>측정단위</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_GRADECD" ClientInstanceName="srcF_GRADECD" Width="100%">
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>규격</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_STAND" ClientInstanceName="srcF_STAND" runat="server" Width="100%">
                                            <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdTitle">
                                        <label>첨부파일수</label>
                                    </td>
                                    <td class="tdContent">
                                        <div style="float:left;">
                                            <dx:ASPxTextBox ID="srcF_ATTFILECNT" ClientInstanceName="srcF_ATTFILECNT" runat="server" Width="100%" class="form-control input-sm" ClientSideEvents-Init="fn_OnControlDisableBox" >
                                                <ClientSideEvents Init="fn_OnControlDisableBox"></ClientSideEvents>
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="srcF_ATTFILENO" ClientInstanceName="srcF_ATTFILENO" runat="server" Width="100%" class="form-control input-sm" ClientVisible="false" />
                                        </div>
                                        <div style="float:left;margin-left:2px;">
                                            <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" ClientVisible="false" runat="server" Width="100%" class="form-control input-sm" Text="" />
                                            <button class="btn btn-sm btn-warning" style="width: 100%; padding-top: 3px;padding-bottom:3px;" onclick="fn_OnAttfileClick(); return false;" >
                                                <i class="i i-file-plus "></i>
                                                <span class="text">파일첨부</span>
                                            </button>
                                        </div>
                                    </td>
                                    <td class="tdTitle">
                                        <label>비고</label>
                                    </td>
                                    <td class="tdContent" colspan="3">
                                       <dx:ASPxTextBox ID="srcF_ETC" ClientInstanceName="srcF_ETC" runat="server" Width="100%"></dx:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                            <table class="InputTable" style="margin-top:5px;">
                                <colgroup>
                                    <col style="width:9%" />
                                    <col style="width:16%" />
                                    <col style="width:9%" />
                                    <col style="width:16%" />
                                    <col style="width:9%" />
                                    <col style="width:16%" />
                                    <col style="width:9%" />
                                    <col style="width:16%" />
                                </colgroup>
                                <tr>
                                    <td class="tdTitle">
                                        <label>사용팀</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_TEAMCD" ClientInstanceName="srcF_TEAMCD" Width="100%" OnCallback="srcF_TEAMCD_Callback">
                                            <ClientSideEvents Init="fn_OnControlDisable"/>
                                            <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>사용자</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_USER" ClientInstanceName="srcF_USER" runat="server" Width="100%"></dx:ASPxTextBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>상태구분</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_STATUSCD" ClientInstanceName="srcF_STATUSCD" Width="100%">
                                             <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>이상처리구분</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_ABNORMALCD" ClientInstanceName="srcF_ABNORMALCD" Width="100%"></dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdTitle">
                                        <label>판정구분</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_JUDGECD" ClientInstanceName="srcF_JUDGECD" Width="100%">
                                             <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>교정구분</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_FIXTYPECD" ClientInstanceName="srcF_FIXTYPECD" Width="100%">
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>교정분야</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_FIXDIVCD" ClientInstanceName="srcF_FIXDIVCD" Width="100%"></dx:ASPxComboBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>교정번호</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_FIXNO" ClientInstanceName="srcF_FIXNO" runat="server" Width="100%"></dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdTitle">
                                        <label>교정기관</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxComboBox runat="server" ID="srcF_FIXGRPCD" ClientInstanceName="srcF_FIXGRPCD" Width="100%">
                                             <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="tdTitle">
                                        <label>최종교정일</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxDateEdit runat="server" ID="srcF_LASTFIXDT" ClientInstanceName="srcF_LASTFIXDT" Width="100%">
                                            <ValidationSettings RequiredField-IsRequired="true"
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic">
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td class="tdTitle">
                                        <label>주기(개월)</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox runat="server" ID="srcF_TERMMONTH" ClientInstanceName="srcF_TERMMONTH" Width="100%">
                                             <ValidationSettings RequiredField-IsRequired="true" 
                                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="tdTitle" style="display:none;">
                                        <label>교정예정일</label>
                                    </td>
                                    <td class="tdContent" style="display:none;">
                                        <dx:ASPxDateEdit runat="server" ID="srcF_FIXPLANDT" ClientInstanceName="srcF_FIXPLANDT" Width="100%">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td class="tdTitle">
                                        <label>비고</label>
                                    </td>
                                    <td class="tdContent">
                                        <dx:ASPxTextBox ID="srcF_REMARK" ClientInstanceName="srcF_REMARK" runat="server" Width="100%"></dx:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </dx:HtmlEditorRoundPanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </section>
            <div class="divline">&nbsp;</div>
            <section id="searchCondition" class="panel panel-default wrapper-sm" style="padding:0;border: 0px none transparent;margin-bottom:5px;">
                <table class="InputTable">
                    <colgroup>
                        <col style="width:7%" />
                        <col style="width:12%" />
                        <col style="width:7%" />
                        <col style="width:12%" />
                        <col style="width:7%" />
                        <col style="width:12%" />
                        <col style="width:7%" />
                        <col style="width:12%" />
                        <col style="width:7%" />
                        <col style="width:12%" />
                        <col style="width:5%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">
                            <label>측정기분류</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="srcF_EQUIPDIVCD_2" ClientInstanceName="srcF_EQUIPDIVCD_2" Width="100%"></dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">
                            <label>관리번호</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_EQUIPNO_2" ClientInstanceName="schF_EQUIPNO_2" runat="server" Width="100%"></dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">
                            <label>측정기명</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_EQUIPNM_2" ClientInstanceName="schF_EQUIPNM_2" runat="server" Width="100%"></dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="srcF_MAKERCD_2" ClientInstanceName="srcF_MAKERCD_2" runat="server" Width="100%" ClientVisible="false"></dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">
                            <label>상태구분</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="srcF_STATUSCD_2" ClientInstanceName="srcF_STATUSCD_2" Width="100%"></dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">
                            <label>사용팀</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="srcF_TEAMCD_2" ClientInstanceName="srcF_TEAMCD_2" Width="100%"></dx:ASPxComboBox>
                        </td>
                        <td class="tdContent" style="text-align:right;padding-right:10px;">
                            <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick(); return false;">
                               <i class="fa fa-search"></i>
                               <span class="text">조회</span>
                           </button>
                        </td>
                    </tr>
                </table>
            </section>
        </div>
        <div class="content">
            <section class="panel panel-default" style="height: 100%;">
                <dx:ASPxTextBox ID="hidcuidx" ClientInstanceName="hidcuidx" runat="server" ClientVisible="false" />
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback"
                    OnDataBinding="devGrid_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                    <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowFocusedRow="true" ColumnResizeMode="NextColumn"/>
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridOnRowDblClick" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="F_FACTNM" Caption="공장구분" Width="100px" FixedStyle="Left">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px" FixedStyle="Left">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="110px" FixedStyle="Left">
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_PRODNO" Caption="제조번호" Width="100px" FixedStyle="Left" CellStyle-HorizontalAlign="Left">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_STAND" Caption="규격" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_MODEL" Caption="모델" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_INDT" Caption="도입일자" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_MAKERCD" Caption="제조회사" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_USER" Caption="사용자" Width="70px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_TEAMNM" Caption="사용팀" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_FIXNO" Caption="교정번호" Width="110px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_LASTFIXDT" Caption="최종교정일" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_FIXPLANDT" Caption="교정예정일" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_EQUIPTYPENM" Caption="측정기구분" Width="100px">                            
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_GRADENM" Caption="측정단위" Width="80px">                            
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_STATUSNM" Caption="상태구분" Width="80px">                            
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_JUDGENM" Caption="판정" Width="80px">                            
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_FIXTYPENM" Caption="교정구분" Width="80px">                            
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_FIXGRPNM" Caption="교정기관" Width="80px">                            
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_ATTFILECNT" Caption="첨부파일수" Width="80px">                            
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
            </section>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>