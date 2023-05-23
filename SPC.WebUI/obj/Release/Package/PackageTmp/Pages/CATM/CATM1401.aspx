<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM1401.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM1401" %><%-- 일별재고현황 조회 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {
            setTimeout(SetNew, 100);
            fn_OnSearchClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            //fnASPxGridView_ReHeight(devGrid);
            fnASPxGridView_ReHeight(devGrid1);
        }

        // 조회
        function fn_OnSearchClick() {
            //devGrid.PerformCallback('GET');
            devGrid1.PerformCallback('GET');
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
        }

        // 취소
        function fn_OnCancelClick() {
            schF_ITEMCD.SetText('');
            fn_OnSearchClick();
        }

        // 삭제
        function fn_OnDeleteClick() {
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
            //return Trim(srcF_WORKNO.GetText()) != '' ;
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
            schF_STDYMD.SetValue(new Date());
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
            //schF_STDYMD.SetValue(new Date());
        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        function SetPageMode(mode) {
            // 페이지 모드에 따라 설정
            switch (mode) {
                case "NEW":
                    //SetPageEnable(Trim(srcF_ITEMCD.GetText()) != '');
                    //SetPageEnable(true);
                    break;
                case "EDIT":
                    //SetPageEnable(true);
                    //srcF_CODE.SetEnabled(false);
                    break;
                case "READONLY":
                    //SetPageEnable(false);
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
            //srcF_WORKNO_SEQ.SetEnabled(enable);
            //srcF_MACHCD.SetEnabled(enable);
            //srcF_MELTCD.SetEnabled(enable);
            //srcF_LOTNO.SetEnabled(enable);
            //srcF_PLANCOUNT.SetEnabled(enable);
            //srcF_PLANYMD.SetEnabled(enable);
            //srcF_MEMO.SetEnabled(enable);
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
                'F_WORKNO': pkey[2]
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 컨트롤 값 설정
        function SetValue(data) {
            //if (typeof data.F_WORKNO != 'undefined') srcF_WORKNO.SetValue(data.F_WORKNO);
            //if (typeof data.F_WORKNO_SEQ != 'undefined') srcF_WORKNO_SEQ.SetValue(data.F_WORKNO_SEQ);
            //if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            //if (typeof data.F_MOLDNO != 'undefined') srcF_MOLDNO.SetValue(data.F_MOLDNO);
            //if (typeof data.F_MOLDNTH != 'undefined') srcF_MOLDNTH.SetValue(data.F_MOLDNTH);
            //if (typeof data.F_CAVITY != 'undefined') srcF_CAVITY.SetValue(data.F_CAVITY);
            //if (typeof data.F_MACHCD != 'undefined') srcF_MACHCD.SetValue(data.F_MACHCD);
            //if (typeof data.F_MELTCD != 'undefined') srcF_MELTCD.SetValue(data.F_MELTCD);
            //if (typeof data.F_LOTNO != 'undefined') srcF_LOTNO.SetValue(data.F_LOTNO);
            //if (typeof data.F_PLANCOUNT != 'undefined') srcF_PLANCOUNT.SetValue(data.F_PLANCOUNT);
            //if (typeof data.F_PLANYMD != 'undefined') {
            //    srcF_PLANYMD.SetValue(convertDateString(data.F_PLANYMD));
            //    schF_PLANYMD.SetValue(convertDateString(data.F_PLANYMD));
            //}
            //if (typeof data.F_MEMO != 'undefined') srcF_MEMO.SetValue(data.F_MEMO);
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
                                result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            }
                            SetValue(result.data);
                            SetPageMode('EDIT');
                            fn_OnValidate();
                            break;
                        case "SAVE":
                            result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            SetValue(result.data);
                            SetPageMode('EDIT');
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
                            ucNavi.SetNoticeOnce('작업지시가 취소되었습니다.');
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
            //var key = s.GetRowKey(e.visibleIndex);
            //ViewList(key);
        }

        function devGrid1_RowDblClick(s, e) {
            //var key = s.GetRowKey(e.visibleIndex);
            //ViewSelectedItem(key);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divRight" style="width:100%;">
            <div id="divMiddle" style="float:left;margin-bottom:2px;width:100%;">
                <table class="InputTable" style="width:50%;">
                    <colgroup>
                        <col style="width: 11%" />
                        <col style="width: 22%" />
                        <col style="width: 11%" />
                        <col style="width: 22%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">기준일자</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_STDYMD" ClientInstanceName="schF_STDYMD" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" >
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="clear:both;"></div>
                        </td>
                        <td class="tdTitle">품번/명</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxTextBox runat="server" ID="schF_ITEMCD" ClientInstanceName="schF_ITEMCD" ClientEnabled="true" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="clear:both;"></div>
            <div id="divRightBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD" EnableViewState="false" EnableRowsCache="false"
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
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="50px" />
                        <%--<dx:GridViewDataColumn FieldName="F_COMPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FACTCD" Visible="false" />--%>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="140px"/>
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="140px"/>
                        <dx:GridViewDataSpinEditColumn FieldName="F_PASTPLANCNT" Caption="전일목표량" Width="120px" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>
                        <dx:GridViewDataSpinEditColumn FieldName="F_PASTPRODCNT" Caption="전일생산량" Width="120px" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>
                        <dx:GridViewDataSpinEditColumn FieldName="F_PLANCNT" Caption="당일목표량" Width="120px" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>
                        <dx:GridViewDataSpinEditColumn FieldName="F_PRODCNT" Caption="당일생산량" Width="120px" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>
                        
                        
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
        <%--<dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>--%>
    </div>
</asp:Content>