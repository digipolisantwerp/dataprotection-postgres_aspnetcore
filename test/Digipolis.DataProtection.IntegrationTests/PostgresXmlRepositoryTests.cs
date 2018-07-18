using Digipolis.DataProtection.Postgres;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Digipolis.DataProtection.IntegrationTests
{
    public class PostgresXmlRepositoryTests
    {
        private readonly Guid _appId;
        private readonly Guid _instanceId;

        public PostgresXmlRepositoryTests()
        {
            _appId = Guid.NewGuid();
            _instanceId = Guid.NewGuid();
        }

        [Fact]
        public void TestConfiguration()
        {
            var connectionString = "Host=postgres;Database=dataprotection;Username=postgres";
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddDataProtection()
                .PersistKeysToPostgres(connectionString, _appId, _instanceId);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var dataProtector = serviceProvider.GetDataProtector("sample");
            dataProtector.Protect("Helloworld");

            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseNpgsql(connectionString);
            var dbContextOptions = builder.Options;
            var dataContext = new DataContext(dbContextOptions);

            var keyCollection =
                dataContext.KeyCollections.FirstOrDefault(kc => kc.AppId == _appId && kc.InstanceId == _instanceId);

            Assert.NotNull(keyCollection);
        }
    }
}


