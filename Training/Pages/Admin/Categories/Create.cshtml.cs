using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        //private readonly AppDbContext _db; // nu mai avem nevoie pentru ca o sa folosim repo
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

                _unitOfWork.Category.Add(Category);
                _unitOfWork.Save();
                TempData["success"] = "Category created succesfully!";

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
