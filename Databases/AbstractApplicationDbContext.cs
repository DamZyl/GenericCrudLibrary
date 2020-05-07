using GenericCrud.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GenericCrud.Databases
{
    public abstract class AbstractApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbContext Instance => this;
        private readonly IOptions<SqlOption> _sqlOption;

        protected AbstractApplicationDbContext(IOptions<SqlOption> sqlOption)
        {
            _sqlOption = sqlOption;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlServer(_sqlOption.Value.ConnectionString);
        }
    }
}