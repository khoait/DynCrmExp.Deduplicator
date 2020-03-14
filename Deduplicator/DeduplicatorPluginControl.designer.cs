namespace DynCrmExp.Deduplicator
{
    partial class DeduplicatorPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvFields = new System.Windows.Forms.DataGridView();
            this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogicalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatch = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colView = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkIgnoreBlank = new System.Windows.Forms.CheckBox();
            this.chkIgnoreWhiteSpace = new System.Windows.Forms.CheckBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEntities = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gbDuplicated = new System.Windows.Forms.GroupBox();
            this.dgvDuplicated = new System.Windows.Forms.DataGridView();
            this.gbDuplicates = new System.Windows.Forms.GroupBox();
            this.dgvDuplicates = new System.Windows.Forms.DataGridView();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.gbDuplicated.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicated)).BeginInit();
            this.gbDuplicates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicates)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbLoadEntities,
            this.tsbRun,
            this.tsbExport});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(835, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = global::DynCrmExp.Deduplicator.Properties.Resources.ico_Close;
            this.tsbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(56, 22);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Image = global::DynCrmExp.Deduplicator.Properties.Resources.ico_16_0;
            this.tsbLoadEntities.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(94, 22);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.btnLoadEntities_Click);
            // 
            // tsbRun
            // 
            this.tsbRun.Image = global::DynCrmExp.Deduplicator.Properties.Resources.DuplicateDetection_16;
            this.tsbRun.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(107, 22);
            this.tsbRun.Text = "Find duplicates";
            this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::DynCrmExp.Deduplicator.Properties.Resources.ico_16_9507_Excel;
            this.tsbExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(119, 22);
            this.tsbExport.Text = "Export Duplicates";
            this.tsbExport.ToolTipText = "Export Duplicates Data to CSV";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(835, 451);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvFields);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbEntities);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 451);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Duplicate Detection Settings";
            // 
            // dgvFields
            // 
            this.dgvFields.AllowUserToAddRows = false;
            this.dgvFields.AllowUserToDeleteRows = false;
            this.dgvFields.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvFields.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDisplayName,
            this.colLogicalName,
            this.colMatch,
            this.colView});
            this.dgvFields.Location = new System.Drawing.Point(9, 83);
            this.dgvFields.MultiSelect = false;
            this.dgvFields.Name = "dgvFields";
            this.dgvFields.RowHeadersVisible = false;
            this.dgvFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFields.Size = new System.Drawing.Size(294, 267);
            this.dgvFields.TabIndex = 6;
            // 
            // colDisplayName
            // 
            this.colDisplayName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDisplayName.HeaderText = "Display Name";
            this.colDisplayName.Name = "colDisplayName";
            this.colDisplayName.ReadOnly = true;
            // 
            // colLogicalName
            // 
            this.colLogicalName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLogicalName.HeaderText = "Logical Name";
            this.colLogicalName.Name = "colLogicalName";
            this.colLogicalName.ReadOnly = true;
            // 
            // colMatch
            // 
            this.colMatch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colMatch.HeaderText = "Match";
            this.colMatch.Name = "colMatch";
            this.colMatch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colMatch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colMatch.Width = 45;
            // 
            // colView
            // 
            this.colView.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colView.HeaderText = "View";
            this.colView.Name = "colView";
            this.colView.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colView.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colView.Width = 45;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkIgnoreBlank);
            this.groupBox4.Controls.Add(this.chkIgnoreWhiteSpace);
            this.groupBox4.Controls.Add(this.chkCaseSensitive);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(3, 356);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(304, 92);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Text Matching Options";
            // 
            // chkIgnoreBlank
            // 
            this.chkIgnoreBlank.AutoSize = true;
            this.chkIgnoreBlank.Checked = true;
            this.chkIgnoreBlank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreBlank.Location = new System.Drawing.Point(6, 65);
            this.chkIgnoreBlank.Name = "chkIgnoreBlank";
            this.chkIgnoreBlank.Size = new System.Drawing.Size(119, 17);
            this.chkIgnoreBlank.TabIndex = 3;
            this.chkIgnoreBlank.Text = "Ignore blank values";
            this.chkIgnoreBlank.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreWhiteSpace
            // 
            this.chkIgnoreWhiteSpace.AutoSize = true;
            this.chkIgnoreWhiteSpace.Checked = true;
            this.chkIgnoreWhiteSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreWhiteSpace.Location = new System.Drawing.Point(6, 42);
            this.chkIgnoreWhiteSpace.Name = "chkIgnoreWhiteSpace";
            this.chkIgnoreWhiteSpace.Size = new System.Drawing.Size(207, 17);
            this.chkIgnoreWhiteSpace.TabIndex = 2;
            this.chkIgnoreWhiteSpace.Text = "Ignore leading and trailing white space";
            this.chkIgnoreWhiteSpace.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Checked = true;
            this.chkCaseSensitive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCaseSensitive.Location = new System.Drawing.Point(6, 19);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(92, 17);
            this.chkCaseSensitive.TabIndex = 0;
            this.chkCaseSensitive.Text = "Case sentitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Fields";
            // 
            // cbEntities
            // 
            this.cbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEntities.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEntities.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEntities.FormattingEnabled = true;
            this.cbEntities.Location = new System.Drawing.Point(9, 39);
            this.cbEntities.Name = "cbEntities";
            this.cbEntities.Size = new System.Drawing.Size(294, 21);
            this.cbEntities.TabIndex = 1;
            this.cbEntities.SelectedIndexChanged += new System.EventHandler(this.cbEntities_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entities Name";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gbDuplicated);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gbDuplicates);
            this.splitContainer2.Size = new System.Drawing.Size(521, 451);
            this.splitContainer2.SplitterDistance = 184;
            this.splitContainer2.TabIndex = 0;
            // 
            // gbDuplicated
            // 
            this.gbDuplicated.Controls.Add(this.dgvDuplicated);
            this.gbDuplicated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDuplicated.Location = new System.Drawing.Point(0, 0);
            this.gbDuplicated.Name = "gbDuplicated";
            this.gbDuplicated.Size = new System.Drawing.Size(521, 184);
            this.gbDuplicated.TabIndex = 0;
            this.gbDuplicated.TabStop = false;
            this.gbDuplicated.Text = "Duplicated Records";
            // 
            // dgvDuplicated
            // 
            this.dgvDuplicated.AllowUserToAddRows = false;
            this.dgvDuplicated.AllowUserToDeleteRows = false;
            this.dgvDuplicated.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvDuplicated.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDuplicated.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDuplicated.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDuplicated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDuplicated.Location = new System.Drawing.Point(3, 16);
            this.dgvDuplicated.MultiSelect = false;
            this.dgvDuplicated.Name = "dgvDuplicated";
            this.dgvDuplicated.ReadOnly = true;
            this.dgvDuplicated.RowHeadersVisible = false;
            this.dgvDuplicated.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDuplicated.Size = new System.Drawing.Size(515, 165);
            this.dgvDuplicated.TabIndex = 7;
            this.dgvDuplicated.SelectionChanged += new System.EventHandler(this.dgvDuplicated_SelectionChanged);
            // 
            // gbDuplicates
            // 
            this.gbDuplicates.Controls.Add(this.dgvDuplicates);
            this.gbDuplicates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDuplicates.Location = new System.Drawing.Point(0, 0);
            this.gbDuplicates.Name = "gbDuplicates";
            this.gbDuplicates.Size = new System.Drawing.Size(521, 263);
            this.gbDuplicates.TabIndex = 0;
            this.gbDuplicates.TabStop = false;
            this.gbDuplicates.Text = "Possible Duplicates";
            // 
            // dgvDuplicates
            // 
            this.dgvDuplicates.AllowUserToAddRows = false;
            this.dgvDuplicates.AllowUserToDeleteRows = false;
            this.dgvDuplicates.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvDuplicates.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDuplicates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDuplicates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDuplicates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDuplicates.Location = new System.Drawing.Point(3, 16);
            this.dgvDuplicates.MultiSelect = false;
            this.dgvDuplicates.Name = "dgvDuplicates";
            this.dgvDuplicates.ReadOnly = true;
            this.dgvDuplicates.RowHeadersVisible = false;
            this.dgvDuplicates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDuplicates.Size = new System.Drawing.Size(515, 244);
            this.dgvDuplicates.TabIndex = 8;
            this.dgvDuplicates.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDuplicates_CellClick);
            // 
            // DeduplicatorPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "DeduplicatorPluginControl";
            this.Size = new System.Drawing.Size(835, 476);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.gbDuplicated.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicated)).EndInit();
            this.gbDuplicates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbEntities;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkIgnoreWhiteSpace;
        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox gbDuplicated;
        private System.Windows.Forms.GroupBox gbDuplicates;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.DataGridView dgvFields;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogicalName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMatch;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colView;
        private System.Windows.Forms.DataGridView dgvDuplicated;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.DataGridView dgvDuplicates;
        private System.Windows.Forms.CheckBox chkIgnoreBlank;
        private System.Windows.Forms.ToolStripButton tsbExport;
    }
}
