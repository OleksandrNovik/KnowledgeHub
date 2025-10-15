namespace KnowledgeHub.Infrastructure.Repositories.Enum;

/// <summary>
///     Behavior for read operations of entities in repositorie
/// </summary>
public enum TrackingBehavior
{
    /// <summary>
    ///     Track no changes for entities that were read by current operation
    ///     Should be used for more performance, when entities will not be changed
    /// </summary>
    NoTracking,

    /// <summary>
    ///     Track all changes in the entities
    ///     Should be used when entities will be changed at some point
    /// </summary>
    Track
}