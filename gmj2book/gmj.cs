﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace gmj2book
{
    public static class Gmj
    {
        // Инициализация HTTP Client
        private static readonly HttpClient Client = new HttpClient();

        // Инициализация HTML Agility Pack
        private static readonly HtmlDocument Document = new HtmlDocument();

        // Получение строки с HTML
        public static async Task<string> GetHtml(Dictionary<string, string> values, string url = "http://my.gmj.ru")
        {
            if (values == null) values = new Dictionary<string, string>();
            var content = new FormUrlEncodedContent(values);
            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsync(url, content).ConfigureAwait(false);
            }
            catch (HttpRequestException webExcp)
            {
                MessageBox.Show(webExcp.Message);
                return null;
            }
            try
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (WebException webExcp)
            {
                MessageBox.Show(webExcp.Message);
                return null;
            }
        }

        // Получение первой страницы блога
        public static HtmlDocument GetFirstPage(string username)
        {
            // Первый запрос HTML для получения __VIEWSTATE и __EVENTVALIDATION
            var html = GetHtml(null).Result;
            if (html == null) return null;

            // Преобразование строки HTML в тип HtmlDocument
            Document.LoadHtml(html);

            // Получение __VIEWSTATE и __EVENTVALIDATION
            var viewstate = Document.DocumentNode.SelectSingleNode("//input[@id='__VIEWSTATE']").Attributes["value"].Value;
            var eventvalidation = Document.DocumentNode.SelectSingleNode("//input[@id='__EVENTVALIDATION']").Attributes["value"].Value;

            // Формирование заголовков для второго запроса
            var formData = new Dictionary<string, string>
            {
                { "__VIEWSTATE", viewstate },
                { "__EVENTVALIDATION", eventvalidation },
                { "ctl00$tbxQGo", username},
                { "ctl00$ibQGo.x", "10"},
                { "ctl00$ibQGo.y", "5"},
                { "ctl00$cph1$sw", "rbSBlog"}
            };

            // Второй запрос для получения HTML с первой страницей блога
            var firstPage = GetHtml(formData).Result;
            if (firstPage == null) return null;

            // Преобразование строки HTML в тип HtmlDocument
            Document.LoadHtml(firstPage);
            return Document;
        }

        // Получить ID поста (type=rid) или автора поста (type=bid) из ссылки
        public static string GetId(Uri href, string type = "rid")
        {
            return HttpUtility.ParseQueryString(href.Query).Get(type);
        }

        // Преобразовать строку времени в формат DateTime
        public static DateTime FormatTime(string time)
        {
            var months = new Dictionary<string, string>
            {
                {" янв ", " 01 "},
                {" фев ", " 02 "},
                {" мар ", " 03 "},
                {" апр ", " 04 "},
                {" май ", " 05 "},
                {" июн ", " 06 "},
                {" июл ", " 07 "},
                {" авг ", " 08 "},
                {" сен ", " 09 "},
                {" окт ", " 10 "},
                {" ноя ", " 11 "},
                {" дек ", " 12 "}
            };
            foreach (var pair in months)
            {
                if (!time.Contains(pair.Key)) continue;
                time = time.Replace(pair.Key, pair.Value);
            }
            return DateTime.ParseExact(time, "d MM yyyy, H:mm", null);
        }
    }
}
