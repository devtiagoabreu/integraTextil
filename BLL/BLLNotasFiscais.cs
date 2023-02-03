using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;
using DAOs;

namespace BLL
{
    public class BLLNotasFiscais
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de notas fiscais encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de notas fiscais não encontrado, nome: nulo. Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: notas fiscais e renomeado para pasta destino. Detalhes: " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível mover e renomear notas fiscais. Detalhes: " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAONotasFiscaisList LerCsv(string path)
        {
            DAONotasFiscaisList daoNotasFiscaisList = new DAONotasFiscaisList();
            var csv = new StreamReader(File.OpenRead(path));
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAONotasFiscais daoNotasFiscais = new DAONotasFiscais();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoNotasFiscais.Emp = campo[0].ToString();
                    daoNotasFiscais.Cnpj = campo[1].ToString();
                    daoNotasFiscais.RazaoSocial = campo[2].ToString();
                    daoNotasFiscais.Fantasia = campo[3].ToString();
                    daoNotasFiscais.Nf = campo[4].ToString();
                    daoNotasFiscais.Seq = campo[5].ToString();
                    daoNotasFiscais.Natureza = campo[6].ToString();
                    daoNotasFiscais.Cfop = campo[7].ToString();
                    daoNotasFiscais.DescNatureza = campo[8].ToString();
                    daoNotasFiscais.DataMovto = Convert.ToDateTime(campo[9].ToString());
                    daoNotasFiscais.EntradaSaida = campo[10].ToString();
                    daoNotasFiscais.FaturamentoSimNao = campo[11].ToString();
                    daoNotasFiscais.ParametroNatFat = campo[12].ToString();
                    daoNotasFiscais.TipoTransacao = campo[13].ToString();
                    daoNotasFiscais.CodCanc = campo[14].ToString();
                    daoNotasFiscais.Produto = campo[15].ToString();
                    daoNotasFiscais.DescricaoItem = campo[16].ToString();
                    daoNotasFiscais.Um = campo[17].ToString();
                    daoNotasFiscais.Qtdesaida = Convert.ToDecimal(campo[18].ToString());
                    daoNotasFiscais.ValorSaida = Convert.ToDecimal(campo[19].ToString());
                    daoNotasFiscais.UnitarioSaida = Convert.ToDecimal(campo[20].ToString());
                    daoNotasFiscais.QtdeEntrada = Convert.ToDecimal(campo[21].ToString());
                    daoNotasFiscais.ValorEntrada = Convert.ToDecimal(campo[22].ToString());
                    daoNotasFiscais.UnitarioEntrada = Convert.ToDecimal(campo[23].ToString());
                    daoNotasFiscais.NfOrigem = campo[24].ToString();
                    daoNotasFiscais.Pedido = campo[25].ToString();
                    daoNotasFiscais.CnpjTransportadora = campo[26].ToString();
                    daoNotasFiscais.NomeTransportadora = campo[27].ToString();
                    daoNotasFiscais.Deposito = campo[28].ToString();
                    daoNotasFiscais.CentroCusto = campo[29].ToString();
                    daoNotasFiscais.Transacao = campo[30].ToString();
                    daoNotasFiscais.ClassificFiscal = campo[31].ToString();
                    daoNotasFiscais.ClassifContabil = campo[32].ToString();
                    daoNotasFiscais.CodigoContabil = campo[33].ToString();
                    daoNotasFiscais.BaseIpi = Convert.ToDecimal(campo[34].ToString());
                    daoNotasFiscais.PercIpi = Convert.ToDecimal(campo[35].ToString());
                    daoNotasFiscais.ValorIpi = Convert.ToDecimal(campo[36].ToString());
                    daoNotasFiscais.CvfIpi = Convert.ToDecimal(campo[37].ToString());
                    daoNotasFiscais.BaseIcms = Convert.ToDecimal(campo[38].ToString());
                    daoNotasFiscais.ValorIcms = Convert.ToDecimal(campo[39].ToString());
                    daoNotasFiscais.CvfIcms = Convert.ToDecimal(campo[40].ToString());
                    daoNotasFiscais.Procedencia = Convert.ToDecimal(campo[41].ToString());
                    daoNotasFiscais.BaseDiferenca = Convert.ToDecimal(campo[42].ToString());
                    daoNotasFiscais.CvfPis = Convert.ToDecimal(campo[43].ToString());
                    daoNotasFiscais.CvfCofins = Convert.ToDecimal(campo[44].ToString());
                    daoNotasFiscais.PercCofins = Convert.ToDecimal(campo[45].ToString());
                    daoNotasFiscais.BasePisCofins = Convert.ToDecimal(campo[46].ToString());
                    daoNotasFiscais.ValorPis = Convert.ToDecimal(campo[47].ToString());
                    daoNotasFiscais.ValorCofins = Convert.ToDecimal(campo[48].ToString());
                    daoNotasFiscais.PercSubtituicao = Convert.ToDecimal(campo[49].ToString());
                    daoNotasFiscais.BaseSubtituicao = Convert.ToDecimal(campo[50].ToString());
                    daoNotasFiscais.ValorSubtituicao = Convert.ToDecimal(campo[51].ToString());
                    daoNotasFiscais.Projeto = campo[52].ToString();

                    daoNotasFiscaisList.Add(daoNotasFiscais);

                }
            }
            return daoNotasFiscaisList;
        }

        public string InserirDadosBD(DAONotasFiscaisList daoNotasFiscaisList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspNotasFiscaisDeletar");

            try
            {
                DataTable dataTableNotasFiscaisList = ConvertToDataTable(daoNotasFiscaisList);
                foreach (DataRow linha in dataTableNotasFiscaisList.Rows)
                {
                    DAONotasFiscais daoNotasFiscais = new DAONotasFiscais();

                    daoNotasFiscais.Emp = linha["Emp"].ToString();
                    daoNotasFiscais.Cnpj = linha["Cnpj"].ToString();
                    daoNotasFiscais.RazaoSocial = linha["RazaoSocial"].ToString();
                    daoNotasFiscais.Fantasia = linha["Fantasia"].ToString();
                    daoNotasFiscais.Nf = linha["Nf"].ToString();
                    daoNotasFiscais.Seq = linha["Seq"].ToString();
                    daoNotasFiscais.Natureza = linha["Natureza"].ToString();
                    daoNotasFiscais.Cfop = linha["Cfop"].ToString();
                    daoNotasFiscais.DescNatureza = linha["DescNatureza"].ToString();
                    daoNotasFiscais.DataMovto = Convert.ToDateTime(linha["DataMovto"].ToString());
                    daoNotasFiscais.EntradaSaida = linha["EntradaSaida"].ToString();
                    daoNotasFiscais.FaturamentoSimNao = linha["FaturamentoSimNao "].ToString();
                    daoNotasFiscais.ParametroNatFat = linha["ParametroNatFat"].ToString();
                    daoNotasFiscais.TipoTransacao = linha["TipoTransacao"].ToString();
                    daoNotasFiscais.CodCanc = linha["CodCanc"].ToString();
                    daoNotasFiscais.Produto = linha["Produto"].ToString();
                    daoNotasFiscais.DescricaoItem = linha["DescricaoItem"].ToString();
                    daoNotasFiscais.Um = linha["Um"].ToString();
                    daoNotasFiscais.Qtdesaida = Convert.ToDecimal(linha["qtdesaida"].ToString());
                    daoNotasFiscais.ValorSaida = Convert.ToDecimal(linha["ValorSaida"].ToString());
                    daoNotasFiscais.UnitarioSaida = Convert.ToDecimal(linha["UnitarioSaida"].ToString());
                    daoNotasFiscais.QtdeEntrada = Convert.ToDecimal(linha["QtdeEntrada"].ToString());
                    daoNotasFiscais.ValorEntrada = Convert.ToDecimal(linha["ValorEntrada"].ToString());
                    daoNotasFiscais.UnitarioEntrada = Convert.ToDecimal(linha["UnitarioEntrada"].ToString());
                    daoNotasFiscais.NfOrigem = linha["NfOrigem"].ToString();
                    daoNotasFiscais.Pedido = linha["Pedido"].ToString();
                    daoNotasFiscais.CnpjTransportadora = linha["CnpjTransportadora"].ToString();
                    daoNotasFiscais.NomeTransportadora = linha["NomeTransportadora"].ToString();
                    daoNotasFiscais.Deposito = linha["Deposito"].ToString();
                    daoNotasFiscais.CentroCusto = linha["CentroCusto"].ToString();
                    daoNotasFiscais.Transacao = linha["Transacao"].ToString();
                    daoNotasFiscais.ClassificFiscal = linha["ClassificFiscal"].ToString();
                    daoNotasFiscais.ClassifContabil = linha["ClassifContabil "].ToString();
                    daoNotasFiscais.CodigoContabil = linha["CodigoContabil"].ToString();
                    daoNotasFiscais.BaseIpi = Convert.ToDecimal(linha["BaseIpi"].ToString());
                    daoNotasFiscais.PercIpi = Convert.ToDecimal(linha["PercIpi"].ToString());
                    daoNotasFiscais.ValorIpi = Convert.ToDecimal(linha["ValorIpi "].ToString());
                    daoNotasFiscais.CvfIpi = Convert.ToDecimal(linha["CvfIpi"].ToString());
                    daoNotasFiscais.BaseIcms = Convert.ToDecimal(linha["BaseIcms"].ToString());
                    daoNotasFiscais.ValorIcms = Convert.ToDecimal(linha["ValorIcms"].ToString());
                    daoNotasFiscais.CvfIcms = Convert.ToDecimal(linha["CvfIcms"].ToString());
                    daoNotasFiscais.Procedencia = Convert.ToDecimal(linha["Procedencia"].ToString());
                    daoNotasFiscais.BaseDiferenca = Convert.ToDecimal(linha["BaseDiferencia"].ToString());
                    daoNotasFiscais.CvfPis = Convert.ToDecimal(linha["CvfPis"].ToString());
                    daoNotasFiscais.CvfCofins = Convert.ToDecimal(linha["CvfConfins"].ToString());
                    daoNotasFiscais.PercCofins = Convert.ToDecimal(linha["PercConfins"].ToString());
                    daoNotasFiscais.BasePisCofins = Convert.ToDecimal(linha["BasePisCofins"].ToString());
                    daoNotasFiscais.ValorPis = Convert.ToDecimal(linha["ValorPis"].ToString());
                    daoNotasFiscais.ValorCofins = Convert.ToDecimal(linha["ValorCofins"].ToString());
                    daoNotasFiscais.PercSubtituicao = Convert.ToDecimal(linha["PercSubtituicao"].ToString());
                    daoNotasFiscais.BaseSubtituicao = Convert.ToDecimal(linha["BaseSubtituicao"].ToString());
                    daoNotasFiscais.ValorSubtituicao = Convert.ToDecimal(linha["ValorSubtituicao"].ToString());
                    daoNotasFiscais.Projeto = linha["Projeto"].ToString();

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Emp", daoNotasFiscais.Emp);
                    dalMySQL.AdicionaParametros("@Cnpj", daoNotasFiscais.Cnpj);
                    dalMySQL.AdicionaParametros("@RazaoSocial", daoNotasFiscais.RazaoSocial);
                    dalMySQL.AdicionaParametros("@Fantasia", daoNotasFiscais.Fantasia);
                    dalMySQL.AdicionaParametros("@Nf", daoNotasFiscais.Nf);
                    dalMySQL.AdicionaParametros("@Seq", daoNotasFiscais.Seq);
                    dalMySQL.AdicionaParametros("@Natureza", daoNotasFiscais.Natureza);
                    dalMySQL.AdicionaParametros("@Cfop", daoNotasFiscais.Cfop);
                    dalMySQL.AdicionaParametros("@DescNatureza", daoNotasFiscais.DescNatureza);
                    dalMySQL.AdicionaParametros("@DataMovto", daoNotasFiscais.DataMovto);
                    dalMySQL.AdicionaParametros("@EntradaSaida", daoNotasFiscais.EntradaSaida);
                    dalMySQL.AdicionaParametros("@FaturamentoSimNao", daoNotasFiscais.FaturamentoSimNao);
                    dalMySQL.AdicionaParametros("@ParametroNatFat", daoNotasFiscais.ParametroNatFat);
                    dalMySQL.AdicionaParametros("@TipoTransacao", daoNotasFiscais.TipoTransacao);
                    dalMySQL.AdicionaParametros("@CodCanc", daoNotasFiscais.CodCanc);
                    dalMySQL.AdicionaParametros("@Produto", daoNotasFiscais.Produto);
                    dalMySQL.AdicionaParametros("@DescricaoItem", daoNotasFiscais.DescricaoItem);
                    dalMySQL.AdicionaParametros("@Um", daoNotasFiscais.Um);
                    dalMySQL.AdicionaParametros("@qtdesaida", daoNotasFiscais.Qtdesaida);
                    dalMySQL.AdicionaParametros("@ValorSaida", daoNotasFiscais.ValorSaida);
                    dalMySQL.AdicionaParametros("@UnitarioSaida", daoNotasFiscais.UnitarioSaida);
                    dalMySQL.AdicionaParametros("@QtdeEntrada", daoNotasFiscais.QtdeEntrada);
                    dalMySQL.AdicionaParametros("@ValorEntrada", daoNotasFiscais.ValorEntrada);
                    dalMySQL.AdicionaParametros("@UnitarioEntrada", daoNotasFiscais.UnitarioEntrada);
                    dalMySQL.AdicionaParametros("@NfOrigem", daoNotasFiscais.NfOrigem);
                    dalMySQL.AdicionaParametros("@Pedido", daoNotasFiscais.Pedido);
                    dalMySQL.AdicionaParametros("@CnpjTransportadora", daoNotasFiscais.CnpjTransportadora);
                    dalMySQL.AdicionaParametros("@NomeTransportadora", daoNotasFiscais.NomeTransportadora);
                    dalMySQL.AdicionaParametros("@Deposito", daoNotasFiscais.Deposito);
                    dalMySQL.AdicionaParametros("@CentroCusto", daoNotasFiscais.CentroCusto);
                    dalMySQL.AdicionaParametros("@Transacao", daoNotasFiscais.Transacao);
                    dalMySQL.AdicionaParametros("@ClassificFiscal", daoNotasFiscais.ClassificFiscal);
                    dalMySQL.AdicionaParametros("@ClassifContabil", daoNotasFiscais.ClassifContabil);
                    dalMySQL.AdicionaParametros("@CodigoContabil", daoNotasFiscais.CodigoContabil);
                    dalMySQL.AdicionaParametros("@BaseIpi", daoNotasFiscais.BaseIpi);
                    dalMySQL.AdicionaParametros("@PercIpi", daoNotasFiscais.PercIpi);
                    dalMySQL.AdicionaParametros("@ValorIpi", daoNotasFiscais.ValorIpi);
                    dalMySQL.AdicionaParametros("@CvfIpi", daoNotasFiscais.CvfIpi);
                    dalMySQL.AdicionaParametros("@BaseIcms", daoNotasFiscais.BaseIcms);
                    dalMySQL.AdicionaParametros("@ValorIcms", daoNotasFiscais.ValorIcms);
                    dalMySQL.AdicionaParametros("@CvfIcms", daoNotasFiscais.CvfIcms);
                    dalMySQL.AdicionaParametros("@Procedencia", daoNotasFiscais.Procedencia);
                    dalMySQL.AdicionaParametros("@BaseDiferenca", daoNotasFiscais.BaseDiferenca);
                    dalMySQL.AdicionaParametros("@CvfPis", daoNotasFiscais.CvfPis);
                    dalMySQL.AdicionaParametros("@CvfCofins", daoNotasFiscais.CvfCofins);
                    dalMySQL.AdicionaParametros("@PercCofins", daoNotasFiscais.PercCofins);
                    dalMySQL.AdicionaParametros("@BasePisCofins", daoNotasFiscais.BasePisCofins);
                    dalMySQL.AdicionaParametros("@ValorPis", daoNotasFiscais.ValorPis);
                    dalMySQL.AdicionaParametros("@ValorCofins", daoNotasFiscais.ValorCofins);
                    dalMySQL.AdicionaParametros("@PercSubtituicao", daoNotasFiscais.PercSubtituicao);
                    dalMySQL.AdicionaParametros("@BaseSubtituicao", daoNotasFiscais.BaseSubtituicao);
                    dalMySQL.AdicionaParametros("@ValorSubtituicao", daoNotasFiscais.ValorSubtituicao);
                    dalMySQL.AdicionaParametros("@Projeto", daoNotasFiscais.Projeto);

                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspNotasFiscaisInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: notas fiscais inserida. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir notas fiscais. Detalhes: " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de notas fiscais deletadas. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de notas fiscais renomeada. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}