using System.Collections.Generic;

namespace gmj2book
{
	/// <summary>
	/// Description of task.
	/// </summary>
	public class Task
	{
		//bool site_ru=true;
		string blog_name;
		public string Blog_name
		{
			get { return blog_name; }
			set {
				// Проверка имени пользователя
				if (checkInput.isNull(value))
				{
					AddError("Введите название блога", "blogName");
				}
				else if (checkInput.containsForbiddenChars(value))
				{
					AddError("Название блога содержит недопустимые символы", "blogName");
				}
				else if (checkInput.isDigitsOnly(value))
				{
					AddError("Название блога не может состоять из одних цифр", "blogName");
				}
				else if (checkInput.startsWithDigit(value))
				{
					AddError("Название блога не может начинаться на цифру", "blogName");
				}
				if (errors.Count == 0)
				{
					blog_name = value;
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
		List<string[]> errors = new List<string[]>();
		public List<string[]> Errors
		{
			get { return errors; }
			set { errors = value; }
		}
		void AddError(string error_text, string param)
		{
			errors.Insert(0, new string[2] { error_text, param });
		}
	}
}
