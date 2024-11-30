using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using PlaneDetectionv2.Models;

namespace PlaneDetectionv2.ViewModels;

public class PredictionViewItem
{
    public Bitmap ImageSource { get; set; } 
    public DateTime Timestamp { get; set; }
    public string FormattedPredictionData { get; set; }
    public ObservableCollection<BoundingBoxViewModel> BoundingBoxes  { get; } = new ObservableCollection<BoundingBoxViewModel>();
    public string Label { get; set; }
    public string Confidence { get; set; }

    public void CalculateScaledBoundingBoxes(double displayWidth, double displayHeight)
    {
        double imageWidth = ImageSource.Size.Width;
        double imageHeight = ImageSource.Size.Height;

        double scaleX = displayWidth / imageWidth;
        double scaleY = displayHeight / imageHeight;

        foreach (var box in BoundingBoxes)
        {
            box.UpdateScaling(scaleX, scaleY);
            // box.UpdateScalingAsync(scaleX, scaleY);
        }
    }
}
// public class PredictionViewItem
// {
//     public Bitmap ImageSource { get; set; } 
//     public DateTime Timestamp { get; set; }
//     public string FormattedPredictionData { get; set; }
//     public ObservableCollection<BoundingBox> BoundingBoxes { get; } = new ObservableCollection<BoundingBox>();
//     public string Label { get; set; }
//     public string Confidence { get; set; }
// }

