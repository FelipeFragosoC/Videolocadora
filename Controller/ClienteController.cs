using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace Controller
{
    public class ClienteController
    {

        public List<Cliente> Listar()
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
                    string query = "SELECT * FROM cliente";

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
                        da.Fill(ds, "cliente");

                        //Recupera as informações do dataset e guarda em lista para retornar para a VIEW
                        List<Cliente> lstRetorno = ds.Tables["cliente"].AsEnumerable().Select(x => new Cliente
                        {
                            Id = x.Field<int>("id"),
                            Nome = x.Field<string>("nome"),
                            Cpf = x.Field<string>("cpf"),
                            Email = x.Field<string>("sinopse"),
                            IdTelefone = x.Field<int>("telefone_id"),
                            IdEndereco = x.Field<int>("endereco_id")
                        }).ToList();

                        foreach (Cliente cliente in lstRetorno)
                        {
                            // Recuperando o genero cinematografico de cada filme da lista
                            TelefoneController telefoneController = new TelefoneController();
                            cliente.Telefone = telefoneController.BuscarPorId(cliente.IdTelefone);

                            // Recuperando a classificacao indicativa de cada filme da lista
                            EnderecoController enderecoController = new EnderecoController();
                            cliente.Endereco = enderecoController.BuscarPorId(cliente.IdEndereco);
                        }

                        //Retorna a informação recuperada
                        return lstRetorno;
                    }
                }
            }
        }
        public Cliente Buscar(int id)
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
                    string query = $"SELECT * FROM cliente WHERE id = {id}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Cliente retorno = new Cliente();

                    //Verifica se existe registro retornado do banco de dados
                    while (reader.Read())
                    {
                        //Popula objeto de retorno com informações vindas do banco de dados
                        retorno.Id = (int)reader["id"];
                        retorno.Nome = (string)reader["nome"];
                        retorno.Cpf = (string)reader["cpf"];
                        retorno.Email = (string)reader["email"];
                        retorno.IdTelefone = (int)reader["telefone_id"];
                        retorno.IdEndereco = (int)reader["endereco_id"];
                    }

                    // Recuperando o genero cinematografico de cada filme da lista
                    TelefoneController telefoneController = new TelefoneController();
                    retorno.Telefone = telefoneController.BuscarPorId(retorno.IdTelefone);

                    // Recuperando a classificacao indicativa de cada filme da lista
                    EnderecoController enderecoController = new EnderecoController();
                    retorno.Endereco = enderecoController.BuscarPorId(retorno.IdEndereco);

                    return retorno;
                }
            }
        }

        public void Inserir(Cliente registro)
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
                    string query = $"INSERT INTO cliente(nome, cpf, email, telefone_id, endereco_id) VALUES ('{registro.Nome}', '{registro.Cpf}', '{registro.Email}', {registro.IdTelefone}, {registro.IdEndereco})";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Cliente registro)
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
                    string query = $@"UPDATE cliente SET
                                    nome = '{registro.Nome}',
                                    cpf = {registro.Cpf},
                                    email = '{registro.Email}',
                                    telefone_id = {registro.IdTelefone},
                                    endereco_id = {registro.IdEndereco}
                                    WHERE id = {registro.Id}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
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
                    //Instancia a controller de telefone
                    TelefoneController telefoneController = new TelefoneController();

                    //Exclui o registro de telefone do cliente
                    telefoneController.Excluir(id);

                    //Instancia a controller de endereco
                    EnderecoController enderecoController = new EnderecoController();

                    //Exclui o registro de endereco do cliente
                    enderecoController.Excluir(id);

                    //Monta a consulta no banco de dados
                    string query = $"DELETE FROM cliente WHERE id = {id}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Cliente BuscarPeloTelefone(int idTelefone)
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
                    string query = $"SELECT * FROM cliente WHERE telefone_id = {idTelefone}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Cliente retorno = new Cliente();

                    //Verifica se existe registro retornado do banco de dados
                    while (reader.Read())
                    {
                        //Popula objeto de retorno com informações vindas do banco de dados
                        retorno.Id = (int)reader["id"];
                        retorno.Nome = (string)reader["nome"];
                        retorno.Cpf = (string)reader["cpf"];
                        retorno.Email = (string)reader["email"];
                        retorno.IdEndereco = (int)reader["endereco_id"];
                    }

                    // Recuperando o endereco do cliente
                    EnderecoController enderecoController = new EnderecoController();
                    retorno.Endereco = enderecoController.BuscarPorId(retorno.IdEndereco);

                    return retorno;
                }
            }
        }

        public Cliente BuscarPeloEndereco(int idEndereco)
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
                    string query = $"SELECT * FROM cliente WHERE endereco_id = {idEndereco}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Cliente retorno = new Cliente();

                    //Verifica se existe registro retornado do banco de dados
                    while (reader.Read())
                    {
                        //Popula objeto de retorno com informações vindas do banco de dados
                        retorno.Id = (int)reader["id"];
                        retorno.Nome = (string)reader["nome"];
                        retorno.Cpf = (string)reader["cpf"];
                        retorno.Email = (string)reader["email"];
                        retorno.IdTelefone = (int)reader["telefone_id"];
                    }

                    // Recuperando o telefone do cliente
                    TelefoneController telefoneController = new TelefoneController();
                    retorno.Telefone = telefoneController.BuscarPorId(retorno.IdTelefone);

                    return retorno;
                }
            }
        }
    }
}