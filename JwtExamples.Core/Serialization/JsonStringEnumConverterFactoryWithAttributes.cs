using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using JwtExamples.Core.Exceptions;

namespace JwtExamples.Core.Serialization;

public class JsonStringEnumConverterWithAttributes : JsonConverterFactory
{
    private static readonly ConcurrentDictionary<Type, JsonConverter> ConvertersCache = new();

    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsEnum;

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return ConvertersCache.GetOrAdd(typeToConvert, CreateEnumConverter);

        static JsonConverter CreateEnumConverter(Type enumType)
        {
            var converterType = typeof(EnumConverter<>).MakeGenericType(enumType);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }

    private class EnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        private static readonly IReadOnlyDictionary<string, TEnum> NameToEnumMap;
        private static readonly IReadOnlyDictionary<TEnum, string> EnumToNameMap;

        static EnumConverter()
        {
            var fields = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static);

            // Create map for Name -> EnumValue
            NameToEnumMap = fields
                .Select(field => new
                {
                    Name = field.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? field.Name,
                    Value = (TEnum)field.GetValue(null)!
                })
                .ToDictionary(x => x.Name, x => x.Value);

            // Reverse map for EnumValue -> Name
            EnumToNameMap = NameToEnumMap.ToDictionary(kv => kv.Value, kv => kv.Key);
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumValue = reader.GetString();

            if (enumValue != null && NameToEnumMap.TryGetValue(enumValue, out var result))
            {
                return result;
            }

            throw new SsoJsonException($"Unable to convert \"{enumValue}\" to enum \"{typeof(TEnum)}\".");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            if (EnumToNameMap.TryGetValue(value, out var name))
            {
                writer.WriteStringValue(name);
            }
            else
            {
                throw new SsoJsonException($"Unable to convert \"{value}\" to string for enum \"{typeof(TEnum)}\".");
            }
        }
    }
}