namespace save_point
{
    partial class AddPlatformForm
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
            components = new System.ComponentModel.Container();
            pnlTop = new Panel();
            lblHeading = new Label();
            pnlBottom = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            pnlContent = new Panel();
            txtRemarks = new TextBox();
            dtpCompletionDate = new DateTimePicker();
            nudHoursPlayed = new NumericUpDown();
            nudRating = new NumericUpDown();
            cmbStatus = new ComboBox();
            cmbPlatform = new ComboBox();
            lblPlatform = new Label();
            lblRemarks = new Label();
            lblCompletion = new Label();
            lblHours = new Label();
            lblRating = new Label();
            lblStatus = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudHoursPlayed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRating).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblHeading);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(332, 60);
            pnlTop.TabIndex = 0;
            // 
            // lblHeading
            // 
            lblHeading.AutoSize = true;
            lblHeading.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeading.Location = new Point(12, 9);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(191, 37);
            lblHeading.TabIndex = 0;
            lblHeading.Text = "Add Platform";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 493);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(332, 60);
            pnlBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(235, 19);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(135, 19);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(txtRemarks);
            pnlContent.Controls.Add(dtpCompletionDate);
            pnlContent.Controls.Add(nudHoursPlayed);
            pnlContent.Controls.Add(nudRating);
            pnlContent.Controls.Add(cmbStatus);
            pnlContent.Controls.Add(cmbPlatform);
            pnlContent.Controls.Add(lblPlatform);
            pnlContent.Controls.Add(lblRemarks);
            pnlContent.Controls.Add(lblCompletion);
            pnlContent.Controls.Add(lblHours);
            pnlContent.Controls.Add(lblRating);
            pnlContent.Controls.Add(lblStatus);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(332, 433);
            pnlContent.TabIndex = 2;
            //
            // txtRemarks
            // 
            txtRemarks.AcceptsReturn = true;
            txtRemarks.AcceptsTab = true;
            txtRemarks.BorderStyle = BorderStyle.FixedSingle;
            txtRemarks.Location = new Point(12, 270);
            txtRemarks.Multiline = true;
            txtRemarks.Name = "txtRemarks";
            txtRemarks.ScrollBars = ScrollBars.Vertical;
            txtRemarks.Size = new Size(308, 157);
            txtRemarks.TabIndex = 10;
            // 
            // dtpCompletionDate
            // 
            dtpCompletionDate.Location = new Point(12, 217);
            dtpCompletionDate.Name = "dtpCompletionDate";
            dtpCompletionDate.Size = new Size(250, 27);
            dtpCompletionDate.TabIndex = 9;
            // 
            // nudHoursPlayed
            // 
            nudHoursPlayed.Location = new Point(179, 164);
            nudHoursPlayed.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudHoursPlayed.Name = "nudHoursPlayed";
            nudHoursPlayed.Size = new Size(150, 27);
            nudHoursPlayed.TabIndex = 8;
            // 
            // nudRating
            // 
            nudRating.DecimalPlaces = 1;
            nudRating.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            nudRating.Location = new Point(12, 164);
            nudRating.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudRating.Name = "nudRating";
            nudRating.Size = new Size(150, 27);
            nudRating.TabIndex = 7;
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(12, 80);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(300, 28);
            cmbStatus.TabIndex = 6;
            // 
            // cmbPlatform
            // 
            cmbPlatform.FormattingEnabled = true;
            cmbPlatform.Location = new Point(12, 26);
            cmbPlatform.Name = "cmbPlatform";
            cmbPlatform.Size = new Size(300, 28);
            cmbPlatform.TabIndex = 5;
            // 
            // lblPlatform
            // 
            lblPlatform.AutoSize = true;
            lblPlatform.Location = new Point(12, 3);
            lblPlatform.Name = "lblPlatform";
            lblPlatform.Size = new Size(66, 20);
            lblPlatform.TabIndex = 3;
            lblPlatform.Text = "Platform";
            // 
            // lblRemarks
            // 
            lblRemarks.AutoSize = true;
            lblRemarks.Location = new Point(12, 247);
            lblRemarks.Name = "lblRemarks";
            lblRemarks.Size = new Size(65, 20);
            lblRemarks.TabIndex = 4;
            lblRemarks.Text = "Remarks";
            // 
            // lblCompletion
            // 
            lblCompletion.AutoSize = true;
            lblCompletion.Location = new Point(12, 194);
            lblCompletion.Name = "lblCompletion";
            lblCompletion.Size = new Size(123, 20);
            lblCompletion.TabIndex = 3;
            lblCompletion.Text = "Completion Date";
            // 
            // lblHours
            // 
            lblHours.AutoSize = true;
            lblHours.Location = new Point(179, 141);
            lblHours.Name = "lblHours";
            lblHours.Size = new Size(96, 20);
            lblHours.TabIndex = 2;
            lblHours.Text = "Hours Played";
            // 
            // lblRating
            // 
            lblRating.AutoSize = true;
            lblRating.Location = new Point(12, 141);
            lblRating.Name = "lblRating";
            lblRating.Size = new Size(52, 20);
            lblRating.TabIndex = 1;
            lblRating.Text = "Rating";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 57);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Status";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // AddPlatformForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 553);
            Controls.Add(pnlContent);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddPlatformForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Platform";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudHoursPlayed).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRating).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Panel pnlBottom;
        private Panel pnlContent;
        private Label lblHeading;
        private Label lblPlatform;
        private Label lblRemarks;
        private Label lblCompletion;
        private Label lblHours;
        private Label lblRating;
        private Label lblStatus;
        private NumericUpDown nudRating;
        private ComboBox cmbStatus;
        private ComboBox cmbPlatform;
        private TextBox txtRemarks;
        private DateTimePicker dtpCompletionDate;
        private NumericUpDown nudHoursPlayed;
        private Button btnCancel;
        private Button btnSave;
        private ContextMenuStrip contextMenuStrip1;
    }
}