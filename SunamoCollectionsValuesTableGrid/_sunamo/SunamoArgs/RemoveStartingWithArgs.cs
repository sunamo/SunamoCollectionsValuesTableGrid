namespace SunamoCollectionsValuesTableGrid._sunamo.SunamoArgs;

/// <summary>
/// Arguments for removing elements starting with specific patterns.
/// </summary>
internal class RemoveStartingWithArgs
{
    /// <summary>
    /// Gets or sets a value indicating whether to trim whitespace before finding the pattern.
    /// Original default value is false.
    /// </summary>
    internal bool IsTrimBeforeFinding { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the comparison is case sensitive.
    /// Original default value is true.
    /// </summary>
    internal bool IsCaseSensitive { get; set; } = true;
}