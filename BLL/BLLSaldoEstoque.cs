using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLSaldoEstoque
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

        public DAOSaldoEstoqueList LerCsv(string path)
        {
            DAOSaldoEstoqueList daoSaldoEstoqueList = new DAOSaldoEstoqueList();
            var csv = new StreamReader(File.OpenRead(path));
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOSaldoEstoque daoSaldoEstoque = new DAOSaldoEstoque();
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
                    daoSaldoEstoque.Lin = campo[18].ToString();
                    daoSaldoEstoque.Linha = campo[19].ToString();
                    daoSaldoEstoque.Art = campo[20].ToString();
                    daoSaldoEstoque.Cota = campo[21].ToString();
                    daoSaldoEstoque.ArtigoCotas = campo[22].ToString();
                    daoSaldoEstoque.Ces = campo[23].ToString();
                    daoSaldoEstoque.ContaEstoque = campo[24].ToString();
                    daoSaldoEstoque.Tpg = campo[25].ToString();
                    daoSaldoEstoque.TipoProdutoGlobal = campo[26].ToString();
                    daoSaldoEstoque.TprogTpg = campo[27].ToString();
                    daoSaldoEstoque.NivTpg = campo[28].ToString();
                    daoSaldoEstoque.EstTpg = campo[29].ToString();
                    daoSaldoEstoque.Cliente = campo[30].ToString();
                    daoSaldoEstoque.NomeCliente = campo[31].ToString();
                    daoSaldoEstoque.Marca = campo[32].ToString();
                    daoSaldoEstoque.NomeMarca = campo[33].ToString();
                    daoSaldoEstoque.TipoTecido = campo[34].ToString();
                    daoSaldoEstoque.Tpm = campo[35].ToString();
                    daoSaldoEstoque.Ncm = campo[36].ToString();
                    daoSaldoEstoque.Altp = campo[37].ToString();
                    daoSaldoEstoque.Rotp = campo[38].ToString();
                    daoSaldoEstoque.Antc = campo[39].ToString();
                    daoSaldoEstoque.Rotc = campo[40].ToString();
                    daoSaldoEstoque.ValorMedioEstoque = Convert.ToDecimal(campo[41].ToString());
                    daoSaldoEstoque.ValorUltimaCopmpra = Convert.ToDecimal(campo[42].ToString());
                    daoSaldoEstoque.Custo = Convert.ToDecimal(campo[43].ToString());
                    daoSaldoEstoque.CustoInformado = Convert.ToDecimal(campo[44].ToString());
                    daoSaldoEstoque.Lead = campo[45].ToString();
                    daoSaldoEstoque.FamiliaTear = campo[46].ToString();
                    daoSaldoEstoque.LoteTam = campo[47].ToString();
                    daoSaldoEstoque.PesoLiquido = Convert.ToDecimal(campo[48].ToString());
                    daoSaldoEstoque.PesoRolo = Convert.ToDecimal(campo[49].ToString());
                    daoSaldoEstoque.PesoMinRolo = Convert.ToDecimal(campo[50].ToString());
                    daoSaldoEstoque.DescTamFicha = campo[51].ToString();
                    daoSaldoEstoque.TipoProdQuimico = campo[52].ToString();
                    daoSaldoEstoque.ItemAtivo = campo[53].ToString();
                    daoSaldoEstoque.CodigoContabil = campo[54].ToString();
                    daoSaldoEstoque.CodProcesso = campo[55].ToString();
                    daoSaldoEstoque.Lote = campo[56].ToString();
                    daoSaldoEstoque.LoteProduto = campo[57].ToString();
                    daoSaldoEstoque.SaldoAtual = campo[58].ToString();
                    daoSaldoEstoque.Volumes = Convert.ToInt32(campo[59].ToString());
                    daoSaldoEstoque.QtEstqInicioMes = Convert.ToDecimal(campo[60].ToString());
                    daoSaldoEstoque.QtEstqFinalMes = Convert.ToDecimal(campo[61].ToString());
                    daoSaldoEstoque.UltimaEntrada = Convert.ToDateTime(campo[62].ToString());
                    daoSaldoEstoque.UltimaSaida = Convert.ToDateTime(campo[63].ToString());
                    daoSaldoEstoque.QtSugerida = Convert.ToDecimal(campo[64].ToString());
                    daoSaldoEstoque.QtEmpenhada = Convert.ToDecimal(campo[65].ToString());
                    daoSaldoEstoque.CnpjFornecedor = campo[66].ToString();
                    daoSaldoEstoque.NotaFiscal = campo[67].ToString();
                    daoSaldoEstoque.PeriodoEstoque = campo[68].ToString();


                    daoSaldoEstoqueList.Add(daoSaldoEstoque);

                }
            }
            return daoSaldoEstoqueList;
        }

        public string InserirDadosBD(DAOSaldoEstoqueList daoSaldoEstoqueList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";
            dalMySQL.LimparParametros();
            dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspSaldoEstoqueDeletar");

            try
            {
                DataTable dataTableSaldoEstoqueList = ConvertToDataTable(daoSaldoEstoqueList);
                foreach (DataRow linha in dataTableSaldoEstoqueList.Rows)
                {
                    DAOSaldoEstoque daoSaldoEstoque = new DAOSaldoEstoque();

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
                    daoSaldoEstoque.CodigoVelho = linha["CodigoVelho "].ToString();
                    daoSaldoEstoque.NomeGrupo = linha["NomeGrupo"].ToString();
                    daoSaldoEstoque.NomeSub = linha["NomeSub"].ToString();
                    daoSaldoEstoque.NomeCor = linha["NomeCor"].ToString();
                    daoSaldoEstoque.Narrativa = linha["Narrativa"].ToString();
                    daoSaldoEstoque.Col = linha["Col"].ToString();
                    daoSaldoEstoque.Lin = linha["Lin"].ToString();
                    daoSaldoEstoque.Linha = linha["Linha"].ToString();
                    daoSaldoEstoque.Art = linha["Art"].ToString();
                    daoSaldoEstoque.Cota = linha["Cota"].ToString();
                    daoSaldoEstoque.ArtigoCotas = linha["ArtigoCotas"].ToString();
                    daoSaldoEstoque.Ces = linha["Ces"].ToString();
                    daoSaldoEstoque.ContaEstoque = linha["ContaEstoque"].ToString();
                    daoSaldoEstoque.Tpg = linha["Tpg"].ToString();
                    daoSaldoEstoque.TipoProdutoGlobal = linha["TipoProdutoGlobal"].ToString();
                    daoSaldoEstoque.TprogTpg = linha["TprogTpg"].ToString();
                    daoSaldoEstoque.NivTpg = linha["NivTpg"].ToString();
                    daoSaldoEstoque.EstTpg = linha["EstTpg"].ToString();
                    daoSaldoEstoque.Cliente = linha["Cliente"].ToString();
                    daoSaldoEstoque.NomeCliente = linha["NomeCliente"].ToString();
                    daoSaldoEstoque.Marca = linha["Marca"].ToString();
                    daoSaldoEstoque.NomeMarca = linha["NomeMarca "].ToString();
                    daoSaldoEstoque.TipoTecido = linha["TipoTecido"].ToString();
                    daoSaldoEstoque.Tpm = linha["Tpm"].ToString();
                    daoSaldoEstoque.Ncm = linha["Ncm"].ToString();
                    daoSaldoEstoque.Altp = linha["Altp"].ToString();
                    daoSaldoEstoque.Rotp = linha["Rotp"].ToString();
                    daoSaldoEstoque.Antc = linha["Antc"].ToString();
                    daoSaldoEstoque.Rotc = linha["Rotc"].ToString();
                    daoSaldoEstoque.ValorMedioEstoque = Convert.ToDecimal(linha["ValorMedioEstoque"].ToString());
                    daoSaldoEstoque.ValorUltimaCopmpra = Convert.ToDecimal(linha["ValorUltimaCopmpra"].ToString());
                    daoSaldoEstoque.Custo = Convert.ToDecimal(linha["Custo"].ToString());
                    daoSaldoEstoque.CustoInformado = Convert.ToDecimal(linha["CustoInformado"].ToString());
                    daoSaldoEstoque.Lead = linha["Lead"].ToString();
                    daoSaldoEstoque.FamiliaTear = linha["FamiliaTear"].ToString();
                    daoSaldoEstoque.LoteTam = linha["LoteTam"].ToString();
                    daoSaldoEstoque.PesoLiquido = Convert.ToDecimal(linha["PesoLiquido"].ToString());
                    daoSaldoEstoque.PesoRolo = Convert.ToDecimal(linha["PesoRolo"].ToString());
                    daoSaldoEstoque.PesoMinRolo = Convert.ToDecimal(linha["PesoMinRolo"].ToString());
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
                    daoSaldoEstoque.UltimaEntrada = Convert.ToDateTime(linha["UltimaEntrada"].ToString());
                    daoSaldoEstoque.UltimaSaida = Convert.ToDateTime(linha["UltimaSaida"].ToString());
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
                    dalMySQL.AdicionaParametros("@Nivel", daoSaldoEstoque.Nivel);
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
                    dalMySQL.AdicionaParametros("@Lin", daoSaldoEstoque.Lin);
                    dalMySQL.AdicionaParametros("@Linha", daoSaldoEstoque.Linha);
                    dalMySQL.AdicionaParametros("@Art", daoSaldoEstoque.Art);
                    dalMySQL.AdicionaParametros("@Cota", daoSaldoEstoque.Cota);
                    dalMySQL.AdicionaParametros("@ArtigoCotas", daoSaldoEstoque.ArtigoCotas);
                    dalMySQL.AdicionaParametros("@Ces", daoSaldoEstoque.Ces);
                    dalMySQL.AdicionaParametros("@ContaEstoque", daoSaldoEstoque.ContaEstoque);
                    dalMySQL.AdicionaParametros("@Tpg", daoSaldoEstoque.Tpg);
                    dalMySQL.AdicionaParametros("@TipoProdutoGlobal", daoSaldoEstoque.TprogTpg);
                    dalMySQL.AdicionaParametros("@TprogTpg", daoSaldoEstoque.TprogTpg);
                    dalMySQL.AdicionaParametros("@NivTpg", daoSaldoEstoque.NivTpg);
                    dalMySQL.AdicionaParametros("@EstTpg", daoSaldoEstoque.EstTpg);
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
                    dalMySQL.AdicionaParametros("@ValorUltimaCopmpra", daoSaldoEstoque.ValorUltimaCopmpra);
                    dalMySQL.AdicionaParametros("@Custo", daoSaldoEstoque.Custo);
                    dalMySQL.AdicionaParametros("@CustoInformado", daoSaldoEstoque.CustoInformado);
                    dalMySQL.AdicionaParametros("@Lead", daoSaldoEstoque.Lead);
                    dalMySQL.AdicionaParametros("@FamiliaTear", daoSaldoEstoque.FamiliaTear);
                    dalMySQL.AdicionaParametros("@LoteTam", daoSaldoEstoque.LoteTam);
                    dalMySQL.AdicionaParametros("@PesoLiquido", daoSaldoEstoque.PesoLiquido);
                    dalMySQL.AdicionaParametros("@PesoRolo", daoSaldoEstoque.PesoRolo);
                    dalMySQL.AdicionaParametros("@PesoMinRolo", daoSaldoEstoque.PesoMinRolo);
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
                    dalMySQL.AdicionaParametros("@UltimaEntrada", daoSaldoEstoque.UltimaEntrada);
                    dalMySQL.AdicionaParametros("@UltimaSaida", daoSaldoEstoque.UltimaSaida);
                    dalMySQL.AdicionaParametros("@QtSugerida", daoSaldoEstoque.QtSugerida);
                    dalMySQL.AdicionaParametros("@QtEmpenhada", daoSaldoEstoque.QtEmpenhada);
                    dalMySQL.AdicionaParametros("@CnpjFornecedor", daoSaldoEstoque.CnpjFornecedor);
                    dalMySQL.AdicionaParametros("@NotaFiscal", daoSaldoEstoque.NotaFiscal);
                    dalMySQL.AdicionaParametros("@PeriodoEstoque", daoSaldoEstoque.PeriodoEstoque);
                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspSaldoEstoqueInserir");
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