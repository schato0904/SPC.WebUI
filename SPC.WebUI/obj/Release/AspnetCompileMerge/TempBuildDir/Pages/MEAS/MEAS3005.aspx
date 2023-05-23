<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS3005.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS3005" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = "";
        var key = "F_EQUIPNO;F_EQUIPNM;F_PRODNO;F_TEAMNM;F_USER;F_LASTFIXDT;F_STAND;F_INDT;F_MAKERNM;F_TERMMONTH";
        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height - 10);
            devGrid2.SetHeight((height- 250)/2);
            devGrid3.SetHeight((height - 250) / 2);
            devGrid4.SetHeight(220);
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
            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            devCallback.PerformCallback('DELETE');
        }

        // 인쇄
        function fn_OnPrintClick() {

            var KEYS = '';

            KEYS += encodeURIComponent(srcF_EQUIPNM.GetText()) + '|'; // 설비명
            KEYS += srcF_STAND.GetText() + '|';   // 규격
            KEYS += srcF_EQUIPNO.GetText() + '|';   // 관리번호
            KEYS += srcF_MAKERNM.GetText() + '|';   // 제조회사
            KEYS += srcF_GRADENM.GetText() + '|';   // 측정단위
            KEYS += srcF_INDT.GetText() + '|';   // 도입일자
            KEYS += srcF_PRICE.GetText() + '|';   // 구입가격
            KEYS += srcF_FIXTYPENM.GetText() + '|';   // 교정구분
            KEYS += srcF_TERMMONTH.GetText() + '|';   // 교정주기

            if (KEYS == '' || KEYS == null) {
                alert("측정기를 선택 후 인쇄하세요.");
            }
            else {
                fn_OnPopupInspectionReport(KEYS);
            }
        }

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

            if (s.cpResultSB != '') {
                fn_OnSetSearchResult(s.cpResultSB)
            }
        }

        function fn_OnSetSearchResult(result) {
            var resultValues = result.split('|');

            srcF_EQUIPNO.SetText(resultValues[0]);
            srcF_EQUIPNM.SetText(resultValues[1]);
            srcF_PRODNO.SetText(resultValues[2]);
            srcF_STAND.SetText(resultValues[3]);
            srcF_INDT.SetText(resultValues[4]);
            srcF_MAKERNM.SetText(resultValues[6]);
            srcF_GRADENM.SetText(resultValues[8]);
            srcF_TEAMNM.SetText(resultValues[10]);
            srcF_USER.SetText(resultValues[11]);
            srcF_TERMMONTH.SetText(resultValues[12]);
            srcF_LASTFIXDT.SetText(resultValues[13]);
            srcF_FIXTYPENM.SetText(resultValues[16]);
            srcF_MS01MID.SetText(resultValues[17]);
            srcF_PRICE.SetText(resultValues[18]);
        }

        function fn_CallbackComplete(s, e) {
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            //alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            return ASPxClientEdit.ValidateEditorsInContainerById('cbpContent');
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
        }

        function devGrid_RowDblClick(s, e) {
        }

        function fn_shcPopupSearch() {
            fn_OnPopupMEAS3001POPSearch('ShcSetItem', shcF_EQUIPNO.GetText());
        }

        function ShcSetItem(resultvalue) {
            shcF_EQUIPNO.SetText(resultvalue.F_EQUIPNO);
            shcF_EQUIPNM.SetText(resultvalue.F_EQUIPNM);
        }

        function fn_devGridOnRowDblClick(s, e) {
            key = s.GetRowKey(e.visibleIndex)

            devGrid2.PerformCallback(key);
            devGrid3.PerformCallback(key);
            devGrid4.PerformCallback(key);
        }

        function fn_OnEquipnoLostFocus(s, e) {
            var F_EQUIPNO = ASPxClientControl.Cast("shcF_EQUIPNO");
            var F_EQUIPNM = ASPxClientControl.Cast("shcF_EQUIPNM");

            var EQUIPCallback = ASPxClientControl.Cast("EQUIPCallback");
            if (!shcF_EQUIPNO.GetText() || shcF_EQUIPNO.GetText() == '') {
                fn_SetTextValue(F_EQUIPNM, '');
                return;
            } else
                EQUIPCallback.PerformCallback();
        }

        function fn_OnEQUIPEndCallback(s, e) {
            var F_EQUIPNO = ASPxClientControl.Cast("shcF_EQUIPNO");
            var F_EQUIPNM = ASPxClientControl.Cast("shcF_EQUIPNM");
            var EQUIPNO = s.cpEQUIPNO;
            var EQUIPNM = s.cpEQUIPNM;

            if (EQUIPNO != '' && EQUIPNM != '') {
                fn_SetTextValue(F_EQUIPNO, EQUIPNO);
                fn_SetTextValue(F_EQUIPNM, EQUIPNM);
            } else {
                fn_SetTextValue(F_EQUIPNO, '');
                fn_SetTextValue(F_EQUIPNM, '');
            }
        };

        function fn_OnAttfileClick(ctrl) {
            var val = $(ctrl).attr("ATTFILENO");
            hidF_ATTFILENO.SetText(val);
            fn_AttachFileOpenReadOnly('자료파일다운로드', 'M', 'T', 'hidF_ATTFILENO');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxCallback runat="server" ID="EQUIPCallback" ClientInstanceName="EQUIPCallback" OnCallback="EQUIPCallback_Callback">
                <ClientSideEvents EndCallback="fn_OnEQUIPEndCallback" CallbackError="fn_OnCallbackError" />
            </dx:ASPxCallback>
            <table class="InputTable" style="margin-bottom: 10px;">
                <colgroup>
                    <col style="width: 10%" />
                    <col style="width: 23%" />
                    <col style="width: 10%" />
                    <col style="width: 23%" />
                    <col style="width: 10%" />
                    <col style="width: 24%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>관리번호</label>
                    </td>
                    <td class="tdContent">
                        <div class="control-label" style="float: left; width: 34%; padding-top: 0;">
                            <dx:ASPxTextBox ID="shcF_EQUIPNO" ClientInstanceName="shcF_EQUIPNO" runat="server" Width="100%">
                                <ClientSideEvents LostFocus="fn_OnEquipnoLostFocus" />
                            </dx:ASPxTextBox>
                        </div>
                        <div class="control-label" style="float: left; width: 54%; margin-left: 2px; padding-top: 0;">
                            <dx:ASPxTextBox ID="shcF_EQUIPNM" ClientInstanceName="shcF_EQUIPNM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                        </div>
                        <div class="control-label" style="float: left; width: 10%; text-align: left; padding-left: 2px; padding-top: 0;">
                            <button class="btn btn-default btn-xs" data-toggle="button" title="관리번호조회" onclick="fn_shcPopupSearch(); return false;">
                                <i class="i i-popup text"></i>
                                <i class="i i-popup text-active text-danger"></i>
                            </button>
                        </div>
                    </td>
                    <td class="tdTitle">
                        <label>측정기분류</label>
                    </td>
                    <td class="tdContent">
                        <dx:ASPxComboBox ID="shcF_EQUIPDIVCD" ClientInstanceName="shcF_EQUIPDIVCD" runat="server" ValueType="System.String" Width="100%"></dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">교정구분</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox ID="shcF_FIXTYPECD" ClientInstanceName="shcF_FIXTYPECD" runat="server" ValueType="System.String" Width="100%"></dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>

        <div class="content">
            <table border="0" >
                <tr>
                    <td style="width:350px">                        
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidF_ATTFILENO" ClientInstanceName="hidF_ATTFILENO" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_MS01MID" ClientInstanceName="srcF_MS01MID" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_PRICE" ClientInstanceName="srcF_PRICE" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="330px"
                            KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridOnRowDblClick" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="80px" />
                                <dx:GridViewDataColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="230px" CellStyle-HorizontalAlign="Left" />

                                <dx:GridViewDataColumn FieldName="F_MS01MID" Visible="false"/>
                            </Columns>
                        </dx:ASPxGridView>
                    </td>
                    <td>
                        <table >
                            <tr>
                                <td>
                                    <table style="margin-bottom:5px">
                                        <tr>
                                            <td>
                                                <table class="InputTable" style="height:200px;">
                                                    <colgroup>
                                                        <col style="width:9%" /><col style="width:20%" /><col style="width:9%" /><col style="width:20%" />
                                                    </colgroup>
                                                    <tr>
                                                        <td class="tdTitle">
                                                            <label>관리번호</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_EQUIPNO" ClientInstanceName="srcF_EQUIPNO" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>                        
                                                        <td class="tdTitle">
                                                            <label>측정기명</label>
                                                        </td>
                                                        <td class="tdContent" >
                                                             <dx:ASPxTextBox ID="srcF_EQUIPNM" ClientInstanceName="srcF_EQUIPNM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>                    
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdTitle">
                                                            <label>제조번호</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_PRODNO" ClientInstanceName="srcF_PRODNO" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>                        
                                                        <td class="tdTitle">
                                                            <label>사용팀</label>
                                                        </td>
                                                        <td class="tdContent" >
                                                            <dx:ASPxTextBox ID="srcF_TEAMNM" ClientInstanceName="srcF_TEAMNM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>                     
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdTitle">
                                                            <label>규격</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_STAND" ClientInstanceName="srcF_STAND" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>                        
                                                        <td class="tdTitle">
                                                            <label>측정단위</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_GRADENM" ClientInstanceName="srcF_GRADENM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdTitle">
                                                            <label>도입일자</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_INDT" ClientInstanceName="srcF_INDT" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>                        
                                                        <td class="tdTitle">
                                                            <label>교정구분</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_FIXTYPENM" ClientInstanceName="srcF_FIXTYPENM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdTitle">
                                                            <label>제조회사</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_MAKERNM" ClientInstanceName="srcF_MAKERNM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>                        
                                                        <td class="tdTitle">
                                                            <label>사용자</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_USER" ClientInstanceName="srcF_USER" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdTitle">
                                                            <label>교정주기</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_TERMMONTH" ClientInstanceName="srcF_TERMMONTH" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>                        
                                                        <td class="tdTitle">
                                                            <label>최종교정일</label>
                                                        </td>
                                                        <td class="tdContent">
                                                            <dx:ASPxTextBox ID="srcF_LASTFIXDT" ClientInstanceName="srcF_LASTFIXDT" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:220px;padding-left:5px">
                                                <dx:ASPxGridView ID="devGrid4" ClientInstanceName="devGrid4" runat="server" AutoGenerateColumns="false" Width="200px"
                                                    KeyFieldName="F_PARTNM" EnableViewState="false" EnableRowsCache="false"
                                                    OnCustomCallback="devGrid4_CustomCallback">
                                                    <Styles>
                                                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                                                        <EditForm CssClass="bg-default"></EditForm>
                                                    </Styles>
                                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                                                    <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridOnRowDblClick" />
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="F_PARTNM" Caption="부속품명칭" Width="120px" />
                                                        <dx:GridViewDataColumn FieldName="F_CNT" Caption="수량" Width="60px" CellStyle-HorizontalAlign="Left" />
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom:5px;">
                                    <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid2_CustomCallback">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                                        <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" CallbackError="fn_OnCallbackError" />
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_LASTFIXDT" Caption="교정일자" Width="100px" />                                            
                                            <dx:GridViewDataColumn FieldName="F_FIXNO" Caption="교정번호" Width="120px" />
                                            <dx:GridViewDataColumn FieldName="F_FIXGRPNM" Caption="교정기관" Width="100px" />
                                            <dx:GridViewDataColumn FieldName="F_JUDGENM" Caption="판정" Width="100px" />
                                            <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고" Width="200px" />
                                            <dx:GridViewDataColumn FieldName="F_ATTFILENO" Caption="첨부파일" Width="100px" Visible="true">
                                                <DataItemTemplate>                                
                                                    <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" ATTFILENO='<%# Eval("F_ATTFILENO") %>' onclick="fn_OnAttfileClick(this); return false;" >
                                                        <i class="i i-file-plus "></i>
                                                        <span class="text">첨부파일</span>
                                                    </button>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom:5px;">
                                    <dx:ASPxGridView ID="devGrid3" ClientInstanceName="devGrid3" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_MS01D3ID" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid3_CustomCallback">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                                        <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" CallbackError="fn_OnCallbackError" />
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_ONDT" Caption="일자" Width="100px" />
                                            <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="구분" Width="100px" />
                                            <dx:GridViewDataColumn FieldName="F_CUSTNM" Caption="거래처" Width="80px" />
                                            <dx:GridViewDataColumn FieldName="F_USER" Caption="담당자" Width="100px" />
                                            <dx:GridViewDataColumn FieldName="F_RETNPLANDT" Caption="반납예정일" Width="100px" />
                                            <dx:GridViewDataColumn FieldName="F_RETNDT" Caption="반납일자" Width="80px" />
                                            <dx:GridViewDataColumn FieldName="F_CONTENTS" Caption="비고" Width="200px" />
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            

            <%--<div class="form-horizontal">
                
            </div>--%>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
