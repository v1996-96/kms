using System;
using System.Data.Common;
using Npgsql;

namespace kms.Data
{
    public class KMSDBConnection : IDisposable, IKMSDBConnection
    {
        private DbConnection connection;
        public DbConnection Connection
        {
            get
            {
                return this.connection;
            }
        }

        public KMSDBConnection(string connectionString)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            this.connection = new NpgsqlConnection(connectionString);
        }

        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}
