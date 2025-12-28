using System.Diagnostics;

using ImageAutomate.Core;
using ImageAutomate.Execution;
using ImageAutomate.Execution.Scheduling;

namespace ImageAutomate.Views.DashboardViews
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            LoadConfigurationToUI();
            WireEventHandlers();

            Debug.WriteLine("Settings view initialized.");
        }

        /// <summary>
        /// Loads configuration values from UserConfiguration into UI controls.
        /// </summary>
        private void LoadConfigurationToUI()
        {
            // Execution Settings
            ModeComboBox.SelectedItem = UserConfiguration.Mode;
            MaxDegreeOfParallelismValue.Value = UserConfiguration.MaxDegreeOfParallelism;
            EnableGcThrottlingValue.Checked = UserConfiguration.EnableGcThrottling;
            WatchdogTimeoutValue.Value = UserConfiguration.WatchdogTimeoutSeconds;
            MaxShipmentSizeValue.Value = UserConfiguration.MaxShipmentSize;
            ProfilingWindowSizeValue.Value = UserConfiguration.ProfilingWindowSize;
            CostEmaAlphaValue.Value = (decimal)UserConfiguration.CostEmaAlpha;
            CriticalPathRecomputeIntervalValue.Value = UserConfiguration.CriticalPathRecomputeInterval;
            BatchSizeValue.Value = UserConfiguration.BatchSize;
            CriticalPathBoostValue.Value = (decimal)UserConfiguration.CriticalPathBoost;

            // Editor Settings
            SelectedBlockOutlineColorButton.SelectedColor = UserConfiguration.SelectedBlockOutlineColor;
            SocketRadiusValue.Value = UserConfiguration.SocketRadius;
            RenderScaleValue.Value = (decimal)UserConfiguration.RenderScale;
            AllowOutOfScreenPanValue.Checked = UserConfiguration.AllowOutOfScreenPan;
            AutoSnapZoneWidthValue.Value = UserConfiguration.AutoSnapZoneWidth;

            // Theme Settings
            HoveredBlockOutlineColorButton.SelectedColor = UserConfiguration.HoveredBlockOutline.Color;
            DefaultNodeColorColorButton.SelectedColor = UserConfiguration.DefaultNodeColor;
            HoveredNodeColorColorButton.SelectedColor = UserConfiguration.HoveredNodeColor;
            DisabledNodeColorColorButton.SelectedColor = UserConfiguration.DisabledNodeColor;
            SuccessColorColorButton.SelectedColor = UserConfiguration.SuccessColor;
            ErrorColorColorButton.SelectedColor = UserConfiguration.ErrorColor;
            SelectedBlockOutlineColorButton2.SelectedColor = UserConfiguration.SelectedBlockOutline.Color;
            TextColorColorButton.SelectedColor = UserConfiguration.TextColor;
            BorderOutlineColorButton.SelectedColor = UserConfiguration.BorderOutline.Color;
            SocketConnectionColorColorButton.SelectedColor = UserConfiguration.SocketConnectionColor.Color;
            NodeWidthValue.Value = UserConfiguration.NodeWidth;
            NodeBorderWidthValue.Value = UserConfiguration.NodeBorderWidth;
            NodeSpacingValue.Value = UserConfiguration.NodeSpacing;
        }

        /// <summary>
        /// Wires up event handlers for all UI controls.
        /// </summary>
        private void WireEventHandlers()
        {
            // Execution Settings
            ModeComboBox.SelectedIndexChanged += (s, e) =>
            {
                UserConfiguration.Mode = ModeComboBox.SelectedItem?.ToString() ?? "SimpleDfs";
                UserConfiguration.Save();
            };

            MaxDegreeOfParallelismValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.MaxDegreeOfParallelism = (int)MaxDegreeOfParallelismValue.Value;
                UserConfiguration.Save();
            };

            EnableGcThrottlingValue.CheckedChanged += (s, e) =>
            {
                UserConfiguration.EnableGcThrottling = EnableGcThrottlingValue.Checked;
                UserConfiguration.Save();
            };

            WatchdogTimeoutValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.WatchdogTimeoutSeconds = (int)WatchdogTimeoutValue.Value;
                UserConfiguration.Save();
            };

            MaxShipmentSizeValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.MaxShipmentSize = (int)MaxShipmentSizeValue.Value;
                UserConfiguration.Save();
            };

            ProfilingWindowSizeValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.ProfilingWindowSize = (int)ProfilingWindowSizeValue.Value;
                UserConfiguration.Save();
            };

            CostEmaAlphaValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.CostEmaAlpha = (double)CostEmaAlphaValue.Value;
                UserConfiguration.Save();
            };

            CriticalPathRecomputeIntervalValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.CriticalPathRecomputeInterval = (int)CriticalPathRecomputeIntervalValue.Value;
                UserConfiguration.Save();
            };

            BatchSizeValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.BatchSize = (int)BatchSizeValue.Value;
                UserConfiguration.Save();
            };

            CriticalPathBoostValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.CriticalPathBoost = (double)CriticalPathBoostValue.Value;
                UserConfiguration.Save();
            };

            // Editor Settings
            SelectedBlockOutlineColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.SelectedBlockOutlineColor = SelectedBlockOutlineColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            SocketRadiusValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.SocketRadius = (int)SocketRadiusValue.Value;
                UserConfiguration.Save();
            };

            RenderScaleValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.RenderScale = (float)RenderScaleValue.Value;
                UserConfiguration.Save();
            };

            AllowOutOfScreenPanValue.CheckedChanged += (s, e) =>
            {
                UserConfiguration.AllowOutOfScreenPan = AllowOutOfScreenPanValue.Checked;
                UserConfiguration.Save();
            };

            AutoSnapZoneWidthValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.AutoSnapZoneWidth = (int)AutoSnapZoneWidthValue.Value;
                UserConfiguration.Save();
            };

            // Theme Settings
            HoveredBlockOutlineColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.HoveredBlockOutline.Color = HoveredBlockOutlineColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            DefaultNodeColorColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.DefaultNodeColor = DefaultNodeColorColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            HoveredNodeColorColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.HoveredNodeColor = HoveredNodeColorColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            DisabledNodeColorColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.DisabledNodeColor = DisabledNodeColorColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            SuccessColorColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.SuccessColor = SuccessColorColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            ErrorColorColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.ErrorColor = ErrorColorColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            SelectedBlockOutlineColorButton2.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.SelectedBlockOutline.Color = SelectedBlockOutlineColorButton2.SelectedColor;
                UserConfiguration.Save();
            };

            TextColorColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.TextColor = TextColorColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            BorderOutlineColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.BorderOutline.Color = BorderOutlineColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            SocketConnectionColorColorButton.SelectedColorChanged += (s, e) =>
            {
                UserConfiguration.SocketConnectionColor.Color = SocketConnectionColorColorButton.SelectedColor;
                UserConfiguration.Save();
            };

            NodeWidthValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.NodeWidth = (int)NodeWidthValue.Value;
                UserConfiguration.Save();
            };

            NodeBorderWidthValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.NodeBorderWidth = (int)NodeBorderWidthValue.Value;
                UserConfiguration.Save();
            };

            NodeSpacingValue.ValueChanged += (s, e) =>
            {
                UserConfiguration.NodeSpacing = (int)NodeSpacingValue.Value;
                UserConfiguration.Save();
            };
        }
    }
}
