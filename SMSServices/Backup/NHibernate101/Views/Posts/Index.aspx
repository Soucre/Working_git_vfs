<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Core.Domain.Model.Post>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Posts</h2>

   <table>
        <tr>
            <th></th>
            <th>Title</th>
            <th>Public</th>
            <th>Created on</th>
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.Id  }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.Id })%>
            </td>
            <td>
                <%=Html.Encode(item.Title) %>
            </td>
            <td>
                <%=Html.Encode(item.IsPublic) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreationDate)) %>
            </td>        
            <td>
                <%= Html.ActionLink("Delete", "Delete", new { id = item.Id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

