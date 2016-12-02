using System;
using System.Globalization;
using Common.Logging;
using Newtonsoft.Json;

namespace NetMonkey.Model.Serialization
{
    /// <summary>Converts a <see cref="DateTime" /> to JSON.</summary>
    public class DateTimeJsonConverter:
        JsonConverter
    {

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">The type of the object.</param>
        /// <returns><c>true</c> if the specified <paramref name="objectType" /> is a <see cref="DateTime" />.</returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType==typeof(DateTime)) || (objectType==typeof(DateTime?));
        }

        /// <summary>Reads the JSON representation of the exception.</summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">The type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The <see cref="MailChimpException" /> instance.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType==JsonToken.Null)
                return null;

            if (reader.TokenType==JsonToken.String)
            {
                try
                {
                    return DateTime.Parse((string)reader.Value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                } catch (FormatException fex)
                {
                    LogManager.GetLogger<DateTimeJsonConverter>().Warn(CultureInfo.InvariantCulture, m => m(SR.InvalidJsonException, reader.Value), fex);

                    return null;
                }
            }

            throw SerializationUtils.CreateJsonException(reader, JsonToken.String);
        }

        /// <summary>Writes the JSON representation of the object.</summary>
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

            writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
        }
    }
}
