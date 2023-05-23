<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0302.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.INSP0302" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_OnSearchClick();
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(window).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid1.SetHeight(height - 40);
            devGrid2.SetHeight(250);
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid1.PerformCallback();
            devGrid2.PerformCallback('');
        }

        // 입력
        function fn_OnNewClick() {
            // F_ITEMCD; F_WORKCD; F_WORKDATE; F_TSERIALNO; F_LOTNO; F_MOLDNO; F_WORKMAN
            var selectedKeys = devGrid1.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('검사성적서를 생성할 데이터를 선택하세요!!');
                return false;
            }

            var key, lotno = '', moldno = '', prevlotno = '', prevmoldno = '', idx = 0, isTrue = true;
            var F_ITEMCD = '', F_WORKCD = '', F_WORKDATE = '', F_TSERIALNO = '', F_LOTNO = '', F_MOLDNO = '', F_WORKMAN = '';
            selectedKeys.forEach(function (keys) {
                key = keys.split('|');
                lotno = key[4];
                moldno = key[5];

                if (idx > 0) {
                    if (lotno != prevlotno || moldno != prevmoldno) {
                        isTrue = false;
                        alert('로트번호 및 작지번호가 같은 검사결과만 동일 성적서로 생성할 수 있습니다!');
                        return;
                    }
                    F_TSERIALNO += "," + key[3] + "";
                } else {
                    F_ITEMCD = key[0];
                    F_WORKCD = key[1];
                    F_WORKDATE = key[2];
                    F_TSERIALNO += key[3];
                    F_LOTNO += key[4];
                    F_MOLDNO += key[5];
                    F_WORKMAN += key[6];
                }

                prevlotno = lotno;
                prevmoldno = moldno;
                idx++;
            });

            if (isTrue && idx > 0) {
                var data = '{';
                data += '"F_ITEMCD":"' + F_ITEMCD + '"';
                data += ',"F_WORKCD":"' + F_WORKCD + '"';
                data += ',"F_WORKDATE":"' + F_WORKDATE + '"';
                data += ',"F_TSERIALNO":"' + F_TSERIALNO + '"';
                data += ',"F_LOTNO":"' + F_LOTNO + '"';
                data += ',"F_MOLDNO":"' + F_MOLDNO + '"';
                data += ',"F_WORKMAN":"' + encodeURIComponent(F_WORKMAN) + '"';
                data += '}';
                fn_OnPopupINSP0302(data);
            }
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

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        function fn_OnRowDblClick(s, e) {
            // F_ITEMCD; F_WORKCD; F_WORKDATE; F_TSERIALNO; F_LOTNO; F_MOLDNO; F_WORKMAN
            var devGrid = ASPxClientGridView.Cast(s);
            var rowKey = devGrid.GetRowKey(e.visibleIndex).split('|');
            var data = '{';
            data += '"F_ITEMCD":"' + rowKey[0] + '"';
            data += ',"F_WORKCD":"' + rowKey[1] + '"';
            data += ',"F_WORKDATE":"' + rowKey[2] + '"';
            data += ',"F_TSERIALNO":"' + rowKey[3] + '"';
            data += '}';
            devGrid2.PerformCallback(data);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
<dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
<dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>              
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                    <label class="col-sm-1 control-label">Lot No.</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxTextBox ID="txtLOTNO" ClientInstanceName="txtLOTNO" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_WORKCD;F_WORKDATE;F_TSERIALNO;F_LOTNO;F_MOLDNO;F_WORKMAN" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid1_CustomCallback" OnHtmlDataCellPrepared="devGrid1_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid1" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid1.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드"  Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="200px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="300px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO" Caption="시료군"  Width="70px"  />
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No."  Width="100px"  />
                    <dx:GridViewDataColumn FieldName="F_MOLDNO" Caption="작지번호"  Width="120px"  />
                    <dx:GridViewDataColumn FieldName="F_OKCNT" Caption="합격수"  Width="80px" CellStyle-ForeColor="Blue"/>
                    <dx:GridViewDataColumn FieldName="F_OCCNT" Caption="관리이탈수"  Width="80px" CellStyle-ForeColor="DimGray" />
                    <dx:GridViewDataColumn FieldName="F_NGCNT" Caption="불합격수"  Width="80px" CellStyle-ForeColor="Red"  />
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="검사원"  Width="80px"  />
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid1" />
            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_SERIALNO;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid2_CustomCallback" OnCustomColumnDisplayText="devGrid2_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO"    Caption="측정군"  Width="70px"  />
                    <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Caption="항목코드"  Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="항목명" Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="순번"  Width="50px"  />
                    <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격"  Width="80px" >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한"  Width="80px" >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한"  Width="80px" >
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="80px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NGOKCHK" Caption="판정" Width="80px" />
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>