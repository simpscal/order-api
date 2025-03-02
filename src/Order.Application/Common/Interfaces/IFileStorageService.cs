namespace Order.Application.Common.Interfaces;

public interface IFileStorageService
{
    public Task<string> GetPresignedUrlAsync(
        string fileName,
        string folderName,
        string contentType,
        TimeSpan duration);
}