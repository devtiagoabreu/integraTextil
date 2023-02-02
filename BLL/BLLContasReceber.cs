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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de contas a receber encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasReceber.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de contas a receber não encontrado, nome: nulo. Detalhes: Classe: BLLContasReceber.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: contas a receber movido e renomeado para pasta destino. Detalhes: " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível mover e renomear contas a receber. Detalhes: " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAOContasReceberList LerCsvContasReceber(string path)
        {
            DAOContasReceberList daoContasReceberList = new DAOContasReceberList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOContasReceber daoContasReceber = new DAOContasReceber();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoContasReceber.Cliente = campo[0].ToString();
                    daoContasReceber.Representante = campo[1].ToString();
                    daoContasReceber.Duplicata = campo[2].ToString();
                    daoContasReceber.TipoTitulo = campo[3].ToString();
                    daoContasReceber.Portador = campo[4].ToString();
                    daoContasReceber.Posicao = campo[5].ToString();
                    daoContasReceber.DataEmissao = Convert.ToDateTime(campo[6].ToString());
                    daoContasReceber.DataVencto = Convert.ToDateTime(campo[7].ToString());
                    daoContasReceber.DataProrrogacao = Convert.ToDateTime(campo[8].ToString());
                    daoContasReceber.ValorDuplicata = Convert.ToDecimal(campo[9].ToString());
                    daoContasReceber.SaldoDuplicata = Convert.ToDecimal(campo[10].ToString());
                    daoContasReceber.Atraso = Convert.ToInt32(campo[11].ToString());
                    
                    daoContasReceberList.Add(daoContasReceber);
                }
            }
            return daoContasReceberList;
        }

        public string InserirDadosBD(DAOContasReceberList daoContasReceberList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspContasReceberDeletar");

            try
            {
                DataTable dataTableContasReceberList = ConvertToDataTable(daoContasReceberList);
                foreach (DataRow linha in dataTableContasReceberList.Rows)
                {
                    DAOContasReceber daoContasReceber = new DAOContasReceber();

                    daoContasReceber.Cliente = linha["Cliente"].ToString();
                    daoContasReceber.Representante = linha["Representante"].ToString();
                    daoContasReceber.Duplicata = linha["Duplicata"].ToString();
                    daoContasReceber.TipoTitulo = linha["TipoTitulo"].ToString();
                    daoContasReceber.Portador = linha["Portador"].ToString();
                    daoContasReceber.Posicao = linha["Posicao"].ToString();
                    daoContasReceber.DataEmissao = Convert.ToDateTime(linha["DataEmissao"].ToString());
                    daoContasReceber.DataVencto = Convert.ToDateTime(linha["DataVencto"].ToString());
                    daoContasReceber.DataProrrogacao = Convert.ToDateTime(linha["DataProrrogacao"].ToString());
                    daoContasReceber.ValorDuplicata = Convert.ToDecimal(linha["ValorDuplicata"].ToString());
                    daoContasReceber.SaldoDuplicata = Convert.ToDecimal(linha["SaldoDuplicata"].ToString());
                    daoContasReceber.Atraso = Convert.ToInt32(linha["Atraso"].ToString());
                    
                   
                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Cliente", daoContasReceber.Cliente);
                    dalMySQL.AdicionaParametros("@Representante", daoContasReceber.Representante);
                    dalMySQL.AdicionaParametros("@Duplicata", daoContasReceber.Duplicata);
                    dalMySQL.AdicionaParametros("@TipoTitulo", daoContasReceber.TipoTitulo);
                    dalMySQL.AdicionaParametros("@Portador", daoContasReceber.Portador);
                    dalMySQL.AdicionaParametros("@Posicao", daoContasReceber.Posicao);
                    dalMySQL.AdicionaParametros("@DataEmissao", daoContasReceber.DataEmissao);
                    dalMySQL.AdicionaParametros("@DataVencto", daoContasReceber.DataVencto);
                    dalMySQL.AdicionaParametros("@DataProrrogacao", daoContasReceber.DataProrrogacao);
                    dalMySQL.AdicionaParametros("@ValorDuplicata", daoContasReceber.ValorDuplicata);
                    dalMySQL.AdicionaParametros("@SaldoDuplicata", daoContasReceber.SaldoDuplicata);
                    dalMySQL.AdicionaParametros("@Atraso", daoContasReceber.Atraso);
                                        
                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspContasReceberInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Contas Receber inserida. Detalhes: BLLContasReceber.InserirDadosBD() | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir Contas Receber. Detalhes: BLLContasReceber.InserirDadosBD() | " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de Contas Receber deletadas. Detalhes: BLLContasReceber.DeletarArquivos() | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de Contas Receber renomeada. Detalhes: BLLContasReceber.DeletarArquivos() | " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}
