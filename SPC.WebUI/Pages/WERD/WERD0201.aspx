<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD0201.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD0201" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        //var _fieldList = "F_PR01MID;F_WORKDT;F_DOCNO;F_FACTCD;F_PROCCD;F_LOTNO;F_ITEMDIVNM;F_PRODCNT;F_MODELNM;F_INSPCNT;F_NGCNT;F_NGRATE;F_NGPPM;F_PRICE;F_ACTCD;F_INSPUSER;F_WORKDTCD;F_PROCNM;F_LINECD;F_UNITPRICE;F_LOTNGDESC;F_LOTNGCNT";
        var _fieldList = "F_DAYPRODUCTNO;F_WORKDATE;F_DAYPRODUCTNO;F_LOTNO;F_PRODUCTQTY;F_MODELNM;F_INSPQTY;F_NGQTY;F_NGRATE;F_NGPPM;F_LOSSAMT;F_PLANNO;F_GUBUN;F_JOBSTDT;F_JOBENDT";
        var key;
        var ucNgTypeTable1;
        var ucNgCauseTable1;

        $(document).ready(function () {
            
            document.onkeypress = CancelEnterKey;
        });

        function CancelEnterKey() {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

        $(window).load(function () {
            ucNgTypeTable1 = window['<%= ucNgTypeTable1.UniqueID %>'];
            ucNgCauseTable1 = window['<%= ucNgCauseTable1.UniqueID %>'];
            ucNavi.SetNotice('<< 신규작성중 >>', 'navy', false);
            fn_OnNewClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(devGrid.GetMainElement()).offset().top;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
            //fn_OnValidate();
        }

        // 조회 클릭
        function fn_OnSearchClick() {
            //fn_ObjInit();
            devGrid.PerformCallback();
        }

        // 입력 클릭
        function fn_OnNewClick() {
            var ctrl = ucNgTypeTable1;
            ctrl.Clear();

            ctrl = ucNgCauseTable1;
            ctrl.Clear();

            fn_ObjInit();

            fn_SetPageMode("NEW");
        }

        // 화면 컨트롤 초기화 ( CHANGE_WORKDT - N:작업일 초기화 안함, 그외:작업일 초기화 )
        function fn_ObjInit(CHANGE_WORKDT) {
            var date = new Date();
            var exceptList = '';
            if (Trim(CHANGE_WORKDT) == 'N') exceptList = 'F_WORKDATE';
            fn_ClearFields(_fieldList, 'src', exceptList);

            fn_OnUCSettingItem1('', '', '', '','','');
            fn_OnSettingWork1_WERD('', '');
            fn_Itemcd1DisableBox(true);
            fn_WorkcdPop1DisableBox(true);
            fn_OnControlEnableComboBox(ASPxClientControl.Cast("srcF_GUBUN"), true);
            fn_OnControlEnableComboBox(ASPxClientControl.Cast("srcF_WORKDATE"), true);
            //fn_OnControlEnableComboBox(ASPxClientControl.Cast("srcF_PLANNO"), true);

            srcF_GUBUN.SetValue('0');

            if (Trim(CHANGE_WORKDT) != 'N') srcF_WORKDATE.SetDate(new Date());

            //srcF_ITEMCD.SetText('');
            //srcF_ITEMNM.SetText('');
            // 로그인 사용자 정보 조회
            fn_GetLoginUserInfo("USERID;USERNM", AfterCallback);
            function AfterCallback(returnJSON) {
                srcF_INSPUSER.fn_SetUCControl({ 'F_USERID': returnJSON.USERID, 'F_USERNM': returnJSON.USERNM });
            }
            //fn_OnValidate();
        }

        // 수정 클릭
        function fn_OnModifyClick() {
        }

        // 저장 클릭
        function fn_OnSaveClick() {
            // 유효성 체크 실패시 저장 안함
            //if (srcF_WORKDATE.GetValue() == "") {
            //    alert("작업일을 입력해 주세요.");
            //    return;
            //}

            if (!fn_OnValidate()) { return; }
            var typeSum = ucNgTypeTable1.GetSumCnt();
            var causeSum = ucNgCauseTable1.GetSumCnt();
            var productqty = srcF_PRODUCTQTY.GetText();
            var inspqty = srcF_INSPQTY.GetText()

            if (productqty < 1) {
                alert("생산수량를 입력해 주세요.");
                return;
            }

            // 부적합 수량 체크
            if (typeSum != causeSum) {
                alert("부적합유형 수량과 부적합원인 수량이 다릅니다.");
                return;
            }

            // 부적합 유형/원인 수량 파라미터로 전달하여 저장시 사용
            var typeVal = ucNgTypeTable1.GetValues();
            var causeVal = ucNgCauseTable1.GetValues();
            devCallback.PerformCallback('UPDATE;' + typeVal + ';' + causeVal);

        }

        // 취소
        function fn_OnCancelClick() {
            // 선택 해제 후 입력/변경 내용 취소
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제 클릭
        function fn_OnDeleteClick() {

            // 삭제전 의사 확인
            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            // 콜백에서 삭제 처리
            devCallback.PerformCallback('DEL');
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀 클릭
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        // CallbackPanel Callback
        function fn_OnEndCallback(s, e) {
            // 1. 콜백 후 결과값이 있을 경우 : 어떤 처리가 진행된 경우
            if (s.cpResultCode != '') {
                // 1.1 페이징 처리 요청시, 페이징 처리
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    // 1.2 페이징 처리가 아닌경우
                    // 1.2.1 메시지 출력 처리
                    alert(s.cpResultMsg);
                    s.cpResultMsg = "";
                    // 1.2.2 DML 처리가 정상일 경우, 
                    if (s.cpResultGbn == "1") {
                        // 1.2.2.1 삭제 처리가 정상일 경우
                        if (s.cpResultAction == "DELETE") {
                            // 1.2.2.2.1 입력부 초기화
                            fn_ObjInit('N');
                        }
                        // 1.2.2.2 입력모드 [신규]로 변경
                        fn_SetPageMode("NEW");
                        fn_ObjInit();
                        ucNgTypeTable1.Clear();
                        ucNgCauseTable1.Clear();
                        // 1.2.2.3 결과 파라미터 변수 클리어
                        s.cpResultCode = "";
                        s.cpResultGbn = "";
                        s.cpResultAction = "";
                        s.cpPkey = "";
                        // 1.2.2.4 하단 그리드 재조회
                        devGrid.PerformCallback();
                    }
                }
            }

            // 2. 결과값이 있고, 액션이 SELECT 인 경우 해당값 조회
            if (s.cpResult != "" && s.cpResultAction == "SELECT") {
                fn_SetCtl(JSON.parse(s.cpResult))
                s.cpResult = "";
                s.cpResultAction = "";
                //if (srcF_PR01MID.GetText() != '') {
                // 2.1 입력모드 [수정]으로 변경
                fn_SetPageMode("EDIT");
                //}
            }
        }

        // 페이지 모드 체인지 : NEW, EDIT, REV
        function fn_SetPageMode(mode) {
            switch (mode) {
                case "NEW":
                    ucNavi.SetNotice('<< 신규작성중 >>', 'navy', false);
                    break;
                case "EDIT":
                    ucNavi.SetNotice('<< 내용수정중 >>', 'green', false);
                    break;
                default:
                    return false;
            }

            hidPageMode.SetText(mode);
        }

        function fn_SetCtl(resultVal) {
            fn_ObjInit('N');
            var ctrl = ucNgTypeTable1;
            ctrl.LoadGrid(resultVal.F_WORKDATE, resultVal.F_GUBUN, resultVal.F_ITEMCD, resultVal.F_WORKCD, resultVal.F_DAYPRODUCTNO);

            ctrl = ucNgCauseTable1;
            ctrl.LoadGrid(resultVal.F_WORKDATE, resultVal.F_GUBUN, resultVal.F_ITEMCD, resultVal.F_WORKCD, resultVal.F_DAYPRODUCTNO);
            //fn_SetTextValue(srcF_WORKDATE, resultVal.F_WORKDATE);
            srcF_WORKDATE.SetDate(convertDateString(resultVal.F_WORKDATE));
            fn_Itemcd1DisableBox(false);
            fn_WorkcdPop1DisableBox(false);
            fn_OnControlEnableComboBox(ASPxClientControl.Cast("srcF_GUBUN"), false);
            fn_OnControlEnableComboBox(ASPxClientControl.Cast("srcF_WORKDATE"), false);
            //fn_OnControlEnableComboBox(ASPxClientControl.Cast("srcF_PLANNO"), false);
            fn_OnUCSettingItem1(resultVal.F_ITEMCD, resultVal.F_ITEMNM, resultVal.F_MODELCD, resultVal.F_MODELNM, resultVal.F_UNIT, resultVal.F_UNITNM);
            fn_OnSettingWork1_WERD(resultVal.F_WORKCD, resultVal.F_WORKNM);
            srcF_DAYPRODUCTNO.SetText(resultVal.F_DAYPRODUCTNO)
            srcF_LOTNO.SetText(resultVal.F_LOTNO);
            srcF_PRODUCTQTY.SetText(resultVal.F_PRODUCTQTY);
            //ASPxClientControl.Cast("hidUCMODELCD1").SetText(resultVal.F_MODELCD);
            //ASPxClientControl.Cast("txtUCMODELNM1").SetText(resultVal.F_MODELNM);
            srcF_INSPQTY.SetText(resultVal.F_INSPQTY);
            srcF_NGQTY.SetText(resultVal.F_NGQTY);
            srcF_NGRATE.SetText(resultVal.F_NGRATE);
            srcF_NGPPM.SetText(resultVal.F_NGPPM);
            srcF_LOSSAMT.SetText(resultVal.F_LOSSAMT);
            srcF_UNITPRICE.SetText(resultVal.F_UNITPRICE);
            srcF_PLANNO.SetText(resultVal.F_PLANNO);
            ASPxClientComboBox.Cast("srcF_GUBUN").SetValue(resultVal.F_GUBUN);
            window["srcF_INSPUSER"].SetCtl(resultVal.F_USERID)

            var aa = ASPxClientTimeEdit.Cast("");
            //aa.SetDate

            if (!resultVal.F_JOBSTDT == "") {
                var sdate = new Date(2017, 1, 1, resultVal.F_JOBSTDT.split(":")[0], resultVal.F_JOBSTDT.split(":")[1], resultVal.F_JOBSTDT.split(":")[2]);
                srcF_JOBSTDT.SetDate(sdate)
                srcF_JOBSTDT.SetText(resultVal.F_JOBSTDT);
            }

            if(!resultVal.F_JOBENDT == ""){
                var edate = new Date(2017, 1, 1, resultVal.F_JOBENDT.split(":")[0], resultVal.F_JOBENDT.split(":")[1], resultVal.F_JOBENDT.split(":")[2]);            
                srcF_JOBENDT.SetDate(edate);                        
                srcF_JOBENDT.SetText(resultVal.F_JOBENDT);
            }

            fn_OnValidate();
        }

        function fn_CallbackComplete(s, e) {
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            //alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            //return ASPxClientEdit.ValidateEditorsInContainerById('cbpContent');
            
            if (fn_ValidateITEM1()) {
                return true;
            }
            else if (fn_ValidateWORK1()) {
                return true;
            } else {
                return false;
            }
        }

        function devGrid_RowDblClick(s, e) {
            key = s.GetRowKey(e.visibleIndex)
            devCallback.PerformCallback('SELECT;' + key);
        }

        function fn_Popupitem() {
            var pPage = '<%=Page.ResolveUrl("~/Pages/PRIN/Popup/PRINITEMPOP.aspx")%>' +
                       '?TITLE=품목조회' +
                       '&CRUD=R' +
                       '&TYPE=' +
                       '&ITEMCD=' + srcF_ITEMCD.GetText() +
                       '&parentCallback=fn_SetItem';
            fn_OnPopupOpen(pPage, 950, 500);
        }

        function fn_SetItem(resultValue) {
            if (resultValue.F_ITEMCD != "undefined" && resultValue.F_ITEMCD != null && resultValue.F_ITEMCD != "") {
                var date = new Date();
                srcF_ITEMCD.SetText(resultValue.F_ITEMCD);
                srcF_ITEMNM.SetText(resultValue.F_ITEMNM);
                window["srcF_INSPUSER"].SetCtl('<%=gsUSERID%>')
                devCallback.PerformCallback('SELECT;' + resultValue.F_ITEMCD + ';;' + resultValue.F_PROCCD);
            }
        }

        function fn_PopupitemSEARCH() {
            var pPage = '<%=Page.ResolveUrl("~/Pages/PRIN/Popup/PRINITEMPOP.aspx")%>' +
                       '?TITLE=품목조회' +
                       '&CRUD=R' +
                       '&TYPE=' +
                       '&ITEMCD=' + srcF_ITEMCD.GetText() +
                       '&parentCallback=fn_SetItemSEARCH';
            fn_OnPopupOpen(pPage, 950, 500);
        }

        function fn_SetItemSEARCH(resultValue) {
            if (resultValue.F_ITEMCD != "undefined" && resultValue.F_ITEMCD != null && resultValue.F_ITEMCD != "") {
                var date = new Date();
                srcF_ITEMCD_SEARCH.SetText(resultValue.F_ITEMCD);
                srcF_ITEMNM_SEARCH.SetText(resultValue.F_ITEMNM);
                srcF_UNITNM.SetText(resultValue.F_UNITNM);

                ASPxClientComboBox.Cast("srcF_WORKCD_SEARCH").SetValue(resultValue.F_PROCCD)
            }
        }

        function fn_OnITEMLostFocus(s, e) {
            if (!s.GetText() || s.GetText() == '') {
                srcF_ITEMNM_SEARCH.SetText('');
                return;
            }
        };

        function fn_NgTypeChange() {
            var typeSum = ucNgTypeTable1.GetSumCnt();
            var procntText = srcF_PRODUCTQTY.GetText();
            var procntValue = srcF_PRODUCTQTY.GetValue();

            srcF_NGQTY.SetText(typeSum);
            srcF_INSPQTY.SetText(procntText);
            srcF_INSPQTY.SetValue(procntValue);

            if (!(typeSum == null || typeSum == "undefined" || typeSum == 0)) {
                srcF_NGPPM.SetText((typeSum * 1000000) / procntValue);
                srcF_NGRATE.SetText((typeSum * 100) / procntValue);
            }
        }

        function fn_srcF_PRODQTYValueChanged(s, e) {
            var typeSum = ucNgTypeTable1.GetSumCnt();
            var procntText = srcF_PRODUCTQTY.GetText();
            var procntValue = srcF_PRODUCTQTY.GetValue();

            srcF_NGQTY.SetText(typeSum);
            srcF_INSPQTY.SetText(procntText);
            srcF_INSPQTY.SetValue(procntValue);

            if (!(typeSum == null || typeSum == "undefined" || typeSum == 0)) {
                srcF_NGPPM.SetText((typeSum * 1000000) / procntValue);
                srcF_NGRATE.SetText((typeSum * 100) / procntValue);
            }
        }

        function fn_CallBack() {
            //window["srcF_INSPUSER"].SetCtl('<%=gsUSERID%>')
            //devCallback.PerformCallback('SELECT;' + '||||');
            ucNgTypeTable1.LoadGrid(fn_GetWorkCD(), '', fn_GetItemCD(), '', '');
            ucNgCauseTable1.LoadGrid('', '', '', '', '');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="divHidden" style="display:none;">        
        <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" Width="100%" ClientVisible="false" Text="NEW" />
    </div>
    <div class="container">
        <div class="search">
            <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" />
            </dx:ASPxCallback>
            <table>
                <colgroup>
                    <col style="width:64%" />
                    <col style="width:18%" />
                    <col style="width:18%" />
                </colgroup>
                <tr>
                    <td>
                        <table class="InputTable" style="margin-bottom: 5px;">
                            <colgroup>
                                <col style="width: 10%" />
                                <col style="width: 40%" />
                                <col style="width: 10%" />
                                <col style="width: 40%" />
                            </colgroup>
                            <tr>
                                <td class="tdTitle">일보 No.</td>
                                <td class="tdContentR">
                                    <dx:ASPxTextBox runat="server" ID="srcF_DAYPRODUCTNO" ClientInstanceName="srcF_DAYPRODUCTNO" Width="100%" >
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdContentR"></td>
                                <td class="tdContent"></td>
                            </tr>
                            <tr>
                                 <td class="tdTitle">
                                    <label>작업일</label>
                                </td>
                                <td class="tdContent">
                                    <dx:ASPxDateEdit runat="server" ID="srcF_WORKDATE" ClientInstanceName="srcF_WORKDATE" Width="100%" UseMaskBehavior="true">
                                        <ValidationSettings RequiredField-IsRequired="true"
                                            ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic">
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td class="tdTitle">시간</td>
                                <td class="tdContent">
                                    <div style="float:left">
                                        <dx:ASPxTimeEdit ID="srcF_JOBSTDT" ClientInstanceName="srcF_JOBSTDT" runat="server"  Width="100" EditFormat="Custom" EditFormatString="HH:mm:ss" DisplayFormatString="HH:mm:ss" >
                                        </dx:ASPxTimeEdit>
                                    </div>    
                                    <div>
                                        <dx:ASPxTimeEdit ID="srcF_JOBENDT" ClientInstanceName="srcF_JOBENDT" runat="server"  Width="100" EditFormat="Custom" EditFormatString="HH:mm:ss" DisplayFormatString="HH:mm:ss" >
                                        </dx:ASPxTimeEdit>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitle">품목</td>
                                <td class="tdContent">
                                    <ucCTF:Item1 ID="ucItem1" runat="server" usedModel="true" useWERD="true" usedUnit="true" />
                                </td>
                                <td class="tdTitle">공정명</td>
                                <td class="tdContent">
                                    <ucCTF:WorkPOP1 ID="ucWorkPOP1" runat="server" useWERD="true" validateFields="hidUCITEMCD1|품목" CallBackInsp="fn_CallBack()"  />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitle">단위</td>
                                <td class="tdContent">
                                    <dx:ASPxTextBox runat="server" ID="F_UNITCD" ClientInstanceName="F_UNITCD" Width="50" ClientVisible="false" />
                                    <dx:ASPxTextBox runat="server" ID="F_UNITNM" ClientInstanceName="F_UNITNM" Width="50" >
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdTitle">Lot No.</td>
                                <td class="tdContent">
                                    <dx:ASPxTextBox runat="server" ID="srcF_LOTNO" ClientInstanceName="srcF_LOTNO" Width="100%" >
                                        <%--<ValidationSettings RequiredField-IsRequired="true" 
                                            ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>--%>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitle">생산수</td>
                                <td class="tdContent">
                                    <dx:ASPxSpinEdit ID="srcF_PRODUCTQTY" ClientInstanceName="srcF_PRODUCTQTY" runat="server" Width="100%" class="form-control input-sm"
                                NumberType="Integer" MinValue="0" MaxValue="9999999" DisplayFormatString="#,#" >
                                        <ClientSideEvents ValueChanged="fn_srcF_PRODQTYValueChanged" />
                                    </dx:ASPxSpinEdit>
                                </td>
                                <td class="tdTitle">모델명</td>
                                <td class="tdContent">
                                    <ucCTF:Model1 ID="ucModel1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitle">검사수</td>
                                <td class="tdContent">
                                    <dx:ASPxSpinEdit ID="srcF_INSPQTY" ClientInstanceName="srcF_INSPQTY" runat="server" Width="100%" class="form-control input-sm"
                                NumberType="Integer" MinValue="0" MaxValue="9999999" DisplayFormatString="#,#" >
                                        
                                    </dx:ASPxSpinEdit>
                                </td>
                                <td class="tdTitle">부적합수</td>
                                <td class="tdContent">
                                     <dx:ASPxTextBox runat="server" ID="srcF_NGQTY" ClientInstanceName="srcF_NGQTY" Width="100%" >
                                        <ClientSideEvents Init="fn_OnControlDisableBox"  />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitle">부적합률(%)</td>
                                <td class="tdContent">
                                    <dx:ASPxTextBox runat="server" ID="srcF_NGRATE" ClientInstanceName="srcF_NGRATE" Width="100%" DisplayFormatString="n2" >
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdTitle">부적합률(PPM)</td>
                                <td class="tdContent">
                                    <dx:ASPxTextBox runat="server" ID="srcF_NGPPM" ClientInstanceName="srcF_NGPPM" Width="100%" DisplayFormatString="#,#" >
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitle">손실금액(원)</td>
                                <td class="tdContent">
                                    <dx:ASPxTextBox runat="server" ID="srcF_LOSSAMT" ClientInstanceName="srcF_LOSSAMT" Width="100%" DisplayFormatString="#,###">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox runat="server" ID="srcF_UNITPRICE" ClientInstanceName="srcF_UNITPRICE"  ClientVisible="false"></dx:ASPxTextBox>
                                </td>
                                <td class="tdTitle">작지번호</td>
                                <td class="tdContent">
                                    <dx:ASPxTextBox runat="server" ID="srcF_PLANNO" ClientInstanceName="srcF_PLANNO" Width="100%" >
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitle">검사원</td>
                                <td class="tdContent">
                                    <ucCTF:User runat="server" ID="ucUser" ClientInstanceID="srcF_INSPUSER" />
                                </td>
                                <td class="tdTitle">작업시간</td>
                                <td class="tdContent">
                                    <dx:ASPxComboBox runat="server" ID="srcF_GUBUN" ClientInstanceName="srcF_GUBUN" Width="100%" >
                                        <Items>
                                            <dx:ListEditItem Text="주간" Value="0" Selected="true" />                                
                                            <dx:ListEditItem Text="야간" Value="1" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>                            
                        </table>
                    </td>
                    <td style="vertical-align:top; padding-left:5px;">
                        <div style="width:100%; height:340px; overflow-y:auto;background:white; border:1px solid #A9A9A9">
                            <ucCTF:NgTypeTable runat="server" ID="ucNgTypeTable1" ValueChange="fn_NgTypeChange();" />
                        </div>
                    </td>
                    <td class="tdContent" style="vertical-align:top; padding-left:5px;">
                        <div style="width:100%; height:340px; overflow-y:auto; background:white;border:1px solid #A9A9A9">
                            <ucCTF:NgCauseTable runat="server" ID="ucNgCauseTable1" />
                        </div>                    
                    </td>
                </tr>
            </table>
        </div>
        <div class="divline">&nbsp;</div>
        <table class="InputTable" style="margin-bottom: 5px;">
            <colgroup>
                <col style="width: 10%" />
                <col style="width: 23%" />
                <col style="width: 10%" />
                <col style="width: 23%" />
                <col style="width: 10%" />
                <col style="width: 24%" />
            </colgroup>
            <tr>
                <td class="tdTitle">
                    <label>작업일자</label>
                </td>
                <td class="tdContentR">
                    <ucCTF:Date runat="server" ID="ucDate" DateTimeOnly="false" MaxDate="0" ClientInstanceID="ucDate" />
                </td>               
                 <td class="tdContent" colspan="4" style="border-left-width: 0px; text-align: right;">
                    <button
                        class="btn btn-sm btn-success" onclick="fn_OnSearchClick(); return false;">
                        <i
                            class="fa fa-search"></i><span class="text">조회</span>
                    </button>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    <label>품번</label>
                </td>
                <td class="tdContent">
                   <ucCTF:Item ID="ucItem" runat="server" />
                </td>
                <td class="tdTitle">
                    <label>공정</label>
                </td>
                <td class="tdContent" style="border-left-width: 0px;">
                    <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                </td>
                <td class="tdTitle">
                    <label>Lot No.</label>
                </td>
                <td class="tdContent" style="border-right-width: 0px;">
                    <dx:ASPxTextBox runat="server" ID="srcF_LOTNO_SEARCH" ClientInstanceName="srcF_LOTNO_SEARCH" ></dx:ASPxTextBox>
                </td>
               
            </tr>
        </table>
        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
            KeyFieldName="F_WORKDATE;F_GUBUN;F_ITEMCD;F_WORKCD;F_DAYPRODUCTNO" EnableViewState="false" EnableRowsCache="false"
            OnCustomCallback="devGrid_CustomCallback">
            <Styles>
                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                <EditForm CssClass="bg-default"></EditForm>
            </Styles>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
            <SettingsPager Mode="ShowAllRecords" />
            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="devGrid_RowDblClick" />
            <Columns>
                <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="작업일자" Width="100px" />
                <dx:GridViewDataColumn FieldName="F_DAYPRODUCTNO" Caption="일보 No." Width="150px" />                
                <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="150px">
                    <CellStyle HorizontalAlign="Left" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="150px">
                    <CellStyle HorizontalAlign="Left" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="150px" >
                    <CellStyle HorizontalAlign="Left" />
                </dx:GridViewDataColumn>                
                <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="130px" >
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" Width="200px">
                    <CellStyle HorizontalAlign="Left" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="F_PRODUCTQTY" Caption="생산수" Width="90px" >
                    <CellStyle HorizontalAlign="Right" />
                    <PropertiesTextEdit DisplayFormatString="n0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="F_INSPQTY" Caption="검사수" Width="90px" >
                    <CellStyle HorizontalAlign="Right" />
                    <PropertiesTextEdit DisplayFormatString="n0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="F_NGQTY" Caption="부적합수" Width="90px" >
                    <CellStyle HorizontalAlign="Right" />
                    <PropertiesTextEdit DisplayFormatString="n0" />
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataColumn FieldName="F_PLANNO" Caption="작지번호" Width="90px" >
                    <CellStyle HorizontalAlign="Center" />
                </dx:GridViewDataColumn>                

                <dx:GridViewDataColumn FieldName="F_GUBUN" Caption="구분" Visible="false" ></dx:GridViewDataColumn>                
            </Columns>
        </dx:ASPxGridView>
    </div>
    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
    <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
    <div class="paging"></div>
</asp:Content>
