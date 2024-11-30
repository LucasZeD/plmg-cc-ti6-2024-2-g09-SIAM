using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlaneDetectionv2.ViewModels;
using PlaneDetectionv2.Data;
using PlaneDetectionv2.Models;
using PlaneDetectionv2.Services;

namespace PlaneDetectionv2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    //Navigation
    private readonly ApiService _apiService;
    private readonly DatabaseService _databaseService;
    private object _currentView;
    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    public ICommand NavigateCommand { get; }
    public MainWindowViewModel()
    {
        var dbContext = new AppDbContext();
        _databaseService = new DatabaseService(dbContext);
        _apiService = new ApiService();
        
        NavigateCommand = new RelayCommand<string>(Navigate);
        // CurrentView = new HomeViewModel(_databaseService);  //default
        CurrentView = new HomeViewModel(_databaseService, _apiService);  //default
    }
    private void Navigate(string? viewName)
    {
        CurrentView = viewName switch
        {
            "HomeView" => new HomeViewModel(_databaseService, _apiService),
            "HistoryView" => new HistoryViewModel(_databaseService),
            "UploadView" => new UploadViewModel(_apiService, _databaseService, p => CurrentView = new PredictionViewModel((Prediction)p)),
            _ => CurrentView
        };
    }
}