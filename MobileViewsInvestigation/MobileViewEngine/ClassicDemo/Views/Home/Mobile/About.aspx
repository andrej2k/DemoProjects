<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ClassicDemo" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
        <div style="color:Red"><b>This is MobileView!!!!</b></div>
        <div><b>Cache usage: <%: Context.Items[Constants.DebugViewEngineContentKey]%></b></div>
        <div><%: Request.Browser.MobileDeviceManufacturer.ToLower()%></div>
</asp:Content>
