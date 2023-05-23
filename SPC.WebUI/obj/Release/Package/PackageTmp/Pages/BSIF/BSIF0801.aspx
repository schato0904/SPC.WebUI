<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0801.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0801" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
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
            fn_doSetGridEventAction('true');
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
            if (!devGrid.IsNewRowEditing() && !devGrid.IsEditing()) {
                alert('신규등록 되거나 수정된 데이타가 없습니다');
                return false;
            }

            if (false == fn_OnValidate()) return false;

            if (uploadFILEIMAGE.GetText() != "") {
                uploadFILEIMAGE.Upload();
            } else {
                devGrid.UpdateEdit();
            }
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
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        function fn_OnValidate() {
            if (hidPLANT.GetText() == '') {
                alert('공장코드 필수입력사항입니다!!');
                ddlPLANT.Focus();
                return false;
            }
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

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                fn_OnControlEnableComboBox(s.GetEditor('F_PLANT'), false);
            } else {
                fn_OnControlEnableComboBox(s.GetEditor('F_PLANT'), true);
            }
        }

        function fn_AddLineClick(s, e) {
            if (devGrid.GetRowKey(e.visibleIndex) == 'undefined' || devGrid.GetRowKey(e.visibleIndex) == null) {
                alert("작업자 정보를 저장 후 등록해주세요.");
                return;
            }
            fn_OnPopupBSIF0801(devGrid.GetRowKey(e.visibleIndex));
        }

        function fn_OnJUYAValueChanged(s, e) {
            var val = s.GetValue();
            hidJUYA.SetValue(val);
        }

        function fn_OnPLANTValueChanged(s, e) {
            var val = s.GetValue();
            hidPLANT.SetValue(val);
        }

        // 도면이미지 Upload
        function fn_OnFileUploadComplete(s, e) {
            devGrid.UpdateEdit();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlPLANT" ClientInstanceName="ddlPLANT" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">주/야구분</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlJUYA" ClientInstanceName="ddlJUYA" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="F_PLANT;F_NO" EnableViewState="False" EnableRowsCache="False" OnHtmlEditFormCreated="devGrid_HtmlEditFormCreated"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnRowInserting="devGrid_RowInserting" OnRowUpdating="devGrid_RowUpdating" OnRowDeleting="devGrid_RowDeleted"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <ClientSideEvents EndCallback="fn_devGridOnEndCallback" />
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" CustomButtonClick="fn_AddLineClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PLANT" Name="F_PLANT" Caption="공장구분" Width="100px" Visible="false" >
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PLANTNM" Name="F_PLANTNM" Caption="공장구분" Width="100px" >
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_JUYA" Name="F_JUYA" Caption="주/야구분" Width="100px" Visible="false" >
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_JUYANM" Name="F_JUYANM" Caption="주/야구분" Width="100px" >
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_NO" Name="F_NO" Caption="번호" Width="40px" Visible="false" >
                        <Settings AllowSort="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ITEMNM" Name="F_ITEMNM" Caption="품명" Width="180px" >
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_WORKNM" Caption="공정" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="적격숙련자" Width="100px">
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LEVEL" Caption="자격레벨" Width="70px" >
                        <PropertiesTextEdit ConvertEmptyStringToNull="false" />
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataBinaryImageColumn FieldName="F_IMAGE" Caption="사진" Width="120px">
                        <PropertiesBinaryImage ImageHeight="140px"></PropertiesBinaryImage>
                    </dx:GridViewDataBinaryImageColumn>
                    <dx:GridViewCommandColumn Caption="사진등록" Width="100px" Visible="false"  >
                        <CustomButtons>                            
                            <dx:GridViewCommandColumnCustomButton ID="등록" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_SEQ" Caption="순번" Name="F_SEQ" Width="40px">
                        <PropertiesSpinEdit NumberType="Integer" MinValue="0" MaxValue="99999" SpinButtons-ShowIncrementButtons="false" >
                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용" Width="60px" />
                </Columns>
                <Templates>
                            <EditForm>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">공장코드</label>
                                        <div class="col-sm-3">
                                            <dx:ASPxTextBox ID="hidPLANT" ClientInstanceName="hidPLANT" runat="server" ClientVisible="false" Text='<%# Bind("[F_PLANT]") %>' />
                                            <dx:ASPxComboBox ID="ddlPLANT" ClientInstanceName="ddlPLANT" runat="server" Width="100%" 
                                                IncrementalFilteringMode="None" CssClass="NoXButton" OnDataBound="ddlComboBox_DataBound">
                                                <ClientSideEvents Init="fn_OnControlDisable" ValueChanged="fn_OnPLANTValueChanged" />
                                            </dx:ASPxComboBox>
                                        </div>
                                        <label class="col-sm-2 control-label">순번</label>
                                        <div class="col-sm-2">
                                            <dx:ASPxTextBox ID="txtSEQ" ClientInstanceName="txtSEQ" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_SEQ]") %>' />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                    <label class="col-sm-2 control-label">품명</label>
                                        <div class="col-sm-3">
                                            <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_ITEMNM]") %>' />
                                        </div>
                                    
                                        <label class="col-sm-2 control-label">공정</label>
                                        <div class="col-sm-3">
                                            <dx:ASPxTextBox ID="txtWORKNM" ClientInstanceName="txtWORKNM" runat="server" Width="100%" class="form-control input-sm" Text='<%# Bind("[F_WORKNM]") %>' />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">적격숙련자</label>
                                        <div class="col-sm-3">
                                            <dx:ASPxTextBox ID="txtWORKMAN" ClientInstanceName="txtWORKMAN" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_WORKMAN]") %>' />
                                        </div>
                                        <label class="col-sm-2 control-label">자격레벨</label>
                                        <div class="col-sm-3">
                                            <dx:ASPxTextBox ID="txtLEVEL" ClientInstanceName="txtLEVEL" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_LEVEL]") %>' />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">사진</label>
                                        <div class="col-sm-3">
                                            <dx:ASPxUploadControl ID="uploadFILEIMAGE" runat="server" Width="200px" BrowseButton-Text="찾기" ClientInstanceName="uploadFILEIMAGE" 
                                                UploadMode="Advanced" CancelButtonHorizontalPosition="Right" ShowProgressPanel="true"
                                                OnFileUploadComplete="uploadFILEIMAGE_FileUploadComplete">
                                                <ClientSideEvents FileUploadComplete="fn_OnFileUploadComplete" />
                                                <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg,.jpeg,.gif,.png" />
                                            </dx:ASPxUploadControl>
                                        </div>
                                    
                                        <label class="col-sm-2 control-label">주/야간</label>
                                        <div class="col-sm-2">
                                            <dx:ASPxTextBox ID="hidJUYA" ClientInstanceName="hidJUYA" runat="server" ClientVisible="false" Text='<%# Bind("[F_JUYA]") %>' />
                                            <dx:ASPxComboBox ID="ddlJUYA" ClientInstanceName="ddlJUYA" runat="server" Width="100%" 
                                                IncrementalFilteringMode="None" CssClass="NoXButton" OnDataBound="ddlComboBox_DataBound">
                                                <ClientSideEvents Init="fn_OnControlDisable" ValueChanged="fn_OnJUYAValueChanged" />
                                            </dx:ASPxComboBox>
                                        </div>
                                        <label class="col-sm-1 control-label">사용</label>
                                        <div class="col-sm-1">
                                            <dx:ASPxCheckBox ID="chkSTATUS" ClientInstanceName="chkSTATUS" runat="server" Width="100%" class="form-control input-sm" Value='<%# Bind("[F_STATUS]") %>' />
                                        </div>
                                    </div>
                                </div>
                            </EditForm>
                        </Templates>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback" />
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>