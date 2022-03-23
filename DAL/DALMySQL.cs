using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DALMySQL
    {
        //Criar Conexao Banco de Dados
        private MySqlConnection CriarConexao()
        {
            String connString = null;
            //connString = "Persist Security  Info = False; server = 192.168.0.234; database = dbintegrafabric; uid = abreu; server = 192.168.0.234; database = dbintegrafabric; uid = abreu; pwd = dqgh3ffrdg";
            //connString = "server=192.168.0.234;initial catalog=dbintegrafabric;uid=abreu;pwd=dqgh3ffrdg";
            StreamReader sr = File.OpenText("stringConexaoMySql.txt");
            string input = null;
            while ((input = sr.ReadLine()) != null)
            {
                connString = input;
            }
            return new MySqlConnection(connString);
            //return new SqlConnection(Settings.Default.stringConexao);
        }

        //Parametros que vao para o banco
        private MySqlParameterCollection mySqlParameterCollection = new MySqlCommand().Parameters;

        public void LimparParametros()
        {
            mySqlParameterCollection.Clear();
        }

        public void AdicionaParametros(string nomeParametro, object valorParametro)
        {
            mySqlParameterCollection.Add(new MySqlParameter(nomeParametro, valorParametro));
        }

        //Persistencia - inserir/alterar/excluir
        public object ExecutarManipulacao(CommandType commandType, string nomeProcedore)
        {   //Criar conexao
            MySqlConnection mySqlConnection = CriarConexao();
            try
            {
                //Criar conexao
                //MySqlConnection mySqlConnection = CriarConexao();
                //Abrir Conexao
                mySqlConnection.Open();
                //Criar comando que leva a informaçao para o banco
                MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                //Alimentando o comando
                //sqlCommand.CommandType = CommandType.StoredProcedure;
                //sqlCommand.CommandType = CommandType.Text;
                mySqlCommand.CommandType = commandType;
                mySqlCommand.CommandText = nomeProcedore;
                mySqlCommand.CommandTimeout = 20200; //20 segundos
                //Adiciona os parametros no comando
                foreach (MySqlParameter mySqlParameter in mySqlParameterCollection)
                {
                    mySqlCommand.Parameters.Add(new MySqlParameter(mySqlParameter.ParameterName, mySqlParameter.Value));
                }

                //Executar Comando
                return mySqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //fecha Conexao
                mySqlConnection.Close();

            }
        }

        //Consultar registros do banco dados
        public DataTable ExecutarConsulta(CommandType commandType, string nomeProcedore)
        {
            try
            {
                //Criar conexao
                MySqlConnection mySqlConnection = CriarConexao();
                //Abrir Conexao
                mySqlConnection.Open();
                //Criar comando que leva a informaçao para o banco
                MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                //Alimentando o comando
                //sqlCommand.CommandType = CommandType.StoredProcedure;
                //sqlCommand.CommandType = CommandType.Text;
                mySqlCommand.CommandType = commandType;
                mySqlCommand.CommandText = nomeProcedore;
                mySqlCommand.CommandTimeout = 20200; //20 segundos
                //Adiciona os parametros no comando
                foreach (MySqlParameter mySqlParameter in mySqlParameterCollection)
                {
                    mySqlCommand.Parameters.Add(new MySqlParameter(mySqlParameter.ParameterName, mySqlParameter.Value));
                }
                //Criar Adaptador
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                //Tabela de dados Vazia
                DataTable dataTable = new DataTable();
                //comando vai ao banco e busca os dados
                mySqlDataAdapter.Fill(dataTable);
                //fecha conexao
                //mySqlConnection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}