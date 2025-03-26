using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapingCostApp
{
    public class DailyLogService
    {
        public static Dictionary<string, double> Generate_TaskHours_Dict(string filePath)
        {
            Dictionary<string, double> curr_taskHours_Dict = new Dictionary<string, double>();

            // Read Excel File with ClosedXML
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Read 1st sheet
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header row

                // Map Column names to indices
                Dictionary<string, int> columnIndices = ExcelHelper.getColumnIndices(worksheet.Row(1));

                foreach (var row in rows)
                {
                    string taskHours_Key = GenerateTaskHoursKey(columnIndices, row);
                    double tasksHours_Val = Math.Round(Convert.ToDouble(row.Cell(columnIndices["Hours"]).GetString()), 2);

                    // Update task Hours
                    if (curr_taskHours_Dict.ContainsKey(taskHours_Key))
                        curr_taskHours_Dict[taskHours_Key] += tasksHours_Val;
                    else
                        curr_taskHours_Dict[taskHours_Key] = tasksHours_Val;
                }
            }

            return curr_taskHours_Dict;
        }

        public static string GenerateTaskHoursKey(Dictionary<string, int> columnIndices, IXLRangeRow? row)
        {
            string projectID_Val = row.Cell(columnIndices["ProjectID"]).GetString();
            string level_Val = row.Cell(columnIndices["Level"]).GetString();
            string taskCode_Val = row.Cell(columnIndices["TaskCode"]).GetString();
            string taskHours_Key = $"{projectID_Val}_{level_Val}_{taskCode_Val}";
            return taskHours_Key;
        }
    }
}
