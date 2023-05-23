<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRK0201POP01.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.Popup.LTRK0201POP01" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
            if (fn_GetCastValue('hidF_STATUS') == '8') {
                $('#btnConfirm').show();
                $('#btnCancel').show();
            } else {

                $('#btnConfirm').hide();
                $('#btnCancel').hide();
            }
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
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }

            devGrid.UpdateEdit();
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
                } else if (s.cpResultCode == '1') {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";

                    parent.fn_OnConfirmDone('<%=sORDERGROUP%>', '<%=sORDERDATE%>');
                    location.href = fn_OnPopupLTRK0201POP02LOC('<%=sORDERGROUP%>', '<%=sORDERDATE%>');
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";

                    fn_OnCancelClick();

                    $("#btnConfirm").attr("disabled", true);
                    $("#btnCancel").attr("disabled", true);
                }

                fn_RendorTotalCount();
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            var rowKey = devGrid.GetRowKey(e.visibleIndex).split('|');
            if (rowKey[4].toLowerCase() == 'false') {
                var editor = s.GetEditor('F_ORDERCNT');
                fn_OnControlDisableBox(editor, null);
                alert('검증실패한 정보는 수정할 수 없습니다');
                fn_OnCancelClick();
            } else {
                var editor = s.GetEditor('F_ORDERCNT');
                fn_OnControlEnableBox(editor, null);
            }
        }

        function fn_OnLoadError(msg) {
            alert(msg);
            fn_Close();
        }

        function fn_OnConfirm() {
            $("#btnConfirm").attr("disabled", true);
            $("#btnCancel").attr("disabled", true);

            if (!confirm('등록을 진행하겠습니까? 계속 진행하려면 확인을 누르세요!\r확정된 작업지시서는 바로 사용할 수 있습니다')) {
                $("#btnConfirm").attr("disabled", false);
                $("#btnCancel").attr("disabled", false);
                return;
            }

            if (!devGrid.batchEditApi.HasChanges()) {
                var data = {
                    'ACTION': 'CONFIRM'
                };

                fn_devCallback_PerformCallback(data);
            } else {
                devGrid.UpdateEdit();
            }
        }

        function fn_OnCancel() {
            $("#btnConfirm").attr("disabled", true);
            $("#btnCancel").attr("disabled", true);

            if (!confirm('등록을 삭제하겠습니까? 계속 진행하려면 확인을 누르세요!\r삭제한 데이터는 복원할 수 없습니다')) {
                $("#btnConfirm").attr("disabled", false);
                $("#btnCancel").attr("disabled", false);
            }

            var data = {
                'ACTION': 'CANCEL'
            };

            fn_devCallback_PerformCallback(data);
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
                    case "CONFIRM":
                        alert(msg);
                        parent.fn_OnConfirmDone('<%=sORDERGROUP%>', '<%=sORDERDATE%>');
                        if (msg.indexOf('취소') < 0)
                            location.href = fn_OnPopupLTRK0201POP02LOC('<%=sORDERGROUP%>', '<%=sORDERDATE%>');
                        break;
                    case "CANCEL":
                        alert(msg);
                        parent.fn_OnConfirmDone('<%=sORDERGROUP%>', '<%=sORDERDATE%>');
                        break;
                }
            } else {
                alert(msg);
                $("#btnConfirm").attr("disabled", false);
                $("#btnCancel").attr("disabled", false);
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
                    <col style="width:150px;" />
                    <col style="width:80px;" />
                    <col />
                    <col style="width:80px;" />
                    <col style="width:120px;" />
                    <col style="width:80px;" />
                    <col style="width:100px;" />
                    <col style="width:90px;" />
                    <col style="width:95px;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">지시일</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_ORDERDATE" ClientInstanceName="srcF_ORDERDATE" runat="server" CssClass="no-border" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />

                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">등록일시</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_INSDT" ClientInstanceName="srcF_INSDT" runat="server" CssClass="no-border" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">첨부파일</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcDATA_ORIGIN_NAME" ClientInstanceName="srcDATA_ORIGIN_NAME" runat="server" CssClass="no-border" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">등록자</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_USERNM" ClientInstanceName="srcF_USERNM" runat="server" CssClass="no-border" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">상태</td>
                    <td class="tdInput tdContentR">
                        <dx:ASPxTextBox ID="hidF_STATUS" ClientInstanceName="hidF_STATUS" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_STATUS" ClientInstanceName="srcF_STATUS" runat="server" CssClass="no-border" Width="100%" HorizontalAlign="Center" Font-Bold="true">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdInput tdContentLR">
                        <button id="btnConfirm" class="btn btn-sm btn-success" onclick="fn_OnConfirm(); return false;">
                            <i class="i i-ok"></i>
                            <span class="text">등록확정</span>
                        </button>
                    </td>
                    <td class="tdInput tdContentL">
                        <button id="btnCancel" class="btn btn-sm btn-danger" onclick="fn_OnCancel(); return false;">
                            <i class="i i-cancel"></i>
                            <span class="text">등록삭제</span>
                        </button>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
            <div style="color:black;background-color:white;border:1px solid #808080;padding:5px;">
                1. <span style="color:blue;font-weight:bold;">지시수량</span>은 수정가능합니다.<br />
                2. 설비는 기본정보 > 공장기본정보관리 > 공정관리 에서 공정별로 설정하세요.<br />
                3. 검증결과가 <span style="color:red;font-weight:bold;">검증실패</span>인 경우 작업지시로 등록되지 않습니다.(검증실패에 마우스 커서를 대면 상세 오류정보가 출력됩니다)<br />
                4. 계속해서 등록을 진행하려면 <span style="color:green;font-weight:bold;">[등록진행]</span>을 등록을 취소하고 엑셀을 다시 등록하려면 <span style="color:red;font-weight:bold;">[등록삭제]</span>를 클릭하세요<br />
                5. <span style="color:green;font-weight:bold;">[등록진행]</span>시 즉시 작업지시서 사용이 가능합니다.<br />
                <span style="color:blue;font-weight:bold;">* 현재 엑셀정보를 데이터베이스로 옮기는 단계입니다. 현재 단계 진행 후 최종 작업지시 등록을 진행해야 작업지시서가 배포됩니다</span>
            </div>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_WORKCD;F_EQUIPCD;F_IDX;F_RESULT" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnBatchUpdate="devGrid_BatchUpdate">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_RESULT" Caption="검증결과" Width="70px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_SINGLE" Caption="중복여부" Width="70px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Caption="품목명">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_GUBN" Caption="품목구분" Width="150px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_GROUP" Caption="품목그룹" Width="150px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_WORKCD" Caption="공정코드" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_WORKNM" Caption="공정명" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPCD" Caption="설비코드" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="설비명" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_ORDERCNT" Caption="지시수량" Width="120px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataTextColumn FieldName="F_UNITNM" Caption="단위" Width="80px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
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