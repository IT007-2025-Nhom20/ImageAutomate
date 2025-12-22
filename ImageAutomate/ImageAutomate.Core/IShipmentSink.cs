namespace ImageAutomate.Core;

/// <summary>
/// Marker interface for blocks that consume work items as final output destinations.
/// </summary>
/// <remarks>
/// Sink blocks (typically SaveBlock) do not produce outputs and represent
/// the termination point of a processing pipeline.
/// 
/// A block implementing this interface must also implement IBlock.
/// 
/// This marker interface is used by GraphValidator to ensure
/// the pipeline has at least one valid sink block.
/// </remarks>
public interface IShipmentSink
{
    // Marker interface - no members required
}
