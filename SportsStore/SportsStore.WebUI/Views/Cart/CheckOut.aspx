﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SportsStore.Domain.Entities.ShippingDetails>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sportstore  :  CheckOut
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Check out now</h2>
    Please enter your details, and we'll ship your goods right away!
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary() %>
        <h3>Ship to</h3>

        <div>Name <%:Html.EditorFor(x => x.Name)%></div>

        <h3>Address</h3>
        <div>Line 1: <%:Html.EditorFor(x => x.Line1)%></div>
        <div>Line 2: <%:Html.EditorFor(x => x.Line2)%></div>
        <div>Line 3: <%:Html.EditorFor(x => x.Line3)%></div>
        <div>City: <%:Html.EditorFor(x => x.City)%></div>
        <div>State: <%:Html.EditorFor(x => x.State)%></div>
        <div>Zip: <%:Html.EditorFor(x => x.Zip)%></div>
        <div>Country: <%:Html.EditorFor(x => x.Country)%></div>

        <h3>Options</h3>
        <label>
            <%:Html.EditorFor(x => x.GiftWrap)%>
            Gift wrap these items
        </label>

        <p align="center"><input type="submit" value="Complete Order" /></p>

    <%}%>

</asp:Content>
