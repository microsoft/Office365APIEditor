namespace Office365APIEditor
{
    partial class LoggingOption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Browse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox_LogFolderPath = new System.Windows.Forms.TextBox();
            this.radioButton_Static = new System.Windows.Forms.RadioButton();
            this.radioButton_DateTime = new System.Windows.Forms.RadioButton();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log folder path :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "File name format :";
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(347, 23);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(75, 23);
            this.button_Browse.TabIndex = 4;
            this.button_Browse.Text = "Browse";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            // 
            // textBox_LogFolderPath
            // 
            this.textBox_LogFolderPath.Location = new System.Drawing.Point(29, 25);
            this.textBox_LogFolderPath.Name = "textBox_LogFolderPath";
            this.textBox_LogFolderPath.Size = new System.Drawing.Size(312, 20);
            this.textBox_LogFolderPath.TabIndex = 1;
            // 
            // radioButton_Static
            // 
            this.radioButton_Static.AutoSize = true;
            this.radioButton_Static.Location = new System.Drawing.Point(29, 76);
            this.radioButton_Static.Name = "radioButton_Static";
            this.radioButton_Static.Size = new System.Drawing.Size(52, 17);
            this.radioButton_Static.TabIndex = 5;
            this.radioButton_Static.TabStop = true;
            this.radioButton_Static.Text = "Static";
            this.radioButton_Static.UseVisualStyleBackColor = true;
            // 
            // radioButton_DateTime
            // 
            this.radioButton_DateTime.AutoSize = true;
            this.radioButton_DateTime.Location = new System.Drawing.Point(29, 99);
            this.radioButton_DateTime.Name = "radioButton_DateTime";
            this.radioButton_DateTime.Size = new System.Drawing.Size(71, 17);
            this.radioButton_DateTime.TabIndex = 6;
            this.radioButton_DateTime.TabStop = true;
            this.radioButton_DateTime.Text = "DateTime";
            this.radioButton_DateTime.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(266, 125);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 11;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(347, 125);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 12;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // LoggingOption
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(435, 158);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.radioButton_DateTime);
            this.Controls.Add(this.radioButton_Static);
            this.Controls.Add(this.button_Browse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_LogFolderPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoggingOption";
            this.Text = "Logging Option";
            this.Load += new System.EventHandler(this.LoggingOption_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_LogFolderPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton radioButton_Static;
        private System.Windows.Forms.RadioButton radioButton_DateTime;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
    }
}