using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Training.DataAccess.Data;
using Training.DataAccess.Repository;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public FoodType FoodType { get; set; }

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                _unitOfWork.FoodType.Add(FoodType);
                _unitOfWork.Save();
                TempData["success"] = "Food Type created succesfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
