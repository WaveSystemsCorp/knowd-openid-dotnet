<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%--
// -----------------------------------------------------------------------
// <copyright file="Index.aspx" company="Wave Systems Corp.">
// Copyright (C) 2013. Wave Systems Corp. All Rights Reserved.
// </copyright>
// -----------------------------------------------------------------------
--%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div class="content-wrapper">
        
        <h2>Sample relying party demonstrating Wave's secure endpoint device identity.</h2>
	    
        <% if (User.Identity.IsAuthenticated) { %>
	    <p><b>You are already logged in!</b> Visit the <%=Html.ActionLink("Members", "Index", "User") %> area. </p>
	    <% } else { %>
	    <p>Click <%=Html.ActionLink("Members", "Index", "User") %> to trigger a login. </p>
	    <% } %>
    </div>

</asp:Content>
