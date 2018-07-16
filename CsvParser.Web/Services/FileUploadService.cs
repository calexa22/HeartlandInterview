using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NodaTime;

namespace CsvParser.Web.Services
{
    public class FileUploadResult
    {
        public Guid FileUploadId { get; set; }
        public string FileName { get; set; }
    }

    public interface IFileService
    {
        Task<FileUploadResult> UploadFileAsync(IFormFile file);
    }

    public class FileService : IFileService
    {
        private readonly IClock _clock;

        public FileService(IClock clock)
        {
            _clock = clock;
        }

        public async Task<FileUploadResult> UploadFileAsync(IFormFile file)
        {
            string storageFileName = GenerateFileNameForStorage(file.FileName);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", storageFileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new FileUploadResult
            {
                FileUploadId = Guid.NewGuid(),
                FileName = storageFileName
            };
        }

        private string GenerateFileNameForStorage(string originalFileName)
        {
            Instant now = _clock.GetCurrentInstant();
            string timeStamp = now.ToDateTimeUtc().ToString("HHmmssffff");

            return $"{timeStamp}_{originalFileName}";
        }
    }
}
