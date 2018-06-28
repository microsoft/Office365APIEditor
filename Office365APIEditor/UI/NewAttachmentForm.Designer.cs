namespace Office365APIEditor.UI
{
    partial class NewAttachmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAttachmentForm));
            this.listBox_Attachments = new System.Windows.Forms.ListBox();
            this.button_Add = new System.Windows.Forms.Button();
            this.button_Remove = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // listBox_Attachments
            // 
            this.listBox_Attachments.FormattingEnabled = true;
            this.listBox_Attachments.HorizontalScrollbar = true;
            this.listBox_Attachments.Location = new System.Drawing.Point(12, 12);
            this.listBox_Attachments.Name = "listBox_Attachments";
            this.listBox_Attachments.Size = new System.Drawing.Size(429, 303);
            this.listBox_Attachments.TabIndex = 0;
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(447, 12);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 1;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Remove
            // 
            this.button_Remove.Location = new System.Drawing.Point(447, 41);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.Size = new System.Drawing.Size(75, 23);
            this.button_Remove.TabIndex = 2;
            this.button_Remove.Text = "Remove";
            this.button_Remove.UseVisualStyleBackColor = true;
            this.button_Remove.Click += new System.EventHandler(this.button_Remove_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(366, 327);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 3;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(447, 327);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 4;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "All Files (*.*)|*.*";
            this.openFileDialog1.Multiselect = true;
            // 
            // NewAttachmentForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(534, 362);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_Remove);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.listBox_Attachments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "NewAttachmentForm";
            this.Text = "Attachments";
            this.Load += new System.EventHandler(this.NewAttachmentForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_Attachments;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_Remove;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}