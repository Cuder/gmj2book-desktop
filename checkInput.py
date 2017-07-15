import re
import globals

text = globals.yaml_load()


# Проверка введенного имени блога
def check_blog_name(blog_name_input):
    error = 0
    # Имя блога может содержать:
    # -- латинские или кириллические буквы в верхнем или нижнем регистрах,
    # -- цифры,
    # -- пробельный символ,
    # -- дефис,
    # -- знак равенства,
    # -- знак подчеркивания,
    # -- точку
    if re.findall('[^а-яА-ЯA-Za-z0-9 \\-=_.]', blog_name_input):
        error = "Имя содержит недопустимые символы."
        return error
    # Имя блога не может содержать только цифры (кроме блога 6020)
    if blog_name_input.isdigit():
        error = "Имя содержит только цифры."
        return error
    # Имя блога должно начинаться на букву (кроме блога 6020)
    if re.search('[а-яА-ЯA-Za-z]', blog_name_input).start() != 0:
        error = "Имя должно начинаться на букву."
        return error
    # Имя блога не может содержать одновременно и латинские, и кириллические буквы
    if re.findall('[а-яА-Я]', blog_name_input) and re.findall('[a-zA-Z]', blog_name_input):
        error = "Имя не может содержать одновременно и латинские, и кириллические буквы."
    return error


# Проверка на публичность блога
def if_blog_public(blog_name_input):
    if blog_name_input.lower() in text["public blogs"]:
        error = "Этот блог в списке общих. Введите название частного блога."
    else:
        error = 0
    return error


# Проверка на закрытость блога
def if_blog_closed(html):
    if html.find("span", {"id": "ctl00_cph1_lblError"}):
        error = "Блог закрыт для общего доступа. Пока мы можем создавать книжки только для открытых блогов."
    else:
        error = 0
    return error


# Удаление лишних символов из настоящих имен
def remove_unwanted_char(name):
    name = re.sub('[^а-яА-ЯA-Za-z ]', '', name).strip()
    return name
