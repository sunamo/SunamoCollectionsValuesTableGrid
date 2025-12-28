// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoCollectionsValuesTableGrid._sunamo.SunamoCollectionsGeneric;

internal class CAG
{
    internal static int MinElementsItemsInnerList<T>(List<List<T>> lists)
    {
        var min = int.MaxValue;

        foreach (var item in lists)
            if (item.Count < min)
                min = item.Count;

        return min;
    }

    internal static int MaxElementsItemsInnerList<T>(List<List<T>> lists)
    {
        var max = 0;

        foreach (var item in lists)
            if (item.Count > max)
                max = item.Count;

        return max;
    }

    internal static int LowestCount<T>(List<List<T>> lists)
    {
        var min = int.MaxValue;

        foreach (var item in lists)
            if (min > item.Count)
                min = item.Count;

        return min;
    }

    internal static List<List<T>> TrimInnersToCount<T>(List<List<T>> lists, int maxCount)
    {
        for (var i = 0; i < lists.Count; i++) lists[i] = lists[i].Take(maxCount).ToList();

        return lists;
    }
}