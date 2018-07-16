using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace CsvParser.Web.Validation.Mvc
{
    public class CsvFileValidator : AbstractValidator<IFormFile>
    {
        private const string CsvExtension = ".csv";

        public CsvFileValidator()
        {
            RuleFor(x => x.Length).GreaterThan(0);
            RuleFor(x => x.FileName).Must(BeCsvFile);
        }

        private static bool BeCsvFile(string fileName)
        {
            return string.Equals(Path.GetExtension(fileName), CsvExtension);
        }
    }
}
