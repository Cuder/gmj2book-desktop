import globals
import GMJFunctions
import parser
import sys
import printing
from lxml import etree as et
from datetime import datetime

text = globals.yaml_load()


def create_root():
    return et.Element("FictionBook", nsmap={
        None: "http://www.gribuser.ru/xml/fictionbook/2.0",
        "xlink": "http://www.w3.org/1999/xlink"
    })


def write_author(parent_node, nickname, site, first_name=None, last_name=None, blog_id=0):
    author = et.SubElement(parent_node, "author")
    if first_name:
        et.SubElement(author, "first-name").text = first_name
    if last_name:
        et.SubElement(author, "last-name").text = last_name
    et.SubElement(author, "nickname").text = nickname
    et.SubElement(author, "home-page").text = site
    if blog_id != 0:
        et.SubElement(author, "id").text = str(blog_id)


def write_language(parent_node, site):
    if site == 1:
        et.SubElement(parent_node, "lang").text = "ru"
    elif site == 2:
        et.SubElement(parent_node, "lang").text = "en"
    else:
        sys.exit("Ошибка при записи языка.")


def write_title_info(parent_node, blog_name, blog_id, site, first_name=None, last_name=None):
    ti = et.SubElement(parent_node, "title-info")
    ti_text = text["document description"]["title info"]
    et.SubElement(ti, "genre", match=str(ti_text["match"])).text = ti_text["genre"]
    write_author(ti, blog_name, GMJFunctions.get_blog_url(site, blog_id), first_name, last_name, blog_id)
    et.SubElement(ti, "book-title").text = "Блог «" + blog_name + "»"
    write_language(ti, site)
    et.SubElement(ti, "sequence", name=ti_text["sequence"])


def write_document_info(parent_node):
    di = et.SubElement(parent_node, "document-info")
    di_text = text["document description"]["document info"]
    write_author(di, di_text["nickname"], di_text["site"], di_text["first name"], di_text["last name"])
    et.SubElement(di, "program-used").text = di_text["program name"]
    et.SubElement(di, "date", value=datetime.now().strftime("%Y-%m-%d")).text = str(datetime.now().year)
    et.SubElement(di, "src-url").text = di_text["program site"]
    et.SubElement(di, "version").text = str(di_text["program version"])


def write_description(root, blog_name, blog_id, site, first_name=None, last_name=None):
    description = et.SubElement(root, "description")
    write_title_info(description, blog_name, blog_id, site, first_name, last_name)
    write_document_info(description)


def save_fb2(root, blog_name):
    et.ElementTree(root).write(
        "books/" + blog_name + ".fb2",
        pretty_print=True,
        xml_declaration=True,
        encoding='utf-8'
    )


def write_annotation(parent_node, site, blog_id):
    di_text = text["document description"]["document info"]
    text_annotation = "Книга была сгенерирована %s при помощи сервиса %s: %s." % (
        datetime.now().strftime("%Y-%m-%d"),
        di_text["program name"],
        di_text["program site"]
    )
    annotation = et.SubElement(parent_node, "annotation")
    et.SubElement(annotation, "p").text = text_annotation
    et.SubElement(annotation, "empty-line")
    text_annotation = "Онлайн-версия блога доступна по адресу: %s." % GMJFunctions.get_blog_url(site, blog_id)
    et.SubElement(annotation, "p").text = text_annotation
    et.SubElement(annotation, "empty-line")
    et.SubElement(annotation, "p").text = "Приятного чтения, друг."


def write_year(parent_node, year):
    section_year = et.Element("section")
    title = et.SubElement(section_year, "title")
    et.SubElement(title, "p").text = str(year)
    parent_node.insert(2, section_year)
    return section_year


def write_month(section_year, date):
    section_month = et.Element("section")
    title = et.SubElement(section_month, "title")
    et.SubElement(title, "p").text = date.strftime("%B")
    section_year.insert(1, section_month)
    return section_month


def write_post(root, section_month, post, include_image, site, images_counter):
    post_id = str(post['id'])
    et.SubElement(section_month, "subtitle", id=post_id).text = globals.russian_date(post['time'])
    first_paragraph = et.SubElement(section_month, "p")
    if post['title']:
        if post['message']:
            message_title = et.Element("strong")
            message_title.text = post['title']
            message_title.tail = " " + str(post['message'][0]).strip()
            first_paragraph.insert(-1, message_title)
        else:
            first_paragraph.text = str(post['title'])
    else:
        first_paragraph.text = str(post['message'][0]).strip()
    if post['message']:
        for para in post['message'][1:]:
            et.SubElement(section_month, "p").text = para.strip()
    if include_image and post['image']:
        image = globals.get_image(GMJFunctions.get_image_url(site, post_id))
        if image['type'] in {"jpeg", "png"}:
            write_image(root, section_month, post_id, image)
            images_counter += 1
    print("Записан пост от %s." % post['time'].strftime('%Y-%m-%d %H:%M'))
    return images_counter


def write_posts_month(root, section_month, posts_month, images, site, images_counter, posts_counter):
    for post_month in posts_month:
        if post_month['title'] or post_month['message']:
            images_counter = write_post(root, section_month, post_month, images, site, images_counter)
            posts_counter += 1
    return images_counter, posts_counter


def write_image(root, section_month, post_id, image):
    image_id = "image" + post_id + "." + image['type']
    image_tag = et.SubElement(section_month, "image")
    image_tag.attrib['{http://www.w3.org/1999/xlink}href'] = "#" + image_id
    binary_tag = et.SubElement(root, "binary", id=image_id)
    binary_tag.attrib['content-type'] = "image/" + image['type']
    binary_tag.text = globals.encode_base64(image['content'])
    printing.indent("Записано изображение %s." % image['type'].upper())


def write_posts(root, parent_section, blog_id, first_page, site, images, coauthor_id):
    stop_parsing = False
    # Счетчики
    pages_counter = 0
    posts_counter = 0
    images_counter = 0
    # Регуляторы записи годов и месяцев
    year_written = 0
    month_written = 0
    section_year = None
    section_month = None
    posts_month = []
    while True:
        if pages_counter == 0:
            posts = first_page
        else:
            blog_page = GMJFunctions.get_html(GMJFunctions.get_blog_url(site, blog_id) + "&sidx=" + str(pages_counter))
            posts = parser.get_posts_table(blog_page)
        if posts:
            # Запись постов со страницы
            for x in range(10):
                # Получаем пост и информацию о нем
                post = parser.get_post_data(posts, x)
                if post:
                    if post['author'] == blog_id or post['author'] == coauthor_id:
                        year = post['time'].year
                        if year != year_written:
                            section_year = write_year(parent_section, year)
                            year_written = year
                        month = post['time'].month
                        if month != month_written or (month == month_written and year != year_written):
                            if posts_month:
                                # Записываем посты за весь месяц
                                c = write_posts_month(
                                    root, section_month,
                                    posts_month, images, site,
                                    images_counter, posts_counter)
                                images_counter = c[0]
                                posts_counter = c[1]
                                # Очищаем массив с постами за месяц
                                posts_month = []
                            section_month = write_month(section_year, post['time'])
                            month_written = month
                        posts_month.insert(0, post)
                    else:
                        # Переходим к следующему сообщению, этот не того автора
                        continue
                else:
                    # Посты закончились, нужно остановить дальнейший парсинг
                    stop_parsing = True
                    break
            if stop_parsing is True:
                # Записываем посты за последний месяц последнего года
                c = write_posts_month(root, section_month, posts_month, images, site, images_counter, posts_counter)
                images_counter = c[0]
                posts_counter = c[1]
                break
            else:
                pages_counter += 1
        else:
            # Записываем посты за последний месяц последнего года
            c = write_posts_month(root, section_month, posts_month, images, site, images_counter, posts_counter)
            images_counter = c[0]
            posts_counter = c[1]
            # Stop parsing
            break
    # Вывод статистики
    printing.indent("Записано постов: %s." % posts_counter)
    printing.indent("Записано изображений: %s." % images_counter)
    printing.indent("Обработано страниц: %s." % str(pages_counter + 1))


def write_body(root, blog_name, blog_id, first_page, site, include_images, coauthor_id):
    section = et.SubElement(et.SubElement(root, "body"), "section")
    et.SubElement(et.SubElement(section, "title"), "p").text = "Блог «" + blog_name + "»"
    write_annotation(section, site, blog_id)
    write_posts(root, section, blog_id, first_page, site, include_images, coauthor_id)


def create_fb2(input_data):
    globals.create_dir()
    root = create_root()
    write_description(
        root,
        input_data['blog_name'],
        input_data['blog_id'],
        input_data['site'],
        input_data['real_name'],
        input_data['real_surname']
    )
    write_body(
        root,
        input_data['blog_name'],
        input_data['blog_id'],
        input_data['first_page'],
        input_data['site'],
        input_data['include_images'],
        input_data['coauthor_id']
    )
    save_fb2(root, input_data['blog_name'])
