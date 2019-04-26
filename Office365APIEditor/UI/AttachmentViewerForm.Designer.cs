namespace Office365APIEditor
{
    partial class AttachmentViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttachmentViewerForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView_AttachmentList = new System.Windows.Forms.DataGridView();
            this.NameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_ItemProps = new System.Windows.Forms.DataGridView();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_AttachmentList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AttachmentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemProps)).BeginInit();
            this.contextMenuStrip_AttachmentList.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView_AttachmentList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_ItemProps);
            this.splitContainer1.Size = new System.Drawing.Size(881, 491);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 1;
            // 
            // dataGridView_AttachmentList
            // 
            this.dataGridView_AttachmentList.AllowUserToAddRows = false;
            this.dataGridView_AttachmentList.AllowUserToDeleteRows = false;
            this.dataGridView_AttachmentList.AllowUserToOrderColumns = true;
            this.dataGridView_AttachmentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_AttachmentList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameCol,
            this.ID,
            this.ContentType});
            this.dataGridView_AttachmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_AttachmentList.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_AttachmentList.MultiSelect = false;
            this.dataGridView_AttachmentList.Name = "dataGridView_AttachmentList";
            this.dataGridView_AttachmentList.ReadOnly = true;
            this.dataGridView_AttachmentList.Size = new System.Drawing.Size(881, 225);
            this.dataGridView_AttachmentList.TabIndex = 0;
            this.dataGridView_AttachmentList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_AttachmentList_CellClick);
            this.dataGridView_AttachmentList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_AttachmentList_CellDoubleClick);
            this.dataGridView_AttachmentList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_AttachmentList_CellMouseClick);
            // 
            // NameCol
            // 
            this.NameCol.HeaderText = "Name";
            this.NameCol.Name = "NameCol";
            this.NameCol.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // ContentType
            // 
            this.ContentType.HeaderText = "ContentType";
            this.ContentType.Name = "ContentType";
            this.ContentType.ReadOnly = true;
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
            this.dataGridView_ItemProps.Size = new System.Drawing.Size(881, 262);
            this.dataGridView_ItemProps.TabIndex = 0;
            this.dataGridView_ItemProps.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ItemProps_CellDoubleClick);
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
            // contextMenuStrip_AttachmentList
            // 
            this.contextMenuStrip_AttachmentList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem});
            this.contextMenuStrip_AttachmentList.Name = "contextMenuStrip_ItemList";
            this.contextMenuStrip_AttachmentList.Size = new System.Drawing.Size(138, 26);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.downloadToolStripMenuItem.Text = "Download...";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "All files (*.*)|*.*";
            // 
            // AttachmentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 491);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AttachmentViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AttachmentViewerForm";
            this.Load += new System.EventHandler(this.AttachmentViewerForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AttachmentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemProps)).EndInit();
            this.contextMenuStrip_AttachmentList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView_AttachmentList;
        private System.Windows.Forms.DataGridView dataGridView_ItemProps;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_AttachmentList;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContentType;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}