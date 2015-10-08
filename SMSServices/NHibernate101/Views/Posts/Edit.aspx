<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NHibernate101.Models.PostViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit Post</h2>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Fields</legend>
        <p>
            Title:
            <%= Html.TextBox("Title", Model.Post.Title) %>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            Body:
            <%= Html.TextArea("Body", Model.Post.Body) %>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            <%= Html.CheckBox("IsPublic", Model.Post.IsPublic) %>
            Public
        </p>
        <p>
            <%= Html.ActionLink("Categories:", "Index", "Categories")%>
            <% foreach (var item in Model.AllCategories)
               { %>
            <%= Html.CheckBox(item.Category.Id.ToString(), item.IsSelected) %>
            &nbsp;
            <%= Html.Encode(item.Category.Name) %>
            <% } %>
        </p>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
