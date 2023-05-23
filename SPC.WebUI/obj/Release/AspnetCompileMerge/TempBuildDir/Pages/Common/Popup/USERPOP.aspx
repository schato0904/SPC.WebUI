<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="USERPOP.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Popup.USERPOP" %>
<%-- 사용자목록 조회 팝업 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var parentCallback = null;
        var callobj = window;

        $(document).ready(function () {
            var parentCallbackNm = '<%=parentCallback%>';
            if (hidOpenType.GetText() == "UC") {
                if (parentCallbackNm != '' && typeof (parent) != "undefined" && typeof (parent.window['<%=parentCallback%>'].fn_SetUCControl) == "function") {
                    parentCallback = parent[parentCallbackNm];
                }
                parentCallback = parent.window['<%=parentCallback%>'].fn_SetUCControl;
                callobj = parent.window['<%=parentCallback%>'];
            } else {
                if (parentCallbackNm != '' && typeof (parent) != "undefined" && typeof (parent['<%=parentCallback%>']) == "function") {
                    parentCallback = parent[parentCallbackNm];
                }
                parentCallback = parent['<%=parentCallback%>']
            }
            fn_OnSearchClick();
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
            var parentParams = ''
            <%if (!gsVENDOR) {%>
            //var parentCompCD = parent.fn_OnGetCompCD();
            //parentParams += (parentParams == '') ? parentCompCD : '|' + parentCompCD;
            //var parentFactCD = parent.fn_OnGetFactCD();
            //parentParams += (parentParams == '') ? parentFactCD : '|' + parentFactCD;
            <%}%>
            devGrid.PerformCallback(parentParams);
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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // RowDblClick
        function fn_OnRowDblClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var rowKey = rowKeys.split('|');
            // JSON 값으로 반환
            var returnValue = {
                F_USERID: rowKey[0]
               , F_USERNM: rowKey[1]
            };

            if (parentCallback != null) {
                parentCallback.apply(callobj, [returnValue]);
            }
            //parent.fn_OnSettingItem(rowKey[0], rowKey[1]);
            //if (hidOpenType.GetText() == 'UC') {            // UserControl 에서 호출한 경우
            //    parent.fn_OnUCSettingItem(rowKey[0], rowKey[1], rowKey[2], rowKey[3]);
            //} else if (hidOpenType.GetText() == 'FORM') {   // 일반 Form 에서 호출한 경우
            //    parent.fn_OnSettingItem(rowKey[0], rowKey[1], rowKey[3]);
            //} else if (hidOpenType.GetText() == 'S') {      // 검사기준복사에서 호출한 경우(Source)
            //    parent.fn_OnSettingItem(rowKey[0], 'S');
            //} else if (hidOpenType.GetText() == 'T') {      // 검사기준복사에서 호출한 경우(Target)
            //    parent.fn_OnSettingItem(rowKey[0], 'T');
            //}
            //else if (hidOpenType.GetText() == 'UC1') {      // 검사기준복사에서 호출한 경우(Target)
            //    parent.fn_OnUCSettingItem1(rowKey[0], rowKey[1], rowKey[2], rowKey[3]);
            //}
            //else if (hidOpenType.GetText() == 'UC2') {      // 검사기준복사에서 호출한 경우(Target)
            //    parent.fn_OnUCSettingItem2(rowKey[0], rowKey[1], rowKey[2], rowKey[3]);
            //}
            parent.fn_devPopupClose();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidOpenType" ClientInstanceName="hidOpenType" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-2 control-label">사용자ID</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtUSERID" ClientInstanceName="txtUSERID" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-7 control-label"></label>
                </div>
            </div>
        </div>
        <dx:ASPxTextBox ID="txtMACHGUBUN" ClientInstanceName="txtMACHGUBUN" runat="server" ClientVisible="false" />
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_USERID;F_USERNM" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_DEPARTCD" Caption="부서코드" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_DEPARTNM" Caption="부서명" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_GRADENM" Caption="직위" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_USERID" Caption="사용자ID" Width="150px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="사용자명" Width="100%">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
