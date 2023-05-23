<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0201.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0201" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_OnSearchClick();
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $('#imgPanel').height(height);
        }

        // 조회
        function fn_OnSearchClick() {
            var data = {
                'ACTION': 'GET'
            };
            fn_devCallback_PerformCallback(data);
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (!fn_OnValidate()) return false;

            var data = {
                'ACTION': 'SAVE'
            };
            fn_devCallback_PerformCallback(data);
        }

        // 취소
        function fn_OnCancelClick() {

        }

        // 삭제
        function fn_OnDeleteClick() {

        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

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
            if (fn_GetCastValue('txtIMAGESEQ') == '' || fn_GetCastValue('srcF_ORGWIDTH') == '' || fn_GetCastValue('srcF_ORGHEIGHT') == '') {
                alert('공장레이아웃등록 버튼을 클릭하여 레이아웃을 등록하세요');
                return false;
            }
            if (fn_GetCastValue('srcF_USEWIDTH') == '') {
                alert('지정사이즈(너비)을 입력하세요!!');
                fn_Focus('srcF_USEWIDTH');
                return false;
            }
            if (fn_GetCastValue('srcF_USEHEIGHT') == '') {
                alert('지정사이즈(높이)을 입력하세요!!');
                fn_Focus('srcF_USEHEIGHT');
                return false;
            }

            return true;
        }

        // 도면 업로드 완료 후
        function fn_UploadedComplete(val) {
            fn_SetTextValue('txtIMAGESEQ', val);
            
            if (val != '') {
                var imageUrl = rootURL + 'API/Common/Download.ashx'
                    + '?attfileno=' + val
                    + '&attfileseq=1'
                    + '&data_gbn=E'
                    + '&compcd=<%=gsCOMPCD%>';

                var layoutImg = $('#imgLayout');
                layoutImg.attr('src', imageUrl);
                layoutImg.load(function () {
                    fn_SetTextValue('srcF_ORGWIDTH', layoutImg.width());
                    fn_SetTextValue('srcF_ORGHEIGHT', layoutImg.height());
                    fn_SetTextValue('srcF_USEWIDTH', layoutImg.width());
                    fn_SetTextValue('srcF_USEHEIGHT', layoutImg.height());
                });
            }
        }

        // 사용자크기 변경 시
        function fn_OnUseKeyUp(t) {
            var imgSeq = fn_GetCastValue('txtIMAGESEQ');

            if (imgSeq == '') {
                alert('공장레이아웃등록 버튼을 클릭하여 레이아웃을 먼저 등록하세요');
                fn_SetTextValue('srcF_USEWIDTH', '');
                fn_SetTextValue('srcF_USEHEIGHT', '');
            } else {
                fn_OnCalcUseSize(t);
            }
        }

        // 지정사이즈 비율별 크기계산
        function fn_OnCalcUseSize(t) {
            if (fn_GetCastValue('srcF_USEWIDTH') == '' && fn_GetCastValue('srcF_USEHEIGHT') == '') {
                alert('지정사이즈는 반드시 입력되야합니다.');
                fn_SetTextValue('srcF_USEWIDTH', fn_GetCastValue('srcF_ORGWIDTH'));
                fn_SetTextValue('srcF_USEHEIGHT', fn_GetCastValue('srcF_ORGHEIGHT'));
            } else if (fn_GetCastValue('srcF_USEWIDTH') == '') {
                fn_OnCalcUseSize('h');
            } else if (fn_GetCastValue('srcF_USEHEIGHT') == '') {
                fn_OnCalcUseSize('w');
            } else {
                if (t == 'w') {
                    fn_SetTextValue('srcF_USEHEIGHT', parseInt(fn_GetCastValue('srcF_USEWIDTH') * fn_GetCastValue('srcF_ORGHEIGHT') / fn_GetCastValue('srcF_ORGWIDTH'), 10));
                } else if (t == 'h') {
                    fn_SetTextValue('srcF_USEWIDTH', parseInt(fn_GetCastValue('srcF_ORGWIDTH') * fn_GetCastValue('srcF_USEHEIGHT') / fn_GetCastValue('srcF_ORGHEIGHT'), 10));
                }
            }
        }

        // 비율고정 변경시
        function fn_OnFixRatioCheckedChanged(s, e) {
            var imgSeq = fn_GetCastValue('txtIMAGESEQ');

            if (imgSeq != '' && fn_GetCastChecked('srcF_FIXRATIO') == true) {
                fn_OnCalcUseSize(t);
            }
        }

        // 입력폼 조회값입력
        function fn_OnInputData(pagedata) {
            var imageIDX = GetJsonValueByKey(pagedata, 'F_IMAGEIDX', '');
            
            if (imageIDX == '') {
                return false;
            }

            fn_SetTextValue('srcF_IMAGEIDX', imageIDX);
            fn_SetTextValue('txtIMAGESEQ', GetJsonValueByKey(pagedata, 'F_IMAGESEQ', ''));
            fn_SetTextValue('srcF_ORGWIDTH', GetJsonValueByKey(pagedata, 'F_ORGWIDTH', ''));
            fn_SetTextValue('srcF_ORGHEIGHT', GetJsonValueByKey(pagedata, 'F_ORGHEIGHT', ''));
            fn_SetTextValue('srcF_USEWIDTH', GetJsonValueByKey(pagedata, 'F_USEWIDTH', ''));
            fn_SetTextValue('srcF_USEHEIGHT', GetJsonValueByKey(pagedata, 'F_USEHEIGHT', ''));
            fn_SetChecked('srcF_FIXRATIO', GetJsonValueByKey(pagedata, 'F_FIXRATIO', ''));

            fn_UploadedComplete(GetJsonValueByKey(pagedata, 'F_IMAGESEQ', ''));
        }

        // CallbackPanel Event Handler 시작
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

        // 콜백 성공시 처리 : 성공시 PKEY에 JSON 구조로 키값을 받는다.
        function fn_devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            var parameter = JSON.parse(e.parameter);
            var isOK = typeof (result.ISOK) != 'undefined' ? result.ISOK : null;
            var msg = typeof (result.MSG) != 'undefined' ? result.MSG : null;
            if (isOK) {
                var pkey = typeof (result.PKEY) != 'undefined' ? result.PKEY : null;
                var data = {};
                if (typeof (result.PAGEDATA) != 'undefined') {
                    data = result.PAGEDATA;
                } else {
                    for (var key in pkey) { data[key] = pkey[key]; }
                }
                var action = typeof (parameter.ACTION) != 'undefined' ? parameter.ACTION : null;

                switch (action) {
                    case "SAVE":
                        alert(msg);
                        fn_OnSearchClick();
                        break;
                    case "GET":
                        fn_OnInputData(data);
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxTextBox ID="srcF_IMAGEIDX" ClientInstanceName="srcF_IMAGEIDX" runat="server" ClientVisible="false" />
            <table class="contentTable">
                <colgroup>
                    <col style="width:150px" />
                    <col style="width:150px" />
                    <col style="width:150px" />
                    <col style="width:150px" />
                    <col style="width:150px" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel" colspan="2">도면사이즈</td>
                    <td class="tdLabel" colspan="3">지정사이즈</td>
                    <td class="tdLabel">공장레이아웃</td>
                </tr>
                <tr>
                    <td class="tdLabel required">너비</td>
                    <td class="tdLabel required">높이</td>
                    <td class="tdLabel">비율고정</td>
                    <td class="tdLabel required">너비</td>
                    <td class="tdLabel required">높이</td>
                    <td class="tdInput" rowspan="2" style="text-align:center;">
                        <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false" />
                        <button class="btn btn-sm btn-success" onclick="fn_AttachFileOpen('공장레이아웃등록', 'E', 'F'); return false;">
                            <i class="fa fa-upload"></i>
                            <span class="text">공장레이아웃등록</span>
                        </button>
                    </td>
                </tr>
                <tr>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_ORGWIDTH" ClientInstanceName="srcF_ORGWIDTH" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_ORGHEIGHT" ClientInstanceName="srcF_ORGHEIGHT" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdInput" style="text-align:center;">
                        <dx:ASPxCheckBox ID="srcF_FIXRATIO" ClientInstanceName="srcF_FIXRATIO" runat="server" Text="비율고정" Layout="Flow" Checked="true">
                            <ClientSideEvents CheckedChanged="fn_OnFixRatioCheckedChanged" />
                        </dx:ASPxCheckBox>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_USEWIDTH" ClientInstanceName="srcF_USEWIDTH" runat="server" Width="100%">
                            <ClientSideEvents KeyPress="fn_ValidateOnlyNumber" KeyUp="function() {fn_OnUseKeyUp('w');}" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_USEHEIGHT" ClientInstanceName="srcF_USEHEIGHT" runat="server" Width="100%">
                            <ClientSideEvents KeyPress="fn_ValidateOnlyNumber" KeyUp="function() {fn_OnUseKeyUp('h');}" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
            <div class="divLine"></div>
        </div>
        <div class="content">
            <div id="imgPanel" style="overflow:scroll;width:100%;background-color:#e0e0e0;"><img id="imgLayout" src="" /></div>
        </div>
        <div class="paging"></div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>