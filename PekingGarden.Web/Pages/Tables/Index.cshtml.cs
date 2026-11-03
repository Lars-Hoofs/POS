using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PekingGarden.Domain;
using PekingGarden.Logic.Services;

namespace PekingGarden.Web.Pages.Tables;

public class IndexModel : PageModel
{
    private readonly TableService tableService;
    public List<DiningTable> Tables { get; set; } = new();

    public IndexModel(TableService tableService)
    {
        this.tableService = tableService;
    }
    public void OnGet()
    {
        Tables = tableService.GetDiningTables();
    }
}
