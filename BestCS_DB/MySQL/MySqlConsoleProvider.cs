using System;
using System.Data.SqlClient;
using DbConsoleService;


namespace MySqlProvider
{
    public class Provider : DbConsoleService.IDbConsoleProvider
    {
        #region IDbConsoleProvider Members

        public System.Data.IDbCommand CreateCommand(System.Data.IDbConnection _Connection, System.Data.IDbTransaction _Transaction, string _SQL)
        {
            return new MySqlCommand(_SQL, (MySqlConnection)_Connection, (MySqlTransaction)_Transaction);
        }

        public System.Data.IDbCommand CreateCommand(System.Data.IDbConnection _Connection, string _SQL)
        {
            return new MySqlCommand(_SQL, (MySqlConnection)_Connection);
        }

        public System.Data.IDbConnection OpenConnection(string _ConnectionString)
        {
            return new MySqlConnection(_ConnectionString);
        }
        
        public EProvider ProviderType(){return EProvider.MySql;}

        #endregion
    }
}
