using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace estacionamento.Utils
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "dd/MM/yyyy-HH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();
            if (DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            throw new JsonException($"Cannot parse date: {dateString}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            string formattedDate = value.ToString(DateFormat, CultureInfo.InvariantCulture);
            writer.WriteStringValue(formattedDate);
        }
    }
}
