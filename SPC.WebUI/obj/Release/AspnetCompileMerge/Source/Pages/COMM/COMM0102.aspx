<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="COMM0102.aspx.cs" Inherits="SPC.WebUI.Pages.COMM.COMM0102" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <style type="text/css">
        #td.black {
            color: black;
        }
    </style>

    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';





        function fn_OnPopupCompSearch(ctrl) {
            pPage = rootURL + 'Pages/COMM/Popup/COMPPOP.aspx' +
                '?TITLE=업체조회' +
                '&COMPNM=' + ASPxClientControl.Cast('tdResult01').GetText();

            fn_OnPopupOpen(pPage, '800', '500');
        }

        function fn_OnPopupCompSearch2() {
            pPage = rootURL + 'Pages/COMM/Popup/COMPPOP2.aspx' +
                '?TITLE=업체조회' +
                '&COMPNM=' + ASPxClientControl.Cast('tdResult12').GetText();

            fn_OnPopupOpen(pPage, '800', '500');
        }




        $(document).ready(function () {
            fn_Clear();
        });

            // 동적으로 높이값을 계산한다
            function fn_AdjustSize() {
                var top = $(".container").offset().top;
                var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
                var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
                var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
                var devheight = $("#InputTable").height();
                //alert(devheight)
                devGrid1.SetHeight(devheight-37);
                devGrid2.SetHeight(height-420);
            }

            // 조회
            function fn_OnSearchClick() {
                // devGrid.PerformCallback();
                devGrid1.PerformCallback();

            }

            // 입력
            function fn_OnNewClick() {
                fn_Clear();
            }

            function fn_OnNewRow() {
                devGrid1.AddNewRow();
            }

            // 수정
            function fn_OnModifyClick() {
                //selectedKeys = devGrid.GetSelectedKeysOnPage();

                // 1 Row 반드시 선택
                if (selectedKeys.length <= 0) {
                    alert('수정할 데이타를 선택하세요!!');
                    return false;
                }

                for (var i = 0; i < selectedKeys.length ; i++) {
                    // devGrid.StartEditRowByKey(selectedKeys[i]);
                }
            }

            // 저장
            function fn_OnSaveClick() {

                devCallback.PerformCallback('SAVE');
            }



            // 취소
            function fn_OnCancelClick() {
                //devGrid.UnselectAllRowsOnPage();
                //devGrid.CancelEdit();
                fn_Clear();
            }

            // 삭제
            function fn_OnDeleteClick() {
                if (!confirm('해당 상담 내역을 삭제하시겠습니까?')) { return false; }

                devCallback.PerformCallback('DELETE');
            }

            function fn_OnDeleteRow() {
                selectedKeys = devGrid1.GetSelectedKeysOnPage();

                //selectedKeys = hidUSER.GetValue()+ '|' + selectedKeys;

                if (selectedKeys.length <= 0) {
                    alert('삭제할 담당자를 선택하세요!!');
                    return false;
                }

                if (!confirm('선택한 담당자를 삭제하시겠습니까?')) { return false; }
                //alert(selectedKeys.length);


                for (var i = 0; i < selectedKeys.length ; i++) {
                    devGrid1.DeleteRowByKey(selectedKeys[i]);
                }
                //devGrid1.UpdateEdit();

            }

            // 인쇄
            function fn_OnPrintClick() { }

            // 엑셀
            function fn_OnExcelClick() {

            }

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

                if (s.cpParam != '') {
                    if (s.cpParam == "SAVE") {
                        if (!devGrid1.batchEditApi.HasChanges()) {
                            alert("저장 되었습니다.")
                        }
                        devGrid1.UpdateEdit();
                        devGrid2.PerformCallback();
                    }
                } else if (s.cpParam == "DELETE") {
                    if (!devGrid1.batchEditApi.HasChanges()) {
                        alert("저장 되었습니다.")
                    }
                    devGrid1.UpdateEdit();
                    devGrid2.PerformCallback();
                }
            }

            function fn_Clear() {
                tdResult01.Clear();
                tdResult02.Clear();
                tdResult03.Clear();
                tdResult04.Clear();
                tdResult05.Clear();
                tdResult06.Clear();
                tdResult07.Clear();
                tdResult08.SetText('<%=dt%>');
                tdResult09.Clear();
                tdResult10.Clear();
                tdResult11.Clear();
                ddlUSER.Clear();
                hidUSER.Clear();


                devGrid1.PerformCallback();
            }

            // Grid Callback Error
            function fn_OnCallbackError(s, e) {
                e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
            }

            function fn_OnSettingComp(KEY) {

                ASPxClientControl.Cast('tdResult01').SetText(KEY[0]);
                ASPxClientControl.Cast('tdResult02').SetText(KEY[1]);
                ASPxClientControl.Cast('tdResult03').SetText(KEY[2]);
                ASPxClientControl.Cast('tdResult04').SetText(KEY[3]);
                ASPxClientControl.Cast('tdResult05').SetText(KEY[4]);
                ASPxClientControl.Cast('tdResult06').SetText('');
                ASPxClientControl.Cast('tdResult07').SetText('');
                ASPxClientControl.Cast('tdResult08').SetText('<%=dt%>');
                ASPxClientControl.Cast('tdResult09').SetText('');
                ASPxClientControl.Cast('tdResult10').SetText('');
                ASPxClientControl.Cast('tdResult11').SetText('');
                ASPxClientControl.Cast('tdHide01').SetText('');
                ASPxClientControl.Cast('hidUSER').SetText(KEY[5]);
                fn_devGrid1Call();

            }

            function fn_OnSettingComp2(KEY) {

                ASPxClientControl.Cast('tdResult12').SetText(KEY[0]);


            }

            function fn_OnSettingComp3(KEY) {

                ASPxClientControl.Cast('tdResult01').SetText(KEY[0]);
                ASPxClientControl.Cast('tdResult02').SetText(KEY[1]);
                ASPxClientControl.Cast('tdResult03').SetText(KEY[2]);
                ASPxClientControl.Cast('tdResult04').SetText(KEY[3]);
                ASPxClientControl.Cast('tdResult05').SetText(KEY[4]);
                ASPxClientControl.Cast('tdResult06').SetText(KEY[5]);
                ASPxClientControl.Cast('tdResult07').SetText(KEY[6]);
                ASPxClientControl.Cast('tdResult08').SetText(KEY[7]);
                ASPxClientControl.Cast('tdResult09').SetText(KEY[8]);
                ASPxClientControl.Cast('tdResult10').SetText(KEY[9]);
                ASPxClientControl.Cast('tdResult11').SetText(KEY[10]);
                ASPxClientControl.Cast('tdHide01').SetText(KEY[11]);
                ASPxClientControl.Cast('ddlUSER').SetValue(KEY[6]);
                ASPxClientControl.Cast('rdoGbn').SetValue(KEY[14]);

            }

            function fn_OnSettingUser(KEY) {
                ASPxClientControl.Cast('hidUSER').SetText(KEY[12]);


            }



            function fn_devGrid2_SearchClick() {
                devGrid2.PerformCallback();
            }


            function fn_OnRowDblClick(s, e) {



                var selectedKeys = fn_OnRowKeysNullValueToEmpty(devGrid2.GetRowKey(e.visibleIndex));

                var rowKey = selectedKeys.split('|');


                fn_OnSettingUser(rowKey);

                devGrid1.PerformCallback();

                fn_OnSettingComp3(rowKey);


            }

            function fn_devGrid1Call() {
                devGrid1.PerformCallback();
            }

            function fn_OnValueChanged(s, e) {
                var val = s.GetValue();
                tdResult07.SetValue(val);

            }

            function fn_OnBatchEditStartEditing(s, e) {

            }






    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">

        <div class="content" style="width: 100%; height: 100%; display: inline;">
            <table>
                <tr>
                    <td style="width:65%;padding-right:5px;">
                        <div style="padding-bottom: 15px;">
                            <table class="tbl1" style="width: 100%; height: 30%; vertical-align: top">
                                <tr>
                                    <td style="height: 100%; padding-top: 1px; vertical-align: top">
                                        <section class="panel panel-default" style="margin-bottom: 0px; height: 100%">
                                            <div class="table-responsive">

                                                <dx:ASPxCallback ID="devCallback" runat="server" ClientInstanceName="devCallback" OnCallback="devCallback_Callback">
                                                    <ClientSideEvents EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" />
                                                </dx:ASPxCallback>

                                                <table class="InputTable" id="InputTable">
                                                    <colgroup>
                                                        <col style="width: 10%" />
                                                        <col style="width: 23%" />
                                                        <col style="width: 10%" />
                                                        <col style="width: 23%" />
                                                        <col style="width: 10%" />
                                                        <col style="width: 23%" />
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td class="tdTitle">업체명</td>
                                                            <td class="tdContentR">
                                                                <div style="float: left; width:88%">
                                                                    <dx:ASPxTextBox ID="tdHide01" ClientInstanceName="tdHide01" runat="server" ClientVisible="false">
                                                                    </dx:ASPxTextBox>

                                                                    <dx:ASPxTextBox ID="tdResult01" ClientInstanceName="tdResult01" Width="100%" OnInit="tdResult01_Init" runat="server">
                                                                    </dx:ASPxTextBox>
                                                                </div>

                                                                <div style="float: left; padding-left: 3px;">
                                                                    <button class="btn btn-default btn-xs" data-toggle="button" onclick="fn_OnPopupCompSearch(); return false;">
                                                                        <i class="i i-popup text"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                            <td class="tdContent" colspan="4"></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdTitle">지역</td>
                                                            <td class="tdContent">
                                                                <dx:ASPxTextBox ID="tdResult02" ClientInstanceName="tdResult02" runat="server" Width="100%" ReadOnly="true" BackColor="#d3d3d3">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td class="tdTitle">전화번호</td>
                                                            <td class="tdContent">
                                                                <dx:ASPxTextBox runat="server" ID="tdResult03" ClientInstanceName="tdResult03" Width="100%" BackColor="#d3d3d3" ReadOnly="true">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td class="tdTitle">주소</td>
                                                            <td class="tdContent">
                                                                <dx:ASPxTextBox runat="server" ID="tdResult04" ClientInstanceName="tdResult04" Width="100%" BackColor="#d3d3d3" ReadOnly="true">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdTitle">특이사항</td>
                                                            <td class="tdContent" colspan="5">
                                                                <dx:ASPxMemo runat="server" ID="tdResult05" ClientInstanceName="tdResult05" Rows="4" Width="100%" BackColor="#d3d3d3" ReadOnly="true">
                                                                </dx:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdTitle">구축담당자</td>
                                                            <td class="tdContent">
                                                                <dx:ASPxTextBox ID="tdResult06" ClientInstanceName="tdResult06" runat="server" Width="100%" class="form-control input-sm">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td class="tdTitle">관리담당자</td>
                                                            <td class="tdContent">
                                                                <dx:ASPxComboBox runat="server" ID="ddlUSER" ClientInstanceName="ddlUSER" Width="100%"
                                                                    TextField="NAME" ValueField="NAME" OnCallback="ddlUSER_Callback"
                                                                    IncrementalFilteringMode="None">
                                                                    <ClientSideEvents ValueChanged="fn_OnValueChanged" Init="fn_OnControlDisable" />
                                                                </dx:ASPxComboBox>
                                                                <dx:ASPxTextBox runat="server" ID="tdResult07" ClientInstanceName="tdResult07" ClientVisible="false">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td class="tdTitle">최근 상담일</td>
                                                            <td class="tdContent">
                                                                <dx:ASPxDateEdit runat="server" ID="tdResult08" ClientInstanceName="tdResult08" Width="100%" ></dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdTitle">사용여부</td>
                                                            <td class="tdContentR" colspan="2">
                                                                <dx:ASPxRadioButtonList ID="rdoGbn" runat="server" ClientInstanceName="rdoGbn" Width="100%" RepeatDirection="Horizontal" Border-BorderStyle="None" BackColor="White" 
                                                                    ValueField="F_GBN" CssClass="NoXButton">
                                                                    <Paddings Padding="0" />
                                                                    <Items>
                                                                        <dx:ListEditItem Value="1" Text="자주사용" />
                                                                        <dx:ListEditItem Value="2" Text="가끔사용" />
                                                                        <dx:ListEditItem Value="3" Text="사용안함" />
                                                                    </Items>
                                                                </dx:ASPxRadioButtonList>
                                                            </td>          
                                                            <td class="tdContent"></td>                                                  
                                                            <td class="tdTitle">기타(이유)</td>
                                                            <td class="tdContent">
                                                                <dx:ASPxTextBox runat="server" ID="tdResult10" ClientInstanceName="tdResult10" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdTitle">비고</td>
                                                            <td class="tdContent" colspan="5">
                                                                <dx:ASPxTextBox runat="server" ID="tdResult09" ClientInstanceName="tdResult09" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdTitle">상담내역</td>
                                                            <td class="tdContent" colspan="5">
                                                                <dx:ASPxMemo runat="server" ID="tdResult11" ClientInstanceName="tdResult11" Rows="4" Width="100%">
                                                                </dx:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </section>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="width:35%">   
                        <div class="blockTitle" style="float:left; width:50%">
                            <span style="font-weight:bold">[담당자 정보]</span>
                        </div>                     
                        <div class="pull-right" style="padding-bottom: 5px;">
                            <button class="btn btn-sm btn-success" onclick="fn_OnNewRow();return false;" style="padding-top: 3px; padding-bottom: 3px;">
                                <i class="glyphicon glyphicon-chevron-down"></i>
                                <span class="text">추가</span>
                            </button>

                            <button class="btn btn-sm btn-success" onclick="fn_OnDeleteRow();return false;" style="padding-top: 3px; padding-bottom: 3px;">
                                <i class="fa fa-scissors"></i>
                                <span class="text">삭제</span>
                            </button>
                        </div>

                            <div style="padding-bottom: 10px; padding-top: 5px;">
                            <dx:ASPxTextBox ID="hidUSER" ClientInstanceName="hidUSER" runat="server" ClientVisible="false" Text="" />
                            <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                                KeyFieldName="F_USER;F_PHONE;F_EMAIL;F_REMARK;" EnableViewState="false" EnableRowsCache="false"
                                OnCustomCallback="devGrid1_CustomCallback"
                                OnBatchUpdate="devGrid1_BatchUpdate">
                                <Styles>
                                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                                    <EditForm CssClass="bg-default"></EditForm>
                                </Styles>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="90" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                                <SettingsBehavior AllowSort="false" ColumnResizeMode="Disabled" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" />
                                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" BatchEditStartEditing="fn_OnBatchEditStartEditing" />

                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption=" " VisibleIndex="0" Width="50px">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataColumn Caption="담당자" FieldName="F_USER" Width="15%" CellStyle-HorizontalAlign="Left" />
                                    <dx:GridViewDataColumn Caption="H.P" FieldName="F_PHONE" Width="20%" CellStyle-HorizontalAlign="Left" />
                                    <dx:GridViewDataColumn Caption="Email" FieldName="F_EMAIL" Width="25%" CellStyle-HorizontalAlign="Left" />
                                    <dx:GridViewDataColumn Caption="비고" FieldName="F_REMARK" Width="40%" CellStyle-HorizontalAlign="Left" />

                                </Columns>
                            </dx:ASPxGridView>

                        </div>
                    </td>
                </tr>
            </table>

            <div style="height: 100%;">
                <table class="InputTable" style="margin-bottom:10px;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 23%" />
                        <col style="width: 10%" />
                        <col style="width: 23%" />
                        <col style="width: 10%" />
                        <col style="width: 23%" />
                    </colgroup>
                    <tbody>
                        <tr>
                            <td class="tdTitle" >조회기간</td>
                            <td class="tdContent">
                                <div style="width: 100%;">

                                    <ucCTF:Date runat="server" ID="ucDate" />
                                </div>


                            </td>

                            <td class="tdTitle" >업체명</td>
                            <td class="tdContentR">

                                <div style="float: left; width:90%">
                                    <dx:ASPxTextBox ID="tdResult12" ClientInstanceName="tdResult12" OnInit="tdResult12_Init" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </div>

                                <div style="float: left; padding-left: 3px;">
                                    <button class="btn btn-default btn-xs" data-toggle="button" onclick="fn_OnPopupCompSearch2(); return false;">
                                        <i class="i i-popup text"></i>

                                    </button>

                                </div>

                            </td>

                            <td colspan="2" class="tdContent" style="text-align:right">

                                <button class="btn btn-sm btn-success" onclick="fn_devGrid2_SearchClick();return false;" style="padding-top: 3px; padding-bottom: 3px;">
                                    <i class="fa fa-search"></i>
                                    <span class="text">조회</span>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="False" Width="100%" EnableViewState="False" EnableRowsCache="False"
                    OnCustomCallback="devGrid2_CustomCallback"
                    KeyFieldName="F_COMPNM;F_REGION;F_TELNO;F_ADDRESS;F_BIGO;F_INSTAL;F_MANAGE;F_COUNDT;F_REMARK;F_REASON;F_COUNREMARK;F_CUST01ID;F_CSCD;F_NO;F_USEGUBUN;">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="100" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_NO" Caption="NO" Width="50px" />
                        <dx:GridViewDataColumn FieldName="F_COUNDT" Caption="최근상담일" Width="120px" />
                        <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="160px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_INSTAL" Caption="구축담당자" Width="140px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_MANAGE" Caption="관리담당자" Width="140px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_USEGUBUNNM" Caption="사용여부" Width="150px" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="F_REMARK" Caption="비고" Width="400px" CellStyle-HorizontalAlign="Left" />


                        <dx:GridViewDataColumn FieldName="F_REGION" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_TELNO" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_ADDRESS" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_BIGO" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_REASON" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_COUNREMARK" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_CUST01ID" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_CSCD" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_USEGUBUN" Visible="false"></dx:GridViewDataColumn>

                    </Columns>
                </dx:ASPxGridView>
            </div>



        </div>







    </div>
</asp:Content>

