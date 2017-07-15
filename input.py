import GMJFunctions
import globals
import parser
import printing
from checkInput import check_blog_name, if_blog_public, if_blog_closed, remove_unwanted_char

text = globals.yaml_load()


def prompt_yes_no(question):
    yes = {'yes', 'y', 'д', 'да', ''}
    no = {'no', 'n', 'нет', 'н'}
    print(question)
    while True:
        response = input('\nВведите Да/нет:').lower()
        if response in yes:
            response = True
            break
        elif response in no:
            response = False
            break
    return response


def site():
    while True:
        print("\nВыберите площадку блогов:")
        print("1: Русскоязычная версия (%s)" % text["url"]["ru"])
        print("2: Англоязычная версия (%s)" % text["url"]["en"])
        try:
            site_choice = int(input("\nВыберите желаемую опцию:"))
            if site_choice in [1, 2]:
                break
            else:
                printing.indent("Введите 1 или 2.")
        except ValueError:
            printing.indent("Введите число.")
    return site_choice


def blog_name(site_input):
    url = GMJFunctions.get_url(site_input)
    while True:
        blog_name_input = input("\nВведите название блога, который должен стать книгой (например, kollaps):")
        if blog_name_input:
            # Имя блога введено
            if blog_name_input == "6020" and site_input == 1:
                # Проверка завершена
                blog_first_page = GMJFunctions.get_first_page(blog_name_input, url)
                blog_id = 375
                blog_name_real = blog_name_input
                posts_table = parser.get_posts_table(blog_first_page)
                break
            else:
                error = check_blog_name(blog_name_input)
                if error != 0:
                    printing.indent(error)
                else:
                    # Имя блога введено корректно
                    error = if_blog_public(blog_name_input)
                    if error != 0:
                        printing.indent(error)
                    else:
                        # Блог не в списке публичных
                        blog_first_page = GMJFunctions.get_first_page(blog_name_input, url)
                        blog_id = parser.get_blog_id(blog_first_page)
                        if blog_id == 0:
                            printing.indent("Блог не найден.")
                        else:
                            # Блог найден
                            error = if_blog_closed(blog_first_page)
                            if error != 0:
                                printing.indent(error)
                            else:
                                # Блог не закрыт для общего доступа
                                posts_table = parser.get_posts_table(blog_first_page)
                                if not posts_table:
                                    printing.indent("В блоге нет записей.")
                                else:
                                    # В блоге есть записи
                                    # Проверка завершена
                                    blog_name_real = parser.get_blog_name(blog_first_page)
                                    break
    return {
        'blog_name': blog_name_real,
        'blog_id': blog_id,
        'first_page': posts_table
    }


def coauthor_name(blog_name_input, site_input):
    print("\nХорошо. Теперь укажите вторую учетную запись автора блога (необязательно).")
    print("Для этого введите имя пользователя, чьи сообщения в блоге также стоит включить в книгу.")
    print("Это может понадобиться, например, если автор ведет свой блог с двух учетных записей.")
    while True:
        coauthor_name_input = input("\nВведите имя или просто нажмите Enter, если автор один:")
        if coauthor_name_input:
            # Имя соавтора введено
            if coauthor_name_input == blog_name_input:
                printing.indent("Введите имя, отличное от основного имени блога.")
            else:
                # Имя соавтора не совпадает с именем автора
                if coauthor_name_input == "6020" and site_input == 1:
                    # Проверка завершена
                    coauthor_id = 375
                    break
                else:
                    error = check_blog_name(coauthor_name_input)
                    if error != 0:
                        printing.indent(error)
                    else:
                        # Имя соавтора введено корректно
                        url = GMJFunctions.get_url(site_input)
                        coauthor_html = GMJFunctions.get_first_page(coauthor_name_input, url)
                        coauthor_id = parser.get_blog_id(coauthor_html)
                        if coauthor_id == 0:
                            printing.indent("Такая учетная запись не найдена.")
                        else:
                            # Соавтор найден
                            # Проверка завершена
                            break
        else:
            # Соавтора не учитывать
            printing.indent("Для книги будут собираться сообщения только одного автора", newline=False)
            print("(%s)" % blog_name_input)
            coauthor_id = 0
            break
    return coauthor_id


def real_name():
    while True:
        real_name_input = input("\nВведите настоящее имя автора блога или нажмите Enter, чтобы его не указывать:")
        if real_name_input:
            real_name_input = remove_unwanted_char(real_name_input)
            if real_name_input:
                break
            else:
                printing.indent("Введите корректное имя. Таких имен не бывает.")
        else:
            printing.indent("Ладно, настоящее имя автора блога в книге указываться не будет.")
            break
    return real_name_input


def real_surname():
    while True:
        real_surname_input = input("\nВведите настоящую фамилию автора блога или нажмите Enter, чтобы ее не указывать:")
        if real_surname_input:
            real_surname_input = remove_unwanted_char(real_surname_input)
            if real_surname_input:
                break
            else:
                printing.indent("Введите корректную фамилию. Таких фамилий не бывает.")
        else:
            printing.indent("Ладно, настоящая фамилия автора блога в книге указываться не будет.")
            break
    return real_surname_input


def include_images():
    include_images_response = prompt_yes_no("\nВключать в книгу изображения, прикрепленные к сообщениям?")
    if include_images_response is True:
        printing.indent("Книга будет включать в себя изображения из блога.")
    else:
        printing.indent("Изображения в блог включены не будут.")
    return include_images_response


def start_processing():
    while True:
        start_agree = input("\nНастройки заданы. Нажмите Enter, чтобы начать процесс создания книги.")
        if not start_agree:
            printing.line()
            break

