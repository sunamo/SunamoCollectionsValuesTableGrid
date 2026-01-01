namespace SunamoCollectionsValuesTableGrid._sunamo.SunamoExceptions;

/// <summary>
/// Helper class for creating exception messages and extracting stack trace information.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// Checks and formats a prefix string for exception messages.
    /// </summary>
    /// <param name="before">The prefix string to check.</param>
    /// <returns>An empty string if the prefix is null or whitespace; otherwise, the prefix followed by ": ".</returns>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Extracts information about where an exception occurred from the current stack trace.
    /// </summary>
    /// <param name="fillAlsoFirstTwo">If true, also fills type and method name from the first non-ThrowEx frame.</param>
    /// <returns>A tuple containing the type name, method name, and formatted stack trace string.</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool fillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceString = stackTrace.ToString();
        var lines = stackTraceString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var line = lines[i];
            if (fillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    fillAlsoFirstTwo = false;
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

    /// <summary>
    /// Extracts the type name and method name from a stack trace line.
    /// </summary>
    /// <param name="line">The stack trace line to parse.</param>
    /// <param name="type">Output parameter for the extracted type name.</param>
    /// <param name="methodName">Output parameter for the extracted method name.</param>
    internal static void TypeAndMethodName(string line, out string type, out string methodName)
    {
        var trimmedLine = line.Split("at ")[1].Trim();
        var methodPath = trimmedLine.Split("(")[0];
        var parts = methodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified depth in the call stack.
    /// </summary>
    /// <param name="depth">The depth in the call stack (1 = immediate caller).</param>
    /// <returns>The name of the calling method, or an error message if it cannot be retrieved.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    /// <summary>
    /// StringBuilder for storing additional inner information about exceptions.
    /// </summary>
    internal readonly static StringBuilder AdditionalInfoInnerStringBuilder = new();

    /// <summary>
    /// StringBuilder for storing additional information about exceptions.
    /// </summary>
    internal readonly static StringBuilder AdditionalInfoStringBuilder = new();
    #endregion

    /// <summary>
    /// Creates an error message for when two collections have different element counts.
    /// </summary>
    /// <param name="before">Prefix text for the error message.</param>
    /// <param name="firstCollectionName">Name of the first collection.</param>
    /// <param name="firstCollectionCount">Element count of the first collection.</param>
    /// <param name="secondCollectionName">Name of the second collection.</param>
    /// <param name="secondCollectionCount">Element count of the second collection.</param>
    /// <returns>An error message string if counts differ; otherwise, null.</returns>
    internal static string? DifferentCountInLists(string before, string firstCollectionName, int firstCollectionCount, string secondCollectionName, int secondCollectionCount)
    {
        if (firstCollectionCount != secondCollectionCount)
            return CheckBefore(before) + " different count elements in collection" + " " +
            string.Concat(firstCollectionName + "-" + firstCollectionCount) + " vs. " +
            string.Concat(secondCollectionName + "-" + secondCollectionCount);
        return null;
    }
}
