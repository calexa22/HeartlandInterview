using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvParser.Web.Services.CsvProcessing
{
    public enum StudentDetailTypeEnum
    {
        StudentId = 1,
        FirstName = 2,
        LastName = 3,
        City = 4,
        State = 5,
        Phone = 6,
        AppNumber = 7,
        Approved = 8
    }

    public class StudentCsvFileHeaderRule
    {
        public StudentDetailTypeEnum HeaderType { get; set; }
        public ICollection<string> AcceptedValues { get; set; }
        public bool Required { get; set; }
    }

    public static class StudentCsvFileHeaderUtility
    {
        private static readonly Dictionary<StudentDetailTypeEnum, StudentCsvFileHeaderRule> HeaderRules =
            new Dictionary<StudentDetailTypeEnum, StudentCsvFileHeaderRule>()
        {
            {
                StudentDetailTypeEnum.StudentId,
                new StudentCsvFileHeaderRule
                {
                    HeaderType = StudentDetailTypeEnum.StudentId,
                    AcceptedValues = new List<string> { "StudentId", "Id", "StudentID", "ID"  },
                    Required = true
                }
            },
            {
                StudentDetailTypeEnum.FirstName,
                new StudentCsvFileHeaderRule
                {
                    HeaderType = StudentDetailTypeEnum.FirstName,
                    AcceptedValues = new List<string> { "FirstName", "First Name", "First" },
                    Required = true
                }
            },
            {
                StudentDetailTypeEnum.LastName,
                new StudentCsvFileHeaderRule
                {
                    HeaderType = StudentDetailTypeEnum.LastName,
                    AcceptedValues = new List<string> { "LastName", "Last Name", "Last" },
                    Required = true
                }
            },
            {
                StudentDetailTypeEnum.City,
                new StudentCsvFileHeaderRule
                {
                    HeaderType = StudentDetailTypeEnum.City,
                    AcceptedValues = new List<string> { "City" },
                    Required = false
                }
            },
            {
                StudentDetailTypeEnum.State,
                new StudentCsvFileHeaderRule
                {
                    HeaderType = StudentDetailTypeEnum.State,
                    AcceptedValues = new List<string> { "St", "State", "StateId" },
                    Required = false
                }
            },
            {
                StudentDetailTypeEnum.Phone,
                new StudentCsvFileHeaderRule
                {
                    HeaderType = StudentDetailTypeEnum.Phone,
                    AcceptedValues = new List<string> { "Phone", "Phone1", "PhoneNumber", "Phone Number" },
                    Required = false
                }
            },
            {
                StudentDetailTypeEnum.AppNumber,
                new StudentCsvFileHeaderRule
                {
                    HeaderType = StudentDetailTypeEnum.AppNumber,
                    AcceptedValues = new List<string> { "AppNumber", "App_Number", "App Number" },
                    Required = false
                }
            },
            {
                StudentDetailTypeEnum.Approved,
                new StudentCsvFileHeaderRule
                {
                    HeaderType = StudentDetailTypeEnum.Approved,
                    AcceptedValues = new List<string> { "Approved" },
                    Required = true
                }
            }
        };

        public static bool IsAcceptableHeaderValue(StudentDetailTypeEnum headerType, string headerStr)
        {
            return HeaderRules[headerType]
                .AcceptedValues
                .Any(h => string.Equals(h, headerStr, StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool IsRequired(StudentDetailTypeEnum headerType)
        {
            return HeaderRules[headerType].Required;
        }

        public static StudentDetailTypeEnum? GetHeaderType(string headerStr)
        {
            foreach(KeyValuePair<StudentDetailTypeEnum, StudentCsvFileHeaderRule> kvp in HeaderRules)
            {
                if (kvp.Value.AcceptedValues.Any(h => string.Equals(h, headerStr)))
                {
                    return kvp.Key;
                }
            }

            return null;
        }
    }
}
