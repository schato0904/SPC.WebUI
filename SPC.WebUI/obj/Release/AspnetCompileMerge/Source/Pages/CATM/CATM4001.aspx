<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM4001.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM4001" %>
<%-- 금형등록 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {
            //fn_InitRequired('required', 'divPopContent1');
            //srcF_JUDGECD.SetEnabled(false);
            // 검사성적 영역 높이 설정
            //var h = $(document).height() - $('#divRight').offset().top - 18;
            //$('#divRight').height(h);

            //fn_InitRequired('required', 'divRightTop');
            //var isFirst = Trim(srcF_PJ10MID.GetText()) != '' && Trim(srcF_PJ10MID.GetText()) != '0';
            setTimeout(SetNew, 100);
            fn_OnSearchClick();

            //if (isFirst) {
            //    ViewSelectedItem(Trim(srcF_PJ10MID.GetText()));
            //}
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            fnASPxGridView_ReHeight(devGrid);
            fnASPxGridView_ReHeight(devGrid1);
        }

        // 조회
        function fn_OnSearchClick() {
            //fn_ObjInit();
            devGrid.PerformCallback('GET');
            //if (Trim(srcF_PJ10MID.GetText()) != '') ViewSelectedItem(srcF_PJ10MID.GetText());
        }

        // 입력
        function fn_OnNewClick() {
            SetNew();
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
            var md = GetPageMode();

            if (!fn_OnValidate()) {
                ucNavi.SetNoticeOnce('필요한 항목이 모두 입력되지 않았습니다.', 'red');
                return false;
            }

            if (!confirm('저장하시겠습니까?')) {
                return false;
            }

            var action = (md == 'NEW' ? 'SAVE' : 'UPDATE');

            var params = {
                'action': action,
                'F_MOLDNO': Trim(srcF_MOLDNO.GetText()),
                'F_MOLDNTH': Trim(srcF_MOLDNTH.GetText())
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(params)));
        }

        // 취소
        function fn_OnCancelClick() {
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (GetPageMode() != "EDIT" || !HasPkey()) {
                //ucNavi.SetNoticeOnce('필요한 항목이 모두 입력되지 않았습니다.', 'red');
                alert('삭제할 데이터가 없습니다.');
                return false;
            }

            if (!confirm('삭제하시겠습니까?')) {
                return false;
            }

            var params = {
                'action': 'DELETE'
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(params)));
        }

        // 인쇄
        function fn_OnPrintClick() {
        }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }
    </script>
    <script type="text/javascript">        // 사용자 정의 함수
        function SetNew(isFirst) {
            SetInit(isFirst);
            SetPageMode('NEW');
            fn_OnValidate();
        }
        // 키가 있는지 확인
        function HasPkey() {
            return Trim(srcF_MOLDNO.GetText()) != '';
        }
        // 초기화
        function SetInit(isFirst) {
            // 초기화
            SetClear(isFirst);
            SetDefault();
            //SetStatusBySubmitCD();
        }
        // 컨트롤값 클리어
        function SetClear(isFirst) {
            // 컨트롤값 제거
            //srcF_MOLDNO.SetValue('');
            srcF_MOLDNTH.SetValue(hidLastF_MOLDNTH.GetValue() + 1);
            srcF_MAKER.SetValue('');
            srcF_MAKEYMD.SetValue('');
            srcF_COST.SetValue('');
            //srcF_ITEMCD.SetValue('');
            srcF_CAVITY.SetValue(1);
            srcF_WEIGHT.SetValue('');
            srcF_STANDARD.SetValue(0);
            srcF_MIN.SetValue(0);
            srcF_MAX.SetValue(0);
            srcF_SLOPETIME_STD.SetValue(0);
            srcF_SLOPETIME_MIN.SetValue(0);
            srcF_SLOPETIME_MAX.SetValue(0);
            srcF_WAITTIME_STD.SetValue(0);
            srcF_WAITTIME_MIN.SetValue(0);
            srcF_WAITTIME_MAX.SetValue(0);
            srcF_LIMITCOUNT.SetValue(0);
            //srcF_COUNTER.SetValue('');
            srcF_MEMO.SetValue('');
            srcF_USEYN.SetValue('1');
        }
        // 기본값 설정
        function SetDefault() {
            //// 기본값 설정할 것만 지정하여 SetValue 호출
            //var data = {
            //    'F_COMPCD': _compCD, // '04',
            //    'F_FACTCD': _factCD, // '01',
            //    // 'F_CUSTID': '41940',
            //    'F_APPRDT1': fn_GetDateString(new Date()),
            //    'F_APPRDT2': fn_GetDateString(new Date()),
            //    'F_APPRDT3': fn_GetDateString(new Date())
            //};
            //srcF_COMPCD.SetText(_compCD);
            //srcF_FACTCD.SetText(_factCD);
            //SetValue(data);
        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        function SetPageMode(mode) {
            // 페이지 모드에 따라 설정
            switch (mode) {
                case "NEW":
                    SetPageEnable(Trim(srcF_ITEMCD.GetText()) != '');
                    //SetPageEnable(true);
                    break;
                case "EDIT":
                    SetPageEnable(true);
                    srcF_MOLDNO.SetEnabled(false);
                    break;
                case "READONLY":
                    SetPageEnable(false);
                    break;
                default:
                    alert("PageMode가 설정되지 않았습니다");
                    break;
            }
            //SetPageEnable(mode);
            hidPageMode.SetText(mode);
        }

        // 페이지 활성/비활성화
        function SetPageEnable(enable) {
            srcF_MOLDNO.SetEnabled(enable && Trim(srcF_MOLDNO.GetText())=='');
            //srcF_MOLDNTH.SetEnabled(enable);
            srcF_MAKER.SetEnabled(enable);
            srcF_MAKEYMD.SetEnabled(enable);
            srcF_COST.SetEnabled(enable);
            //srcF_ITEMCD.SetEnabled(enable);
            srcF_CAVITY.SetEnabled(enable);
            srcF_WEIGHT.SetEnabled(enable);
            srcF_STANDARD.SetEnabled(enable);
            srcF_MIN.SetEnabled(enable);
            srcF_MAX.SetEnabled(enable);
            srcF_SLOPETIME_STD.SetEnabled(enable);
            srcF_SLOPETIME_MIN.SetEnabled(enable);
            srcF_SLOPETIME_MAX.SetEnabled(enable);
            srcF_WAITTIME_STD.SetEnabled(enable);
            srcF_WAITTIME_MIN.SetEnabled(enable);
            srcF_WAITTIME_MAX.SetEnabled(enable);
            srcF_LIMITCOUNT.SetEnabled(enable);
            //srcF_COUNTER.SetEnabled(enable);
            srcF_MEMO.SetEnabled(enable);
            srcF_USEYN.SetEnabled(enable);

        }

        // 페이지모드 조회
        function GetPageMode() {
            return hidPageMode.GetText();
        }

        // 선택항목 조회(좌측 키로 우측 조회)
        function ViewSelectedItem(pkey) {
            // 콜백으로 우측영역값 json으로 조회
            // 우측영역 세팅
            pkey = pkey.split('|');
            var param = {
                'action': 'VIEW',
                'F_COMPCD': pkey[0],
                'F_FACTCD': pkey[1],
                'F_MOLDNO': pkey[2],
                'F_MOLDNTH': pkey[3]
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 선택항목 조회(좌측 키로 우측 목록 조회)
        function ViewList(pkey) {
            // 콜백으로 우측목록 grid 조회
            pkey = pkey.split('|');
            srcF_ITEMCD.SetText(pkey[0]);
            if (pkey[1] == '') {
                srcF_MOLDNO.SetText(pkey[0]);
            }
            else
            {
                srcF_MOLDNO.SetText(pkey[1]);
            }
            devGrid1.PerformCallback('GET');
            SetNew();
        }

        // 컨트롤 값 설정
        function SetValue(data) {
            if (typeof data.F_MOLDNO != 'undefined') srcF_MOLDNO.SetValue(data.F_MOLDNO);
            if (typeof data.F_MOLDNTH != 'undefined') {
                srcF_MOLDNTH.SetValue(data.F_MOLDNTH);
                var F_MOLDNTH = parseInt(data.F_MOLDNTH || '0', 10);
                hidLastF_MOLDNTH.SetValue(Math.max(F_MOLDNTH, hidLastF_MOLDNTH.GetValue()));
            }
            if (typeof data.F_MAKER != 'undefined') srcF_MAKER.SetValue(data.F_MAKER);
            if (typeof data.F_MAKEYMD != 'undefined') srcF_MAKEYMD.SetValue(data.F_MAKEYMD);
            if (typeof data.F_COST != 'undefined') srcF_COST.SetValue(data.F_COST);
            //if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            if (typeof data.F_CAVITY != 'undefined') srcF_CAVITY.SetValue(data.F_CAVITY);
            if (typeof data.F_WEIGHT != 'undefined') srcF_WEIGHT.SetValue(data.F_WEIGHT);
            if (typeof data.F_STANDARD != 'undefined') srcF_STANDARD.SetValue(data.F_STANDARD);
            if (typeof data.F_MIN != 'undefined') srcF_MIN.SetValue(data.F_MIN);
            if (typeof data.F_MAX != 'undefined') srcF_MAX.SetValue(data.F_MAX);
            if (typeof data.F_SLOPETIME_STD != 'undefined') srcF_SLOPETIME_STD.SetValue(data.F_SLOPETIME_STD);
            if (typeof data.F_SLOPETIME_MIN != 'undefined') srcF_SLOPETIME_MIN.SetValue(data.F_SLOPETIME_MIN);
            if (typeof data.F_SLOPETIME_MAX != 'undefined') srcF_SLOPETIME_MAX.SetValue(data.F_SLOPETIME_MAX);
            if (typeof data.F_WAITTIME_STD != 'undefined') srcF_WAITTIME_STD.SetValue(data.F_WAITTIME_STD);
            if (typeof data.F_WAITTIME_MIN != 'undefined') srcF_WAITTIME_MIN.SetValue(data.F_WAITTIME_MIN);
            if (typeof data.F_WAITTIME_MAX != 'undefined') srcF_WAITTIME_MAX.SetValue(data.F_WAITTIME_MAX);
            if (typeof data.F_LIMITCOUNT != 'undefined') srcF_LIMITCOUNT.SetValue(data.F_LIMITCOUNT);
            //if (typeof data.F_COUNTER != 'undefined') srcF_COUNTER.SetValue(data.F_COUNTER);
            if (typeof data.F_MEMO != 'undefined') srcF_MEMO.SetValue(data.F_MEMO);
            if (typeof data.F_USEYN != 'undefined') srcF_USEYN.SetValue(data.F_USEYN);
        }

        function SetButtonEnable(enable) {
            //var v = enable ? 'inline-block' : 'none';
            //$('#btnSubmit').css('display', v);
            //$('#btnSubmitCancel').css('display', v);
        }
    </script>
    <script type="text/javascript">
        // 이벤트 핸들러
        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            // 리턴값 처리
            var result = '';
            var data = '';
            if (Trim(s.cpResult) != '') {
                try {
                    result = JSON.parse(s.cpResult);
                    if (result.error != '') {
                        alert(result.error);
                    } else {
                        if (result.msg != '') {
                            ucNavi.SetNoticeOnce(result.msg);
                        }
                        if (result.data != '') {
                            data = result.data;
                        }
                    }
                } catch (err) {
                    alert(err);
                }
            }

            // 그리드별 처리
            switch (s) {
                //case devGrid:
                //    break;
                case devGrid1:
                    var F_MOLDNTH = typeof (s.cpF_MOLDNTH) != 'undefined' ? parseInt(s.cpF_MOLDNTH || '0', 10) : 0;
                    hidLastF_MOLDNTH.SetValue(F_MOLDNTH);
                    if (GetPageMode() == 'NEW') {
                        srcF_MOLDNTH.SetValue(F_MOLDNTH + 1);
                    }
                    break;
            }
        }

        // ASPxCallback 사용 : 우측항목 조회, 저장, 삭제
        function devCallback_CallbackComplete(s, e) {
            try {
                if (e.result != '') {
                    var result = JSON.parse(e.result);
                    if (typeof result.msg != 'undefined' && Trim(result.msg) != '') ucNavi.SetNoticeOnce(result.msg);
                    if (typeof result.error != 'undefined' && Trim(result.error) != '') {
                        if (result.error.indexOf('미등록 품번') == 0) {
                            srcF_ITEMCD.SetText('');
                            srcF_ITEMCD.Validate();
                        }
                        alert(result.error);
                        return false;
                    }
                    switch (result.action) {
                        case "VIEW":
                            if (typeof result.data == 'string') {
                                result.data = JSON.parse(decodeURIComponent(result.data||'{}'));
                            }
                            SetValue(result.data);
                            SetPageMode('EDIT');
                            fn_OnValidate();
                            break;
                        case "SAVE":
                            result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            SetValue(result.data);
                            SetPageMode('EDIT');
                            devGrid.PerformCallback('GET');
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('저장되었습니다.');
                            break;
                        case "UPDATE":
                            result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            SetValue(result.data);
                            SetPageMode('EDIT');
                            devGrid.PerformCallback('GET');
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('저장되었습니다.');
                            break;
                        case "DELETE":
                            //SetInit();
                            SetNew();
                            devGrid.PerformCallback('GET');
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('삭제되었습니다.');
                            break;
                        default:
                            break;
                    }
                }
            } catch (err) {
                alert(err);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            var result = true;
            result = ASPxClientEdit.ValidateEditorsInContainerById('divRight');
            //ASPxClientComboBox.Cast('').SetIsValid(false);
            return result;
        }

        function devGrid_RowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            ViewList(key);
        }

        function devGrid1_RowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            ViewSelectedItem(key);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divLeft" style="width:29%;float:left;">
            <div class="blockTitle">
                <span>[금형등록 대상품번 목록]</span>
            </div>
            <div id="divLeftTop" style="width:100%;">
                <table class="InputTable" style="margin-bottom: 5px;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 23%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">품번</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox runat="server" ID="schF_ITEMCD" ClientInstanceName="schF_ITEMCD" ClientEnabled="true" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">금형번호</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox runat="server" ID="schF_MOLDNO" ClientInstanceName="schF_MOLDNO" ClientEnabled="true" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLeftBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_MOLDNO" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback"
                    OnDataBinding="devGrid_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />
                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError"
                        RowDblClick="devGrid_RowDblClick" />
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount"  runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="80px"/>
                        <%--<dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />--%>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="120px"/>
                        <dx:GridViewDataColumn FieldName="F_MOLDNO" Caption="금형번호" Width="120px"/>
                        <dx:GridViewDataColumn FieldName="F_MOLDNTH" Caption="금형차수" Width="120px"/>
                        <%--<dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" />--%>
                        <%--<dx:GridViewDataColumn FieldName="F_REMAINS" Caption="현재고" Width="80px"/>--%>
                        <%--<dx:GridViewDataColumn FieldName="F_NEEDCOUNT" Caption="적정재고" Width="80px"/>--%>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
        <div id="divSpace" style="width:1%;float:left;">&nbsp;</div>
        <div id="divRight" style="width:70%;float:left;">
            <div class="blockTitle">
                <span>[금형 정보]</span>
            </div>
            <div id="divRightItem" style="margin-bottom:10px;">
                <table class="InputTable" style="margin-bottom: 5px;width:33%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">품번</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divRightTop" style="margin-bottom:10px;">
                <table class="InputTable" style="margin-bottom: 5px;width:100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 22%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">금형번호</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MOLDNO" ClientInstanceName="srcF_MOLDNO" ClientEnabled="false" Width="100%" > <%--MaxLength="5"--%>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true">
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">금형차수</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_MOLDNTH" ClientInstanceName="srcF_MOLDNTH" ClientEnabled="false" Width="100%" MaxLength="3">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_MOLDNTH" ClientInstanceName="srcF_MOLDNTH" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="1" MaxValue="100" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">Cavity</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_CAVITY" ClientInstanceName="srcF_CAVITY" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_CAVITY" ClientInstanceName="srcF_CAVITY" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="1" MaxValue="100" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">제작업체</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MAKER" ClientInstanceName="srcF_MAKER" ClientEnabled="false" Width="100%" MaxLength="50">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">제작일자</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MAKEYMD" ClientInstanceName="srcF_MAKEYMD" ClientEnabled="false" Width="100%" MaxLength="10">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">제작비용</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_COST" ClientInstanceName="srcF_COST" ClientEnabled="false" Width="100%" MaxLength="20">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <%--<td class="tdTitle">품번</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" ClientEnabled="false" Width="100%" MaxLength="40">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="tdTitle">금형수명</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_LIMITCOUNT" ClientInstanceName="srcF_LIMITCOUNT" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_LIMITCOUNT" ClientInstanceName="srcF_LIMITCOUNT" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="1" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">원재료무게</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_WEIGHT" ClientInstanceName="srcF_WEIGHT" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RegularExpression ValidationExpression="[0-9]*" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">사용여부</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxRadioButtonList runat="server" ID="srcF_USEYN" ClientInstanceName="srcF_USEYN" ClientEnabled="false" Width="100%" RepeatDirection="Horizontal">
                                    <Paddings Padding="0" />
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                    <Border BorderStyle="None" />
                                    <Items>
                                        <dx:ListEditItem Value="1" Text="사용" Selected="true" />
                                        <dx:ListEditItem Value="0" Text="사용안함" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">금형온도 Std.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_STANDARD" ClientInstanceName="srcF_STANDARD" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_STANDARD" ClientInstanceName="srcF_STANDARD" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">금형온도 Min.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_MIN" ClientInstanceName="srcF_MIN" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_MIN" ClientInstanceName="srcF_MIN" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">금형온도 Max.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_MAX" ClientInstanceName="srcF_MAX" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_MAX" ClientInstanceName="srcF_MAX" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">틸팅시간 Std.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_SLOPETIME_STD" ClientInstanceName="srcF_SLOPETIME_STD" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_SLOPETIME_STD" ClientInstanceName="srcF_SLOPETIME_STD" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">틸팅시간 Min.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_SLOPETIME_MIN" ClientInstanceName="srcF_SLOPETIME_MIN" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_SLOPETIME_MIN" ClientInstanceName="srcF_SLOPETIME_MIN" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">틸팅시간 Max.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_SLOPETIME_MAX" ClientInstanceName="srcF_SLOPETIME_MAX" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_SLOPETIME_MAX" ClientInstanceName="srcF_SLOPETIME_MAX" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">응고시간 Std.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_WAITTIME_STD" ClientInstanceName="srcF_WAITTIME_STD" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_WAITTIME_STD" ClientInstanceName="srcF_WAITTIME_STD" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">응고시간 Min.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_WAITTIME_MIN" ClientInstanceName="srcF_WAITTIME_MIN" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_WAITTIME_MIN" ClientInstanceName="srcF_WAITTIME_MIN" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">응고시간 Max.</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_WAITTIME_MAX" ClientInstanceName="srcF_WAITTIME_MAX" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                        <RequiredField IsRequired="true" ErrorText="필수항목입니다." />
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_WAITTIME_MAX" ClientInstanceName="srcF_WAITTIME_MAX" ClientEnabled="false" Width="100%" NumberType="Float" Number="1" MinValue="0" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle" >비고</td>
                        <td class="tdContent" colspan="5">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MEMO" ClientInstanceName="srcF_MEMO" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="blockTitle">
                <span>[금형 목록]</span>
            </div>
            <div id="divRightBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_COMPCD;F_FACTCD;F_MOLDNO;F_MOLDNTH" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid1_CustomCallback"
                    OnDataBinding="devGrid1_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Visible" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />
                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError"
                        RowDblClick="devGrid1_RowDblClick"/>
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount"  runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="60px" />
                        <dx:GridViewDataTextColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataTextColumn FieldName="F_FACTCD" Visible="false" />
                        <dx:GridViewDataTextColumn FieldName="F_MOLDNO" Caption="금형번호" Width="80px"/>
                        <dx:GridViewDataTextColumn FieldName="F_MOLDNTH" Caption="금형차수" Width="80px"/>
                        <dx:GridViewDataTextColumn FieldName="F_MAKER" Caption="제작업체" Width="90px"/>
                        <dx:GridViewDataTextColumn FieldName="F_MAKEYMD" Caption="제작일자" Width="80px"/>
                        <dx:GridViewDataTextColumn FieldName="F_COST" Caption="제작비용" Width="80px"/>
                        <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품번" Width="120px"/>
                        <dx:GridViewDataTextColumn FieldName="F_CAVITY" Caption="Cavity" Width="80px"/>
                        <dx:GridViewDataTextColumn FieldName="F_LIMITCOUNT" Caption="금형수명" Width="80px"/>
                        <dx:GridViewBandColumn Caption="금형온도">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="Std." Width="70px"/>
                                <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="Min." Width="70px"/>
                                <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="Max." Width="70px"/>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="틸팅시간">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_SLOPETIME_STD" Caption="Std." Width="70px"/>
                                <dx:GridViewDataTextColumn FieldName="F_SLOPETIME_MIN" Caption="Min." Width="70px"/>
                                <dx:GridViewDataTextColumn FieldName="F_SLOPETIME_MAX" Caption="Max." Width="70px"/>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="응고시간">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_WAITTIME_STD" Caption="Std." Width="70px"/>
                                <dx:GridViewDataTextColumn FieldName="F_WAITTIME_MIN" Caption="Min." Width="70px"/>
                                <dx:GridViewDataTextColumn FieldName="F_WAITTIME_MAX" Caption="Max." Width="70px"/>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataTextColumn FieldName="F_WEIGHT" Caption="원재료무게" Width="80px"/>
                        <dx:GridViewDataTextColumn FieldName="F_MEMO" Caption="비고" CellStyle-HorizontalAlign="Left" Width="200px" />
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid1" OnRenderBrick="devGrid1Exporter_RenderBrick"></dx:ASPxGridViewExporter>
            </div>
        </div>
        <div style="clear:both;"></div>
    </div>
    <div id="divHidden" style="display:none;">
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
        <dx:ASPxTextBox runat="server" ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <%--<dx:ASPxTextBox runat="server" ID="srcF_STARTYN" ClientInstanceName="srcF_STARTYN" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcApprF_PJ11D1ID" ClientInstanceName="srcApprF_PJ11D1ID" ClientVisible="false">
        </dx:ASPxTextBox>--%>
        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxSpinEdit runat="server" ID="hidLastF_MOLDNTH" ClientInstanceName="hidLastF_MOLDNTH" ClientVisible="false" NumberType="Integer" Number="0">
        </dx:ASPxSpinEdit>
        <%--<dx:ASPxTextBox runat="server" ID="hidGridData" ClientInstanceName="hidGridData" ClientVisible="false">
        </dx:ASPxTextBox>--%>
        <%--<dx:ASPxButton ID="btnResourceTemp" runat="server" Image-IconID="save_save_32x32" ClientVisible="false" ></dx:ASPxButton>--%>
        <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>
    </div>
</asp:Content>