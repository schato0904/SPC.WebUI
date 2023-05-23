<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS2004.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS2004" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList1 = 'F_EQUIPDIVCD;F_EQUIPTYPECD;F_TEAMCD;F_FIXTYPECD;F_FIXGRPCD;F_REQNO';
        var _fieldList2 = 'F_REQNO;F_REQCNT;F_REGUSER;F_REMARK;';

        $(document).ready(function () {
            $.strPad = function (i, l, s) {
                var o = i.toString();
                if (!s) { s = '0'; }
                while (o.length < l) {
                    o = s + o;
                }
                return o;
            };
            fn_setdate();
        });

        function fn_setdate() {
            srcF_REQDT.SetValue(new Date());
            srcF_FIXPLANDT_FROM.SetValue(new Date().First());
            srcF_FIXPLANDT_TO.SetValue(new Date());
            schF_REQDT_FROM.SetValue(new Date().First());
            schF_REQDT_TO.SetValue(new Date());
        }

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(devGrid2.GetMainElement()).offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - pagingHeight - scrollbarWidth(), 10));
            
            devGrid2.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {

            devGrid2.PerformCallback();
        }

        // 신청대상검색
        function fn_OnSearchResultClick() {

            fn_SetPageMode("NEW");
            fn_InputArea_Init();

            devGrid1.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            fn_SetPageMode("NEW");
            fn_InputArea_Init();
            devGrid1.PerformCallback("clear");
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
            if (fn_OnValidate()) {
                devCallback.PerformCallback('SAVE');
            }
        }

        // 취소
        function fn_OnCancelClick() {            
            fn_setdate();

            schF_REQDT.Clear();
            var fromDt = new Date().getFullYear() + '-' + $.strPad(new Date().getMonth() + 1, 2) + '-01';
            var toDt = new Date().getFullYear() + '-' + $.strPad(new Date().getMonth() + 1, 2) + '-' + $.strPad(new Date().getDate(), 2);
            var value = { F_FROMDT: fromDt, F_TODT: toDt };
            schF_REQDT.fn_SetUCControl(value);

            fn_ClearFields(_fieldList1, "sch");
            
            fn_SetPageMode("CANCEL");
            fn_InputArea_Init();
            fn_OnValidate();

            devGrid1.PerformCallback("clear");
            devGrid2.PerformCallback("clear");
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (srcF_REQNO.GetText() == "") {
                alert("삭제할 대상 정보가 없습니다. 조회후 다시 시도해주세요.");
                return false;
            }

            var endcnt = srcF_ENDCNT.GetText();
            var remncnt = srcF_REMNCNT.GetText();

            // 실적등록할 잔여건이 있을 때
            if (endcnt != '' && endcnt != '0' && remncnt != '' && remncnt != '0') {
                if (!confirm("신청 건 중 실적등록이 완료되지 않은 잔여 건이 삭제됩니다.\n\r삭제하시겠습니까?")) {
                    return false;
                }
            } else {
                if (endcnt != '' && endcnt != '0' && (remncnt == '' || remncnt == '0')) {
                    alert("실적등록 완료된 신청건은 삭제할 수 없습니다.");
                    return false;
                }

                if (!confirm("삭제하시겠습니까? 삭제된 데이터는 복원할 수 없습니다.")) {
                    return false;
                }
            }

            devCallback.PerformCallback('DEL');
        }

        // 인쇄
        function fn_OnPrintClick() {}

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        // 신청대상 엑셀
        function fn_OnExcelResultClick() {
            //var key = srcF_REQNO.GetText();
            //var pageMode = hidPageMode.GetText();
            //if (pageMode == "EIDT" || key == "") {
            //    alert("조회 후 다시 시도해주세요.");
            //    return;
            //}
            var devGrid1 = ASPxClientGridView.Cast('devGrid1');
            if (devGrid1.GetVisibleRowsOnPage() == 0) {
                alert('데이터가 없습니다.');
                return false;
            }

            btnExportResult.DoClick();
        }

        // 저장, 삭제 후 처리
        function devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            // 정상일 경우 그리드 재 조회
            if (result.CODE == "1") {
                switch (result.TYPE) {
                    case "AfterSave":                        
                        cbpContent.PerformCallback(result.PKEY);
                        devGrid1.PerformCallback("VIEW;" + result.PKEY);
                        devGrid2.PerformCallback();
                        break;
                    case "AfterDelete":
                        // 잔여항목 삭제인 경우, 재조회 처리
                        if (typeof (result.KEY) != 'undefined' && result.KEY != '') {
                            var key = result.KEY;
                            cbpContent.PerformCallback(key);
                            devGrid1.PerformCallback("VIEW;" + key);
                        } else {
                            // 입력화면 클리어
                            fn_OnNewClick();
                        }
                        // 그리드 조회
                        devGrid2.PerformCallback();
                        break;
                    case "AfterView":
                        fn_SetPageMode("EDIT");
                        return;
                    default: break;
                }
            }

            alert(result.MESSAGE);
        }

        // 오류시
        function devCallback_CallbackError(s, e) {
            alert(e);
        }

        // Grid End Callback
        function fn_OnGridEndCallback(s, e) {            
            if (s.cpResultCode == '0') {
                alert(s.cpResultMsg);                
            }

            if (s.cpResultCode == '2') {
                fn_SetPageMode("EDIT");
            }

            s.cpResultCode = "";
            s.cpResultMsg = "";
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // 그리드 더블클릭
        function fn_devGridOnRowDblClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            if (key == null || key == undefined || key == "") {
                return;
            }
            
            cbpContent.PerformCallback(key);
            devGrid1.PerformCallback("VIEW;" + key);
        }

        // Validate
        function fn_OnValidate(s, e) {
            var pageMode = hidPageMode.GetText();

            if (pageMode == 'EDIT' || pageMode == 'NEW') {
                var F_MS01MID_LST = devGrid1.GetSelectedKeysOnPage();
                F_MS01MID_LST = F_MS01MID_LST.join(';')

                if (devGrid1.GetVisibleRowsOnPage() == 0) {
                    alert("신청대상 검색후 측정기 정보를 선택해주세요.");
                    return false;
                }

                if (srcF_REQCNT.GetText() == "" || F_MS01MID_LST == "") {
                    alert("선택된 측정기가 없습니다. 측정기 정보를 선택해주세요.");
                    return false;
                }

                if (srcF_REQDT.GetText() == null) {
                    alert("의뢰일자를 입력해주세요.");
                    srcF_REQDT.Focus();
                    return false;
                }

                if (srcF_REGUSER.GetText() == "") {
                    alert("교정 신청할 작성자를 입력해주세요.");
                    srcF_REGUSER.Focus();
                    return false;
                }

                if (pageMode == 'EDIT') {
                    if (srcF_REQNO.GetText() == "") {
                        alert("수정할 대상 정보가 없습니다. 조회후 다시 시도해주세요.");
                        return false;
                    }

                    if (!confirm('데이터를 수정하시겠습니까?')) {
                        return false;
                    }
                }

                hidF_MS01MID_LST.SetText(F_MS01MID_LST);
            }
            else {
                return ASPxClientEdit.ValidateEditorsInContainerById('cbpContent');
            }

            return true;
        }

        function fn_InputArea_Init() {
            fn_ClearFields(_fieldList2, "src");
            var dt = new Date().getFullYear() + '-' + $.strPad(new Date().getMonth() + 1, 2) + '-' + $.strPad(new Date().getDate(), 2);
            var value = { F_FROMDT: dt, F_TODT: dt };
            //srcF_REQDT.SetValue(value);
            srcF_RECVYNNM.SetText('');

            // 로그인 사용자 정보 조회
            fn_GetLoginUserInfo("USERNM", function (result) {
                srcF_REGUSER.SetText(result.USERNM);
            });
        }

        // 화면 로드시 필수 필드 표시
        function cbpContent_Init(s, e) {
            fn_InputArea_Init();
            return ASPxClientEdit.ValidateEditorsInContainerById('cbpContent');
        }

        // cbpContent End Callback
        function cbpContent_EndCallback(s, e) {
            fn_SetPageMode("EDIT");
        }

        // cbpContent Callback Error
        function cbpContent_CallbackError(s, e) {
            alert(e.message);
        }

        // 페이지 모드 체인지 : NEW, EDIT, REV
        function fn_SetPageMode(mode) {
            hidPageMode.SetText(mode);
        }

        function fn_OnAllCheckedInit(s, e) {
            if (devGrid1.GetSelectedKeysOnPage().length == devGrid1.GetVisibleRowsOnPage()) {
               devGrid1.SelectAllRowsOnPage(s.GetChecked());
            }
        }

        function fn_OnAllCheckedChanged(s, e) {
            devGrid1.SelectAllRowsOnPage(s.GetChecked());
        }

        function fn_OnSelectionChanged(s, e) {
            srcF_REQCNT.SetText(devGrid1.GetSelectedKeysOnPage().length);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
    <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
    <dx:ASPxButton ID="btnExportResult" ClientInstanceName="btnExportResult" runat="server" ClientVisible="false" OnClick="btnExportResult_Click" CausesValidation="false" />
    <dx:ASPxTextBox ID="hidF_MS01MID_LST" ClientInstanceName="hidF_MS01MID_LST" runat="server" Width="100%" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback" ClientSideEvents-CallbackError="devCallback_CallbackError" ClientSideEvents-CallbackComplete="devCallback_CallbackComplete" />
            <section id="sectionSearch" class="panel panel-default wrapper-sm" style="padding:0;border: 0px none transparent;margin-bottom:0px;">
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
                            <label>교정예정일</label>
                        </td>
                        <td class="tdContent">
                            <div style="width:50%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_FIXPLANDT_FROM" ClientInstanceName="srcF_FIXPLANDT_FROM" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="width:50%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_FIXPLANDT_TO" ClientInstanceName="srcF_FIXPLANDT_TO" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="clear:both;"></div>
                        </td>
                        <td class="tdTitle">
                            <label>측정기분류</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_EQUIPDIVCD" ClientInstanceName="schF_EQUIPDIVCD" Width="100%"></dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">
                            <label>측정기구분</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_EQUIPTYPECD" ClientInstanceName="schF_EQUIPTYPECD" Width="100%"></dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="tdTitle">
                            <label>공장</label>
                        </td>
                        <td class="tdContent">
                            <ucCTF:Fact ID="ucFact" IsRequired="true" runat="server" targetCtrls="schF_TEAMCD" />
                        </td>
                        <td class="tdTitle">
                            <label>사용팀</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_TEAMCD" ClientInstanceName="schF_TEAMCD" Width="100%">
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">
                            <label>사용반</label>
                        </td>
                        <td class="tdContent">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            <label>교정구분</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_FIXTYPECD" ClientInstanceName="schF_FIXTYPECD" Width="100%"></dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">
                            <label>교정기관</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_FIXGRPCD" ClientInstanceName="schF_FIXGRPCD" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisable"/>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdContent" colspan="2" style="text-align:right;">
                            <button class="btn btn-sm btn-success" onclick="fn_OnSearchResultClick(); return false;">
                                <i class="fa fa-search"></i>
                                <span class="text">신청대상검색</span>
                            </button>
                        </td>
                    </tr>
                 </table>
            </section>

            <div class="form-group"></div>
            <div class="search2">
                <div class="form-group">
                    <label class="col-sm-12 h5 font-bold bg-primary" style="padding:5px;">■ 교정 신청내역</label>
                </div>
                <section id="sectionContent" class="panel panel-default wrapper-sm" style="padding:0px;border: 0px none transparent;margin-bottom:0px;">
                    <dx:ASPxCallbackPanel ID="cbpContent" ClientInstanceName="cbpContent" runat="server" CssClass="form-horizontal" OnCallback="cbpContent_Callback" >
                        <ClientSideEvents />
                        <PanelCollection>
                            <dx:HtmlEditorRoundPanelContent>            
                                <table class="InputTable" style="margin-bottom:-1px;">
                                    <colgroup>
                                        <col style="width:8%" />
                                        <col style="width:17%" />
                                        <col style="width:8%" />
                                        <col style="width:17%" />
                                        <col style="width:8%" />
                                        <col style="width:17%" />
                                        <col style="width:8%" />
                                        <col style="width:17%" />
                                    </colgroup>
                                    <tr>
                                        <td class="tdTitle">
                                            <label>의뢰번호</label>
                                        </td>
                                        <td class="tdContent">
                                            <dx:ASPxTextBox ID="srcF_REQNO" ClientInstanceName="srcF_REQNO" runat="server" ReadOnly="true" BackColor="WhiteSmoke">
                                                <ValidationSettings RequiredField-IsRequired="true" 
                                                    ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="tdTitle">
                                            <label>의뢰일자</label>
                                        </td>
                                        <td class="tdContent">
                                            <dx:ASPxDateEdit runat="server" ID="srcF_REQDT" ClientInstanceName="srcF_REQDT" UseMaskBehavior="true" 
                                                EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                                Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td class="tdTitle">
                                            <label>의뢰수량</label>
                                        </td>
                                        <td class="tdContent">
                                            <dx:ASPxTextBox ID="srcF_REQCNT" ClientInstanceName="srcF_REQCNT" runat="server" ReadOnly="true" BackColor="WhiteSmoke">
                                                <ValidationSettings RequiredField-IsRequired="true" 
                                                    ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="srcF_ENDCNT" ClientInstanceName="srcF_ENDCNT" runat="server" ReadOnly="true" ClientVisible="false">
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="srcF_REMNCNT" ClientInstanceName="srcF_REMNCNT" runat="server" ReadOnly="true" ClientVisible="false">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="tdTitle">
                                            <label>작성자</label>
                                        </td>
                                        <td class="tdContent">
                                            <dx:ASPxTextBox ID="srcF_REGUSER" ClientInstanceName="srcF_REGUSER" runat="server">
                                                <ValidationSettings RequiredField-IsRequired="true" 
                                                    ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdTitle">
                                            <label>비고</label>
                                        </td>
                                        <td class="tdContent" colspan="3">
                                            <dx:ASPxTextBox ID="srcF_REMARK" ClientInstanceName="srcF_REMARK" runat="server" Width="100%"></dx:ASPxTextBox>
                                        </td>
                                        <td class="tdTitle">
                                            <label>접수상태</label>
                                        </td>
                                        <td class="tdContent">
                                            <dx:ASPxTextBox ID="srcF_RECVYNNM" ClientInstanceName="srcF_RECVYNNM" runat="server" Width="100%">
                                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="tdContent" colspan="2" style="text-align:right;">
                                            <button class="btn btn-sm btn-success" onclick="fn_OnExcelResultClick(); return false;">
                                                <i class="i i-file-excel"></i>
                                                <span class="text">신청대상 엑셀변환</span>
                                            </button>
                                        </td>
                                    </tr>
                                </table>
                            </dx:HtmlEditorRoundPanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>            
                    <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                                     KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback"
                                     OnAfterPerformCallback="devGrid1_AfterPerformCallback">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="250" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                        <SettingsBehavior AllowSort="false" AllowDragDrop="false"/>
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                                          SelectionChanged="fn_OnSelectionChanged"/>
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px" FixedStyle="Left">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="chkAll" runat="server"
                                        AutoPostBack="false" ClientSideEvents-CheckedChanged="fn_OnAllCheckedChanged"/>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="F_ROWNUM" Caption="NO" Width="40px" FixedStyle="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MS01MID" Caption="계측기ID" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_REQNO" Caption="의뢰번호" Width="100px" FixedStyle="Left"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px" FixedStyle="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="100px" FixedStyle="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_PRODNO" Caption="제조번호" Width="100px" FixedStyle="Left" CellStyle-HorizontalAlign="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_STAND" Caption="규격" Width="110px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MODEL" Caption="모델" Width="110px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_UNQNO" Caption="고유번호" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_FIXTYPENM" Caption="교정구분" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_TEAMNM" Caption="사용팀" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_TERMMONTH" Caption="주기" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_LASTFIXDT" Caption="최종교정일" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_FIXPLANDT" Caption="교정예정일" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_FIXGRPNM" Caption="교정기관" Width="100px">
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn FieldName="" Caption="교정일자(XXXX-XX-XX)" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="" Caption="교정번호" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="" Caption="판정(합격,불합격)" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="" Caption="완료(완료,미완료)" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="" Caption="온도" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="" Caption="습도" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="" Caption="작성자" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="" Caption="승인자" Width="0px" HeaderStyle-Border-BorderWidth="0px" CellStyle-Paddings-Padding="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MS02MID" Visible="false"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_SEQNO" Visible="false"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_MS01MID" Visible="false"></dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            </div>
        </div>        
        
        <div class="divline">&nbsp;</div>
        <%-- 하단 그리드 --%>
        <div class="content">
            <section id="searchCondition" class="panel panel-default wrapper-sm" style="padding:0;border: 0px none transparent;margin-bottom:5px;">
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
                            <label>의뢰일자</label>
                        </td>
                        <td class="tdContent">
                            <div style="width:50%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_REQDT_FROM" ClientInstanceName="schF_REQDT_FROM" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="width:50%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_REQDT_TO" ClientInstanceName="schF_REQDT_TO" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="clear:both;"></div>
                        </td>
                        <td class="tdTitle">
                            <label>의뢰번호</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_REQNO" ClientInstanceName="schF_REQNO" runat="server" Width="100%"></dx:ASPxTextBox>
                        </td>
                        <td class="tdContent" colspan="2" style="text-align:right;">
                            <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick(); return false;">
                               <i class="fa fa-search"></i>
                               <span class="text">조회</span>
                           </button>
                        </td>
                    </tr>
                </table>
            </section>
            <section class="panel panel-default" style="height: 100%;">
                <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" Width="100%" ClientVisible="false" Text="FIRST" />
                <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_REQNO" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback"
                    SettingsDataSecurity-AllowEdit="false">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                    <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridOnRowDblClick" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="F_ROWNUM" Caption="NO" Width="40px" FixedStyle="Left">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_REQNO" Caption="의뢰번호" Width="100px" FixedStyle="Left">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_REQDT" Caption="의뢰일자" Width="100px" FixedStyle="Left">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_REQCNT" Caption="의뢰수량" Width="80px">
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_ENDCNT" Caption="교정수량" Width="80px">
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_REMNCNT" Caption="잔여수량" Width="80px">
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_REGUSER" Caption="작성자" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_REMARK" Caption="비고" Width="400px">
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </section>
        </div>
        <div class="paging"></div>        
    </div>
</asp:Content>
