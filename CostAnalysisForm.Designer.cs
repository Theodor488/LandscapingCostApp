﻿
namespace LandscapingCostApp
{
    partial class CostAnalysisForm
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
            buttonSelectFiles = new Button();
            buttonViewLogs = new Button();
            updateManHoursButton = new Button();
            viewProjectCostbutton = new Button();
            panelDropArea = new Panel();
            label_logFilesDrop = new Label();
            listBox_dailyLogs = new ListBox();
            panelDropArea.SuspendLayout();
            SuspendLayout();
            // 
            // buttonSelectFiles
            // 
            buttonSelectFiles.Location = new Point(101, 221);
            buttonSelectFiles.Name = "buttonSelectFiles";
            buttonSelectFiles.Size = new Size(245, 34);
            buttonSelectFiles.TabIndex = 10;
            buttonSelectFiles.Text = "2. Combine Daily Logs";
            buttonSelectFiles.UseVisualStyleBackColor = true;
            buttonSelectFiles.Click += buttonCombineDailyLogs;
            // 
            // buttonViewLogs
            // 
            buttonViewLogs.Location = new Point(466, 221);
            buttonViewLogs.Name = "buttonViewLogs";
            buttonViewLogs.Size = new Size(222, 34);
            buttonViewLogs.TabIndex = 11;
            buttonViewLogs.Text = "3. View Daily Logs";
            buttonViewLogs.UseVisualStyleBackColor = true;
            buttonViewLogs.Click += ButtonViewLogs_Click;
            // 
            // updateManHoursButton
            // 
            updateManHoursButton.Location = new Point(92, 363);
            updateManHoursButton.Name = "updateManHoursButton";
            updateManHoursButton.Size = new Size(264, 34);
            updateManHoursButton.TabIndex = 15;
            updateManHoursButton.Text = "4. Update Project Cost Hours";
            updateManHoursButton.UseVisualStyleBackColor = true;
            updateManHoursButton.Click += updateManHoursButton_Click;
            // 
            // viewProjectCostbutton
            // 
            viewProjectCostbutton.Location = new Point(466, 363);
            viewProjectCostbutton.Name = "viewProjectCostbutton";
            viewProjectCostbutton.Size = new Size(222, 34);
            viewProjectCostbutton.TabIndex = 18;
            viewProjectCostbutton.Text = "5. Open Project Cost Sheet";
            viewProjectCostbutton.Click += viewProjectCostbutton_Click;
            // 
            // panelDropArea
            // 
            panelDropArea.AllowDrop = true;
            panelDropArea.BackColor = SystemColors.ActiveBorder;
            panelDropArea.Controls.Add(label_logFilesDrop);
            panelDropArea.Location = new Point(53, 61);
            panelDropArea.Name = "panelDropArea";
            panelDropArea.Size = new Size(332, 154);
            panelDropArea.TabIndex = 17;
            panelDropArea.DragDrop += panelDropArea_DragDrop;
            panelDropArea.DragEnter += panelDropArea_DragEnter;
            panelDropArea.Paint += panelDropArea_Paint;
            // 
            // label_logFilesDrop
            // 
            label_logFilesDrop.AutoSize = true;
            label_logFilesDrop.Location = new Point(63, 65);
            label_logFilesDrop.Name = "label_logFilesDrop";
            label_logFilesDrop.Size = new Size(188, 25);
            label_logFilesDrop.TabIndex = 20;
            label_logFilesDrop.Text = "1. Drop Log Files Here";
            label_logFilesDrop.Click += label_logFilesDrop_Click;
            // 
            // listBox_dailyLogs
            // 
            listBox_dailyLogs.FormattingEnabled = true;
            listBox_dailyLogs.ItemHeight = 25;
            listBox_dailyLogs.Location = new Point(410, 61);
            listBox_dailyLogs.Name = "listBox_dailyLogs";
            listBox_dailyLogs.Size = new Size(327, 154);
            listBox_dailyLogs.TabIndex = 19;
            listBox_dailyLogs.SelectedIndexChanged += listBox_dailyLogs_SelectedIndexChanged;
            // 
            // CostAnalysisForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBox_dailyLogs);
            Controls.Add(panelDropArea);
            Controls.Add(viewProjectCostbutton);
            Controls.Add(updateManHoursButton);
            Controls.Add(buttonViewLogs);
            Controls.Add(buttonSelectFiles);
            Name = "CostAnalysisForm";
            Text = "Form1";
            panelDropArea.ResumeLayout(false);
            panelDropArea.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button buttonSelectFiles;
        private Button buttonViewLogs;
        private Button updateManHoursButton;
        private Button viewProjectCostbutton;
        private Panel panelDropArea;
        private ListBox listBox_dailyLogs;
        private Label label_logFilesDrop;
    }
}
