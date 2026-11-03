using PekingGarden.Domain;
using PekingGarden.Logic.Interfaces;

namespace PekingGarden.Logic.Services;

public class TableService
{
    private readonly ITableRepository tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        this.tableRepository = tableRepository;
    }

    public List<DiningTable> GetDiningTables()
    {
        return tableRepository.GetTables();
    }
}