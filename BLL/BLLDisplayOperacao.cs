using System.Text;
using System.ComponentModel;
using System.Data;
using System.Net;
using DAL;
using DAO;

namespace BLL
{
    public class BLLDisplayOperacao
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
                    bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: displayOperacao encontrado, nome: " + arquivoNome + ".  Detalhes: bllDisplayOperacao.PegarNomeArquivo() linha 70 | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: displayOperacao não encontrado, nome: nulo. Detalhes: bllDisplayOperacao.PegarNomeArquivo() linha 76 | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: displayOperacao movido e renomeado para pasta destino. Detalhes: bllDisplayOperacao.RenomearArquivo() linha 114 | " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível mover e renomear displayOperacao. Detalhes: bllDisplayOperacao.RenomearArquivo() linha 121 | " + retorno + " | " + data);

            }

            return retorno;


        }

        public string CopiarArquivo(string arquivoNome, string pastaOrigem, string pastaDestino)
        {
            //string origem = txtArquivo.Text;
            //string arquivo = Path.GetFileName(pastaOrigem);
            //string destino = Path.Combine(txtDiretorio.Text, arquivo);
            //copia o arquivo e sobrescreve se ja existir
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";

            try
            {
                File.Copy(pastaOrigem + arquivoNome, pastaDestino + arquivoNome, true);

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: Display de Operacao copiado. Detalhes: bllDisplayOperacao.CopiarArquivo() linha 148 | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {

                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível copiar display de Operacao. Detalhes: bllDisplayOperacao.CopiarArquivo() linha 155 | " + retorno + " | " + data);

            }

            return retorno;

        }

        public string InserirDisplayOperacao(DAODisplayOperacaoList daoDisplayOperacaoList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";

            try
            {
                DataTable dataTableDisplayOperacaoList = ConvertToDataTable(daoDisplayOperacaoList);
                foreach (DataRow linha in dataTableDisplayOperacaoList.Rows)
                {
                    DAODisplayOperacao daoDisplayOperacao = new DAODisplayOperacao();

                    daoDisplayOperacao.Tear = linha["Tear"].ToString();
                    daoDisplayOperacao.Artigo = linha["Artigo"].ToString();
                    daoDisplayOperacao.TearStatus = linha["TearStatus"].ToString();
                    daoDisplayOperacao.Continuando = linha["Continuando"].ToString();
                    daoDisplayOperacao.ParadasEficienciaTurnoAtual = linha["ParadasEficienciaTurnoAtual"].ToString();
                    daoDisplayOperacao.ParadasEficiencia24h = linha["ParadasEficiencia24h"].ToString();
                    daoDisplayOperacao.RPM = linha["RPM"].ToString();
                    daoDisplayOperacao.PrevisaoTrocaRoloTecido = linha["PrevisaoTrocaRoloTecido"].ToString();
                    daoDisplayOperacao.PrevisaoTrocaRoloUrdume = linha["PrevisaoTrocaRoloUrdume"].ToString();
                    
                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Tear", daoDisplayOperacao.Tear);
                    dalMySQL.AdicionaParametros("@Artigo", daoDisplayOperacao.Artigo);
                    dalMySQL.AdicionaParametros("@TearStatus", daoDisplayOperacao.TearStatus);
                    dalMySQL.AdicionaParametros("@Continuando", daoDisplayOperacao.Continuando);
                    dalMySQL.AdicionaParametros("@ParadasEficienciaTurnoAtual", daoDisplayOperacao.ParadasEficienciaTurnoAtual);
                    dalMySQL.AdicionaParametros("@ParadasEficiencia24h", daoDisplayOperacao.ParadasEficiencia24h);
                    dalMySQL.AdicionaParametros("@RPM", daoDisplayOperacao.RPM);
                    dalMySQL.AdicionaParametros("@PrevisaoTrocaRoloTecido", daoDisplayOperacao.PrevisaoTrocaRoloTecido);
                    dalMySQL.AdicionaParametros("@PrevisaoTrocaRoloUrdume", daoDisplayOperacao.PrevisaoTrocaRoloUrdume);
                    
                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspInsertDisplayOperacao");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: displayOperacao inserido. Detalhes: bllDisplayOperacao.InserirDisplayOperacao() linha 201 | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: Nao foi Possivel inserir dados de DisplayOperacao. Detalhes: bllDisplayOperacao.InserirDisplayOperacao() linha 207 | " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: displayOperacao renomeada deletado. Detalhes: bllDisplayOperacao.DeletarArquivos() linha 214 | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível deletar displayOperacao renomeada. Detalhes: bllDisplayOperacao.DeletarArquivos() linha 217 | " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}
