using PekingGarden.Domain;
using PekingGarden.Logic.Interfaces;

namespace PekingGarden.Tests.Fakes;

public class FakeTableRepository : ITableRepository
{
    private readonly List<DiningTable> tables;
    
    public FakeTableRepository(List<DiningTable> tables)
    {
        this.tables = tables;
    }

    public List<DiningTable> GetTables()
    {
        return tables;
    }
}