using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Training.DataAccess.Data;
using Training.Models;

namespace Training.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public FoodType FoodType { get; set; }
        public DeleteModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            FoodType = _db.FoodTypes.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (FoodType != null)
            {
                _db.FoodTypes.Remove(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"]="Food Type deleted succesfully!";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
