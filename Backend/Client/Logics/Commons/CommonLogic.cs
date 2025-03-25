using System.Text;
using System.Text.Json;
using Client.Controllers;
using Client.Utils.Consts;
using Microsoft.EntityFrameworkCore;
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
    public static IQueryable<T?> ApplySorting<T>(IQueryable<T> query, byte? sortBy) where T : class?
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
                Convert.ToDouble(EF.Property<int?>(x, "TotalSold")) * 0.5 +
                Convert.ToDouble(EF.Property<int?>(x, "TotalOrders")) * 0.3 +
                Convert.ToDouble(EF.Property<decimal?>(x, "AverageRating")) * 0.2
            ),
            _ => query
        };
    }

    /// <summary>
    /// Call the API
    /// </summary>
    public class ApiClient<U, V> where U : AbstractApiResponse<V>, new()
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<U> CallApiAsync(HttpMethod method,
            string url,
            object body = null,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                url = AddQueryParameters(url, queryParams);
            }

            var request = new HttpRequestMessage(method, url);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (body != null)
            {
                var json = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new U
                {
                    Success = false,
                    MessageId = "E00000",
                    Message = $"Error {response.StatusCode}: {responseContent}"
                };
            }

            var jsonResponse = JsonConvert.DeserializeObject<V>(responseContent);

            return new U { Success = true, Response = jsonResponse };
        }

        private string AddQueryParameters(string url, Dictionary<string, string> queryParams)
        {
            var queryString = string.Join("&",
                queryParams.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));

            return url.Contains("?") ? $"{url}&{queryString}" : $"{url}?{queryString}";
        }
    }
    
}