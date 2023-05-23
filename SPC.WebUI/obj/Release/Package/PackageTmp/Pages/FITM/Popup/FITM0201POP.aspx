<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="FITM0201POP.aspx.cs" Inherits="SPC.WebUI.Pages.FITM.Popup.FITM0201POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            var oSetParam = "<%=oSetParam%>";
            if (oSetParam != '') {
                fn_SetTextValue('hidKey', oSetParam);
            }
            devGrid.PerformCallback();
            
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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        function fn_OnRowDblClick(s, e) {

            var visibleIndex = e.visibleIndex
            if (visibleIndex < 0) return;

            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(e.visibleIndex));

            var oParams = "";

            oParams += dateAdd("<%=oSetParam.Split('|')[8]%>", -10, "d") + '|'
            oParams += "<%=oSetParam.Split('|')[8]%>" + '|'            
            oParams += rowKeys; //F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN
            oParams += '|||' + fn_GetCastText('hidCOMPCD') + '|' + fn_GetCastText('hidFACTCD');
            parent.parent.fn_OnDeleteTab('ANLS0101', parent.parent.fn_OnGetTabObject('ANLS0101'));
            parent.parent.parent.doCreateTab('MM03|MM0301|ANLS|ANLS0101|품질종합현황|RP|1', oParams);

        }

        function dateAdd(sDate, v, t) {
            var yy = parseInt(sDate.substr(0, 4), 10);
            var mm = parseInt(sDate.substr(5, 2), 10);
            var dd = parseInt(sDate.substr(8), 10);

            if (t == "d") {
                d = new Date(yy, mm - 1, dd + v);
            } else if (t == "m") {
                d = new Date(yy, mm - 1 + v, dd);
            } else if (t == "y") {
                d = new Date(yy + v, mm - 1, dd);
            } else {
                d = new Date(yy, mm - 1, dd + v);
            }

            yy = d.getFullYear();
            mm = d.getMonth() + 1; mm = (mm < 10) ? '0' + mm : mm;
            dd = d.getDate(); dd = (dd < 10) ? '0' + dd : dd;

            return '' + yy + '-' + mm + '-' + dd;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-3 control-label">일자 : <%=oSetParam.Split('|')[8]%></label>
                    <label class="col-sm-4 control-label">품목 : <%=oSetParam.Split('|')[7]%></label>
                    <label class="col-sm-4 control-label">공정 : <%=oSetParam.Split('|')[5]%></label>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidKey" ClientInstanceName="hidKey" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                SettingsBehavior-AllowSelectSingleRowOnly="true" SettingsBehavior-AllowSelectByRowClick="true" SettingsBehavior-AllowSort="true"
                KeyFieldName="F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" 
                OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                >
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_SIRYO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_FREEPOINT" Visible="false" />

                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="작업시간" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자" Width="120px" />
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="200px" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewBandColumn Caption="검사기준">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="검사규격" Width="80px" CellStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격" Width="80px" CellStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격" Width="80px" CellStyle-HorizontalAlign="Right" />
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO" Caption="시료군" Width="60px" />
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번" Width="40px" />
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값" Width="80px" CellStyle-HorizontalAlign="Right" />
                    <dx:GridViewDataColumn FieldName="F_FOURNMKR" Caption="초품사유" Width="150px" />

                    
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
