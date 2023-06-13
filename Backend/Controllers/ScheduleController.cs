using Backend.Data.Entities;
using Backend.DTOs;
using Backend.Service.ScheduleService;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Backend.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("InsertDataSchedule")]
        public async Task<IActionResult> InsertDataSchedule(List<List<string>> schedules)
        {
            try
            {
                if (schedules == null)
                {
                    return BadRequest(new ResponseDTO
                    {
                        message = "Required request not null!!"
                    });
                }
                var resData = await _scheduleService.InsertDataSchedule(schedules)   ;
                return StatusCode(resData.code, resData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Import")]
        public IActionResult ImnportFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return BadRequest("No file uploaded.");

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first worksheet

                    // Read the data from the worksheet
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    List<List<string>> data = new List<List<string>>();

                    for (int row = 1; row <= rowCount; row++)
                    {
                        List<string> rowData = new List<string>();

                        for (int col = 1; col <= colCount; col++)
                        {
                            var cellValue = GetCellValueAsDate(worksheet, row, col)?.ToString()
                                ?? worksheet.Cells[row, col].Value?.ToString() ?? "";
                            rowData.Add(cellValue);
                        }

                        data.Add(rowData);
                    }

                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("data")]
        public IActionResult ReadExcel()
        {
            try
            {
                string filePath = "E:\\Fs\\MockProject2\\Test\\BE\\Mock2\\Mock2\\wwwroot\\Template_plan.xlsx";

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Lấy worksheet đầu tiên (index 0)

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    List<List<string>> data = new List<List<string>>();

                    for (int row = 1; row <= rowCount; row++)
                    {
                        List<string> rowData = new List<string>();

                        for (int col = 1; col <= colCount; col++)
                        {
                            var cellValue = GetCellValueAsDate(worksheet, row, col)?.ToString()
                                ?? worksheet.Cells[row, col].Value?.ToString() ?? "";
                            rowData.Add(cellValue);
                        }

                        data.Add(rowData);
                    }

                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private DateTime? GetCellValueAsDate(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellValue != null && worksheet.Cells[row, column].Style.Numberformat.Format.Contains("yyyy"))
            {
                double excelDateValue = double.Parse(cellValue.ToString());
                DateTime dateValue = DateTime.FromOADate(excelDateValue);
                return dateValue;
            }

            return null;
        }


    }
}
