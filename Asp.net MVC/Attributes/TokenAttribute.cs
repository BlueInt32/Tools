using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tools.Asp.net_MVC.Attributes
{
	public class TokenAttribute : Attribute
	{
		public string Token { get; set; }
		public TokenAttribute(string token)
		{
			Token = token;
		}
	}
}