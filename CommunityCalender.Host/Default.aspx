<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CommunityCalender.Host._Default" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Header" runat="server">
    <title>CommunityCalender</title>
</head>
<body style="background-color:Black">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <iframe id="WebAuthControl" name="WebAuthControl" src="http://login.live.com/controls/WebAuth.htm?appid=<%=System.Web.Configuration.WebConfigurationManager.AppSettings["wll_appid"]%>&context=myContext&style=font-size%3A+10pt%3B+font-family%3A+verdana%3B+background%3A+white%3B"
        width="80px" height="20px" marginwidth="0" marginheight="0" align="middle" frameborder="0"
        scrolling="no" style="border-style: hidden; border-width: 0"></iframe>
    <div style="height: 100%;">
        <asp:Silverlight ID="Xaml1" runat="server" Source="~/ClientBin/CommunityCalender.xap"
            MinimumVersion="2.0.31005.0" Width="100%" Height="100%" />
    </div>
    </form>
</body>
</html>
