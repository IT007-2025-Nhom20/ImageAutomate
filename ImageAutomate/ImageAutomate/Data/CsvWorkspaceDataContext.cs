using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImageAutomate.Models;
using nietras.SeparatedValues;

namespace ImageAutomate.Data
{
    /// <summary>
    /// CSV-based implementation of workspace data storage using SEP library.
    /// Stores workspace metadata in %APPDATA%/ImageAutomate/workspaces.csv
    /// </summary>
    public class CsvWorkspaceDataContext : IWorkspaceDataContext
    {
        private readonly string _csvFilePath;
        private readonly List<WorkspaceInfo> _workspaces;
        private readonly object _lock = new();

        /// <summary>
        /// Gets the default CSV file path in %APPDATA%/ImageAutomate/workspaces.csv
        /// </summary>
        public static string DefaultCsvPath
        {
            get
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var appFolder = Path.Combine(appDataPath, "ImageAutomate");
                Directory.CreateDirectory(appFolder);
                return Path.Combine(appFolder, "workspaces.csv");
            }
        }

        public CsvWorkspaceDataContext() : this(DefaultCsvPath)
        {
        }

        public CsvWorkspaceDataContext(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
            _workspaces = new List<WorkspaceInfo>();
            LoadFromFile();
        }

        public List<WorkspaceInfo> GetAll()
        {
            lock (_lock)
            {
                return new List<WorkspaceInfo>(_workspaces);
            }
        }

        public void Add(WorkspaceInfo workspace)
        {
            lock (_lock)
            {
                // Remove existing entry with same path
                _workspaces.RemoveAll(w => w.FilePath.Equals(workspace.FilePath, StringComparison.OrdinalIgnoreCase));
                _workspaces.Add(workspace);
            }
        }

        public void Update(WorkspaceInfo workspace)
        {
            lock (_lock)
            {
                var existing = _workspaces.FirstOrDefault(w => w.FilePath.Equals(workspace.FilePath, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    _workspaces.Remove(existing);
                }
                _workspaces.Add(workspace);
            }
        }

        public void Remove(string filePath)
        {
            lock (_lock)
            {
                _workspaces.RemoveAll(w => w.FilePath.Equals(filePath, StringComparison.OrdinalIgnoreCase));
            }
        }

        public WorkspaceInfo? FindByPath(string filePath)
        {
            lock (_lock)
            {
                return _workspaces.FirstOrDefault(w => w.FilePath.Equals(filePath, StringComparison.OrdinalIgnoreCase));
            }
        }

        public void SaveChanges()
        {
            lock (_lock)
            {
                SaveToFile();
            }
        }

        private void LoadFromFile()
        {
            if (!File.Exists(_csvFilePath))
            {
                return;
            }

            try
            {
                using var reader = Sep.Reader().FromFile(_csvFilePath);
                foreach (var row in reader)
                {
                    var workspace = new WorkspaceInfo
                    {
                        Name = row["Name"].ToString(),
                        FilePath = row["FilePath"].ToString(),
                        LastModified = DateTime.Parse(row["LastModified"].ToString()),
                        LastOpened = DateTime.Parse(row["LastOpened"].ToString()),
                        ThumbnailPath = row["ThumbnailPath"].ToString(),
                        Description = row["Description"].ToString()
                    };

                    // Convert empty strings to null for optional fields
                    if (string.IsNullOrWhiteSpace(workspace.ThumbnailPath))
                        workspace.ThumbnailPath = null;
                    if (string.IsNullOrWhiteSpace(workspace.Description))
                        workspace.Description = null;

                    _workspaces.Add(workspace);
                }
            }
            catch (Exception ex)
            {
                // If CSV is corrupted, start fresh
                System.Diagnostics.Debug.WriteLine($"Failed to load workspaces CSV: {ex.Message}");
                _workspaces.Clear();
            }
        }

        private void SaveToFile()
        {
            try
            {
                using var writer = Sep.Writer().ToFile(_csvFilePath);
                using var writeRow = writer.NewRow();

                // Write header
                writeRow["Name"].Set("Name");
                writeRow["FilePath"].Set("FilePath");
                writeRow["LastModified"].Set("LastModified");
                writeRow["LastOpened"].Set("LastOpened");
                writeRow["ThumbnailPath"].Set("ThumbnailPath");
                writeRow["Description"].Set("Description");

                // Write data rows
                foreach (var workspace in _workspaces)
                {
                    writeRow["Name"].Set(workspace.Name);
                    writeRow["FilePath"].Set(workspace.FilePath);
                    writeRow["LastModified"].Set(workspace.LastModified.ToString("o")); // ISO 8601 format
                    writeRow["LastOpened"].Set(workspace.LastOpened.ToString("o"));
                    writeRow["ThumbnailPath"].Set(workspace.ThumbnailPath ?? string.Empty);
                    writeRow["Description"].Set(workspace.Description ?? string.Empty);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save workspaces CSV: {ex.Message}");
                throw;
            }
        }
    }
}
