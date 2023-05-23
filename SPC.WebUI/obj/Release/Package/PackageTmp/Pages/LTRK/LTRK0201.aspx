<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRK0201.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.LTRK0201" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var orderDate = '';

        $(document).ready(function () {
            fn_RendorTotalCount();

            fn_SetDate('txtTODT', addDays(new Date(), 10));
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

        function fn_OnUploadClick() {
            if (fn_GetCastText('hidUCFROMDT1') == '' || fn_GetCastValue('hidUCFROMDT1') == '') {
                alert('작업지시일을 선택하세요!!');
                return false;
            }

            devUploader.Upload();
        }

        // 업로드 버튼 활성화 & 비활성화
        function fn_OnUploaderTextChanged(s, e) {
            if (s.GetText(0) != '') {
                $("#btnUpload").removeClass("disabled");
            } else {
                $("#btnUpload").addClass("disabled");
            }
        }

        // Upload Start
        function fn_OnFileUploadStart(s, e) {
            var files = s.GetText().replace(/\s|C:\\fakepath\\/g, "").split(",");
        }

        // Upload Complete(개별파일완료시)
        function fn_OnFileUploadComplete(s, e) {
            //alert(e.callbackData)
        }

        // Upload Complete(전체완료시)
        function fn_OnFilesUploadComplete(s, e) {
            if (e.callbackData != '') {
                //alert(e.callbackData)
                var _callbackData = e.callbackData.split('|');
                //alert(e.callbackData)
                if (_callbackData[0] == 'Error') {
                    var errMsg = _callbackData[1].split('^');
                    errMsg = errMsg.join('\r');
                    alert('파일업로드중 장애가 발생하였습니다\r에러내용\r' + errMsg);
                } else if (_callbackData[0] == 'Success') {
                    //alert(e.callbackData);
                    devGrid.PerformCallback('S|' + e.callbackData + '|' + fn_GetCastText('hidUCFROMDT1'));
                }
            } else {
                alert('파일업로드중 알수없는 장애가 발생하였습니다\r계속해서 장애가 발생하는 경우 관리자에 문의 바랍니다.');
            }
        }

        function fn_OnDownloadExcelClick() {
            window.open('./ExcelDownload.ashx?pCOMPCD=<%=this.gsCOMPCD%>');
        }

        function fn_OnSelDownload(ATTFILENO, ATTFILESEQ, DATA_GBN, COMPCD) {
            window.open('../../API/Common/Download.ashx?attfileno=' + ATTFILENO + '&attfileseq=' + ATTFILESEQ + '&data_gbn=' + DATA_GBN + '&compcd=' + COMPCD);
        }

        function fn_OnLink(code, date, status) {
            if (status == '2' || status == '8') {
                fn_OnPopupLTRK0201POP01(code, date)
            } else {
                fn_OnPopupLTRK0201POP02(code, date)
            }
        }

        function fn_OnConfirmDone(code, date) {
            fn_OnSearchClick();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="contentTable">
                <colgroup>
                    <col style="width:80px;" />
                    <col style="width:230px;" />
                    <col style="width:80px;" />
                    <col style="width:150px;" />
                    <col style="width:80px;" />
                    <col style="width:60px;" />
                    <col style="width:110px;" />
                    <col />
                    <col style="width:80px;" />
                    <col style="width:150px;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">지시기간</td>
                    <td class="tdInput">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </td>
                    <td class="tdLabel">상태</td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="ddlSTATUS" ClientInstanceName="ddlSTATUS" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true" />
                                <dx:ListEditItem Text="임시등록" Value="8" />
                                <dx:ListEditItem Text="등록중" Value="9" />
                                <dx:ListEditItem Text="등록완료" Value="1" />
                                <dx:ListEditItem Text="등록취소" Value="0" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdLabel">업로드</td>
                    <td class="tdInput tdContentR text-danger-lter font-bold text-right">지시일</td>
                    <td class="tdInput tdContentLR">
                        <ucCTF:Date1 ID="ucDate1" runat="server" SingleDate="true" TodayFromDate="true" />
                    </td>
                    <td class="tdInput tdContentLR">
                        <dx:ASPxUploadControl ID="devUploader" ClientInstanceName="devUploader" runat="server" Width="100%" NullText="파일을 선택하세요"
                            UploadMode="Advanced" ShowProgressPanel="true" CancelButtonHorizontalPosition="Right"
                            OnFileUploadComplete="devUploader_FileUploadComplete" OnFilesUploadComplete="devUploader_FilesUploadComplete">
                            <CancelButton Text="업로드취소" />
                            <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".xls,.xlsx" />
                            <BrowseButton Text="파일찾기" />
                            <ClientSideEvents TextChanged="fn_OnUploaderTextChanged" FileUploadStart="fn_OnFileUploadStart"
                                FileUploadComplete="fn_OnFileUploadComplete" FilesUploadComplete="fn_OnFilesUploadComplete" />
                        </dx:ASPxUploadControl>
                    </td>
                    <td class="tdInput tdContentLR">
                        <button id="btnUpload" class="btn btn-sm btn-warning disabled" onclick="fn_OnUploadClick(); return false;">
                            <i class="fa fa-upload"></i>
                            <span class="text">업로드</span>
                        </button>
                    </td>
                    <td class="tdInput tdContentL">
                        <button id="btnDownload" class="btn btn-sm btn-info" onclick="fn_OnDownloadExcelClick(); return false;">
                            <i class="fa fa-download"></i>
                            <span class="text">기본양식 다운로드</span>
                        </button>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
            <div class="text-danger-dker font-bold" style="padding-left:640px;">↑↑↑파일업로드 시 선택한 지시일로 작업지시가 생성됩니다. 반드시 지시일을 확인하고 업로드 하세요.</div>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD;F_ORDERGROUP;F_ORDERDATE" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ORDERDATE" Caption="지시일" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_INSDT" Caption="등록일시" Width="200px" />
                    <dx:GridViewDataTextColumn FieldName="DATA_ORIGIN_NAME" Caption="첨부파일" UnboundType="String">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                        <DataItemTemplate>
                            <dx:ASPxHyperLink ID="btnOneDownload" runat="server" OnInit="btnOneDownload_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_USERNM" Caption="등록자" Width="120px" />
                    <dx:GridViewDataColumn FieldName="F_STATUS" Caption="상태" Width="120px" />
                    <dx:GridViewDataTextColumn Caption="등록현황" UnboundType="String" Width="120px">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnLink" runat="server" Text="등록현황" OnInit="btnLink_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>