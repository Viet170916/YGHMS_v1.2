namespace YGHMS.API.Common;

public static class Uri
{
  public static string? BuildUrlWithHost(string relativePath)
  {
    if (string.IsNullOrEmpty(relativePath)) return null;
    if (System.Uri.TryCreate(relativePath, UriKind.Absolute, out _)) { return relativePath; }

    const string protocol = "https";
    // var host = Dns.GetHostEntry(Dns.GetHostName());
    // var localIp =
    //   (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString())
    //   .FirstOrDefault();
    return $"{protocol}://{"localhost"}:7049/{relativePath}";
  }
}