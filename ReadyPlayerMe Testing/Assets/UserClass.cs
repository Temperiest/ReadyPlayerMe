using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class UserClass
{
    [JsonProperty("user")]
    public UserData UserUser { get; set; }

    public UserClass(string email,string pass)
    {
        UserUser = new UserData(email, pass);
    }
}

public partial class UserData
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("passapp")]
    public string Passapp { get; set; }

    public UserData(string mail, string pass)
    {
        Email = mail;
        Passapp = pass;
    }
}

public partial class Resp
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }

    [JsonProperty("id_user")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long IdUser { get; set; }

    [JsonProperty("nombre")]
    public string Nombre { get; set; }

    [JsonProperty("apellido1")]
    public string Apellido1 { get; set; }

    [JsonProperty("apellido2")]
    public string Apellido2 { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("url_avatar")]
    public Uri UrlAvatar { get; set; }
}

internal static class Converter
{
    public static object Deserializer(string json, object o)
    {
        return JsonConvert.DeserializeObject<object>(json, Converter.Settings);
    }

public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
}

internal class ParseStringConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        long l;
        if (Int64.TryParse(value, out l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (long)untypedValue;
        serializer.Serialize(writer, value.ToString());
        return;
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
}
