using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace FunctionalTestServer.Application.Data
{
    public abstract class RepositoryBase
    {
        protected static SqlConnection GetConnection()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)?.Replace(@"file:\", string.Empty);

            var mdfFile = Path.Combine(path, "Data", "FunctionalTestServer.mdf");

            var connection =
                new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={mdfFile};Integrated Security=True");

            return connection;
        }
    }
}