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
    public class LocacaoController
    {

        public List<Locacao> Listar()
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
                    string query = "SELECT * FROM Locacao";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Cria um adapter que usará a instrução SQL para acessar a tabela de Locacao
                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        //Cria dataset para preencher a informação vinda do banco de dados
                        DataSet ds = new DataSet();
                        //Preenche o dataset via adapter
                        da.Fill(ds, "Locacao");

                        //Recupera as informações do dataset e guarda em lista para retornar para a VIEW
                        List<Locacao> lstRetorno = ds.Tables["Locacao"].AsEnumerable().Select(x => new Locacao
                        {
                            Id = x.Field<int>("id"),
                            DataInclusao = x.Field<DateTime>("data_inclusao"),
                            IdPagamento = Convert.ToInt32(x.Field<Int16>("pagamento_id")),
                            IdCliente = x.Field<int>("cliente_id"),
                            IdLocacao = x.Field<int>("locacao_id")
                        }).ToList();
                        // lucas ver aqui com atencao
                        foreach (Locacao Locacao in lstRetorno)
                        {
                            // Recuperando a data da inclusao do pagamento
                            DataInclusaoController data_inclusaoController = new DataInclusaoController();
                            Locacao.DataInclusao = data_inclusaoController.BuscarPorId(Locacao.data_inclusao);

                            // Recuperando o pagamento
                            PagamentoController pagamentoController = new PagamentoController();
                            Locacao.Pagamento = pagamentoController.BuscarPorId(Locacao.IdPagamento);

                            // Recuperando o id do cliente
                            ClienteController clienteController = new ClienteController();
                            Locacao.Cliente = clienteController.BuscarPorId(Locacao.IdCliente);

                            // Recuperando a locacao
                            LocacaoController locacaoController = new LocacaoController();
                            Locacao.Locacao = LocacaoController.BuscarPorId(Locacao.IdLocacao);
                        }

                        //Retorna a informação recuperada
                        return lstRetorno;
                    }
                }
            }
        }
        public Locacao Buscar(int id)
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
                    string query = $"SELECT * FROM Locacao WHERE id = {id}";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Monta objeto de retorno
                    Locacao retorno = new Locacao();

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
                    Locacao.DataInclusao = data_inclusaoController.BuscarPorId(Locacao.data_inclusao);

                    // Recuperando o pagamento
                    PagamentoController pagamentoController = new PagamentoController();
                    Locacao.Pagamento = pagamentoController.BuscarPorId(Locacao.IdPagamento);

                    // Recuperando o id do cliente
                    ClienteController clienteController = new ClienteController();
                    Locacao.Cliente = clienteController.BuscarPorId(Locacao.IdCliente);

                    // Recuperando a locacao
                    LocacaoController locacaoController = new LocacaoController();
                    Locacao.Locacao = LocacaoController.BuscarPorId(Locacao.IdLocacao);

                    return retorno;
                }
            }
        }

        public void Inserir(Locacao registro)
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
                    string query = $"INSERT INTO Locacao(data_inclusao, pagamento_id, cliente_id, locacao_id) VALUES ('{registro.DataInclusao}', {registro.IdPagamento}, '{registro.IdCliente}', {registro.IdLocacao})";

                    //Passa informação de conexão e consulta para o comando
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    //Executa a instrução SQL
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Locacao registro)
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
                    string query = $@"UPDATE Locacao SET
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