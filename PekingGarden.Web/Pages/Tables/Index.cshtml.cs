using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PekingGarden.Domain;

namespace PekingGarden.Web.Pages.Tables;

public class IndexModel : PageModel
{
    public List<DiningTable> TableNumbers { get; set; } = new();
    public void OnGet()
    {
        TableNumbers = new List<DiningTable>
        {
            new DiningTable(1, "1"),
            new DiningTable(2, "2"),
            new DiningTable(3, "3")
        };
    }
}
