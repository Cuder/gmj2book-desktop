from kivy import require
from kivy.app import App
from kivy.config import Config
from kivy.lang.builder import Builder
from kivy.properties import BooleanProperty, ListProperty, StringProperty
from kivy.uix.behaviors import ToggleButtonBehavior
from kivy.uix.button import Button
from kivy.uix.screenmanager import Screen

import input
from FB2Functions import create_fb2
from globals import archive_book, delete_book

Config.set('graphics', 'width', '700')
Config.set('graphics', 'height', '450')
Config.set('graphics', 'resizable', False)
Config.set('kivy', 'log_level', 'warning')

Builder.load_file('design.kv')

require('1.10.0')
normal_text = 'Запустить процесс создания книги'
red = [1, 0.01, 0.01, 1]
white = [1, 1, 1, 1]


class PressButton(Button):
    def __init__(self, **kwargs):
        super(PressButton, self).__init__(**kwargs)
        self.text = normal_text
        self.bind(state=self.button_toggle)

    def button_toggle(self, widget, value):
        if self.state == 'down':
            self.text = 'Идет проверка введенных данных...'
        else:
            self.text = normal_text


class SelectSite(ToggleButtonBehavior, Button):
    active = BooleanProperty(False)

    def __init__(self, **kwargs):
        super(SelectSite, self).__init__(**kwargs)
        self.allow_no_selection = False
        self.background_color = white
        self.group = "site"
        self.bind(state=self.toggle)

    def toggle(self, widget, value):
        if self.state == 'down':
            self.background_color = [1, 0, 1, 1]
        else:
            self.background_color = white


class WelcomeScreen(Screen):
    blog_name_color = ListProperty(white)
    coauthor_name_color = ListProperty(white)
    real_name_color = ListProperty(white)
    real_surname_color = ListProperty(white)
    error_text = StringProperty('')
    button_text = StringProperty(normal_text)

    def process(self, site, blog_name, coauthor_name, real_name, real_surname, include_images):
        input_data = {}
        self.error_text = ''
        if site == 'down':
            site = 1
        else:
            site = 2
        input_data['site'] = site
        input_data.update(input.blog_name(blog_name, input_data['site']))
        if input_data['error'] == 0:
            coauthor = input.coauthor_name(coauthor_name, input_data['blog_name'], input_data['site'])
            self.blog_name_color = white
            self.error_text = ''
            if coauthor['error'] == 0:
                input_data.update(coauthor)
                real_name_input = input.real_name(real_name)
                self.coauthor_name_color = white
                self.error_text = ''
                if real_name_input['error'] == 0:
                    input_data.update(real_name_input)
                    real_surname_input = input.real_name(real_surname, surname=True)
                    self.real_name_color = white
                    self.error_text = ''
                    if real_surname_input['error'] == 0:
                        input_data.update(real_surname_input)
                        self.real_surname_color = white
                        self.error_text = ''
                        input_data['include_images'] = include_images

                        # Создание FB2
                        create_fb2(input_data)

                        # Архивирование
                        archive_book(input_data['blog_name'])
                        delete_book(input_data['blog_name'])
                    else:
                        self.real_surname_color = red
                        self.error_text = real_surname_input['error']
                else:
                    self.real_name_color = red
                    self.error_text = real_name_input['error']
            else:
                self.coauthor_name_color = red
                self.error_text = coauthor['error']
        else:
            self.blog_name_color = red
            self.error_text = input_data['error']


class MyApp(App):
    def build(self):
        self.title = 'gmj2book'
        return WelcomeScreen()


if __name__ == '__main__':
    MyApp().run()
