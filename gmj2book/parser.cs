using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace gmj2book
{
    public static class Parser
    {
        // Получение ID блога из его страницы
        public static ushort GetBlogId(HtmlDocument doc)
        {
            var action = doc.DocumentNode.SelectSingleNode("//form[@id='aspnetForm']").Attributes["action"].Value;
            var blogId = action.Substring(action.LastIndexOf('=') + 1);
            if (blogId == action) blogId = "0"; // Блог с таким именем не найден
            return Convert.ToUInt16(blogId);
        }

        // Получение имени блога из его страницы
        public static string GetBlogName(HtmlDocument doc)
        {
            var title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
            var reg = new Regex("&quot;(.*)&quot;");
            return reg.Matches(title)[0].Groups[1].ToString();
        }

        // Проверка на закрытость блога
        public static bool IfBlogClosed(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//span[@id='ctl00_cph1_lblError']") != null;
        }

        // Получить таблицу с постами блога
        public static HtmlNode GetPostsTable(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//table[@class='BlogDG']");
        }

        /* Получить данные поста из таблицы с постами блога
           Где postNumber — номер сообщения на странице (начиная с 0) */
        public static HtmlNode GetPostData(HtmlNode posts, int postNumber)
        {
            try
            {
                return posts.SelectNodes(".//table[@class='BlogT']")[postNumber];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        // Получить идентификатор сообщения из данных поста
        public static uint GetPostId(HtmlNode postData)
        {
            var href = new Uri("http://" + postData.SelectSingleNode(".//td[@align='right']/a").Attributes["href"].Value);
            var s = Gmj.GetId(href);
            return Convert.ToUInt32(s);
        }

        // Получить идентификатор автора сообщения из данных поста
        public static ushort GetPostAuthor(HtmlNode postData)
        {
            var href = new Uri("http:/" + postData.SelectSingleNode(".//td[@align='left']/a").Attributes["href"].Value);
            var s = Gmj.GetId(href, "bid");
            return Convert.ToUInt16(s);
        }

        // Получить заголовок поста
        public static string GetPostTitle(HtmlNode postData)
        {
            return postData.SelectSingleNode(".//th[@align='left']").InnerText;
        }

        // Получить время публикации поста
        public static DateTime GetPostDateTime(HtmlNode postData)
        {
            var time = postData.SelectSingleNode(".//th[@align='right']").InnerText; // Время в формате "31 янв 2011, 15:23"
            return Gmj.FormatTime(time);
        }

        // Получить сообщение поста

        // Получить информацию о наличии картинки в посте
    }
}
