using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
    /// Apply sorting
    /// </summary>
    /// <param name="query"></param>
    /// <param name="sortBy"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, byte? sortBy) where T : class
    {
        if (!sortBy.HasValue)
            return query;

        return sortBy switch
        {
            (byte)ConstantEnum.Sort.IncreasingPrice => query.OrderBy(x => EF.Property<decimal>(x, "Price")),
            (byte)ConstantEnum.Sort.DecreasingPrice => query.OrderByDescending(x => EF.Property<decimal>(x, "Price")),
            (byte)ConstantEnum.Sort.Newest => query.OrderByDescending(x => EF.Property<DateTime>(x, "CreatedAt")),
            (byte)ConstantEnum.Sort.Oldest => query.OrderBy(x => EF.Property<DateTime>(x, "CreatedAt")),
            (byte)ConstantEnum.Sort.MostPopular => query.OrderByDescending(x =>
                Convert.ToDouble(EF.Property<int>(x, "TotalSold")) * 0.5 +
                Convert.ToDouble(EF.Property<int>(x, "TotalOrders")) * 0.3 +
                Convert.ToDouble(EF.Property<decimal>(x, "AverageRating")) * 0.2
            ),
            _ => query
        };
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
}