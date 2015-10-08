<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NHibernate101.Models.PostViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Post Details</h2>

    <fieldset>
            <legend>Fields</legend>
            
            <p>
                Title:
                <%= Html.Encode(Model.Post.Title) %>
            </p>
            <p>
                Body:
                <%= Html.Encode(Model.Post.Body)%>
            </p>
            <p>
                <% if (Model.Post.IsPublic == true)
                   { %>
                    Public post
                <%}
                   else
                   { %>
                    Private post
                <% } %>
            </p>
            <p>Created on:
            <%= Html.Encode(String.Format("{0:g}", Model.Post.CreationDate)) %>
            </p>

            <p>
                Categories:
                <% foreach (var item in Model.Post.Categories)
                   { %>
                     <%= Html.CheckBox(item.Id.ToString()) %> &nbsp; <%= Html.Encode(item.Name) %>
                <% } %>
            </p>
            
        </fieldset>
    <p>
        <%=Html.ActionLink("Edit", "Edit", new { id=Model.Post.Id }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

