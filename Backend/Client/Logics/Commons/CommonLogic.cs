using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Client.Controllers;
using Client.Models.Helper;
using Client.Settings;
using Client.Utils.Consts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using HttpMethod = System.Net.Http.HttpMethod;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Client.Logics.Commons;

/// <summary>
/// Common logic
/// </summary>
public static class CommonLogic
{
    /// <summary>
    /// Encrypt the text
    /// </summary>
    /// <param name="beforeEncrypt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string EncryptText(string beforeEncrypt, AppDbContext context)
    {
        // Check for null or empty
        ArgumentException.ThrowIfNullOrEmpty(beforeEncrypt);

        // Get the system config
        var key = context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptIv)?.Value;
        var iv = context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptIv)?.Value;
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
                        sw.Write(beforeEncrypt);
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    /// <summary>
    /// Decrypt the text
    /// </summary>
    /// <param name="beforeDecrypt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string DecryptText(string beforeDecrypt, AppDbContext context)
    {
        // Check for null or empty
        ArgumentException.ThrowIfNullOrEmpty(beforeDecrypt);
        // Get the system config
        var key = context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptKey)?.Value;
        var iv = context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptIv)?.Value;
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
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(beforeDecrypt)))
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


    /// <summary>
    /// Call the API
    /// </summary>
    public class ApiClient<U, V> where U : AbstractApiResponse<V>
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<T, V>> CallApiAsync<T>(HttpMethod method,
            string url,
            object body = null,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> queryParams = null)
        {
            // Add query parameters
            if (queryParams != null)
            {
                url = AddQueryParameters(url, queryParams);
            }

            // Create the request
            var request = new HttpRequestMessage(method, url);

            // Add headers
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // Add body
            if (body != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                var json = JsonSerializer.Serialize(body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            // Send the request
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Check for error
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<T, V>
                {
                    Success = false,
                    MessageId = MessageId.E00000,
                    Message = responseContent
                };
            }
            
            // Deserialize the response
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

            // Check for error if success is false
            if (jsonResponse != null && jsonResponse.success == false)
            {
                return new ApiResponse<T, V>
                {
                    Success = false,
                    MessageId = MessageId.E00000,
                    Message = jsonResponse.Message
                };
            }

            // True
            var apiResponse = new ApiResponse<T, V>
            {
                Success = response.IsSuccessStatusCode,
                MessageId = MessageId.I00001,
                Message = MessageId.I00001,
            };

            // Deserialize the response
            if (response.IsSuccessStatusCode)
            {
                apiResponse.Response = JsonSerializer.Deserialize<V>(responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return apiResponse;
        }

        /// <summary>
        /// Add query parameters
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        private string AddQueryParameters(string url, Dictionary<string, string> queryParams)
        {
            var queryString = new StringBuilder();
            foreach (var param in queryParams)
            {
                queryString.Append($"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value)}&");
            }

            return url.Contains("?")
                ? $"{url}&{queryString.ToString().TrimEnd('&')}"
                : $"{url}?{queryString.ToString().TrimEnd('&')}";
        }
    }

    /// <summary>
    /// ApiResponse - Used to return the response from the CallApiAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class ApiResponse<T, V> : AbstractApiResponse<V>
    {
        public override V Response { get; set; }
    }
    
    
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> options)
        {

            var settings = options.Value;
            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File không hợp lệ.");
            }

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            // Check for error
            if (uploadResult.Error != null)
            {
                throw new Exception($"Cloudinary upload failed: {uploadResult.Error.Message}");
            }

            // Return the URL
            return uploadResult.SecureUrl?.ToString() ?? throw new Exception("Upload không trả về URL.");
        }
    }
}