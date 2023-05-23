<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0101.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            // 입력상자 Enter Key Event
            $('#inputTable input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_OnSaveClick();
                    return false;
                }
            });

            fn_OnChangeMode('C');

            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick(bAuto) {
            fn_doSetGridEventAction('true');
            fn_OnChangeMode('C');
            if(!bAuto) fn_OnInputClear();
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            fn_OnInputClear();
            fn_OnChangeMode('C');
        }

        // 수정
        function fn_OnModifyClick() {
            fn_OnChangeMode('U');
        }

        // 저장
        function fn_OnSaveClick() {
            if (fn_GetCastValue('hidPageMode') != 'C' && fn_GetCastValue('hidPageMode') != 'U') { alert('입력, 수정모드에서만 저장할 수 있습니다'); return false; }
            if (!fn_OnValidate()) return false;

            var data = {
                'PAGEMODE': fn_GetCastValue('hidPageMode'),
                'ACTION': 'SAVE'
            };
            
            fn_devCallback_PerformCallback(data);
        }

        // 취소
        function fn_OnCancelClick() {
            fn_OnInputClear();
            fn_OnChangeMode('C');
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (fn_GetCastValue('hidPageMode') != 'U') { alert('수정모드에서만 삭제할 수 있습니다\r하단 설비목록에서 삭제할 정보(double-click)를 선택하세요'); return false; }
            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 정보는 복원할 수 없습니다.')) { return false; }
            fn_OnChangeMode('D');
            var data = {
                'PAGEMODE': fn_GetCastValue('hidPageMode'),
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
            if (fn_GetCastValue('srcF_MACHCD') == '') {
                alert('설비코드를 입력하세요!!');
                fn_Focus('srcF_MACHCD');
                return false;
            }
            if (fn_GetCastValue('srcF_MACHNM') == '') {
                alert('설비명을 입력하세요!!');
                fn_Focus('srcF_MACHNM');
                return false;
            }
            if (fn_GetCastValue('srcF_BANCD') == '') {
                alert('반을 선택하세요!!');
                srcF_BANCD.Focus();
                return false;
            }
            if (fn_GetCastValue('srcF_LINECD') == '') {
                alert('라인을 선택하세요!!');
                srcF_LINECD.Focus();
                return false;
            }
            if (fn_GetCastValue('srcF_USEYN') == '') {
                alert('가동/비가동을 선택하세요!!');
                return false;
            }
            if (fn_GetCastValue('srcF_USEYN') == '0' && fn_GetCastValue('srcF_REASON') == '') {
                alert('비가동사유를 선택하세요!!');
                return false;
            }
            if (fn_GetCastValue('srcF_MACHKIND') == '') {
                alert('설비구분을 선택하세요!!');
                srcF_MACHKIND.Focus();
                return false;
            }
            if ((fn_GetCastValue('hidPageMode') == 'U' || fn_GetCastValue('hidPageMode') == 'D') && fn_GetCastValue('srcF_MODREASON') == '') {
                alert('수정/삭제사유를 입력하세요!!');
                fn_Focus('srcF_MODREASON');
                return false;
            }

            return true;
        }

        // 입력폼 모드변경
        function fn_OnChangeMode(t) {
            fn_SetTextValue('hidPageMode', t);

            if (t == 'R') {
                $('#inputTable').find('*').prop('disabled', true);
                $('#tdMode').html('조회모드(새로운 정보를 등록하려면 [입력] 버튼을, 기존 정보를 수정하려면 목록에서 수정할 정보를 선택하세요)');
                $('#tdMODREASON').removeClass('required');
                fn_OnControlEnableBox(srcF_MACHCD, null);
            } else if (t == 'C') {
                $('#inputTable').find('*').prop('disabled', false);
                $('#tdMode').html('신규입력모드(입력한 정보를 저장하려면[저장] 버튼을, 입력을 취소하려면 [취소] 버튼을 누르세요)');
                $('#tdMODREASON').removeClass('required');
                fn_OnControlEnableBox(srcF_MACHCD, null);
            } else if (t == 'U') {
                $('#inputTable').find('*').prop('disabled', false);
                $('#tdMode').html('수정입력모드(입력한 정보를 저장하려면[저장] 버튼을, 입력을 취소하려면 [취소] 버튼을 누르세요)');
                $('#tdMODREASON').addClass('required');
                fn_OnControlDisableBox(srcF_MACHCD, null);
            }
        }

        // 입력폼 초기화
        function fn_OnInputClear() {
            fn_SetTextValue('srcF_MACHIDX', '');
            fn_SetTextValue('srcF_MACHCD', '');
            fn_SetTextValue('srcF_MACHNM', '');
            fn_SetValue('srcF_MACHKIND', null);
            fn_SetValue('srcF_BANCD', null);
            fn_SetValue('srcF_LINECD', null);
            fn_SetTextValue('srcF_SORTNO', '');
            fn_SetValue('srcF_USEYN', '1');
            fn_SetValue('srcF_REASON', null);
            fn_SetTextValue('srcF_MAKER', '');
            fn_SetTextValue('srcF_INDATE', '');
            fn_SetTextValue('srcF_SELLER', '');
            fn_SetTextValue('srcF_PRICE', '');
            fn_SetTextValue('srcF_SPEC', '');
            fn_SetTextValue('srcF_SUBPART', '');
            fn_SetDevImage(''); // 이미지 초기화
            fn_SetTextValue('srcF_REMARK', '');
            fn_SetTextValue('srcF_POINTX', '');
            fn_SetTextValue('srcF_POINTY', '');
            fn_SetTextValue('srcF_MODREASON', '');
        }

        // 입력폼 조회값입력
        function fn_OnInputData(pagedata) {
            var machIDX = GetJsonValueByKey(pagedata, 'F_MACHIDX', '');
            
            if (machIDX == '') {
                alert('조회된 데이터가 없거나 일시적 장애입니다');
                return false;
            }

            var JsonValue = '';

            fn_SetTextValue('srcF_MACHIDX', machIDX);
            fn_SetTextValue('srcF_MACHCD', GetJsonValueByKey(pagedata, 'F_MACHCD', ''));
            fn_SetTextValue('srcF_MACHNM', GetJsonValueByKey(pagedata, 'F_MACHNM', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_MACHKIND', '');
            JsonValue = JsonValue == '' ? null : JsonValue;
            srcF_MACHKIND.SetValue(JsonValue);
            srcF_BANCD.SetValue(GetJsonValueByKey(pagedata, 'F_BANCD', ''));
            srcF_LINECD.SetIntervalValue(GetJsonValueByKey(pagedata, 'F_LINECD', ''));
            fn_SetTextValue('srcF_SORTNO', GetJsonValueByKey(pagedata, 'F_SORTNO', ''));
            fn_SetValue('srcF_USEYN', GetJsonValueByKey(pagedata, 'F_USEYN', ''));
            JsonValue = GetJsonValueByKey(pagedata, 'F_REASON', '');
            JsonValue = JsonValue == '' ? null : JsonValue;
            srcF_REASON.SetValue(JsonValue);
            fn_SetTextValue('srcF_MAKER', GetJsonValueByKey(pagedata, 'F_MAKER', ''));
            fn_SetTextValue('srcF_INDATE', GetJsonValueByKey(pagedata, 'F_INDATE', ''));
            fn_SetTextValue('srcF_SELLER', GetJsonValueByKey(pagedata, 'F_SELLER', ''));
            fn_SetTextValue('srcF_PRICE', GetJsonValueByKey(pagedata, 'F_PRICE', ''));
            fn_SetTextValue('srcF_SPEC', GetJsonValueByKey(pagedata, 'F_SPEC', ''));
            fn_SetTextValue('srcF_SUBPART', GetJsonValueByKey(pagedata, 'F_SUBPART', ''));
            fn_SetDevImage(GetJsonValueByKey(pagedata, 'F_IMAGENO', ''));   // 이미지
            fn_SetTextValue('srcF_REMARK', GetJsonValueByKey(pagedata, 'F_REMARK', ''));
            fn_SetTextValue('srcF_POINTX', GetJsonValueByKey(pagedata, 'F_POINTX', ''));
            fn_SetTextValue('srcF_POINTY', GetJsonValueByKey(pagedata, 'F_POINTY', ''));
            fn_SetTextValue('srcF_MODREASON', GetJsonValueByKey(pagedata, 'F_MODREASON', ''));
        }

        // 가동/비가동 선택시
        function fn_OnUSEYNSelectedIndexChanged(s, e) {
            if (s.GetValue() == '0') {
                $('#tdREASON').addClass('required');
            } else {
                $('#tdREASON').removeClass('required');
            }
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
                        data.ACTION = 'GET';
                        fn_devCallback_PerformCallback(data);
                        fn_OnSearchClick('true');
                        break;
                    case "GET":
                        fn_OnInputData(data);
                        fn_OnModifyClick();
                        break;
                    case "DELETE":
                        alert(msg);
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
        // CallbackPanel Event Handler   끝

        // 그리드 더블 클릭시 선택항목 상세 조회
        function fn_devGrid_RowDblClick(s, e) {
            var data = {
                'F_MACHIDX': devGrid.GetRowKey(e.visibleIndex),
                'ACTION': 'GET'
            };
            fn_devCallback_PerformCallback(data);
        }

        // 등록된 이미지 로드
        function fn_SetDevImage(sIMAGENO) {
            fn_SetTextValue('txtIMAGESEQ', sIMAGENO);
            var objImage = $('#srcImage');
            var objDiv = $('#divImage');
            $(objDiv).width($(objDiv).parent().width());
            $(objDiv).height($(objDiv).parent().height());
            var imageUrl = sIMAGENO == '' ? '' : rootURL + 'API/Common/Download.ashx'
                + '?attfileno=' + sIMAGENO
                + '&attfileseq=1'
                + '&data_gbn=E'
                + '&compcd=<%=gsCOMPCD%>';
            $(objImage).attr('src', imageUrl);
            testImage(imageUrl, objImage);
        }

        // 설비사진 업로드 완료 후
        function fn_UploadedComplete(val) {
            fn_SetTextValue('txtIMAGESEQ', val);

            if (val != '') {
                fn_SetDevImage(val);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" ClientVisible="false" Text="R" /><!--C:신규,R:조회,U:수정-->
            <dx:ASPxTextBox ID="srcF_MACHIDX" ClientInstanceName="srcF_MACHIDX" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="srcF_POINTX" ClientInstanceName="srcF_POINTX" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="srcF_POINTY" ClientInstanceName="srcF_POINTY" runat="server" ClientVisible="false" />
            <table id="inputTable" class="contentTable">
                <colgroup>
                    <col style="width:11%" />
                    <col style="width:26%" />
                    <col style="width:11%" />
                    <col style="width:26%" />
                    <col style="width:26%" />
                </colgroup>
                <tr>
                    <td class="tdLabel">현재상태</td>
                    <td id="tdMode" class="tdInput" colspan="5" style="color:black;font:bold;">조회모드</td>
                </tr>
                <tr>
                    <td class="tdLabel required">설비코드</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MACHCD" ClientInstanceName="srcF_MACHCD" runat="server" Width="100%" />
                    </td>
                    <td class="tdLabel required">설비명</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MACHNM" ClientInstanceName="srcF_MACHNM" runat="server" Width="100%" MaxLength="50" />
                    </td>
                    <td class="tdLabel">설비사진</td>
                </tr>
                <tr>
                    <td class="tdLabel required">반</td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="srcF_BANCD" ClientInstanceName="srcF_BANCD" nullText="선택하세요" targetCtrls="srcF_LINECD" />
                    </td>
                    <td class="tdLabel required">라인</td>
                    <td class="tdInput">
                        <ucCTF:LineMulti runat="server" id="srcF_LINECD" ClientInstanceName="srcF_LINECD" nullText="선택하세요" />
                    </td>
                    <td class="tdInput" rowspan="8" style="background-color:#e0e0e0;">
                        <div id="divImage" style="position:relative;"><img id="srcImage" src="" class="centerImage resizeImageRatio" /></div>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel required">가동/비가동</td>
                    <td class="tdInput">
                        <dx:ASPxRadioButtonList ID="srcF_USEYN" ClientInstanceName="srcF_USEYN" runat="server" Border-BorderStyle="None" RepeatDirection="Horizontal" Paddings-Padding="0">
                            <Items>
                                <dx:ListEditItem Text="가동" Value="1" Selected="true" />
                                <dx:ListEditItem Text="비가동" Value="0" />
                            </Items>
                            <ClientSideEvents SelectedIndexChanged="fn_OnUSEYNSelectedIndexChanged" />
                        </dx:ASPxRadioButtonList></td>
                    <td id="tdREASON" class="tdLabel">비가동사유</td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" id="srcF_REASON" ClientInstanceName="srcF_REASON" F_CODEGROUP="44" nullText="선택하세요" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel required">설비분류</td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" id="srcF_MACHKIND" ClientInstanceName="srcF_MACHKIND" F_CODEGROUP="40" nullText="선택하세요" />
                    </td>
                    <td class="tdLabel">도입일자</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_INDATE" ClientInstanceName="srcF_INDATE" runat="server" Width="100%" MaxLength="10" MaskSettings-Mask="9999.99.99" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">제조사</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MAKER" ClientInstanceName="srcF_MAKER" runat="server" Width="100%" MaxLength="80" />
                    </td>
                    <td class="tdLabel">구입가격</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_PRICE" ClientInstanceName="srcF_PRICE" runat="server" Width="100%" MaxLength="40" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">구입처</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_SELLER" ClientInstanceName="srcF_SELLER" runat="server" Width="100%" MaxLength="80" />
                    </td>
                    <td class="tdLabel">정렬순번</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_SORTNO" ClientInstanceName="srcF_SORTNO" runat="server" Width="100%" NullText="숫자만가능">
                            <ClientSideEvents KeyPress="fn_ValidateOnlyNumberAbs" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">사양</td>
                    <td colspan="3" class="tdInput">
                        <dx:ASPxTextBox ID="srcF_SPEC" ClientInstanceName="srcF_SPEC" runat="server" Width="100%" MaxLength="200" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">주요부속품</td>
                    <td colspan="3" class="tdInput">
                        <dx:ASPxTextBox ID="srcF_SUBPART" ClientInstanceName="srcF_SUBPART" runat="server" Width="100%" MaxLength="200" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">비고</td>
                    <td colspan="3" class="tdInput">
                        <dx:ASPxTextBox ID="srcF_REMARK" ClientInstanceName="srcF_REMARK" runat="server" Width="100%" MaxLength="1000" />
                    </td>
                </tr>
                <tr>
                    <td id="tdMODREASON" class="tdLabel">수정/삭제사유</td>
                    <td colspan="3" class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MODREASON" ClientInstanceName="srcF_MODREASON" runat="server" Width="100%" MaxLength="100" />
                    </td>
                    <td class="tdInput" style="text-align:right;">
                        <button class="btn btn-sm btn-success" onclick="fn_AttachFileOpen('설비사진등록', 'E', 'F'); return false;">
                            <i class="fa fa-upload"></i>
                            <span class="text">설비사진등록</span>
                        </button>
                    </td>
                </tr>
            </table>
            <div class="divLine"></div>
            <table class="contentTable">
                <colgroup>
                    <col style="width:8%;" />
                    <col style="width:17%;" />
                    <col style="width:8%;" />
                    <col style="width:17%;" />
                    <col style="width:8%;" />
                    <col style="width:17%;" />
                    <col style="width:8%;" />
                    <col style="width:17%;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">반</td>
                    <td class="tdInput">
                        <ucCTF:BanMulti runat="server" id="schF_BANCD" ClientInstanceName="schF_BANCD" targetCtrls="schF_LINECD" />
                    </td>
                    <td class="tdLabel">라인</td>
                    <td class="tdInput">
                        <ucCTF:LineMulti runat="server" ID="schF_LINECD" ClientInstanceName="schF_LINECD" />
                    </td>
                    <td class="tdLabel">설비분류</td>
                    <td class="tdInput">
                        <ucCTF:SYCOD01 runat="server" ID="schF_MACHKIND" ClientInstanceName="schF_MACHKIND" F_CODEGROUP="40" />
                    </td>
                    <td class="tdLabel">설비명</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" Width="100%" MaxLength="50" />
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MACHIDX" EnableViewState="false" EnableRowsCache="false"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGrid_RowDblClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="NO" Caption="No." Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="180px" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="180px" />
                    <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="100%">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MACHKINDNM" Caption="설비분류" Width="180px" />
                    <dx:GridViewDataColumn FieldName="F_USEYN" Caption="가동여부" Width="70px" />
                    
                    <dx:GridViewDataColumn FieldName="F_MACHIDX" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
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