using System.Collections.Generic;

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
		        if (BlogId == 0 && Errors.Count == 0) GetBlogId(value); // Получение идентификатора блога
                if (BlogId == 0)
				{
				    AddError("Блог не найден.", "blogName");
				}
                if (Errors.Count == 0)
				{
					_blogName = value;
				}
			}
		}

	    public ushort BlogId { get; set; } // идентификатор автора блога

        private void GetBlogId(string username, bool coauthor = false)
        {
            var id = Parser.GetBlogId(Gmj.GetFirstPage(username));
            if (coauthor)
            {
                CoauthorId = id;
            }
            else
            {
                BlogId = id;
            }
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
                if (CoauthorId == 0 && Errors.Count == 0) GetBlogId(value, true); // Получение идентификатора блога
	            if (CoauthorId == 0)
	            {
	                AddError("Блог не найден.", "coauthorName");
                }
                if (Errors.Count == 0)
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
        //string first_page; // Yet unknown type
        public List<string[]> Errors { get; set; } = new List<string[]>();

	    private void AddError(string errorText, string param)
		{
			Errors.Insert(0, new string[2] { errorText, param });
		}
	}
}
