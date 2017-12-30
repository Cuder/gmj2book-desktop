using System.Text.RegularExpressions;

namespace gmj2book
{
	/// <summary>
	/// Description of checkInput.
	/// </summary>
	public static class checkInput
	{
		public static bool isNull(string str)
		{
			return str.Length == 0;
		}
		public static bool containsForbiddenChars(string str)
		{
			var regexItem = new Regex("^[а-яА-ЯA-Za-z0-9 \\-=_.]*$");
			return !regexItem.IsMatch(str);
		}
		public static bool isDigitsOnly(string str)
		{
		    foreach (char c in str)
		    {
		    	if (c < '0' || c > '9') return false;
		    }
		    return true;
		}
		public static bool startsWithDigit(string str)
		{
			char c = str[0];
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


