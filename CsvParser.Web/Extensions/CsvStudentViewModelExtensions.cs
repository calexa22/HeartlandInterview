using System;
using CsvParser.Web.Model;
using CsvParser.Web.Services.CsvProcessing;

namespace CsvParser.Web.Extensions
{
    public static class CsvStudentViewModelExtensions
    {
        public static void SetDetail(this CsvStudentViewModel student, StudentDetailTypeEnum detailType, string detailValue)
        {
            if (detailType == StudentDetailTypeEnum.AppNumber)
            {
                student.AppNumber = Convert.ToInt32(detailValue);
            }
            else if (detailType == StudentDetailTypeEnum.Approved)
            {
                student.Approved = detailValue == "T" ? true : false;
            }
        }
    }
}
