using PekingGarden.Domain;

namespace PekingGarden.Logic.Interfaces;

public interface ITableRepository
{
    List<DiningTable> GetTables();
}