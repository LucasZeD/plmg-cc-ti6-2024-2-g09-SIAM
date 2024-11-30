namespace PlaneDetectionv2.Models;

public class BoundingBox
{
    public double XMin { get; set; }
    public double YMin { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public string Label { get; set; }
    public double Confidence { get; set; }
}