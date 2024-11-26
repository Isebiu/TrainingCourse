using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.Models;


namespace Training.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public List<Category> Categories { get; set; }
        public IndexModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Categories = _db.Categories.ToList();
        }
    }
}
