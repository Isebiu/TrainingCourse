using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int? id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        }
        public async Task<IActionResult> OnPost()
        {

            var categFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == Category.Id);
            if (categFromDb != null)
            {
                _unitOfWork.Category.Remove(categFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted succesfully!";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
