using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Controller
{
    public class HistoricoController
    {

        public List<Historico> Listar()
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
                    string query = "SELECT * FROM historico";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Cria um adapter que usará a instrução SQL para acessar a tabela de Historico
                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        //Cria dataset para preencher a informação vinda do banco de dados
                        DataSet ds = new DataSet();
                        //Preenche o dataset via adapter
                        da.Fill(ds, "historico");

                        //Recupera as informações do dataset e guarda em lista para retornar para a VIEW
                        List<Historico> lstRetorno = ds.Tables["historico"].AsEnumerable().Select(x => new Historico
                        {
                            Id = x.Field<int>("id"),
                            DataInclusao = x.Field<DateTime>("data_inclusao"),
                            IdPagamento = Convert.ToInt32(x.Field<Int16>("pagamento_id")),
                            IdCliente = x.Field<int>("cliente_id"),
                            IdLocacao = x.Field<int>("locacao_id")
                        }).ToList();
                        // lucas ver aqui com atencao
                        foreach (Historico historico in lstRetorno)
                        {
                            // Recuperando a data da inclusao do pagamento
                            DataInclusaoController data_inclusaoController = new DataInclusaoController();
                            historico.DataInclusao = data_inclusaoController.BuscarPorId(historico.data_inclusao);

                            // Recuperando o pagamento
                            PagamentoController pagamentoController = new PagamentoController();
                            historico.Pagamento = pagamentoController.BuscarPorId(historico.IdPagamento);

                            // Recuperando o id do cliente
                            ClienteController clienteController = new ClienteController();
                            historico.Cliente = clienteController.BuscarPorId(historico.IdCliente);

                            // Recuperando a locacao
                            LocacaoController locacaoController = new LocacaoController();
                            historico.Locacao = LocacaoController.BuscarPorId(historico.IdLocacao);
                        }

                        //Retorna a informação recuperada
                        return lstRetorno;
                    }
                }
            }
        }
        public Historico Buscar(int id)
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
                    string query = $"SELECT * FROM historico WHERE id = {id}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Historico retorno = new Historico();

                    //Verifica se existe registro retornado do banco de dados
                    while (reader.Read())
                    {
                        //Popula objeto de retorno com informações vindas do banco de dados
                        retorno.Id = (int)reader["id"];
                        retorno.DataInclusao = (DateTime)reader["data_inclusao"];
                        retorno.Pagamento = Convert.ToInt32((Int16)reader["pagamento_id"]);
                        retorno.IdCliente = (string)reader["cliente_id"];
                        retorno.IdLocacao = (int)reader["locacao_id"];
                    }
                    //Duvida nas chaves//tem que colocar mais opcao de busca ou a parte de baixo ja e pra selecionar as opcoes?
                    // Recuperando a data da inclusao do pagamento
                    DataInclusaoController data_inclusaoController = new DataInclusaoController();
                    historico.DataInclusao = data_inclusaoController.BuscarPorId(historico.data_inclusao);

                    // Recuperando o pagamento
                    PagamentoController pagamentoController = new PagamentoController();
                    historico.Pagamento = pagamentoController.BuscarPorId(historico.IdPagamento);

                    // Recuperando o id do cliente
                    ClienteController clienteController = new ClienteController();
                    historico.Cliente = clienteController.BuscarPorId(historico.IdCliente);

                    // Recuperando a locacao
                    LocacaoController locacaoController = new LocacaoController();
                    historico.Locacao = LocacaoController.BuscarPorId(historico.IdLocacao);

                    return retorno;
                }
            }
        }

        public void Inserir(Historico registro)
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
                    string query = $"INSERT INTO historico(data_inclusao, pagamento_id, cliente_id, locacao_id) VALUES ('{registro.DataInclusao}', {registro.IdPagamento}, '{registro.IdCliente}', {registro.IdLocacao})";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Historico registro)
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
                    string query = $@"UPDATE historico SET
                                    data_inclusao = '{registro.DataInclusao}',
                                    pagamento_id = {registro.IdPagamento},
                                    cliente_id = '{registro.IdCliente}',
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


    }
}