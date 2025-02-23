using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OpenAI;
using OpenAI.Chat;
using Polly;
using System.Text;

namespace server.Controllers.V1.AEPS.OpenAI
{
    public class ImageValidationService
    {
        private static readonly string apiUrl = "https://api.openai.com/v1/chat/completions";

        public async static Task<bool> ImageCheck(List<string> imageUrls, AppDbContext context)
        {
            var apiKey = context.SystemConfigs.FirstOrDefault(c => c.Id == "APIKEY_AI")?.Value;
            var httpClient = new HttpClient();
            var contentList = new List<object>
            {
                new { type = "text", text = "Is this image a blind box or Baby Three? The answer must be true or false." }
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
}



