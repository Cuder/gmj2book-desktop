import printing
import input
import FB2Functions
from datetime import datetime

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
    FB2Functions.create_fb2(input_data)
    printing.indent("Время выполнения: %s." % str(datetime.now()-startTime))
