using System;
using MySqlProvider;

namespace MySqlTest
{	
	class Program
	{
		

		
	public static void Main(string[] args)
		{
		
		// данные соединения
string MySQL_host = "localhost";
string MySQL_port = "3306";
string MySQL_uid = "root";
string MySQL_pw = "admin";

MySqlConnection Connection = new MySqlConnection("Data Source=" + MySQL_host + ";Port=" + MySQL_port + ";User Id=" + MySQL_uid + ";Password=" + MySQL_pw + ";"); // Создаем соединение. Формат строки соединения подробно описан в прилагающейся документации.
MySqlCommand Query = new MySqlCommand(); // С помощью этого объекта выполняются запросы к БД
Query.Connection = Connection; // Присвоим объекту только что созданное соединение
try
{
    Console.WriteLine("Соединяюсь с сервером базы данных...");
    Connection.Open();// Соединяемся
}
catch (MySqlException SSDB_Exception)
{
    // Ошибка - выходим
    Console.WriteLine("Проверьте настройки соединения, не могу соединиться с базой данных!\nОшибка: " + SSDB_Exception.Message);
    return;
}
Console.WriteLine("OK");

//Теперь нам нужно создать новую базу данных и таблицу в ней.
//Создаем базу:
string MySQL_dbname = "mctestdb";
try
{
    Query.CommandText = "CREATE DATABASE " + MySQL_dbname + ";";
    Console.WriteLine("Создание базы данных...");
    Query.ExecuteNonQuery();// Этот вызов отправляет запрос, не требующий чтения данных на выполнение
    Query.CommandText = "USE " + MySQL_dbname + ";";
    Query.ExecuteNonQuery();
}
catch (MySqlException SSDB_Exception)
{
    Console.WriteLine("Ошибка при создании базы данных " + MySQL_dbname + ":\n" + SSDB_Exception.Message);
    return;
}
Console.WriteLine("OK");

//Создаем таблицу:
string MySQL_tbname = "dm_autos";
try
{
    Console.WriteLine("Создание таблицы " + MySQL_tbname + "...");
    Query.CommandText = "CREATE TABLE `" + MySQL_tbname + "` (  `id` mediumint(9) unsigned NOT NULL auto_increment,  `model` tinytext NOT NULL,  `drive` tinytext NOT NULL,  `rudder` tinytext NOT NULL,  `gearbox` tinytext NOT NULL,  PRIMARY KEY  (`id`),  UNIQUE KEY `id` (`id`)) ENGINE=MyISAM;";
    Query.ExecuteNonQuery();
}
catch (MySqlException SSDB_Exception)
{
    Console.WriteLine("Ошибка при создании таблицы " + MySQL_tbname + ":\n" + SSDB_Exception.Message);
    return;
}
Console.WriteLine("OK");

//Итак, база с одной таблицей создана, добавим в нее несколько тестовых значений:
Console.WriteLine("\nДобавление данных...");
string[] models = { "Opel Astra", "Volkswagen Golf", "Honda Civic", "Toyota Corolla" };
string[] drives = { "передний", "передний", "передний", "задний" };
string[] rudders = { "левый", "левый", "левый", "правый" };
string[] gearboxes = { "автомат", "механика", "механика", "автомат" };
int count = models.Length;
for(int i=0; i<count; i++)
{
    Console.WriteLine("- " + models[i]);
    Query.CommandText = "INSERT INTO " + MySQL_tbname + " VALUES(NULL, '" + models[i] + "','" + drives[i] + "','" + rudders[i] + "','" + gearboxes[i] + "');";
    Query.ExecuteNonQuery();
}
Console.WriteLine("Данные добавлены!");

//Прочитаем эти данные:
Console.WriteLine("\nЧтение данных...\nID - Модель авто - Привод - Руль - Коробка передач");
Query.CommandText = "SELECT * FROM " + MySQL_tbname + ";";
MySqlDataReader MyReader = Query.ExecuteReader();// Запрос, подразумевающий чтение данных из таблиц.
while (MyReader.Read())// Читаем
{
    // Каждое значение вытягиваем с помощью MySqlDataReader.GetValue(<номер значения в выборке>)
    Console.WriteLine("{0} - {1} - {2} - {3} - {4}", MyReader.GetValue(0), MyReader.GetValue(1), MyReader.GetValue(2), MyReader.GetValue(3), MyReader.GetValue(4));
}

//Выгружаем ресурсы, закрываем соединение:
MyReader.Close();
Query.Dispose();
Connection.Close();
		}
	}
}