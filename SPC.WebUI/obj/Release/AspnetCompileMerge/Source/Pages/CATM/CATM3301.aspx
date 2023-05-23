<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM3301.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM3301" %>

<%-- 출하지시서(선입선출) 등록 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {

            setTimeout(SetNew, 100);
            fn_OnSearchClick();
            fn_OnSearchClick2();

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            fnASPxGridView_ReHeight(devGrid);
            fnASPxGridView_ReHeight(devGrid1);
        }

        // 조회
        function fn_OnSearchClick() {
            //fn_ObjInit();
            devGrid1.PerformCallback('GET');


        }

        function fn_OnSearchClick2() {
            devGrid.PerformCallback('GET');
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


            if (!confirm('저장하시겠습니까?')) {
                return false;
            }
            devCallback3.PerformCallback();



        }

        // 취소
        function fn_OnCancelClick() {
        }

        // 삭제
        function fn_OnDeleteClick() {

            if (!confirm('삭제하시겠습니까?')) {
                return false;
            }

            if (hidSERIAL.GetText() == '') {
                alert('삭제할 반품내역은 하단에서 선택해야 합니다.')
                return false;
            }

            hidGBN.SetValue('3');
            devCallback3.PerformCallback();
        }

        // 인쇄
        function fn_OnPrintClick() {

        }



        // 엑셀
        function fn_OnExcelClick() {
            if (devGrid.GetVisibleRowsOnPage() == 0) {
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
            return Trim(srcF_OUTORDERNO.GetText()) != '' && Trim(srcF_OUTORDERNO.GetText()) != '0';
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
            //    srcF_ITEMCD.SetValue('');
            //    srcF_ITEMNM.SetValue('');
            //}
            //var today = new Date().toISOString().slice(0, 10);
            srcF_OUTORDERNO.SetValue('');
            srcF_OUTDATE.SetValue(new Date());
            srcF_CUSTCD.SetValue('');
            srcF_ORDERNO.SetValue('');
            //srcF_ITEMCD.SetValue('');
            srcF_OUTCOUNT.SetValue('');
            srcF_MEMO.SetValue('');
        }
        // 기본값 설정
        function SetDefault() {
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
            srcF_OUTDATE.SetEnabled(false);
            srcF_CUSTCD.SetEnabled(false);
            srcF_ORDERNO.SetEnabled(false);
            //srcF_ITEMCD.SetEnabled(enable);
            srcF_OUTCOUNT.SetEnabled(false);
            //srcF_MEMO.SetEnabled(false);
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

        function ViewSelectedItem2(pkey) {
            // 콜백으로 우측영역값 json으로 조회
            // 우측영역 세팅
            pkey = pkey.split('|');
            var param = {
                'action': 'VIEW',
                //'F_COMPCD': pkey[0],
                //'F_FACTCD': pkey[1],
                'F_OUTORDERNO': pkey[0],
                'F_SERIAL': pkey[1]
            };
            devCallback2.PerformCallback(encodeURIComponent(JSON.stringify(param)));
        }

        // 선택항목 조회(좌측 키로 우측 목록 조회)
        function ViewList(pkey) {
            // 콜백으로 우측목록 grid 조회
            pkey = pkey.split('|');
            srcF_ITEMCD.SetText(pkey[0]);
            srcF_ITEMNM.SetText(pkey[1]);
            devGrid1.PerformCallback('GET');
            SetNew();
        }
        // 컨트롤 값 설정
        function SetValue(data) {
            if (typeof data.F_OUTORDERNO != 'undefined') srcF_OUTORDERNO.SetValue(data.F_OUTORDERNO);
            if (typeof data.F_OUTDATE != 'undefined') srcF_OUTDATE.SetValue(str2date(data.F_OUTYMD));
            if (typeof data.F_CUSTCD != 'undefined') srcF_CUSTCD.SetValue(data.F_CUSTCD);
            if (typeof data.F_ORDERNO != 'undefined') srcF_ORDERNO.SetValue(data.F_ORDERNO);
            if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            if (typeof data.F_ITEMNM != 'undefined') srcF_ITEMNM.SetValue(data.F_ITEMNM);
            if (typeof data.F_OUTCOUNT != 'undefined') srcF_OUTCOUNT.SetValue(data.F_OUTCOUNT);

            srcF_RECALL.SetValue('');

            F_RECALLCOUNT.SetValue('');
            srcF_MEMO.SetValue('');
            hidGBN.SetValue('1');
            hidSERIAL.SetValue('');
        }

        function SetValue2(data) {
            if (typeof data.F_OUTORDERNO != 'undefined') srcF_OUTORDERNO.SetValue(data.F_OUTORDERNO);
            if (typeof data.F_OUTDATE != 'undefined') srcF_OUTDATE.SetValue(str2date(data.F_OUTYMD));
            if (typeof data.F_CUSTCD != 'undefined') srcF_CUSTCD.SetValue(data.F_CUSTCD);
            if (typeof data.F_ORDERNO != 'undefined') srcF_ORDERNO.SetValue(data.F_ORDERNO);
            if (typeof data.F_ITEMCD != 'undefined') srcF_ITEMCD.SetValue(data.F_ITEMCD);
            if (typeof data.F_ITEMNM != 'undefined') srcF_ITEMNM.SetValue(data.F_ITEMNM);
            if (typeof data.F_OUTCOUNT != 'undefined') srcF_OUTCOUNT.SetValue(data.F_OUTCOUNT);

            if (typeof data.F_REASON != 'undefined') srcF_RECALL.SetValue(data.F_REASON);
            if (typeof data.F_RECALLCOUNT != 'undefined') F_RECALLCOUNT.SetValue(data.F_RECALLCOUNT);
            if (typeof data.F_MEMO != 'undefined') srcF_MEMO.SetValue(data.F_MEMO);
            if (typeof data.F_SERIAL != 'undefined') hidSERIAL.SetValue(data.F_SERIAL);

            hidGBN.SetValue('2');
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

        function devCallback_CallbackComplete2(s, e) {
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
                            SetValue2(result.data);
                            SetPageMode('EDIT');
                            fn_OnValidate();
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



        function devGrid1_RowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            ViewSelectedItem(key);
        }

        function devGrid_RowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            ViewSelectedItem2(key);
        }

        function fn_OnEndCallback3(s, e) {
            alert(s.cpResultMsg);
            fn_OnSearchClick2();
            SetReset();
        }

        function SetReset() {



            srcF_OUTDATE.SetText('');
            srcF_OUTORDERNO.SetValue('');
            srcF_ORDERNO.SetValue('');
            srcF_CUSTCD.SetValue('');
            srcF_OUTCOUNT.SetValue('');
            srcF_ITEMNM.SetValue('');
            srcF_ITEMCD.SetValue('');
            srcF_RECALL.SetValue('');
            F_RECALLCOUNT.SetValue('');
            srcF_MEMO.SetValue('');
            hidSERIAL.SetValue('');
            hidGBN.SetValue('');

        }





    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divLeft" style="width: 35%; float: left;">
            <div class="blockTitle">
                <span>[출하대상 품목 목록]</span>
            </div>
            <div id="divLeftTop" style="width: 100%; margin-bottom: 10px;">
                <table class="InputTable" style="width: 100%;">
                    <colgroup>
                        <col style="width: 7%" />
                        <col style="width: 20%" />                        
                        <col style="width: 6%" />                        
                    </colgroup>
                    <tr>
                        <td class="tdTitle">출하일자</td>
                        <td class="tdContentR">

                            <div style="width: 50%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_FROMYMD" ClientInstanceName="schF_FROMYMD" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                    Width="100%" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="width: 50%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_TOYMD" ClientInstanceName="schF_TOYMD" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                    Width="100%" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                        <td class="tdContentL">

                        </td>
                       
                    </tr>
                    <tr>`
                         <td class="tdTitle">품목코드</td>
                         <td class="tdContentR">
                            <ucCTF:Item ID="ucItem" runat="server" />

                        </td>
                        <td>

                        </td>
                    </tr>
                </table>
            </div>
            <div id="divLeftBody" style="width: 100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_OUTORDERNO" EnableViewState="false" EnableRowsCache="false"
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

                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError"
                        RowDblClick="devGrid1_RowDblClick" />
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>

                        <dx:GridViewDataColumn FieldName="F_OUTDATE" Caption="출하일자" Width="16%" />
                        <dx:GridViewDataColumn FieldName="F_CUSTNM" Caption="출하처" Width="16%" />
                        <dx:GridViewDataColumn FieldName="F_OUTORDERNO" Caption="출하지시번호" Width="20%" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="16%" />
                        <dx:GridViewDataSpinEditColumn FieldName="F_OUTCOUNT" Caption="출하수량" Width="16%" PropertiesSpinEdit-DisplayFormatString="{0:#,0}" />



                        <dx:GridViewDataColumn FieldName="F_MEMO" Caption="비고" CellStyle-HorizontalAlign="Left" />
                    </Columns>
                </dx:ASPxGridView>

            </div>
        </div>
        <div id="divSpace" style="width: 1%; float: left;">&nbsp;</div>
        <div id="divRight" style="width: 60%; float: left;">
            <div class="blockTitle">
                <span>[출하지시서 정보]</span>
            </div>

            <div id="divMiddle" style="margin-bottom: 10px;">
                <table class="InputTable" style="margin-bottom: 5px; width: 100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 22%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">출하일시</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_OUTDATE" ClientInstanceName="srcF_OUTDATE" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" ClientEnabled="false"
                                    Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd">
                                </dx:ASPxDateEdit>

                            </div>
                        </td>
                        <td class="tdTitle">출하지시번호</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_OUTORDERNO" ClientInstanceName="srcF_OUTORDERNO" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">발주번호</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ORDERNO" ClientInstanceName="srcF_ORDERNO" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">출하처</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_CUSTCD" ClientInstanceName="srcF_CUSTCD" IncrementalFilteringMode="None" ValueType="System.String" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">출하수량</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">

                                <dx:ASPxSpinEdit runat="server" ID="srcF_OUTCOUNT" ClientInstanceName="srcF_OUTCOUNT" ClientEnabled="false" Width="100%" NumberType="Integer" Number="1" MinValue="1" MaxValue="1000000" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" DisplayFormatString="{0:#,0}">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxSpinEdit>
                            </div>
                        </td>

                        <td class="tdTitle">품번</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_ITEMNM" ClientInstanceName="srcF_ITEMNM" ClientEnabled="false" Width="100%" ClientVisible="false">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                                <dx:ASPxTextBox runat="server" ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" ClientEnabled="false" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>

                    </tr>
                </table>
            </div>
            <div id="divMiddle2" style="margin-bottom: 10px;">
                <table class="InputTable" style="margin-bottom: 5px; width: 100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 22%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">반품사유</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_RECALL" ClientInstanceName="srcF_RECALL" IncrementalFilteringMode="None" ValueField="F_RECALLCD" OnDataBound="srcF_RECALL_DataBound" Width="100%"
                                    TextField="F_RECALLNM" ValueType="System.String" OnCallback="srcF_RECALL_Callback">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>

                            </div>
                        </td>
                        <td class="tdTitle">반품수량</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="F_RECALLCOUNT" ClientInstanceName="F_RECALLCOUNT" Width="100%" DisplayFormatString="{0:#,0}">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <td class="tdTitle">비고</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxTextBox runat="server" ID="srcF_MEMO" ClientInstanceName="srcF_MEMO" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
            <div class="blockTitle">
                <span>[반품 목록]</span>

            </div>
            <div id="divMiddle3" style="margin-bottom: 2px; width: 100%;">
                <table class="InputTable" style="margin-bottom: 5px; width: 100%;">
                    <colgroup>
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 21%" />
                        <col style="width: 12%" />
                        <col style="width: 22%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">반품일</td>
                        <td class="tdContent">
                            <%-- <div style="width: 100%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_OUTYMD2" ClientInstanceName="srcF_OUTYMD2" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>--%>

                            <div style="width: 50%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_FROMYMD2" ClientInstanceName="schF_FROMYMD2" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                    Width="100%" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="width: 50%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_TOYMD2" ClientInstanceName="schF_TOYMD2" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                    Width="100%" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>
                        </td>




                        <td class="tdTitle">품번/명</td>
                        <td class="tdContentR">
                            <dx:ASPxTextBox runat="server" ID="schF_SEARCHTEXT" ClientInstanceName="schF_SEARCHTEXT" ClientEnabled="true">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxTextBox>

                        </td>
                        <td class="tdContentLR"></td>


                        <td class="tdContentL">

                            <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick2(); return false;" style="width: 80px;">
                                <span class="text">조회</span>
                            </button>
                        </td>




                    </tr>
                    <tr>
                        <td class="tdTitle">출하처</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_CUSTCD2" ClientInstanceName="srcF_CUSTCD2" IncrementalFilteringMode="None" Width="100%"
                                    ValueField="F_CUSTCD" TextField="F_CUSTNM" ValueType="System.String" OnCallback="srcF_CUSTCD2_Callback" OnDataBound="srcF_CUSTCD2_DataBound">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">반품사유</td>
                        <td class="tdContentR">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="srcF_RECALL2" ClientInstanceName="srcF_RECALL2" IncrementalFilteringMode="None" ValueField="F_RECALLCD" OnDataBound="srcF_RECALL2_DataBound" Width="100%"
                                    TextField="F_RECALLNM" ValueType="System.String" OnCallback="srcF_RECALL2_Callback">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdContentLR"></td>
                        <td class="tdContentL"></td>
                    </tr>
                </table>

            </div>
            <div id="divRightBody" style="width: 100%;">
                <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGrid1Exporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_OUTORDERNO;F_SERIAL;" EnableViewState="false" EnableRowsCache="false"
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
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>

                        
                        <dx:GridViewDataColumn FieldName="F_OUTYMD" Caption="출하일자" Width="11%" />
                        <dx:GridViewDataColumn FieldName="F_RECALLYMD" Caption="반품일자" Width="11%" />
                        <dx:GridViewDataColumn FieldName="F_CUSTNM" Caption="출하처" Width="11%" />
                        <dx:GridViewDataColumn FieldName="F_OUTORDERNO" Caption="출하지시번호" Width="12%" />
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="11%" />                        
                        <dx:GridViewDataSpinEditColumn FieldName="F_OUTCOUNT" Caption="납품수량" Width="11%" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>                        
                        <dx:GridViewDataColumn FieldName="F_REASON" Caption="반품사유" Width="11%" />
                        <dx:GridViewDataSpinEditColumn FieldName="F_RECALLCOUNT" Caption="반품수량" Width="11%" PropertiesSpinEdit-DisplayFormatString="{0:#,0}"/>
                        <dx:GridViewDataColumn FieldName="F_MEMO" Caption="비고" Width="11%" />
                        <dx:GridViewDataColumn FieldName="F_ORDERNO" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <dx:GridViewDataColumn FieldName="F_SERIAL" CellStyle-CssClass="hidden" EditCellStyle-CssClass="hidden" EditFormCaptionStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" GroupFooterCellStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
        <div style="clear: both;"></div>
    </div>
    <div id="divHidden" style="display: none;">
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />

        <dx:ASPxTextBox runat="server" ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" ClientVisible="false">
        </dx:ASPxTextBox>

        <dx:ASPxTextBox runat="server" ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" ClientVisible="false">
        </dx:ASPxTextBox>

        <dx:ASPxTextBox runat="server" ID="hidSERIAL" ClientInstanceName="hidSERIAL" ClientVisible="false">
        </dx:ASPxTextBox>

        <dx:ASPxTextBox runat="server" ID="hidGBN" ClientInstanceName="hidGBN" ClientVisible="false">
        </dx:ASPxTextBox>

        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>

        <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>

        <dx:ASPxCallback ID="devCallback2" runat="server" ClientInstanceName="devCallback2" OnCallback="devCallback2_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete2" />
        </dx:ASPxCallback>

        <dx:ASPxCallback ID="devCallback3" runat="server" ClientInstanceName="devCallback3" OnCallback="devCallback3_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback3" />
        </dx:ASPxCallback>
    </div>
</asp:Content>
