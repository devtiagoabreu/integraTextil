using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLContasReceber
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
                    bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\dashboard\logs\logs.txt", "Sucesso: Relatório de contas a receber encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasReceber.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\dashboard\logs\logs.txt", "Erro: Relatório de contas a receber não encontrado, nome: nulo. Detalhes: Classe: BLLContasReceber.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: production movido e renomeado para pasta destino. Detalhes: bllProduction.RenomearArquivo() linha 114 | " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível mover e renomear production. Detalhes: bllProduction.RenomearArquivo() linha 121 | " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAOContasReceberList LerCSV(string path)
        {
            DAOContasReceberList daoContasReceberList = new DAOContasReceberList();
            StreamReader csv = new StreamReader(path, Encoding.UTF8);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOContasReceber daoContasReceber = new DAOContasReceber();
                campo = linha.Split(',');
                index++;

                if (index > 1)
                {
                    daoContasReceber.Duplicata = campo[0].ToString().Replace("/", "-").Replace("=", "").Replace("''", "");
                    daoContasReceber.Cliente = campo[1].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.TipoTitulo = campo[2].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.DataEmissao = campo[4].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.DataVencto = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.DataRecebido = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.DataProrrogacao = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.ValorDuplicata = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.SaldoDuplicata = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.Atraso = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoContasReceber.ValorJuros = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    
                    daoContasReceberList.Add(daoContasReceber);

                }
            }

            return daoContasReceberList;

        }

        public string InserirDadosBD(DAOProductionList daoProductionList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";

            try
            {
                DataTable dataTableProductionList = ConvertToDataTable(daoProductionList);
                foreach (DataRow linha in dataTableProductionList.Rows)
                {
                    DAOContasReceber daoContasReceber = new DAOContasReceber();

                    daoContasReceber.Duplicata = linha["Duplicata"].ToString();
                    daoContasReceber.Cliente = linha["Cliente"].ToString();
                    daoContasReceber.TipoTitulo = linha["TipoTitulo"].ToString();
                    daoContasReceber.DataEmissao = linha["DataEmissao"].ToString();
                    daoContasReceber.DataVencto = linha["DataVencto"].ToString();
                    daoContasReceber.DataRecebido = linha["DataRecebido"].ToString();
                    daoContasReceber.DataProrrogacao = linha["DataProrrogacao"].ToString();
                    daoContasReceber.ValorDuplicata = linha["ValorDuplicata"].ToString();
                    daoContasReceber.SaldoDuplicata = linha["SaldoDuplicata"].ToString();
                    daoContasReceber.Atraso = linha["Atraso"].ToString();
                    daoContasReceber.ValorJuros = linha["ValorJuros"].ToString();
                   
                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Duplicata", daoContasReceber.Duplicata);
                    dalMySQL.AdicionaParametros("@Cliente", daoContasReceber.Cliente);
                    dalMySQL.AdicionaParametros("@TipoTitulo", daoContasReceber.TipoTitulo);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.DataEmissao);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.DataVencto);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.DataRecebido);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.DataProrrogacao);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.ValorDuplicata);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.SaldoDuplicata);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.Atraso);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.ValorJuros);
                    
                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspInsertContasReceber");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\dashboard\logs\logs.txt", "Sucesso: Contas Receber inserida. Detalhes: BLLContasReceber.InserirDadosBD() | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\dashboard\logs\logs.txt", "Erro: não foi possível inserir Contas Receber. Detalhes: BLLContasReceber.InserirDadosBD() | " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: Relatório de Contas Receber deletadas. Detalhes: BLLContasReceber.DeletarArquivos() | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível deletar relatório de Contas Receber renomeada. Detalhes: BLLContasReceber.DeletarArquivos() | " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}
