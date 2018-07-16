using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CsvParser.Web.Services;
using CsvParser.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CsvParser.Web.Controllers
{
    public class SummaryController : Controller
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> All()
        {
            SummaryViewModel summary = await _summaryService.GetSummariesAsync();
            return View(summary);
        }
    }
}
