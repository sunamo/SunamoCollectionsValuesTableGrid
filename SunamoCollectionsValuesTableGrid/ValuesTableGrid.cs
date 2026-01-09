// variables names: ok
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
    private readonly List<List<T>> _exists;

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
            var lowest = CAG.LowestCount(rows);
            rows = CAG.TrimInnersToCount(rows, lowest);
        }

        _exists = rows;
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
        if (_exists.Count > 0)
        {
            newTable.Columns.Add(string.Empty);
            for (var i = 0; i < _exists.Count; i++)
                newTable.Columns.Add();
            var firstRow = _exists[0];
            for (var i = 0; i < firstRow.Count; i++)
            {
                var newRow = newTable.NewRow();
                var caption = Captions[i];
                newRow[0] = caption == null ? string.Empty : caption;
                for (var j = 0; j < _exists.Count; j++)
                    newRow[j + 1] = _exists[j][i];
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
        var min = CAG.MinElementsItemsInnerList(_exists);
        var max = CAG.MaxElementsItemsInnerList(_exists);
        var captionCount = Captions.Count;
        if (min != captionCount)
        {
            ThrowEx.DifferentCountInLists("min", min, "captionCount", captionCount);
            return null;
        }

        if (max != captionCount)
        {
            ThrowEx.DifferentCountInLists("max", max, "captionCount", captionCount);
            return null;
        }

        for (var i = 0; i < captionCount; i++) dataTable.Columns.Add();
        var captionArray = Captions.ToArray();
        dataTable.Rows.Add(captionArray);
        foreach (var item in _exists)
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
        return _exists[columnIndex].All(element => EqualityComparer<T>.Default.Equals(element, value));
    }

    /// <summary>
    /// Checks if all elements in the specified row equal the given value.
    /// </summary>
    /// <param name="rowIndex">The zero-based index of the row to check.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>True if all elements in the row equal the value; otherwise, false.</returns>
    public bool IsAllInRow(int rowIndex, T value)
    {
        var list = _exists[rowIndex];
        foreach (var item in list)
            if (!EqualityComparer<T>.Default.Equals(item, value))
                return false;
        return true;
    }
}