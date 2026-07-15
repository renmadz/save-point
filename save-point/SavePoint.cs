using save_point.Models;
using save_point.Services;

namespace save_point
{
    /// <summary>
    /// Main window. Shows one grid row per game, aggregating its platform
    /// entries, and opens <see cref="AddEditGameForm"/> for add/edit.
    /// Delete key removes the selected game after confirmation.
    /// Games are loaded once into memory; search, filter and sort work on
    /// the in-memory list without touching the database.
    /// </summary>
    public partial class SavePoint : Form
    {
        private const string FilterAll = "All";

        private static readonly string[] SortOptions =
        {
            "Title (A-Z)",
            "Title (Z-A)",
            "Rating (High-Low)",
            "Rating (Low-High)",
            "Hours Played (High-Low)",
            "Hours Played (Low-High)",
            "Recently Completed",
            "Oldest Completed"
        };

        private readonly GameService gameService = new();
        private readonly CoverArtService coverArtService = new();
        private List<Game> allGames = new();

        private readonly ContextMenuStrip gridMenu = new();
        private readonly ToolStripMenuItem miEdit = new("Edit");
        private readonly ToolStripMenuItem miDelete = new("Delete");
        private readonly ToolStripMenuItem miCopyTitle = new("Copy Title");

        public SavePoint()
        {
            InitializeComponent();

            RestoreWindowPlacement();
            FormClosing += SavePoint_FormClosing;

            tslStatus.Text = "";

            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Items.Add(FilterAll);
            foreach (var status in Enum.GetValues<GameStatus>())
            {
                cmbFilter.Items.Add(status);
            }
            cmbFilter.SelectedIndex = 0;

            cmbSort.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSort.Items.AddRange(SortOptions);
            cmbSort.SelectedIndex = 0;

            gridMenu.Items.AddRange(new ToolStripItem[]
            {
                miEdit, miDelete, new ToolStripSeparator(), miCopyTitle
            });
            gridMenu.Opening += GridMenu_Opening;
            miEdit.Click += MiEdit_Click;
            miDelete.Click += MiDelete_Click;
            miCopyTitle.Click += MiCopyTitle_Click;
            dgvGames.ContextMenuStrip = gridMenu;

            Load += SavePoint_Load;
            btnAdd.Click += BtnAdd_Click;
            dgvGames.CellDoubleClick += DgvGames_CellDoubleClick;
            dgvGames.KeyDown += DgvGames_KeyDown;
            dgvGames.CellMouseDown += DgvGames_CellMouseDown;
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.TextChanged += ViewOption_Changed;
            cmbFilter.SelectedIndexChanged += ViewOption_Changed;
            cmbSort.SelectedIndexChanged += ViewOption_Changed;
        }

        private async void SavePoint_Load(object? sender, EventArgs e)
        {
            await LoadGamesAsync();
        }

        /// <summary>
        /// Applies the saved window placement. When nothing was saved, or
        /// the saved rectangle no longer touches any screen (for example
        /// after disconnecting a monitor), the designer's default of
        /// centering on the primary display is left in effect.
        /// </summary>
        private void RestoreWindowPlacement()
        {
            var s = WindowSettings.Default;
            var saved = new Rectangle(s.WindowX, s.WindowY, s.WindowWidth, s.WindowHeight);

            bool valid = saved.Width > 0 && saved.Height > 0 &&
                Screen.AllScreens.Any(screen => screen.WorkingArea.IntersectsWith(saved));
            if (!valid)
            {
                return;
            }

            StartPosition = FormStartPosition.Manual;
            Bounds = saved;
            if (s.WindowMaximized)
            {
                // Bounds set first so un-maximizing returns to the saved
                // rectangle via RestoreBounds.
                WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// Saves the window placement on normal close. Maximized and
        /// minimized windows save their restore rectangle instead, so the
        /// app never restores into a minimized state.
        /// </summary>
        private void SavePoint_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                var s = WindowSettings.Default;
                var bounds = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;

                s.WindowX = bounds.X;
                s.WindowY = bounds.Y;
                s.WindowWidth = bounds.Width;
                s.WindowHeight = bounds.Height;
                s.WindowMaximized = WindowState == FormWindowState.Maximized;
                s.Save();
            }
            catch
            {
                // Failing to persist placement must never block closing.
            }
        }

        /// <summary>
        /// Keyboard shortcuts. Delete is intentionally not handled here:
        /// it stays on the grid's KeyDown so it never steals the key from
        /// text editing in the search box.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.N:
                    btnAdd.PerformClick();
                    return true;

                case Keys.Control | Keys.F:
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                    return true;

                case Keys.Control | Keys.Shift | Keys.F:
                    cmbFilter.Focus();
                    return true;

                case Keys.Control | Keys.Shift | Keys.S:
                    cmbSort.Focus();
                    return true;

                case Keys.F2:
                    if (dgvGames.CurrentRow?.Tag is Game game)
                    {
                        _ = EditGameAsync(game);
                    }
                    return true;

                case Keys.Escape when txtSearch.Focused:
                    if (txtSearch.TextLength > 0)
                    {
                        // TextChanged re-renders the grid; focus stays here.
                        txtSearch.Clear();
                    }
                    else
                    {
                        dgvGames.Focus();
                    }
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>Opens the edit dialog and reloads the grid on save.</summary>
        private async Task EditGameAsync(Game game)
        {
            using var dialog = new AddEditGameForm(game);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadGamesAsync();
            }
        }

        /// <summary>Deletes a game after confirmation, then reloads.</summary>
        private async Task DeleteGameAsync(Game game)
        {
            var confirm = MessageBox.Show(
                $"Delete \"{game.Title}\" and all of its platform entries?",
                "Save Point",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                await gameService.DeleteGameAsync(game.Id);
                await LoadGamesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to delete the game:\n\n{ex.Message}",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private Game? SelectedGame => dgvGames.CurrentRow?.Tag as Game;

        private void GridMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            bool hasSelection = SelectedGame is not null;
            miEdit.Enabled = hasSelection;
            miDelete.Enabled = hasSelection;
            miCopyTitle.Enabled = hasSelection;
        }

        private async void MiEdit_Click(object? sender, EventArgs e)
        {
            if (SelectedGame is Game game)
            {
                await EditGameAsync(game);
            }
        }

        private async void MiDelete_Click(object? sender, EventArgs e)
        {
            if (SelectedGame is Game game)
            {
                await DeleteGameAsync(game);
            }
        }

        private void MiCopyTitle_Click(object? sender, EventArgs e)
        {
            if (SelectedGame is Game game)
            {
                Clipboard.SetText(game.Title);
            }
        }

        /// <summary>Right-click selects the row so the menu acts on it.</summary>
        private void DgvGames_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvGames.CurrentCell = dgvGames.Rows[e.RowIndex].Cells[e.ColumnIndex >= 0 ? e.ColumnIndex : 0];
            }
        }

        private void TxtSearch_Enter(object? sender, EventArgs e)
        {
            // Deferred so a focusing mouse click cannot undo the selection.
            BeginInvoke(txtSearch.SelectAll);
        }

        private void ViewOption_Changed(object? sender, EventArgs e)
        {
            ApplyView();
        }

        /// <summary>Reloads all games from the database, then re-renders.</summary>
        private async Task LoadGamesAsync()
        {
            tslStatus.Text = "Loading…";
            try
            {
                allGames = await gameService.GetAllGamesAsync();

                // Decode all covers off the UI thread into the service's
                // cache so ApplyView can render synchronously without IO.
                await Task.WhenAll(
                    allGames.Select(g => coverArtService.GetImageAsync(g.CoverArtPath)));

                ApplyView();
            }
            catch (Exception ex)
            {
                tslStatus.Text = "Failed to load games";
                MessageBox.Show(
                    $"Failed to load games:\n\n{ex.Message}",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Renders the grid from the in-memory list using the current
        /// search text, status filter and sort order. No database access.
        /// </summary>
        private void ApplyView()
        {
            IEnumerable<Game> view = allGames;

            string search = txtSearch.Text.Trim();
            if (search.Length > 0)
            {
                view = view.Where(g =>
                    g.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (cmbFilter.SelectedItem is GameStatus filterStatus)
            {
                view = view.Where(g =>
                    g.PlatformEntries.Any(pe => pe.Status == filterStatus));
            }

            view = ApplySort(view, cmbSort.SelectedIndex);

            var games = view.ToList();

            dgvGames.SuspendLayout();
            dgvGames.Rows.Clear();
            int number = 1;
            foreach (var game in games)
            {
                var entries = game.PlatformEntries;
                var first = entries.FirstOrDefault();
                double? bestRating = BestRating(game);
                int? totalHours = TotalHours(game);

                int rowIndex = dgvGames.Rows.Add(
                    number++,
                    coverArtService.GetCachedImage(game.CoverArtPath)
                        ?? CoverArtService.PlaceholderImage,
                    game.Title,
                    bestRating?.ToString("0.#") ?? "",
                    string.Join(", ", entries.Select(pe => pe.Platform)),
                    first?.Status.ToString() ?? "",
                    totalHours?.ToString() ?? "",
                    first?.Remarks ?? "");
                dgvGames.Rows[rowIndex].Tag = game;
            }
            dgvGames.ResumeLayout();

            tslStatus.Text = games.Count == allGames.Count
                ? $"{games.Count} game{(games.Count == 1 ? "" : "s")}"
                : $"{games.Count} of {allGames.Count} games";
        }

        private static IEnumerable<Game> ApplySort(IEnumerable<Game> view, int sortIndex)
        {
            // Games without a value for the chosen key always sort last.
            return sortIndex switch
            {
                0 => view.OrderBy(g => g.Title, StringComparer.OrdinalIgnoreCase),
                1 => view.OrderByDescending(g => g.Title, StringComparer.OrdinalIgnoreCase),
                2 => view.OrderBy(g => BestRating(g) is null)
                         .ThenByDescending(BestRating),
                3 => view.OrderBy(g => BestRating(g) is null)
                         .ThenBy(BestRating),
                4 => view.OrderBy(g => TotalHours(g) is null)
                         .ThenByDescending(TotalHours),
                5 => view.OrderBy(g => TotalHours(g) is null)
                         .ThenBy(TotalHours),
                6 => view.OrderBy(g => LatestCompletion(g) is null)
                         .ThenByDescending(LatestCompletion),
                7 => view.OrderBy(g => LatestCompletion(g) is null)
                         .ThenBy(LatestCompletion),
                _ => view
            };
        }

        /// <summary>Highest rating across the game's entries, or null.</summary>
        private static double? BestRating(Game game) =>
            game.PlatformEntries
                .Where(pe => pe.Rating is not null)
                .Max(pe => pe.Rating);

        /// <summary>Total tracked hours across entries, or null when none tracked.</summary>
        private static int? TotalHours(Game game) =>
            game.PlatformEntries.Any(pe => pe.HoursPlayed is not null)
                ? game.PlatformEntries.Sum(pe => pe.HoursPlayed ?? 0)
                : null;

        /// <summary>Most recent completion date across entries, or null.</summary>
        private static DateTime? LatestCompletion(Game game) =>
            game.PlatformEntries
                .Where(pe => pe.CompletionDate is not null)
                .Max(pe => pe.CompletionDate);

        private async void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var dialog = new AddEditGameForm();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadGamesAsync();
            }
        }

        private async void DgvGames_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvGames.Rows[e.RowIndex].Tag is not Game game)
            {
                return;
            }

            await EditGameAsync(game);
        }

        private async void DgvGames_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || SelectedGame is not Game game)
            {
                return;
            }

            e.Handled = true;
            await DeleteGameAsync(game);
        }
    }
}
