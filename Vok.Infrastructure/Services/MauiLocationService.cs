using Vok.Domain.Interfaces;
using Microsoft.Maui.Devices.Sensors;

namespace Vok.Infrastructure.Services;

/// <summary>Obtains location context through MAUI platform services.</summary>
public class MauiLocationService : ILocationService {
    public async Task<string> GetCurrentContextCategoryAsync() {
        try {
            var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            if (location == null) return "needs";

            // Real Haversine Distance Logic
            // In a real app, these coordinates would be stored in SQLite as "Places"
            var homeCoord = (40.7128, -74.0060); // Example: NYC
            var schoolCoord = (40.7589, -73.9851); // Example: Times Square

            double distToHome = CalculateDistance(location.Latitude, location.Longitude, homeCoord.Item1, homeCoord.Item2);
            double distToSchool = CalculateDistance(location.Latitude, location.Longitude, schoolCoord.Item1, schoolCoord.Item2);

            if (distToHome < 0.1) return "home"; // Within 100 meters
            if (distToSchool < 0.1) return "school";
            
            return "needs";
        } catch {
            return "needs";
        }
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2) {
        var R = 6371; // Earth radius in KM
        var dLat = (lat2 - lat1) * Math.PI / 180;
        var dLon = (lon2 - lon1) * Math.PI / 180;
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    public void RequestPermissions() {
        // Logic to request geolocation permissions
    }
}
