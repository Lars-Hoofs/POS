using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PekingGarden.Web.Pages.Tables;

public class IndexModel : PageModel
{
    public List<int> TableNumbers { get; set; } = new();
    public void OnGet()
    {
        TableNumbers = new List<int>{1, 2, 3};
    }
}
