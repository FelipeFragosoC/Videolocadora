using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            /** GERENCIAR FILMES **/

            ListarFilmes();
            //InserirFilme(new Filme());//Parametro: Filme(model)
            //BuscarFilme(1); //Parametro: id
            //AtualizarFilme(new Filme());//Parametro: Filme(model)
            //ExcluirFilme(2); //Parametro: id
            //GerarArquivosFilmes();


            /** GERENCIAR ESTOQUE **/

            //ListarEstoque();
            //CadastrarEstoque(5, 4); //Parametros: quantidade e idFilme
            //BuscarEstoque(4); //Parametro: idFilme
            //AtualizarEstoque(10, 4); //Parametros: quantidade e idFilme
            //ExcluirEstoque(4); //Parametro: idFilme


            /** GERENCIAR CLIENTES **/

            //ListarClientes();
            //CadastrarCliente(new Endereco(), new Telefone(), new Cliente()); //Parametros: Endereco(model), Telefone(model) e Cliente(model)
            //BuscarCliente(1); //Parametro: id
            //AtualizarCliente(new Endereco(), new Telefone(), new Cliente()); //Parametros: Endereco(model), Telefone(model) e Cliente(model)
            //ExcluirCliente(1);


            /** GERENCIAR TELEFONES **/

            //ListarTelefones();
            //BuscarTelefone(1); //Parametro: idCliente
            //AtualizarTelefone(new Telefone(), 1); //Parametro: Telefone(model) e idCliente


            /** GERENCIAR ENDERECOS **/

            //ListarEnderecos();
            //BuscarEndereco(1); //Parametro: idCliente
            //AtualizarEndereco(new Endereco(), 1); //Parametros: Endereco(model) e idCliente
        }

        /** ~~~~~~~~~~ METODOS FILMES ~~~~~~~~~~ **/

        /// <summary>
        /// Método da VIEW para chamada da controller de listagem de filmes
        /// </summary>
        private static void ListarFilmes()
        {
            //Instancia a controller de filme
            FilmeController controller = new FilmeController();

            //Recupera a lista de filmes
            var lstFilme = controller.Listar();

            //Laço de repetição para navegar na lista de filmes
            foreach (var item in lstFilme)
            {
                //Imprime em tela os dados do filme
                Console.WriteLine($"ID: {item.Id} || Titulo: {item.Titulo} || Lancamento:{item.Lancamento} || Sinopse: {(item.Sinopse != null ? item.Sinopse : "")} || Genero Cinematografico: {item.GeneroCinematografico.Genero} || Classificacao Indicativa: {item.ClassificacaoIndicativa.Indicacao} - {item.ClassificacaoIndicativa.Descricao}");
            }
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de cadastrar um filme
        /// </summary>
        private static void InserirFilme(Filme registro)
        {
            //Populando registro de filme (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            registro.Titulo = "Titanic";
            registro.Lancamento = 1997;
            registro.Sinopse = "Um artista pobre e uma jovem rica se conhecem e se apaixonam na fatídica jornada do Titanic, em 1912. Embora esteja noiva do arrogante herdeiro de uma siderúrgica, a jovem desafia sua família e amigos em busca do verdadeiro amor.";
            registro.IdGeneroCinematografico = 13;
            registro.IdClassificacaoIndicativa = 3;

            //Instancia a controller de filme
            FilmeController controller = new FilmeController();

            //Insere o registro de filme na base de dados
            controller.Inserir(registro);
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de pesquisar um filme
        /// </summary>
        private static void BuscarFilme(int id)
        {
            //Instancia a controller de filme
            FilmeController controller = new FilmeController();

            //Recupera o filme
            var filme = controller.Buscar(id);

            //Imprime em tela os dados do filme
            Console.WriteLine($"ID: {filme.Id} || Titulo: {filme.Titulo} || Lancamento:{filme.Lancamento} || Sinopse: {(filme.Sinopse != null ? filme.Sinopse : "")} || Genero Cinematografico: {filme.GeneroCinematografico.Genero} || Classificacao Indicativa: {filme.ClassificacaoIndicativa.Indicacao} - {filme.ClassificacaoIndicativa.Descricao}");
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de atualizar dados de um filme
        /// </summary>
        private static void AtualizarFilme(Filme registro)
        {
            //Populando registro de filme (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            registro.Id = 1;
            registro.Titulo = "Madagascar";
            registro.Lancamento = 2005;
            registro.Sinopse = "O leão Alex é a grande atração do zoológico do Central Park, em Nova York. Ele e seus melhores amigos, a zebra Marty, a girafa Melman e a hipopótamo Glória, sempre passaram a vida em cativeiro e desconhecem o que é morar na natureza.";
            registro.IdGeneroCinematografico = 9;
            registro.IdClassificacaoIndicativa = 1;

            //Instancia a controller de filme
            FilmeController controller = new FilmeController();

            //Atualiza o registro de filme na base de dados
            controller.Atualizar(registro);
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de excluir um filme
        /// </summary>
        private static void ExcluirFilme(int id)
        {
            //Instancia a controller de filme
            FilmeController controller = new FilmeController();

            //Exclui o registro de filme na base de dados
            controller.Excluir(id);
        }

        /// <summary>
        /// Método da VIEW para chamada da controller para gerar arquivos XML e CSV com dados dos filmes
        /// </summary>
        private static void GerarArquivosFilmes()
        {
            //Instancia a controller de arquivo
            ArquivoController controller = new ArquivoController();

            //Recupera o arquivo XML
            controller.GerarArquivoXML();

            //Recupera o arquivo CSV
            controller.GerarArquivoCSV();
        }

        /** ~~~~~~~~~~ METODOS ESTOQUE ~~~~~~~~~~ **/

        /// <summary>
        /// Método da VIEW para chamada da controller de listagem de estoque
        /// </summary>
        private static void ListarEstoque()
        {
            //Instancia a controller de estoque
            EstoqueController controller = new EstoqueController();

            //Recupera a lista de filmes em estoque
            var lstEstoque = controller.Listar();

            //Laço de repetição para navegar na lista de filmes em estoque
            foreach (var item in lstEstoque)
            {
                //Imprime em tela a quantidade de filmes em estoque
                Console.WriteLine($"IdEstoque: {item.Id} || Filme:{item.Filme.Titulo} || Quantidade: {item.Quantidade}");
            }
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de cadastrar quantidade de um filme no estoque
        /// </summary>
        private static void CadastrarEstoque(int quantidade, int idFilme)
        {
            //Instancia a controller de estoque
            EstoqueController estoqueController = new EstoqueController();

            //Insere o registro de estoque na base de dados
            estoqueController.Inserir(quantidade, idFilme);
        }

        /// <summary>
        /// Método da VIEW para chamada da controller para pesquisar a quantidade de um filme no estoque
        /// </summary>
        private static void BuscarEstoque(int idFilme)
        {
            //Instancia a controller de estoque
            EstoqueController estoqueController = new EstoqueController();

            var filme = estoqueController.Buscar(idFilme);

            //Imprime em tela a quantidade do filme em estoque
            Console.WriteLine($"IdEstoque: {filme.Id} || Filme:{filme.Filme.Titulo} || Quantidade: {filme.Quantidade}");
        }

        /// <summary>
        /// Método da VIEW para chamada da controller atualizar a quantidade de um filme no estoque
        /// </summary>
        private static void AtualizarEstoque(int quantidade, int idFilme)
        {
            //Instancia a controller de estoque
            EstoqueController estoqueController = new EstoqueController();

            //Atualiza o registro de estoque na base de dados
            estoqueController.Atualizar(quantidade, idFilme);
        }

        /// <summary>
        /// Método da VIEW para chamada da controller excluir o estoque de um filme
        /// </summary>
        private static void ExcluirEstoque(int idFilme)
        {
            //Instancia a controller de estoque
            EstoqueController estoqueController = new EstoqueController();

            //Exclui o registro de estoque na base de dados
            estoqueController.Excluir(idFilme);
        }

        /** ~~~~~~~~~~ METODOS CLIENTES ~~~~~~~~~~ **/

        /// <summary>
        /// Método da VIEW para chamada da controller de listagem de clientes
        /// </summary>
        private static void ListarClientes()
        {
            //Instancia a controller de cliente
            ClienteController controller = new ClienteController();

            //Recupera a lista de clientes
            var lstCliente = controller.Listar();

            //Laço de repetição para navegar na lista de clientes
            foreach (var item in lstCliente)
            {
                //Imprime em tela os dados do cliente
                Console.WriteLine($"ID: {item.Id} || Nome: {item.Nome} || CPF:{item.Cpf} || E-mail: {item.Email} || Telefones: Celular - {item.Telefone.Celular} {(item.Telefone.Residencial != null ? ", Residencial - " + item.Telefone.Residencial : "" )} {(item.Telefone.Comercial != null ? "Comercial - " + item.Telefone.Comercial : "")} {(item.Telefone.Recado != null ? "Recado - " + item.Telefone.Recado : "")} || Endereco: {item.Endereco.Logradouro}, {item.Endereco.Bairro}. {item.Endereco.Cep}, {item.Endereco.Cidade}-{item.Endereco.Uf}");
            }
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de cadastrar um cliente
        /// </summary>
        private static void CadastrarCliente(Endereco endereco, Telefone telefone, Cliente cliente)
        {
            // !!!!!! AO INSERIR UM CLIENTE DEVE-SE CADASTRAR UM TELEFONE E UM ENDERECO !!!!!!
            //Populando registro de endereco do cliente (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            endereco.Logradouro = "Av. Castanheiras Rua 123 Lote 01";
            endereco.Bairro = "Aguas Claras";
            endereco.Cidade = "Brasilia";
            endereco.Uf = "DF";
            endereco.Cep = "00000000";

            //Instancia a controller de endereco
            EnderecoController enderecoController = new EnderecoController();

            //Insere o registro de endereco na base de dados e retorna o id para cadastro em cliente
            cliente.IdEndereco = enderecoController.Inserir(endereco);

            //Populando registro de telefone do cliente (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            telefone.Residencial = "";
            telefone.Celular = "61999999999";
            telefone.Comercial = "";
            telefone.Recado = "61988888888";

            //Instacia a controller de telefone
            TelefoneController telefoneController = new TelefoneController();
            cliente.IdTelefone = telefoneController.Inserir(telefone);

            if (cliente.IdTelefone != 0 && cliente.IdEndereco != 0)
            {

                //Populando registro de cliente (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
                cliente.Nome = "Joao Pereira";
                cliente.Cpf = "00000000000";
                cliente.Email = "joao.pereira@email.com.br";

                //Instancia a controller de filme
                ClienteController controller = new ClienteController();

                //Insere o registro de filme na base de dados
                controller.Inserir(cliente);
            } else
            {
                Console.WriteLine("Nao foi possivel cadastrar o cliente. Verifique os dados e tente novamente!");
            }
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de pesquisar um cliente
        /// </summary>
        private static void BuscarCliente(int id)
        {
            //Instancia a controller de filme
            ClienteController controller = new ClienteController();

            //Recupera o filme
            var cliente = controller.Buscar(id);

            //Imprime em tela os dados do filme
            Console.WriteLine($"ID: {cliente.Id} || Nome: {cliente.Nome} || CPF:{cliente.Cpf} || E-mail: {cliente.Email} || Telefones: Celular - {cliente.Telefone.Celular} {(cliente.Telefone.Residencial != null ? ", Residencial - " + cliente.Telefone.Residencial : "")} {(cliente.Telefone.Comercial != null ? "Comercial - " + cliente.Telefone.Comercial : "")} {(cliente.Telefone.Recado != null ? "Recado - " + cliente.Telefone.Recado : "")} || Endereco: {cliente.Endereco.Logradouro}, {cliente.Endereco.Bairro}. {cliente.Endereco.Cep}, {cliente.Endereco.Cidade}-{cliente.Endereco.Uf}");
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de atualizar dados de um filme
        /// </summary>
        private static void AtualizarCliente(Endereco endereco, Telefone telefone, Cliente cliente)
        {
            //Populando registro de filme (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            cliente.Id = 1;
            cliente.Nome = "Jose Ferreira";
            cliente.Cpf = "11111111111";
            cliente.Email = "jose.ferreira@gmail.com";

            //Instancia a controller de filme
            ClienteController controller = new ClienteController();

            //Atualiza o registro de filme na base de dados
            controller.Atualizar(cliente);

            if (endereco != null)
            {
                AtualizarEndereco(endereco, cliente.IdEndereco);
            }

            if(telefone != null)
            {
                AtualizarTelefone(telefone, cliente.Id);
            }
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de excluir um filme
        /// </summary>
        private static void ExcluirCliente(int id)
        {
            //Instancia a controller de filme
            ClienteController controller = new ClienteController();

            //Exclui o registro de filme na base de dados
            controller.Excluir(id);
        }

        /** ~~~~~~~~~~ METODOS TELEFONE ~~~~~~~~~~ **/

        /// <summary>
        /// Método da VIEW para chamada da controller de listagem de telefones
        /// </summary>
        private static void ListarTelefones()
        {
            //Instancia a controller de telefone
            TelefoneController controller = new TelefoneController();

            //Recupera a lista de telefones
            var lstTelefone = controller.Listar();

            //Laço de repetição para navegar na lista de telefones
            foreach (var item in lstTelefone)
            {
                //Instancia a controller de cliente
                ClienteController clienteController = new ClienteController();

                //Recupera o cliente pelo idTelefone
                item.Cliente = clienteController.BuscarPeloTelefone(item.Id);

                //Imprime em tela os telefones do cliente
                Console.WriteLine($"ID: {item.Id} || Cliente: {item.Cliente.Nome} || Residencial: {(item.Residencial != null ? item.Residencial : "")} || Celular: {item.Celular} || Comercial: {(item.Comercial != null ? item.Comercial : "")} || Recado: {(item.Recado != null ? item.Recado : "")}");
            }
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de pesquisar o telefone do cliente
        /// </summary>
        private static void BuscarTelefone(int idCliente)
        {
            //Instancia a controller de telefone
            TelefoneController controller = new TelefoneController();

            //Recupera o telefone do cliente
            var telefone = controller.BuscarPorId(idCliente);

            //Imprime em tela os dados de telefone do cliente
            Console.WriteLine($"ID: {telefone.Id} || Residencial: {(telefone.Residencial != null ? telefone.Residencial : "")} || Celular: {telefone.Celular} || Comercial: {(telefone.Comercial != null ? telefone.Comercial : "")} || Recado: {(telefone.Recado != null ? telefone.Recado : "")}");
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de atualizar telefone do cliente
        /// </summary>
        private static void AtualizarTelefone(Telefone telefone, int idCliente)
        {
            //Populando registro de id do cliente (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            idCliente = 1;

            //Populando registro de telefone do cliente (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            telefone.Residencial = "";
            telefone.Celular = "61977777777";
            telefone.Comercial = "";
            telefone.Recado = "61966666666";

            //Instacia a controller de telefone
            TelefoneController telefoneController = new TelefoneController();

            //Atualiza o telefone do cliente
            telefoneController.Atualizar(telefone, idCliente);
        }

        /** ~~~~~~~~~~ METODOS ENDERECO ~~~~~~~~~~ **/

        /// <summary>
        /// Método da VIEW para chamada da controller de listagem de enderecos
        /// </summary>
        private static void ListarEnderecos()
        {
            //Instancia a controller de endereco
            EnderecoController controller = new EnderecoController();

            //Recupera a lista de enderecos
            var lstEndreco = controller.Listar();

            //Laço de repetição para navegar na lista de enderecos
            foreach (var item in lstEndreco)
            {
                //Instancia a controller de cliente
                ClienteController clienteController = new ClienteController();

                //Recupera o cliente pelo idEndereco
                item.Cliente = clienteController.BuscarPeloEndereco(item.Id);

                //Imprime em tela os enderecos do cliente
                Console.WriteLine($"ID: {item.Id} || Cliente: {item.Cliente.Nome} || Logradouro: {item.Logradouro} || Bairro: {item.Bairro} || Cidade: {item.Cidade} || UF: {item.Uf} || CEP: {item.Cep}");
            }
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de pesquisar o endereco do cliente
        /// </summary>
        private static void BuscarEndereco(int idCliente)
        {
            //Instancia a controller de endereco
            EnderecoController controller = new EnderecoController();

            //Recupera o endereco do cliente
            var endereco = controller.BuscarPorId(idCliente);

            //Imprime em tela os dados de endereco do cliente
            Console.WriteLine($"ID: {endereco.Id} || Logradouro: {endereco.Logradouro} || Bairro: {endereco.Bairro} || Cidade: {endereco.Cidade} || UF: {endereco.Uf} || CEP: {endereco.Cep}");
        }

        /// <summary>
        /// Método da VIEW para chamada da controller de atualizar endereco do cliente
        /// </summary>
        private static void AtualizarEndereco(Endereco endereco, int idCliente)
        {
            //Populando registro de id do cliente (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            idCliente = 1;

            //Populando registro de endereco do cliente (ESSE REGISTRO PODE SER POPULADO PELO USUÁRIO EM TELA)
            endereco.Logradouro = "Av. Araucarias Rua 321 Lote 10";
            endereco.Bairro = "Aguas Claras";
            endereco.Cidade = "Brasilia";
            endereco.Uf = "DF";
            endereco.Cep = "11111111";

            //Instancia a controller de endereco
            EnderecoController enderecoController = new EnderecoController();
            
            //Atualiza o endereco do cliente
            enderecoController.Atualizar(endereco, idCliente);
        }
    }
}