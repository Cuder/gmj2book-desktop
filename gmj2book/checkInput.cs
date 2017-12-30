using System.Text.RegularExpressions;

namespace gmj2book
{
	public static class CheckInput
	{
        // Введено ли имя блога
	    public static bool IsNull(string str)
		{
			return str.Length == 0;
		}
        /* Содержит ли имя блога запрещенные символы
         Имя блога может содержать:
         -- латинские или кириллические буквы в верхнем или нижнем регистрах;
         -- цифры;
         -- пробельный символ;
         -- дефис;
         -- знак равенства;
         -- знак подчеркивания;
         -- точку. */
	    public static bool ContainsForbiddenChars(string str)
		{
			var regexItem = new Regex("^[а-яА-ЯA-Za-z0-9 \\-=_.]*$");
			return !regexItem.IsMatch(str);
		}
        // Начинается ли имя блога на цифру
	    public static bool StartsWithDigit(string str)
		{
			var c = str[0];
			return char.IsDigit(c);
		}
        // Содержит ли имя блога одновременно и кириллические, и латинские символы
	    public static bool CyrLatinCharacters(string str)
	    {
	        return (Regex.IsMatch(str, @"\p{IsCyrillic}") && Regex.IsMatch(str, @"\p{IsBasicLatin}"));
	    }
        // Относится ли блог автора к общим
        // Открыт ли блог автора для чтения
		/* public static bool incorrectFirstChar(string str)
		{
			char c = str[0];
			var regexItem = new Regex("^[а-яА-ЯA-Za-z]*$");
			return !regexItem.IsMatch(c);
		}*/
	}
}


