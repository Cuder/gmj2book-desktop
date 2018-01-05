using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace gmj2book
{
    public class Fb2
    {
        // Инициализация XMLWriter
        private static readonly XmlWriter Xml = XmlWriter.Create("book.xml");

        // Создание корневого элемента
        private static void CreateRoot()
        {
            const string ns = "http://www.gribuser.ru/xml/fictionbook/2.0";
            const string xlink = "http://www.w3.org/1999/xlink";
            const string prefix = "xlink";
   
            Xml.WriteStartElement("FictionBook", ns);
            Xml.WriteAttributeString("xmlns", prefix, null, value: xlink);
            Xml.WriteEndElement();
        }

        // Добавление описания книги
        private void WriteDescription()
        {

        }

        // Запись тела книги
        private void WriteBody()
        {

        }

        // Создание FB2
        public void SaveFb2()
        {
            Xml.WriteStartDocument();
            CreateRoot();
            //WriteDescription();
            //WriteBody();
            Xml.WriteEndDocument();
            Xml.Close();
        }

        /* Сохранение файла FB2
        public void SaveFb2()
        {
            CreateFb2();
        }*/
    }
}
