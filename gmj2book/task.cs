using System.Collections.Generic;

namespace gmj2book
{
	public class Task
	{
		//bool site_ru=true;
	    private string _blogName;
		public string BlogName
		{
			get => _blogName;
		    set {
				// Проверка имени пользователя
				if (CheckInput.IsNull(value))
				{
					AddError("Введите название блога", "blogName");
				}
				else if (CheckInput.ContainsForbiddenChars(value))
				{
					AddError("Название блога содержит недопустимые символы", "blogName");
				}
				else if (CheckInput.IsDigitsOnly(value))
				{
					AddError("Название блога не может состоять из одних цифр", "blogName");
				}
				else if (CheckInput.StartsWithDigit(value))
				{
					AddError("Название блога не может начинаться на цифру", "blogName");
				}
				if (Errors.Count == 0)
				{
					_blogName = value;
				}
			}
		}
		//ushort blog_id;
		//string coauthor_name;
		//ushort coauthor_id;
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
