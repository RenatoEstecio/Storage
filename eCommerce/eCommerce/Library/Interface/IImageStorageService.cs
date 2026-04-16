public interface IImageStorageService
{
    Task<(string Url, string Base64)> SaveAsync(Stream stream, string fileName, string contentType);
}