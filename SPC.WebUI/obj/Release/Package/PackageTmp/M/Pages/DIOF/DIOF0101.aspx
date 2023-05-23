<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DIOF0101.aspx.cs" Inherits="SPC.WebUI.M.Pages.DIOF.DIOF0101" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    
    <link rel="stylesheet" href="../../jquery/jquery.mobile-1.4.5.min.css" />
    <link rel="stylesheet" href="../../jquery/jquery.dataTables.min.css " />
    <link rel="stylesheet" href="../../css/mobileStyle.css" />

    <script src="../../jquery/jquery-1.12.4.min.js"></script>
    <script src="../../jquery/jquery.mobile-1.4.5.min.js"></script>
    <script src="../../jquery/jquery.dataTables.min.js"></script>       
    <script src="../../jquery/jquery-ui.custom.js"></script>
    <script src="../../script/mobileScript.js"></script>

    <script>
        /*
        * jQuery Mobile: jQuery UI Datepicker Monkey Patch
        * http://salman-w.blogspot.com/2014/03/jquery-ui-datepicker-for-jquery-mobile.html
        */
        (function () {
            // use a jQuery Mobile icon on trigger button
            $.datepicker._triggerClass += " ui-btn ui-btn-right ui-icon-carat-d ui-btn-icon-notext ui-corner-all";
            // replace jQuery UI CSS classes with jQuery Mobile CSS classes in the generated HTML
            $.datepicker._generateHTML_old = $.datepicker._generateHTML;
            $.datepicker._generateHTML = function (inst) {
                return $("<div></div>").html(this._generateHTML_old(inst))
                    .find(".ui-datepicker-header").removeClass("ui-widget-header ui-helper-clearfix").addClass("ui-bar-inherit").end()
                    .find(".ui-datepicker-prev").addClass("ui-btn ui-btn-left ui-icon-carat-l ui-btn-icon-notext").end()
                    .find(".ui-datepicker-next").addClass("ui-btn ui-btn-right ui-icon-carat-r ui-btn-icon-notext").end()
                    .find(".ui-icon.ui-icon-circle-triangle-e, .ui-icon.ui-icon-circle-triangle-w").replaceWith(function () { return this.childNodes; }).end()
                    .find("span.ui-state-default").removeClass("ui-state-default").addClass("ui-btn").end()
                    .find("a.ui-state-default.ui-state-active").removeClass("ui-state-default ui-state-highlight ui-priority-secondary ui-state-active").addClass("ui-btn ui-btn-active").end()
                    .find("a.ui-state-default").removeClass("ui-state-default ui-state-highlight ui-priority-secondary").addClass("ui-btn").end()
                    .find(".ui-datepicker-buttonpane").removeClass("ui-widget-content").end()
                    .find(".ui-datepicker-current").removeClass("ui-state-default ui-priority-secondary").addClass("ui-btn ui-btn-inline ui-mini").end()
                    .find(".ui-datepicker-close").removeClass("ui-state-default ui-priority-primary").addClass("ui-btn ui-btn-inline ui-mini").end()
                    .html();
            };
            // replace jQuery UI CSS classes with jQuery Mobile CSS classes on the datepicker div, unbind mouseover and mouseout events on the datepicker div
            $.datepicker._newInst_old = $.datepicker._newInst;
            $.datepicker._newInst = function (target, inline) {
                var inst = this._newInst_old(target, inline);
                if (inst.dpDiv.hasClass("ui-widget")) {
                    inst.dpDiv.removeClass("ui-widget ui-widget-content ui-helper-clearfix").addClass(inline ? "ui-content" : "ui-content ui-overlay-shadow ui-body-a").unbind("mouseover mouseout");
                }
                return inst;
            };
        })();
    </script>
    <script>
        $(document).ready(function () {
            $("#txtDate").prop("readonly", true).datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', 'today');

            $("#teamLst option").remove();
            $.ajax({
                type: "POST",
                url: "../../../API/Common/Websvc/mobileSvc.asmx/QCD72_LST",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonData) {
                    var json = eval("(" + jsonData.d + ")");
                    var ItemList = "";
                    $(json).each(function (i) {
                        if (i == 0) {
                            ItemList = '<option value="' + this.F_BANCD + '" selected="selected">' + this.F_BANNM + '</option>'
                            fn_teamLstChange(this.F_BANCD);
                        } else {
                            ItemList = '<option value="' + this.F_BANCD + '">' + this.F_BANNM + '</option>'
                        }
                        $("#teamLst").append(ItemList);
                    });

                    $("#teamLst").selectmenu();
                    $("#teamLst").selectmenu('refresh', true);
                },
                error: function () {
                    alert("Error : TeamLst");
                }
            });

            $("#teamLst").bind("change", function (a, b) {
                fn_teamLstChange($("#teamLst").val());
            });

            $("#btnSearch").bind("click", function () {
                fn_Search();
            });

            $("#divTable").append('<table style="width: 100%; font-size: 9pt" class="display" id="DataGrid" cellspacing="0"><thead><tr><th>반명</th><th>라인명</th><th>설비코드</th><th>설비명</th><th></th></tr></thead></table>')
        });

        function fn_Search() {

            //var table = $('#DataGrid').dataTable();
            $("#divTable").remove()
            $("#content").append('<div id="divTable"><table style="width: 100%; font-size: 9pt" class="display" id="DataGrid" cellspacing="0"><thead><tr><th>반명</th><th>라인명</th><th>설비코드</th><th>설비명</th><th></th></tr></thead></table></div>')
            //$("#divTable").append('<table style="width: 100%; font-size: 9pt" class="display" id="DataGrid" cellspacing="0"><thead><tr><th>반명</th><th>라인명</th><th>설비코드</th><th>설비명</th><th></th></tr></thead></table>')

            $('#DataGrid').dataTable({
                "scrollX": true,
                "scrollXollapse": true,
                "searching": false,
                "paging": false,
                "ordering": false,
                "info": false,
                "lengthChange": false,
                "bServerSide": true,
                "processing": true,
                "retrieve": true,
                "destroy": true,
                "ajax": {
                    "type": "POST",
                    "url": "../../../API/Common/Websvc/mobileSvc.asmx/MACH21_LST_BY_USR",
                    "data": { "BANCD": $("#teamLst").val(), "LINECD": $("#lineLst").val() },
                    "dataType": "json",
                    "dataSrc": "data",
                    error: function (a, b, c) {
                        alert("Err : " + c);
                    }
                },
                //"ajax": "../../jquery/ajax.txt",
                "columns": [
                                {
                                    "data": "F_BANNM",
                                    render: function (data, type) {
                                        return "<p style='text-align:center; '>"+data+"</p>";
                                    }},
                                {
                                    "data": "F_LINENM",
                                    render: function (data, type) {
                                        return "<p style='text-align:center; '>" + data + "</p>";
                                    }
                                },
                                {
                                    "data": "F_MACHCD",
                                    render: function (data, type) {
                                        return "<p style='text-align:center; '>" + data + "</p>";
                                    }
                                },
                                {
                                    "data": "F_MACHNM",
                                    render: function (data, type) {
                                        return "<p style='text-align:left; '>" + data + "</p>";
                                    }
                                },
                                {
                                    "data": "F_MACHIDX",
                                    "render": function (data, type, row, meta) {
                                        //return '<input type="button" value="조회" onclick="fn_DataTableClick(' + data + ',' + type + ',' + row + ',' + meta + ');" />'
                                        return "<input type='button' value='선택' style='display:inline-block;padding:4px 12px;margin-bottom: 0;font-size: 10px;line-height: 20px;color: #333333;text-align: center;text-shadow: 0 1px 1px rgba(255, 255, 255, 0.75); vertical-align: middle;cursor: pointer;background-color: #f5f5f5;background-image: -moz-linear-gradient(top, #ffffff, #e6e6e6);                                        background-image: -webkit-gradient(linear, 0 0, 0 100%, from(#ffffff), to(#e6e6e6));                                        background-image: -webkit-linear-gradient(top, #ffffff, #e6e6e6);                                        background-image: -o-linear-gradient(top, #ffffff, #e6e6e6);                                        background-image: linear-gradient(to bottom, #ffffff, #e6e6e6);                                        background-repeat: repeat-x;                                        border: 1px solid #cccccc;                                        border-color: #e6e6e6 #e6e6e6 #bfbfbf;                                        border-color: rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.25);                                        border-bottom-color: #b3b3b3;                                        -webkit-border-radius: 4px;                                        -moz-border-radius: 4px;                                        border-radius: 4px;                                       filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffffffff', endColorstr='#ffe6e6e6', GradientType=0);                                        filter: progid:DXImageTransform.Microsoft.gradient(enabled=false);                                        -webkit-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);                                        -moz-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);' onclick=fn_DataTableClick('" + data + "') />"
                                    }
                                }
                ],
                "select": true
            });
        }

        function fn_DataTableClick(data) {
            window.location.href = "DIOF0102.aspx?MACHIDX=" + data + "&DATE=" + $("#txtDate").val() + "&BANCD="+$("#teamLst").val() + "&LINECD=" + $("#lineLst").val();
        }

        function fn_teamLstChange(BANCD) {
            $("#lineLst option").remove();
            $.ajax({
                type: "POST",
                url: "../../../API/Common/Websvc/mobileSvc.asmx/QCD73_LST",
                data: "{'BANCD' : '" + BANCD + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonData) {
                    var json = eval("(" + jsonData.d + ")");
                    var ItemList = "";
                    $(json).each(function (i) {
                        if (i == 0) {
                            ItemList = '<option value="' + this.F_LINECD + '" selected="selected">' + this.F_LINENM + '</option>'
                        } else {
                            ItemList = '<option value="' + this.F_LINECD + '">' + this.F_LINENM + '</option>'
                        }
                        $("#lineLst").append(ItemList);
                    });

                    $("#lineLst").selectmenu();
                    $("#lineLst").selectmenu('refresh', true);
                },
                error: function () {
                    alert("Error : LineLst");
                }
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div id="content" data-role="content">
                <div id="namesDiv" data-role="fieldcontain">
                    <table style="width:100%">
                        <tbody>
                            <tr>
                                <td>
                                    <label for="textinput">점검일자</label>
                                </td>
                                <td>
                                    <input data-mini="true" id="txtDate" type="text" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label for="teamLst">생산팀</label>
                                </td>
                                <td>
                                    <select data-mini="true" name="teamLst" id="teamLst" style="z-index:1"></select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="lineLst">라인</label></td>
                                <td>
                                    <select data-mini="true" name="lineLst" id="lineLst" style="z-index:1"></select>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input data-mini="true" type="button" id="btnSearch" value="검색"  style="z-index:1;" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div id="divTable" data-role="fieldcontain">
                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>
