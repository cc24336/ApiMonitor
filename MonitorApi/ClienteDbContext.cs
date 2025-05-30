using Microsoft.EntityFrameworkCore;

namespace APIClienteVesp
{
    public class ClienteDbContext: DbContext
    {
        //Construtor da classe filha
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options): base(options){

        }

        //Propriedade para criar a tabela ClienteTabela no SQLServer
        public DbSet<Cliente> ClienteTabela { get; set; } 
    }
}