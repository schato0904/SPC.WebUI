<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="favorites.ascx.cs" Inherits="SPC.WebUI.Resources.controls.login.oto.favorites" %>
<%if (!Page.gsVENDOR) {%>
<div class="line dk hidden-nav-xs"></div>
<div class="hidden-nav-xs padder m-t-sm m-b-sm font-bold text-black">SPC 바로가기</div>
<ul class="nav">
    <li>
        <a href="<%=m_sByPassURL%>">
            <i class="i i-sm i-share3 text-info-dk"></i>
            <span><%=Page.gsCOMPNM%> SPC 바로가기</span>
        </a>
    </li>
</ul>
<%}%>