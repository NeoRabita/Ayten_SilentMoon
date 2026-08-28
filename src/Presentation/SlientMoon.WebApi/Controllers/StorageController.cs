using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Enums;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StorageController : BaseController
{
    private readonly IFileStorageService _fileStorageService;

    public StorageController(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(
        IFormFile file,
        MinioBucket bucket = MinioBucket.Media)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Fayl seçilməyib.");

        using var stream = file.OpenReadStream();

        var objectKey = await _fileStorageService.UploadAsync(
            stream,
            file.FileName,
            file.ContentType,
            bucket);

        var url = await _fileStorageService.GetPresignedUrlAsync(
            bucket,
            objectKey);

        return Ok(new
        {
            FileName = file.FileName,
            ObjectKey = objectKey,
            Url = url
        });
    }
}