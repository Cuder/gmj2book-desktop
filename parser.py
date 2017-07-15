import re
import GMJFunctions
import urllib.parse as up


# Получить ID блога из его страницы
def get_blog_id(html):
    action = html.find("form", {"id": "aspnetForm"})['action']
    blog_id = action[action.rfind('=') + 1:]
    if blog_id == action:
        # Блог не найден
        blog_id = 0
    return blog_id


# Получить ID поста или автора поста из ссылки
def get_id(url, obj_id):
    return ", ".join(up.parse_qs(up.urlparse(url).query)[obj_id])


# Получить имя блога из его страницы
def get_blog_name(html):
    title = html.title.string
    blog_name = re.search(r'\"(.*)\"', title).group(1)
    return blog_name


# Получить таблицу с постами блога
def get_posts_table(html):
    posts_table = html.find("table", {"class": "BlogDG"})
    return posts_table


# Заменить гиперссылки на строку "[ссылка]"
def destroy_hyperlinks(post_message):
    for link in post_message.findAll('a'):
        if link.previous_sibling:
            previous = str(link.previous_sibling)
            if not (previous.endswith(" ")):
                previous += " "
            link.previous_sibling.extract()
        else:
            previous = ""
        if link.next_sibling:
            following = str(link.next_sibling)
            if not (following.startswith(" ")):
                following = " " + following
            link.next_sibling.extract()
        else:
            following = ""
        link.replace_with(previous + "[ссылка]" + following)
    return None


# Получить информацию о картинке к сообщению
def find_image(post_message):
    image = False
    attachment = post_message.find("div", {"class": "att"})
    if attachment:
        if attachment.find("img"):
            image = True
        attachment.extract()
    return image


def process_post_message(post_message):
    # Удалить обрывы строк
    for br in post_message.findAll('br'):
        br.extract()
    # Удалить ссылки
    destroy_hyperlinks(post_message)
    # Удалить неразрывные пробелы
    post_message = post_message.contents
    post_message = [x for x in post_message if x != u'\xa0']
    return post_message


def process_post_title(title):
    title = str(title.string.strip())
    if not re.sub('[^A-Za-zА-Яа-я0-9]', '', title):
        title = None
    else:
        if re.search('[A-Za-zА-Яа-я0-9]', title[-1:]):
            title = title + "."
    return title


# Получить данные поста из таблицы с постами блога
# Где post_number — номер сообщения на странице (начиная с 0)
def get_post_data(posts, post_number):
    post = {}
    try:
        post_table = posts.findAll("table", {"class": "BlogT"})[post_number]
    except IndexError:
        return None
    # Идентификатор автора сообщения
    post['author'] = get_id(post_table.find("td", {"align": "left"}).a['href'], "bid")
    # Идентификатор сообщения
    post['id'] = get_id(post_table.find("td", {"align": "right"}).a['href'], "rid")
    # Заголовок сообщения
    post['title'] = process_post_title(post_table.find("th", {"align": "left"}))
    # Время публикации сообщения
    post['time'] = GMJFunctions.gmj_time(post_table.find("th", {"align": "right"}).string)
    # Само сообщение
    post_message = post_table.find("td", {"colspan": "2"})
    # Картинка к сообщению
    post['image'] = find_image(post_message)
    post['message'] = process_post_message(post_message)
    return post
