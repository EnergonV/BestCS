using System;
using System.Data; 

namespace DbConsoleService
{
	
	public enum EProvider
	{
	Sql,
	MySql,
	PostgreSql,
	Odbc,
	Oracle,
	OleDb,
	Undefinite = 0	
	}
	
    public interface IDbConsoleProvider
    {

        IDbConnection OpenConnection(string _ConnectionString);
        IDbCommand CreateCommand(IDbConnection _Connection, string _SQL);
        IDbCommand CreateCommand(IDbConnection _Connection, IDbTransaction _Transaction, string _SQL);
        EProvider ProviderType();
    }

    public class DbConsoleManager
    {
        IDbConnection m_Connection;
        IDbTransaction m_Transaction;
        IDbConsoleProvider m_Provider;

        public DbConsoleManager(IDbConsoleProvider _Provider) { m_Provider = _Provider; }

        //Открытие соединения
        public void Open(string _ConnectionString)
        {
            m_Connection = m_Provider.OpenConnection(_ConnectionString);   
            m_Connection.Open();
        }
        //Закрытие соединения
        public void Close()
        {
            m_Connection.Close();
        }
        //Инициализация новой транзакции
        public void BeginTransaction()
        {
            m_Transaction = m_Connection.BeginTransaction();
        }
        //Завершение транзакции с сохранением изменений
        public void CommitTransaction()
        {
            m_Transaction.Commit();
            m_Transaction = null;
        }
        //Завершение транзакции с отменой всех изменений
        public void RollbackTransaction()
        {
            m_Transaction.Rollback();
            m_Transaction = null;
        }
        //Получение данных
        public IDataReader Select(string SQL)
        {
            return this.CreateCommand(SQL).ExecuteReader();
        }
        //Обновление данных
        public int Execute(string SQL)
        {
            return this.CreateCommand(SQL).ExecuteNonQuery();
        }
        //Выполнение SQL инструкции
        private IDbCommand CreateCommand(string _SQL)
        {
            if (m_Transaction == null)
                return m_Provider.CreateCommand(m_Connection, _SQL);

            return m_Provider.CreateCommand(m_Connection, m_Transaction, _SQL);
        }
    }
}
