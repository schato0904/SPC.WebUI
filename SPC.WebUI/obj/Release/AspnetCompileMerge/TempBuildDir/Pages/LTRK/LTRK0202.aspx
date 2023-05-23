<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRK0202.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.LTRK0202" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_OnSearchClick()
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
        function fn_OnSearchClick() {
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {

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
        }

        function fn_OnClose(sORDERGROUP, sORDERDATE, sORDERNO) {
            if (!confirm('선택한 작업지시서를 마감 처리하겠습니까? 계속 진행하려면 확인을 누르세요!\r마감처리된 작업지시서는 복원할 수 없습니다')) return;

            var data = {
                'ACTION': 'CLOSE',
                'F_ORDERGROUP': sORDERGROUP,
                'F_ORDERDATE': sORDERDATE,
                'F_ORDERNO': sORDERNO
            };

            fn_devCallback_PerformCallback(data);
        }

        function fn_OnDelete(sORDERGROUP, sORDERDATE, sORDERNO, bISDELETE) {
            if (bISDELETE == '0') {
                if (!confirm('선택한 작업지시서를 삭제 처리하겠습니까? 계속 진행하려면 확인을 누르세요!\r삭제한 데이터는 복원할 수 없습니다')) return;

                var data = {
                    'ACTION': 'DELETE',
                    'F_ORDERGROUP': sORDERGROUP,
                    'F_ORDERDATE': sORDERDATE,
                    'F_ORDERNO': sORDERNO
                };

                fn_devCallback_PerformCallback(data);
            } else {
                alert('실적이 등록된 작업지시서는 삭제할 수 없습니다.\r더 이상 사용을 원하지 않는 경우 마감처리 하세요');
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
                    case "CLOSE":
                        alert(msg);
                        fn_OnSearchClick();
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="contentTable">
                <colgroup>
                    <col style="width:80px;" />
                    <col style="width:100px;" />
                    <col style="width:80px;" />
                    <col style="width:100px;" />
                    <col style="width:80px;" />
                    <col />
                    <col style="width:80px;" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">지시일</td>
                    <td class="tdInput">
                        <ucCTF:Date runat="server" id="ucDate" SingleDate="true" TodayFromDate="true" />
                    </td>
                    <td class="tdLabel">상태</td>
                    <td class="tdInput">
                        <ucCTF:CommonCodeDDL ID="ucCommonCode" runat="server" targetParams="AA|AAE6" />
                    </td>
                    <td class="tdLabel">품목</td>
                    <td class="tdInput">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </td>
                    <td class="tdLabel">공정</td>
                    <td class="tdInput">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ORDERGROUP;F_ORDERDATE;F_ORDERNO;F_STATUS" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_ORDERNO" Caption="작지번호" Width="150px" />
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품목코드" Width="110px" />
                    <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_GUBNNM" Caption="품목구분" Width="90px" />
                    <dx:GridViewDataTextColumn FieldName="F_GROUPNM" Caption="품목그룹" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_WORKNM" Caption="공정명" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPCD" Caption="설비코드" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="설비명" Width="120px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="F_WORKSTDT" Caption="생산시작일시" Width="130px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" />
                    <dx:GridViewDataDateColumn FieldName="F_WORKEDDT" Caption="생산종료일시" Width="130px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" />
                    <dx:GridViewDataSpinEditColumn FieldName="F_ORDERCNT" Caption="지시수량" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_USEDCNT" Caption="실적수량" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataTextColumn FieldName="F_UNITNM" Caption="단위" Width="70px" />
                    <dx:GridViewDataTextColumn FieldName="F_STATUS" Caption="상태" Width="70px" />
                    <dx:GridViewDataTextColumn Caption="마감" UnboundType="String" Width="60px">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnClose" runat="server" Text="마감" OnInit="btnClose_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="삭제" UnboundType="String" Width="60px">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnDelete" runat="server" Text="삭제" OnInit="btnDelete_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="투입" UnboundType="String" Width="60px">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnInput" runat="server" Text="투입" OnInit="btnInput_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="실적" UnboundType="String" Width="60px">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnEarning" runat="server" Text="실적" OnInit="btnEarning_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
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