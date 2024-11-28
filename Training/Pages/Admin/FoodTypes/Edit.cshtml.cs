using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.Models;

namespace Training.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public FoodType FoodType { get; set; }
        public EditModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            FoodType = _db.FoodTypes.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                _db.Update(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"]="Food Type updated succesfully!";
                return RedirectToPage("Index");

            }
            return Page();
        }
    }
}
