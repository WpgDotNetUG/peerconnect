using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PeerCentral.Storage.NHibernate.Domain;

namespace PeerCentral.Storage.NHibernate
{
    public class SessionFactoryGateway
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(SQLiteConfiguration
                              .Standard
                              .UsingFile(@"C:\temp\peercentral.sqlite")
                              .ShowSql())
                .Mappings(m => m.AutoMappings.Add(AutoMap
                                                      .AssemblyOf<User>()
                                                      .Where(t => t.Namespace.EndsWith("Domain"))))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, true);
        }
    }
}