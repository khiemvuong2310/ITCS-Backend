using FSCMS.Core.Entities;
using FSCMS.Core.Models.Options.FSCMS.Core.Models.Options;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class FirebaseStorageService : IFileStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public FirebaseStorageService(IOptions<FirebaseOptions> options)
        {
            var credential = GoogleCredential.FromFile(options.Value.CredentialPath);
            _storageClient = StorageClient.Create(credential);
            _bucketName = options.Value.Bucket;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var objectName = $"uploads/{DateTime.UtcNow:yyyy/MM}/{Guid.NewGuid()}_{fileName}";
            await _storageClient.UploadObjectAsync(_bucketName, objectName, contentType, fileStream);
            return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
        }

        public async Task DeleteFileAsync(string filePath)
        {
            var objectName = filePath.Split($"{_bucketName}/").Last();
            await _storageClient.DeleteObjectAsync(_bucketName, objectName);
        }

        public string GetPublicUrl(string filePath) => filePath;
    }

}