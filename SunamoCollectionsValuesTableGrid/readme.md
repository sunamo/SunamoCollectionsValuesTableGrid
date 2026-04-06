# SunamoCollectionsValuesTableGrid

A generic two-dimensional table grid for .NET that allows querying parallel collections as one. Supports exporting to `DataTable`, transposing rows into columns and vice versa, and checking if all rows or columns contain the same value.

## Features

- **ValuesTableGrid&lt;T&gt;** — generic class inheriting from `List<List<T>>`
- **SwitchRowsAndColumn()** — transposes the grid into a `DataTable` (rows become columns)
- **ToDataTable()** — converts the grid to a `DataTable` with captions as the first row
- **IsAllInColumn() / IsAllInRow()** — checks whether all elements in a given column or row equal a specified value

## Installation

```bash
dotnet add package SunamoCollectionsValuesTableGrid
```

## Target Frameworks

`net10.0`, `net9.0`, `net8.0`

## Dependencies

- **Microsoft.Extensions.Logging.Abstractions**

## Links

- [NuGet](https://www.nuget.org/profiles/sunamo)
- [GitHub](https://github.com/sunamo/PlatformIndependentNuGetPackages)
- [Developer site](https://sunamo.cz)

## License

MIT
