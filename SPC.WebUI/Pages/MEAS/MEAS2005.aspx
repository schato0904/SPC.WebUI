<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS2005.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS2005" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = 'F_REQNO;F_TEAMCD;'

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
            srcF_RECVDT_FROM.SetValue(new Date().First());
            srcF_RECVDT_TO.SetValue(new Date());
            srcF_REQDT_FROM.SetValue(new Date().First());
            srcF_REQDT_TO.SetValue(new Date());
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
            if (srcF_REQDT_FROM.GetValue() == null) {
                alert("검색하고자하는 의뢰일자의 시작날짜을 입력해주세요.");
                srcF_REQDT.txtFROMDT.Focus();
                return false;
            }

            if (srcF_REQDT_TO.GetValue() == null) {
                alert("검색하고자하는 의뢰일자의 마지막날짜를 입력해주세요.");
                srcF_REQDT.txtTODT.Focus();
                return false;
            }

            if (srcF_RECVDT_FROM.GetValue() != null || srcF_RECVDT_TO.GetValue() != null) {
                if (srcF_RECVDT_FROM.GetValue() == null) {
                    alert("검색하고자하는 접수일자의 시작날짜을 입력해주세요.");
                    srcF_RECVDT_FROM.Focus();
                    return false;
                }

                if (srcF_RECVDT_TO.GetValue() == null) {
                    alert("검색하고자하는  접수일자의 마지막날짜를 입력해주세요.");
                    srcF_RECVDT_TO.Focus();
                    return false;
                }
            }

            devGrid1.PerformCallback();
        }

        // 엑셀
        function fn_OnExcelClick() {
            if (devGrid2.GetVisibleRowsOnPage() == 0) {
                alert("엑셀로 변환할 정보가 없습니다.");
                return false;
            }

            btnExport.DoClick();
        }

        // 입력
        function fn_OnNewClick() { }

        // 수정
        function fn_OnModifyClick() { }

        // 저장
        function fn_OnSaveClick() {
            if (fn_OnValidate()) {
                devCallback.PerformCallback("SAVE");
            }
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (fn_OnValidate()) {
                if (!confirm('접수처리된 정보를 삭제하시겠습니까? 삭제시 의뢰상태로 변경됩니다.')) return;

                devCallback.PerformCallback("DEL");
            }
        }

        // 인쇄
        function fn_OnPrintClick() {}

        // 취소
        function fn_OnCancelClick() {            
            
            fn_setdate();
            fn_ClearFields(_fieldList, "src");

            devGrid1.PerformCallback("clear");
            devGrid2.PerformCallback("clear");
        }

        // Validate
        function fn_OnValidate(s, e) {
            var F_MS02MID_LST = devGrid1.GetSelectedKeysOnPage();
            F_MS02MID_LST = F_MS02MID_LST.join(';')

            if (devGrid1.GetVisibleRowsOnPage() == 0) {
                alert("조회된 정보가 없습니다.");
                return false;
            }

            if (F_MS02MID_LST == "") {
                alert("의뢰번호를 선책해주세요.");
                return false;
            }

            hidF_MS02MID_LST.SetText(F_MS02MID_LST);

            return true;
        }

        // Grid End Callback
        function fn_OnGridEndCallback(s, e) {
            if (s.cpResultCode == '0') {
                alert(s.cpResultMsg);
            }

            s.cpResultCode = "";
            s.cpResultMsg = "";
        }

        // 오류시
        function fn_OnGridCallbackError(s, e) {
            alert(e);
        }

        function fn_OnGridCellClick(index, colNm) {            
            if (colNm == "F_MS02MID") {
                return;
            }

            var key = devGrid1.GetRowKey(index);
            if (key == null || key == undefined || key == "") {
                return;
            }

            devGrid2.PerformCallback("VIEW;" + key);
        }

        function devCallback_CallbackError(s, e) {
            alert(e);
        }

        function devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            // 정상일 경우 그리드 재 조회
            if (result.CODE == "1") {
                alert(result.MESSAGE);
                devGrid1.PerformCallback();
            }
        }

        function fn_OnAllCheckedChanged(s, e) {
            devGrid1.SelectAllRowsOnPage(s.GetChecked());
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback" ClientSideEvents-CallbackError="devCallback_CallbackError" ClientSideEvents-CallbackComplete="devCallback_CallbackComplete"></dx:ASPxCallback>
    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid1" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
    <dx:ASPxTextBox ID="hidPageMode" ClientInstanceName="hidPageMode" runat="server" Width="100%" ClientVisible="false" Text="" />
    <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
    <dx:ASPxTextBox ID="hidF_MS02MID_LST" ClientInstanceName="hidF_MS02MID_LST" runat="server" Width="100%" ClientVisible="false" />
    <div class="container">
        <div class="search">            
            <section id="sectionSearch" class="panel panel-default wrapper-sm" style="padding:0;border: 0px none transparent;margin-bottom:0px;">
                <table class="InputTable">
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
                            <label>접수일자</label>
                        </td>
                        <td class="tdContent">
                            <div style="width:50%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_RECVDT_FROM" ClientInstanceName="srcF_RECVDT_FROM" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="width:50%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="srcF_RECVDT_TO" ClientInstanceName="srcF_RECVDT_TO" UseMaskBehavior="true" 
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
                            <dx:ASPxTextBox ID="srcF_REQNO" ClientInstanceName="srcF_REQNO" runat="server" Width="100%"></dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">
                            <label>사용팀</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="srcF_TEAMCD" ClientInstanceName="srcF_TEAMCD" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisable"/>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                 </table>
            </section>

            <div class="form-group"></div>
            <div class="search2">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                        KeyFieldName="F_MS02MID" EnableViewState="false" EnableRowsCache="false"
                        OnHtmlDataCellPrepared="devGrid1_HtmlDataCellPrepared"
                        OnCustomCallback="devGrid_CustomCallback"
                        SettingsDataSecurity-AllowEdit="false">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" ShowStatusBar="Hidden" />
                        <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowFocusedRow="true" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnGridCallbackError" />
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="45px" FixedStyle="Left">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="chkAll" runat="server"
                                        AutoPostBack="false" ClientSideEvents-CheckedChanged="fn_OnAllCheckedChanged"/>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="F_ROWNUM" Caption="NO" Width="50px" FixedStyle="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_REQNO" Caption="의뢰번호" Width="100px" FixedStyle="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_REQDT" Caption="의뢰일자" Width="100px" FixedStyle="Left">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_REQCNT" Caption="의뢰수량" Width="80px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_REGUSER" Caption="의뢰자" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_RECVDT" Caption="접수일자" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_REMARK" Caption="비고" Width="400px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
            </div>

        </div>        
        <div class="form-group"></div>
        <div class="content">                
            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                             KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false"/>
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" CallbackError="fn_OnGridCallbackError"/>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_ROWNUM" Caption="NO" Width="40px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_REQNO" Caption="의뢰번호" Width="100px" FixedStyle="Left">
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="100px" FixedStyle="Left">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_STAND" Caption="규격" Width="110px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_TEAMNM" Caption="사용팀" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_USER" Caption="사용자" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LASTFIXDT" Caption="최종교정일" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_TERMMONTH" Caption="교정주기" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_FIXPLANDT" Caption="교정예정일" Width="100px">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>        
        <div class="paging"></div>        
    </div>
</asp:Content>
