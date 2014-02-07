using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;

namespace Nivea.OpeNoel.Filters
{
	/// <summary>
	/// Filtre permettant de savoir si on se trouve bien dans un contexte Facebook.
	/// Décorer les actions de controlleur avec ce filtre permet d'effectuer cette validation avant d'entrer dans l'action.
	/// </summary>
	public class FacebookFilter : ActionFilterAttribute
	{
		public bool RedirectIfPageNotLiked { get; set; }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// On bypass completement l'accès facebook si la conf n'est pas à true.
			if (ConfigurationManager.AppSettings["FbEnabled"] != "true")
			{
				return;
			}
			var request = filterContext.RequestContext.HttpContext.Request;


			//vérifie si l'on est bien dans Facebook
			if (request.Params["signed_request"] != null)
			{
				var fb = new FacebookClient();
				
				dynamic sr = fb.ParseSignedRequest(ConfigurationManager.AppSettings["FbAppSecret"], request.Params["signed_request"]);
				filterContext.HttpContext.Session["signedRequest"] = sr;
				filterContext.HttpContext.Session["SignedRequestReceived"] = true;
				if (sr.page != null)
				{
					//page is liked or not ?
					if (!sr.page.liked && RedirectIfPageNotLiked)
					{
						filterContext.Result = new RedirectResult("~/");
					}
				}
			}
			else if (filterContext.HttpContext.Session["SignedRequestReceived"] != null)
			{
				filterContext.HttpContext.Session["SignedRequestReceived"] = false;
				return;
			}
			else
			{
				filterContext.Result = new RedirectResult(ConfigurationManager.AppSettings["FbAppRoot"]);
			}
			base.OnActionExecuting(filterContext);
		}
	}
}