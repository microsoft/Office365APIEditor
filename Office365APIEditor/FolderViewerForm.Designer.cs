namespace Office365APIEditor
{
    partial class FolderViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderViewerForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView_ItemList = new System.Windows.Forms.DataGridView();
            this.dataGridView_ItemProps = new System.Windows.Forms.DataGridView();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemProps)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView_ItemList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_ItemProps);
            this.splitContainer1.Size = new System.Drawing.Size(881, 491);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView_ItemList
            // 
            this.dataGridView_ItemList.AllowUserToAddRows = false;
            this.dataGridView_ItemList.AllowUserToDeleteRows = false;
            this.dataGridView_ItemList.AllowUserToOrderColumns = true;
            this.dataGridView_ItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_ItemList.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_ItemList.Name = "dataGridView_ItemList";
            this.dataGridView_ItemList.ReadOnly = true;
            this.dataGridView_ItemList.Size = new System.Drawing.Size(881, 225);
            this.dataGridView_ItemList.TabIndex = 0;
            this.dataGridView_ItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ItemList_CellClick);
            this.dataGridView_ItemList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ItemList_CellDoubleClick);
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
            // FolderViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 491);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FolderViewerForm";
            this.Text = "FolderViewerForm";
            this.Load += new System.EventHandler(this.FolderViewerForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ItemProps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView_ItemList;
        private System.Windows.Forms.DataGridView dataGridView_ItemProps;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
    }
}