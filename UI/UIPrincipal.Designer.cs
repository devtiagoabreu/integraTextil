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
            this.components = new System.ComponentModel.Container();
            this.btnShiftReport = new System.Windows.Forms.Button();
            this.btnProduction = new System.Windows.Forms.Button();
            this.btnRenProduction = new System.Windows.Forms.Button();
            this.btnRenShiftReport = new System.Windows.Forms.Button();
            this.btnZipCsv = new System.Windows.Forms.Button();
            this.btnDelProduction = new System.Windows.Forms.Button();
            this.btnLerDisplayOperacao = new System.Windows.Forms.Button();
            this.btnCopyDisplayOperacao = new System.Windows.Forms.Button();
            this.btnInsertDisplayOperacao = new System.Windows.Forms.Button();
            this.btnDelShiftReport = new System.Windows.Forms.Button();
            this.btnWebScrapingDisplayOperacao = new System.Windows.Forms.Button();
            this.timerWebScrapingGeral = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnShiftReport
            // 
            this.btnShiftReport.BackColor = System.Drawing.Color.SeaGreen;
            this.btnShiftReport.Location = new System.Drawing.Point(154, 89);
            this.btnShiftReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnShiftReport.Name = "btnShiftReport";
            this.btnShiftReport.Size = new System.Drawing.Size(131, 35);
            this.btnShiftReport.TabIndex = 0;
            this.btnShiftReport.Text = "Insert ShiftReport";
            this.btnShiftReport.UseVisualStyleBackColor = false;
            this.btnShiftReport.Click += new System.EventHandler(this.btnShiftReport_Click);
            // 
            // btnProduction
            // 
            this.btnProduction.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnProduction.Location = new System.Drawing.Point(11, 89);
            this.btnProduction.Margin = new System.Windows.Forms.Padding(2);
            this.btnProduction.Name = "btnProduction";
            this.btnProduction.Size = new System.Drawing.Size(131, 35);
            this.btnProduction.TabIndex = 1;
            this.btnProduction.Text = "Insert Production";
            this.btnProduction.UseVisualStyleBackColor = false;
            this.btnProduction.Click += new System.EventHandler(this.btnProduction_Click);
            // 
            // btnRenProduction
            // 
            this.btnRenProduction.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRenProduction.Location = new System.Drawing.Point(11, 50);
            this.btnRenProduction.Margin = new System.Windows.Forms.Padding(2);
            this.btnRenProduction.Name = "btnRenProduction";
            this.btnRenProduction.Size = new System.Drawing.Size(131, 35);
            this.btnRenProduction.TabIndex = 2;
            this.btnRenProduction.Text = "Ren Mov Production";
            this.btnRenProduction.UseVisualStyleBackColor = false;
            this.btnRenProduction.Click += new System.EventHandler(this.btnRenProduction_Click);
            // 
            // btnRenShiftReport
            // 
            this.btnRenShiftReport.BackColor = System.Drawing.Color.SeaGreen;
            this.btnRenShiftReport.Location = new System.Drawing.Point(154, 50);
            this.btnRenShiftReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnRenShiftReport.Name = "btnRenShiftReport";
            this.btnRenShiftReport.Size = new System.Drawing.Size(131, 35);
            this.btnRenShiftReport.TabIndex = 3;
            this.btnRenShiftReport.Text = "Ren Mov ShiftReport";
            this.btnRenShiftReport.UseVisualStyleBackColor = false;
            this.btnRenShiftReport.Click += new System.EventHandler(this.btnRenShiftReport_Click);
            // 
            // btnZipCsv
            // 
            this.btnZipCsv.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnZipCsv.Location = new System.Drawing.Point(11, 11);
            this.btnZipCsv.Margin = new System.Windows.Forms.Padding(2);
            this.btnZipCsv.Name = "btnZipCsv";
            this.btnZipCsv.Size = new System.Drawing.Size(274, 35);
            this.btnZipCsv.TabIndex = 4;
            this.btnZipCsv.Text = "BKP ZIP CSV";
            this.btnZipCsv.UseVisualStyleBackColor = false;
            this.btnZipCsv.Click += new System.EventHandler(this.btnZipCsv_Click);
            // 
            // btnDelProduction
            // 
            this.btnDelProduction.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDelProduction.Location = new System.Drawing.Point(11, 128);
            this.btnDelProduction.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelProduction.Name = "btnDelProduction";
            this.btnDelProduction.Size = new System.Drawing.Size(131, 37);
            this.btnDelProduction.TabIndex = 5;
            this.btnDelProduction.Text = "Del Production";
            this.btnDelProduction.UseVisualStyleBackColor = false;
            this.btnDelProduction.Click += new System.EventHandler(this.btnDelProduction_Click);
            // 
            // btnLerDisplayOperacao
            // 
            this.btnLerDisplayOperacao.BackColor = System.Drawing.Color.OrangeRed;
            this.btnLerDisplayOperacao.Location = new System.Drawing.Point(290, 89);
            this.btnLerDisplayOperacao.Name = "btnLerDisplayOperacao";
            this.btnLerDisplayOperacao.Size = new System.Drawing.Size(192, 35);
            this.btnLerDisplayOperacao.TabIndex = 6;
            this.btnLerDisplayOperacao.Text = "Ler DisplayOperacao";
            this.btnLerDisplayOperacao.UseVisualStyleBackColor = false;
            this.btnLerDisplayOperacao.Click += new System.EventHandler(this.btnLerDisplayOperacao_Click);
            // 
            // btnCopyDisplayOperacao
            // 
            this.btnCopyDisplayOperacao.BackColor = System.Drawing.Color.OrangeRed;
            this.btnCopyDisplayOperacao.Location = new System.Drawing.Point(290, 51);
            this.btnCopyDisplayOperacao.Name = "btnCopyDisplayOperacao";
            this.btnCopyDisplayOperacao.Size = new System.Drawing.Size(192, 35);
            this.btnCopyDisplayOperacao.TabIndex = 7;
            this.btnCopyDisplayOperacao.Text = "Copiar DisplayOperacao";
            this.btnCopyDisplayOperacao.UseVisualStyleBackColor = false;
            this.btnCopyDisplayOperacao.Click += new System.EventHandler(this.btnCopyDisplayOperacao_Click);
            // 
            // btnInsertDisplayOperacao
            // 
            this.btnInsertDisplayOperacao.BackColor = System.Drawing.Color.OrangeRed;
            this.btnInsertDisplayOperacao.Location = new System.Drawing.Point(290, 130);
            this.btnInsertDisplayOperacao.Name = "btnInsertDisplayOperacao";
            this.btnInsertDisplayOperacao.Size = new System.Drawing.Size(192, 35);
            this.btnInsertDisplayOperacao.TabIndex = 8;
            this.btnInsertDisplayOperacao.Text = "Insert DisplayOperacao";
            this.btnInsertDisplayOperacao.UseVisualStyleBackColor = false;
            this.btnInsertDisplayOperacao.Click += new System.EventHandler(this.btnInsertDisplayOperacao_Click);
            // 
            // btnDelShiftReport
            // 
            this.btnDelShiftReport.BackColor = System.Drawing.Color.SeaGreen;
            this.btnDelShiftReport.Location = new System.Drawing.Point(154, 128);
            this.btnDelShiftReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelShiftReport.Name = "btnDelShiftReport";
            this.btnDelShiftReport.Size = new System.Drawing.Size(131, 35);
            this.btnDelShiftReport.TabIndex = 9;
            this.btnDelShiftReport.Text = "Del ShiftReport";
            this.btnDelShiftReport.UseVisualStyleBackColor = false;
            this.btnDelShiftReport.Click += new System.EventHandler(this.btnDelShiftReport_Click);
            // 
            // btnWebScrapingDisplayOperacao
            // 
            this.btnWebScrapingDisplayOperacao.BackColor = System.Drawing.Color.OrangeRed;
            this.btnWebScrapingDisplayOperacao.Location = new System.Drawing.Point(290, 11);
            this.btnWebScrapingDisplayOperacao.Name = "btnWebScrapingDisplayOperacao";
            this.btnWebScrapingDisplayOperacao.Size = new System.Drawing.Size(192, 35);
            this.btnWebScrapingDisplayOperacao.TabIndex = 10;
            this.btnWebScrapingDisplayOperacao.Text = "WebScraping DisplayOperacao";
            this.btnWebScrapingDisplayOperacao.UseVisualStyleBackColor = false;
            this.btnWebScrapingDisplayOperacao.Click += new System.EventHandler(this.btnWebScrapingDisplayOperacao_Click);
            // 
            // timerWebScrapingGeral
            // 
            this.timerWebScrapingGeral.Interval = 60000;
            this.timerWebScrapingGeral.Tick += new System.EventHandler(this.timerWebScrapingGeral_Tick);
            // 
            // UIPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 412);
            this.Controls.Add(this.btnWebScrapingDisplayOperacao);
            this.Controls.Add(this.btnDelShiftReport);
            this.Controls.Add(this.btnInsertDisplayOperacao);
            this.Controls.Add(this.btnCopyDisplayOperacao);
            this.Controls.Add(this.btnLerDisplayOperacao);
            this.Controls.Add(this.btnDelProduction);
            this.Controls.Add(this.btnZipCsv);
            this.Controls.Add(this.btnRenShiftReport);
            this.Controls.Add(this.btnRenProduction);
            this.Controls.Add(this.btnProduction);
            this.Controls.Add(this.btnShiftReport);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UIPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IntegraTêxtil";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnShiftReport;
        private Button btnProduction;
        private Button btnRenProduction;
        private Button btnRenShiftReport;
        private Button btnZipCsv;
        private Button btnDelProduction;
        private Button btnLerDisplayOperacao;
        private Button btnCopyDisplayOperacao;
        private Button btnInsertDisplayOperacao;
        private Button btnDelShiftReport;
        private Button btnWebScrapingDisplayOperacao;
        private System.Windows.Forms.Timer timerWebScrapingGeral;
    }
}