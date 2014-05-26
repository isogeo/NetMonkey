using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
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

        private struct ExceptionMetadata
        {

            public MailChimpExceptionKind Kind;
            public Type ExceptionType;
        }

        static MailChimpExceptionJsonConverter()
        {
            _MailChimpExceptionMetadata=new Dictionary<string, ExceptionMetadata>();

            foreach(var kind in typeof(MailChimpExceptionKind).GetFields())
            {
                var attribute=(MailChimpExceptionAttribute)Attribute.GetCustomAttribute(kind, typeof(MailChimpExceptionAttribute));
                if (attribute!=null)
                    _MailChimpExceptionMetadata.Add(
                        attribute.Error,
                        new ExceptionMetadata() {
                            Kind=(MailChimpExceptionKind)kind.GetValue(null),
                            ExceptionType=attribute.ExceptionType ?? typeof(MailChimpException)
                        }
                    );
            }
        }

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
            Type exceptionType=typeof(MailChimpException);

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

                    if (_MailChimpExceptionMetadata!=null)
                    {
                        string v=reader.Value.ToString();
                        if (_MailChimpExceptionMetadata.ContainsKey(v))
                        {
                            kind=_MailChimpExceptionMetadata[v].Kind;
                            exceptionType=_MailChimpExceptionMetadata[v].ExceptionType;
                        }
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

            return Activator.CreateInstance(exceptionType, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { message, code, kind }, CultureInfo.InvariantCulture);
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

        private static IDictionary<string, ExceptionMetadata> _MailChimpExceptionMetadata;
    }
}
