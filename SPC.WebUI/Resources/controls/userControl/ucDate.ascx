<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDate.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucDate" %>
<script type="text/javascript">
    function fn_DateCheck(s, e) {
        hidUCFROMDT.SetText(txtFROMDT.GetText());
        hidUCTODT.SetText(txtTODT.GetText());
        
        // Changed 추가
        <% if (!string.IsNullOrEmpty(this.Changed) && this.Changed.IndexOf(' ') < 0)
           { %>
        if ( typeof(<%=Changed%>) == 'function' ){
            <%=Changed%>(s, e);
        }
        <% } %>

        if ("<%=SingleDate%>" == "True") {
            
            hidUCFROMDT.SetText(txtFROMDT.GetText());
            hidUCTODT.SetText(txtFROMDT.GetText());
        }else{
        

            var date = daysBetween(txtFROMDT.GetDate(), txtTODT.GetDate())
            var month = monthBetween(txtFROMDT.GetText(), txtTODT.GetText())
            if ("<%=MonthOnly%>" == "True") {
                if ("<%=MaxMonth%>" != 0){
                    if (Math.abs(month) > "<%=MaxMonth%>") {
                        alert('검색기간은 최대 <%=MaxMonth%>개월 입니다');
                        if (s == txtFROMDT) {
                            var ToDate = addMonths(txtFROMDT.GetDate(), parseInt("<%=MaxMonth%>",10)-1);
                            txtTODT.SetDate(ToDate);
                            hidUCTODT.SetText(txtTODT.GetText());
                        }
                        else {
                            var FromDate = addMonths(txtTODT.GetDate(), parseInt("-<%=MaxMonth%>",10)+1);
                            txtFROMDT.SetDate(FromDate);
                            hidUCFROMDT.SetText(txtFROMDT.GetText());
                        }
                    }
                }
            } else {
                if ("<%=MaxDate%>" != 0) {
                    if (Math.abs(date) > "<%=MaxDate%>") {
                        alert('검색기간은 최대 <%=MaxDate%>일 입니다');
                        if (s == txtFROMDT) {
                            var ToDate = addDays(txtFROMDT.GetDate(), "<%=MaxDate%>");
                            txtTODT.SetDate(ToDate);
                            hidUCTODT.SetText(txtTODT.GetText());
                        }
                        else {
                            var FromDate = addDays(txtTODT.GetDate(), "-<%=MaxDate%>");
                            txtFROMDT.SetDate(FromDate);
                            hidUCFROMDT.SetText(txtFROMDT.GetText());
                        }
                    }
                } else if ("<%=MaxMonth%>" != 0){
                    if (Math.abs(month) > "<%=MaxMonth%>"&&Math.abs(date)>"<%=MaxMonth%>"*30) {
                        alert('검색기간은 최대 <%=MaxMonth%>개월 입니다');
                        if (s == txtFROMDT) {
                            var ToDate = convertDateString(getDate(txtFROMDT.GetDate(), 0, parseInt("<%=MaxMonth%>", 10), -1));
                            txtTODT.SetDate(ToDate);
                            hidUCTODT.SetText(txtTODT.GetText());
                        } else {
                            var FromDate = convertDateString(getDate(txtTODT.GetDate(), 0, parseInt("-<%=MaxMonth%>", 10), 1));
                            txtFROMDT.SetDate(FromDate);
                            hidUCFROMDT.SetText(txtFROMDT.GetText());
                        }
                    }
                }
            }
        }

        
    }

    function fn_UCDateInit(s,e) {
        hidUCFROMDT.SetText(txtFROMDT.GetText());
        hidUCTODT.SetText(txtTODT.GetText());

        if ("<%=MonthOnly%>" == "True") {
            var calendar = s.GetCalendar();
            calendar.owner = s;
            calendar.GetMainElement().style.opacity = '0';
        }
    }
    function fn_DropDown(s, e) {
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
    <dx:ASPxTextBox ID="hidUCFROMDT" ClientInstanceName="hidUCFROMDT" runat="server" ClientVisible="false" />
    <dx:ASPxDateEdit ID="txtFROMDT" ClientInstanceName="txtFROMDT" runat="server" UseMaskBehavior="true" EditFormat="Custom"  
        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%"
        OnInit="txtDate_Init" >
        <ClientSideEvents QueryCloseUp="fn_DateCheck" DateChanged="fn_DateCheck" Init="fn_UCDateInit" DropDown="fn_DropDown" />
    </dx:ASPxDateEdit>
</div>
<div style="float: left; width: 2%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 48%;" runat="server" id="Todiv">
    <dx:ASPxTextBox ID="hidUCTODT" ClientInstanceName="hidUCTODT" runat="server" ClientVisible="false" />
    <dx:ASPxDateEdit ID="txtTODT" ClientInstanceName="txtTODT" runat="server" UseMaskBehavior="true" EditFormat="Custom" 
        CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" Theme="MetropolisBlue" Width="100%"
        OnInit="txtDate_Init">
        <ClientSideEvents QueryCloseUp="fn_DateCheck"  DateChanged="fn_DateCheck" Init="fn_UCDateInit"  DropDown="fn_DropDown" />
    </dx:ASPxDateEdit>
</div>

