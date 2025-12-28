namespace ImageAutomate.Views.DashboardViews
{
    partial class WorkspaceView
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
            LabelHeader = new Label();
            TextBoxSearch = new TextBox();
            BtnNew = new Button();
            BtnBrowse = new Button();
            PanelWorkspaces = new FlowLayoutPanel();
            LabelEmpty = new Label();
            SuspendLayout();
            // 
            // LabelHeader
            // 
            LabelHeader.AutoSize = true;
            LabelHeader.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            LabelHeader.Location = new Point(20, 20);
            LabelHeader.Name = "LabelHeader";
            LabelHeader.Size = new Size(176, 32);
            LabelHeader.TabIndex = 0;
            LabelHeader.Text = "My Workspaces";
            // 
            // TextBoxSearch
            // 
            TextBoxSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBoxSearch.Font = new Font("Segoe UI", 10F);
            TextBoxSearch.Location = new Point(20, 65);
            TextBoxSearch.Name = "TextBoxSearch";
            TextBoxSearch.PlaceholderText = "Search workspaces...";
            TextBoxSearch.Size = new Size(450, 25);
            TextBoxSearch.TabIndex = 1;
            // 
            // BtnNew
            // 
            BtnNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnNew.Font = new Font("Segoe UI", 9F);
            BtnNew.Location = new Point(490, 65);
            BtnNew.Name = "BtnNew";
            BtnNew.Size = new Size(110, 25);
            BtnNew.TabIndex = 2;
            BtnNew.Text = "Create New";
            BtnNew.UseVisualStyleBackColor = true;
            // 
            // BtnBrowse
            // 
            BtnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnBrowse.Font = new Font("Segoe UI", 9F);
            BtnBrowse.Location = new Point(610, 65);
            BtnBrowse.Name = "BtnBrowse";
            BtnBrowse.Size = new Size(110, 25);
            BtnBrowse.TabIndex = 3;
            BtnBrowse.Text = "Browse...";
            BtnBrowse.UseVisualStyleBackColor = true;
            // 
            // PanelWorkspaces
            // 
            PanelWorkspaces.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PanelWorkspaces.AutoScroll = true;
            PanelWorkspaces.Location = new Point(20, 105);
            PanelWorkspaces.Name = "PanelWorkspaces";
            PanelWorkspaces.Padding = new Padding(5);
            PanelWorkspaces.Size = new Size(700, 475);
            PanelWorkspaces.TabIndex = 4;
            // 
            // LabelEmpty
            // 
            LabelEmpty.Anchor = AnchorStyles.None;
            LabelEmpty.AutoSize = true;
            LabelEmpty.Font = new Font("Segoe UI", 11F);
            LabelEmpty.ForeColor = SystemColors.GrayText;
            LabelEmpty.Location = new Point(245, 300);
            LabelEmpty.Name = "LabelEmpty";
            LabelEmpty.Size = new Size(250, 20);
            LabelEmpty.TabIndex = 5;
            LabelEmpty.Text = "No workspaces found. Create a new one!";
            LabelEmpty.Visible = false;
            // 
            // WorkspaceView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(LabelEmpty);
            Controls.Add(PanelWorkspaces);
            Controls.Add(BtnBrowse);
            Controls.Add(BtnNew);
            Controls.Add(TextBoxSearch);
            Controls.Add(LabelHeader);
            Name = "WorkspaceView";
            Size = new Size(740, 600);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelHeader;
        private TextBox TextBoxSearch;
        private Button BtnNew;
        private Button BtnBrowse;
        private FlowLayoutPanel PanelWorkspaces;
        private Label LabelEmpty;
    }
}
