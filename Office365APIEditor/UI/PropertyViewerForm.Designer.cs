namespace Office365APIEditor
{
    partial class PropertyViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyViewerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label_PropertyName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_PropertyValue = new System.Windows.Forms.TextBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.radioButton_Original = new System.Windows.Forms.RadioButton();
            this.radioButton_Json = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name : ";
            // 
            // label_PropertyName
            // 
            this.label_PropertyName.AutoSize = true;
            this.label_PropertyName.Location = new System.Drawing.Point(62, 9);
            this.label_PropertyName.Name = "label_PropertyName";
            this.label_PropertyName.Size = new System.Drawing.Size(64, 13);
            this.label_PropertyName.TabIndex = 1;
            this.label_PropertyName.Text = "[Prop name]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value : ";
            // 
            // textBox_PropertyValue
            // 
            this.textBox_PropertyValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_PropertyValue.Location = new System.Drawing.Point(61, 34);
            this.textBox_PropertyValue.Multiline = true;
            this.textBox_PropertyValue.Name = "textBox_PropertyValue";
            this.textBox_PropertyValue.ReadOnly = true;
            this.textBox_PropertyValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_PropertyValue.Size = new System.Drawing.Size(511, 186);
            this.textBox_PropertyValue.TabIndex = 3;
            // 
            // button_Close
            // 
            this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Close.Location = new System.Drawing.Point(497, 226);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 4;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // radioButton_Original
            // 
            this.radioButton_Original.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_Original.AutoSize = true;
            this.radioButton_Original.Checked = true;
            this.radioButton_Original.Location = new System.Drawing.Point(61, 229);
            this.radioButton_Original.Name = "radioButton_Original";
            this.radioButton_Original.Size = new System.Drawing.Size(60, 17);
            this.radioButton_Original.TabIndex = 5;
            this.radioButton_Original.TabStop = true;
            this.radioButton_Original.Text = "Original";
            this.radioButton_Original.UseVisualStyleBackColor = true;
            this.radioButton_Original.CheckedChanged += new System.EventHandler(this.radioButton_Original_CheckedChanged);
            // 
            // radioButton_Json
            // 
            this.radioButton_Json.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_Json.AutoSize = true;
            this.radioButton_Json.Location = new System.Drawing.Point(127, 229);
            this.radioButton_Json.Name = "radioButton_Json";
            this.radioButton_Json.Size = new System.Drawing.Size(100, 17);
            this.radioButton_Json.TabIndex = 6;
            this.radioButton_Json.Text = "Decoded JSON";
            this.radioButton_Json.UseVisualStyleBackColor = true;
            // 
            // PropertyViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Close;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.radioButton_Json);
            this.Controls.Add(this.radioButton_Original);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.textBox_PropertyValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_PropertyName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "PropertyViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Property Viewer";
            this.Load += new System.EventHandler(this.PropertyViewerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_PropertyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_PropertyValue;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.RadioButton radioButton_Original;
        private System.Windows.Forms.RadioButton radioButton_Json;
    }
}