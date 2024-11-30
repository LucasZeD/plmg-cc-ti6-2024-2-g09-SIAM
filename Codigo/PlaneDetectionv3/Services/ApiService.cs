using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlaneDetectionv2.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8000")
            //BaseAddress = new Uri(" <aws-link>")
        };
    }
    
    public async Task<string> GetPredictionAsync(string? imagePath, string endpoint)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
            throw new ArgumentException("Endpoint cannot be null or empty.", nameof(endpoint));
        
        using var content = new MultipartFormDataContent();
        await using var fileStream = File.OpenRead(imagePath);
        content.Add(new StreamContent(fileStream), "file", Path.GetFileName(imagePath));
        
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<bool> IsApiAvailableAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/status");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content.Contains("\"online\"");
            }
        }
        catch (Exception)
        {
            // Log exception (optional)
            Console.WriteLine("Error checking API status");
        }
        return false;
    }
}