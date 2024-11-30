using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PlaneDetectionv2.Models;
using PlaneDetectionv2.ViewModels;

namespace PlaneDetectionv2.Views;

public partial class PredictionView : UserControl
{
    public PredictionView(/*Prediction prediction*/)
    {
        InitializeComponent();
        // DataContext = new PredictionViewModel(prediction);
    }
}