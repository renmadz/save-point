using System.Configuration;

namespace save_point
{
    /// <summary>
    /// Per-user persisted window placement, stored via the standard
    /// WinForms application-settings mechanism (user.config).
    /// Width/Height of 0 means nothing has been saved yet.
    /// </summary>
    public sealed class WindowSettings : ApplicationSettingsBase
    {
        public static WindowSettings Default { get; } =
            (WindowSettings)Synchronized(new WindowSettings());

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int WindowX
        {
            get => (int)this[nameof(WindowX)];
            set => this[nameof(WindowX)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int WindowY
        {
            get => (int)this[nameof(WindowY)];
            set => this[nameof(WindowY)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int WindowWidth
        {
            get => (int)this[nameof(WindowWidth)];
            set => this[nameof(WindowWidth)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int WindowHeight
        {
            get => (int)this[nameof(WindowHeight)];
            set => this[nameof(WindowHeight)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool WindowMaximized
        {
            get => (bool)this[nameof(WindowMaximized)];
            set => this[nameof(WindowMaximized)] = value;
        }
    }
}
