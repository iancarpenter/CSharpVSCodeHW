using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

using System.Text.Json.Serialization;

public class WeatherForecast
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; }

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("current_weather_units")]
    public CurrentWeatherUnits CurrentWeatherUnits { get; set; }

    [JsonPropertyName("current_weather")]
    public CurrentWeather CurrentWeather { get; set; }
}

public class CurrentWeatherUnits
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("interval")]
    public string Interval { get; set; }

    [JsonPropertyName("temperature")]
    public string Temperature { get; set; }

    [JsonPropertyName("windspeed")]
    public string WindSpeed { get; set; }

    [JsonPropertyName("winddirection")]
    public string WindDirection { get; set; }

    [JsonPropertyName("is_day")]
    public string IsDay { get; set; }

    [JsonPropertyName("weathercode")]
    public string WeatherCode { get; set; }
}

public class CurrentWeather
{
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("windspeed")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("winddirection")]
    public int WindDirection { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("weathercode")]
    public int WeatherCode { get; set; }
}
internal class Program
{
    private static async Task Main(string[] args)
    {
        
        await CallWebService("51.40031135419849", "0.8449984462603838");
    }

    private static async Task CallWebService(string latitude, string longitude)
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);
                WeatherForecast? weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(responseBody);
                                                 
                double temperature = weatherForecast?.CurrentWeather?.Temperature ?? 0.0D;
                                
                Console.WriteLine($"Temperature: {Math.Ceiling(temperature)}");
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
