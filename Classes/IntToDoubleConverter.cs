using Newtonsoft.Json;
using System;

// Vår JSON-fil kan inte ta emot "Value" som en Double, så jag skrev en klass som har metoder som kan konvertera doublen till en int när den serialiseras och tvärtom när den deserialiseras (läsen till och från JSON)
public class IntToDoubleConverter : JsonConverter<double>
{
    // Konverterar från JSON (int till double)
    public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.Value is int intValue)
        {
            return Convert.ToDouble(intValue);
        }

        throw new JsonSerializationException($"Ogiltigt värde: {reader.Value}");
    }

    // Konverterar från double till int vid skrivning till JSON
    public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
    {
        // Här avrundar vi double till int
        writer.WriteValue((int)value);
    }
}
