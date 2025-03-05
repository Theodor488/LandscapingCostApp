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

        private void buttonSelectFiles_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\theod\\Documents";
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    // Read Excel File with ClosedXML
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(2); // Read 2nd sheet
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header row

                        string excelData = "Extracted Data:\n";
                        Dictionary<string, string> taskHours = new Dictionary<string, string>();

                        foreach (var row in rows)
                        {
                            string taskHoursKey = $"{row.Cell(1).GetString()}_{row.Cell(3).GetString()}_{row.Cell(7).GetString()}\n";
                            string tasksHours = row.Cell(9).GetString();
                            
                            // Read data from first three columns (adjust based on actual file structure)
                            excelData += $"{row.Cell(1).GetString()} | {row.Cell(2).GetString()} | {row.Cell(3).GetString()}\n";
                        }

                        MessageBox.Show(excelData, "Excel Data Preview", MessageBoxButtons.OK);
                    }
                }
            }

            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }
    }
}
