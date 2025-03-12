namespace LandscapingCostApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelDropArea = new Panel();
            listviewFiles = new ListView();
            buttonSelectFiles = new Button();
            buttonViewLogs = new Button();
            SuspendLayout();
            // 
            // panelDropArea
            // 
            panelDropArea.AllowDrop = true;
            panelDropArea.Location = new Point(53, 31);
            panelDropArea.Name = "panelDropArea";
            panelDropArea.Size = new Size(635, 150);
            panelDropArea.TabIndex = 8;
            panelDropArea.DragDrop += panelDropArea_DragDrop;
            panelDropArea.DragEnter += panelDropArea_DragEnter;
            panelDropArea.Paint += panelDropArea_Paint;
            // 
            // listviewFiles
            // 
            listviewFiles.Location = new Point(53, 187);
            listviewFiles.Name = "listviewFiles";
            listviewFiles.Size = new Size(635, 197);
            listviewFiles.TabIndex = 9;
            listviewFiles.UseCompatibleStateImageBehavior = false;
            listviewFiles.SelectedIndexChanged += listviewFiles_SelectedIndexChanged;
            // 
            // buttonSelectFiles
            // 
            buttonSelectFiles.Location = new Point(53, 404);
            buttonSelectFiles.Name = "buttonSelectFiles";
            buttonSelectFiles.Size = new Size(217, 34);
            buttonSelectFiles.TabIndex = 10;
            buttonSelectFiles.Text = "Combine Daily Logs";
            buttonSelectFiles.UseVisualStyleBackColor = true;
            buttonSelectFiles.Click += buttonSelectFiles_Click;
            // 
            // buttonViewLogs
            // 
            buttonViewLogs.Location = new Point(576, 404);
            buttonViewLogs.Name = "buttonViewLogs";
            buttonViewLogs.Size = new Size(112, 34);
            buttonViewLogs.TabIndex = 11;
            buttonViewLogs.Text = "View Logs";
            buttonViewLogs.UseVisualStyleBackColor = true;
            buttonViewLogs.Click += buttonViewLogs_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonViewLogs);
            Controls.Add(buttonSelectFiles);
            Controls.Add(listviewFiles);
            Controls.Add(panelDropArea);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Panel panelDropArea;
        private ListView listviewFiles;
        private Button buttonSelectFiles;
        private Button buttonViewLogs;
    }
}
