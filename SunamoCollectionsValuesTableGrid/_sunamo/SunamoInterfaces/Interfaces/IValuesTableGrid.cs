namespace SunamoCollectionsValuesTableGrid._sunamo.SunamoInterfaces.Interfaces;

/// <summary>
/// Defines methods for a two-dimensional table grid that stores values in parallel collections.
/// </summary>
/// <typeparam name="T">The type of elements in the table grid.</typeparam>
internal interface IValuesTableGrid<T>
{
    /// <summary>
    /// Checks if all elements in the specified column equal the given value.
    /// </summary>
    /// <param name="columnIndex">The zero-based index of the column to check.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>True if all elements in the column equal the value; otherwise, false.</returns>
    bool IsAllInColumn(int columnIndex, T value);

    /// <summary>
    /// Checks if all elements in the specified row equal the given value.
    /// </summary>
    /// <param name="rowIndex">The zero-based index of the row to check.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>True if all elements in the row equal the value; otherwise, false.</returns>
    bool IsAllInRow(int rowIndex, T value);

    /// <summary>
    /// Switches rows and columns to create a transposed DataTable.
    /// </summary>
    /// <returns>A DataTable with rows and columns switched.</returns>
    DataTable SwitchRowsAndColumn();

    /// <summary>
    /// Converts the grid data to a DataTable with captions as the first row.
    /// </summary>
    /// <returns>A DataTable representing the grid data.</returns>
    DataTable ToDataTable();
}
