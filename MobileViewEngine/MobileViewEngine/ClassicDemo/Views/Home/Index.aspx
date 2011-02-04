<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ClassicDemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
        <div><b>Cache usage: <%: Context.Items[Constants.DebugViewEngineContentKey]%></b><div/>
        <%: Request.Browser.MobileDeviceManufacturer.ToLower()%>
</asp:Content>
