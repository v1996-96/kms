using Dapper.FastCrud;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace kms.Data
{
    public class KMSDBConnection : IDisposable, IKMSDBConnection
    {
        private DbConnection _connection;
        public DbConnection Connection {
            get { return this._connection; }
        }

        public KMSDBConnection(string connectionString) {
            OrmConfiguration.DefaultDialect = SqlDialect.PostgreSql;
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            this._connection = new NpgsqlConnection(connectionString);
            this._connection.Open();
        }

        public void Dispose() {
            this._connection.Dispose();
        }
    }
}
