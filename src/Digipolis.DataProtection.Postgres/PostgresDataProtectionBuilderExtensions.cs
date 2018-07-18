using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Digipolis.DataProtection.Postgres
{
    /// <summary>
    /// Contains Postgres-specific extension methods for modifying a <see cref="IDataProtectionBuilder"/>.
    /// </summary>
    public static class PostgresDataProtectionBuilderExtensions
    {
        /// <summary>
        /// Configures the data protection system to persist keys in Postgres database
        /// </summary>
        /// <param name="builder">The builder instance to modify.</param>
        /// <param name="connectionString">The connection string to the backing Postgres database.</param>
        /// <param name="appId">An identifier for the application.</param>
        /// <param name="instanceId">An identifier for the instance.</param>
        /// <returns></returns>
        public static IDataProtectionBuilder PersistKeysToPostgres(this IDataProtectionBuilder builder, string connectionString, Guid appId, Guid instanceId)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (String.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }

            if (appId == Guid.Empty)
            {
                throw new ArgumentException(nameof(appId));
            }

            return PersistKeysToPostgresInternal(builder, connectionString, appId, instanceId);
        }

        private static IDataProtectionBuilder PersistKeysToPostgresInternal(IDataProtectionBuilder config, string connectionString, Guid appId, Guid instanceId)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseNpgsql(connectionString);
            var dbContextOptions = builder.Options;

            config.Services.Configure<KeyManagementOptions>(options =>
            {
                options.XmlRepository =
                    new PostgresXmlRepository(() => new DataContext(dbContextOptions), appId, instanceId);
            });
            return config;
        }
    }
}
