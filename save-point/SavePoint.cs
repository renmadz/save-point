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

        public SavePoint()
        {
            InitializeComponent();

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

            Load += SavePoint_Load;
            btnAdd.Click += BtnAdd_Click;
            dgvGames.CellDoubleClick += DgvGames_CellDoubleClick;
            dgvGames.KeyDown += DgvGames_KeyDown;
            txtSearch.TextChanged += ViewOption_Changed;
            cmbFilter.SelectedIndexChanged += ViewOption_Changed;
            cmbSort.SelectedIndexChanged += ViewOption_Changed;
        }

        private async void SavePoint_Load(object? sender, EventArgs e)
        {
            await LoadGamesAsync();
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

            using var dialog = new AddEditGameForm(game);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadGamesAsync();
            }
        }

        private async void DgvGames_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete ||
                dgvGames.CurrentRow is null ||
                dgvGames.CurrentRow.Tag is not Game game)
            {
                return;
            }

            e.Handled = true;

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
    }
}
