using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GracefulExpandoObject.Json
{
	public class GracefulExpandoObjectConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return ReadValue(reader);
		}

		private object ReadValue(JsonReader reader)
		{
			while (reader.TokenType == JsonToken.Comment)
			{
				if (!reader.Read())
				{
					throw new JsonSerializationException("Unexpected end when reading ExpandoObject.");
				}
			}

			switch (reader.TokenType)
			{
				case JsonToken.StartObject:
					return ReadObject(reader);

				case JsonToken.StartArray:
					return ReadList(reader);

				default:
					return reader.Value;
			}
		}

		private object ReadList(JsonReader reader)
		{
			IList<object> list = new List<object>();

			while (reader.Read())
			{
				switch (reader.TokenType)
				{
					case JsonToken.Comment:
						break;

					default:
						object v = ReadValue(reader);

						list.Add(v);
						break;

					case JsonToken.EndArray:
						return list;
				}
			}

			throw new JsonSerializationException("Unexpected end when reading ExpandoObject.");
		}

		private object ReadObject(JsonReader reader)
		{
			IDictionary<string, object> expandoObject = new GracefulExpandoObject();

			while (reader.Read())
			{
				switch (reader.TokenType)
				{
					case JsonToken.PropertyName:
						string propertyName = reader.Value.ToString();

						if (!reader.Read())
						{
							throw new JsonSerializationException("Unexpected end when reading ExpandoObject.");
						}

						object v = ReadValue(reader);

						expandoObject[propertyName] = v;
						break;

					case JsonToken.Comment:
						break;

					case JsonToken.EndObject:
						return expandoObject;
				}
			}

			throw new JsonSerializationException("Unexpected end when reading ExpandoObject.");
		}

		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(GracefulExpandoObject));
		}

		public override bool CanWrite
		{
			get { return false; }
		}
	}
}
