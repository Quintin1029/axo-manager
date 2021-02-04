using System;
using Microsoft.Data.Sqlite;

public class DBConnector
{

    private string _query;

	public DBConnector(string query)
	{
        _query = query;
    }

    public Object ExecuteScalar()
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = Library.DB_ADDR; //May need to change this later

        using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
        {
            connection.Open();

            Object ret = null;

            using (var transaction = connection.BeginTransaction())
            {
                var insertCmd = connection.CreateCommand();

                insertCmd.CommandText = _query;
                ret = insertCmd.ExecuteScalar();
                transaction.Commit();
            }

            return ret;
        }
    }
}
