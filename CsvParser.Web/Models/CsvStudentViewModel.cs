using System;
namespace CsvParser.Web.Model
{
    public class CsvStudentViewModel
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public int? AppNumber { get; set; }
        public bool Approved { get; set; }
    }
}
