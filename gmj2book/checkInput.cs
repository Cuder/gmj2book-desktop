using System.Text.RegularExpressions;
using System.Linq;

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
	        return Regex.IsMatch(str, "[а-яА-Я]") && Regex.IsMatch(str, "[a-zA-Z]");
	    }
        // Относится ли блог автора к общим
	    public static bool IsPublic(string str)
	    {
	        string[] publicBlogs =
	        {
	            "интервью",
	            "информация",
	            "мы",
	            "анекдоты",
	            "избушка",
	            "достало!",
	            "курилка",
	            "помощь",
	            "про это",
	            "тех.помощь",
	            "топ",
	            "третий тайм",
	            "центр звука",
	            "госреестр",
	            "сонник",
	            "ассоциации",
	            "dtr",
	            "softnet",
	            "siemens"
            };
	        return publicBlogs.Contains(str.ToLower());
	    }
        // Не пустой ли блог автора
	}
}


