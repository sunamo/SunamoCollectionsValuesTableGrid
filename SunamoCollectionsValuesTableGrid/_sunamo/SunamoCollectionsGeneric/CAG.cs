namespace SunamoCollectionsValuesTableGrid._sunamo.SunamoCollectionsGeneric;

/// <summary>
/// Collection aggregate generic helper methods for working with nested lists.
/// </summary>
internal class CAG
{
    /// <summary>
    /// Finds the minimum number of elements among all inner lists.
    /// </summary>
    /// <typeparam name="T">The type of elements in the lists.</typeparam>
    /// <param name="lists">The collection of lists to analyze.</param>
    /// <returns>The minimum count of elements found in any inner list.</returns>
    internal static int MinElementsItemsInnerList<T>(List<List<T>> lists)
    {
        var min = int.MaxValue;

        foreach (var item in lists)
            if (item.Count < min)
                min = item.Count;

        return min;
    }

    /// <summary>
    /// Finds the maximum number of elements among all inner lists.
    /// </summary>
    /// <typeparam name="T">The type of elements in the lists.</typeparam>
    /// <param name="lists">The collection of lists to analyze.</param>
    /// <returns>The maximum count of elements found in any inner list.</returns>
    internal static int MaxElementsItemsInnerList<T>(List<List<T>> lists)
    {
        var max = 0;

        foreach (var item in lists)
            if (item.Count > max)
                max = item.Count;

        return max;
    }

    /// <summary>
    /// Finds the lowest count of elements among all inner lists.
    /// </summary>
    /// <typeparam name="T">The type of elements in the lists.</typeparam>
    /// <param name="lists">The collection of lists to analyze.</param>
    /// <returns>The lowest count of elements found in any inner list.</returns>
    internal static int LowestCount<T>(List<List<T>> lists)
    {
        var min = int.MaxValue;

        foreach (var item in lists)
            if (min > item.Count)
                min = item.Count;

        return min;
    }

    /// <summary>
    /// Trims all inner lists to a specified maximum count by taking only the first maxCount elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the lists.</typeparam>
    /// <param name="lists">The collection of lists to trim.</param>
    /// <param name="maxCount">The maximum number of elements to keep in each inner list.</param>
    /// <returns>The modified collection with trimmed inner lists.</returns>
    internal static List<List<T>> TrimInnersToCount<T>(List<List<T>> lists, int maxCount)
    {
        for (var i = 0; i < lists.Count; i++) lists[i] = lists[i].Take(maxCount).ToList();

        return lists;
    }
}