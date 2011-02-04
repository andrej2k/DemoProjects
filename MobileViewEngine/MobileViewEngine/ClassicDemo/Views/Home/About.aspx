<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ClassicDemo" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
        <div>Web browser view</div>
        <div>Cache usage: <%: Context.Items[Constants.DebugViewEngineContentKey]%></div>
        <div><%: Request.Browser.MobileDeviceManufacturer.ToLower()%></div>
</asp:Content>
