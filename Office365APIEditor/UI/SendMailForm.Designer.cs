namespace Office365APIEditor.UI
{
    partial class SendMailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMailForm));
            this.button_Send = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_To = new System.Windows.Forms.TextBox();
            this.textBox_Cc = new System.Windows.Forms.TextBox();
            this.textBox_Bcc = new System.Windows.Forms.TextBox();
            this.button_Save = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Subject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_BodyType = new System.Windows.Forms.ComboBox();
            this.textBox_Body = new System.Windows.Forms.TextBox();
            this.checkBox_SaveToSentItems = new System.Windows.Forms.CheckBox();
            this.button_Attachments = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_Importance = new System.Windows.Forms.ComboBox();
            this.checkBox_RequestReadReceipt = new System.Windows.Forms.CheckBox();
            this.checkBox_RequestDeliveryReceipt = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(12, 12);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Send.TabIndex = 7;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.Button_Send_ClickAsync);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Bcc";
            // 
            // textBox_To
            // 
            this.textBox_To.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_To.Location = new System.Drawing.Point(76, 70);
            this.textBox_To.Name = "textBox_To";
            this.textBox_To.Size = new System.Drawing.Size(712, 20);
            this.textBox_To.TabIndex = 0;
            // 
            // textBox_Cc
            // 
            this.textBox_Cc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Cc.Location = new System.Drawing.Point(76, 96);
            this.textBox_Cc.Name = "textBox_Cc";
            this.textBox_Cc.Size = new System.Drawing.Size(712, 20);
            this.textBox_Cc.TabIndex = 1;
            // 
            // textBox_Bcc
            // 
            this.textBox_Bcc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Bcc.Location = new System.Drawing.Point(76, 122);
            this.textBox_Bcc.Name = "textBox_Bcc";
            this.textBox_Bcc.Size = new System.Drawing.Size(712, 20);
            this.textBox_Bcc.TabIndex = 2;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(12, 41);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 8;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Subject";
            // 
            // textBox_Subject
            // 
            this.textBox_Subject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Subject.Location = new System.Drawing.Point(76, 148);
            this.textBox_Subject.Name = "textBox_Subject";
            this.textBox_Subject.Size = new System.Drawing.Size(712, 20);
            this.textBox_Subject.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 425);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Body Type";
            // 
            // comboBox_BodyType
            // 
            this.comboBox_BodyType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_BodyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BodyType.FormattingEnabled = true;
            this.comboBox_BodyType.Items.AddRange(new object[] {
            "Text",
            "HTML"});
            this.comboBox_BodyType.Location = new System.Drawing.Point(76, 422);
            this.comboBox_BodyType.Name = "comboBox_BodyType";
            this.comboBox_BodyType.Size = new System.Drawing.Size(92, 21);
            this.comboBox_BodyType.TabIndex = 5;
            // 
            // textBox_Body
            // 
            this.textBox_Body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Body.Location = new System.Drawing.Point(15, 174);
            this.textBox_Body.Multiline = true;
            this.textBox_Body.Name = "textBox_Body";
            this.textBox_Body.Size = new System.Drawing.Size(773, 240);
            this.textBox_Body.TabIndex = 4;
            // 
            // checkBox_SaveToSentItems
            // 
            this.checkBox_SaveToSentItems.AutoSize = true;
            this.checkBox_SaveToSentItems.Checked = true;
            this.checkBox_SaveToSentItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SaveToSentItems.Location = new System.Drawing.Point(104, 16);
            this.checkBox_SaveToSentItems.Name = "checkBox_SaveToSentItems";
            this.checkBox_SaveToSentItems.Size = new System.Drawing.Size(116, 17);
            this.checkBox_SaveToSentItems.TabIndex = 9;
            this.checkBox_SaveToSentItems.Text = "Save to Sent Items";
            this.checkBox_SaveToSentItems.UseVisualStyleBackColor = true;
            // 
            // button_Attachments
            // 
            this.button_Attachments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Attachments.Location = new System.Drawing.Point(303, 420);
            this.button_Attachments.Name = "button_Attachments";
            this.button_Attachments.Size = new System.Drawing.Size(97, 23);
            this.button_Attachments.TabIndex = 6;
            this.button_Attachments.Text = "Attachments...";
            this.button_Attachments.UseVisualStyleBackColor = true;
            this.button_Attachments.Click += new System.EventHandler(this.Button_Attachments_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(101, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Importance :";
            // 
            // comboBox_Importance
            // 
            this.comboBox_Importance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Importance.FormattingEnabled = true;
            this.comboBox_Importance.Items.AddRange(new object[] {
            "Low",
            "Normal",
            "High"});
            this.comboBox_Importance.Location = new System.Drawing.Point(173, 43);
            this.comboBox_Importance.Name = "comboBox_Importance";
            this.comboBox_Importance.Size = new System.Drawing.Size(92, 21);
            this.comboBox_Importance.TabIndex = 10;
            // 
            // checkBox_RequestReadReceipt
            // 
            this.checkBox_RequestReadReceipt.AutoSize = true;
            this.checkBox_RequestReadReceipt.Location = new System.Drawing.Point(303, 45);
            this.checkBox_RequestReadReceipt.Name = "checkBox_RequestReadReceipt";
            this.checkBox_RequestReadReceipt.Size = new System.Drawing.Size(144, 17);
            this.checkBox_RequestReadReceipt.TabIndex = 12;
            this.checkBox_RequestReadReceipt.Text = "Request a Read Receipt";
            this.checkBox_RequestReadReceipt.UseVisualStyleBackColor = true;
            // 
            // checkBox_RequestDeliveryReceipt
            // 
            this.checkBox_RequestDeliveryReceipt.AutoSize = true;
            this.checkBox_RequestDeliveryReceipt.Location = new System.Drawing.Point(303, 16);
            this.checkBox_RequestDeliveryReceipt.Name = "checkBox_RequestDeliveryReceipt";
            this.checkBox_RequestDeliveryReceipt.Size = new System.Drawing.Size(156, 17);
            this.checkBox_RequestDeliveryReceipt.TabIndex = 11;
            this.checkBox_RequestDeliveryReceipt.Text = "Request a Delivery Receipt";
            this.checkBox_RequestDeliveryReceipt.UseVisualStyleBackColor = true;
            // 
            // SendMailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBox_RequestDeliveryReceipt);
            this.Controls.Add(this.checkBox_RequestReadReceipt);
            this.Controls.Add(this.comboBox_Importance);
            this.Controls.Add(this.button_Attachments);
            this.Controls.Add(this.checkBox_SaveToSentItems);
            this.Controls.Add(this.textBox_Body);
            this.Controls.Add(this.comboBox_BodyType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_Subject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.textBox_Bcc);
            this.Controls.Add(this.textBox_Cc);
            this.Controls.Add(this.textBox_To);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Send);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SendMailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "New Mail";
            this.Load += new System.EventHandler(this.SendMailForm_LoadAsync);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_To;
        private System.Windows.Forms.TextBox textBox_Cc;
        private System.Windows.Forms.TextBox textBox_Bcc;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Subject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_BodyType;
        private System.Windows.Forms.TextBox textBox_Body;
        private System.Windows.Forms.CheckBox checkBox_SaveToSentItems;
        private System.Windows.Forms.Button button_Attachments;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_Importance;
        private System.Windows.Forms.CheckBox checkBox_RequestReadReceipt;
        private System.Windows.Forms.CheckBox checkBox_RequestDeliveryReceipt;
    }
}