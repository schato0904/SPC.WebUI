<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0402_POLYTEC.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0402_POLYTEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            // 모니터링에서 넘어온 경우 조회시작
            var oSetParam = "<%=oSetParam%>";
            if (oSetParam != '') {
                var oSetParams = oSetParam.split('|');
                //// 업체
                fn_SetTextValue('hidCOMPCD', oSetParams[0]);
                // 공장
                fn_SetTextValue('hidFACTCD', oSetParams[1]);
                // 검색일
                fn_SetTextValue('hidUCFROMDT', oSetParams[2]);
                fn_SetDate('txtFROMDT', convertDateString(oSetParams[2]));
                fn_SetTextValue('hidUCTODT', oSetParams[2]);
                fn_SetDate('txtTODT', convertDateString(oSetParams[2]));

                devGrid.PerformCallback();
            }
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
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
            var _url = './Export/ADTR0402EXPORTPOLYTEC.aspx';
            _url += '?pSTDT=' + fn_GetCastValue('hidUCFROMDT');
            _url += '&pEDDT=' + fn_GetCastValue('hidUCTODT');
            _url += '&pJUDGE=' + fn_GetCastSelectedItemValue('ddlJUDGE');
            _url += '&pINSPCD=' + fn_GetCastSelectedItemValue('ddlINSPCD');
            _url += '&pITEMCD=' + fn_GetCastValue('hidUCITEMCD');
            _url += '&pWORKCD=' + fn_GetCastValue('hidUCWORKPOPCD');
            _url += '&pLOTNO=' + fn_GetCastValue('txtLOTNO');
            _url += '&pMOLDNO=' + fn_GetCastValue('txtMOLDNO');
            _url += '&pTAGAKNO=' + fn_GetCastValue('txtTAGAKNO');
            _url += '&pCOMPCD=' + fn_GetCastValue('hidCOMPCD');
            _url += '&pFACTCD=' + fn_GetCastValue('hidFACTCD');
            window.open(_url);
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

        function fn_OpenANLS0101(s, e) {
            var visibleIndex = e.visibleIndex
            if (visibleIndex < 0) return;

            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(visibleIndex));

            if (rowKeys.indexOf('|AAC501|') >= 0) {

                var oParams = "";

                oParams += fn_GetCastText('hidUCFROMDT') + '|'
                oParams += fn_GetCastText('hidUCTODT') + '|'

                var resultKeys = [];
                $.each(rowKeys.split('|'), function (idx, rowKey) {
                    if (idx < 14) {
                        resultKeys[idx] = rowKey;
                    }
                });

                oParams += resultKeys.join('|');

                <%if(!gsVENDOR) {%>
                oParams += '|' + fn_GetCastText('hidCOMPCD');
                oParams += '|' + fn_GetCastText('hidFACTCD');
                <%}%>

                parent.fn_OnDeleteTab('ANLS0101', parent.fn_OnGetTabObject('ANLS0101'));
                parent.parent.doCreateTab('MM03|MM0301|ANLS|ANLS0101|품질종합현황|RP|1', oParams);
            } else {
                alert('외관검사는 SPC분석을 할 수 없습니다!!');
            }
            return false;
        }

        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            txtSERIALNO.SetText(resultValues[10]);
            //alert(txtSERIALNO.GetText());
        }

        function fn_MeainspPopUP() {
            var inputElement = txtMEAINSPCD.GetInputElement();
            if (!inputElement.disabled) {
                fn_OnPopupMeainspSearch();
            }
        }

        // 검색된 검사항목 세팅
        function fn_OnSettingMeainsp(CODE, TEXT) {
            txtMEAINSPCD.SetText(CODE);
            txtINSPDETAIL.SetText(TEXT);
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
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>
                    <label class="col-sm-1 control-label">합격여부</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlJUDGE" ClientInstanceName="ddlJUDGE" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">검사구분</label>
                    <div class="col-sm-1">
                        <dx:ASPxComboBox ID="ddlINSPCD" ClientInstanceName="ddlINSPCD" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">Lot No.</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="txtLOTNO" ClientInstanceName="txtLOTNO" runat="server" Width="100%"></dx:ASPxTextBox>
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
                    <label class="col-sm-1 control-label">검사항목</label>
                    <div class="col-sm-2">
                        <div style="float: left; width: 40%; padding-right: 3px;">
                            <dx:ASPxTextBox ID="txtMEAINSPCD" ClientInstanceName="txtMEAINSPCD" runat="server" Width="100%" class="form-control input-sm"
                                OnInit="txtMEAINSPCD_Init">
                            </dx:ASPxTextBox>
                        </div>
                        <div style="float: left; width: 60%;">
                            <dx:ASPxTextBox ID="txtINSPDETAIL" ClientInstanceName="txtINSPDETAIL" runat="server" Width="100%" class="form-control input-sm">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">작지번호</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtMOLDNO" ClientInstanceName="txtMOLDNO" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">제품QR</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtTAGAKNO" ClientInstanceName="txtTAGAKNO" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_ITEMNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_SERIALNO;F_SIRYO;F_FREEPOINT;F_STANDARD;F_MAX;F_MIN;F_WORKDATE;F_WORKTIME;F_PCNM;F_MEASNO;F_INSPCD;F_TSERIALNO" EnableViewState="false" EnableRowsCache="false"
                OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto"  />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    CustomButtonClick="fn_OpenANLS0101" />
                <Columns>
                    <dx:GridViewCommandColumn Caption="SPC분석" Width="80px"  >
                        <CustomButtons>                            
                            <dx:GridViewCommandColumnCustomButton ID="보기" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="200px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="검사일자"  Width="80px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKTIME" Caption="검사시간" Width="80px"    />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드"  Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px"  >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TSERIALNO"    Caption="시료군"  Width="70px"  />
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
                    <dx:GridViewDataColumn FieldName="F_MEASURE" Caption="측정값"  Width="60px"  >
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LOTNO" Caption="Lot No."  Width="100px"  />
                    <dx:GridViewDataColumn FieldName="F_WORKMAN" Caption="작업자"  Width="60px"  />
                    <dx:GridViewDataColumn FieldName="F_PCNM" Caption="PC명"   Width="100px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_MOLDNO" Caption="작지번호"   Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_TAGAKNO" Caption="제품QR"   Width="150px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                        
                    <%--여기서부터 히든 필드--%>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_MEASNO" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_NGOKCHK" Visible="false" />
                </Columns>                
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="50" targetCtrls="devGrid" />
        </div>
    </div>
</asp:Content>
