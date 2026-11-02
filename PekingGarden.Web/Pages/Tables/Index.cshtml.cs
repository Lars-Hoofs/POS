using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PekingGarden.Domain;
using PekingGarden.Logic.Services;

namespace PekingGarden.Web.Pages.Tables;

public class IndexModel : PageModel
{
    public List<DiningTable> Tables { get; set; } = new();
    public void OnGet()
    {
        TableService service = new TableService();
        Tables = service.GetDiningTables();
    }
}
