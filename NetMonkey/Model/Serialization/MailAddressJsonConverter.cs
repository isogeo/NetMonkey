using System;
using System.Globalization;
using System.Net.Mail;
using Newtonsoft.Json;
using Common.Logging;

namespace NetMonkey.Model.Serialization
{

    /// <summary>Converts a <see cref="MailAddress" /> to JSON.</summary>
    public class MailAddressJsonConverter:
        JsonConverter
    {

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">The type of the object.</param>
        /// <returns><c>true</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(MailAddress).IsAssignableFrom(objectType);
        }

        /// <summary>Reads the JSON representation of the object. </summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">The type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType==JsonToken.Null)
                return null;

            if (reader.TokenType==JsonToken.String)
                try
                {
                    return new MailAddress((string)reader.Value);
                } catch (FormatException fex)
                {
                    LogManager.GetLogger<MailAddressJsonConverter>().Warn(CultureInfo.InvariantCulture, m => m(SR.InvalidJsonException, reader.Value), fex);

                    return null;
                }

            throw SerializationUtils.CreateJsonException(reader, JsonToken.String);
        }

        /// <summary>Writes the JSON representation of the object. </summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value==null)
            {
                writer.WriteNull();
                return;
            }

            var ma=value as MailAddress;
            if (ma!=null)
            {
                writer.WriteValue(ma.Address);
                return;
            }

            throw new JsonSerializationException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    SR.InvalidJsonException,
                    typeof(MailAddress),
                    value.GetType()
                )
            );
        }
    }
}
