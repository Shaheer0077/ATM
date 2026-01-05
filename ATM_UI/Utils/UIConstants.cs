namespace ATM_UI.Utils
{
    public static class UIConstants
    {
        // Color Scheme - Modern Blue Theme
        public static readonly Color PrimaryColor = Color.FromArgb(22, 109, 150);       // Modern Blue (#1F8CB9)
        public static readonly Color PrimaryDarkColor = Color.FromArgb(22, 109, 150);   // Darker Blue (#166D96)
        public static readonly Color AccentColor = Color.FromArgb(52, 211, 153);        // Green Accent (#34D399)
        public static readonly Color BackgroundColor = Color.FromArgb(240, 244, 249);   // Light Gray Background (#F0F4F9)
        public static readonly Color TextPrimaryColor = Color.FromArgb(31, 41, 55);     // Dark Text (#1F2937)
        public static readonly Color TextSecondaryColor = Color.FromArgb(107, 114, 128); // Gray Text (#6B7280)
        public static readonly Color SuccessColor = Color.FromArgb(52, 211, 153);       // Green (#34D399)
        public static readonly Color ErrorColor = Color.FromArgb(239, 68, 68);          // Red (#EF4444)
        public static readonly Color WarningColor = Color.FromArgb(251, 191, 36);       // Yellow (#FBF124)

        // Fonts
        public static readonly Font TitleFont = new Font("Segoe UI", 20, FontStyle.Bold);
        public static readonly Font HeadingFont = new Font("Segoe UI", 14, FontStyle.Bold);
        public static readonly Font LabelFont = new Font("Segoe UI", 11, FontStyle.Regular);
        public static readonly Font NormalFont = new Font("Segoe UI", 10, FontStyle.Regular);
        public static readonly Font SmallFont = new Font("Segoe UI", 9, FontStyle.Regular);

        // Spacing
        public const int LargeMargin = 30;
        public const int StandardMargin = 20;
        public const int SmallMargin = 10;
        public const int TinyMargin = 5;

        // Control Heights
        public const int ButtonHeight = 40;
        public const int InputHeight = 35;
        public const int LabelHeight = 25;

        // Control Widths
        public const int StandardButtonWidth = 120;
        public const int WideButtonWidth = 200;

        // Border Radius simulation values
        public const int BorderRadius = 8;

        // Window Sizes
        public const int LoginFormWidth = 450;
        public const int LoginFormHeight = 380;

        public const int MainMenuFormWidth = 500;
        public const int MainMenuFormHeight = 550;

        public const int OperationFormWidth = 450;
        public const int OperationFormHeight = 350;

        public const int BalanceFormWidth = 450;
        public const int BalanceFormHeight = 300;

        public const int HistoryFormWidth = 750;
        public const int HistoryFormHeight = 500;

        public const int ChangePINFormWidth = 500;
        public const int ChangePINFormHeight = 400;
    }
}
