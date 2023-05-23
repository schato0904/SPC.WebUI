<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDate2.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucDate2" %>
<script type="text/javascript">
    function fn_DateCheck2(s, e) {
        hidUCFROMDT2.SetText(txtFROMDT2.GetText());
        hidUCTODT2.SetText(txtTODT2.GetText());
        
        // Changed 추가
        <% if (!string.IsNullOrEmpty(this.Changed) && this.Changed.IndexOf(' ') < 0)
           { %>
        if ( typeof(<%=Changed%>) == 'function' ){
            <%=Changed%>(s, e);
        }
        <% } %>
        
        if ("<%=SingleDate%>" == "True") return false;

        var date = daysBetween(txtFROMDT2.GetDate(), txtTODT2.GetDate())
        var month = monthBetween(txtFROMDT2.GetText(), txtTODT2.GetText())
        if ("<%=MonthOnly%>" == "True") {
            if ("<%=MaxMonth%>" != 0){
                if (Math.abs(month) > "<%=MaxMonth%>") {
                    alert('검색기간은 최대 <%=MaxMonth%>개월 입니다');
                    if (s == txtFROMDT2) {
                        var ToDate = addMonths(txtFROMDT2.GetDate(), parseInt("<%=MaxMonth%>",10)-1);
                        txtTODT2.SetDate(ToDate);
                        hidUCTODT2.SetText(txtTODT2.GetText());
                    }
                    else {
                        var FromDate = addMonths(txtTODT2.GetDate(), parseInt("-<%=MaxMonth%>",10)+1);
                        txtFROMDT2.SetDate(FromDate);
                        hidUCFROMDT2.SetText(txtFROMDT2.GetText());
                    }
                }
             }
        } else {
            if ("<%=MaxDate%>" != 0) {
                if (Math.abs(date) > "<%=MaxDate%>") {
                    alert('검색기간은 최대 <%=MaxDate%>일 입니다');
                    if (s == txtFROMDT2) {
                        var ToDate = addDays(txtFROMDT2.GetDate(), "<%=MaxDate%>");
                        txtTODT2.SetDate(ToDate);
                        hidUCTODT2.SetText(txtTODT2.GetText());
                    }
                    else {
                        var FromDate = addDays(txtTODT2.GetDate(), "-<%=MaxDate%>");
                        txtFROMDT2.SetDate(FromDate);
                        hidUCFROMDT2.SetText(txtFROMDT2.GetText());
                    }
                }
            } else if ("<%=MaxMonth%>" != 0){
                if (Math.abs(month) > "<%=MaxMonth%>") {
                    alert('검색기간은 최대 <%=MaxMonth%>개월 입니다');
                    if (s == txtFROMDT) {
                        var ToDate = convertDateString(getDate(txtFROMDT2.GetDate(), 0, parseInt("<%=MaxMonth%>", 10), -1));
                        txtTODT2.SetDate(ToDate);
                        hidUCTODT2.SetText(txtTODT.GetText());
                    } else {
                        var FromDate = convertDateString(getDate(txtTODT2.GetDate(), 0, parseInt("- <%=MaxMonth%>", 10), 1));
                        txtFROMDT2.SetDate(FromDate);
                        hidUCFROMDT2.SetText(txtFROMDT.GetText());
                    }
                }
            }
        }
    }

    function fn_UCDateInit2(s,e) {
        hidUCFROMDT2.SetText(txtFROMDT2.GetText());
        hidUCTODT2.SetText(txtTODT2.GetText());

        if ("<%=MonthOnly%>" == "True") {
            var calendar = s.GetCalendar();
            calendar.owner = s;
            calendar.GetMainElement().style.opacity = '0';
        }
    }
    function fn_DropDown2(s, e) {
        if ("<%=MonthOnly%>" == "True") {
            var calendar = s.GetCalendar();
            var fastNav = calendar.fastNavigation;
            fastNav.activeView = calendar.GetView(0, 0);
            fastNav.Prepare();
            fastNav.GetPopup().popupVerticalAlign = "Below";
            fastNav.GetPopup().ShowAtElement(s.GetMainElement())

            fastNav.OnOkClick = function () {
                var parentDateEdit = this.calendar.owner;
                var currentDate = new Date(fastNav.activeYear, fastNav.activeMonth, 1);
                parentDateEdit.SetDate(currentDate);
                parentDateEdit.HideDropDown();
            }

            fastNav.OnCancelClick = function () {
                var parentDateEdit = this.calendar.owner;
                parentDateEdit.HideDropDown();
            }
        }
    }
</script>

<div class="control-label" style="float: left; width: 48%;" runat="server" id="Fromdiv">
    <dx:ASPxTextBox ID="hidUCFROMDT2" ClientInstanceName="hidUCFROMDT2" runat="server" ClientVisible="false" />
    <dx:ASPxDateEdit ID="txtFROMDT2" ClientInstanceName="txtFROMDT2" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%"
        OnInit="txtDate_Init" >
        <ClientSideEvents QueryCloseUp="fn_DateCheck2" DateChanged="fn_DateCheck2" Init="fn_UCDateInit2" DropDown="fn_DropDown2" />
    </dx:ASPxDateEdit>
</div>
<div style="float: left; width: 2%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 48%;" runat="server" id="Todiv">
    <dx:ASPxTextBox ID="hidUCTODT2" ClientInstanceName="hidUCTODT2" runat="server" ClientVisible="false" />
    <dx:ASPxDateEdit ID="txtTODT2" ClientInstanceName="txtTODT2" runat="server" UseMaskBehavior="true" EditFormat="Custom" 
        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%"
        OnInit="txtDate_Init">
        <ClientSideEvents QueryCloseUp="fn_DateCheck2"  DateChanged="fn_DateCheck2" Init="fn_UCDateInit2"  DropDown="fn_DropDown2" />
    </dx:ASPxDateEdit>
</div>

