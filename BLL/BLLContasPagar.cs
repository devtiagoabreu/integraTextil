using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLContasPagar
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de contas a pagar encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasReceber.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de contas a pagar não encontrado, nome: nulo. Detalhes: Classe: BLLContasReceber.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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

        public DAOContasPagarList LerCsv(string path)
        {
            DAOContasPagarList daoContasPagarList = new DAOContasPagarList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOContasPagar daoContasPagar = new DAOContasPagar();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoContasPagar.Fornecedor = campo[0].ToString();
                    daoContasPagar.Duplicata = campo[1].ToString();
                    daoContasPagar.TipoTitulo = campo[2].ToString();
                    daoContasPagar.Portador = campo[3].ToString();
                    daoContasPagar.Posicao = campo[4].ToString();
                    daoContasPagar.CentroCusto = campo[5].ToString();
                    daoContasPagar.DataEmissao = Convert.ToDateTime(campo[6].ToString());
                    daoContasPagar.DataTransacao = Convert.ToDateTime(campo[7].ToString());
                    daoContasPagar.DataVencto = Convert.ToDateTime(campo[8].ToString());
                    daoContasPagar.ValorParcela = Convert.ToDecimal(campo[9].ToString());
                    daoContasPagar.SaldoParcela = Convert.ToDecimal(campo[10].ToString());
                    
                    daoContasPagarList.Add(daoContasPagar);
                }
            }
            return daoContasPagarList;
        }

        public string InserirDadosBD(DAOContasPagarList daoContasPagarList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspContasPagarDeletar");

            try
            {
                DataTable dataTableContasPagarList = ConvertToDataTable(daoContasPagarList);
                foreach (DataRow linha in dataTableContasPagarList.Rows)
                {
                    DAOContasPagar daoContasPagar = new DAOContasPagar();

                    daoContasPagar.Fornecedor = linha["Fornecedor"].ToString();
                    daoContasPagar.Duplicata = linha["Duplicata"].ToString();
                    daoContasPagar.TipoTitulo = linha["TipoTitulo"].ToString();
                    daoContasPagar.Portador = linha["Portador"].ToString();
                    daoContasPagar.Posicao = linha["Posicao"].ToString();
                    daoContasPagar.CentroCusto = linha["CentroCusto"].ToString();
                    daoContasPagar.DataEmissao = Convert.ToDateTime(linha["DataEmissao"].ToString());
                    daoContasPagar.DataTransacao = Convert.ToDateTime(linha["DataTransacao"].ToString());
                    daoContasPagar.DataVencto = Convert.ToDateTime(linha["DataVencto"].ToString());
                    daoContasPagar.ValorParcela = Convert.ToDecimal(linha["ValorParcela"].ToString());
                    daoContasPagar.SaldoParcela = Convert.ToDecimal(linha["SaldoParcela"].ToString());
                    
                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Fornecedor", daoContasPagar.Fornecedor);
                    dalMySQL.AdicionaParametros("@Duplicata", daoContasPagar.Duplicata);
                    dalMySQL.AdicionaParametros("@TipoTitulo", daoContasPagar.TipoTitulo);
                    dalMySQL.AdicionaParametros("@Portador", daoContasPagar.Portador);
                    dalMySQL.AdicionaParametros("@Posicao", daoContasPagar.Posicao);
                    dalMySQL.AdicionaParametros("@CentroCusto", daoContasPagar.CentroCusto);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasPagar.DataEmissao);
                    dalMySQL.AdicionaParametros("@DataTransacao", daoContasPagar.DataTransacao);
                    dalMySQL.AdicionaParametros("@DataVencto", daoContasPagar.DataVencto);
                    dalMySQL.AdicionaParametros("@ValorParcela", daoContasPagar.ValorParcela);
                    dalMySQL.AdicionaParametros("@SaldoParcela", daoContasPagar.SaldoParcela);
                    
                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspContasPagarInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Contas Pagar inserida. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir Contas Pagar. Detalhes: " + retorno + " | " + data);
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
