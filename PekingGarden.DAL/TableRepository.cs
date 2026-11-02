using PekingGarden.Domain;
using PekingGarden.Logic.Interfaces;

namespace PekingGarden.DAL;

public class TableRepository : ITableRepository
{
    public List<DiningTable> GetTables()
    {
        return new List<DiningTable>()
        {
            new DiningTable(1, "1"),
            new DiningTable(2, "2"),
            new DiningTable(3, "3")
        };
    }
}