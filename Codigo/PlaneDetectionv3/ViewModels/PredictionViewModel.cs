using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Avalonia.Media.Imaging;
using PlaneDetectionv2.Models;

namespace PlaneDetectionv2.ViewModels;

// public class PredictionResult
// {
//     public string Label { get; set; }
//     public double Confidence { get; set; }
// }
public class PredictionViewModel : ViewModelBase
{
    private readonly Prediction _prediction;
    public string Label => _prediction?.Label ?? string.Empty;
    public double Confidence => _prediction?.Confidence ?? 0.0;
    public ObservableCollection<BoundingBoxViewModel> BoundingBoxes { get; }
    
    public PredictionViewModel(Prediction prediction)
    {
        _prediction = prediction ?? throw new ArgumentNullException(nameof(prediction));
        BoundingBoxes = new ObservableCollection<BoundingBoxViewModel>();
        
        // Convert byte array back to bitmap
        using (var memoryStream = new MemoryStream(prediction.ImageData))
        {
            // ImageBitmap is assigned the bitmap created from the byte array
            ImageBitmap = new Bitmap(memoryStream);
        }
        
        // image scaling factors for bounding box display
        double imageWidth = ImageBitmap.Size.Width;
        double imageHeight = ImageBitmap.Size.Height;

        double displayWidth = 500; // Match XAML's Image control Height/Width
        double displayHeight = 300;

        double scaleX = displayWidth / imageWidth;
        double scaleY = displayHeight / imageHeight;
            
        var jsonDoc = JsonDocument.Parse(prediction.PredictionData);
        foreach (var element in jsonDoc.RootElement.GetProperty("predictions").EnumerateArray())
        {
            // BoundingBoxes.Add(new BoundingBox
            // {
            //     XMin = element.GetProperty("xmin").GetDouble(),
            //     YMin = element.GetProperty("ymin").GetDouble(),
            //     Width = element.GetProperty("xmax").GetDouble() - element.GetProperty("xmin").GetDouble(),
            //     Height = element.GetProperty("ymax").GetDouble() - element.GetProperty("ymin").GetDouble(),
            //     Label = element.GetProperty("label").GetString(),
            //     Confidence = element.GetProperty("confidence").GetDouble()
            // });
            double confidence = element.GetProperty("confidence").GetDouble();
            if (confidence > 0.1)
            {
                var box = new BoundingBox
                {
                    XMin = element.GetProperty("xmin").GetDouble(),
                    YMin = element.GetProperty("ymin").GetDouble(),
                    Width = element.GetProperty("xmax").GetDouble() - element.GetProperty("xmin").GetDouble(),
                    Height = element.GetProperty("ymax").GetDouble() - element.GetProperty("ymin").GetDouble(),
                    Label = element.GetProperty("label").GetString(),
                    Confidence = confidence
                };

                BoundingBoxes.Add(new BoundingBoxViewModel(box, scaleX, scaleY));
            }
        }
    }
    public Bitmap ImageBitmap { get; }
}