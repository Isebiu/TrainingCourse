using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using Training.DataAccess.Data;
using Training.Models;

namespace Training.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.Order.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Name != Order");
            }
            if (ModelState.IsValid)
            {

                await _db.Categories.AddAsync(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category created succesfully!";

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
