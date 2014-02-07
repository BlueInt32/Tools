using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sc2TutsBase.Areas.administration.Filters
{
	public class AdminFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!IsConnected)
			{
				filterContext.HttpContext.Response.Redirect("~/administration/login/login");
			}
			base.OnActionExecuting(filterContext);
		}


		public static bool IsConnected
		{
			get
			{
				var sessionState = HttpContext.Current.Session["logged"];
				return sessionState != null && Convert.ToBoolean(sessionState);
			}
		}
	}
}