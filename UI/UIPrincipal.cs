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

            //BLLDisplayOperacao bllDisplayOperacao = new BLLDisplayOperacao();



            #endregion
        }

        private void btnRenContasReceber_Click(object sender, EventArgs e)
        {
            BLLContasReceber bllContasReceber = new BLLContasReceber();

            bllContasReceber.RenomearArquivo(bllContasReceber.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "contas_receber.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\contas_receber\ren");
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

            bllContasPagar.RenomearArquivo(bllContasPagar.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "contas_pagar.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\contas_pagar\ren");
        
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

            bllContasPagas.RenomearArquivo(bllContasPagas.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "contas_pagas.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\contas_pagas\ren");
        
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

            bllCaixasFios.RenomearArquivo(bllCaixasFios.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "caixas_fios.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\caixas_fios\ren");
        
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

            bllNotasFiscais.RenomearArquivo(bllNotasFiscais.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "notas_fiscais.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\notas_fiscais\ren");
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

        private void btnRenSaldosEstoque_Click(object sender, EventArgs e)
        {
            BLLSaldosEstoque bllSaldosEstoque = new BLLSaldosEstoque();

            bllSaldosEstoque.RenomearArquivo(bllSaldosEstoque.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "saldos_estoque.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\saldos_estoque\ren");
        
        }

        private void btnInsertSaldosEstoque_Click(object sender, EventArgs e)
        {
            BLLSaldosEstoque bllSaldosEstoque = new BLLSaldosEstoque();

            bllSaldosEstoque.InserirDadosBD(bllSaldosEstoque.LerCsv(@"C:\integraTextil\relatorios\saldos_estoque\ren\saldos_estoque.csv"));
        
        }

        private void btnDelSaldosEstoque_Click(object sender, EventArgs e)
        {
            BLLSaldosEstoque bllSaldosEstoque = new BLLSaldosEstoque();

            bllSaldosEstoque.DeletarArquivos(@"C:\integraTextil\relatorios\saldos_estoque\ren\");
        
        }

        private void btnRenEstoqueRolos_Click(object sender, EventArgs e)
        {
            BLLEstoqueRolos bllEstoqueRolos = new BLLEstoqueRolos();

            bllEstoqueRolos.RenomearArquivo(bllEstoqueRolos.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "estoque_rolos.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\estoque_rolos\ren");
        
        }

        private void btnInsertEstoqueRolos_Click(object sender, EventArgs e)
        {
            BLLEstoqueRolos bllEstoqueRolos = new BLLEstoqueRolos();

            bllEstoqueRolos.InserirDadosBD(bllEstoqueRolos.LerCsv(@"C:\integraTextil\relatorios\estoque_rolos\ren\estoque_rolos.csv"));
        
        }

        private void btnDelEstoqueRolos_Click(object sender, EventArgs e)
        {
            BLLEstoqueRolos bllEstoqueRolos = new BLLEstoqueRolos();

            bllEstoqueRolos.DeletarArquivos(@"C:\integraTextil\relatorios\estoque_rolos\ren\");
        
        }

        private void UIPrincipal_Load(object sender, EventArgs e)
        {
            // NOTAS FISCAIS - ENTRADAS E SAÍDAS - FATURAMENTO
            //BLLNotasFiscais bllNotasFiscais = new BLLNotasFiscais();

            //bllNotasFiscais.RenomearArquivo(bllNotasFiscais.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "notas_fiscais.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\notas_fiscais\ren");

            //bllNotasFiscais.InserirDadosBD(bllNotasFiscais.LerCsv(@"C:\integraTextil\relatorios\notas_fiscais\ren\notas_fiscais.csv"));

            //bllNotasFiscais.DeletarArquivos(@"C:\integraTextil\relatorios\notas_fiscais\ren\");

            //BLLContasReceber bllContasReceber = new BLLContasReceber();

            //CONTAS A RECEBER
            //BLLContasReceber bllContasReceber = new BLLContasReceber();
            //bllContasReceber.RenomearArquivo(bllContasReceber.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "contas_receber.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\contas_receber\ren");

            //bllContasReceber.InserirDadosBD(bllContasReceber.LerCsvContasReceber(@"C:\integraTextil\relatorios\contas_receber\ren\contas_receber.csv"));

            //bllContasReceber.DeletarArquivos(@"C:\integraTextil\relatorios\contas_receber\ren\");

            //CONTAS A PAGAR
            //BLLContasPagar bllContasPagar = new BLLContasPagar();

            //bllContasPagar.RenomearArquivo(bllContasPagar.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "contas_pagar.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\contas_pagar\ren");

            //bllContasPagar.InserirDadosBD(bllContasPagar.LerCsv(@"C:\integraTextil\relatorios\contas_pagar\ren\contas_pagar.csv"));

            //bllContasPagar.DeletarArquivos(@"C:\integraTextil\relatorios\contas_pagar\ren\");

            //CONTAS PAGAS ( PAMENTOS - RATEIO POR CENTRO CUSTO)
            //BLLContasPagas bllContasPagas = new BLLContasPagas();

            //bllContasPagas.RenomearArquivo(bllContasPagas.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "contas_pagas.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\contas_pagas\ren");

            //bllContasPagas.InserirDadosBD(bllContasPagas.LerCsv(@"C:\integraTextil\relatorios\contas_pagas\ren\contas_pagas.csv"));

            //bllContasPagas.DeletarArquivos(@"C:\integraTextil\relatorios\contas_pagas\ren\");

            //CAIXA FIOS
            //BLLCaixasFios bllCaixasFios = new BLLCaixasFios();

            //bllCaixasFios.RenomearArquivo(bllCaixasFios.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "caixas_fios.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\caixas_fios\ren");

            //bllCaixasFios.InserirDadosBD(bllCaixasFios.LerCsv(@"C:\integraTextil\relatorios\caixas_fios\ren\caixas_fios.csv"));

            //bllCaixasFios.DeletarArquivos(@"C:\integraTextil\relatorios\caixas_fios\ren\");

            //SALDOS ESTOQUE
            //BLLSaldosEstoque bllSaldosEstoque = new BLLSaldosEstoque();

            //bllSaldosEstoque.RenomearArquivo(bllSaldosEstoque.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "saldos_estoque.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\saldos_estoque\ren");

            //bllSaldosEstoque.InserirDadosBD(bllSaldosEstoque.LerCsv(@"C:\integraTextil\relatorios\saldos_estoque\ren\saldos_estoque.csv"));

            //bllSaldosEstoque.DeletarArquivos(@"C:\integraTextil\relatorios\saldos_estoque\ren\");

            //ESTOQUE ROLOS
            // BLLEstoqueRolos bllEstoqueRolos = new BLLEstoqueRolos();

            //bllEstoqueRolos.RenomearArquivo(bllEstoqueRolos.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "estoque_rolos.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\estoque_rolos\ren");

            // bllEstoqueRolos.InserirDadosBD(bllEstoqueRolos.LerCsv(@"C:\integraTextil\relatorios\estoque_rolos\ren\estoque_rolos.csv"));

            //bllEstoqueRolos.DeletarArquivos(@"C:\integraTextil\relatorios\estoque_rolos\ren\");

            //COMERCIAL VENDAS
            //BLLComercialVendas bllComercialVendas = new BLLComercialVendas();

            //bllComercialVendas.RenomearArquivo(bllComercialVendas.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "comercial_vendas.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\comercial_vendas\ren");

            //bllComercialVendas.InserirDadosBD(bllComercialVendas.LerCsv(@"C:\integraTextil\relatorios\comercial_vendas\ren\comercial_vendas.csv"));

            //bllComercialVendas.DeletarArquivos(@"C:\integraTextil\relatorios\comercial_vendas\ren\");

            //POSIÇÃO OPS
            //BLLPosicaoOp bllPosicaoOp = new BLLPosicaoOp();

            //bllPosicaoOp.RenomearArquivo(bllPosicaoOp.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "posicao_op.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\posicao_op\ren");

            //bllPosicaoOp.InserirDadosBD(bllPosicaoOp.LerCsv(@"C:\integraTextil\relatorios\posicao_op\ren\posicao_op.csv"));

            //bllPosicaoOp.DeletarArquivos(@"C:\integraTextil\relatorios\posicao_op\ren\");

            //bllComercialVendas.InserirDadosBD(bllComercialVendas.LerCsv(@"C:\integraTextil\relatorios\comercial_vendas\ren\comercial_vendas.csv"));

            //bllComercialVendas.DeletarArquivos(@"C:\integraTextil\relatorios\comercial_vendas\ren\");

            //PEDIDO COMPRA - EXECUTA AUTOMATICAMENTE OS METODOS SEM EXIBIR NADA NA TELA
            
            BLLPedidoCompra bllPedidoCompra = new BLLPedidoCompra();

            bllPedidoCompra.RenomearArquivo(bllPedidoCompra.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "pedido_compra.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\pedido_compra\ren");

            bllPedidoCompra.InserirDadosBD(bllPedidoCompra.LerCsv(@"C:\integraTextil\relatorios\pedido_compra\ren\pedido_compra.csv"));

            bllPedidoCompra.DeletarArquivos(@"C:\integraTextil\relatorios\pedido_compra\ren\");

            Application.Exit();
        }

        private void btnRenComercialVendas_Click(object sender, EventArgs e)
        {
            BLLComercialVendas bllComercialVendas = new BLLComercialVendas();

            bllComercialVendas.RenomearArquivo(bllComercialVendas.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "comercial_vendas.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\comercial_vendas\ren");

        }

        private void btnInsertComercialVendas_Click(object sender, EventArgs e)
        {
            BLLComercialVendas bllComercialVendas = new BLLComercialVendas();

            bllComercialVendas.InserirDadosBD(bllComercialVendas.LerCsv(@"C:\integraTextil\relatorios\comercial_vendas\ren\comercial_vendas.csv"));

        }

        private void btnDelComercialVendas_Click(object sender, EventArgs e)
        {
            BLLComercialVendas bllComercialVendas = new BLLComercialVendas();

            bllComercialVendas.DeletarArquivos(@"C:\integraTextil\relatorios\comercial_vendas\ren\");

        }

        private void btnRenPosicaoOps_Click(object sender, EventArgs e)
        {
            BLLPosicaoOp bllPosicaoOp = new BLLPosicaoOp();

            bllPosicaoOp.RenomearArquivo(bllPosicaoOp.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "posicao_op.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\posicao_op\ren");

        }

        private void btnInsertPosicaoOps_Click(object sender, EventArgs e)
        {
            BLLPosicaoOp bllPosicaoOp = new BLLPosicaoOp();

            bllPosicaoOp.InserirDadosBD(bllPosicaoOp.LerCsv(@"C:\integraTextil\relatorios\posicao_op\ren\posicao_op.csv"));

        }

        private void btnDelPosicaoOps_Click(object sender, EventArgs e)
        {
            BLLPosicaoOp bllPosicaoOp = new BLLPosicaoOp();

            bllPosicaoOp.DeletarArquivos(@"C:\integraTextil\relatorios\posicao_op\ren\");

        }

        private void btnRenRecebimentos_Click(object sender, EventArgs e)
        {
            BLLRecebimentos bllRecebimentos = new BLLRecebimentos();

            bllRecebimentos.RenomearArquivo(bllRecebimentos.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "recebimentos.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\recebimentos\ren");

        
        }

        private void btnInsertRecebimentos_Click(object sender, EventArgs e)
        {
            BLLRecebimentos bllRecebimentos = new BLLRecebimentos();

            bllRecebimentos.InserirDadosBD(bllRecebimentos.LerCsv(@"C:\integraTextil\relatorios\recebimentos\ren\recebimentos.csv"));


        }

        private void btnDelRecebimentos_Click(object sender, EventArgs e)
        {
            BLLRecebimentos bllRecebimentos = new BLLRecebimentos();

            bllRecebimentos.DeletarArquivos(@"C:\integraTextil\relatorios\recebimentos\ren\");
        
        }

        private void btnRenPedidoCompra_Click(object sender, EventArgs e)
        {
            BLLPedidoCompra bllPedidoCompra = new BLLPedidoCompra();

            bllPedidoCompra.RenomearArquivo(bllPedidoCompra.PegarNomeArquivo(@"C:\integraTextil\relatorios\", "ROD"), "pedido_compra.csv", @"C:\integraTextil\relatorios\", @"C:\integraTextil\relatorios\pedido_compra\ren");


        }

        private void btnInsertPedidoCompra_Click(object sender, EventArgs e)
        {
            BLLPedidoCompra bllPedidoCompra = new BLLPedidoCompra();

            bllPedidoCompra.InserirDadosBD(bllPedidoCompra.LerCsv(@"C:\integraTextil\relatorios\pedido_compra\ren\pedido_compra.csv"));

        }

        private void btnDelPedidoCompra_Click(object sender, EventArgs e)
        {
            BLLPedidoCompra bllPedidoCompra = new BLLPedidoCompra();

            bllPedidoCompra.DeletarArquivos(@"C:\integraTextil\relatorios\pedido_compra\ren\");
        }
    }
}