using save_point.Models;

namespace save_point
{
    /// <summary>
    /// Dialog for creating or editing a single <see cref="PlatformEntry"/>.
    /// Works entirely in memory; the caller decides when to persist.
    /// Completion is derived from the status: the completion date is only
    /// enabled and saved when the status is Completed.
    /// </summary>
    public partial class AddPlatformForm : Form
    {
        private static readonly string[] CommonPlatforms =
        {
            "PC", "PS5", "PS4", "Xbox Series X|S", "Xbox One",
            "Nintendo Switch", "Steam Deck", "Mobile", "Other"
        };

        private readonly PlatformEntry? existing;

        /// <summary>
        /// The completed entry. Set only when the dialog result is OK.
        /// In edit mode it keeps the original Id and GameId.
        /// </summary>
        public PlatformEntry? Entry { get; private set; }

        /// <summary>
        /// Pass null to add a new entry, or an existing entry to edit it.
        /// </summary>
        public AddPlatformForm(PlatformEntry? existing = null)
        {
            InitializeComponent();
            this.existing = existing;

            cmbPlatform.Items.AddRange(CommonPlatforms);
            cmbStatus.DataSource = Enum.GetValues<GameStatus>();

            btnCancel.DialogResult = DialogResult.Cancel;
            CancelButton = btnCancel;

            cmbStatus.SelectedIndexChanged += CmbStatus_SelectedIndexChanged;
            btnSave.Click += BtnSave_Click;

            if (existing is not null)
            {
                Text = "Edit Platform";
                lblHeading.Text = "Edit Platform";
                PopulateFrom(existing);
            }

            UpdateCompletionDateState();
        }

        private void PopulateFrom(PlatformEntry entry)
        {
            cmbPlatform.Text = entry.Platform;
            cmbStatus.SelectedItem = entry.Status;
            if (entry.CompletionDate is not null)
            {
                dtpCompletionDate.Value = entry.CompletionDate.Value;
            }
            nudRating.Value = (decimal)(entry.Rating ?? 0);
            nudHoursPlayed.Value = entry.HoursPlayed ?? 0;
            txtRemarks.Text = entry.Remarks;
        }

        private bool IsCompletedSelected =>
            cmbStatus.SelectedItem is GameStatus.Completed;

        private void UpdateCompletionDateState()
        {
            dtpCompletionDate.Enabled = IsCompletedSelected;
        }

        private void CmbStatus_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateCompletionDateState();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            string platformName = cmbPlatform.Text.Trim();
            if (platformName.Length == 0)
            {
                MessageBox.Show(
                    "Please choose or enter a platform.",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus.SelectedItem is not GameStatus status)
            {
                MessageBox.Show(
                    "Please choose a status.",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool completed = status == GameStatus.Completed;

            Entry = new PlatformEntry
            {
                Id = existing?.Id ?? 0,
                GameId = existing?.GameId ?? 0,
                Platform = platformName,
                Status = status,
                Completed = completed,
                CompletionDate = completed ? dtpCompletionDate.Value.Date : null,
                Rating = nudRating.Value > 0 ? (double)nudRating.Value : null,
                HoursPlayed = nudHoursPlayed.Value > 0 ? (int)nudHoursPlayed.Value : null,
                Remarks = string.IsNullOrWhiteSpace(txtRemarks.Text) ? null : txtRemarks.Text.Trim()
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
