namespace Application.Extensions;

public static class StringExtensions
{
    public static string? NullIfEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value;
    }
}