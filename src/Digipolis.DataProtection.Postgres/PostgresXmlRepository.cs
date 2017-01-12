using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Digipolis.DataProtection.Postgres
{
    /// <summary>
    /// An XML repository backed by a Postgres database.
    /// </summary>
    internal class PostgresXmlRepository : IXmlRepository
    {
        private readonly Func<DataContext> _dbContextFactory;
        private readonly DataContext _dbContext;
        private readonly Guid _appIdentifier;
        private readonly Guid _instanceIdentifier;

        public PostgresXmlRepository(Func<DataContext> dbContextFactory, Guid appIdentifier, Guid instanceIdentifier)
        {
            if (dbContextFactory == null) throw new ArgumentNullException(nameof(dbContextFactory));
            if (appIdentifier == null || appIdentifier == Guid.Empty) throw new ArgumentException(nameof(dbContextFactory));
            if (instanceIdentifier == null || appIdentifier == Guid.Empty) throw new ArgumentException(nameof(dbContextFactory));

            _dbContextFactory = dbContextFactory;
            _appIdentifier = appIdentifier;
            _instanceIdentifier = instanceIdentifier;

            _dbContext = _dbContextFactory();
        }

        /// <inheritdoc />
        public IReadOnlyCollection<XElement> GetAllElements()
        {
            return GetAllElementsCore().ToList().AsReadOnly();
        }

        private IEnumerable<XElement> GetAllElementsCore()
        {
            var keys = _dbContext.KeyCollections.AsNoTracking().Where(x => x.AppId == _appIdentifier).Select(x => x.Value);

            foreach (var value in keys)
            {
                yield return XElement.Parse(value);
            }
        }

        /// <inheritdoc />
        public void StoreElement(XElement element, string friendlyName)
        {
            var keyCollection = new KeyValuesCollection
            {
                AppId = _appIdentifier,
                Value = element.ToString(SaveOptions.DisableFormatting),
                Timestamp = DateTime.Now,
                InstanceId = _instanceIdentifier
            };

            _dbContext.KeyCollections.Add(keyCollection);
            _dbContext.SaveChanges();
        }
    }
}
