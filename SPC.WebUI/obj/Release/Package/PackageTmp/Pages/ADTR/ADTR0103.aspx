<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0103.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0103" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var cpcpk= "1";
        $(document).ready(function () {
            $("#cpk").hide();
            $("#cp").show();

            fn_RendorTotalCount();

            // 모니터링에서 넘어온 경우 조회시작
            var oSetParam = "<%=oSetParam%>";
            
            if (oSetParam != '') {
                //alert(oSetParam)
                
                var oSetParams = oSetParam.split('|');

                //// 업체
                fn_SetTextValue('hidCOMPCD', oSetParams[0]);
                // 공장
                fn_SetTextValue('hidFACTCD', oSetParams[1]);
                // 검색일
                fn_SetTextValue('hidUCFROMDT', oSetParams[2]);
                fn_SetDate('txtFROMDT', convertDateString(oSetParams[2]));
                fn_SetTextValue('hidUCTODT', oSetParams[3]);
                fn_SetDate('txtTODT', convertDateString(oSetParams[3]));
                fn_SetTextValue('hidUCTODT', oSetParams[3]);
                //fn_OnSetUCBan(oSetParams[4])

                //var Ctrl = ASPxClientControl.Cast('ddlBAN');
                ////Ctrl.PerformCallback();
                //fn_OnBANValueChanged(Ctrl)
                timerFACT = setTimeout(function () { fn_OnSetUCBan(oSetParams[4]); }, 500);
                timerFACT = setTimeout(function () { fn_OnSetUCLine(oSetParams[5]); }, 1000);
                //fn_OnSetUCLine(oSetParams[5])
                timerFACT = setTimeout(function () { devGrid.PerformCallback(); }, 1500);
              
            }
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var errTableHeight = 15;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth() - errTableHeight, 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            <%if (!gsVENDOR){%>
                if (hidCOMPCD.GetValue() == "" || hidCOMPCD.GetValue() == null) {
                    alert("업체를 선택하세요!!");
                    return false;
                }
                if (hidFACTCD.GetValue() == "" || hidFACTCD.GetValue() == null) {
                    alert("공장을 선택하세요!!");
                    return false;
                }
            <%}%>

            cpcpk = rdoGbn.GetValue();
            devGrid.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() {
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else
                devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
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

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {
            btnExport.DoClick();
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
            } else {
                var cpCount = s.cpCount.split('|');

                for (i = 0; i < 4; i++) {
                    $("#cp" + i).text(cpCount[i]);
                }
                for (i = 0; i < 4; i++) {
                    $("#cpk" + i).text(cpCount[i+4]);
                }

                if (cpcpk == "1") {
                    $("#cpk").hide();
                    $("#cp").show();
                } else {
                    $("#cp").hide();
                    $("#cpk").show();
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
            fn_OnBatchValidate("F_BANCD", s, e);
            fn_OnBatchValidate("F_BANNM", s, e);
            
            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {                
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlDisableBox(editor, null);                
            } else {
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlEnableBox(editor, null);
            }
        }

        function fn_OnRowDblClick(s, e) {
            var visibleIndex = e.visibleIndex
            if (visibleIndex < 0) return;

            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(e.visibleIndex));

            var oParams = "";

            oParams += fn_GetCastText('hidUCFROMDT') + '|'
            oParams += fn_GetCastText('hidUCTODT') + '|'
            oParams += rowKeys; //F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN
            oParams += '|||' + fn_GetCastText('hidCOMPCD') + '|' + fn_GetCastText('hidFACTCD');
            parent.fn_OnDeleteTab('ANLS0101', parent.fn_OnGetTabObject('ANLS0101'));
            parent.parent.doCreateTab('MM03|MM0301|ANLS|ANLS0101|품질종합현황|RP|1', oParams);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">                
                <div class="form-group" style="display:<%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" targetCtrls="ddlBAN" nullText="선택" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-4 control-label">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>     
                    <label class="col-sm-1 control-label">구분</label>
                    <div class="col-sm-2 control-label">
                        <div class="control-label" style="float: left; width:50% ">
                            <dx:ASPxRadioButtonList ID="rdoGbn" runat="server" ClientInstanceName="rdoGbn" Width="100%" RepeatDirection="Horizontal" Border-BorderStyle="None" BackColor="White"
                                ValueField="F_GBN" CssClass="NoXButton">
                                <Paddings Padding="0" />
                                <Items>
                                    <dx:ListEditItem Value="1" Text="Cp" />
                                    <dx:ListEditItem Value="2" Text="Cpk" />
                                </Items>
                            </dx:ASPxRadioButtonList>
                        </div>
                        <div class="control-label" style="float: left; width:50%" >
                             <dx:ASPxCheckBox ID="chkImport" runat="server" >                            
                                </dx:ASPxCheckBox>
                                중요항목             
                        </div>
                    </div>    
                                      
                </div>
                
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" targetCtrls="ddlWORK" />
                    </div>                
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Work ID="ucWork" runat="server" />
                    </div>                        
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품질수준</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="ddlRANK" ClientInstanceName="ddlRANK" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">등급</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxComboBox ID="ddlGrade" runat="server" Width="100%" ClientInstanceName="ddlGrade" ValueField="F_GBN" >
                            <ClientSideEvents Init="fn_OnControlDisable" />
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" />                                
                                <dx:ListEditItem Text="A등급:cp(Cpk) >= 1.67" Value="1" />
                                <dx:ListEditItem Text="B등급:1.67 > cp(Cpk) >= 1.33" Value="2" />
                                <dx:ListEditItem Text="C등급:1.33 > cp(Cpk) >= 1" Value="3" />
                                <dx:ListEditItem Text="D등급:1.0 > cp(Cpk)" Value="4" />
                            </Items>
                        </dx:ASPxComboBox>
                        
                        
                    </div>
                    
                </div>             
            </div>
            <section class="panel panel-default">
                <div id="errTable">
                    <table class="table table-striped m-b-none">
                        <thead>
                            <tr>
                                <th style="width:12%">구분</th>
                                <th style="width:22%;background-color: #1aae88;" class="bg-success">A급(우수)1.67 이상</th>
                                <th style="width:22%;background-color: #1ccacc;" class="bg-info">B급(양호)1.33 - 1.67</th>
                                <th style="width:22%;background-color: #fcc633;" class="bg-warning">C급(보통)1.0 - 1.33</th>
                                <th style="width:22%;background-color: #e33244;" class="bg-danger">D급(미흡)1.0 이하</th>
                            </tr>
                            <tr id="cp">
                                <th>Cp(건)</th>
                                <th id="cp0"></th>
                                <th id="cp1"></th>
                                <th id="cp2"></th>
                                <th id="cp3"></th>    
                            </tr>
                            <tr id="cpk">
                                <th>Cpk(건)</th>
                                <th id="cpk0"></th>
                                <th id="cpk1"></th>
                                <th id="cpk2"></th>
                                <th id="cpk3"></th>    
                            </tr>                       
                        </thead>
                        <tbody>                         
                        </tbody>
                    </table>
                </div>
            </section>
            <p>해당내역을 더블클릭 하시면 품질종합현황을 확인하실 수 있습니다.</p>
        </div>
        <div class="content">
            <section class="panel panel-default" style="height: 100%;">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    SettingsBehavior-AllowSelectSingleRowOnly="true" SettingsBehavior-AllowSelectByRowClick="true" SettingsBehavior-AllowSort="true"
                    KeyFieldName="F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                    OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto"  />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_STATUS"  Caption="등급" Width="60px"   />
                        <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명"  Width="200px"   >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px"   />
                        <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명"   Width="200px" >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목"  Width="150px"  >
                            <CellStyle HorizontalAlign="Left" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_CP" Caption="Cp"  Width="80px"   >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_CPK" Caption="Cpk" Width="80px"    >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="F_STANDARD" Caption="규격"   Width="80px" >
                            <CellStyle HorizontalAlign="Right" />                            
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_MAX" Caption="상한"   Width="80px"  >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="F_MIN" Caption="하한"   Width="80px"  >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="F_AVG" Caption="평균"   Width="80px"  >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_RANGE" Caption="R"    Width="80px"  >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_CNT" Caption="시료수"   Width="80px"  >
                            <CellStyle HorizontalAlign="Right" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_MEAINSPCD" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_SIRYO" Visible="false" />
                        <dx:GridViewDataColumn FieldName="F_FREEPOINT" Visible="false" />
                    </Columns>                
                </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            </section>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
