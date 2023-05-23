<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS1001POP.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.Popup.MEAS1001POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';        
        var parentCallback = null;
        var callobj = window;
        var rowMaxCount;

        $(document).ready(function () {
            var parentCallbackNm = '<%=parentCallback%>';
            if ("<%=@Type%>" == "UC") {
                if (parentCallbackNm != '' && typeof (parent) != "undefined" && typeof (parent.window['<%=parentCallback%>'].fn_SetUCControl) == "function") {
                    parentCallback = parent[parentCallbackNm];
                }
                parentCallback = parent.window['<%=parentCallback%>'].fn_SetUCControl;
                callobj = parent.window['<%=parentCallback%>'];
            } else {
                if (parentCallbackNm != '' && typeof (parent) != "undefined" && typeof (parent['<%=parentCallback%>']) == "function") {
                    parentCallback = parent[parentCallbackNm];
                }
                parentCallback = parent['<%=parentCallback%>']
            }

            var partInfoStr = '<%=F_PART_INFO%>';
            if (partInfoStr != '') {
                $(partInfoStr.split(';')).each(function (seq, info) {
                    if (info == "")
                        return true;

                    var values = info.split('|');

                    if (values.length == 1)
                        return true;

                    fn_CreatePartInfo(values[0], values[1]);
                });
            }
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() { }

        // 확인
        function fn_OnAcceptClick() {
            var returnValue = "";
            var bAccept = true;
            var textHash = {};

            $.each($('#tblPartInfo tr'), function () {
                var partnm = $(this).find('.partnm');
                var cnt = $(this).find('.cnt');
                var key = "";

                if (partnm.val() == "" && cnt.val() == "") {
                    return true;
                }

                if (partnm.val() == "") {
                    bAccept = false;
                    alert("부속품명은 필수입력항목입니다.");
                    partnm.focus();
                    return false;
                }

                if (cnt.val() == "") {
                    bAccept = false;
                    alert("수량은 필수입력항목입니다.");
                    cnt.focus();
                    return false;
                }

                key = $.trim(partnm.val());

                if (textHash[key]) {
                    bAccept = false;
                    alert(key + "는(은) 중복된 부속품명입니다.");
                    partnm.focus();
                    return false;
                } else {
                    textHash[key] = true;
                }

                returnValue = returnValue + partnm.val() + "|" + cnt.val() + ";"
            });

            if (!bAccept) return;

            if (parentCallback != null && returnValue != "") {
                parentCallback.apply(callobj, [returnValue]);
            }

            parent.fn_devPopupClose();
        }

        // 조회
        function fn_OnSearchClick() {}

        // 입력
        function fn_OnNewClick() {
            fn_CreatePartInfo("", "");
        }

        // 수정
        function fn_OnModifyClick() {}

        // 저장
        function fn_OnSaveClick() { }

        // 취소
        function fn_OnCancelClick() {}

        // 삭제
        function fn_OnDeleteClick() {
            if ($('#tblPartInfo input[type="checkbox"]:checked').length < 1) {
                alert("선택된 항목이 없습니다.");
                return;
            }

            var deleteRow = [];
            $('#tblPartInfo input[type="checkbox"]:checked').each(function () {
                $chk = $(this);               
                deleteRow.push($chk.val());
            });

            for (var del in deleteRow) {
                $('.' + deleteRow[del]).remove();
            }

            $('#chkSelect').prop('checked', false);
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

        // Grid End Callback
        function fn_OnEndCallback(s, e) { }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) { }

        // Validate
        function fn_OnValidate(s, e) { }

        // 부속품 테이블 목록 생성
        function fn_CreatePartInfo(partnm, cnt) {
            var tmpl = $('#partInfoTmpl').html();
            var rowCount = parseInt($('#tblPartInfo tr').length);
            if (rowMaxCount == null) {
                rowMaxCount = rowCount;
            }
            else {
                rowMaxCount++;
            }

            var trClassName = 'tr_' + rowMaxCount;  
            $('#tblPartInfo tbody').append(tmpl);

            $.each($('#tblPartInfo tr:eq(' + rowCount +')'), function () {
                $tr = $(this);
                $tr.prop('class', trClassName);
            });

            $.each($('.' + trClassName + ' > td'), function () {
                $(this).children().each(function () {
                    if (this.tagName == 'INPUT') {
                        if ($(this).prop('type') == "checkbox") {
                            $(this).prop('checked', false);
                            $(this).val(trClassName);
                        }

                        if ($(this).hasClass('partnm')) {
                            $(this).val(partnm);
                        }
                        else if ($(this).hasClass('cnt')) {
                            $(this).val(cnt);
                        }
                    }
                });
            });
        }

        function fn_OnSelectAll() {
            var checked = $('#chkSelect').is(':checked');

            $('#tblPartInfo input[type="checkbox"]').each(function () {
                var $chk = $(this);
                $chk.prop('checked', checked);
            });
        }
    </script>

    <script type="text/template" id="partInfoTmpl">
        <tr>
            <td style="text-align:center;"><input class="view" type="checkbox" /></td>
            <td><input class="partnm" name="partnm" type="text" style="width:100%;"/></td>
            <td style="text-align:right;"><input class="cnt" name="cnt" type="text" style="width:100%;" onkeydown="fn_ValidateOnlyNumberAbs(this, event);"/></td>
        </tr>
    </script>

    <style type="text/css">
        .tblPartHeader th {
            border-left: 1px solid #696969;
            border-top: 1px solid #696969;
            border-right: 1px solid #696969;
            border-bottom: 1px solid #696969;
            height:30px;
            text-align:center;
            background-color: #808080;
            color:#FFFFFF;
        }

        .tblPart td {
            border-left: 1px solid #696969;            
            border-right: 1px solid #696969;
            border-bottom: 1px solid #696969;
            height:30px;
            padding: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidF_MS01MID" ClientInstanceName="hidF_MS01MID" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidOpenType" ClientInstanceName="hidOpenType" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <div style="width:100%;">
                <table class="tblPartHeader" style="width:100%;border-collapse:collapse;empty-cells:show;table-layout:fixed;border-collapse:collapse;">
                    <colgroup>
					    <col style="width: 35px;" />
					    <col style="width: 600px;" />
					    <col style="width: auto;" />
                        <col style="width: 18px;" />
                    </colgroup>
                    <thead>
                        <tr style="height:30px;">
                            <th style="text-align:center;border-left:1px solid #696969;border-top:1px solid #696969;border-right:1px solid #696969;"><input type="checkbox" id="chkSelect" name="chkSelect" onclick="fn_OnSelectAll();" /></th>
                            <th style="text-align:center;border-left:1px solid #696969;border-top:1px solid #696969;border-right:1px solid #696969;">부속품명</th>
                            <th style="text-align:center;border-left:1px solid #696969;border-top:1px solid #696969;border-right:1px solid #696969;" colspan="2">수량</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div style="width:100%;overflow-y:scroll;overflow-x:hidden;height:391px;">
                <table id="tblPartInfo" class="tblPart" style="width: 100%;table-layout:fixed;border-collapse:collapse;">
                    <colgroup>
					    <col style="width: 35px;" />
					    <col style="width: 600px;" />
					    <col style="width: auto;" />
                    </colgroup>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="form-group" style="width:100%;text-align:right;">
                <label class="col-sm-4 control-label" style="color: red;"></label>
                <label class="col-sm-8 control-label" style="color: red;">* 확인 또는 삭제 후 반드시 부모창의 저장버튼을 눌러 저장해야 반영됩니다.</label>
            </div>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
