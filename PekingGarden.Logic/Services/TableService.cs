using PekingGarden.Domain;

namespace PekingGarden.Logic.Services;

public class TableService
{
    public List<DiningTable> GetDiningTables()
    {
        return new List<DiningTable>
        {
            new DiningTable(1, "1"),
            new DiningTable(2, "2"),
            new DiningTable(3, "3")
        };
    }    
}