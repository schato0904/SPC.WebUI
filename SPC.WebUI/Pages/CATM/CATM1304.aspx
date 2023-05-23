<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM1304.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM1304" %>
<%-- 보온로 투입이력관리 --%>
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
                'action': action
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
            if (devGrid1.GetVisibleRowsOnPage() == 0) {
                ucNavi.SetNoticeOnce('조회된 내용이 없습니다.', 'red');
                return false;
            }
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
            return Trim(srcF_SEQ.GetText()) != '' && Trim(srcF_SEQ.GetText()) != '0';
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
            //if (!isFirst) {
            //    srcF_MACHCD.SetValue('');
            //    srcF_MACHNM.SetValue('');
            //}
            //var today = new Date().toISOString().slice(0, 10);
            srcF_SEQ.SetValue('0');
            //srcF_MELTCD.SetValue('');
            srcF_INDATE.SetValue(new Date());
            srcF_ITEMCD.SetValue(null);
            srcF_INAMT.SetValue('');
            srcF_INNGAMT.SetValue('');
            srcF_MEMO.SetValue('');
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
                    SetPageEnable(Trim(srcF_MELTCD.GetText()) != '');
                    //SetPageEnable(true);
                    break;
                case "EDIT":
                    SetPageEnable(true);
                    //srcF_CODE.SetEnabled(false);
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
            //srcF_MELTCD.SetEnabled(enable);
            srcF_INDATE.SetEnabled(enable);
            srcF_ITEMCD.SetEnabled(enable);
            srcF_INAMT.SetEnabled(enable);
            srcF_INNGAMT.SetEnabled(enable);
            srcF_MEMO.SetEnabled(enable);
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
                'F_SEQ': pkey[2]
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 선택항목 조회(좌측 키로 우측 목록 조회)
        function ViewList(pkey) {
            // 콜백으로 우측목록 grid 조회
            pkey = pkey.split('|');
            srcF_MELTCD.SetText(pkey[0]);
            srcF_MELTNM.SetText(pkey[1]);
            devGrid1.PerformCallback('GET');
            SetNew();
        }
        // 컨트롤 값 설정
        function SetValue(data) {
            if (typeof data.F_SEQ != 'undefined') srcF_SEQ.SetValue(data.F_SEQ);
            if (typeof data.F_MELTCD != 'undefined') srcF_MELTCD.SetValue(data.F_MELTCD);
            if (typeof data.F_MELTNM != 'undefined') srcF_MELTNM.SetValue(data.F_MELTNM);
            if (typeof data.F_INDATE != 'undefined') srcF_INDATE.SetValue(str2date(data.F_INDATE));
            if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            if (typeof data.F_INAMT != 'undefined') srcF_INAMT.SetValue(data.F_INAMT);
            if (typeof data.F_INNGAMT != 'undefined') srcF_INNGAMT.SetValue(data.F_INNGAMT);
            if (typeof data.F_MEMO != 'undefined') srcF_MEMO.SetValue(data.F_MEMO);
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
                case devGrid:
                    break;
                case devGrid1:
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
                            devGrid1.PerformCallback('GET');
                            ucNavi.SetNoticeOnce('저장되었습니다.');
                            break;
                        case "DELETE":
                            //SetInit();
                            SetNew();
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

        //function str2date(str) {
        //    var pat = /^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}(:\d{2})?$/;
        //    if (!pat.test(str)) {
        //        alert('날짜 형식이 올바르지 않습니다.');
        //        return false;
        //    }
        //    var d = str.split(/[\s-:]/gi);
        //    var nd = new Date(d[0] * 1, d[1] * 1 - 1, d[2] * 1, (d[3] || 0) * 1, (d[4] || 0) * 1, (d[5] || 0) * 1);
        //    return nd;
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divLeft" style="width:29%;float:left;">
            <div class="blockTitle">
                <span>[이력대상 설비목록]</span>
            </div>
            <div id="divLeftTop" style="width:100%;">
                <%--<table class="InputTable" style="margin-bottom: 5px;">
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
                </table>--%>
            </div>
            <div id="divLeftBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MACHCD;F_MACHNM" EnableViewState="false" EnableRowsCache="false"
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
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="40px"/>
                        <%--<dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />--%>
                        <dx:GridViewDataColumn FieldName="F_MACHCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="보온로" Width="150px"/>
                        <dx:GridViewDataColumn FieldName="F_INDATE" Caption="최종투입이력" Width="180px"/>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
        <div id="divSpace" style="width:1%;float:left;">&nbsp;</div>
        <div id="divRight" style="width:70%;float:left;">
            <div class="blockTitle">
                <span>[보온로 투입이력 정보]</span>
            </div>
            <div id="divRightTop" style="margin-bottom:10px;">
                <table class="InputTable" style="margin-bottom: 5px;width:33%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">보온로</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MELTNM" ClientInstanceName="srcF_MELTNM" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                                <dx:ASPxTextBox runat="server" ID="srcF_MELTCD" ClientInstanceName="srcF_MELTCD" ClientEnabled="false" Width="100%" ClientVisible="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divMiddle" style="margin-bottom:10px;">
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
                        <td class="tdTitle">투입일시</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_INDATE" ClientInstanceName="srcF_INDATE" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd HH:mm" EditFormatString="yyyy-MM-dd HH:mm" >
                                </dx:ASPxDateEdit>
                                <dx:ASPxTextBox runat="server" ID="srcF_SEQ" ClientInstanceName="srcF_SEQ" ClientEnabled="false" Width="100%" ClientVisible="false" Text="0">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">투입원재료</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" IncrementalFilteringMode="None" ValueType="System.String" >
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">원재료투입량</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_INAMT" ClientInstanceName="srcF_INAMT" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수 항목입니다.">
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_INAMT" ClientInstanceName="srcF_INAMT" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="1" MaxValue="10000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" DisplayFormatString="{0:#,0}">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">불량품투입량</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <%--<dx:ASPxTextBox runat="server" ID="srcF_INNGAMT" ClientInstanceName="srcF_INNGAMT" ClientEnabled="false" Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" ValidateOnLeave="true" RequiredField-ErrorText="필수 항목입니다.">
                                        <RegularExpression ValidationExpression="[0-9]+" ErrorText="숫자만 가능합니다." />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit runat="server" ID="srcF_INNGAMT" ClientInstanceName="srcF_INNGAMT" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="1" MaxValue="10000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" DisplayFormatString="{0:#,0}">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>
                        <td class="tdTitle">비고</td>
                        <td class="tdContent" colspan="3">
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
                <span>[보온로투입이력 목록]</span>
            </div>
            <div id="divRightBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_COMPCD;F_FACTCD;F_SEQ" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid1_CustomCallback"
                    OnDataBinding="devGrid1_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />
                    <%--<SettingsEditing Mode="Batch">
                        <BatchEditSettings AllowValidationOnEndEdit="true" EditMode="Row" StartEditAction="DblClick" />
                    </SettingsEditing>--%>
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
                        <dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_SEQ" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_MELTNM" Caption="보온로" Width="120px"/>
                        <dx:GridViewDataColumn FieldName="F_INDATE" Caption="투입일시" Width="200px"/>
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="투입원재료" Width="100px"/>
                        <dx:GridViewDataSpinEditColumn FieldName="F_INAMT" Caption="원재료투입량" Width="100px" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>
                        <dx:GridViewDataSpinEditColumn FieldName="F_INNGAMT" Caption="불량품투입량" Width="100px" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>
                        <dx:GridViewDataColumn FieldName="F_RATIO" Caption="불량품<br />투입비율(%)" Width="100px"/>
                        <dx:GridViewDataColumn FieldName="F_MEMO" Caption="비고" CellStyle-HorizontalAlign="Left" />
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
        <%--<dx:ASPxTextBox runat="server" ID="hidGridData" ClientInstanceName="hidGridData" ClientVisible="false">
        </dx:ASPxTextBox>--%>
        <%--<dx:ASPxButton ID="btnResourceTemp" runat="server" Image-IconID="save_save_32x32" ClientVisible="false" ></dx:ASPxButton>--%>
        <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>
    </div>
</asp:Content>