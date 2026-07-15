using System.Security.Cryptography;

namespace save_point.Services
{
    /// <summary>
    /// Stores cover art images under Assets/Covers and hands out local paths
    /// and cached <see cref="Image"/> objects. Files are named by the SHA-256
    /// of their bytes, so the same image is never stored twice and cached
    /// images never go stale. The database stores only the relative path
    /// this service returns.
    /// </summary>
    public class CoverArtService
    {
        private const string CoversFolder = @"Assets\Covers";

        private static readonly HttpClient http = new()
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        private static readonly object cacheLock = new();
        private static readonly Dictionary<string, Image> imageCache = new(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, string> downloadedUrls = new(StringComparer.OrdinalIgnoreCase);

        private static readonly Lazy<Image> placeholder = new(CreatePlaceholder);

        /// <summary>Shown wherever a game has no usable cover image.</summary>
        public static Image PlaceholderImage => placeholder.Value;

        /// <summary>
        /// Maps a stored cover path to an absolute path on disk.
        /// Accepts both the relative paths this service creates and any
        /// old absolute paths, so existing records keep working.
        /// </summary>
        public static string? ResolveFullPath(string? coverArtPath)
        {
            if (string.IsNullOrWhiteSpace(coverArtPath))
            {
                return null;
            }

            return Path.IsPathRooted(coverArtPath)
                ? coverArtPath
                : Path.Combine(AppContext.BaseDirectory, coverArtPath);
        }

        /// <summary>
        /// Copies a local image file into Assets/Covers and returns the
        /// relative path to store in the database. Throws when the file
        /// is not a readable image.
        /// </summary>
        public async Task<string> SaveFromFileAsync(string sourcePath)
        {
            byte[] bytes = await Task.Run(() => File.ReadAllBytes(sourcePath));
            return await StoreAsync(bytes, Path.GetExtension(sourcePath));
        }

        /// <summary>
        /// Downloads an image and stores it like <see cref="SaveFromFileAsync"/>.
        /// The same URL is only downloaded once per session; identical bytes
        /// are never stored twice regardless of URL.
        /// </summary>
        public async Task<string> DownloadAsync(string url)
        {
            lock (cacheLock)
            {
                if (downloadedUrls.TryGetValue(url, out var known) &&
                    File.Exists(ResolveFullPath(known)))
                {
                    return known;
                }
            }

            byte[] bytes = await http.GetByteArrayAsync(url);
            string extension = Path.GetExtension(new Uri(url).AbsolutePath);
            string path = await StoreAsync(bytes, extension);

            lock (cacheLock)
            {
                downloadedUrls[url] = path;
            }
            return path;
        }

        /// <summary>
        /// Loads the image for a stored cover path, using the in-memory
        /// cache. Returns null when the path is empty, the file is missing,
        /// or the file is not a readable image.
        /// </summary>
        public async Task<Image?> GetImageAsync(string? coverArtPath)
        {
            string? fullPath = ResolveFullPath(coverArtPath);
            if (fullPath is null)
            {
                return null;
            }

            lock (cacheLock)
            {
                if (imageCache.TryGetValue(fullPath, out var cached))
                {
                    return cached;
                }
            }

            var image = await Task.Run(() => TryLoadImage(fullPath));
            if (image is not null)
            {
                lock (cacheLock)
                {
                    imageCache[fullPath] = image;
                }
            }
            return image;
        }

        /// <summary>
        /// Cache-only lookup, safe to call from rendering code on the UI
        /// thread. Returns null when the image has not been loaded yet.
        /// </summary>
        public Image? GetCachedImage(string? coverArtPath)
        {
            string? fullPath = ResolveFullPath(coverArtPath);
            if (fullPath is null)
            {
                return null;
            }

            lock (cacheLock)
            {
                return imageCache.TryGetValue(fullPath, out var cached) ? cached : null;
            }
        }

        /// <summary>
        /// Validates the bytes as an image, writes them (if new) under a
        /// content-hash file name and returns the relative path.
        /// </summary>
        private static async Task<string> StoreAsync(byte[] bytes, string? extension)
        {
            await Task.Run(() =>
            {
                // Image.FromStream throws on non-image data; the decoded
                // copy is discarded, this is validation only.
                using var probe = Image.FromStream(new MemoryStream(bytes));
            });

            if (string.IsNullOrWhiteSpace(extension))
            {
                extension = ".img";
            }

            string hash = Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
            string relativePath = Path.Combine(CoversFolder, hash + extension.ToLowerInvariant());
            string fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);

            if (!File.Exists(fullPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                await Task.Run(() => File.WriteAllBytes(fullPath, bytes));
            }

            return relativePath;
        }

        private static Image? TryLoadImage(string fullPath)
        {
            try
            {
                if (!File.Exists(fullPath))
                {
                    return null;
                }

                // Read into memory so the file is not kept locked.
                return Image.FromStream(new MemoryStream(File.ReadAllBytes(fullPath)));
            }
            catch
            {
                return null;
            }
        }

        private static Image CreatePlaceholder()
        {
            // 2:3 cover ratio; drawn at runtime so no asset file is needed.
            var bitmap = new Bitmap(200, 300);
            using var g = Graphics.FromImage(bitmap);
            g.Clear(Color.FromArgb(230, 230, 230));
            using var pen = new Pen(Color.FromArgb(200, 200, 200), 2);
            g.DrawRectangle(pen, 1, 1, bitmap.Width - 3, bitmap.Height - 3);
            using var font = new Font("Segoe UI", 14F);
            using var brush = new SolidBrush(Color.FromArgb(150, 150, 150));
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString("No Cover", font, brush,
                new RectangleF(0, 0, bitmap.Width, bitmap.Height), format);
            return bitmap;
        }
    }
}
