using save_point.Models;
using save_point.Services;

namespace save_point
{
    /// <summary>
    /// Main window. Shows one grid row per game, aggregating its platform
    /// entries, and opens <see cref="AddEditGameForm"/> for add/edit.
    /// Delete key removes the selected game after confirmation.
    /// </summary>
    public partial class SavePoint : Form
    {
        private readonly GameService gameService = new();

        public SavePoint()
        {
            InitializeComponent();

            // Empty cover cells would otherwise render the broken-image icon.
            cover.DefaultCellStyle.NullValue = null;
            tslStatus.Text = "";

            Load += SavePoint_Load;
            btnAdd.Click += BtnAdd_Click;
            dgvGames.CellDoubleClick += DgvGames_CellDoubleClick;
            dgvGames.KeyDown += DgvGames_KeyDown;
        }

        private async void SavePoint_Load(object? sender, EventArgs e)
        {
            await LoadGamesAsync();
        }

        /// <summary>Reloads all games from the database into the grid.</summary>
        private async Task LoadGamesAsync()
        {
            tslStatus.Text = "Loading…";
            try
            {
                var games = await gameService.GetAllGamesAsync();

                dgvGames.Rows.Clear();
                int number = 1;
                foreach (var game in games)
                {
                    var entries = game.PlatformEntries;
                    var first = entries.FirstOrDefault();
                    double? bestRating = entries
                        .Where(pe => pe.Rating is not null)
                        .Max(pe => pe.Rating);
                    int totalHours = entries.Sum(pe => pe.HoursPlayed ?? 0);

                    int rowIndex = dgvGames.Rows.Add(
                        number++,
                        // Null is a valid cell value; the column's NullValue renders it blank.
                        (object?)LoadCover(game.CoverArtPath)!,
                        game.Title,
                        bestRating?.ToString("0.#") ?? "",
                        string.Join(", ", entries.Select(pe => pe.Platform)),
                        first?.Status.ToString() ?? "",
                        totalHours > 0 ? totalHours.ToString() : "",
                        first?.Remarks ?? "");
                    dgvGames.Rows[rowIndex].Tag = game;
                }

                tslStatus.Text = $"{games.Count} game{(games.Count == 1 ? "" : "s")}";
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

        private static Image? LoadCover(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            try
            {
                // Read into memory so the file is not kept locked.
                return Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
            }
            catch
            {
                return null;
            }
        }

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
