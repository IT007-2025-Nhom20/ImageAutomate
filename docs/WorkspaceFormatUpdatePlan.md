# Development Plan: ZIP Format + Monaco Text Editor Integration

---

## Project Overview

**Objective:** Extend existing .NET application to support ZIP-based workspaces with dynamic schema merging and Monaco text editor integration.

**Current State:**
- Visual pipeline editor (GUI) - existing
- JSON workspace format - existing
- Static schema validation - existing

**Target State:**
- ZIP workspace format (workspace.zip with schemas/)
- Dynamic schema merging (master + plugins)
- Monaco text editor with intellisense
- Tabbed interface (visual + text views)
- Backward compatibility (JSON migration)

---

## Architecture Overview

```
┌────────────────────────────────────────────────────────────┐
│                    MainWindow                              │
├────────────────────────────────────────────────────────────┤
│  ┌─────────────────┐  ┌─────────────────────────────────┐  │
│  │ Visual Editor   │  │ Text Editor (Monaco WebView)    │  │
│  │ (Existing)      │  │ (New)                           │  │
│  └─────────────────┘  └─────────────────────────────────┘  │
└────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌────────────────────────────────────────────────────────────┐
│                  WorkspaceViewModel                        │
├────────────────────────────────────────────────────────────┤
│  • ZipWorkspace (storage)                                  │
│  • SchemaMerger (dynamic validation)                       │
│  • MasterSchemaLoader (URL fetching)                       │
│  • PluginSchemaManager (discovery + loading)               │
└────────────────────────────────────────────────────────────┘
```

---

## Phase 1: Foundation

### 1.1 ZIP Storage Layer

**Task 1.1.1: Create ZipWorkspace Class**
- Create `ZipWorkspace.cs` model class
- Implement `Load(string path)` method
- Implement `Save(string path)` method
- Add unit tests for ZIP operations


**Task 1.1.2: Define ZIP Structure**
```
workspace.zip
├── workspace.json
└── schemas/
    ├── plugins/
    │   ├── {blockType}.json
    │   └── ...
    └── optional-master-backup.json
```

**Task 1.1.3: Add JSON Migration Support**
- Create `WorkspaceLoader` factory
- Detect file extension (.json vs .zip)
- Create `ZipWorkspace` from legacy JSON files
- Add migration prompt logic

**Task 1.1.4: Create JsonSchema model class**

---

### 1.2 Schema System

**Task 1.2.1: Refactor to Dynamic Schema Merging**
- Extract existing static schema loading
- Create `SchemaMerger.cs` class
- Implement `Merge(JsonSchema master, IEnumerable<JsonSchema> plugins)`
- Add unit tests for merge logic

**Task 1.2.2: Master Schema Loader**

**Task 1.2.3: Plugin Schema Manager**

---

### 1.3 Integrate ZipWorkspace into main app

**Task 1.3.1: Replaces existing workspace loading and saving with new ZipWorkspace model**

---

## Phase 2: Monaco Text Editor

### 2.1 WebView2 Integration

**Task 2.1.1: Add WebView2 SDK**
- Install `Microsoft.Web.WebView2` NuGet package
- Add WebView2 control to project
- Configure WebView2 environment

**Task 2.1.2: Create TextEditorControl**
- Create `TextEditorControl.cs`
- Initialize WebView2 in constructor
- Handle `CoreWebView2InitializationCompleted`
- Implement resource request handler

**Task 2.1.3: Monaco HTML Asset**
- Create `MonacoEditor.html` embedded resource
- Include Monaco from CDN (or bundle locally)
- Configure editor settings (theme, font, options)

```html
<!-- MonacoEditor.html -->
<!DOCTYPE html>
<html>
<head>
    <script src="vs/loader.min.js"></script>
    <style>
        html, body, #editor { height: 100%; margin: 0; }
    </style>
</head>
<body>
    <div id="editor"></div>
    <script>
        require(['vs/editor/editor.main'], function() {
            window.editor = monaco.editor.create(document.getElementById('editor'), {
                value: '// Loading...',
                language: 'json',
                theme: 'vs-dark',
                automaticLayout: true,
                minimap: { enabled: false },
                fontSize: 14,
                wordWrap: 'on',
                lineNumbers: 'on',
                scrollBeyondLastLine: false
            });
        });
    </script>
</body>
</html>
```

---

### 2.2 Script Bridge

**Task 2.2.1: Editor JavaScript API**
- Create JavaScript functions for Monaco control:
  - `setEditorValue(json, schema)`
  - `getEditorValue()`
  - `setSchema(schema)`
  - `validate()`
  - `format()`
  - `minify()`

**Task 2.2.2: C# to JavaScript Bridge**
```csharp
public class MonacoEditorBridge
{
    private WebView2 webView;
    
    public async Task SetValue(string json, JsonSchema schema);
    public async Task<string> GetValue();
    public async Task SetSchema(JsonSchema schema);
    public async Task FormatDocument();
    public async Task Validate();
}
```

**Task 2.2.3: Error Display Integration**
- Capture Monaco validation errors
- Display in error list panel
- Navigate to error locations

---

### 2.3 Editor Integration

**Task 2.3.1: Connect TextEditorControl to existing EditorView**
- Change `EditorView` main layout to tabbed layout
- Move visual editor `GraphRenderPanel`, `PropertyGrid` and `Toolbox` to `VisualEditorView`
- Connect `TextEditorControl` to text editor tab

**Task 2.3.2: Sync Between Views**
- Visual → Text: Update Monaco JSON on visual changes
- Text → Visual: Update visual graph on Monaco edits
- Debounce to prevent excessive updates
- Show change indicator when out of sync

---

## Phase 3: Operational Integration

### 3.1 File Operations

**Task 3.1.1: File Open Handler**
- Filter: `Workspace Files (*.workspace.zip)|*.workspace.zip|JSON Files (*.json)|*.json`
- Auto-detect format
- Update window title with filename

**Task 3.1.2: File Save Handler**
- Check if format changed (JSON → ZIP)
- Prompt for migration if needed
- Update window title
- Clear unsaved indicator

**Task 3.1.3: Export JSON (Debug)**
- Export `workspace.json` only
- Save as standalone JSON
- Use for debugging/testing

---

### 3.2 Validation System

**Task 3.2.1: Create ValidationEngine**
```csharp
public class ValidationEngine
{
    public ValidationResult Validate(WorkspaceData workspace, JsonSchema schema);
}

public class ValidationResult
{
    public bool IsValid { get; }
    public List<ValidationError> Errors { get; }
    public List<ValidationWarning> Warnings { get; }
}
```

**Task 3.2.2: Display Validation Results**
- Create `ValidationErrorList.xaml`
- Show errors in list
- Double-click to navigate to location
- Color-code severity (error/warning)

**Task 3.2.3: Auto-Validation**
- Validate on file load
- Validate on save
- Validate on tab switch (optional)
- Add manual validation command

---

## Phase 4: Polish & Testing

### 4.1 Plugin System

**Task 4.1.1: Plugin Schema Format Documentation**
- Define JSON schema format for plugins
- Create example plugin schema
- Document plugin installation process

**Task 4.1.2: Plugin Schema Auto-Loading**
- Scan plugins directory
- Load plugin schemas from assembly metadata
- Add to `ZipWorkspace.PluginSchemas`
- Update `WorkspaceViewModel` with new schemas

**Task 4.1.3: Plugin Discovery UI**
- Add "Manage Plugins" dialog
- List installed plugins
- Show plugin schemas
- Enable/disable plugins

---

### 4.2 Error Handling

**Task 4.2.1: Graceful Degradation**
- Handle missing master schema (use backup from ZIP)
- Handle corrupted ZIP files (show error, don't crash)
- Handle invalid JSON (show Monaco errors)
- Handle schema merge conflicts (log warnings)

**Task 4.2.2: User-Friendly Error Messages**
- Convert technical errors to user-friendly messages
- Add error icons
- Add "View Details" for technical information
- Add "Help" links

---

### 4.3 Performance Optimization

**Task 4.3.1: Lazy Loading**
- Delay schema merging until needed
- Load Monaco on-demand (when text tab opened)
- Cache merged schemas

**Task 4.3.2: Debounce Operations**
- Debounce text → visual sync (500ms)
- Debounce validation (1s)
- Prevent excessive file saves

**Task 4.3.3: Reduce Memory Footprint**
- Dispose ZIP archives properly
- Dispose WebView2 properly
- Clear unused cached data

---

### 4.4 Testing

**Task 4.4.1: Unit Tests**
- `ZipWorkspaceTests.cs`
  - Load ZIP with valid structure
  - Load ZIP with missing schemas
  - Save ZIP with plugins
  - JSON to ZIP migration
- `SchemaMergerTests.cs`
  - Merge with no plugins
  - Merge with multiple plugins
  - Handle conflicting definitions
- `ValidationEngineTests.cs`
  - Valid workspace
  - Invalid workspace (missing fields)
  - Invalid workspace (wrong types)

**Task 4.4.2: Integration Tests**
- Open → Edit → Save cycle (JSON)
- Open → Edit → Save cycle (ZIP)
- Switch between views (sync validation)
- Load workspace with plugins
- Export JSON

**Task 4.4.3: Manual Testing**
- Test with real workspaces
- Test with corrupted files
- Test performance with large workspaces
- Test edge cases (empty, single block, 20+ blocks)

---

## Phase 5: Documentation & Release

### 5.1 Documentation

**Task 5.1.1: User Documentation**
- README.md overview
- Workspace file format
- Plugin development guide
- Troubleshooting guide

**Task 5.1.2: Developer Documentation**
- Architecture overview
- Class diagrams
- Extension points

**Task 5.1.3: Release Notes**
- List new features
- List breaking changes
- Migration guide

---

### 5.2 Release Preparation

**Task 5.2.1: Version Bump**
- Update AssemblyVersion
- Update AssemblyFileVersion
- Update NuGet package version (if applicable)

**Task 5.2.2: Build Release**
- Configure Release build
- Create installer (if applicable)
- Test installer

**Task 5.2.3: Migration Script**
- Create PowerShell script for bulk JSON → ZIP migration
- Create batch script for Windows
- Document usage

---

## Summary Timeline

| Phase                     | Status      |
|---------------------------|-------------|
| Phase 1: Foundation       | Not Started |
| Phase 2: Monaco Editor    | Not Started |
| Phase 3: UI Integration   | Not Started |
| Phase 4: Polish & Testing | Not Started |
| Phase 5: Documentation    | Not Started |

---

## Risk Mitigation

| Risk                       | Probability | Impact   | Mitigation                              |
|----------------------------|-------------|----------|-----------------------------------------|
| WebView2 not installed     | Medium      | High     | Bundle WebView2 runtime in installer    |
| CDN for Monaco unavailable | Low         | Medium   | Bundle Monaco locally as fallback       |
| Schema merge conflicts     | Medium      | Medium   | Log warnings, user can edit manually    |
| Performance issues         | Low         | High     | Implement lazy loading, caching         |
| Migration data loss        | Low         | Critical | Backup before migration, confirm prompt |

---

## Success Criteria

**MVP:**
- [ ] Can load/save `.workspace.zip` files
- [ ] Can open legacy `.json` files (with migration prompt)
- [ ] Text editor view with Monaco works
- [ ] Intellisense works for dynamic schemas
- [ ] Validation works with plugin schemas
- [ ] Can switch between visual/text views

**Stretch Goals:**
- [ ] Plugin discovery UI
- [ ] Export JSON (debug)
- [ ] Bulk migration script
- [ ] Offline Monaco bundle

---

## Next Steps

**Immediate Actions:**
1. Set up new branch `feature/zip-format`
2. Install WebView2 SDK
3. Create `ZipWorkspace` class
4. Test ZIP operations

**Prerequisites:**
- Visual Studio with .NET SDK
- WebView2 runtime (or include in installer)
- Test workspaces (existing JSON files)