using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public static class ExtensionString
    {
		public static string RemoveMaskTelefone(this string value)
		{

			return value.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
		}

		public static string RemoveMask(this string value)
		{

			return value.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace(".", "").Replace(",", "").Replace("/", "");
		}
	}
}
