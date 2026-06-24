namespace SunamoCollectionsValuesTableGrid;

public class ValuesTableGrid<T> : List<List<T>>
{
    private readonly List<List<T>> rows;

    public List<string> Captions { get; set; } = new List<string>();

    public ValuesTableGrid(List<List<T>> rows, bool isTrimToSmallest = true)
    {
        if (isTrimToSmallest)
        {
            var lowestCount = CAG.LowestCount(rows);
            rows = CAG.TrimInnersToCount(rows, lowestCount);
        }

        this.rows = rows;
    }

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

    public bool IsAllInColumn(int columnIndex, T value)
    {
        return rows[columnIndex].All(element => EqualityComparer<T>.Default.Equals(element, value));
    }

    public bool IsAllInRow(int rowIndex, T value)
    {
        var row = rows[rowIndex];
        foreach (var item in row)
            if (!EqualityComparer<T>.Default.Equals(item, value))
                return false;
        return true;
    }
}
