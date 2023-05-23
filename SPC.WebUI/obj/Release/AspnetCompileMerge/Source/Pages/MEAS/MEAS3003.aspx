<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS3003.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS3003" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = "";

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            fn_OnValidate();
            devGrid.SetHeight(height-20);
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            fn_OnSetClear();
            fn_OnValidate();
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
            if (srcF_MS01MID.GetText() == "")
                return;

            if (!fn_OnValidate()) { return; }

            devCallback.PerformCallback('UPDATE');
        }

        // 취소
        function fn_OnCancelClick() {
            fn_OnSetClear();
        }

        // 삭제
        function fn_OnDeleteClick() {
            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            devCallback.PerformCallback('DELETE');
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
                    alert(s.cpResultMsg);
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
        }

        function fn_OnSetSearchResult(result) {
        }

        function fn_OnSetClear() {
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

        function fn_OnEquipnoLostFocus(s,e) {
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxCallback runat="server" ID="EQUIPCallback" ClientInstanceName="EQUIPCallback" OnCallback="EQUIPCallback_Callback">
                <ClientSideEvents EndCallback="fn_OnEQUIPEndCallback" CallbackError="fn_OnCallbackError" />
            </dx:ASPxCallback>

            <section id="searchCondition" class="panel panel-default" style="padding:0px;margin-bottom: 5px; border: 0px none transparent;">
            <div class="form-horizontal">
                <table class="InputTable">
                    <colgroup>
                        <col style="width:9%" /><col style="width:22%" /><col style="width:9%" /><col style="width:22%" /><col style="width:9%" /><col style="width:22%" /><col style="width:7%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">
                            <label>반납예정일</label>
                        </td>
                        <td class="tdContent">
                            <ucCTF:Date ID="ucDate" runat="server" />
                        </td>                        
                        <td class="tdTitle">
                            <label>사용자</label>
                        </td>
                        <td class="tdContentR" style="border-right-width:0px;">
                            <dx:ASPxTextBox ID="shcF_USER" ClientInstanceName="shcF_USER" runat="server" Width="100%" >
                            </dx:ASPxTextBox>                                                            
                        </td>
                        <td class="tdContentR"></td>
                        <td class="tdContent"></td>
                    </tr>
                </table>
            </div>
            </section>
        </div>
        <div class="content">
            <section class="panel panel-default" >
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MS01D4ID" EnableViewState="false" EnableRowsCache="false"
                     OnCustomCallback="devGrid_CustomCallback">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowSelectByRowClick="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_USER" Caption="사용자"  Width="100px" CellStyle-HorizontalAlign="Left" />                        
                        <dx:GridViewDataColumn FieldName="F_ONDT" Caption="발생일자"  Width="90px" />                        
                        <dx:GridViewDataColumn FieldName="F_EQUIPNO" Caption="관리번호"  Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_EQUIPNM" Caption="측정기명"  Width="200px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_RETNPLANDT" Caption="반납예정일"  Width="90px" />
                        <dx:GridViewDataColumn FieldName="F_RETNDT" Caption="반납일"  Width="90px" />                        
                        <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="사용구분"  Width="90px" />                        
                        <dx:GridViewDataColumn FieldName="F_ABNORMALNM" Caption="이상처리구분"  Width="90px" />                       
                        <dx:GridViewDataColumn FieldName="F_TEAMNM" Caption="사용팀"  Width="90px" CellStyle-HorizontalAlign="Left" />                        
                        <dx:GridViewDataColumn FieldName="F_CONTENTS" Caption="비고"  Width="100%" CellStyle-HorizontalAlign="Left"  >
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn FieldName="F_MS01D4ID" Visible="false" />
                    </Columns>
                </dx:ASPxGridView>
            </section>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
