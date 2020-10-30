namespace Office365APIEditor.UI
{
    partial class CalendarViewForm
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
            this.components = new System.ComponentModel.Container();
            this.dateTimePicker_EndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_StartTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView_ItemList = new System.Windows.Forms.DataGridView();
            this.dataGridView_ItemProps = new System.Windows.Forms.DataGridView();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip_ItemList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.displayAttachmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemProps)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip_ItemList.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker_EndTime
            // 
            this.dateTimePicker_EndTime.CustomFormat = "HH:mm";
            this.dateTimePicker_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_EndTime.Location = new System.Drawing.Point(447, 12);
            this.dateTimePicker_EndTime.Name = "dateTimePicker_EndTime";
            this.dateTimePicker_EndTime.ShowUpDown = true;
            this.dateTimePicker_EndTime.Size = new System.Drawing.Size(59, 20);
            this.dateTimePicker_EndTime.TabIndex = 25;
            // 
            // dateTimePicker_StartTime
            // 
            this.dateTimePicker_StartTime.CustomFormat = "HH:mm";
            this.dateTimePicker_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_StartTime.Location = new System.Drawing.Point(182, 12);
            this.dateTimePicker_StartTime.Name = "dateTimePicker_StartTime";
            this.dateTimePicker_StartTime.ShowUpDown = true;
            this.dateTimePicker_StartTime.Size = new System.Drawing.Size(59, 20);
            this.dateTimePicker_StartTime.TabIndex = 23;
            // 
            // dateTimePicker_StartDate
            // 
            this.dateTimePicker_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_StartDate.Location = new System.Drawing.Point(76, 12);
            this.dateTimePicker_StartDate.Name = "dateTimePicker_StartDate";
            this.dateTimePicker_StartDate.Size = new System.Drawing.Size(100, 20);
            this.dateTimePicker_StartDate.TabIndex = 22;
            this.dateTimePicker_StartDate.ValueChanged += new System.EventHandler(this.DateTimePicker_StartDate_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Start (Local):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(271, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "End (Local):";
            // 
            // dateTimePicker_EndDate
            // 
            this.dateTimePicker_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_EndDate.Location = new System.Drawing.Point(341, 12);
            this.dateTimePicker_EndDate.Name = "dateTimePicker_EndDate";
            this.dateTimePicker_EndDate.Size = new System.Drawing.Size(100, 20);
            this.dateTimePicker_EndDate.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Refresh);
            this.panel1.Controls.Add(this.dateTimePicker_StartDate);
            this.panel1.Controls.Add(this.dateTimePicker_EndTime);
            this.panel1.Controls.Add(this.dateTimePicker_EndDate);
            this.panel1.Controls.Add(this.dateTimePicker_StartTime);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 40);
            this.panel1.TabIndex = 26;
            // 
            // button_Refresh
            // 
            this.button_Refresh.Location = new System.Drawing.Point(543, 9);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(75, 23);
            this.button_Refresh.TabIndex = 26;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.Button_Refresh_ClickAsync);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(881, 491);
            this.panel2.TabIndex = 27;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView_ItemList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_ItemProps);
            this.splitContainer1.Size = new System.Drawing.Size(881, 491);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.TabIndex = 1;
            // 
            // dataGridView_ItemList
            // 
            this.dataGridView_ItemList.AllowUserToAddRows = false;
            this.dataGridView_ItemList.AllowUserToDeleteRows = false;
            this.dataGridView_ItemList.AllowUserToOrderColumns = true;
            this.dataGridView_ItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_ItemList.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_ItemList.MultiSelect = false;
            this.dataGridView_ItemList.Name = "dataGridView_ItemList";
            this.dataGridView_ItemList.ReadOnly = true;
            this.dataGridView_ItemList.Size = new System.Drawing.Size(881, 224);
            this.dataGridView_ItemList.TabIndex = 0;
            this.dataGridView_ItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_ItemList_CellClickAsync);
            this.dataGridView_ItemList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_ItemList_CellDoubleClick);
            this.dataGridView_ItemList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_ItemList_CellMouseClick);
            // 
            // dataGridView_ItemProps
            // 
            this.dataGridView_ItemProps.AllowUserToAddRows = false;
            this.dataGridView_ItemProps.AllowUserToDeleteRows = false;
            this.dataGridView_ItemProps.AllowUserToOrderColumns = true;
            this.dataGridView_ItemProps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ItemProps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Property,
            this.Value,
            this.Type});
            this.dataGridView_ItemProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_ItemProps.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_ItemProps.Name = "dataGridView_ItemProps";
            this.dataGridView_ItemProps.ReadOnly = true;
            this.dataGridView_ItemProps.Size = new System.Drawing.Size(881, 263);
            this.dataGridView_ItemProps.TabIndex = 0;
            this.dataGridView_ItemProps.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_ItemProps_CellDoubleClick);
            // 
            // Property
            // 
            this.Property.HeaderText = "Property";
            this.Property.Name = "Property";
            this.Property.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 509);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(881, 22);
            this.statusStrip1.TabIndex = 28;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Status
            // 
            this.toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            this.toolStripStatusLabel_Status.Size = new System.Drawing.Size(0, 17);
            // 
            // contextMenuStrip_ItemList
            // 
            this.contextMenuStrip_ItemList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayAttachmentsToolStripMenuItem});
            this.contextMenuStrip_ItemList.Name = "contextMenuStrip_ItemList";
            this.contextMenuStrip_ItemList.Size = new System.Drawing.Size(193, 26);
            // 
            // displayAttachmentsToolStripMenuItem
            // 
            this.displayAttachmentsToolStripMenuItem.Name = "displayAttachmentsToolStripMenuItem";
            this.displayAttachmentsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.displayAttachmentsToolStripMenuItem.Text = "Display Attachments...";
            this.displayAttachmentsToolStripMenuItem.Click += new System.EventHandler(this.DisplayAttachmentsToolStripMenuItem_Click);
            // 
            // CalendarViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 531);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(660, 360);
            this.Name = "CalendarViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Calendar View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalendarViewForm_FormClosing);
            this.Load += new System.EventHandler(this.CalendarViewForm_LoadAsync);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemProps)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip_ItemList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_EndTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker_EndDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView_ItemList;
        private System.Windows.Forms.DataGridView dataGridView_ItemProps;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Status;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_ItemList;
        private System.Windows.Forms.ToolStripMenuItem displayAttachmentsToolStripMenuItem;
    }
}