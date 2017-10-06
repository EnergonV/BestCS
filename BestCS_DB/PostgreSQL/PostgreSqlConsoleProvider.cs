using System;
using System.Data;
using DbConsoleService;
using Npgsql; 


namespace PostgreSqlProvider
{
    public class Provider : IDbConsoleProvider
    {
        #region IDbConsoleProvider Members

        public System.Data.IDbCommand CreateCommand(System.Data.IDbConnection _Connection, System.Data.IDbTransaction _Transaction, string _SQL)
        {
            return new NpgsqlCommand(_SQL, (NpgsqlConnection)_Connection, (NpgsqlTransaction)_Transaction);
        }

        public System.Data.IDbCommand CreateCommand(System.Data.IDbConnection _Connection, string _SQL)
        {
            return new NpgsqlCommand(_SQL, (NpgsqlConnection)_Connection);
        }

        public System.Data.IDbConnection OpenConnection(string _ConnectionString)
        {
            return new NpgsqlConnection(_ConnectionString);
        }
        
        public EProvider ProviderType(){return EProvider.PostgreSql;}

        #endregion
    }
}
