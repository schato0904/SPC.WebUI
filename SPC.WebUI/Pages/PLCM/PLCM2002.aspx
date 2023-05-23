<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM2002.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM2002" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        $(window).bind('resizeEnd', function () {
        });

        // 조회
        function fn_OnSearchClick() {
            if (schF_MACHCD.GetValue() == null) {
                alert('설비를 선택하세요.');
                return false;
            }
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
        }

        // 인쇄
        function fn_OnPrintClick() {
        }

        // 엑셀
        function fn_OnExcelClick() {
            if (devGrid.GetVisibleRowsOnPage() == 0) {
                ucNavi.SetNoticeOnce('조회된 내용이 없습니다.', 'red');
                return false;
            }
            btnExport.DoClick();
        }

        // 이벤트 핸들러
        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            // 리턴값 처리
            var result = '';
            var data = '';
            if (Trim(s.cpResult) != '') {
                try {
                    result = JSON.parse(s.cpResult);
                    if (result.error != '') {
                        alert(result.error);
                    } else {
                        if (result.msg != '') {
                            ucNavi.SetNoticeOnce(result.msg);
                        }
                        if (result.data != '') {
                            data = result.data;
                        }
                    }
                } catch (err) {
                    alert(err);
                }
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        function fn_schF_FROMYMD_ValueChanged(s, e) {
            if (schF_MACHCD.GetValue() != null && schF_MACHCD.GetValue != '') {
                schF_RECIPEID.PerformCallback();
            }
        }

        function fn_schF_MACHCD_ValueChanged(s, e) {
            if (s.GetValue() != null && s.GetValue() != '') {
                schF_RECIPEID.PerformCallback();
            }
        }

        function fn_schF_RECIPEID_ValueChanged(s, e) {
            if (s.GetValue() != null && s.GetValue() != '') {
                schF_MASTERID.PerformCallback();
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="InputTable" style="width: 100%;">
                <colgroup>
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">작업일자</td>
                    <td class="tdContent">
                        <ucCTF:Date ID="ucDate" runat="server" Changed="fn_schF_FROMYMD_ValueChanged" />
                    </td>
                    <td class="tdTitle">설비</td>
                    <td class="tdContent">
                        <div style="width: 100%; float: left;">
                            <dx:ASPxComboBox runat="server" ID="schF_MACHCD" ClientInstanceName="schF_MACHCD" IncrementalFilteringMode="None" ValueType="System.String" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                <ClientSideEvents ValueChanged="fn_schF_MACHCD_ValueChanged" />
                            </dx:ASPxComboBox>
                        </div>
                    </td>
                    <td class="tdTitle">레시피</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox runat="server" ID="schF_RECIPEID" ClientInstanceName="schF_RECIPEID" IncrementalFilteringMode="None" ValueType="System.String" OnCallback="schF_RECIPEID_Callback" Width="100%">
                            <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            <ClientSideEvents ValueChanged="fn_schF_RECIPEID_ValueChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">마스터ID</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox runat="server" ID="schF_MASTERID" ClientInstanceName="schF_MASTERID" IncrementalFilteringMode="None" ValueType="System.String" OnCallback="schF_MASTERID_Callback" Width="100%">
                            <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="clear: both; height: 2px;"></div>
        <div class="content">
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit"
                    EndCallback="fn_OnGridEndCallback"
                    CallbackError="fn_OnCallbackError" />
                <Templates>
                    <StatusBar>
                        <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                        <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="No" Caption="No" Width="35px" />
                    <dx:GridViewDataTextColumn FieldName="F_MACHCD" Caption="설비코드" Width="70px" />
                    <dx:GridViewDataTextColumn FieldName="F_MACHNM" Caption="설비명" CellStyle-HorizontalAlign="Left" Width="100px" />
                    <dx:GridViewDataTextColumn FieldName="F_TDATE" Caption="작업일자" Width="100px" />
                    <dx:GridViewDataTextColumn FieldName="F_STIME" Caption="작업 시작시간" Width="100px" />
                    <dx:GridViewDataTextColumn FieldName="F_ETIME" Caption="작업 종료시간" Width="100px" />
                    <dx:GridViewDataTextColumn FieldName="F_RECIPEID" Caption="레시피ID" CellStyle-HorizontalAlign="Left" Width="150px" />
                    <dx:GridViewDataTextColumn FieldName="F_MASTERID" Caption="마스터ID" CellStyle-HorizontalAlign="Left" Width="150px" />
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
    <div id="divHidden" style="display: none;">
        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
    </div>
</asp:Content>
