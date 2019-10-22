using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Controller
{
    public class ArquivoController
    {

        /// <summary>
        /// Gera arquivo em XML
        /// </summary>
        public void GerarArquivoXML()
        {
            //Informa o local e nome do arquivo a ser gerado
            string arquivoXML = @"C:\Temp\Filmes.xml";

            //Instancia objeto controller de filme
            FilmeController controller = new FilmeController();
            List<Filme> lstFilmes = controller.Listar();

            //Efetua a montagem das informações do arquivo XML (via LINQ) com os nós (ELEMENT) e atributos (ATTRIBUTE)
            var docXML = new XDocument(new XElement("Filmes",
                lstFilmes.Select(x => new XElement("Filme",
                                       new XAttribute("Id", x.Id),
                                       new XAttribute("Titulo", x.Titulo),
                                       new XAttribute("Lancamento", x.Lancamento),
                                       new XAttribute("Sinopse", x.Sinopse),
                                       new XAttribute("Genero_Cinematografico", x.GeneroCinematografico.Genero),
                                       new XAttribute("Classificacao_Indicativa", x.ClassificacaoIndicativa.Indicacao + " - " + x.ClassificacaoIndicativa.Descricao )))));

            //Salva o arquivo XML no local indicado
            docXML.Save(arquivoXML);
        }

        /// <summary>
        /// Gera arquivo em CSV
        /// </summary>
        public void GerarArquivoCSV()
        {
            //Informa o local e nome do arquivo a ser gerado
            string arquivoCSV = @"C:\Temp\Filmes.csv";

            //Cria o arquivo CSV no local indicado
            using (StreamWriter writer = new StreamWriter(arquivoCSV, false, Encoding.UTF8))
            {
                //Monta o cabeçalho do arquivo
                writer.WriteLine("ID;TITULO;LANCAMENTO;SINOPSE;GENERO_CINEMATOGRAFICO;CLASSIFICACAO_INDICATIVA");

                //Instancia objeto controller de filme
                FilmeController controller = new FilmeController();

                //Navega na lista de Filme
                foreach (var item in controller.Listar())
                {
                    //Escreve as informações dos filmes
                    writer.WriteLine($"{item.Id};{item.Titulo};{item.Lancamento};{item.Sinopse};{item.GeneroCinematografico};{item.ClassificacaoIndicativa.Indicacao + " - " + item.ClassificacaoIndicativa.Descricao}");
                }
            }
        }
    }
}
