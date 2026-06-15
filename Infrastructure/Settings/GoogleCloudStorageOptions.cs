namespace Infrastructure.Settings
{
    public class GoogleCloudStorageOptions
    {
        public string BucketName { get; set; } = string.Empty;
        public string CredentialsPath { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
    }
}
