﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Tools.CustomActionResults
{
	/// <summary>
	/// Encapsulates a custom action result for sending a csv file directly as a response.
	/// </summary>
	public class SiteMapActionResult : ActionResult
	{
		private readonly string _sitemapContent;
		private readonly Encoding _fileEncoding;


		/// <summary>
		/// Encapsulates a custom action result for sending a sitemap.xml as a response
		/// </summary>
		/// <param name="completefilePath">The Server File to send. Must use \\ as path delimiters to extract the filename. </param>
		/// <param name="fileEncoding"></param>
		public SiteMapActionResult(string sitemapContent, Encoding fileEncoding)
		{
			_sitemapContent = sitemapContent;
			_fileEncoding = fileEncoding;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.ContentType = "application/xml";
			context.HttpContext.Response.ContentEncoding = _fileEncoding;
			//byte[] fileContents = System.IO.File.ReadAllBytes(_completefilePath);
			context.HttpContext.Response.Write(_sitemapContent);
			//context.HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + _pureFileName + "\"");
		}
	}
}
