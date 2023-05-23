<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM3131.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM3131" %>
<%-- 출하지시서(선입선출) 등록 --%>
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
           
        }

        // 인쇄
        function fn_OnPrintClick() {
            //var F_OUTORDERNO = Trim(srcF_OUTORDERNO.GetText());
            //var TITLE = '부품식별표';
            //if (F_OUTORDERNO == '') {
            //    ucNavi.SetNoticeOnce('인쇄할 내용이 없습니다.', 'red');
            //    return false;
            //}

            //fn_OnPopRptCATM3121(TITLE, F_OUTORDERNO, '');
        }

        // 부품식별표 팝업창 열기
        function fn_OnPopRptCATM3121(TITLE, F_OUTORDERNO, parentCallbackName) {
            var PopParam = {
                'F_OUTORDERNO': F_OUTORDERNO
            };
            pPage = './Popup/PopRptCATM3101.aspx' +
                '?TITLE=' + encodeURIComponent(TITLE) +
                '&parentCallback=' + parentCallbackName +
                '&PopParam=' + encodeURIComponent(JSON.stringify(PopParam));
            //pPage = rootURL + 'Pages/Common/Popup/INSPREPORTPOP.aspx?TITLE=test&KEYFIELDS=14|LB%20RA506101|41940|04|1';
            fn_OnPopupOpen(pPage, '1000', '0');
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
       
        // 초기화
        function SetInit(isFirst) {
            // 초기화
            SetClear(isFirst);
            SetDefault();
            //SetStatusBySubmitCD();
        }
        // 컨트롤값 클리어
        function SetClear(isFirst) {
            
            
            
            
         
            
            
        }
        // 기본값 설정
        function SetDefault() {
        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        function SetPageMode(mode) {
            // 페이지 모드에 따라 설정
            switch (mode) {
                case "NEW":
                    
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
                //'F_COMPCD': pkey[0],
                //'F_FACTCD': pkey[1],
                'F_OUTORDERNO': pkey[0]
            };
            devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 선택항목 조회(좌측 키로 우측 목록 조회)
        function ViewList(pkey) {
            // 콜백으로 우측목록 grid 조회
            pkey = pkey.split('|');
                        
            SetNew();
        }
        // 컨트롤 값 설정
        function SetValue(data) {
            if (typeof data.F_OUTORDERNO != 'undefined') srcF_OUTORDERNO.SetValue(data.F_OUTORDERNO);
            
            
            
            
            if (typeof data.F_ITEMNM != 'undefined') srcF_ITEMNM.SetValue(data.F_ITEMNM);
            
            
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

        

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divLeft" style="width:50%;float:left;">
            <div class="blockTitle">
                <span>[현 재고]</span>
            </div>
            <div id="divLeftTop" style="width:50%;">
                <table class="InputTable" style="margin-bottom: 5px;">
                    <colgroup>
                        <col style="width: 7%" />
                        <col style="width: 20%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">품번/명</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox runat="server" ID="schF_SEARCHTEXT" ClientInstanceName="schF_SEARCHTEXT" ClientEnabled="true" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLeftBody" style="width:100%;">
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
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
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="10%"/>                        
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="28%" CellStyle-HorizontalAlign="Left"/>
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" Width="28%" CellStyle-HorizontalAlign="Left"/>
                        <dx:GridViewDataColumn FieldName="F_REMAINS" Caption="현재고" Width="12%" CellStyle-HorizontalAlign="Right"/>
                        <dx:GridViewDataColumn FieldName="F_PLANYMD" Caption="최종 작업일" Width="22%"/>
                        
                    </Columns>
                </dx:ASPxGridView>
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