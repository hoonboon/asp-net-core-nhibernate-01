using Core_WebMVC_NHibernate_01.Orm.Impl;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Core_WebMVC_NHibernate_01.Orm
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(
            this IServiceCollection services, string connectionString, bool isDevelopmentEnv)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.Driver<SqlClientDriver>();
                c.ConnectionString = connectionString;
                c.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.BatchSize = 50;
                c.Timeout = 20;
                c.AutoCommentSql = false;

                if (isDevelopmentEnv)
                {
                    c.SchemaAction = SchemaAutoAction.Create;
                    c.LogFormattedSql = true;
                    c.LogSqlInConsole = true;
                }
                else
                {
                    c.SchemaAction = SchemaAutoAction.Validate;
                    c.LogFormattedSql = false;
                    c.LogSqlInConsole = false;
                }
            });
            configuration.AddMapping(domainMapping);

            NHibernateLogger.SetLoggersFactory(new NLogLoggerFactory());

            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped<INHSession, NHSession>();

            return services;
        }

    }
}
