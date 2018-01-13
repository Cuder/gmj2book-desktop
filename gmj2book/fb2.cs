using System;
using System.Xml.Linq;
using System.IO;
using XElement = System.Xml.Linq.XElement;

namespace gmj2book
{
    public class Fb2
    {
        // Текущее время
        private static readonly DateTime CurrentTime = DateTime.UtcNow.Date;

        // Инициализация пространства имен FB2
        private static readonly XNamespace Ns = "http://www.gribuser.ru/xml/fictionbook/2.0";

        private static XElement WriteRoot(XElement description, XElement body)
        {
            return new XElement(Ns + "FictionBook", new XAttribute(XNamespace.Xmlns + "xlink", "http://www.w3.org/1999/xlink"),
                description,
                body);
        }

        private static XElement WriteDescription(XElement titleInfo)
        {
            return new XElement(Ns + "description", titleInfo, WriteDocumentInfo());
        }

        // Запись информации о книге
        private static XElement WriteTitleInfo(Task t)
        {
            XElement firstName = null;
            if (t.RealName != null)
                firstName = new XElement(Ns + "first-name", t.RealName);
            XElement lastName = null;
            if (t.RealSurname != null)
                lastName = new XElement(Ns + "last-name", t.RealSurname);

            return new XElement(Ns + "title-info",
                new XElement(Ns + "genre", "nonf_biography", new XAttribute("match", "100")),
                new XElement(Ns + "author", 
                    firstName, 
                    lastName,
                    new XElement(Ns + "nickname", t.BlogName),
                    new XElement(Ns + "home-page", t.BlogUrl),
                    new XElement(Ns + "id", t.BlogId)),
                new XElement(Ns + "book-title", $"Блог «{t.BlogName}»"),
                new XElement(Ns + "lang", "ru"),
                new XElement(Ns + "sequence", new XAttribute("name", "Блоги GMJ"))
            );
        }

        // Запись информации об FB2-файле
        private static XElement WriteDocumentInfo()
        {
            return new XElement(Ns + "document-info",
                new XElement(Ns + "author",
                    new XElement(Ns + "first-name", "Никита"),
                    new XElement(Ns + "last-name", "Ковин"),
                    new XElement(Ns + "nickname", "Cuder"),
                    new XElement(Ns + "home-page", "http://nikitakovin.ru")),
                new XElement(Ns + "program-used", "gmj2book"),
                new XElement(Ns + "date", CurrentTime.ToString("yyyy"), new XAttribute("value", CurrentTime.ToString("yyyy-MM-dd"))),
                new XElement(Ns + "src-url", "http://nikitakovin.ru/gmj2book/"),
                new XElement(Ns + "version", "1.0")
            );
        }

        // Запись тела книги
        private static XElement WriteBody(Task t)
        {
            return new XElement(Ns + "body");
        }

        // Создание FB2
        public void SaveFb2(Task t)
        {
            var filename = $"{t.BlogName}.xml";

            // Удалить временный файл, если вдруг есть
            if (File.Exists(filename)) File.Delete(filename);

            var description = WriteDescription(WriteTitleInfo(t));
            var body = WriteBody(t);

            var root = WriteRoot(description, body);

            root.Save(filename);
        }
    }
}
