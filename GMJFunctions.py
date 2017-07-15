# noinspection PyUnresolvedReferences
import re
import sys
import globals
import urllib.error
import urllib.parse
import urllib.request
from bs4 import BeautifulSoup
from datetime import datetime

text = globals.yaml_load()


def get_url(site):
    if site == 1:
        url = text["url"]["ru"]
    elif site == 2:
        url = text["url"]["en"]
    else:
        sys.exit("Ошибка при получении URL портала.")
    return url


def get_blog_url(site, blog_id):
    return get_url(site) + "/Blog/Blog.aspx?bid=" + str(blog_id)


def get_image_url(site, post_id):
    return get_url(site) + "/Blog/Attachment.ashx?aid=" + post_id


def get_html(url, headers=''):
    access_error_message = 'Сайт недоступен. Попробуйте позднее.'
    if headers:
        try:
            f = urllib.request.urlopen(url, headers)
        except (urllib.error.HTTPError, urllib.error.URLError):
            sys.exit(access_error_message)
    else:
        try:
            f = urllib.request.urlopen(url).read()
        except (urllib.error.HTTPError, urllib.error.URLError):
            sys.exit(access_error_message)
    html = BeautifulSoup(f, "html.parser")
    return html


def get_first_page(username, url):
    # Первый запрос для получения __VIEWSTATE и __EVENTVALIDATION
    html = get_html(url)
    view_state = html.select("#__VIEWSTATE")[0]['value']
    event_validation = html.select("#__EVENTVALIDATION")[0]['value']

    # Формирование заголовков для второго запроса
    form_data = (
        ('__VIEWSTATE', view_state),
        ('__EVENTVALIDATION', event_validation),
        ('ctl00$tbxQGo', username),
        ('ctl00$ibQGo.x', 10),
        ('ctl00$ibQGo.y', 5),
        ('ctl00$cph1$sw', 'rbSBlog'),
    )
    encoded_fields = urllib.parse.urlencode(form_data)
    binary_data = encoded_fields.encode('utf8')

    # Второй запрос для получения HTML с первой страницей блога
    html = get_html(url, binary_data)
    return html


def gmj_time(time):
    months = [
        (" янв ", " 01 "),
        (" фев ", " 02 "),
        (" мар ", " 03 "),
        (" апр ", " 04 "),
        (" май ", " 05 "),
        (" июн ", " 06 "),
        (" июл ", " 07 "),
        (" авг ", " 08 "),
        (" сен ", " 09 "),
        (" окт ", " 10 "),
        (" ноя ", " 11 "),
        (" дек ", " 12 ")
    ]
    for month in months:
        if month[0] in time:
            time = time.replace(month[0], month[1])
            break
    time = datetime.strptime(time, '%d %m %Y, %H:%M')
    return time
