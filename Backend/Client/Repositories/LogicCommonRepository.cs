using System.Security.Cryptography;
using System.Text;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Polly;

namespace Client.Repositories;

public class LogicCommonRepository : ILogicCommonRepository
{
    private readonly AppDbContext _context;
    private static readonly string apiUrl = "https://api.openai.com/v1/chat/completions";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public LogicCommonRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Encrypt the text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public string EncryptText(string text)
    {
        // Check for null or empty
        ArgumentException.ThrowIfNullOrEmpty(text);

        // Get the system config
        var key = _context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptIv)?.Value;
        var iv = _context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptIv)?.Value;
        // Check for null
        if (key == null)
        {
            throw new ArgumentException();
        }

        // Encrypt the text
        using (Aes aes = Aes.Create())
        {
            // Set the key and IV
            aes.Key = Encoding.UTF8.GetBytes(key);
            if (iv != null) aes.IV = Encoding.UTF8.GetBytes(iv);

            // Encrypt
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(text);
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
    
    /// <summary>
    /// Decrypt the text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public string DecryptText(string text)
    {
        // Check for null or empty
        ArgumentException.ThrowIfNullOrEmpty(text);
        // Get the system config
        var key = _context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptKey)?.Value;
        var iv = _context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptIv)?.Value;
        // Check for null
        if (key == null)
        {
            throw new ArgumentException();
        }

        // Decrypt the text
        using (Aes aes = Aes.Create())
        {
            // Set the key and IV
            aes.Key = Encoding.UTF8.GetBytes(key);
            if (iv != null) aes.IV = Encoding.UTF8.GetBytes(iv);
            // Decrypt
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }

    public async Task<bool> ImageCheck(List<string> imageUrls)
    {
        var apiKey = _context.SystemConfigs.FirstOrDefault(c => c.Id == "APIKEY_AI")?.Value;
        var httpClient = new HttpClient();
        var contentList = new List<object>
        {
            new { type = "text", text = "Determine if it is a blind box contained in a box or bag. Return only 'true' if it is a blind box that is contained within a box or bag, or 'false' if it is not." }
        };
        foreach (var url in imageUrls)
        {
            contentList.Add(new
            {
                type = "image_url",
                image_url = new { url = url }
            });
        }

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = contentList
                }
            },
            max_tokens = 300
        };

        var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(apiUrl, content);
        var responseString = await response.Content.ReadAsStringAsync();
        // Truy cập phần tử "content" trong mảng "choices"
        var jsonObj = JObject.Parse(responseString);
        string result = (string)jsonObj["choices"][0]["message"]["content"];
        if (result.Contains("False")) return false;
        else return true;
    }
}