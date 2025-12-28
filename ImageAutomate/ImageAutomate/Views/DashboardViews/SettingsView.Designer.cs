namespace ImageAutomate.Views.DashboardViews
{
    partial class SettingsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainScrollPanel = new Panel();
            ExecutionGroupBox = new GroupBox();
            ExecutionTable = new TableLayoutPanel();
            WatchdogTimeoutLabel = new Label();
            WatchdogTimeoutValue = new NumericUpDown();
            MaxShipmentSizeLabel = new Label();
            MaxShipmentSizeValue = new NumericUpDown();
            ProfilingWindowSizeLabel = new Label();
            ProfilingWindowSizeValue = new NumericUpDown();
            CostEmaAlphaLabel = new Label();
            CostEmaAlphaValue = new NumericUpDown();
            CriticalPathRecomputeIntervalLabel = new Label();
            CriticalPathRecomputeIntervalValue = new NumericUpDown();
            BatchSizeLabel = new Label();
            BatchSizeValue = new NumericUpDown();
            CriticalPathBoostLabel = new Label();
            CriticalPathBoostValue = new NumericUpDown();
            Label2 = new Label();
            ModeComboBox = new ComboBox();
            MaxDegreeOfParallelismLabel = new Label();
            MaxDegreeOfParallelismValue = new NumericUpDown();
            EnableGcThrottlingLabel = new Label();
            EnableGcThrottlingValue = new CheckBox();
            EditorGroupBox = new GroupBox();
            EditorTable = new TableLayoutPanel();
            AutoSnapZoneWidthLabel = new Label();
            AutoSnapZoneWidthValue = new NumericUpDown();
            RenderScaleLabel = new Label();
            RenderScaleValue = new NumericUpDown();
            SocketRadiusLabel = new Label();
            SocketRadiusValue = new NumericUpDown();
            AllowOutOfScreenPanLabel = new Label();
            AllowOutOfScreenPanValue = new CheckBox();
            Label3 = new Label();
            SelectedBlockOutlineColorButton = new ImageAutomate.UI.ColorDialogButton();
            ThemeGroupBox = new GroupBox();
            ThemeTable = new TableLayoutPanel();
            NodeWidthLabel = new Label();
            NodeWidthValue = new NumericUpDown();
            NodeBorderWidthLabel = new Label();
            NodeBorderWidthValue = new NumericUpDown();
            NodeSpacingLabel = new Label();
            NodeSpacingValue = new NumericUpDown();
            TextColorLabel = new Label();
            TextColorColorButton = new ImageAutomate.UI.ColorDialogButton();
            DisabledNodeColorLabel = new Label();
            DisabledNodeColorColorButton = new ImageAutomate.UI.ColorDialogButton();
            SuccessColorLabel = new Label();
            SuccessColorColorButton = new ImageAutomate.UI.ColorDialogButton();
            ErrorColorLabel = new Label();
            ErrorColorColorButton = new ImageAutomate.UI.ColorDialogButton();
            HoveredNodeColorLabel = new Label();
            HoveredNodeColorColorButton = new ImageAutomate.UI.ColorDialogButton();
            DefaultNodeColorLabel = new Label();
            DefaultNodeColorColorButton = new ImageAutomate.UI.ColorDialogButton();
            SelectedBlockOutlineLabel = new Label();
            SelectedBlockOutlineColorButton2 = new ImageAutomate.UI.ColorDialogButton();
            HoveredBlockOutlineLabel = new Label();
            HoveredBlockOutlineColorButton = new ImageAutomate.UI.ColorDialogButton();
            BorderOutlineLabel = new Label();
            BorderOutlineColorButton = new ImageAutomate.UI.ColorDialogButton();
            SocketConnectionColorLabel = new Label();
            SocketConnectionColorColorButton = new ImageAutomate.UI.ColorDialogButton();
            MainScrollPanel.SuspendLayout();
            ExecutionGroupBox.SuspendLayout();
            ExecutionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WatchdogTimeoutValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxShipmentSizeValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ProfilingWindowSizeValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CostEmaAlphaValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CriticalPathRecomputeIntervalValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BatchSizeValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CriticalPathBoostValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxDegreeOfParallelismValue).BeginInit();
            EditorGroupBox.SuspendLayout();
            EditorTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AutoSnapZoneWidthValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RenderScaleValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SocketRadiusValue).BeginInit();
            ThemeGroupBox.SuspendLayout();
            ThemeTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NodeWidthValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NodeBorderWidthValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NodeSpacingValue).BeginInit();
            SuspendLayout();
            // 
            // MainScrollPanel
            // 
            MainScrollPanel.AutoScroll = true;
            MainScrollPanel.Controls.Add(ExecutionGroupBox);
            MainScrollPanel.Controls.Add(EditorGroupBox);
            MainScrollPanel.Controls.Add(ThemeGroupBox);
            MainScrollPanel.Dock = DockStyle.Fill;
            MainScrollPanel.Location = new Point(0, 0);
            MainScrollPanel.Name = "MainScrollPanel";
            MainScrollPanel.Size = new Size(740, 600);
            MainScrollPanel.TabIndex = 0;
            // 
            // ExecutionGroupBox
            // 
            ExecutionGroupBox.Controls.Add(ExecutionTable);
            ExecutionGroupBox.Dock = DockStyle.Top;
            ExecutionGroupBox.Location = new Point(0, 510);
            ExecutionGroupBox.Name = "ExecutionGroupBox";
            ExecutionGroupBox.Size = new Size(723, 320);
            ExecutionGroupBox.TabIndex = 0;
            ExecutionGroupBox.TabStop = false;
            ExecutionGroupBox.Text = "Execution";
            // 
            // ExecutionTable
            // 
            ExecutionTable.ColumnCount = 2;
            ExecutionTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            ExecutionTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ExecutionTable.Controls.Add(WatchdogTimeoutLabel, 0, 3);
            ExecutionTable.Controls.Add(WatchdogTimeoutValue, 1, 3);
            ExecutionTable.Controls.Add(MaxShipmentSizeLabel, 0, 4);
            ExecutionTable.Controls.Add(MaxShipmentSizeValue, 1, 4);
            ExecutionTable.Controls.Add(ProfilingWindowSizeLabel, 0, 5);
            ExecutionTable.Controls.Add(ProfilingWindowSizeValue, 1, 5);
            ExecutionTable.Controls.Add(CostEmaAlphaLabel, 0, 6);
            ExecutionTable.Controls.Add(CostEmaAlphaValue, 1, 6);
            ExecutionTable.Controls.Add(CriticalPathRecomputeIntervalLabel, 0, 7);
            ExecutionTable.Controls.Add(CriticalPathRecomputeIntervalValue, 1, 7);
            ExecutionTable.Controls.Add(BatchSizeLabel, 0, 8);
            ExecutionTable.Controls.Add(BatchSizeValue, 1, 8);
            ExecutionTable.Controls.Add(CriticalPathBoostLabel, 0, 9);
            ExecutionTable.Controls.Add(CriticalPathBoostValue, 1, 9);
            ExecutionTable.Controls.Add(Label2, 0, 0);
            ExecutionTable.Controls.Add(ModeComboBox, 1, 0);
            ExecutionTable.Controls.Add(MaxDegreeOfParallelismLabel, 0, 1);
            ExecutionTable.Controls.Add(MaxDegreeOfParallelismValue, 1, 1);
            ExecutionTable.Controls.Add(EnableGcThrottlingLabel, 0, 2);
            ExecutionTable.Controls.Add(EnableGcThrottlingValue, 1, 2);
            ExecutionTable.Dock = DockStyle.Fill;
            ExecutionTable.Location = new Point(3, 19);
            ExecutionTable.Name = "ExecutionTable";
            ExecutionTable.RowCount = 10;
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ExecutionTable.Size = new Size(717, 298);
            ExecutionTable.TabIndex = 0;
            // 
            // WatchdogTimeoutLabel
            // 
            WatchdogTimeoutLabel.AutoSize = true;
            WatchdogTimeoutLabel.Dock = DockStyle.Fill;
            WatchdogTimeoutLabel.Location = new Point(3, 90);
            WatchdogTimeoutLabel.Name = "WatchdogTimeoutLabel";
            WatchdogTimeoutLabel.Size = new Size(194, 30);
            WatchdogTimeoutLabel.TabIndex = 9;
            WatchdogTimeoutLabel.Text = "Watchdog Timeout (seconds):";
            WatchdogTimeoutLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WatchdogTimeoutValue
            // 
            WatchdogTimeoutValue.Dock = DockStyle.Fill;
            WatchdogTimeoutValue.Location = new Point(203, 93);
            WatchdogTimeoutValue.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            WatchdogTimeoutValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            WatchdogTimeoutValue.Name = "WatchdogTimeoutValue";
            WatchdogTimeoutValue.Size = new Size(511, 23);
            WatchdogTimeoutValue.TabIndex = 8;
            WatchdogTimeoutValue.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // MaxShipmentSizeLabel
            // 
            MaxShipmentSizeLabel.AutoSize = true;
            MaxShipmentSizeLabel.Dock = DockStyle.Fill;
            MaxShipmentSizeLabel.Location = new Point(3, 120);
            MaxShipmentSizeLabel.Name = "MaxShipmentSizeLabel";
            MaxShipmentSizeLabel.Size = new Size(194, 30);
            MaxShipmentSizeLabel.TabIndex = 7;
            MaxShipmentSizeLabel.Text = "Max Shipment Size:";
            MaxShipmentSizeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MaxShipmentSizeValue
            // 
            MaxShipmentSizeValue.Dock = DockStyle.Fill;
            MaxShipmentSizeValue.Location = new Point(203, 123);
            MaxShipmentSizeValue.Maximum = new decimal(new int[] { 512, 0, 0, 0 });
            MaxShipmentSizeValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MaxShipmentSizeValue.Name = "MaxShipmentSizeValue";
            MaxShipmentSizeValue.Size = new Size(511, 23);
            MaxShipmentSizeValue.TabIndex = 6;
            MaxShipmentSizeValue.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // ProfilingWindowSizeLabel
            // 
            ProfilingWindowSizeLabel.AutoSize = true;
            ProfilingWindowSizeLabel.Dock = DockStyle.Fill;
            ProfilingWindowSizeLabel.Location = new Point(3, 150);
            ProfilingWindowSizeLabel.Name = "ProfilingWindowSizeLabel";
            ProfilingWindowSizeLabel.Size = new Size(194, 30);
            ProfilingWindowSizeLabel.TabIndex = 5;
            ProfilingWindowSizeLabel.Text = "Profiling Window Size:";
            ProfilingWindowSizeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ProfilingWindowSizeValue
            // 
            ProfilingWindowSizeValue.Dock = DockStyle.Fill;
            ProfilingWindowSizeValue.Location = new Point(203, 153);
            ProfilingWindowSizeValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ProfilingWindowSizeValue.Name = "ProfilingWindowSizeValue";
            ProfilingWindowSizeValue.Size = new Size(511, 23);
            ProfilingWindowSizeValue.TabIndex = 4;
            ProfilingWindowSizeValue.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // CostEmaAlphaLabel
            // 
            CostEmaAlphaLabel.AutoSize = true;
            CostEmaAlphaLabel.Dock = DockStyle.Fill;
            CostEmaAlphaLabel.Location = new Point(3, 180);
            CostEmaAlphaLabel.Name = "CostEmaAlphaLabel";
            CostEmaAlphaLabel.Size = new Size(194, 30);
            CostEmaAlphaLabel.TabIndex = 3;
            CostEmaAlphaLabel.Text = "Cost EMA Alpha:";
            CostEmaAlphaLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CostEmaAlphaValue
            // 
            CostEmaAlphaValue.DecimalPlaces = 2;
            CostEmaAlphaValue.Dock = DockStyle.Fill;
            CostEmaAlphaValue.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            CostEmaAlphaValue.Location = new Point(203, 183);
            CostEmaAlphaValue.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            CostEmaAlphaValue.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            CostEmaAlphaValue.Name = "CostEmaAlphaValue";
            CostEmaAlphaValue.Size = new Size(511, 23);
            CostEmaAlphaValue.TabIndex = 2;
            CostEmaAlphaValue.Value = new decimal(new int[] { 2, 0, 0, 65536 });
            // 
            // CriticalPathRecomputeIntervalLabel
            // 
            CriticalPathRecomputeIntervalLabel.AutoSize = true;
            CriticalPathRecomputeIntervalLabel.Dock = DockStyle.Fill;
            CriticalPathRecomputeIntervalLabel.Location = new Point(3, 210);
            CriticalPathRecomputeIntervalLabel.Name = "CriticalPathRecomputeIntervalLabel";
            CriticalPathRecomputeIntervalLabel.Size = new Size(194, 30);
            CriticalPathRecomputeIntervalLabel.TabIndex = 1;
            CriticalPathRecomputeIntervalLabel.Text = "Critical Path Recompute Interval:";
            CriticalPathRecomputeIntervalLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CriticalPathRecomputeIntervalValue
            // 
            CriticalPathRecomputeIntervalValue.Dock = DockStyle.Fill;
            CriticalPathRecomputeIntervalValue.Location = new Point(203, 213);
            CriticalPathRecomputeIntervalValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            CriticalPathRecomputeIntervalValue.Name = "CriticalPathRecomputeIntervalValue";
            CriticalPathRecomputeIntervalValue.Size = new Size(511, 23);
            CriticalPathRecomputeIntervalValue.TabIndex = 0;
            CriticalPathRecomputeIntervalValue.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // BatchSizeLabel
            // 
            BatchSizeLabel.AutoSize = true;
            BatchSizeLabel.Dock = DockStyle.Fill;
            BatchSizeLabel.Location = new Point(3, 240);
            BatchSizeLabel.Name = "BatchSizeLabel";
            BatchSizeLabel.Size = new Size(194, 30);
            BatchSizeLabel.TabIndex = 25;
            BatchSizeLabel.Text = "Batch Size:";
            BatchSizeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BatchSizeValue
            // 
            BatchSizeValue.Dock = DockStyle.Fill;
            BatchSizeValue.Location = new Point(203, 243);
            BatchSizeValue.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            BatchSizeValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            BatchSizeValue.Name = "BatchSizeValue";
            BatchSizeValue.Size = new Size(511, 23);
            BatchSizeValue.TabIndex = 24;
            BatchSizeValue.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // CriticalPathBoostLabel
            // 
            CriticalPathBoostLabel.AutoSize = true;
            CriticalPathBoostLabel.Dock = DockStyle.Fill;
            CriticalPathBoostLabel.Location = new Point(3, 270);
            CriticalPathBoostLabel.Name = "CriticalPathBoostLabel";
            CriticalPathBoostLabel.Size = new Size(194, 30);
            CriticalPathBoostLabel.TabIndex = 23;
            CriticalPathBoostLabel.Text = "Critical Path Boost:";
            CriticalPathBoostLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CriticalPathBoostValue
            // 
            CriticalPathBoostValue.DecimalPlaces = 2;
            CriticalPathBoostValue.Dock = DockStyle.Fill;
            CriticalPathBoostValue.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            CriticalPathBoostValue.Location = new Point(203, 273);
            CriticalPathBoostValue.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            CriticalPathBoostValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            CriticalPathBoostValue.Name = "CriticalPathBoostValue";
            CriticalPathBoostValue.Size = new Size(511, 23);
            CriticalPathBoostValue.TabIndex = 22;
            CriticalPathBoostValue.Value = new decimal(new int[] { 15, 0, 0, 65536 });
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Dock = DockStyle.Fill;
            Label2.Location = new Point(3, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(194, 30);
            Label2.TabIndex = 15;
            Label2.Text = "Mode:";
            Label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ModeComboBox
            // 
            ModeComboBox.Dock = DockStyle.Fill;
            ModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ModeComboBox.FormattingEnabled = true;
            ModeComboBox.Items.AddRange(new object[] { "SimpleDfs", "Adaptive", "AdaptiveBatched" });
            ModeComboBox.Location = new Point(203, 3);
            ModeComboBox.Name = "ModeComboBox";
            ModeComboBox.Size = new Size(511, 23);
            ModeComboBox.TabIndex = 14;
            // 
            // MaxDegreeOfParallelismLabel
            // 
            MaxDegreeOfParallelismLabel.AutoSize = true;
            MaxDegreeOfParallelismLabel.Dock = DockStyle.Fill;
            MaxDegreeOfParallelismLabel.Location = new Point(3, 30);
            MaxDegreeOfParallelismLabel.Name = "MaxDegreeOfParallelismLabel";
            MaxDegreeOfParallelismLabel.Size = new Size(194, 30);
            MaxDegreeOfParallelismLabel.TabIndex = 13;
            MaxDegreeOfParallelismLabel.Text = "Max Degree of Parallelism:";
            MaxDegreeOfParallelismLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MaxDegreeOfParallelismValue
            // 
            MaxDegreeOfParallelismValue.Dock = DockStyle.Fill;
            MaxDegreeOfParallelismValue.Location = new Point(203, 33);
            MaxDegreeOfParallelismValue.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            MaxDegreeOfParallelismValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MaxDegreeOfParallelismValue.Name = "MaxDegreeOfParallelismValue";
            MaxDegreeOfParallelismValue.Size = new Size(511, 23);
            MaxDegreeOfParallelismValue.TabIndex = 12;
            MaxDegreeOfParallelismValue.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // EnableGcThrottlingLabel
            // 
            EnableGcThrottlingLabel.AutoSize = true;
            EnableGcThrottlingLabel.Dock = DockStyle.Fill;
            EnableGcThrottlingLabel.Location = new Point(3, 60);
            EnableGcThrottlingLabel.Name = "EnableGcThrottlingLabel";
            EnableGcThrottlingLabel.Size = new Size(194, 30);
            EnableGcThrottlingLabel.TabIndex = 11;
            EnableGcThrottlingLabel.Text = "Enable GC Throttling:";
            EnableGcThrottlingLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // EnableGcThrottlingValue
            // 
            EnableGcThrottlingValue.AutoSize = true;
            EnableGcThrottlingValue.Dock = DockStyle.Fill;
            EnableGcThrottlingValue.Location = new Point(203, 63);
            EnableGcThrottlingValue.Name = "EnableGcThrottlingValue";
            EnableGcThrottlingValue.Size = new Size(511, 24);
            EnableGcThrottlingValue.TabIndex = 10;
            EnableGcThrottlingValue.UseVisualStyleBackColor = true;
            // 
            // EditorGroupBox
            // 
            EditorGroupBox.Controls.Add(EditorTable);
            EditorGroupBox.Dock = DockStyle.Top;
            EditorGroupBox.Location = new Point(0, 330);
            EditorGroupBox.Name = "EditorGroupBox";
            EditorGroupBox.Size = new Size(723, 180);
            EditorGroupBox.TabIndex = 1;
            EditorGroupBox.TabStop = false;
            EditorGroupBox.Text = "Editor";
            // 
            // EditorTable
            // 
            EditorTable.ColumnCount = 2;
            EditorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            EditorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            EditorTable.Controls.Add(AutoSnapZoneWidthLabel, 0, 4);
            EditorTable.Controls.Add(AutoSnapZoneWidthValue, 1, 4);
            EditorTable.Controls.Add(RenderScaleLabel, 0, 3);
            EditorTable.Controls.Add(RenderScaleValue, 1, 3);
            EditorTable.Controls.Add(SocketRadiusLabel, 0, 2);
            EditorTable.Controls.Add(SocketRadiusValue, 1, 2);
            EditorTable.Controls.Add(AllowOutOfScreenPanLabel, 0, 1);
            EditorTable.Controls.Add(AllowOutOfScreenPanValue, 1, 1);
            EditorTable.Controls.Add(Label3, 0, 0);
            EditorTable.Controls.Add(SelectedBlockOutlineColorButton, 1, 0);
            EditorTable.Dock = DockStyle.Fill;
            EditorTable.Location = new Point(3, 19);
            EditorTable.Name = "EditorTable";
            EditorTable.RowCount = 5;
            EditorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            EditorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            EditorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            EditorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            EditorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            EditorTable.Size = new Size(717, 158);
            EditorTable.TabIndex = 0;
            // 
            // AutoSnapZoneWidthLabel
            // 
            AutoSnapZoneWidthLabel.AutoSize = true;
            AutoSnapZoneWidthLabel.Dock = DockStyle.Fill;
            AutoSnapZoneWidthLabel.Location = new Point(3, 120);
            AutoSnapZoneWidthLabel.Name = "AutoSnapZoneWidthLabel";
            AutoSnapZoneWidthLabel.Size = new Size(194, 38);
            AutoSnapZoneWidthLabel.TabIndex = 5;
            AutoSnapZoneWidthLabel.Text = "Auto Snap Zone Width:";
            AutoSnapZoneWidthLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AutoSnapZoneWidthValue
            // 
            AutoSnapZoneWidthValue.Dock = DockStyle.Fill;
            AutoSnapZoneWidthValue.Location = new Point(203, 123);
            AutoSnapZoneWidthValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            AutoSnapZoneWidthValue.Name = "AutoSnapZoneWidthValue";
            AutoSnapZoneWidthValue.Size = new Size(511, 23);
            AutoSnapZoneWidthValue.TabIndex = 4;
            AutoSnapZoneWidthValue.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // RenderScaleLabel
            // 
            RenderScaleLabel.AutoSize = true;
            RenderScaleLabel.Dock = DockStyle.Fill;
            RenderScaleLabel.Location = new Point(3, 90);
            RenderScaleLabel.Name = "RenderScaleLabel";
            RenderScaleLabel.Size = new Size(194, 30);
            RenderScaleLabel.TabIndex = 3;
            RenderScaleLabel.Text = "Render Scale:";
            RenderScaleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RenderScaleValue
            // 
            RenderScaleValue.DecimalPlaces = 2;
            RenderScaleValue.Dock = DockStyle.Fill;
            RenderScaleValue.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            RenderScaleValue.Location = new Point(203, 93);
            RenderScaleValue.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            RenderScaleValue.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            RenderScaleValue.Name = "RenderScaleValue";
            RenderScaleValue.Size = new Size(511, 23);
            RenderScaleValue.TabIndex = 2;
            RenderScaleValue.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // SocketRadiusLabel
            // 
            SocketRadiusLabel.AutoSize = true;
            SocketRadiusLabel.Dock = DockStyle.Fill;
            SocketRadiusLabel.Location = new Point(3, 60);
            SocketRadiusLabel.Name = "SocketRadiusLabel";
            SocketRadiusLabel.Size = new Size(194, 30);
            SocketRadiusLabel.TabIndex = 1;
            SocketRadiusLabel.Text = "Socket Radius:";
            SocketRadiusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SocketRadiusValue
            // 
            SocketRadiusValue.Dock = DockStyle.Fill;
            SocketRadiusValue.Location = new Point(203, 63);
            SocketRadiusValue.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            SocketRadiusValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SocketRadiusValue.Name = "SocketRadiusValue";
            SocketRadiusValue.Size = new Size(511, 23);
            SocketRadiusValue.TabIndex = 0;
            SocketRadiusValue.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // AllowOutOfScreenPanLabel
            // 
            AllowOutOfScreenPanLabel.AutoSize = true;
            AllowOutOfScreenPanLabel.Dock = DockStyle.Fill;
            AllowOutOfScreenPanLabel.Location = new Point(3, 30);
            AllowOutOfScreenPanLabel.Name = "AllowOutOfScreenPanLabel";
            AllowOutOfScreenPanLabel.Size = new Size(194, 30);
            AllowOutOfScreenPanLabel.TabIndex = 7;
            AllowOutOfScreenPanLabel.Text = "Allow Out-of-Screen Pan:";
            AllowOutOfScreenPanLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AllowOutOfScreenPanValue
            // 
            AllowOutOfScreenPanValue.AutoSize = true;
            AllowOutOfScreenPanValue.Dock = DockStyle.Fill;
            AllowOutOfScreenPanValue.Location = new Point(203, 33);
            AllowOutOfScreenPanValue.Name = "AllowOutOfScreenPanValue";
            AllowOutOfScreenPanValue.Size = new Size(511, 24);
            AllowOutOfScreenPanValue.TabIndex = 6;
            AllowOutOfScreenPanValue.UseVisualStyleBackColor = true;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Dock = DockStyle.Fill;
            Label3.Location = new Point(3, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(194, 30);
            Label3.TabIndex = 9;
            Label3.Text = "Selected Block Outline Color:";
            Label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SelectedBlockOutlineColorButton
            // 
            SelectedBlockOutlineColorButton.Dock = DockStyle.Fill;
            SelectedBlockOutlineColorButton.Location = new Point(203, 3);
            SelectedBlockOutlineColorButton.Name = "SelectedBlockOutlineColorButton";
            SelectedBlockOutlineColorButton.Size = new Size(511, 24);
            SelectedBlockOutlineColorButton.TabIndex = 8;
            SelectedBlockOutlineColorButton.Text = "Color";
            SelectedBlockOutlineColorButton.UseVisualStyleBackColor = true;
            // 
            // ThemeGroupBox
            // 
            ThemeGroupBox.Controls.Add(ThemeTable);
            ThemeGroupBox.Dock = DockStyle.Top;
            ThemeGroupBox.Location = new Point(0, 0);
            ThemeGroupBox.Name = "ThemeGroupBox";
            ThemeGroupBox.Size = new Size(723, 330);
            ThemeGroupBox.TabIndex = 2;
            ThemeGroupBox.TabStop = false;
            ThemeGroupBox.Text = "Theme";
            // 
            // ThemeTable
            // 
            ThemeTable.ColumnCount = 2;
            ThemeTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            ThemeTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ThemeTable.Controls.Add(NodeWidthLabel, 0, 10);
            ThemeTable.Controls.Add(NodeWidthValue, 1, 10);
            ThemeTable.Controls.Add(NodeBorderWidthLabel, 0, 9);
            ThemeTable.Controls.Add(NodeBorderWidthValue, 1, 9);
            ThemeTable.Controls.Add(NodeSpacingLabel, 0, 8);
            ThemeTable.Controls.Add(NodeSpacingValue, 1, 8);
            ThemeTable.Controls.Add(TextColorLabel, 0, 7);
            ThemeTable.Controls.Add(TextColorColorButton, 1, 7);
            ThemeTable.Controls.Add(DisabledNodeColorLabel, 0, 6);
            ThemeTable.Controls.Add(DisabledNodeColorColorButton, 1, 6);
            ThemeTable.Controls.Add(SuccessColorLabel, 0, 5);
            ThemeTable.Controls.Add(SuccessColorColorButton, 1, 5);
            ThemeTable.Controls.Add(ErrorColorLabel, 0, 4);
            ThemeTable.Controls.Add(ErrorColorColorButton, 1, 4);
            ThemeTable.Controls.Add(HoveredNodeColorLabel, 0, 3);
            ThemeTable.Controls.Add(HoveredNodeColorColorButton, 1, 3);
            ThemeTable.Controls.Add(DefaultNodeColorLabel, 0, 2);
            ThemeTable.Controls.Add(DefaultNodeColorColorButton, 1, 2);
            ThemeTable.Controls.Add(SelectedBlockOutlineLabel, 0, 1);
            ThemeTable.Controls.Add(SelectedBlockOutlineColorButton2, 1, 1);
            ThemeTable.Controls.Add(HoveredBlockOutlineLabel, 0, 0);
            ThemeTable.Controls.Add(HoveredBlockOutlineColorButton, 1, 0);
            ThemeTable.Controls.Add(BorderOutlineLabel, 0, 11);
            ThemeTable.Controls.Add(BorderOutlineColorButton, 1, 11);
            ThemeTable.Controls.Add(SocketConnectionColorLabel, 0, 12);
            ThemeTable.Controls.Add(SocketConnectionColorColorButton, 1, 12);
            ThemeTable.Dock = DockStyle.Fill;
            ThemeTable.Location = new Point(3, 19);
            ThemeTable.Name = "ThemeTable";
            ThemeTable.RowCount = 13;
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            ThemeTable.Size = new Size(717, 308);
            ThemeTable.TabIndex = 0;
            // 
            // NodeWidthLabel
            // 
            NodeWidthLabel.AutoSize = true;
            NodeWidthLabel.Dock = DockStyle.Fill;
            NodeWidthLabel.Location = new Point(3, 300);
            NodeWidthLabel.Name = "NodeWidthLabel";
            NodeWidthLabel.Size = new Size(194, 30);
            NodeWidthLabel.TabIndex = 23;
            NodeWidthLabel.Text = "Node Width:";
            NodeWidthLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // NodeWidthValue
            // 
            NodeWidthValue.Dock = DockStyle.Fill;
            NodeWidthValue.Location = new Point(203, 303);
            NodeWidthValue.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            NodeWidthValue.Name = "NodeWidthValue";
            NodeWidthValue.Size = new Size(511, 23);
            NodeWidthValue.TabIndex = 22;
            NodeWidthValue.Value = new decimal(new int[] { 35, 0, 0, 0 });
            // 
            // NodeBorderWidthLabel
            // 
            NodeBorderWidthLabel.AutoSize = true;
            NodeBorderWidthLabel.Dock = DockStyle.Fill;
            NodeBorderWidthLabel.Location = new Point(3, 270);
            NodeBorderWidthLabel.Name = "NodeBorderWidthLabel";
            NodeBorderWidthLabel.Size = new Size(194, 30);
            NodeBorderWidthLabel.TabIndex = 21;
            NodeBorderWidthLabel.Text = "Node Border Width:";
            NodeBorderWidthLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // NodeBorderWidthValue
            // 
            NodeBorderWidthValue.Dock = DockStyle.Fill;
            NodeBorderWidthValue.Location = new Point(203, 273);
            NodeBorderWidthValue.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            NodeBorderWidthValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NodeBorderWidthValue.Name = "NodeBorderWidthValue";
            NodeBorderWidthValue.Size = new Size(511, 23);
            NodeBorderWidthValue.TabIndex = 20;
            NodeBorderWidthValue.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // NodeSpacingLabel
            // 
            NodeSpacingLabel.AutoSize = true;
            NodeSpacingLabel.Dock = DockStyle.Fill;
            NodeSpacingLabel.Location = new Point(3, 240);
            NodeSpacingLabel.Name = "NodeSpacingLabel";
            NodeSpacingLabel.Size = new Size(194, 30);
            NodeSpacingLabel.TabIndex = 19;
            NodeSpacingLabel.Text = "Node Spacing:";
            NodeSpacingLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // NodeSpacingValue
            // 
            NodeSpacingValue.Dock = DockStyle.Fill;
            NodeSpacingValue.Location = new Point(203, 243);
            NodeSpacingValue.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            NodeSpacingValue.Name = "NodeSpacingValue";
            NodeSpacingValue.Size = new Size(511, 23);
            NodeSpacingValue.TabIndex = 18;
            NodeSpacingValue.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // TextColorLabel
            // 
            TextColorLabel.AutoSize = true;
            TextColorLabel.Dock = DockStyle.Fill;
            TextColorLabel.Location = new Point(3, 210);
            TextColorLabel.Name = "TextColorLabel";
            TextColorLabel.Size = new Size(194, 30);
            TextColorLabel.TabIndex = 17;
            TextColorLabel.Text = "Text Color:";
            TextColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextColorColorButton
            // 
            TextColorColorButton.Dock = DockStyle.Fill;
            TextColorColorButton.Location = new Point(203, 213);
            TextColorColorButton.Name = "TextColorColorButton";
            TextColorColorButton.Size = new Size(511, 24);
            TextColorColorButton.TabIndex = 16;
            TextColorColorButton.Text = "Color";
            TextColorColorButton.UseVisualStyleBackColor = true;
            // 
            // DisabledNodeColorLabel
            // 
            DisabledNodeColorLabel.AutoSize = true;
            DisabledNodeColorLabel.Dock = DockStyle.Fill;
            DisabledNodeColorLabel.Location = new Point(3, 180);
            DisabledNodeColorLabel.Name = "DisabledNodeColorLabel";
            DisabledNodeColorLabel.Size = new Size(194, 30);
            DisabledNodeColorLabel.TabIndex = 15;
            DisabledNodeColorLabel.Text = "Disabled Node Color:";
            DisabledNodeColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DisabledNodeColorColorButton
            // 
            DisabledNodeColorColorButton.Dock = DockStyle.Fill;
            DisabledNodeColorColorButton.Location = new Point(203, 183);
            DisabledNodeColorColorButton.Name = "DisabledNodeColorColorButton";
            DisabledNodeColorColorButton.Size = new Size(511, 24);
            DisabledNodeColorColorButton.TabIndex = 14;
            DisabledNodeColorColorButton.Text = "Color";
            DisabledNodeColorColorButton.UseVisualStyleBackColor = true;
            // 
            // SuccessColorLabel
            // 
            SuccessColorLabel.AutoSize = true;
            SuccessColorLabel.Dock = DockStyle.Fill;
            SuccessColorLabel.Location = new Point(3, 150);
            SuccessColorLabel.Name = "SuccessColorLabel";
            SuccessColorLabel.Size = new Size(194, 30);
            SuccessColorLabel.TabIndex = 13;
            SuccessColorLabel.Text = "Success Color:";
            SuccessColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SuccessColorColorButton
            // 
            SuccessColorColorButton.Dock = DockStyle.Fill;
            SuccessColorColorButton.Location = new Point(203, 153);
            SuccessColorColorButton.Name = "SuccessColorColorButton";
            SuccessColorColorButton.Size = new Size(511, 24);
            SuccessColorColorButton.TabIndex = 12;
            SuccessColorColorButton.Text = "Color";
            SuccessColorColorButton.UseVisualStyleBackColor = true;
            // 
            // ErrorColorLabel
            // 
            ErrorColorLabel.AutoSize = true;
            ErrorColorLabel.Dock = DockStyle.Fill;
            ErrorColorLabel.Location = new Point(3, 120);
            ErrorColorLabel.Name = "ErrorColorLabel";
            ErrorColorLabel.Size = new Size(194, 30);
            ErrorColorLabel.TabIndex = 11;
            ErrorColorLabel.Text = "Error Color:";
            ErrorColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ErrorColorColorButton
            // 
            ErrorColorColorButton.Dock = DockStyle.Fill;
            ErrorColorColorButton.Location = new Point(203, 123);
            ErrorColorColorButton.Name = "ErrorColorColorButton";
            ErrorColorColorButton.Size = new Size(511, 24);
            ErrorColorColorButton.TabIndex = 10;
            ErrorColorColorButton.Text = "Color";
            ErrorColorColorButton.UseVisualStyleBackColor = true;
            // 
            // HoveredNodeColorLabel
            // 
            HoveredNodeColorLabel.AutoSize = true;
            HoveredNodeColorLabel.Dock = DockStyle.Fill;
            HoveredNodeColorLabel.Location = new Point(3, 90);
            HoveredNodeColorLabel.Name = "HoveredNodeColorLabel";
            HoveredNodeColorLabel.Size = new Size(194, 30);
            HoveredNodeColorLabel.TabIndex = 9;
            HoveredNodeColorLabel.Text = "Hovered Node Color:";
            HoveredNodeColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // HoveredNodeColorColorButton
            // 
            HoveredNodeColorColorButton.Dock = DockStyle.Fill;
            HoveredNodeColorColorButton.Location = new Point(203, 93);
            HoveredNodeColorColorButton.Name = "HoveredNodeColorColorButton";
            HoveredNodeColorColorButton.Size = new Size(511, 24);
            HoveredNodeColorColorButton.TabIndex = 8;
            HoveredNodeColorColorButton.Text = "Color";
            HoveredNodeColorColorButton.UseVisualStyleBackColor = true;
            // 
            // DefaultNodeColorLabel
            // 
            DefaultNodeColorLabel.AutoSize = true;
            DefaultNodeColorLabel.Dock = DockStyle.Fill;
            DefaultNodeColorLabel.Location = new Point(3, 60);
            DefaultNodeColorLabel.Name = "DefaultNodeColorLabel";
            DefaultNodeColorLabel.Size = new Size(194, 30);
            DefaultNodeColorLabel.TabIndex = 7;
            DefaultNodeColorLabel.Text = "Default Node Color:";
            DefaultNodeColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DefaultNodeColorColorButton
            // 
            DefaultNodeColorColorButton.Dock = DockStyle.Fill;
            DefaultNodeColorColorButton.Location = new Point(203, 63);
            DefaultNodeColorColorButton.Name = "DefaultNodeColorColorButton";
            DefaultNodeColorColorButton.Size = new Size(511, 24);
            DefaultNodeColorColorButton.TabIndex = 6;
            DefaultNodeColorColorButton.Text = "Color";
            DefaultNodeColorColorButton.UseVisualStyleBackColor = true;
            // 
            // SelectedBlockOutlineLabel
            // 
            SelectedBlockOutlineLabel.AutoSize = true;
            SelectedBlockOutlineLabel.Dock = DockStyle.Fill;
            SelectedBlockOutlineLabel.Location = new Point(3, 30);
            SelectedBlockOutlineLabel.Name = "SelectedBlockOutlineLabel";
            SelectedBlockOutlineLabel.Size = new Size(194, 30);
            SelectedBlockOutlineLabel.TabIndex = 5;
            SelectedBlockOutlineLabel.Text = "Selected Block Outline:";
            SelectedBlockOutlineLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SelectedBlockOutlineColorButton2
            // 
            SelectedBlockOutlineColorButton2.Dock = DockStyle.Fill;
            SelectedBlockOutlineColorButton2.Location = new Point(203, 33);
            SelectedBlockOutlineColorButton2.Name = "SelectedBlockOutlineColorButton2";
            SelectedBlockOutlineColorButton2.Size = new Size(511, 24);
            SelectedBlockOutlineColorButton2.TabIndex = 4;
            SelectedBlockOutlineColorButton2.Text = "Color";
            SelectedBlockOutlineColorButton2.UseVisualStyleBackColor = true;
            // 
            // HoveredBlockOutlineLabel
            // 
            HoveredBlockOutlineLabel.AutoSize = true;
            HoveredBlockOutlineLabel.Dock = DockStyle.Fill;
            HoveredBlockOutlineLabel.Location = new Point(3, 0);
            HoveredBlockOutlineLabel.Name = "HoveredBlockOutlineLabel";
            HoveredBlockOutlineLabel.Size = new Size(194, 30);
            HoveredBlockOutlineLabel.TabIndex = 3;
            HoveredBlockOutlineLabel.Text = "Hovered Block Outline:";
            HoveredBlockOutlineLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // HoveredBlockOutlineColorButton
            // 
            HoveredBlockOutlineColorButton.Dock = DockStyle.Fill;
            HoveredBlockOutlineColorButton.Location = new Point(203, 3);
            HoveredBlockOutlineColorButton.Name = "HoveredBlockOutlineColorButton";
            HoveredBlockOutlineColorButton.Size = new Size(511, 24);
            HoveredBlockOutlineColorButton.TabIndex = 2;
            HoveredBlockOutlineColorButton.Text = "Color";
            HoveredBlockOutlineColorButton.UseVisualStyleBackColor = true;
            // 
            // BorderOutlineLabel
            // 
            BorderOutlineLabel.AutoSize = true;
            BorderOutlineLabel.Dock = DockStyle.Fill;
            BorderOutlineLabel.Location = new Point(3, 330);
            BorderOutlineLabel.Name = "BorderOutlineLabel";
            BorderOutlineLabel.Size = new Size(194, 30);
            BorderOutlineLabel.TabIndex = 25;
            BorderOutlineLabel.Text = "Border Outline:";
            BorderOutlineLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BorderOutlineColorButton
            // 
            BorderOutlineColorButton.Dock = DockStyle.Fill;
            BorderOutlineColorButton.Location = new Point(203, 333);
            BorderOutlineColorButton.Name = "BorderOutlineColorButton";
            BorderOutlineColorButton.Size = new Size(511, 24);
            BorderOutlineColorButton.TabIndex = 24;
            BorderOutlineColorButton.Text = "Color";
            BorderOutlineColorButton.UseVisualStyleBackColor = true;
            // 
            // SocketConnectionColorLabel
            // 
            SocketConnectionColorLabel.AutoSize = true;
            SocketConnectionColorLabel.Dock = DockStyle.Fill;
            SocketConnectionColorLabel.Location = new Point(3, 360);
            SocketConnectionColorLabel.Name = "SocketConnectionColorLabel";
            SocketConnectionColorLabel.Size = new Size(194, 30);
            SocketConnectionColorLabel.TabIndex = 27;
            SocketConnectionColorLabel.Text = "Socket Connection Color:";
            SocketConnectionColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SocketConnectionColorColorButton
            // 
            SocketConnectionColorColorButton.Dock = DockStyle.Fill;
            SocketConnectionColorColorButton.Location = new Point(203, 363);
            SocketConnectionColorColorButton.Name = "SocketConnectionColorColorButton";
            SocketConnectionColorColorButton.Size = new Size(511, 24);
            SocketConnectionColorColorButton.TabIndex = 26;
            SocketConnectionColorColorButton.Text = "Color";
            SocketConnectionColorColorButton.UseVisualStyleBackColor = true;
            // 
            // SettingsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainScrollPanel);
            Name = "SettingsView";
            Size = new Size(740, 600);
            MainScrollPanel.ResumeLayout(false);
            ExecutionGroupBox.ResumeLayout(false);
            ExecutionTable.ResumeLayout(false);
            ExecutionTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WatchdogTimeoutValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxShipmentSizeValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)ProfilingWindowSizeValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)CostEmaAlphaValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)CriticalPathRecomputeIntervalValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)BatchSizeValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)CriticalPathBoostValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxDegreeOfParallelismValue).EndInit();
            EditorGroupBox.ResumeLayout(false);
            EditorTable.ResumeLayout(false);
            EditorTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AutoSnapZoneWidthValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)RenderScaleValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)SocketRadiusValue).EndInit();
            ThemeGroupBox.ResumeLayout(false);
            ThemeTable.ResumeLayout(false);
            ThemeTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NodeWidthValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)NodeBorderWidthValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)NodeSpacingValue).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel MainScrollPanel;
        private GroupBox ExecutionGroupBox;
        private TableLayoutPanel ExecutionTable;
        private Label WatchdogTimeoutLabel;
        private NumericUpDown WatchdogTimeoutValue;
        private Label MaxDegreeOfParallelismLabel;
        private NumericUpDown MaxDegreeOfParallelismValue;
        private Label EnableGcThrottlingLabel;
        private CheckBox EnableGcThrottlingValue;
        private Label ProfilingWindowSizeLabel;
        private NumericUpDown ProfilingWindowSizeValue;
        private Label CostEmaAlphaLabel;
        private NumericUpDown CostEmaAlphaValue;
        private Label CriticalPathRecomputeIntervalLabel;
        private NumericUpDown CriticalPathRecomputeIntervalValue;
        private Label BatchSizeLabel;
        private NumericUpDown BatchSizeValue;
        private Label CriticalPathBoostLabel;
        private NumericUpDown CriticalPathBoostValue;
        private Label MaxShipmentSizeLabel;
        private NumericUpDown MaxShipmentSizeValue;
        private Label Label2;
        private ComboBox ModeComboBox;
        private GroupBox EditorGroupBox;
        private TableLayoutPanel EditorTable;
        private Label AutoSnapZoneWidthLabel;
        private NumericUpDown AutoSnapZoneWidthValue;
        private Label RenderScaleLabel;
        private NumericUpDown RenderScaleValue;
        private Label SocketRadiusLabel;
        private NumericUpDown SocketRadiusValue;
        private Label AllowOutOfScreenPanLabel;
        private CheckBox AllowOutOfScreenPanValue;
        private Label Label3;
        private ImageAutomate.UI.ColorDialogButton SelectedBlockOutlineColorButton;
        private GroupBox ThemeGroupBox;
        private TableLayoutPanel ThemeTable;
        private Label NodeWidthLabel;
        private NumericUpDown NodeWidthValue;
        private Label NodeBorderWidthLabel;
        private NumericUpDown NodeBorderWidthValue;
        private Label NodeSpacingLabel;
        private NumericUpDown NodeSpacingValue;
        private Label TextColorLabel;
        private ImageAutomate.UI.ColorDialogButton TextColorColorButton;
        private Label DisabledNodeColorLabel;
        private ImageAutomate.UI.ColorDialogButton DisabledNodeColorColorButton;
        private Label SuccessColorLabel;
        private ImageAutomate.UI.ColorDialogButton SuccessColorColorButton;
        private Label ErrorColorLabel;
        private ImageAutomate.UI.ColorDialogButton ErrorColorColorButton;
        private Label HoveredNodeColorLabel;
        private ImageAutomate.UI.ColorDialogButton HoveredNodeColorColorButton;
        private Label DefaultNodeColorLabel;
        private ImageAutomate.UI.ColorDialogButton DefaultNodeColorColorButton;
        private Label SelectedBlockOutlineLabel;
        private ImageAutomate.UI.ColorDialogButton SelectedBlockOutlineColorButton2;
        private Label HoveredBlockOutlineLabel;
        private ImageAutomate.UI.ColorDialogButton HoveredBlockOutlineColorButton;
        private Label BorderOutlineLabel;
        private ImageAutomate.UI.ColorDialogButton BorderOutlineColorButton;
        private Label SocketConnectionColorLabel;
        private ImageAutomate.UI.ColorDialogButton SocketConnectionColorColorButton;
    }
}
