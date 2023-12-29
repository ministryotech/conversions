namespace Ministry;

/// <summary>
/// A static class of methods to convert types which can be used directly or as extensions.
/// </summary>
[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "Library")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Library")]
[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Library")]
public static class Conversions
{
    /// <summary>
    /// Converts a boolean to an integer value of a specified type.
    /// </summary>
    /// <param name="value">The boolean value to convert from.</param>
    /// <param name="trueValue">The true value.</param>
    /// <param name="falseValue">The false value.</param>
    /// <returns>
    /// The appropriate integer value.
    /// </returns>
    public static int ToInt32(this bool value, int trueValue = 1, int falseValue = 0) 
        => value ? trueValue : falseValue;

    /// <summary>
    /// Converts a boolean to an integer value of a specified type.
    /// </summary>
    /// <param name="value">The boolean value to convert from.</param>
    /// <param name="trueValue">The integer value to return if the boolean value is true.</param>
    /// <param name="falseValue">The false value.</param>
    /// <returns>
    /// The appropriate integer value.
    /// </returns>
    public static int? ToInt32(this bool? value, int trueValue = 1, int falseValue = 0) 
        => value?.ToInt32(trueValue, falseValue);

    /// <summary>
    /// Converts a boolean to a string value of a specified type.
    /// </summary>
    /// <param name="value">The boolean value to convert from.</param>
    /// <param name="stringType">Type of the string.</param>
    /// <returns>
    /// The appropriate string value.
    /// </returns>
    /// <remarks>
    /// To convert to 1/0 call ToInt().
    /// </remarks>
    public static string ToString(this bool value, BooleanToStringConversionType stringType)
        => FixedBooleanToStringMap.TryGetValue(stringType, out var stringValue)
            ? value 
                ? stringValue.TrueValue
                : stringValue.FalseValue
            : string.Empty;

    /// <summary>
    /// Converts a boolean to a string value of a specified type.
    /// </summary>
    /// <param name="value">The boolean value to convert from.</param>
    /// <param name="stringType">Type of the string.</param>
    /// <returns>
    /// The appropriate string value.
    /// </returns>
    /// <remarks>
    /// To convert to 1/0 call ToInt().
    /// </remarks>
    public static string ToString(this bool? value, BooleanToStringConversionType stringType) 
        => value.HasValue ? value.Value.ToString(stringType) : string.Empty;

    /// <summary>
    /// Converts an integer to it's boolean value.
    /// </summary>
    /// <param name="value">The integer to convert from.</param>
    /// <returns>
    /// The appropriate boolean value.
    /// </returns>
    /// <remarks>
    /// Converts 0 False, all else True.
    /// </remarks>
    public static bool ToBoolean(this int value) => value > 0;

    /// <summary>
    /// Converts a string to it's boolean value.
    /// </summary>
    /// <param name="value">The string to convert from.</param>
    /// <returns>
    /// The appropriate boolean value.
    /// </returns>
    /// <remarks>
    /// Converts 1/0, True/False, Yes/No, etc. Returns nothing otherwise.
    /// </remarks>
    /// <exception cref="System.InvalidCastException">Thrown if the string cannot be transformed into a boolean value.</exception>
    public static bool ToBoolean(this string value)
    {
        switch (value.Trim().ToUpper())
        {
            case TrueValues.Bit:
            case TrueValues.YesUpper:
            case TrueValues.TrueUpper:
            case TrueValues.Y:
                return true;
            case FalseValues.Bit:
            case FalseValues.Negative:
            case FalseValues.NoUpper:
            case FalseValues.FalseUpper:
            case FalseValues.N:
                return false;
            default:
                throw new InvalidCastException("The object passed cannot be converted");
        }
    }
    
    #region | Private Methods |

    /// <summary>
    /// Represents a fixed map of boolean values to corresponding string representations.
    /// </summary>
    private static readonly Dictionary<BooleanToStringConversionType, (string TrueValue, string FalseValue)
        > FixedBooleanToStringMap
            = new()
            {
                [BooleanToStringConversionType.TrueOrFalse] = (TrueValues.True, FalseValues.False),
                [BooleanToStringConversionType.YesOrNo] = (TrueValues.Yes, FalseValues.No),
                [BooleanToStringConversionType.YOrN] = (TrueValues.Y, FalseValues.N)
            };
    
    #endregion
}