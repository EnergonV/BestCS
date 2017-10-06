using System;
using System.Data.OracleClient;
using DbConsoleService; 

namespace OracleClient
{
    public class Provider : IDbConsoleProvider
    {
        #region IDbConsoleProvider Members

        public System.Data.IDbCommand CreateCommand(System.Data.IDbConnection _Connection, System.Data.IDbTransaction _Transaction, string _SQL)
        {
            return new OracleCommand(_SQL, (OracleConnection)_Connection, (OracleTransaction)_Transaction);
        }

        public System.Data.IDbCommand CreateCommand(System.Data.IDbConnection _Connection, string _SQL)
        {
            return new OracleCommand(_SQL, (OracleConnection)_Connection);
        }

        public System.Data.IDbConnection OpenConnection(string _ConnectionString)
        {
            return new OracleConnection(_ConnectionString);
        }
        
        public EProvider ProviderType(){return EProvider.Oracle;}

        #endregion
    }
}
