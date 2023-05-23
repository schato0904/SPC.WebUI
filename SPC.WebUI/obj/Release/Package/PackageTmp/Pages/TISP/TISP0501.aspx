<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TISP0501.aspx.cs" Inherits="SPC.WebUI.Pages.TISP.TISP0501" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
         <style type="text/css">
        .container {
            width: 100%;
            height: 100%;
            display: table;
        }
        .search {
            width: 100%;
        }
        .content {
            width: 100%;
            height: 100%;
        }
       .left {
            width: 1200px;
            height: 100%;
            display: table-cell;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            width:40%;
            height: 100%;
            display: table-cell;
        }
    </style>

    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
            devGrid2.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            //if (!fn_ValidateITEM()) { return;};
            //if (!fn_ValidateWORK()) { return; };
            //if(!fn_ValidateINSPITEM()) { return;};

            devGrid.PerformCallback();
            devGrid2.PerformCallback('|||');
        }

        // 입력
        function fn_OnNewClick() {
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            // 수정은 1 Row씩만 가능
            if (selectedKeys.length > 1) {
                alert('수정은 한개의 데이타만 가능합니다.');
                return false;
            }

            fn_doSetGridEventAction('true');

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {

            if (!fn_OnValidate()) return false;

            fn_doSetGridEventAction('true');
            devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            fn_doSetGridEventAction('true');
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제한 데이타는 복원할 수 없습니다.')) { return false; }

            fn_doSetGridEventAction('true');

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }
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

            // EditForm 크기를 조정한다
            if ($(".divEditForm").length) {
                //var editFormWidth = $("#cphBody_devGrid").width() - 34 - scrollbarWidth();
                var editFormWidth = $("#cphBody_devGrid").width() //- $(".right").width();
                editFormWidth = editFormWidth > parseInt(hidGridColumnsWidth.GetText(), 10) ? parseInt(hidGridColumnsWidth.GetText(), 10) - 34 : editFormWidth;
                $(".divEditForm").width(editFormWidth);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {

            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function devGrid_RowDbClick(s, e) {
            
            if (!(parseInt(e.visibleIndex, 10) >= 0))
                return;

            //if (devGrid.GetRowKey(e.visibleIndex).split('|')[3].replace(/\s+$/, "") != "AAC501") {
            //    alert("외관인 항목은 상세내역을 조회 할 수 없습니다");
            //    devGrid2.PerformCallback("|||");
            //}
            //else {
            //    devGrid2.PerformCallback(devGrid.GetRowKey(e.visibleIndex));
            //}

            devGrid2.PerformCallback(devGrid.GetRowKey(e.visibleIndex));

            fn_doSetGridEventAction('true');
            devGrid.StartEditRow(e.visibleIndex);
        }

        // Validate
        function fn_OnValidate(s, e) {

            if (txtCONTENTS.GetText() == '') {
                alert('삭제사유를 입력하세요!!');
                txtCONTENTS.Focus();
                return false;
            } else {
                return true;
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" SingleDate="true" TodayFromDate="true"  />
                    </div>                    
                    <label class="col-sm-1 control-label">합불여부</label>
                    <div class="col-sm-3">
                        <dx:ASPxComboBox ID="ddlOKNGCHK" ClientInstanceName="ddlOKNGCHK" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true" />
                                <dx:ListEditItem Text="합격" Value="0"  />
                                <dx:ListEditItem Text="불합격" Value="1"  />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>                 
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="left" >
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_WORKDATE;F_ITEMCD;F_WORKCD;F_WORKTIME" EnableViewState="false" EnableRowsCache="false"
                    OnInitNewRow="devGrid_InitNewRow" 
                    OnCustomCallback="devGrid_CustomCallback" 
                    OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" 
                    OnHtmlRowPrepared="devGrid_HtmlRowPrepared" 
                    OnHtmlEditFormCreated="devGrid_HtmlEditFormCreated"
                    OnRowUpdating="devGrid_RowUpdating"
                    >
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                    <SettingsBehavior AllowSort="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="devGrid_RowDbClick" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                    ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="150px" />
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px"  >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Width="100px"/>
                        <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="200px" >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                        <dx:GridViewDataColumn FieldName="F_CNT" Caption="항목수"  Width="70px"  />
                        <dx:GridViewDataColumn FieldName="F_NGCNT"    Caption="부적합수"  Width="70px"  />
                    </Columns>
                    <Templates>
                        <EditForm>
                            <div class="form-horizontal divEditForm">
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">작업일자</label>
                                    <div class="col-sm-4">
                                        <div style="float: left; width: 40%; padding-right: 3px;">
                                            <dx:ASPxTextBox ID="txtWORKDATE" ClientInstanceName="txtWORKDATE" runat="server" Width="100%" 
                                                class="form-control input-sm" Text='<%# Bind("[F_WORKDATE]") %>' >
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div style="float: left; width: 60%;">
                                        </div>
                                    </div>
                                    <label class="col-sm-1 control-label">작업시간</label>
                                    <div class="col-sm-4">
                                        <div style="float: left; width: 40%; padding-right: 3px;">
                                            <dx:ASPxTextBox ID="txtWORKTIME" ClientInstanceName="txtWORKTIME" runat="server" Width="100%" 
                                                class="form-control input-sm" Text='<%# Bind("[F_WORKTIME]") %>'>
                                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div style="float: left; width: 60%;">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">품목코드</label>
                                    <div class="col-sm-4">
                                        <div style="float: left; width: 40%; padding-right: 3px;">
                                            <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" 
                                                class="form-control input-sm" Text='<%# Bind("[F_ITEMCD]") %>' >
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div style="float: left; width: 60%;">
                                            <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%" 
                                                class="form-control input-sm" Text='<%# Bind("[F_ITEMNM]") %>'>
                                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </div>
                                    </div>
                                    <label class="col-sm-1 control-label">공정코드</label>
                                    <div class="col-sm-4">
                                        <div style="float: left; width: 40%; padding-right: 3px;">
                                            <dx:ASPxTextBox ID="txtWORKCD" ClientInstanceName="txtWORKCD" runat="server" Width="100%" 
                                                class="form-control input-sm" Text='<%# Bind("[F_WORKCD]") %>'>
                                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div style="float: left; width: 60%;">
                                            <dx:ASPxTextBox ID="txtWORKNM" ClientInstanceName="txtWORKNM" runat="server" Width="100%" 
                                                class="form-control input-sm" Text='<%# Bind("[F_WORKNM]") %>'>
                                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                                            </dx:ASPxTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">항목수</label>
                                    <div class="col-sm-4">
                                        <dx:ASPxTextBox ID="txtNUMBER" ClientInstanceName="txtNUMBER" runat="server" Width="100%"
                                            class="form-control input-sm" Text='<%# Bind("[F_CNT]") %>' >
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                    <label class="col-sm-1 control-label">부적합수</label>
                                    <div class="col-sm-4">
                                        <dx:ASPxTextBox ID="txtTSERIALNO" ClientInstanceName="txtTSERIALNO" runat="server" Width="100%"
                                            class="form-control input-sm" Text='<%# Bind("[F_NGCNT]") %>' >                                                
                                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                                        </dx:ASPxTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">삭제사유</label>
                                    <div class="col-sm-9">
                                        <dx:ASPxMemo ID="txtCONTENTS" ClientInstanceName="txtCONTENTS" runat="server" Width="100%"
                                            class="form-control input-sm" Text='<%# Bind("[F_CONTENTS]") %>' >                                                
                                        </dx:ASPxMemo>
                                    </div>   
                                    <div class="col-sm-1">
                                        <ul class="nav navbar-nav navbar-right m-n">                                    
                                            <li id="D" class="topbutton" style="padding-top: 9px; padding-right: 5px;">
                                                <button class="btn btn-sm btn-danger" onclick="fn_OnSaveClick(); return false;">
                                                    <i class="fa fa-scissors"></i>
                                                    <span class="text">삭제</span>
                                                </button>
                                            </li>                                    
                                        </ul>
                                    </div>                               
                                </div>
                        </EditForm>
                    </Templates>                
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                <div class="paging">
                    <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
                </div>
            </div>
            <div class="right">
                <section class="panel panel-default" style="height: 100%;">                    
                    <dx:ASPxTextBox ID="hidGridAction2" ClientInstanceName="hidGridAction2" runat="server" ClientVisible="false" />
                    <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="False"  Width="100%" 
                        KeyFieldName="F_ROWNUM" EnableViewState="False" EnableRowsCache="False"
                        OnCustomCallback="devGrid2_CustomCallback" 
                        OnHtmlRowPrepared="devGrid2_HtmlRowPrepared"
                        >
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                        <SettingsBehavior AllowSort="false" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError" />
                        <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
                        </StatusBar>
                    </Templates>
                        <Columns>                            
                           <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="90px"  />
                            <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목"  Width="140px"  >
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>       
                            <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한규격"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>       
                            <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한규격"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>       
                            <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>       
                            <dx:GridViewDataColumn FieldName="F_SD" Caption="편차"  Width="70px"  >
                                <CellStyle HorizontalAlign="Right" />
                           </dx:GridViewDataColumn>    
                            
                            
                            <dx:GridViewDataColumn FieldName="F_ROWNUM" Visible="false"/>   
                        </Columns>
                    </dx:ASPxGridView>
                </section>
            </div>
        </div>
        
    </div>
</asp:Content>
