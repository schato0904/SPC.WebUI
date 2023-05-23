<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="Upload3.aspx.cs" Inherits="SPC.WebUI.API.Common.Popup.Upload3" %>
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
        var parentCallback;

        $(document).ready(function () {
            var parentCallbackNm = '<%=ParentCallbackNm%>';
            if (parentCallbackNm != '' && typeof (parent) != "undefined" && typeof (parent['<%=ParentCallbackNm%>']) == "function") {
                parentCallback = parent[parentCallbackNm];
            }

            if (parentCallback != null) {
                $(window).bind("beforeunload", function () {
                    var attFileNo = hidATTFILENO.GetText();
                    var attFileCnt = hidATTFILECNT.GetText();
                    var idx = hidParentGridIndex.GetText();

                    if (attFileNo != "") {
                        var resultJson = {
                            "FileNO":attFileNo,
                            "FileCNT": attFileCnt,
                            "IDX": idx
                        };

                        parentCallback.call(parent.window, resultJson);
                    }
                });
            }
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth() - 30, 10));
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
                            //parent.fn_UploadedComplete2('');
                            // function fn_UploadedComplete2(val, NoCtrlId, valCnt, CntCtrlId)
                            parent.fn_UploadedComplete2('', hidATTFILENO_CtrlId.GetText(), 0, hidATTFILECNT_CtrlId.GetText());
                            $("#btnUpload").removeClass("disabled");                            
                        } else {
                            parent.fn_UploadedComplete2(s.cpATTFILENO, hidATTFILENO_CtrlId.GetText(), s.cpResultCnt, hidATTFILECNT_CtrlId.GetText());
                            $("#btnUpload").addClass("disabled");
                        }
                    }
                    hidATTFILECNT.SetText(devGrid.GetVisibleRowsOnPage());

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
            setTimeout(function () { ucNavi.SetNoticeOnce(""); }, 10);
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
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
            <%if(bMultiUpload){%>
                if (s.GetText(0) != '') {
                    $("#btnUpload").removeClass("disabled");
                } else {
                    $("#btnUpload").addClass("disabled");
                }
            <%}
              else {
                  if (FileCnt < 1)
                  {%>
                        if (s.GetText(0) != '') {
                            $("#btnUpload").removeClass("disabled");
                        } else {
                            $("#btnUpload").addClass("disabled");
                        }
                  <%}
              }%>
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
                    //alert('파일업로드가 완료되었습니다.');
                    var fileCnt = parseInt(_callbackData[2], 10);
                    // function fn_UploadedComplete2(val, NoCtrlId, valCnt, CntCtrlId)
                    var devGrid = ASPxClientGridView.Cast('devGrid');
                    hidATTFILENO.SetText(_callbackData[1]);
                    hidATTFILECNT.SetText(_callbackData[2]);

                    parent.fn_UploadedComplete2(_callbackData[1], hidATTFILENO_CtrlId.GetText(), parseInt(devGrid.GetVisibleRowsOnPage(), 10) + fileCnt, hidATTFILECNT_CtrlId.GetText());
                    devGrid.PerformCallback('search;' + _callbackData[1]);
                }
            } else {
                alert('파일업로드중 알수없는 장애가 발생하였습니다\r계속해서 장애가 발생하는 경우 관리자에 문의 바랍니다.');
            }
        }

        // 선택파일 다운로드
        function fn_OnSelectDownload(e) {
            var rowKey = devGrid.GetRowKey(e.visibleIndex).split('|');
            window.open('../Download.ashx?attfileno=' + rowKey[0] + '&attfileseq=' + rowKey[1] + '&data_gbn=' + rowKey[2] + '&compcd=<%=sCOMPCD%>');
        }

        // 전체파일 다운로드
        function fn_OnAllDownloadClick() {
            window.open('../Download.ashx?attfileno=' + hidATTFILENO.GetText() + '&data_gbn=' + hidDATA_GBN.GetText() + '&compcd=<%=sCOMPCD%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <%--<div class="search"></div>--%>
        <div class="content">
            <div runat="server" id="divDesc" style="display:none;">
                <span style="font-size:9pt;color:red;font-weight:bold;"><%= NOTICE %></span>
            </div>
            <dx:ASPxTextBox ID="hidATTFILENO_CtrlId" ClientInstanceName="hidATTFILENO_CtrlId" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
            <dx:ASPxTextBox ID="hidATTFILECNT_CtrlId" ClientInstanceName="hidATTFILECNT_CtrlId" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
            <dx:ASPxTextBox ID="hidATTFILECNT" ClientInstanceName="hidATTFILECNT" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
            <dx:ASPxTextBox ID="hidATTFILENO" ClientInstanceName="hidATTFILENO" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
            <dx:ASPxTextBox ID="hidParentGridIndex" ClientInstanceName="hidParentGridIndex" runat="server" ClientVisible="false" Text="false"></dx:ASPxTextBox>
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
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px" FixedStyle="Left">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox Native="true" ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
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
                            UploadMode="Standard" ShowProgressPanel="true" CancelButtonHorizontalPosition="Right"
                            OnFileUploadComplete="devUploader_FileUploadComplete" OnFilesUploadComplete="devUploader_FilesUploadComplete"  >
                            <CancelButton Text="업로드취소" />
                            <ValidationSettings MaxFileSize="104857600" />
                            <BrowseButton Text="파일찾기" />
                            <ClientSideEvents TextChanged="fn_OnUploaderTextChanged" FileUploadStart="fn_OnFileUploadStart"
                                FileUploadComplete="fn_OnFileUploadComplete" FilesUploadComplete="fn_OnFilesUploadComplete"
                                Init="function(s,e){ s.slUploadHelperUrl = _fn_CheckURL(s.slUploadHelperUrl); s.progressHandlerPage = _fn_CheckURL(s.progressHandlerPage); }" />
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
                                        <button id="btnAllDaonload" class="btn btn-sm btn-info" onclick="fn_OnAllDownloadClick(); return false;">
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
    <script type="text/javascript">
        (function () {
            var locs = location.href.split("/SPC.WebUI/");
            var rootURL = locs[0] + '/SPC.WebUI/';
            if (devUploader.progressHandlerPage.indexOf('/SPC.WebUI/') >= 0) {
                devUploader.progressHandlerPage = rootURL + devUploader.progressHandlerPage.substring(devUploader.progressHandlerPage.indexOf('/SPC.WebUI/') + 12);
            }
            if (devUploader.slUploadHelperUrl.indexOf('/SPC.WebUI/') >= 0) {
                devUploader.slUploadHelperUrl = rootURL + devUploader.slUploadHelperUrl.substring(devUploader.slUploadHelperUrl.indexOf('/SPC.WebUI/') + 12);
            }
        })();
    </script>
</asp:Content>
