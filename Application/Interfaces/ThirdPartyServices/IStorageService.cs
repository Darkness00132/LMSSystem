namespace Application.Interfaces.ThirdPartyServices
{
    public interface IStorageService
    {
        Task<string> UploadAsync(
            Stream stream,
            string fileName,
            string contentType,
            string objectPath,
            StorageAccess access);

        Task DeleteAsync(string objectPath);
    }
    public enum StorageAccess
    {
        Public,
        Private
    }
}
