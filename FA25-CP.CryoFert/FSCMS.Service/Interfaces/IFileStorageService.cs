namespace FSCMS.Service.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
        Task DeleteFileAsync(string filePath);
        string GetPublicUrl(string filePath);
    }

}