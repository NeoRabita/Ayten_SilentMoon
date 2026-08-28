using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using SlientMoon.Application.DTOs.Storage;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Infrastructure.Minio.Settings;
using SlientMoon.Infrastructure.Minio.Services;

namespace SlientMoon.Infrastructure.Minio.Services;

public class MinioStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly MinioSettings _settings;

    public MinioStorageService(
        IMinioClient minioClient,
        IOptions<MinioSettings> settings)
    {
        _minioClient = minioClient;
        _settings = settings.Value;
    }

    public async Task<string> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        MinioBucket bucket,
        CancellationToken cancellationToken = default)
    {
        var bucketName = GetBucketName(bucket);

        var exists = await _minioClient.BucketExistsAsync(
            new BucketExistsArgs()
                .WithBucket(bucketName),
            cancellationToken);

        if (!exists)
        {
            await _minioClient.MakeBucketAsync(
                new MakeBucketArgs()
                    .WithBucket(bucketName),
                cancellationToken);
        }

        var objectKey =
            $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

        var args = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectKey)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType(contentType);

        await _minioClient.PutObjectAsync(
            args,
            cancellationToken);

        return objectKey;
    }

    public async Task<string> GetPresignedUrlAsync(
        MinioBucket bucket,
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        var bucketName = GetBucketName(bucket);

        var args = new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectKey)
            .WithExpiry(
                _settings.PresignedUrlExpiryInSeconds);

        return await _minioClient.PresignedGetObjectAsync(args);
    }

    private string GetBucketName(MinioBucket bucket)
    {
        return bucket switch
        {
            MinioBucket.Media => _settings.BucketName,
            MinioBucket.Icons => _settings.IconsBucketName,
            MinioBucket.Tracks => _settings.TracksBucketName,
            _ => _settings.BucketName
        };
    }
}