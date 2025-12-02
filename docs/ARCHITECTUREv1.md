# ImageAutomate v1 Architecture

This document outlines the architecture for **ImageAutomate** v1. The solution is divided into 5 distinct projects to ensure separation of concerns, testability, and plugin support.

## 1\. Project Context & Objectives

From PoC to Production:  
The project is transitioning from a monolithic Proof-of-Concept (ConvertBlockPoC) to a production-ready (v1) architecture. The PoC successfully demonstrated the viability of MSAGL for layout and GDI+ for rendering, but it tightly coupled visualization logic with business logic, making the system inflexible.  

Key Architectural Details:

1. **Plugin Architecture:** Blocks must be distributable as independent DLLs that rely only on a stable Contract Library (Core).
2. **Separation of Concern:** The "Visual Graph" (MSAGL/UI) is strictly a view of the "Logical Graph" (Core). The Core does not know it is being drawn.

Execution Strategy (V1 Constraints):

- **Vertical Parallelism:** Data is processed in complete $G$ groups of $N$ batches at once. Batch $b$ flows through the entire pipeline before Batch $b+G\cdot{N}$ begins.
- **Single-Source:** Pipelines are restricted to a single Generator node (`LoadBlock`) to eliminate complex stream synchronization logic in the first release.
- **Directed Message Passing:** Data flows through explicitly defined `Socket`s ("Ports"), functioning like a directed UDP network where blocks push data to specific target sockets.

## 2\. System Overview

The system architecture is **Core-Periphery**. `ImageAutomate.Core` acts as the central contract that all other components depend on. The Execution Engine and UI are decoupled.

## 3\. Component Breakdown

### A. ImageAutomate.Core

- **Type:** Class Library \(.NET 9\)
- **Role**: The "Holy Scripture." Defines data structures, contracts, and topology.
- **Dependencies**: `SixLabors.ImageSharp`
- **Responsibilities:**
    - Defines the `IBlock` interface (Title, Content, Execution logic).
    - Defines data units (`WorkItem`, `Socket`).
    - Manages the logical topology via `PipelineGraph` (Pure C\# datastructure).
    - **No references to WinForms, WPF, or MSAGL**.
- **Key Types:**
    - `IBlock`: The contract for all nodes.
    - `WorkItem`: Container for `Image` and Metadata.
    - `PipelineGraph`: Manages `List<IBlock>` and `List<Connection>`.
    - `Socket`: Definition of input/output ports.
    - `Connection`: Record linking `(Source, Socket) -> (Target, Socket)`.

### B. ImageAutomate.UI

- **Type:** WinForms Control Library
- **Role**: The Visualization Layer. Adapts the Core graph into a visual representation.  
- **Dependencies**: `ImageAutomate.Core`, `Microsoft.Msagl`, `System.Windows.Forms`
- **Responsibilities:**
    - Renders the `PipelineGraph` using MSAGL and GDI+.
    - Handles user interaction (Pan, Zoom, Selection).
    - **Measurement Bridge:** Calculates the pixel size of blocks based on their content and font settings, then updates `IBlock.Width`/`IBlock.Height`.  
- **Key Types:**
    - `GraphRenderPanel`: The main canvas control.
    - `VisualGraphAdapter`: Synchronizes `Core.PipelineGraph` changes to the internal MSAGL `GeometryGraph`.
    - `NodeRenderer`: Handles custom GDI+ drawing of the "Card" style nodes.

### C. ImageAutomate.Execution

- **Type:** Class Library
- **Role**: The Workhorse. Implements the execution strategy.  
- **Dependencies**: `ImageAutomate.Core`
- **Responsibilities:**
    - Validates the `PipelineGraph` (Checks for single source, cycles).
    - Orchestrates the execution loop.
    - Manages batch lifecycle (`Generation -> Propagation -> Disposal`).
- **Key Types:**
    - `PipelineExecutor`: The main entry point for running a graph.

### D. ImageAutomate.StandardBlocks

- **Type:** Class Library
- **Role**: Built-in Block implementations.  
- **Dependencies**: ImageAutomate.Core, SixLabors.ImageSharp.Drawing
- **Responsibilities:**
    - Provides standard operations (Load, Save, Resize, Convert, Grayscale).
    - Serves as a reference implementation for 3rd-party plugins.
- **Key Types:**
    - `LoadBlock`: The Generator node. Reads files from disk.
    - `SaveBlock`: The Terminal node. Writes images to disk.
    - `ResizeBlock`, `ConvertBlock`, ...: Processing nodes.

### E. ImageAutomate.App

- **Type:** WinForms Application
- **Role**: Composition Root & Shell.  
- **Dependencies**: All Projects
- **Responsibilities:**
    - Bootstraps the application.
    - Layouts the main window (Toolbox | Canvas | PropertyGrid).
    - **Plugin Loading:** Scans StandardBlocks (and future DLLs) to populate the Toolbox.
    - Wires GraphRenderPanel events to the PropertyGrid.

## 4\. Key Data Flows

### Graph Construction

1. User **adds** "Resize" from Toolbox.
2. **App** instantiates `ResizeBlock` (from **StandardBlocks**).
3. **App** calls `PipelineGraph.AddBlock` (in **Core**).
4. **UI** listens to `OnBlockAdded`, creates a `GeomNode`, measures text, and renders it.

### Execution

1. User clicks "Run" in **App**.
2. **App** passes `PipelineGraph` to `PipelineExecutor` (in **Execution**).
3. **Execution** validates topology.
4. **Execution** triggers LoadBlock, pushes Batch 1 through graph, disposes, repeats.
