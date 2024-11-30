using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia.Media.Imaging;

namespace PlaneDetectionv2.Models;

public class Prediction
{
    public int Id { get; set; }
    public byte[] ImageData { get; set; }
    // public string? ImagePath { get; set; }
    public string PredictionData { get; set; }
    public DateTime Timestamp { get; set; }
    
    public string Label => ExtractFromJson("label");
    public double Confidence => double.TryParse(ExtractFromJson("confidence"), out var result) ? result : 0.0;
    public string Coordinates =>
        $"({ExtractFromJson("xmin")}, {ExtractFromJson("ymin")}, {ExtractFromJson("xmax")}, {ExtractFromJson("ymax")})";

    private string ExtractFromJson(string key)
    {
        if (string.IsNullOrEmpty(PredictionData)) return string.Empty;

        try
        {
            var json = JsonDocument.Parse(PredictionData).RootElement;
            // Ensure the JSON contains the "predictions" array
            if (json.TryGetProperty("predictions", out var predictions))
            {
                var firstPrediction = predictions.EnumerateArray().FirstOrDefault();
                // Retrieve the property from the first prediction in the array
                if (firstPrediction.ValueKind == JsonValueKind.Object &&
                    firstPrediction.TryGetProperty(key, out var value))
                {
                    return value.ToString();
                }
            }
        }
        catch
        {
            Console.WriteLine("Error parsing JSON");
            return string.Empty;
        }
        return string.Empty;
    }
    public static byte[] BitmapToByteArray(Bitmap bitmap)
    {
        using MemoryStream stream = new MemoryStream();
        bitmap.Save(stream);
        return stream.ToArray();
    }
    public static Bitmap ByteArrayToBitmap(byte[] imageData)
    {
        using MemoryStream stream = new MemoryStream(imageData);
        return new Bitmap(stream);
    }
}