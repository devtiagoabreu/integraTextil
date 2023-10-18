using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLPedidoCompra
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de pedido de compra encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLL pedido de compra.cs | Metodo: PegarNomeArquivo | " + data);
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

        public DAOPedidoCompraList LerCsv(string path)
        {
            DAOPedidoCompraList daoPedidoCompraList = new DAOPedidoCompraList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOPedidoCompra daoPedidoCompra = new DAOPedidoCompra();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoPedidoCompra.PcNumPedCompra = campo[0].ToString();
                    daoPedidoCompra.PcSeqItemPedido = campo[1].ToString();
                    daoPedidoCompra.PcItem100Nivel99 = campo[2].ToString();
                    daoPedidoCompra.PcItem100Grupo = campo[3].ToString();
                    daoPedidoCompra.PcItem100Subgrupo = campo[4].ToString();
                    daoPedidoCompra.PcItem100Item = campo[5].ToString();
                    daoPedidoCompra.PcDescricaoItem = campo[6].ToString();
                    try
                    {
                        daoPedidoCompra.PcQtdePedidaItem = Convert.ToDecimal(campo[7].ToString());
                    }
                    catch
                    {
                        daoPedidoCompra.PcQtdePedidaItem = 0;
                    }
                    
                    daoPedidoCompra.PcUnidadeMedida = campo[8].ToString();

                    try
                    {
                        daoPedidoCompra.PcPrecoItemComp = Convert.ToDecimal(campo[9].ToString());
                    }
                    catch (Exception)
                    {
                        daoPedidoCompra.PcPrecoItemComp = 0;
                    }

                    daoPedidoCompraList.Add(daoPedidoCompra);
                }
            }
            return daoPedidoCompraList;
        }

        public string InserirDadosBD(DAOPedidoCompraList daoPedidoCompraList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspPedidoCompraDeletar");

            try
            {
                DataTable dataTablePedidoCompraList = ConvertToDataTable(daoPedidoCompraList);
                foreach (DataRow linha in dataTablePedidoCompraList.Rows)
                {
                    DAOPedidoCompra daoPedidoCompra = new DAOPedidoCompra();

                    daoPedidoCompra.PcNumPedCompra = linha["PcNumPedCompra"].ToString();
                    daoPedidoCompra.PcSeqItemPedido = linha["PcSeqItemPedido"].ToString();
                    daoPedidoCompra.PcItem100Nivel99 = linha["PcItem100Nivel99"].ToString();
                    daoPedidoCompra.PcItem100Grupo = linha["PcItem100Grupo"].ToString();
                    daoPedidoCompra.PcItem100Subgrupo = linha["PcItem100Subgrupo"].ToString();
                    daoPedidoCompra.PcItem100Item = linha["PcItem100Item"].ToString();
                    daoPedidoCompra.PcDescricaoItem = linha["PcDescricaoItem"].ToString();
                    daoPedidoCompra.PcQtdePedidaItem = Convert.ToDecimal(linha["PcQtdePedidaItem"].ToString());
                    daoPedidoCompra.PcUnidadeMedida = linha["PcUnidadeMedida"].ToString();
                    daoPedidoCompra.PcPrecoItemComp = Convert.ToDecimal(linha["PcPrecoItemComp"].ToString());

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@PcNumPedCompra", daoPedidoCompra.PcNumPedCompra);
                    dalMySQL.AdicionaParametros("@PcSeqItemPedido", daoPedidoCompra.PcSeqItemPedido);
                    dalMySQL.AdicionaParametros("@PcItem100Nivel99", daoPedidoCompra.PcItem100Nivel99);
                    dalMySQL.AdicionaParametros("@PcItem100Grupo", daoPedidoCompra.PcItem100Grupo);
                    dalMySQL.AdicionaParametros("@PcItem100Subgrupo", daoPedidoCompra.PcItem100Subgrupo);
                    dalMySQL.AdicionaParametros("@PcItem100Item", daoPedidoCompra.PcItem100Item);
                    dalMySQL.AdicionaParametros("@PcDescricaoItem", daoPedidoCompra.PcDescricaoItem);
                    dalMySQL.AdicionaParametros("@PcQtdePedidaItem", daoPedidoCompra.PcQtdePedidaItem);
                    dalMySQL.AdicionaParametros("@PcUnidadeMedida", daoPedidoCompra.PcUnidadeMedida);
                    dalMySQL.AdicionaParametros("@PcPrecoItemComp", daoPedidoCompra.PcPrecoItemComp);

                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspPedidoCompraInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Pedidos de compra inseridos. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir Pedidos de compra. Detalhes: " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de Pedido de compra deletado. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de Pedido de compra renomeado. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }


        #endregion
    }
}
