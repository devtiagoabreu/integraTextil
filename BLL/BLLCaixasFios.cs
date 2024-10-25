using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLCaixasFios
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de caixas fios encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de caixas fios não encontrado, nome: nulo. Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Caixas fios e renomeado para pasta destino. Detalhes: " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível mover e renomear Caixas fios. Detalhes: " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAOCaixasFiosList LerCsv(string path)
        {
            DAOCaixasFiosList daoCaixasFiosList = new DAOCaixasFiosList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOCaixasFios daoCaixasFios = new DAOCaixasFios();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    try
                    {
                        daoCaixasFios.Emp = campo[0].ToString();
                    }
                    catch 
                    {
                        daoCaixasFios.Emp = "01";
                    }

                    try
                    {
                        daoCaixasFios.Empresa = campo[1].ToString();
                    }
                    catch 
                    {
                        daoCaixasFios.Empresa = "Pro Moda Têxtil LTDA";
                    }
                    
                    try
                    {
                        daoCaixasFios.Dep = campo[2].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Dep = "000";
                    }

                    try
                    {
                        daoCaixasFios.Deposito = campo[3].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Deposito = "000";
                    }

                    try
                    {
                        daoCaixasFios.Tran = campo[4].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Tran = "000";
                    }

                    try
                    {
                        daoCaixasFios.Transacao = campo[5].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Transacao = "000";
                    }

                    try
                    {
                        daoCaixasFios.Produto = campo[6].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Produto = "000";
                    }

                    try
                    {
                        daoCaixasFios.Nivel = campo[7].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Nivel = "000";
                    }

                    try
                    {
                        daoCaixasFios.Grupo = campo[8].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Grupo = "000";
                    }

                    try
                    {
                        daoCaixasFios.Sub = campo[9].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Sub = "000";
                    }

                    try
                    {
                        daoCaixasFios.Item = campo[10].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Item = "000";
                    }

                    try
                    {
                        daoCaixasFios.Um = campo[11].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Um = "000";
                    }

                    try
                    {
                        daoCaixasFios.Narrativa = campo[12].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Narrativa = "000";
                    }

                    try
                    {
                        daoCaixasFios.Tpg = campo[13].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Tpg = "000";
                    }

                    try
                    {
                        daoCaixasFios.TipoGlobal = campo[14].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.TipoGlobal = "000";
                    }

                    try
                    {
                        daoCaixasFios.Lote = campo[15].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Lote = "000";
                    }

                    try
                    {
                        daoCaixasFios.LoteProduto = campo[16].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.LoteProduto = "000";
                    }
                             
                    try
                    {
                        daoCaixasFios.Quantidade = Convert.ToDecimal(campo[17].ToString());
                    }
                    catch 
                    {
                        daoCaixasFios.Quantidade = 0;
                    }
                    try
                    {
                        daoCaixasFios.PesoBruto = Convert.ToDecimal(campo[18].ToString());
                    }
                    catch 
                    {
                        daoCaixasFios.PesoBruto = 0;
                    }
                    try
                    {
                        daoCaixasFios.PesoLiquido = Convert.ToDecimal(campo[19].ToString());
                    }
                    catch
                    {
                        daoCaixasFios.PesoLiquido = 0;
                    }

                    try
                    {
                        daoCaixasFios.PesoLiquido = Convert.ToDecimal(campo[19].ToString());
                    }
                    catch
                    {
                        daoCaixasFios.PesoLiquido = 0;
                    }

                    try
                    {
                        daoCaixasFios.NumeroVolume = campo[20].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.NumeroVolume = "000";
                    }

                    try
                    {
                        daoCaixasFios.NumeroOrigem = campo[21].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.NumeroOrigem = "000";
                    }

                    try
                    {
                        daoCaixasFios.Situacao = campo[22].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Situacao = "000";
                    }

                    try
                    {
                        daoCaixasFios.DataEntrada = Convert.ToDateTime(campo[23].ToString());
                        //se a máquina estiver no idioma Inglês
                        //daoCaixasFios.DataEntrada = DateTime.ParseExact(campo[23].ToString(), "dd/MM/yyyy", null);
                    }
                    catch
                    {
                        daoCaixasFios.DataEntrada = Convert.ToDateTime("09/09/1982");
                    }

                    try
                    {
                        daoCaixasFios.Nf = campo[24].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Nf = "000";
                    }

                    try
                    {
                        daoCaixasFios.Serie = campo[25].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Serie = "1";
                    }

                    try
                    {
                        daoCaixasFios.seqNf = campo[26].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.seqNf = "000";
                    }

                    try
                    {
                        daoCaixasFios.Fornecedor = campo[27].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Fornecedor = "000";
                    }

                    try
                    {
                        daoCaixasFios.NomeFornecedor = campo[28].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.NomeFornecedor = "000";
                    }

                    try
                    {
                        daoCaixasFios.Obs = campo[29].ToString();
                    }
                    catch
                    {
                        daoCaixasFios.Obs = "Erro na leitura de alguns campos";
                    }                   

                    daoCaixasFiosList.Add(daoCaixasFios);
                }
            }
            return daoCaixasFiosList;
        }

        public string InserirDadosBD(DAOCaixasFiosList daoCaixasFiosList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspCaixasFiosDeletar");

            try
            {
                DataTable dataTableCaixasFiosList = ConvertToDataTable(daoCaixasFiosList);
                foreach (DataRow linha in dataTableCaixasFiosList.Rows)
                {
                    DAOCaixasFios daoCaixasFios = new DAOCaixasFios();

                    daoCaixasFios.Emp = linha["Emp"].ToString();
                    daoCaixasFios.Empresa = linha["Empresa"].ToString();
                    daoCaixasFios.Dep = linha["Dep"].ToString();
                    daoCaixasFios.Deposito = linha["Deposito"].ToString();
                    daoCaixasFios.Tran = linha["Tran"].ToString();
                    daoCaixasFios.Transacao = linha["Transacao"].ToString();
                    daoCaixasFios.Produto = linha["Produto"].ToString();
                    daoCaixasFios.Nivel = linha["Nivel"].ToString();
                    daoCaixasFios.Grupo = linha["Grupo"].ToString();
                    daoCaixasFios.Sub = linha["Sub"].ToString();
                    daoCaixasFios.Item = linha["Item"].ToString();
                    daoCaixasFios.Um = linha["Um"].ToString();
                    daoCaixasFios.Narrativa = linha["Narrativa"].ToString();
                    daoCaixasFios.Tpg = linha["Tpg"].ToString();
                    daoCaixasFios.TipoGlobal = linha["TipoGlobal"].ToString();
                    daoCaixasFios.Lote = linha["Lote"].ToString();
                    daoCaixasFios.LoteProduto = linha["LoteProduto"].ToString();
                    daoCaixasFios.Quantidade = Convert.ToDecimal(linha["Quantidade"].ToString());
                    daoCaixasFios.PesoBruto = Convert.ToDecimal(linha["PesoBruto"].ToString());
                    daoCaixasFios.PesoLiquido = Convert.ToDecimal(linha["PesoLiquido"].ToString());
                    daoCaixasFios.NumeroVolume = linha["NumeroVolume"].ToString();
                    daoCaixasFios.NumeroOrigem = linha["NumeroOrigem"].ToString();
                    daoCaixasFios.Situacao = linha["Situacao"].ToString();
                    daoCaixasFios.DataEntrada = Convert.ToDateTime(linha["DataEntrada"].ToString());
                    daoCaixasFios.Nf = linha["Nf"].ToString();
                    daoCaixasFios.Serie = linha["Serie"].ToString();
                    daoCaixasFios.seqNf = linha["seqNf"].ToString();
                    daoCaixasFios.Fornecedor = linha["Fornecedor"].ToString();
                    daoCaixasFios.NomeFornecedor = linha["NomeFornecedor"].ToString();
                    daoCaixasFios.Obs = linha["Obs"].ToString();

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Emp", daoCaixasFios.Emp);
                    dalMySQL.AdicionaParametros("@Empresa", daoCaixasFios.Empresa);
                    dalMySQL.AdicionaParametros("@Dep", daoCaixasFios.Dep);
                    dalMySQL.AdicionaParametros("@Deposito", daoCaixasFios.Deposito);
                    dalMySQL.AdicionaParametros("@Tran", daoCaixasFios.Tran);
                    dalMySQL.AdicionaParametros("@Transacao", daoCaixasFios.Transacao);
                    dalMySQL.AdicionaParametros("@Produto", daoCaixasFios.Produto);
                    dalMySQL.AdicionaParametros("@Nivel", daoCaixasFios.Nivel);
                    dalMySQL.AdicionaParametros("@Grupo", daoCaixasFios.Grupo);
                    dalMySQL.AdicionaParametros("@Sub", daoCaixasFios.Sub);
                    dalMySQL.AdicionaParametros("@Item", daoCaixasFios.Item);
                    dalMySQL.AdicionaParametros("@Um", daoCaixasFios.Um);
                    dalMySQL.AdicionaParametros("@Narrativa", daoCaixasFios.Narrativa);
                    dalMySQL.AdicionaParametros("@Tpg", daoCaixasFios.Tpg);
                    dalMySQL.AdicionaParametros("@TipoGlobal", daoCaixasFios.TipoGlobal);
                    dalMySQL.AdicionaParametros("@Lote", daoCaixasFios.Lote);
                    dalMySQL.AdicionaParametros("@LoteProduto", daoCaixasFios.LoteProduto);
                    dalMySQL.AdicionaParametros("@Quantidade", daoCaixasFios.Quantidade);
                    dalMySQL.AdicionaParametros("@PesoBruto", daoCaixasFios.PesoBruto);
                    dalMySQL.AdicionaParametros("@PesoLiquido", daoCaixasFios.PesoLiquido);
                    dalMySQL.AdicionaParametros("@NumeroVolume", daoCaixasFios.NumeroVolume);
                    dalMySQL.AdicionaParametros("@NumeroOrigem", daoCaixasFios.NumeroOrigem);
                    dalMySQL.AdicionaParametros("@Situacao", daoCaixasFios.Situacao);
                    dalMySQL.AdicionaParametros("@DataEntrada", daoCaixasFios.DataEntrada);
                    dalMySQL.AdicionaParametros("@Nf", daoCaixasFios.Nf);
                    dalMySQL.AdicionaParametros("@Serie", daoCaixasFios.Serie);
                    dalMySQL.AdicionaParametros("@seqNf", daoCaixasFios.seqNf);
                    dalMySQL.AdicionaParametros("@Fornecedor", daoCaixasFios.Fornecedor);
                    dalMySQL.AdicionaParametros("@NomeFornecedor", daoCaixasFios.NomeFornecedor);
                    dalMySQL.AdicionaParametros("@Obs", daoCaixasFios.Obs);


                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspCaixasFiosInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Caixas fios inserida. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir Caixas fios. Detalhes: " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de Caixas fios deletadas. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de Caixas fios renomeada. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}
