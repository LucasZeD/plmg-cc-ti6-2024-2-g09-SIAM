using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlaneDetectionv2.Data;
using PlaneDetectionv2.Models;

namespace PlaneDetectionv2.Services;

public class DatabaseService
{
    private readonly AppDbContext _dbContext;

    public DatabaseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        //_dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }
    public void SavePrediction(Prediction prediction)
    {
        _dbContext.Predictions.Add(prediction);
        _dbContext.SaveChanges();
    }
    public IEnumerable<Prediction> GetRecentPredictions(int count)
    {
        try
        {
            return _dbContext.Predictions
                .OrderByDescending(p => p.Timestamp)
                .Take(count)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error querying predictions: {ex.Message}");
            throw;
        }
    }

    public IEnumerable<Prediction> GetAllPredictions()
    {
        return _dbContext.Predictions
            .OrderByDescending(p => p.Timestamp)
            .ToList();
    }
    
    public void ClearDatabase()
    {
        _dbContext.Predictions.RemoveRange(_dbContext.Predictions);
        _dbContext.SaveChanges();
    }
}