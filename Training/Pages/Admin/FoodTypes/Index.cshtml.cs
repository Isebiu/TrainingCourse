using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.Models;

namespace Training.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public List<FoodType> FoodTypes { get; set; }
        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            FoodTypes = _db.FoodTypes.ToList();
        }
    }
}
