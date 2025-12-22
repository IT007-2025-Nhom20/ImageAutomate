namespace ImageAutomate.Core;

/// <summary>
/// Marker interface for blocks that produce work items in batches (shipments).
/// </summary>
/// <remarks>
/// Shipment sources (typically LoadBlock) execute multiple times, producing a limited
/// number of work items per execution to control memory pressure.
/// 
/// A block implementing this interface must also implement IBlock.
/// 
/// Execution flow:
/// 1. Executor calls Execute() on the block
/// 2. Block returns up to MaxShipmentSize work items
/// 3. If output count &lt; MaxShipmentSize, block is exhausted
/// 4. If output count == MaxShipmentSize, block may have more shipments
/// 5. Executor re-enqueues the block until exhausted
/// 
/// This is transparent to downstream blocks - they simply process whatever arrives.
/// </remarks>
public interface IShipmentSource
{
    /// <summary>
    /// Gets or sets the maximum number of work items to produce per execution.
    /// </summary>
    /// <remarks>
    /// This should be set by the executor during initialization.
    /// Default recommendation: 64 work items per shipment.
    /// </remarks>
    int MaxShipmentSize { get; set; }
}
