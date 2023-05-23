<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="CATM1302POP.aspx.cs" Inherits="SPC.WebUI.Pages.CATM.Popup.CATM1302POP" %>

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

            devGrid.SetHeight(400);
            devGrid2.SetHeight(400);
        }

        // 조회
        function fn_OnSearchClick() {
            //devGrid.PerformCallback('GET');

            devGrid.PerformCallback();
            devGrid2.PerformCallback();
            

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


        }
    </script>
    <script type="text/javascript">        // 사용자 정의 함수
        function SetNew(isFirst) {
            SetInit(isFirst);
          
          
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

            //schF_FROMYMD.SetValue(new Date().Add(0, 0, -7));
            //schF_TOYMD.SetValue(new Date());
        }
        // 기본값 설정
        function SetDefault() {

        }

        // 페이지모드 변경 (mode : NEW, EDIT, READONLY)
        

        // 페이지 활성/비활성화
        function SetPageEnable(enable) {

        }

        // 페이지모드 조회
        function GetPageMode() {
            return hidPageMode.GetText();
        }

        // 선택항목 조회(좌측 키로 우측 조회)


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



        }



        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // Validate
       




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divRight" style="width: 100%;">
            <div id="divMiddle" style="float: left; margin-bottom: 2px; width: 100%;">
                <table class="InputTable" style="width: 100%;">
                    <colgroup>
                        <col style="width: 11%" />
                        <col style="width: 22%" />
                        <col style="width: 11%" />
                        <col style="width: 22%" />
                        <col style="width: 11%" />
                        <col style="width: 23%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">작업완료일</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_WORKDATE" ClientInstanceName="schF_WORKDATE" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">설비명</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">생산품번</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_ITEMCD" ClientInstanceName="schF_ITEMCD" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">작지번호</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_WORKNO" ClientInstanceName="schF_WORKNO" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">금형번호</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_MOLDNO" ClientInstanceName="schF_MOLDNO" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">금형차수</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_MOLDNTH" ClientInstanceName="schF_MOLDNTH" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">LotNO</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_LOTNO" ClientInstanceName="schF_LOTNO" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">생산수량</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_PRODCOUNT" ClientInstanceName="schF_PRODCOUNT" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdTitle">불량수량</td>
                        <td class="tdContent">
                            <dx:ASPxTextBox ID="schF_ERRCOUNT" ClientInstanceName="schF_ERRCOUNT" runat="server" class="form-control input-sm" Width="100%">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>

                </table>
            </div>
            <div style="clear: both;"></div>
            <div class="content" style="width: 100%; float: right;">
                <dx:ASPxTextBox ID="hidKEY" ClientInstanceName="hidKEY" runat="server" ClientVisible="false" Text="" />
                <table style="width: 100%;">
                    <colgroup>
                        <col style="width: 50%" />
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td>
                            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" Width="100%"
                                EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Visible" />
                                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                <Templates>
                                    <StatusBar>
                                        <ucCTF:Count ID="ucCount" runat="server" targetCtrl="devGrid" />
                                    </StatusBar>
                                </Templates>
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="F_RANK" Caption="No" Width="11%"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_ERRORCD" Caption="유형코드" Width="25%"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_ERRORNM" Caption="불량유형" Width="32%" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_ERRORCOUNT" Caption="불량수량" Width="32%" CellStyle-HorizontalAlign="Right"></dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </td>
                        <td style="padding-left:15px;">
                            <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" Width="100%"
                                EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center" Font-Size="9pt"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Visible" />
                                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                <Templates>
                                    <StatusBar>
                                        <ucCTF:Count ID="ucCount" runat="server" targetCtrl="devGrid2" />
                                    </StatusBar>
                                </Templates>
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="F_RANK" Caption="No" Width="11%"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_LOSSCD" Caption="유형코드" Width="25%"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_LOSSNM" Caption="유실유형" Width="32%" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="F_LOSSHOUR" Caption="유실시간" Width="32%" CellStyle-HorizontalAlign="Right"></dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="clear: both;"></div>
    </div>
    <div id="divHidden" style="display: none;">


        <dx:ASPxTextBox runat="server" ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" ClientVisible="false">
        </dx:ASPxTextBox>

        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>

    </div>
</asp:Content>
