using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using PlaneDetectionv2.ViewModels;
using PlaneDetectionv2.Models;
using PlaneDetectionv2.Services;
using CommunityToolkit.Mvvm.Input;

namespace PlaneDetectionv2.ViewModels;

public class HistoryViewModel : ViewModelBase
{
    public ObservableCollection<PredictionViewItem> Predictions { get; }
    public ICommand ClearDatabaseCommand { get; }
    private DatabaseService _databaseService;
    public HistoryViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        Predictions = new ObservableCollection<PredictionViewItem>();
        
        // Fetch all predictions from the database
        var predictionsFromDb = databaseService.GetAllPredictions();
        foreach (var prediction in predictionsFromDb)
        {
            try
            {
                var predictionData = JsonDocument.Parse(prediction.PredictionData);
                var firstPrediction = predictionData.RootElement
                    .GetProperty("predictions")
                    .EnumerateArray()
                    .FirstOrDefault();

                if (firstPrediction.ValueKind == JsonValueKind.Object)
                {
                    var viewItem = new PredictionViewItem
                    {
                        ImageSource = ConvertImageDataToBitmap(prediction.ImageData),
                        Timestamp = prediction.Timestamp,
                        FormattedPredictionData = FormatPredictionData(prediction.PredictionData),
                        Label = firstPrediction.GetProperty("label").GetString(),
                        Confidence = firstPrediction.GetProperty("confidence").GetDouble().ToString("P")
                    };
                    
                    

                    foreach (var element in predictionData.RootElement.GetProperty("predictions").EnumerateArray())
                    {
                        // viewItem.BoundingBoxes.Add(new BoundingBox
                        // {
                        //     XMin = element.GetProperty("xmin").GetDouble(),
                        //     YMin = element.GetProperty("ymin").GetDouble(),
                        //     Width = element.GetProperty("xmax").GetDouble() - element.GetProperty("xmin").GetDouble(),
                        //     Height = element.GetProperty("ymax").GetDouble() - element.GetProperty("ymin").GetDouble()
                        // });
                        var box = new BoundingBox
                        {
                            XMin = element.GetProperty("xmin").GetDouble(),
                            YMin = element.GetProperty("ymin").GetDouble(),
                            Width = element.GetProperty("xmax").GetDouble() - element.GetProperty("xmin").GetDouble(),
                            Height = element.GetProperty("ymax").GetDouble() - element.GetProperty("ymin").GetDouble(),
                            Label = element.GetProperty("label").GetString(),
                            Confidence = element.GetProperty("confidence").GetDouble()
                        };
                        
                        double imageWidth = viewItem.ImageSource.Size.Width;
                        double imageHeight = viewItem.ImageSource.Size.Height;
                    
                        double displayWidth = 500; // Match XAML's Image control Height/Width
                        double displayHeight = 300;
                    
                        double scaleX = displayWidth / imageWidth;
                        double scaleY = displayHeight / imageHeight;
                        
                        
                        viewItem.BoundingBoxes.Add(new BoundingBoxViewModel(box, scaleX, scaleY));
                    }
                    // Predictions.Add(viewItem);
                    viewItem.CalculateScaledBoundingBoxes(500, 300);
                    Predictions.Add(viewItem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing prediction data: {ex.Message}");
            }
        }
        
        ClearDatabaseCommand = new AsyncRelayCommand(ClearDatabaseAsync);
    }
    private string FormatPredictionData(string predictionData)
    {
        try
        {
            // Format JSON string for better readability
            var jsonElement = JsonDocument.Parse(predictionData).RootElement;
            return JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { WriteIndented = true });
        }
        catch
        {
            return predictionData;
        }
    }
    private async Task ClearDatabaseAsync()
    {
        var result = await ShowConfirmationDialogAsync("Tem certeza que deseja limpar o historico?\n Isso não poderá ser desfeito.");
        if (result)
        {
            _databaseService.ClearDatabase();
            Predictions.Clear();
        }
    }
    private async Task<bool> ShowConfirmationDialogAsync(string message)
    {
        var dialog = new Window
        {
            Title = "",
            Width = 300,
            Height = 100
        };

        var result = false;

        dialog.Content = new StackPanel
        {
            Children =
            {
                new TextBlock { Text = message, Margin = new Thickness(10) },
                new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Children =
                    {
                        new Button
                        {
                            Content = "Sim",
                            Command = new RelayCommand(() => 
                            {
                                result = true;
                                dialog.Close();
                            }),
                            Margin = new Thickness(5)
                        },
                        new Button
                        {
                            Content = "Não",
                            Command = new RelayCommand(() => 
                            {
                                result = false; // Define resultado como falso
                                dialog.Close(); // Fecha o diálogo
                            }),
                            Margin = new Thickness(5)
                        }
                    }
                }
            }
        };

        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await dialog.ShowDialog(desktop.MainWindow);
        }

        return result;
    }

    private Bitmap ConvertImageDataToBitmap(byte[] imageData)
    {
        using (var stream = new MemoryStream(imageData))
        {
            return new Bitmap(stream);
        }
    }
}



