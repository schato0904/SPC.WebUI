<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0205IMGPOP.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.Popup.BSIF0205IMGPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var uploadElement = ['', '', '', '', ''];
        var uploadFiles = [];
        var uploadFileCnt = 0;
        var uploadStart = false;

        $(document).ready(function () {
            var oSetParam = "<%=oSetParam%>";
            if (oSetParam != '') {
                var oSetParams = oSetParam.split('|');
                //품목
                $('#divITEMCD').text(oSetParams[0]);
                $('#divITEMNM').text(oSetParams[1]);
            }
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid.PerformCallback('select;');
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

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 데이타는 복원할 수 없습니다.')) { return false; }

            fn_doSetGridEventAction('true');

            var oParams = 'delete;' + selectedKeys;

            devGrid.PerformCallback(oParams);
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

        // Callback Control EndCallback
        function fn_OnCallbackComplete(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            } else {
                devGrid.PerformCallback('select;');
            }

            uploadElement = ['', '', '', '', ''];
            uploadFiles = [];
            uploadStart = false;
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // 이미지보기
        function fn_OnCustomButtonClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var comp = '<%=this.GetCompCD()%>';
            window.open("<%=this.ResolveUrl("~/")%>API/Common/Gathering/ImageView.ashx?code=" + comp + "&name=" + rowKeys.split('|')[2] + "&gbn=DRAWING", "_blank", "");
        }

        // Upload
        function fn_OnUploadClick() {
            uploadFiles = [];
            uploadStart = true;

            uploadFILEIMAGE01.Upload();
            uploadFILEIMAGE02.Upload();
            uploadFILEIMAGE03.Upload();
            uploadFILEIMAGE04.Upload();
            uploadFILEIMAGE05.Upload();
        }

        // Upload Complete(개별파일완료시)
        function fn_OnFileUploadComplete(s, e) {
            if (e.callbackData != '') {
                uploadFiles[uploadFiles.length] = e.callbackData;
            }

            //alert('uploadFileCnt : ' + uploadFileCnt);
            //alert('uploadFiles.length : ' + uploadFiles.length);

            if (uploadFiles.length >= uploadFileCnt) {
                callbackControl.PerformCallback(uploadFiles.join(';'));
            }
        }

        // 파일명 변경
        function fn_OnUploaderTextChanged(s, e) {
            if (!uploadStart) {
                var inputElement = s.GetMainElement();
                var idx = parseInt(inputElement.id.substring(inputElement.id.length - 1), 10) - 1;
                if (s.GetText(0) != '') {
                    uploadElement[idx] = 'exists'
                } else {
                    uploadElement[idx] = ''
                }

                uploadFileCnt = 0;
                uploadElement.forEach(function (value) { if (value != '') uploadFileCnt++; });
                
                if (uploadFileCnt > 0) {
                    $("#btnUpload").removeClass("disabled");
                } else {
                    $("#btnUpload").addClass("disabled");
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">품번</label>
                    <div id="divITEMCD" class="col-sm-4"></div>
                    <label class="col-sm-1 control-label">품명</label>
                    <div id="divITEMNM" class="col-sm-6"></div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="form-group">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_ITEMCD;F_FIELD;F_FILE" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                    <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="false" AllowSelectByRowClick="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" CustomButtonClick="fn_OnCustomButtonClick" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                    ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="F_TITLE" Caption="구분" Width="80px">
                        </dx:GridViewDataColumn>
                        <%--<dx:GridViewDataColumn FieldName="F_PATH" Caption="경로" Width="100%">
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataColumn>--%>
                        <dx:GridViewDataColumn FieldName="F_FILE" Caption="파일명" Width="100%">
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_FIELD" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewCommandColumn Caption="미리보기" Width="100px"  >
                        <CustomButtons>                            
                            <dx:GridViewCommandColumnCustomButton Text="미리보기" ID="btnPreview" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
            </div>
        </div>
        <div class="paging">
            <div class="form-horizontal" style="margin-bottom: 10px;">
                <div class="form-group">
                    <div class="col-sm-10">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">도면</label>
                            <div class="col-sm-10">
                                <dx:ASPxUploadControl ID="uploadFILEIMAGE01" ClientInstanceName="uploadFILEIMAGE01" runat="server" Width="100%"  NullText="파일을 선택하세요"
                                    UploadMode="Advanced" ShowProgressPanel="true"
                                    OnFileUploadComplete="uploadFILEIMAGE_FileUploadComplete">
                                    <BrowseButton Text="파일찾기" />
                                    <ClientSideEvents FileUploadComplete="fn_OnFileUploadComplete" TextChanged="fn_OnUploaderTextChanged" />
                                    <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png" />
                                </dx:ASPxUploadControl>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">사진1</label>
                            <div class="col-sm-10">
                                <dx:ASPxUploadControl ID="uploadFILEIMAGE02" ClientInstanceName="uploadFILEIMAGE02" runat="server" Width="100%"  NullText="파일을 선택하세요"
                                    UploadMode="Advanced" ShowProgressPanel="true"
                                    OnFileUploadComplete="uploadFILEIMAGE_FileUploadComplete">
                                    <BrowseButton Text="파일찾기" />
                                    <ClientSideEvents FileUploadComplete="fn_OnFileUploadComplete" TextChanged="fn_OnUploaderTextChanged" />
                                    <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png" />
                                </dx:ASPxUploadControl>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">사진2</label>
                            <div class="col-sm-10">
                                <dx:ASPxUploadControl ID="uploadFILEIMAGE03" ClientInstanceName="uploadFILEIMAGE03" runat="server" Width="100%"  NullText="파일을 선택하세요"
                                    UploadMode="Advanced" ShowProgressPanel="true"
                                    OnFileUploadComplete="uploadFILEIMAGE_FileUploadComplete">
                                    <BrowseButton Text="파일찾기" />
                                    <ClientSideEvents FileUploadComplete="fn_OnFileUploadComplete" TextChanged="fn_OnUploaderTextChanged" />
                                    <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png" />
                                </dx:ASPxUploadControl>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">사진3</label>
                            <div class="col-sm-10">
                                <dx:ASPxUploadControl ID="uploadFILEIMAGE04" ClientInstanceName="uploadFILEIMAGE04" runat="server" Width="100%"  NullText="파일을 선택하세요"
                                    UploadMode="Advanced" ShowProgressPanel="true"
                                    OnFileUploadComplete="uploadFILEIMAGE_FileUploadComplete">
                                    <BrowseButton Text="파일찾기" />
                                    <ClientSideEvents FileUploadComplete="fn_OnFileUploadComplete" TextChanged="fn_OnUploaderTextChanged" />
                                    <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png" />
                                </dx:ASPxUploadControl>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">사진4</label>
                            <div class="col-sm-10">
                                <dx:ASPxUploadControl ID="uploadFILEIMAGE05" ClientInstanceName="uploadFILEIMAGE05" runat="server" Width="100%"  NullText="파일을 선택하세요"
                                    UploadMode="Advanced" ShowProgressPanel="true"
                                    OnFileUploadComplete="uploadFILEIMAGE_FileUploadComplete">
                                    <BrowseButton Text="파일찾기" />
                                    <ClientSideEvents FileUploadComplete="fn_OnFileUploadComplete" TextChanged="fn_OnUploaderTextChanged" />
                                    <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png" />
                                </dx:ASPxUploadControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <ul class="nav navbar-nav navbar-right m-n nav-user user">
                                    <li style="padding-right: 5px;">
                                        <button id="btnUpload" class="btn btn-sm btn-warning disabled" onclick="fn_OnUploadClick(); return false;">
                                            <i class="fa fa-upload"></i>
                                            <span class="text">업로드</span>
                                        </button>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    <!-- CallBack 처리를 위한 객체 -->
    <dx:ASPxCallback ID="callbackControl" ClientInstanceName="callbackControl" runat="server" OnCallback="callbackControl_Callback">
        <ClientSideEvents CallbackComplete="fn_OnCallbackComplete" CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>