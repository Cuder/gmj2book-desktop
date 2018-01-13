using System;
using System.Text.RegularExpressions;

namespace gmj2book
{
    public class Post
    {
        public uint Id { get; set; } // Идентификатор сообщения
        public ushort Author { get; set; } // Идентификатор автора сообщения

        private string _title; // Заголовок сообщения
        public string Title
        {
            get => _title;
            set
            {
                // Удалить пробелы в начале и конце заголовка
                var s = value.Trim();

                // Удалить заголовок, если он содержит только спецсимволы
                if (!CheckInput.IsAlphanumericOnly(s)) return;

                // Добавить точку в конец заголовка, если он заканчивается на букву или цифру
                var rgx = new Regex("[A-Za-zА-Яа-я0-9]");
                var last = s[s.Length - 1].ToString();
                if (rgx.IsMatch(last)) s = s + ".";

                _title = s;
            }
        }

        public DateTime Time { get; set; } // Время публикации сообщения
        public string Message { get; set; } // Само сообщение
        public bool HasImage { get; set; } // Картинка к сообщению
    }
}
