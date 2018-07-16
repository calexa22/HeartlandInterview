using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CsvParser.Web.Extensions;
using CsvParser.Web.Model;

namespace CsvParser.Web.Services.CsvProcessing
{
    public class StudentCsvProcessorResult
    {
        public StudentCsvProcessorResult()
        {
            Errors = new List<string>();
            Students = new List<CsvStudentViewModel>();
        }

        public ICollection<string> Errors { get; set; }
        public ICollection<CsvStudentViewModel> Students { get; set; }
    }

    public interface IStudentCsvFileProcessor
    {
        Task<StudentCsvProcessorResult> ProcessFileAsync(IFormFile file);
    }

    public class StudentCsvProcessor : IStudentCsvFileProcessor
    {
        private static readonly StudentDetailTypeEnum[] HeaderTypes =
            (StudentDetailTypeEnum[])Enum.GetValues(typeof(StudentDetailTypeEnum));

        private readonly List<string> Errors = new List<string>();
            
        private Dictionary<int, StudentDetailTypeEnum> HeaderIndices { get; set; }
        private ICollection<CsvStudentViewModel> Students { get; set; }

        public async Task<StudentCsvProcessorResult> ProcessFileAsync(IFormFile file)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (StreamReader reader = new StreamReader(stream))
                {
                    ProcessHeaders(reader.ReadLine());

                    if (HeaderIndices == null)
                    {
                        return new StudentCsvProcessorResult
                        {
                            Errors = Errors
                        };
                    }

                    ProcessStudents(reader);

                    if (Students == null)
                    {
                        return new StudentCsvProcessorResult
                        {
                            Errors = Errors
                        };
                    }
                }
            }

            return new StudentCsvProcessorResult
            {
                Students = Students
            };
        }

        private void ProcessStudents(StreamReader reader)
        {
            List<CsvStudentViewModel> csvStudents = new List<CsvStudentViewModel>();

            string studentLine;
            bool hasErrors = false;

            while((studentLine = reader.ReadLine()) != null)
            {
                string[] fields = studentLine.Split(',');

                if (fields.Length != HeaderIndices.Count)
                {
                    hasErrors = true;
                    continue;
                }
                CsvStudentViewModel csvStudent = new CsvStudentViewModel();

                for (int i = 0; i < fields.Length; ++i)
                {
                    string detailValue = fields[i];

                    StudentDetailTypeEnum detailType = HeaderIndices[i];

                    bool isNullorWhiteSpace = string.IsNullOrWhiteSpace(detailValue);
                    bool isRequired = StudentCsvFileHeaderUtility.IsRequired(detailType);

                    if (isNullorWhiteSpace && isRequired)
                    {
                        hasErrors = true;
                    }
                    else if (!isNullorWhiteSpace)
                    {
                        csvStudent.SetDetail(detailType, detailValue);
                    }
                }

                csvStudents.Add(csvStudent);
            }

            if (!hasErrors)
            {
                Students = csvStudents;
            }
        }

        private void ProcessHeaders(string headerLine)
        {
            Dictionary<int, StudentDetailTypeEnum> headerIndicies = 
                new Dictionary<int, StudentDetailTypeEnum>();

            string[] headers = headerLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
            bool hasErrors = false;

            for (int i = 0; i < headers.Length; ++i)
            {
                StudentDetailTypeEnum? headerType =
                    StudentCsvFileHeaderUtility.GetHeaderType(headers[i]);

                // not a valid header value
                if (!headerType.HasValue)
                {
                    hasErrors = true;
                }

                // header already loaded
                if (headerIndicies.Any(kvp => kvp.Value == headerType.Value))
                {
                    hasErrors = true;
                }

                headerIndicies[i] = headerType.Value;
            }

            if (!hasErrors)
            {
                HeaderIndices = headerIndicies;
            }
        }
    }
}
