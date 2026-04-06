namespace SunamoCollectionsValuesTableGrid._sunamo.SunamoExceptions;

/// <summary>
/// Helper class for throwing exceptions with detailed stack trace information.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws an exception if two collections have different element counts.
    /// </summary>
    /// <param name="firstCollectionName">Name of the first collection.</param>
    /// <param name="firstCollectionCount">Element count of the first collection.</param>
    /// <param name="secondCollectionName">Name of the second collection.</param>
    /// <param name="secondCollectionCount">Element count of the second collection.</param>
    /// <returns>True if an exception was thrown or would be thrown; otherwise, false.</returns>
    internal static bool DifferentCountInLists(string firstCollectionName, int firstCollectionCount, string secondCollectionName, int secondCollectionCount)
    {
        return ThrowIsNotNull(
            Exceptions.DifferentCountInLists(FullNameOfExecutedCode(), firstCollectionName, firstCollectionCount, secondCollectionName, secondCollectionCount));
    }


    #region Other
    /// <summary>
    /// Gets the full name (type.method) of the currently executed code.
    /// </summary>
    /// <returns>A string in the format "TypeFullName.MethodName".</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Constructs the full name (type.method) of the executed code from the given type and method name.
    /// </summary>
    /// <param name="type">The type information (can be Type, MethodBase, string, or any object).</param>
    /// <param name="methodName">The method name, or null to auto-detect from call stack.</param>
    /// <param name="isFromThrowEx">True if called from ThrowEx class (adjusts stack depth).</param>
    /// <returns>A string in the format "TypeFullName.MethodName".</returns>
    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type matchedType)
        {
            typeFullName = matchedType.FullName ?? "Type cannot be retrieved via type is Type";
        }
        else if (type is MethodBase methodBase)
        {
            typeFullName = methodBase.ReflectedType?.FullName ?? "Type cannot be retrieved via type is MethodBase";
            methodName = methodBase.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be retrieved via type is string";
        }
        else
        {
            Type resolvedType = type.GetType();
            typeFullName = resolvedType.FullName ?? "Type cannot be retrieved via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the provided exception message is not null.
    /// </summary>
    /// <param name="exception">The exception message to check.</param>
    /// <param name="isReallyThrowing">If true, actually throws the exception; if false, only breaks debugger.</param>
    /// <returns>True if an exception message was provided; otherwise, false.</returns>
    internal static bool ThrowIsNotNull(string? exception, bool isReallyThrowing = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }
    #endregion
}
