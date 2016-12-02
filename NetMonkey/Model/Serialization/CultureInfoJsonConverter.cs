using System;
using System.Globalization;
using Common.Logging;
using Newtonsoft.Json;

namespace NetMonkey.Model.Serialization
{

    /// <summary>Converts a <see cref="CultureInfo" /> to JSON.</summary>
    public class CultureInfoJsonConverter:
        JsonConverter
    {

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">The type of the object.</param>
        /// <returns><c>true</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(CultureInfo).IsAssignableFrom(objectType);
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
            {
                try
                {
                    return CultureInfo.GetCultureInfo((string)reader.Value);
                } catch (CultureNotFoundException cnfex)
                {
                    LogManager.GetLogger<CultureInfoJsonConverter>().Warn(CultureInfo.InvariantCulture, m => m(SR.InvalidJsonException, reader.Value), cnfex);

                    return null;
                }
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

            var ci=value as CultureInfo;
            if (ci!=null)
            {
                writer.WriteValue(ci.Name);
                return;
            }

            throw new JsonSerializationException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    SR.InvalidJsonException,
                    typeof(CultureInfo),
                    value.GetType()
                )
            );
        }
    }
}
