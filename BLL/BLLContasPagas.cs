using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLContasPagas
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de contas Pagas encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de contas Pagas não encontrado, nome: nulo. Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: contas Pagas movido e renomeado para pasta destino. Detalhes: " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível mover e renomear contas Pagas. Detalhes: " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAOContasPagasList LerCsv(string path)
        {
            DAOContasPagasList daoContasPagasList = new DAOContasPagasList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOContasPagas daoContasPagas = new DAOContasPagas();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoContasPagas.Fornecedor = campo[0].ToString();
                    daoContasPagas.Duplicata = campo[1].ToString();
                    daoContasPagas.TipoTitulo = campo[2].ToString();
                    daoContasPagas.Portador = campo[3].ToString();
                    daoContasPagas.Posicao = campo[4].ToString();
                    daoContasPagas.CentroCusto = campo[5].ToString();
                    daoContasPagas.DataEmissao = Convert.ToDateTime(campo[6].ToString());
                    daoContasPagas.DataVencto = Convert.ToDateTime(campo[7].ToString());
                    daoContasPagas.DataPagto = Convert.ToDateTime(campo[8].ToString());
                    daoContasPagas.ValorParcela = Convert.ToDecimal(campo[9].ToString());
                    daoContasPagas.ValorPago = Convert.ToDecimal(campo[10].ToString());
                    daoContasPagas.ValorJuros = Convert.ToDecimal(campo[11].ToString());
                    daoContasPagas.ValorDesconto = Convert.ToDecimal(campo[12].ToString());
                    daoContasPagas.ValorAbatido = Convert.ToDecimal(campo[13].ToString());
                    daoContasPagas.SaldoParcela = Convert.ToDecimal(campo[14].ToString());

                    daoContasPagasList.Add(daoContasPagas);
                }
            }
            return daoContasPagasList;
        }

        public string InserirDadosBD(DAOContasPagasList daoContasPagasList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspContasPagasDeletar");

            try
            {
                DataTable dataTableContasPagasList = ConvertToDataTable(daoContasPagasList);
                foreach (DataRow linha in dataTableContasPagasList.Rows)
                {
                    DAOContasPagas daoContasPagas = new DAOContasPagas();

                    daoContasPagas.Fornecedor = linha["Fornecedor"].ToString();
                    daoContasPagas.Duplicata = linha["Duplicata"].ToString();
                    daoContasPagas.TipoTitulo = linha["TipoTitulo"].ToString();
                    daoContasPagas.Portador = linha["Portador"].ToString();
                    daoContasPagas.Posicao = linha["Posicao"].ToString();
                    daoContasPagas.CentroCusto = linha["CentroCusto"].ToString();
                    daoContasPagas.DataEmissao = Convert.ToDateTime(linha["DataEmissao"].ToString());
                    daoContasPagas.DataVencto = Convert.ToDateTime(linha["DataVencto"].ToString());
                    daoContasPagas.DataPagto = Convert.ToDateTime(linha["DataPagto"].ToString());
                    daoContasPagas.ValorParcela = Convert.ToDecimal(linha["ValorParcela"].ToString());
                    daoContasPagas.ValorPago = Convert.ToDecimal(linha["ValorPago"].ToString());
                    daoContasPagas.ValorJuros = Convert.ToDecimal(linha["ValorJuros"].ToString());
                    daoContasPagas.ValorDesconto = Convert.ToDecimal(linha["ValorDesconto"].ToString());
                    daoContasPagas.ValorAbatido = Convert.ToDecimal(linha["ValorAbatido"].ToString());
                    daoContasPagas.SaldoParcela = Convert.ToDecimal(linha["SaldoParcela"].ToString());

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Fornecedor", daoContasPagas.Fornecedor);
                    dalMySQL.AdicionaParametros("@Duplicata", daoContasPagas.Duplicata);
                    dalMySQL.AdicionaParametros("@TipoTitulo", daoContasPagas.TipoTitulo);
                    dalMySQL.AdicionaParametros("@Portador", daoContasPagas.Portador);
                    dalMySQL.AdicionaParametros("@Posicao", daoContasPagas.Posicao);
                    dalMySQL.AdicionaParametros("@CentroCusto", daoContasPagas.CentroCusto);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasPagas.DataEmissao);
                    dalMySQL.AdicionaParametros("@DataVencto", daoContasPagas.DataVencto);
                    dalMySQL.AdicionaParametros("@DataPagto", daoContasPagas.DataPagto);
                    dalMySQL.AdicionaParametros("@ValorParcela", daoContasPagas.ValorParcela);
                    dalMySQL.AdicionaParametros("@ValorPago", daoContasPagas.ValorPago);
                    dalMySQL.AdicionaParametros("@ValorJuros", daoContasPagas.ValorJuros);
                    dalMySQL.AdicionaParametros("@ValorDesconto", daoContasPagas.ValorDesconto);
                    dalMySQL.AdicionaParametros("@ValorAbatido", daoContasPagas.ValorAbatido);
                    dalMySQL.AdicionaParametros("@SaldoParcela", daoContasPagas.SaldoParcela);


                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspContasPagasInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Contas Pagas inserida. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir Contas Pagas. Detalhes: " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de Contas Pagas deletadas. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de Contas Pagas renomeada. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}
