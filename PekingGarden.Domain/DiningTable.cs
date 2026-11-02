namespace PekingGarden.Domain;

public class DiningTable
{
    private int Id { get; }
    public string Name { get; }

    public DiningTable(int id, string name)
    {
        Id = id;
        Name = name;
    }
}