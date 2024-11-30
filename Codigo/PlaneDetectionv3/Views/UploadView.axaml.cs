using System.Linq;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PlaneDetectionv2.ViewModels;

namespace PlaneDetectionv2.Views;

public partial class UploadView : UserControl
{
    public UploadView()
    {
        InitializeComponent();
        
        // Add handlers for drag-and-drop
        DropBorder.AddHandler(DragDrop.DropEvent, OnImageDrop, RoutingStrategies.Tunnel);
        DropBorder.AddHandler(DragDrop.DragEnterEvent, OnDragEnter, RoutingStrategies.Tunnel);
        DropBorder.AddHandler(DragDrop.DragLeaveEvent, OnDragLeave, RoutingStrategies.Tunnel);
    }
    private async void OnImageDrop(object? sender, DragEventArgs e)
    {
        if (e.Data.Contains(DataFormats.FileNames))
        {
            var filePaths = e.Data.GetFileNames();
            if (filePaths != null)
            {
                var imagePath = filePaths.FirstOrDefault(); // Get the first dropped file
                if (imagePath != null && File.Exists(imagePath))
                {
                    if (DataContext is UploadViewModel viewModel)
                    {
                        viewModel.SelectedImagePath = imagePath;
                    }
                }
            }
        }
    }
    private void OnDragEnter(object? sender, DragEventArgs e)
    {
        if (DataContext is UploadViewModel viewModel)
        {
            viewModel.IsDragging = true;
        }
    }
    private void OnDragLeave(object? sender, RoutedEventArgs e)
    {
        if (DataContext is UploadViewModel viewModel)
        {
            viewModel.IsDragging = false;
        }
    }
}