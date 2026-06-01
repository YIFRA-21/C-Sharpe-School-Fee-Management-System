using Microsoft.AspNetCore.Mvc;
using SchoolFeeManagemetSystem.API.Interface;
using static SchoolFeeManagemetSystem.API.DTOs.ReportDTOs;

namespace SchoolFeeManagemetSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }
        [HttpPost("filter")] public async Task<IActionResult> GetReport(CreateReportDTO dto)
        {
            var result = await _service.GetReportsAsync(new ReportFilterDTO {
            FromDate = dto.FromDate, ToDate = dto.ToDate, Class = dto.Class, FeeCategory = dto.FeeCategory }); 
            return Ok(result); 
        }
        // ✅ GET REPORT (MAIN)
        [HttpGet]
        public async Task<IActionResult> GetReports(
     DateTime from,
     DateTime to,
     string? className,
     string? category)
        {
            var result = await _service.GetReportsAsync(new ReportFilterDTO
            {
                FromDate = from,
                ToDate = to,
                Class = className ?? "",
                FeeCategory = category ?? ""
            });

            return Ok(result); // 
        }

        // ✅ TOTAL COLLECTION
        [HttpGet("total-collection")]
        public async Task<IActionResult> GetTotalCollection(DateTime from, DateTime to)
        {
            return Ok(await _service.GetTotalCollectionAsync(from, to));
        }

        // ✅ TOTAL STUDENTS
        [HttpGet("total-students")]
        public async Task<IActionResult> GetTotalStudents()
        {
            return Ok(await _service.GetTotalStudentsAsync());
        }
    }
}