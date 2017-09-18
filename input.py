from GMJFunctions import get_url, get_first_page
from globals import yaml_load
from site_parser import get_posts_table, get_blog_id, get_blog_name
from checkInput import check_blog_name, if_blog_public, if_blog_closed, remove_unwanted_char

text = yaml_load()


def blog_name(blog_name_input, site_input):
    url = get_url(site_input)
    blog_name_real = ''
    blog_id = ''
    posts_table = ''
    error = 0
    if blog_name_input.strip():
        # Имя блога введено
        if blog_name_input == "6020" and site_input == 1:
            # Проверка завершена
            blog_first_page = get_first_page(blog_name_input, url)
            blog_id = 375
            blog_name_real = blog_name_input
            posts_table = get_posts_table(blog_first_page)
        else:
            error = check_blog_name(blog_name_input)
            if error == 0:
                # Имя блога введено корректно
                error = if_blog_public(blog_name_input)
                if error == 0:
                    # Блог не в списке публичных
                    blog_first_page = get_first_page(blog_name_input, url)
                    blog_id = get_blog_id(blog_first_page)
                    if blog_id == 0:
                        error = "Блог не найден."
                    else:
                        # Блог найден
                        error = if_blog_closed(blog_first_page)
                        if error == 0:
                            # Блог не закрыт для общего доступа
                            posts_table = get_posts_table(blog_first_page)
                            if not posts_table:
                                error = "В блоге нет записей."
                            else:
                                # В блоге есть записи
                                # Проверка завершена
                                blog_name_real = get_blog_name(blog_first_page)
    else:
        error = "Введите название блога."
    return {
        'blog_name': blog_name_real,
        'blog_id': blog_id,
        'first_page': posts_table,
        'error': error
    }


def coauthor_name(coauthor_name_input, blog_name_input, site_input):
    coauthor_id = 0
    error = 0
    if coauthor_name_input.strip():
        # Имя соавтора введено
        if coauthor_name_input == blog_name_input:
            error = "Введите имя, отличное от основного имени блога."
        else:
            # Имя соавтора не совпадает с именем автора
            if coauthor_name_input == "6020" and site_input == 1:
                # Проверка завершена
                coauthor_id = 375
            else:
                error = check_blog_name(coauthor_name_input)
                if error == 0:
                    # Имя соавтора введено корректно
                    url = get_url(site_input)
                    coauthor_html = get_first_page(coauthor_name_input, url)
                    coauthor_id = get_blog_id(coauthor_html)
                    if coauthor_id == 0:
                        error = "Такая учетная запись не найдена."
    else:
        # Соавтора не учитывать
        coauthor_id = 0
    return {
        'coauthor_id': coauthor_id,
        'error': error
    }


def real_name(real_name_input, surname=False):
    error = 0
    if real_name_input:
        real_name_input = remove_unwanted_char(real_name_input)
        if not real_name_input:
            if surname:
                error = "Введите корректную фамилию. Таких фамилий не бывает."
            else:
                error = "Введите корректное имя. Таких имен не бывает."
    else:
        real_name_input = ''
    if surname:
        return {
            'real_name': real_name_input,
            'error': error
        }
    else:
        return {
            'real_surname': real_name_input,
            'error': error
        }
