using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

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

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void panelDropArea_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelDropArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Show copy cursor
            }
        }

        private void panelDropArea_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToLower() == ".xlsx") // Only allow Excel files
                {
                    listviewFiles.Items.Add(new ListViewItem(file)); // Add file to ListView
                }
            }
        }

        private void listviewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        record DailyLog(string ProjectID, string ProjectName, string Level, string LeadName, DateOnly DailyLogDate, string TaskCode, string Task, double Hours);

        private void buttonSelectFiles_Click(object sender, EventArgs e)
        {
            string inputPath = @"C:\Users\theod\Documents\LandscapeProject\DailyLogs";
            string[] excelFiles = Directory.GetFiles(inputPath, "*.xlsx");

            if (excelFiles.Length > 0)
            {
                string firstFile = excelFiles[0];

                using (var workbook = new XLWorkbook(""))
                {
                    var worksheet = workbook.Worksheet(1);
                    var firstTable = worksheet.Tables.FirstOrDefault();

                    // Create new sheet
                    using var wb = new XLWorkbook();
                    var ws = wb.AddWorksheet();
                    ws.ColumnWidth = 8;
                    ws.FirstCell().InsertTable(new[]
                    {
                        new DailyLog("ProjectID", firstTable.FirstCell)
                    }, "PastrySales", true);

                    ws.Range("D2:D5").CreateTable("Table");

                    wb.SaveAs("Showcase.xlsx");

            /*
            string inputPath = "C:\\Users\\theod\\Documents\\LandscapeProject\\DailyLogs";
            string outputPath = "C:\\Users\\theod\\Documents\\LandscapeProject";

            FileInfo[] files = new DirectoryInfo(inputPath).GetFiles();

            List<Stream> streams = new List<Stream>();

            foreach (FileInfo file in files)
            {
                streams.Add(file.OpenRead());
            }

            Stream mergedStream = MergeExcelDocuments(streams);

            // MergedDailyLogs minDate-maxDate. Add logic to enumerate if name is the same
            FileStream fileStream = new FileStream(outputPath + "MergedDailyLogs.xlsx", FileMode.Create, FileAccess.Write);
            mergedStream.Position = 0;
            mergedStream.CopyTo(fileStream);
            fileStream.Close();
            */
        }
        
        private void SelectSheetValues()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\theod\\Documents\\LandscapeProject";
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    // Read Excel File with ClosedXML
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1); // Read 1st sheet
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header row

                        // Map Column names to indices
                        Dictionary<string, int> columnIndices = getColumnIndices(worksheet.Row(1));

                        string excelData = "ProjectID | Level | TaskCode | DailyLogDate | Hours\n";
                        Dictionary<string, double> taskHours_Dict = new Dictionary<string, double>();

                        foreach (var row in rows)
                        {
                            string projectID_Val = row.Cell(columnIndices["ProjectID"]).GetString();
                            string level_Val = row.Cell(columnIndices["Level"]).GetString();
                            string taskCode_Val = row.Cell(columnIndices["TaskCode"]).GetString();
                            DateTime dailyLogDate = row.Cell(columnIndices["DailyLogDate"]).GetDateTime();
                            string dailyLogDate_Val = dailyLogDate.ToString("MM/dd/yyyy");

                            string taskHours_Key = $"{projectID_Val}_{level_Val}_{taskCode_Val}_{dailyLogDate_Val}";
                            double tasksHours_Val = Math.Round(Convert.ToDouble(row.Cell(columnIndices["Hours"]).GetString()), 2);

                            // Update task Hours
                            if (taskHours_Dict.ContainsKey(taskHours_Key))
                            {
                                taskHours_Dict[taskHours_Key] += tasksHours_Val;
                            }
                            else
                            {
                                taskHours_Dict[taskHours_Key] = tasksHours_Val;
                            }

                            // Read data from chosen columns
                            excelData += $"{projectID_Val} | {level_Val} | {taskCode_Val} | {dailyLogDate_Val} | {tasksHours_Val}\n";
                        }

                        excelData += "\nTask Hours\n";

                        foreach (var kvp in taskHours_Dict)
                        {
                            excelData += $"{kvp.Key} : {kvp.Value}\n";
                        }

                        MessageBox.Show(excelData, "Excel Data Preview", MessageBoxButtons.OK);
                    }
                }
            }

            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
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

        private void buttonViewLogs_Click(object sender, EventArgs e)
        {

        }
    }
}
