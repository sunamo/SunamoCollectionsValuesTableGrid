namespace SunamoCollectionsValuesTableGrid;

/// <summary>
/// Represents a two-dimensional table grid that allows querying parallel collections as one.
/// Similar class with two dimension array is UniqueTableInWhole.
/// Row - wrapper - files 2
/// Column - inner - apps 4
/// </summary>
/// <typeparam name="T">The type of elements in the table grid.</typeparam>
public class ValuesTableGrid<T> : List<List<T>>
{
    private readonly List<List<T>> rows;

    /// <summary>
    /// Gets or sets the column captions for the table grid.
    /// </summary>
    public List<string> Captions { get; set; } = new List<string>();

    /// <summary>
    /// Initializes a new instance of the ValuesTableGrid class.
    /// </summary>
    /// <param name="rows">The collection of rows to initialize the grid with.</param>
    /// <param name="isTrimToSmallest">If true, trims all rows to match the smallest row count.</param>
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
    /// Switches rows and columns to create a transposed DataTable.
    /// Must have initialized captions variable.
    /// All rows must be trimmed from \r \n.
    /// </summary>
    /// <returns>A DataTable with rows and columns switched.</returns>
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
                newRow[0] = caption == null ? string.Empty : caption;
                for (var j = 0; j < rows.Count; j++)
                    newRow[j + 1] = rows[j][i];
                newTable.Rows.Add(newRow);
            }
        }

        return newTable;
    }

    /// <summary>
    /// Converts the grid data to a DataTable with captions as the first row.
    /// </summary>
    /// <returns>A DataTable representing the grid data, or null if validation fails.</returns>
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
            var strings = new List<string>(item.Count);
            foreach (var element in item) strings.Add(element?.ToString() ?? string.Empty);
            dataTable.Rows.Add(strings);
        }

        return dataTable;
    }

    /// <summary>
    /// Checks if all elements in the specified column equal the given value.
    /// </summary>
    /// <param name="columnIndex">The zero-based index of the column to check.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>True if all elements in the column equal the value; otherwise, false.</returns>
    public bool IsAllInColumn(int columnIndex, T value)
    {
        return rows[columnIndex].All(element => EqualityComparer<T>.Default.Equals(element, value));
    }

    /// <summary>
    /// Checks if all elements in the specified row equal the given value.
    /// </summary>
    /// <param name="rowIndex">The zero-based index of the row to check.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>True if all elements in the row equal the value; otherwise, false.</returns>
    public bool IsAllInRow(int rowIndex, T value)
    {
        var list = rows[rowIndex];
        foreach (var item in list)
            if (!EqualityComparer<T>.Default.Equals(item, value))
                return false;
        return true;
    }
}
