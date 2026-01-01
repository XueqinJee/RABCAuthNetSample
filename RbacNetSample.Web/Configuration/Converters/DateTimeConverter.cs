using System.Text.Json;

namespace RbacNetSample.Web.Configuration.Converters {
    public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTimeOffset> {

        public static readonly TimeZoneInfo ChinaStandardTime = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateTimeOffset.TryParse(reader.GetString(), out var date))
            {
                return date.ToUniversalTime();
            }
            return DateTimeOffset.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            
            writer.WriteStringValue(TimeZoneInfo.ConvertTime(value.ToUniversalTime(), ChinaStandardTime));
        }
    }
}
