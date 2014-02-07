using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nivea.Baby.FrontOffice.Logic.Utils
{

	public static partial class MyExtensions
	{
		private const string MAIL_PATTERN = @"^(([\w\-]+)\.?)+@(([\w\-]+)\.?)+\.([\w]+)$";
		private const string MOBILE_PATTERN = @"^06(\d{2}){4}$";
		private const string TELEPHONE_PATTERN = @"^(01|02|03|04|05|06|07|09)(\d{2}){4}$";
		private const string URL_PATTERN = @"^([a-zA-Z0-9-_])+$";
		private const string P_PATTERN = @"^<p\s?>|</p>$";
		private const string BORING_PATTERN = @"<\s?/?\s?(p|div)\s?>";
		private const string HTML_SPACE_PATTERN = @"^(&nbsp;|\s)+|(&nbsp;|\s)+$";
		private const string OPENING_P_PATTERN = @"<p\s?>";
		private const string CLOSING_P_PATTERN = @"</p>";
		private const string OPENING_DIV_PATTERN = @"<div[^>]*>";
		private const string CLOSING_DIV_PATTERN = @"</div>";
		private const string CODE_POSTAL_PATTERN = @"^(\s?\d)+$";
		private const string URL_VALID_PATTERN = @"^(http)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";

		public static bool IsNullOrInt(this string str)
		{
			int i;
			if (string.IsNullOrEmpty(str))
				return true;
			return int.TryParse(str, out i);
		}

		public static bool IsNullOrPositiveInt(this string str)
		{
			int i;
			if (string.IsNullOrEmpty(str))
				return true;

			if (int.TryParse(str, out i))
				return i > 0;

			return false;
		}

		public static bool IsNullOrPositiveDouble(this string str)
		{
			double d;
			if (string.IsNullOrEmpty(str))
				return true;

			if (double.TryParse(str, out d))
				return d > 0;

			return false;
		}

		public static int? ToNullOrInt(this string str)
		{
			int i;
			if (string.IsNullOrEmpty(str))
				return null;

			if (int.TryParse(str, out i))
				return i;

			return null;
		}

		public static double? ToNullOrDouble(this string str)
		{
			double d;
			if (string.IsNullOrEmpty(str))
				return null;

			if (double.TryParse(str, out d))
				return d;

			return null;
		}

		public static bool IsPositiveInt(this string str)
		{
			int i;
			if (string.IsNullOrEmpty(str))
				return false;

			if (int.TryParse(str, out i))
				return i > 0;

			return false;
		}

		public static int ToInt(this string str)
		{
			int i = 0;
			int.TryParse(str, out i);
			return i;
		}

		public static bool IsPositiveDouble(this string str)
		{
			double d;
			if (string.IsNullOrEmpty(str))
				return false;

			if (double.TryParse(str, out d))
				return d > 0;

			return false;
		}

		public static bool IsNullOrValidUrl(this string str, string extension)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			string pattern = URL_PATTERN.Replace("$", string.Format("\\.{0}$", extension));

			return Regex.IsMatch(str, pattern);
		}

		public static bool IsNullOrValidUrl(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			return Regex.IsMatch(str, URL_PATTERN);
		}

		public static bool IsValidFileName(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			FileInfo fi = new FileInfo(str);
			string name = fi.Name.Replace(fi.Extension, null);

			return Regex.IsMatch(name, URL_PATTERN);
		}

		public static bool IsNullOrValidCodePostal(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			return Regex.IsMatch(str, CODE_POSTAL_PATTERN);
		}

		public static bool IsNullOrValidMail(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			return Regex.IsMatch(str, MAIL_PATTERN);
		}

		public static bool IsValidMail(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return false;

			return Regex.IsMatch(str, MAIL_PATTERN);
		}

		public static bool IsNullOrValidMobile(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			return Regex.IsMatch(str, MOBILE_PATTERN);
		}

		public static bool IsValidMobile(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return false;

			return Regex.IsMatch(str, MOBILE_PATTERN);
		}

		public static bool IsNullOrValidTelephone(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			return Regex.IsMatch(str, TELEPHONE_PATTERN);
		}

		public static bool IsNullOrValidBirthDay(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			DateTime date;

			bool success = DateTime.TryParse(str, out date);

			if (!success)
				return false;
			else
				return date < DateTime.Today;
		}

		public static bool IsValidBirthDay(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return false;

			DateTime date;

			bool success = DateTime.TryParse(str, out date);

			if (!success)
				return false;
			else
				return date < DateTime.Today;
		}

		public static bool IsValidDateTime(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return false;

			DateTime date;

			bool success = DateTime.TryParse(str, out date);

			return success;
		}

		public static DateTime? ToDateTime(this string str)
		{
			try
			{
				DateTime date;

				bool success = DateTime.TryParse(str, out date);

				if (success)
					return date;
			}
			catch
			{

			}

			return null;
		}

		public static string GetWithoutParagraphTag(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			return Regex.Replace(str, P_PATTERN, string.Empty);
		}

		public static string GetWithoutBoringTags(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			str = Regex.Replace(str, OPENING_P_PATTERN, "<BR/>");
			str = Regex.Replace(str, CLOSING_P_PATTERN, "<BR/>");
			str = Regex.Replace(str, OPENING_DIV_PATTERN, "<BR/>");
			str = Regex.Replace(str, CLOSING_DIV_PATTERN, "<BR/>");
			return str;
		}

		public static string HtmlTrim(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			return Regex.Replace(str, HTML_SPACE_PATTERN, string.Empty);
		}

		public static string GetWithoutAnyParagraphTag(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			str = Regex.Replace(str, OPENING_P_PATTERN, string.Empty);
			str = Regex.Replace(str, CLOSING_P_PATTERN, "<BR/>");
			return str;
		}

		public static string Truncate(this string str, int length)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			if (str.Length <= length)
				return str;

			StringBuilder pattern = new StringBuilder();
			pattern.Append("^([^\\f]{0,");
			pattern.Append(length);
			pattern.Append("})\\W");

			return string.Format("{0}...", Regex.Match(str, pattern.ToString()).Groups[1].Value);

		}

		public static string ToTitleCase(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			TextInfo textInfo = new CultureInfo("fr-FR", false).TextInfo;

			return textInfo.ToTitleCase(str);
		}


		public static bool IsValidUrl(this string str)
		{
			Regex _urlRegex = new Regex(URL_VALID_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			return Regex.IsMatch(str, URL_VALID_PATTERN);
		}

		public static string ProperCase(this string original)
		{
			string result = string.Empty;

			if (!string.IsNullOrEmpty(original))
			{
				result = Char.ToUpper(original[0]).ToString();
				if (original.Length > 1)
				{
					result += original.Substring(1).ToLower();
				}
			}

			return result;
		}

	}
}
