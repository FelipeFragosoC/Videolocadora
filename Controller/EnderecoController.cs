using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Controller
{
    public class EnderecoController
    {
        public List<Endereco> Listar()
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
                    string query = "SELECT * FROM endereco";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Cria um adapter que usará a instrução SQL para acessar a tabela de endereço
                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        //Cria dataset para preencher a informação vinda do banco de dados
                        DataSet ds = new DataSet();
                        //Preenche o dataset via adapter
                        da.Fill(ds, "endereco");

                        //Recupera as informações do dataset e guarda em lista para retornar para a VIEW
                        List<Endereco> lstRetorno = ds.Tables["endereco"].AsEnumerable().Select(x => new Endereco
                        {
                            Id = x.Field<int>("id"),
                            Logradouro = x.Field<string>("logradouro"),
                            Bairro = x.Field<string>("bairro"),
                            Cidade = x.Field<string>("cidade"),
                            Uf = x.Field<string>("uf"),
                            Cep = x.Field<string>("cep")
                        }).ToList();

                        foreach (Endereco endereco in lstRetorno)
                        {
                            // Recuperando o cliente de cada telefone da lista
                            ClienteController clienteController = new ClienteController();
                            endereco.Cliente = clienteController.BuscarPeloEndereco(endereco.Id);
                        }

                        //Retorna a informação recuperada
                        return lstRetorno;
                    }
                }
            }
        }

        public Endereco BuscarPorId(int idCliente)
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
                    string query = $"SELECT * FROM endereco WHERE id = {idCliente}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Endereco retorno = new Endereco();

                    //Verifica se existe registro retornado do banco de dados
                    while (reader.Read())
                    {
                        //Popula objeto de retorno com informações vindas do banco de dados
                        retorno.Id = (int)reader["id"];
                        retorno.Logradouro = (string)reader["logradouro"];
                        retorno.Bairro = (string)reader["bairro"];
                        retorno.Cidade = (string)reader["cidade"];
                        retorno.Uf = (string)reader["uf"];
                        retorno.Cep = (string)reader["cep"];
                    }

                    return retorno;
                }
            }
        }

        public int Inserir(Endereco registro)
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
                    string query = $"INSERT INTO endereco(logradouro, bairro, cidade, uf, cep) VALUES ('{registro.Logradouro}', '{registro.Bairro}', '{registro.Cidade}', '{registro.Uf}', '{registro.Cep}')";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();

                    return (int)cmd.LastInsertedId;
                }
            }
        }

        public void Atualizar(Endereco endereco, int idCliente)
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
                    string query = $@"UPDATE endereco SET
                                    logradouro = '{endereco.Logradouro}',
                                    bairro = '{endereco.Bairro}',
                                    cidade = '{endereco.Cidade}',
                                    uf = '{endereco.Uf}',
                                    cep = '{endereco.Cep}'
                                    WHERE id = (SELECT endereco_id FROM cliente WHERE id = {idCliente})";

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
                    string query = $"DELETE FROM endereco WHERE id = (SELECT endereco_id FROM cliente WHERE id = {idCliente})";

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