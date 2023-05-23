<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS4001POP.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.Popup.MEAS4001POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var parentCallback = null;
        var callObj = null;

        $(document).ready(function () {
            var parentCallbackNm = '<%=ParentCallback%>';
            if (parentCallbackNm.split('.').length > 1) {
                var parentId = parentCallbackNm.split('.')[0];
                var parentFunc = parentCallbackNm.split('.')[1];
                if (typeof (eval('parent[parentId].' + parentFunc)) == "function") {
                    callObj = parent[parentId];
                    parentCallback = eval('parent[parentId].' + parentFunc);
                }
            }
            else {
                if (parentCallbackNm != '' && typeof (parent) != "undefined" && typeof (parent[parentCallbackNm]) == "function") {
                    callObj = parent;
                    parentCallback = parent[parentCallbackNm];
                }
            }
            fn_setdate();
        });

        function fn_setdate() {
            srcF_REQDT_FROM.SetValue(new Date().First());
            srcF_REQDT_TO.SetValue(new Date());
        }

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
        function fn_OnValidate(s, e) { }

        // RowDblClick
        function fn_OnRowDblClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var rowKey = rowKeys.split('|');

            //F_REQNO;F_REQDT
            // JSON 값으로 반환
            var i = 0;
            var returnValue = {
                "F_REQNO": rowKey[i++]
               , "F_REQDT": rowKey[i++]
            };

            if (parentCallback != null) {
                parentCallback.call(callObj, returnValue);
            }

            parent.fn_devPopupClose();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search" style="margin-bottom:5px;">
            <table class="InputTable">
                <colgroup>
                    <col style="width:10%" />
                    <col style="width:23%" />
                    <col style="width:10%" />
                    <col style="width:23%" />
                    <col style="width:10%" />
                    <col style="width:24%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>의뢰번호</label>
                    </td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_REQNO" ClientInstanceName="srcF_REQNO" runat="server" Width="100%" ></dx:ASPxTextBox>
                    </td>
                    <td class="tdTitle">
                        <label>의뢰일자</label>
                    </td>
                    <td class="tdContent">
                        <div style="width:50%;float:left;">
                        <dx:ASPxDateEdit runat="server" ID="srcF_REQDT_FROM" ClientInstanceName="srcF_REQDT_FROM" UseMaskBehavior="true" 
                            EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                            Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                        </dx:ASPxDateEdit>
                    </div>
                    <div style="width:50%;float:left;">
                        <dx:ASPxDateEdit runat="server" ID="srcF_REQDT_TO" ClientInstanceName="srcF_REQDT_TO" UseMaskBehavior="true" 
                            EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                            Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                        </dx:ASPxDateEdit>
                    </div>
                    <div style="clear:both;"></div>
                    </td>
                    <td class="tdTitle">
                        <label>진행상태</label>
                    </td>
                    <td class="tdContent">
                        <dx:ASPxComboBox runat="server" ID="srcF_STATUS" ClientInstanceName="srcF_STATUS" Width="100%"></dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_REQNO;F_REQDT" 
                EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowFocusedRow="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_REQNO" Caption="의뢰번호" Width="150px" CellStyle-HorizontalAlign="Center" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_REQDT" Caption="의뢰일자" Width="100px" CellStyle-HorizontalAlign="Center" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_REQCNT" Caption="의뢰수량" PropertiesTextEdit-DisplayFormatString="n0" Width="80px" CellStyle-HorizontalAlign="Right" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_REGUSER" Caption="작성자명" Width="90px" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STATUS" Caption="진행상태" Width="90px" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
