<%@ Page Title="OpenID Relying Party, by DotNetOpenAuth" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
	Inherits="System.Web.Mvc.ViewPage" %>
<%--
// -----------------------------------------------------------------------
// <copyright file="Index.aspx" company="Wave Systems Corp.">
// Copyright (C) 2013. Wave Systems Corp. All Rights Reserved.
// </copyright>
// -----------------------------------------------------------------------
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	
    <div class="content-wrapper">
        <h2>Members Area </h2>
	    <p>Congratulations, you have completed the device identification via Nodes&trade; and have been issued an EndpointID.</p>
        <dl class="openIdResponseData">
            <dt>endpointID:</dt><dd><%=Session["FriendlyIdentifier"] %></dd>
            <dt>trustScore:</dt><dd><%=Session["OpenIdResponse_trustScore"] %></dd>
            <dt>needsSetup:</dt><dd><%=Session["OpenIdResponse_needsSetup"] %></dd>
            <dt>setupURL:</dt><dd><a href="<%=Session["OpenIdResponse_setupURL"] %>"><%=Session["OpenIdResponse_setupURL"] %></a></dd>
        </dl>
	    <p>
		    <%=Html.ActionLink("Logout", "logout") %>
	    </p>
   </div>
</asp:Content>
