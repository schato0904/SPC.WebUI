<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDate1.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucDate1" %>
<script type="text/javascript">
    function fn_DateCheck1(s, e) {
        hidUCFROMDT1.SetText(txtFROMDT1.GetText());
        hidUCTODT1.SetText(txtTODT1.GetText());
        
        // Changed 추가
        <% if (!string.IsNullOrEmpty(this.Changed) && this.Changed.IndexOf(' ') < 0)
           { %>
        if ( typeof(<%=Changed%>) == 'function' ){
            <%=Changed%>(s, e);
        }
        <% } %>
        
        if ("<%=SingleDate%>" == "True") return false;

        var date = daysBetween(txtFROMDT1.GetDate(), txtTODT1.GetDate())
        var month = monthBetween(txtFROMDT1.GetText(), txtTODT1.GetText())
        if ("<%=MonthOnly%>" == "True") {
            if ("<%=MaxMonth%>" != 0){
                if (Math.abs(month) > "<%=MaxMonth%>") {
                    alert('검색기간은 최대 <%=MaxMonth%>개월 입니다');
                    if (s == txtFROMDT1) {
                        var ToDate = addMonths(txtFROMDT1.GetDate(), parseInt("<%=MaxMonth%>",10)-1);
                        txtTODT1.SetDate(ToDate);
                        hidUCTODT1.SetText(txtTODT1.GetText());
                    }
                    else {
                        var FromDate = addMonths(txtTODT1.GetDate(), parseInt("-<%=MaxMonth%>",10)+1);
                        txtFROMDT1.SetDate(FromDate);
                        hidUCFROMDT1.SetText(txtFROMDT1.GetText());
                    }
                }
             }
        } else {
            if ("<%=MaxDate%>" != 0) {
                if (Math.abs(date) > "<%=MaxDate%>") {
                    alert('검색기간은 최대 <%=MaxDate%>일 입니다');
                    if (s == txtFROMDT1) {
                        var ToDate = addDays(txtFROMDT1.GetDate(), "<%=MaxDate%>");
                        txtTODT1.SetDate(ToDate);
                        hidUCTODT1.SetText(txtTODT1.GetText());
                    }
                    else {
                        var FromDate = addDays(txtTODT1.GetDate(), "-<%=MaxDate%>");
                        txtFROMDT1.SetDate(FromDate);
                        hidUCFROMDT1.SetText(txtFROMDT1.GetText());
                    }
                }
            } else if ("<%=MaxMonth%>" != 0){
                if (Math.abs(month) > "<%=MaxMonth%>") {
                    alert('검색기간은 최대 <%=MaxMonth%>개월 입니다');
                    if (s == txtFROMDT) {
                        var ToDate = convertDateString(getDate(txtFROMDT1.GetDate(), 0, parseInt("<%=MaxMonth%>", 10), -1));
                        txtTODT1.SetDate(ToDate);
                        hidUCTODT1.SetText(txtTODT.GetText());
                    } else {
                        var FromDate = convertDateString(getDate(txtTODT1.GetDate(), 0, parseInt("- <%=MaxMonth%>", 10), 1));
                        txtFROMDT1.SetDate(FromDate);
                        hidUCFROMDT1.SetText(txtFROMDT.GetText());
                    }
                }
            }
        }
    }

    function fn_UCDateInit1(s,e) {
        hidUCFROMDT1.SetText(txtFROMDT1.GetText());
        hidUCTODT1.SetText(txtTODT1.GetText());

        if ("<%=MonthOnly%>" == "True") {
            var calendar = s.GetCalendar();
            calendar.owner = s;
            calendar.GetMainElement().style.opacity = '0';
        }
    }
    function fn_DropDown1(s, e) {
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
    <dx:ASPxTextBox ID="hidUCFROMDT1" ClientInstanceName="hidUCFROMDT1" runat="server" ClientVisible="false" />
    <dx:ASPxDateEdit ID="txtFROMDT1" ClientInstanceName="txtFROMDT1" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%"
        OnInit="txtDate_Init" >
        <ClientSideEvents QueryCloseUp="fn_DateCheck1" DateChanged="fn_DateCheck1" Init="fn_UCDateInit1" DropDown="fn_DropDown1" />
    </dx:ASPxDateEdit>
</div>
<div style="float: left; width: 2%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 48%;" runat="server" id="Todiv">
    <dx:ASPxTextBox ID="hidUCTODT1" ClientInstanceName="hidUCTODT1" runat="server" ClientVisible="false" />
    <dx:ASPxDateEdit ID="txtTODT1" ClientInstanceName="txtTODT1" runat="server" UseMaskBehavior="true" EditFormat="Custom" 
        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%"
        OnInit="txtDate_Init">
        <ClientSideEvents QueryCloseUp="fn_DateCheck1"  DateChanged="fn_DateCheck1" Init="fn_UCDateInit1"  DropDown="fn_DropDown1" />
    </dx:ASPxDateEdit>
</div>

