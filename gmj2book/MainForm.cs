using System;
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
			filePath.Text = System.AppDomain.CurrentDomain.BaseDirectory;
		}

	    private void TextBox1TextChanged(object sender, EventArgs e)
		{
			// Действия по нажатии на кнопку
		    var t = new Task
		    {
		        Errors = new List<string[]>(),
		        BlogName = blogName.Text
		    };
			DisplayErrors(t.Errors);
		}

	    private void ToolTip1Popup(object sender, PopupEventArgs e) {}

	    private void DisplayErrors(IReadOnlyCollection<string[]> errors) {
			if (errors.Count != 0)
			{
				foreach (var error in errors)
				{
					DisplayError(error[0], error[1]);
				}
			}
			else
			{
				errorProvider1.Clear();
				errorProvider1.SetError(blogName, "");
			}
		}

	    private void DisplayError(string error, string param)
		{
			switch (param)
			{
				case "blogName":
					errorProvider1.SetError(blogName, error);
					break;
				case "coauthorName":
					break;
			}
		}

	    private void Button1Click(object sender, EventArgs e)
		{
		    var fbd = new FolderBrowserDialog
		    {
		        RootFolder = Environment.SpecialFolder.MyComputer,
		        Description = "Выберите путь для сохранения готовой книги."
		    };
		    if (fbd.ShowDialog() == DialogResult.OK)
				filePath.Text = fbd.SelectedPath;
		}
	}
}
