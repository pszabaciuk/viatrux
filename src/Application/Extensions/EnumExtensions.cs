namespace Application.Extensions;

public static class EnumExtensions
{
    public static string ToCommaSeparatedString<T>(this T enumValue) where T : Enum
    {
        return string.Join(", ", Enum.GetValues(typeof(T)).Cast<T>().Select(s => s.ToString()));
    }
}