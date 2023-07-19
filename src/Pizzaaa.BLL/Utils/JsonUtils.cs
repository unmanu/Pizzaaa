using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Utils;

public static class JsonUtils
{
    public static T? DeserializeFile<T>(string path)
    {
        string json = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };
        return JsonSerializer.Deserialize<T>(json, options);

    }

    public static void SerializeToFile<T>(T item, string path)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
        string json = JsonSerializer.Serialize(item, options);
        File.WriteAllText(path, json);
    }

    public static void SerializeToFileUnsafeEncode<T>(T item, string path)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        string json = JsonSerializer.Serialize(item, options);
        File.WriteAllText(path, json);
    }

    public static string FormatJsonString(string input)
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
        object? o = JsonSerializer.Deserialize<object>(input, options);
        string json = JsonSerializer.Serialize(o, options);
        return json;
    }

    public static string InlineJsonString(string input)
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
        object? o = JsonSerializer.Deserialize<object>(input, options);
        string json = JsonSerializer.Serialize(o, options);
        return json;
    }
}