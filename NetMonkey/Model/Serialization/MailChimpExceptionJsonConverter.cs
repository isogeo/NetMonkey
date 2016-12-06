using System;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetMonkey.Model.Serialization
{

    /// <summary>Converts a <see cref="MailChimpException" /> to JSON.</summary>
    public class MailChimpExceptionJsonConverter:
        JsonConverter
    {

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">The type of the object.</param>
        /// <returns><c>true</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(MailChimpException).IsAssignableFrom(objectType);
        }

        /// <summary>Reads the JSON representation of the object. </summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">The type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Debug.Assert(reader!=null);
            if (reader==null)
                throw new ArgumentNullException("reader");
            Debug.Assert(serializer!=null);
            if (serializer==null)
                throw new ArgumentNullException("serializer");

            if (reader.TokenType==JsonToken.Null)
                return null;

            if (reader.TokenType!=JsonToken.StartObject)
                throw SerializationUtils.CreateJsonException(reader, JsonToken.StartObject);

            MailChimpException ret = null;

            var jobj = (JObject)JToken.ReadFrom(reader);
            var message=jobj.GetValue("detail", StringComparison.Ordinal);
            if (message!=null)
                ret=new MailChimpException(message.Value<string>());
            else
                ret=new MailChimpException();

            serializer.Populate(jobj.CreateReader(), ret);

            return ret;
        }

        /// <summary>Writes the JSON representation of the object. </summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Debug.Assert(writer!=null);
            if (writer==null)
                throw new ArgumentNullException("writer");

            if (value==null)
            {
                writer.WriteNull();
                return;
            }

            var mcex=value as MailChimpException;
            if (mcex!=null)
            {
                writer.WriteStartObject();
                if (!string.IsNullOrEmpty(mcex.Kind))
                {
                    writer.WritePropertyName("type");
                    writer.WriteValue(mcex.Kind);
                }
                if (!string.IsNullOrEmpty(mcex.Title))
                {
                    writer.WritePropertyName("title");
                    writer.WriteValue(mcex.Title);
                }
                writer.WritePropertyName("status");
                writer.WriteValue((int)mcex.StatusCode);
                if (!string.IsNullOrEmpty(mcex.Message))
                {
                    writer.WritePropertyName("detail");
                    writer.WriteValue(mcex.Message);
                }
                if (!string.IsNullOrEmpty(mcex.Instance))
                {
                    writer.WritePropertyName("instance");
                    writer.WriteValue(mcex.Instance);
                }
                writer.WriteEndObject();
                return;
            }

            throw new JsonSerializationException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    SR.InvalidJsonException,
                    typeof(MailChimpException),
                    value.GetType()
                )
            );
        }
    }
}
