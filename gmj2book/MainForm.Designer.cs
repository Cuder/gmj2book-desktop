namespace gmj2book
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button launch;
		private System.Windows.Forms.TextBox blogName;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox coauthorName;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox realSurname;
		private System.Windows.Forms.TextBox realName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox filePath;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.launch = new System.Windows.Forms.Button();
            this.blogName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.coauthorName = new System.Windows.Forms.TextBox();
            this.realName = new System.Windows.Forms.TextBox();
            this.realSurname = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.filePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // launch
            // 
            this.launch.AccessibleName = "";
            this.launch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.launch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.launch.Location = new System.Drawing.Point(152, 367);
            this.launch.Margin = new System.Windows.Forms.Padding(0);
            this.launch.Name = "launch";
            this.launch.Size = new System.Drawing.Size(100, 25);
            this.launch.TabIndex = 0;
            this.launch.Text = "Запустить";
            this.launch.UseVisualStyleBackColor = true;
            this.launch.Click += new System.EventHandler(this.TextBox1TextChanged);
            // 
            // blogName
            // 
            this.blogName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blogName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconPadding(this.blogName, 7);
            this.blogName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.blogName.Location = new System.Drawing.Point(182, 140);
            this.blogName.MaxLength = 20;
            this.blogName.Name = "blogName";
            this.blogName.Size = new System.Drawing.Size(190, 20);
            this.blogName.TabIndex = 1;
            this.toolTip1.SetToolTip(this.blogName, "Введите название блога, который должен стать книгой (например, kollaps)");
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 0;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.ToolTip1Popup);
            // 
            // coauthorName
            // 
            this.coauthorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coauthorName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconPadding(this.coauthorName, 7);
            this.coauthorName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.coauthorName.Location = new System.Drawing.Point(182, 173);
            this.coauthorName.MaxLength = 20;
            this.coauthorName.Name = "coauthorName";
            this.coauthorName.Size = new System.Drawing.Size(190, 20);
            this.coauthorName.TabIndex = 4;
		    this.toolTip1.SetToolTip(this.coauthorName, "Укажите вторую учетную запись автора блога (необязательно).\nДля этого введите имя пользователя, чьи сообщения в блоге также стоит включить в книгу.\nЭто может понадобиться, например, если автор ведет свой блог с двух учетных записей.");
            // 
            // realName
            // 
            this.realName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.realName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconPadding(this.realName, 7);
            this.realName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.realName.Location = new System.Drawing.Point(182, 206);
            this.realName.MaxLength = 20;
            this.realName.Name = "realName";
            this.realName.Size = new System.Drawing.Size(190, 20);
            this.realName.TabIndex = 10;
            this.toolTip1.SetToolTip(this.realName, "Введите настоящее имя автора блога (необязательно)");
            // 
            // realSurname
            // 
            this.realSurname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.realSurname.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconPadding(this.realSurname, 7);
            this.realSurname.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.realSurname.Location = new System.Drawing.Point(182, 239);
            this.realSurname.MaxLength = 20;
            this.realSurname.Name = "realSurname";
            this.realSurname.Size = new System.Drawing.Size(190, 20);
            this.realSurname.TabIndex = 11;
            this.toolTip1.SetToolTip(this.realSurname, "Введите настоящую фамилию автора блога (необязательно)");
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox1.Location = new System.Drawing.Point(182, 270);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 24);
            this.checkBox1.TabIndex = 13;
            this.toolTip1.SetToolTip(this.checkBox1, "Включать в книгу изображения, прикрепленные к сообщениям? Это может существенно у" +
        "величить размер готовой книги.");
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // filePath
            // 
            this.filePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filePath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.filePath.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.filePath.Location = new System.Drawing.Point(179, 305);
            this.filePath.MaxLength = 20;
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Size = new System.Drawing.Size(160, 20);
            this.filePath.TabIndex = 16;
            this.toolTip1.SetToolTip(this.filePath, "Выберите путь для сохранения готовой книги");
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Location = new System.Drawing.Point(344, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 20);
            this.button1.TabIndex = 15;
            this.button1.Text = "...";
            this.toolTip1.SetToolTip(this.button1, "Выберите путь для сохранения готовой книги");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(0, 140);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(169, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название блога";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::gmj2book.Resource2.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.pictureBox1.Size = new System.Drawing.Size(404, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(0, 173);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(169, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Вторая учетная запись";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(0, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(169, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Площадка блогов";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radioButton1.Location = new System.Drawing.Point(182, 105);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(89, 24);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "my.gmj.ru";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radioButton2.Location = new System.Drawing.Point(270, 105);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(115, 24);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.Text = "my.2gmj.com";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(0, 206);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label4.Size = new System.Drawing.Size(169, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Имя автора блога";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(0, 239);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(169, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Фамилия автора блога";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(0, 272);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(169, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Включать изображения";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(0, 305);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label7.Size = new System.Drawing.Size(169, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Путь для сохранения";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 401);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.realSurname);
            this.Controls.Add(this.realName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.coauthorName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blogName);
            this.Controls.Add(this.launch);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 440);
            this.MinimumSize = new System.Drawing.Size(420, 440);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "app1d";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}