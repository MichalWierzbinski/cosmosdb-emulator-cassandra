using Cassandra;
using Cassandra.Mapping;
using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace CassandraQuickStartSample
{
    public class Program
    {
        // Cassandra Cluster Configs
        // Keyspace is your database name, table will be a container.
        // That's how Cosmos DB maps your entities.
        private const string KeyspaceName = "cosmoscassandra";

        private const string UserName = "localhost";

        private const string Password = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private const string CassandraContactPoint = "localhost";  // DnsName

        private static readonly int CassandraPort = 10350;

        public static void Main(string[] args)
        {
            // Connect to cassandra cluster  (Cassandra API on Azure Cosmos DB supports only TLSv1.2)
            var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);
            options.SetHostNameResolver((ipAddress) => CassandraContactPoint);
            Cluster cluster = Cluster.Builder().WithCredentials(UserName, Password).WithPort(CassandraPort).AddContactPoint(CassandraContactPoint).WithSSL(options).Build();
            ISession session = cluster.Connect();

            // Creating KeySpace and table
            session.Execute($"DROP KEYSPACE IF EXISTS {KeyspaceName}");
            session.Execute($"CREATE KEYSPACE {KeyspaceName} WITH REPLICATION = " + "{ 'class' : 'NetworkTopologyStrategy', 'datacenter1' : 1 };");
            Console.WriteLine($"created keyspace {KeyspaceName}");
            session.Execute($"CREATE TABLE IF NOT EXISTS {KeyspaceName}.user (user_id int PRIMARY KEY, user_name text, user_bcity text)");
            Console.WriteLine(String.Format("created table user"));

            session = cluster.Connect($"{KeyspaceName}");
            IMapper mapper = new Mapper(session);

            // Inserting Data into user table
            mapper.Insert<User>(new User(1, "LyubovK", "Dubai"));
            mapper.Insert<User>(new User(2, "JiriK", "Toronto"));
            mapper.Insert<User>(new User(3, "IvanH", "Mumbai"));
            mapper.Insert<User>(new User(4, "LiliyaB", "Seattle"));
            mapper.Insert<User>(new User(5, "JindrichH", "Buenos Aires"));
            Console.WriteLine("Inserted data into user table");

            Console.WriteLine("Select ALL");
            Console.WriteLine("-------------------------------");
            foreach (User user in mapper.Fetch<User>("Select * from user"))
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("Getting by id 3");
            Console.WriteLine("-------------------------------");
            User userId3 = mapper.FirstOrDefault<User>("Select * from user where user_id = ?", 3);
            Console.WriteLine(userId3);

            // Clean up of Table and KeySpace
            session.Execute("DROP table user");
            session.Execute($"DROP KEYSPACE {KeyspaceName}");

            // Wait for enter key before exiting
            Console.ReadLine();
        }

        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
    }
}