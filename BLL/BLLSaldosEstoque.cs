using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;
using System;

namespace BLL
{
    public class BLLSaldosEstoque
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de saldo de estoque encontrado, nome: " + arquivoNome + ".  Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + data);
                }

            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: Relatório de saldo de estoque não encontrado, nome: nulo. Detalhes: Classe: BLLContasPagas.cs | Metodo: PegarNomeArquivo | " + ex.Message.ToString() + " | " + data);
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
                    bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: saldo de estoque e renomeado para pasta destino. Detalhes: " + retorno + " | " + data);
                }

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível mover e renomear saldo de estoque. Detalhes: " + retorno + " | " + data);
            }

            return retorno;


        }

        public DAOSaldosEstoqueList LerCsv(string path)
        {
            DAOSaldosEstoqueList daoSaldoEstoqueList = new DAOSaldosEstoqueList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOSaldosEstoque daoSaldoEstoque = new DAOSaldosEstoque();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoSaldoEstoque.Emp = campo[0].ToString();
                    daoSaldoEstoque.Empresa = campo[1].ToString();
                    daoSaldoEstoque.Dep = campo[2].ToString();
                    daoSaldoEstoque.Deposito = campo[3].ToString();
                    daoSaldoEstoque.Nivel = campo[4].ToString();
                    daoSaldoEstoque.Grupo = campo[5].ToString();
                    daoSaldoEstoque.Sub = campo[6].ToString();
                    daoSaldoEstoque.Cor = campo[7].ToString();
                    daoSaldoEstoque.Produto = campo[8].ToString();
                    daoSaldoEstoque.Um = campo[9].ToString();
                    daoSaldoEstoque.CodigoBarras = campo[10].ToString();
                    daoSaldoEstoque.CodigoVelho = campo[11].ToString();
                    daoSaldoEstoque.NomeGrupo = campo[12].ToString();
                    daoSaldoEstoque.NomeSub = campo[13].ToString();
                    daoSaldoEstoque.NomeCor = campo[14].ToString();
                    daoSaldoEstoque.Narrativa = campo[15].ToString();
                    daoSaldoEstoque.Cf = campo[16].ToString();
                    daoSaldoEstoque.Col = campo[17].ToString();
                    daoSaldoEstoque.Colecao = campo[18].ToString();
                    daoSaldoEstoque.Lin = campo[19].ToString();
                    daoSaldoEstoque.Linha = campo[20].ToString();
                    daoSaldoEstoque.Art = campo[21].ToString();
                    daoSaldoEstoque.Artigo = campo[22].ToString();
                    daoSaldoEstoque.Cota = campo[23].ToString();
                    daoSaldoEstoque.ArtigoCotas = campo[24].ToString();
                    daoSaldoEstoque.Ces = campo[25].ToString();
                    daoSaldoEstoque.ContaEstoque = campo[26].ToString();
                    daoSaldoEstoque.Tpg = campo[27].ToString();
                    daoSaldoEstoque.TipoProdutoGlobal = campo[28].ToString();
                    daoSaldoEstoque.TprogTpg = campo[29].ToString();
                    daoSaldoEstoque.NivTpg = campo[30].ToString();
                    daoSaldoEstoque.EstTpg = campo[31].ToString();
                    daoSaldoEstoque.DepTpg = campo[32].ToString();
                    daoSaldoEstoque.Cliente = campo[33].ToString();
                    daoSaldoEstoque.NomeCliente = campo[34].ToString();
                    daoSaldoEstoque.Marca = campo[35].ToString();
                    daoSaldoEstoque.NomeMarca = campo[36].ToString();
                    daoSaldoEstoque.TipoTecido = campo[37].ToString();
                    daoSaldoEstoque.Tpm = campo[38].ToString();
                    daoSaldoEstoque.Ncm = campo[39].ToString();
                    daoSaldoEstoque.Altp = campo[40].ToString();
                    daoSaldoEstoque.Rotp = campo[41].ToString();
                    daoSaldoEstoque.Antc = campo[42].ToString();
                    daoSaldoEstoque.Rotc = campo[43].ToString();
                    daoSaldoEstoque.ValorMedioEstoque = Convert.ToDecimal(campo[44].ToString());
                    daoSaldoEstoque.ValorUltimaCompra = Convert.ToDecimal(campo[45].ToString());
                    daoSaldoEstoque.Custo = Convert.ToDecimal(campo[46].ToString());
                    daoSaldoEstoque.CustoInformado = Convert.ToDecimal(campo[47].ToString());
                    daoSaldoEstoque.Lead = campo[48].ToString();
                    daoSaldoEstoque.FamiliaTear = campo[49].ToString();
                    daoSaldoEstoque.LoteTam = campo[50].ToString();
                    daoSaldoEstoque.PesoLiquido = Convert.ToDecimal(campo[51].ToString());
                    daoSaldoEstoque.PesoRolo = Convert.ToDecimal(campo[52].ToString());
                    daoSaldoEstoque.PesoMiniRolo = Convert.ToDecimal(campo[53].ToString());
                    daoSaldoEstoque.DescTamFicha = campo[54].ToString();
                    daoSaldoEstoque.TipoProdQuimico = campo[55].ToString();
                    daoSaldoEstoque.ItemAtivo = campo[56].ToString();
                    daoSaldoEstoque.CodigoContabil = campo[57].ToString();
                    daoSaldoEstoque.CodProcesso = campo[58].ToString();
                    daoSaldoEstoque.Lote = campo[59].ToString();
                    daoSaldoEstoque.LoteProduto = campo[60].ToString();
                    daoSaldoEstoque.SaldoAtual = campo[61].ToString();
                    daoSaldoEstoque.Volumes = Convert.ToInt32(campo[62].ToString());
                    try
                    {
                        daoSaldoEstoque.QtEstqInicioMes = Convert.ToDecimal(campo[63].ToString());
                    }
                    catch 
                    {
                        daoSaldoEstoque.QtEstqInicioMes = 0;
                    }
                    try
                    {
                        daoSaldoEstoque.QtEstqFinalMes = Convert.ToDecimal(campo[64].ToString());
                    }
                    catch
                    {
                        daoSaldoEstoque.QtEstqFinalMes = 0;
                    }
                    try
                    {
                        if (campo[65].ToString().Equals(""))
                            daoSaldoEstoque.UltimaEntrada = null;
                        else
                            daoSaldoEstoque.UltimaEntrada = campo[65].ToString();
                    }
                    catch 
                    {

                    }
                    try
                    {
                        if (campo[66].ToString().Equals(""))
                            daoSaldoEstoque.UltimaSaida = null;
                        else
                            daoSaldoEstoque.UltimaSaida = campo[66].ToString(); 
                        
                    }
                    catch 
                    {

                    }
                    try
                    {
                        daoSaldoEstoque.QtSugerida = Convert.ToDecimal(campo[67].ToString());
                    }
                    catch 
                    {
                        daoSaldoEstoque.QtSugerida = 0;
                    }
                    try
                    {
                        daoSaldoEstoque.QtEmpenhada = Convert.ToDecimal(campo[68].ToString());
                    }
                    catch 
                    {
                        daoSaldoEstoque.QtEmpenhada = 0;
                    }                    
                    daoSaldoEstoque.CnpjFornecedor = campo[69].ToString();
                    daoSaldoEstoque.NotaFiscal = campo[70].ToString();
                    try
                    {
                        if (campo[71].ToString().Equals(""))
                            daoSaldoEstoque.PeriodoEstoque = null;
                        else
                            daoSaldoEstoque.PeriodoEstoque = campo[71].ToString();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    

                    daoSaldoEstoqueList.Add(daoSaldoEstoque);

                }
            }
            return daoSaldoEstoqueList;
        }

        public string InserirDadosBD(DAOSaldosEstoqueList daoSaldoEstoqueList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspSaldosEstoqueDeletar");

            try
            {
                DataTable dataTableSaldoEstoqueList = ConvertToDataTable(daoSaldoEstoqueList);
                foreach (DataRow linha in dataTableSaldoEstoqueList.Rows)
                {
                    DAOSaldosEstoque daoSaldoEstoque = new DAOSaldosEstoque();

                    daoSaldoEstoque.Emp = linha["Emp"].ToString();
                    daoSaldoEstoque.Empresa = linha["Empresa"].ToString();
                    daoSaldoEstoque.Dep = linha["Dep"].ToString();
                    daoSaldoEstoque.Deposito = linha["Deposito"].ToString();
                    daoSaldoEstoque.Nivel = linha["Nivel"].ToString();
                    daoSaldoEstoque.Grupo = linha["Grupo"].ToString();
                    daoSaldoEstoque.Sub = linha["Sub"].ToString();
                    daoSaldoEstoque.Cor = linha["Cor"].ToString();
                    daoSaldoEstoque.Produto = linha["Produto"].ToString();
                    daoSaldoEstoque.Um = linha["Um"].ToString();
                    daoSaldoEstoque.CodigoBarras = linha["CodigoBarras"].ToString();
                    daoSaldoEstoque.CodigoVelho = linha["CodigoVelho"].ToString();
                    daoSaldoEstoque.NomeGrupo = linha["NomeGrupo"].ToString();
                    daoSaldoEstoque.NomeSub = linha["NomeSub"].ToString();
                    daoSaldoEstoque.NomeCor = linha["NomeCor"].ToString();
                    daoSaldoEstoque.Narrativa = linha["Narrativa"].ToString();
                    daoSaldoEstoque.Col = linha["Col"].ToString();
                    daoSaldoEstoque.Colecao = linha["Colecao"].ToString();
                    daoSaldoEstoque.Lin = linha["Lin"].ToString();
                    daoSaldoEstoque.Linha = linha["Linha"].ToString();
                    daoSaldoEstoque.Art = linha["Art"].ToString();
                    daoSaldoEstoque.Artigo = linha["Art"].ToString();
                    daoSaldoEstoque.Cota = linha["Cota"].ToString();
                    daoSaldoEstoque.ArtigoCotas = linha["ArtigoCotas"].ToString();
                    daoSaldoEstoque.Ces = linha["Ces"].ToString();
                    daoSaldoEstoque.ContaEstoque = linha["ContaEstoque"].ToString();
                    daoSaldoEstoque.Tpg = linha["Tpg"].ToString();
                    daoSaldoEstoque.TipoProdutoGlobal = linha["TipoProdutoGlobal"].ToString();
                    daoSaldoEstoque.TprogTpg = linha["TprogTpg"].ToString();
                    daoSaldoEstoque.NivTpg = linha["NivTpg"].ToString();
                    daoSaldoEstoque.EstTpg = linha["EstTpg"].ToString();
                    daoSaldoEstoque.DepTpg = linha["DepTpg"].ToString();
                    daoSaldoEstoque.Cliente = linha["Cliente"].ToString();
                    daoSaldoEstoque.NomeCliente = linha["NomeCliente"].ToString();
                    daoSaldoEstoque.Marca = linha["Marca"].ToString();
                    daoSaldoEstoque.NomeMarca = linha["NomeMarca"].ToString();
                    daoSaldoEstoque.TipoTecido = linha["TipoTecido"].ToString();
                    daoSaldoEstoque.Tpm = linha["Tpm"].ToString();
                    daoSaldoEstoque.Ncm = linha["Ncm"].ToString();
                    daoSaldoEstoque.Altp = linha["Altp"].ToString();
                    daoSaldoEstoque.Rotp = linha["Rotp"].ToString();
                    daoSaldoEstoque.Antc = linha["Antc"].ToString();
                    daoSaldoEstoque.Rotc = linha["Rotc"].ToString();
                    daoSaldoEstoque.ValorMedioEstoque = Convert.ToDecimal(linha["ValorMedioEstoque"].ToString());
                    daoSaldoEstoque.ValorUltimaCompra = Convert.ToDecimal(linha["ValorUltimaCompra"].ToString());
                    daoSaldoEstoque.Custo = Convert.ToDecimal(linha["Custo"].ToString());
                    daoSaldoEstoque.CustoInformado = Convert.ToDecimal(linha["CustoInformado"].ToString());
                    daoSaldoEstoque.Lead = linha["Lead"].ToString();
                    daoSaldoEstoque.FamiliaTear = linha["FamiliaTear"].ToString();
                    daoSaldoEstoque.LoteTam = linha["LoteTam"].ToString();
                    daoSaldoEstoque.PesoLiquido = Convert.ToDecimal(linha["PesoLiquido"].ToString());
                    daoSaldoEstoque.PesoRolo = Convert.ToDecimal(linha["PesoRolo"].ToString());
                    daoSaldoEstoque.PesoMiniRolo = Convert.ToDecimal(linha["PesoMiniRolo"].ToString());
                    daoSaldoEstoque.DescTamFicha = linha["DescTamFicha"].ToString();
                    daoSaldoEstoque.TipoProdQuimico = linha["TipoProdQuimico"].ToString();
                    daoSaldoEstoque.ItemAtivo = linha["ItemAtivo"].ToString();
                    daoSaldoEstoque.CodigoContabil = linha["CodigoContabil"].ToString();
                    daoSaldoEstoque.CodProcesso = linha["CodProcesso"].ToString();
                    daoSaldoEstoque.Lote = linha["Lote"].ToString();
                    daoSaldoEstoque.LoteProduto = linha["LoteProduto"].ToString();
                    daoSaldoEstoque.SaldoAtual = linha["SaldoAtual"].ToString();
                    daoSaldoEstoque.Volumes = Convert.ToInt32(linha["Volumes"].ToString());
                    daoSaldoEstoque.QtEstqInicioMes = Convert.ToDecimal(linha["QtEstqInicioMes"].ToString());
                    daoSaldoEstoque.QtEstqFinalMes = Convert.ToDecimal(linha["QtEstqFinalMes"].ToString());
                    daoSaldoEstoque.UltimaEntrada = linha["UltimaEntrada"].ToString();
                    daoSaldoEstoque.UltimaSaida = linha["UltimaSaida"].ToString();
                    daoSaldoEstoque.QtSugerida = Convert.ToDecimal(linha["QtSugerida"].ToString());
                    daoSaldoEstoque.QtEmpenhada = Convert.ToDecimal(linha["QtEmpenhada"].ToString());
                    daoSaldoEstoque.CnpjFornecedor = linha["CnpjFornecedor"].ToString();
                    daoSaldoEstoque.NotaFiscal = linha["NotaFiscal"].ToString();
                    daoSaldoEstoque.PeriodoEstoque = linha["PeriodoEstoque"].ToString();

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Emp", daoSaldoEstoque.Emp);
                    dalMySQL.AdicionaParametros("@Empresa", daoSaldoEstoque.Empresa);
                    dalMySQL.AdicionaParametros("@Dep", daoSaldoEstoque.Dep);
                    dalMySQL.AdicionaParametros("@Deposito", daoSaldoEstoque.Deposito);
                    dalMySQL.AdicionaParametros("@Nivel", daoSaldoEstoque.Nivel);
                    dalMySQL.AdicionaParametros("@Grupo", daoSaldoEstoque.Grupo);
                    dalMySQL.AdicionaParametros("@Sub", daoSaldoEstoque.Sub);
                    dalMySQL.AdicionaParametros("@Cor", daoSaldoEstoque.Cor);
                    dalMySQL.AdicionaParametros("@Produto", daoSaldoEstoque.Produto);
                    dalMySQL.AdicionaParametros("@Um", daoSaldoEstoque.Um);
                    dalMySQL.AdicionaParametros("@CodigoBarras", daoSaldoEstoque.CodigoBarras);
                    dalMySQL.AdicionaParametros("@CodigoVelho", daoSaldoEstoque.CodigoVelho);
                    dalMySQL.AdicionaParametros("@NomeGrupo", daoSaldoEstoque.NomeGrupo);
                    dalMySQL.AdicionaParametros("@NomeSub", daoSaldoEstoque.NomeSub);
                    dalMySQL.AdicionaParametros("@NomeCor", daoSaldoEstoque.NomeCor);
                    dalMySQL.AdicionaParametros("@Narrativa", daoSaldoEstoque.Narrativa);
                    dalMySQL.AdicionaParametros("@Cf", daoSaldoEstoque.Cf);
                    dalMySQL.AdicionaParametros("@Col", daoSaldoEstoque.Col);
                    dalMySQL.AdicionaParametros("@Colecao", daoSaldoEstoque.Colecao);
                    dalMySQL.AdicionaParametros("@Lin", daoSaldoEstoque.Lin);
                    dalMySQL.AdicionaParametros("@Linha", daoSaldoEstoque.Linha);
                    dalMySQL.AdicionaParametros("@Art", daoSaldoEstoque.Art);
                    dalMySQL.AdicionaParametros("@Artigo", daoSaldoEstoque.Artigo);
                    dalMySQL.AdicionaParametros("@Cota", daoSaldoEstoque.Cota);
                    dalMySQL.AdicionaParametros("@ArtigoCotas", daoSaldoEstoque.ArtigoCotas);
                    dalMySQL.AdicionaParametros("@Ces", daoSaldoEstoque.Ces);
                    dalMySQL.AdicionaParametros("@ContaEstoque", daoSaldoEstoque.ContaEstoque);
                    dalMySQL.AdicionaParametros("@Tpg", daoSaldoEstoque.Tpg);
                    dalMySQL.AdicionaParametros("@TipoProdutoGlobal", daoSaldoEstoque.TprogTpg);
                    dalMySQL.AdicionaParametros("@TprogTpg", daoSaldoEstoque.TprogTpg);
                    dalMySQL.AdicionaParametros("@NivTpg", daoSaldoEstoque.NivTpg);
                    dalMySQL.AdicionaParametros("@EstTpg", daoSaldoEstoque.EstTpg);
                    dalMySQL.AdicionaParametros("@DepTpg", daoSaldoEstoque.DepTpg);
                    dalMySQL.AdicionaParametros("@Cliente", daoSaldoEstoque.Cliente);
                    dalMySQL.AdicionaParametros("@NomeCliente", daoSaldoEstoque.NomeCliente);
                    dalMySQL.AdicionaParametros("@Marca", daoSaldoEstoque.Marca);
                    dalMySQL.AdicionaParametros("@NomeMarca", daoSaldoEstoque.NomeMarca);
                    dalMySQL.AdicionaParametros("@TipoTecido", daoSaldoEstoque.TipoTecido);
                    dalMySQL.AdicionaParametros("@Tpm", daoSaldoEstoque.Tpm);
                    dalMySQL.AdicionaParametros("@Ncm", daoSaldoEstoque.Ncm);
                    dalMySQL.AdicionaParametros("@Altp", daoSaldoEstoque.Altp);
                    dalMySQL.AdicionaParametros("@Rotp", daoSaldoEstoque.Rotp);
                    dalMySQL.AdicionaParametros("@Antc", daoSaldoEstoque.Antc);
                    dalMySQL.AdicionaParametros("@Rotc", daoSaldoEstoque.Rotc);
                    dalMySQL.AdicionaParametros("@ValorMedioEstoque", daoSaldoEstoque.ValorMedioEstoque);
                    dalMySQL.AdicionaParametros("@ValorUltimaCompra", daoSaldoEstoque.ValorUltimaCompra);
                    dalMySQL.AdicionaParametros("@Custo", daoSaldoEstoque.Custo);
                    dalMySQL.AdicionaParametros("@CustoInformado", daoSaldoEstoque.CustoInformado);
                    dalMySQL.AdicionaParametros("@Lead", daoSaldoEstoque.Lead);
                    dalMySQL.AdicionaParametros("@FamiliaTear", daoSaldoEstoque.FamiliaTear);
                    dalMySQL.AdicionaParametros("@LoteTam", daoSaldoEstoque.LoteTam);
                    dalMySQL.AdicionaParametros("@PesoLiquido", daoSaldoEstoque.PesoLiquido);
                    dalMySQL.AdicionaParametros("@PesoRolo", daoSaldoEstoque.PesoRolo);
                    dalMySQL.AdicionaParametros("@PesoMiniRolo", daoSaldoEstoque.PesoMiniRolo);
                    dalMySQL.AdicionaParametros("@DescTamFicha", daoSaldoEstoque.DescTamFicha);
                    dalMySQL.AdicionaParametros("@TipoProdQuimico", daoSaldoEstoque.TipoProdQuimico);
                    dalMySQL.AdicionaParametros("@ItemAtivo", daoSaldoEstoque.ItemAtivo);
                    dalMySQL.AdicionaParametros("@CodigoContabil", daoSaldoEstoque.CodigoContabil);
                    dalMySQL.AdicionaParametros("@CodProcesso", daoSaldoEstoque.CodProcesso);
                    dalMySQL.AdicionaParametros("@Lote", daoSaldoEstoque.Lote);
                    dalMySQL.AdicionaParametros("@LoteProduto", daoSaldoEstoque.LoteProduto);
                    dalMySQL.AdicionaParametros("@SaldoAtual", daoSaldoEstoque.SaldoAtual);
                    dalMySQL.AdicionaParametros("@Volumes", daoSaldoEstoque.Volumes);
                    dalMySQL.AdicionaParametros("@QtEstqInicioMes", daoSaldoEstoque.QtEstqInicioMes);
                    dalMySQL.AdicionaParametros("@QtEstqFinalMes", daoSaldoEstoque.QtEstqFinalMes);

                    if (daoSaldoEstoque.UltimaEntrada.Equals(""))
                        dalMySQL.AdicionaParametros("@UltimaEntrada", null);
                    else
                        dalMySQL.AdicionaParametros("@UltimaEntrada", Convert.ToDateTime(daoSaldoEstoque.UltimaEntrada));
                    
                    if (daoSaldoEstoque.UltimaSaida.Equals(""))
                        dalMySQL.AdicionaParametros("@UltimaSaida", null);
                    else
                        dalMySQL.AdicionaParametros("@UltimaSaida", Convert.ToDateTime(daoSaldoEstoque.UltimaSaida));

                    dalMySQL.AdicionaParametros("@QtSugerida", daoSaldoEstoque.QtSugerida);
                    dalMySQL.AdicionaParametros("@QtEmpenhada", daoSaldoEstoque.QtEmpenhada);
                    dalMySQL.AdicionaParametros("@CnpjFornecedor", daoSaldoEstoque.CnpjFornecedor);
                    dalMySQL.AdicionaParametros("@NotaFiscal", daoSaldoEstoque.NotaFiscal);
                    if (daoSaldoEstoque.PeriodoEstoque.Equals(""))
                        dalMySQL.AdicionaParametros("@PeriodoEstoque", null);
                    else
                      dalMySQL.AdicionaParametros("@PeriodoEstoque", Convert.ToDateTime(daoSaldoEstoque.PeriodoEstoque));

                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspSaldosEstoqueInserir");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: saldo de estoque inserida. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível inserir saldo de estoque. Detalhes: " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Sucesso: Relatório de saldo de estoque deletadas. Detalhes: " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\integratextil\logs\logs.txt", "Erro: não foi possível deletar relatório de saldo de estoque renomeada. Detalhes: " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}