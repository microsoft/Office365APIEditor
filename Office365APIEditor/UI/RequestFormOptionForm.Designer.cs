namespace Office365APIEditor
{
    partial class RequestFormOptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestFormOptionForm));
            this.button_LogFolderPathBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.radioButton_DateTime = new System.Windows.Forms.RadioButton();
            this.radioButton_Static = new System.Windows.Forms.RadioButton();
            this.textBox_LogFolderPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_AllowAutoRedirect = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_LogFolderPathBrowse
            // 
            this.button_LogFolderPathBrowse.Location = new System.Drawing.Point(333, 25);
            this.button_LogFolderPathBrowse.Name = "button_LogFolderPathBrowse";
            this.button_LogFolderPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.button_LogFolderPathBrowse.TabIndex = 16;
            this.button_LogFolderPathBrowse.Text = "Browse";
            this.button_LogFolderPathBrowse.UseVisualStyleBackColor = true;
            this.button_LogFolderPathBrowse.Click += new System.EventHandler(this.button_LogFolderPathBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Log folder path :";
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(333, 154);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 20;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(252, 154);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 19;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // radioButton_DateTime
            // 
            this.radioButton_DateTime.AutoSize = true;
            this.radioButton_DateTime.Location = new System.Drawing.Point(6, 42);
            this.radioButton_DateTime.Name = "radioButton_DateTime";
            this.radioButton_DateTime.Size = new System.Drawing.Size(71, 17);
            this.radioButton_DateTime.TabIndex = 18;
            this.radioButton_DateTime.TabStop = true;
            this.radioButton_DateTime.Text = "DateTime";
            this.radioButton_DateTime.UseVisualStyleBackColor = true;
            // 
            // radioButton_Static
            // 
            this.radioButton_Static.AutoSize = true;
            this.radioButton_Static.Location = new System.Drawing.Point(6, 19);
            this.radioButton_Static.Name = "radioButton_Static";
            this.radioButton_Static.Size = new System.Drawing.Size(52, 17);
            this.radioButton_Static.TabIndex = 17;
            this.radioButton_Static.TabStop = true;
            this.radioButton_Static.Text = "Static";
            this.radioButton_Static.UseVisualStyleBackColor = true;
            // 
            // textBox_LogFolderPath
            // 
            this.textBox_LogFolderPath.Location = new System.Drawing.Point(15, 27);
            this.textBox_LogFolderPath.Name = "textBox_LogFolderPath";
            this.textBox_LogFolderPath.Size = new System.Drawing.Size(312, 20);
            this.textBox_LogFolderPath.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Static);
            this.groupBox1.Controls.Add(this.radioButton_DateTime);
            this.groupBox1.Location = new System.Drawing.Point(15, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 68);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log file name format";
            // 
            // checkBox_AllowAutoRedirect
            // 
            this.checkBox_AllowAutoRedirect.AutoSize = true;
            this.checkBox_AllowAutoRedirect.Location = new System.Drawing.Point(15, 128);
            this.checkBox_AllowAutoRedirect.Name = "checkBox_AllowAutoRedirect";
            this.checkBox_AllowAutoRedirect.Size = new System.Drawing.Size(113, 17);
            this.checkBox_AllowAutoRedirect.TabIndex = 67;
            this.checkBox_AllowAutoRedirect.Text = "Allow auto redirect";
            this.checkBox_AllowAutoRedirect.UseVisualStyleBackColor = true;
            // 
            // RequestFormOptionForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(414, 190);
            this.Controls.Add(this.checkBox_AllowAutoRedirect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox_LogFolderPath);
            this.Controls.Add(this.button_LogFolderPathBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RequestFormOptionForm";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.RequestFormOptionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_LogFolderPathBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.RadioButton radioButton_DateTime;
        private System.Windows.Forms.RadioButton radioButton_Static;
        private System.Windows.Forms.TextBox textBox_LogFolderPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_AllowAutoRedirect;
    }
}