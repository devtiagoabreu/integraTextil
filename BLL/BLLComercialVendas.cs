using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLComercialVendas
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de comercial vendas encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de comercial vendas não encontrado, nome: nulo. Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: comercial vendas e renomeado para pasta destino. Detalhes: " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível mover e renomear comercial vendas. Detalhes: " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAOComercialVendasList LerCsv(string path)
        {
            DAOComercialVendasList daoComercialVendasList = new DAOComercialVendasList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOComercialVendas daoComercialVendas = new DAOComercialVendas();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoComercialVendas.Pedido = campo[0].ToString();
                    daoComercialVendas.PedidoCliente = campo[1].ToString();
                    daoComercialVendas.NumeroInterno = campo[2].ToString();
                    daoComercialVendas.Emp = campo[3].ToString();
                    daoComercialVendas.Empresa = campo[4].ToString();
                    daoComercialVendas.TipoPedido = campo[5].ToString();
                    daoComercialVendas.TipoProduto = campo[6].ToString();
                    daoComercialVendas.CriterioPedido = campo[7].ToString();
                    daoComercialVendas.CriterioQualidade = campo[8].ToString();
                    daoComercialVendas.Situacao = campo[9].ToString();
                    daoComercialVendas.BloqueiosPendentes = campo[10].ToString();
                    daoComercialVendas.BloqueiosLiberados = campo[11].ToString();
                    try
                    {
                        if (campo[12].ToString().Equals(""))
                        {
                            daoComercialVendas.Emissao = null;
                        }
                        else
                        {
                            daoComercialVendas.Emissao = campo[12].ToString().Substring(6, 4)+"-"+ campo[12].ToString().Substring(3, 2)+"-"+ campo[12].ToString().Substring(0, 2);
                        }
                    }
                    catch 
                    {

                    }
                    try
                    {
                        if (campo[13].ToString().Equals(""))
                        {
                            daoComercialVendas.Entrega = null;
                        }
                        else
                        {
                            daoComercialVendas.Entrega = campo[13].ToString().Substring(6, 4) + "-" + campo[13].ToString().Substring(3, 2) + "-" + campo[13].ToString().Substring(0, 2);
                        }
                    }
                    catch 
                    {

                    }

                    try
                    {
                        if (campo[14].ToString().Equals(""))
                        {
                            daoComercialVendas.Chegada = null;
                        }
                        else
                        {
                            daoComercialVendas.Chegada = campo[14].ToString().Substring(6, 4) + "-" + campo[14].ToString().Substring(3, 2) + "-" + campo[14].ToString().Substring(0, 2);
                        }
                    }
                    catch 
                    {
                        
                    }
                    
                    daoComercialVendas.Periodo = campo[15].ToString();
                    daoComercialVendas.Uf = campo[16].ToString();
                    daoComercialVendas.Cnpj = campo[17].ToString();
                    daoComercialVendas.NomeCliente = campo[18].ToString();
                    daoComercialVendas.Fantasia = campo[19].ToString();
                    daoComercialVendas.Geco = campo[20].ToString();
                    daoComercialVendas.GrupoEconomico = campo[21].ToString();
                    try
                    {
                        if (campo[22].ToString().Equals(""))
                        {
                            daoComercialVendas.DataValidadeLimite = null;
                        }
                        else
                        {
                            daoComercialVendas.DataValidadeLimite = campo[22].ToString().Substring(6, 4) + "-" + campo[22].ToString().Substring(3, 2) + "-" + campo[22].ToString().Substring(0, 2);
                        }
                    }
                    catch
                    {

                    }
                    try
                    {
                        if (campo[23].ToString().Equals(""))
                        {
                            daoComercialVendas.DataAlteracaoLimite = null;
                        }
                        else
                        {
                            daoComercialVendas.DataAlteracaoLimite = campo[23].ToString().Substring(6, 4) + "-" + campo[23].ToString().Substring(3, 2) + "-" + campo[23].ToString().Substring(0, 2);
                        }
                    }
                    catch 
                    {

                    }                    
                    daoComercialVendas.ObsCredito = campo[24].ToString();
                    try
                    {
                        daoComercialVendas.LimCreditoConfeccao = Convert.ToDecimal(campo[25].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.LimCreditoConfeccao = 0;
                    }
                    try
                    {
                        daoComercialVendas.LimCreditoTecidos = Convert.ToDecimal(campo[26].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.LimCreditoTecidos = 0;
                    }
                    try
                    {
                        daoComercialVendas.LimCreditoCrus = Convert.ToDecimal(campo[27].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.LimCreditoCrus = 0;
                    }
                    try
                    {
                        daoComercialVendas.LimCreditoFios = Convert.ToDecimal(campo[28].ToString());
                    }
                    catch
                    {
                        daoComercialVendas.LimCreditoFios = 0;
                    }
                    try
                    {
                        if (true)
                        {
                            daoComercialVendas.DataUltimaFatura = null;
                        }
                        else
                        {
                            daoComercialVendas.DataUltimaFatura = campo[29].ToString().Substring(6, 4) + "-" + campo[29].ToString().Substring(3, 2) + "-" + campo[29].ToString().Substring(0, 2);
                        }
                    }
                    catch 
                    {

                    }                    
                    try
                    {
                        daoComercialVendas.PedidosAFaturar = Convert.ToDecimal(campo[30].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.PedidosAFaturar = 0;
                    }
                    try
                    {
                        daoComercialVendas.TitulosVencidos = Convert.ToDecimal(campo[31].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.TitulosVencidos = 0;
                    }
                    

                    try
                    {
                        daoComercialVendas.TitulosAVencer = Convert.ToDecimal(campo[32].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.TitulosAVencer = 0;
                    }
                    try
                    {
                        daoComercialVendas.TitulosPagos = Convert.ToDecimal(campo[33].ToString());
                    }
                    catch
                    {
                        daoComercialVendas.TitulosPagos = 0;
                    }

                    daoComercialVendas.Rep = campo[34].ToString();
                    daoComercialVendas.NomeRepresenante = campo[35].ToString();
                    daoComercialVendas.Regiao = campo[36].ToString();
                    daoComercialVendas.NomeRegiao = campo[37].ToString();
                    daoComercialVendas.Bco = campo[38].ToString();
                    daoComercialVendas.Banco = campo[39].ToString();
                    daoComercialVendas.Cid = campo[40].ToString();
                    daoComercialVendas.Cidade = campo[41].ToString();
                    daoComercialVendas.TipoComissao = campo[42].ToString();
                    daoComercialVendas.Comissao = campo[43].ToString();
                    daoComercialVendas.Class = campo[44].ToString();
                    daoComercialVendas.Classificacao = campo[45].ToString();
                    daoComercialVendas.Pgto = campo[46].ToString();
                    daoComercialVendas.CondicaoPgto = campo[47].ToString();
                    daoComercialVendas.TabelaPreco = campo[48].ToString();
                    daoComercialVendas.TipoDesconto = campo[49].ToString();
                    try
                    {
                        daoComercialVendas.Desconto1 = Convert.ToDecimal(campo[50].ToString());
                    }
                    catch
                    {
                        daoComercialVendas.Desconto1 = 0;
                    }
                    try
                    {
                        daoComercialVendas.Desconto2 = Convert.ToDecimal(campo[51].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Desconto2 = 0;
                    }
                    try
                    {
                        daoComercialVendas.Desconto3 = Convert.ToDecimal(campo[52].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Desconto3 = 0;
                    }
                    try
                    {
                        daoComercialVendas.DescontoItem1 = Convert.ToDecimal(campo[53].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.DescontoItem1 = 0;
                    }
                    try
                    {
                        daoComercialVendas.DescontoItem2 = Convert.ToDecimal(campo[54].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.DescontoItem2 = 0;
                    }
                    try
                    {
                        daoComercialVendas.DescontoItem3 = Convert.ToDecimal(campo[55].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.DescontoItem3 = 0;
                    }
                    try
                    {
                        daoComercialVendas.DescontoEspecial = Convert.ToDecimal(campo[56].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.DescontoEspecial = 0;
                    }
                    try
                    {
                        daoComercialVendas.DescontoExtra = Convert.ToDecimal(campo[57].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.DescontoExtra = 0;
                    }
                    try
                    {
                        daoComercialVendas.DescFinanceiro = Convert.ToDecimal(campo[58].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.DescFinanceiro = 0;
                    }
                    try
                    {
                        daoComercialVendas.DescDuplicatas = Convert.ToDecimal(campo[59].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.DescDuplicatas = 0;
                    }
                    daoComercialVendas.TipoFrete = campo[60].ToString();
                    daoComercialVendas.CnpjTrans = campo[61].ToString();
                    daoComercialVendas.Transportadora = campo[62].ToString();
                    daoComercialVendas.TipoRedespacho = campo[63].ToString();
                    daoComercialVendas.CnpjRedesp = campo[64].ToString();
                    daoComercialVendas.Redespacho = campo[65].ToString();

                    try
                    {
                        daoComercialVendas.ValorTotalPedido = Convert.ToDecimal(campo[66].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.ValorTotalPedido = 0;
                    }
                    try
                    {
                        daoComercialVendas.ValorSaldoPedido = Convert.ToDecimal(campo[67].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.ValorSaldoPedido = 0;
                    }
                    try
                    {
                        daoComercialVendas.ValorItens = Convert.ToDecimal(campo[68].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.ValorItens = 0;
                    }
                    try
                    {
                        daoComercialVendas.ValorFrete = Convert.ToDecimal(campo[69].ToString());
                    }
                    catch
                    {
                        daoComercialVendas.ValorFrete = 0;
                    }                    
                    daoComercialVendas.Col = campo[70].ToString();
                    daoComercialVendas.Colecao = campo[71].ToString();
                    daoComercialVendas.Lin = campo[72].ToString();
                    daoComercialVendas.Linha = campo[73].ToString();
                    daoComercialVendas.Art = campo[74].ToString();
                    daoComercialVendas.Artigo = campo[75].ToString();
                    daoComercialVendas.Grupo = campo[76].ToString();
                    daoComercialVendas.Sub = campo[77].ToString();
                    daoComercialVendas.Cor = campo[78].ToString();
                    daoComercialVendas.Produto = campo[79].ToString();
                    daoComercialVendas.NomeGrupo = campo[80].ToString();
                    daoComercialVendas.NomeSUB = campo[81].ToString();
                    daoComercialVendas.NomeCor = campo[82].ToString();
                    daoComercialVendas.Narrativa = campo[83].ToString();
                    daoComercialVendas.Lote = campo[84].ToString();
                    daoComercialVendas.Embalagem = campo[85].ToString();
                    daoComercialVendas.Dep = campo[86].ToString();
                    daoComercialVendas.Deposito = campo[87].ToString();
                    try
                    {
                        daoComercialVendas.Vendido = Convert.ToDecimal(campo[88].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Vendido = 0;
                    }
                    try
                    {
                        daoComercialVendas.Faturado = Convert.ToDecimal(campo[89].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Faturado = 0;
                    }
                    try
                    {
                        daoComercialVendas.Solicitado = Convert.ToDecimal(campo[90].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Solicitado = 0;
                    }
                    try
                    {
                        daoComercialVendas.Cancelado = Convert.ToDecimal(campo[91].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Cancelado = 0;
                    }
                    try
                    {
                        daoComercialVendas.Saldo = Convert.ToDecimal(campo[92].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Saldo = 0;
                    }                    
                    daoComercialVendas.SitItem = campo[93].ToString();
                    try
                    {
                        daoComercialVendas.Alocado = Convert.ToDecimal(campo[94].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Alocado = 0;
                    }
                    daoComercialVendas.CodCanc = campo[95].ToString();
                    try
                    {
                        daoComercialVendas.DescontoITEM = Convert.ToDecimal(campo[96].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.DescontoITEM = 0;
                    }
                    try
                    {
                        daoComercialVendas.Unitario = Convert.ToDecimal(campo[97].ToString());
                    }
                    catch 
                    {
                        daoComercialVendas.Unitario = 0;
                    }

                    daoComercialVendasList.Add(daoComercialVendas);

                }
            }
            return daoComercialVendasList;
        }

        public string InserirDadosBD(DAOComercialVendasList daoComercialVendasList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspComercialVendasDeletar");

            try
            {
                DataTable dataTableComercialVendasList = ConvertToDataTable(daoComercialVendasList);
                foreach (DataRow linha in dataTableComercialVendasList.Rows)
                {
                    DAOComercialVendas daoComercialVendas = new DAOComercialVendas();

                    daoComercialVendas.Pedido = linha["Pedido"].ToString();
                    daoComercialVendas.PedidoCliente = linha["PedidoCliente"].ToString();
                    daoComercialVendas.NumeroInterno = linha["NumeroInterno"].ToString();
                    daoComercialVendas.Emp = linha["Emp"].ToString();
                    daoComercialVendas.Empresa = linha["Empresa"].ToString();
                    daoComercialVendas.TipoPedido = linha["TipoPedido"].ToString();
                    daoComercialVendas.TipoProduto = linha["TipoProduto"].ToString();
                    daoComercialVendas.CriterioPedido = linha["CriterioPedido"].ToString();
                    daoComercialVendas.CriterioQualidade = linha["CriterioQualidade"].ToString();
                    daoComercialVendas.Situacao = linha["Situacao"].ToString();
                    daoComercialVendas.BloqueiosPendentes = linha["BloqueiosPendentes"].ToString();
                    daoComercialVendas.BloqueiosLiberados = linha["BloqueiosLiberados"].ToString();
                    daoComercialVendas.Emissao = linha["Emissao"].ToString().Equals("") ? null : linha["Emissao"].ToString();
                    daoComercialVendas.Entrega = linha["Entrega"].ToString().Equals("") ? null : linha["Entrega"].ToString();
                    daoComercialVendas.Chegada = linha["Chegada"].ToString().Equals("") ? null : linha["Chegada"].ToString();
                    daoComercialVendas.Periodo = linha["Periodo"].ToString();
                    daoComercialVendas.Uf = linha["Uf"].ToString();
                    daoComercialVendas.Cnpj = linha["Cnpj"].ToString();
                    daoComercialVendas.NomeCliente = linha["NomeCliente"].ToString();
                    daoComercialVendas.Fantasia = linha["Fantasia"].ToString();
                    daoComercialVendas.Geco = linha["Geco"].ToString();
                    daoComercialVendas.GrupoEconomico = linha["GrupoEconomico"].ToString();
                    daoComercialVendas.DataValidadeLimite = linha["DataValidadeLimite"].ToString().Equals("") ? null : linha["DataValidadeLimite"].ToString();
                    daoComercialVendas.DataAlteracaoLimite = linha["DataAlteracaoLimite"].ToString().Equals("") ? null : linha["DataAlteracaoLimite"].ToString();
                    daoComercialVendas.ObsCredito = linha["ObsCredito"].ToString();
                    daoComercialVendas.LimCreditoConfeccao = Convert.ToDecimal(linha["LimCreditoConfeccao"].ToString());
                    daoComercialVendas.LimCreditoTecidos = Convert.ToDecimal(linha["LimCreditoTecidos"].ToString());
                    daoComercialVendas.LimCreditoCrus = Convert.ToDecimal(linha["LimCreditoCrus"].ToString());
                    daoComercialVendas.LimCreditoFios = Convert.ToDecimal(linha["LimCreditoFios"].ToString());
                    daoComercialVendas.DataUltimaFatura = linha["DataUltimaFatura"].ToString().Equals("") ? null : linha["DataUltimaFatura"].ToString();
                    daoComercialVendas.PedidosAFaturar = Convert.ToDecimal(linha["PedidosAFaturar"].ToString());
                    daoComercialVendas.TitulosVencidos = Convert.ToDecimal(linha["TitulosVencidos"].ToString());
                    daoComercialVendas.TitulosAVencer = Convert.ToDecimal(linha["TitulosAVencer"].ToString());
                    daoComercialVendas.TitulosPagos = Convert.ToDecimal(linha["TitulosPagos"].ToString());
                    daoComercialVendas.Rep = linha["Rep"].ToString();
                    daoComercialVendas.NomeRepresenante = linha["NomeRepresenante"].ToString();
                    daoComercialVendas.Regiao = linha["Regiao"].ToString();
                    daoComercialVendas.NomeRegiao = linha["NomeRegiao"].ToString();
                    daoComercialVendas.Bco = linha["Bco"].ToString();
                    daoComercialVendas.Banco = linha["Banco"].ToString();
                    daoComercialVendas.Cid = linha["Cid"].ToString();
                    daoComercialVendas.Cidade = linha["Cidade"].ToString();
                    daoComercialVendas.TipoComissao = linha["TipoComissao"].ToString();
                    daoComercialVendas.Comissao = linha["Comissao"].ToString();
                    daoComercialVendas.Class = linha["Class"].ToString();
                    daoComercialVendas.Classificacao = linha["Classificacao"].ToString();
                    daoComercialVendas.Pgto = linha["Pgto"].ToString();
                    daoComercialVendas.CondicaoPgto = linha["CondicaoPgto"].ToString();
                    daoComercialVendas.TabelaPreco = linha["TabelaPreco"].ToString();
                    daoComercialVendas.TipoDesconto = linha["TipoDesconto"].ToString();
                    daoComercialVendas.Desconto1 = Convert.ToDecimal(linha["Desconto1"].ToString());
                    daoComercialVendas.Desconto2 = Convert.ToDecimal(linha["Desconto2"].ToString());
                    daoComercialVendas.Desconto3 = Convert.ToDecimal(linha["Desconto3"].ToString());
                    daoComercialVendas.DescontoItem1 = Convert.ToDecimal(linha["DescontoItem1"].ToString());
                    daoComercialVendas.DescontoItem2 = Convert.ToDecimal(linha["DescontoItem2"].ToString());
                    daoComercialVendas.DescontoItem3 = Convert.ToDecimal(linha["DescontoItem3"].ToString());
                    daoComercialVendas.DescontoEspecial = Convert.ToDecimal(linha["DescontoEspecial"].ToString());
                    daoComercialVendas.DescontoExtra = Convert.ToDecimal(linha["DescontoExtra"].ToString());
                    daoComercialVendas.DescFinanceiro = Convert.ToDecimal(linha["DescFinanceiro"].ToString());
                    daoComercialVendas.DescDuplicatas = Convert.ToDecimal(linha["DescDuplicatas"].ToString());
                    daoComercialVendas.TipoFrete = linha["TipoFrete"].ToString();
                    daoComercialVendas.CnpjTrans = linha["CnpjTrans"].ToString();
                    daoComercialVendas.Transportadora = linha["Transportadora"].ToString();
                    daoComercialVendas.TipoRedespacho = linha["TipoRedespacho"].ToString();
                    daoComercialVendas.CnpjRedesp = linha["CnpjRedesp"].ToString();
                    daoComercialVendas.Redespacho = linha["Redespacho"].ToString();
                    daoComercialVendas.ValorTotalPedido = Convert.ToDecimal(linha["ValorTotalPedido"].ToString());
                    daoComercialVendas.ValorSaldoPedido = Convert.ToDecimal(linha["ValorSaldoPedido"].ToString());
                    daoComercialVendas.ValorItens = Convert.ToDecimal(linha["ValorItens"].ToString());
                    daoComercialVendas.ValorFrete = Convert.ToDecimal(linha["ValorFrete"].ToString());
                    daoComercialVendas.Col = linha["Col"].ToString();
                    daoComercialVendas.Colecao = linha["Colecao"].ToString();
                    daoComercialVendas.Lin = linha["Lin"].ToString();
                    daoComercialVendas.Linha = linha["Linha"].ToString();
                    daoComercialVendas.Art = linha["Art"].ToString();
                    daoComercialVendas.Artigo = linha["Artigo"].ToString();
                    daoComercialVendas.Grupo = linha["Grupo"].ToString();
                    daoComercialVendas.Sub = linha["Sub"].ToString();
                    daoComercialVendas.Cor = linha["Cor"].ToString();
                    daoComercialVendas.Produto = linha["Produto"].ToString();
                    daoComercialVendas.NomeGrupo = linha["NomeGrupo"].ToString();
                    daoComercialVendas.NomeSUB = linha["NomeSUB"].ToString();
                    daoComercialVendas.NomeCor = linha["NomeCor"].ToString();
                    daoComercialVendas.Narrativa = linha["Narrativa"].ToString();
                    daoComercialVendas.Lote = linha["Lote"].ToString();
                    daoComercialVendas.Embalagem = linha["Embalagem"].ToString();
                    daoComercialVendas.Dep = linha["Dep"].ToString();
                    daoComercialVendas.Deposito = linha["Deposito"].ToString();
                    daoComercialVendas.Vendido = Convert.ToDecimal(linha["Vendido"].ToString());
                    daoComercialVendas.Faturado = Convert.ToDecimal(linha["Faturado"].ToString());
                    daoComercialVendas.Solicitado = Convert.ToDecimal(linha["Solicitado"].ToString());
                    daoComercialVendas.Cancelado = Convert.ToDecimal(linha["Cancelado"].ToString());
                    daoComercialVendas.Saldo = Convert.ToDecimal(linha["Saldo"].ToString());
                    daoComercialVendas.SitItem = linha["SitItem"].ToString();
                    daoComercialVendas.Alocado = Convert.ToDecimal(linha["Alocado"].ToString());
                    daoComercialVendas.CodCanc = linha["CodCanc"].ToString();
                    daoComercialVendas.DescontoITEM = Convert.ToDecimal(linha["DescontoITEM"].ToString());
                    daoComercialVendas.Unitario = Convert.ToDecimal(linha["Unitario"].ToString());

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Pedido", daoComercialVendas.Pedido);
                    dalMySQL.AdicionaParametros("@PedidoCliente", daoComercialVendas.PedidoCliente);
                    dalMySQL.AdicionaParametros("@NumeroInterno", daoComercialVendas.NumeroInterno);
                    dalMySQL.AdicionaParametros("@Emp", daoComercialVendas.Emp);
                    dalMySQL.AdicionaParametros("@Empresa", daoComercialVendas.Empresa);
                    dalMySQL.AdicionaParametros("@TipoPedido", daoComercialVendas.TipoPedido);
                    dalMySQL.AdicionaParametros("@TipoProduto", daoComercialVendas.TipoProduto);
                    dalMySQL.AdicionaParametros("@CriterioPedido", daoComercialVendas.CriterioPedido);
                    dalMySQL.AdicionaParametros("@CriterioQualidade", daoComercialVendas.CriterioQualidade);
                    dalMySQL.AdicionaParametros("@Situacao", daoComercialVendas.Situacao);
                    dalMySQL.AdicionaParametros("@BloqueiosPendentes", daoComercialVendas.BloqueiosPendentes);
                    dalMySQL.AdicionaParametros("@BloqueiosLiberados", daoComercialVendas.BloqueiosLiberados);
                    dalMySQL.AdicionaParametros("@Emissao", daoComercialVendas.Emissao);
                    dalMySQL.AdicionaParametros("@Entrega", daoComercialVendas.Entrega);
                    dalMySQL.AdicionaParametros("@Chegada", daoComercialVendas.Chegada);
                    dalMySQL.AdicionaParametros("@Periodo", daoComercialVendas.Periodo);
                    dalMySQL.AdicionaParametros("@Uf", daoComercialVendas.Uf);
                    dalMySQL.AdicionaParametros("@Cnpj", daoComercialVendas.Cnpj);
                    dalMySQL.AdicionaParametros("@NomeCliente", daoComercialVendas.NomeCliente);
                    dalMySQL.AdicionaParametros("@Fantasia", daoComercialVendas.Fantasia);
                    dalMySQL.AdicionaParametros("@Geco", daoComercialVendas.Geco);
                    dalMySQL.AdicionaParametros("@GrupoEconomico", daoComercialVendas.GrupoEconomico);
                    dalMySQL.AdicionaParametros("@DataValidadeLimite", daoComercialVendas.DataValidadeLimite);
                    dalMySQL.AdicionaParametros("@ObsCredito", daoComercialVendas.ObsCredito);
                    dalMySQL.AdicionaParametros("@DataAlteracaoLimite", daoComercialVendas.DataAlteracaoLimite);
                    dalMySQL.AdicionaParametros("@LimCreditoConfeccao", daoComercialVendas.LimCreditoConfeccao);
                    dalMySQL.AdicionaParametros("@LimCreditoTecidos", daoComercialVendas.LimCreditoTecidos);
                    dalMySQL.AdicionaParametros("@LimCreditoCrus", daoComercialVendas.LimCreditoCrus);
                    dalMySQL.AdicionaParametros("@LimCreditoFios", daoComercialVendas.LimCreditoFios);
                    dalMySQL.AdicionaParametros("@DataUltimaFatura", daoComercialVendas.DataUltimaFatura);
                    dalMySQL.AdicionaParametros("@PedidosAFaturar", daoComercialVendas.PedidosAFaturar);
                    dalMySQL.AdicionaParametros("@TitulosVencidos", daoComercialVendas.TitulosVencidos);
                    dalMySQL.AdicionaParametros("@TitulosAVencer", daoComercialVendas.TitulosAVencer);
                    dalMySQL.AdicionaParametros("@TitulosPagos", daoComercialVendas.TitulosPagos);
                    dalMySQL.AdicionaParametros("@Rep", daoComercialVendas.Rep);
                    dalMySQL.AdicionaParametros("@NomeRepresenante", daoComercialVendas.NomeRepresenante);
                    dalMySQL.AdicionaParametros("@Regiao", daoComercialVendas.Regiao);
                    dalMySQL.AdicionaParametros("@NomeRegiao", daoComercialVendas.NomeRegiao);
                    dalMySQL.AdicionaParametros("@Bco", daoComercialVendas.Bco);
                    dalMySQL.AdicionaParametros("@Banco", daoComercialVendas.Banco);
                    dalMySQL.AdicionaParametros("@Cid", daoComercialVendas.Cid);
                    dalMySQL.AdicionaParametros("@Cidade", daoComercialVendas.Cidade);
                    dalMySQL.AdicionaParametros("@TipoComissao", daoComercialVendas.TipoComissao);
                    dalMySQL.AdicionaParametros("@Comissao", daoComercialVendas.Comissao);
                    dalMySQL.AdicionaParametros("@Class", daoComercialVendas.Class);
                    dalMySQL.AdicionaParametros("@Classificacao", daoComercialVendas.Classificacao);
                    dalMySQL.AdicionaParametros("@Pgto", daoComercialVendas.Pgto);
                    dalMySQL.AdicionaParametros("@CondicaoPgto", daoComercialVendas.CondicaoPgto);
                    dalMySQL.AdicionaParametros("@TabelaPreco", daoComercialVendas.TabelaPreco);
                    dalMySQL.AdicionaParametros("@TipoDesconto", daoComercialVendas.TipoDesconto);
                    dalMySQL.AdicionaParametros("@Desconto1", daoComercialVendas.Desconto1);
                    dalMySQL.AdicionaParametros("@Desconto2", daoComercialVendas.Desconto2);
                    dalMySQL.AdicionaParametros("@Desconto3", daoComercialVendas.Desconto3);
                    dalMySQL.AdicionaParametros("@DescontoItem1", daoComercialVendas.DescontoItem1);
                    dalMySQL.AdicionaParametros("@DescontoItem2", daoComercialVendas.DescontoItem2);
                    dalMySQL.AdicionaParametros("@DescontoItem3", daoComercialVendas.DescontoItem3);
                    dalMySQL.AdicionaParametros("@DescontoEspecial", daoComercialVendas.DescontoEspecial);
                    dalMySQL.AdicionaParametros("@DescontoExtra", daoComercialVendas.DescontoExtra);
                    dalMySQL.AdicionaParametros("@DescFinanceiro", daoComercialVendas.DescFinanceiro);
                    dalMySQL.AdicionaParametros("@DescDuplicatas", daoComercialVendas.DescDuplicatas);
                    dalMySQL.AdicionaParametros("@TipoFrete", daoComercialVendas.TipoFrete);
                    dalMySQL.AdicionaParametros("@CnpjTrans", daoComercialVendas.CnpjTrans);
                    dalMySQL.AdicionaParametros("@Transportadora", daoComercialVendas.Transportadora);
                    dalMySQL.AdicionaParametros("@TipoRedespacho", daoComercialVendas.TipoRedespacho);
                    dalMySQL.AdicionaParametros("@CnpjRedesp", daoComercialVendas.CnpjRedesp);
                    dalMySQL.AdicionaParametros("@Redespacho", daoComercialVendas.Redespacho);
                    dalMySQL.AdicionaParametros("@ValorTotalPedido", daoComercialVendas.ValorTotalPedido);
                    dalMySQL.AdicionaParametros("@ValorSaldoPedido", daoComercialVendas.ValorSaldoPedido);
                    dalMySQL.AdicionaParametros("@ValorItens", daoComercialVendas.ValorItens);
                    dalMySQL.AdicionaParametros("@ValorFrete", daoComercialVendas.ValorFrete);
                    dalMySQL.AdicionaParametros("@Col", daoComercialVendas.Col);
                    dalMySQL.AdicionaParametros("@Colecao", daoComercialVendas.Colecao);
                    dalMySQL.AdicionaParametros("@Lin", daoComercialVendas.Lin);
                    dalMySQL.AdicionaParametros("@Linha", daoComercialVendas.Linha);
                    dalMySQL.AdicionaParametros("@Art", daoComercialVendas.Art);
                    dalMySQL.AdicionaParametros("@Artigo", daoComercialVendas.Artigo);
                    dalMySQL.AdicionaParametros("@Grupo", daoComercialVendas.Grupo);
                    dalMySQL.AdicionaParametros("@Sub", daoComercialVendas.Sub);
                    dalMySQL.AdicionaParametros("@Cor", daoComercialVendas.Cor);
                    dalMySQL.AdicionaParametros("@Produto", daoComercialVendas.Produto);
                    dalMySQL.AdicionaParametros("@NomeGrupo", daoComercialVendas.NomeGrupo);
                    dalMySQL.AdicionaParametros("@NomeSUB", daoComercialVendas.NomeSUB);
                    dalMySQL.AdicionaParametros("@NomeCor", daoComercialVendas.NomeCor);
                    dalMySQL.AdicionaParametros("@Narrativa", daoComercialVendas.Narrativa);
                    dalMySQL.AdicionaParametros("@Lote", daoComercialVendas.Lote);
                    dalMySQL.AdicionaParametros("@Embalagem", daoComercialVendas.Embalagem);
                    dalMySQL.AdicionaParametros("@Dep", daoComercialVendas.Dep);
                    dalMySQL.AdicionaParametros("@Deposito", daoComercialVendas.Deposito);
                    dalMySQL.AdicionaParametros("@Vendido", daoComercialVendas.Vendido);
                    dalMySQL.AdicionaParametros("@Faturado", daoComercialVendas.Faturado);
                    dalMySQL.AdicionaParametros("@Solicitado", daoComercialVendas.Solicitado);
                    dalMySQL.AdicionaParametros("@Cancelado", daoComercialVendas.Cancelado);
                    dalMySQL.AdicionaParametros("@Saldo", daoComercialVendas.Saldo);
                    dalMySQL.AdicionaParametros("@SitItem", daoComercialVendas.SitItem);
                    dalMySQL.AdicionaParametros("@Alocado", daoComercialVendas.Alocado);
                    dalMySQL.AdicionaParametros("@CodCanc", daoComercialVendas.CodCanc);
                    dalMySQL.AdicionaParametros("@DescontoITEM", daoComercialVendas.DescontoITEM);
                    dalMySQL.AdicionaParametros("@Unitario", daoComercialVendas.Unitario);

                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspComercialVendasInserir");

                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: comercial vendas inserida. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir comercial vendas. Detalhes: " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de comercial vendas deletadas. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de comercial vendas renomeada. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}