using System.Text.Json;
using System.Text.Json.Serialization;

namespace InstaCommon.JsonConverters
{
    public class ExceptionConverter : JsonConverter<Exception>
    {
        public override Exception Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            
            // Add any other propoerties that you may want to include in your JSON.
            // ...
            WriteFields(writer, value);

            writer.WriteEndObject();
        }

        private void WriteFields(Utf8JsonWriter writer, Exception value)
        {
            writer.WriteString("Message", value.Message);
            writer.WriteString("StackTrace", value.StackTrace);

            if (value.InnerException != null)
                WriteFields(writer, value.InnerException);
        }
    }
}
