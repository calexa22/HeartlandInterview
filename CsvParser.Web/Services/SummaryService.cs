using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvParser.Web.Models;

namespace CsvParser.Web.Services
{
    public interface ISummaryService
    {
        Task<SummaryViewModel> GetSummariesAsync();
    }

    public class SummaryService : ISummaryService
    {
        public async Task<SummaryViewModel> GetSummariesAsync()
        {
            List<SingleUploadSummary> summaries = new List<SingleUploadSummary>();


            return new SummaryViewModel
            {
                Summaries = Enumerable.Range(0, 10).Select(i => new SingleUploadSummary {UploadId = Guid.NewGuid()}).ToList()
            };
        }
    }
}
