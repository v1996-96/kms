using System.Data.Common;

namespace kms.Data
{
    public interface IKMSDBConnection
    {
        DbConnection Connection { get; }
    }
}
