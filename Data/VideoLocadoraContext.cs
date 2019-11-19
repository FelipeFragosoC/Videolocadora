using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VideoLocadora.Models
{
    public class VideoLocadoraContext : DbContext
    {
        public VideoLocadoraContext(DbContextOptions<VideoLocadoraContext> options)
            : base(options)
        {
        }

        public DbSet<ClassificacaoIndicativa> ClassificacaoIndicativa { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Dependente> Dependente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Filme> Filme { get; set; }
        public DbSet<GeneroCinematografico> GeneroCinematografico { get; set; }
        public DbSet<Historico> Historico { get; set; }
        public DbSet<Locacao> Locacao { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<RelLocacaoFilme> RelLocacaoFilme { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
    }
}
