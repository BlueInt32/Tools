<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web.Hosting" %>
<script runat="server" LANGUAGE="C#">
    void Page_Load(Object Source, EventArgs E)
    {
        txtMsg.InnerHtml = "Framework Version: " + Environment.Version.ToString() + "<br/>AppPath: " + HostingEnvironment.ApplicationPhysicalPath;
    }
</script>
<HTML>
<HEAD>
<TITLE>Demo</TITLE>
</HEAD>
<BODY>
<div id="txtMsg" runat="server"/>
</BODY>
</HTML>