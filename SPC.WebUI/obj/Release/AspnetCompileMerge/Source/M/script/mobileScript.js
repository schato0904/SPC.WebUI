

function getUrlParams() {
    var params = {};
    window.location.search.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (str, key, value) { params[key] = value; });
    return params;
}

function isEmpty(str) {
    if (typeof str == "undefined" || str == null || str == "")
        return true;
    else
        return false;
}

function fn_MonthYearList(obj, showYear) {
    var months = ["12", "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01"];

    var yearVal = new Date().getFullYear()
    var monthVal = new Date().getMonth() + 1;

    if (monthVal < 10) {
        monthVal = "0" + monthVal;
    }   

    var cnt = yearVal - showYear;
    for (; yearVal > cnt; yearVal--) {
        $.each(months, function (index, value) {
            if (new Date().getFullYear() + monthVal >= yearVal + value) {
                if (new Date().getFullYear() + "-" + monthVal == yearVal + "-" + value) {
                    $(obj).append($('<option selected="selected" />').val(yearVal + "-" + value).html(yearVal + "-" + value));
                }
                else {
                    $(obj).append($('<option />').val(yearVal + "-" + value).html(yearVal + "-" + value));
                }
            }         
        });
    }

    $(obj).selectmenu();
    $(obj).selectmenu('refresh', true);
}