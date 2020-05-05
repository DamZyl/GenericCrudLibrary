using System;
using System.Threading.Tasks;
using GenericCrud.Auth;
using Microsoft.EntityFrameworkCore;

namespace GenericCrud.Databases
{
    public abstract class AbstractDatabaseInitializer
    {
        protected readonly IApplicationDbContext _context;
        protected readonly IPasswordHasher _passwordHasher;

        protected AbstractDatabaseInitializer(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task PrepPopulate()
        {
            Console.WriteLine("Appling Migrations...");
            
            await _context.Instance.Database.MigrateAsync();
            await SeedData();
        }

        protected abstract Task SeedData();
    }
}