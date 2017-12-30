/*
 * Created by SharpDevelop.
 * User: crest
 * Date: 14.09.2017
 * Time: 18:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace gmj2book
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			// Действия при запуске приложения
			filePath.Text = System.AppDomain.CurrentDomain.BaseDirectory;
		}
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			// Действия по нажатии на кнопку
			//filePath.Text = AppDomain.CurrentDomain.BaseDirectory;
			Task t = new Task();
			t.Errors = new List<string[]>();
			t.Blog_name = blogName.Text;
			//blogName.ForeColor = SystemColors.WindowText;
			//blogName.ForeColor = Color.Red;
			//MessageBox.Show(t.Blog_name);
			displayErrors(t.Errors);
		}
		void ToolTip1Popup(object sender, PopupEventArgs e) {}
		void displayErrors(List<string[]> errors) {
			if (errors.Count != 0)
			{
				foreach (var error in errors)
				{
					displayError(error[0], error[1]);
				}
			}
			else
			{
				errorProvider1.Clear();
				errorProvider1.SetError(blogName, "");
			}
		}
		void displayError(string error, string param)
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
		void Button1Click(object sender, EventArgs e)
		{
			FolderBrowserDialog	fbd = new FolderBrowserDialog();
			fbd.RootFolder = Environment.SpecialFolder.MyComputer;
			fbd.Description = "Выберите путь для сохранения готовой книги.";
			if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				filePath.Text = fbd.SelectedPath;
		}
	}
}
