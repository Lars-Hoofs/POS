using PekingGarden.Domain;
using PekingGarden.Logic.Services;
using PekingGarden.Tests.Fakes;

namespace PekingGarden.Tests.Services;

[TestClass]
public class TableServiceTests
{
    [TestMethod]
    public void GetDiningTables_ReturnsTablesFromRepository()
    {
        List<DiningTable> tables = new List<DiningTable>
        {
            new DiningTable(1, "1"),
            new DiningTable(2, "2"),
        };

        TableService service = new TableService(new FakeTableRepository(tables));
        
        List<DiningTable> result = service.GetDiningTables();
        
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("1", result[0].Name);
        Assert.AreEqual("2", result[1].Name);
    }
}