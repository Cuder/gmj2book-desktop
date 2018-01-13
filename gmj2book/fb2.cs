using System;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using XElement = System.Xml.Linq.XElement;

namespace gmj2book
{
    public class Fb2
    {
        private static readonly DateTime CurrentTime = DateTime.UtcNow.Date;
        private static readonly XNamespace Ns = "http://www.gribuser.ru/xml/fictionbook/2.0";
        private const string ProgramSite = "http://nikitakovin.ru/gmj2book/";

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

        private static string BlogTitle(string name)
        {
            return $"Блог «{name}»";
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
                new XElement(Ns + "book-title", BlogTitle(t.BlogName)),
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
                new XElement(Ns + "src-url", ProgramSite),
                new XElement(Ns + "version", "1.0")
            );
        }

        private static XElement WriteAnnotation(string blogUrl)
        {
            return new XElement(Ns + "annotation",
                new XElement(Ns + "p",
                    $"Книга была сгенерирована {CurrentTime:yyyy-MM-dd} при помощи сервиса gmj2book: {ProgramSite}."),
                new XElement(Ns + "empty-line"),
                new XElement(Ns + "p", $"Онлайн-версия блога доступна по адресу: {blogUrl}."),
                new XElement(Ns + "empty-line"),
                new XElement(Ns + "p", "Приятного чтения, друг."));
        }

        private static void WriteRootSection(string blogTitle, string blogUrl)
        {
            RootSection = new XElement(Ns + "section",
                new XElement(Ns + "title", new XElement(Ns + "p", blogTitle)),
                WriteAnnotation(blogUrl));
        }

        private static XElement RootSection { get; set; }

        private static XElement WriteYear(int yr)
        {
            var year = new XElement(Ns + "section",
                new XElement(Ns + "title",
                    new XElement(Ns + "p", yr)));
            RootSection.Add(year);
            return year;
        }

        private static void WritePosts(Task t)
        {
            var stopParsing = false;
            // Счетчики
            var pagesCounter = 0;
            var postsCounter = 0;
            var imagesCounter = 0;
            // Регуляторы записи годов и месяцев
            var yearWritten = 0;
            var monthWritten = 0;
            // var sectionYear = None;
            // var sectionMonth = None;
            // var posts_month = [];

            while (true)
            {
                HtmlNode posts;
                if (pagesCounter == 0)
                {
                    posts = Parser.GetPostsTable(t.FirstPage);
                }
                else
                {
                    var html = Gmj.GetHtml(null, t.BlogUrl + "&sidx=" + pagesCounter).Result;
                    var blogPage = new HtmlDocument();
                    blogPage.LoadHtml(html);
                    posts = Parser.GetPostsTable(blogPage);
                }
                if (posts != null)
                {
                    // Запись постов со страницы
                    var range = Enumerable.Range(0, 10);
                    foreach (var postNumber in range)
                    {
                        // Получить пост и информацию о нем
                        var postData = Parser.GetPostData(posts, postNumber);
                        if (postData != null)
                        {
                            var p = new Post
                            {
                                Id = Parser.GetPostId(postData),
                                Author = Parser.GetPostAuthor(postData),
                                Title = Parser.GetPostTitle(postData),
                                Time = Parser.GetPostDateTime(postData),
                                Message = "",
                                HasImage = true
                            };
                            MessageBox.Show(p.Time.ToString("yy hh:mm"));
                        }
                        else
                        {
                            // Посты закончились, нужно остановить дальнейший парсинг
                            stopParsing = true;
                            MessageBox.Show($@"Parsing stopped");
                            break;
                        }
                    }

                    if (stopParsing)
                    {
                        // Запись постов за последний месяц последнего года
                        break;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    // Запись постов за последний месяц последнего года
                }
                //break;
            }
        }

        // Запись тела книги
        private static XElement WriteBody(Task t)
        {
            WriteRootSection(BlogTitle(t.BlogName), t.BlogUrl);
            return new XElement(Ns + "body", RootSection);
        }

        // Создание FB2
        public void SaveFb2(Task t)
        {
            var filename = $"{t.BlogName}.xml";

            // Удалить временный файл, если вдруг есть
            if (File.Exists(filename)) File.Delete(filename);

            var description = WriteDescription(WriteTitleInfo(t));
            var body = WriteBody(t);

            WritePosts(t);

            var root = WriteRoot(description, body);

            root.Save(filename);
        }
    }
}
