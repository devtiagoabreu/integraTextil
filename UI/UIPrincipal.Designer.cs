namespace UI
{
    partial class UIPrincipal
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
            this.btnShiftReport = new System.Windows.Forms.Button();
            this.btnProduction = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShiftReport
            // 
            this.btnShiftReport.Location = new System.Drawing.Point(11, 11);
            this.btnShiftReport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShiftReport.Name = "btnShiftReport";
            this.btnShiftReport.Size = new System.Drawing.Size(78, 20);
            this.btnShiftReport.TabIndex = 0;
            this.btnShiftReport.Text = "ShiftReport";
            this.btnShiftReport.UseVisualStyleBackColor = true;
            this.btnShiftReport.Click += new System.EventHandler(this.btnShiftReport_Click);
            // 
            // btnProduction
            // 
            this.btnProduction.Location = new System.Drawing.Point(11, 35);
            this.btnProduction.Margin = new System.Windows.Forms.Padding(2);
            this.btnProduction.Name = "btnProduction";
            this.btnProduction.Size = new System.Drawing.Size(78, 20);
            this.btnProduction.TabIndex = 1;
            this.btnProduction.Text = "Production";
            this.btnProduction.UseVisualStyleBackColor = true;
            this.btnProduction.Click += new System.EventHandler(this.btnProduction_Click);
            // 
            // UIPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 351);
            this.Controls.Add(this.btnProduction);
            this.Controls.Add(this.btnShiftReport);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UIPrincipal";
            this.Text = "Integra Têxtil";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnShiftReport;
        private Button btnProduction;
    }
}