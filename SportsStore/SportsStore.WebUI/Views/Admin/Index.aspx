<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SportsStore.Domain.Entities.Product>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Admin : All Products
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>All Products</h1>

    <table class="Grid">
        <tr>
            
            <th>
                Id
            </th>
            <th>
                Name
            </th>
            <th class="NumericCol">
                Price
            </th>
           
            <th>
                Actions
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.ProductId %>
            </td>
            <td>
                <%: Html.ActionLink(item.Name, "Edit", new { item.ProductId }) %> 
              
            </td>
             <td class="NumericCol">
                <%: item.Price.ToString("c") %>
            </td>
            
           
            <td>
                <% using (Html.BeginForm("Delete", "Admin")) { %>
                    <%: Html.Hidden("ProductId", item.ProductId) %>
                    <button type="submit">Delete</button>
                <%} %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Add a new product", "Create") %>
    </p>

</asp:Content>

