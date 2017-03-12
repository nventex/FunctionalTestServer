using Dapper;

namespace FunctionalTestServer.Application.Common
{
    public static class SqlBuilderExtensions
    {
        public static SqlBuilder AppendWhereWhen(this SqlBuilder builder, string sql, bool predicate) => predicate ? builder.Where(sql) : builder;
    }
}