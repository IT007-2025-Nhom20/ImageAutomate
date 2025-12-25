# GraphRenderPanel

`GraphRenderPanel` is a high-performance, custom-drawn Windows Forms control responsible for visualizing and interacting with the `PipelineGraph`.

## Overview

*   **Custom Rendering**: Uses GDI+ to draw nodes, sockets, and bezier connections.
*   **Interaction**: Supports dragging nodes, creating connections, panning, and zooming.
*   **Layout Support**: Integrates with `Workspace` and `ViewState` to persist layout information.

## API Reference

### Properties

#### Data Binding
*   **`Workspace`** (`Workspace?`): The workspace containing the graph and view state.
*   **`Graph`** (`PipelineGraph?`): Read-only shortcut to `Workspace.Graph`.

#### Appearance
*   **`SelectedBlockOutlineColor`** (`Color`): Color of the selection border. Default: `Red`.
*   **`SocketRadius`** (`double`): Size of connection sockets. Default: `6`.
*   **`RenderScale`** (`float`): Current zoom level. Default: `1.0`.

#### Behavior
*   **`AllowOutOfScreenPan`** (`bool`): Whether the graph can be panned off-screen. Default: `false`.
*   **`AutoSnapZoneWidth`** (`float`): Width of the zone around sockets for easier connection snapping.

### Methods

#### Graph Manipulation
*   **`AddBlockAndConnect(...)`**: Adds blocks and creates a connection between them.
*   **`AddSuccessor(...)`**: Connects the selected block to a new downstream block.
*   **`AddPredecessor(...)`**: Connects a new upstream block to the selected block.
*   **`DeleteBlock(IBlock block)`**: Removes a block.
*   **`DeleteConnection(Connection connection)`**: Removes a connection.
*   **`DeleteSelectedItem()`**: Removes the currently selected block or connection.

#### View Control
*   **`GetViewportCenterWorld()`**: Returns the world coordinates of the viewport center.

## User Interaction

*   **Left Click**: Select node/edge.
*   **Left Drag (Node)**: Move node.
*   **Left Drag (Socket)**: Create connection.
*   **Right Drag**: Pan the view.
*   **Mouse Wheel**: Zoom in/out.
