using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetMonkey.Serialization
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Converts a MailChimp JSON error to a <see cref="MailChimpException" />.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public class MailChimpExceptionJsonConverter:
        JsonConverter
    {

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">The type of the object.</param>
        /// <returns><c>true</c> if the specified <paramref name="objectType" /> is a descendant of <see cref="MailChimpException" />.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(MailChimpException).IsAssignableFrom(objectType);
        }

        /// <summary>Reads the JSON representation of the exception. </summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">The type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The <see cref="MailChimpException" /> instance.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType==JsonToken.Null)
                return null;

            if (reader.TokenType!=JsonToken.StartObject)
                throw new JsonReaderException(SR.InvalidJsonLineInfoException);

            string message=null;
            int code=0;
            MailChimpExceptionKind kind=MailChimpExceptionKind.Unknown;

            while(reader.Read() && (reader.TokenType==JsonToken.PropertyName))
            {
                var propertyName=reader.Value.ToString();
                if (!reader.Read())
                    throw new JsonReaderException(SR.InvalidJsonLineInfoException);

                switch (propertyName)
                {
                case "code":
                    if (reader.TokenType!=JsonToken.Integer)
                        throw new JsonReaderException(SR.InvalidJsonLineInfoException);
                    code=Convert.ToInt32(reader.Value);
                    break;
                case "name":
                    if (reader.TokenType!=JsonToken.String)
                        throw new JsonReaderException(SR.InvalidJsonLineInfoException);
                    switch(reader.Value.ToString())
                    {
                    case "Invalid_ApiKey":
                        kind=MailChimpExceptionKind.ApiInvalidKey;
                        break;
                    case "User_Disabled":
                        kind=MailChimpExceptionKind.UserDisabled;
                        break;
                    case "User_InvalidRole":
                        kind=MailChimpExceptionKind.UserInvalidRole;
                        break;
                    case "Too_Many_Connections":
                        kind=MailChimpExceptionKind.ApiTooManyConnections;
                        break;
                    case "User_UnderMaintenance":
                        kind=MailChimpExceptionKind.UserUnderMaintenance;
                        break;
                    case "User_InvalidAction":
                        kind=MailChimpExceptionKind.UserInvalidAction;
                        break;
                    case "ValidationError":
                        kind=MailChimpExceptionKind.ApiValidationError;
                        break;
                    }
                    break;
                case "error":
                    if (reader.TokenType!=JsonToken.String)
                        throw new JsonReaderException(SR.InvalidJsonLineInfoException);
                    message=reader.Value.ToString();
                    break;
                }
            }

            if (reader.TokenType!=JsonToken.EndObject)
                throw new JsonReaderException(SR.InvalidJsonLineInfoException);

            return new MailChimpException(message, code, kind);
        }

        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="NotImplementedException">This method should never be called.</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets a value indicating whether this <see cref="MailChimpExceptionJsonConverter" /> can write JSON.</summary>
        /// <value><c>false</c>, as this converter cannot write JSON.</value>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }
    }
}
