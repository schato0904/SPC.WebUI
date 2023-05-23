<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="CONTROLPOP.aspx.cs" Inherits="SPC.WebUI.Pages.Common.Popup.CONTROLPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_OnSearchClick();
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
            var parentParams = ''
            <%if (!gsVENDOR) {%>
            var parentCompCD = parent.fn_OnGetCompCD();
            parentParams += (parentParams == '') ? parentCompCD : '|' + parentCompCD;
            var parentFactCD = parent.fn_OnGetFactCD();
            parentParams += (parentParams == '') ? parentFactCD : '|' + parentFactCD;
            <%}%>

            devGrid.PerformCallback(parentParams);
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목코드</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">검사항목</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtMEAINSPCD" ClientInstanceName="txtMEAINSPCD" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="txtINSPDETAIL" ClientInstanceName="txtINSPDETAIL" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MODIFYNO" EnableViewState="false" EnableRowsCache="false" PreviewFieldName="F_CONTENTS"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" ShowPreview="True" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="120px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="120px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewBandColumn Name="F_DATEBETWEEN" Caption="적용기간">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_FROMDATE" Caption="시작일" Width="100px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_TODATE" Caption="종료일" Width="100px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Name="F_DATEBETWEEN" Caption="검사기준 및 목표">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_STANDARDNEW" Caption="규격" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_MAXNEW" Caption="상한값" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_MINNEW" Caption="하한값" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_UCLXNEW" Caption="UCLX" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_LCLXNEW" Caption="LCLX" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_UCLRNEW" Caption="UCLR" Width="80px">
                                <CellStyle HorizontalAlign="Right" />
                            </dx:GridViewDataColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataColumn FieldName="F_INSUSER" Caption="수정자" Width="150px"></dx:GridViewDataColumn>
                    <dx:GridViewDataDateColumn FieldName="F_INSDT" Caption="수정일시" Width="150px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="검사항목코드" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="검사유형" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="검사유형명" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Caption="일련번호" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한값" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한값" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_UCLX" Caption="UCLX" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LCLX" Caption="LCLX" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_UCLR" Caption="UCLR" Visible="false"></dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
