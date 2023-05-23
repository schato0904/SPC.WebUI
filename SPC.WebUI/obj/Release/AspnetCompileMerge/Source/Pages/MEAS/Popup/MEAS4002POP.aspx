<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS4002POP.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.Popup.MEAS4002POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var callObj = null;

        $(document).ready(function () {
            devGrid.PerformCallback();
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
            var rowCount = devGrid.GetVisibleRowsOnPage();
            if (rowCount == 0) {
                alert('변경된 사항이 없습니다');
            }
            else {
                if (!confirm('엑셀 데이터를 업로드 하시겠습니까?.')) { return false; }
                var param = {
                    'action': 'SAVE'
                };
                devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
            }
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
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
                    //alert(s.cpResultMsg);
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

        function cboF_CCSCD_ValueChanged(s, e) {
            fn_OnCancelClick();
            devGrid.PerformCallback();
        }

        // ASPxCallback 사용 : 우측항목 조회, 저장, 삭제
        function devCallback_CallbackComplete(s, e) {
            try {
                if (e.result != '') {
                    var result = JSON.parse(decodeURIComponent(e.result));
                    if (typeof result.msg != 'undefined' && Trim(result.msg) != '') alert(result.msg);
                }
            } catch (err) {
                alert(err);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search" style="margin-bottom:5px;">
            <table class="InputTable">
                <colgroup>
                    <col style="width:10%" />
                    <col style="width:20%" />
                    <col style="width:70%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>첨부파일</label>
                    </td>
                    <td class="tdContentR">
                        <dx:ASPxUploadControl ID="srcF_FILE" runat="server" ClientInstanceName="srcF_FILE" AutoStartUpload="true" ShowProgressPanel="True" FileUploadMode="OnPageLoad" OnFileUploadComplete="srcF_FILE_FileUploadComplete" >
                            <ClientSideEvents FileUploadComplete="cboF_CCSCD_ValueChanged" />
                        </dx:ASPxUploadControl>
                    </td>
                    <td class="tdContent">
                        <button class="btn btn-sm btn-success" style="width: 100px; padding: 3px 0px;" onclick="fn_OnSaveClick(); return false;">
                            <i class="i i-file-plus "></i>
                            <span class="text">저장</span>
                        </button>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <div class="blockTitle">
                    <span>[엑셀정보]</span>
                </div>
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%"
                EnableViewState="false" EnableRowsCache="false" KeyFieldName="NO"
                OnCustomCallback="devGrid_CustomCallback" >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="10" ShowStatusBar="Hidden"  HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false"  />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"/>
            </dx:ASPxGridView>
            <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
            <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CallbackComplete="devCallback_CallbackComplete" />
        </dx:ASPxCallback>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
