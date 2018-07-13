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

namespace Digipolis.DataProtection.Tests
{
    public class PostgresXmlRepositoryTests
    {
        private readonly Guid _appId;
        private readonly Guid _instanceId;
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public PostgresXmlRepositoryTests()
        {
            _appId = Guid.NewGuid();
            _instanceId = Guid.NewGuid();

            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(_appId.ToString());
            _dbContextOptions = builder.Options;
        }

        [Fact]
        public void GetAllElements_ReturnsAllXmlValuesForGivenKey()
        {
            //Arrange
            var context = new DataContext(_dbContextOptions);

            context.KeyCollections.AddRange(new KeyValuesCollection { AppId = _appId, Value = "<Element1/>" },
                                            new KeyValuesCollection { AppId = _appId, Value = "<Element2/>" },
                                            new KeyValuesCollection { AppId = Guid.NewGuid(), Value = "<Element3/>" });
            context.SaveChanges();

            var repo = new PostgresXmlRepository(() => context, _appId, _instanceId);

            //Act
            var elements = repo.GetAllElements().ToArray();

            //Assert
            Assert.Equal(2, elements.Count());
            Assert.Equal(new XElement("Element1").ToString(), elements[0].ToString());
            Assert.Equal(new XElement("Element2").ToString(), elements[1].ToString());
        }

        [Fact]
        public void GetAllElements_ThrowsParsingException()
        {
            //Arrange
            var context = new DataContext(_dbContextOptions);

            context.KeyCollections.AddRange(new KeyValuesCollection { AppId = _appId, Value = "<Element1/>" },
                                            new KeyValuesCollection { AppId = _appId, Value = "<Element2" });
            context.SaveChanges();

            var repo = new PostgresXmlRepository(() => context, _appId, _instanceId);

            //Act - Assert
            Assert.Throws<XmlException>(() => repo.GetAllElements());
        }

        [Fact]
        public void StoreElement_PushesValueToList()
        {
            //Arrange
            var context = new DataContext(_dbContextOptions);
            var repo = new PostgresXmlRepository(() => context, _appId, _instanceId);

            //Act
            repo.StoreElement(new XElement("Element2"), null);

            //Assert
            var storedElement = context.KeyCollections.First();

            Assert.Equal(_appId, storedElement.AppId);
            Assert.Equal(_instanceId, storedElement.InstanceId);
            Assert.Equal("<Element2 />", storedElement.Value);
            Assert.True(storedElement.Timestamp >= DateTime.Now.AddMinutes(-1));
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


