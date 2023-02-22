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
            DAOSaldosEstoqueList daoSaldosEstoqueList = new DAOSaldosEstoqueList();
            var csv = new StreamReader(File.OpenRead(path), Encoding.UTF7);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOSaldosEstoque daoSaldosEstoque = new DAOSaldosEstoque();
                campo = linha.Split(';');
                index++;

                if (index > 1)
                {
                    daoSaldosEstoque.Emp = campo[0].ToString();
                    daoSaldosEstoque.Empresa = campo[1].ToString();
                    daoSaldosEstoque.Dep = campo[2].ToString();
                    daoSaldosEstoque.Deposito = campo[3].ToString();
                    daoSaldosEstoque.Nivel = campo[4].ToString();
                    daoSaldosEstoque.Grupo = campo[5].ToString();
                    daoSaldosEstoque.Sub = campo[6].ToString();
                    daoSaldosEstoque.Cor = campo[7].ToString();
                    daoSaldosEstoque.Produto = campo[8].ToString();
                    daoSaldosEstoque.Um = campo[9].ToString();
                    daoSaldosEstoque.CodigoBarras = campo[10].ToString();
                    daoSaldosEstoque.CodigoVelho = campo[11].ToString();
                    daoSaldosEstoque.NomeGrupo = campo[12].ToString();
                    daoSaldosEstoque.NomeSub = campo[13].ToString();
                    daoSaldosEstoque.NomeCor = campo[14].ToString();
                    daoSaldosEstoque.Narrativa = campo[15].ToString();
                    daoSaldosEstoque.Cf = campo[16].ToString();
                    daoSaldosEstoque.Col = campo[17].ToString();
                    daoSaldosEstoque.Colecao = campo[18].ToString();
                    daoSaldosEstoque.Lin = campo[19].ToString();
                    daoSaldosEstoque.Linha = campo[20].ToString();
                    daoSaldosEstoque.Art = campo[21].ToString();
                    daoSaldosEstoque.Artigo = campo[22].ToString();
                    daoSaldosEstoque.Cota = campo[23].ToString();
                    daoSaldosEstoque.ArtigoCotas = campo[24].ToString();
                    daoSaldosEstoque.Ces = campo[25].ToString();
                    daoSaldosEstoque.ContaEstoque = campo[26].ToString();
                    daoSaldosEstoque.Tpg = campo[27].ToString();
                    daoSaldosEstoque.TipoProdutoGlobal = campo[28].ToString();
                    daoSaldosEstoque.TprogTpg = campo[29].ToString();
                    daoSaldosEstoque.NivTpg = campo[30].ToString();
                    daoSaldosEstoque.EstTpg = campo[31].ToString();
                    daoSaldosEstoque.DepTpg = campo[32].ToString();
                    daoSaldosEstoque.Cliente = campo[33].ToString();
                    daoSaldosEstoque.NomeCliente = campo[34].ToString();
                    daoSaldosEstoque.Marca = campo[35].ToString();
                    daoSaldosEstoque.NomeMarca = campo[36].ToString();
                    daoSaldosEstoque.TipoTecido = campo[37].ToString();
                    daoSaldosEstoque.Tpm = campo[38].ToString();
                    daoSaldosEstoque.Ncm = campo[39].ToString();
                    daoSaldosEstoque.Altp = campo[40].ToString();
                    daoSaldosEstoque.Rotp = campo[41].ToString();
                    daoSaldosEstoque.Antc = campo[42].ToString();
                    daoSaldosEstoque.Rotc = campo[43].ToString();
                    try
                    {
                        daoSaldosEstoque.ValorMedioEstoque = Convert.ToDecimal(campo[44].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.ValorMedioEstoque = 0;
                    }
                    try
                    {
                        daoSaldosEstoque.ValorUltimaCompra = Convert.ToDecimal(campo[45].ToString());
                    }
                    catch
                    {
                        daoSaldosEstoque.ValorUltimaCompra = 0;
                    }
                    try
                    {
                        daoSaldosEstoque.Custo = Convert.ToDecimal(campo[46].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.Custo = 0;
                    }
                    try
                    {
                        daoSaldosEstoque.CustoInformado = Convert.ToDecimal(campo[47].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.CustoInformado = 0;
                    }
                    
                    daoSaldosEstoque.Lead = campo[48].ToString();
                    daoSaldosEstoque.FamiliaTear = campo[49].ToString();
                    daoSaldosEstoque.LoteTam = campo[50].ToString();
                    try
                    {
                        daoSaldosEstoque.PesoLiquido = Convert.ToDecimal(campo[51].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.PesoLiquido = 0;
                    }
                    try
                    {
                        daoSaldosEstoque.PesoRolo = Convert.ToDecimal(campo[52].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.PesoRolo = 0;
                    }
                    try
                    {
                        daoSaldosEstoque.PesoMiniRolo = Convert.ToDecimal(campo[53].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.PesoMiniRolo = 0;
                    }
                    
                    daoSaldosEstoque.DescTamFicha = campo[54].ToString();
                    daoSaldosEstoque.TipoProdQuimico = campo[55].ToString();
                    daoSaldosEstoque.ItemAtivo = campo[56].ToString();
                    daoSaldosEstoque.CodigoContabil = campo[57].ToString();
                    daoSaldosEstoque.CodProcesso = campo[58].ToString();
                    daoSaldosEstoque.Lote = campo[59].ToString();
                    daoSaldosEstoque.LoteProduto = campo[60].ToString();
                    daoSaldosEstoque.SaldoAtual = campo[61].ToString();
                    try
                    {
                        daoSaldosEstoque.Volumes = Convert.ToInt32(campo[62].ToString());
                    }
                    catch
                    {
                        daoSaldosEstoque.Volumes = 0;
                    }
                    
                    try
                    {
                        daoSaldosEstoque.QtEstqInicioMes = Convert.ToDecimal(campo[63].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.QtEstqInicioMes = 0;
                    }
                    try
                    {
                        daoSaldosEstoque.QtEstqFinalMes = Convert.ToDecimal(campo[64].ToString());
                    }
                    catch
                    {
                        daoSaldosEstoque.QtEstqFinalMes = 0;
                    }
                    try
                    {
                        if (campo[65].ToString().Equals(""))
                            daoSaldosEstoque.UltimaEntrada = null;
                        else
                            daoSaldosEstoque.UltimaEntrada = campo[65].ToString();
                    }
                    catch 
                    {

                    }
                    try
                    {
                        if (campo[66].ToString().Equals(""))
                            daoSaldosEstoque.UltimaSaida = null;
                        else
                            daoSaldosEstoque.UltimaSaida = campo[66].ToString(); 
                        
                    }
                    catch 
                    {

                    }
                    try
                    {
                        daoSaldosEstoque.QtSugerida = Convert.ToDecimal(campo[67].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.QtSugerida = 0;
                    }
                    try
                    {
                        daoSaldosEstoque.QtEmpenhada = Convert.ToDecimal(campo[68].ToString());
                    }
                    catch 
                    {
                        daoSaldosEstoque.QtEmpenhada = 0;
                    }
                    daoSaldosEstoque.CnpjFornecedor = campo[69].ToString();
                    daoSaldosEstoque.NotaFiscal = campo[70].ToString();
                    try
                    {
                        if (campo[71].ToString().Equals(""))
                            daoSaldosEstoque.PeriodoEstoque = null;
                        else
                            daoSaldosEstoque.PeriodoEstoque = campo[71].ToString();
                    }
                    catch (Exception)
                    {

                        throw;
                    }


                    daoSaldosEstoqueList.Add(daoSaldosEstoque);

                }
            }
            return daoSaldosEstoqueList;
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