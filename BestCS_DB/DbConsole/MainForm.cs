using System;
using System.Data; 
using System.Drawing; 
using System.Collections.Generic;   
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using DbConsoleService;



namespace DbConsole
{	
     public partial class MainForm : Form
    {
        Dictionary<string, IDbConsoleProvider> m_Providers;
        DbConsoleManager m_Manager;
        ProviderType m_provider;

        public MainForm()
        {
            InitializeComponent();
            m_provider = new ProviderType();                     
            LoadProviders();
            ShowProviders(); 
        }

        
        public void LoadProviders()
        {
            if (m_Providers == null)
                m_Providers = new Dictionary<string, IDbConsoleProvider>();
            else
                m_Providers.Clear();

            string _ErrorMessage = string.Empty; 
        
            foreach (string _ProviderLocation in Directory.GetFiles(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "*.dll"))
                try
                {
                    Assembly _Assembly;
                    _Assembly = Assembly.LoadFrom(_ProviderLocation);

                    foreach (Type _Type in _Assembly.GetTypes())
                    {
                        if (_Type.GetInterface("DbConsoleService.IDbConsoleProvider") == typeof(IDbConsoleProvider))
                            m_Providers.Add(_Type.FullName, (IDbConsoleProvider)Activator.CreateInstance(_Type));
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = _ErrorMessage + ex.Message + Environment.NewLine;  
                }

            if (_ErrorMessage.Length > 0)
                ShowErrorMessage(_ErrorMessage, "Загрузка провайдеров");   
        }

        public void ShowProviders()
        {
            if (m_Providers == null)
                return;

            ListViewProviders.Items.Clear();

            foreach (KeyValuePair<string, IDbConsoleProvider> _Pair in m_Providers)
            {
                ListViewItem _Item;

                switch(_Pair.Key)
                {
                    case "SqlClient.Provider":
                        _Item = ListViewProviders.Items.Add("Провайдер Sql");
                        _Item.Tag = _Pair.Value;
                        _Item.Checked = false;                 
                        continue;
                    case "OracleClient.Provider":
						_Item = ListViewProviders.Items.Add("Провайдер Oracle");
                        _Item.Tag = _Pair.Value;
                        _Item.Checked = false;                  
                        continue;
                    case "OleDbClient.Provider":                        
						_Item = ListViewProviders.Items.Add("Провайдер OleDb");
                        _Item.Tag = _Pair.Value;
                        _Item.Checked = false;                   
                        continue;
                    case "OdbcClient.Provider":
						_Item = ListViewProviders.Items.Add("Провайдер Odbc");
                        _Item.Tag = _Pair.Value;
                        _Item.Checked = false;              
                        continue;
                    case "MySqlProvider.Provider":
						_Item = ListViewProviders.Items.Add("Провайдер MySql");
                        _Item.Tag = _Pair.Value;
                        _Item.Checked = false;                
                        continue;
                    case "PostgreSqlProvider.Provider":
						_Item = ListViewProviders.Items.Add("Провайдер PostgreSql");
                        _Item.Tag = _Pair.Value;
                        _Item.Checked = false;                    
                        continue;
                    default:
                        _Item = ListViewProviders.Items.Add(_Pair.Key);
                        _Item.Tag = _Pair.Value;
                        _Item.Checked = false;
                        break;
                }        				 
 
            }
        }

        private void ValidateExecuteButtonState()
        {
            ExecuteButton.Enabled = (m_Manager != null) && (textBoxSQL.Text.Trim().Length > 0); 
        }

        private void ShowErrorMessage(string _Message, string _Caption)
        {
            MessageBox.Show(_Message, _Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);        
        }

        private void ShowInformationMessage(string _Message, string _Caption)
        {
            MessageBox.Show(_Message, _Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CloseManager()
        {
            if (m_Manager == null)
                return;

            try
            {
                m_Manager.Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Закрытие соединения");
            }
            finally
            {
                m_Manager = null;
            }

            ClearData(); 
            ConnectionStatusLabel.Text = string.Empty;
            BeginButton.Enabled = false;
            CommitButton.Enabled = false;
            RollbackButton.Enabled = false;
            ExecuteButton.Enabled = false;
        }

        private bool OpenManager(IDbConsoleProvider _Provider)
        {
            MyConnectionStringForm _constrForm;

            if (m_provider.MySqlProvider)
            {
                _constrForm = new MySqlConnectionStringForm();
           
            }
            else if (m_provider.PostgreSqlProvider)
            {
                _constrForm =new PostgreSqlConnectionStringForm();
             
            }
            
            else  _constrForm = new ConnectionStringForm();            

            _constrForm.ShowDialog();  

            if (!_constrForm.IsOk())
                return false;

            m_Manager = new DbConsoleManager(_Provider);

            try
            {
            	m_Manager.Open(_constrForm.ConnectionString());
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Открытие соединения");
                m_Manager = null;
                return false;
            }

            ShowInformationMessage(string.Format("Соединение с '{0}' выполнено.", _constrForm.ConnectionString()), "Открытие соединения");
            ConnectionStatusLabel.Text = _constrForm.ConnectionString();
            BeginButton.Enabled = true;
            ValidateExecuteButtonState();

            return true;
        }

        public void ClearData()
        {
            ListViewDataSet.Clear();
            SQLStatusLabel.Text = string.Empty; 
        }

        public void ShowData(IDataReader Reader)
        {
            ListViewDataSet.Clear();

            int _Result = 0;
            bool _CreateColumns = true;
            while (Reader.Read())
            {
                //Создание колонок в списке
                if (_CreateColumns)
                {
                    ListViewDataSet.Columns.Add("").Width = 20;

                    for (int i = 0; i <= Reader.FieldCount - 1; i++)
                        ListViewDataSet.Columns.Add(Reader.GetName(i));

                    _CreateColumns = false;
                }

                ListViewItem NewItem;
                NewItem = ListViewDataSet.Items.Add("");

                for (int i = 0; i <= Reader.FieldCount - 1; i++)
                    NewItem.SubItems.Add(Reader.GetValue(i).ToString());

                _Result++;
            }

            ListViewDataSet.AlignColumns();  
            SQLStatusLabel.Text = string.Format("Выбрано {0} записей.", _Result);
        }

        private void ListViewProviders_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CloseManager();

            if (!e.Item.Checked)
                return;

            foreach (ListViewItem _Item in ListViewProviders.Items)
            {
                if (_Item != e.Item)
                    _Item.Checked = false;

                if(_Item == e.Item)
                {
                    switch (_Item.Text)
                    {
                        case "Провайдер Sql":
                            m_provider.SqlProvider = true;
                            if (!OpenManager((IDbConsoleProvider)e.Item.Tag))
                            {
                                e.Item.Checked = false;
                                m_provider.SqlProvider = false;
                            }
                            break;

                        case "Провайдер Oracle":
                            m_provider.OracleProvider = true;
                            if (!OpenManager((IDbConsoleProvider)e.Item.Tag))
                            {
                                e.Item.Checked = false;
                                m_provider.OracleProvider = false;
                            }
                            break;

                        case "Провайдер OleDb":
                            m_provider.OleDbProvider = true;
                            if (!OpenManager((IDbConsoleProvider)e.Item.Tag))
                            {
                                e.Item.Checked = false;
                                m_provider.OleDbProvider = false;
                            }
                            break;

                        case "Провайдер Odbc":
                            m_provider.OdbcProvider = true;
                            if (!OpenManager((IDbConsoleProvider)e.Item.Tag))
                            {
                                e.Item.Checked = false;
                                m_provider.OdbcProvider = false;
                            }
                            break;

                        case "Провайдер PostgreSql":
                            m_provider.PostgreSqlProvider = true;
                            if (!OpenManager((IDbConsoleProvider)e.Item.Tag))
                            {
                                e.Item.Checked = false;
                                m_provider.PostgreSqlProvider = false;
                            }
                            break;

                        case "Провайдер MySql":
                            m_provider.MySqlProvider = true;
                            if (!OpenManager((IDbConsoleProvider)e.Item.Tag))
                            {
                                e.Item.Checked = false;
                                m_provider.MySqlProvider = false;
                            }
                            break;

                        default:
                            m_provider.UndefiniteProvider = true;
                            if (!OpenManager((IDbConsoleProvider)e.Item.Tag))
                            {
                                e.Item.Checked = false;
                                m_provider.UndefiniteProvider = false;
                            }                     
                            break;

                    }

                }

            }    
   
        }

        private void textBoxSQL_TextChanged(object sender, EventArgs e)
        {
            ValidateExecuteButtonState();
        }

        private void BeginButton_Click(object sender, EventArgs e)
        {
            try
            {
                m_Manager.BeginTransaction(); 
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Начало транзакции");
                return;
            }

            BeginButton.Enabled = false; 
            CommitButton.Enabled = true;
            RollbackButton.Enabled = true; 
        }

        private void CommitButton_Click(object sender, EventArgs e)
        {
            try
            {
                m_Manager.CommitTransaction(); 
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Завершение транзакции");
                return;
            }

            BeginButton.Enabled = true;
            CommitButton.Enabled = false;
            RollbackButton.Enabled = false; 
        }

        private void RollbackButton_Click(object sender, EventArgs e)
        {
            try
            {
                m_Manager.RollbackTransaction(); 
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Отмена транзакции");
                return;
            }

            BeginButton.Enabled = true;
            CommitButton.Enabled = false;
            RollbackButton.Enabled = false; 
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            string _SQL = textBoxSQL.Text;

            try
            {
                if (_SQL.StartsWith("select", StringComparison.OrdinalIgnoreCase))
                    ShowData(m_Manager.Select(_SQL));
                else
                {
                    ClearData(); 
                    SQLStatusLabel.Text = string.Format("Обработано {0} записей.", m_Manager.Execute(_SQL));
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Выполнение SQL запроса.");
                return;
            }
        }

    }

    public class DoubleBufferedListView : ListView
    {
        public DoubleBufferedListView()
        {
            this.DoubleBuffered = true;
        }

        public void AlignColumns()
        {
            foreach (ColumnHeader _Column in this.Columns)
                _Column.Width = GetColumnGetColumnPreferredWidth(this, _Column); 
        }

        public static int GetColumnGetColumnPreferredWidth(ListView _ListView, ColumnHeader _Column)
        {
            Label _Label = new Label();
            _Label.Font = _ListView.Font;
            _Label.Text = _Column.Text;

            int _PreferredWidth = _Label.PreferredWidth;

            foreach (ListViewItem _Item in _ListView.Items)
            {
                _Label.Text = _Item.SubItems[_Column.Index].Text;

                if (_Label.PreferredWidth > _PreferredWidth)
                    _PreferredWidth = _Label.PreferredWidth;
            }

            if (_PreferredWidth == 0)
                return 20;

            _PreferredWidth = _PreferredWidth + 10;

            if (_Column.Index == 0)
                _PreferredWidth = _PreferredWidth + 10;

            return _PreferredWidth;
        }

    }
}
