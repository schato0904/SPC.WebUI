<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM5002.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.CATM5002" %>

<%-- 주조기 모니터링 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {
            setTimeout(SetNew, 100);
            fn_OnSearchClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var h = Math.floor(($(document).height() - $('#div1').offset().top - _hMargin) / 2);
            $('#div1').height(h);
            $('#div2').height(h);
            fnASPxGridView_ReHeight_ByContainer(devGrid1);
            fnASPxGridView_ReHeight_ByContainer(devGrid2);
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid1.PerformCallback('GET');
        }

        // 입력
        function fn_OnNewClick() {
            SetNew();
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
            if (devGrid1.GetVisibleRowsOnPage() == 0 && devGrid2.GetVisibleRowsOnPage() == 0) {
                ucNavi.SetNoticeOnce('조회된 내용이 없습니다.', 'red');
                return false;
            }
            btnExport.DoClick();
        }
    </script>
    <script type="text/javascript">        // 사용자 정의 함수
        function SetNew(isFirst) {
            SetInit(isFirst);
            SetPageMode('NEW');
            fn_OnValidate();
        }
        // 키가 있는지 확인
        function HasPkey() {
            //return Trim(srcF_WORKNO.GetText()) != '' ;
        }
        // 초기화
        function SetInit(isFirst) {
            // 초기화
            SetClear(isFirst);
            SetDefault();
            //SetStatusBySubmitCD();
        }
        // 컨트롤값 클리어
        function SetClear(isFirst) {
            schF_PLANYMD.SetValue(new Date());
        }
        // 기본값 설정
        function SetDefault() {
        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        function SetPageMode(mode) {
            // 페이지 모드에 따라 설정
            switch (mode) {
                case "NEW":
                    //SetPageEnable(Trim(srcF_ITEMCD.GetText()) != '');
                    //SetPageEnable(true);
                    break;
                case "EDIT":
                    //SetPageEnable(true);
                    //srcF_CODE.SetEnabled(false);
                    break;
                case "READONLY":
                    //SetPageEnable(false);
                    break;
                default:
                    alert("PageMode가 설정되지 않았습니다");
                    break;
            }
            //SetPageEnable(mode);
            hidPageMode.SetText(mode);
        }

        // 페이지 활성/비활성화
        function SetPageEnable(enable) {
        }

        // 컨트롤 값 설정
        function SetValue(data) {
        }

        function SetButtonEnable(enable) {
        }
    </script>
    <script type="text/javascript">
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

            // 그리드별 처리
            switch (s) {
                //case devGrid:
                //    break;
                case devGrid1:
                    var width = $('#divRightChart').width();
                    var height = Math.floor(($(document).height() - $('#divRightChart').offset().top - _hMargin) / 2);
                    SetChartSize(width, height);
                    devChart1.PerformCallback();
                    devGrid2.PerformCallback('GET');
                    break;
                case devGrid2:
                    devChart2.PerformCallback();
                    break;
            }
        }

        

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            var result = true;
            result = ASPxClientEdit.ValidateEditorsInContainerById('divRight');
            return result;
        }

        function SetChartSize(w, h) {
            hidChartWidth.SetValue(w);
            hidChartHeight.SetValue(h);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divRight" style="width: 100%;">
            <div id="divTop" style="float: left; margin-bottom: 2px; width: 100%;">
                <table class="InputTable" style="width: 100%;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col style="width: 15%" />
                        <col style="width: 10%" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">작업일자</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_PLANYMD" ClientInstanceName="schF_PLANYMD" UseMaskBehavior="true"
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White"
                                    Width="100%" AllowNull="true">
                                </dx:ASPxDateEdit>
                            </div>
                            <div style="clear: both;"></div>
                        </td>
                        <td class="tdTitle">주조기</td>
                        <td class="tdContent">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox runat="server" ID="schF_MACHCD" ClientInstanceName="schF_MACHCD" IncrementalFilteringMode="None" ValueType="System.String" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">자동여부</td>
                        <td class="tdContentR">
                            <div style="width: 100%; float: left;">
                                <dx:ASPxComboBox ID="schF_AUTO" ClientInstanceName="schF_AUTO" runat="server" Width="50%">
                                    <Items>
                                        <dx:ListEditItem Value="" Text="전체" Selected="true" />
                                        <dx:ListEditItem Value="1" Text="사용" />
                                        <dx:ListEditItem Value="2" Text="미사용" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdContentL" colspan="2"></td>
                    </tr>
                </table>
            </div>
            <div style="clear: both;"></div>
        </div>
        <div id="div1" style="width: 100%; height: 350px;">
            <div id="divRightBody" style="width: 30%; float: left; height: 100%;">
                <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="No"
                    EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid1_CustomCallback"
                    OnDataBinding="devGrid1_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />
                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError"/>
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="50px" />
                        <dx:GridViewDataTextColumn FieldName="F_WORKTIME" Caption="작업시간" />
                        <dx:GridViewDataTextColumn FieldName="F_TEMP01" Caption="금형上온도" Width="120px" />
                        <dx:GridViewDataTextColumn FieldName="F_TEMP02" Caption="금형下온도" Width="120px" />
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid1" OnRenderBrick="devGrid1Exporter_RenderBrick"></dx:ASPxGridViewExporter>
            </div>
            <div id="divRightChart" style="width: 70%; float: left;">
                <dx:WebChartControl ID="devChart1" runat="server" ClientInstanceName="devChart1" OnCustomCallback="devChart1_CustomCallback" OnDataBinding="devChart1_DataBinding"></dx:WebChartControl>
            </div>
            <div style="clear: both;"></div>
        </div>
        <div id="div2" style="width: 100%; height: 350px;">
            <div id="divRightBody2" style="width: 30%; float: left; height: 100%;">
                <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="No" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid2_CustomCallback"
                    OnDataBinding="devGrid2_DataBinding" OnHtmlDataCellPrepared="devGrid2_HtmlDataCellPrepared">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="EndlessPaging" PageSize="50" />
                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError" />
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus1" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount1" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="50px" />
                        <dx:GridViewDataTextColumn FieldName="F_WORKTIME" Caption="작업시간" Width="90px" />
                        <dx:GridViewDataTextColumn FieldName="F_ITEMCD" Caption="품번" />
                        <dx:GridViewDataTextColumn FieldName="F_SLOPETIME" Caption="틸팅시간" Width="90px" />
                        <dx:GridViewDataTextColumn FieldName="F_WAITTIME" Caption="응고시간" Width="90px" />
                        <dx:GridViewDataColumn FieldName="F_AUTO" Caption="자동" Width="45px" />
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGrid2Exporter" runat="server" GridViewID="devGrid2" OnRenderBrick="devGrid2Exporter_RenderBrick"></dx:ASPxGridViewExporter>
            </div>
            <div id="divRightChart2" style="width: 70%; float: left;">
                <dx:WebChartControl ID="devChart2" runat="server" ClientInstanceName="devChart2" OnCustomCallback="devChart2_CustomCallback" OnDataBinding="devChart2_DataBinding"></dx:WebChartControl>
            </div>
            <div style="clear: both;"></div>
        </div>
    </div>
    <div id="divHidden" style="display: none;">
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
        <dx:ASPxTextBox runat="server" ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxSpinEdit runat="server" ID="hidChartWidth" ClientInstanceName="hidChartWidth" ClientVisible="false" NumberType="Integer" Number="1024">
        </dx:ASPxSpinEdit>
        <dx:ASPxSpinEdit runat="server" ID="hidChartHeight" ClientInstanceName="hidChartHeight" ClientVisible="false" NumberType="Integer" Number="768">
        </dx:ASPxSpinEdit>
    </div>
</asp:Content>
