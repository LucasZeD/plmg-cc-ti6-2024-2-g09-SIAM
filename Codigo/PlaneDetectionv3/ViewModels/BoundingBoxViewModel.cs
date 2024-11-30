using System;
using System.Threading.Tasks;
using PlaneDetectionv2.Models;

namespace PlaneDetectionv2.ViewModels;

public class BoundingBoxViewModel
{
    private readonly double _scaleX;
    private readonly double _scaleY;

    public double ScaledXMin => XMin * _scaleX;
    public double ScaledYMin => YMin * _scaleY;
    public double ScaledWidth => Width * _scaleX;
    public double ScaledHeight => Height * _scaleY;
    
    //fix later for history
    public double ScaledXMinH { get; private set; }
    public double ScaledYMinH { get; private set; }
    public double ScaledWidthH { get; private set; }
    public double ScaledHeightH { get; private set; }

    //public double XMin { get; }
    public double XMin { get; private set; }
    public double YMin { get; private set; }
    public double Width { get; private set; }
    public double Height { get; private set; }
    public string Label { get; private set; }
    public double Confidence { get; private set; }

    public BoundingBoxViewModel(BoundingBox box, double scaleX, double scaleY)
    {
        XMin = box.XMin;
        YMin = box.YMin;
        Width = box.Width;
        Height = box.Height;
        Label = box.Label;
        Confidence = box.Confidence;

        _scaleX = scaleX;
        _scaleY = scaleY;
        
        Console.WriteLine($"ScaledXMin: {ScaledXMin}, ScaledYMin: {ScaledYMin}, ScaledWidth: {ScaledWidth}, ScaledHeight: {ScaledHeight}");
    }
    
    public void UpdateScaling(double scaleX, double scaleY)
    {
        ScaledXMinH = XMin * scaleX;
        ScaledYMinH = YMin * scaleY;
        ScaledWidthH = Width * scaleX;
        ScaledHeightH = Height * scaleY;
    }
    
    public async Task UpdateScalingAsync(double scaleX, double scaleY)
    {
        await Task.Delay(10);

        ScaledXMinH = XMin * scaleX;
        ScaledYMinH = YMin * scaleY;
        ScaledWidthH = Width * scaleX;
        ScaledHeightH = Height * scaleY;
    }
}