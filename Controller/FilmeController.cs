using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Controller
{
    public class FilmeController
    {

        public List<Filme> Listar()
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
                    string query = "SELECT * FROM filme";

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
                        da.Fill(ds, "filme");

                        //Recupera as informações do dataset e guarda em lista para retornar para a VIEW
                        List<Filme> lstRetorno = ds.Tables["filme"].AsEnumerable().Select(x => new Filme
                        {
                            Id = x.Field<int>("id"),
                            Titulo = x.Field<string>("titulo"),
                            Lancamento = Convert.ToInt32(x.Field<Int16>("lancamento")),
                            Sinopse = x.Field<string>("sinopse"),
                            IdGeneroCinematografico = x.Field<int>("genero_cinematografico_id"),
                            IdClassificacaoIndicativa = x.Field<int>("classificacao_indicativa_id")
                        }).ToList();

                        foreach(Filme filme in lstRetorno)
                        {
                            // Recuperando o genero cinematografico de cada filme da lista
                            GeneroCinematograficoController generoController = new GeneroCinematograficoController();
                            filme.GeneroCinematografico = generoController.BuscarPorId(filme.IdGeneroCinematografico);

                            // Recuperando a classificacao indicativa de cada filme da lista
                            ClassificacaoIndicativaController classificacaoController = new ClassificacaoIndicativaController();
                            filme.ClassificacaoIndicativa = classificacaoController.BuscarPorId(filme.IdClassificacaoIndicativa);
                        }

                        //Retorna a informação recuperada
                        return lstRetorno;
                    }
                }
            }
        }
        public Filme Buscar(int id)
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
                    string query = $"SELECT * FROM filme WHERE id = {id}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Filme retorno = new Filme();

                    //Verifica se existe registro retornado do banco de dados
                    while (reader.Read())
                    {
                        //Popula objeto de retorno com informações vindas do banco de dados
                        retorno.Id = (int)reader["id"];
                        retorno.Titulo = (string)reader["titulo"];
                        retorno.Lancamento = Convert.ToInt32((Int16)reader["lancamento"]);
                        retorno.Sinopse = (string)reader["sinopse"];
                        retorno.IdGeneroCinematografico = (int)reader["genero_cinematografico_id"];
                        retorno.IdClassificacaoIndicativa = (int)reader["classificacao_indicativa_id"];
                    }

                    // Recuperando o genero cinematografico de cada filme da lista
                    GeneroCinematograficoController generoController = new GeneroCinematograficoController();
                    retorno.GeneroCinematografico = generoController.BuscarPorId(retorno.IdGeneroCinematografico);

                    // Recuperando a classificacao indicativa de cada filme da lista
                    ClassificacaoIndicativaController classificacaoController = new ClassificacaoIndicativaController();
                    retorno.ClassificacaoIndicativa = classificacaoController.BuscarPorId(retorno.IdClassificacaoIndicativa);

                    return retorno;
                }
            }
        }

        public void Inserir(Filme registro)
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
                    string query = $"INSERT INTO filme(titulo, lancamento, sinopse, genero_cinematografico_id, classificacao_indicativa_id) VALUES ('{registro.Titulo}', {registro.Lancamento}, '{registro.Sinopse}', {registro.IdGeneroCinematografico}, {registro.IdClassificacaoIndicativa})";
               
                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Filme registro)
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
                    string query = $@"UPDATE filme SET
                                    titulo = '{registro.Titulo}',
                                    lancamento = {registro.Lancamento},
                                    sinopse = '{registro.Sinopse}',
                                    genero_cinematografico_id = {registro.IdGeneroCinematografico},
                                    classificacao_indicativa_id = {registro.IdClassificacaoIndicativa}
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
                    //Monta a consulta no banco de dados
                    string query = $"DELETE FROM filme WHERE id = {id}";

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