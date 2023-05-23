<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD5002_DACO.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD5002_DACO" %>

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
            devGrid2.SetHeight(height - 145);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
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
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            if (fn_GetCastText('txtAUTH') == '조회' || fn_GetCastText('txtAUTH') == '작성') {
                alert('결재 권한이 없습니다.');
                return false;
            }

            if (selectedKeys == '' || selectedKeys == null) {
                alert('결재 공정을 선택하세요.');
                return false;
            } else {
                devCallback.PerformCallback(selectedKeys);
            }
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
        }

        // 인쇄
        function fn_OnPrintClick() {

            var KEYS = '';

            KEYS += encodeURIComponent(fn_GetCastText('txtDATE')) + '|'; // 작업일자
            KEYS += encodeURIComponent(fn_GetCastText('txtITEMCD1')) + '|'; // 오전품목
            KEYS += encodeURIComponent(fn_GetCastText('txtITEMCD2')) + '|'; // 오후품목
            KEYS += encodeURIComponent(fn_GetCastText('txtLINENM')) + '|'; // 라인명


            KEYS += encodeURIComponent(fn_GetCastText('txtWRI')) + '|'; // 작성자
            KEYS += encodeURIComponent(fn_GetCastText('txtREV')) + '|'; // 검토자
            KEYS += encodeURIComponent(fn_GetCastText('txtACK')) + '|'; // 승인자
            KEYS += encodeURIComponent(fn_GetCastText('txtAUTH1DATE')) + '|'; // 작성일
            KEYS += encodeURIComponent(fn_GetCastText('txtAUTH2DATE')) + '|'; // 검토일
            KEYS += encodeURIComponent(fn_GetCastText('txtAUTH3DATE')) + '|'; // 승인일

            KEYS += encodeURIComponent(fn_GetCastText('txtWORKTIME1')) + '|'; // 오전시간
            KEYS += encodeURIComponent(fn_GetCastText('txtWORKTIME2')); // 오후시간

            fn_OnPopupWERD5002_DACO_POP(KEYS);
        }

        // 엑셀
        function fn_OnExcelClick() {
        }


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

        function fn_OnRowdblclick(s, e) {
            selectedKey = devGrid.GetRowKey(e.visibleIndex);
            //F_WORKDATE;F_BANCD;F_LINECD;F_WORKCD;F_CHK;F_BANNM;F_LINENM;F_WORKNM;F_ITEMCD1;F_ITEMNM1;F_ITEMCD2;F_ITEMNM2;F_AUTH1NM;F_AUTH2NM;F_AUTH3NM;F_AUTH1DATE;F_AUTH2DATE;F_AUTH3DATE;F_WORKTIME1;F_WORKTIME2

            var Keys = selectedKey.split('|');

            txtDATE.SetValue(Keys[0]);
            txtBANCD.SetValue(Keys[1]);
            txtLINECD.SetValue(Keys[2]);
            txtWORKCD.SetValue(Keys[3]);
            txtBANNM.SetValue(Keys[5]);
            txtLINENM.SetValue(Keys[6]);
            txtWORKNM.SetValue(Keys[7]);
            txtITEMCD1.SetValue(Keys[8]);
            txtITEMNM1.SetValue(Keys[9]);
            txtITEMCD2.SetValue(Keys[10]);
            txtITEMNM2.SetValue(Keys[11]);
            txtWRI.SetValue(Keys[12]);
            txtREV.SetValue(Keys[13]);
            txtACK.SetValue(Keys[14]);

            txtAUTH1DATE.SetValue(Keys[15]);
            txtAUTH2DATE.SetValue(Keys[16]);
            txtAUTH3DATE.SetValue(Keys[17]);
            txtWORKTIME1.SetValue(Keys[18]);
            txtWORKTIME2.SetValue(Keys[19]);

            devGrid2.PerformCallback(selectedKey);
        }

        function fn_OndevCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);
                devGrid.UnselectAllRowsOnPage();
                devGrid.PerformCallback();
            }
        }

        function fn_devGrid2EndCallback(s, e) {
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" ID="ucDate" />
                    </div>
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">구분</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="cbGBN" ClientInstanceName="cbGBN" runat="server" CssClass="NoXButton">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true" />
                                <dx:ListEditItem Text="미완료" Value="0" />
                                <dx:ListEditItem Text="완료" Value="1" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">권한</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxTextBox ID="txtAUTH" ClientInstanceName="txtAUTH" runat="server" Text="">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <table style="width: 100%;">
                <colgroup>
                    <col style="width: 45%;" />
                    <col style="width: 54%;" />
                </colgroup>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="99%"
                            KeyFieldName="F_WORKDATE;F_BANCD;F_LINECD;F_WORKCD;F_CHK;F_BANNM;F_LINENM;F_WORKNM;F_ITEMCD1;F_ITEMNM1;F_ITEMCD2;F_ITEMNM2;F_AUTH1NM;F_AUTH2NM;F_AUTH3NM;F_AUTH1DATE;F_AUTH2DATE;F_AUTH3DATE;F_WORKTIME1;F_WORKTIME2" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowdblclick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count ID="ucCount" runat="server" targetCtrl="devGrid" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " VisibleIndex="0" Width="6%" ButtonType="Button">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="F_WORKDATE" Caption="일자" Width="18%" />
                                <dx:GridViewDataTextColumn FieldName="F_BANCD" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_BANNM" Caption="반명" Width="12%">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_LINECD" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_LINENM" Caption="라인명" Width="18%">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_WORKNM" Caption="공정명" Width="26%">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_WORKCD" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_AUTH1NM" Caption="작성" Width="8%">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_AUTH2NM" Caption="검토" Width="8%">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_AUTH3NM" Caption="승인" Width="8%">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CHK" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_ITEMCD1" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_ITEMNM1" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_ITEMCD2" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_ITEMNM2" Visible="false" />

                                <dx:GridViewDataTextColumn FieldName="F_AUTH1DATE" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_AUTH2DATE" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_AUTH3DATE" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_WORKTIME1" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="F_WORKTIME2" Visible="false" />

                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                            <ClientSideEvents EndCallback="fn_OndevCallback" />
                        </dx:ASPxCallback>
                    </td>
                    <td>
                        <div class="form-horizontal bg-gray fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F; height: 135px;">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">일자</label>
                                <div class="col-sm-3 control-label">
                                    <dx:ASPxTextBox ID="txtDATE" ClientInstanceName="txtDATE" runat="server" Width="95%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">오전품목</label>
                                <dx:ASPxTextBox ID="txtITEMCD1" ClientInstanceName="txtITEMCD1" runat="server" ClientVisible="false" Text="" />
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtITEMNM1" ClientInstanceName="txtITEMNM1" runat="server" Width="95%" Text="">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>

                                <label class="col-sm-2 control-label">오후품목</label>
                                <dx:ASPxTextBox ID="txtITEMCD2" ClientInstanceName="txtITEMCD2" runat="server" ClientVisible="false" />
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtITEMNM2" ClientInstanceName="txtITEMNM2" runat="server" Width="95%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">반</label>
                                <dx:ASPxTextBox ID="txtBANCD" ClientInstanceName="txtBANCD" runat="server" ClientVisible="false" />
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtBANNM" ClientInstanceName="txtBANNM" runat="server" Width="95%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>

                                <label class="col-sm-2 control-label">라인</label>
                                <dx:ASPxTextBox ID="txtLINECD" ClientInstanceName="txtLINECD" runat="server" ClientVisible="false" />
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtLINENM" ClientInstanceName="txtLINENM" runat="server" Width="95%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>

                                <label class="col-sm-2 control-label">공정</label>
                                <dx:ASPxTextBox ID="txtWORKCD" ClientInstanceName="txtWORKCD" runat="server" ClientVisible="false" />
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtWORKNM" ClientInstanceName="txtWORKNM" runat="server" Width="95%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">작성</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtWRI" ClientInstanceName="txtWRI" runat="server" Width="95%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-2 control-label">검토</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtREV" ClientInstanceName="txtREV" runat="server" Width="95%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <label class="col-sm-2 control-label">승인</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtACK" ClientInstanceName="txtACK" runat="server" Width="95%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <dx:ASPxTextBox ID="txtAUTH1DATE" ClientInstanceName="txtAUTH1DATE" runat="server" ClientVisible="false" />
                                <dx:ASPxTextBox ID="txtAUTH2DATE" ClientInstanceName="txtAUTH2DATE" runat="server" ClientVisible="false" />
                                <dx:ASPxTextBox ID="txtAUTH3DATE" ClientInstanceName="txtAUTH3DATE" runat="server" ClientVisible="false" />
                                <dx:ASPxTextBox ID="txtWORKTIME1" ClientInstanceName="txtWORKTIME1" runat="server" ClientVisible="false" />
                                <dx:ASPxTextBox ID="txtWORKTIME2" ClientInstanceName="txtWORKTIME2" runat="server" ClientVisible="false" />
                            </div>
                        </div>
                        <div>
                            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="F_WORKDATE" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGrid2_CustomCallback" ClientSideEvents-EndCallback="fn_devGrid2EndCallback">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                <SettingsBehavior AllowSort="false" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                <Templates>
                                    <StatusBar>
                                        <ucCTF:Count ID="ucCount" runat="server" targetCtrl="devGrid2" />
                                    </StatusBar>
                                </Templates>
                                <Columns>
                                    <%--<dx:GridViewDataTextColumn FieldName="F_NO" Caption="NO" Width="4%" />--%>
                                    <dx:GridViewDataTextColumn FieldName="F_MEAINSPNM" Caption="관리항목" Width="13%">
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="관리기준" Width="13%">
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewBandColumn Caption="오전">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA1" Caption="1" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA2" Caption="2" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA3" Caption="3" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA4" Caption="4" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA5" Caption="5" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="오후">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA6" Caption="1" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA7" Caption="2" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA8" Caption="3" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA9" Caption="4" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="F_DATA10" Caption="5" Width="7%">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
