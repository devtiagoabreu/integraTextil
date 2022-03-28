using System.Text;
using System.ComponentModel;
using System.Data;
using DAL;
using DAO;

namespace BLL
{
    public class BLLProduction
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
                    bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: production encontrado, nome: " + arquivoNome + ".  Detalhes: bllProduction.PegarNomeArquivo() linha 70 | " + data);
                }
                
            }
            catch (Exception ex)
            {
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: production não encontrado, nome: nulo. Detalhes: bllProduction.PegarNomeArquivo() linha 76 | " + ex.Message.ToString() + " | " + data);
            }

            return arquivoNome;

        }

        public string RenomearArquivo(string arquivoNome, string arquivoNomeFinal,string pastaOrigem, string pastaDestino)
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
                    bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: production movido e renomeado para pasta destino. Detalhes: bllProduction.RenomearArquivo() linha 114 | " + retorno +  " | " + data);
                }
                
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível mover e renomear production. Detalhes: bllProduction.RenomearArquivo() linha 121 | " + retorno + " | " + data);
            }

            return retorno;


        }
        
        public DAOProductionList LerCSV(string path)
        {
            DAOProductionList daoProductionList = new DAOProductionList();
            StreamReader csv = new StreamReader(path, Encoding.UTF8);
            string linha;
            string[] campo;
            int index = 0;

            while ((linha = csv.ReadLine()) != null)
            {
                DAOProduction daoProduction = new DAOProduction();
                campo = linha.Split(',');
                index++;

                if (index > 7)
                {
                    daoProduction.Date = campo[0].ToString().Replace("/", "-").Replace("=", "").Replace("''", "");
                    daoProduction.Loom = campo[1].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoProduction.Style = campo[2].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    daoProduction.Product_Pick = campo[3].ToString().Replace("\"", "").Replace("=", "").Replace("''", "");
                    
                    daoProductionList.Add(daoProduction);

                }
            }

            return daoProductionList;

        }

        public string InserirProduction(DAOProductionList daoProductionList)
        {
            BLLFerramentas bllFerramentas = new BLLFerramentas();
            string retorno = "";

            try
            {
                DataTable dataTableProductionList = ConvertToDataTable(daoProductionList);
                foreach (DataRow linha in dataTableProductionList.Rows)
                {
                    DAOProduction daoProduction = new DAOProduction();

                    daoProduction.Date = linha["Date"].ToString();
                    daoProduction.Loom = linha["Loom"].ToString();
                    daoProduction.Style = linha["Style"].ToString();
                    daoProduction.Product_Pick = linha["Product_Pick"].ToString();
                    
                    dalMySQL.LimparParametros();

                    dalMySQL.AdicionaParametros("@Date", daoProduction.Date);
                    dalMySQL.AdicionaParametros("@Loom", daoProduction.Loom);
                    dalMySQL.AdicionaParametros("@Style", daoProduction.Style);
                    dalMySQL.AdicionaParametros("@Product_Pick", daoProduction.Product_Pick);
                   
                    dalMySQL.ExecutarManipulacao(CommandType.StoredProcedure, "uspInsertProduction");
                }

                retorno = "ok";
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: production inserido. Detalhes: bllProduction.InserirProduction() linha 190 | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível inserir production. Detalhes: bllProduction.InserirProduction() linha 193 | " + retorno + " | " + data);
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
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Sucesso: production renomeada deletado. Detalhes: bllProduction.DeletarArquivos() linha 214 | " + retorno + " | " + data);

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                bllFerramentas.GravarLog(@"C:\Apache2\htdocs\integratextil\teares\logs\logs.txt", "Erro: não foi possível deletar production renomeada. Detalhes: bllProduction.DeletarArquivos() linha 217 | " + retorno + " | " + data);
            }

            return retorno;
        }

        #endregion
    }
}
