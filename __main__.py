import printing
import input
from FB2Functions import create_fb2
from datetime import datetime
from globals import archive_book, delete_book

if __name__ == '__main__':
    startTime = datetime.now()
    input_data = {}
    # Ввод данных
    printing.line()
    printing.greeting()
    printing.line()
    input_data['site'] = input.site()
    input_data.update(input.blog_name(input_data['site']))
    input_data['coauthor_id'] = input.coauthor_name(input_data['blog_name'], input_data['site'])
    input_data['real_name'] = input.real_name()
    if input_data['real_name']:
        input_data['real_surname'] = input.real_surname()
    else:
        input_data['real_surname'] = None
    input_data['include_images'] = input.include_images()
    input.start_processing()

    # Создание FB2
    create_fb2(input_data)

    # Архивирование
    archive_book(input_data['blog_name'])
    delete_book(input_data['blog_name'])
    print("Книга упакована в ZIP-архив.")

    printing.indent("\nВремя выполнения: %s." % str(datetime.now() - startTime))