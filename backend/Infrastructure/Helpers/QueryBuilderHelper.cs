namespace Infrastructure.Helpers;

using Infrastructure.Attributes;
using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

public static class QueryBuilder
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyCache = new();

    public static Dictionary<string, string> ToDictionary<T>(T request, JsonNamingPolicy? namingPolicy = null)
    {
        ArgumentNullException.ThrowIfNull(request);

        var properties = _propertyCache.GetOrAdd(typeof(T), t => t.GetProperties());
        var result = new Dictionary<string, string>(properties.Length);

        foreach (var prop in properties)
        {
            if (prop.GetCustomAttribute<IgnoreQueryAttribute>() is not null) continue;

            var value = prop.GetValue(request);
            if (value is null) continue;

            var name        = ResolveParameterName(prop, namingPolicy);
            var stringValue = FormatValue(prop, value);

            result[name] = stringValue;
        }

        return result;
    }

    public static string BuildUrl(string baseUrl, Dictionary<string, string> queryParams)
    {
        var queryString = string.Join("&", queryParams.Select(p =>
            $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value)}"));

        return $"{baseUrl}?{queryString}";
    }

    public static string Build<T>(T request, JsonNamingPolicy? namingPolicy = null)
    {
        ArgumentNullException.ThrowIfNull(request);

        var properties = _propertyCache.GetOrAdd(typeof(T), t => t.GetProperties());
        var query = new List<string>(properties.Length);

        foreach (var prop in properties)
        {
            if (prop.GetCustomAttribute<IgnoreQueryAttribute>() is not null) continue;

            var value = prop.GetValue(request);
            if (value is null) continue;

            var name        = ResolveParameterName(prop, namingPolicy);
            var stringValue = FormatValue(prop, value);

            query.Add($"{HttpUtility.UrlEncode(name)}={HttpUtility.UrlEncode(stringValue)}");
        }

        return string.Join("&", query);
    }

    private static string ResolveParameterName(PropertyInfo prop, JsonNamingPolicy? namingPolicy)
    {
        if (IsDateType(prop.PropertyType))
            return "date";

        if (prop.GetCustomAttribute<JsonPropertyNameAttribute>() is { Name: var jsonName })
            return jsonName;

        if (namingPolicy is not null)
            return namingPolicy.ConvertName(prop.Name);

        return prop.Name;
    }

    private static string FormatValue(PropertyInfo prop, object value)
    {
        if (prop.GetCustomAttribute<PreserveFormatAttribute>() is not null)
            return value.ToString()!;

        if (IsDateType(prop.PropertyType))
            return FormatDate(value);

        return value.ToString()!;
    }

    private static string FormatDate(object value)
    {
        if (value is DateTime dt)
            return dt.ToString("o", CultureInfo.InvariantCulture);

        if (value is DateOnly dateOnly)
            return dateOnly.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        if (value is DateTimeOffset dto)
            return dto.ToString("o", CultureInfo.InvariantCulture);

        return value.ToString()!;
    }

    private static bool IsDateType(Type type)
    {
        var underlying = Nullable.GetUnderlyingType(type) ?? type;

        return underlying == typeof(DateTime)
            || underlying == typeof(DateOnly)
            || underlying == typeof(DateTimeOffset);
    }
}
