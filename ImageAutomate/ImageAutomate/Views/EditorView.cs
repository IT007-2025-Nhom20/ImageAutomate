using System.Diagnostics;

using ImageAutomate.Core;
using ImageAutomate.Execution;
using ImageAutomate.StandardBlocks;
using ImageAutomate.UI;


namespace ImageAutomate.Views;

public partial class EditorView : UserControl
{
    private PipelineGraph graph = new();
    public EditorView()
    {
        InitializeComponent();

        var workspace = new Workspace(graph);
        GraphPanel.Workspace = workspace;

        Toolbox.DisplayMember = "Name";
        Toolbox.Items.AddRange(new Type[]
        {
            typeof(LoadBlock),
            typeof(SaveBlock),
            typeof(ConvertBlock),
            typeof(CropBlock),
            typeof(ResizeBlock),
            typeof(FlipBlock),
            typeof(GrayscaleBlock),
            typeof(BrightnessBlock),
            typeof(ContrastBlock),
            typeof(HueBlock),
            typeof(SaturationBlock),
            typeof(GaussianBlurBlock),
            typeof(SharpenBlock),
            typeof(PixelateBlock),
            typeof(VignetteBlock)
        });
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Delete)
        {
            GraphPanel.DeleteSelectedItem();
            GraphPanel.Invalidate();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void OnToolboxMouseDown(object sender, MouseEventArgs e)
    {
        ListBox lb = Toolbox;
        Point pt = new(e.X, e.Y);
        int index = lb.IndexFromPoint(pt);

        if (index >= 0)
        {
            lb.DoDragDrop(lb.Items[index], DragDropEffects.Copy);
        }
    }

    private async void OnExecuteMenuItemClick(object sender, EventArgs e)
    {
        Debug.WriteLine("Execute was called");

        GraphExecutor executor = new(new GraphValidator());

        try
        {
            // Disable the execute button/menu during execution
            if (sender is ToolStripMenuItem menuItem)
            {
                menuItem.Enabled = false;
            }

            // Use ExecuteAsync with default configuration and cancellation token
            await executor.ExecuteAsync(graph, new ExecutorConfiguration(), CancellationToken.None);

            MessageBox.Show("Pipeline execution completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred during execution: {ex.Message}", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            // Re-enable the execute button/menu
            if (sender is ToolStripMenuItem menuItem)
            {
                menuItem.Enabled = true;
            }
        }
    }

    private void OnGraphSelectedItemChange(object? sender, EventArgs e)
    {
        if (sender is GraphRenderPanel panel)
        {
            BlockPropertyGrid.SelectedObject = panel.Graph?.SelectedItem;
        }
    }
}
