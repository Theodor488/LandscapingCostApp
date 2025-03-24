using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using WinForms = System.Windows.Forms;

// To Do
// Fix ordering of sheets appened to output sheet
// Button click open output sheet
// Better UI
// Sum up man hours functionality

namespace LandscapingCostApp
{
    public partial class CostAnalysisForm : Form
    {
        public CostAnalysisForm()
        {
            InitializeComponent();
        }

        private void panelDropArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Show copy cursor
            }
        }

        record DailyLog(string ProjectID, string ProjectName, string Level, string LeadName, DateOnly DailyLogDate, string TaskCode, string Task, double Hours);

        private void buttonSelectFiles_Click(object sender, EventArgs e)
        {
            string inputPath = SelectExcelsFolderPath();

            string[] excelFiles = Directory.GetFiles(inputPath, "*.xlsx");

            DataTable dataTable = new DataTable();
            bool headersAdded = false;

            foreach (string file in excelFiles)
            {
                using (var workbook = new XLWorkbook(file))
                {
                    var worksheet = workbook.Worksheet(1);

                    if (!headersAdded)
                    {
                        ReadHeadersFromWorksheet(dataTable, worksheet);

                        headersAdded = true;
                    }

                    ReadRowsFromWorksheet(dataTable, worksheet);
                }
            }

            SaveDataTableToExcel(dataTable);
        }

        private static string SelectExcelsFolderPath()
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
            dialog.InitialDirectory = Environment.CurrentDirectory;
            WinForms.DialogResult result = dialog.ShowDialog();
            string inputPath = Environment.CurrentDirectory;

            if (result == WinForms.DialogResult.OK)
            {
                inputPath = dialog.SelectedPath;
            }

            return inputPath;
        }

        private static void SaveDataTableToExcel(DataTable dataTable)
        {
            using (var outputWorkbook = new XLWorkbook())
            {
                var outputWorksheet = outputWorkbook.Worksheets.Add("Consolidated Data");
                outputWorksheet.Cell("A1").InsertTable(dataTable);

                string savePath = @"C:\Users\theod\Documents\LandscapeProject\Output\Demo-DataTable2.xlsx";
                outputWorkbook.SaveAs(savePath);
            }
        }

        private static void ReadRowsFromWorksheet(DataTable dataTable, IXLWorksheet worksheet)
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

        private static void ReadHeadersFromWorksheet(DataTable dataTable, IXLWorksheet worksheet)
        {
            foreach (var header in from cell in worksheet.Row(1).Cells()
                                   let header = cell.GetString()
                                   select header)
            {
                dataTable.Columns.Add(header, typeof(string));
            }
        }

        private void buttonViewLogs_Click(object sender, EventArgs e)
        {
            string outputPath = @"C:\Users\theod\Documents\LandscapeProject\Output";
            Process.Start("explorer.exe", outputPath);
        }

        private void updateManHoursButton_Click(object sender, EventArgs e)
        {
            var dailyLogsFilePath = string.Empty;
            var outputFilePath = string.Empty;
            Dictionary<string, double> taskHours_Dict = new Dictionary<string, double>();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\theod\\Documents\\LandscapeProject";
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

                // Part 1. Consolidate hours per ProjectId/Level/TaskCode (Output - Demo Table)
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dailyLogsFilePath = openFileDialog.FileName;
                }

                taskHours_Dict = Generate_TaskHours_Dict(dailyLogsFilePath, openFileDialog);
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            { 
                // Part 2. Update hours in output table using hours : ProjectId/Level/TaskCode dict (Jobs_Latest table)
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFilePath = openFileDialog.FileName;
                }

                // Read Excel File with ClosedXML
                using (var workbook_output = new XLWorkbook(outputFilePath)) // FIX: Switch to Jobs_Latest instead daily logs appended
                {
                    var worksheet_output = workbook_output.Worksheet(1); // Read 1st sheet
                    var rows = worksheet_output.RangeUsed().RowsUsed().Skip(1); // Skip header row

                    // Map Column names to indices
                    Dictionary<string, int> columnIndices = getColumnIndices(worksheet_output.Row(1));

                    foreach (var row in rows)
                    {
                        string projectID_Val = row.Cell(columnIndices["ProjectID"]).GetString();
                        string level_Val = row.Cell(columnIndices["Level"]).GetString();
                        string taskCode_Val = row.Cell(columnIndices["TaskCode"]).GetString();

                        string taskHours_Key = $"{projectID_Val}_{level_Val}_{taskCode_Val}";
                        //double tasksHours_Val = Math.Round(Convert.ToDouble(row.Cell(columnIndices["Hours"]).GetString()), 2);

                        if (taskHours_Dict.ContainsKey(taskHours_Key))
                        {
                            row.Cell(columnIndices["ManHoursActual"]).Value = taskHours_Dict[taskHours_Key];
                        }
                    }
                }
            }
        }

        private Dictionary<string, double> Generate_TaskHours_Dict(string filePath, OpenFileDialog openFileDialog)
        {
            filePath = openFileDialog.FileName;
            Dictionary<string, double> curr_taskHours_Dict = new Dictionary<string, double>();

            // Read Excel File with ClosedXML
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Read 1st sheet
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header row

                // Map Column names to indices
                Dictionary<string, int> columnIndices = getColumnIndices(worksheet.Row(1));

                string excelData = "ProjectID | Level | TaskCode | DailyLogDate | Hours\n";
                    
                foreach (var row in rows)
                {
                    string projectID_Val = row.Cell(columnIndices["ProjectID"]).GetString();
                    string level_Val = row.Cell(columnIndices["Level"]).GetString();
                    string taskCode_Val = row.Cell(columnIndices["TaskCode"]).GetString();

                    string taskHours_Key = $"{projectID_Val}_{level_Val}_{taskCode_Val}";
                    double tasksHours_Val = Math.Round(Convert.ToDouble(row.Cell(columnIndices["Hours"]).GetString()), 2);

                    // Update task Hours
                    if (curr_taskHours_Dict.ContainsKey(taskHours_Key))
                    {
                        curr_taskHours_Dict[taskHours_Key] += tasksHours_Val;
                    }
                    else
                    {
                        curr_taskHours_Dict[taskHours_Key] = tasksHours_Val;
                    }
                }
            }

            return curr_taskHours_Dict;
        }

        private void viewProjectCostbutton_Click(object sender, EventArgs e)
        {

        }

        private Dictionary<string, int> getColumnIndices(IXLRow headerRow)
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