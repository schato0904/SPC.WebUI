<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS4001.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS4001" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = '';
        var editVisibleIndex = -1;

        $(document).ready(function () {
            FIXDT.SetValue(new Date());
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(devGrid.GetMainElement()).offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - pagingHeight - scrollbarWidth(), 10));
            //alert(height);
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            //if (ucReqNoPOP.GetReqNo() == "") {
            //    ucReqNoPOP.fn_OnPopupReqNoSearch();
            //    return;
            //}

            devGrid.PerformCallback();
        }

        // 엑셀
        function fn_OnExcelClick() {
            //srcF_REQNO.SetText(ucReqNoPOP.GetReqNo());
            //srcF_REQDT.SetText(ucReqNoPOP.GetReqDt());
            btnExport.DoClick();
        }

        // 입력
        function fn_OnNewClick() {
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
            if (fn_OnValidate()) {
                devGrid.batchEditApi.EndEdit();
                if (!devGrid.batchEditApi.HasChanges())
                    alert('변경된 사항이 없습니다');
                else
                    devGrid.UpdateEdit();
            }
        }

        // 오류시
        function fn_OnCallbackError(s, e) {
            alert(e);
        }

        // 취소
        function fn_OnCancelClick() {
            ASPxClientEdit.ClearEditorsInContainerById('clientContainer');
            devGrid.CancelEdit();
            //devGrid.PerformCallback('clear');
        }

        // 삭제
        function fn_OnDeleteClick() {
            var selectedKeys = devGrid.GetSelectedKeysOnPage();
            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (devGrid.batchEditApi.HasChanges()) {
                if (!confirm("저장되지 않은 정보가 있습니다.확인 버튼 클릭시 저장하지 않은 데이터는 사라집니다.\r삭제하시겠습니까?")) return false;
            }
            else {
                if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 데이터는 복원하실 수 없습니다.')) { return false; }
            }

            devCallback.PerformCallback('DEL');

            //for (var i = 0; i < selectedKeys.length ; i++) {
            //    devGrid.DeleteRowByKey(selectedKeys[i]);
            //}
        }

        // 인쇄
        function fn_OnPrintClick() {
            btnExport.DoClick();
        }

        // Validate
        function fn_OnValidate(s, e) {
            return ASPxClientEdit.ValidateEditorsInContainerById('clientContainer');
        }

        // Grid End Callback
        function fn_OnGridEndCallback(s, e) {
            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    fn_pagerPerformCallback(s.cpResultMsg);
                }
                else {
                    alert(s.cpResultMsg);
                    fn_doSetGridEventAction('false');
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
        }

        // 저장, 삭제 후 처리
        function devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            // 정상일 경우 그리드 재 조회
            if (result.CODE == "1") {
                switch (result.TYPE) {                   
                    case "AfterDelete":
                        // 그리드 조회 후
                        devGrid.PerformCallback();
                        break;
                    default: break;
                }
            }

            alert(result.MESSAGE);
        }

        // 오류시
        function devCallback_CallbackError(s, e) {
            alert(e);
        }

        // 첨부파일보기
        function fn_OnAttfileViewClick(index) 
        {
            
            var F_ATTFILENO = devGrid.batchEditApi.GetCellValue(index, 'F_ATTFILENO');
            if (F_ATTFILENO != null && F_ATTFILENO != "") {
                fn_AttachFileOpenReadOnly_grid('파일보기', 'M', 'T', F_ATTFILENO, '<%=gsCOMPCD%>');
            };
        }

        // 첨부파일 버튼 클릭
        function fn_OnAttfileClick() {
            var F_ATTFILENO = devGrid.GetEditValue("F_ATTFILENO");            
            if (F_ATTFILENO == null)
                F_ATTFILENO = "";

            fn_AttachFileOpenCallBackForGrid('파일등록', 'M', 'T', editVisibleIndex, F_ATTFILENO, '<%=gsCOMPCD%>', 'fn_OnAttfileCallback');
        }

        function fn_OnAttfileCallback(result) {

            devGrid.SetEditValue("F_PRE_ATTFILECNT", result.FileCNT);
            devGrid.SetEditValue("F_ATTFILECNT", result.FileCNT);
            devGrid.SetEditValue("F_ATTFILENO", result.FileNO);

            devGrid.batchEditApi.SetCellValue(editVisibleIndex, "F_PRE_ATTFILECNT", result.FileCNT);
            devGrid.batchEditApi.SetCellValue(editVisibleIndex, "F_ATTFILECNT", result.FileCNT);
            devGrid.batchEditApi.SetCellValue(editVisibleIndex, "F_ATTFILENO", result.FileNO);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            editVisibleIndex = e.visibleIndex;

            srcF_TEMP.SetText("");
            srcF_HYGRO.SetText("");
            srcF_REGUSER.SetText("");
            srcF_CNFMUSER.SetText("");

            srcF_TEMP.SetText(devGrid.batchEditApi.GetCellValue(editVisibleIndex, 'F_TEMP'));
            srcF_HYGRO.SetText(devGrid.batchEditApi.GetCellValue(editVisibleIndex, 'F_HYGRO'))
            srcF_REGUSER.SetText(devGrid.batchEditApi.GetCellValue(editVisibleIndex, 'F_REGUSER_2'))
            srcF_CNFMUSER.SetText(devGrid.batchEditApi.GetCellValue(editVisibleIndex, 'F_CNFMUSER'))
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) { }

        function fn_OnPopupReqNoSearch()
        {

            fn_OnPopupMEAS4001POPSearch('fn_OnPopupCloseCallback');
        }

        function fn_OnPopupCloseCallback(result)
        {
            srcF_REQNO.SetValue(result.F_REQNO);
            srcF_REQDT.SetValue(result.F_REQDT);
            devGrid.PerformCallback();
        }

        function fn_setting()
        {
            //alert(FIXNUM.GetText());
            //alert(devGrid.GetVisibleRowsOnPage());
            for (var i = 0; i < devGrid.GetVisibleRowsOnPage() ; i++)
            {
                //alert(rdoJUDGE.GetValue());
                devGrid.batchEditApi.SetCellValue(i, "F_FIXNO", FIXNUM.GetValue());
                devGrid.batchEditApi.SetCellValue(i, "F_FIXDT", FIXDT.GetValue());
                devGrid.batchEditApi.SetCellValue(i, "F_JUDGECD", rdoJUDGE.GetValue());
                devGrid.batchEditApi.SetCellValue(i, "F_ENDYN", chk_endyn.GetValue());
            }
        }

        function expop() {
            var pPage = './Popup/MEAS4002POP.aspx?TITLE=엑셀 업로드(검교정실적)';
            fn_OnPopupOpen(pPage, 1280, 850);
        }

    </script>
    <style type="text/css">
        .dispNone {
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback" ClientSideEvents-CallbackError="devCallback_CallbackError" ClientSideEvents-CallbackComplete="devCallback_CallbackComplete" />
    <div class="container">
        <div class="search">
        <%-- 하단 조회조건 --%>
        <div class="content2">
            <table class="InputTable">
                <colgroup>
                    <col style="width:8%" />
                    <col style="width:15%" />
                    <col style="width:8%" />
                    <col style="width:8%" />
                    <col style="width:7%" />
                    <col style="width:24%" />
                    <col style="width:8%" />
                    <col style="width:15%" />
                    <col style="width:10%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>의뢰번호</label>
                    </td>
                    <td class="tdContentR" colspan="3">
                        <div class="control-label" style="float: left; width:42%;">
                            <dx:ASPxTextBox ID="srcF_REQNO" ClientInstanceName="srcF_REQNO" runat="server"></dx:ASPxTextBox>
                        </div>
                        <div class="control-label" style="float: left; width: 8%;text-align:left;padding-left:2px;padding-top:0;">
                            <button id="btn" class="btn btn-default btn-xs" data-toggle="button" title="의뢰번호" onclick="fn_OnPopupReqNoSearch(); return false;">
                                <i class="i i-popup text"></i>
                                <i class="i i-popup text-active text-danger"></i>
                            </button>
                        </div>
                        <div class="control-label" style="float: left; width:50%;">
                            <dx:ASPxTextBox ID="srcF_REQDT" ClientInstanceName="srcF_REQDT" runat="server" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                        </div>
                    </td>
                    <td class="tdContentR" colspan="4"></td>
                    <td class="tdContent">
                        <button class="btn btn-sm btn-success" style="width: 100px; padding: 3px 0px;" onclick="expop(); return false;">
                            <i class="i i-file-plus "></i>
                            <span class="text">엑셀업로드</span>
                        </button>
                    </td>
                </tr>
             </table>
            </div>
            <div class="form-group"></div>
            <div class="content2" id="clientContainer">
                <div class="form-group">
                    <label class="col-sm-12 h5 font-bold" style="padding:3px;"> </label>
                    <label class="col-sm-12 h5 font-bold bg-primary" style="padding:5px;">■ 일괄적용항목 </label>
                </div>
                <table class="InputTable">
                    <colgroup>
                        <col style="width:8%" />
                        <col style="width:15%" />
                        <col style="width:8%" />
                        <col style="width:15%" />
                        <col style="width:8%" />
                        <col style="width:15%" />
                        <col style="width:8%" />
                        <col style="width:15%" />
                        <col style="width:10%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">
                            <label>교정일자</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxDateEdit runat="server" ID="FIXDT" ClientInstanceName="FIXDT"  Width="100%"></dx:ASPxDateEdit>
                        </td>
                        <td class="tdTitle">
                            <label>교정번호</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="FIXNUM" ClientInstanceName="FIXNUM" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                            
                        </td>
                        <td class="tdTitle">
                            <label>판정</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxRadioButtonList ID="rdoJUDGE" ClientInstanceName="rdoJUDGE" runat="server" RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None" >
                                <Items>
                                    <dx:ListEditItem Value="SS0201" Text="합격" Selected="true" />
                                    <dx:ListEditItem Value="SS0202" Text="불합격" />
                                </Items>
                            </dx:ASPxRadioButtonList>
                        </td>
                        <td class="tdTitle">
                            <label>완료</label>
                        </td>
                        <td class="tdContentR">
                            <dx:ASPxCheckBox ID="chk_endyn" ClientInstanceName="chk_endyn" runat="server" Text="완료"  Checked="true"/>
                        </td>
                         <td class="tdContent">
                             <span style="width: 100%">
                            <button class="btn btn-sm btn-warning" style="width: 100px; padding: 3px 0px;" onclick="fn_setting(); return false;">
                                <i class="i i-file-plus "></i>
                                <span class="text">일괄적용</span>
                            </button>
                        </span>
                         </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            <label>온도</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="srcF_TEMP" ClientInstanceName="srcF_TEMP" runat="server" Width="100%">
                                <ValidationSettings RequiredField-IsRequired="true" 
                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">
                            <label>습도</label>
                        </td>
                        <td class="tdContent">
                           <dx:ASPxTextBox ID="srcF_HYGRO" ClientInstanceName="srcF_HYGRO" runat="server" Width="100%">
                                <ValidationSettings RequiredField-IsRequired="true" 
                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">
                            <label>작성자</label>
                        </td>
                        <td class="tdContent">
                           <dx:ASPxTextBox ID="srcF_REGUSER" ClientInstanceName="srcF_REGUSER" runat="server" Width="100%">
                                <ValidationSettings RequiredField-IsRequired="true" 
                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">
                            <label>승인자</label>
                        </td>
                        <td class="tdContentR">
                           <dx:ASPxTextBox ID="srcF_CNFMUSER" ClientInstanceName="srcF_CNFMUSER" runat="server" Width="100%">
                                <ValidationSettings RequiredField-IsRequired="true" 
                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdContent">
                            
                        </td>
                    </tr>
                 </table>
            </div>        
        </div>
        <div class="form-group"></div>
        <%-- 하단 그리드 --%>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_FIXREQNO;F_MS01MID" EnableViewState="false" EnableRowsCache="false"
                OnDataBinding="devGrid_DataBinding" OnCustomCallback="devGrid_CustomCallback" OnDataBound="devGrid_DataBound"
                OnBatchUpdate="devGrid_BatchUpdate" OnCellEditorInitialize="devGrid_CellEditorInitialize">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" AllowFocusedRow="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                         BatchEditStartEditing="fn_OnBatchEditStartEditing" BatchEditEndEditing="fn_OnBatchEditEndEditing" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_FIXREQNO" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MS01MID" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MS01D5ID" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ATTFILENO" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ATTFILECNT" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TEMP" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="F_HYGRO" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_REGUSER_2" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_CNFMUSER" Width="0px" FixedStyle="Left"
                                           HeaderStyle-Border-BorderWidth="0px" HeaderStyle-Paddings-Padding="0"
                                           EditFormCaptionStyle-Border-BorderWidth="0px" EditFormCaptionStyle-Paddings-Padding="0"
                                           CellStyle-BorderBottom-BorderWidth="0px" CellStyle-Paddings-Padding="0" 
                                           EditCellStyle-Border-BorderWidth="0px" EditCellStyle-Paddings-Padding="0">
                        <EditCellStyle CssClass="dispNone"></EditCellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="F_ROWNUM" Caption="NO" Width="40px" FixedStyle="Left" CellStyle-HorizontalAlign="Right" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="80px" FixedStyle="Left" CellStyle-HorizontalAlign="Left" >
                        <PropertiesTextEdit ClientSideEvents-Init="fn_OnControlDisableBox"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="200px" FixedStyle="Left" CellStyle-HorizontalAlign="Left" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PRODNO" Caption="제조번호" Width="140px" FixedStyle="Left" CellStyle-HorizontalAlign="Left" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STAND" Caption="규격" Width="100px" FixedStyle="Left" CellStyle-HorizontalAlign="Left" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PREFIXDT" Caption="이전교정일" Width="80px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_TERMMONTH" Caption="주기" Width="80px" CellStyle-HorizontalAlign="Right">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="F_FIXDT" Caption="*교정일자" Width="80px">
                        <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd"></PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXNO" Caption="교정번호" Width="140px" CellStyle-HorizontalAlign="Left" >
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_JUDGECD" Caption="*판정" Width="80px" CellStyle-HorizontalAlign="Center" ></dx:GridViewDataComboBoxColumn>
                    <dx:GridViewBandColumn  Caption="첨부">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_PRE_ATTFILECNT" Caption="첨부수" Width="50px" CellStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit ClientSideEvents-Init="fn_OnControlDisableBox"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn Caption="파일" Width="100px">
                                <DataItemTemplate>
                                    <div style="width:100%;text-align:center;text-align:left;">
                                        <span style="width:80%">
                                            <button id="bntATTFILE" class="btn btn-sm btn-success" style="width: 100%; padding: 3px 0px;" type="button" onclick="fn_OnAttfileViewClick('<%#Container.VisibleIndex%>');return false;">
                                                <i class="fa fa-save "></i>
                                                <span class="text">파일보기</span>
                                            </button>
                                        </span>
                                    </div>
                                </DataItemTemplate>
                                <EditItemTemplate>
                                    <div style="width:89%;text-align:center;text-align:left;">
                                        <span style="width:100%">
                                            <button id="bntATTFILE" class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" type="button" onclick="fn_OnAttfileClick();;return false;">
                                                <i class="i i-file-plus "></i>
                                                <span class="text">파일첨부</span>
                                            </button>
                                        </span>
                                    </div>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                    </dx:GridViewBandColumn>        
                    <dx:GridViewDataCheckColumn FieldName="F_ENDYN" Caption="완료" Width="50px">
                        <PropertiesCheckEdit AllowGrayed="true" AllowGrayedByClick="false" />
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn FieldName="F_REGUSER" Caption="담당자" Width="80px" CellStyle-HorizontalAlign="Left" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXGRPNM" Caption="교정기관" Width="80px" CellStyle-HorizontalAlign="Left" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <div style="display:none;">
                <dx:ASPxGridView ID="devGridExcel" ClientInstanceName="devGridExcel" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_FIXREQNO;F_MS01MID;" EnableViewState="false" EnableRowsCache="false">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                    <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" AllowFocusedRow="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit"/>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="F_ROWNUM" Caption="NO" Width="40px" FixedStyle="Left">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px" FixedStyle="Left">
                            <PropertiesTextEdit ClientSideEvents-Init="fn_OnControlDisableBox"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="110px" FixedStyle="Left">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_STAND" Caption="규격" Width="100px" FixedStyle="Left">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_PREFIXDT" Caption="이전교정일" Width="80px">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_TERMMONTH" Caption="주기" Width="80px">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="F_FIXDT" Caption="*교정일자" Width="80px">
                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="F_FIXNO" Caption="교정번호" Width="140px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_JUDGENM" Caption="판정명" Width="100px">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_PRE_ATTFILECNT" Caption="첨부수" Width="100px">
                        </dx:GridViewDataTextColumn> 
                        <dx:GridViewDataCheckColumn FieldName="F_ENDYN" Caption="완료" Width="50px">
                            <PropertiesCheckEdit AllowGrayed="true" AllowGrayedByClick="false" />
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="F_REGUSER" Caption="담당자" Width="80px">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_FIXGRPNM" Caption="교정기관" Width="80px">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
        <div class="paging"></div>       
    </div>
    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGridExcel" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
    <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
</asp:Content>