using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CsvParser.Web.Model;
using CsvParser.Web.Services;
using CsvParser.Web.Services.CsvProcessing;
using CsvParser.Web.Validation;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CsvParser.Web.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IStudentCsvFileProcessor _studentCsvFileProcessor;

        public UploadController(IFileService fileService, IStudentCsvFileProcessor studentCsvFileProcessor)
        {
            _fileService = fileService;
            _studentCsvFileProcessor = studentCsvFileProcessor;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file == null)
            {
                return Content("No file uploaded");
            }

            if (!ModelState.IsValid)
            {
                return Content("You suck");
            }

            Task<StudentCsvProcessorResult> processingTask = _studentCsvFileProcessor.ProcessFileAsync(file);
            Task<FileUploadResult> fileUploadTask = _fileService.UploadFileAsync(file);

            await Task.WhenAll(processingTask, fileUploadTask);

            StudentCsvProcessorResult processingResult = processingTask.Result;
            FileUploadResult uploadResult = fileUploadTask.Result;

            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Result(Guid uploadId)
        //{
            
        //}
    }
}
