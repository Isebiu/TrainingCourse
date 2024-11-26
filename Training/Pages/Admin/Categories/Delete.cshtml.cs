using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.Models;

namespace Training.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            Category = _db.Categories.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {

            var categFromDb = _db.Categories.Find(Category.Id);
            if (categFromDb != null)
            {
                _db.Categories.Remove(categFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category deleted succesfully!";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
