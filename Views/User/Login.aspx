<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%--
// -----------------------------------------------------------------------
// <copyright file="Login.aspx" company="Wave Systems Corp.">
// Copyright (C) 2013. Wave Systems Corp. All Rights Reserved.
// </copyright>
// -----------------------------------------------------------------------
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	
    <div class="content-wrapper">
        <h2>Login</h2>
        <% if (ViewData["Message"] != null) { %>
	    <div style="color: red;font-weight:bold;">
		    <%= Html.Encode(ViewData["Message"].ToString())%>
	    </div>
	    <% } %>
	    <p>You must log in before entering the Members Area: </p>
	    <form action="Authenticate?ReturnUrl=<%=HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]) %>" method="post">
	    <label for="openid_identifier">Wave Endpoint Registrar: </label>
	    <input id="openid_identifier" name="openid_identifier" size="40" value="<%=System.Configuration.ConfigurationManager.AppSettings["WaveEndpointRegistry"] %>" style="width:500px;"/>
	    <br id="break" style="display:none;"/>
        <input type="submit" value="Login" />
	    </form>
    </div>
	<script type="text/javascript">
	    document.getElementById("openid_identifier").focus();
	    if (window.innerWidth < 500) {
	        $('#break').show();
	    }
	</script>

</asp:Content>
