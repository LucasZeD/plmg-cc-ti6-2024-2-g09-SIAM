using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PlaneDetectionv2.Models;
using PlaneDetectionv2.Services;
using PlaneDetectionv2.Utils;

namespace PlaneDetectionv2.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public HomeViewModel(DatabaseService databaseService, ApiService apiService)
    {
        _apiService = apiService;
        
        // _databaseService = databaseService;
        RecentPredictions = new ObservableCollection<Prediction>(
            //_databaseService.GetRecentPredictions(4));
            databaseService.GetRecentPredictions(4));
        
        foreach (var prediction in RecentPredictions)
        {
            Console.WriteLine($"Prediction: {prediction.Label}, Confidence: {prediction.Confidence}");
        }
        
        AvailableModels = new ObservableCollection<string>(AIModels.GetAvailableModels());
        
        CheckApiStatus();
    }
    //available ai models
    public ObservableCollection<string> AvailableModels { get; }
    
    //Api service connection
    private readonly ApiService _apiService;
    
    //recent predictions - logic
    // private readonly DatabaseService _databaseService;
    public ObservableCollection<Prediction> RecentPredictions { get; }
    
    //api status check - logic
    private bool _isApiOnline;
    public string ApiStatusText => IsApiOnline ? "Servidor Online" : "Servidor Offline";
    public string ApiStatusColor => IsApiOnline ? "Green" : "Red";
    public bool IsApiOnline
    {
        get => _isApiOnline;
        set
        {
            _isApiOnline = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ApiStatusText));
            OnPropertyChanged(nameof(ApiStatusColor));
        }
    }
    private async void CheckApiStatus()
    {
        IsApiOnline = await _apiService.IsApiAvailableAsync();
    }
}