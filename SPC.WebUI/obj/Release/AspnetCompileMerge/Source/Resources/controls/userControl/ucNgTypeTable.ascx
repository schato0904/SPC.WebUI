<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNgTypeTable.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucNgTypeTable" %>
<%-- 공정검사 일보 중 부적합유형 그리드 --%>
<%-- 
    사용예 :     
            <ucCTF:NgTypeTable id="ucNgTypeTable1" runat="server"></ucCTF:NgTypeTable>
            <!-- 동작 샘플 버튼 -->
            <input type="button" value="click" onclick="btn_click();" />
            <input type="button" value="getValue" onclick="btn_click1();" />
            <input type="button" value="getSum" onclick="btn_click2();" />
            <input type="button" value="clear" onclick="btn_click3();" />
    
        // 목록 조회시
        function btn_click() {
            var id = '<%= ucNgTypeTable1.UniqueID %>';
            var ctrl = window[id];
            ctrl.LoadGrid('', 'PPA401'); // 파라미터: 공정검사ID, 공정코드
        }

        // 그리드 값 가져올때
        function btn_click1() {
            var id = '<%= ucNgTypeTable1.UniqueID %>';
            var ctrl = window[id];
            var result = ctrl.GetValues(); // 반환값유형 : json( F_NGTYPECD, F_NGTYPENM, F_CNT );
            alert(JSON.stringify(result));
        }

        // 수량 합산값 가져올때
        function btn_click2() {
            var id = '<%= ucNgTypeTable1.UniqueID %>';
            var ctrl = window[id];
            var result = ctrl.GetSumCnt(); // 반환값 : 숫자
            alert(result);
        }

        // Clear
        function btn_click3() {
            var id = '<%= ucNgTypeTable1.UniqueID %>';
            var ctrl = window[id];
            ctrl.Clear(); 
        }
     --%>
    <script type="text/javascript">
        $(document).ready(function () {
            var ID = '<%=this.UniqueID%>';
            var panelId = '<%= ucNgTypePanel.UniqueID %>';
            var idListStrId = '<%= hidIdListStr.UniqueID %>';
            if (ID != '') { // && typeof(window[ClientInstanceID]) == 'undefined') {
                window[ID] = new (function (ID, panelId, idListStrId) {
                    // 멤버변수 세팅
                    this.ID = ID;
                    this.panelId = panelId;
                    this.idListStrId = idListStrId;

                    // 멤버함수 세팅
                    this.GetIdListStr = function () {
                        return ASPxClientTextBox.Cast(this.idListStrId).GetText();
                    };

                    this.GetValues = function () {
                        var result = [];
                        // 행이 안만들어졌을 경우 빈값 반환
                        if (typeof (CTF) == 'undefined'
                            || typeof (CTF.UserControl) == 'undefined'
                            || typeof (CTF.UserControl.NgTypeRow) == 'undefined') return result;

                        var idArray = this.GetIdListStr().split('|');
                        for (var i = 0 ; i < idArray.length; ++i) {
                            //alert(UserBlock[idArray[i]]);
                            result.push(JSON.stringify(CTF.UserControl.NgTypeRow[idArray[i]]));
                        }
                        return result;
                    };

                    this.GetSumCnt = function () {
                        var sumCnt = 0;
                        var vals = this.GetValues();
                        if (!vals[0] == "") {
                            for (var i = 0 ; i < vals.length ; ++i) {
                                var j = JSON.parse(vals[i]);
                                sumCnt += isNaN(j.F_CNT) ? 0 : parseInt(j.F_CNT);
                            }
                        }
                        return sumCnt;
                    };

                    this.LoadGrid = function (F_WORKDATE, F_GUBUN, F_ITEMCD, F_WORKCD, F_DAYPRODUCTNO) {
                        var params = 'select|' + F_WORKDATE + ';' + F_GUBUN + ';' + F_ITEMCD + ';' + F_WORKCD + ';' + F_DAYPRODUCTNO;
                        ASPxClientCallbackPanel.Cast(this.panelId).PerformCallback(params);
                    };

                    this.ucNgTypeRowInit = function (rowId, F_NGTYPECD, F_NGTYPENM, F_CNT) {
                        var id = rowId;
                        if (typeof (CTF) == "undefined") CTF = {};
                        if (typeof (CTF.UserControl) == "undefined") CTF.UserControl = {};
                        if (typeof (CTF.UserControl.NgTypeRow) == "undefined") CTF.UserControl.NgTypeRow = {};
                        if (id != '') {
                            CTF.UserControl.NgTypeRow[id] = {
                                "F_NGTYPECD": F_NGTYPECD
                               , "F_NGTYPENM": F_NGTYPENM
                               , "F_CNT": F_CNT
                            };
                        }
                    };

                    this.Clear = function () {
                        var params = 'clear';
                        if (typeof (CTF) != "undefined" && typeof (CTF.UserControl) != "undefined") {
                            CTF.UserControl.NgTypeRow = null;
                            delete CTF.UserControl.NgTypeRow;
                        }
                        ASPxClientCallbackPanel.Cast(this.panelId).PerformCallback(params);
                    };

                    this.ValueChange = function () {
                        <%=ValueChange%>
                    }

                })(ID, panelId, idListStrId);
            }
        });
    </script>
    <%--<div class="container">
        <div class="content">--%>
            <dx:ASPxCallbackPanel ID="ucNgTypePanel" runat="server" CssClass="form-horizontal" OnInit="ucNgTypePanel_Init" OnCallback="ucNgTypePanel_Callback">
                <PanelCollection>
                    <dx:PanelContent>
                        <dx:ASPxTextBox ID="hidIdListStr" runat="server" ClientVisible="false" OnInit="hidIdListStr_Init"></dx:ASPxTextBox>
                        <table style="width:100%;border-collapse:collapse;border:1px solid darkgrey;height:30px;background-color:#c9c9c9;">
                            <tr style="height:100%;">
                                <td style="width:60%;border:1px solid darkgrey;text-align:center;height:100%;">
                                    <label style="font-weight:bold;color:#777777;">부적합유형</label>
                                </td>
                                <td style="width:40%;border:1px solid darkgrey;text-align:center;height:100%;">
                                    <label style="font-weight:bold;color:#777777;">수량</label>
                                </td>
                            </tr>
                        </table>
                        <asp:PlaceHolder ID="pHolder1" runat="server"></asp:PlaceHolder>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
<%--        </div>
    </div>--%>
