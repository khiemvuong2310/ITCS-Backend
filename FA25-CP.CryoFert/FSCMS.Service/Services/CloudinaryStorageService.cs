using System;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FSCMS.Core.Models;
using FSCMS.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace FSCMS.Service.Services
{
    public class CloudinaryStorageService : IFileStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryStorageService(IOptions<CloudinarySettings> options)
        {
            var acc = new Account(
                options.Value.CloudName,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                PublicId = $"{Guid.NewGuid()}_{Path.GetFileNameWithoutExtension(fileName)}"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(result.Error?.Message ?? "Upload failed");

            return result.SecureUrl.ToString(); // lưu URL vào DB
        }

        public async Task DeleteFileAsync(string filePath)
        {
            // Cloudinary dùng public_id để xóa
            var publicId = Path.GetFileNameWithoutExtension(filePath);
            var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));
            if (result.Result != "ok") throw new Exception(result.Error?.Message ?? "Delete failed");
        }

        public string GetPublicUrl(string filePath) => filePath; // filePath là URL Cloudinary
    }
}
