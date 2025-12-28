namespace SunamoCollectionsValuesTableGrid._sunamo.SunamoInterfaces.Interfaces;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
internal interface IValuesTableGrid<T>
{
    bool IsAllInColumn(int columnIndex, T value);
    bool IsAllInRow(int rowIndex, T value);
    DataTable SwitchRowsAndColumn();
    DataTable ToDataTable();
}