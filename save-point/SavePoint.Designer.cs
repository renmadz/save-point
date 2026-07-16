namespace save_point
{
    partial class SavePoint
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvGames = new DataGridView();
            pnlTop = new Panel();
            btnAdd = new Button();
            cmbFilter = new ComboBox();
            cmbSort = new ComboBox();
            txtSearch = new TextBox();
            lblSearch = new Label();
            statusStripMain = new StatusStrip();
            tslStatus = new ToolStripStatusLabel();
            number = new DataGridViewTextBoxColumn();
            cover = new DataGridViewImageColumn();
            title = new DataGridViewTextBoxColumn();
            rating = new DataGridViewTextBoxColumn();
            platform = new DataGridViewTextBoxColumn();
            status = new DataGridViewTextBoxColumn();
            hours = new DataGridViewTextBoxColumn();
            remarks = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvGames).BeginInit();
            pnlTop.SuspendLayout();
            statusStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // dgvGames
            // 
            dgvGames.AllowUserToAddRows = false;
            dgvGames.AllowUserToDeleteRows = false;
            dgvGames.AllowUserToResizeRows = false;
            dgvGames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGames.Columns.AddRange(new DataGridViewColumn[] { number, cover, title, rating, platform, status, hours, remarks });
            dgvGames.Dock = DockStyle.Fill;
            dgvGames.Location = new Point(0, 75);
            dgvGames.MultiSelect = false;
            dgvGames.Name = "dgvGames";
            dgvGames.ReadOnly = true;
            dgvGames.RowHeadersVisible = false;
            dgvGames.RowHeadersWidth = 51;
            dgvGames.RowTemplate.Height = 75;
            dgvGames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGames.Size = new Size(982, 502);
            dgvGames.TabIndex = 0;
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(btnAdd);
            pnlTop.Controls.Add(cmbFilter);
            pnlTop.Controls.Add(cmbSort);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(lblSearch);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(982, 75);
            pnlTop.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAdd.Location = new Point(155, 24);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(107, 31);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "+ Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // cmbFilter
            // 
            cmbFilter.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(268, 24);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(151, 31);
            cmbFilter.TabIndex = 3;
            cmbFilter.Text = "Filter";
            // 
            // cmbSort
            // 
            cmbSort.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbSort.FormattingEnabled = true;
            cmbSort.Location = new Point(425, 24);
            cmbSort.Name = "cmbSort";
            cmbSort.Size = new Size(151, 31);
            cmbSort.TabIndex = 2;
            cmbSort.Text = "Sort";
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.Location = new Point(705, 25);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = " Search...";
            txtSearch.Size = new Size(265, 30);
            txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearch.Location = new Point(643, 27);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(65, 23);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";
            // 
            // statusStripMain
            // 
            statusStripMain.ImageScalingSize = new Size(20, 20);
            statusStripMain.Items.AddRange(new ToolStripItem[] { tslStatus });
            statusStripMain.Location = new Point(0, 577);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(982, 26);
            statusStripMain.TabIndex = 2;
            statusStripMain.Text = "statusStrip1";
            // 
            // tslStatus
            // 
            tslStatus.Name = "tslStatus";
            tslStatus.Size = new Size(151, 20);
            tslStatus.Text = "toolStripStatusLabel1";
            // 
            // number
            // 
            number.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            number.HeaderText = "#";
            number.MinimumWidth = 6;
            number.Name = "number";
            number.ReadOnly = true;
            number.Width = 45;
            // 
            // cover
            // 
            cover.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            cover.HeaderText = "Cover Art";
            cover.ImageLayout = DataGridViewImageCellLayout.Zoom;
            cover.MinimumWidth = 6;
            cover.Name = "cover";
            cover.ReadOnly = true;
            cover.Resizable = DataGridViewTriState.True;
            cover.Width = 90;
            // 
            // title
            // 
            title.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            title.HeaderText = "Title";
            title.MinimumWidth = 6;
            title.Name = "title";
            title.ReadOnly = true;
            // 
            // rating
            // 
            rating.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            rating.HeaderText = "Rating";
            rating.MinimumWidth = 6;
            rating.Name = "rating";
            rating.ReadOnly = true;
            rating.Width = 70;
            // 
            // platform
            // 
            platform.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            platform.HeaderText = "Platform";
            platform.MinimumWidth = 6;
            platform.Name = "platform";
            platform.ReadOnly = true;
            platform.Width = 110;
            // 
            // status
            // 
            status.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            status.HeaderText = "Status";
            status.MinimumWidth = 6;
            status.Name = "status";
            status.ReadOnly = true;
            status.Width = 110;
            // 
            // hours
            // 
            hours.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            hours.HeaderText = "Hours Played";
            hours.MinimumWidth = 6;
            hours.Name = "hours";
            hours.ReadOnly = true;
            hours.Width = 90;
            // 
            // remarks
            // 
            remarks.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            remarks.HeaderText = "Remarks";
            remarks.MinimumWidth = 6;
            remarks.Name = "remarks";
            remarks.ReadOnly = true;
            // 
            // SavePoint
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 603);
            Controls.Add(dgvGames);
            Controls.Add(statusStripMain);
            Controls.Add(pnlTop);
            MinimumSize = new Size(1000, 650);
            Name = "SavePoint";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Save Point";
            ((System.ComponentModel.ISupportInitialize)dgvGames).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvGames;
        private Panel pnlTop;
        private ComboBox cmbFilter;
        private ComboBox cmbSort;
        private TextBox txtSearch;
        private Label lblSearch;
        private Button btnAdd;
        private StatusStrip statusStripMain;
        private ToolStripStatusLabel tslStatus;
        private DataGridViewTextBoxColumn number;
        private DataGridViewImageColumn cover;
        private DataGridViewTextBoxColumn title;
        private DataGridViewTextBoxColumn rating;
        private DataGridViewTextBoxColumn platform;
        private DataGridViewTextBoxColumn status;
        private DataGridViewTextBoxColumn hours;
        private DataGridViewTextBoxColumn remarks;
    }
}
