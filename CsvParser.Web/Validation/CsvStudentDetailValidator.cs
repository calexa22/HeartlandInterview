using System;
using System.Collections.Generic;
using CsvParser.Web.Services.CsvProcessing;
               
namespace CsvParser.Web.Validation
{
    public class CsvStudentDetailValidator
    {
        //private readonly Dictionary<StudentDetailTypeEnum, >
    }

    public abstract class AStudentDetailValidationRule<T>
    {
        
        public abstract bool Validate(StudentDetailTypeEnum detailType, string detailValue);
    }
}
