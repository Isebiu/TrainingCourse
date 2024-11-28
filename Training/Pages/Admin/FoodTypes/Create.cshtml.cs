using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Training.DataAccess.Data;
using Training.Models;

namespace Training.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public FoodType FoodType { get; set; }

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (FoodType.Name.Length > 32)
            {
                ModelState.AddModelError(FoodType.Name, "The max number of characters is 32!");
            }
            if (ModelState.IsValid) //Verif daca modelul este valid
            {
                await _db.FoodTypes.AddAsync(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food Type created succesfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
