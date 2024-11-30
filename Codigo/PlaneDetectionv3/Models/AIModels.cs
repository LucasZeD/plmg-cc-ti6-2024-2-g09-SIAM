using System.Collections.Generic;

namespace PlaneDetectionv2.Models;

public class AIModels
{
    public static List<string> GetAvailableModels()
    {
        return new List<string>
        {
            "SIAMv1",
            "SIAMv2"
        };
    }
}