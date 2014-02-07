<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<ImagineR.OpeNoel.Models.User>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>GetUsers</title>
	<style>
		* { font-family:Arial; }
		#mainList { padding:0; margin:0; list-style: none; font-size:smaller; width:100%;}
		#mainList li.line {  }
		#mainList li ul { list-style: none; height: 30px;}
		#mainList li ul li {float: left;overflow: hidden;height: 30px;line-height: 30px;text-align: center;width: 6%;border-width: 0px 1px 1px 0;border-color: #808080;border-style: solid;}
		#mainList li ul li:first-child { clear:both;border-width: 0px 1px 1px 1px;}
		
		#mainList li.line ul li{ height: 30px; background: teal;   color:White;}
	
	</style>
</head>
<body>
	<ul id="mainList">
	<li class="line">
		<ul>
			<li>FBID</li>
			<li>Nom</li>
			<li>Prenom</li>
			<li>Email</li>
			<li>Tel</li>
			<li>N° abo</li>
			<li>Civilite</li>
			<li>CodePostal</li>
			<li>Ville</li>
			<li>Adresse</li>
			<li>Insc.</li>
			<li>Naissance</li>
			<li>News_Actualites</li>
			<li>News_BP</li>
			<li>Chance Supp</li>
		</ul>
		</li>
		<script runat="server" type="text/C#">
			public string liMaker(string value)
			{
				return string.Format("<li title='{0}'>{0}</li>", value);
			}
    </script>
    <% 
			
	
		foreach (ImagineR.OpeNoel.Models.User user in Model)
	{%>
		<li>
		<ul>
			<%=liMaker(user.FBID) %>
			<%=liMaker(user.Nom)%>
			<%=liMaker(user.Prenom)%>
			<%=liMaker(user.Email)%>
			<%=liMaker(user.Telephone)%>
			<%=liMaker(user.NumAbonne)%>
			<%=liMaker(user.Civilite)%>
			<%=liMaker(user.CodePostal)%>
			<%=liMaker(user.Ville)%>
			<%=liMaker(user.Adresse)%>
			<%=liMaker(String.Format("{0:u}", user.DateInscription))%>
			<%=liMaker(String.Format("{0:u}", user.DateNaissance))%>
			<%=liMaker(user.Newsletter_Actualites ? "Oui" : "Non")%>
			<%=liMaker(user.Newsletter_BP ? "Oui" : "Non")%>
			<%=liMaker(user.ChanceSupp ? "Oui" : "Non")%>
		</ul>
		</li>
	<% } %>
	</ul>
</body>
</html>
