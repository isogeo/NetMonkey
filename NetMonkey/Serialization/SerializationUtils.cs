using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace NetMonkey.Serialization
{

    internal static class SerializationUtils
    {

        internal static Exception CreateJsonException(JsonReader reader, JsonToken? expectedToken)
        {
            var li=reader as IJsonLineInfo;
            if ((li!=null) && li.HasLineInfo())
            {
                if (expectedToken.HasValue)
                    return new JsonSerializationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            SR.InvalidJsonLineInfoExpectingException,
                            li.LineNumber,
                            li.LinePosition,
                            expectedToken.Value,
                            reader.TokenType
                        )
                    );
                else
                    return new JsonSerializationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            SR.InvalidJsonLineInfoException,
                            li.LineNumber,
                            li.LinePosition
                        )
                    );
            } else
            {
                if (expectedToken.HasValue)
                    return new JsonSerializationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            SR.InvalidJsonExpectingException,
                            expectedToken.Value,
                            reader.TokenType
                        )
                    );
                else
                    return new JsonSerializationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            SR.InvalidJsonException
                        )
                    );
            }
        }
    }
}
