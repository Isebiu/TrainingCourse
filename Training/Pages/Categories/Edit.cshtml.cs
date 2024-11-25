using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.Data;
using Training.Model;

namespace Training.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public Category Category { get; set; }


        public EditModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.Categories.Find(id);
            //Category = _db.Categories.SingleOrDefault(o=>o.Id == id);
            // Category = _db.Categories.FirstOrDefault(c => c.Id == id);
            // Category = _db.Categories.Where(c => c.Id == id).FirstOrDefault();

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }

    }
}
