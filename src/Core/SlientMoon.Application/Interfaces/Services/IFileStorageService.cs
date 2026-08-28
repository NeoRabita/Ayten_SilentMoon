using SlientMoon.Domain.Enums;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Services;

public interface IFileStorageService
{
    Task<string> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        MinioBucket bucket,
        CancellationToken cancellationToken = default);

    Task<string> GetPresignedUrlAsync(MinioBucket bucket, string objectKey, CancellationToken cancellationToken = default);
}