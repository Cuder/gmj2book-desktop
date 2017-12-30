using System.Text.RegularExpressions;

namespace gmj2book
{
	public static class CheckInput
	{
		public static bool IsNull(string str)
		{
			return str.Length == 0;
		}
		public static bool ContainsForbiddenChars(string str)
		{
			var regexItem = new Regex("^[а-яА-ЯA-Za-z0-9 \\-=_.]*$");
			return !regexItem.IsMatch(str);
		}
		public static bool IsDigitsOnly(string str)
		{
		    foreach (var c in str)
		    {
		    	if (c < '0' || c > '9') return false;
		    }
		    return true;
		}
		public static bool StartsWithDigit(string str)
		{
			var c = str[0];
			return char.IsDigit(c);
		}
		/* public static bool incorrectFirstChar(string str)
		{
			char c = str[0];
			var regexItem = new Regex("^[а-яА-ЯA-Za-z]*$");
			return !regexItem.IsMatch(c);
		}*/
	}
}


