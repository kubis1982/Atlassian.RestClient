using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kubis1982.Atlassian.RestClient
{
    /// <summary>
    /// Custom JSON converter for DateTime to match Jira's expected format: 2026-04-18T09:04:00.1182+0000
    /// </summary>
    internal class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (string.IsNullOrEmpty(dateString))
            {
                return default;
            }

            // Jira format: 2026-04-18T09:04:00.1182+0000
            if (DateTimeOffset.TryParseExact(dateString, 
                ["yyyy-MM-ddTHH:mm:ss.ffffzzz", "yyyy-MM-ddTHH:mm:ss.fffzzz", "yyyy-MM-ddTHH:mm:ss.ffzzz", "yyyy-MM-ddTHH:mm:ss.fzzz", "yyyy-MM-ddTHH:mm:sszzz"], 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out var date))
            {
                return date;
            }

            // Fallback to standard parsing
            return DateTimeOffset.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            // Convert to UTC if not already
            var utcDate = value.ToUniversalTime();
            
            // Format: yyyy-MM-ddTHH:mm:ss.ffff+0000
            // Maximum 4 digits for milliseconds, timezone without colon
            var milliseconds = utcDate.Millisecond;
            var formattedDate = utcDate.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            
            if (milliseconds > 0)
            {
                // Format milliseconds with max 4 digits, removing trailing zeros
                var ms = milliseconds.ToString("0000", CultureInfo.InvariantCulture).TrimEnd('0');
                if (!string.IsNullOrEmpty(ms))
                {
                    formattedDate += "." + ms;
                }
            }
            
            formattedDate += "+0000";
            
            writer.WriteStringValue(formattedDate);
        }
    }

    /// <summary>
    /// Custom JSON converter for nullable DateTimeOffset
    /// </summary>
    internal class NullableDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
    {
        private readonly DateTimeOffsetConverter _baseConverter = new();

        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            return _baseConverter.Read(ref reader, typeof(DateTimeOffset), options);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNullValue();
                return;
            }

            _baseConverter.Write(writer, value.Value, options);
        }
    }
}
