using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace gmj2book
{
	public class Task
	{
        //bool site_ru=true;
	    private string _blogName; // имя блога автора
		public string BlogName
		{
			get => _blogName;
		    set {
                // Проверка имени пользователя
                if (CheckInput.IsNull(value))
				{
					AddError("Введите название блога", "blogName");
				}
				else if (CheckInput.StartsWithDigit(value))
				{
				    AddError("Название блога не может начинаться на цифру", "blogName");
				}
				else if (CheckInput.ContainsForbiddenChars(value))
				{
				    AddError("Название блога содержит недопустимые символы", "blogName");
				}
				else if (CheckInput.CyrLatinCharacters(value))
				{
				    AddError("Название блога не может содержать одновременно и латинские, и кириллические буквы", "blogName");
				}
				else if (CheckInput.IsPublic(value))
				{
				    AddError("Этот блог в списке общих. Введите название частного блога", "blogName");
				}
		        var errorsCountAuthor = Errors.Count(s => s.Contains("blogName"));
                if (FirstPage == null && errorsCountAuthor == 0) GetFirstPage(value); // Получение первой страницы блога
                if (BlogId == 0 && errorsCountAuthor == 0) GetBlogId(); // Получение идентификатора блога
                if (BlogId == 0)
				{
				    AddError("Блог не найден", "blogName");
				}   
                else if (Parser.IfBlogClosed(FirstPage))
                {
                    AddError("Блог закрыт для общего доступа. Пока мы можем создавать книжки только для открытых блогов", "blogName");
                }
                if (errorsCountAuthor == 0)
				{
					_blogName = value;
				}
			}
		}

	    public ushort BlogId { get; set; } // идентификатор автора блога

        private void GetBlogId(bool coauthor = false)
        {
            if (coauthor)
                CoauthorId = Parser.GetBlogId(FirstPageCoauthor);
            else
                BlogId = Parser.GetBlogId(FirstPage);
        }

        private string _coauthorName; // имя соавтора блога
	    public string CoauthorName
	    {
	        get => _coauthorName;
	        set
	        {
	            if (CheckInput.IsNull(value)) return;
                // Проверка имени соавтора блога
	            if (value == BlogName)
	            {
	                AddError("Введите имя, отличное от основного имени блога", "coauthorName");
	            }
                else if (CheckInput.StartsWithDigit(value))
	            {
	                AddError("Название блога не может начинаться на цифру", "coauthorName");
	            }
	            else if (CheckInput.ContainsForbiddenChars(value))
	            {
	                AddError("Название блога содержит недопустимые символы", "coauthorName");
	            }
	            else if (CheckInput.CyrLatinCharacters(value))
	            {
	                AddError("Название блога не может содержать одновременно и латинские, и кириллические буквы", "coauthorName");
	            }
	            var errorsCountCoauthor = Errors.Count(s => s.Contains("coauthorName"));
                if (FirstPageCoauthor == null && errorsCountCoauthor == 0) GetFirstPage(value, true); // Получение первой страницы блога
                if (CoauthorId == 0 && errorsCountCoauthor == 0) GetBlogId(true); // Получение идентификатора блога
	            if (CoauthorId == 0)
	            {
	                AddError("Блог не найден", "coauthorName");
                }
                if (errorsCountCoauthor == 0)
	            {
	                _coauthorName = value;
	            }
	        }
	    }
        //ushort coauthor_id;
	    public ushort CoauthorId { get; set; } // идентификатор соавтора блога

        //string real_name;
        //string real_surname;
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

        public List<string[]> Errors { get; set; } = new List<string[]>();

	    private void AddError(string errorText, string param)
		{
			Errors.Insert(0, new string[2] { errorText, param });
		}
	}
}
