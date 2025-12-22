# ImageAutomate

**ImageAutomate** is a modular, node-based image processing framework designed to build flexible execution pipelines. It separates logical graph definition from visualization and execution, enabling users to create complex image manipulation workflows visually or programmatically.

## 🏗 Architecture

The system follows a **Core-Periphery** architecture, ensuring that the core logic is decoupled from the user interface and execution details.

The solution is divided into the following key components:

*   **ImageAutomate.Core**: The central contract library defining the topology (`PipelineGraph`), data units (`WorkItem`, `Socket`), and interfaces (`IBlock`). It has no dependencies on UI frameworks.
*   **ImageAutomate.StandardBlocks**: A library of integrated standard image processing nodes (Resize, Crop, Blur, etc.) implemented using `SixLabors.ImageSharp`.
*   **ImageAutomate.UI**: The visualization layer responsible for rendering the graph using high-performance **GDI+** (System.Drawing).
*   **ImageAutomate.Execution**: The engine that validates the topology and orchestrates the dataflow.
*   **ImageAutomate**: The main WinForms application (Composition Root) that provides the visual editor and wires the components together.

### Execution Engine

The Execution Engine implements a **Pipes and Filters** architecture governed by a **Dataflow** model.

*   **Warehouses (Data Adapters)**: Act as buffers attached to the output of producer blocks. They manage data storage and implement "JIT Cloning" to optimize memory usage (transferring ownership for the last consumer, cloning for others).
*   **Barriers (Control Adapters)**: Lightweight control structures attached to consumer blocks. They track dependencies and signal the engine when a block is ready for execution.

This design effectively decouples blocks from one another, facilitating a robust flow where the Engine orchestrates execution based on data availability (Warehouses) and readiness signaling (Barriers).

## 🧩 Core Concepts

### Node-Based Workflow

ImageAutomate allows users to construct image processing pipelines by connecting nodes (Blocks). Each node represents an operation, and connections represent the flow of image data.

### IBlock

All processing nodes implement the `IBlock` interface. This defines the contract for inputs, outputs, configuration, and execution logic.

*   **Inputs/Outputs**: Defined via `Socket`s.
*   **Configuration**: Blocks expose metadata and configuration options.

### Visualization

The UI is powered by `GraphRenderPanel`, a custom WinForms control in the `ImageAutomate.UI` library. It features:
*   Manual layout system (drag-and-drop).
*   Custom GDI+ rendering for nodes and connections (Bezier curves).
*   Zoom and Pan capabilities.

## 📦 Standard Blocks

The project includes a suite of integrated standard blocks:

*   **I/O**: `LoadBlock`, `SaveBlock`.
*   **Transform**: `ResizeBlock`, `CropBlock`, `FlipBlock`.
*   **Effects**: `GaussianBlurBlock`, `SharpenBlock`, `PixelateBlock`, `HueBlock`, `SaturationBlock`, `ContrastBlock`, `VignetteBlock`.
*   **Format**: `ConvertBlock`.

## 🚀 Getting Started

### Prerequisites

*   .NET 9.0 SDK
*   Visual Studio 2022 (or compatible IDE)

### Building and Running

1.  Clone the repository.
2.  Open `ImageAutomate.sln`.
3.  Set the **ImageAutomate** project as the Startup Project.
4.  Build and Run the application.

### Usage

The main application window provides a menu bar and a canvas:

*   **File**: Create, Load, or Save graph files.
*   **Edit**: Add nodes or manage the graph.
*   **Plugins**: Manage and load external block plugins.

## 🔌 Extensibility

ImageAutomate is designed for extensibility. New blocks can be added via plugins.

1.  Create a Class Library referencing `ImageAutomate.Core`.
2.  Implement the `IBlock` interface.
3.  Compile and load the DLL via the application's Plugin Manager.

## ⚠️ Status

This project is currently in a **Prototype-Ready** state. The individual components (Core, Execution, UI, StandardBlocks) are functional, but the main application ensemble is under active development.
