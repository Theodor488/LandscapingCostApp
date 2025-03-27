using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapingCostApp
{
    public class ManHoursUpdaterService
    {
        public static Dictionary<string, double> UpdateManHours(string dailyLogsFilePath, ref string outputFilePath, OpenFileDialog openFileDialog)
        {
            Dictionary<string, double> taskHours_Dict = DailyLogService.Generate_TaskHours_Dict(dailyLogsFilePath);

            MessageBox.Show("Select Project Cost Sheet to Update Hours.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Update hours in output table using hours : ProjectId/Level/TaskCode dict (Jobs_Latest table)
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputFilePath = openFileDialog.FileName;
            }

            // Read Excel File with ClosedXML
            using (var workbook_output = new XLWorkbook(outputFilePath))
            {
                var worksheet_output = workbook_output.Worksheet(1); // Read 1st sheet
                var rows = worksheet_output.RangeUsed().RowsUsed().Skip(1); // Skip header row

                // Map Column names to indices
                Dictionary<string, int> columnIndices = ExcelHelper.getColumnIndices(worksheet_output.Row(1));

                foreach (var row in rows)
                {
                    string taskHours_Key = DailyLogService.GenerateTaskHoursKey(columnIndices, row);

                    if (taskHours_Dict.ContainsKey(taskHours_Key))
                    {
                        row.Cell(columnIndices["ManHoursActual"]).Value = taskHours_Dict[taskHours_Key];
                    }
                }

                workbook_output.Save();
            }

            return taskHours_Dict;
        }
    }
}
