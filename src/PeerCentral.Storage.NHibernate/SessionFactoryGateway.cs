using System.Configuration;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PeerCentral.Storage.NHibernate.Domain;
using Configuration = NHibernate.Cfg.Configuration;

namespace PeerCentral.Storage.NHibernate
{
    public class SessionFactoryGateway
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(ProductionDBConfig() ?? DefaultDBConfig())
                .Mappings(m => m.AutoMappings.Add(CreateAutomappings))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static IPersistenceConfigurer DefaultDBConfig()
        {
            return SQLiteConfiguration
                .Standard
                .UsingFile(@"C:\temp\peercentral.sqlite")
                .ShowSql();
        }

        private static IPersistenceConfigurer ProductionDBConfig()
        {
            var connString = ConfigurationManager.AppSettings["MYSQL_CONNECTION_STRING"];

            return connString == null
                       ? null
                       : MySQLConfiguration.Standard.ConnectionString(connString);
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }

        public static AutoPersistenceModel CreateAutomappings()
        {
            return AutoMap
                .AssemblyOf<User>()
                .Where(t => t.Namespace.EndsWith("Domain"))
                .Override<Brag>(b => b.References<User>(r => r.Author))
                .Override<User>(u => u.HasMany<Brag>(r => r.Braggings).KeyColumn("Author_id"));
        }
    }
}