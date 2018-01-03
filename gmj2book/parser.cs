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
            if (blogId == action)
            {
                // Блог с таким именем не найден
                blogId = "0";
            }
            return Convert.ToUInt16(blogId);
        }
    }
}
