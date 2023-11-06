using Newtonsoft.Json;

namespace YGHMS.API.Helpers;

public class LocationSearch
{
  public int place_id { get; set; }
  public string licence { get; set; }
  public string osm_type { get; set; }
  public long osm_id { get; set; }
  public double lat { get; set; }
  public double lon { get; set; }

  // public string class { get; set; }
  public string type { get; set; }
  public int place_rank { get; set; }
  public double importance { get; set; }
  public string addresstype { get; set; }
  public string name { get; set; }
  public string display_name { get; set; }
  public Address address { get; set; }
  public List<string> boundingbox { get; set; }
}

public class Address
{
  public string road { get; set; }
  public string suburb { get; set; }
  public string city { get; set; }
  public string county { get; set; }
  public string state { get; set; }
  public string ISO3166_2_lvl4 { get; set; }
  public string postcode { get; set; }
  public string country { get; set; }
  public string country_code { get; set; }
}

public static class GetLocation
{
  public static async Task<LocationSearch> FetchDataAsync(double? latitude, double? longitude)
  {
    try
    {
      var lon = string.Format("{0}", longitude).Replace(",", ".");
      var lat = string.Format("{0}", latitude).Replace(",", ".");
      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("User-Agent",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.0.0 Safari/537.36");
      var response = await httpClient.GetAsync(
        $"https://nominatim.openstreetmap.org/reverse?lat={lat}&lon={lon}&format=json&zoom=18&addressdetails=1");
      // YourObject result = JsonConvert.DeserializeObject<YourObject>(jsonString);
      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();
        var address = JsonConvert.DeserializeObject<LocationSearch>(content);
        return address;
      }
      else
        Console.WriteLine($"Lỗi: {response.StatusCode}");
    }
    catch (Exception ex) { Console.WriteLine($"Lỗi: {ex.Message}"); }

    return null;
  }
}