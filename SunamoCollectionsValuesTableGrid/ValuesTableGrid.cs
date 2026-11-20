namespace SunamoCollectionsValuesTableGrid;

/// <summary>
/// Represents the values table grid t type.
/// </summary>
public class ValuesTableGrid<T> : List<List<T>>
{
    private readonly List<List<T>> rows;

    /// <summary>
    /// Gets or sets the captions.
    /// </summary>
    public List<string> Captions { get; set; } = new List<string>();

    /// <summary>
    /// Values table grid.
    /// </summary>
    /// <typeparam name="string">The type string.</typeparam>
    /// <returns>The values table grid result.</returns>
    public ValuesTableGrid(List<List<T>> rows, bool isTrimToSmallest = true)
    {
        if (isTrimToSmallest)
        {
            var lowestCount = CAG.LowestCount(rows);
            rows = CAG.TrimInnersToCount(rows, lowestCount);
        }

        this.rows = rows;
    }

    /// <summary>
    /// Switch rows and column.
    /// </summary>
    /// <returns>The switch rows and column result.</returns>
    public DataTable SwitchRowsAndColumn()
    {
        var newTable = new DataTable();
        if (rows.Count > 0)
        {
            newTable.Columns.Add(string.Empty);
            for (var i = 0; i < rows.Count; i++)
                newTable.Columns.Add();
            var firstRow = rows[0];
            for (var i = 0; i < firstRow.Count; i++)
            {
                var newRow = newTable.NewRow();
                var caption = Captions[i];
                newRow[0] = caption ?? string.Empty;
                for (var j = 0; j < rows.Count; j++)
                    newRow[j + 1] = rows[j][i];
                newTable.Rows.Add(newRow);
            }
        }

        return newTable;
    }

    /// <summary>
    /// To data table.
    /// </summary>
    /// <returns>The to data table result.</returns>
    public DataTable? ToDataTable()
    {
        var dataTable = new DataTable();
        var minCount = CAG.MinElementsItemsInnerList(rows);
        var maxCount = CAG.MaxElementsItemsInnerList(rows);
        var captionCount = Captions.Count;
        if (minCount != captionCount)
        {
            ThrowEx.DifferentCountInLists("minCount", minCount, "captionCount", captionCount);
            return null;
        }

        if (maxCount != captionCount)
        {
            ThrowEx.DifferentCountInLists("maxCount", maxCount, "captionCount", captionCount);
            return null;
        }

        for (var i = 0; i < captionCount; i++) dataTable.Columns.Add();
        var captionArray = Captions.ToArray();
        dataTable.Rows.Add(captionArray);
        foreach (var item in rows)
        {
            var rowValues = new List<string>(item.Count);
            foreach (var element in item) rowValues.Add(element?.ToString() ?? string.Empty);
            dataTable.Rows.Add(rowValues);
        }

        return dataTable;
    }

    /// <summary>
    /// Is all in column.
    /// </summary>
    /// <param name="columnIndex">The column index.</param>
    /// <param name="value">The value.</param>
    /// <returns>The is all in column result.</returns>
    public bool IsAllInColumn(int columnIndex, T value)
    {
        return rows[columnIndex].All(element => EqualityComparer<T>.Default.Equals(element, value));
    }

    /// <summary>
    /// Is all in row.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="value)">The value.</param>
    /// <returns>The is all in row result.</returns>
    public bool IsAllInRow(int rowIndex, T value)
    {
        var row = rows[rowIndex];
        foreach (var item in row)
            if (!EqualityComparer<T>.Default.Equals(item, value))
                return false;
        return true;
    }
}
