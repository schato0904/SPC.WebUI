<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS3001.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.MEAS3001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var _fieldList = "F_MS01D4ID;F_MS01MID;F_EQUIPNO;F_EQUIPNM;F_MAKERNM;F_MODEL;F_INDT;F_UNQNO;F_STAND;F_LASTFIXDT;F_PROCNM;F_STATUSCD;F_ABNORMALCD;F_ONDT;F_CUSTID;F_CUSTNM;F_TEAMCD;F_USER;F_ATTFILENO;F_RETNPLANDT;F_RETNDT;F_CONTENTS";
        var key="";
        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(devGrid.GetMainElement()).offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - pagingHeight - scrollbarWidth(), 10));
            fn_OnValidate();
            devGrid.SetHeight(height);
            srcF_PROCDATE.SetValue(new Date());
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
            devCallback._Command = 'DELETE';
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

                    if (devCallback._Command == 'DELETE') {
                        fn_OnNewClick();
                        devCallback._Command = '';
                    }

                    devGrid.PerformCallback();
                    if (s.cpResultGbn != "") {
                        fn_GetTable("MEAS", "QMM10_TBL", "F_MS01D4ID", s.cpResultGbn, AfterCallback);
                        s.cpResultGbn = "";
                    }
                }
            }
        }

        function fn_OnSetSearchResult(result) {
            fn_SetFields(result)
        }

        function fn_OnSetClear() {
            fn_ClearFields(_fieldList, "src");

            fn_OnControlEnableBox(srcF_STATUSCD);
            srcF_STATUSCD.SetEnabled(true);
            fn_OnControlEnableBox(srcF_ABNORMALCD);
            srcF_ABNORMALCD.SetEnabled(true);
            //window["srcF_CUSTID"].fn_SetEnabled(true);
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

        // 입고번호 조회
        function fn_PopupSearch() {
            fn_OnPopupMEAS3001POPSearch('SetItem', srcF_EQUIPNO.GetText());
        }

        // 입고번호 조회 후 콜백
        function SetItem(resultvalue) {
            fn_OnSetClear();
            fn_OnValidate();
            srcF_MS01MID.SetText(resultvalue.F_MS01MID);
            srcF_EQUIPNO.SetText(resultvalue.F_EQUIPNO);
            srcF_EQUIPNM.SetText(resultvalue.F_EQUIPNM);
            srcF_MAKERNM.SetText(resultvalue.F_MS01MID);
            srcF_MODEL.SetText(resultvalue.F_MODEL);
            srcF_INDT.SetText(resultvalue.F_INDT);
            srcF_LASTFIXDT.SetText(resultvalue.F_LASTFIXDT);
            srcF_EQUIPNO.Validate();
        }

        function fn_shcPopupSearch() {
            fn_OnPopupMEAS3001POPSearch('ShcSetItem', shcF_EQUIPNO.GetText());
        }

        function ShcSetItem(resultvalue) {
            shcF_EQUIPNO.SetText(resultvalue.F_EQUIPNO);
            shcF_EQUIPNM.SetText(resultvalue.F_EQUIPNM);
        }

        // 그리드 더블클릭시
        function fn_devGridOnRowDblClick(s, e) {
            key = s.GetRowKey(e.visibleIndex)
            fn_GetTable("MEAS", "QMM10_TBL", "F_MS01D4ID", key, AfterCallback);
        }

        function AfterCallback(result) {
            fn_SetFields(result);
            fn_OnControlDisableBox(srcF_STATUSCD);
            srcF_STATUSCD.SetEnabled(false);
            fn_OnControlDisableBox(srcF_ABNORMALCD);
            srcF_ABNORMALCD.SetEnabled(false);

            if (srcF_PROCDATE.GetText() == "") {
                srcF_PROCDATE.SetValue(new Date());
            }
            fn_OnValidate();
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
            <dx:ASPxCallback runat="server" ID="devCallback" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" />
            </dx:ASPxCallback>
            <table class="InputTable" style="margin-bottom: 5px;">
                <colgroup>
                    <col style="width:10%" /><col style="width:23%" /><col style="width:10%" /><col style="width:23%" /><col style="width:10%" /><col style="width:24%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>관리번호</label>
                    </td>
                    <td class="tdContent">
                        <div style="float: left; width: 80%;">
                            <dx:ASPxTextBox ID="srcF_MS01D4ID" ClientInstanceName="srcF_MS01D4ID" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="srcF_MS01MID" ClientInstanceName="srcF_MS01MID" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="srcF_EQUIPNO" ClientInstanceName="srcF_EQUIPNO" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox">
                                <ValidationSettings RequiredField-IsRequired="true" 
                                            ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
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
                        <label>측정기명</label>
                    </td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_EQUIPNM" ClientInstanceName="srcF_EQUIPNM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                    </td>
                    <td class="tdTitle" >제조사</td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_MAKERNM" ClientInstanceName="srcF_MAKERNM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle" >모델</td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_MODEL" ClientInstanceName="srcF_MODEL" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdTitle">
                        <label>도입일자</label>
                    </td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_INDT" ClientInstanceName="srcF_INDT" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdTitle">
                        <label>고유번호</label>
                    </td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_UNQNO" ClientInstanceName="srcF_UNQNO" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox">
                        </dx:ASPxTextBox>
                    </td>                         
                </tr>
                <tr>
                    <td class="tdTitle">
                        <label>규격</label>
                    </td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_STAND" ClientInstanceName="srcF_STAND" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                    </td>
                    <td class="tdTitle">최종교정일</td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_LASTFIXDT" ClientInstanceName="srcF_LASTFIXDT" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                    </td>
                    <td class="tdTitle" >처리일자</td>
                    <td class="tdContent">
                        <dx:ASPxDateEdit runat="server" ID="srcF_PROCDATE" ClientInstanceName="srcF_PROCDATE"  Width="100%">
                            <ValidationSettings RequiredField-IsRequired="true" 
                                            ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
            </table>
            <table class="InputTable" style="margin-bottom: 10px;">
                <colgroup>
                    <col style="width:10%" /><col style="width:23%" /><col style="width:10%" /><col style="width:23%" /><col style="width:10%" /><col style="width:24%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">상태구분</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox ID="srcF_STATUSCD" ClientInstanceName="srcF_STATUSCD" runat="server" ValueType="System.String" Width="100%">
                            <ValidationSettings RequiredField-IsRequired="true" 
                                            ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">이상처리구분</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox ID="srcF_ABNORMALCD" ClientInstanceName="srcF_ABNORMALCD" runat="server" ValueType="System.String" Width="100%">
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">발생일자</td>
                    <td class="tdContent">
                        <dx:ASPxDateEdit runat="server" ID="srcF_ONDT" ClientInstanceName="srcF_ONDT"  Width="100%">
                            <ValidationSettings RequiredField-IsRequired="true" 
                                            ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">거래처</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox runat="server" ID="srcF_CUSTID" ClientInstanceName="srcF_CUSTID" Width="100%"></dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">사용팀</td>
                    <td class="tdContent">
                        <dx:ASPxComboBox runat="server" ID="srcF_TEAMCD" ClientInstanceName="srcF_TEAMCD" Width="100%" OnCallback="srcF_TEAMCD_Callback">
                            <ClientSideEvents Init="fn_OnControlDisable"/>
                            <ValidationSettings RequiredField-IsRequired="true" 
                                ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" Display="Dynamic"></ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdTitle">사용자</td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_USER" ClientInstanceName="srcF_USER" runat="server" Width="100%" ></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">반납예정일</td>
                    <td class="tdContent">
                        <dx:ASPxDateEdit runat="server" ID="srcF_RETNPLANDT" ClientInstanceName="srcF_RETNPLANDT"  Width="100%">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdTitle">반납일</td>
                    <td class="tdContent">
                        <dx:ASPxDateEdit runat="server" ID="srcF_RETNDT" ClientInstanceName="srcF_RETNDT"  Width="100%"></dx:ASPxDateEdit>
                    </td>
                    <td class="tdTitle">첨부</td>
                    <td class="tdContent">
                        <dx:ASPxTextBox ID="srcF_ATTFILENO" ClientInstanceName="srcF_ATTFILENO" ClientVisible="false" runat="server" Width="100%" class="form-control input-sm" />
                        <span style="width: 100%">
                            <button class="btn btn-sm btn-warning" style="width: 100px; padding: 3px 0px;" onclick="fn_AttachFileOpen('부적합첨부파일등록', 'A', 'F','srcF_ATTFILENO'); return false;">
                                <i class="i i-file-plus "></i>
                                <span class="text">파일첨부</span>
                            </button>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">내용</td>
                    <td colspan="5">
                        <dx:ASPxMemo ID="srcF_CONTENTS" ClientInstanceName="srcF_CONTENTS" runat="server" Width="100%" Height="50px"/>
                    </td>
                </tr>
            </table>
            <div class="divline">&nbsp;</div>
            <section id="searchCondition" class="panel panel-default" style="padding:0px;margin-bottom: 5px; border: 0px none transparent;">
            <div class="form-horizontal">
                <table class="InputTable">
                    <colgroup>
                        <col style="width:9%" /><col style="width:22%" /><col style="width:9%" /><col style="width:22%" /><col style="width:9%" /><col style="width:22%" /><col style="width:7%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">
                            <label>발생일</label>
                        </td>
                        <td class="tdContent">
                            <ucCTF:Date ID="ucDate" runat="server" />
                        </td>
                        <td class="tdTitle">
                            <label>사용구분</label>
                        </td>
                        <td class="tdContent">
                            <dx:ASPxComboBox ID="shcF_STATUSCD" ClientInstanceName="shcF_STATUSCD" runat="server" ValueType="System.String" Width="100%"></dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">
                            <label>관리번호</label>
                        </td>
                        <td class="tdContent" style="border-right-width:0px;">
                            <div class="control-label" style="float: left; width: 34%;padding-top:0;">
                                <dx:ASPxTextBox ID="shcF_EQUIPNO" ClientInstanceName="shcF_EQUIPNO" runat="server" Width="100%" >
                                    <ClientSideEvents LostFocus="fn_OnEquipnoLostFocus" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="control-label" style="float: left; width: 54%;margin-left:2px;padding-top:0;">
                                <dx:ASPxTextBox ID="shcF_EQUIPNM" ClientInstanceName="shcF_EQUIPNM" runat="server" Width="100%" ClientSideEvents-Init="fn_OnControlDisableBox"></dx:ASPxTextBox>
                            </div>
                            <div class="control-label" style="float: left; width: 10%;text-align:left;padding-left:2px; padding-top:0;">
                                <button class="btn btn-default btn-xs" data-toggle="button" title="관리번호조회" onclick="fn_shcPopupSearch(); return false;">
                                    <i class="i i-popup text"></i>
                                    <i class="i i-popup text-active text-danger"></i>
                                </button>
                            </div>
                        </td>
                        <td class="tdContent" style="text-align:right;padding-right:10px;">
                            <button class="btn btn-sm btn-success" onclick="fn_OnSearchClick(); return false;">
                               <i class="fa fa-search"></i>
                               <span class="text">조회</span>
                           </button>
                        </td>
                    </tr>
                </table>
            </div>
            </section>
        </div>
        <div class="content">
            <section class="panel panel-default" >
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%" OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
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
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_devGridOnRowDblClick" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_ONDT" Caption="발생일자"  Width="100px" />
                        <dx:GridViewDataColumn FieldName="F_EQUIPNO" Caption="관리번호"  Width="100px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_EQUIPNM" Caption="측정기명"  Width="200px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_PRODNO" Caption="제조번호"  Width="120px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_STAND" Caption="규격"  Width="100px"/>
                        <dx:GridViewDataColumn FieldName="F_STATUSNM" Caption="사용구분"  Width="90px" />                        
                        <dx:GridViewDataColumn FieldName="F_ABNORMALNM" Caption="이상처리구분"  Width="90px" />                        
                        <dx:GridViewDataColumn FieldName="F_RETNPLANDT" Caption="반납예정일"  Width="90px" />                        
                        <dx:GridViewDataColumn FieldName="F_RETNDT" Caption="반납일"  Width="90px" />                        
                        <dx:GridViewDataColumn FieldName="F_TEAMNM" Caption="사용팀"  Width="90px" CellStyle-HorizontalAlign="Left" />                        
                        <dx:GridViewDataColumn FieldName="F_USER" Caption="사용자"  Width="100px" CellStyle-HorizontalAlign="Left" />                        
                        <dx:GridViewDataColumn FieldName="F_CONTENTS" Caption="비고"  Width="220px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_PROCFLG" Caption="작업자요청"  Width="90px" CellStyle-HorizontalAlign="Center" />
                        <dx:GridViewDataColumn FieldName="F_PROCDATE" Caption="처리일자"  Width="90px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_MS01D4ID" Visible="false" />
                    </Columns>
                </dx:ASPxGridView>
            </section>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
