
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
            SuspendLayout();
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
            buttonViewLogs.Click += ButtonViewLogs_Click;
            // 
            // updateManHoursButton
            // 
            updateManHoursButton.Location = new Point(53, 328);
            updateManHoursButton.Name = "updateManHoursButton";
            updateManHoursButton.Size = new Size(217, 34);
            updateManHoursButton.TabIndex = 15;
            updateManHoursButton.Text = "Update Hours";
            updateManHoursButton.UseVisualStyleBackColor = true;
            updateManHoursButton.Click += updateManHoursButton_Click;
            // 
            // viewProjectCostbutton
            // 
            viewProjectCostbutton.Location = new Point(466, 328);
            viewProjectCostbutton.Name = "viewProjectCostbutton";
            viewProjectCostbutton.Size = new Size(222, 34);
            viewProjectCostbutton.TabIndex = 16;
            viewProjectCostbutton.Text = "View Project Cost";
            viewProjectCostbutton.UseVisualStyleBackColor = true;
            viewProjectCostbutton.Click += viewProjectCostbutton_Click;
            // 
            // CostAnalysisForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(viewProjectCostbutton);
            Controls.Add(updateManHoursButton);
            Controls.Add(buttonViewLogs);
            Controls.Add(buttonSelectFiles);
            Name = "CostAnalysisForm";
            Text = "Form1";
            ResumeLayout(false);
        }

#endregion
        private Button buttonSelectFiles;
        private Button buttonViewLogs;
        private Button updateManHoursButton;
        private Button viewProjectCostbutton;
    }
}
