using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace gmj2book
{
	public class Task
	{
        //bool site_ru=true;
	    private string _blogName; // имя блога автора
		public string BlogName
		{
			get => _blogName;
		    set { if (AddBlogNameErrors(value) == 0) _blogName = Parser.GetBlogName(FirstPage); }
        }

	    // Проверка имени пользователя
        private int AddBlogNameErrors(string username, bool coauthor = false)
        {
	        var param = coauthor ? "coauthorName" : "blogName";

            // Локальная проверка имени
            if (!coauthor && CheckInput.IsNull(username))
	        {
	            AddError("Введите название блога", param);
	        }
            else if (coauthor && username == BlogName)
            {
                AddError("Введите имя, отличное от основного имени блога", param);
            }
            else if (CheckInput.StartsWithDigit(username))
	        {
	            AddError("Название блога не может начинаться на цифру", param);
	        }
	        else if (CheckInput.ContainsForbiddenChars(username))
	        {
	            AddError("Название блога содержит недопустимые символы", param);
	        }
	        else if (CheckInput.CyrLatinCharacters(username))
	        {
	            AddError("Название блога не может содержать одновременно и латинские, и кириллические буквы", param);
	        }
	        else if (!coauthor && CheckInput.IsPublic(username))
	        {
	            AddError("Этот блог в списке общих. Введите название частного блога", param);
	        }

            var errorCount = Errors.Count(s => s.Contains(param));
            if (errorCount != 0) return errorCount;

            // Получение первой страницы блога и идентификатора блога
            GetFirstPage(username, coauthor); 
            GetBlogId(coauthor);

            // Проверка имени с использованием парсера
            if (!coauthor && BlogId == 0 || coauthor && CoauthorId == 0)
            {
                AddError("Блог не найден", param);
            }
            else if (!coauthor)
            {
                if (Parser.IfBlogClosed(FirstPage))
                {
                    AddError("Блог закрыт для общего доступа. Пока мы можем создавать книжки только для открытых блогов", param);
                }
                else if (Parser.GetPostsTable(FirstPage) == null)
                {
                    AddError("В блоге нет записей", param);
                }
            }

            // Возврат количества ошибок
            return Errors.Count(s => s.Contains(param));
	    }

	    private int AddRealNamesErrors(string name, bool surname = false)
	    {
	        var param = surname ? "realSurname" : "realName";

	        if (CheckInput.ContainsForbiddenChars(name, true))
	        {
	            AddError("Может содержать только буквы и дефис", param);
	        }

            // Возврат количества ошибок
            return Errors.Count(s => s.Contains(param));
        }

        public ushort BlogId { get; set; } // идентификатор автора блога

	    private void GetBlogId(bool coauthor = false)
	    {
	        if (coauthor)
	        {
	            CoauthorId = Parser.GetBlogId(FirstPageCoauthor);
	        }
	        else
	        {
	            BlogId = Parser.GetBlogId(FirstPage);
	            BlogUrl = "http://my.gmj.ru/Blog/Blog.aspx?bid=" + BlogId;
	        }
        }

        private string _coauthorName; // имя соавтора блога

	    public string CoauthorName
	    {
	        get => _coauthorName;
	        set
	        {
	            if (CheckInput.IsNull(value)) return;
	            if (AddBlogNameErrors(value, true) == 0) _coauthorName = value;
	        }
	    }

	    public ushort CoauthorId { get; set; } // идентификатор соавтора блога

	    private string _realName;
        public string RealName
	    {
	        get => _realName;
	        set
	        {
	            if (CheckInput.IsNull(value)) return;
	            if (AddRealNamesErrors(value) == 0) _realName = value;
	        }
	    }

	    private string _realSurname;
	    public string RealSurname
	    {
	        get => _realSurname;
	        set
	        {
	            if (CheckInput.IsNull(value)) return;
	            if (AddRealNamesErrors(value, true) == 0) _realSurname = value;
	        }
	    }

        //bool include_images=true;
        //string book_path;

        public HtmlDocument FirstPage { get; set; } // Первая страница блога автора

	    public HtmlDocument FirstPageCoauthor { get; set; } // Первая страница блога соавтора

        private void GetFirstPage(string username, bool coauthor = false)
        {
            var firstPageHtml = Gmj.GetFirstPage(username);
            if (coauthor)
                FirstPageCoauthor = firstPageHtml;
            else
                FirstPage = firstPageHtml;
        }

        public string BlogUrl { get; set; } // полная ссылка на блог автора

        public List<string[]> Errors { get; set; } = new List<string[]>();

	    private void AddError(string errorText, string param)
		{
			Errors.Insert(0, new string[2] { errorText, param });
		}
	}
}
