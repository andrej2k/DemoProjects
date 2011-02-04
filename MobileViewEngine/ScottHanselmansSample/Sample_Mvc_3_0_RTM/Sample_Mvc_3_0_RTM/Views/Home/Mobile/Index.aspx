<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewBag.Message %></h2>

    <div style="color:Red"><b>MOBILE VIEW!!!!!</b></div>
    <div><%: Request.Browser.MobileDeviceManufacturer.ToLower()%></div>
</asp:Content>
