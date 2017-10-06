namespace DbConsole
{
	
		public class ProviderType
		    {
		
		        bool m_SqlProvider = false;
		        bool m_MySqlProvider = false;
		        bool m_PostgreSqlProvider = false;
		        bool m_OracleProvider = false;
		        bool m_OdbcProvider = false;
		        bool m_OleDbProvider = false;
		        bool m_UndefiniteProvider = false;
		
		    public bool SqlProvider
		    {
		        get { return m_SqlProvider; }
		        set { m_SqlProvider = value; }
		    }
		
		    public bool MySqlProvider
		    {
		        get { return m_MySqlProvider; }
		        set {  m_MySqlProvider = value; }
		        }
		
		    public bool PostgreSqlProvider
		    {
		        get { return m_PostgreSqlProvider; }
		        set {  m_PostgreSqlProvider = value; }
		        }
		
		    public bool OracleProvider
		    {
		        get { return m_OracleProvider; }
		            set { m_OracleProvider = value; }
		    }
		    public bool OdbcProvider
		    {
		        get { return m_OdbcProvider; }
		            set { m_OdbcProvider = value; }
		    }
		    public bool OleDbProvider
		    {
		        get { return m_OleDbProvider; }
		            set { m_OleDbProvider = value; }
		    }
		    public bool UndefiniteProvider
		    {
		        get { return m_UndefiniteProvider; }
		            set { m_UndefiniteProvider = value; }
		    }
		}
}