using System.Drawing;

namespace ImageAutomate.Core
{
    /// <summary>
    /// Centralized storage for user-configurable options.
    /// All settings are persisted to user preferences and loaded on application startup.
    /// </summary>
    public static class UserConfiguration
    {
        #region Execution Settings

        /// <summary>
        /// Gets or sets the execution mode or custom scheduler name.
        /// Built-in modes: "SimpleDfs", "Adaptive" (not implemented), "AdaptiveBatched" (not implemented).
        /// Custom: Use registered scheduler name from plugins.
        /// Default: "SimpleDfs".
        /// </summary>
        public static string Mode { get; set; } = "SimpleDfs";

        /// <summary>
        /// Gets or sets the maximum degree of parallelism (concurrent block executions).
        /// Default: Number of logical processors.
        /// </summary>
        public static int MaxDegreeOfParallelism { get; set; } = Environment.ProcessorCount;

        /// <summary>
        /// Gets or sets the watchdog timeout for deadlock detection.
        /// If no progress occurs within this duration, a PipelineDeadlockException is thrown.
        /// Default: 30 seconds.
        /// </summary>
        public static int WatchdogTimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// Gets or sets whether GC throttling is enabled.
        /// When enabled, the engine pauses dispatches if GC frequency exceeds 10/second.
        /// Default: true.
        /// </summary>
        public static bool EnableGcThrottling { get; set; } = true;

        /// <summary>
        /// Gets or sets the maximum number of work items to process per shipment.
        /// Used for batching work from shipment sources (e.g., LoadBlock).
        /// Higher values increase memory pressure but reduce overhead.
        /// Default: 64 work items.
        /// </summary>
        public static int MaxShipmentSize { get; set; } = 64;

        /// <summary>
        /// Gets or sets the profiling window size for cost estimation (Adaptive Mode only).
        /// Default: 20 samples.
        /// </summary>
        public static int ProfilingWindowSize { get; set; } = 20;

        /// <summary>
        /// Gets or sets the exponential moving average alpha for cost profiling (Adaptive Mode only).
        /// Default: 0.2 (emphasizes recent behavior).
        /// </summary>
        public static double CostEmaAlpha { get; set; } = 0.2;

        /// <summary>
        /// Gets or sets the critical path recomputation interval in blocks (Adaptive Mode only).
        /// Default: Every 10 blocks.
        /// </summary>
        public static int CriticalPathRecomputeInterval { get; set; } = 10;

        /// <summary>
        /// Gets or sets the batch size for grouped scheduling (AdaptiveBatched mode only).
        /// Default: 5 blocks.
        /// </summary>
        public static int BatchSize { get; set; } = 5;

        /// <summary>
        /// Gets or sets the critical path boost multiplier (Adaptive Mode only).
        /// Blocks on the critical path receive priority × this multiplier.
        /// Default: 1.5.
        /// </summary>
        public static double CriticalPathBoost { get; set; } = 1.5;

        #endregion

        #region Editor Settings

        /// <summary>
        /// Gets or sets the outline color for selected blocks.
        /// Default: Red.
        /// </summary>
        public static Color SelectedBlockOutlineColor { get; set; } = Color.Red;

        /// <summary>
        /// Gets or sets the radius of connection sockets.
        /// Default: 6.
        /// </summary>
        public static int SocketRadius { get; set; } = 6;

        /// <summary>
        /// Gets or sets the render scale factor for the graph editor.
        /// Default: 1.0.
        /// </summary>
        public static float RenderScale { get; set; } = 1.0f;

        /// <summary>
        /// Gets or sets whether the graph can be panned completely off-screen.
        /// Default: false.
        /// </summary>
        public static bool AllowOutOfScreenPan { get; set; } = false;

        /// <summary>
        /// Gets or sets the auto-snap zone width for block connections.
        /// Default: 20.
        /// </summary>
        public static int AutoSnapZoneWidth { get; set; } = 20;

        #endregion

        #region Theme Settings

        /// <summary>
        /// Gets or sets the default node color.
        /// Default: RGB(60, 60, 60).
        /// </summary>
        public static Color DefaultNodeColor { get; set; } = Color.FromArgb(60, 60, 60);

        /// <summary>
        /// Gets or sets the hovered node color.
        /// Default: RGB(80, 80, 80).
        /// </summary>
        public static Color HoveredNodeColor { get; set; } = Color.FromArgb(80, 80, 80);

        /// <summary>
        /// Gets or sets the text color for nodes.
        /// Default: White.
        /// </summary>
        public static Color TextColor { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the color for disabled nodes.
        /// Default: RGB(100, 100, 100).
        /// </summary>
        public static Color DisabledNodeColor { get; set; } = Color.FromArgb(100, 100, 100);

        /// <summary>
        /// Gets or sets the success color for UI elements.
        /// Default: RGB(100, 200, 100).
        /// </summary>
        public static Color SuccessColor { get; set; } = Color.FromArgb(100, 200, 100);

        /// <summary>
        /// Gets or sets the error color for UI elements.
        /// Default: RGB(200, 100, 100).
        /// </summary>
        public static Color ErrorColor { get; set; } = Color.FromArgb(200, 100, 100);

        /// <summary>
        /// Gets or sets the selected block outline color (duplicate property for Theme category).
        /// Default: Red.
        /// </summary>
        public static ThemeColorInfo SelectedBlockOutline { get; set; } = new ThemeColorInfo(Color.Red, "Selected Block Outline");

        /// <summary>
        /// Gets or sets the hovered block outline color.
        /// Default: Orange.
        /// </summary>
        public static ThemeColorInfo HoveredBlockOutline { get; set; } = new ThemeColorInfo(Color.Orange, "Hovered Block Outline");

        /// <summary>
        /// Gets or sets the border outline color.
        /// Default: RGB(150, 150, 150).
        /// </summary>
        public static ThemeColorInfo BorderOutline { get; set; } = new ThemeColorInfo(Color.FromArgb(150, 150, 150), "Border Outline");

        /// <summary>
        /// Gets or sets the socket connection color.
        /// Default: RGB(150, 150, 150).
        /// </summary>
        public static ThemeColorInfo SocketConnectionColor { get; set; } = new ThemeColorInfo(Color.FromArgb(100, 100, 100), "Socket Connection");

        /// <summary>
        /// Gets or sets the node width size.
        /// Default: 35.
        /// </summary>
        public static int NodeWidth { get; set; } = 35;

        /// <summary>
        /// Gets or sets the node border width.
        /// Default: 8.
        /// </summary>
        public static int NodeBorderWidth { get; set; } = 8;

        /// <summary>
        /// Gets or sets the spacing between nodes.
        /// Default: 25.
        /// </summary>
        public static int NodeSpacing { get; set; } = 25;

        #endregion

        /// <summary>
        /// Resets all configuration values to their defaults.
        /// </summary>
        public static void ResetToDefaults()
        {
            // Execution Settings
            Mode = "SimpleDfs";
            MaxDegreeOfParallelism = Environment.ProcessorCount;
            WatchdogTimeoutSeconds = 30;
            EnableGcThrottling = true;
            MaxShipmentSize = 64;
            ProfilingWindowSize = 20;
            CostEmaAlpha = 0.2;
            CriticalPathRecomputeInterval = 10;
            BatchSize = 5;
            CriticalPathBoost = 1.5;

            // Editor Settings
            SelectedBlockOutlineColor = Color.Red;
            SocketRadius = 6;
            RenderScale = 1.0f;
            AllowOutOfScreenPan = false;
            AutoSnapZoneWidth = 20;

            // Theme Settings
            DefaultNodeColor = Color.FromArgb(60, 60, 60);
            HoveredNodeColor = Color.FromArgb(80, 80, 80);
            TextColor = Color.White;
            DisabledNodeColor = Color.FromArgb(100, 100, 100);
            SuccessColor = Color.FromArgb(100, 200, 100);
            ErrorColor = Color.FromArgb(200, 100, 100);
            SelectedBlockOutline = new ThemeColorInfo(Color.Red, "Selected Block Outline");
            HoveredBlockOutline = new ThemeColorInfo(Color.Orange, "Hovered Block Outline");
            BorderOutline = new ThemeColorInfo(Color.FromArgb(150, 150, 150), "Border Outline");
            SocketConnectionColor = new ThemeColorInfo(Color.FromArgb(100, 100, 100), "Socket Connection");
            NodeWidth = 35;
            NodeBorderWidth = 8;
            NodeSpacing = 25;
        }

        /// <summary>
        /// Saves all configuration values to persistent storage.
        /// TODO: Implement actual persistence (e.g., JSON file, user settings).
        /// </summary>
        public static void Save()
        {
            // TODO: Implement persistence
        }

        /// <summary>
        /// Loads all configuration values from persistent storage.
        /// TODO: Implement actual loading (e.g., JSON file, user settings).
        /// </summary>
        public static void Load()
        {
            // TODO: Implement persistence
        }
    }

    /// <summary>
    /// Represents a color with metadata for theme configuration.
    /// </summary>
    public class ThemeColorInfo
    {
        /// <summary>
        /// Gets or sets the color value.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the display name/description of the color.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Initializes a new instance of ThemeColorInfo.
        /// </summary>
        public ThemeColorInfo(Color color, string displayName)
        {
            Color = color;
            DisplayName = displayName;
        }
    }
}
