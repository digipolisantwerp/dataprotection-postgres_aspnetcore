using Digipolis.DataProtection.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Dataprotection.Postgres.Migrations
{
    internal class DataContextFactory : IDbContextFactory<DataContext>
    {
        private readonly string _connectionString;

        public DataContextFactory()
        {
            _connectionString = @"Server=localhost;Port=5432;Database=DataProtection;User Id=postgres;Password=postgres;";
        }

        public DataContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseNpgsql(_connectionString);
            var dbContextOptions = builder.Options;

            return new DataContext(dbContextOptions);
        }
    }
}
