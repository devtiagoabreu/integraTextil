using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLShiftReport
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

        public DAOShiftReportList LerCSV(string caminhoArquivo)
        {
            DAOShiftReportList daoShiftReportList = new DAOShiftReportList();
            StreamReader csv = new StreamReader(caminhoArquivo, Encoding.UTF8);
            string linha;
            string[] campo;
            int index = 0;
            string lixo = "";

            while ((linha = csv.ReadLine()) != null)
            {
                
                DAOShiftReport daoShiftReport = new DAOShiftReport();
                campo = linha.Split(',');
                index++;

                if (index > 7)
                {
                    if (campo[0].ToString().Equals(""))
                        daoShiftReport.Sortkey = "1";
                    else
                        daoShiftReport.Sortkey = campo[0].ToString().Replace("/", "").Replace("=", "").Replace("''", "");
                    
                    daoShiftReport.Date = campo[1].ToString().Replace("/", "-").Replace("=", "").Replace("''", "");
                    daoShiftReport.Loom = campo[2].ToString().Replace("\"" , "").Replace("=", "").Replace("''", "");
                    daoShiftReport.Style = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoShiftReport.RPM = campo[4].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoShiftReport.EfficPercent = campo[5].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoShiftReport.RunMinute = campo[6].ToString();
                    daoShiftReport.StopMinute = campo[7].ToString();
                    daoShiftReport.ProductPick = campo[8].ToString();// valor * 1000 batidas 
                    lixo = campo[9].ToString();
                    daoShiftReport.WarpCount = campo[10].ToString();
                    daoShiftReport.WarpMinute = campo[11].ToString();
                    daoShiftReport.WarpRatePH = campo[12].ToString();
                    daoShiftReport.WarpRatePDAY = campo[13].ToString();                    
                    daoShiftReport.WarpRatePP = campo[14].ToString();
                    daoShiftReport.Weft_Count = campo[15].ToString();
                    daoShiftReport.WeftMinute = campo[16].ToString();
                    daoShiftReport.WeftRatePH = campo[17].ToString();
                    daoShiftReport.WeftRatePDAY = campo[18].ToString();
                    daoShiftReport.WeftRatePP = campo[19].ToString();
                    daoShiftReport.UnselectCount = campo[20].ToString();
                    daoShiftReport.UnselectMinute = campo[21].ToString();
                    daoShiftReport.Unselect2Count = campo[22].ToString();
                    daoShiftReport.UnselectRatePH = campo[23].ToString();
                    daoShiftReport.UnselectRatePDAY = campo[24].ToString();
                    daoShiftReport.UnselectRatePP = campo[25].ToString();
                    daoShiftReport.TotalCount = campo[26].ToString();
                    daoShiftReport.TotalMinute = campo[27].ToString();
                    daoShiftReport.Total2Count = campo[28].ToString();
                    daoShiftReport.TotalRatePH = campo[29].ToString();
                    daoShiftReport.TotalRatePDAY = campo[30].ToString();
                    daoShiftReport.TotalRatePP = campo[31].ToString();

                    if (daoShiftReport.RunMinute != "0.0" || daoShiftReport.RunMinute != "")
                    {
                        decimal runMinute = Convert.ToDecimal(daoShiftReport.RunMinute.Replace(".", ","));
                        decimal stopMinute = Convert.ToDecimal(daoShiftReport.StopMinute.Replace(".", ","));
                        decimal ProductPick = Convert.ToDecimal(daoShiftReport.ProductPick.Replace(".", ","));
                        decimal rpm = ProductPick * 1000 / (runMinute);
                        daoShiftReport.RPM = Math.Round(rpm).ToString().Replace(",", ".");
                    }
                    else
                    {
                        daoShiftReport.RPM = "0.0";
                    }

                    if (daoShiftReport.RunMinute != "0.0" || daoShiftReport.RunMinute != "")
                    {
                        decimal runMinute = Convert.ToDecimal(daoShiftReport.RunMinute.Replace(".", ","));
                        decimal stopMinute = Convert.ToDecimal(daoShiftReport.StopMinute.Replace(".", ","));
                        decimal efficPercent = runMinute * 100 / (runMinute + stopMinute);
                        daoShiftReport.EfficPercent = Math.Round(efficPercent).ToString().Replace(",", ".");  
                    }
                    else
                    {
                        daoShiftReport.EfficPercent = "0.0";
                    }

                    daoShiftReportList.Add(daoShiftReport);
                }
            }

            return daoShiftReportList;

        }

        public string InserirShiftReport(DAOShiftReportList daoShiftReportList)
        {
            try
            {
                string retorno = "ok";
                DataTable dataTableShiftReportList = ConvertToDataTable(daoShiftReportList);
                foreach (DataRow linha in dataTableShiftReportList.Rows)
                {
                    DAOShiftReport daoShiftReport = new DAOShiftReport();

                    daoShiftReport.Sortkey = linha["Sortkey"].ToString();
                    daoShiftReport.Date = linha["Date"].ToString();
                    daoShiftReport.Loom = linha["Loom"].ToString();
                    daoShiftReport.Style = linha["Style"].ToString();
                    daoShiftReport.RPM = linha["RPM"].ToString();
                    daoShiftReport.EfficPercent = linha["EfficPercent"].ToString();
                    daoShiftReport.RunMinute = linha["RunMinute"].ToString();
                    daoShiftReport.StopMinute = linha["StopMinute"].ToString();
                    daoShiftReport.ProductPick = linha["ProductPick"].ToString();
                    daoShiftReport.WarpCount = linha["WarpCount"].ToString();
                    daoShiftReport.WarpMinute = linha["WarpMinute"].ToString();
                    daoShiftReport.WarpRatePH = linha["WarpRatePH"].ToString();
                    daoShiftReport.WarpRatePDAY = linha["WarpRatePDAY"].ToString();
                    daoShiftReport.WarpRatePP = linha["WarpRatePP"].ToString();
                    daoShiftReport.Weft_Count = linha["Weft_Count"].ToString();
                    daoShiftReport.WeftMinute = linha["WeftMinute"].ToString();
                    daoShiftReport.WeftRatePH = linha["WeftRatePH"].ToString();
                    daoShiftReport.WeftRatePDAY = linha["WeftRatePDAY"].ToString();
                    daoShiftReport.WeftRatePP = linha["WeftRatePP"].ToString();
                    daoShiftReport.UnselectCount = linha["UnselectCount"].ToString();
                    daoShiftReport.UnselectMinute = linha["UnselectMinute"].ToString();
                    daoShiftReport.Unselect2Count = linha["Unselect2Count"].ToString();
                    daoShiftReport.UnselectRatePH = linha["UnselectRatePH"].ToString();
                    daoShiftReport.UnselectRatePDAY = linha["UnselectRatePDAY"].ToString();
                    daoShiftReport.UnselectRatePP = linha["UnselectRatePP"].ToString();
                    daoShiftReport.TotalCount = linha["TotalCount"].ToString();
                    daoShiftReport.TotalMinute = linha["TotalMinute"].ToString();
                    daoShiftReport.Total2Count = linha["Total2Count"].ToString();
                    daoShiftReport.TotalRatePH = linha["TotalRatePH"].ToString();
                    daoShiftReport.TotalRatePDAY = linha["TotalRatePDAY"].ToString();
                    daoShiftReport.TotalRatePP = linha["TotalRatePP"].ToString();

                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Sortkey", daoShiftReport.Sortkey);
                    dalMySQL.AdicionaParametros("@Date", daoShiftReport.Date);
                    dalMySQL.AdicionaParametros("@Loom", daoShiftReport.Loom);
                    dalMySQL.AdicionaParametros("@Style", daoShiftReport.Style);
                    dalMySQL.AdicionaParametros("@RPM", daoShiftReport.RPM);
                    dalMySQL.AdicionaParametros("@EfficPercent", daoShiftReport.EfficPercent);
                    dalMySQL.AdicionaParametros("@RunMinute", daoShiftReport.RunMinute);
                    dalMySQL.AdicionaParametros("@StopMinute", daoShiftReport.StopMinute);
                    dalMySQL.AdicionaParametros("@ProductPick", daoShiftReport.ProductPick);
                    dalMySQL.AdicionaParametros("@WarpCount", daoShiftReport.WarpCount);
                    dalMySQL.AdicionaParametros("@WarpMinute", daoShiftReport.WarpMinute);
                    dalMySQL.AdicionaParametros("@WarpRatePH", daoShiftReport.WarpRatePH);
                    dalMySQL.AdicionaParametros("@WarpRatePDAY", daoShiftReport.WarpRatePDAY);
                    dalMySQL.AdicionaParametros("@WarpRatePP", daoShiftReport.WarpRatePP);
                    dalMySQL.AdicionaParametros("@Weft_Count", daoShiftReport.Weft_Count);
                    dalMySQL.AdicionaParametros("@WeftMinute", daoShiftReport.WeftMinute);
                    dalMySQL.AdicionaParametros("@WeftRatePH", daoShiftReport.WeftRatePH);
                    dalMySQL.AdicionaParametros("@WeftRatePDAY", daoShiftReport.WeftRatePDAY);
                    dalMySQL.AdicionaParametros("@WeftRatePP", daoShiftReport.WeftRatePP);
                    dalMySQL.AdicionaParametros("@UnselectCount", daoShiftReport.UnselectCount);
                    dalMySQL.AdicionaParametros("@UnselectMinute", daoShiftReport.UnselectMinute);
                    dalMySQL.AdicionaParametros("@Unselect2Count", daoShiftReport.Unselect2Count);
                    dalMySQL.AdicionaParametros("@UnselectRatePH", daoShiftReport.UnselectRatePH);
                    dalMySQL.AdicionaParametros("@UnselectRatePDAY", daoShiftReport.UnselectRatePDAY);
                    dalMySQL.AdicionaParametros("@UnselectRatePP", daoShiftReport.UnselectRatePP);
                    dalMySQL.AdicionaParametros("@TotalCount", daoShiftReport.TotalCount);
                    dalMySQL.AdicionaParametros("@TotalMinute", daoShiftReport.TotalMinute);
                    dalMySQL.AdicionaParametros("@Total2Count", daoShiftReport.Total2Count);
                    dalMySQL.AdicionaParametros("@TotalRatePH", daoShiftReport.TotalRatePH);
                    dalMySQL.AdicionaParametros("@TotalRatePDAY", daoShiftReport.TotalRatePDAY);
                    dalMySQL.AdicionaParametros("@TotalRatePP", daoShiftReport.TotalRatePP);

                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspInsertShiftReport");
                }
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi Possivel inserir dados de ShiftReport. Detalhes: " + ex.Message);
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
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: shiftReport renomeada deletado. Detalhes: bllShiftReport.DeletarArquivos() linha 261 | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível deletar shiftreport renomeada. Detalhes: bllShiftReport.DeletarArquivos() linha 265 | " + retorno + " | " + data);
            }

            return retorno;
        }



        #endregion
    }
}