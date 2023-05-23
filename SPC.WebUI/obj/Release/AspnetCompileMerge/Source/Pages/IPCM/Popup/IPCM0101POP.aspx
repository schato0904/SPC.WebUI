<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="IPCM0101POP.aspx.cs" Inherits="SPC.WebUI.Pages.IPCM.Popup.IPCM0101POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
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
            //devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {

        }

        // 입력
        function fn_OnNewClick() {
            
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (false == fn_OnValidate()) return false;
            if (!confirm('품질이상 제기를 저장하시겠습니까?\n계속진행하려면 확인을 누르세요!!')) return false;

            devCallback.PerformCallback('SAVE|');
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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            <%if (gsVENDOR) {%>
            if (!txtCTRLNO.GetValue() || txtCTRLNO.GetValue() == '') {
                alert('관리번호를 입력하세요!!');
                txtCTRLNO.Focus();
                return false;
            }
            <%}%>
            if (!txtRQDATE.GetValue() || txtRQDATE.GetValue() == '') {
                alert('이상제기일을 입력하세요!!');
                txtRQDATE.Focus();
                return false;
            }
            if (!txtITEMCD.GetValue() || txtITEMCD.GetValue() == '') {
                alert('품목코드를 선택하세요!!');
                txtITEMCD.Focus();
                return false;
            }
            if (!txtWORKCD.GetValue() || txtWORKCD.GetValue() == '') {
                alert('공정코드를 선택하세요!!');
                txtWORKCD.Focus();
                return false;
            }
            if (!ddlUNSTTP.GetValue() || ddlUNSTTP.GetValue() == '') {
                alert('부적합유형을 선택하세요!!');
                ddlUNSTTP.Focus();
                return false;
            }
            if (!txtRQTXT5.GetValue() || txtRQTXT5.GetValue() == '') {
                alert('의뢰사항을 입력하세요!!');
                txtRQTXT5.Focus();
                return false;
            }
        }

        // 품목코드 입력시 명칭조회
        function fn_OnITEMCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                txtITEMNM.SetValue('');
                txtITEMNM.SetText('');
                txtITEMNM.GetMainElement().title = '';
                txtMODELNM.SetValue('');
                txtMODELNM.SetText('');
                txtMODELNM.GetMainElement().title = '';
                return;
            } else
                devCallback.PerformCallback('ITEM|' + s.GetValue());
        }

        // 공정코드 입력시 명칭조회
        function fn_OnWORKCDLostFocus(s, e) {
            if (!s.GetValue() || s.GetValue() == '') {
                txtWORKNM.SetValue('');
                txtWORKNM.SetText('');
                txtWORKNM.GetMainElement().title = '';
                return;
            } else
                devCallback.PerformCallback('WORK|' + s.GetValue());
        }

        // 검사항목, 공정 콜백처리
        function fn_OndevCallback(s, e) {
            if (s.cpResultCode == '99') {
                alert(s.cpResultMsg);
            } else if (s.cpResultCode == 'save') {
                alert(s.cpResultMsg);
                parent.fn_devPopupClose();
            } else {
                var IDCD = s.cpIDCD;
                var IDNM = s.cpIDNM;
                var CODE = s.cpCODE;
                var TEXT = s.cpTEXT;

                var txtIDCD = ASPxClientControl.Cast("txt" + IDCD);
                var txtIDNM = ASPxClientControl.Cast("txt" + IDNM);

                if (CODE != '' && TEXT != '') {
                    txtIDCD.SetValue(CODE);
                    txtIDCD.SetText(CODE);
                    txtIDNM.SetValue(TEXT);
                    txtIDNM.SetText(TEXT);
                    txtIDNM.GetMainElement().title = TEXT;
                } else {
                    txtIDCD.SetValue('');
                    txtIDCD.SetText('');
                    txtIDNM.SetValue('');
                    txtIDNM.SetText('');
                    txtIDNM.GetMainElement().title = '';
                }
            }
        }

        function fn_OnUNSTTPValueChanged(s, e) {
            var val = s.GetValue();
            hidUNSTTP.SetValue(val);
        }

        function fn_OnDEPARTCDalueChanged(s, e) {
            var val = s.GetValue();
            hidDEPARTCD.SetValue(val);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div class="form-horizontal">
                <div class="form-group" style="display: none;">
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
                    <label class="col-sm-1 control-label">관리번호</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtCTRLNO" ClientInstanceName="txtCTRLNO" runat="server" Width="100%"
                            class="form-control input-sm">
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">이상제기일</label>
                    <div class="col-sm-2">
                        <dx:ASPxDateEdit ID="txtRQDATE" ClientInstanceName="txtRQDATE" runat="server" Width="100%" Theme="MetropolisBlue"
                            DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd"
                            class="form-control input-sm">
                        </dx:ASPxDateEdit>
                    </div>
                    <label class="col-sm-1 control-label">회신요구일</label>
                    <div class="col-sm-2">
                        <dx:ASPxDateEdit ID="txtRQRCDT" ClientInstanceName="txtRQRCDT" runat="server" Width="100%" Theme="MetropolisBlue"
                            DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd"
                            class="form-control input-sm">
                        </dx:ASPxDateEdit>
                    </div>
                    <label class="col-sm-1 control-label">발생기간</label>
                    <div class="col-sm-3">
                        <div style="float: left; width: 50%; padding-right: 3px;">
                            <dx:ASPxDateEdit ID="txtOCSTDT" ClientInstanceName="txtOCSTDT" runat="server" Width="100%" Theme="MetropolisBlue"
                                DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd"
                                class="form-control input-sm">
                            </dx:ASPxDateEdit>
                        </div>
                        <div style="float: left; width: 50%;">
                            <dx:ASPxDateEdit ID="txtOCEDDT" ClientInstanceName="txtOCEDDT" runat="server" Width="100%" Theme="MetropolisBlue"
                                DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd"
                                class="form-control input-sm">
                            </dx:ASPxDateEdit>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목코드</label>
                    <div class="col-sm-4">
                        <div style="float: left; width: 40%; padding-right: 3px;">
                            <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%"
                                class="form-control input-sm"
                                OnInit="txtITEMCD_Init">
                                <ClientSideEvents LostFocus="fn_OnITEMCDLostFocus" />
                            </dx:ASPxTextBox>
                        </div>
                        <div style="float: left; width: 60%;">
                            <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%"
                                class="form-control input-sm">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                    <label class="col-sm-1 control-label">공정코드</label>
                    <div class="col-sm-3">
                        <div style="float: left; width: 40%; padding-right: 3px;">
                            <dx:ASPxTextBox ID="txtBANCD" ClientInstanceName="txtBANCD" runat="server" ClientVisible="false" />
                            <dx:ASPxTextBox ID="txtLINECD" ClientInstanceName="txtLINECD" runat="server" ClientVisible="false" />
                            <dx:ASPxTextBox ID="txtWORKCD" ClientInstanceName="txtWORKCD" runat="server" Width="100%"
                                class="form-control input-sm"
                                OnInit="txtWORKCD_Init">
                                <ClientSideEvents LostFocus="fn_OnWORKCDLostFocus" />
                            </dx:ASPxTextBox>
                        </div>
                        <div style="float: left; width: 60%;">
                            <dx:ASPxTextBox ID="txtWORKNM" ClientInstanceName="txtWORKNM" runat="server" Width="100%"
                                class="form-control input-sm">
                                <ClientSideEvents Init="fn_OnControlDisableBox" />
                            </dx:ASPxTextBox>
                        </div>
                    </div>
                    <label class="col-sm-1 control-label">기종명</label>
                    <div class="col-sm-2">
                        <dx:ASPxTextBox ID="txtMODELCD" ClientInstanceName="txtMODELCD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="txtMODELNM" ClientInstanceName="txtMODELNM" runat="server" Width="100%"
                            class="form-control input-sm">
                            <ClientSideEvents Init="fn_OnControlDisableBox" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">Lot No.</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtLOTNO" ClientInstanceName="txtLOTNO" runat="server" Width="100%"
                            class="form-control input-sm">
                        </dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-1 control-label">부적합유형</label>
                    <div class="col-sm-2">
                        <dx:ASPxTextBox ID="hidUNSTTP" ClientInstanceName="hidUNSTTP" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlUNSTTP" ClientInstanceName="ddlUNSTTP" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" OnDataBound="ddlComboBox_DataBound">
                            <ClientSideEvents Init="fn_OnControlDisable" ValueChanged="fn_OnUNSTTPValueChanged" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">대책부서</label>
                    <div class="col-sm-2">
                        <dx:ASPxTextBox ID="hidDEPARTCD" ClientInstanceName="hidDEPARTCD" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlDEPARTCD" ClientInstanceName="ddlDEPARTCD" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" OnDataBound="ddlComboBox_DataBound">
                            <ClientSideEvents Init="fn_OnControlDisable" ValueChanged="fn_OnDEPARTCDalueChanged" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">대책요청</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlMEASGD" runat="server" ClientInstanceName="ddlMEASGD" Width="100%">
                            <Items>
                                <dx:ListEditItem Text="선택" Value="" />
                                <dx:ListEditItem Text="A급" Value="A" />
                                <dx:ListEditItem Text="B급" Value="B" />
                                <dx:ListEditItem Text="C급" Value="C" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">파일첨부</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="txtIMAGESEQ" ClientInstanceName="txtIMAGESEQ" ClientVisible="false" runat="server" Width="100%" class="form-control input-sm" />
                        <button class="btn btn-sm btn-warning" style="width: 100%; padding: 3px 0px;" onclick="fn_AttachFileOpenParam('품질이상제기', 'F', 'T', '<%=F_COMPCD%>'); return false;">
                            <i class="i i-file-plus "></i>
                            <span class="text">파일첨부</span>
                        </button>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">주제</label>
                    <div class="col-sm-11">
                        <dx:ASPxTextBox ID="txtRQTXT1" ClientInstanceName="txtRQTXT1" runat="server" Width="100%" MaxLength="400"
                            class="form-control input-sm">
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">부적합사항</label>
                    <div class="col-sm-5">
                        <dx:ASPxMemo ID="txtRQTXT2" ClientInstanceName="txtRQTXT2" runat="server" Width="100%" Height="60px" MaxLength="400"
                            class="form-control input-sm">
                        </dx:ASPxMemo>
                    </div>
                    <label class="col-sm-1 control-label">의뢰사항</label>
                    <div class="col-sm-5">
                        <dx:ASPxMemo ID="txtRQTXT5" ClientInstanceName="txtRQTXT5" runat="server" Width="100%" Height="60px" MaxLength="400"
                            class="form-control input-sm">
                        </dx:ASPxMemo>
                    </div>
                </div>
                <div class="form-group" style="display: <%if (gsVENDOR){%>display<%} else { %>none<%} %>">
                    <label class="col-sm-1 control-label">발견동기</label>
                    <div class="col-sm-5">
                        <dx:ASPxMemo ID="txtRQTXT3" ClientInstanceName="txtRQTXT3" runat="server" Width="100%" Height="60px" MaxLength="400"
                            class="form-control input-sm">
                        </dx:ASPxMemo>
                    </div>
                    <label class="col-sm-1 control-label">잠정조치</label>
                    <div class="col-sm-5">
                        <dx:ASPxMemo ID="txtRQTXT4" ClientInstanceName="txtRQTXT4" runat="server" Width="100%" Height="60px" MaxLength="400"
                            class="form-control input-sm">
                        </dx:ASPxMemo>
                    </div>
                </div>
            </div>
            <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
                <ClientSideEvents EndCallback="fn_OndevCallback" />
            </dx:ASPxCallback>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
