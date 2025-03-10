using System.Text.RegularExpressions;
using Client.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Client.Logics.Commons;

public class CloudinaryLogic
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryLogic(IOptions<CloudinarySettings> options)
        {

            var settings = options.Value;
            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        /// <summary>
        /// Upload images to Cloudinary
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
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
        
        /// <summary>
        /// Delete image 
        /// </summary>
        /// <param name="publicId"></param>
        /// <returns></returns>
        public bool DeleteImage(string url)
        {
            var deletionParams = new DeletionParams(ExtractPublicId(url))
            {
                ResourceType = ResourceType.Image
            };

            var result = _cloudinary.Destroy(deletionParams);
            return result.Result == "ok"; 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        private string ExtractPublicId(string imageUrl)
        {
            // Find Public Id
            var match = Regex.Match(imageUrl, @"/upload/v\d+/(.*)\..+$");
            return match.Success ? match.Groups[1].Value : null;
        }
    }