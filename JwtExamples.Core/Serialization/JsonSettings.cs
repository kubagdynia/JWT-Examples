using System.Text.Json;

namespace JwtExamples.Core.Serialization;

internal static class JsonSettings
{
    public static readonly JsonSerializerOptions JsonEnumOptions = new()
    {
        Converters =
        {
            //new JsonStringEnumConverterWithAttributes<UserStatus>()
            new JsonStringEnumConverterWithAttributes()
        }
    };
}