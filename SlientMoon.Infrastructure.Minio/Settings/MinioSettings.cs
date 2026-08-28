using SlientMoon.Infrastructure.Minio.Settings;
namespace SlientMoon.Infrastructure.Minio.Settings;
public class MinioSettings
{
    public string Endpoint { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string BucketName { get; set; }
    public string IconsBucketName { get; set; }
    public string TracksBucketName { get; set; }
    public bool UseSSL { get; set; }
    public int PresignedUrlExpiryInSeconds { get; set; }
}