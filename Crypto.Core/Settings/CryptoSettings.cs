namespace Crypto.Core.Settings;

public class CryptoSettings
{
    public CryptoSettings(string url, string key, string secret)
    {
        Url = url;
        Key = key;
        Secret = secret;
    }

    public string Url { get; private set; }
    public string Key { get; private set; }
    public string Secret { get; private set; }
}
