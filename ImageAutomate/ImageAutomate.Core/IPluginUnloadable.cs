namespace ImageAutomate.Core;

/// <summary>
/// Interface for plugin types that need to handle unload requests.
/// Implement this interface to participate in the plugin unloading process.
/// </summary>
public interface IPluginUnloadable
{
    /// <summary>
    /// Called when the plugin is about to be unloaded.
    /// </summary>
    /// <returns>
    /// True if the object accepts the unload request and has cleaned up its resources.
    /// False if the object rejects the unload request (e.g., work in progress).
    /// </returns>
    bool OnUnloadRequested();
}
