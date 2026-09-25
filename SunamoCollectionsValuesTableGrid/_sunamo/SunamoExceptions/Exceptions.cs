namespace SunamoCollectionsValuesTableGrid._sunamo.SunamoExceptions;

internal sealed class Exceptions
{
    internal static string FormatPrefix(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }

    internal static Tuple<string, string, string> PlaceOfException(bool isFillingFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceString = stackTrace.ToString();
        var lines = stackTraceString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        string type = string.Empty;
        string methodName = string.Empty;
        for (var lineIndex = 0; lineIndex < lines.Count; lineIndex++)
        {
            var line = lines[lineIndex];
            if (isFillingFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    isFillingFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    internal static void TypeAndMethodName(string line, out string type, out string methodName)
    {
        var trimmedLine = line.Split("at ")[1].Trim();
        var methodPath = trimmedLine.Split("(")[0];
        var parts = methodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase is null)
        {
            return "Method name cannot be retrieved";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    internal static string? DifferentCountInLists(string prefix, string firstCollectionName, int firstCollectionCount, string secondCollectionName, int secondCollectionCount)
    {
        if (firstCollectionCount != secondCollectionCount)
            return FormatPrefix(prefix) + " different count elements in collection" + " " +
            string.Concat(firstCollectionName + "-" + firstCollectionCount) + " vs. " +
            string.Concat(secondCollectionName + "-" + secondCollectionCount);
        return null;
    }
}
