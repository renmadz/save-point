using save_point.Models;
using save_point.Services;

namespace save_point
{
    /// <summary>
    /// Dialog for adding a new game or editing an existing one, including
    /// its platform entries and cover art. Platform entries are kept in
    /// memory while the dialog is open and only persisted after the game
    /// saves successfully. Cover images are stored on disk by
    /// <see cref="CoverArtService"/>; only the path is saved with the game.
    /// </summary>
    public partial class AddEditGameForm : Form
    {
        private readonly GameService gameService = new();
        private readonly PlatformEntryService platformEntryService = new();
        private readonly CoverArtService coverArtService = new();

        private readonly Game? existing;
        private readonly List<PlatformEntry> entries = new();
        private readonly List<int> removedEntryIds = new();
        private readonly ContextMenuStrip changeCoverMenu = new();

        private string? coverArtPath;

        /// <summary>
        /// Pass null to add a new game, or an existing game (with its
        /// platform entries loaded) to edit it.
        /// </summary>
        public AddEditGameForm(Game? existing = null)
        {
            InitializeComponent();
            this.existing = existing;

            // The designer leaves the ListView in its default LargeIcon view,
            // which hides the columns; Details is required to show them.
            lvPlatforms.View = View.Details;
            lvPlatforms.FullRowSelect = true;
            lvPlatforms.MultiSelect = false;

            btnCancel.DialogResult = DialogResult.Cancel;
            CancelButton = btnCancel;
            AcceptButton = btnSave;

            changeCoverMenu.Items.Add("From File...", null, CoverFromFile_Click);
            changeCoverMenu.Items.Add("From URL...", null, CoverFromUrl_Click);

            btnAddPlatform.Click += BtnAddPlatform_Click;
            btnEditPlatform.Click += BtnEditPlatform_Click;
            btnRemovePlatform.Click += BtnRemovePlatform_Click;
            btnChangeCover.Click += BtnChangeCover_Click;
            btnRemoveCover.Click += BtnRemoveCover_Click;
            btnSave.Click += BtnSave_Click;
            Load += AddEditGameForm_Load;

            if (existing is not null)
            {
                Text = "Edit Game";
                lblHeading.Text = "Edit Game";
                txtTitle.Text = existing.Title;
                coverArtPath = existing.CoverArtPath;
                entries.AddRange(existing.PlatformEntries);
            }

            picCover.Image = CoverArtService.PlaceholderImage;
            RefreshPlatformList();
        }

        private async void AddEditGameForm_Load(object? sender, EventArgs e)
        {
            await RefreshCoverPreviewAsync();
        }

        /// <summary>Shows the current cover, or the placeholder when none.</summary>
        private async Task RefreshCoverPreviewAsync()
        {
            var image = await coverArtService.GetImageAsync(coverArtPath);
            picCover.Image = image ?? CoverArtService.PlaceholderImage;
        }

        private void BtnChangeCover_Click(object? sender, EventArgs e)
        {
            changeCoverMenu.Show(btnChangeCover, new Point(0, btnChangeCover.Height));
        }

        private async void CoverFromFile_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Choose Cover Image",
                Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.webp|All files|*.*"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            await SetCoverAsync(() => coverArtService.SaveFromFileAsync(dialog.FileName));
        }

        private async void CoverFromUrl_Click(object? sender, EventArgs e)
        {
            string? url = PromptForUrl();
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            await SetCoverAsync(() => coverArtService.DownloadAsync(url.Trim()));
        }

        /// <summary>
        /// Runs one cover-store operation with the buttons disabled, then
        /// updates the preview. Failures keep the current cover.
        /// </summary>
        private async Task SetCoverAsync(Func<Task<string>> storeCover)
        {
            btnChangeCover.Enabled = false;
            btnRemoveCover.Enabled = false;
            btnSave.Enabled = false;
            try
            {
                coverArtPath = await storeCover();
                await RefreshCoverPreviewAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to set the cover image:\n\n{ex.Message}",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnChangeCover.Enabled = true;
                btnRemoveCover.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void BtnRemoveCover_Click(object? sender, EventArgs e)
        {
            // The file stays on disk: the content-hashed store may be
            // shared by another game, and the path is simply dropped.
            coverArtPath = null;
            picCover.Image = CoverArtService.PlaceholderImage;
        }

        /// <summary>Small code-built modal asking for an image URL.</summary>
        private string? PromptForUrl()
        {
            using var prompt = new Form
            {
                Text = "Cover Image URL",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(420, 110)
            };
            var label = new Label { Text = "Image URL:", Location = new Point(12, 12), AutoSize = true };
            var textBox = new TextBox { Location = new Point(12, 36), Width = 396 };
            var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(232, 72), Size = new Size(85, 28) };
            var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(323, 72), Size = new Size(85, 28) };
            prompt.Controls.AddRange(new Control[] { label, textBox, ok, cancel });
            prompt.AcceptButton = ok;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog(this) == DialogResult.OK ? textBox.Text : null;
        }

        /// <summary>Rebuilds the ListView from the in-memory entry list.</summary>
        private void RefreshPlatformList()
        {
            lvPlatforms.BeginUpdate();
            lvPlatforms.Items.Clear();
            foreach (var entry in entries)
            {
                var item = new ListViewItem(entry.Platform)
                {
                    Tag = entry
                };
                item.SubItems.Add(entry.Status.ToString());
                item.SubItems.Add(entry.Rating?.ToString("0.#") ?? "");
                lvPlatforms.Items.Add(item);
            }
            lvPlatforms.EndUpdate();
        }

        private PlatformEntry? SelectedEntry =>
            lvPlatforms.SelectedItems.Count > 0
                ? lvPlatforms.SelectedItems[0].Tag as PlatformEntry
                : null;

        private void BtnAddPlatform_Click(object? sender, EventArgs e)
        {
            using var dialog = new AddPlatformForm();
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.Entry is not null)
            {
                entries.Add(dialog.Entry);
                RefreshPlatformList();
            }
        }

        private void BtnEditPlatform_Click(object? sender, EventArgs e)
        {
            var selected = SelectedEntry;
            if (selected is null)
            {
                MessageBox.Show(
                    "Please select a platform to edit.",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using var dialog = new AddPlatformForm(selected);
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.Entry is not null)
            {
                entries[entries.IndexOf(selected)] = dialog.Entry;
                RefreshPlatformList();
            }
        }

        private void BtnRemovePlatform_Click(object? sender, EventArgs e)
        {
            var selected = SelectedEntry;
            if (selected is null)
            {
                MessageBox.Show(
                    "Please select a platform to remove.",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Remove \"{selected.Platform}\" from this game?",
                "Save Point",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            // Entries that already exist in the database are deleted on save.
            if (selected.Id != 0)
            {
                removedEntryIds.Add(selected.Id);
            }

            entries.Remove(selected);
            RefreshPlatformList();
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            if (title.Length == 0)
            {
                MessageBox.Show(
                    "Please enter a title.",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            btnSave.Enabled = false;
            try
            {
                if (existing is null)
                {
                    await SaveNewGameAsync(title);
                }
                else
                {
                    await SaveExistingGameAsync(title);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to save the game:\n\n{ex.Message}",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private async Task SaveNewGameAsync(string title)
        {
            // The game must save first so the entries have a real GameId.
            var game = await gameService.AddGameAsync(new Game
            {
                Title = title,
                CoverArtPath = coverArtPath
            });

            foreach (var entry in entries)
            {
                entry.GameId = game.Id;
                await platformEntryService.AddPlatformEntryAsync(entry);
            }
        }

        private async Task SaveExistingGameAsync(string title)
        {
            var game = existing!;
            game.Title = title;
            game.CoverArtPath = coverArtPath;
            game.PlatformEntries = new List<PlatformEntry>();
            await gameService.UpdateGameAsync(game);

            foreach (int id in removedEntryIds)
            {
                await platformEntryService.DeletePlatformEntryAsync(id);
            }

            foreach (var entry in entries)
            {
                entry.GameId = game.Id;
                if (entry.Id == 0)
                {
                    await platformEntryService.AddPlatformEntryAsync(entry);
                }
                else
                {
                    await platformEntryService.UpdatePlatformEntryAsync(entry);
                }
            }
        }
    }
}
