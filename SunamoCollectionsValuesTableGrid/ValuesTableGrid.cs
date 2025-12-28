namespace SunamoCollectionsValuesTableGrid;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
///     Similar class with two dimension array is UniqueTableInWhole
///     Allow make query to parallel collections as be one
/// </summary>
/// <typeparam name="T"></typeparam>
public class ValuesTableGrid<T> : List<List<T>> //, IValuesTableGrid<T>
{
    /// <summary>
    ///     Row - wrapper - files 2
    ///     Column - inner - apps 4
    /// </summary>
    private readonly List<List<T>> _exists;

    public List<string> Captions { get; set; }

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
    ///     Must be initialized captions variable
    ///     All rows must be trimmed from \r \n
    /// </summary>
    public DataTable SwitchRowsAndColumn()
    {
        var newTable = new DataTable();
        if (_exists.Count > 0)
        {
            // Prvně přidám prázdný sloupec kde budou captions
            newTable.Columns.Add(string.Empty);
            // Můžu přidám sloupec pro B,C,D...
            for (var i = 0; i < _exists.Count; i++)
                newTable.Columns.Add();
            var firstRow = _exists[0];
            for (var i = 0; i < firstRow.Count; i++)
            {
                var newRow = newTable.NewRow();
                var caption = Captions[i]; //CA.GetIndex(Captions, i);
                newRow[0] = caption == null ? string.Empty : caption;
                for (var j = 0; j < _exists.Count; j++)
                    newRow[j + 1] = _exists[j][i];
                newTable.Rows.Add(newRow);
            }
        }

        return newTable;
    }

    public DataTable ToDataTable()
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
        //var t1 = captionArray.GetType();
        dataTable.Rows.Add(captionArray);
        foreach (var item in _exists)
        {
            var strings = new List<string>(item.Count);
            foreach (var element in item) strings.Add(element.ToString());
            //var ts2 = CA.ToListStringIList(item).ToArray();
            //var t2 = ts2.GetType();
            dataTable.Rows.Add(strings);
        }

        return dataTable;
    }

    public bool IsAllInColumn(int columnIndex, T value)
    {
        return _exists[columnIndex].All(element => EqualityComparer<T>.Default.Equals(element, value)); //CAG.IsAllTheSame<T>(value, );
    }

    public bool IsAllInRow(int rowIndex, T value)
    {
        var list = _exists[rowIndex];
        foreach (var item in list)
            if (!EqualityComparer<T>.Default.Equals(item, value))
                return false;
        return true;
    }
}