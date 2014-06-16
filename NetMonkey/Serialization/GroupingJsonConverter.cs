using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Logging;
using Newtonsoft.Json;

namespace NetMonkey.Serialization
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Converts a <see cref="Grouping" /> to JSON.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public class GroupingJsonConverter:
        JsonConverter
    {

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">The type of the object.</param>
        /// <returns><c>true</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Grouping).IsAssignableFrom(objectType);
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

            Grouping ret=new Grouping();
            if (reader.TokenType==JsonToken.StartObject)
            {
                while (reader.Read())
                {
                    if (reader.TokenType==JsonToken.PropertyName)
                    {
                        switch (reader.Value.ToString())
                        {
                        case "id":
                            if (!reader.Read() || reader.TokenType!=JsonToken.Integer)
                                throw SerializationUtils.CreateJsonException(reader, JsonToken.Integer);
                            ret.Identifier=Convert.ToInt32(reader.Value);
                            break;
                        case "name":
                            if (!reader.Read() || reader.TokenType!=JsonToken.String)
                                throw SerializationUtils.CreateJsonException(reader, JsonToken.String);
                            ret.Name=reader.Value.ToString();
                            break;
                        // deserialize
                        case "groups":
                            if (!reader.Read() || reader.TokenType!=JsonToken.StartArray)
                                throw SerializationUtils.CreateJsonException(reader, JsonToken.StartArray);
                            ret.Groups=new List<Group>();
                            while (reader.Read() && (reader.TokenType!=JsonToken.EndArray))
                            {
                                if (reader.TokenType==JsonToken.String)
                                {
                                    ret.Groups.Add(new Group() { Name = reader.Value.ToString() });
                                } else if (reader.TokenType==JsonToken.StartObject)
                                {
                                    if (reader.TokenType!=JsonToken.EndObject)
                                        throw SerializationUtils.CreateJsonException(reader, JsonToken.EndObject);

                                } else
                                    throw SerializationUtils.CreateJsonException(reader, JsonToken.String);
                            }
                            break;
                        }
                    } else
                        throw SerializationUtils.CreateJsonException(reader, JsonToken.PropertyName);
                }
            } else
                throw SerializationUtils.CreateJsonException(reader, JsonToken.StartObject);

            if (!reader.Read() || (reader.TokenType!=JsonToken.EndObject))
                throw SerializationUtils.CreateJsonException(reader, JsonToken.EndObject);

            return ret;
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

            var grouping=value as Grouping;
            if (grouping!=null)
            {
                writer.WriteStartObject();

                if (grouping.Identifier.HasValue)
                {
                    writer.WritePropertyName("id");
                    writer.WriteValue(grouping.Identifier.Value);
                }
                if (grouping.Name!=null)
                {
                    writer.WritePropertyName("name");
                    writer.WriteValue(grouping.Name);
                }
                // only serialize group names
                if (grouping.Groups!=null)
                {
                    var gr=grouping.Groups.Where( g => g.IsInterested.HasValue && g.IsInterested.Value ).Select( g => g.Name );
                    if (gr.Count()>0)
                    {
                        writer.WritePropertyName("groups");
                        writer.WriteStartArray();
                        foreach (var g in gr)
                            writer.WriteValue(g);
                        writer.WriteEndArray();
                    }
                }
                writer.WriteEndObject();
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

        /// <summary>Gets a value indicating whether this <see cref="GroupingJsonConverter" /> can read JSON.</summary>
        /// <value><c>false</c></value>
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
    }
}
