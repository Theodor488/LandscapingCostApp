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
            buttonSelectFiles = new Button();
            buttonViewLogs = new Button();
            button1 = new Button();
            button2 = new Button();
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
            // buttonSelectFiles
            // 
            buttonSelectFiles.Location = new Point(53, 240);
            buttonSelectFiles.Name = "buttonSelectFiles";
            buttonSelectFiles.Size = new Size(217, 34);
            buttonSelectFiles.TabIndex = 10;
            buttonSelectFiles.Text = "Combine Daily Logs";
            buttonSelectFiles.UseVisualStyleBackColor = true;
            buttonSelectFiles.Click += buttonSelectFiles_Click;
            // 
            // buttonViewLogs
            // 
            buttonViewLogs.Location = new Point(466, 240);
            buttonViewLogs.Name = "buttonViewLogs";
            buttonViewLogs.Size = new Size(222, 34);
            buttonViewLogs.TabIndex = 11;
            buttonViewLogs.Text = "View Daily Logs";
            buttonViewLogs.UseVisualStyleBackColor = true;
            buttonViewLogs.Click += buttonViewLogs_Click;
            // 
            // button1
            // 
            button1.Location = new Point(53, 328);
            button1.Name = "button1";
            button1.Size = new Size(217, 34);
            button1.TabIndex = 12;
            button1.Text = "Update Man Hours";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(466, 328);
            button2.Name = "button2";
            button2.Size = new Size(222, 34);
            button2.TabIndex = 13;
            button2.Text = "View Master Project Cost";
            button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(buttonViewLogs);
            Controls.Add(buttonSelectFiles);
            Controls.Add(panelDropArea);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Panel panelDropArea;
        private Button buttonSelectFiles;
        private Button buttonViewLogs;
        private Button button1;
        private Button button2;
    }
}
