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
            bllFerramentas.CriarArquivoZip(bllFerramentas.PegarNomesArquivo(@"C:\Apache2\htdocs\tmsdata\xlsdata\", ".csv"), @"D:\Teares\coletas\" + data + ".zip");
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
    }
}