using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Asp.net_MVC.Attributes;

namespace Tools.Helpers
{
	public class AttributesHelper
	{
		public static bool IsIgnoredInDropDownLists<TEnum>(TEnum value)
		{
			var memInfo = typeof(TEnum).GetMember(value.ToString());
			var attributes = memInfo[0].GetCustomAttributes(typeof(IgnoreInDropDownListsAttribute), false);
			return attributes.Count() > 0;
		}

		public static string GetToken<TEnum>(TEnum value)
		{
			var memInfo = typeof(TEnum).GetMember(value.ToString());
			var attributes = memInfo[0].GetCustomAttributes(typeof(TokenAttribute), false);
			return ((TokenAttribute)attributes[0]).Token;
		}
		public static List<string> GetTokensList<TEnum>()
		{
			IEnumerable<TEnum> enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
			return enumValues.ToList().ConvertAll(GetToken);

		}
		public static string GetTokens<TEnum>(string separator)
		{
			return string.Join(separator, GetTokensList<TEnum>().ToArray());
		}

		public static TEnum GetEnumValueFromToken<TEnum>(string token)
		{
			List<string> tokensList = GetTokensList<TEnum>();
			int index = tokensList.IndexOf(token);
			return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList()[index];
		}
	}
}
