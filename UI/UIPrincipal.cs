using BLL;

namespace UI
{
    public partial class UIPrincipal : Form
    {
        public UIPrincipal()
        {
            InitializeComponent();
        }

        private void btnShiftReport_Click(object sender, EventArgs e)
        {
            BLLShiftReport bllShiftReport = new BLLShiftReport();

            bllShiftReport.InserirShiftReport(bllShiftReport.LerCSV(@"D:\Teares\shiftReportRenomeada\shiftReport.csv"));
        }

        private void btnProduction_Click(object sender, EventArgs e)
        {
            BLLProduction bllProduction = new BLLProduction();

            bllProduction.InserirProduction(bllProduction.LerCSV(@"D:\Teares\productionRenomeada\production.csv"));
        }

        private void btnRenProduction_Click(object sender, EventArgs e)
        {
            BLLProduction bllProduction = new BLLProduction();

            bllProduction.RenomearArquivo(bllProduction.PegarNomeArquivo(@"C:\Apache2\htdocs\tmsdata\xlsdata\", "production"), "production.csv", @"C:\Apache2\htdocs\tmsdata\xlsdata\", @"D:\Teares\productionRenomeada\");
        }

        private void btnRenShiftReport_Click(object sender, EventArgs e)
        {
            BLLShiftReport bllShiftReport = new BLLShiftReport();

            bllShiftReport.RenomearArquivo(bllShiftReport.PegarNomeArquivo(@"C:\Apache2\htdocs\tmsdata\xlsdata\", "shiftreport"), "shiftReport.csv", @"C:\Apache2\htdocs\tmsdata\xlsdata\", @"D:\Teares\shiftReportRenomeada\");
        }

        private void btnZipCsv_Click(object sender, EventArgs e)
        {
            string data = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss" );
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            //bllFerramentas.CriarArquivoZip(bllFerramentas.PegarNomesArquivo(@"C:\Apache2\htdocs\tmsdata\xlsdata\", ".csv"), @"D:\Teares\coletas\" + data + ".zip");
        }

        private void btnDelProduction_Click(object sender, EventArgs e)
        {
            BLLProduction bllProduction = new BLLProduction();

            bllProduction.DeletarArquivos(@"D:\Teares\productionRenomeada\");
        }

        private void btnLerDisplayOperacao_Click(object sender, EventArgs e)
        {
            BLLDisplayOperacao bllDisplayOperacao = new BLLDisplayOperacao();
            
        }

        private void btnCopyDisplayOperacao_Click(object sender, EventArgs e)
        {
            BLLDisplayOperacao bllDisplayOperacao = new BLLDisplayOperacao();

            bllDisplayOperacao.CopiarArquivo("displayOperacao.xlsx", @"D:\Teares\ods\", @"D:\Teares\ods\import\");//
        }

        private void btnInsertDisplayOperacao_Click(object sender, EventArgs e)
        {
            BLLDisplayOperacao bllDisplayOperacao = new BLLDisplayOperacao();

        }

        private void btnDelShiftReport_Click(object sender, EventArgs e)
        {
            BLLShiftReport bllShiftReport = new BLLShiftReport();

            bllShiftReport.DeletarArquivos(@"D:\Teares\shiftReportRenomeada\");
        }

        private void btnWebScrapingDisplayOperacao_Click(object sender, EventArgs e)
        {
            BLLDisplayOperacao bllDisplayOperacao = new BLLDisplayOperacao();
            
        }

        private void timerWebScrapingGeral_Tick(object sender, EventArgs e)
        {
            #region DISPLAY DE OPERAÇÃO

            BLLDisplayOperacao bllDisplayOperacao = new BLLDisplayOperacao();

            #endregion
        }

        private void btnRenContasReceber_Click(object sender, EventArgs e)
        {
            BLLContasReceber bllContasReceber = new BLLContasReceber();

            bllContasReceber.RenomearArquivo(bllContasReceber.PegarNomeArquivo(@"C:\integraTextil\relatorios\contas_receber\", "ROD"), "contas_receber.csv", @"C:\integraTextil\relatorios\contas_receber\", @"C:\integraTextil\relatorios\contas_receber\ren");
        }

        private void btnInsertContasReceber_Click(object sender, EventArgs e)
        {
            BLLContasReceber bllContasReceber = new BLLContasReceber();

            bllContasReceber.InserirDadosBD(bllContasReceber.LerCsvContasReceber(@"C:\integraTextil\relatorios\contas_receber\ren\contas_receber.csv"));
        }

        private void btnDelContasReceber_Click(object sender, EventArgs e)
        {
            BLLContasReceber bllContasReceber = new BLLContasReceber();

            bllContasReceber.DeletarArquivos(@"C:\integraTextil\relatorios\contas_receber\ren\");
        }

        private void btnRenContasPagar_Click(object sender, EventArgs e)
        {
            BLLContasPagar bllContasPagar = new BLLContasPagar();

            bllContasPagar.RenomearArquivo(bllContasPagar.PegarNomeArquivo(@"C:\integraTextil\relatorios\contas_pagar\", "ROD"), "contas_pagar.csv", @"C:\integraTextil\relatorios\contas_pagar\", @"C:\integraTextil\relatorios\contas_pagar\ren");
        }

        private void btnInsertContasPagar_Click(object sender, EventArgs e)
        {
            BLLContasPagar bllContasPagar = new BLLContasPagar();

            bllContasPagar.InserirDadosBD(bllContasPagar.LerCsv(@"C:\integraTextil\relatorios\contas_pagar\ren\contas_pagar.csv"));
        }

        private void btnDelContasPagar_Click(object sender, EventArgs e)
        {
            BLLContasPagar bllContasPagar = new BLLContasPagar();

            bllContasPagar.DeletarArquivos(@"C:\integraTextil\relatorios\contas_pagar\ren\");
        }

        private void btnRenContasPagas_Click(object sender, EventArgs e)
        {
            BLLContasPagas bllContasPagas = new BLLContasPagas();

            bllContasPagas.RenomearArquivo(bllContasPagas.PegarNomeArquivo(@"C:\integraTextil\relatorios\contas_pagas\", "ROD"), "contas_pagas.csv", @"C:\integraTextil\relatorios\contas_pagas\", @"C:\integraTextil\relatorios\contas_pagas\ren");
        }

        private void btnInsertContasPagas_Click(object sender, EventArgs e)
        {
            BLLContasPagas bllContasPagas = new BLLContasPagas();

            bllContasPagas.InserirDadosBD(bllContasPagas.LerCsv(@"C:\integraTextil\relatorios\contas_pagas\ren\contas_pagas.csv"));
        }

        private void btnDelContasPagas_Click(object sender, EventArgs e)
        {
            BLLContasPagas bllContasPagas = new BLLContasPagas();

            bllContasPagas.DeletarArquivos(@"C:\integraTextil\relatorios\contas_pagas\ren\");
        }

        private void btnRenCaixasFios_Click(object sender, EventArgs e)
        {
            BLLCaixasFios bllCaixasFios = new BLLCaixasFios();

            bllCaixasFios.RenomearArquivo(bllCaixasFios.PegarNomeArquivo(@"C:\integraTextil\relatorios\caixas_fios\", "ROD"), "caixas_fios.csv", @"C:\integraTextil\relatorios\caixas_fios\", @"C:\integraTextil\relatorios\caixas_fios\ren");
        }

        private void btnInsertCaixasFios_Click(object sender, EventArgs e)
        {
            BLLCaixasFios bllCaixasFios = new BLLCaixasFios();

            bllCaixasFios.InserirDadosBD(bllCaixasFios.LerCsv(@"C:\integraTextil\relatorios\caixas_fios\ren\caixas_fios.csv"));
        }

        private void btnDelCaixasFios_Click(object sender, EventArgs e)
        {
            BLLCaixasFios bllCaixasFios = new BLLCaixasFios();

            bllCaixasFios.DeletarArquivos(@"C:\integraTextil\relatorios\caixas_fios\ren\");
        }

        private void btnRenNotasFiscais_Click(object sender, EventArgs e)
        {
            BLLNotasFiscais bllNotasFiscais = new BLLNotasFiscais();

            bllNotasFiscais.RenomearArquivo(bllNotasFiscais.PegarNomeArquivo(@"C:\integraTextil\relatorios\notas_fiscais\", "ROD"), "notas_fiscais.csv", @"C:\integraTextil\relatorios\notas_fiscais\", @"C:\integraTextil\relatorios\notas_fiscais\ren");
        }

        private void btnInsertNotasFiscais_Click(object sender, EventArgs e)
        {
            BLLNotasFiscais bllNotasFiscais = new BLLNotasFiscais();

            bllNotasFiscais.InserirDadosBD(bllNotasFiscais.LerCsv(@"C:\integraTextil\relatorios\notas_fiscais\ren\notas_fiscais.csv"));
        }

        private void btnDelNotasFiscais_Click(object sender, EventArgs e)
        {
            BLLNotasFiscais bllNotasFiscais = new BLLNotasFiscais();

            bllNotasFiscais.DeletarArquivos(@"C:\integraTextil\relatorios\notas_fiscais\ren\");
        }
    }
}