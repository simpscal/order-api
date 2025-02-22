using System.Reflection;

namespace Order.Shared.Extensions;

public static class StringExtensions
{
    public static string UpperCaseFirstChar(this string input)
    {
        return string.IsNullOrEmpty(input) ? input : $"{char.ToUpper(input[0])}{input.Substring(1)}";
    }

    public static bool ExistsInConstant<T>(this string input)
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(field => field is { IsLiteral: true, IsInitOnly: false })
            .Select(field => field.GetValue(null)?.ToString())
            .Any(value => value == input);
    }
}