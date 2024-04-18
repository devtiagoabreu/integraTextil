using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLDadosTeares
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

            return arquivoNome;
        }

        public string RenomearArquivo(string arquivoNome, string arquivoNomeFinal, string pastaOrigem, string pastaDestino)
        {
            try
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

                return "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi Possivel mover production para pasta productionRenomeada. Detalhes: " + ex.Message);
            }


        }

        public DAODadosTearesList LerCSV(string caminhoArquivo)
        {
            DAODadosTearesList daoDadosTearesList = new DAODadosTearesList();
            StreamReader csv = new StreamReader(caminhoArquivo, Encoding.UTF8);
            string linha;
            string[] campo;
            int index = 0;
            
            while ((linha = csv.ReadLine()) != null)
            {

                DAODadosTeares daoDadosTeares = new DAODadosTeares();
                campo = linha.Split(",");
                index++;

                daoDadosTeares.DataTurno = campo[0].ToString();
                daoDadosTeares.Tear = campo[1].ToString();
                daoDadosTeares.Artigo = campo[2].ToString();
                daoDadosTeares.None = campo[3].ToString();
                daoDadosTeares.ArtigoGen = campo[4].ToString();
                daoDadosTeares.Rpm = campo[5].ToString();
                daoDadosTeares.Eficiencia = campo[6].ToString();
                daoDadosTeares.Funcionando = campo[7].ToString();
                daoDadosTeares.Parado = campo[8].ToString();
                daoDadosTeares.Pontos = campo[9].ToString();
                daoDadosTeares.Metros = campo[10].ToString();
                daoDadosTeares.Jardas = campo[11].ToString();
                daoDadosTeares.MedidaGen = campo[12].ToString();
                daoDadosTeares.QtdGen = campo[13].ToString();
                daoDadosTeares.MinGen = campo[14].ToString();
                daoDadosTeares.QtdParadasUrdume = campo[15].ToString();
                daoDadosTeares.MinParadasUrdume = campo[16].ToString();
                daoDadosTeares.QtdParadasOurelaFalsa = campo[17].ToString();
                daoDadosTeares.MinParadasOurelaFalsa = campo[18].ToString();
                daoDadosTeares.QtdParadasLenoDireita = campo[19].ToString();
                daoDadosTeares.MinParadasLenoDireita = campo[20].ToString();
                daoDadosTeares.QtdParadasLenoEsquerda = campo[21].ToString();
                daoDadosTeares.MinParadasLenoEsquerda = campo[22].ToString();
                daoDadosTeares.QtdParadasTrama = campo[23].ToString();
                daoDadosTeares.MinParadasTrama = campo[24].ToString();
                daoDadosTeares.QtdTrocaDeRolo = campo[25].ToString();
                daoDadosTeares.MinTrocaDeRolo = campo[26].ToString();
                daoDadosTeares.QtdCorteTecido = campo[27].ToString();
                daoDadosTeares.MinCorteTecido = campo[28].ToString();
                daoDadosTeares.QtdParadaManual = campo[29].ToString();
                daoDadosTeares.MinParadaManual = campo[30].ToString();
                daoDadosTeares.QtdEnergiaDesligada = campo[31].ToString();
                daoDadosTeares.MinEnergiaDesligada = campo[32].ToString();
                daoDadosTeares.QtdParadasOutras = campo[33].ToString();
                daoDadosTeares.MinParadasOutras = campo[34].ToString();
                daoDadosTeares.Wf11 = campo[35].ToString();
                daoDadosTeares.Wf12 = campo[36].ToString();
                daoDadosTeares.Wf21 = campo[37].ToString();
                daoDadosTeares.Wf22 = campo[38].ToString();
                daoDadosTeares.QtdGen1 = campo[39].ToString();
                daoDadosTeares.MinGen1 = campo[40].ToString();
                daoDadosTeares.QtdGen2 = campo[41].ToString();
                daoDadosTeares.MinGen2 = campo[42].ToString();
                daoDadosTeares.QtdGen3 = campo[43].ToString();
                daoDadosTeares.MinGen3 = campo[44].ToString();
                daoDadosTeares.QtdGen4 = campo[45].ToString();
                daoDadosTeares.MinGen4 = campo[46].ToString();
                daoDadosTeares.QtdGen5 = campo[47].ToString();
                daoDadosTeares.MinGen5 = campo[48].ToString();
                daoDadosTeares.QtdGen6 = campo[49].ToString();
                daoDadosTeares.MinGen6 = campo[50].ToString();
                daoDadosTeares.QtdGen7 = campo[51].ToString();
                daoDadosTeares.MinGen7 = campo[52].ToString();
                daoDadosTeares.QtdGen8 = campo[53].ToString();
                daoDadosTeares.MinGen8 = campo[54].ToString();
                daoDadosTeares.QtdGen9 = campo[55].ToString();
                daoDadosTeares.MinGen9 = campo[56].ToString();
                daoDadosTeares.QtdGen10 = campo[57].ToString();
                daoDadosTeares.MinGen10 = campo[58].ToString();
                daoDadosTeares.QtdGen11 = campo[59].ToString();
                daoDadosTeares.MinGen11 = campo[60].ToString();
                daoDadosTeares.QtdGen12 = campo[61].ToString();
                daoDadosTeares.MinGen12 = campo[62].ToString();
                daoDadosTeares.QtdGen13 = campo[63].ToString();
                daoDadosTeares.MinGen13 = campo[64].ToString();
                daoDadosTeares.QtdGen14 = campo[65].ToString();
                daoDadosTeares.MinGen14 = campo[66].ToString();
                daoDadosTeares.QtdGen15 = campo[67].ToString();
                daoDadosTeares.MinGen15 = campo[68].ToString();
                daoDadosTeares.QtdGen16 = campo[69].ToString();
                daoDadosTeares.MinGen16 = campo[70].ToString();

                daoDadosTearesList.Add(daoDadosTeares);
                
            }
            return daoDadosTearesList;
        }

        public string InserirDadosTeares(DAODadosTearesList daoDadosTearesList)
        {
            try
            {
                string retorno = "ok";
                DataTable dataTableDadosTearestList = ConvertToDataTable(daoDadosTearesList);
                foreach (DataRow linha in dataTableDadosTearestList.Rows)
                {
                    DAODadosTeares daoDadosTeares = new DAODadosTeares();

                    daoDadosTeares.DataTurno = linha["DataTurno"].ToString();
                    daoDadosTeares.Tear = linha["Tear"].ToString();
                    daoDadosTeares.Artigo = linha["Artigo"].ToString();
                    daoDadosTeares.None = linha["None"].ToString();
                    daoDadosTeares.ArtigoGen = linha["ArtigoGen"].ToString();
                    daoDadosTeares.Rpm = linha["Rpm"].ToString();
                    daoDadosTeares.Eficiencia = linha["Eficiencia"].ToString();
                    daoDadosTeares.Funcionando = linha["Funcionando"].ToString();
                    daoDadosTeares.Parado = linha["Parado"].ToString();
                    daoDadosTeares.Pontos = linha["Pontos"].ToString();
                    daoDadosTeares.Metros = linha["Metros"].ToString();
                    daoDadosTeares.Jardas = linha["Jardas"].ToString();
                    daoDadosTeares.MedidaGen = linha["MedidaGen"].ToString();
                    daoDadosTeares.QtdGen = linha["QtdGen"].ToString();
                    daoDadosTeares.MinGen = linha["MinGen"].ToString();
                    daoDadosTeares.QtdParadasUrdume = linha["QtdParadasUrdume"].ToString();
                    daoDadosTeares.MinParadasUrdume = linha["MinParadasUrdume"].ToString();
                    daoDadosTeares.QtdParadasOurelaFalsa = linha["QtdParadasOurelaFalsa"].ToString();
                    daoDadosTeares.MinParadasOurelaFalsa = linha["MinParadasOurelaFalsa"].ToString();
                    daoDadosTeares.QtdParadasLenoDireita = linha["QtdParadasLenoDireita"].ToString();
                    daoDadosTeares.MinParadasLenoDireita = linha["MinParadasLenoDireita"].ToString();
                    daoDadosTeares.QtdParadasLenoEsquerda = linha["QtdParadasLenoEsquerda"].ToString();
                    daoDadosTeares.MinParadasLenoEsquerda = linha["MinParadasLenoEsquerda"].ToString();
                    daoDadosTeares.QtdParadasTrama = linha["QtdParadasTrama"].ToString();
                    daoDadosTeares.MinParadasTrama = linha["MinParadasTrama"].ToString();
                    daoDadosTeares.QtdTrocaDeRolo = linha["QtdTrocaDeRolo"].ToString();
                    daoDadosTeares.MinTrocaDeRolo = linha["MinTrocaDeRolo"].ToString();
                    daoDadosTeares.QtdCorteTecido = linha["QtdCorteTecido"].ToString();
                    daoDadosTeares.MinCorteTecido = linha["MinCorteTecido"].ToString();
                    daoDadosTeares.QtdParadaManual = linha["QtdParadaManual"].ToString();
                    daoDadosTeares.MinParadaManual = linha["MinParadaManual"].ToString();
                    daoDadosTeares.QtdEnergiaDesligada = linha["QtdEnergiaDesligada"].ToString();
                    daoDadosTeares.MinEnergiaDesligada = linha["MinEnergiaDesligada"].ToString();
                    daoDadosTeares.QtdParadasOutras = linha["QtdParadasOutras"].ToString();
                    daoDadosTeares.MinParadasOutras = linha["MinParadasOutras"].ToString();
                    daoDadosTeares.Wf11 = linha["Wf11"].ToString();
                    daoDadosTeares.Wf12 = linha["Wf12"].ToString();
                    daoDadosTeares.Wf21 = linha["Wf21"].ToString();
                    daoDadosTeares.Wf22 = linha["Wf22"].ToString();
                    daoDadosTeares.QtdGen1 = linha["QtdGen1"].ToString();
                    daoDadosTeares.MinGen1 = linha["MinGen1"].ToString();
                    daoDadosTeares.QtdGen2 = linha["QtdGen2"].ToString();
                    daoDadosTeares.MinGen2 = linha["MinGen2"].ToString();
                    daoDadosTeares.QtdGen3 = linha["QtdGen3"].ToString();
                    daoDadosTeares.MinGen3 = linha["MinGen3"].ToString();
                    daoDadosTeares.QtdGen4 = linha["QtdGen4"].ToString();
                    daoDadosTeares.MinGen4 = linha["MinGen4"].ToString();
                    daoDadosTeares.QtdGen5 = linha["QtdGen5"].ToString();
                    daoDadosTeares.MinGen5 = linha["MinGen5"].ToString();
                    daoDadosTeares.QtdGen6 = linha["QtdGen6"].ToString();
                    daoDadosTeares.MinGen6 = linha["MinGen6"].ToString();
                    daoDadosTeares.QtdGen7 = linha["QtdGen7"].ToString();
                    daoDadosTeares.MinGen7 = linha["MinGen7"].ToString();
                    daoDadosTeares.QtdGen8 = linha["QtdGen8"].ToString();
                    daoDadosTeares.MinGen8 = linha["MinGen8"].ToString();
                    daoDadosTeares.QtdGen9 = linha["QtdGen9"].ToString();
                    daoDadosTeares.MinGen9 = linha["MinGen9"].ToString();
                    daoDadosTeares.QtdGen10 = linha["QtdGen10"].ToString();
                    daoDadosTeares.MinGen10 = linha["MinGen10"].ToString();
                    daoDadosTeares.QtdGen11 = linha["QtdGen11"].ToString();
                    daoDadosTeares.MinGen11 = linha["MinGen11"].ToString();
                    daoDadosTeares.QtdGen12 = linha["QtdGen12"].ToString();
                    daoDadosTeares.MinGen12 = linha["MinGen12"].ToString();
                    daoDadosTeares.QtdGen13 = linha["QtdGen13"].ToString();
                    daoDadosTeares.MinGen13 = linha["MinGen13"].ToString();
                    daoDadosTeares.QtdGen14 = linha["QtdGen14"].ToString();
                    daoDadosTeares.MinGen14 = linha["MinGen14"].ToString();
                    daoDadosTeares.QtdGen15 = linha["QtdGen15"].ToString();
                    daoDadosTeares.MinGen15 = linha["MinGen15"].ToString();
                    daoDadosTeares.QtdGen16 = linha["QtdGen16"].ToString();
                    daoDadosTeares.MinGen16 = linha["MinGen16"].ToString();

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@DataTurno", daoDadosTeares.DataTurno);
                    dalMySQL.AdicionaParametros("@Tear", daoDadosTeares.Tear);
                    dalMySQL.AdicionaParametros("@Artigo", daoDadosTeares.Artigo);
                    dalMySQL.AdicionaParametros("@None", daoDadosTeares.None);
                    dalMySQL.AdicionaParametros("@ArtigoGen", daoDadosTeares.ArtigoGen);
                    dalMySQL.AdicionaParametros("@Rpm", daoDadosTeares.Rpm);
                    dalMySQL.AdicionaParametros("@Eficiencia", daoDadosTeares.Eficiencia);
                    dalMySQL.AdicionaParametros("@Funcionando", daoDadosTeares.Funcionando);
                    dalMySQL.AdicionaParametros("@Parado", daoDadosTeares.Parado);
                    dalMySQL.AdicionaParametros("@Pontos", daoDadosTeares.Pontos);
                    dalMySQL.AdicionaParametros("@Metros", daoDadosTeares.Metros);
                    dalMySQL.AdicionaParametros("@Jardas", daoDadosTeares.Jardas);
                    dalMySQL.AdicionaParametros("@MedidaGen", daoDadosTeares.MedidaGen);
                    dalMySQL.AdicionaParametros("@QtdGen", daoDadosTeares.QtdGen);
                    dalMySQL.AdicionaParametros("@MinGen", daoDadosTeares.MinGen);
                    dalMySQL.AdicionaParametros("@QtdParadasUrdume", daoDadosTeares.QtdParadasUrdume);
                    dalMySQL.AdicionaParametros("@MinParadasUrdume", daoDadosTeares.MinParadasUrdume);
                    dalMySQL.AdicionaParametros("@QtdParadasOurelaFalsa", daoDadosTeares.QtdParadasOurelaFalsa);
                    dalMySQL.AdicionaParametros("@MinParadasOurelaFalsa", daoDadosTeares.MinParadasOurelaFalsa);
                    dalMySQL.AdicionaParametros("@QtdParadasLenoDireita", daoDadosTeares.QtdParadasLenoDireita);
                    dalMySQL.AdicionaParametros("@MinParadasLenoDireita", daoDadosTeares.MinParadasLenoDireita);
                    dalMySQL.AdicionaParametros("@QtdParadasLenoEsquerda", daoDadosTeares.QtdParadasLenoEsquerda);
                    dalMySQL.AdicionaParametros("@MinParadasLenoEsquerda", daoDadosTeares.MinParadasLenoEsquerda);
                    dalMySQL.AdicionaParametros("@QtdParadasTrama", daoDadosTeares.QtdParadasTrama);
                    dalMySQL.AdicionaParametros("@MinParadasTrama", daoDadosTeares.MinParadasTrama);
                    dalMySQL.AdicionaParametros("@QtdTrocaDeRolo", daoDadosTeares.QtdTrocaDeRolo);
                    dalMySQL.AdicionaParametros("@MinTrocaDeRolo", daoDadosTeares.MinTrocaDeRolo);
                    dalMySQL.AdicionaParametros("@QtdCorteTecido", daoDadosTeares.QtdCorteTecido);
                    dalMySQL.AdicionaParametros("@MinCorteTecido", daoDadosTeares.MinCorteTecido);
                    dalMySQL.AdicionaParametros("@QtdParadaManual", daoDadosTeares.QtdParadaManual);
                    dalMySQL.AdicionaParametros("@MinParadaManual", daoDadosTeares.MinParadaManual);
                    dalMySQL.AdicionaParametros("@QtdEnergiaDesligada", daoDadosTeares.QtdEnergiaDesligada);
                    dalMySQL.AdicionaParametros("@MinEnergiaDesligada", daoDadosTeares.MinEnergiaDesligada);
                    dalMySQL.AdicionaParametros("@QtdParadasOutras", daoDadosTeares.QtdParadasOutras);
                    dalMySQL.AdicionaParametros("@MinParadasOutras", daoDadosTeares.MinParadasOutras);
                    dalMySQL.AdicionaParametros("@Wf11", daoDadosTeares.Wf11);
                    dalMySQL.AdicionaParametros("@Wf12", daoDadosTeares.Wf12);
                    dalMySQL.AdicionaParametros("@Wf21", daoDadosTeares.Wf21);
                    dalMySQL.AdicionaParametros("@Wf22", daoDadosTeares.Wf22);
                    dalMySQL.AdicionaParametros("@QtdGen1", daoDadosTeares.QtdGen1);
                    dalMySQL.AdicionaParametros("@MinGen1", daoDadosTeares.MinGen1);
                    dalMySQL.AdicionaParametros("@QtdGen2", daoDadosTeares.QtdGen2);
                    dalMySQL.AdicionaParametros("@MinGen2", daoDadosTeares.MinGen2);
                    dalMySQL.AdicionaParametros("@QtdGen3", daoDadosTeares.QtdGen3);
                    dalMySQL.AdicionaParametros("@MinGen3", daoDadosTeares.MinGen3);
                    dalMySQL.AdicionaParametros("@QtdGen4", daoDadosTeares.QtdGen4);
                    dalMySQL.AdicionaParametros("@MinGen4", daoDadosTeares.MinGen4);
                    dalMySQL.AdicionaParametros("@QtdGen5", daoDadosTeares.QtdGen5);
                    dalMySQL.AdicionaParametros("@MinGen5", daoDadosTeares.MinGen5);
                    dalMySQL.AdicionaParametros("@QtdGen6", daoDadosTeares.QtdGen6);
                    dalMySQL.AdicionaParametros("@MinGen6", daoDadosTeares.MinGen6);
                    dalMySQL.AdicionaParametros("@QtdGen7", daoDadosTeares.QtdGen7);
                    dalMySQL.AdicionaParametros("@MinGen7", daoDadosTeares.MinGen7);
                    dalMySQL.AdicionaParametros("@QtdGen8", daoDadosTeares.QtdGen8);
                    dalMySQL.AdicionaParametros("@MinGen8", daoDadosTeares.MinGen8);
                    dalMySQL.AdicionaParametros("@QtdGen9", daoDadosTeares.QtdGen9);
                    dalMySQL.AdicionaParametros("@MinGen9", daoDadosTeares.MinGen9);
                    dalMySQL.AdicionaParametros("@QtdGen10", daoDadosTeares.QtdGen10);
                    dalMySQL.AdicionaParametros("@MinGen10", daoDadosTeares.MinGen10);
                    dalMySQL.AdicionaParametros("@QtdGen11", daoDadosTeares.QtdGen11);
                    dalMySQL.AdicionaParametros("@MinGen11", daoDadosTeares.MinGen11);
                    dalMySQL.AdicionaParametros("@QtdGen12", daoDadosTeares.QtdGen12);
                    dalMySQL.AdicionaParametros("@MinGen12", daoDadosTeares.MinGen12);
                    dalMySQL.AdicionaParametros("@QtdGen13", daoDadosTeares.QtdGen13);
                    dalMySQL.AdicionaParametros("@MinGen13", daoDadosTeares.MinGen13);
                    dalMySQL.AdicionaParametros("@QtdGen14", daoDadosTeares.QtdGen14);
                    dalMySQL.AdicionaParametros("@MinGen14", daoDadosTeares.MinGen14);
                    dalMySQL.AdicionaParametros("@QtdGen15", daoDadosTeares.QtdGen15);
                    dalMySQL.AdicionaParametros("@MinGen15", daoDadosTeares.MinGen15);
                    dalMySQL.AdicionaParametros("@QtdGen16", daoDadosTeares.QtdGen16);
                    dalMySQL.AdicionaParametros("@MinGen16", daoDadosTeares.MinGen16);

                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspDadosTearesInserir");
                }
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi Possivel inserir dados dos teares. Detalhes: " + ex.Message);
            }
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
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: DadosTeares renomeada deletado. Detalhes: bllShiftReport.DeletarArquivos() linha 261 | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível deletar DadosTeares renomeada. Detalhes: bllShiftReport.DeletarArquivos() linha 265 | " + retorno + " | " + data);
            }

            return retorno;
        }



        #endregion
    }
}
