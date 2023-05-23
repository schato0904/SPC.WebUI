<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0401.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0401" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartHeight = 0;

        $(document).ready(function () {
            
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height);
            $(".tdClass").height((height / 3));
            devChart1.SetHeight((height / 3));
            devChart2.SetHeight((height / 3));
            devChart3.SetHeight((height / 3));
            devChart4.SetHeight((height / 3));
            devChart5.SetHeight((height / 3));
            devChart6.SetHeight((height / 3));
            devChart7.SetHeight((height / 3));
            devChart8.SetHeight((height / 3));
            devChart9.SetHeight((height / 3));
            devChart10.SetHeight((height / 3));
            devChart11.SetHeight((height / 3));
            devChart12.SetHeight((height / 3));
            chartHeight = height / 3;
            
            pnlChart.PerformCallback("247|" + chartHeight);
        }

        // 조회
        function fn_OnSearchClick() {
            pnlChart.PerformCallback("247|"+ chartHeight);
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
        function fn_OnExcelClick() { }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {            
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function fn_SelectInspcd() {
            if (hidUCITEMCD.GetText() == "") {
                alert("품목코드를 선택 하세요!!");
                return;
            }

            fn_OnPopupANLS0401(hidUCITEMCD.GetText(),hidINSPCD.GetText());
        }

        function fn_GetInspcdItem(value) {
            //F_ITEMCD;F_SERIALNO;F_INSPDETAIL;F_SIRYO;F_WORKCD
            hidINSPCD.SetText(value)
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search" style="width:990px; ">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate"  />
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>                                     
                    <label class="col-sm-2 control-label">
                        <a href="#" onclick="fn_SelectInspcd()" class="btn btn-default btn-sm">검사항목</a>
                    </label>
                                         
                </div>                
            </div>
        </div>
        <div class="content" style="width:990px">
            <dx:ASPxTextBox ID="hidINSPCD" ClientInstanceName="hidINSPCD" runat="server" ClientVisible="false" />
            <dx:ASPxCallbackPanel ID="pnlChart" runat="server" ClientInstanceName="pnlChart" Enabled="true" Visible="true" Width="100%" OnCallback="pnlChart_Callback" >
                <ClientSideEvents EndCallback="fn_OnEndCallback" />
                <Paddings PaddingBottom="10px"></Paddings>
                <PanelCollection>
                    <dx:PanelContent>
                        <table border="0" style="width:990px; ">
                            <tr>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart3" ClientInstanceName="devChart3" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart4" ClientInstanceName="devChart4" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart5" ClientInstanceName="devChart5" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart6" ClientInstanceName="devChart6" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart7" ClientInstanceName="devChart7" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart8" ClientInstanceName="devChart8" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart9" ClientInstanceName="devChart9" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart10" ClientInstanceName="devChart10" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart11" ClientInstanceName="devChart11" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdClass">
                                    <dx:WebChartControl ID="devChart12" ClientInstanceName="devChart12" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"  Width="247px" >
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
