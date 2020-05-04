using GenericCrud.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GenericCrud.Databases
{
    public class AbstractApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IOptions<SqlOption> _sqlOption;

        public AbstractApplicationDbContext(IOptions<SqlOption> sqlOption)
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