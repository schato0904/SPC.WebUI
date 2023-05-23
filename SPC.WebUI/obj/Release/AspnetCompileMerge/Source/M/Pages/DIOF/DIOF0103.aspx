<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DIOF0103.aspx.cs" Inherits="SPC.WebUI.M.Pages.DIOF.DIOF0103" %>

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
        var oParams = "";

        $(document).ready(function () {
            oParams = getUrlParams();
            $("#txtDate").val(oParams.DATE);

            //설비명 조회
            $.ajax({
                type: "POST",
                url: "../../../API/Common/Websvc/mobileSvc.asmx/MACH21_LST",
                data: "{'MACHIDX' : '" + oParams.MACHIDX + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonData) {
                    var json = eval("(" + jsonData.d + ")");
                    var ItemList = "";
                    $(json).each(function (i) {
                        ItemList = '<option value="' + this.F_MACHCD + '" selected="selected">' + this.F_MACHNM + '</option>'
                        $("#machLst").append(ItemList);
                    });

                    $("#machLst").selectmenu();
                    $("#machLst").selectmenu('refresh', true);
                },
                error: function () {
                    alert("Error : MACH21_LST");
                }
            });

            fn_Search();
        });

        function fn_Search() {
            $("#divTable").remove()
            $("#content").append('<div id="divTable"><table class="cell-border" cellspacing="0" style="width: 250%; font-size: 9pt" class="display" id="DataGrid" cellspacing="0"><thead><tr><th>점검<br>부위</th><th>점검<br>항목</th><th>점검<br>내용</th><th>점검<br>방법</th><th>점검<br>규격</th><th>점검<br>결과</th><th>판정</th></tr></thead></table></div>')

            $('#DataGrid').dataTable({
                "autoWidth": false,
                "scrollX": "100%",
                "scrollY": "100%",
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
                    "url": "../../../API/Common/Websvc/mobileSvc.asmx/MACH23_MACH26_LST_SHEET",
                    "data": { 'MACHIDX': oParams.MACHIDX, 'DATE': oParams.DATE },
                    "dataType": "json",
                    "dataSrc": "data",
                    error: function (a, b, c) {
                        alert("Err : " + c);
                    }
                },
                "columnDefs": [
                    {
                        targets: 0,
                        "width": "30px"
                    },
                    {
                        targets: 1,
                        "width": "60px"
                    },
                    {
                        targets: 2,
                        "width": "100px"
                    },
                    {
                        targets: 5,
                        "width": "30px"
                    }
                ],
                "columns": [
                                {
                                    "data": "F_INSPNO",
                                    render: function (data, type) {
                                        return "<p style='text-align:center; '>"+data+"</p>";
                                    }
                                },
                                {
                                    "data": "F_INSPNM",
                                    render: function (data, type) {
                                        return "<p style='text-align:left; '>" + data + "</p>";
                                    }
                                },
                                {
                                    "data": "F_INSPREMARK",
                                    render: function (data, type) {
                                        return "<p style='text-align:left; '>" + data + "</p>";
                                    }
                                },
                                {
                                    "data": "F_INSPWAY",
                                    render: function (data, type) {
                                        return "<p style='text-align:left; '>" + data + "</p>";
                                    }
                                },
                                {
                                    "data": "F_VIEWSTAND",
                                    render: function (data, type) {
                                        return "<p style='text-align:left; '>" + data + "</p>";
                                    }
                                },
                                {
                                    "data": "F_MEASURE",
                                    render: function (data, type) {
                                        return "<p style='text-align:center; '>" + data + "</p>";
                                    }
                                },
                                {
                                    "data": "F_JUDGE",
                                    render: function (data, type) {
                                        if (data == "AAG701") {
                                            return "<p style='text-align:center; '>OK</p>";
                                        } else {
                                            return "<p style='text-align:center; '>NG</p>";
                                        }
                                        
                                    }
                                }
                ],
                "select": true
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
                                    <input data-mini="true" id="txtDate" type="text" readonly="readonly" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label for="teamLst">설비</label>
                                </td>
                                <td>
                                    <select disabled="disabled" data-mini="true" name="teamLst" id="machLst"></select>
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
