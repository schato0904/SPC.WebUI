<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0302SUBPOP.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.Popup.INSP0302SUBPOP" %>
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
            var height = Math.max(0, parseInt($(window).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid1.SetHeight(height - 10);
            devGrid2.SetHeight(250);
        }

        // 조회
        function fn_OnSearchClick() {
            var data = '{';
            data += '"ACTION":"GET_NM"';
            data += ',"F_ITEMCD":"' + fn_GetCastValue('schF_ITEMCD') + '"';
            data += ',"F_WORKCD":"' + fn_GetCastValue('schF_WORKCD') + '"';
            data += ',"F_MOLDNO":"' + fn_GetCastValue('schF_MOLDNO') + '"';
            data += '}';
            fn_devCallback_PerformCallback($.parseJSON(data));
            devGrid1.PerformCallback(data);
        }

        // 입력
        function fn_OnNewClick() {
            // F_ITEMCD;F_WORKCD;F_WORKDATE;F_TSERIALNO
            var selectedKeys = devGrid1.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('중간검사(EF) 샘플검사로 등록할 데이터를 선택하세요!!');
                return false;
            }

            var key, i = 0;
            var data = '[';
            selectedKeys.forEach(function (keys) {
                key = keys.split('|');
                if (i > 0) data += ',';
                data += '{';
                data += '"F_ITEMCD":"' + key[0] + '"';
                data += ',"F_WORKCD":"' + key[1] + '"';
                data += ',"F_WORKDATE":"' + key[2] + '"';
                data += ',"F_TSERIALNO":"' + key[3] + '"';
                data += '}';
                i++;
            });
            data += ']';
            parent.fn_OnSetSampleData(data);
            fn_Close();
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

        // CallbackPanel Event Handler 시작
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                fn_OnOpenLoadingPanel();
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
                    case "GET_NM":
                        fn_SetTextValue('schF_ITEMNM', GetJsonValueByKey(data, 'F_ITEMNM', ''));
                        fn_SetTextValue('schF_WORKNM', GetJsonValueByKey(data, 'F_WORKNM', ''));
                        break;
                }
            } else {
                alert(msg);
            }

            fn_OnCloseLoadingPanel();
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

        function fn_OnRowDblClick(s, e) {
            // F_ITEMCD; F_WORKCD; F_WORKDATE; F_TSERIALNO; F_LOTNO; F_MOLDNO; F_WORKMAN
            var devGrid = ASPxClientGridView.Cast(s);
            var rowKey = devGrid.GetRowKey(e.visibleIndex).split('|');
            var data = '{';
            data += '"F_ITEMCD":"' + rowKey[0] + '"';
            data += ',"F_WORKCD":"' + rowKey[1] + '"';
            data += ',"F_WORKDATE":"' + rowKey[2] + '"';
            data += ',"F_TSERIALNO":"' + rowKey[3] + '"';
            data += '}';
            devGrid2.PerformCallback(data);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
    <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="blockTitle"><span>[선택한 품목/공정 정보]</span></div>
            <table class="contentTable">
                <colgroup>
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">품번/품명</td>
                    <td class="tdInput" colspan="5">
                        <table class="layerTable">
                            <colgroup>
                                <col style="width:200px;" />
                                <col style="width:5px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="schF_ITEMCD" ClientInstanceName="schF_ITEMCD" runat="server" Width="100%" HorizontalAlign="Center">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td></td>
                                <td>
                                    <dx:ASPxTextBox ID="schF_ITEMNM" ClientInstanceName="schF_ITEMNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                    <td class="tdLabel">공정코드/명</td>
                    <td class="tdInput" colspan="3">
                        <table class="layerTable">
                            <colgroup>
                                <col style="width:100px;" />
                                <col style="width:5px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="schF_WORKCD" ClientInstanceName="schF_WORKCD" runat="server" Width="100%" HorizontalAlign="Center">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td></td>
                                <td>
                                    <dx:ASPxTextBox ID="schF_WORKNM" ClientInstanceName="schF_WORKNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdLabel">작지번호</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="schF_MOLDNO" ClientInstanceName="schF_MOLDNO" runat="server" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_WORKCD;F_WORKDATE;F_TSERIALNO" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid1_CustomCallback" OnHtmlDataCellPrepared="devGrid1_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid1" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid1.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드"  Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="200px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO" Caption="시료군"  Width="70px"  />
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No."  Width="100px"  />
                    <dx:GridViewDataColumn FieldName="F_MEASECNT" Caption="시료수"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_OKCNT" Caption="합격수"  Width="80px" CellStyle-ForeColor="Blue"/>
                    <dx:GridViewDataColumn FieldName="F_OCCNT" Caption="관리이탈수"  Width="80px" CellStyle-ForeColor="DimGray" />
                    <dx:GridViewDataColumn FieldName="F_NGCNT" Caption="불합격수"  Width="80px" CellStyle-ForeColor="Red"  />
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="검사원"  Width="80px"  />
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging">
            <div class="divPadding"></div>
            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_SERIALNO;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid2_CustomCallback" OnCustomColumnDisplayText="devGrid2_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO"    Caption="측정군"  Width="70px"  />
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="항목코드"  Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="항목명" Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번"  Width="50px"  />
                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격"  Width="80px" >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한"  Width="80px" >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한"  Width="80px" >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NGOKCHK" Caption="판정" Width="80px" />
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>