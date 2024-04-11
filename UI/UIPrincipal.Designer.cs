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
            this.btnDelProduction = new System.Windows.Forms.Button();
            this.btnLerDisplayOperacao = new System.Windows.Forms.Button();
            this.btnCopyDisplayOperacao = new System.Windows.Forms.Button();
            this.btnInsertDisplayOperacao = new System.Windows.Forms.Button();
            this.btnDelShiftReport = new System.Windows.Forms.Button();
            this.timerWebScrapingGeral = new System.Windows.Forms.Timer(this.components);
            this.btnDelContasReceber = new System.Windows.Forms.Button();
            this.btnRenContasReceber = new System.Windows.Forms.Button();
            this.btnInsertContasReceber = new System.Windows.Forms.Button();
            this.btnDelContasPagar = new System.Windows.Forms.Button();
            this.btnRenContasPagar = new System.Windows.Forms.Button();
            this.btnInsertContasPagar = new System.Windows.Forms.Button();
            this.btnDelContasPagas = new System.Windows.Forms.Button();
            this.btnRenContasPagas = new System.Windows.Forms.Button();
            this.btnInsertContasPagas = new System.Windows.Forms.Button();
            this.btnDelCaixasFios = new System.Windows.Forms.Button();
            this.btnRenCaixasFios = new System.Windows.Forms.Button();
            this.btnInsertCaixasFios = new System.Windows.Forms.Button();
            this.btnDelNotasFiscais = new System.Windows.Forms.Button();
            this.btnRenNotasFiscais = new System.Windows.Forms.Button();
            this.btnInsertNotasFiscais = new System.Windows.Forms.Button();
            this.btnDelSaldosEstoque = new System.Windows.Forms.Button();
            this.btnRenSaldosEstoque = new System.Windows.Forms.Button();
            this.btnInsertSaldosEstoque = new System.Windows.Forms.Button();
            this.btnDelEstoqueRolos = new System.Windows.Forms.Button();
            this.btnRenEstoqueRolos = new System.Windows.Forms.Button();
            this.btnInsertEstoqueRolos = new System.Windows.Forms.Button();
            this.btnDelComercialVendas = new System.Windows.Forms.Button();
            this.btnRenComercialVendas = new System.Windows.Forms.Button();
            this.btnInsertComercialVendas = new System.Windows.Forms.Button();
            this.btnDelPosicaoOps = new System.Windows.Forms.Button();
            this.btnRenPosicaoOps = new System.Windows.Forms.Button();
            this.btnInsertPosicaoOps = new System.Windows.Forms.Button();
            this.btnDelRecebimentos = new System.Windows.Forms.Button();
            this.btnRenRecebimentos = new System.Windows.Forms.Button();
            this.btnInsertRecebimentos = new System.Windows.Forms.Button();
            this.btnDelPedidoCompra = new System.Windows.Forms.Button();
            this.btnRenPedidoCompra = new System.Windows.Forms.Button();
            this.btnInsertPedidoCompra = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShiftReport
            // 
            this.btnShiftReport.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnShiftReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnShiftReport.ForeColor = System.Drawing.Color.DimGray;
            this.btnShiftReport.Location = new System.Drawing.Point(13, 87);
            this.btnShiftReport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnShiftReport.Name = "btnShiftReport";
            this.btnShiftReport.Size = new System.Drawing.Size(211, 47);
            this.btnShiftReport.TabIndex = 0;
            this.btnShiftReport.Text = "Insert ShiftReport";
            this.btnShiftReport.UseVisualStyleBackColor = false;
            this.btnShiftReport.Click += new System.EventHandler(this.btnShiftReport_Click);
            // 
            // btnProduction
            // 
            this.btnProduction.BackColor = System.Drawing.Color.Tan;
            this.btnProduction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnProduction.ForeColor = System.Drawing.Color.DimGray;
            this.btnProduction.Location = new System.Drawing.Point(228, 87);
            this.btnProduction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnProduction.Name = "btnProduction";
            this.btnProduction.Size = new System.Drawing.Size(209, 47);
            this.btnProduction.TabIndex = 1;
            this.btnProduction.Text = "Insert Production";
            this.btnProduction.UseVisualStyleBackColor = false;
            this.btnProduction.Click += new System.EventHandler(this.btnProduction_Click);
            // 
            // btnRenProduction
            // 
            this.btnRenProduction.BackColor = System.Drawing.Color.Tan;
            this.btnRenProduction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenProduction.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenProduction.Location = new System.Drawing.Point(228, 35);
            this.btnRenProduction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenProduction.Name = "btnRenProduction";
            this.btnRenProduction.Size = new System.Drawing.Size(209, 47);
            this.btnRenProduction.TabIndex = 2;
            this.btnRenProduction.Text = "Ren Mov Production";
            this.btnRenProduction.UseVisualStyleBackColor = false;
            this.btnRenProduction.Click += new System.EventHandler(this.btnRenProduction_Click);
            // 
            // btnRenShiftReport
            // 
            this.btnRenShiftReport.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRenShiftReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenShiftReport.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenShiftReport.Location = new System.Drawing.Point(13, 35);
            this.btnRenShiftReport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenShiftReport.Name = "btnRenShiftReport";
            this.btnRenShiftReport.Size = new System.Drawing.Size(211, 47);
            this.btnRenShiftReport.TabIndex = 3;
            this.btnRenShiftReport.Text = "Ren Mov ShiftReport";
            this.btnRenShiftReport.UseVisualStyleBackColor = false;
            this.btnRenShiftReport.Click += new System.EventHandler(this.btnRenShiftReport_Click);
            // 
            // btnDelProduction
            // 
            this.btnDelProduction.BackColor = System.Drawing.Color.Tan;
            this.btnDelProduction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelProduction.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelProduction.Location = new System.Drawing.Point(228, 139);
            this.btnDelProduction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelProduction.Name = "btnDelProduction";
            this.btnDelProduction.Size = new System.Drawing.Size(209, 49);
            this.btnDelProduction.TabIndex = 5;
            this.btnDelProduction.Text = "Del Production";
            this.btnDelProduction.UseVisualStyleBackColor = false;
            this.btnDelProduction.Click += new System.EventHandler(this.btnDelProduction_Click);
            // 
            // btnLerDisplayOperacao
            // 
            this.btnLerDisplayOperacao.BackColor = System.Drawing.Color.Salmon;
            this.btnLerDisplayOperacao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLerDisplayOperacao.ForeColor = System.Drawing.Color.DimGray;
            this.btnLerDisplayOperacao.Location = new System.Drawing.Point(443, 86);
            this.btnLerDisplayOperacao.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLerDisplayOperacao.Name = "btnLerDisplayOperacao";
            this.btnLerDisplayOperacao.Size = new System.Drawing.Size(208, 47);
            this.btnLerDisplayOperacao.TabIndex = 6;
            this.btnLerDisplayOperacao.Text = "Ler DisplayOperacao";
            this.btnLerDisplayOperacao.UseVisualStyleBackColor = false;
            this.btnLerDisplayOperacao.Click += new System.EventHandler(this.btnLerDisplayOperacao_Click);
            // 
            // btnCopyDisplayOperacao
            // 
            this.btnCopyDisplayOperacao.BackColor = System.Drawing.Color.Salmon;
            this.btnCopyDisplayOperacao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCopyDisplayOperacao.ForeColor = System.Drawing.Color.DimGray;
            this.btnCopyDisplayOperacao.Location = new System.Drawing.Point(443, 35);
            this.btnCopyDisplayOperacao.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCopyDisplayOperacao.Name = "btnCopyDisplayOperacao";
            this.btnCopyDisplayOperacao.Size = new System.Drawing.Size(208, 47);
            this.btnCopyDisplayOperacao.TabIndex = 7;
            this.btnCopyDisplayOperacao.Text = "Copiar DisplayOperacao";
            this.btnCopyDisplayOperacao.UseVisualStyleBackColor = false;
            this.btnCopyDisplayOperacao.Click += new System.EventHandler(this.btnCopyDisplayOperacao_Click);
            // 
            // btnInsertDisplayOperacao
            // 
            this.btnInsertDisplayOperacao.BackColor = System.Drawing.Color.Salmon;
            this.btnInsertDisplayOperacao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertDisplayOperacao.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertDisplayOperacao.Location = new System.Drawing.Point(443, 140);
            this.btnInsertDisplayOperacao.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnInsertDisplayOperacao.Name = "btnInsertDisplayOperacao";
            this.btnInsertDisplayOperacao.Size = new System.Drawing.Size(208, 47);
            this.btnInsertDisplayOperacao.TabIndex = 8;
            this.btnInsertDisplayOperacao.Text = "Insert DisplayOperacao";
            this.btnInsertDisplayOperacao.UseVisualStyleBackColor = false;
            this.btnInsertDisplayOperacao.Click += new System.EventHandler(this.btnInsertDisplayOperacao_Click);
            // 
            // btnDelShiftReport
            // 
            this.btnDelShiftReport.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDelShiftReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelShiftReport.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelShiftReport.Location = new System.Drawing.Point(13, 139);
            this.btnDelShiftReport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelShiftReport.Name = "btnDelShiftReport";
            this.btnDelShiftReport.Size = new System.Drawing.Size(211, 47);
            this.btnDelShiftReport.TabIndex = 9;
            this.btnDelShiftReport.Text = "Del ShiftReport";
            this.btnDelShiftReport.UseVisualStyleBackColor = false;
            this.btnDelShiftReport.Click += new System.EventHandler(this.btnDelShiftReport_Click);
            // 
            // timerWebScrapingGeral
            // 
            this.timerWebScrapingGeral.Interval = 60000;
            this.timerWebScrapingGeral.Tick += new System.EventHandler(this.timerWebScrapingGeral_Tick);
            // 
            // btnDelContasReceber
            // 
            this.btnDelContasReceber.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDelContasReceber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelContasReceber.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelContasReceber.Location = new System.Drawing.Point(11, 491);
            this.btnDelContasReceber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelContasReceber.Name = "btnDelContasReceber";
            this.btnDelContasReceber.Size = new System.Drawing.Size(210, 49);
            this.btnDelContasReceber.TabIndex = 13;
            this.btnDelContasReceber.Text = "Del Contas a Receber";
            this.btnDelContasReceber.UseVisualStyleBackColor = false;
            this.btnDelContasReceber.Click += new System.EventHandler(this.btnDelContasReceber_Click);
            // 
            // btnRenContasReceber
            // 
            this.btnRenContasReceber.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRenContasReceber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenContasReceber.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenContasReceber.Location = new System.Drawing.Point(11, 387);
            this.btnRenContasReceber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenContasReceber.Name = "btnRenContasReceber";
            this.btnRenContasReceber.Size = new System.Drawing.Size(210, 47);
            this.btnRenContasReceber.TabIndex = 12;
            this.btnRenContasReceber.Text = "Ren Mov Contas a Receber";
            this.btnRenContasReceber.UseVisualStyleBackColor = false;
            this.btnRenContasReceber.Click += new System.EventHandler(this.btnRenContasReceber_Click);
            // 
            // btnInsertContasReceber
            // 
            this.btnInsertContasReceber.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnInsertContasReceber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertContasReceber.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertContasReceber.Location = new System.Drawing.Point(11, 439);
            this.btnInsertContasReceber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertContasReceber.Name = "btnInsertContasReceber";
            this.btnInsertContasReceber.Size = new System.Drawing.Size(210, 47);
            this.btnInsertContasReceber.TabIndex = 11;
            this.btnInsertContasReceber.Text = "Insert Contas a Receber";
            this.btnInsertContasReceber.UseVisualStyleBackColor = false;
            this.btnInsertContasReceber.Click += new System.EventHandler(this.btnInsertContasReceber_Click);
            // 
            // btnDelContasPagar
            // 
            this.btnDelContasPagar.BackColor = System.Drawing.Color.Tan;
            this.btnDelContasPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelContasPagar.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelContasPagar.Location = new System.Drawing.Point(225, 491);
            this.btnDelContasPagar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelContasPagar.Name = "btnDelContasPagar";
            this.btnDelContasPagar.Size = new System.Drawing.Size(210, 49);
            this.btnDelContasPagar.TabIndex = 16;
            this.btnDelContasPagar.Text = "Del Contas a Pagar";
            this.btnDelContasPagar.UseVisualStyleBackColor = false;
            this.btnDelContasPagar.Click += new System.EventHandler(this.btnDelContasPagar_Click);
            // 
            // btnRenContasPagar
            // 
            this.btnRenContasPagar.BackColor = System.Drawing.Color.Tan;
            this.btnRenContasPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenContasPagar.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenContasPagar.Location = new System.Drawing.Point(225, 387);
            this.btnRenContasPagar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenContasPagar.Name = "btnRenContasPagar";
            this.btnRenContasPagar.Size = new System.Drawing.Size(210, 47);
            this.btnRenContasPagar.TabIndex = 15;
            this.btnRenContasPagar.Text = "Ren Mov Contas a Pagar";
            this.btnRenContasPagar.UseVisualStyleBackColor = false;
            this.btnRenContasPagar.Click += new System.EventHandler(this.btnRenContasPagar_Click);
            // 
            // btnInsertContasPagar
            // 
            this.btnInsertContasPagar.BackColor = System.Drawing.Color.Tan;
            this.btnInsertContasPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertContasPagar.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertContasPagar.Location = new System.Drawing.Point(225, 439);
            this.btnInsertContasPagar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertContasPagar.Name = "btnInsertContasPagar";
            this.btnInsertContasPagar.Size = new System.Drawing.Size(210, 47);
            this.btnInsertContasPagar.TabIndex = 14;
            this.btnInsertContasPagar.Text = "Insert Contas a Pagar";
            this.btnInsertContasPagar.UseVisualStyleBackColor = false;
            this.btnInsertContasPagar.Click += new System.EventHandler(this.btnInsertContasPagar_Click);
            // 
            // btnDelContasPagas
            // 
            this.btnDelContasPagas.BackColor = System.Drawing.Color.Tan;
            this.btnDelContasPagas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelContasPagas.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelContasPagas.Location = new System.Drawing.Point(223, 668);
            this.btnDelContasPagas.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelContasPagas.Name = "btnDelContasPagas";
            this.btnDelContasPagas.Size = new System.Drawing.Size(210, 49);
            this.btnDelContasPagas.TabIndex = 19;
            this.btnDelContasPagas.Text = "Del Contas a Pagas";
            this.btnDelContasPagas.UseVisualStyleBackColor = false;
            this.btnDelContasPagas.Click += new System.EventHandler(this.btnDelContasPagas_Click);
            // 
            // btnRenContasPagas
            // 
            this.btnRenContasPagas.BackColor = System.Drawing.Color.Tan;
            this.btnRenContasPagas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenContasPagas.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenContasPagas.Location = new System.Drawing.Point(223, 564);
            this.btnRenContasPagas.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenContasPagas.Name = "btnRenContasPagas";
            this.btnRenContasPagas.Size = new System.Drawing.Size(210, 47);
            this.btnRenContasPagas.TabIndex = 18;
            this.btnRenContasPagas.Text = "Ren Mov Contas a Pagas";
            this.btnRenContasPagas.UseVisualStyleBackColor = false;
            this.btnRenContasPagas.Click += new System.EventHandler(this.btnRenContasPagas_Click);
            // 
            // btnInsertContasPagas
            // 
            this.btnInsertContasPagas.BackColor = System.Drawing.Color.Tan;
            this.btnInsertContasPagas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertContasPagas.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertContasPagas.Location = new System.Drawing.Point(223, 617);
            this.btnInsertContasPagas.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertContasPagas.Name = "btnInsertContasPagas";
            this.btnInsertContasPagas.Size = new System.Drawing.Size(210, 47);
            this.btnInsertContasPagas.TabIndex = 17;
            this.btnInsertContasPagas.Text = "Insert Contas a Pagas";
            this.btnInsertContasPagas.UseVisualStyleBackColor = false;
            this.btnInsertContasPagas.Click += new System.EventHandler(this.btnInsertContasPagas_Click);
            // 
            // btnDelCaixasFios
            // 
            this.btnDelCaixasFios.BackColor = System.Drawing.Color.Aquamarine;
            this.btnDelCaixasFios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelCaixasFios.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelCaixasFios.Location = new System.Drawing.Point(655, 314);
            this.btnDelCaixasFios.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelCaixasFios.Name = "btnDelCaixasFios";
            this.btnDelCaixasFios.Size = new System.Drawing.Size(210, 49);
            this.btnDelCaixasFios.TabIndex = 22;
            this.btnDelCaixasFios.Text = "Del Caixas fios";
            this.btnDelCaixasFios.UseVisualStyleBackColor = false;
            this.btnDelCaixasFios.Click += new System.EventHandler(this.btnDelCaixasFios_Click);
            // 
            // btnRenCaixasFios
            // 
            this.btnRenCaixasFios.BackColor = System.Drawing.Color.Aquamarine;
            this.btnRenCaixasFios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenCaixasFios.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenCaixasFios.Location = new System.Drawing.Point(655, 210);
            this.btnRenCaixasFios.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenCaixasFios.Name = "btnRenCaixasFios";
            this.btnRenCaixasFios.Size = new System.Drawing.Size(210, 47);
            this.btnRenCaixasFios.TabIndex = 21;
            this.btnRenCaixasFios.Text = "Ren Mov Caixas fios";
            this.btnRenCaixasFios.UseVisualStyleBackColor = false;
            this.btnRenCaixasFios.Click += new System.EventHandler(this.btnRenCaixasFios_Click);
            // 
            // btnInsertCaixasFios
            // 
            this.btnInsertCaixasFios.BackColor = System.Drawing.Color.Aquamarine;
            this.btnInsertCaixasFios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertCaixasFios.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertCaixasFios.Location = new System.Drawing.Point(655, 263);
            this.btnInsertCaixasFios.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertCaixasFios.Name = "btnInsertCaixasFios";
            this.btnInsertCaixasFios.Size = new System.Drawing.Size(210, 47);
            this.btnInsertCaixasFios.TabIndex = 20;
            this.btnInsertCaixasFios.Text = "Insert Caixas fios";
            this.btnInsertCaixasFios.UseVisualStyleBackColor = false;
            this.btnInsertCaixasFios.Click += new System.EventHandler(this.btnInsertCaixasFios_Click);
            // 
            // btnDelNotasFiscais
            // 
            this.btnDelNotasFiscais.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDelNotasFiscais.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelNotasFiscais.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelNotasFiscais.Location = new System.Drawing.Point(11, 314);
            this.btnDelNotasFiscais.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelNotasFiscais.Name = "btnDelNotasFiscais";
            this.btnDelNotasFiscais.Size = new System.Drawing.Size(210, 49);
            this.btnDelNotasFiscais.TabIndex = 25;
            this.btnDelNotasFiscais.Text = "Del Notas Fiscais";
            this.btnDelNotasFiscais.UseVisualStyleBackColor = false;
            this.btnDelNotasFiscais.Click += new System.EventHandler(this.btnDelNotasFiscais_Click);
            // 
            // btnRenNotasFiscais
            // 
            this.btnRenNotasFiscais.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRenNotasFiscais.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenNotasFiscais.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenNotasFiscais.Location = new System.Drawing.Point(11, 210);
            this.btnRenNotasFiscais.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenNotasFiscais.Name = "btnRenNotasFiscais";
            this.btnRenNotasFiscais.Size = new System.Drawing.Size(210, 47);
            this.btnRenNotasFiscais.TabIndex = 24;
            this.btnRenNotasFiscais.Text = "Ren Mov Notas Ficais";
            this.btnRenNotasFiscais.UseVisualStyleBackColor = false;
            this.btnRenNotasFiscais.Click += new System.EventHandler(this.btnRenNotasFiscais_Click);
            // 
            // btnInsertNotasFiscais
            // 
            this.btnInsertNotasFiscais.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnInsertNotasFiscais.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertNotasFiscais.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertNotasFiscais.Location = new System.Drawing.Point(11, 263);
            this.btnInsertNotasFiscais.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertNotasFiscais.Name = "btnInsertNotasFiscais";
            this.btnInsertNotasFiscais.Size = new System.Drawing.Size(210, 47);
            this.btnInsertNotasFiscais.TabIndex = 23;
            this.btnInsertNotasFiscais.Text = "Insert Notas Fiscais";
            this.btnInsertNotasFiscais.UseVisualStyleBackColor = false;
            this.btnInsertNotasFiscais.Click += new System.EventHandler(this.btnInsertNotasFiscais_Click);
            // 
            // btnDelSaldosEstoque
            // 
            this.btnDelSaldosEstoque.BackColor = System.Drawing.Color.Aquamarine;
            this.btnDelSaldosEstoque.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelSaldosEstoque.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelSaldosEstoque.Location = new System.Drawing.Point(655, 668);
            this.btnDelSaldosEstoque.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelSaldosEstoque.Name = "btnDelSaldosEstoque";
            this.btnDelSaldosEstoque.Size = new System.Drawing.Size(210, 49);
            this.btnDelSaldosEstoque.TabIndex = 28;
            this.btnDelSaldosEstoque.Text = "Del Saldos Estoque";
            this.btnDelSaldosEstoque.UseVisualStyleBackColor = false;
            this.btnDelSaldosEstoque.Click += new System.EventHandler(this.btnDelSaldosEstoque_Click);
            // 
            // btnRenSaldosEstoque
            // 
            this.btnRenSaldosEstoque.BackColor = System.Drawing.Color.Aquamarine;
            this.btnRenSaldosEstoque.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenSaldosEstoque.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenSaldosEstoque.Location = new System.Drawing.Point(655, 564);
            this.btnRenSaldosEstoque.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenSaldosEstoque.Name = "btnRenSaldosEstoque";
            this.btnRenSaldosEstoque.Size = new System.Drawing.Size(210, 47);
            this.btnRenSaldosEstoque.TabIndex = 27;
            this.btnRenSaldosEstoque.Text = "Ren Mov Saldos Estoque ";
            this.btnRenSaldosEstoque.UseVisualStyleBackColor = false;
            this.btnRenSaldosEstoque.Click += new System.EventHandler(this.btnRenSaldosEstoque_Click);
            // 
            // btnInsertSaldosEstoque
            // 
            this.btnInsertSaldosEstoque.BackColor = System.Drawing.Color.Aquamarine;
            this.btnInsertSaldosEstoque.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertSaldosEstoque.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertSaldosEstoque.Location = new System.Drawing.Point(655, 617);
            this.btnInsertSaldosEstoque.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertSaldosEstoque.Name = "btnInsertSaldosEstoque";
            this.btnInsertSaldosEstoque.Size = new System.Drawing.Size(210, 47);
            this.btnInsertSaldosEstoque.TabIndex = 26;
            this.btnInsertSaldosEstoque.Text = "Insert Saldos Estoque";
            this.btnInsertSaldosEstoque.UseVisualStyleBackColor = false;
            this.btnInsertSaldosEstoque.Click += new System.EventHandler(this.btnInsertSaldosEstoque_Click);
            // 
            // btnDelEstoqueRolos
            // 
            this.btnDelEstoqueRolos.BackColor = System.Drawing.Color.Aquamarine;
            this.btnDelEstoqueRolos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelEstoqueRolos.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelEstoqueRolos.Location = new System.Drawing.Point(657, 491);
            this.btnDelEstoqueRolos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelEstoqueRolos.Name = "btnDelEstoqueRolos";
            this.btnDelEstoqueRolos.Size = new System.Drawing.Size(210, 49);
            this.btnDelEstoqueRolos.TabIndex = 31;
            this.btnDelEstoqueRolos.Text = "Del Estoque Rolos";
            this.btnDelEstoqueRolos.UseVisualStyleBackColor = false;
            this.btnDelEstoqueRolos.Click += new System.EventHandler(this.btnDelEstoqueRolos_Click);
            // 
            // btnRenEstoqueRolos
            // 
            this.btnRenEstoqueRolos.BackColor = System.Drawing.Color.Aquamarine;
            this.btnRenEstoqueRolos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenEstoqueRolos.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenEstoqueRolos.Location = new System.Drawing.Point(657, 387);
            this.btnRenEstoqueRolos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenEstoqueRolos.Name = "btnRenEstoqueRolos";
            this.btnRenEstoqueRolos.Size = new System.Drawing.Size(210, 47);
            this.btnRenEstoqueRolos.TabIndex = 30;
            this.btnRenEstoqueRolos.Text = "Ren Mov Estoque Rolos";
            this.btnRenEstoqueRolos.UseVisualStyleBackColor = false;
            this.btnRenEstoqueRolos.Click += new System.EventHandler(this.btnRenEstoqueRolos_Click);
            // 
            // btnInsertEstoqueRolos
            // 
            this.btnInsertEstoqueRolos.BackColor = System.Drawing.Color.Aquamarine;
            this.btnInsertEstoqueRolos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertEstoqueRolos.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertEstoqueRolos.Location = new System.Drawing.Point(657, 440);
            this.btnInsertEstoqueRolos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertEstoqueRolos.Name = "btnInsertEstoqueRolos";
            this.btnInsertEstoqueRolos.Size = new System.Drawing.Size(210, 47);
            this.btnInsertEstoqueRolos.TabIndex = 29;
            this.btnInsertEstoqueRolos.Text = "Insert Estoque Rolos";
            this.btnInsertEstoqueRolos.UseVisualStyleBackColor = false;
            this.btnInsertEstoqueRolos.Click += new System.EventHandler(this.btnInsertEstoqueRolos_Click);
            // 
            // btnDelComercialVendas
            // 
            this.btnDelComercialVendas.BackColor = System.Drawing.Color.Salmon;
            this.btnDelComercialVendas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelComercialVendas.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelComercialVendas.Location = new System.Drawing.Point(443, 314);
            this.btnDelComercialVendas.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelComercialVendas.Name = "btnDelComercialVendas";
            this.btnDelComercialVendas.Size = new System.Drawing.Size(210, 49);
            this.btnDelComercialVendas.TabIndex = 34;
            this.btnDelComercialVendas.Text = "Del Comercial Vendas";
            this.btnDelComercialVendas.UseVisualStyleBackColor = false;
            this.btnDelComercialVendas.Click += new System.EventHandler(this.btnDelComercialVendas_Click);
            // 
            // btnRenComercialVendas
            // 
            this.btnRenComercialVendas.BackColor = System.Drawing.Color.Salmon;
            this.btnRenComercialVendas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenComercialVendas.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenComercialVendas.Location = new System.Drawing.Point(443, 210);
            this.btnRenComercialVendas.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenComercialVendas.Name = "btnRenComercialVendas";
            this.btnRenComercialVendas.Size = new System.Drawing.Size(210, 47);
            this.btnRenComercialVendas.TabIndex = 33;
            this.btnRenComercialVendas.Text = "Ren Mov Comercial Vendas";
            this.btnRenComercialVendas.UseVisualStyleBackColor = false;
            this.btnRenComercialVendas.Click += new System.EventHandler(this.btnRenComercialVendas_Click);
            // 
            // btnInsertComercialVendas
            // 
            this.btnInsertComercialVendas.BackColor = System.Drawing.Color.Salmon;
            this.btnInsertComercialVendas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertComercialVendas.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertComercialVendas.Location = new System.Drawing.Point(443, 263);
            this.btnInsertComercialVendas.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertComercialVendas.Name = "btnInsertComercialVendas";
            this.btnInsertComercialVendas.Size = new System.Drawing.Size(210, 47);
            this.btnInsertComercialVendas.TabIndex = 32;
            this.btnInsertComercialVendas.Text = "Insert Comercial Vendas";
            this.btnInsertComercialVendas.UseVisualStyleBackColor = false;
            this.btnInsertComercialVendas.Click += new System.EventHandler(this.btnInsertComercialVendas_Click);
            // 
            // btnDelPosicaoOps
            // 
            this.btnDelPosicaoOps.BackColor = System.Drawing.Color.Salmon;
            this.btnDelPosicaoOps.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelPosicaoOps.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelPosicaoOps.Location = new System.Drawing.Point(443, 491);
            this.btnDelPosicaoOps.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelPosicaoOps.Name = "btnDelPosicaoOps";
            this.btnDelPosicaoOps.Size = new System.Drawing.Size(210, 49);
            this.btnDelPosicaoOps.TabIndex = 37;
            this.btnDelPosicaoOps.Text = "Del Posicao OPs";
            this.btnDelPosicaoOps.UseVisualStyleBackColor = false;
            this.btnDelPosicaoOps.Click += new System.EventHandler(this.btnDelPosicaoOps_Click);
            // 
            // btnRenPosicaoOps
            // 
            this.btnRenPosicaoOps.BackColor = System.Drawing.Color.Salmon;
            this.btnRenPosicaoOps.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenPosicaoOps.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenPosicaoOps.Location = new System.Drawing.Point(443, 387);
            this.btnRenPosicaoOps.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenPosicaoOps.Name = "btnRenPosicaoOps";
            this.btnRenPosicaoOps.Size = new System.Drawing.Size(210, 47);
            this.btnRenPosicaoOps.TabIndex = 36;
            this.btnRenPosicaoOps.Text = "Ren Mov Posicao OPs";
            this.btnRenPosicaoOps.UseVisualStyleBackColor = false;
            this.btnRenPosicaoOps.Click += new System.EventHandler(this.btnRenPosicaoOps_Click);
            // 
            // btnInsertPosicaoOps
            // 
            this.btnInsertPosicaoOps.BackColor = System.Drawing.Color.Salmon;
            this.btnInsertPosicaoOps.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertPosicaoOps.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertPosicaoOps.Location = new System.Drawing.Point(443, 440);
            this.btnInsertPosicaoOps.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertPosicaoOps.Name = "btnInsertPosicaoOps";
            this.btnInsertPosicaoOps.Size = new System.Drawing.Size(210, 47);
            this.btnInsertPosicaoOps.TabIndex = 35;
            this.btnInsertPosicaoOps.Text = "Insert Posicao OPs";
            this.btnInsertPosicaoOps.UseVisualStyleBackColor = false;
            this.btnInsertPosicaoOps.Click += new System.EventHandler(this.btnInsertPosicaoOps_Click);
            // 
            // btnDelRecebimentos
            // 
            this.btnDelRecebimentos.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDelRecebimentos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelRecebimentos.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelRecebimentos.Location = new System.Drawing.Point(12, 668);
            this.btnDelRecebimentos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelRecebimentos.Name = "btnDelRecebimentos";
            this.btnDelRecebimentos.Size = new System.Drawing.Size(210, 49);
            this.btnDelRecebimentos.TabIndex = 40;
            this.btnDelRecebimentos.Text = "Del recebimentos";
            this.btnDelRecebimentos.UseVisualStyleBackColor = false;
            this.btnDelRecebimentos.Click += new System.EventHandler(this.btnDelRecebimentos_Click);
            // 
            // btnRenRecebimentos
            // 
            this.btnRenRecebimentos.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRenRecebimentos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenRecebimentos.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenRecebimentos.Location = new System.Drawing.Point(12, 564);
            this.btnRenRecebimentos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenRecebimentos.Name = "btnRenRecebimentos";
            this.btnRenRecebimentos.Size = new System.Drawing.Size(210, 47);
            this.btnRenRecebimentos.TabIndex = 39;
            this.btnRenRecebimentos.Text = "Ren Mov recebimentos";
            this.btnRenRecebimentos.UseVisualStyleBackColor = false;
            this.btnRenRecebimentos.Click += new System.EventHandler(this.btnRenRecebimentos_Click);
            // 
            // btnInsertRecebimentos
            // 
            this.btnInsertRecebimentos.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnInsertRecebimentos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertRecebimentos.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertRecebimentos.Location = new System.Drawing.Point(12, 617);
            this.btnInsertRecebimentos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertRecebimentos.Name = "btnInsertRecebimentos";
            this.btnInsertRecebimentos.Size = new System.Drawing.Size(210, 47);
            this.btnInsertRecebimentos.TabIndex = 38;
            this.btnInsertRecebimentos.Text = "Insert recebimentos";
            this.btnInsertRecebimentos.UseVisualStyleBackColor = false;
            this.btnInsertRecebimentos.Click += new System.EventHandler(this.btnInsertRecebimentos_Click);
            // 
            // btnDelPedidoCompra
            // 
            this.btnDelPedidoCompra.BackColor = System.Drawing.Color.Tan;
            this.btnDelPedidoCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelPedidoCompra.ForeColor = System.Drawing.Color.DimGray;
            this.btnDelPedidoCompra.Location = new System.Drawing.Point(228, 314);
            this.btnDelPedidoCompra.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelPedidoCompra.Name = "btnDelPedidoCompra";
            this.btnDelPedidoCompra.Size = new System.Drawing.Size(210, 49);
            this.btnDelPedidoCompra.TabIndex = 43;
            this.btnDelPedidoCompra.Text = "Del Pedido Compra";
            this.btnDelPedidoCompra.UseVisualStyleBackColor = false;
            this.btnDelPedidoCompra.Click += new System.EventHandler(this.btnDelPedidoCompra_Click);
            // 
            // btnRenPedidoCompra
            // 
            this.btnRenPedidoCompra.BackColor = System.Drawing.Color.Tan;
            this.btnRenPedidoCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenPedidoCompra.ForeColor = System.Drawing.Color.DimGray;
            this.btnRenPedidoCompra.Location = new System.Drawing.Point(228, 210);
            this.btnRenPedidoCompra.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRenPedidoCompra.Name = "btnRenPedidoCompra";
            this.btnRenPedidoCompra.Size = new System.Drawing.Size(210, 47);
            this.btnRenPedidoCompra.TabIndex = 42;
            this.btnRenPedidoCompra.Text = "Ren Pedido Compra";
            this.btnRenPedidoCompra.UseVisualStyleBackColor = false;
            this.btnRenPedidoCompra.Click += new System.EventHandler(this.btnRenPedidoCompra_Click);
            // 
            // btnInsertPedidoCompra
            // 
            this.btnInsertPedidoCompra.BackColor = System.Drawing.Color.Tan;
            this.btnInsertPedidoCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInsertPedidoCompra.ForeColor = System.Drawing.Color.DimGray;
            this.btnInsertPedidoCompra.Location = new System.Drawing.Point(228, 263);
            this.btnInsertPedidoCompra.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsertPedidoCompra.Name = "btnInsertPedidoCompra";
            this.btnInsertPedidoCompra.Size = new System.Drawing.Size(210, 47);
            this.btnInsertPedidoCompra.TabIndex = 41;
            this.btnInsertPedidoCompra.Text = "Insert Pedido Compra";
            this.btnInsertPedidoCompra.UseVisualStyleBackColor = false;
            this.btnInsertPedidoCompra.Click += new System.EventHandler(this.btnInsertPedidoCompra_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Salmon;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.DimGray;
            this.button1.Location = new System.Drawing.Point(443, 668);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 49);
            this.button1.TabIndex = 46;
            this.button1.Text = "----------";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Salmon;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.DimGray;
            this.button2.Location = new System.Drawing.Point(443, 564);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(210, 47);
            this.button2.TabIndex = 45;
            this.button2.Text = "----------";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Salmon;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.Color.DimGray;
            this.button3.Location = new System.Drawing.Point(443, 617);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(210, 47);
            this.button3.TabIndex = 44;
            this.button3.Text = "----------";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Aquamarine;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button4.ForeColor = System.Drawing.Color.DimGray;
            this.button4.Location = new System.Drawing.Point(657, 140);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(208, 47);
            this.button4.TabIndex = 49;
            this.button4.Text = "----------";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Aquamarine;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button5.ForeColor = System.Drawing.Color.DimGray;
            this.button5.Location = new System.Drawing.Point(657, 35);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(208, 47);
            this.button5.TabIndex = 48;
            this.button5.Text = "----------";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Aquamarine;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button6.ForeColor = System.Drawing.Color.DimGray;
            this.button6.Location = new System.Drawing.Point(657, 86);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(208, 47);
            this.button6.TabIndex = 47;
            this.button6.Text = "----------";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // UIPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 764);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnDelPedidoCompra);
            this.Controls.Add(this.btnRenPedidoCompra);
            this.Controls.Add(this.btnInsertPedidoCompra);
            this.Controls.Add(this.btnDelRecebimentos);
            this.Controls.Add(this.btnRenRecebimentos);
            this.Controls.Add(this.btnInsertRecebimentos);
            this.Controls.Add(this.btnDelPosicaoOps);
            this.Controls.Add(this.btnRenPosicaoOps);
            this.Controls.Add(this.btnInsertPosicaoOps);
            this.Controls.Add(this.btnDelComercialVendas);
            this.Controls.Add(this.btnRenComercialVendas);
            this.Controls.Add(this.btnInsertComercialVendas);
            this.Controls.Add(this.btnDelEstoqueRolos);
            this.Controls.Add(this.btnRenEstoqueRolos);
            this.Controls.Add(this.btnInsertEstoqueRolos);
            this.Controls.Add(this.btnDelSaldosEstoque);
            this.Controls.Add(this.btnRenSaldosEstoque);
            this.Controls.Add(this.btnInsertSaldosEstoque);
            this.Controls.Add(this.btnDelNotasFiscais);
            this.Controls.Add(this.btnRenNotasFiscais);
            this.Controls.Add(this.btnInsertNotasFiscais);
            this.Controls.Add(this.btnDelCaixasFios);
            this.Controls.Add(this.btnRenCaixasFios);
            this.Controls.Add(this.btnInsertCaixasFios);
            this.Controls.Add(this.btnDelContasPagas);
            this.Controls.Add(this.btnRenContasPagas);
            this.Controls.Add(this.btnInsertContasPagas);
            this.Controls.Add(this.btnDelContasPagar);
            this.Controls.Add(this.btnRenContasPagar);
            this.Controls.Add(this.btnInsertContasPagar);
            this.Controls.Add(this.btnDelContasReceber);
            this.Controls.Add(this.btnRenContasReceber);
            this.Controls.Add(this.btnInsertContasReceber);
            this.Controls.Add(this.btnDelShiftReport);
            this.Controls.Add(this.btnInsertDisplayOperacao);
            this.Controls.Add(this.btnCopyDisplayOperacao);
            this.Controls.Add(this.btnLerDisplayOperacao);
            this.Controls.Add(this.btnDelProduction);
            this.Controls.Add(this.btnRenShiftReport);
            this.Controls.Add(this.btnRenProduction);
            this.Controls.Add(this.btnProduction);
            this.Controls.Add(this.btnShiftReport);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "UIPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IntegraTêxtil";
            this.Load += new System.EventHandler(this.UIPrincipal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnShiftReport;
        private Button btnProduction;
        private Button btnRenProduction;
        private Button btnRenShiftReport;
        private Button btnDelProduction;
        private Button btnLerDisplayOperacao;
        private Button btnCopyDisplayOperacao;
        private Button btnInsertDisplayOperacao;
        private Button btnDelShiftReport;
        private System.Windows.Forms.Timer timerWebScrapingGeral;
        private Button btnDelContasReceber;
        private Button btnRenContasReceber;
        private Button btnInsertContasReceber;
        private Button btnDelContasPagar;
        private Button btnRenContasPagar;
        private Button btnInsertContasPagar;
        private Button btnDelContasPagas;
        private Button btnRenContasPagas;
        private Button btnInsertContasPagas;
        private Button btnDelCaixasFios;
        private Button btnRenCaixasFios;
        private Button btnInsertCaixasFios;
        private Button btnDelNotasFiscais;
        private Button btnRenNotasFiscais;
        private Button btnInsertNotasFiscais;
        private Button btnDelSaldosEstoque;
        private Button btnRenSaldosEstoque;
        private Button btnInsertSaldosEstoque;
        private Button btnDelEstoqueRolos;
        private Button btnRenEstoqueRolos;
        private Button btnInsertEstoqueRolos;
        private Button btnDelComercialVendas;
        private Button btnRenComercialVendas;
        private Button btnInsertComercialVendas;
        private Button btnDelPosicaoOps;
        private Button btnRenPosicaoOps;
        private Button btnInsertPosicaoOps;
        private Button btnDelRecebimentos;
        private Button btnRenRecebimentos;
        private Button btnInsertRecebimentos;
        private Button btnDelPedidoCompra;
        private Button btnRenPedidoCompra;
        private Button btnInsertPedidoCompra;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
    }
}