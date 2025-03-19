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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"Hello {textBox1.Text} {textBox2.Text}");
            //textBox3.Text = $"{textBox1.Text} {textBox2.Text}";
            //progressBar1.Value += 10;
            Form2 frm = new Form2();
            frm.Show();
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
    }
}