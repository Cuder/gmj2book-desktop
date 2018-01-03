using System;
using HtmlAgilityPack;

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

        // Проверка на закрытость блога
        public static bool IfBlogClosed(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//span[@id='ctl00_cph1_lblError']") != null;
        }

        // Получение таблицы с постами блога
        public static HtmlNode GetPostsTable(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//table[@class='BlogDG']");
        }
    }
}
