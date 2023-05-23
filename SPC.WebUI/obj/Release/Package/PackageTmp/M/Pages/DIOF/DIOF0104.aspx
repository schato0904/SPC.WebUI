<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DIOF0104.aspx.cs" Inherits="SPC.WebUI.M.Pages.DIOF.DIOF0104" %>

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
        
        $(document).ready(function () {            
            fn_MonthYearList($("#txtDate"), 5);
            
            $("#machLst option").remove();

            //설비명 조회
            $.ajax({
                type: "POST",
                url: "../../../API/Common/Websvc/mobileSvc.asmx/MACH21_LST",
                //data: "{'MACHIDX' : '" + oParams.MACHIDX + "' }",
                data: "{'MACHIDX' : '' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonData) {
                    var json = eval("(" + jsonData.d + ")");
                    var ItemList = "";
                    $(json).each(function (i) {
                        if (i == 0) {
                            ItemList = '<option value="' + this.F_MACHIDX + '" selected="selected">' + this.F_MACHNM + '</option>'
                        } else {
                            ItemList = '<option value="' + this.F_MACHIDX + '" >' + this.F_MACHNM + '</option>'
                        }
                        $("#machLst").append(ItemList);
                    });

                    $("#machLst").selectmenu();
                    $("#machLst").selectmenu('refresh', true);
                },
                error: function () {
                    alert("Error : MACH21_LST");
                }
            });

            $("#btnSearch").bind("click", function () {
                fn_Search();
            });

            $("#divTable").append('<table style="width: 300%; font-size: 9pt" class="cell-border" cellspacing="0" class="display" id="DataGrid" cellspacing="0">'
                + '<thead><tr>'
                + '<th>점검부위</th><th>점검항목</th><th>점검내용</th><th>점검주기</th>'
                + '<th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th>'
                + '<th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th>'
                + '<th>21</th><th>22</th><th>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th><th>29</th><th>30</th><th>31</th>'
                + '</tr></thead></table>');
        });

        function fn_Search() {
            $("#divTable").remove();

            var weekNm = new Array('일', '월', '화', '수', '목', '금', '토');
            var searchDate = $("#txtDate").val();
            var AppenElement = '<div id="divTable">'
                            + '<table style="width: 600%; font-size: 9pt" class="cell-border" cellspacing="0" class="display" id="DataGrid" cellspacing="0">'
                            + '<thead><tr>'
                            + '<th>점검부위</th><th>점검항목</th><th>점검내용</th><th>점검주기</th>'
            for (var i = 1 ; i <= 31 ; i++) {
                var today = new Date(searchDate + '-' + i).getDay();

                AppenElement += '<th>' + i + '<br/>' + weekNm[today] + '</th>';
            }
            AppenElement += '</tr></thead></table></div>';

            $("#content").append(AppenElement);

            $('#DataGrid').dataTable({
                "autoWidth": false,
                "scrollX": "100%",
                "scrollY": "100%",
                //"scrollXollapse": true,
                //"scrollYollapse": true,
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
                    "url": "../../../API/Common/Websvc/mobileSvc.asmx/QWK_MACH23_LST",
                    "data": { "MONTH": $("#txtDate").val(), "MACHIDX": $("#machLst").val() },
                    "dataType": "json",
                    "dataSrc": "data",
                    error: function (a, b, c) {
                        alert("Err : " + c);
                    }
                }, "columnDefs": [
                    {
                        targets: 0,
                        "width": "60px"
                    },
                    {
                        targets: 1,
                        "width": "60px"
                    },
                    {
                        targets: 2,
                        "width": "60px"
                    },
                    {
                        targets: 3,
                        "width": "60px"
                    }
                ],
                "columns": [
                    { "data": "F_INSPNO", render: function (data, type) { return "<p style='text-align:center; '>" + data + "</p>"; } },
                    { "data": "F_INSPNM", render: function (data, type) { return "<p style='text-align:left; '>" + data + "</p>"; } },
                    { "data": "F_INSPREMARK", render: function (data, type) { return "<p style='text-align:left; '>" + data + "</p>"; } },
                    { "data": "F_CYCLENM", render: function (data, type) { return "<p style='text-align:center; '>" + data + "</p>"; } },
                    {
                        "data": "F_DAY1",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";                                
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY2",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY3",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY4",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY5",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY6",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY7",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY8",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY9",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY10",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY11",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY12",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY13",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY14",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY15",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY16",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY17",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY18",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY19",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY20",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY21",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY22",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY23",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY24",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY25",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY26",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY27",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY28",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY29",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY30",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                    {
                        "data": "F_DAY31",
                        render: function (data, type) {
                            if (data == "X" || data == "--") {
                                return "<p style='text-align:center; '></p>";
                            } else {
                                var array = data.split('|');
                                if (array[1] == "AAG701") {
                                    return "<p style='text-align:center;background-color:green; color:#ffffff;'>" + array[0] + "</p>";
                                } else {
                                    return "<p style='text-align:center;background-color:red;  color:#ffffff;'>" + array[0] + "</p>";
                                }
                            }
                        }
                    },
                ],
                rowCallback: function (row, data, index) {
                    var arr = [];
                    for (var prop in data) {
                        arr.push(data[prop]);
                    }

                    var cnt = 4;
                    for (var i = 17; i <= 47; i++) {
                        
                        if (arr[i] == "X" || arr[i] == "--") {
                            
                        } else {
                            var array = arr[i].split('|');
                            if (array[1] == "AAG701") {
                                $('td:eq('+cnt+')', row).css('background', "green");
                            } else {
                                $('td:eq(' + cnt + ')', row).css('background', "red");
                            }
                        }
                        cnt++
                    }
                },
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
                                    <select class="form-control"  data-mini="true" id="txtDate" ></select>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label for="teamLst">설비</label>
                                </td>
                                <td>
                                    <select data-mini="true" name="machLst" id="machLst" style="z-index:1"></select>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input data-mini="true" type="button" id="btnSearch" style="z-index:1" value="조회" />
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
