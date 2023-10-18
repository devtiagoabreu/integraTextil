using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLRecebimentos
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de financeiro recebimentos encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de financeiro recebimentos não encontrado, nome: nulo. Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: financeiro recebimentos e renomeado para pasta destino. Detalhes: " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível mover e renomear financeiro recebimentos Detalhes: " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAORecebimentosList LerCsv(string path)
        {
            DAORecebimentosList daoRecebimentosList = new DAORecebimentosList();
            var csv = new StreamReader(File.OpenRead(path));
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAORecebimentos daoRecebimentos = new DAORecebimentos();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoRecebimentos.Empresa = campo[0].ToString();
                    daoRecebimentos.NumDuplicata = campo[1].ToString();
                    daoRecebimentos.Parcela = campo[2].ToString();
                    daoRecebimentos.TipoTitulo = campo[3].ToString();
                    daoRecebimentos.SituacaoDuplic = campo[4].ToString();
                    daoRecebimentos.Emissao = campo[5].ToString();
                    daoRecebimentos.VencOriginal = campo[6].ToString();
                    daoRecebimentos.Vencimento = campo[7].ToString();
                    daoRecebimentos.CodCliente = campo[8].ToString();
                    daoRecebimentos.Cliente = campo[9].ToString();
                    daoRecebimentos.CodResponsavel = campo[10].ToString();
                    daoRecebimentos.Responsavel = campo[11].ToString();
                    daoRecebimentos.CodEndosso = campo[12].ToString();
                    daoRecebimentos.Endosso = campo[13].ToString();
                    daoRecebimentos.PedidoVenda = campo[14].ToString();
                    daoRecebimentos.Representante = campo[15].ToString();
                    try
                    {
                        daoRecebimentos.PercComissao = Convert.ToDecimal(campo[16].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.PercComissao = 0;
                    }
                    try
                    {
                        daoRecebimentos.BaseComissao = Convert.ToDecimal(campo[17].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.BaseComissao = 0;
                    }
                    try
                    {
                        daoRecebimentos.ValorComissao = Convert.ToDecimal(campo[18].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.ValorComissao = 0;
                    }                    
                    daoRecebimentos.Portador = campo[19].ToString();
                    daoRecebimentos.NumeroBordero = campo[20].ToString();
                    daoRecebimentos.NumeroRemessa = campo[21].ToString();
                    daoRecebimentos.NrTituloBanco = campo[22].ToString();
                    daoRecebimentos.ContaCorrente = campo[23].ToString();
                    daoRecebimentos.CodCarteira = campo[24].ToString();
                    daoRecebimentos.Transacao = campo[25].ToString();
                    daoRecebimentos.PercDesconto = campo[26].ToString();
                    daoRecebimentos.NrSolicitacao = campo[27].ToString();
                    try
                    {
                        daoRecebimentos.ValorDuplicata = Convert.ToDecimal(campo[28].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.ValorDuplicata = 0;
                    }
                    try
                    {
                        daoRecebimentos.SaldoDuplicata = Convert.ToDecimal(campo[29].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.SaldoDuplicata = 0;
                    }                              
                    daoRecebimentos.Moeda = campo[30].ToString();
                    daoRecebimentos.Posicao = campo[31].ToString();
                    daoRecebimentos.LocalEmpresa = campo[32].ToString();
                    daoRecebimentos.SituacaoDuplicata = campo[33].ToString();
                    daoRecebimentos.Historico = campo[34].ToString();
                    daoRecebimentos.ComplHistorico = campo[35].ToString();
                    daoRecebimentos.NumContabil = campo[36].ToString();
                    daoRecebimentos.FormaPagto = campo[37].ToString();
                    daoRecebimentos.CodBarras = campo[38].ToString();
                    daoRecebimentos.LinhaDigitavel = campo[39].ToString();
                    daoRecebimentos.DuplicImpressa = campo[40].ToString();
                    daoRecebimentos.Previsao = campo[41].ToString();
                    daoRecebimentos.NumeroTitulo = campo[42].ToString();
                    daoRecebimentos.NotaFiscal = campo[43].ToString();
                    daoRecebimentos.Serie = campo[44].ToString();
                    daoRecebimentos.CodCancelamento = campo[45].ToString();
                    daoRecebimentos.Cancelamento = campo[46].ToString();
                    daoRecebimentos.CodigoContabil = campo[47].ToString();
                    try
                    {
                        daoRecebimentos.ValorMoeda = Convert.ToDecimal(campo[48].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.ValorMoeda = 0;
                    }                    
                    daoRecebimentos.CodUsuario = campo[49].ToString();
                    daoRecebimentos.NumeroCaixa = campo[50].ToString();
                    daoRecebimentos.NrAdiantamento = campo[51].ToString();
                    daoRecebimentos.FantasiaCliente = campo[52].ToString();
                    daoRecebimentos.TelefoneCliente = campo[53].ToString();
                    daoRecebimentos.emailCliente = campo[54].ToString();
                    daoRecebimentos.Radm = campo[55].ToString();
                    daoRecebimentos.Administrador = campo[56].ToString();
                    daoRecebimentos.ComissaoAdministr = campo[57].ToString();
                    daoRecebimentos.SeqRcbto = campo[58].ToString();
                    daoRecebimentos.DataRcnto = campo[59].ToString();
                    daoRecebimentos.DataCredito = campo[60].ToString();
                    try
                    {
                        daoRecebimentos.ValorRecebido = Convert.ToDecimal(campo[61].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.ValorRecebido = 0;
                    }
                    try
                    {
                        daoRecebimentos.ValorJuros = Convert.ToDecimal(campo[62].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.ValorJuros = 0;
                    }
                    try
                    {
                        daoRecebimentos.ValorDesconto = Convert.ToDecimal(campo[63].ToString());
                    }
                    catch 
                    {
                        daoRecebimentos.ValorDesconto = 0;
                    }                    
                    daoRecebimentos.HisRcbto = campo[64].ToString();
                    daoRecebimentos.HistoricoRcbto = campo[65].ToString();
                    daoRecebimentos.NumeroDocumento = campo[66].ToString();
                    daoRecebimentos.DoctoRcbto = campo[67].ToString();
                    daoRecebimentos.PorRcbto = campo[68].ToString();
                    daoRecebimentos.PortadorRcbto = campo[69].ToString();
                    daoRecebimentos.ContaCorrenteRcbto = campo[70].ToString();
                    daoRecebimentos.NumContabilRcbto = campo[71].ToString();
                    daoRecebimentos.Atraso = campo[72].ToString();

                    daoRecebimentosList.Add(daoRecebimentos);

                }
            }
            return daoRecebimentosList;
        }

        public string InserirDadosBD(DAORecebimentosList daoRecebimentosList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspRecebimentosDeletar");

            try
            {
                DataTable dataTableRecebimentosList = ConvertToDataTable(daoRecebimentosList);
                foreach (DataRow linha in dataTableRecebimentosList.Rows)
                {
                    DAORecebimentos daoRecebimentos = new DAORecebimentos();

                    daoRecebimentos.Empresa = linha["Empresa"].ToString();
                    daoRecebimentos.NumDuplicata = linha["NumDuplicata"].ToString();
                    daoRecebimentos.Parcela = linha["Parcela"].ToString();
                    daoRecebimentos.TipoTitulo = linha["TipoTitulo"].ToString();
                    daoRecebimentos.SituacaoDuplic = linha["SituacaoDuplic"].ToString();
                    daoRecebimentos.Emissao = linha["Emissao"].ToString();
                    daoRecebimentos.VencOriginal = linha["VencOriginal"].ToString();
                    daoRecebimentos.Vencimento = linha["Vencimento"].ToString();
                    daoRecebimentos.CodCliente = linha["CodCliente"].ToString();
                    daoRecebimentos.Cliente = linha["Cliente"].ToString();
                    daoRecebimentos.CodResponsavel = linha["CodResponsavel"].ToString();
                    daoRecebimentos.Responsavel = linha["Responsavel"].ToString();
                    daoRecebimentos.CodEndosso = linha["CodEndosso"].ToString();
                    daoRecebimentos.Endosso = linha["Endosso"].ToString();
                    daoRecebimentos.PedidoVenda = linha["PedidoVenda"].ToString();
                    daoRecebimentos.Representante = linha["Representante"].ToString();
                    daoRecebimentos.PercComissao = Convert.ToDecimal(linha["PercComissao"].ToString());
                    daoRecebimentos.BaseComissao = Convert.ToDecimal(linha["BaseComissao"].ToString());
                    daoRecebimentos.ValorComissao = Convert.ToDecimal(linha["ValorComissao"].ToString());
                    daoRecebimentos.Portador = linha["Portador"].ToString();
                    daoRecebimentos.NumeroBordero = linha["NumeroBordero"].ToString();
                    daoRecebimentos.NumeroRemessa = linha["NumeroRemessa"].ToString();
                    daoRecebimentos.NrTituloBanco = linha["NrTituloBanco"].ToString();
                    daoRecebimentos.ContaCorrente = linha["ContaCorrente"].ToString();
                    daoRecebimentos.CodCarteira = linha["CodCarteira"].ToString();
                    daoRecebimentos.Transacao = linha["Transacao"].ToString();
                    daoRecebimentos.PercDesconto = linha["PercDesconto"].ToString();
                    daoRecebimentos.NrSolicitacao = linha["NrSolicitacao"].ToString();
                    daoRecebimentos.ValorDuplicata = Convert.ToDecimal(linha["ValorDuplicata"].ToString());
                    daoRecebimentos.SaldoDuplicata = Convert.ToDecimal(linha["SaldoDuplicata"].ToString());
                    daoRecebimentos.Moeda = linha["Moeda"].ToString();
                    daoRecebimentos.Posicao = linha["Posicao"].ToString();
                    daoRecebimentos.LocalEmpresa = linha["LocalEmpresa"].ToString();
                    daoRecebimentos.SituacaoDuplicata = linha["SituacaoDuplicata"].ToString();
                    daoRecebimentos.Historico = linha["Historico"].ToString();
                    daoRecebimentos.ComplHistorico = linha["ComplHistorico"].ToString();
                    daoRecebimentos.NumContabil = linha["NumContabil"].ToString();
                    daoRecebimentos.FormaPagto = linha["FormaPagto"].ToString();
                    daoRecebimentos.CodBarras = linha["CodBarras"].ToString();
                    daoRecebimentos.LinhaDigitavel = linha["LinhaDigitavel"].ToString();
                    daoRecebimentos.DuplicImpressa = linha["DuplicImpressa"].ToString();
                    daoRecebimentos.Previsao = linha["Previsao"].ToString();
                    daoRecebimentos.NumeroTitulo = linha["NumeroTitulo"].ToString();
                    daoRecebimentos.NotaFiscal = linha["NotaFiscal"].ToString();
                    daoRecebimentos.Serie = linha["Serie"].ToString();
                    daoRecebimentos.CodCancelamento = linha["CodCancelamento"].ToString();
                    daoRecebimentos.Cancelamento = linha["Cancelamento"].ToString();
                    daoRecebimentos.CodigoContabil = linha["CodigoContabil"].ToString();
                    daoRecebimentos.ValorMoeda = Convert.ToDecimal(linha["ValorMoeda"].ToString());
                    daoRecebimentos.CodUsuario = linha["CodUsuario"].ToString();
                    daoRecebimentos.NumeroCaixa = linha["NumeroCaixa"].ToString();
                    daoRecebimentos.NrAdiantamento = linha["NrAdiantamento"].ToString();
                    daoRecebimentos.FantasiaCliente = linha["FantasiaCliente"].ToString();
                    daoRecebimentos.TelefoneCliente = linha["TelefoneCliente"].ToString();
                    daoRecebimentos.emailCliente = linha["emailCliente"].ToString();
                    daoRecebimentos.Radm = linha["Radm"].ToString();
                    daoRecebimentos.Administrador = linha["Administrador"].ToString();
                    daoRecebimentos.ComissaoAdministr = linha["ComissaoAdministr"].ToString();
                    daoRecebimentos.SeqRcbto = linha["SeqRcbto"].ToString();
                    daoRecebimentos.DataRcnto = linha["DataRcnto"].ToString();
                    daoRecebimentos.DataCredito = linha["DataCredito"].ToString();
                    daoRecebimentos.ValorRecebido = Convert.ToDecimal(linha["ValorRecebido"].ToString());
                    daoRecebimentos.ValorJuros = Convert.ToDecimal(linha["ValorJuros"].ToString());
                    daoRecebimentos.ValorDesconto = Convert.ToDecimal(linha["ValorDesconto"].ToString());
                    daoRecebimentos.HisRcbto = linha["HisRcbto"].ToString();
                    daoRecebimentos.HistoricoRcbto = linha["HistoricoRcbto"].ToString();
                    daoRecebimentos.NumeroDocumento = linha["NumeroDocumento"].ToString();
                    daoRecebimentos.DoctoRcbto = linha["DoctoRcbto"].ToString();
                    daoRecebimentos.PorRcbto = linha["PorRcbto"].ToString();
                    daoRecebimentos.PortadorRcbto = linha["PortadorRcbto"].ToString();
                    daoRecebimentos.ContaCorrenteRcbto = linha["ContaCorrenteRcbto"].ToString();
                    daoRecebimentos.NumContabilRcbto = linha["NumContabilRcbto"].ToString();
                    daoRecebimentos.Atraso = linha["Atraso"].ToString();

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Empresa", daoRecebimentos.Empresa);
                    dalMySQL.AdicionaParametros("@NumDuplicata", daoRecebimentos.NumDuplicata);
                    dalMySQL.AdicionaParametros("@Parcela", daoRecebimentos.Parcela);
                    dalMySQL.AdicionaParametros("@TipoTitulo", daoRecebimentos.TipoTitulo);
                    dalMySQL.AdicionaParametros("@SituacaoDuplic", daoRecebimentos.SituacaoDuplic);
                    try
                    {
                        dalMySQL.AdicionaParametros("@Emissao", Convert.ToDateTime(daoRecebimentos.Emissao));
                    }
                    catch 
                    {
                        dalMySQL.AdicionaParametros("@Emissao", null);
                    }
                    try
                    {
                        dalMySQL.AdicionaParametros("@VencOriginal", Convert.ToDateTime(daoRecebimentos.VencOriginal));
                    }
                    catch 
                    {
                        dalMySQL.AdicionaParametros("@VencOriginal", null);
                    }
                    try
                    {
                        dalMySQL.AdicionaParametros("@Vencimento", Convert.ToDateTime(daoRecebimentos.Vencimento));
                    }
                    catch 
                    {
                        dalMySQL.AdicionaParametros("@Vencimento", null);
                    }                    
                    dalMySQL.AdicionaParametros("@CodCliente", daoRecebimentos.CodCliente);
                    dalMySQL.AdicionaParametros("@Cliente", daoRecebimentos.Cliente);
                    dalMySQL.AdicionaParametros("@CodResponsavel", daoRecebimentos.CodResponsavel);
                    dalMySQL.AdicionaParametros("@Responsavel", daoRecebimentos.Responsavel);
                    dalMySQL.AdicionaParametros("@CodEndosso", daoRecebimentos.CodEndosso);
                    dalMySQL.AdicionaParametros("@Endosso", daoRecebimentos.Endosso);
                    dalMySQL.AdicionaParametros("@PedidoVenda", daoRecebimentos.PedidoVenda);
                    dalMySQL.AdicionaParametros("@Representante", daoRecebimentos.Representante);
                    dalMySQL.AdicionaParametros("@PercComissao", daoRecebimentos.PercComissao);
                    dalMySQL.AdicionaParametros("@BaseComissao", daoRecebimentos.BaseComissao);
                    dalMySQL.AdicionaParametros("@ValorComissao", daoRecebimentos.ValorComissao);
                    dalMySQL.AdicionaParametros("@Portador", daoRecebimentos.Portador);
                    dalMySQL.AdicionaParametros("@NumeroBordero", daoRecebimentos.NumeroBordero);
                    dalMySQL.AdicionaParametros("@NumeroRemessa", daoRecebimentos.NumeroRemessa);
                    dalMySQL.AdicionaParametros("@NrTituloBanco", daoRecebimentos.NrTituloBanco);
                    dalMySQL.AdicionaParametros("@ContaCorrente", daoRecebimentos.ContaCorrente);
                    dalMySQL.AdicionaParametros("@CodCarteira", daoRecebimentos.CodCarteira);
                    dalMySQL.AdicionaParametros("@Transacao", daoRecebimentos.Transacao);
                    dalMySQL.AdicionaParametros("@PercDesconto", daoRecebimentos.PercDesconto);
                    dalMySQL.AdicionaParametros("@NrSolicitacao", daoRecebimentos.NrSolicitacao);
                    dalMySQL.AdicionaParametros("@ValorDuplicata", daoRecebimentos.ValorDuplicata);
                    dalMySQL.AdicionaParametros("@SaldoDuplicata", daoRecebimentos.SaldoDuplicata);
                    dalMySQL.AdicionaParametros("@Moeda", daoRecebimentos.Moeda);
                    dalMySQL.AdicionaParametros("@Posicao", daoRecebimentos.Posicao);
                    dalMySQL.AdicionaParametros("@LocalEmpresa", daoRecebimentos.LocalEmpresa);
                    dalMySQL.AdicionaParametros("@SituacaoDuplicata", daoRecebimentos.SituacaoDuplicata);
                    dalMySQL.AdicionaParametros("@Historico", daoRecebimentos.Historico);
                    dalMySQL.AdicionaParametros("@ComplHistorico", daoRecebimentos.ComplHistorico);
                    dalMySQL.AdicionaParametros("@NumContabil", daoRecebimentos.NumContabil);
                    dalMySQL.AdicionaParametros("@FormaPagto", daoRecebimentos.FormaPagto);
                    dalMySQL.AdicionaParametros("@CodBarras", daoRecebimentos.CodBarras);
                    dalMySQL.AdicionaParametros("@LinhaDigitavel", daoRecebimentos.LinhaDigitavel);
                    dalMySQL.AdicionaParametros("@DuplicImpressa", daoRecebimentos.DuplicImpressa);
                    dalMySQL.AdicionaParametros("@Previsao", daoRecebimentos.Previsao);
                    dalMySQL.AdicionaParametros("@NumeroTitulo", daoRecebimentos.NumeroTitulo);
                    dalMySQL.AdicionaParametros("@NotaFiscal", daoRecebimentos.NotaFiscal);
                    dalMySQL.AdicionaParametros("@Serie", daoRecebimentos.Serie);
                    dalMySQL.AdicionaParametros("@CodCancelamento", daoRecebimentos.CodCancelamento);
                    dalMySQL.AdicionaParametros("@Cancelamento", daoRecebimentos.Cancelamento);
                    dalMySQL.AdicionaParametros("@CodigoContabil", daoRecebimentos.CodigoContabil);
                    dalMySQL.AdicionaParametros("@ValorMoeda", daoRecebimentos.ValorMoeda);
                    dalMySQL.AdicionaParametros("@CodUsuario", daoRecebimentos.CodUsuario);
                    dalMySQL.AdicionaParametros("@NumeroCaixa", daoRecebimentos.NumeroCaixa);
                    dalMySQL.AdicionaParametros("@NrAdiantamento", daoRecebimentos.NrAdiantamento);
                    dalMySQL.AdicionaParametros("@FantasiaCliente", daoRecebimentos.FantasiaCliente);
                    dalMySQL.AdicionaParametros("@TelefoneCliente", daoRecebimentos.TelefoneCliente);
                    dalMySQL.AdicionaParametros("@emailCliente", daoRecebimentos.emailCliente);
                    dalMySQL.AdicionaParametros("@Radm", daoRecebimentos.Radm);
                    dalMySQL.AdicionaParametros("@Administrador", daoRecebimentos.Administrador);
                    dalMySQL.AdicionaParametros("@ComissaoAdministr", daoRecebimentos.ComissaoAdministr);
                    dalMySQL.AdicionaParametros("@SeqRcbto", daoRecebimentos.SeqRcbto);
                    try
                    {
                        dalMySQL.AdicionaParametros("@DataRcnto", Convert.ToDateTime(daoRecebimentos.DataRcnto));
                    }
                    catch 
                    {
                        dalMySQL.AdicionaParametros("@DataRcnto", null);
                    }
                    try
                    {
                        dalMySQL.AdicionaParametros("@DataCredito", Convert.ToDateTime(daoRecebimentos.DataCredito));
                    }
                    catch 
                    {
                        dalMySQL.AdicionaParametros("@DataCredito", null);
                    }                    
                    dalMySQL.AdicionaParametros("@ValorRecebido", daoRecebimentos.ValorRecebido);
                    dalMySQL.AdicionaParametros("@ValorJuros", daoRecebimentos.ValorJuros);
                    dalMySQL.AdicionaParametros("@ValorDesconto", daoRecebimentos.ValorDesconto);
                    dalMySQL.AdicionaParametros("@HisRcbto", daoRecebimentos.HisRcbto);
                    dalMySQL.AdicionaParametros("@HistoricoRcbto", daoRecebimentos.HistoricoRcbto);
                    dalMySQL.AdicionaParametros("@NumeroDocumento", daoRecebimentos.NumeroDocumento);
                    dalMySQL.AdicionaParametros("@DoctoRcbto", daoRecebimentos.DoctoRcbto);
                    dalMySQL.AdicionaParametros("@PorRcbto", daoRecebimentos.PorRcbto);
                    dalMySQL.AdicionaParametros("@PortadorRcbto", daoRecebimentos.PortadorRcbto);
                    dalMySQL.AdicionaParametros("@ContaCorrenteRcbto", daoRecebimentos.ContaCorrenteRcbto);
                    dalMySQL.AdicionaParametros("@NumContabilRcbto", daoRecebimentos.NumContabilRcbto);
                    dalMySQL.AdicionaParametros("@Atraso", daoRecebimentos.Atraso);

                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspRecebimentosInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: financeiro recebimentos inserida. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir financeiro recebimentos Detalhes: " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de financeiro recebimentos deletadas. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de financeiro recebimentos renomeada. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}
