using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.Input;
using PlaneDetectionv2.Models;
using PlaneDetectionv2.Services;

namespace PlaneDetectionv2.ViewModels;

public class UploadViewModel : ViewModelBase
{
    //data
    private readonly ApiService _apiService;
    private readonly DatabaseService _databaseService;
    private readonly Action<object> _goToPrediction;
    public string? _errorMessageText;
    public string ErrorMessageText
    {
        get => _errorMessageText ?? "";
        set
        {
            _errorMessageText = value;
            OnPropertyChanged();
        }
    }
    
    //image
    private string? _selectedImagePath;
    public bool IsImageSelected => !string.IsNullOrEmpty(SelectedImagePath);
    public string SelectedImagePath
    {
        get => _selectedImagePath;
        set
        {
            _selectedImagePath = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsImageSelected));
            OnPropertyChanged(nameof(SelectedImageBitmap));
        }
        
    }
    public Bitmap? SelectedImageBitmap
    {
        get
        {
            if (string.IsNullOrEmpty(SelectedImagePath) || !File.Exists(SelectedImagePath))
                return null;

            return new Bitmap(SelectedImagePath);
        }
    }
    // model selection
    private ObservableCollection<string> _availableModels;
    public ObservableCollection<string> AvailableModels
    {
        get => _availableModels;
        set
        {
            _availableModels = value;
            OnPropertyChanged();
        }
    }
    private string _selectedModel;
    public string SelectedModel
    {
        get => _selectedModel;
        set
        {
            _selectedModel = value;
            OnPropertyChanged();
            OnModelSelected(); // This method can be called to handle logic when a model is selected
        }
    }

    private void OnModelSelected()
    {
        // Handle logic for when a model is selected
        Console.WriteLine($"Selected model: {SelectedModel}");
    }
    
    //drag and drop image - not working fix later
    private bool _isDragging;
    public bool IsDragging
    {
        get => _isDragging;
        set
        {
            _isDragging = value;
            OnPropertyChanged();
        }
    }

    
    //command
    public ICommand SelectImageCommand { get; }
    public ICommand PredictImageCommand { get; }
    public UploadViewModel(ApiService apiService, DatabaseService databaseService, Action<object> goToPrediction)
    {
        Console.WriteLine("Entered Update View Model");
        _apiService = apiService;
        _databaseService = databaseService;
        _goToPrediction = goToPrediction;

        AvailableModels = new ObservableCollection<string>(AIModels.GetAvailableModels());
        
        SelectImageCommand = new RelayCommand(SelectImage);
        PredictImageCommand = new RelayCommand(async () => await PredictImage());
    }
    private async void SelectImage()
    {
        Console.WriteLine("Choosing Image File");
        var dialog = new OpenFileDialog()
        {
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Image", Extensions = { "jpg", "jpeg" } }
            }
        };

        var result = await dialog.ShowAsync(
            App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : null);
        
        if (result != null && result.Length > 0)
        {
            SelectedImagePath = result[0];
            ErrorMessageText = "";
            Console.WriteLine($"An image was selected: {SelectedImagePath}");
        }
        else
        {
            Console.WriteLine("No image was selected.");
        }
    }
    private async Task PredictImage()
    {
        if (string.IsNullOrEmpty(SelectedImagePath))
        {
            ErrorMessageText = "Nenhuma Imagem Foi Selecionada.";
            Console.WriteLine("No image selected");
            return;
        }
        
        if (string.IsNullOrEmpty(SelectedModel))
        {
            ErrorMessageText = "Nenhum Modelo Foi Selecionado.";
            Console.WriteLine("No model selected");
            return;
        }

        Console.WriteLine("Checking API availability...");
        
        var isApiAvailable = await _apiService.IsApiAvailableAsync();
        if (!isApiAvailable)
        {
            ErrorMessageText = "Falha Na Comunicação Com O Servidor";
            Console.WriteLine("API is not available.");
            return;
        }
        
        Console.WriteLine("API is available. Trying to send prediction request to API...");
        
        try
        {
            string endpoint = "predict/"+SelectedModel+"/";
            var predictionJson = await _apiService.GetPredictionAsync(SelectedImagePath, endpoint);
            
            Console.WriteLine("Prediction request sent to API.");
            Console.WriteLine($"Prediction JSON: {predictionJson}");
            
            // Convert image to byte array
            byte[] imageData = File.ReadAllBytes(SelectedImagePath);

            var prediction = new Prediction
            {
                ImageData = imageData,
                PredictionData = predictionJson,
                Timestamp = DateTime.Now
            };

            _databaseService.SavePrediction(prediction);

            _goToPrediction(prediction);
        }
        catch (HttpRequestException ex)
        {
            ErrorMessageText = "Could not connect to the API.";
            Console.WriteLine($"HTTP request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            ErrorMessageText = "An unexpected error occurred.";
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}