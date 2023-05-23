<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS4003.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS4003" %>
<%-- 교정현황 조회 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .search {
            width: 100%;
            display: table-row;
        }
        .content {
            width: 100%;
            height: 100%;
            display: table-row;
        }
        .left {
            width: 300px;
            height: 100%;
            display: table-cell;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            height: 100%;
            display: table-cell;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = '';
        var _DOCTYPECD = 'AAG804';

        $(document).ready(function () {
            fn_doSetGridEventAction('true');
            fn_setdate();
        });

        function fn_setdate() {
            schF_FIXDT_FROM.SetValue(new Date().First());
            schF_FIXDT_TO.SetValue(new Date());
        }

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var devGrid = ASPxClientGridView.Cast('devGrid');
            var top = $(".content").offset().top;
            var height = Math.max(0, $(document.documentElement).height() - _hMargin - top);
            devGrid.SetHeight(fn_GetDocumentHeight() - _hMargin - $(devGrid.GetMainElement()).offset().top);
        }

        // 조회
        function fn_OnSearchClick() {
            if (false == fn_OnValidate()) return false;
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

        // 저장, 삭제 후 처리
        function devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            // 정상일 경우 그리드 재 조회
            if (result.CODE == "1") {
                fn_OnSearchClick();
            }

            alert(result.MESSAGE);
        }

        // 오류시
        function devCallback_CallbackError(s, e) {
            alert(e);
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
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

        // Grid End Callback
        function fn_devGridOnEndCallback(s, e) {
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
            else {
            }
        }

        // Validation 체크
        function fn_OnValidate() {
            return ASPxClientEdit.ValidateEditorsInContainerById('searchCondition');
        }

        // Tree End Callback
        function fn_OnEndCallback() { }

        //// 첨부파일 클릭
        //function fn_OnAttfileClick(ctrl) {
        //    var val = $(ctrl).attr("ATTFILENO");
        //    var F_DOCTYPECD = _DOCTYPECD
        //       , F_DOCNO = $(ctrl).attr("PLANNO")
        //       , F_DOCNM = $(ctrl).attr("PLANNM")
        //       , F_DOCID = $(ctrl).attr("DOCID");
        //    fn_SetViewHistory(F_DOCTYPECD, F_DOCNO, F_DOCNM, F_DOCID, function () { });
        //    hidF_ATTFILENO.SetText(val);
        //    fn_AttachFileOpenReadOnly('도면파일다운로드', 'P', 'T', 'hidF_ATTFILENO');
        //}

        // 라디오버튼 변경시 값 저장
        function schF_PLTYPECD_Changed(s, e) {
            var s = ASPxClientRadioButtonList.Cast(s);
            hidF_PLTYPECD.SetText(s.GetValue());
            //alert(s.GetValue());
        }

        // 그리드내 접수확인 버튼 클릭시
        function fn_OnGridAcceptClick(btn, id) {
            //alert(id);
            devCallback.PerformCallback('accept|' + id);
        }

        // 관리번호 조회
        function fn_PopupSearch() {
            fn_OnPopupMEAS3001POPSearch('SetItem', schF_EQUIPNO.GetText());
        }

        // 관리번호 조회 후 콜백
        function SetItem(resultvalue) {
            schF_MS01MID.SetText(resultvalue.F_MS01MID);
            schF_EQUIPNO.SetText(resultvalue.F_EQUIPNO);
            //srcF_EQUIPNM.SetText(resultvalue.F_EQUIPNM);
        }

        function fn_OnAttfileClick(ctrl) {
            var val = $(ctrl).attr("ATTFILENO");
            hidF_ATTFILENO.SetText(val);
            fn_AttachFileOpenReadOnly('자료파일다운로드', 'M', 'T', 'hidF_ATTFILENO');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div id="hdnFields" style="display:none;">
                <dx:ASPxTextBox ID="schF_PL01MID" ClientInstanceName="schF_PL01MID" runat="server" ClientVisible="false" />
                <dx:ASPxTextBox ID="hidF_ATTFILENO" ClientInstanceName="hidF_ATTFILENO" runat="server" ClientVisible="false" />
                <dx:ASPxTextBox ID="hidF_PLTYPECD" ClientInstanceName="hidF_PLTYPECD" runat="server" ClientVisible="false" />
            </div>
            <table class="InputTable" style="margin-bottom:5px;">
		        <colgroup>
		            <col style="width:10%" />
		            <col style="width:23%" />
		            <col style="width:10%" />
		            <col style="width:23%" />
		            <col style="width:10%" />
		            <col style="width:24%" />
		        </colgroup>
		        <tr>
		            <td class="tdTitle">
		                <label>교정일자</label>
		            </td>
		            <td class="tdContent" >
                        <div style="width:50%;float:left;">
                        <dx:ASPxDateEdit runat="server" ID="schF_FIXDT_FROM" ClientInstanceName="schF_FIXDT_FROM" UseMaskBehavior="true" 
                            EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                            Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                        </dx:ASPxDateEdit>
                    </div>
                    <div style="width:50%;float:left;">
                        <dx:ASPxDateEdit runat="server" ID="schF_FIXDT_TO" ClientInstanceName="schF_FIXDT_TO" UseMaskBehavior="true" 
                            EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                            Width="100%" AllowNull="true" DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" >
                        </dx:ASPxDateEdit>
                    </div>
                    <div style="clear:both;"></div>
		            </td>
		            <td class="tdTitle">
		                <label>측정기분류</label>
		            </td>
		            <td class="tdContent">
                        <dx:ASPxComboBox ID="schF_EQUIPDIVCD" ClientInstanceName="schF_EQUIPDIVCD" runat="server" ValueType="System.String" Width="100%"></dx:ASPxComboBox>
		            </td>
		            <td class="tdTitle">
		                <label>교정구분</label>
		            </td>
		            <td class="tdContent">
                        <dx:ASPxComboBox ID="schF_FIXTYPECD" ClientInstanceName="schF_FIXTYPECD" runat="server" ValueType="System.String" Width="100%"></dx:ASPxComboBox>
		            </td>
		        </tr>
		        <tr>
		            <td class="tdTitle">
		                <label>판정</label>
		            </td>
		            <td class="tdContent">
                        <dx:ASPxComboBox ID="schF_JUDGECD" ClientInstanceName="schF_JUDGECD" runat="server" ValueType="System.String" Width="100%"></dx:ASPxComboBox>
		            </td>
		            <td class="tdTitle">
		                <label>관리번호</label>
		            </td>
		            <td class="tdContent">
                        <div style="float: left; width: 80%;">
                            <dx:ASPxTextBox ID="schF_MS01MID" ClientInstanceName="schF_MS01MID" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="schF_EQUIPNO" ClientInstanceName="schF_EQUIPNO" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox">
                            </dx:ASPxTextBox>
                        </div>
                        <div id="divInButton" class="control-label" style="float: left; width: 20%; text-align: left; padding-left: 5px;">
                            <button class="btn btn-default btn-xs" data-toggle="button" title="관리번호조회" onclick="fn_PopupSearch(); return false;">
                                <i class="i i-popup text"></i>
                                <i class="i i-popup text-active text-danger"></i>
                            </button>
                        </div>
		            </td>
		            <td class="tdTitle">
		                <label>교정분야</label>
		            </td>
		            <td class="tdContent">
                        <dx:ASPxComboBox ID="schF_FIXDIVCD" ClientInstanceName="schF_FIXDIVCD" runat="server" ValueType="System.String" Width="100%"></dx:ASPxComboBox>
		            </td>
		        </tr>
		    </table>
            <section class="panel panel-default" style="height: 100%;">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MS01MID" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback"
                    OnHtmlRowCreated="devGrid_HtmlRowCreated"
                    OnDataBinding="devGrid_DataBinding">
                    <ClientSideEvents EndCallback="fn_devGridOnEndCallback" />
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_NO" Caption="No" Width="40px" UnboundType="Integer" CellStyle-HorizontalAlign="Right" >
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_FIXDT" Caption="교정일자" Width="100px">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px" CellStyle-HorizontalAlign="Left" ></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_EQUIPNM" Caption="측정기명" Width="180px" CellStyle-HorizontalAlign="Left" ></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_PRODNO" Caption="제조번호" Width="120px" CellStyle-HorizontalAlign="Left" ></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_STAND" Caption="규격" Width="80px" CellStyle-HorizontalAlign="Left" ></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_FIXREQNO" Caption="의뢰번호" Width="100px"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_REGUSER" Caption="의뢰자" Width="80px" CellStyle-HorizontalAlign="Left" ></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_TEAMNM" Caption="사용팀" Width="90px" CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_USERNM" Caption="사용자" Width="100px"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INDT" Caption="도입일자" Width="100px"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_TERMMONTH" Caption="주기" Width="60px" CellStyle-HorizontalAlign="Right" ></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_FIXGRPNM" Caption="교정기관" Width="100px" CellStyle-HorizontalAlign="Left" ></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_FIXNO" Caption="교정번호" Width="100px" CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_JUDGENM" Caption="판정" Width="70px" CellStyle-HorizontalAlign="Left"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_FIXDIVNM" Caption="교정분야" Width="80px" Visible="true">
                        </dx:GridViewDataColumn>
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
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
            </section>
        </div>
    </div>

    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback" ClientSideEvents-CallbackError="devCallback_CallbackError" ClientSideEvents-CallbackComplete="devCallback_CallbackComplete" />
</asp:Content>