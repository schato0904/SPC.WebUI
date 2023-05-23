<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" CodeBehind="FDCKUSERPOP.aspx.cs" Inherits="SPC.WebUI.Pages.FDCK.Popup.FDCKUSERPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .search {
            width: 100%;
            display: table-row;
        }
        .content {
            width: 100%;
            height: 100%;
            display: table-row;
        }
        .left {
            width: 300px;
            height: 100%;
            display: table-cell;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            height: 100%;
            display: table-cell;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            _selectedRowKeyValue = devTree.GetFocusedNodeKey();
            hidTreeFocusedKey.SetText(_selectedRowKeyValue);
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".content").offset().top;
            var height = Math.max(0, $(document).height() - _hMargin - top);
            var sectionContentHeight = parseInt($("#sectionContent").height(), 10);
            devTree.SetHeight(height - 10);
        }

        // 트리노드 더블클릭 : 우측에 해당정보 조회
        function fn_OnNodeDblClick(s, e) {
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

        // Grid End Callback
        function fn_devGridOnEndCallback(s, e) {
        }

        function fn_OnValidate() {
        }

        // 캐쉬갱신하기
        function fn_OnRefreshCommonCodeCache() {
        }

        // Tree End Callback
        function fn_OnEndCallback() { }

        // 완료 클릭시 선택값 반환
        function fn_OnAcceptClick() {
            var devPopCallback = ASPxClientCallback.Cast('devPopCallback');
            devPopCallback.PerformCallback();
            //devPopCallback.SendCallback('');
        }

        // 콜백 완료시
        function fn_devPopCallback_CallbackComplete(s, e) {
            var result = e.result;
            var callbackfn = hidCallbackFn.GetText();
            if (parent != null) {
                if (callbackfn != '' && typeof parent[callbackfn] == 'function') {
                    parent[callbackfn](result);
                } else if (typeof (parent.fn_AfterFDCKUSERPOP) == "function") {
                    parent.fn_AfterFDCKUSERPOP(result);
                }
            }
            fn_Close();
        }

        function fn_devPopCallback_CallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_devPopCallback_EndCallback(s, e) {
            //alert('Callback End');
        }

        function fn_OnGridInit(s, e) {
            var devTree = ASPxClientTreeList.Cast(s);
            devTree.SetFocusedNodeKey(null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div class="left">
                <section id="sectionTree" class="panel panel-default">
                    <dx:ASPxTextBox ID="hidCallbackFn" ClientInstanceName="hidCallbackFn" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidTreeFocusedKey" ClientInstanceName="hidTreeFocusedKey" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidTreeFocusedLevel" ClientInstanceName="hidTreeFocusedLevel" runat="server" ClientVisible="false" Text="0"></dx:ASPxTextBox>
                    <dx:ASPxTreeList ID="devTree" ClientInstanceName="devTree" runat="server" AutoGenerateColumns="false"
                        Width="100%" KeyFieldName="F_CODE" ParentFieldName="F_UPCD" OnInit="devTree_Init" OnDataBound="devTree_DataBound" >
                        <Columns>
                            <dx:TreeListDataColumn FieldName="F_CODENM" Caption="사용자" Width="100%" VisibleIndex="0" />
                            <dx:TreeListDataColumn FieldName="F_CODE" Caption="사용자/부서코드" Width="50" VisibleIndex="1" Visible="false" />
                        </Columns>
                        <SettingsSelection Enabled="true" AllowSelectAll="false" />
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" ShowColumnHeaders="true" />
                        <SettingsBehavior ExpandCollapseAction="Button" AllowSort="false" AllowFocusedNode="True" AutoExpandAllNodes="true" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" NodeDblClick="fn_OnNodeDblClick" />
                    </dx:ASPxTreeList>
                    <dx:ASPxCallback ID="devPopCallback" ClientInstanceName="devPopCallback" runat="server" OnCallback="devPopCallback_Callback">
                        <ClientSideEvents CallbackComplete="fn_devPopCallback_CallbackComplete" CallbackError="fn_devPopCallback_CallbackError" EndCallback="fn_devPopCallback_EndCallback" />
                    </dx:ASPxCallback>
                </section>
            </div>
        </div>
    </div>
</asp:Content>
