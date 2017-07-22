import yaml
import sys
import os
import locale
import io
import imghdr
from urllib.request import urlopen
import base64
import zipfile
from platform import system


def yaml_load():
    with open("config.yaml", "r", encoding="utf-8") as stream:
        try:
            text = yaml.load(stream)
        except yaml.YAMLError as exc:
            print(exc)
            sys.exit("Не могу загрузить модуль YAML.")
    return text


def create_dir():
    if not os.path.exists("books"):
        os.makedirs("books")


def russian_date(time):
    if system() == "Linux":
        locale.setlocale(locale.LC_ALL, 'ru_RU.UTF-8')
        time = time.strftime("%-d %B, %A. %-H:%M")
    elif system() == "Windows":
        locale.setlocale(locale.LC_ALL, 'rus_rus')
        time = time.strftime("%#d %B, %A. %#H:%M")
    months = [
        ("Январь", "января"),
        ("Февраль", "февраля"),
        ("Март", "марта"),
        ("Апрель", "апреля"),
        ("Май", "мая"),
        ("Июнь", "июня"),
        ("Июль", "июля"),
        ("Август", "августа"),
        ("Сентябрь", "сентября"),
        ("Октябрь", "октября"),
        ("Ноябрь", "ноября"),
        ("Декабрь", "декабря")
    ]
    for month in months:
        if month[0] in time:
            time = time.replace(month[0], month[1])
            break
    return time.lower()


def get_image(url):
    image = urlopen(url).read()
    img_type = imghdr.what(io.BytesIO(image))
    if img_type not in {"jpeg", "png"}:
        image = None
    return {
        'type': img_type,
        'content': image
    }


def encode_base64(image):
    return base64.b64encode(image)


def archive_book(blog_name):
    with zipfile.ZipFile('books/%s.zip' % blog_name, mode='w') as zf:
        try:
            zf.write('books/%s.fb2' % blog_name, arcname='%s.fb2' % blog_name, compress_type=zipfile.ZIP_DEFLATED)
        except FileNotFoundError:
            print("Файл книги не найден.")
        zf.close()


def delete_book(blog_name):
    try:
        os.remove("books/%s.fb2" % blog_name)
    except OSError:
        pass
