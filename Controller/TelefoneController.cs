using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Controller
{
    public class TelefoneController
    {

        public List<Telefone> Listar()
        {
            //Define string de conexão
            string strConexao = "SERVER=localhost; DataBase=videolocadora; UID=root; pwd=root";

            //Cria conexão com banco de dados
            using (MySqlConnection conn = new MySqlConnection(strConexao))
            {
                //Abre a conexão com o banco de dados
                conn.Open();

                //Inicia comando para o banco de dados
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    //Monta a consulta no banco de dados
                    string query = "SELECT * FROM telefone";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Cria um adapter que usará a instrução SQL para acessar a tabela de Filme
                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        //Cria dataset para preencher a informação vinda do banco de dados
                        DataSet ds = new DataSet();
                        //Preenche o dataset via adapter
                        da.Fill(ds, "telefone");

                        //Recupera as informações do dataset e guarda em lista para retornar para a VIEW
                        List<Telefone> lstRetorno = ds.Tables["telefone"].AsEnumerable().Select(x => new Telefone
                        {
                            Id = x.Field<int>("id"),
                            Residencial = x.Field<string>("residencial"),
                            Celular = x.Field<string>("celular"),
                            Comercial = x.Field<string>("comercial"),
                            Recado = x.Field<string>("recado")
                        }).ToList();

                        foreach (Telefone telefone in lstRetorno)
                        {
                            // Recuperando o cliente de cada telefone da lista
                            ClienteController clienteController = new ClienteController();
                            telefone.Cliente = clienteController.BuscarPeloTelefone(telefone.Id);
                        }

                        //Retorna a informação recuperada
                        return lstRetorno;
                    }
                }
            }
        }

        public Telefone BuscarPorId(int idCliente)
        {
            //Define string de conexão
            string strConexao = "SERVER=localhost; DataBase=videolocadora; UID=root; pwd=root";

            //Cria conexão com banco de dados
            using (MySqlConnection conn = new MySqlConnection(strConexao))
            {
                //Abre a conexão com o banco de dados
                conn.Open();

                //Inicia comando para o banco de dados
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    //Monta a consulta no banco de dados
                    string query = $"SELECT * FROM telefone WHERE id = (SELECT telefone_id FROM cliente WHERE id = {idCliente})";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Telefone retorno = new Telefone();

                    //Verifica se existe registro retornado do banco de dados
                    while (reader.Read())
                    {
                        //Popula objeto de retorno com informações vindas do banco de dados
                        retorno.Id = (int)reader["id"];
                        retorno.Residencial = (string)reader["residencial"];
                        retorno.Celular = (string)reader["celular"];
                        retorno.Comercial = (string)reader["comercial"];
                        retorno.Recado = (string)reader["recado"];
                    }

                    return retorno;
                }
            }
        }

        public int Inserir(Telefone registro)
        {
            //Define string de conexão
            string strConexao = "SERVER=localhost; DataBase=videolocadora; UID=root; pwd=root";

            //Cria conexão com banco de dados
            using (MySqlConnection conn = new MySqlConnection(strConexao))
            {
                //Abre a conexão com o banco de dados
                conn.Open();

                //Inicia comando para o banco de dados
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    //Monta a consulta no banco de dados
                    string query = $"INSERT INTO telefone(residencial, celular, comercial, recado) VALUES ('{registro.Residencial}', '{registro.Celular}', '{registro.Comercial}', '{registro.Recado}')";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();

                    return (int)cmd.LastInsertedId;
                }
            }
        }

        public void Atualizar(Telefone telefone, int idCliente)
        {
            //Define string de conexão
            string strConexao = "SERVER=localhost; DataBase=videolocadora; UID=root; pwd=root";

            //Cria conexão com banco de dados
            using (MySqlConnection conn = new MySqlConnection(strConexao))
            {
                //Abre a conexão com o banco de dados
                conn.Open();

                //Inicia comando para o banco de dados
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    //Monta a consulta no banco de dados
                    string query = $@"UPDATE telefone SET
                                    residencial = '{telefone.Residencial}',
                                    celular = '{telefone.Celular}',
                                    comercial = '{telefone.Comercial}',
                                    recado = '{telefone.Recado}'
                                    WHERE id = (SELECT telefone_id FROM cliente WHERE id = {idCliente})";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int idCliente)
        {
            //Define string de conexão
            string strConexao = "SERVER=localhost; DataBase=videolocadora; UID=root; pwd=root";

            //Cria conexão com banco de dados
            using (MySqlConnection conn = new MySqlConnection(strConexao))
            {
                //Abre a conexão com o banco de dados
                conn.Open();

                //Inicia comando para o banco de dados
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    //Monta a consulta no banco de dados
                    string query = $"DELETE FROM telefone WHERE id = (SELECT telefone_id FROM cliente WHERE id = {idCliente})";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}