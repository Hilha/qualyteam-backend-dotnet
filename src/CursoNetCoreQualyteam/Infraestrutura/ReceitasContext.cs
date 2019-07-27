using CursoNetCoreQualyteam.Dominio;
using Microsoft.EntityFrameworkCore;

namespace CursoNetCoreQualyteam.Infraestrutura
{
    public class ReceitasContext: DbContext
    {
        public DbSet<Receita> Receitas { get; set; }
        public ReceitasContext(DbContextOptions<ReceitasContext> options): base(options){
            
        }
    }
}