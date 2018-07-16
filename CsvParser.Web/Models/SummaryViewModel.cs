using System;
using System.Collections.Generic;

namespace CsvParser.Web.Models
{
    public class SingleUploadSummary
    {
        public Guid UploadId { get; set; }
    }

    public class SummaryViewModel
    {
        public SummaryViewModel()
        {
            Summaries = new List<SingleUploadSummary>();
        }

        public ICollection<SingleUploadSummary> Summaries { get; set; }
    }
}
