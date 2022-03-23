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

        public DAOProductionList LerProductionCSV()
        {
            DAOProductionList daoProductionList = new DAOProductionList();
            StreamReader csv = new StreamReader(@"X:\csv\Production.csv", Encoding.UTF8);
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
            try
            {
                string retorno = "ok";
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
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi Possivel inserir dados de Production. Detalhes: " + ex.Message);
            }
        }

        #endregion
    }
}
