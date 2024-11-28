using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;


namespace Training.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //Category = _db.Categories.SingleOrDefault(o=>o.Id == id);
            // Category = _db.Categories.FirstOrDefault(c => c.Id == id);
            // Category = _db.Categories.Where(c => c.Id == id).FirstOrDefault();

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(Category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated succesfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }

    }
}
