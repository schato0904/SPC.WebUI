<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="SPC.WebUI.API.Common.Popup.Upload" %>
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
        .paging {
            width: 100%;
            display: table-row;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 데이타는 복원할 수 없습니다.')) { return false; }

            fn_doSetGridEventAction('true');

            selectedKeys = devGrid.GetSelectedKeysOnPage();

            var oParams = 'delete;' + selectedKeys;
            
            devGrid.PerformCallback(oParams);
        }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);

                    if (s.cpResultCode == '1') {
                        if (parseInt(s.cpResultCnt, 10) == 0) {
                            parent.fn_UploadedComplete('');
                        }
                    }

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Grid RowDblClick
        function fn_OnRowDblClick(s, e) {
            fn_OnSelectDownload(e);
        }

        // Upload
        function fn_OnUploadClick() {
            devUploader.Upload();
        }

        // 업로드 버튼 활성화 & 비활성화
        function fn_OnUploaderTextChanged(s, e) {
            if (s.GetText(0) != '') {
                // 검사성적서인 경우 파일을 무조건 1개만 등록가능
                if (fn_GetText('hidDATA_GBN') == 'C') {
                    var nCountFiles = devGrid.GetVisibleRowsOnPage();
                    if (nCountFiles > 0) {
                        alert('검사성적서는 1개의 파일만 업로드할 수 있습니다.\r\n새로운 파일을 올리려면 기존 등록된 파일을 삭제하세요!!');
                        s.ClearText();
                        return;
                    }
                }
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
            
        }

        // Upload Complete(전체완료시)
        function fn_OnFilesUploadComplete(s, e) {
            if (e.callbackData != '') {
                var _callbackData = e.callbackData.split('|');

                if (_callbackData[0] == 'Error') {
                    var errMsg = _callbackData[1].split('^');
                    errMsg = errMsg.join('\r');
                    alert('파일업로드중 장애가 발생하였습니다\r에러내용\r' + errMsg);
                } else if (_callbackData[0] == 'Success') {
                    alert('파일업로드가 완료되었습니다.');
                    parent.fn_UploadedComplete(_callbackData[1]);
                    devGrid.PerformCallback('search;' + _callbackData[1]);
                }
            } else {
                alert('파일업로드중 알수없는 장애가 발생하였습니다\r계속해서 장애가 발생하는 경우 관리자에 문의 바랍니다.');
            }
        }

        // 선택파일 다운로드
        function fn_OnSelectDownload(e) {
            var rowKey = devGrid.GetRowKey(e.visibleIndex).split('|');
            window.open('../Download.ashx?attfileno=' + rowKey[0] + '&attfileseq=' + rowKey[1] + '&data_gbn=' + rowKey[2] + '&compcd=<%#gsCOMPCD%>');
        }

        // 전체파일 다운로드
        function fn_OnAllDownloadClick() {
            window.open('../Download.ashx?attfileno=' + hidATTFILENO.GetText() + '&data_gbn=' + hidDATA_GBN.GetText() + '&compcd=<%#gsCOMPCD%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidATTFILENO" ClientInstanceName="hidATTFILENO" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
            <dx:ASPxTextBox ID="hidDATA_GBN" ClientInstanceName="hidDATA_GBN" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="ATTFILENO;ATTFILESEQ;DATA_GBN;DATA_NAME" EnableViewState="false" EnableRowsCache="false"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px" FixedStyle="Left">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="DATA_ORIGIN_NAME" Caption="파일명" Width="100%">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ATTFILESIZE" Caption="파일크기" Width="100px">
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ATTFILENO" Caption="파일그룹번호" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ATTFILESEQ" Caption="파일번호" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DATA_NAME" Caption="저장된파일명" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DATA_GBN" Caption="구분코드" Visible="false"></dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging">
            <div class="form-horizontal" style="padding-top: 10px;">
                <div class="form-group">
                    <div class="col-sm-7">
                        <dx:ASPxUploadControl ID="devUploader" ClientInstanceName="devUploader" runat="server" Width="100%" NullText="파일을 선택하세요"
                            UploadMode="Advanced" ShowProgressPanel="true" CancelButtonHorizontalPosition="Right"
                            OnFileUploadComplete="devUploader_FileUploadComplete" OnFilesUploadComplete="devUploader_FilesUploadComplete">
                            <CancelButton Text="업로드취소" />
                            <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png,.doc,.docx,.pdf" />
                            <BrowseButton Text="파일찾기" />
                            <ClientSideEvents TextChanged="fn_OnUploaderTextChanged" FileUploadStart="fn_OnFileUploadStart"
                                FileUploadComplete="fn_OnFileUploadComplete" FilesUploadComplete="fn_OnFilesUploadComplete" />
                        </dx:ASPxUploadControl>
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <ul class="nav navbar-nav navbar-right m-n nav-user user">
                                    <li style="padding-right: 5px;">
                                        <button id="btnUpload" class="btn btn-sm btn-warning disabled" onclick="fn_OnUploadClick(); return false;">
                                            <i class="fa fa-upload"></i>
                                            <span class="text">업로드</span>
                                        </button>
                                    </li>
                                    <li style="padding-right: 5px;">
                                        <button id="btnAllDownload" class="btn btn-sm btn-info" onclick="fn_OnAllDownloadClick(); return false;">
                                            <i class="fa fa-download"></i>
                                            <span class="text">전체다운로드</span>
                                        </button>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label" style="text-align: left;">(최대 100MB 까지 업로드 가능)</label>
                    <label class="col-sm-8 control-label" style="color: red;">업로드 또는 삭제 후 반드시 부모창의 저장버튼을 눌러 저장해야 반영됩니다</label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
