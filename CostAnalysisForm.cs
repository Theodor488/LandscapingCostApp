using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using WinForms = System.Windows.Forms;

namespace LandscapingCostApp
{
    public partial class CostAnalysisForm : Form
    {
        List<string> droppedDailyLogFiles = new List<string>();

        public CostAnalysisForm()
        {
            InitializeComponent();

            panelDropArea.AllowDrop = true;
            panelDropArea.DragEnter += panelDropArea_DragEnter;
            panelDropArea.DragDrop += panelDropArea_DragDrop;

            // Ensure child controls do not handle drag
            label_logFilesDrop.AllowDrop = false;
            listBox_dailyLogs.AllowDrop = false;
        }

        private void buttonCombineDailyLogs(object sender, EventArgs e)
        {
            string inputPath = SelectExcelsFolderPath();
            DataTable dataTable = new DataTable();
            bool headersAdded = false;

            foreach (string file in droppedDailyLogFiles)
            {
                using (var workbook = new XLWorkbook(file))
                {
                    var worksheet = workbook.Worksheet(1);

                    if (!headersAdded)
                    {
                        ExcelHelper.ReadHeadersFromWorksheet(dataTable, worksheet);
                        headersAdded = true;
                    }
                    ExcelHelper.ReadRowsFromWorksheet(dataTable, worksheet);
                }
            }

            ExcelHelper.SaveDataTableToExcel(dataTable);
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

        private void ButtonViewLogs_Click(object sender, EventArgs e)
        {
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
            Process.Start("explorer.exe", outputPath);
        }

        private void updateManHoursButton_Click(object sender, EventArgs e)
        {
            var dailyLogsFilePath = string.Empty;
            var outputFilePath = string.Empty;
            Dictionary<string, double> taskHours_Dict = new Dictionary<string, double>();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

                MessageBox.Show("Select Consolidated Daily Logs File.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Consolidate hours per ProjectId/Level/TaskCode
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dailyLogsFilePath = openFileDialog.FileName;
                }

                taskHours_Dict = ManHoursUpdaterService.UpdateManHours(dailyLogsFilePath, ref outputFilePath, openFileDialog);
            }
        }

        private void panelDropArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                bool allExcel = files.All(file => Path.GetExtension(file).ToLower() == ".xlsx");

                if (allExcel)
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        private void panelDropArea_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                if (!droppedDailyLogFiles.Contains(file))
                {
                    droppedDailyLogFiles.Add(file);

                    if (Path.GetExtension(file).ToLower() == ".xlsx")
                    {
                        string fileName = Path.GetFileName(file);
                        listBox_dailyLogs.Items.Add(fileName);
                    }
                }
            }
        }

        private void viewProjectCostbutton_Click(object sender, EventArgs e)
        {
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
            Process.Start("explorer.exe", outputPath);
        }

        private void label_logFilesDrop_Click(object sender, EventArgs e)
        {

        }

        private void panelDropArea_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox_dailyLogs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}