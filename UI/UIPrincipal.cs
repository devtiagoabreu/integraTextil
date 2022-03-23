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

            bllShiftReport.InserirShiftReport(bllShiftReport.LerShiftReportCSV());
        }

        private void btnProduction_Click(object sender, EventArgs e)
        {
            BLLProduction bllProduction = new BLLProduction();

            bllProduction.InserirProduction(bllProduction.LerProductionCSV());
        }
    }
}