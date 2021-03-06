﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gmj2book
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

	    private void MainFormLoad(object sender, EventArgs e)
		{
			// Действия при запуске приложения
			filePath.Text = AppDomain.CurrentDomain.BaseDirectory;
		}

	    // Действия по нажатии на кнопку
        private void TextBox1TextChanged(object sender, EventArgs e)
		{
            // Сброс сохраненных ранее сообщений об ошибках
            errorProvider1.Clear();
		    ClearErrorProvider(blogName);
		    ClearErrorProvider(coauthorName);
		    ClearErrorProvider(realName);
		    ClearErrorProvider(realSurname);

            // Изменение текста нажатой кнопки
            launch.Text = @"Проверка введенных данных...";
            launch.Enabled = false;

            // Инициализация нового задания
            var t = new Task
		    {
		        Errors = new List<string[]>(),
		        BlogName = blogName.Text,
                CoauthorName = coauthorName.Text,
                RealName = realName.Text,
                RealSurname = realSurname.Text
            };

            // Отображение ошибок ввода данных, если есть
			DisplayErrors(t.Errors);

            // Создание FB2 книги
		    if (t.Errors.Count == 0)
		    {
                launch.Text = @"Создание книги..."; // Изменение текста нажатой кнопки
		        var b = new Fb2();
                b.SaveFb2(t);
            }

            // Возврат данных кнопки
		    launch.Text = @"Запустить";
            launch.Enabled = true;
        }

        // Функция для всплывающих подсказок
	    private void ToolTip1Popup(object sender, PopupEventArgs e) {}

        // Отображение всех ошибок ввода данных
	    private void DisplayErrors(IReadOnlyCollection<string[]> errors)
	    {
	        if (errors.Count == 0) return;
	        foreach (var error in errors)
	        {
	            DisplayError(error[0], error[1]);
	        }
	    }

        // Отображение конкретной ошибки ввода данных
	    private void DisplayError(string error, string param)
		{
			switch (param)
			{
				case "blogName":
					errorProvider1.SetError(blogName, error);
					break;
				case "coauthorName":
				    errorProvider1.SetError(coauthorName, error);
                    break;
			    case "realName":
			        errorProvider1.SetError(realName, error);
			        break;
			    case "realSurname":
			        errorProvider1.SetError(realSurname, error);
			        break;
            }
		}

        // Действия по нажатию на кнопку "..."
	    private void Button1Click(object sender, EventArgs e)
		{
		    var fbd = new FolderBrowserDialog
		    {
		        RootFolder = Environment.SpecialFolder.MyComputer,
		        Description = @"Выберите путь для сохранения готовой книги."
		    };
		    if (fbd.ShowDialog() == DialogResult.OK)
				filePath.Text = fbd.SelectedPath;
		}

        // Очистка данных об ошибках
	    private void ClearErrorProvider(Control name)
	    {
	        errorProvider1.SetIconPadding(name, 7);
	        errorProvider1.SetError(name, "");
        }
    }
}
