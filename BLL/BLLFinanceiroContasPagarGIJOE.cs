using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLFinanceiroContasPagarGIJOE
    {
        #region ATRIBUTOS | OBJETOS

        DALMySQL dalMySQL = new DALMySQL();

        string data = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        #endregion

        #region MÉTODOS


        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public string PegarNomeArquivo(string pasta, string parteNomeArquivo)
        {
            string arquivoNome = "";

            BLLFerramentas bllFerramentas = new BLLFerramentas();

            try
            {
                // Take a snapshot of the file system.  
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(pasta);

                // This method assumes that the application has discovery permissions  
                // for all folders under the specified path.  
                IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                //Create the query  
                IEnumerable<System.IO.FileInfo> fileQuery =
                    from file in fileList
                        //where file.Extension == ".csv"
                    where file.Name.Contains(parteNomeArquivo)
                    orderby file.Name
                    select file;

                //Execute the query. This might write out a lot of files!  
                foreach (System.IO.FileInfo fi in fileQuery)
                {
                    arquivoNome = fi.Name;
                }

                if (arquivoNome.Equals(""))
                {
                    Convert.ToInt32(arquivoNome);
                }
                else
                {
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de contas a pagar GIJOE encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasReceber.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de contas a pagar GIJOE não encontrado, nome: nulo. Detalhes: Classe: BLLContasReceber.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
            }

            return arquivoNome;

        }

        public string RenomearArquivo(string arquivoNome, string arquivoNomeFinal, string pastaOrigem, string pastaDestino)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";

            try
            {
                if (arquivoNome.Equals(""))
                {
                    Convert.ToInt32(arquivoNome);
                    retorno = "Arquivo não foi encontrado na pasta Origem";
                }
                else
                {
                    string[] arquivos = Directory.GetFiles(pastaOrigem);
                    string dirSaida = pastaDestino;

                    if (!Directory.Exists(dirSaida))
                        Directory.CreateDirectory(dirSaida);

                    for (int i = 0; i < arquivos.Length; i++)
                    {

                        if (arquivos[i].Equals(pastaOrigem + arquivoNome))
                        {
                            var files = new FileInfo(arquivos[i]);
                            files.MoveTo(Path.Combine(dirSaida, files.Name.Replace(arquivoNome, arquivoNomeFinal)));
                        }

                    }

                    retorno = "ok";
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: contas a pagar movido e renomeado para pasta destino. Detalhes: " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível mover e renomear contas a pagar. Detalhes: " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAOFinanceiroContasPagarGIJOEList LerCsv(string path)
        {
            DAOFinanceiroContasPagarGIJOEList daoFinanceiroContasPagarGIJOEList = new DAOFinanceiroContasPagarGIJOEList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOFinanceiroContasPagarGIJOE daoFinanceiroContasPagarGIJOE = new DAOFinanceiroContasPagarGIJOE();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoFinanceiroContasPagarGIJOE.Empresa = campo[0].ToString();
                    try {
                        daoFinanceiroContasPagarGIJOE.Duplicata = Convert.ToInt32(campo[1]);
                    }
                    catch (Exception ex) {
                        daoFinanceiroContasPagarGIJOE.Duplicata = 999999999;
                        MessageBox.Show("O campo Duplicata foi preenchido com o valor: " + daoFinanceiroContasPagarGIJOE.Duplicata.ToString() + " - " + ex.Message);
                    }
                    daoFinanceiroContasPagarGIJOE.Parcela = campo[2].ToString();
                    daoFinanceiroContasPagarGIJOE.DataContrato = Convert.ToDateTime(campo[3]);
                    daoFinanceiroContasPagarGIJOE.TipoTitulo = campo[4].ToString();
                    daoFinanceiroContasPagarGIJOE.Documento = Convert.ToInt32(campo[5]);
                    daoFinanceiroContasPagarGIJOE.Serie = campo[6].ToString();
                    daoFinanceiroContasPagarGIJOE.Historico = campo[7].ToString();
                    daoFinanceiroContasPagarGIJOE.EmpresaCobranca = campo[8].ToString();
                    daoFinanceiroContasPagarGIJOE.CodContabil = campo[9].ToString();
                    daoFinanceiroContasPagarGIJOE.CodFornecedor = campo[10].ToString();
                    daoFinanceiroContasPagarGIJOE.NomeFornecedor = campo[11].ToString();
                    daoFinanceiroContasPagarGIJOE.TipoFornecedor = campo[12].ToString();
                    daoFinanceiroContasPagarGIJOE.DataTransacao = Convert.ToDateTime(campo[13]);
                    daoFinanceiroContasPagarGIJOE.Previsao = campo[14].ToString();
                    daoFinanceiroContasPagarGIJOE.Portador = Convert.ToInt32(campo[15]);
                    daoFinanceiroContasPagarGIJOE.VencimentoOrig = campo[16].ToString();
                    daoFinanceiroContasPagarGIJOE.Vencimento = Convert.ToDateTime(campo[17]);
                    daoFinanceiroContasPagarGIJOE.Posicao = campo[18].ToString();
                    daoFinanceiroContasPagarGIJOE.NumContabil = Convert.ToInt32(campo[19]);
                    daoFinanceiroContasPagarGIJOE.OrigemDebito = campo[20].ToString();
                    daoFinanceiroContasPagarGIJOE.SituacaoTitulo = campo[21].ToString();
                    daoFinanceiroContasPagarGIJOE.SituacaoSispag = campo[22].ToString();
                    daoFinanceiroContasPagarGIJOE.TipoPagamento = campo[23].ToString();
                    daoFinanceiroContasPagarGIJOE.CodigoBarras = campo[24].ToString();
                    daoFinanceiroContasPagarGIJOE.Moeda = campo[25].ToString();
                    daoFinanceiroContasPagarGIJOE.ValorTitulo = Convert.ToDecimal(campo[26]);
                    daoFinanceiroContasPagarGIJOE.SaldoTitulo = Convert.ToDecimal(campo[27]);
                    daoFinanceiroContasPagarGIJOE.Tran = Convert.ToInt32(campo[28]);
                    daoFinanceiroContasPagarGIJOE.Transacao = campo[29].ToString();
                    daoFinanceiroContasPagarGIJOE.CCusto = Convert.ToInt32(campo[30]);
                    daoFinanceiroContasPagarGIJOE.CentroCusto = campo[31].ToString();
                    daoFinanceiroContasPagarGIJOE.CentroCustoPai = Convert.ToInt32(campo[32]);
                    daoFinanceiroContasPagarGIJOE.ValorCcusto = Convert.ToDecimal(campo[33]);
                    daoFinanceiroContasPagarGIJOE.CodContabilRateio =campo[34].ToString();
                    daoFinanceiroContasPagarGIJOE.MesAnoVencimento = campo[35].ToString();
                    daoFinanceiroContasPagarGIJOE.Pd = campo[36].ToString();
                    daoFinanceiroContasPagarGIJOE.Previsao2 = campo[37].ToString();
                    daoFinanceiroContasPagarGIJOE.CentrosCustoTecelagem = campo[38].ToString();
                    daoFinanceiroContasPagarGIJOE.CentrosCustoValorPonto = campo[39].ToString();
                    daoFinanceiroContasPagarGIJOE.CentrosCustoBeneficiamento = campo[40].ToString();

                    daoFinanceiroContasPagarGIJOEList.Add(daoFinanceiroContasPagarGIJOE);
                }
            }
            return daoFinanceiroContasPagarGIJOEList;
        }

        public string InserirDadosBD(DAOFinanceiroContasPagarGIJOEList daoFinanceiroContasPagarGIJOEList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspFinanceiroContasPagarGIJOEDeletar");

            try
            {
                foreach (DAOFinanceiroContasPagarGIJOE daoFinanceiroContasPagarGIJOE in daoFinanceiroContasPagarGIJOEList)
                {
                    DAOContasPagar daoContasPagar = new DAOContasPagar();

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Empresa", daoFinanceiroContasPagarGIJOE.Empresa);
                    dalMySQL.AdicionaParametros("@Duplicata", daoFinanceiroContasPagarGIJOE.Duplicata);
                    dalMySQL.AdicionaParametros("@Parcela", daoFinanceiroContasPagarGIJOE.Parcela);
                    dalMySQL.AdicionaParametros("@DataContrato", daoFinanceiroContasPagarGIJOE.DataContrato);
                    dalMySQL.AdicionaParametros("@TipoTitulo", daoFinanceiroContasPagarGIJOE.TipoTitulo);
                    dalMySQL.AdicionaParametros("@Documento", daoFinanceiroContasPagarGIJOE.Documento);
                    dalMySQL.AdicionaParametros("@Serie", daoFinanceiroContasPagarGIJOE.Serie);
                    dalMySQL.AdicionaParametros("@Historico", daoFinanceiroContasPagarGIJOE.Historico);
                    dalMySQL.AdicionaParametros("@EmpresaCobranca", daoFinanceiroContasPagarGIJOE.EmpresaCobranca);
                    dalMySQL.AdicionaParametros("@CodContabil", daoFinanceiroContasPagarGIJOE.CodContabil);
                    dalMySQL.AdicionaParametros("@CodFornecedor", daoFinanceiroContasPagarGIJOE.CodFornecedor);
                    dalMySQL.AdicionaParametros("@NomeFornecedor", daoFinanceiroContasPagarGIJOE.NomeFornecedor);
                    dalMySQL.AdicionaParametros("@TipoFornecedor", daoFinanceiroContasPagarGIJOE.TipoFornecedor);
                    dalMySQL.AdicionaParametros("@DataTransacao", daoFinanceiroContasPagarGIJOE.DataTransacao);
                    dalMySQL.AdicionaParametros("@Previsao", daoFinanceiroContasPagarGIJOE.Previsao);
                    dalMySQL.AdicionaParametros("@Portador", daoFinanceiroContasPagarGIJOE.Portador);
                    dalMySQL.AdicionaParametros("@VencimentoOrig", daoFinanceiroContasPagarGIJOE.VencimentoOrig);
                    dalMySQL.AdicionaParametros("@Vencimento", daoFinanceiroContasPagarGIJOE.Vencimento);
                    dalMySQL.AdicionaParametros("@Posicao", daoFinanceiroContasPagarGIJOE.Posicao);
                    dalMySQL.AdicionaParametros("@NumContabil", daoFinanceiroContasPagarGIJOE.NumContabil);
                    dalMySQL.AdicionaParametros("@OrigemDebito", daoFinanceiroContasPagarGIJOE.OrigemDebito);
                    dalMySQL.AdicionaParametros("@SituacaoTitulo", daoFinanceiroContasPagarGIJOE.SituacaoTitulo);
                    dalMySQL.AdicionaParametros("@SituacaoSispag", daoFinanceiroContasPagarGIJOE.SituacaoSispag);
                    dalMySQL.AdicionaParametros("@TipoPagamento", daoFinanceiroContasPagarGIJOE.TipoPagamento);
                    dalMySQL.AdicionaParametros("@CodigoBarras", daoFinanceiroContasPagarGIJOE.CodigoBarras);
                    dalMySQL.AdicionaParametros("@Moeda", daoFinanceiroContasPagarGIJOE.Moeda);
                    dalMySQL.AdicionaParametros("@ValorTitulo", daoFinanceiroContasPagarGIJOE.ValorTitulo);
                    dalMySQL.AdicionaParametros("@SaldoTitulo", daoFinanceiroContasPagarGIJOE.SaldoTitulo);
                    dalMySQL.AdicionaParametros("@Tran", daoFinanceiroContasPagarGIJOE.Tran);
                    dalMySQL.AdicionaParametros("@Transacao", daoFinanceiroContasPagarGIJOE.Transacao);
                    dalMySQL.AdicionaParametros("@CCusto", daoFinanceiroContasPagarGIJOE.CCusto);
                    dalMySQL.AdicionaParametros("@CentroCusto", daoFinanceiroContasPagarGIJOE.CentroCusto);
                    dalMySQL.AdicionaParametros("@CentroCustoPai", daoFinanceiroContasPagarGIJOE.CentroCustoPai);
                    dalMySQL.AdicionaParametros("@ValorCcusto", daoFinanceiroContasPagarGIJOE.ValorCcusto);
                    dalMySQL.AdicionaParametros("@CodContabilRateio", daoFinanceiroContasPagarGIJOE.CodContabilRateio);
                    dalMySQL.AdicionaParametros("@MesAnoVencimento", daoFinanceiroContasPagarGIJOE.MesAnoVencimento);
                    dalMySQL.AdicionaParametros("@Pd", daoFinanceiroContasPagarGIJOE.Pd);
                    dalMySQL.AdicionaParametros("@Previsao2", daoFinanceiroContasPagarGIJOE.Previsao2);
                    dalMySQL.AdicionaParametros("@CentrosCustoTecelagem", daoFinanceiroContasPagarGIJOE.CentrosCustoTecelagem);
                    dalMySQL.AdicionaParametros("@CentrosCustoValorPonto", daoFinanceiroContasPagarGIJOE.CentrosCustoValorPonto);
                    dalMySQL.AdicionaParametros("@CentrosCustoBeneficiamento", daoFinanceiroContasPagarGIJOE.CentrosCustoBeneficiamento);

                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspFinanceiroContasPagarGIJOEInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Contas a Pagar GIJOE inserida. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir Contas a Pagar GIJOE. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }

        public string DeletarArquivos(string path)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            try
            {
                //System.IO.Directory.Delete(path, true);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de Contas Pagar deletadas. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de Contas Pagar renomeada. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }


        #endregion
    }
}
