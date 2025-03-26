using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapingCostApp
{
    public class ExcelHelper
    {
        public static void ReadHeadersFromWorksheet(DataTable dataTable, IXLWorksheet worksheet)
        {
            foreach (var header in from cell in worksheet.Row(1).Cells()
                                   let header = cell.GetString()
                                   select header)
            {
                dataTable.Columns.Add(header, typeof(string));
            }
        }

        public static void ReadRowsFromWorksheet(DataTable dataTable, IXLWorksheet worksheet)
        {
            foreach (IXLRangeRow row in worksheet.RangeUsed().Rows().Skip(1))
            {
                DataRow dataRow = dataTable.NewRow();
                int columnIndex = 0;

                foreach (var cell in row.Cells())
                {
                    string cell_val = cell.GetString();
                    dataRow[columnIndex] = cell_val;
                    columnIndex++;
                }

                dataTable.Rows.Add(dataRow);
            }
        }

        public static void SaveDataTableToExcel(DataTable dataTable)
        {
            using (var outputWorkbook = new XLWorkbook())
            {
                var outputWorksheet = outputWorkbook.Worksheets.Add("Consolidated Data");
                outputWorksheet.Cell("A1").InsertTable(dataTable);
                string newTimestamp = DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss");
                string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output", $"ConsolidatedDailyLogs_{newTimestamp}.xlsx");
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                outputWorkbook.SaveAs(savePath);
            }
        }

        public static Dictionary<string, int> getColumnIndices(IXLRow headerRow)
        {
            Dictionary<string, int> columnIndices = new Dictionary<string, int>();

            foreach (var cell in headerRow.CellsUsed())
            {
                columnIndices[cell.GetString()] = cell.Address.ColumnNumber;
            }

            return columnIndices;
        }
    }
}
