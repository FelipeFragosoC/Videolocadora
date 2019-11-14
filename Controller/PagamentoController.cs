using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Controller
{
    public class PagamentoController
    {

        public List<Pagamento> Listar()
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
                    string query = "SELECT * FROM pagamento";

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
                        da.Fill(ds, "pagamento");

                        //Recupera as informações do dataset e guarda em lista para retornar para a VIEW
                        List<Pagamento> lstRetorno = ds.Tables["pagamento"].AsEnumerable().Select(x => new Pagamento
                        {
                            Id = x.Field<int>("id"),
                            Valor = x.Field<Decimal>("valor"),
                            IdLocacao = x.Field<int>("locacao_id"),
                        }).ToList();

                        foreach (Pagamento pagamento in lstRetorno)
                        {
                            // Recuperando o valor 
                            PagamentoController pagamentoController = new PagamentoController();
                            pagamento.valor = valorcontroller.BuscarPorId(pagamento.Valor);

                            // Recuperando o id da locacao
                            IdLocacaoController locacao_idController = new IdLocacaoController();
                            pagamento.locacao_idController = locacao_id.BuscarPorId(pagamento.locacao_id);
                        }

                        //Retorna a informação recuperada
                        return lstRetorno;
                    }
                }
            }
        }
        public Pagamento Buscar(int id)
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
                    string query = $"SELECT * FROM pagamento WHERE id = {id}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Pagamento retorno = new Pagamento();

                    //Verifica se existe registro retornado do banco de dados
                    while (reader.Read())
                    {
                        //Popula objeto de retorno com informações vindas do banco de dados
                        retorno.Id = (int)reader["id"];
                        retorno.Valor = (Decimal)reader["valor"];
                        retorno.IdLocacao = (int)reader["locacao_id"];
                    }

                    // Recuperando o valor de cada locacao
                    ValorController valorController = new ValorController();
                    retorno.Valor = valorController.BuscarPorId(retorno.Valor);

                    // Recuperando a classificacao indicativa de cada filme da lista
                    IdLocacaoController locacao_idController = new IdLocacaoController();
                    retorno.locacao_idController = locacao_idController.BuscarPorId(retorno.IdLocacao);

                    return retorno;
                }
            }
        }

        public void Inserir(Pagamento registro)
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
                    string query = $"INSERT INTO pagamento(valro, locacao_id) VALUES ('{registro.Valor}', {registro.IdLocacao})";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Pagamento registro)
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
                    string query = $@"UPDATE pagamento SET
                                    valor = '{registro.Valor}',
                                    locacao_id = {registro.IdLocacao},
                                    WHERE id = {registro.Id}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }