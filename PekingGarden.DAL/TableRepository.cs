using Microsoft.Data.SqlClient;
using PekingGarden.Domain;
using PekingGarden.Logic.Interfaces;

namespace PekingGarden.DAL;

public class TableRepository : ITableRepository
{
    private readonly string connectionString;
    
    public  TableRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<DiningTable> GetTables()
    {
        List<DiningTable> tables = new List<DiningTable>();

        using SqlConnection connection = new SqlConnection(connectionString);
        using SqlCommand command = new SqlCommand("SELECT Id, Name FROM DiningTable ORDER BY  SortOrder", connection);
        
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            tables.Add(new DiningTable(reader.GetInt32(0), reader.GetString(1)));
        }
        return tables;
    }
}