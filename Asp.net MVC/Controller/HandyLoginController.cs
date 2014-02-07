using System.Configuration;
using System.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
namespace Tools.Asp.net_MVC.Controller
{
	public class HandyLoginController
	{

		//#region Login Module
		//protected int LoginFailInc()
		//{
		//    if (HttpContext.Application[Request.ServerVariables["REMOTE_ADDR"]] == null)
		//        HttpContext.Application[Request.ServerVariables["REMOTE_ADDR"]] = 1;
		//    else
		//        HttpContext.Application[Request.ServerVariables["REMOTE_ADDR"]] = (int)HttpContext.Application[Request.ServerVariables["REMOTE_ADDR"]] + 1;

		//    return (int)HttpContext.Application[Request.ServerVariables["REMOTE_ADDR"]];
		//}
		//public ViewResult Login()
		//{
		//    return View();
		//}

		//[HttpPost]
		//public ActionResult Login(string login, string mdp)
		//{
		//    if (HttpContext.Application["Freeze"] != null && DateTime.Now < ((DateTime)HttpContext.Application["Freeze"]))
		//    {
		//        ViewBag.Message = "Accès Bloqué.";
		//        return View();
		//    }
		//    HttpContext.Application["Freeze"] = null;
		//    if (login == ConfigurationManager.AppSettings["adminLogin"] && mdp == ConfigurationManager.AppSettings["adminPass"])
		//        Session["logged"] = true;
		//    else
		//    {
		//        ViewBag.Message = "Identifiants incorrects.";
		//        if (LoginFailInc() > 10)
		//        {
		//            HttpContext.Application["Freeze"] = DateTime.Now.AddHours(1);
		//        }
		//        return View();
		//    }
		//    return RedirectToAction("CreateTuto");
		//}
		//#endregion
	}
}
