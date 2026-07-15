namespace save_point
{
    partial class AddEditGameForm
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
            pnlTop = new Panel();
            lblHeading = new Label();
            pnlContent = new Panel();
            btnRemovePlatform = new Button();
            btnEditPlatform = new Button();
            btnAddPlatform = new Button();
            lvPlatforms = new ListView();
            platform = new ColumnHeader();
            status = new ColumnHeader();
            rating = new ColumnHeader();
            lblPlatform = new Label();
            txtTitle = new TextBox();
            lblTitle = new Label();
            pnlBottom = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            picCover = new PictureBox();
            btnChangeCover = new Button();
            btnRemoveCover = new Button();
            pnlTop.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblHeading);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(532, 60);
            pnlTop.TabIndex = 0;
            // 
            // lblHeading
            // 
            lblHeading.AutoSize = true;
            lblHeading.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeading.Location = new Point(12, 9);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(151, 37);
            lblHeading.TabIndex = 0;
            lblHeading.Text = "Add Game";
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(picCover);
            pnlContent.Controls.Add(btnChangeCover);
            pnlContent.Controls.Add(btnRemoveCover);
            pnlContent.Controls.Add(btnRemovePlatform);
            pnlContent.Controls.Add(btnEditPlatform);
            pnlContent.Controls.Add(btnAddPlatform);
            pnlContent.Controls.Add(lvPlatforms);
            pnlContent.Controls.Add(lblPlatform);
            pnlContent.Controls.Add(txtTitle);
            pnlContent.Controls.Add(lblTitle);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(532, 323);
            pnlContent.TabIndex = 1;
            // 
            // btnRemovePlatform
            // 
            btnRemovePlatform.Location = new Point(412, 227);
            btnRemovePlatform.Name = "btnRemovePlatform";
            btnRemovePlatform.Size = new Size(100, 30);
            btnRemovePlatform.TabIndex = 6;
            btnRemovePlatform.Text = "Remove";
            btnRemovePlatform.UseVisualStyleBackColor = true;
            // 
            // btnEditPlatform
            // 
            btnEditPlatform.Location = new Point(412, 176);
            btnEditPlatform.Name = "btnEditPlatform";
            btnEditPlatform.Size = new Size(100, 30);
            btnEditPlatform.TabIndex = 5;
            btnEditPlatform.Text = "Edit";
            btnEditPlatform.UseVisualStyleBackColor = true;
            // 
            // btnAddPlatform
            // 
            btnAddPlatform.Location = new Point(412, 122);
            btnAddPlatform.Name = "btnAddPlatform";
            btnAddPlatform.Size = new Size(100, 30);
            btnAddPlatform.TabIndex = 4;
            btnAddPlatform.Text = "Add";
            btnAddPlatform.UseVisualStyleBackColor = true;
            // 
            // lvPlatforms
            // 
            lvPlatforms.Columns.AddRange(new ColumnHeader[] { platform, status, rating });
            lvPlatforms.Location = new Point(12, 122);
            lvPlatforms.Name = "lvPlatforms";
            lvPlatforms.Size = new Size(390, 135);
            lvPlatforms.TabIndex = 3;
            lvPlatforms.UseCompatibleStateImageBehavior = false;
            // 
            // platform
            // 
            platform.Text = "Platform";
            platform.Width = 210;
            // 
            // status
            // 
            status.Text = "Status";
            status.Width = 150;
            // 
            // rating
            // 
            rating.Text = "Rating";
            rating.Width = 90;
            // 
            // lblPlatform
            // 
            lblPlatform.AutoSize = true;
            lblPlatform.Location = new Point(12, 99);
            lblPlatform.Name = "lblPlatform";
            lblPlatform.Size = new Size(66, 20);
            lblPlatform.TabIndex = 1;
            lblPlatform.Text = "Platform";
            // 
            // txtTitle
            // 
            txtTitle.BorderStyle = BorderStyle.FixedSingle;
            txtTitle.Location = new Point(12, 42);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(500, 27);
            txtTitle.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(12, 19);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(38, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title";
            //
            // picCover
            //
            picCover.BorderStyle = BorderStyle.FixedSingle;
            picCover.Location = new Point(536, 42);
            picCover.Name = "picCover";
            picCover.Size = new Size(140, 187);
            picCover.SizeMode = PictureBoxSizeMode.Zoom;
            picCover.TabIndex = 7;
            picCover.TabStop = false;
            //
            // btnChangeCover
            //
            btnChangeCover.Location = new Point(536, 235);
            btnChangeCover.Name = "btnChangeCover";
            btnChangeCover.Size = new Size(140, 30);
            btnChangeCover.TabIndex = 8;
            btnChangeCover.Text = "Change Cover...";
            btnChangeCover.UseVisualStyleBackColor = true;
            //
            // btnRemoveCover
            //
            btnRemoveCover.Location = new Point(536, 271);
            btnRemoveCover.Name = "btnRemoveCover";
            btnRemoveCover.Size = new Size(140, 30);
            btnRemoveCover.TabIndex = 9;
            btnRemoveCover.Text = "Remove Cover";
            btnRemoveCover.UseVisualStyleBackColor = true;
            //
            // pnlBottom
            //
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 383);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(532, 70);
            pnlBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(568, 19);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(458, 19);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 30);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // AddEditGameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(688, 453);
            Controls.Add(pnlContent);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddEditGameForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Game";
            ((System.ComponentModel.ISupportInitialize)picCover).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblHeading;
        private Panel pnlContent;
        private Panel pnlBottom;
        private Label lblPlatform;
        private TextBox txtTitle;
        private Label lblTitle;
        private Button btnRemovePlatform;
        private Button btnEditPlatform;
        private Button btnAddPlatform;
        private ListView lvPlatforms;
        private ColumnHeader platform;
        private ColumnHeader status;
        private ColumnHeader rating;
        private Button btnCancel;
        private Button btnSave;
        private PictureBox picCover;
        private Button btnChangeCover;
        private Button btnRemoveCover;
    }
}